// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace HandshakeChanges
{
    using System;
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
        private static SemaphoreSlim dataAvailable;

        #endregion

        #region ---------- Public static methods ----------

        public static void Main()
        {
            consumerControl = new CancellationTokenSource();
            dataAvailable = new SemaphoreSlim(0);

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

        private static IEnumerable<OpcNodeId> BrowseNodes(OpcClient client, OpcNodeId rootNodeId)
        {
            var rootNode = client.BrowseNode(rootNodeId);

            foreach (var node in rootNode.Children()) {
                var name = node.Name.Value;

                if (name.StartsWith("Var") || name == "Timestamp")
                    yield return node.NodeId;
            }
        }

        private static void HandleDataChange(object sender, OpcDataChangeReceivedEventArgs e)
        {
            if (e.Item.Value.As<bool>())
                dataAvailable.Release();
        }

        private static void ConsumeDataChanges(object state)
        {
            var client = (OpcClient)state;
            client.SubscribeDataChange("ns=2;s=Data/DataAvailable", HandleDataChange);

            var nodeIds = BrowseNodes(client, "ns=2;s=Data").ToArray();

            while (!consumerControl.IsCancellationRequested) {
                try {
                    Console.WriteLine("{0} HANDSHAKE: Awaiting data being available.", DateTime.Now);
                    dataAvailable.Wait(consumerControl.Token);

                    var values = client.ReadNodes(nodeIds);
                    client.WriteNode("ns=2;s=Data/DataProcessed", value: true);

                    foreach (var value in values) {
                        if (value.Value is DateTime timestamp) {
                            Console.WriteLine(
                                    "{0} HANDSHAKE: Completed (Duration = {1} ms)",
                                    DateTime.Now,
                                    DateTime.UtcNow.Subtract(timestamp).TotalMilliseconds);
                        }
                        else {
                            Console.WriteLine(
                                    "{0} HANDSHAKE: New Data: {1}",
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
