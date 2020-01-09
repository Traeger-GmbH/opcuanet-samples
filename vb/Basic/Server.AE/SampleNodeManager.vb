' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Collections.Generic
Imports System.Threading

Imports Opc.UaFx
Imports Opc.UaFx.Server

Namespace AE
    ''' <summary>
    ''' Represents a sample implementation of a custom OpcNodeManager.
    ''' </summary>
    Friend Class SampleNodeManager
        Inherits OpcNodeManager
        '---------- Private fields ----------

        ''' <summary>
        ''' Stores a variable node which Is used to control the "job" processing.
        ''' </summary>
        Private isActiveNode As OpcDataVariableNode(Of Boolean)

        ''' <summary>
        ''' Stores a variable node which Is used to indicate the progress status of the current
        ''' "job" processing including pre- And post-setup stages.
        ''' </summary>
        Private statusNode As OpcDataVariableNode(Of Byte)

        ''' <summary>
        ''' Stores a dialog condition node (an alarm with interaction) used to query clients
        ''' whether to continue with the "next job".
        ''' </summary>
        Private statusChangeNode As OpcDialogConditionNode

        ''' <summary>
        ''' Stores a analog item node used to store the volatile position of a "mechanical" part of
        ''' the "machine" represented by the server.
        ''' </summary>
        Private positionNode As OpcAnalogItemNode(Of Integer)

        ''' <summary>
        ''' Stores a limit alarm node (an alarm with lower And upper bounds) used to notify about
        ''' the reaching of a limit through the position of a "mechanical" part of the "machine".
        ''' </summary>
        Private positionLimitNode As OpcExclusiveLimitAlarmNode

        ''' <summary>
        ''' Stores a variable node which Is used to represent a fictive temperature measured at the
        ''' "mechanical" part of the "machine".
        ''' </summary>
        Private temperatureNode As OpcAnalogItemNode(Of Double)

        ''' <summary>
        ''' Stores a simple alarm node to notify about the reaching of technical supported
        ''' temperature values without to inform about the defined limits using the alarm.
        ''' </summary>
        Private temperatureCriticalNode As OpcAlarmConditionNode

        '---------- Public constructors ----------

        ''' <summary>
        ''' Initializes a new instance of the <see cref="SampleNodeManager"/> class.
        ''' </summary>
        Public Sub New()
            MyBase.New("http://sampleserver/machines")
        End Sub

        '---------- Public methods ----------

        ''' <summary>
        ''' Simulates a continuous running progress which can be ended using the
        ''' <paramref name="semaphore"/> specified.
        ''' </summary>
        ''' <param name="semaphore">The <see cref="SemaphoreSlim"/> which Is used
        ''' to determine whether the simulation Is to be canceled. If it Is released
        ''' the simulation Is cancelled.</param>
        Public Sub Simulate(semaphore As SemaphoreSlim)
            'By default we define each condition as acknowledged, because we will change it 
            ' depending on outcome of the evaluations bound to the alarms.
            Me.positionLimitNode.ChangeIsAcked(Me.SystemContext, True)
            Me.temperatureCriticalNode.ChangeIsAcked(Me.SystemContext, True)

            Dim run = 0
            Dim random As New Random(45)

            Do While (Not semaphore.Wait(1000))
                'Only perform "job"-simulation in case the "machine" Is active.
                If (Not Me.isActiveNode.Value) Then
                    Continue Do
                End If

                Me.SimulatePosition(run, random)
                Me.SimulateTemperature(run, random)
                Me.SimulateStatus(run, random)

                run = run + 1
            Loop
        End Sub

        '---------- Protected methods ----------

        ''' <summary>
        ''' Creates the nodes provided And associated with the node manager.
        ''' </summary>
        ''' <param name="references">A dictionary used to determine the logical references between
        ''' existing nodes (e.g. OPC default nodes) And the nodes provided by the node
        ''' manager.</param>
        ''' <returns>An enumerable containing the root nodes of the node manager.</returns>
        ''' <remarks>This method will be only called once by the server on start up.</remarks>
        Protected Overrides Function CreateNodes(references As OpcNodeReferenceCollection) As IEnumerable(Of IOpcNode)
            'It Is necessary to assign to all root nodes one of the namespaces used to
            'identify one of the associated namespaces (see the ctor of the class). This
            'namespace does identify the node as member of the namespace of the node
            'manager. Optionally it Is possible to assign namespace to the child nodes
            'too. But by default their missing namespace will be auto-completed through the
            'namespace of their parent node.
            Dim machineOne As New OpcFolderNode(Me.DefaultNamespace.GetName("Machine_1"))

            'In case a client requests a condition referesh it queries the current event
            'information which Is gathered using the CreateEvent method from each active
            'And retained alarm nodes.
            machineOne.QueryEventsCallback _
                    = Sub(context, events)
                          'Ensure that an re-entrance upon notifier cross-references will Not add
                          'events to the collection which are already stored in.
                          If (events.Count > 0) Then
                              Return
                          End If

                          If (Me.statusChangeNode.IsRetained) Then
                              events.Add(Me.statusChangeNode.CreateEvent(context))
                          End If

                          If (Me.positionLimitNode.IsRetained) Then
                              events.Add(Me.positionLimitNode.CreateEvent(context))
                          End If

                          If (Me.temperatureCriticalNode.IsRetained) Then
                              events.Add(Me.temperatureCriticalNode.CreateEvent(context))
                          End If
                      End Sub

            'Add New reference to make the node visible beneath the ObjectsFolder
            '(the top most root node within every OPC UA server).
            references.Add(machineOne, OpcObjectTypes.ObjectsFolder)

            Dim nameNode As New OpcDataVariableNode(Of String)(machineOne, "Name", "Machine 1")
            Me.isActiveNode = New OpcDataVariableNode(Of Boolean)(machineOne, "IsActive", True)

            'An alarm node have to be a notifier for another node Or for the whole server.
            'Is a alarm a notifier of another node:
            '-> this node (the notified one) needs to be subscribed by the client to receive
            '   the alarm data.
            'Is a alarm a notifier of the whole server:
            '-> the OpcObjectTypes.Server needs to be subscribed by the client to receive
            '   the alarm data.

            'Machine 1, Status nodes setup
            Me.statusNode = New OpcDataVariableNode(Of Byte)(machineOne, "Status", 1)

            'Define an alarm used to request a dialog which requires a dedicated response
            'action by a client. This kind of node can be used for service / operator tasks.
            'Handle any client response on an active dialog through applying the response
            'using RespondDialog And configuring the dialog as inactive.
            Me.statusChangeNode = New OpcDialogConditionNode(machineOne, "StatusChange") With {
                .AutoReportChanges = True,
                .Message = "Operator requested",
                .Prompt = "The job has been finished, continue with the next one?",
                .ResponseOptions = {"Yes", "No"},
                .DefaultResponse = 0,
                .CancelResponse = 1,
                .OkResponse = 0,
                .RespondCallback _
                    = Function(context, response)
                          Me.isActiveNode.Value = (response = Me.statusChangeNode.OkResponse)
                          Me.isActiveNode.ApplyChanges(context)

                          Me.statusChangeNode.RespondDialog(context, response)

                          Me.statusChangeNode.Message = "No operator required"
                          Me.statusChangeNode.IsRetained = False

                          Return OpcStatusCode.Good
                      End Function
            }

            'Define the alarm as the notifier of the machineOne node.
            machineOne.AddNotifier(Me.SystemContext, Me.statusChangeNode)

            'Machine 1, Position nodes setup
            Me.positionNode = New OpcAnalogItemNode(Of Integer)(machineOne, "Position", -1) With {
                .InstrumentRange = New OpcValueRange(120, 1),
                .EngineeringUnit = New OpcEngineeringUnitInfo(4732211, "mm", "millimetre"),
                .EngineeringUnitRange = New OpcValueRange(Byte.MaxValue)
            }

            'Define an alarm used to indicate the reaching of one Or more limits during
            'a progress. Such limits may be predefined Or progress dependent.
            Me.positionLimitNode = New OpcExclusiveLimitAlarmNode(
                    machineOne, "PositionLimit", OpcLimitAlarmStates.All) With {
                .HighHighLimit = 120, 'e.g. mm
                .HighLimit = 100,     'e.g. mm
                .LowLimit = 5,        'e.g. mm
                .LowLowLimit = 1,     'e.g. mm
                .Message = "No range problems",
                .ReceiveTime = DateTime.UtcNow
            }

            Me.positionLimitNode.AcknowledgeCallback _
                    = Function(context, eventId, comment)
                          Me.positionLimitNode.Message = "Acknowledged with " + comment.Value
                          Return OpcStatusCode.Good
                      End Function

            'Define the alarm as the notifier of the machineOne node.
            machineOne.AddNotifier(Me.SystemContext, Me.positionLimitNode)

            'Machine 1, Temperature nodes setup
            Me.temperatureNode = New OpcAnalogItemNode(Of Double)(machineOne, "Temperature", 18.3)
            Me.temperatureNode.InstrumentRange = New OpcValueRange(80.0, -40.0)
            Me.temperatureNode.EngineeringUnit = New OpcEngineeringUnitInfo(4408652, "°C", "degree Celsius")
            Me.temperatureNode.EngineeringUnitRange = New OpcValueRange(70.8, 5.0)

            'Define an alarm which just indicates the fulfillment of an alarm associated
            'condition. Such simple alarms only notify about the fulfillment without to
            'define additional prerequisites defined by the alarm itself. Much more
            'specialized alarms are subclasses of this type of alarm node.
            Me.temperatureCriticalNode = New OpcAlarmConditionNode(machineOne, "TemperatureCritical")

            'Define the alarm as the notifier of the machineOne node.
            machineOne.AddNotifier(Me.SystemContext, Me.temperatureCriticalNode)

            'Define the alarm as the notifier of the whole Server node.
            Me.AddNotifierNode(Me.temperatureCriticalNode)

            Return New IOpcNode() {machineOne}
        End Function


        '---------- Private methods ----------

        ''' <summary>
        ''' Simulates a progress which influences the <see cref="statusNode"/>,
        ''' <see cref="isActiveNode"/> And publishes alarms using the
        ''' <see cref="statusChangeNode"/>.
        ''' </summary>
        ''' <param name="run">The sequence number of the simulation run within the status
        ''' simulation Is to be performed.</param>
        ''' <param name="random">The <see cref="Random"/> instance used for random number
        ''' generation.</param>
        Private Sub SimulateStatus(run As Integer, random As Random)
            Me.statusNode.Value = CByte(run Mod 20)

            'This will trigger DataChange notification being send to DataChange subscriptions.
            Me.statusNode.ApplyChanges(Me.SystemContext)

            If (Me.statusNode.Value = 45) Then
                Me.isActiveNode.Value = False

                'This will trigger DataChange notification being send to DataChange subscriptions.
                Me.isActiveNode.ApplyChanges(Me.SystemContext)

                Me.statusChangeNode.ReceiveTime = DateTime.UtcNow
                Me.statusChangeNode.Time = DateTime.UtcNow

                Me.statusChangeNode.Message = "Operator requested"
                Me.statusChangeNode.IsRetained = True

                Me.statusChangeNode.ActivateDialog(Me.SystemContext)

                'This will trigger Event notification being send to Event subscriptions.
                Me.statusChangeNode.ReportEventFrom(
                        Me.SystemContext, Me.statusNode)
            End If
        End Sub

        ''' <summary>
        ''' Simulates a progress which influences the <see cref="positionNode"/> And publishes
        ''' alarms using the <see cref="positionLimitNode"/>.
        ''' </summary>
        ''' <param name="run">The sequence number of the simulation run within the position
        ''' simulation Is to be performed.</param>
        ''' <param name="random">The <see cref="Random"/> instance used for random number
        ''' generation.</param>
        Private Sub SimulatePosition(run As Integer, random As Random)
            If (Me.positionNode.Value = -1) Then
                Me.positionLimitNode.ChangeLimitState(
                        Me.SystemContext, OpcLimitAlarmStates.Inactive)
            End If

            Dim ackRequired = (Me.positionLimitNode.ReceiveTime.AddSeconds(45) < DateTime.UtcNow)

            If (Not Me.positionLimitNode.IsActive Or (Not ackRequired Or Me.positionLimitNode.IsAcked)) Then
                Dim positionValue = random.Next(
                        CInt(Math.Truncate(Me.positionLimitNode.LowLowLimit - run Mod 3)),
                        CInt(Math.Truncate(Me.positionLimitNode.HighHighLimit + run Mod 7)))

                Me.positionNode.Value = positionValue

                'This will trigger DataChange notification being send to DataChange subscriptions.
                Me.positionNode.ApplyChanges(Me.SystemContext)

                Dim severity = OpcEventSeverity.Low
                Dim limits = OpcLimitAlarmStates.Inactive

                Dim message = "No range problems"

                If (positionValue <= Me.positionLimitNode.LowLowLimit) Then
                    limits = OpcLimitAlarmStates.LowLow
                    message = "Out of lower bound range!"
                    severity = OpcEventSeverity.Medium
                ElseIf (positionValue <= Me.positionLimitNode.LowLimit) Then
                    limits = OpcLimitAlarmStates.Low
                    message = "About to reach lower bound!"
                    severity = OpcEventSeverity.MediumHigh
                ElseIf (positionValue >= Me.positionLimitNode.HighLimit) Then
                    limits = OpcLimitAlarmStates.High
                    message = "About to reach upper bound!"
                    severity = OpcEventSeverity.MediumHigh
                ElseIf (positionValue >= Me.positionLimitNode.HighHighLimit) Then
                    limits = OpcLimitAlarmStates.HighHigh
                    message = "Out of upper bound range!"
                    severity = OpcEventSeverity.High
                End If

                Me.positionLimitNode.ChangeSeverity(Me.SystemContext, severity)
                Me.positionLimitNode.ChangeLimitState(Me.SystemContext, limits)

                If (Me.positionLimitNode.IsActive) Then
                    Me.positionLimitNode.Time = DateTime.UtcNow

                    If (ackRequired) Then
                        Me.positionLimitNode.Message = message + " - Acknowledgement is required!"

                        Me.positionLimitNode.ChangeIsAcked(Me.SystemContext, False)
                        Me.positionLimitNode.ChangeIsConfirmed(Me.SystemContext, False)

                        Me.positionLimitNode.ReceiveTime = DateTime.UtcNow
                    End If
                End If

                'This will trigger Event notification being send to Event subscriptions.
                Me.positionLimitNode.ReportEventFrom(
                            Me.SystemContext, Me.positionNode)
            End If
        End Sub

        ''' <summary>
        ''' Simulates a progress which influences the <see cref="temperatureNode"/> And publishes
        ''' alarms using the <see cref="temperatureCriticalNode"/>.
        ''' </summary>
        ''' <param name="run">The sequence number of the simulation run within the temperature
        ''' simulation Is to be performed.</param>
        ''' <param name="random">The <see cref="Random"/> instance used for random number
        ''' generation.</param>
        Private Sub SimulateTemperature(run As Integer, random As Random)
            Dim temperatureValue = random.Next(12, 20 * (((run Mod 7) \ 4) + 1))
            Me.temperatureNode.Value = temperatureValue

            'This will trigger DataChange notification being send to DataChange subscriptions.
            Me.temperatureNode.ApplyChanges(Me.SystemContext)

            If (temperatureValue <= 20) Then
                Me.temperatureCriticalNode.ChangeIsActive(Me.SystemContext, False)
            Else
                Dim message = "The temperature is higher than 20°C!"
                Dim severity = OpcEventSeverity.Low

                If (temperatureValue <= 25) Then
                    severity = OpcEventSeverity.Medium
                ElseIf (temperatureValue <= 30) Then
                    message = "The temperature is near to 30°C!"
                    severity = OpcEventSeverity.MediumHigh
                ElseIf (temperatureValue <= 35) Then
                    severity = OpcEventSeverity.High
                Else
                    message = "The temperature is near to 40°C!"
                    severity = OpcEventSeverity.Max
                End If

                Me.temperatureCriticalNode.Message = message

                Me.temperatureCriticalNode.ReceiveTime = DateTime.UtcNow
                Me.temperatureCriticalNode.Time = DateTime.UtcNow

                Me.temperatureCriticalNode.ChangeSeverity(Me.SystemContext, severity)
                Me.temperatureCriticalNode.ChangeIsActive(Me.SystemContext, True)

                'This will trigger Event notification being send to Event subscriptions.
                Me.temperatureCriticalNode.ReportEventFrom(
                        Me.SystemContext, Me.temperatureNode)
            End If
        End Sub
    End Class
End Namespace
