// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace DynamicNodeManagers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Opc.UaFx.Server;

    /// <summary>
    /// This use case realizes dynamic nodes which are created on-demand by the server depending
    /// on the underlying system needs.
    /// </summary>
    public class Program
    {
        #region ---------- Public static methods ----------

        public static void Main(string[] args)
        {
            while (true) {
                Console.Clear();
                var managers = QueryNodeManagers();

                using (var server = new OpcServer("opc.tcp://localhost:4840/", managers)) {
                    Console.WriteLine("Starting...");
                    server.Start();

                    foreach (var nodeManager in server.NodeManagers.OfType<NodeManager>()) {
                        Console.Write(" - {0}...", nodeManager.Name);
                        nodeManager.Start();
                        Console.WriteLine("done.");
                    }

                    Console.WriteLine("Started.");

                    Console.Write("Press any key to use different node managers.");
                    Console.ReadLine();

                    Console.WriteLine("Stopping...");

                    foreach (var nodeManager in server.NodeManagers.OfType<NodeManager>()) {
                        Console.Write(" - {0}...", nodeManager.Name);
                        nodeManager.Stop();
                        Console.WriteLine("done.");
                    }
                }

                Console.WriteLine("Stopped.");

                Console.Write("Press any key to setup a new server.");
                Console.ReadLine();
            }
        }

        private static IEnumerable<NodeManager> QueryNodeManagers()
        {
            Console.WriteLine("Select node managers...");

            var names = new[] {
                "Doors",
                "Motors",
                "Drills",
                "Cameras",
                "Files",
                "Conveyors",
            };

            for (int index = 0; index < names.Length; index++) {
                var name = names[index];
                Console.Write($"{name}? [y (default)|n]: ");

                do {
                    var input = Console.ReadLine().ToLower();

                    if (input == "n")
                        break;

                    if (input.Length == 0 || input == "y") {
                        yield return new NodeManager(name);
                        break;
                    }

                    Console.WriteLine("Enter y (default) or n: ");
                } while (true);
            }
        }

        #endregion
    }
}
