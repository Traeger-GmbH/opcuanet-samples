' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Collections.Concurrent
Imports System.Collections.Generic
Imports System.Diagnostics.CodeAnalysis
Imports System.Linq
Imports System.Threading
Imports Opc.UaFx
Imports Opc.UaFx.Client

Namespace BulkChanges
    ''' <summary>
    ''' This sample demonstrates how to receive data changes of a large number of nodes with minimal
    ''' latency, using a publishing interval of zero and a callback for each data set.
    ''' </summary>
    <SuppressMessage("Style", "IDE1006:Naming Styles")>
    Public Class Program
        '---------- Private static fields ----------

        Private Shared consumerControl As CancellationTokenSource

        Private Shared dataChanges As BlockingCollection(Of OpcValue())
        Private Shared dataChangeSubscription As OpcSubscription


        '---------- Public static methods ----------

        Public Shared Sub Main()
            consumerControl = New CancellationTokenSource()
            dataChanges = New BlockingCollection(Of OpcValue())()

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

        Private Shared Iterator Function CreateMonitoredItems(
                client As OpcClient,
                rootNodeId As OpcNodeId) As IEnumerable(Of OpcMonitoredItem)
            Dim node = client.BrowseNode(rootNodeId)

            For Each childNode In node.Children()
                Yield New OpcMonitoredItem(childNode.NodeId, OpcAttribute.Value)
            Next
        End Function

        Private Shared Sub HandleDataChange(dataChangeItems As IEnumerable(Of OpcDataChangeDataSetItem))
            ' Determine the according monitored item for each data change.
            '''' Dim monitoredItem = dataChangeSubscription.GetMonitoredItem(dataChangeItem)

            dataChanges.Add(dataChangeItems.Select(Function(item) item.Value).ToArray())
        End Sub

        Private Shared Sub ConsumeDataChanges(state As Object)
            Dim client = DirectCast(state, OpcClient)

            dataChangeSubscription = client.SubscribeNodes()
            Dim subscription = dataChangeSubscription

            subscription.AddMonitoredItem(CreateMonitoredItems(client, "ns=2;s=Data"))
            subscription.ReceivedDataChangeCallback = AddressOf HandleDataChange

            ' Enforce the fastest supported publishing interval.
            subscription.PublishingInterval = 0

            ' Disable monitored item related cache und data change events.
            subscription.UseMonitoredItemDataCache = False

            ' Commit recent changes to the subscription.
            subscription.ApplyChanges()

            While Not consumerControl.IsCancellationRequested
                Try
                    Dim values = dataChanges.Take(consumerControl.Token)

                    For index = 0 To values.Length - 1
                        Dim value = values(index)

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
                    Next
                Catch ex As OperationCanceledException
                    Exit While
                End Try
            End While
        End Sub
    End Class
End Namespace
