' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports Opc.UaFx.Client

Namespace Client.StateMachine
    ''' <summary>
    ''' This use case demonstrates how a client monitors the current state of a state machine
    ''' provided by the server.
    ''' </summary>
    Public Class Program
        Public Shared Sub Main()
            Using client As New OpcClient("opc.tcp://localhost:4840")
                client.Connect()
                Console.WriteLine("Connected")

                client.SubscribeDataChange("ns=2;s=SampleStateMachine/CurrentState", AddressOf HandleDataChanged)

                Console.WriteLine("Perform state changes now!")
                Console.ReadLine()
            End Using
        End Sub

        Private Shared Sub HandleDataChanged(sender As Object, e As OpcDataChangeReceivedEventArgs)
            Console.WriteLine("State changed to: {0}", e.Item.Value)
        End Sub
    End Class
End Namespace
