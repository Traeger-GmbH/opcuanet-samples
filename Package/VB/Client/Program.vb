' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports Opc.UaFx.Client

Namespace Client
    ''' <summary>
    ''' This sample demonstrates how to implement a primitive OPC UA client.
    ''' </summary>
    Public Class Program
        ' ---------- Public static methods ----------

        Public Shared Sub Main(args As String())
            '''' If the server domain name does not match localhost just replace it
            '''' e.g. with the IP address or name of the server machine.

            ' 1st Way: Use the OpcClient class.
            If True Then
                ' The OpcClient class interacts with one OPC UA server. While this class
                ' provides session based access to the different OPC UA services of the
                ' server, it does not implement a main loop.
                Dim client As New OpcClient("opc.tcp://localhost:4840/SampleServer")

                client.Connect()
                Program.CommunicateWithServer(client)
                client.Disconnect()
            End If

            ' 2nd Way: Use the OpcClientApplication class.
            If True Then
                '' The OpcClientApplication class uses a single OpcClient instance which is
                '' wrapped within a main loop.
                ''
                '' Remarks
                '' - The app instance starts a main loop when the session to the server has
                ''   been established.
                '' - Custom client/session dependent code have to be implemented within the event
                ''   handler of the Started event.
                'Dim app As New OpcClientApplication("opc.tcp://localhost:4840/SampleServer")
                'AddHandler app.Started, AddressOf Program.HandleAppStarted

                'app.Run()
            End If
        End Sub

        '---------- Private static methods ----------

        Private Shared Sub CommunicateWithServer(client As OpcClient)
            Console.WriteLine("ReadNode: {0}", client.ReadNode("ns=2;s=Machine_1/IsActive"))
            client.WriteNode("ns=2;s=Machine_1/IsActive", False)
            Console.WriteLine("ReadNode: {0}", client.ReadNode("ns=2;s=Machine_1/IsActive"))
        End Sub

        Private Shared Sub HandleAppStarted(sender As Object, e As EventArgs)
            Program.CommunicateWithServer(DirectCast(sender, OpcClientApplication).Client)
        End Sub
    End Class
End Namespace
