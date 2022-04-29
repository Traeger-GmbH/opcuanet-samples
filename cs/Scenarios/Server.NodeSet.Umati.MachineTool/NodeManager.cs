// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace NodeSet.Umati
{
    using System.Collections.Generic;

    using Opc.UaFx;
    using Opc.UaFx.Server;

    internal class NodeManager : OpcNodeManager
    {
        #region ---------- Public constructors ----------

        public NodeManager()
            : base(MachineToolExample.Namespace)
        {
        }

        #endregion

        #region ---------- Public methods ----------

        public MachineToolNode AddMachineTool(string name)
        {
            var node = new MachineToolNode(name);
            this.AddNode(node);

            return node;
        }

        #endregion

        #region ---------- Protected methods ----------

        protected override IEnumerable<OpcNodeSet> ImportNodes()
        {
            yield return OpcNodeSet.Load(@".\NodeSets\MachineTool-Example.xml");
        }

        #endregion
    }
}
