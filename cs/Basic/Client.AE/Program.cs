// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace AE
{
    using System;
    using System.Threading;

    using Opc.UaFx;
    using Opc.UaFx.Client;

    /// <summary>
    /// This sample demonstrates how to perform handling of Alarm + Events (AE) on the nodes
    /// provided by the OPC UA server.
    /// </summary>
    public class Program
    {
        #region ---------- Private static fields ----------

        /// <summary>
        /// Stores the namespace information used by the client to access the server.
        /// </summary>
        private static readonly OpcNamespace Namespace
                = OpcNamespace.Get(2, "http://sampleserver/machines");

        /// <summary>
        /// Stores the node identifier of the root node of the machine node the client operate on.
        /// </summary>
        private static readonly OpcNodeId machineId = Namespace.GetId("Machine_1");

        /// <summary>
        /// Stores the semaphore used to determine a ctrl+c input to exit the application.
        /// </summary>
        private static readonly SemaphoreSlim cancelSemaphore = new SemaphoreSlim(0);

        /// <summary>
        /// Stores a sequence of the simple storage objects used to simplify the sample code.
        /// </summary>
        private static readonly ObservedNodeCollection nodes = new ObservedNodeCollection();

        #endregion

        #region ---------- Public static methods ----------

        public static void Main(string[] args)
        {
            //// If the server domain name does not match localhost just replace it
            //// e.g. with the IP address or name of the server machine.

            var client = new OpcClient("opc.tcp://localhost:4840/SampleServer");
            client.Connect();

            var positionNode = nodes.Add(client, machineId, "Position");
            var temperatureNode = nodes.Add(client, machineId, "Temperature");
            var statusNode = nodes.Add(client, machineId, "Status");

            var severity = new OpcSimpleAttributeOperand(OpcEventTypes.Event, "Severity");
            var sourceName = new OpcSimpleAttributeOperand(OpcEventTypes.Event, "SourceName");

            var filter = OpcFilter.Using(client)
                // Construct the filter to use for event subscriptions...
                .FromEvents(
                    // ... define the types of events to include in the filter
                    // this will automatically add all properties defined by
                    // the types of events specified ...
                    OpcEventTypes.AlarmCondition,
                    OpcEventTypes.ExclusiveLimitAlarm,
                    OpcEventTypes.DialogCondition)
                .Where(
                    // ... restrict the event information received from the server
                    // by specifying the types of events and ...
                    OpcFilterOperand.OfType(OpcEventTypes.AlarmCondition)
                    | OpcFilterOperand.OfType(OpcEventTypes.ExclusiveLimitAlarm)
                    | OpcFilterOperand.OfType(OpcEventTypes.DialogCondition))
                // ... maybe additional conditions to fulfill, like the required severity
                // using the severity operand defined above like follows:
                //// severity > OpcEventSeverity.Medium
                // ... these conditions can then enhanced logical operators like follows:
                //// & sourceName.Like("Limit")
                .Select(); // ... finally using Select() will query all the necessary event
                           // information required and will create the event filter setup with the
                           // givent event types, constraints and additional selection fields.

            // All in one Subscription
            {
                // Subscribe to data changes (value changes committed using ApplyChanges(...)
                // calls) on the status, position and temperature node. Also subscribe to all
                // events reported through notifiers assigned to the machine node. Each of
                // these subscribe tasks represent a single monitored item instance which is used
                // to define the reporting characteristics to use. The so defined monitored items
                // are then maintained by one single subscription.
                var subscription = client.SubscribeNodes(
                        new OpcSubscribeDataChange(statusNode.Id, HandleDataChanges),
                        new OpcSubscribeDataChange(positionNode.Id, HandleDataChanges),
                        new OpcSubscribeDataChange(temperatureNode.Id, HandleDataChanges),
                        new OpcSubscribeEvent(machineId, filter, HandleDataEvents));

                // In case there the client is interested in the current event information (which
                // is not explicitly re-published through the server when a subscription is
                // created) the client have to query a "ConditionRefresh" to query the latest
                // event information known by the server. In general the server does not need to
                // hold something like a history of events.
                subscription.RefreshConditions();
            }

            // Everyone in its own Subscription
            ////{
            ////    var eventsSubscription = client.SubscribeEvent(machineId, filter, HandleDataEvents);
            ////    eventsSubscription.RefreshConditions();

            ////    // In case there only following events need to be known, the "ConditionRefresh" can
            ////    // be omitted and the subscription variable can be removed:
            ////    //// client.SubscribeEvent(machineId, filter, HandleDataEvents);

            ////    // The following created subscription do not need to trigger a "ConditionRefresh",
            ////    // because in general only event information may be important and the current
            ////    // value can be just read, too.
            ////    client.SubscribeDataChange(statusNode.Id, HandleDataChanges);
            ////    client.SubscribeDataChange(positionNode.Id, HandleDataChanges);
            ////    client.SubscribeDataChange(positionNode.Id, HandleDataChanges);
            ////    client.SubscribeDataChange(temperatureNode.Id, HandleDataChanges);
            ////}

            // Handle global (server-wide) events
            {
                var conditionName = new OpcSimpleAttributeOperand(OpcEventTypes.Condition, "ConditionName");

                var globalFilter = OpcFilter.Using(client)
                        .FromEvents(OpcEventTypes.AlarmCondition)
                        .Where(severity > OpcEventSeverity.Medium & conditionName.Like("Temperature"))
                        .Select();

                client.SubscribeEvent(OpcObjectTypes.Server, globalFilter, HandleGlobalEvents);
            }

            Console.CancelKeyPress += (sender, e) => cancelSemaphore.Release();

            do {
                UpdateConsole(client);
            }
            while (!cancelSemaphore.Wait(1000));

            client.Disconnect();
        }

        #endregion

        #region ---------- Private static methods ----------

        /// <summary>
        /// Handles the <see cref="OpcMonitoredItem.DataChangeReceived"/> event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private static void HandleDataChanges(object sender, OpcDataChangeReceivedEventArgs e)
        {
            // In general change handling can be implemented here, too. The approach of this sample
            // takes use of the nodes collection to summarize the received information to process
            // it all in one run to simplify the handling of the console out- and input.
            var nodeId = ((OpcMonitoredItem)sender).NodeId.ToString(OpcNodeIdFormat.Foundation);

            if (nodes.Contains(nodeId))
                nodes[nodeId].Value = e.Item.Value;
        }

        /// <summary>
        /// Handles the <see cref="OpcMonitoredItem.EventReceived"/> event using node events.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private static void HandleDataEvents(object sender, OpcEventReceivedEventArgs e)
        {
            // In general event handling can be implemented here, too. The approach of this sample
            // takes use of the nodes collection to summarize the received information to process
            // it all in one run to simplify the handling of the console out- and input.
            var nodeId = e.Event.SourceNodeId?.ToString(OpcNodeIdFormat.Foundation);

            if (nodeId != null && nodes.Contains(nodeId))
                nodes[nodeId].Event = e.Event;
        }

        /// <summary>
        /// Handles the <see cref="OpcMonitoredItem.EventReceived"/> event using global events.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private static void HandleGlobalEvents(object sender, OpcEventReceivedEventArgs e)
        {
            Console.Title = e.Event.Message;
        }

        /// <summary>
        /// Determines the according <see cref="ConsoleColor"/> to use for the
        /// <paramref name="severity"/> specified.
        /// </summary>
        /// <param name="severity">The numeric expression of the severity for that the according
        /// color is to be determined.</param>
        /// <returns>The <see cref="ConsoleColor"/> to use to represent the output of the
        /// <paramref name="severity"/> specified.</returns>
        private static ConsoleColor SeverityToColor(OpcEventSeverity severity)
        {
            if (severity <= OpcEventSeverity.Min)
                return ConsoleColor.White;

            if (severity <= OpcEventSeverity.Low)
                return ConsoleColor.Green;

            if (severity <= OpcEventSeverity.Medium)
                return ConsoleColor.Yellow;

            if (severity <= OpcEventSeverity.MediumHigh)
                return ConsoleColor.DarkYellow;

            if (severity <= OpcEventSeverity.High)
                return ConsoleColor.Red;

            return ConsoleColor.Magenta;
        }

        /// <summary>
        /// Updates the whole console output by first clearing it all out and determining the
        /// necessary information to write and read.
        /// </summary>
        /// <param name="client">The <see cref="OpcClient"/> to use in cases there are user inputs
        /// required for which the client is used to communicate with the server.</param>
        private static void UpdateConsole(OpcClient client)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Monitoring Alarm & Events...");

            foreach (var node in nodes) {
                Console.Write(node.Label);
                WriteValue(node);

                Console.Write("\t");
                WriteEvent(node);

                Console.WriteLine();
            }

            foreach (var node in nodes) {
                var ackCondition = node.Event as OpcAcknowledgeableCondition;

                if (ackCondition != null)
                    QueryAcknowledgment(client, ackCondition);

                var dialogCondition = node.Event as OpcDialogCondition;

                if (dialogCondition != null)
                    QueryDialog(client, dialogCondition);
            }
        }

        /// <summary>
        /// Queries an acknowledgment by the user of the <paramref name="client"/>.
        /// </summary>
        /// <param name="client">The <see cref="OpcClient"/> used to acknowledge the
        /// <paramref name="condition"/> using the user input as the comment.</param>
        /// <param name="condition">The <see cref="OpcAcknowledgeableCondition"/> identifying the
        /// event information for which the acknowledgement is to be performed.</param>
        private static void QueryAcknowledgment(
                OpcClient client,
                OpcAcknowledgeableCondition condition)
        {
            if (condition.IsAcked)
                return;

            Console.WriteLine();
            Console.WriteLine($"Acknowledgment is required for condtion: {condition.ConditionName}");
            Console.WriteLine($"  -> {condition.Message}");
            Console.Write("Enter your acknowlegment comment and press Enter to acknowledge: ");

            var comment = Console.ReadLine();
            condition.Acknowledge(client, comment);
        }

        /// <summary>
        /// Queries a dialog to the user of the <paramref name="client"/>.
        /// </summary>
        /// <param name="client">The <see cref="OpcClient"/> used to forward the respond on
        /// the dialog using the user input as the selected response.</param>
        /// <param name="condition">The <see cref="OpcDialogCondition"/> identifying the event
        /// information required to handle the dialog.</param>
        private static void QueryDialog(
                OpcClient client,
                OpcDialogCondition condition)
        {
            if (!condition.IsActive)
                return;

            Console.WriteLine();
            Console.WriteLine(condition.Prompt);

            Console.WriteLine("    Options:");

            var responseOptions = condition.ResponseOptions;

            for (int index = 0; index < responseOptions.Length; index++) {
                Console.Write($"      [{index}] = {responseOptions[index].Value}");

                if (index == condition.DefaultResponse)
                    Console.Write(" (default)");

                Console.WriteLine();
            }

            var respond = string.Empty;
            var respondOption = condition.DefaultResponse;

            do {
                Console.Write("Enter the number of the option and press Enter to respond: ");
                respond = Console.ReadLine();

                if (string.IsNullOrEmpty(respond))
                    break;
            } while (!int.TryParse(respond, out respondOption));

            condition.Respond(client, respondOption);
        }

        /// <summary>
        /// Writes the <see cref="ObservedNode.Event"/> information of the <paramref name="node"/>
        /// specified to the console.
        /// </summary>
        /// <param name="node">The <see cref="ObservedNode"/> its event information is to
        /// be written.</param>
        private static void WriteEvent(ObservedNode node)
        {
            var eventData = node.Event;

            if (eventData != null) {
                var conditionData = node.Event as OpcCondition;

                if (conditionData == null || conditionData.IsRetained)
                    Console.ForegroundColor = SeverityToColor(eventData.Severity);
                else
                    Console.ForegroundColor = ConsoleColor.DarkGray;

                var eventTime = eventData.Time.ToLocalTime();
                var eventText = eventData.Message;

                Console.Write($"{eventTime.ToString("HH:mm:ss.fff")} - {eventText}");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Writes the <see cref="ObservedNode.Value"/> and <see cref="ObservedNode.Unit"/>
        /// information of the <paramref name="node"/> specified to the console.
        /// </summary>
        /// <param name="node">The <see cref="ObservedNode"/> its value and unit information is to
        /// written.</param>
        private static void WriteValue(ObservedNode node)
        {
            if (node.Value != null) {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(node.Value);

                if (node.Unit == null)
                    Console.Write("\t");
                else
                    Console.Write($" {node.Unit.DisplayName}\t");

                Console.ResetColor();
            }
        }

        #endregion
    }
}
