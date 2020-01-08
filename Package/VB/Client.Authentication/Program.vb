' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports Opc.UaFx.Client

Namespace Authentication
    ''' <summary>
    ''' This sample demonstrates how to authenticate at an OPC UA server using user name
    ''' and password.
    ''' </summary>
    Public Class Program
        '---------- Public static methods ----------

        Public Shared Sub Main(args As String())
            '''' If the server domain name does not match localhost just replace it
            '''' e.g. with the IP address or name of the server machine.

            Dim client As New OpcClient("opc.tcp://localhost:4840/SampleServer")

            ' Just configure the OpcClient instance with an appropriate user identity with
            ' the name of the user and its password to use to authenticate.
            client.Security.UserIdentity = New OpcClientIdentity("username", "password")

            client.Connect()
            Console.WriteLine("ReadNode: {0}", client.ReadNode("ns=2;s=Machine_1/IsActive"))

            Try
                client.WriteNode("ns=2;s=Machine_1/IsActive", False)
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try

            Console.WriteLine("ReadNode: {0}", client.ReadNode("ns=2;s=Machine_1/IsActive"))

            client.Disconnect()
            Console.ReadKey(True)
        End Sub
    End Class
End Namespace
