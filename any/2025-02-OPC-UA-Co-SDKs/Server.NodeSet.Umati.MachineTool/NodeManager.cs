// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace Opc.UaFx.MachineTool
{
    using Opc.UaFx.MachineTool.Example;

    internal class NodeManager : OpcMachineToolExampleNodeManager
    {
        #region ---------- Public constructors ----------

        public NodeManager()
            : base()
        {
        }

        #endregion

        #region ---------- Public methods ----------

        public OpcMachineToolNode AddMachineTool(string name)
        {
            var node = new OpcMachineToolNode(name);
            this.AddNode(node);

            return node;
        }

        #endregion
    }
}
