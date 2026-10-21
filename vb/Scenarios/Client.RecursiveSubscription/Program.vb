' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Collections.Generic
Imports System.Linq

Imports Opc.UaFx
Imports Opc.UaFx.Client

Namespace RecursiveSubscription
    ''' <summary>
    ''' This use case demonstrates how a client adds job nodes below a monitored node and receives
    ''' the events the server raises for each added node.
    ''' </summary>
    Public Class Program
        Public Shared Sub Main()
            Using client As New OpcClient("opc.tcp://localhost:4841")
                client.Connect()
                client.SubscribeEvent("ns=2;s=Machine/Jobs", AddressOf HandleJobsEvent)

                Do
                    Console.WriteLine("Press any key to schedule a new job.")
                    Console.ReadLine()

                    Dim jobsNode = client.BrowseNode("ns=2;s=Machine/Jobs")
                    Dim job = $"JOB{GetLastJob(jobsNode) + 1:D2}"

                    client.AddNode(New OpcAddDataVariableNode(Of Integer)(
                            name:=job,
                            nodeId:=OpcNodeId.Null,
                            parentNodeId:=jobsNode.NodeId,
                            value:=0))

                    Console.WriteLine($"CLIENT: New job '{job}' schedulded.")
                Loop
            End Using
        End Sub

        Private Shared Function GetLastJob(jobsNode As OpcNodeInfo) As Integer
            Dim readJobs = New List(Of OpcReadNode)()

            For Each childNode In jobsNode.Children()
                Dim jobNode = TryCast(childNode, OpcVariableNodeInfo)

                If jobNode IsNot Nothing Then
                    readJobs.Add(New OpcReadNode(jobNode.NodeId, OpcAttribute.BrowseName))
                End If
            Next

            Dim client = jobsNode.Context.Client

            Return (From value In client.ReadNodes(readJobs)
                    Where value.Status.IsGood
                    Let jobName = value.As(Of OpcName)().Value
                    Let jobId = Integer.Parse(jobName.Substring("JOB".Length))
                    Select jobId).Max()
        End Function

        Private Shared Sub HandleJobsEvent(sender As Object, e As OpcEventReceivedEventArgs)
            Console.WriteLine("SERVER: {0} - {1}", e.Event.SourceName, e.Event.Message)
        End Sub
    End Class
End Namespace
