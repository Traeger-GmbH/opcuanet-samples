' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System

Imports Opc.UaFx
Imports Opc.UaFx.Server

Namespace Methods
    ''' <summary>
    ''' This sample demonstrates how to setup an OPC UA server to provide method nodes.
    ''' </summary>
    Public Class Program
        '---------- Public static methods ----------

        Public Shared Sub Main(args As String())
            ' Async method execution can be globally enabled for every server and node manager.
            ' This results into the use of one worker thread per method being called. Independent
            ' whether the method is implemented using the TPL (using Tasks) or not.
            ' By default only TPL methods are executed in parallel.
            'OpcAutomatism.UseAsyncMethodCalls = True

            ' If the server domain name does not match localhost just replace it
            ' e.g. with the IP address or name of the server machine.
            Dim server As New OpcServer(
                    "opc.tcp://localhost:4840/SampleServer",
                    New SampleNodeManager())

            ' NOTE: All node specific code will be found in the SampleNodeManager.vb.

            server.Start()

            Console.WriteLine("Started.")
            Console.ReadKey(True)

            server.Stop()
        End Sub
    End Class
End Namespace
