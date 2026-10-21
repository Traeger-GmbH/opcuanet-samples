' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Diagnostics.CodeAnalysis
Imports System.Threading

Imports Opc.UaFx
Imports Opc.UaFx.Server

Namespace LightweightEvents
    ''' <summary>
    ''' This use case demonstrates in which way a server can "re-use" a single event node to
    ''' represent events from different event sources without to define for each source
    ''' another event node.
    ''' </summary>
    <SuppressMessage("Style", "IDE1006:Naming Styles")>
    Public Class Program
        '---------- Private static methods ----------

        Private Shared machineControl As CancellationTokenSource
        Private Shared machineThread As Thread

        Private Shared syncRoot As New Object()

        Private Shared temperatureNode As OpcDataVariableNode(Of Double)
        Private Shared speedNode As OpcDataVariableNode(Of Integer)

        '---------- Public static methods ----------

        Public Shared Sub Main(args As String())
            temperatureNode = New OpcDataVariableNode(Of Double)("Temperature")
            speedNode = New OpcDataVariableNode(Of Integer)("Speed")

            Dim machineNode As New OpcObjectNode(
                    "Machine",
                    temperatureNode,
                    speedNode,
                    New OpcActionMethodNode("StartMachine", AddressOf StartMachine),
                    New OpcActionMethodNode("StopMachine", AddressOf StopMachine))

            Using server As New OpcServer("opc.tcp://localhost:4840/", machineNode)
                server.Start()

                machineNode.AddNotifier(server.SystemContext, temperatureNode)
                machineNode.AddNotifier(server.SystemContext, speedNode)

                Console.WriteLine("Server started - press any key to exit.")
                Console.ReadKey(True)
            End Using
        End Sub

        '---------- Private static methods ----------

        Private Shared Sub RunMachine(state As Object)
            Dim context = DirectCast(state, OpcContext)

            Dim eventNode As New OpcEventNode("State")
            Dim eventSourceNode As OpcNode = Nothing

            While Not machineControl.IsCancellationRequested
                Dim time = DateTime.Now.TimeOfDay

                temperatureNode.Value = time.Seconds + time.Milliseconds * 0.1
                temperatureNode.ApplyChanges(context)

                speedNode.Value = time.Seconds * 1000
                speedNode.ApplyChanges(context)

                If time.Seconds Mod 11 = 0 Then
                    eventSourceNode = speedNode
                    eventNode.Severity = OpcEventSeverity.Medium
                Else
                    eventSourceNode = temperatureNode

                    If temperatureNode.Value <= 10 Then
                        eventNode.Severity = OpcEventSeverity.Low
                    ElseIf temperatureNode.Value <= 20 Then
                        eventNode.Severity = OpcEventSeverity.MediumLow
                    ElseIf temperatureNode.Value <= 30 Then
                        eventNode.Severity = OpcEventSeverity.Medium
                    ElseIf temperatureNode.Value <= 40 Then
                        eventNode.Severity = OpcEventSeverity.MediumHigh
                    ElseIf temperatureNode.Value <= 50 Then
                        eventNode.Severity = OpcEventSeverity.High
                    Else
                        eventNode.Severity = OpcEventSeverity.Max
                    End If
                End If

                eventNode.SourceName = eventSourceNode.SymbolicName
                eventNode.SourceNodeId = eventSourceNode.Id

                Dim eventData = eventNode.CreateEvent(context)
                eventSourceNode.ReportEvent(context, eventData)

                ' Wait for "StopMachine" or perform next steps after interval elapsed
                If machineControl.Token.WaitHandle.WaitOne(2500) Then
                    Exit While
                End If
            End While
        End Sub

        Private Shared Sub StartMachine(context As OpcMethodContext)
            SyncLock syncRoot
                If machineThread Is Nothing Then
                    machineControl = New CancellationTokenSource()

                    machineThread = New Thread(AddressOf RunMachine)
                    machineThread.Start(context)
                End If
            End SyncLock
        End Sub

        Private Shared Sub StopMachine(context As OpcMethodContext)
            SyncLock syncRoot
                If machineThread IsNot Nothing Then
                    machineControl.Cancel()
                    machineThread.Join()

                    machineControl.Dispose()
                    machineControl = Nothing

                    machineThread = Nothing
                End If
            End SyncLock
        End Sub
    End Class
End Namespace
