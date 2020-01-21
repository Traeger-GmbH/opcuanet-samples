// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace Server.Playground
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;

    public partial class Program
    {
        #region ---------- Static constructor ----------

        static Program()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, e) => {
                if (e.ExceptionObject is Exception ex) {
                    Console.WriteLine();
                    Console.WriteLine("FAILURE");
                    Console.Write("- Message: {0}", ex.Message);
                    Console.Write("- StackTrace: {0}", ex.StackTrace);
                    Console.ReadLine();

                    Environment.Exit(ex.HResult);
                }
            };
        }

        #endregion

        #region ---------- Public static methods ----------

        public static Uri GetAddress(params string[] uriStrings)
        {
            var address = new Uri("opc.tcp://localhost:48400/");

            for (int index = 0; index < uriStrings.Length; index++) {
                if (Uri.TryCreate(uriStrings[index], UriKind.Absolute, out var result)) {
                    address = result;
                    break;
                }
            }

            return address;
        }

        public static IEnumerable<string> GetHostAddresses(Uri address)
        {
            foreach (var hostAddress in Dns.GetHostAddresses(Dns.GetHostName())) {
                var addressFamily = hostAddress.AddressFamily;

                if (addressFamily == AddressFamily.InterNetwork) {
                    var uriBuilder = new UriBuilder(address);
                    uriBuilder.Host = hostAddress.ToString();

                    yield return uriBuilder.Uri.ToString();
                }
            }
        }

        #endregion
    }
}
