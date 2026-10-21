' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System

Imports Opc.UaFx
Imports Opc.UaFx.Client

Namespace ClientAddedNodesWithHistory
    ''' <summary>
    ''' This use case demonstrates how a client adds a variable node with history access to the
    ''' server and writes values that the server records as history.
    ''' </summary>
    Public Class Program
        Public Shared Sub Main()
            Using client As New OpcClient("opc.tcp://localhost:4840")
                client.Connect()
                Console.WriteLine("Connected!")

                Console.WriteLine("Press any key to add a new 'Speed' node with history to the 'Machine' node.")
                Console.ReadLine()

                ' The initializer may also set: .IsHistorizing = True
                Dim result = client.AddNode(New OpcAddDataVariableNode(Of Integer)(
                        name:="Speed",
                        nodeId:=OpcNodeId.Null, ' the server shall define it
                        parentNodeId:="ns=2;s=Machine") With {
                    .Value = 1000,
                    .AccessLevel = OpcAccessLevel.CurrentReadOrWrite Or OpcAccessLevel.HistoryReadOrWrite,
                    .UserAccessLevel = OpcAccessLevel.CurrentReadOrWrite Or OpcAccessLevel.HistoryReadOrWrite
                })

                If result.IsBad Then
                    Console.WriteLine(result.Description)
                Else
                    Console.WriteLine(
                            "Enter a number and press enter to write it as the new speed value " _
                            & "which is also added to the history.")

                    Dim speed As Integer

                    While Integer.TryParse(Console.ReadLine(), speed)
                        client.WriteNode(result.NodeId, New OpcValue(speed, DateTime.UtcNow))
                    End While
                End If
            End Using
        End Sub
    End Class
End Namespace
