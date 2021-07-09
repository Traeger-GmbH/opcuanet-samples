// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace EventTypes
{
    using System;
    using System.Collections.Generic;

    using Opc.UaFx;
    using Opc.UaFx.Server;

    internal partial class NodeManager : OpcNodeManager
    {
        #region ---------- Public constructors ----------

        public NodeManager()
            : base("http://sampleserver/sampleeventtypes")
        {
        }

        #endregion

        #region ---------- Protected methods ----------

        protected override IEnumerable<OpcNodeSet> ImportNodes()
        {
            yield return OpcNodeSet.Load(@".\MyNodeSet.xml");
        }

        protected override IEnumerable<IOpcNode> CreateNodes(OpcNodeReferenceCollection references)
        {
            yield return new OpcDataTypeNode<MachineSetup>();
            yield return new OpcDataTypeNode<MachineJob>();

            var machines = new OpcFolderNode("Machines");

            // Add new reference to make the node visible beneath the ObjectsFolder
            // (the top most root node within every OPC UA server).
            references.Add(machines, OpcObjectTypes.ObjectsFolder);

            var machineOne = new OpcFolderNode(machines, "Machine_1");
            new OpcDataVariableNode<string>(machineOne, "Name", "Machine 1");
            new OpcDataVariableNode<double>(machineOne, "Temperature", 18.3);

            var machineOneJobChanged = new MachineJobEventNode(machineOne, "JobChanged");
            machineOne.AddNotifier(this.SystemContext, machineOneJobChanged);

            new OpcMethodNode(machineOne, "StartJob", new Action<MachineJob>(job => {
                Console.WriteLine("Started job '{0}' with setup '{1}'.", job.Number, job.RequiredSetup);

                machineOneJobChanged.Job.Value = job;
                machineOneJobChanged.ReportEventFrom(this.SystemContext, machineOne);
            }));

            var machineTwo = new OpcFolderNode(machines, "Machine_2");
            new OpcDataVariableNode<string>(machineTwo, "Name", "Machine 2");
            new OpcDataVariableNode<double>(machineTwo, "Temperature", 20.7);

            var machineTwoJobChanged = new MachineJobEventNode(machineTwo, "JobChanged");
            machineTwo.AddNotifier(this.SystemContext, machineTwoJobChanged);

            new OpcMethodNode(machineTwo, "StartJob", new Action<MachineJob>(job => {
                Console.WriteLine("Started job '{0}' with setup '{1}'.", job.Number, job.RequiredSetup);

                machineTwoJobChanged.Job.Value = job;
                machineTwoJobChanged.ReportEventFrom(this.SystemContext, machineTwo);
            }));

            yield return machines;
        }

        #endregion
    }
}
