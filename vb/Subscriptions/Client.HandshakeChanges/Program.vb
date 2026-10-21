' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Collections.Generic
Imports System.Diagnostics.CodeAnalysis
Imports System.Linq
Imports System.Threading
Imports Opc.UaFx
Imports Opc.UaFx.Client

Namespace HandshakeChanges
    ''' <summary>
    ''' This sample demonstrates how a client reads a block of data as soon as the server signals it
    ''' as available and confirms its processing through a handshake node.
    ''' </summary>
    <SuppressMessage("Style", "IDE1006:Naming Styles")>
    Public Class Program
        '---------- Private static fields ----------

        Private Shared consumerControl As CancellationTokenSource
        Private Shared dataAvailable As SemaphoreSlim


        '---------- Public static methods ----------

        Public Shared Sub Main()
            consumerControl = New CancellationTokenSource()
            dataAvailable = New SemaphoreSlim(0)

            Dim consumer As New Thread(AddressOf ConsumeDataChanges)

            Using client As New OpcClient("opc.tcp://localhost:4840")
                client.Connect()
                consumer.Start(client)

                Console.WriteLine("Client started - press any key to exit.")
                Console.ReadKey(True)

                consumerControl.Cancel()
                consumer.Join()
            End Using
        End Sub


        '---------- Private static methods ----------

        Private Shared Iterator Function BrowseNodes(
                client As OpcClient,
                rootNodeId As OpcNodeId) As IEnumerable(Of OpcNodeId)
            Dim rootNode = client.BrowseNode(rootNodeId)

            For Each node In rootNode.Children()
                Dim name = node.Name.Value

                If name.StartsWith("Var") OrElse name = "Timestamp" Then
                    Yield node.NodeId
                End If
            Next
        End Function

        Private Shared Sub HandleDataChange(sender As Object, e As OpcDataChangeReceivedEventArgs)
            If e.Item.Value.As(Of Boolean)() Then
                dataAvailable.Release()
            End If
        End Sub

        Private Shared Sub ConsumeDataChanges(state As Object)
            Dim client = DirectCast(state, OpcClient)
            client.SubscribeDataChange("ns=2;s=Data/DataAvailable", AddressOf HandleDataChange)

            Dim nodeIds = BrowseNodes(client, "ns=2;s=Data").ToArray()

            While Not consumerControl.IsCancellationRequested
                Try
                    Console.WriteLine("{0} HANDSHAKE: Awaiting data being available.", DateTime.Now)
                    dataAvailable.Wait(consumerControl.Token)

                    Dim values = client.ReadNodes(nodeIds)
                    client.WriteNode("ns=2;s=Data/DataProcessed", value:=True)

                    For Each value In values
                        If TypeOf value.Value Is DateTime Then
                            Dim timestamp = DirectCast(value.Value, DateTime)

                            Console.WriteLine(
                                    "{0} HANDSHAKE: Completed (Duration = {1} ms)",
                                    DateTime.Now,
                                    DateTime.UtcNow.Subtract(timestamp).TotalMilliseconds)
                        Else
                            Console.WriteLine(
                                    "{0} HANDSHAKE: New Data: {1}",
                                    DateTime.Now,
                                    value)
                        End If
                    Next
                Catch ex As OperationCanceledException
                    Exit While
                End Try
            End While
        End Sub
    End Class
End Namespace
