// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace EventTypes
{
    using System;
    using Opc.UaFx.Server;

    /// <summary>
    /// This sample demonstrates how to setup an OPC UA server to provide a custom event type
    /// defined in a nodeset. Its events carry structured data and are raised when a client calls
    /// the StartJob method.
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
