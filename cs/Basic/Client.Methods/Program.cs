// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace Methods
{
    using System;
    using System.Diagnostics;

    using Opc.UaFx;
    using Opc.UaFx.Client;

    /// <summary>
    /// This sample demonstrates how to perform method calls using the OPC UA server
    /// provided method nodes.
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

            // Call a method using its objectId and methodId passing the parameters required.
            client.CallMethod("ns=2;s=Methods", "ns=2;s=Methods/Hello", "John");

            // Call a method using its objectId and methodId passing the typed parameters required.
            client.CallAction<string>("ns=2;s=Methods", "ns=2;s=Methods/Hello", "May");

            // Call a method using its objectId and methodId passing the parameters required.
            var sum1 = (int)client.CallMethod("ns=2;s=Methods", "ns=2;s=Methods/Add", 5, 3)[0];

            // Call a method using its objectId and methodId passing the typed parameters required.
            // Using this way the user code does not need to manually select the result from the
            // results and cast it according to the type of value returned.
            var sum2 = client.CallFunc<int, int, int>("ns=2;s=Methods", "ns=2;s=Methods/Add", 5, 3);

            // Call a method using its objectId and methodId passing the parameters required.
            var diff1 = (int)client.CallMethod("ns=2;s=Methods", "ns=2;s=Methods/Subtract", 8, 7)[0];

            // Call a method using its objectId and methodId passing the typed parameters required.
            // Using this way the user code does not need to manually select the result from the
            // results and cast it according to the type of value returned.
            var diff2 = client.CallFunc<int, int, int>("ns=2;s=Methods", "ns=2;s=Methods/Subtract", 8, 7);

            Console.WriteLine("Starting machines in sequence.");
            var watch = Stopwatch.StartNew();

            // It is possible to call multiple methods using one request, like the following one is used
            // to start multiple machines using just one request.
            client.CallMethods(
                    new OpcCallMethod("ns=2;s=Machines/Machine_1", "ns=2;s=Machines/Machine_1/StartMachine"),
                    new OpcCallMethod("ns=2;s=Machines/Machine_2", "ns=2;s=Machines/Machine_2/StartMachine"),
                    new OpcCallMethod("ns=2;s=Machines/Machine_3", "ns=2;s=Machines/Machine_3/StartMachine"));

            watch.Stop();
            Console.WriteLine("Started machines after: {0} ms", watch.ElapsedMilliseconds);

            watch.Restart();
            Console.WriteLine("Stopping machines in sequence.");

            // It is possible to call multiple methods using one request, like the following one is used
            // to start multiple machines using just one request.
            client.CallMethods(
                    new OpcCallMethod("ns=2;s=Machines/Machine_1", "ns=2;s=Machines/Machine_1/StopMachine"),
                    new OpcCallMethod("ns=2;s=Machines/Machine_2", "ns=2;s=Machines/Machine_2/StopMachine"),
                    new OpcCallMethod("ns=2;s=Machines/Machine_3", "ns=2;s=Machines/Machine_3/StopMachine"));

            watch.Stop();
            Console.WriteLine("Stopped machines after: {0} ms", watch.ElapsedMilliseconds);

            watch.Restart();
            Console.WriteLine("Starting machines in parallel.");

            // It the server provides asynchronous methods (the methods do not necessarily express
            // that in their name) the clients response to "CallMethods" is processed much faster.
            client.CallMethods(
                    new OpcCallMethod("ns=2;s=Machines/Machine_1", "ns=2;s=Machines/Machine_1/StartMachineAsync"),
                    new OpcCallMethod("ns=2;s=Machines/Machine_2", "ns=2;s=Machines/Machine_2/StartMachineAsync"),
                    new OpcCallMethod("ns=2;s=Machines/Machine_3", "ns=2;s=Machines/Machine_3/StartMachineAsync"));

            watch.Stop();
            Console.WriteLine("Started machines after: {0} ms", watch.ElapsedMilliseconds);

            watch.Restart();
            Console.WriteLine("Stopping machines in parallel.");

            // It the server provides asynchronous methods (the methods do not necessarily express
            // that in their name) the clients response to "CallMethods" is processed much faster.
            client.CallMethods(
                    new OpcCallMethod("ns=2;s=Machines/Machine_1", "ns=2;s=Machines/Machine_1/StopMachineAsync"),
                    new OpcCallMethod("ns=2;s=Machines/Machine_2", "ns=2;s=Machines/Machine_2/StopMachineAsync"),
                    new OpcCallMethod("ns=2;s=Machines/Machine_3", "ns=2;s=Machines/Machine_3/StopMachineAsync"));

            watch.Stop();
            Console.WriteLine("Stopped machines after: {0} ms", watch.ElapsedMilliseconds);

            client.Disconnect();
            Console.ReadKey(true);
        }

        #endregion
    }
}
