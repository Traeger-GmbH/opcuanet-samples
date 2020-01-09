// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace NodeAccess
{
    using System.Collections.Generic;

    using Opc.UaFx;
    using Opc.UaFx.Server;

    internal partial class NodeManager : OpcNodeManager
    {
        #region ---------- Public constructors ----------

        public NodeManager()
            : base("http://sampleserver/nodeaccess")
        {
        }

        #endregion

        #region ---------- Public properties ----------

        public OpcAccessControlList AccessControl
        {
            get;
            set;
        }

        #endregion

        #region ---------- Internal methods ----------

        internal bool CanWrite(OpcVariableNode node, OpcUserIdentity sessionIdentity)
        {
            var entries = this.AccessControl.Entries;

            foreach (var entry in entries) {
                if (entry.Principal.Identity is SystemIdentity serverIdentity) {
                    if (serverIdentity.DisplayName == sessionIdentity.DisplayName) {
                        return entry.IsAllowed(OpcRequestType.Write)
                                && serverIdentity.IsAllowed(node.Id.ValueAsString);
                    }
                }
            }

            return false;
        }

        #endregion

        #region ---------- Protected methods ----------

        protected override IEnumerable<IOpcNode> CreateNodes(OpcNodeReferenceCollection references)
        {
            var machineNode = new OpcObjectNode(this.DefaultNamespace.GetName("Machine"));

            new VariableNode<int>(this, machineNode, "Status", value: 42);
            new VariableNode<string>(this, machineNode, "Job", value: "JOB-0543");

            new VariableNode<bool>(this, machineNode, "Shutdown", value: false);
            new VariableNode<short>(this, machineNode, "Speed", value: 1000);
            new VariableNode<string>(this, machineNode, "Tooling", value: "cutter");

            references.Add(machineNode, OpcObjectTypes.ObjectsFolder);
            return new IOpcNode[] { machineNode };
        }

        #endregion
    }
}
