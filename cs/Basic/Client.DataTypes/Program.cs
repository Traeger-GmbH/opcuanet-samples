// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace DataTypes
{
    using System;

    using Opc.UaFx.Client;
    using Opc.UaFx.ServerDataTypes.Machines;

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

            //
            // The data types used in following lines were generated using the OPC Watch.
            // https://docs.traeger.de/en/software/sdk/opc-ua/net/client.development.guide#generate-data-types
            //

            var job = client.ReadNode("ns=2;s=Machines/Machine_1/Job").As<MachineJob>();
            Console.WriteLine("Machine 1 - Job");
            PrintJob(job);

            var order = client.ReadNode("ns=2;s=Machines/Machine_2/Order").As<ManufacturingOrder>();

            Console.WriteLine();
            Console.WriteLine("Machine 2 - Order");
            Console.WriteLine(".Order = {0}", order.Order);
            Console.WriteLine(".Article = {0}", order.Article);

            Console.WriteLine();
            Console.WriteLine("Machine 2 - Order, Jobs");

            foreach (var orderJob in order.Jobs) {
                PrintJob(orderJob);
                Console.WriteLine('-');
            }

            client.Disconnect();
            Console.ReadKey(true);
        }

        #endregion

        #region ---------- Private static methods ----------

        private static void PrintJob(MachineJob job)
        {
            Console.WriteLine(".Number = {0}", job.Number);

            if (job.DurationSpecified)
                Console.WriteLine(".Duration = {0} (Estimated = {1})", job.Duration, job.EstimatedDuration);

            Console.WriteLine(".In-Process = {0}", job.InProcess);
            Console.WriteLine(".Required-Setup = {0}", (MachineSetup)job.RequiredSetup);
            Console.WriteLine(".Schedule-Setup = {0}", job.ScheduleTime);
        }

        #endregion
    }
}
