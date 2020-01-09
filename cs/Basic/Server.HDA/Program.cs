// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace HDA
{
    using System;
    using Opc.UaFx.Server;

    /// <summary>
    /// This sample demonstrates how to setup an OPC UA server to provide HDA to its
    /// nodes provided.
    /// </summary>
    public class Program
    {
        #region ---------- Public static methods ----------

        public static void Main(string[] args)
        {
            // If the server domain name does not match localhost just replace it
            // e.g. with the IP address or name of the server machine.
            OpcServer server = new OpcServer(
                    "opc.tcp://localhost:4840/SampleServer",
                    new SampleNodeManager());

            //// NOTE: All HDA specific code will be found in the SampleNodeManager.cs.

            server.Start();
            Console.ReadKey(true);
            server.Stop();
        }

        #endregion
    }
}
