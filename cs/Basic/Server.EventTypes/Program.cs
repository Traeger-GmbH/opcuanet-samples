// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace EventTypes
{
    using System;
    using Opc.UaFx.Server;

    /// <summary>
    /// This use case demonstrates how to import and implement custom node types using a nodeset.
    /// </summary>
    public class Program
    {
        #region ---------- Public static methods ----------

        public static void Main(string[] args)
        {
            var manager = new NodeManager();

            using (var server = new OpcServer("opc.tcp://localhost:4840/", manager)) {
                server.ApplicationUri = new Uri("http://sampleserver/sampleeventtypes");
                server.Start();

                Console.WriteLine("Server started - now subscribe to Machine node event 'MachineJobEventNode' and call method 'StartJob'.");
                Console.ReadLine();
            }
        }

        #endregion
    }
}
