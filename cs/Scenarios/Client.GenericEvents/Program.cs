// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace GenericEvents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Opc.UaFx;
    using Opc.UaFx.Client;

    public class Program
    {
        public static void Main()
        {
            OpcEvent.RegisterType<GenericEvent>("ns=2;i=1");

            using (var client = new OpcClient("opc.tcp://localhost:4840")) {
                client.UseDynamic = true;
                client.Connect();

                // Define a filter to only handle the custom event.
                var filter = OpcFilter.Using(client)
                        .FromEvents(OpcNodeId.Parse("ns=2;i=1"))
                        .Select();

                var sub = client.SubscribeEvent("ns=2;s=Machine", filter, (sender, e) => {
                    if (e.Event is GenericEvent genericEvent)
                        Print(genericEvent.GetData());
                });

                Console.Write("Waiting for events (press any key to exit)...");
                Console.ReadKey(true);
            }
        }

        private static void Print(Dictionary<string, object> data, int level = 0)
        {
            var indent = new string('.', level * 2);

            foreach (var item in data) {
                if (item.Value is OpcDataObject instance) {
                    Console.WriteLine(indent + item.Key);

                    var instanceData = instance.GetFields().ToDictionary(
                            field => field.Name,
                            field => field.Value);

                    Print(instanceData, level + 1);
                }
                else if (item.Value is OpcDataObject[] instances) {
                    for (int index = 0; index < instances.Length; index++) {
                        Console.WriteLine(indent + "{0}[{1}]", item.Key, index);

                        var instanceData = instances[index].GetFields().ToDictionary(
                                field => field.Name,
                                field => field.Value);

                        Print(instanceData, level + 1);
                    }
                }
                else {
                    Console.WriteLine(indent + "{0} = {1}", item.Key, item.Value);
                }
            }
        }
    }
}
