' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System

Imports Opc.UaFx
Imports Opc.UaFx.Server

Namespace DynamicNodes
    ''' <summary>
    ''' This use case realizes dynamic nodes which are created on-demand by the server depending
    ''' on the underlying system needs.
    ''' </summary>
    Public Class Program
        '---------- Public static methods ----------

        Public Shared Sub Main(args As String())
            Dim manager As New NodeManager()

            Using server As New OpcServer("opc.tcp://localhost:4840/", manager)
                server.Start()

                Console.WriteLine("Press enter to exit or ...")
                Console.WriteLine("... enter a node path like '/a/b/c' to create a folder node.")
                Console.WriteLine("... enter a path like '/a/b/.d' to create a numeric variable node.")
                Console.WriteLine("... begin the path with '$' instead of a '/' to create a new root node.")
                Console.WriteLine()

                While True
                    Dim path = Console.ReadLine().Trim().Split({"/"c}, StringSplitOptions.RemoveEmptyEntries)

                    If path.Length = 0 Then
                        Exit While
                    End If

                    Dim node As IOpcNode = Nothing

                    If path(0)(0) = "$"c Then
                        node = manager.AddRootNode(path)
                    Else
                        node = manager.AddNode(path)
                    End If

                    Console.WriteLine("Created: '{0}'", node.Id.ToString(OpcNodeIdFormat.Foundation))
                End While
            End Using
        End Sub
    End Class
End Namespace
