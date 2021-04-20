// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace CustomFile
{
    using System;
    using System.Net;

    using Opc.UaFx;
    using Opc.UaFx.Server;

    /// <summary>
    /// This use case represents in which way the IOpcFileInfo interface can be used to implement
    /// "custom file sources" for a OpcFileNode.
    /// </summary>
    public class Program
    {
        #region ---------- Public static methods ----------

        public static void Main(string[] args)
        {
            var fileInfo = new FtpFileInfo(
                    path: "ftp://test.rebex.net/readme.txt",
                    credentials: new NetworkCredential("demo", "password"));

            using (var server = new OpcServer(
                    "opc.tcp://localhost:4840/",
                    new OpcFileNode("readme.txt", fileInfo))) {
                server.Start();

                Console.WriteLine("Server started - press any key to exit.");
                Console.ReadKey(true);
            }
        }

        #endregion
    }
}
