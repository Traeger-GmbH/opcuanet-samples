// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace NodeSet
{
    using System;
    using Opc.UaFx.Server;

    /// <summary>
    /// This use case demonstrates how to import and implement an alarm from a nodeset using the
    /// companion specification from UMATI.
    /// </summary>
    public class Program
    {
        #region ---------- Public static methods ----------

        public static void Main(string[] args)
        {
            var manager = new NodeManager();

            using (var server = new OpcServer("opc.tcp://localhost:4840/", manager)) {
                server.Start();

                Console.WriteLine("Server started - press enter to report an event.");
                var line = Console.ReadLine();

                while (line.Length == 0) {
                    manager.ReportEvent();
                    line = Console.ReadLine();
                }
            }
        }

        #endregion
    }
}
