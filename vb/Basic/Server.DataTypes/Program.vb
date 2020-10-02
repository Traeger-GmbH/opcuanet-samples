' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports Opc.UaFx.Server

Namespace DataTypes
    ''' <summary>
    ''' This sample demonstrates how to setup an OPC UA server to provide nodes with
    ''' custom data types.
    ''' </summary>
    Public Class Program
        '---------- Public static methods ----------

        Public Shared Sub Main(args As String())
            ' If the server domain name does not match localhost just replace it
            ' e.g. with the IP address or name of the server machine.
            Dim server As New OpcServer(
                    "opc.tcp://localhost:4840/SampleServer",
                    New SampleNodeManager())

            'NOTE: All DataTypes specific code will be found in the SampleNodeManager.vb.
            server.Start()

            Console.Write("Server started...")
            Console.ReadKey(True)

            server.Stop()
        End Sub
    End Class
End Namespace
