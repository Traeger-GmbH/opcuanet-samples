// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace NodeSet.Umati
{
    using System;

    using Opc.UaFx;
    using Opc.UaFx.Server;

    /// <summary>
    /// This use case demonstrates how to import and implement an alarm from a nodeset using the
    /// companion specifications from UMATI: In this case 'MachineTool'.
    /// </summary>
    public class Program
    {
        #region ---------- Public static methods ----------

        public static void Main(string[] args)
        {
            var umatiNodeManager = OpcNodeSetManager.Create(
                    OpcNodeSet.Load(@".\NodeSets\Opc.Ua.Di\Opc.Ua.Di.NodeSet2.xml"),
                    OpcNodeSet.Load(@".\NodeSets\Opc.Ua.IA\Opc.Ua.IA.NodeSet2.xml"),
                    OpcNodeSet.Load(@".\NodeSets\Opc.Ua.Machinery\Opc.Ua.Machinery.NodeSet2.xml"),
                    OpcNodeSet.Load(@".\NodeSets\Opc.Ua.MachineTool\Opc.Ua.MachineTool.NodeSet2.xml"));

            var exampleNodeManager = new NodeManager();

            using (var server = new OpcServer("opc.tcp://localhost:4840/", umatiNodeManager, exampleNodeManager)) {
                server.Start();

                // Resolve namespaceIndeces of namespaces.
                Di.Namespace.Resolve(server);
                IA.Namespace.Resolve(server);
                Machinery.Namespace.Resolve(server);
                MachineTool.Namespace.Resolve(server);
                MachineToolExample.Namespace.Resolve(server);

                var tool1Node = exampleNodeManager.AddMachineTool("MyTool 1");
                var tool2Node = exampleNodeManager.AddMachineTool("MyTool 2");
                var tool3Node = exampleNodeManager.AddMachineTool("MyTool 3");

                Console.WriteLine("Server started.");
                Console.WriteLine("Press enter to report some alerts.");
                Console.ReadLine();

                tool1Node.Notification.Messages.Report(server.SystemContext, "CODE01");
                tool2Node.Notification.Messages.Report(server.SystemContext, "CODE02");
                tool3Node.Notification.Messages.Report(server.SystemContext, "CODE03");

                Console.WriteLine("Alerts reported!");
                Console.ReadKey(true);
            }
        }

        #endregion
    }
}
