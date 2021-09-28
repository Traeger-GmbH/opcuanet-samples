// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace DataTypes.Complex
{
    using System;
    using Opc.UaFx.Server;

    /// <summary>
    /// This sample demonstrates how to setup an OPC UA server to provide nodes with
    /// custom data types which are supplied using subtrees there appropriate.
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

            //// NOTE: All DataTypes specific code will be found in the SampleNodeManager.cs.
            server.Start();

            Console.Write("Server started...");
            Console.ReadKey(true);

            server.Stop();
        }

        #endregion
    }
}
