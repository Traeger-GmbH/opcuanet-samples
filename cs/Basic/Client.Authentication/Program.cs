// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace Authentication
{
    using System;
    using Opc.UaFx.Client;

    /// <summary>
    /// This sample demonstrates how to authenticate at an OPC UA server using user name
    /// and password.
    /// </summary>
    public class Program
    {
        #region ---------- Public static methods ----------

        public static void Main(string[] args)
        {
            //// If the server domain name does not match localhost just replace it
            //// e.g. with the IP address or name of the server machine.

            var client = new OpcClient("opc.tcp://localhost:4840/SampleServer");

            // Just configure the OpcClient instance with an appropriate user identity with
            // the name of the user and its password to use to authenticate.
            client.Security.UserIdentity = new OpcClientIdentity("username", "password");

            client.Connect();
            Console.WriteLine("ReadNode: {0}", client.ReadNode("ns=2;s=Machine_1/IsActive"));

            try {
                client.WriteNode("ns=2;s=Machine_1/IsActive", false);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("ReadNode: {0}", client.ReadNode("ns=2;s=Machine_1/IsActive"));

            client.Disconnect();
            Console.ReadKey(true);
        }

        #endregion
    }
}
