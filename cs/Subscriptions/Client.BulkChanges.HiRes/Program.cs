// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace BulkChanges
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading;

    using Opc.UaFx;
    using Opc.UaFx.Client;

    [SuppressMessage("Style", "IDE1006:Naming Styles")]
    public class Program
    {
        #region ---------- Private static fields ----------

        private static CancellationTokenSource consumerControl;

        private static BlockingCollection<OpcValue[]> dataChanges;
        private static OpcSubscription dataChangeSubscription;

        #endregion

        #region ---------- Public static methods ----------

        public static void Main()
        {
            consumerControl = new CancellationTokenSource();
            dataChanges = new BlockingCollection<OpcValue[]>();

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

        private static IEnumerable<OpcMonitoredItem> CreateMonitoredItems(OpcClient client, OpcNodeId rootNodeId)
        {
            var node = client.BrowseNode(rootNodeId);

            foreach (var childNode in node.Children())
                yield return new OpcMonitoredItem(childNode.NodeId, OpcAttribute.Value);
        }

        private static void HandleDataChange(IEnumerable<OpcDataChangeDataSetItem> dataChangeItems)
        {
            // Determine the according monitored item for each data change.
            ////var monitoredItem = dataChangeSubscription.GetMonitoredItem(dataChangeItem);

            dataChanges.Add(dataChangeItems.Select(item => item.Value).ToArray());
        }

        private static void ConsumeDataChanges(object state)
        {
            var client = (OpcClient)state;
            var subscription = dataChangeSubscription = client.SubscribeNodes();

            subscription.AddMonitoredItem(CreateMonitoredItems(client, "ns=2;s=Data"));
            subscription.ReceivedDataChangeCallback = HandleDataChange;

            // Enforce the fastest supported publishing interval.
            subscription.PublishingInterval = 0;

            // Disable monitored item related cache und data change events.
            subscription.UseMonitoredItemDataCache = false;

            // Commit recent changes to the subscription.
            subscription.ApplyChanges();

            while (!consumerControl.IsCancellationRequested) {
                try {
                    var values = dataChanges.Take(consumerControl.Token);

                    for (int index = 0; index < values.Length; index++) {
                        var value = values[index];

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
                }
                catch (OperationCanceledException) {
                    break;
                }
            }
        }

        #endregion
    }
}
