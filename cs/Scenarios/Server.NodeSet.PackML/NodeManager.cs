// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace NodeSet
{
    using System.Collections.Generic;

    using Opc.UaFx;
    using Opc.UaFx.Server;

    internal class NodeManager : OpcNodeManager
    {
        #region ---------- Private fields ----------

        private int objectIndex;
        private OpcObjectNode objectsNode;

        #endregion

        #region ---------- Public constructors ----------

        public NodeManager()
            : base(Program.Namespace, PackML.Namespace)
        {
        }

        #endregion

        #region ---------- Public methods ----------

        public void AddObject()
        {
            var name = $"Machine{++this.objectIndex}";
            var id = this.DefaultNamespace.GetId(name);

            this.AddNode(new PackMLBaseObjectNode(this.objectsNode, name, id));
        }

        #endregion

        #region ---------- Protected methods ----------

        protected override IEnumerable<OpcNodeSet> ImportNodes()
        {
            yield return OpcNodeSet.Load(@".\Opc.Ua.PackML.NodeSet2.xml");
        }

        protected override void ImplementNode(IOpcNode node)
        {
            if (node is OpcObjectNode objectNode && node.Name.Value == "PackMLObjects")
                this.objectsNode = objectNode;
        }

        #endregion
    }
}
