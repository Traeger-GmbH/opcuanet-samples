// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace ConfiguredViaCode
{
    using System;
    using System.IO;
    
    using Opc.UaFx;
    using Opc.UaFx.Client;

    /// <summary>
    /// This sample demonstrates how to configure an OPC UA client using in code configuration.
    /// </summary>
    public class Program
    {
        #region ---------- Public static methods ----------

        public static void Main(string[] args)
        {
            //// To simple use the in code configuration you just need to configure your client
            //// instance using the Configuration property of it.
            //// By default it is not necessary to explicitly configure an OPC UA client. But in case
            //// of advanced and productive scenarios you will have to.

            // If the server domain name does not match localhost just replace it
            // e.g. with the IP address or name of the server machine.
            var client = new OpcClient("opc.tcp://localhost:4840/SampleServer");

            //// There are different ways to configure the client instance.

            // 1st Way: Use the default configuration which does use information from the environment
            //         to setup the client. To use the default configuration you either do not take
            //         any changes on the Configuration property of the client instance or set its
            //         value to null (Nothing in Visual Basic).
            client.Configuration = null;

            // 2nd Way: Instantiate a default configuration, manipulate it and set these instance as
            //         the new configuration to use by the client.
            var configuration = new OpcApplicationConfiguration(OpcApplicationType.Client);
            configuration.ClientConfiguration.DefaultSessionTimeout = 300000; // 5 Minutes

            // ApplicationName
            //
            // Using the default configuration or a new instance of the OpcApplicationConfiguration
            // means, that the client does use the value specified in the AssemblyTitleAttribute
            // of your executable as the ApplicationName. This value should maybe changed to the
            // ApplicationName specified in your client certificate. To do that just use a line of
            // code like the following one.
            client.Configuration.ApplicationName = "My Application Name";

            // Certificate Stores
            //
            // Using the default configuration or a new instance of the OpcApplicationConfiguration
            // means, that the client does use the path
            // "%CommonApplicationData%\OPC Foundation\CertificateStores\" as the root directory of
            // - the "MachineDefault" application certificates store
            // - the "RejectedCertificates" certificates store
            // - the "UA Certificate Authorities" trusted issuer certificates store
            // - the "UA Applications" trusted peer certificates store.
            // While the first directory "%CommonApplicationData%" does point to (on a default
            // windows installation to the C: drive) the path "C:\ProgramData" it is obvious that
            // this directories are accessable by any user of the system and that the certificate
            // stores are not in the same directory as your client application.
            // In some scenarios it would be necessary to change the location of the used
            // certificate store directories like the following code does demonstrate.
            var securityConfiguration = client.Configuration.SecurityConfiguration;

            securityConfiguration.ApplicationCertificate.StorePath
                    = Path.Combine(Environment.CurrentDirectory, "App Certificates");
            securityConfiguration.RejectedCertificateStore.StorePath
                    = Path.Combine(Environment.CurrentDirectory, "Rejected Certificates");
            securityConfiguration.TrustedIssuerCertificates.StorePath
                    = Path.Combine(Environment.CurrentDirectory, "Trusted Issuer Certificates");
            securityConfiguration.TrustedIssuerCertificates.StorePath
                    = Path.Combine(Environment.CurrentDirectory, "Trusted Peer Certificates");

            // In case you want to take use of one of the SpecialFolders defined by the
            // Environment.SpecialFolder enumeration you can use the enumeration members name as
            // a placeholder in your custom StorePath like the following code does demonstrate.
            securityConfiguration.ApplicationCertificate.StorePath
                    = @"%LocalApplicationData%\My Application\App Certificates";
            securityConfiguration.RejectedCertificateStore.StorePath
                    = @"%LocalApplicationData%\My Application\Rejected Certificates";
            securityConfiguration.TrustedIssuerCertificates.StorePath
                    = @"%LocalApplicationData%\My Application\Trusted Issuer Certificates";
            securityConfiguration.TrustedPeerCertificates.StorePath
                    = @"%LocalApplicationData%\My Application\Trusted Peer Certificates";

            //// It is not necessary that all certificate stores have to point to the same root
            //// directory as above. Each store can also point to a totally different directory.

            client.Configuration = configuration;

            // 3rd Way: Directly change the default configuration of the client instance using the
            //         Configuration property.
            client.Configuration.ClientConfiguration.DefaultSessionTimeout = 300000; // 5 Minutes

            client.Connect();
            client.Disconnect();

            // In case you are using the OpcClientApplication class, you can directly configure
            // your client/application using the Configuration property of the application instance
            // as the following code does demonstrate.
            var app = new OpcClientApplication("opc.tcp://localhost:4840/SampleServer");
            app.Configuration.ClientConfiguration.DefaultSessionTimeout = 300000; // 5 Minutes

            app.Run();
        }

        #endregion
    }
}
