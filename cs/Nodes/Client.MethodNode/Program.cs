// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace DataTypeNode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Opc.UaFx;
    using Opc.UaFx.Client;

    /// <summary>
    /// This sample demonstrates how to access and work with method nodes.
    /// </summary>
    public class Program
    {
        #region ---------- Public static methods ----------

        public static void Main(string[] args)
        {
            //// If the server domain name does not match localhost just replace it
            //// e.g. with the IP address or name of the server machine.

            var client = new OpcClient("opc.tcp://localhost:4840/SampleServer");
            client.Connect();

            var methodsNode = client.BrowseNode("ns=2;s=Methods");
            var methods = BrowseMethods(methodsNode).ToList();

            do {
                Console.WriteLine("Enter index of method to call or 'exit':");

                for (int index = 0; index < methods.Count; index++)
                    Console.WriteLine("{0}. {1}", index, methods[index].Node.DisplayName.Value);

                var line = Console.ReadLine();

                if (line == "exit")
                    break;

                if (int.TryParse(line, out var methodIndex)) {
                    var method = methods[methodIndex];

                    Console.WriteLine();
                    Console.WriteLine("Calling method '{0}' ...", method.Node.DisplayName.Value);

                    var inputs = method.Node.GetInputArguments();

                    foreach (var input in inputs) {
                        Console.WriteLine(
                                "\t[IN] {0} : {1} // {2}",
                                input.Name,
                                OpcDataTypes.GetDataType(input.DataTypeId),
                                input.Description);
                    }

                    var outputs = method.Node.GetOutputArguments();

                    foreach (var output in outputs) {
                        Console.WriteLine(
                                "\t[OUT] {0} : {1} // {2}",
                                string.IsNullOrEmpty(output.Name) ? "ret" : output.Name,
                                OpcDataTypes.GetDataType(output.DataTypeId),
                                output.Description);
                    }

                    var inputArguments = new List<object>();

                    if (inputs.Length > 0) {
                        Console.WriteLine();

                        foreach (var input in inputs) {
                            Console.Write(
                                    "Enter '{0}' value of '{1}': ",
                                    OpcDataTypes.GetDataType(input.DataTypeId),
                                    input.Name);

                            var value = Console.ReadLine();

                            if (input.DataTypeId == OpcDataTypes.String)
                                inputArguments.Add(value);
                            else if (input.DataTypeId == OpcDataTypes.Int32)
                                inputArguments.Add(int.Parse(value));
                        }
                    }

                    try {
                        var outputArguments = client.CallMethod(method.ParentId, method.Node.NodeId, inputArguments);
                        Console.WriteLine("Call succeeded!");

                        for (int index = 0; index < outputs.Length; index++) {
                            var output = outputs[index];

                            Console.WriteLine(
                                    "'{0}' value of '{1}': {2}",
                                    OpcDataTypes.GetDataType(output.DataTypeId),
                                    string.IsNullOrEmpty(output.Name) ? "ret" : output.Name,
                                    outputArguments[index]);
                        }
                    }
                    catch (OpcException ex) {
                        Console.WriteLine("Call failed: {0}", ex.Message);
                    }

                    Console.WriteLine();
                }
            } while (true);

            client.Disconnect();
        }

        #endregion

        #region ---------- Private static methods ----------

        private static IEnumerable<(OpcNodeId ParentId, OpcMethodNodeInfo Node)> BrowseMethods(OpcNodeInfo node)
        {
            var parentId = node.NodeId;

            foreach (var childNode in node.Children()) {
                if (childNode is OpcMethodNodeInfo methodNode) {
                    yield return (parentId, methodNode);
                }
                else {
                    foreach (var childMethod in BrowseMethods(childNode))
                        yield return childMethod;
                }
            }
        }

        #endregion
    }
}
