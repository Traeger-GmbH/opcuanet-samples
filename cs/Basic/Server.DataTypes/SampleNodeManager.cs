// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace DataTypes
{
    using System;
    using System.Collections.Generic;

    using Opc.UaFx;
    using Opc.UaFx.Server;

    /// <summary>
    /// Represents a sample implementation of a custom OpcNodeManager.
    /// </summary>
    internal class SampleNodeManager : OpcNodeManager
    {
        #region ---------- Public constructors ----------

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleNodeManager"/> class.
        /// </summary>
        public SampleNodeManager()
            : base("http://sampleserver/machines")
        {
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
            yield return new OpcDataTypeNode<MachineStatus>();
            yield return new OpcDataTypeNode<MachineSetup>();

            yield return new OpcDataTypeNode<MachineJob>();
            yield return new OpcDataTypeNode<ManufacturingOrder>();

            var machines = new OpcFolderNode("Machines");

            // Add new reference to make the node visible beneath the ObjectsFolder
            // (the top most root node within every OPC UA server).
            references.Add(machines, OpcObjectTypes.ObjectsFolder);

            var machineOne = new OpcFolderNode(machines, "Machine_1");
            new OpcDataVariableNode<string>(machineOne, "Name", "Machine 1");
            new OpcDataVariableNode<MachineStatus>(machineOne, "Status", MachineStatus.Stopped);
            new OpcDataVariableNode<bool>(machineOne, "IsActive", false);
            new OpcDataVariableNode<double>(machineOne, "Temperature", 18.3);
            new OpcMethodNode(machineOne, "StartJob", new Action<MachineJob>(job => Console.WriteLine(job.Number)));
            new OpcDataVariableNode<MachineJob>(machineOne, "Job", new MachineJob() {
                Number = "JOB001",
                EstimatedDuration = 12500,
                InProcess = false,
                CuttingPositions = new int[] { 1000, 1500, 1570, 2020 },
                RequiredSetup = MachineSetup.Packager,
                ScheduleTime = DateTime.UtcNow.AddMinutes(10)
            });

            var machineTwo = new OpcFolderNode(machines, "Machine_2");
            new OpcDataVariableNode<string>(machineTwo, "Name", "Machine 2");
            new OpcDataVariableNode<MachineStatus>(machineTwo, "Status", MachineStatus.Suspended);
            new OpcDataVariableNode<bool>(machineTwo, "IsActive", true);
            new OpcDataVariableNode<double>(machineTwo, "Temperature", 20.7);
            new OpcMethodNode(machineOne, "StartJob", new Action<MachineJob>(job => Console.WriteLine(job.Number)));
            new OpcDataVariableNode<ManufacturingOrder>(machineTwo, "Order", new ManufacturingOrder() {
                Order = "2020.10.10001",
                Article = "ART10025",
                Jobs = new [] {
                    new MachineJob {
                        Number = "JOB1001",
                        Duration = 900,
                        EstimatedDuration = 1000,
                        InProcess = false,
                        CuttingPositions = new int[4],
                        RequiredSetup = MachineSetup.Corrugator,
                        ScheduleTime = DateTime.UtcNow
                    },
                    new MachineJob {
                        Number = "JOB1002",
                        Duration = 510,
                        EstimatedDuration = 500,
                        InProcess = true,
                        CuttingPositions = new int[] { 100, 1300, 1700, 2520 },
                        RequiredSetup = MachineSetup.Cutter,
                        ScheduleTime = DateTime.UtcNow.AddSeconds(1)
                    },
                    new MachineJob {
                        Number = "JOB1003",
                        Duration = 1030,
                        EstimatedDuration = 2200,
                        InProcess = true,
                        CuttingPositions = new int[4],
                        RequiredSetup = MachineSetup.Printer1,
                        ScheduleTime = DateTime.UtcNow.AddSeconds(2)
                    }
                }
            });

            yield return machines;
        }

        #endregion
    }
}
