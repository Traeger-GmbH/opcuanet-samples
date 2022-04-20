'Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports Opc.UaFx.Server

Namespace NodeTypes
    ''' <summary>
    ''' This use case demonstrates how to import and implement custom node types using a nodeset.
    ''' </summary>
    Public Class Program
#Region "---------- Public shared methods ----------"

        Public Shared Sub Main(ByVal args As String())
            Dim manager = New NodeManager()

            Using server = New OpcServer("opc.tcp://localhost:4840/", manager)
                server.ApplicationUri = New Uri("http://sampleserver/samplenodetypes")
                server.Start()

                Console.WriteLine("Server started - now browse the resulting nodes.")
                Console.ReadLine()
            End Using
        End Sub

#End Region
    End Class
End Namespace
