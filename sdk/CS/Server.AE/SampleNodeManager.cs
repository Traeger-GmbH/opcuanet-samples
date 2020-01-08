// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace AE
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    using Opc.UaFx;
    using Opc.UaFx.Server;

    /// <summary>
    /// Represents a sample implementation of a custom OpcNodeManager.
    /// </summary>
    internal class SampleNodeManager : OpcNodeManager
    {
        #region ---------- Private fields ----------

        /// <summary>
        /// Stores a variable node which is used to control the "job" processing.
        /// </summary>
        private OpcDataVariableNode<bool> isActiveNode;

        /// <summary>
        /// Stores a variable node which is used to indicate the progress status of the current
        /// "job" processing including pre- and post-setup stages.
        /// </summary>
        private OpcDataVariableNode<byte> statusNode;

        /// <summary>
        /// Stores a dialog condition node (an alarm with interaction) used to query clients
        /// whether to continue with the "next job".
        /// </summary>
        private OpcDialogConditionNode statusChangeNode;

        /// <summary>
        /// Stores a analog item node used to store the volatile position of a "mechanical" part of
        /// the "machine" represented by the server.
        /// </summary>
        private OpcAnalogItemNode<int> positionNode;

        /// <summary>
        /// Stores a limit alarm node (an alarm with lower and upper bounds) used to notify about
        /// the reaching of a limit through the position of a "mechanical" part of the "machine".
        /// </summary>
        private OpcExclusiveLimitAlarmNode positionLimitNode;

        /// <summary>
        /// Stores a variable node which is used to represent a fictive temperature measured at the
        /// "mechanical" part of the "machine".
        /// </summary>
        private OpcAnalogItemNode<double> temperatureNode;

        /// <summary>
        /// Stores a simple alarm node to notify about the reaching of technical supported
        /// temperature values without to inform about the defined limits using the alarm.
        /// </summary>
        private OpcAlarmConditionNode temperatureCriticalNode;

        #endregion

        #region ---------- Public constructors ----------

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleNodeManager"/> class.
        /// </summary>
        public SampleNodeManager()
            : base("http://sampleserver/machines")
        {
        }

        #endregion

        #region ---------- Public methods ----------

        /// <summary>
        /// Simulates a continuous running progress which can be ended using the
        /// <paramref name="semaphore"/> specified.
        /// </summary>
        /// <param name="semaphore">The <see cref="SemaphoreSlim"/> which is used
        /// to determine whether the simulation is to be canceled. If it is released
        /// the simulation is cancelled.</param>
        public void Simulate(SemaphoreSlim semaphore)
        {
            // By default we define each condition as acknowledged, because we will change it 
            // depending on outcome of the evaluations bound to the alarms.
            this.positionLimitNode.ChangeIsAcked(this.SystemContext, true);
            this.temperatureCriticalNode.ChangeIsAcked(this.SystemContext, true);

            var run = 0;
            var random = new Random(45);

            while (!semaphore.Wait(1000)) {
                // Only perform "job"-simulation in case the "machine" is active.
                if (!this.isActiveNode.Value)
                    continue;

                this.SimulatePosition(run, random);
                this.SimulateTemperature(run, random);
                this.SimulateStatus(run, random);

                run = unchecked(run + 1);
            }
        }

        #endregion

        #region ---------- Protected methods ----------

        /// <summary>
        /// Creates the nodes provided and associated with the node manager.
        /// </summary>
        /// <param name="references">A dictionary used to determine the logical references between
        /// existing nodes (e.g. OPC default nodes) and the nodes provided by the node
        /// manager.</param>
        /// <returns>An enumerable containing the root nodes of the node manager.</returns>
        /// <remarks>This method will be only called once by the server on start up.</remarks>
        protected override IEnumerable<IOpcNode> CreateNodes(OpcNodeReferenceCollection references)
        {
            // It is necessary to assign to all root nodes one of the namespaces used to
            // identify one of the associated namespaces (see the ctor of the class). This
            // namespace does identify the node as member of the namespace of the node
            // manager. Optionally it is possible to assign namespace to the child nodes
            // too. But by default their missing namespace will be auto-completed through the
            // namespace of their parent node.
            var machineOne = new OpcFolderNode(this.DefaultNamespace.GetName("Machine_1"));

            // In case a client requests a condition referesh it queries the current event
            // information which is gathered using the CreateEvent method from each active
            // and retained alarm nodes.
            machineOne.QueryEventsCallback = (context, events) => {
                // Ensure that an re-entrance upon notifier cross-references will not add
                // events to the collection which are already stored in.
                if (events.Count != 0)
                    return;

                if (this.statusChangeNode.IsRetained)
                    events.Add(this.statusChangeNode.CreateEvent(context));

                if (this.positionLimitNode.IsRetained)
                    events.Add(this.positionLimitNode.CreateEvent(context));

                if (this.temperatureCriticalNode.IsRetained)
                    events.Add(this.temperatureCriticalNode.CreateEvent(context));
            };

            // Add new reference to make the node visible beneath the ObjectsFolder
            // (the top most root node within every OPC UA server).
            references.Add(machineOne, OpcObjectTypes.ObjectsFolder);

            new OpcDataVariableNode<string>(machineOne, "Name", "Machine 1");
            this.isActiveNode = new OpcDataVariableNode<bool>(machineOne, "IsActive", true);

            //// An alarm node have to be a notifier for another node or for the whole server.
            //// Is a alarm a notifier of another node:
            //// -> this node (the notified one) needs to be subscribed by the client to receive
            ////    the alarm data.
            //// Is a alarm a notifier of the whole server:
            //// -> the OpcObjectTypes.Server needs to be subscribed by the client to receive
            ////    the alarm data.

            // Machine 1, Status nodes setup
            {
                this.statusNode = new OpcDataVariableNode<byte>(machineOne, "Status", 1);

                // Define an alarm used to request a dialog which requires a dedicated response
                // action by a client. This kind of node can be used for service / operator tasks.
                this.statusChangeNode = new OpcDialogConditionNode(machineOne, "StatusChange");
                this.statusChangeNode.AutoReportChanges = true;

                this.statusChangeNode.Message = "Operator requested";
                this.statusChangeNode.Prompt = "The job has been finished, continue with the next one?";
                this.statusChangeNode.ResponseOptions = new OpcText[] { "Yes", "No" };
                this.statusChangeNode.DefaultResponse = 0;
                this.statusChangeNode.CancelResponse = 1;
                this.statusChangeNode.OkResponse = 0;

                // Handle any client response on an active dialog through applying the response
                // using RespondDialog and configuring the dialog as inactive.
                this.statusChangeNode.RespondCallback = (context, response) => {
                    this.isActiveNode.Value = (response == this.statusChangeNode.OkResponse);
                    this.isActiveNode.ApplyChanges(context);

                    this.statusChangeNode.RespondDialog(context, response);

                    this.statusChangeNode.Message = "No operator required";
                    this.statusChangeNode.IsRetained = false;

                    return OpcStatusCode.Good;
                };

                // Define the alarm as the notifier of the machineOne node.
                machineOne.AddNotifier(this.SystemContext, this.statusChangeNode);
            }

            // Machine 1, Position nodes setup
            {
                this.positionNode = new OpcAnalogItemNode<int>(machineOne, "Position", -1);
                this.positionNode.InstrumentRange = new OpcValueRange(low: 120, high: 1);
                this.positionNode.EngineeringUnit = new OpcEngineeringUnitInfo(4732211, "mm", "millimetre");
                this.positionNode.EngineeringUnitRange = new OpcValueRange(byte.MaxValue);

                // Define an alarm used to indicate the reaching of one or more limits during
                // a progress. Such limits may be predefined or progress dependent.
                this.positionLimitNode = new OpcExclusiveLimitAlarmNode(
                        machineOne, "PositionLimit", OpcLimitAlarmStates.All);

                this.positionLimitNode.HighHighLimit = 120; // e.g. mm
                this.positionLimitNode.HighLimit = 100;     // e.g. mm
                this.positionLimitNode.LowLimit = 5;        // e.g. mm
                this.positionLimitNode.LowLowLimit = 1;     // e.g. mm

                this.positionLimitNode.Message = "No range problems";
                this.positionLimitNode.ReceiveTime = DateTime.UtcNow;

                this.positionLimitNode.AcknowledgeCallback = (context, eventId, comment) => {
                    this.positionLimitNode.Message = "Acknowledged with " + comment;
                    return OpcStatusCode.Good;
                };

                // Define the alarm as the notifier of the machineOne node.
                machineOne.AddNotifier(this.SystemContext, this.positionLimitNode);
            }

            // Machine 1, Temperature nodes setup
            {
                this.temperatureNode = new OpcAnalogItemNode<double>(machineOne, "Temperature", 18.3);
                this.temperatureNode.InstrumentRange = new OpcValueRange(80.0, -40.0);
                this.temperatureNode.EngineeringUnit = new OpcEngineeringUnitInfo(4408652, "°C", "degree Celsius");
                this.temperatureNode.EngineeringUnitRange = new OpcValueRange(70.8, 5.0);

                // Define an alarm which just indicates the fulfillment of an alarm associated
                // condition. Such simple alarms only notify about the fulfillment without to
                // define additional prerequisites defined by the alarm itself. Much more
                // specialized alarms are subclasses of this type of alarm node.
                this.temperatureCriticalNode = new OpcAlarmConditionNode(machineOne, "TemperatureCritical");

                // Define the alarm as the notifier of the machineOne node.
                machineOne.AddNotifier(this.SystemContext, this.temperatureCriticalNode);

                // Define the alarm as the notifier of the whole Server node.
                this.AddNotifierNode(this.temperatureCriticalNode);
            }

            return new IOpcNode[] { machineOne };
        }

        #endregion

        #region ---------- Private methods ----------

        /// <summary>
        /// Simulates a progress which influences the <see cref="statusNode"/>,
        /// <see cref="isActiveNode"/> and publishes alarms using the
        /// <see cref="statusChangeNode"/>.
        /// </summary>
        /// <param name="run">The sequence number of the simulation run within the status
        /// simulation is to be performed.</param>
        /// <param name="random">The <see cref="Random"/> instance used for random number
        /// generation.</param>
        private void SimulateStatus(int run, Random random)
        {
            this.statusNode.Value = (byte)(run % 20);

            // This will trigger DataChange notification being send to DataChange subscriptions.
            this.statusNode.ApplyChanges(this.SystemContext);

            if (this.statusNode.Value == 45) {
                this.isActiveNode.Value = false;

                // This will trigger DataChange notification being send to DataChange subscriptions.
                this.isActiveNode.ApplyChanges(this.SystemContext);

                this.statusChangeNode.ReceiveTime = DateTime.UtcNow;
                this.statusChangeNode.Time = DateTime.UtcNow;

                this.statusChangeNode.Message = "Operator requested";
                this.statusChangeNode.IsRetained = true;

                this.statusChangeNode.ActivateDialog(this.SystemContext);

                // This will trigger Event notification being send to Event subscriptions.
                this.statusChangeNode.ReportEventFrom(
                        this.SystemContext, this.statusNode);
            }
        }

        /// <summary>
        /// Simulates a progress which influences the <see cref="positionNode"/> and publishes
        /// alarms using the <see cref="positionLimitNode"/>.
        /// </summary>
        /// <param name="run">The sequence number of the simulation run within the position
        /// simulation is to be performed.</param>
        /// <param name="random">The <see cref="Random"/> instance used for random number
        /// generation.</param>
        private void SimulatePosition(int run, Random random)
        {
            if (this.positionNode.Value == -1) {
                this.positionLimitNode.ChangeLimitState(
                        this.SystemContext, OpcLimitAlarmStates.Inactive);
            }

            var ackRequired = (this.positionLimitNode.ReceiveTime.AddSeconds(45) < DateTime.UtcNow);

            if (!this.positionLimitNode.IsActive || (!ackRequired || this.positionLimitNode.IsAcked)) {
                var positionValue = random.Next(
                        (int)(this.positionLimitNode.LowLowLimit - run % 3),
                        (int)(this.positionLimitNode.HighHighLimit + run % 7));

                this.positionNode.Value = positionValue;

                // This will trigger DataChange notification being send to DataChange subscriptions.
                this.positionNode.ApplyChanges(this.SystemContext);

                var severity = OpcEventSeverity.Low;
                var limits = OpcLimitAlarmStates.Inactive;

                var message = "No range problems";

                if (positionValue <= this.positionLimitNode.LowLowLimit) {
                    limits = OpcLimitAlarmStates.LowLow;
                    message = "Out of lower bound range!";
                    severity = OpcEventSeverity.Medium;
                }
                else if (positionValue <= this.positionLimitNode.LowLimit) {
                    limits = OpcLimitAlarmStates.Low;
                    message = "About to reach lower bound!";
                    severity = OpcEventSeverity.MediumHigh;
                }
                else if (positionValue >= this.positionLimitNode.HighLimit) {
                    limits = OpcLimitAlarmStates.High;
                    message = "About to reach upper bound!";
                    severity = OpcEventSeverity.MediumHigh;
                }
                else if (positionValue >= this.positionLimitNode.HighHighLimit) {
                    limits = OpcLimitAlarmStates.HighHigh;
                    message = "Out of upper bound range!";
                    severity = OpcEventSeverity.High;
                }

                this.positionLimitNode.ChangeSeverity(this.SystemContext, severity);
                this.positionLimitNode.ChangeLimitState(this.SystemContext, limits);

                if (this.positionLimitNode.IsActive) {
                    this.positionLimitNode.Time = DateTime.UtcNow;

                    if (ackRequired) {
                        this.positionLimitNode.Message = message + " - Acknowledgement is required!";

                        this.positionLimitNode.ChangeIsAcked(this.SystemContext, false);
                        this.positionLimitNode.ChangeIsConfirmed(this.SystemContext, false);

                        this.positionLimitNode.ReceiveTime = DateTime.UtcNow;
                    }
                }

                // This will trigger Event notification being send to Event subscriptions.
                this.positionLimitNode.ReportEventFrom(
                        this.SystemContext, this.positionNode);
            }
        }

        /// <summary>
        /// Simulates a progress which influences the <see cref="temperatureNode"/> and publishes
        /// alarms using the <see cref="temperatureCriticalNode"/>.
        /// </summary>
        /// <param name="run">The sequence number of the simulation run within the temperature
        /// simulation is to be performed.</param>
        /// <param name="random">The <see cref="Random"/> instance used for random number
        /// generation.</param>
        private void SimulateTemperature(int run, Random random)
        {
            var temperatureValue = random.Next(12, 20 * (((run % 7) / 4) + 1));
            this.temperatureNode.Value = temperatureValue;

            // This will trigger DataChange notification being send to DataChange subscriptions.
            this.temperatureNode.ApplyChanges(this.SystemContext);

            if (temperatureValue <= 20) {
                this.temperatureCriticalNode.ChangeIsActive(this.SystemContext, false);
            }
            else {
                var message = "The temperature is higher than 20°C!";
                var severity = OpcEventSeverity.Low;

                if (temperatureValue <= 25) {
                    severity = OpcEventSeverity.Medium;
                }
                else if (temperatureValue <= 30) {
                    message = "The temperature is near to 30°C!";
                    severity = OpcEventSeverity.MediumHigh;
                }
                else if (temperatureValue <= 35) {
                    severity = OpcEventSeverity.High;
                }
                else {
                    message = "The temperature is near to 40°C!";
                    severity = OpcEventSeverity.Max;
                }

                this.temperatureCriticalNode.Message = message;

                this.temperatureCriticalNode.ReceiveTime = DateTime.UtcNow;
                this.temperatureCriticalNode.Time = DateTime.UtcNow;

                this.temperatureCriticalNode.ChangeSeverity(this.SystemContext, severity);
                this.temperatureCriticalNode.ChangeIsActive(this.SystemContext, true);

                // This will trigger Event notification being send to Event subscriptions.
                this.temperatureCriticalNode.ReportEventFrom(
                        this.SystemContext, this.temperatureNode);
            }
        }

        #endregion
    }
}
