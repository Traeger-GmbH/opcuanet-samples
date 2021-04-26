// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace HandshakeChanges
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

        private static readonly int Interval = 250;
        private static readonly int NumberOfNodes = 50000;

        #endregion

        #region ---------- Private static fields ----------

        private static CancellationTokenSource producerControl;
        private static SemaphoreSlim dataProcessed;

        private static OpcFolderNode dataNode;
        private static List<OpcDataVariableNode<int>> dataNodes;

        private static OpcDataVariableNode<bool> dataAvailableNode;
        private static OpcDataVariableNode<bool> dataProcessedNode;

        private static OpcDataVariableNode<DateTime> timestampNode;

        #endregion

        #region ---------- Public static methods ----------

        public static void Main(string[] args)
        {
            producerControl = new CancellationTokenSource();
            dataProcessed = new SemaphoreSlim(0);

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
            dataAvailableNode = new OpcDataVariableNode<bool>(dataNode, "DataAvailable");

            dataProcessedNode = new OpcDataVariableNode<bool>(dataNode, "DataProcessed");
            dataProcessedNode.WriteVariableValueCallback = WriteDataProcessed;

            for (int index = 0; index < NumberOfNodes; index++)
                dataNodes.Add(new OpcDataVariableNode<int>(dataNode, $"Var{index:00000}", value: index));

            timestampNode = new OpcDataVariableNode<DateTime>(dataNode, "Timestamp");
            return dataNode;
        }

        private static void ProduceDataChanges(object state)
        {
            var server = (OpcServer)state;

            while (!producerControl.IsCancellationRequested) {
                dataAvailableNode.Value = false;
                dataAvailableNode.ApplyChanges(server.SystemContext);

                unchecked {
                    foreach (var node in dataNodes)
                        node.Value++;
                }

                dataProcessedNode.Value = false;
                dataAvailableNode.Value = true;

                timestampNode.Value = DateTime.UtcNow;
                dataNode.ApplyChanges(server.SystemContext, recursive: true);

                try {
                    do {
                        Console.WriteLine("{0} HANDSHAKE: New Data available - awaiting data being processed.", DateTime.Now);
                    } while (!dataProcessed.Wait(Interval, producerControl.Token));

                    Console.WriteLine("{0} HANDSHAKE: Completed - processing for {1} ms.", DateTime.Now, Interval);

                    // Wait for "Stop" or perform next handshake changes after interval elapsed
                    if (producerControl.Token.WaitHandle.WaitOne(Interval))
                        break;
                }
                catch (OperationCanceledException) {
                    break;
                }
            }
        }

        private static OpcVariableValue<object> WriteDataProcessed(
                OpcWriteVariableValueContext context,
                OpcVariableValue<object> value)
        {
            if (value.Value is bool condition && condition)
                dataProcessed.Release();

            // Reset DataProcessed
            return new OpcVariableValue<object>(false);
        }

        #endregion
    }
}
