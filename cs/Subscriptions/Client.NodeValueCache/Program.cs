// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace NodeValueCache
{
    using System;

    using Opc.UaFx.Client;

    internal static class Program
    {
        #region ---------- Private static methods ----------

        private static void Main()
        {
            using var client = new OpcClient("opc.tcp://localhost/");
            client.Connect();

            using var manager = new OpcClientNodeItemManager(client);
            manager.Start();

            var item = new NodeItem("ns=2;s=Machine_1/Temperature");
            item.ValueChanged += (s, e) => {
                Console.WriteLine("Value Changed: " + e.Value);
            };

            manager.AddItem("x1", item);

            Console.WriteLine("Press key to retrieve cached value...");

            while (true) {
                Console.ReadKey();

                var val = item.Value;
                Console.WriteLine("Cached Value: " + val);
            }
        }

        #endregion
    }
}