// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace ClientAddedNodesWithHistory
{
    using System;
    using Opc.UaFx.Server;

    /// <summary>
    /// This use case implementes the necessary logic required to support nodes which are added
    /// through a client application which wants the server to enable historical data.
    /// </summary>
    public class Program
    {
        #region ---------- Public static methods ----------

        public static void Main(string[] args)
        {
            var manager = new NodeManager();

            using (var server = new OpcServer("opc.tcp://localhost:4840/", manager)) {
                server.Start();
                Console.ReadKey(true);
            }
        }

        #endregion
    }
}
