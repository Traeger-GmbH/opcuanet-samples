' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Collections.Generic
Imports System.Linq

Imports Opc.UaFx.Server

Namespace DynamicNodeManagers
    ''' <summary>
    ''' This use case demonstrates how to provide each namespace through its own node manager and
    ''' how to restart the server with a different selection of node managers.
    ''' </summary>
    Public Class Program
        '---------- Public static methods ----------

        Public Shared Sub Main(args As String())
            Do
                Console.Clear()
                Dim managers = QueryNodeManagers()

                Using server As New OpcServer("opc.tcp://localhost:4840/", managers)
                    Console.WriteLine("Starting...")
                    server.Start()

                    For Each nodeManager In server.NodeManagers.OfType(Of NodeManager)()
                        Console.Write(" - {0}...", nodeManager.Name)
                        nodeManager.Start()
                        Console.WriteLine("done.")
                    Next

                    Console.WriteLine("Started.")

                    Console.Write("Press any key to use different node managers.")
                    Console.ReadLine()

                    Console.WriteLine("Stopping...")

                    For Each nodeManager In server.NodeManagers.OfType(Of NodeManager)()
                        Console.Write(" - {0}...", nodeManager.Name)
                        nodeManager.Stop()
                        Console.WriteLine("done.")
                    Next
                End Using

                Console.WriteLine("Stopped.")

                Console.Write("Press any key to setup a new server.")
                Console.ReadLine()
            Loop
        End Sub

        Private Shared Iterator Function QueryNodeManagers() As IEnumerable(Of NodeManager)
            Console.WriteLine("Select node managers...")

            Dim names = {
                "Doors",
                "Motors",
                "Drills",
                "Cameras",
                "Files",
                "Conveyors"
            }

            For index = 0 To names.Length - 1
                Dim name = names(index)
                Console.Write($"{name}? [y (default)|n]: ")

                Do
                    Dim input = Console.ReadLine().ToLower()

                    If input = "n" Then
                        Exit Do
                    End If

                    If input.Length = 0 OrElse input = "y" Then
                        Yield New NodeManager(name)
                        Exit Do
                    End If

                    Console.WriteLine("Enter y (default) or n: ")
                Loop
            Next
        End Function
    End Class
End Namespace
