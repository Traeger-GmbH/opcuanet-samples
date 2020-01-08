' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Threading
Imports Microsoft.VisualBasic
Imports Opc.UaFx
Imports Opc.UaFx.Client

Namespace AE
    ''' <summary>
    ''' This sample demonstrates how to perform handling of Alarm + Events (AE) on the nodes
    ''' provided by the OPC UA server.
    ''' </summary>
    Public Class Program
        '---------- Private static fields ----------

        ''' <summary>
        ''' Stores the namespace information used by the client to access the server.
        ''' </summary>
        Private Shared ReadOnly ServerNamespace As OpcNamespace _
                = OpcNamespace.Get(2, "http://sampleserver/machines")

        ''' <summary>
        ''' Stores the node identifier of the root node of the machine node the client operate on.
        ''' </summary>
        Private Shared ReadOnly machineId As OpcNodeId = ServerNamespace.GetId("Machine_1")

        ''' <summary>
        ''' Stores the semaphore used to determine a ctrl+c input to exit the application.
        ''' </summary>
        Private Shared ReadOnly cancelSemaphore As New SemaphoreSlim(0)

        ''' <summary>
        ''' Stores a sequence of the simple storage objects used to simplify the sample code.
        ''' </summary>
        Private Shared ReadOnly nodes As New ObservedNodeCollection()


        '---------- Public static methods ----------

        Public Shared Sub Main(args As String())
            '''' If the server domain name does not match localhost just replace it
            '''' e.g. with the IP address or name of the server machine.

            Dim client As New OpcClient("opc.tcp://localhost:4840/SampleServer")
            client.Connect()

            Dim positionNode = nodes.Add(client, machineId, "Position")
            Dim temperatureNode = nodes.Add(client, machineId, "Temperature")
            Dim statusNode = nodes.Add(client, machineId, "Status")

            Dim severity As New OpcSimpleAttributeOperand(OpcEventTypes.Event, "Severity")
            Dim sourceName As New OpcSimpleAttributeOperand(OpcEventTypes.Event, "SourceName")

            ' Construct the filter to use for event subscriptions...
            ' ... define the types of events to include in the filter
            ' this will automatically add all properties defined by
            ' the types of events specified ...
            ' ... restrict the event information received from the server
            ' by specifying the types of events And ...
            ' ... maybe additional conditions to fulfill, Like the required severity
            ' using the severity operand defined above Like follows:
            '     severity > OpcEventSeverity.Medium
            ' ... these conditions can then enhanced logical operators Like follows:
            '     & sourceName.Like("Limit")
            ' ... Finally Using Select() will query all the necessary Event
            ' information required And will create the event filter setup with the
            ' givent event types, constraints And additional selection fields.
            Dim filter = OpcFilter.Using(client) _
                    .FromEvents(
                        OpcEventTypes.AlarmCondition,
                        OpcEventTypes.ExclusiveLimitAlarm,
                        OpcEventTypes.DialogCondition) _
                    .Where(
                        OpcFilterOperand.OfType(OpcEventTypes.AlarmCondition) _
                        Or OpcFilterOperand.OfType(OpcEventTypes.ExclusiveLimitAlarm) _
                        Or OpcFilterOperand.OfType(OpcEventTypes.DialogCondition)) _
                    .Select()

            ' All in one Subscription
            ' Subscribe to data changes (value changes committed using ApplyChanges(...)
            ' calls) on the status, position And temperature node. Also subscribe to all
            ' events reported through notifiers assigned to the machine node. Each of
            ' these subscribe tasks represent a single monitored item instance which Is used
            ' to define the reporting characteristics to use. The so defined monitored items
            ' are then maintained by one single subscription.
            Dim subscription = client.SubscribeNodes(
                    New OpcSubscribeDataChange(statusNode.Id, AddressOf HandleDataChanges),
                    New OpcSubscribeDataChange(positionNode.Id, AddressOf HandleDataChanges),
                    New OpcSubscribeDataChange(temperatureNode.Id, AddressOf HandleDataChanges),
                    New OpcSubscribeEvent(machineId, filter, AddressOf HandleDataEvents))

            ' In case there the client Is interested in the current event information (which
            ' Is Not explicitly re-published through the server when a subscription Is
            ' created) the client have to query a "ConditionRefresh" to query the latest
            ' event information known by the server. In general the server does Not need to
            ' hold something Like a history of events.
            subscription.RefreshConditions()

            ' Everyone in its own Subscription
            '    Dim eventsSubscription = client.SubscribeEvent(machineId, filter, HandleDataEvents)
            '    eventsSubscription.RefreshConditions()

            '    ' In case there only following events need to be known, the "ConditionRefresh" can
            '    ' be omitted And the subscription variable can be removed:
            '    ' client.SubscribeEvent(machineId, filter, HandleDataEvents);

            '    ' The following created subscription do Not need to trigger a "ConditionRefresh",
            '    ' because in general only event information may be important And the current
            '    ' value can be just read, too.
            '    client.SubscribeDataChange(statusNode.Id, HandleDataChanges);
            '    client.SubscribeDataChange(positionNode.Id, HandleDataChanges);
            '    client.SubscribeDataChange(positionNode.Id, HandleDataChanges);
            '    client.SubscribeDataChange(temperatureNode.Id, HandleDataChanges);

            ' Handle global (server-wide) events
            Dim conditionName = New OpcSimpleAttributeOperand(OpcEventTypes.Condition, "ConditionName")

            Dim globalFilter = OpcFilter.Using(client) _
                    .FromEvents(OpcEventTypes.AlarmCondition) _
                    .Where(severity > OpcEventSeverity.Medium And conditionName.Like("Temperature")) _
                    .Select()

            client.SubscribeEvent(OpcObjectTypes.Server, globalFilter, AddressOf HandleGlobalEvents)

            AddHandler Console.CancelKeyPress, Sub(sender, e) cancelSemaphore.Release()

            Do
                UpdateConsole(client)
            Loop While (Not cancelSemaphore.Wait(1000))

            client.Disconnect()
        End Sub


        '---------- Private static methods ----------

        ''' <summary>
        ''' Handles the <see cref="OpcMonitoredItem.DataChangeReceived"/> event.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The event data.</param>
        Private Shared Sub HandleDataChanges(sender As Object, e As OpcDataChangeReceivedEventArgs)
            ' In general change handling can be implemented here, too. The approach of this sample
            ' takes use of the nodes collection to summarize the received information to process
            ' it all in one run to simplify the handling of the console out- and input.
            Dim nodeId = CType(sender, OpcMonitoredItem).NodeId.ToString(OpcNodeIdFormat.Foundation)

            If (nodes.Contains(nodeId)) Then
                nodes(nodeId).Value = e.Item.Value
            End If
        End Sub

        ''' <summary>
        ''' Handles the <see cref="OpcMonitoredItem.EventReceived"/> event using node events.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The event data.</param>
        Private Shared Sub HandleDataEvents(sender As Object, e As OpcEventReceivedEventArgs)
            ' In general event handling can be implemented here, too. The approach of this sample
            ' takes use of the nodes collection to summarize the received information to process
            ' it all in one run to simplify the handling of the console out- and input.
            Dim nodeId = e.Event.SourceNodeId.ToString(OpcNodeIdFormat.Foundation)

            If (Not nodeId Is Nothing) Then
                If (nodes.Contains(nodeId)) Then
                    nodes(nodeId).EventData = e.Event
                End If
            End If
        End Sub

        ''' <summary>
        ''' Handles the <see cref="OpcMonitoredItem.EventReceived"/> event using global events.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The event data.</param>
        Private Shared Sub HandleGlobalEvents(sender As Object, e As OpcEventReceivedEventArgs)
            Console.Title = e.Event.Message
        End Sub

        ''' <summary>
        ''' Determines the according <see cref="ConsoleColor"/> to use for the
        ''' <paramref name="severity"/> specified.
        ''' </summary>
        ''' <param name="severity">The numeric expression of the severity for that the according
        ''' color is to be determined.</param>
        ''' <returns>The <see cref="ConsoleColor"/> to use to represent the output of the
        ''' <paramref name="severity"/> specified.</returns>
        Private Shared Function SeverityToColor(ByVal severity As OpcEventSeverity) As ConsoleColor
            If (severity <= OpcEventSeverity.Min) Then
                Return ConsoleColor.White
            End If

            If (severity <= OpcEventSeverity.Low) Then
                Return ConsoleColor.Green
            End If

            If (severity <= OpcEventSeverity.Medium) Then
                Return ConsoleColor.Yellow
            End If

            If (severity <= OpcEventSeverity.MediumHigh) Then
                Return ConsoleColor.DarkYellow
            End If

            If (severity <= OpcEventSeverity.High) Then
                Return ConsoleColor.Red
            End If

            Return ConsoleColor.Magenta
        End Function

        ''' <summary>
        ''' Updates the whole console output by first clearing it all out and determining the
        ''' necessary information to write and read.
        ''' </summary>
        ''' <param name="client">The <see cref="OpcClient"/> to use in cases there are user inputs
        ''' required for which the client is used to communicate with the server.</param>
        Private Shared Sub UpdateConsole(client As OpcClient)
            Console.Clear()
            Console.SetCursorPosition(0, 0)
            Console.WriteLine("Monitoring Alarm & Events...")

            For Each node In nodes
                Console.Write(node.Label)
                WriteValue(node)

                Console.Write(Constants.vbTab)
                WriteEvent(node)

                Console.WriteLine()
            Next

            For Each node In nodes
                Dim ackCondition = TryCast(node.EventData, OpcAcknowledgeableCondition)

                If (Not ackCondition Is Nothing) Then
                    QueryAcknowledgment(client, ackCondition)
                End If

                Dim dialogCondition = TryCast(node.EventData, OpcDialogCondition)

                If (Not dialogCondition Is Nothing) Then
                    QueryDialog(client, dialogCondition)
                End If
            Next
        End Sub

        ''' <summary>
        ''' Queries an acknowledgment by the user of the <paramref name="client"/>.
        ''' </summary>
        ''' <param name="client">The <see cref="OpcClient"/> used to acknowledge the
        ''' <paramref name="condition"/> using the user input as the comment.</param>
        ''' <param name="condition">The <see cref="OpcAcknowledgeableCondition"/> identifying the
        ''' event information for which the acknowledgement is to be performed.</param>
        Private Shared Sub QueryAcknowledgment(
                client As OpcClient,
                condition As OpcAcknowledgeableCondition)
            If (condition.IsAcked) Then
                Return
            End If

            Console.WriteLine()
            Console.WriteLine($"Acknowledgment is required for condtion: {condition.ConditionName}")
            Console.WriteLine($"  -> {condition.Message}")
            Console.Write("Enter your acknowlegment comment and press Enter to acknowledge: ")

            Dim comment = Console.ReadLine()
            condition.Acknowledge(client, comment)
        End Sub

        ''' <summary>
        ''' Queries a dialog to the user of the <paramref name="client"/>.
        ''' </summary>
        ''' <param name="client">The <see cref="OpcClient"/> used to forward the respond on
        ''' the dialog using the user input as the selected response.</param>
        ''' <param name="condition">The <see cref="OpcDialogCondition"/> identifying the event
        ''' information required to handle the dialog.</param>
        Private Shared Sub QueryDialog(
                client As OpcClient,
                condition As OpcDialogCondition)
            If (Not condition.IsActive) Then
                Return
            End If

            Console.WriteLine()
            Console.WriteLine(condition.Prompt)

            Console.WriteLine("    Options:")

            Dim responseOptions = condition.ResponseOptions

            For index = 0 To responseOptions.Length - 1
                Console.Write($"      [{index}] = {responseOptions(index).Value}")

                If (index = condition.DefaultResponse) Then
                    Console.Write(" (default)")
                End If

                Console.WriteLine()
            Next

            Dim respond = String.Empty
            Dim respondOption = condition.DefaultResponse

            Do
                Console.Write("Enter the number of the option and press Enter to respond: ")
                respond = Console.ReadLine()

                If (String.IsNullOrEmpty(respond)) Then
                    Exit Do
                End If
            Loop While (Not Int32.TryParse(respond, respondOption))

            condition.Respond(client, respondOption)
        End Sub

        ''' <summary>
        ''' Writes the <see cref="ObservedNode.EventData"/> information of the <paramref name="node"/>
        ''' specified to the console.
        ''' </summary>
        ''' <param name="node">The <see cref="ObservedNode"/> its event information is to
        ''' be written.</param>
        Private Shared Sub WriteEvent(node As ObservedNode)
            Dim eventData = node.EventData

            If (Not eventData Is Nothing) Then
                Dim conditionData = TryCast(node.EventData, OpcCondition)

                If (conditionData Is Nothing Or conditionData.IsRetained) Then
                    Console.ForegroundColor = SeverityToColor(eventData.Severity)
                Else
                    Console.ForegroundColor = ConsoleColor.DarkGray
                End If

                Dim eventTime = eventData.Time.ToLocalTime()
                Dim eventText = eventData.Message

                Console.Write($"{eventTime.ToString("HH:mm:ss.fff")} - {eventText}")
                Console.ResetColor()
            End If
        End Sub

        ''' <summary>
        ''' Writes the <see cref="ObservedNode.Value"/> and <see cref="ObservedNode.Unit"/>
        ''' information of the <paramref name="node"/> specified to the console.
        ''' </summary>
        ''' <param name="node">The <see cref="ObservedNode"/> its value and unit information is to
        ''' written.</param>
        Private Shared Sub WriteValue(node As ObservedNode)
            If (Not node.Value Is Nothing) Then
                Console.ForegroundColor = ConsoleColor.White
                Console.Write(node.Value)

                If (node.Unit Is Nothing) Then
                    Console.Write(Constants.vbTab)
                Else
                    Console.Write($" {node.Unit.DisplayName}" + Constants.vbTab)
                End If

                Console.ResetColor()
            End If
        End Sub
    End Class
End Namespace
