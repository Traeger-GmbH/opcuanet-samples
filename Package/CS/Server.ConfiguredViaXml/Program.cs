// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace ConfiguredViaXml
{
    using System;
    using System.IO;

    using Opc.UaFx;
    using Opc.UaFx.Server;

    /// <summary>
    /// This sample demonstrates how to configure an OPC UA server using a XML configuration file.
    /// </summary>
    public class Program
    {
        #region ---------- Public static methods ----------

        public static void Main(string[] args)
        {
            //// To simple use the configuration stored within the XML configuration file
            //// beside the server application you just need to load the configuration file as the
            //// following code does demonstrate.
            //// By default it is not necessary to explicitly configure an OPC UA server. But in case
            //// of advanced and productive scenarios you will have to.

            // There are different ways to load the server configuration.
            OpcApplicationConfiguration configuration = null;

            // 1st Way: Load server config using a file path.
            configuration = OpcApplicationConfiguration.LoadServerConfigFile(
                    Path.Combine(Environment.CurrentDirectory, "ServerConfig.xml"));

            // 2nd Way: Load server config specified in a specific section of your App.config.
            configuration = OpcApplicationConfiguration.LoadServerConfig("Opc.UaFx.Server");

            // If the server domain name does not match localhost just replace it
            // e.g. with the IP address or name of the server machine.
            var server = new OpcServer(
                    "opc.tcp://localhost:4840/SampleServer",
                    new SampleNodeManager());

            // To take use of the loaded server configuration, just set it on the server instance.
            server.Configuration = configuration;

            server.Start();
            server.Stop();

            // In case you are using the OPC UA server (Service) Application class, you can explicitly
            // trigger loading a configuration file using the App.config as the following code does
            // demonstrate.
            var app = new OpcServerApplication(
                    "opc.tcp://localhost:4840/SampleServer",
                    new SampleNodeManager());

            app.LoadConfiguration();

            // Alternatively you can assign the manually loaded server configuration on the server
            // instance used by the application instance, as the following code does demonstrate.
            app.Server.Configuration = configuration;

            app.Run();
        }

        #endregion
    }
}
