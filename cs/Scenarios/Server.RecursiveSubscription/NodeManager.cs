// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace RecursiveSubscription
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading;

    using Opc.UaFx;
    using Opc.UaFx.Server;

    internal class NodeManager : OpcNodeManager
    {
        #region ---------- Private readonly fields ----------

        private readonly ConcurrentQueue<OpcDataVariableNode> jobs;

        #endregion

        #region ---------- Private fields ----------

        private OpcFolderNode jobsNode;

        private OpcEventNode jobScheduldedEventNode;
        private OpcEventNode jobCompletedEventNode;

        #endregion

        #region ---------- Public constructors ----------

        public NodeManager()
            : base("http://sampleserver/recursivesubscription")
        {
            this.jobs = new ConcurrentQueue<OpcDataVariableNode>();
        }

        #endregion

        #region ---------- Public methods ----------

        public void Schedule(SemaphoreSlim semaphore)
        {
            var random = new Random();
            var context = this.SystemContext;

            while (!semaphore.Wait(3000)) {
                if (this.jobs.TryDequeue(out var jobNode) && jobNode.Value is int stage) {
                    // Raised job-schedulded event.
                    this.jobScheduldedEventNode.ReportEventFrom(context, jobNode);

                    // Simulate a kind of "process" which processes the job.
                    while (!semaphore.Wait(500)) {
                        stage = Math.Min(stage + random.Next(10), 100);

                        jobNode.Timestamp = DateTime.UtcNow;
                        jobNode.Value = stage;

                        jobNode.ApplyChanges(context);

                        if (stage == 100)
                            break;
                    }

                    // Raised job-completed event.
                    this.jobCompletedEventNode.ReportEventFrom(context, jobNode);
                }
            }
        }

        #endregion

        #region ---------- Protected methods ----------

        protected override void AddNode(
                OpcContext context,
                IOpcNode node,
                IEnumerable<IOpcNodeReferenceAware> references)
        {
            base.AddNode(context, node, references);

            // "Catch" nodes added by the client and "schedule" job nodes.
            if (node.Parent == this.jobsNode) {
                if (node is OpcDataVariableNode jobNode)
                    this.jobs.Enqueue(jobNode);
            }
        }

        protected override IEnumerable<IOpcNode> CreateNodes(OpcNodeReferenceCollection references)
        {
            var machineNode = new OpcObjectNode(this.DefaultNamespace.GetName("Machine"));

            this.jobsNode = new OpcFolderNode(machineNode, "Jobs");
            references.Add(machineNode, OpcObjectTypes.ObjectsFolder);

            // Add some predefined jobs.
            new OpcDataVariableNode<int>(this.jobsNode, "JOB01", value: 0);
            new OpcDataVariableNode<int>(this.jobsNode, "JOB02", value: 0);
            new OpcDataVariableNode<int>(this.jobsNode, "JOB03", value: 0);

            // Define a generic event to notify about job-scheduled.
            this.jobScheduldedEventNode = new OpcEventNode(this.jobsNode, "JobSchedulded");
            this.jobScheduldedEventNode.Severity = OpcEventSeverity.Medium;
            this.jobScheduldedEventNode.Message = "The job has been schedulded.";

            this.jobsNode.AddNotifier(this.SystemContext, this.jobScheduldedEventNode);

            // Define a generic event to notify about job-completed.
            this.jobCompletedEventNode = new OpcEventNode(this.jobsNode, "JobCompleted");
            this.jobCompletedEventNode.Severity = OpcEventSeverity.Medium;
            this.jobCompletedEventNode.Message = "The job has been completed.";

            this.jobsNode.AddNotifier(this.SystemContext, this.jobCompletedEventNode);
            return new IOpcNode[] { machineNode };
        }

        #endregion
    }
}
