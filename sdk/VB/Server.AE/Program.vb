' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Threading

Imports Opc.UaFx.Server

Namespace AE
    ''' <summary>
    ''' This sample demonstrates how to setup an OPC UA server to provide AE to its
    ''' nodes provided.
    ''' </summary>
    Public Class Program
        '---------- Public static methods ----------

        Public Shared Sub Main(args As String())
            Dim nodeManager As New SampleNodeManager()

            ' If the server domain name does not match localhost just replace it
            ' e.g. with the IP address or name of the server machine.
            Dim server As New OpcServer(
                    "opc.tcp://localhost:4840/SampleServer",
                    nodeManager)

            server.Start()
            '''' NOTE: All AE specific code will be found in the SampleNodeManager.vb.

            Using semaphore As SemaphoreSlim = New SemaphoreSlim(0)
                Dim thread As New Thread(Sub() nodeManager.Simulate(semaphore))
                thread.Start()

                Console.WriteLine("OPC UA Server is running...")
                Console.ReadKey(True)

                semaphore.Release()
                thread.Join()

                server.Stop()
            End Using
        End Sub
    End Class
End Namespace
