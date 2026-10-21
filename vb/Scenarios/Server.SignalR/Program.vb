' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Collections.Generic
Imports System.Diagnostics.CodeAnalysis
Imports System.Threading
Imports Opc.UaFx
Imports Opc.UaFx.Server

Namespace SignalR
    ''' <summary>
    ''' This use case demonstrates how to setup an OPC UA server whose node values change
    ''' periodically. It provides the nodes that the Client.SignalR sample forwards to the web
    ''' browser.
    ''' </summary>
    <SuppressMessage("Style", "IDE1006:Naming Styles")>
    Public Class Program
        '---------- Private static readonly fields ----------

        Private Shared ReadOnly Interval As Integer = 5000


        '---------- Private static fields ----------

        Private Shared producerControl As CancellationTokenSource

        Private Shared dataNode As OpcFolderNode
        Private Shared dataNodes As List(Of OpcDataVariableNode(Of Integer))


        '---------- Public static methods ----------

        Public Shared Sub Main(args As String())
            producerControl = New CancellationTokenSource()

            dataNodes = New List(Of OpcDataVariableNode(Of Integer))()
            dataNode = CreateDataNode()

            Dim producer As New Thread(AddressOf ProduceDataChanges)

            Using server As New OpcServer("opc.tcp://localhost:4840/", dataNode)
                server.Start()
                producer.Start(server)

                Console.WriteLine("Server started - press any key to exit.")
                Console.ReadKey(True)

                producerControl.Cancel()
                producer.Join()
            End Using
        End Sub


        '---------- Private static methods ----------

        Private Shared Function CreateDataNode() As OpcFolderNode
            Dim dataNode As New OpcFolderNode("Data")

            dataNodes.Add(New OpcDataVariableNode(Of Integer)(dataNode, "My Node 01", 1))
            dataNodes.Add(New OpcDataVariableNode(Of Integer)(dataNode, "My Node 02", 2))

            Return dataNode
        End Function

        Private Shared Sub ProduceDataChanges(state As Object)
            Dim server = DirectCast(state, OpcServer)

            While Not producerControl.IsCancellationRequested
                SyncLock state
                    For Each node In dataNodes
                        ' Wrap around instead of throwing an OverflowException.
                        node.Value = If(node.Value = Integer.MaxValue, Integer.MinValue, node.Value + 1)
                    Next

                    dataNode.ApplyChanges(server.SystemContext, recursive:=True)
                End SyncLock

                Console.WriteLine("{0} : New Data available - pausing for {1} ms.", DateTime.Now, Interval)

                ' Wait for "Stop" or perform next changes after interval elapsed
                If producerControl.Token.WaitHandle.WaitOne(Interval) Then
                    Exit While
                End If
            End While
        End Sub
    End Class
End Namespace
