' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Collections.Concurrent
Imports System.Collections.Generic
Imports System.Threading

Imports Opc.UaFx
Imports Opc.UaFx.Server

Namespace RecursiveSubscription
    Friend Class NodeManager
        Inherits OpcNodeManager

        '---------- Private readonly fields ----------

        Private ReadOnly jobs As ConcurrentQueue(Of OpcDataVariableNode)


        '---------- Private fields ----------

        Private jobsNode As OpcFolderNode

        Private jobScheduldedEventNode As OpcEventNode
        Private jobCompletedEventNode As OpcEventNode


        '---------- Public constructors ----------

        Public Sub New()
            MyBase.New("http://sampleserver/recursivesubscription")
            Me.jobs = New ConcurrentQueue(Of OpcDataVariableNode)()
        End Sub


        '---------- Public methods ----------

        Public Sub Schedule(semaphore As SemaphoreSlim)
            Dim random = New Random()
            Dim context = Me.SystemContext

            While Not semaphore.Wait(3000)
                Dim jobNode As OpcDataVariableNode = Nothing

                If Me.jobs.TryDequeue(jobNode) AndAlso TypeOf jobNode.Value Is Integer Then
                    Dim stage = CInt(jobNode.Value)

                    ' Raised job-schedulded event.
                    Me.jobScheduldedEventNode.ReportEventFrom(context, jobNode)

                    ' Simulate a kind of "process" which processes the job.
                    While Not semaphore.Wait(500)
                        stage = Math.Min(stage + random.Next(10), 100)

                        jobNode.Timestamp = DateTime.UtcNow
                        jobNode.Value = stage

                        jobNode.ApplyChanges(context)

                        If stage = 100 Then
                            Exit While
                        End If
                    End While

                    ' Raised job-completed event.
                    Me.jobCompletedEventNode.ReportEventFrom(context, jobNode)
                End If
            End While
        End Sub


        '---------- Protected methods ----------

        Protected Overrides Sub AddNode(
                context As OpcContext,
                node As IOpcNode,
                references As IEnumerable(Of IOpcNodeReferenceAware))
            MyBase.AddNode(context, node, references)

            ' "Catch" nodes added by the client and "schedule" job nodes.
            If node.Parent Is Me.jobsNode Then
                Dim jobNode = TryCast(node, OpcDataVariableNode)

                If jobNode IsNot Nothing Then
                    Me.jobs.Enqueue(jobNode)
                End If
            End If
        End Sub

        Protected Overrides Function CreateNodes(references As OpcNodeReferenceCollection) As IEnumerable(Of IOpcNode)
            Dim machineNode = New OpcObjectNode(Me.DefaultNamespace.GetName("Machine"))

            Me.jobsNode = New OpcFolderNode(machineNode, "Jobs")
            references.Add(machineNode, OpcObjectTypes.ObjectsFolder)

            ' Add some predefined jobs.
            Dim job01 = New OpcDataVariableNode(Of Integer)(Me.jobsNode, "JOB01", value:=0)
            Dim job02 = New OpcDataVariableNode(Of Integer)(Me.jobsNode, "JOB02", value:=0)
            Dim job03 = New OpcDataVariableNode(Of Integer)(Me.jobsNode, "JOB03", value:=0)

            ' Define a generic event to notify about job-scheduled.
            Me.jobScheduldedEventNode = New OpcEventNode(Me.jobsNode, "JobSchedulded")
            Me.jobScheduldedEventNode.Severity = OpcEventSeverity.Medium
            Me.jobScheduldedEventNode.Message = "The job has been schedulded."

            Me.jobsNode.AddNotifier(Me.SystemContext, Me.jobScheduldedEventNode)

            ' Define a generic event to notify about job-completed.
            Me.jobCompletedEventNode = New OpcEventNode(Me.jobsNode, "JobCompleted")
            Me.jobCompletedEventNode.Severity = OpcEventSeverity.Medium
            Me.jobCompletedEventNode.Message = "The job has been completed."

            Me.jobsNode.AddNotifier(Me.SystemContext, Me.jobCompletedEventNode)
            Return New IOpcNode() {machineNode}
        End Function
    End Class
End Namespace
