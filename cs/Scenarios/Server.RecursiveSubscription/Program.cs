// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace RecursiveSubscription
{
    using System;
    using System.Threading;

    using Opc.UaFx.Server;

    /// <summary>
    /// This use case represents in which way a server can monitor newly added nodes, work with
    /// them and notify clients about changes on these nodes.
    /// </summary>
    public class Program
    {
        #region ---------- Public static methods ----------

        public static void Main(string[] args)
        {
            var manager = new NodeManager();

            using (var server = new OpcServer("opc.tcp://localhost:4841/", manager)) {
                server.Start();

                using (var semaphore = new SemaphoreSlim(0)) {
                    var thread = new Thread(() => manager.Schedule(semaphore));
                    thread.Start();

                    Console.WriteLine("Server started - press any key to exit.");
                    Console.ReadKey(true);

                    semaphore.Release();
                    thread.Join();
                }
            }
        }

        #endregion
    }
}
