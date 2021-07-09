// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace EventTypes
{
    using System;
    using Opc.UaFx.Client;

    /// <summary>
    /// This sample demonstrates how to consume structured data types defined and used by
    /// the OPC UA server.
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

            var filter = OpcFilter.Using(client)
                    .FromEvents("ns=1;s=MachineJobEventType")
                    .Select();

            client.SubscribeNodes(
                    new OpcSubscribeEvent("ns=1;s=Machines/Machine_1", filter, HandleEventReceived),
                    new OpcSubscribeEvent("ns=1;s=Machines/Machine_2", filter, HandleEventReceived));

            Console.WriteLine("Connected & subscribed!");

            client.CallMethod(
                    "ns=1;s=Machines/Machine_1",
                    "ns=1;s=Machines/Machine_1/StartJob",
                    new MachineJob { Number = "0001", RequiredSetup = MachineSetup.Laminator });

            client.CallMethod(
                    "ns=1;s=Machines/Machine_2",
                    "ns=1;s=Machines/Machine_2/StartJob",
                    new MachineJob { Number = "1002", RequiredSetup = MachineSetup.Packager });

            Console.ReadKey(true);
            client.Disconnect();
        }

        #endregion

        #region ---------- Private static methods ----------

        private static void HandleEventReceived(object sender, OpcEventReceivedEventArgs e)
        {
            if (e.Event is MachineJobEvent jobEvent) {
                Console.WriteLine(e.MonitoredItem.NodeId.ValueAsString);

                var job = jobEvent.Job;
                Console.WriteLine(".Number = {0}", job.Number);
                Console.WriteLine(".Required-Setup = {0}", job.RequiredSetup);

                Console.WriteLine();
            }
        }

        #endregion
    }
}
