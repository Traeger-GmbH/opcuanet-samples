' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Collections.Generic
Imports System.Diagnostics.CodeAnalysis
Imports System.Threading
Imports Opc.UaFx
Imports Opc.UaFx.Server

Namespace HandshakeChanges
    ''' <summary>
    ''' This sample demonstrates how to setup an OPC UA server that provides blocks of data using a
    ''' handshake: it signals new data and waits until a client confirms their processing.
    ''' </summary>
    <SuppressMessage("Style", "IDE1006:Naming Styles")>
    Public Class Program
        '---------- Private static readonly fields ----------

        Private Shared ReadOnly Interval As Integer = 5000
        Private Shared ReadOnly NumberOfNodes As Integer = 500


        '---------- Private static fields ----------

        Private Shared producerControl As CancellationTokenSource
        Private Shared dataProcessed As SemaphoreSlim

        Private Shared dataNode As OpcFolderNode
        Private Shared dataNodes As List(Of OpcDataVariableNode(Of Integer))

        Private Shared dataAvailableNode As OpcDataVariableNode(Of Boolean)
        Private Shared dataProcessedNode As OpcDataVariableNode(Of Boolean)

        Private Shared timestampNode As OpcDataVariableNode(Of DateTime)


        '---------- Public static methods ----------

        Public Shared Sub Main(args As String())
            producerControl = New CancellationTokenSource()
            dataProcessed = New SemaphoreSlim(0)

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
            dataAvailableNode = New OpcDataVariableNode(Of Boolean)(dataNode, "DataAvailable")

            dataProcessedNode = New OpcDataVariableNode(Of Boolean)(dataNode, "DataProcessed")
            dataProcessedNode.WriteVariableValueCallback = AddressOf WriteDataProcessed

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
                    dataAvailableNode.Value = False
                    dataAvailableNode.ApplyChanges(server.SystemContext)

                    For Each node In dataNodes
                        ' Wrap around instead of throwing an OverflowException.
                        node.Value = If(node.Value = Integer.MaxValue, Integer.MinValue, node.Value + 1)
                    Next

                    dataProcessedNode.Value = False
                    dataAvailableNode.Value = True

                    timestampNode.Value = DateTime.UtcNow
                    dataNode.ApplyChanges(server.SystemContext, recursive:=True)
                End SyncLock

                Try
                    Do
                        Console.WriteLine("{0} HANDSHAKE: New Data available - awaiting data being processed.", DateTime.Now)
                    Loop While Not dataProcessed.Wait(Interval, producerControl.Token)

                    Console.WriteLine("{0} HANDSHAKE: Completed - processing for {1} ms.", DateTime.Now, Interval)

                    ' Wait for "Stop" or perform next handshake changes after interval elapsed
                    If producerControl.Token.WaitHandle.WaitOne(Interval) Then
                        Exit While
                    End If
                Catch ex As OperationCanceledException
                    Exit While
                End Try
            End While
        End Sub

        Private Shared Function WriteDataProcessed(
                context As OpcWriteVariableValueContext,
                value As OpcVariableValue(Of Object)) As OpcVariableValue(Of Object)
            If TypeOf value.Value Is Boolean AndAlso DirectCast(value.Value, Boolean) Then
                dataProcessed.Release()
            End If

            ' Reset DataProcessed
            Return New OpcVariableValue(Of Object)(False)
        End Function
    End Class
End Namespace
