// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace Companion.Machinery
{
    using System;

    using Opc.UaFx;
    using Opc.UaFx.Client;
    using Opc.UaFx.ISA95.JobControl;
    using Opc.UaFx.Machinery;
    using Opc.UaFx.Machinery.Jobs;

    public class Program
    {
        #region ---------- Public static methods ----------

        public static void Main(string[] args)
        {
            var exampleNodeManager = new NodeManager();

            using var server = new OpcMachineryServer(
                    "opc.tcp://localhost:4840/",
                    new OpcMachineryJobsNodeManager(),
                    new OpcISA95JobControlNodeManager(),
                    exampleNodeManager);

            server.Start();
            var machineNode1 = exampleNodeManager.AddMachine("MAC01");

            Console.WriteLine("Server started.");
            Console.ReadKey(true);
        }

        #endregion
    }
}
