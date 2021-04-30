// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace LightweightEvents
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;

    using Opc.UaFx;
    using Opc.UaFx.Server;

    /// <summary>
    /// This use case demonstrates in which way a server can "re-use" a single event node to
    /// represent events from different event sources without to define for each source
    /// another event node.
    /// </summary>
    [SuppressMessage("Style", "IDE1006:Naming Styles")]
    public class Program
    {
        #region ---------- Private static methods ----------

        private static CancellationTokenSource machineControl;
        private static Thread machineThread;

        private static object syncRoot = new object();

        private static OpcDataVariableNode<double> temperatureNode;
        private static OpcDataVariableNode<int> speedNode;

        #endregion

        #region ---------- Public static methods ----------

        public static void Main(string[] args)
        {
            temperatureNode = new OpcDataVariableNode<double>("Temperature");
            speedNode = new OpcDataVariableNode<int>("Speed");

            var machineNode = new OpcObjectNode(
                    "Machine",
                    temperatureNode,
                    speedNode,
                    new OpcActionMethodNode("StartMachine", StartMachine),
                    new OpcActionMethodNode("StopMachine", StopMachine));

            using (var server = new OpcServer("opc.tcp://localhost:4840/", machineNode)) {
                server.Start();

                machineNode.AddNotifier(server.SystemContext, temperatureNode);
                machineNode.AddNotifier(server.SystemContext, speedNode);

                Console.WriteLine("Server started - press any key to exit.");
                Console.ReadKey(true);
            }
        }

        #endregion

        #region ---------- Private static methods ----------

        private static void RunMachine(object state)
        {
            var context = (OpcContext)state;

            var eventNode = new OpcEventNode("State");
            var eventSourceNode = default(OpcNode);

            while (!machineControl.IsCancellationRequested) {
                var time = DateTime.Now.TimeOfDay;

                temperatureNode.Value = time.Seconds + time.Milliseconds * 0.1;
                temperatureNode.ApplyChanges(context);

                speedNode.Value = time.Seconds * 1000;
                speedNode.ApplyChanges(context);

                if (time.Seconds % 11 == 0) {
                    eventSourceNode = speedNode;
                    eventNode.Severity = OpcEventSeverity.Medium;
                }
                else {
                    eventSourceNode = temperatureNode;

                    if (temperatureNode.Value <= 10)
                        eventNode.Severity = OpcEventSeverity.Low;
                    else if (temperatureNode.Value <= 20)
                        eventNode.Severity = OpcEventSeverity.MediumLow;
                    else if (temperatureNode.Value <= 30)
                        eventNode.Severity = OpcEventSeverity.Medium;
                    else if (temperatureNode.Value <= 40)
                        eventNode.Severity = OpcEventSeverity.MediumHigh;
                    else if (temperatureNode.Value <= 50)
                        eventNode.Severity = OpcEventSeverity.High;
                    else
                        eventNode.Severity = OpcEventSeverity.Max;
                }

                eventNode.SourceName = eventSourceNode.SymbolicName;
                eventNode.SourceNodeId = eventSourceNode.Id;

                var eventData = eventNode.CreateEvent(context);
                eventSourceNode.ReportEvent(context, eventData);

                // Wait for "StopMachine" or perform next steps after interval elapsed
                if (machineControl.Token.WaitHandle.WaitOne(2500))
                    break;
            }
        }

        private static void StartMachine(OpcMethodContext context)
        {
            lock (syncRoot) {
                if (machineThread == null) {
                    machineControl = new CancellationTokenSource();

                    machineThread = new Thread(RunMachine);
                    machineThread.Start(context);
                }
            }
        }

        private static void StopMachine(OpcMethodContext context)
        {
            lock (syncRoot) {
                if (machineThread != null) {
                    machineControl.Cancel();
                    machineThread.Join();

                    machineControl.Dispose();
                    machineControl = null;

                    machineThread = null;
                }
            }
        }

        #endregion
    }
}
