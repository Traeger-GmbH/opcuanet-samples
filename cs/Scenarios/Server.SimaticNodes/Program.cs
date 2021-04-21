// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace SimaticNodes
{
    using System;

    using Opc.UaFx;
    using Opc.UaFx.Server;

    /// <summary>
    /// This use case demonstrates how to define a custom factory for node identifiers to
    /// define / use node identifiers using the "SIEMENS SIMATIC" style of dot separated and quoted
    /// node names to construct the node identifier of a node.
    /// </summary>
    public class Program
    {
        #region ---------- Public static methods ----------

        public static void Main(string[] args)
        {
            OpcNodeId.Factory = new SimaticNodeIdFactory();

            var db1 = new OpcFolderNode(
                    "DataBlock1",
                    new OpcDataVariableNode<byte>("MaxByte", value: byte.MaxValue),
                    new OpcDataVariableNode<short>("MaxInt", value: short.MaxValue),
                    new OpcDataVariableNode<int>("MaxDInt", value: int.MaxValue));

            var db2 = new OpcFolderNode(
                    "DataBlock2",
                    new OpcObjectNode("MyStruct",
                            new OpcDataVariableNode<int>("FieldA", value: 10),
                            new OpcDataVariableNode<int>("FieldB", value: 20)));

            using (var server = new OpcServer("opc.tcp://localhost:4840/", db1, db2)) {
                server.Start();

                Console.WriteLine("Server started - press any key to exit.");
                Console.ReadKey(true);
            }
        }

        #endregion
    }
}
