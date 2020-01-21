// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace Server.Playground
{
    using System;
    using System.Threading;

    using Opc.UaFx.Server;

    /// <summary>
    /// This use case realizes a server which offers different kinds of nodes to easily test
    /// client applications against different possible use cases.
    /// </summary>
    public partial class Program
    {
        public static void Main(string[] args)
        {
            var address = GetAddress(args);
            var hostAddresses = GetHostAddresses(address);

            var manager = new NodeManager();

            using (var server = new OpcServer(address, manager)) {
                Console.Write("Starting server...");
                server.Start();
                Console.WriteLine("started!");

                Console.WriteLine();
                Console.WriteLine("Server listening to:");
                Console.WriteLine($"\t{address}");

                foreach (var hostAddress in hostAddresses)
                    Console.WriteLine($"\t{hostAddress}");

                Console.WriteLine();

                var semaphore = new SemaphoreSlim(0);
                var thread = new Thread(() => manager.Simulate(semaphore));

                thread.Start();

                Console.WriteLine("Press any key to stop the server.");
                Console.ReadKey(true);

                semaphore.Release();
                thread.Join();

                Console.WriteLine();
                Console.Write("Stopping server...");
            }

            Console.Write("stopped!");
        }
    }
}
