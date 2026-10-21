' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System

Imports Opc.UaFx.Server

Namespace Server.StateMachine
    ''' <summary>
    ''' This use case demonstrates how to implement a finite state machine node with custom states
    ''' and how the server changes its state.
    ''' </summary>
    Public Class Program
        Public Shared Sub Main()
            Dim sampleStateMachineNode = New SampleStateMachineNode("SampleStateMachine")

            Using server As New OpcServer("opc.tcp://localhost:4840/", sampleStateMachineNode)
                server.Start()

                Console.WriteLine("Started.")

                Do
                    Console.WriteLine("Enter <enter> to change state.")
                    Console.ReadLine()

                    sampleStateMachineNode.ChangeStateTo(server.SystemContext, SampleStates.Starting)

                    Console.WriteLine("Enter <enter> to change state.")
                    Console.ReadLine()

                    sampleStateMachineNode.ChangeStateTo(server.SystemContext, SampleStates.Started)

                    Console.WriteLine("Enter <enter> to change state.")
                    Console.ReadLine()

                    sampleStateMachineNode.ChangeStateTo(server.SystemContext, SampleStates.Stopping)

                    Console.WriteLine("Enter <enter> to change state.")
                    Console.ReadLine()

                    sampleStateMachineNode.ChangeStateTo(server.SystemContext, SampleStates.Stopped)
                Loop
            End Using
        End Sub
    End Class
End Namespace
