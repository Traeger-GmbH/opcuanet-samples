// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace NodeSet
{
    using System;

    using Opc.UaFx;
    using Opc.UaFx.Server;

    /// <summary>
    /// This use case demonstrates how to import and implement an alarm from a nodeset using the
    /// companion specification from PackML.
    /// </summary>
    public class Program
    {
        #region ---------- Public static readonly fields ----------

        public static readonly OpcNamespace Namespace = OpcNamespace.Create("http://sampleserver/");

        #endregion

        #region ---------- Public static methods ----------

        public static void Main(string[] args)
        {
            var manager = new NodeManager();

            using (var server = new OpcServer("opc.tcp://localhost:4840/", manager)) {
                server.ApplicationUri = Namespace.Uri;
                server.Start();

                PackML.Namespace.Resolve(server);
                PackML.RegisterTypes();

                // Add some objects.
                manager.AddObject();
                manager.AddObject();
                manager.AddObject();

                Console.WriteLine("Server started - press any key to exit.");
                Console.ReadKey(true);
            }
        }

        #endregion
    }
}
