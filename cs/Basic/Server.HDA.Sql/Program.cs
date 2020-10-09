// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace HDA.Sql
{
    using System;

    using Opc.UaFx;
    using Opc.UaFx.Server;

    /// <summary>
    /// This sample demonstrates how to setup an OPC UA server to provide HDA via SQL to its
    /// nodes provided.
    /// </summary>
    public class Program
    {
        #region ---------- Public static methods ----------

        public static void Main(string[] args)
        {
            var manager = new SampleNodeManager();

            // If the server domain name does not match localhost just replace it
            // e.g. with the IP address or name of the server machine.
            OpcServer server = new OpcServer("opc.tcp://localhost:4840/SampleServer", manager);

            //// NOTE: All HDA specific code will be found in the SampleNodeManager.cs.

            server.Start();
            CreateHistoryEntries(manager);

            Console.Write("Server started - press any key to exit.");
            Console.ReadKey(true);

            server.Stop();
        }

        #endregion

        #region ---------- Private static methods ----------

        private static void CreateHistoryEntries(SampleNodeManager manager)
        {
            var random = new Random();

            if (manager.PositionHistorian.History.IsEmpty()) {
                CreateHistoryEntries(
                        manager.PositionHistorian,
                        modifier => random.Next(-1000, 1000) / modifier);
            }

            manager.PositionHistorian.AutoUpdateHistory = true;

            if (manager.TemperatureHistorian.History.IsEmpty()) {
                CreateHistoryEntries(
                        manager.TemperatureHistorian,
                        modifier => (double)random.Next(-70, 150) / modifier);
            }

            manager.TemperatureHistorian.AutoUpdateHistory = true;
        }

        private static void CreateHistoryEntries<T>(
                SampleHistorian historian,
                Func<int, T> nextValue)
        {
            for (int second = 0; second < 60; second++) {
                var timestamp = DateTime.UtcNow.Date.AddHours(6).AddSeconds(second);

                var modifier = Math.Max(10, second) / 10;
                var value = new OpcHistoryValue(nextValue(modifier), timestamp);

                if ((second % 30) == 0) {
                    historian.ModifiedHistory.Add(value.CreateModified(
                            OpcHistoryModificationType.Delete, "Anonymous", value.Timestamp));
                }
                else {
                    historian.History.Add(value);
                }
            }
        }

        #endregion
    }
}
