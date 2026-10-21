' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System

Imports Opc.UaFx.Server

Namespace ClientAddedNodesWithHistory
    ''' <summary>
    ''' This use case implementes the necessary logic required to support nodes which are added
    ''' through a client application which wants the server to enable historical data.
    ''' </summary>
    Public Class Program
        '---------- Public static methods ----------

        Public Shared Sub Main(args As String())
            Dim manager = New NodeManager()

            Using server As New OpcServer("opc.tcp://localhost:4840/", manager)
                server.Start()

                Console.WriteLine("Server started - press any key to exit.")
                Console.ReadKey(True)
            End Using
        End Sub
    End Class
End Namespace
