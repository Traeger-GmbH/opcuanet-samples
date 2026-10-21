' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports Opc.UaFx.Server

Namespace SimaticNodeSet
    ''' <summary>
    ''' This use case demonstrates how to setup an OPC UA server that imports the nodeset of a
    ''' SIMATIC S7-1500 controller and identifies itself like the server of the controller, e.g. to
    ''' test clients without the hardware.
    ''' </summary>
    Public Class Program
        '---------- Public static methods ----------

        Public Shared Sub Main(args As String())
            Dim nodeManager As New NodeManager()

            Using server As New OpcServer("opc.tcp://localhost:4840/", nodeManager)
                server.ApplicationUri = New Uri("urn:SIMATIC.S7-1500.OPC-UA.Application:PLC_1")
                server.Start()

                nodeManager.Start()

                Console.WriteLine("Server started - press any key to exit.")
                Console.ReadKey(True)
            End Using
        End Sub
    End Class
End Namespace
