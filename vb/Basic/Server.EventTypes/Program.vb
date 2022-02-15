'Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports Opc.UaFx.Server

Namespace EventTypes
    ''' <summary>
    ''' This use case demonstrates how to import and implement custom node types using a nodeset.
    ''' </summary>
    Public Class Program
        Public Shared Sub Main(ByVal args As String())
            Dim manager = New NodeManager()

            Using server = New OpcServer("opc.tcp://localhost:4840/", manager)
                server.ApplicationUri = New Uri("http://sampleserver/sampleeventtypes")
                server.Start()

                Console.WriteLine("Server started - now subscribe to Machine node event 'MachineJobEventNode' and call method 'StartJob'.")
                Console.ReadLine()
            End Using
        End Sub
    End Class
End Namespace
