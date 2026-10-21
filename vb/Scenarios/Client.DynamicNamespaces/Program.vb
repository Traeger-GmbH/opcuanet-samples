' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Collections.Generic

Imports Opc.UaFx.Client

Namespace DynamicNamespaces
    ''' <summary>
    ''' This use case demonstrates how a client addresses nodes by namespace URI instead of
    ''' namespace index and renews its subscriptions after each reconnect, so it copes with
    ''' namespaces that change on the server.
    ''' </summary>
    Public Class Program
        Private Shared Data As New Dictionary(Of String, Object)()
        Private Shared Subscription As OpcSubscription

        Public Shared Sub Main()
            Using client As New OpcClient("opc.tcp://localhost:4840")
                AddHandler client.Connected, AddressOf HandleClientConnected
                client.Connect()

                Console.WriteLine("Connected and subscribed.")
                Console.ReadLine()
            End Using
        End Sub

        Private Shared Sub HandleClientConnected(sender As Object, e As EventArgs)
            Console.Clear()
            Subscription?.Unsubscribe()

            Subscription = DirectCast(sender, OpcClient).SubscribeNodes(
                    New OpcSubscribeDataChange("nsu=http://sampleserver/cameras;s=Cameras/Var9700", AddressOf HandleDataChange),
                    New OpcSubscribeDataChange("nsu=http://sampleserver/conveyors;s=Conveyors/Var11100", AddressOf HandleDataChange),
                    New OpcSubscribeDataChange("nsu=http://sampleserver/doors;s=Doors/Var11400", AddressOf HandleDataChange),
                    New OpcSubscribeDataChange("nsu=http://sampleserver/files;s=Files/Var10800", AddressOf HandleDataChange),
                    New OpcSubscribeDataChange("nsu=http://sampleserver/motors;s=Motors/Var11400", AddressOf HandleDataChange))

            For Each monitoredItem In Subscription.MonitoredItems
                Data(monitoredItem.NodeId.ValueAsString) = Nothing

                If monitoredItem.Status.IsCreated Then
                    Console.ForegroundColor = ConsoleColor.Green
                    Console.WriteLine("{0} - active!", monitoredItem.NodeId.Value)
                Else
                    Console.ForegroundColor = ConsoleColor.Yellow
                    Console.WriteLine("{0} - inactive!", monitoredItem.NodeId.Value)
                End If

                Console.ResetColor()
            Next

            Console.WriteLine(New String("-"c, 30))
        End Sub

        Public Shared Sub HandleDataChange(sender As Object, e As OpcDataChangeReceivedEventArgs)
            Data(e.MonitoredItem.NodeId.ValueAsString) = e.Item.Value.As(Of Object)()
            Console.SetCursorPosition(0, 6)

            For Each item In Data
                Console.WriteLine("{0} = {1}          ", item.Key, If(item.Value, "<missing>"))
            Next
        End Sub
    End Class
End Namespace
