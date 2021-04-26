// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace BulkChanges
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;

    using Opc.UaFx;
    using Opc.UaFx.Client;

    [SuppressMessage("Style", "IDE1006:Naming Styles")]
    public class Program
    {
        #region ---------- Private static fields ----------

        private static CancellationTokenSource consumerControl;
        private static BlockingCollection<OpcValue> dataChanges;

        #endregion

        #region ---------- Public static methods ----------

        public static void Main()
        {
            consumerControl = new CancellationTokenSource();
            dataChanges = new BlockingCollection<OpcValue>();

            var consumer = new Thread(ConsumeDataChanges);

            using (var client = new OpcClient("opc.tcp://localhost:4840")) {
                client.Connect();
                consumer.Start(client);

                Console.WriteLine("Client started - press any key to exit.");
                Console.ReadKey(true);

                consumerControl.Cancel();
                consumer.Join();
            }
        }

        #endregion

        #region ---------- Private static methods ----------

        private static IEnumerable<OpcSubscribeDataChange> CreateCommands(OpcClient client, OpcNodeId rootNodeId)
        {
            var node = client.BrowseNode(rootNodeId);

            foreach (var childNode in node.Children())
                yield return new OpcSubscribeDataChange(childNode.NodeId, HandleDataChange);
        }

        private static void HandleDataChange(object sender, OpcDataChangeReceivedEventArgs e)
        {
            dataChanges.Add(e.Item.Value);
        }

        private static void ConsumeDataChanges(object state)
        {
            var client = (OpcClient)state;
            client.SubscribeNodes(CreateCommands(client, "ns=2;s=Data"));

            while (!consumerControl.IsCancellationRequested) {
                try {
                    var value = dataChanges.Take(consumerControl.Token);

                    if (value.Value is DateTime timestamp) {
                        Console.WriteLine(
                                "{0} BULK: Completed (Duration = {1} ms)",
                                DateTime.Now,
                                DateTime.UtcNow.Subtract(timestamp).TotalMilliseconds);
                    }
                    else {
                        Console.WriteLine(
                                "{0} BULK: New Data: {1}",
                                DateTime.Now,
                                value);
                    }
                }
                catch (OperationCanceledException) {
                    break;
                }
            }
        }

        #endregion
    }
}
