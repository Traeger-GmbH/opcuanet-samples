' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Threading

Imports Opc.UaFx.Server

Namespace RecursiveSubscription
    ''' <summary>
    ''' This use case represents in which way a server can monitor newly added nodes, work with
    ''' them and notify clients about changes on these nodes.
    ''' </summary>
    Public Class Program
        '---------- Public static methods ----------

        Public Shared Sub Main(args As String())
            Dim manager = New NodeManager()

            Using server As New OpcServer("opc.tcp://localhost:4841/", manager)
                server.Start()

                Using semaphore As New SemaphoreSlim(0)
                    Dim thread = New Thread(Sub() manager.Schedule(semaphore))
                    thread.Start()

                    Console.WriteLine("Server started - press any key to exit.")
                    Console.ReadKey(True)

                    semaphore.Release()
                    thread.Join()
                End Using
            End Using
        End Sub
    End Class
End Namespace
