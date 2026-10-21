' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System

Imports Opc.UaFx.Server

Namespace NodeSet
    ''' <summary>
    ''' This use case demonstrates how to import and implement an alarm from a nodeset using the
    ''' companion specification from UMATI.
    ''' </summary>
    Public Class Program
        '---------- Public static methods ----------

        Public Shared Sub Main(args As String())
            Dim manager = New NodeManager()

            Using server = New OpcServer("opc.tcp://localhost:4840/", manager)
                server.Start()

                Console.WriteLine("Server started - press enter to report an event.")
                Dim line = Console.ReadLine()

                Do While line.Length = 0
                    manager.ReportEvent()
                    line = Console.ReadLine()
                Loop
            End Using
        End Sub
    End Class
End Namespace
