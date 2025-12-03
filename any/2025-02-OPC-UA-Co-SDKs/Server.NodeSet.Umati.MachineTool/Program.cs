// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace NodeSet.Umati
{
    using System;
    using Opc.UaFx.MachineTool;

    /// <summary>
    /// This use case demonstrates how to import and implement an alarm from a nodeset using the
    /// companion specifications from UMATI: In this case 'MachineTool'.
    /// </summary>
    public class Program
    {
        #region ---------- Public static methods ----------

        public static void Main(string[] args)
        {
            var nodeManager = new NodeManager();

            using var server = new OpcMachineToolServer("opc.tcp://localhost:4840/", nodeManager);
            server.Start();

            var tool1Node = nodeManager.AddMachineTool("MyTool 1");
            var tool2Node = nodeManager.AddMachineTool("MyTool 2");
            var tool3Node = nodeManager.AddMachineTool("MyTool 3");

            Console.WriteLine("Server started.");
            Console.WriteLine("Press enter to report some alerts.");
            Console.ReadLine();

            // Sample Implementation
            ////tool1Node.Notification.Messages.Report(server.SystemContext, "CODE01");
            ////tool2Node.Notification.Messages.Report(server.SystemContext, "CODE02");
            ////tool3Node.Notification.Messages.Report(server.SystemContext, "CODE03");

            Console.WriteLine("Alerts reported!");
            Console.ReadKey(true);
        }

        #endregion
    }
}
