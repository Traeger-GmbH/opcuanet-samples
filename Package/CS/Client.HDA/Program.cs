// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace HDA
{
    using System;

    using Opc.UaFx;
    using Opc.UaFx.Client;

    /// <summary>
    /// This sample demonstrates how to perform history data access (HDA) on the data provided by
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

            {
                // Read the historical 'Raw' data.
                // - for one specific node.
                // - the whole history in one request.
                var rawHistory = client.ReadNodeHistory(
                        DateTime.UtcNow.Date.AddHours(6),
                        DateTime.UtcNow.Date.AddHours(6).AddSeconds(10),
                        "ns=2;s=Machine_1/Position");

                Console.WriteLine("Read the historical 'Raw' data...");

                foreach (var item in rawHistory)
                    Console.WriteLine("{0} - {1}", item.Timestamp, item.Value);
            }

            {
                // Read the historical 'Raw' data page wise.
                // - for one specific node.
                // - the whole history partitioned into multiple requests.
                var rawHistoryNavigator = client.ReadNodeHistory(
                        DateTime.UtcNow.Date.AddHours(6),
                        DateTime.UtcNow.Date.AddHours(6).AddSeconds(10),
                        2,
                        "ns=2;s=Machine_1/Position");

                Console.WriteLine("Read the historical 'Raw' data page wise...");

                do {
                    foreach (var item in rawHistoryNavigator)
                        Console.WriteLine("{0} - {1}", item.Timestamp, item.Value);

                    Console.Write("Press any key to read the next page...");
                    Console.ReadKey();
                    Console.WriteLine();
                }
                while (rawHistoryNavigator.MoveNextPage());
            }

            {
                // Read the historical 'ModifiedRaw' data.
                // - for one specific node.
                // - the whole history in one request.
                var modifiedRawHistory = client.ReadNodeHistoryModified(
                        DateTime.UtcNow.Date.AddHours(6),
                        DateTime.UtcNow.Date.AddHours(6).AddSeconds(300),
                        "ns=2;s=Machine_1/Position");

                Console.WriteLine("Read the historical 'ModifiedRaw' data...");

                foreach (var item in modifiedRawHistory) {
                    Console.WriteLine("{0} - {1}", item.Timestamp, item.Value);
                    Console.WriteLine("\t[{0}] by {1}", item.ModificationType, item.ModificationUserName);
                }
            }

            {
                // Read the historical 'ModifiedRaw' data page wise.
                // - for one specific node.
                // - the whole history partitioned into multiple requests.
                var modifiedRawHistoryNavigator = client.ReadNodeHistoryModified(
                        DateTime.UtcNow.Date.AddHours(6),
                        DateTime.UtcNow.Date.AddHours(6).AddSeconds(300),
                        2,
                        "ns=2;s=Machine_1/Position");

                Console.WriteLine("Read the historical 'ModifiedRaw' data page wise...");

                do {
                    foreach (var item in modifiedRawHistoryNavigator) {
                        Console.WriteLine("{0} - {1}", item.Timestamp, item.Value);
                        Console.WriteLine("\t[{0}] by {1}", item.ModificationType, item.ModificationUserName);
                    }

                    Console.Write("Press any key to read the next page...");
                    Console.ReadKey();
                    Console.WriteLine();
                }
                while (modifiedRawHistoryNavigator.MoveNextPage());
            }

            {
                // Read the historical data 'at time'.
                // - for one specific node.
                // - the whole history for each time specified.
                var atTimeHistory = client.ReadNodeHistoryAtTime(
                        new DateTime[]
                        {
                            DateTime.UtcNow.Date.AddHours(6),
                            DateTime.UtcNow.Date.AddHours(6).AddSeconds(10),
                            DateTime.UtcNow.Date.AddHours(6).AddSeconds(20),
                            DateTime.UtcNow.Date.AddHours(6).AddSeconds(30),
                            DateTime.UtcNow.Date.AddHours(6).AddSeconds(40),
                            DateTime.UtcNow.Date.AddHours(6).AddSeconds(50),
                        },
                        "ns=2;s=Machine_1/Position");

                Console.WriteLine("Read the historical data 'at time'...");

                foreach (var item in atTimeHistory)
                    Console.WriteLine("{0} - {1}", item.Timestamp, item.Value);
            }

            {
                // Read the historical data 'processed'.
                // - for one specific node.
                // - the whole history is processed on the server side.
                var processedHistory = client.ReadNodeHistoryProcessed(
                        DateTime.UtcNow.Date.AddHours(6),
                        DateTime.UtcNow.Date.AddHours(6).AddMinutes(30),
                        OpcAggregateType.Maximum,
                        "ns=2;s=Machine_1/Position");

                Console.WriteLine("Read the historical data 'processed'...");

                foreach (var item in processedHistory)
                    Console.WriteLine("{0} - {1}", item.Timestamp, item.Value);
            }

            client.Disconnect();
            Console.ReadKey(true);
        }

        #endregion
    }
}
