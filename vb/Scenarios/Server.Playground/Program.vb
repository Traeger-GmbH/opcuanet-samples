' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Threading

Imports Opc.UaFx.Server

Namespace Server.Playground
    ''' <summary>
    ''' This use case realizes a server which offers different kinds of nodes to easily test
    ''' client applications against different possible use cases.
    ''' </summary>
    Partial Public Class Program
        Public Shared Sub Main(args As String())
            Dim address = GetAddress(args)
            Dim hostAddresses = GetHostAddresses(address)

            Dim manager As New NodeManager()

            Using server As New OpcServer(address, manager)
                Console.Write("Starting server...")
                server.Start()
                Console.WriteLine("started!")

                Console.WriteLine()
                Console.WriteLine("Server listening to:")
                Console.WriteLine($"{vbTab}{address}")

                For Each hostAddress In hostAddresses
                    Console.WriteLine($"{vbTab}{hostAddress}")
                Next

                Console.WriteLine()

                Dim semaphore As New SemaphoreSlim(0)
                Dim thread As New Thread(Sub() manager.Simulate(semaphore))

                thread.Start()

                Console.WriteLine("Press any key to stop the server.")
                Console.ReadKey(True)

                semaphore.Release()
                thread.Join()

                Console.WriteLine()
                Console.Write("Stopping server...")
            End Using

            Console.Write("stopped!")
        End Sub
    End Class
End Namespace
