// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace AE
{
    using System;
    using System.Threading;

    using Opc.UaFx.Server;

    /// <summary>
    /// This sample demonstrates how to setup an OPC UA server to provide AE to its
    /// nodes provided.
    /// </summary>
    public class Program
    {
        #region ---------- Public static methods ----------

        public static void Main(string[] args)
        {
            var nodeManager = new SampleNodeManager();

            // If the server domain name does not match localhost just replace it
            // e.g. with the IP address or name of the server machine.
            OpcServer server = new OpcServer(
                    "opc.tcp://localhost:4840/SampleServer",
                    nodeManager);

            server.Start();
            //// NOTE: All AE specific code will be found in the SampleNodeManager.cs.

            using (var semaphore = new SemaphoreSlim(0)) {
                var thread = new Thread(() => nodeManager.Simulate(semaphore));
                thread.Start();

                Console.WriteLine("OPC UA Server is running...");
                Console.ReadKey(true);

                semaphore.Release();
                thread.Join();

                server.Stop();
            }
        }

        #endregion
    }
}
