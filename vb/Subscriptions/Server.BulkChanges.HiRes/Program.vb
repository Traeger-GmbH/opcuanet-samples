' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Collections.Generic
Imports System.Diagnostics.CodeAnalysis
Imports System.Threading
Imports Opc.UaFx
Imports Opc.UaFx.Server

Namespace BulkChanges
    ''' <summary>
    ''' This sample demonstrates how to configure an OPC UA server for high resolution publishing,
    ''' to deliver changes of a large number of nodes in intervals of a few milliseconds.
    ''' </summary>
    <SuppressMessage("Style", "IDE1006:Naming Styles")>
    Public Class Program
        '---------- Private static readonly fields ----------

        Private Shared ReadOnly Interval As Integer = 5
        Private Shared ReadOnly NumberOfNodes As Integer = 500


        '---------- Private static fields ----------

        Private Shared producerControl As CancellationTokenSource

        Private Shared dataNode As OpcFolderNode
        Private Shared dataNodes As List(Of OpcDataVariableNode(Of Integer))

        Private Shared timestampNode As OpcDataVariableNode(Of DateTime)


        '---------- Public static methods ----------

        Public Shared Sub Main(args As String())
            producerControl = New CancellationTokenSource()

            dataNodes = New List(Of OpcDataVariableNode(Of Integer))()
            dataNode = CreateDataNode()

            Dim producer As New Thread(AddressOf ProduceDataChanges)

            Using server As New OpcServer("opc.tcp://localhost:4840/", dataNode)
                server.Configuration.DisableHiResClock = False
                server.Configuration.ServerConfiguration.PublishingResolution = 1
                server.Configuration.ServerConfiguration.MinPublishingInterval = 5

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

            For index = 0 To NumberOfNodes - 1
                dataNodes.Add(New OpcDataVariableNode(Of Integer)(dataNode, $"Var{index:00000}", value:=index))
            Next

            timestampNode = New OpcDataVariableNode(Of DateTime)(dataNode, "Timestamp")
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

                    timestampNode.Value = DateTime.UtcNow
                    dataNode.ApplyChanges(server.SystemContext, recursive:=True)
                End SyncLock

                Console.WriteLine("{0} BULK: New Data available - pausing for {1} ms.", DateTime.Now, Interval)

                ' Wait for "Stop" or perform next bulk changes after interval elapsed
                If producerControl.Token.WaitHandle.WaitOne(Interval) Then
                    Exit While
                End If
            End While
        End Sub
    End Class
End Namespace
