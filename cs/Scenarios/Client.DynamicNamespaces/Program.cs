// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace DynamicNamespaces
{
    using System;
    using System.Collections.Generic;

    using Opc.UaFx.Client;

    public class Program
    {
        private static Dictionary<string, object> Data = new Dictionary<string, object>();
        private static OpcSubscription Subscription;

        public static void Main()
        {
            using (var client = new OpcClient("opc.tcp://localhost:4840")) {
                client.Connected += HandleClientConnected;
                client.Connect();

                Console.WriteLine("Connected and subscribed.");
                Console.ReadLine();
            }
        }

        private static void HandleClientConnected(object sender, EventArgs e)
        {
            Console.Clear();
            Subscription?.Unsubscribe();

            Subscription = ((OpcClient)sender).SubscribeNodes(
                    new OpcSubscribeDataChange("nsu=http://sampleserver/cameras;s=Cameras/Var9700", HandleDataChange),
                    new OpcSubscribeDataChange("nsu=http://sampleserver/conveyors;s=Conveyors/Var11100", HandleDataChange),
                    new OpcSubscribeDataChange("nsu=http://sampleserver/doors;s=Doors/Var11400", HandleDataChange),
                    new OpcSubscribeDataChange("nsu=http://sampleserver/files;s=Files/Var10800", HandleDataChange),
                    new OpcSubscribeDataChange("nsu=http://sampleserver/motors;s=Motors/Var11400", HandleDataChange));

            foreach (var monitoredItem in Subscription.MonitoredItems) {
                Data[monitoredItem.NodeId.ValueAsString] = null;

                if (monitoredItem.Status.IsCreated) {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("{0} - active!", monitoredItem.NodeId.Value);
                }
                else {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("{0} - inactive!", monitoredItem.NodeId.Value);
                }

                Console.ResetColor();
            }

            Console.WriteLine(new string('-', 30));
        }

        public static void HandleDataChange(object sender, OpcDataChangeReceivedEventArgs e)
        {
            Data[e.MonitoredItem.NodeId.ValueAsString] = e.Item.Value.As<object>();
            Console.SetCursorPosition(0, 6);

            foreach (var item in Data)
                Console.WriteLine("{0} = {1}          ", item.Key, item.Value ?? "<missing>");
        }
    }
}
