// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace Umati
{
    using System;
    using Opc.UaFx.Client;

    public class Program
    {
        public static void Main()
        {
            using (var client = new OpcClient("opc.tcp://localhost:4840")) {
                client.Connect();

                // Resolve namespaceIndex of namespace.
                MachineTool.Namespace.Resolve(client);

                var filter = OpcFilter.Using(client)
                        .FromEvents(MachineTool.AlertType)
                        .Select();

                client.SubscribeEvent(Opc.Ua.ObjectIds.Server, filter, (sender, e) => {
                    if (e.Event is Alert alert) {
                        Console.WriteLine(new Guid(alert.EventId.ToArray()).ToString());
                        Console.WriteLine("- Source: {0}", alert.SourceName);
                        Console.WriteLine("- ErrorCode: {0}", alert.ErrorCode);

                        alert.Confirm(client, "Confirmed by John.");
                    }
                });

                Console.WriteLine("Press any key to exit.");
                Console.ReadKey(true);
            }
        }
    }
}
