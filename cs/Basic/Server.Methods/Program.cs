// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace Methods
{
    using System;

    using Opc.UaFx;
    using Opc.UaFx.Server;

    /// <summary>
    /// This sample demonstrates how to setup an OPC UA server to provide method nodes.
    /// </summary>
    public class Program
    {
        #region ---------- Public static methods ----------

        public static void Main(string[] args)
        {
            // Async method execution can be globally enabled for every server and node manager.
            // This results into the use of one worker thread per method being called. Independent
            // whether the method is implemented using the TPL (using Tasks) or not.
            // By default only TPL methods are executed in parallel.
            ////OpcAutomatism.UseAsyncMethodCalls = true;

            // If the server domain name does not match localhost just replace it
            // e.g. with the IP address or name of the server machine.
            OpcServer server = new OpcServer(
                    "opc.tcp://localhost:4840/SampleServer",
                    new SampleNodeManager());

            //// NOTE: All node specific code will be found in the SampleNodeManager.cs.

            server.Start();

            Console.WriteLine("Started.");
            Console.ReadKey(true);

            server.Stop();
        }

        #endregion
    }
}
