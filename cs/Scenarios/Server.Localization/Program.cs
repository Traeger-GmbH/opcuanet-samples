// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace Localization
{
    using System;
    using System.Collections.Generic;

    using Opc.UaFx;
    using Opc.UaFx.Server;

    /// <summary>
    /// This use case realizes restricted node access using built-in ACL functionality and
    /// subclassing for user dependent node metadata.
    /// </summary>
    public class Program
    {
        #region ---------- Public static methods ----------

        public static void Main(string[] args)
        {
            var node = new OpcFolderNode("MyFolder");

            node.DisplayName = new OpcText("MyFolder", "en-US", "MyFolder.DisplayName");
            node.Description = new OpcText("MyFolder Description", "en-US", "MyFolder.Description");

            using (var server = new OpcServer("opc.tcp://localhost:4840", node)) {
                server.Globalization.AddResources(
                        "en-US",
                        new KeyValuePair<string, string>("MyFolder.DisplayName", "MyFolder (en)"),
                        new KeyValuePair<string, string>("MyFolder.Description", "MyFolder Description (en)"));
                server.Globalization.AddResources(
                        "de-DE",
                        new KeyValuePair<string, string>("MyFolder.DisplayName", "Mein Ordner (de)"),
                        new KeyValuePair<string, string>("MyFolder.Description", "Meine Ordner Beschreibung (de)"));
                server.Globalization.AddResources(
                        "fr-FR",
                        new KeyValuePair<string, string>("MyFolder.DisplayName", "Mon dossier (fr)"),
                        new KeyValuePair<string, string>("MyFolder.Description", "Description de mon dossier (fr)"));

                server.Start();

                Console.WriteLine("Server started - press any key to exit.");
                Console.ReadKey(true);
            }
        }

        #endregion
    }
}
