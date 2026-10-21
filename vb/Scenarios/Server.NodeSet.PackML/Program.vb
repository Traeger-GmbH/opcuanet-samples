' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System

Imports Opc.UaFx
Imports Opc.UaFx.Server

Namespace NodeSet
    ''' <summary>
    ''' This use case demonstrates how to import and implement an alarm from a nodeset using the
    ''' companion specification from PackML.
    ''' </summary>
    Public Class Program
        '---------- Public static readonly fields ----------

        Public Shared ReadOnly [Namespace] As OpcNamespace = OpcNamespace.Create("http://sampleserver/")

        '---------- Public static methods ----------

        Public Shared Sub Main(args As String())
            Dim manager = New NodeManager()

            Using server = New OpcServer("opc.tcp://localhost:4840/", manager)
                server.ApplicationUri = [Namespace].Uri
                server.Start()

                ' Add some objects.
                manager.AddObject()
                manager.AddObject()
                manager.AddObject()

                Console.WriteLine("Server started - press any key to exit.")
                Console.ReadKey(True)
            End Using
        End Sub
    End Class
End Namespace
