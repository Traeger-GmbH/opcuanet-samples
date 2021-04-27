// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace BulkChanges
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;

    using Opc.UaFx;
    using Opc.UaFx.Server;

    /// <summary>
    /// 
    /// </summary>
    [SuppressMessage("Style", "IDE1006:Naming Styles")]
    public class Program
    {
        #region ---------- Private static readonly fields ----------

        private static readonly int Interval = 5000;
        private static readonly int NumberOfNodes = 500;

        #endregion

        #region ---------- Private static fields ----------

        private static CancellationTokenSource producerControl;

        private static OpcFolderNode dataNode;
        private static List<OpcDataVariableNode<int>> dataNodes;

        private static OpcDataVariableNode<DateTime> timestampNode;

        #endregion

        #region ---------- Public static methods ----------

        public static void Main(string[] args)
        {
            producerControl = new CancellationTokenSource();

            dataNodes = new List<OpcDataVariableNode<int>>();
            dataNode = CreateDataNode();

            var producer = new Thread(ProduceDataChanges);

            using (var server = new OpcServer("opc.tcp://localhost:4840/", dataNode)) {
                server.Start();
                producer.Start(server);

                Console.WriteLine("Server started - press any key to exit.");
                Console.ReadKey(true);

                producerControl.Cancel();
                producer.Join();
            }
        }

        #endregion

        #region ---------- Private static methods ----------

        private static OpcFolderNode CreateDataNode()
        {
            var dataNode = new OpcFolderNode("Data");

            for (int index = 0; index < NumberOfNodes; index++)
                dataNodes.Add(new OpcDataVariableNode<int>(dataNode, $"Var{index:00000}", value: index));

            timestampNode = new OpcDataVariableNode<DateTime>(dataNode, "Timestamp");
            return dataNode;
        }

        private static void ProduceDataChanges(object state)
        {
            var server = (OpcServer)state;

            while (!producerControl.IsCancellationRequested) {
                lock (state) {
                    unchecked {
                        foreach (var node in dataNodes)
                            node.Value++;
                    }

                    timestampNode.Value = DateTime.UtcNow;
                    dataNode.ApplyChanges(server.SystemContext, recursive: true);
                }

                Console.WriteLine("{0} BULK: New Data available - pausing for {1} ms.", DateTime.Now, Interval);

                // Wait for "Stop" or perform next bulk changes after interval elapsed
                if (producerControl.Token.WaitHandle.WaitOne(Interval))
                    break;
            }
        }

        #endregion
    }
}
