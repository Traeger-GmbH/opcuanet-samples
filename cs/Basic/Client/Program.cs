// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Opc.UaFx;
    using Opc.UaFx.Client;

    public class Program
    {
        private static List<int> values = new List<int>();

        public static void Main(string[] args)
        {
            var id = new OpcNodeId("a", 1);
            var ids = new List<OpcNodeId>();

            ids.Add(id);
            ids.Add(id);
            ids.Add(id);
            ids.Add(id);
            ids.Add(new OpcNodeId("a", 1));

            var xx = ids.Distinct().ToArray();

            using (var client = new OpcClient("opc.tcp://192.168.0.83:4840")) {
                client.Connect();

                var sub = client.SubscribeNodes(
                        new OpcSubscribeDataChange("ns=3;s=\"FlankenDB\".\"FlankentriggerCounter\"", OpcAttribute.Value, new OpcDataChangeFilter(OpcDataChangeTrigger.StatusValue), HandleDataChanged),
                        new OpcSubscribeDataChange("ns=3;s=\"FlankenDB\".\"FlankentriggerCounter\"", HandleDataChanged));

                sub.PublishingInterval = 1000;
                sub.ApplyChanges();

                Console.WriteLine("Subscribed.");
                Console.ReadLine();
            }
        }


        private static void HandleDataChanged(object sender, OpcDataChangeReceivedEventArgs e)
        {
            var item = (OpcMonitoredItem)sender;
            var last = item.LastDataChange;
            //var last = e.Item;

            var value = last.Value.As<int>();

            if (values.Contains(value))
                System.Diagnostics.Debugger.Break();

            values.Add(value);

            Console.WriteLine($"{DateTime.Now.ToString("o")} {last.Value} {last.Value.ServerTimestamp} {last.Value.ServerPicoseconds} Data Change from NodeId: '{item.NodeId.ValueAsString}'");
            Console.WriteLine(new string('-', 100));
        }
    }
}
