// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace DataTypeNode
{
    using System;

    using Opc.UaFx;
    using Opc.UaFx.Client;

    /// <summary>
    /// This sample demonstrates how to access and work with nodes which use custom data types.
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

            var statusNode = client.BrowseNode("ns=2;s=Machine_1/Status") as OpcVariableNodeInfo;

            if (statusNode != null) {
                var statusValues = statusNode.DataType.GetEnumMembers();

                var currentStatus = client.ReadNode(statusNode.NodeId);
                var currentStatusValue = null as OpcEnumMember;

                foreach (var statusValue in statusValues) {
                    if (statusValue.Value == currentStatus.As<int>()) {
                        currentStatusValue = statusValue;
                        break;
                    }
                }

                Console.WriteLine(
                        "Status: {0} ({1})",
                        currentStatusValue.Value,
                        currentStatusValue.Name);

                Console.WriteLine("-> " + currentStatusValue.Description);

                Console.WriteLine();
                Console.WriteLine("Possible status values...");

                foreach (var statusValue in statusValues)
                    Console.WriteLine("{0} = {1}", statusValue.Value, statusValue.Name);

                Console.Write("Enter new status: ");
                var value = Console.ReadLine();

                var newStatus = 0;

                if (int.TryParse(value, out newStatus))
                    client.WriteNode(statusNode.NodeId, newStatus);

                currentStatus = client.ReadNode(statusNode.NodeId);

                foreach (var statusValue in statusValues) {
                    if (statusValue.Value == currentStatus.As<int>()) {
                        currentStatusValue = statusValue;
                        break;
                    }
                }

                Console.WriteLine();
                Console.WriteLine(
                        "New Status: {0} ({1})",
                        currentStatusValue.Value,
                        currentStatusValue.Name);

                Console.WriteLine("-> " + currentStatusValue.Description);
            }

            client.Disconnect();
            Console.ReadKey(true);
        }

        #endregion
    }
}
