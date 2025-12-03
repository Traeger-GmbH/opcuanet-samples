// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace Companion.Machinery
{
    using System.Collections.Generic;

    using Opc.UaFx;
    using Opc.UaFx.Di;
    using Opc.UaFx.Server;

    internal class NodeManager : OpcNodeManager
    {
        #region ---------- Private fields ----------

        private OpcNodeModel machineModel;

        #endregion

        #region ---------- Public constructors ----------

        public NodeManager()
            : base("urn:example")
        {
        }

        #endregion

        #region ---------- Public methods ----------

        public MyMachineNode AddMachine(string name)
        {
            var node = new MyMachineNode(this.machineModel, this.DefaultNamespace.GetName(name));
            this.AddNode(node);


            

            return node;
        }

        #endregion

        #region ---------- Protected methods ----------

        protected override IEnumerable<OpcNodeSet> ImportNodes()
        {
            var machineType = this.SystemContext
                    .GetNodeType(OpcDiNodes.TopologyElementType);

            this.machineModel = OpcNodeModel.Of(machineType)
                    .OmitOptionals()
                    //.CreateLike(node)
                    .Create(OpcDiNodes.GetName("Identification"));

            yield break;
        }

        #endregion
    }
}
