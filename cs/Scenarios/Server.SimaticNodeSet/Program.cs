// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace SimaticNodeSet
{
    using System;
    using Opc.UaFx.Server;

    /// <summary>
    /// This use case demonstrates how to define a custom server which can be used to simulate a
    /// "SIEMENS SIMATIC OPC UA Server" using a once exported information model (= nodeset).
    /// </summary>
    /// <remarks>This implementation provides only a frame to start with the development.</remarks>
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
