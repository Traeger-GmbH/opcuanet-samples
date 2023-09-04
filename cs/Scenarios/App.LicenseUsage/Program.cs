// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

// The following assembly attribute controls whether the obfuscator shall use string encryption.
// However this option does not need to be specified explicitly, because of string encryption is
// enabled / used by default. Therefore this attribute can be used to disable the string encryption
// feature. To disable the string encryption just uncomment the following lines of code.
// Online Reference: https://documentation.help/Eazfuscator.NET/ch04s06.html
// 
////using System.Reflection;
////[assembly: Obfuscation(Feature = "string encryption", Exclude = true)]

namespace LicenseUsage
{
    using System;

    using Opc.UaFx;
    using Opc.UaFx.Client;
    using Opc.UaFx.Server;

    /// <summary>
    /// This use case demonstrates how to use a license key in a secure way using "string
    /// encryption" when obfuscating your application. To do so we are using the Eazfuscator from
    /// Gapotchenko. Depending on which obfuscator you want to use the configuration might be
    /// slightly different. Please refer to your obfuscators documentation for "string encryption".
    /// </summary>
    /// <remarks>The Eazfuscator is part of the projects configuration and is executed whenever
    /// this project is build using the "Release" configuration.</remarks>
    public class Program
    {
        #region ---------- Public static methods ----------

        public static void Main()
        {
            // Use the following line to activate/use the license purchased for ...
            // 
            // - OPC UA Client .NET SDK – Branch Lic.
            // - OPC UA Client .NET SDK – Single Dev. Lic.
            // - OPC (UA) Client .NET SDK – Branch Lic.
            // - OPC (UA) Client .NET SDK – Single Dev. Lic.
            // 
            Opc.UaFx.Client.Licenser.LicenseKey = "replace this string with the license key";

            // Use the following line to activate/use a license purchased for ...
            // 
            // - OPC UA Server .NET SDK – Branch Lic.
            // - OPC UA Server .NET SDK – Single Dev. Lic.
            // 
            Opc.UaFx.Server.Licenser.LicenseKey = "replace this string with the license key";

            // Use the following line to activate/use a license purchased for ...
            // 
            // - OPC UA Client & Server .NET SDK – Branch Lic.
            // - OPC UA Client & Server .NET SDK – Single Dev. Lic.
            // - OPC (UA) CL&UA SV .NET SDK – Branch Lic.
            // - OPC (UA) CL&UA SV .NET SDK – Single Dev. Lic.
            // 
            Opc.UaFx.Licenser.LicenseKey = "replace this string with the license key";

            using (var server = new OpcServer(
                    "opc.tcp://localhost:4840",
                    new OpcDataVariableNode("Hello", value: "Hello World!"))) {

                server.Start();

                using (var client = new OpcClient(server.Address)) {
                    client.Connect();
                    Console.WriteLine(client.ReadNode("ns=2;s=Hello"));
                }
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey(true);
        }

        #endregion
    }
}
