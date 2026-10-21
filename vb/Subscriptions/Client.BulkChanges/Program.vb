' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Collections.Concurrent
Imports System.Collections.Generic
Imports System.Diagnostics.CodeAnalysis
Imports System.Threading
Imports Opc.UaFx
Imports Opc.UaFx.Client

Namespace BulkChanges
    ''' <summary>
    ''' This sample demonstrates how to subscribe to a large number of nodes and process their data
    ''' changes in a separate consumer thread.
    ''' </summary>
    <SuppressMessage("Style", "IDE1006:Naming Styles")>
    Public Class Program
        '---------- Private static fields ----------

        Private Shared consumerControl As CancellationTokenSource
        Private Shared dataChanges As BlockingCollection(Of OpcValue)


        '---------- Public static methods ----------

        Public Shared Sub Main()
            consumerControl = New CancellationTokenSource()
            dataChanges = New BlockingCollection(Of OpcValue)()

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

        Private Shared Iterator Function CreateCommands(
                client As OpcClient,
                rootNodeId As OpcNodeId) As IEnumerable(Of OpcSubscribeDataChange)
            Dim node = client.BrowseNode(rootNodeId)

            For Each childNode In node.Children()
                Yield New OpcSubscribeDataChange(childNode.NodeId, AddressOf HandleDataChange)
            Next
        End Function

        Private Shared Sub HandleDataChange(sender As Object, e As OpcDataChangeReceivedEventArgs)
            dataChanges.Add(e.Item.Value)
        End Sub

        Private Shared Sub ConsumeDataChanges(state As Object)
            Dim client = DirectCast(state, OpcClient)
            client.SubscribeNodes(CreateCommands(client, "ns=2;s=Data"))

            While Not consumerControl.IsCancellationRequested
                Try
                    Dim value = dataChanges.Take(consumerControl.Token)

                    If TypeOf value.Value Is DateTime Then
                        Dim timestamp = DirectCast(value.Value, DateTime)

                        Console.WriteLine(
                                "{0} BULK: Completed (Duration = {1} ms)",
                                DateTime.Now,
                                DateTime.UtcNow.Subtract(timestamp).TotalMilliseconds)
                    Else
                        Console.WriteLine(
                                "{0} BULK: New Data: {1}",
                                DateTime.Now,
                                value)
                    End If
                Catch ex As OperationCanceledException
                    Exit While
                End Try
            End While
        End Sub
    End Class
End Namespace
