// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace ConfiguredViaXml
{
    using System;
    using System.IO;

    using Opc.UaFx;
    using Opc.UaFx.Client;

    /// <summary>
    /// This sample demonstrates how to configure an OPC UA client using a XML configuration file.
    /// </summary>
    public class Program
    {
        #region ---------- Public static methods ----------

        public static void Main(string[] args)
        {
            //// To simple use the configuration stored within the XML configuration file
            //// beside the client application you just need to load the configuration file as the
            //// following code does demonstrate.
            //// By default it is not necessary to explicitly configure an OPC UA client. But in case
            //// of advanced and productive scenarios you will have to.

            // There are different ways to load the client configuration.
            OpcApplicationConfiguration configuration = null;

            // 1st Way: Load client config using a file path.
            configuration = OpcApplicationConfiguration.LoadClientConfigFile(
                    Path.Combine(Environment.CurrentDirectory, "ClientConfig.xml"));

            // 2nd Way: Load client config specified in a specific section of your App.config.
            configuration = OpcApplicationConfiguration.LoadClientConfig("Opc.UaFx.Client");

            // If the client uris domain name does not match localhost just replace it
            // e.g. with the IP address or name of the client machine.
            var client = new OpcClient("opc.tcp://localhost:4840/SampleClient");

            // To take use of the loaded client configuration, just set it on the client instance.
            client.Configuration = configuration;

            client.Connect();
            client.Disconnect();

            // In case you are using the OpcClientApplication class, you can explicitly trigger
            // loading a configuration file using the App.config as the following code does
            // demonstrate.
            var app = new OpcClientApplication("opc.tcp://localhost:4840/SampleClient");
            app.LoadConfiguration();

            // Alternatively you can assign the manually loaded client configuration on the client
            // instance used by the application instance, as the following code does demonstrate.
            app.Client.Configuration = configuration;

            app.Run();
        }

        #endregion
    }
}
