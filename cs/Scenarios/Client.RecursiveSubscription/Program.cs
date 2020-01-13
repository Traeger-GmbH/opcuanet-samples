namespace RecursiveSubscription
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Opc.UaFx;
    using Opc.UaFx.Client;

    public class Program
    {
        public static void Main()
        {
            using (var client = new OpcClient("opc.tcp://localhost:4841")) {
                client.Connect();
                client.SubscribeEvent("ns=2;s=Machine/Jobs", HandleJobsEvent);

                while (true) {
                    Console.WriteLine("Press any key to schedule a new job.");
                    Console.ReadLine();

                    var jobsNode = client.BrowseNode("ns=2;s=Machine/Jobs");
                    var job = $"JOB{GetLastJob(jobsNode) + 1:D2}";

                    client.AddNode(new OpcAddDataVariableNode<int>(
                            name: job,
                            nodeId: OpcNodeId.Null,
                            parentNodeId: jobsNode.NodeId,
                            value: 0));

                    Console.WriteLine($"CLIENT: New job '{job}' schedulded.");
                }
            }
        }

        private static int GetLastJob(OpcNodeInfo jobsNode)
        {
            var readJobs = new List<OpcReadNode>();

            foreach (var childNode in jobsNode.Children()) {
                if (childNode is OpcVariableNodeInfo jobNode)
                    readJobs.Add(new OpcReadNode(jobNode.NodeId, OpcAttribute.BrowseName));
            }

            var client = jobsNode.Context.Client;

            return (from value in client.ReadNodes(readJobs)
                    where value.Status.IsGood
                    let jobName = value.As<OpcName>().Value
                    let jobId = int.Parse(jobName.Substring("JOB".Length))
                    select jobId).Max();
        }

        private static void HandleJobsEvent(object sender, OpcEventReceivedEventArgs e)
        {
            Console.WriteLine("SERVER: {0} - {1}", e.Event.SourceName, e.Event.Message);
        }
    }
}
