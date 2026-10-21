// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace SimaticNodeSet
{
    using System;
    using Opc.UaFx.Server;

    /// <summary>
    /// This use case demonstrates how to setup an OPC UA server that imports the nodeset of a
    /// SIMATIC S7-1500 controller and identifies itself like the server of the controller, e.g. to
    /// test clients without the hardware.
    /// </summary>
    public class Program
    {
        #region ---------- Public static methods ----------

        public static void Main(string[] args)
        {
            var nodeManager = new NodeManager();

            using (var server = new OpcServer("opc.tcp://localhost:4840/", nodeManager)) {
                server.ApplicationUri = new Uri("urn:SIMATIC.S7-1500.OPC-UA.Application:PLC_1");
                server.Start();

                nodeManager.Start();

                Console.WriteLine("Server started - press any key to exit.");
                Console.ReadKey(true);
            }
        }

        #endregion
    }
}
