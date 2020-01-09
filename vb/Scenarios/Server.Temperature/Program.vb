Imports System.Threading

Imports Opc.UaFx
Imports Opc.UaFx.Server

Namespace Server
    Public Class Program
        Public Shared Sub Main()
            Dim temperatureNode = New OpcDataVariableNode(Of Double)("Temperature", 100.0)

            Using server = New OpcServer("opc.tcp://localhost:4840/", temperatureNode)
                server.Start()

                While True
                    If (temperatureNode.Value = 110) Then
                        temperatureNode.Value = 100
                    Else
                        temperatureNode.Value += 1
                    End If

                    temperatureNode.ApplyChanges(server.SystemContext)
                    Thread.Sleep(1000)
                End While
            End Using
        End Sub
    End Class
End Namespace
