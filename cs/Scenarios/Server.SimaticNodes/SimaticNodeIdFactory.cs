// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace SimaticNodes
{
    using Opc.UaFx;

    public class SimaticNodeIdFactory : OpcNominalNodeIdFactory
    {
        #region ---------- Public constructors ----------

        public SimaticNodeIdFactory()
            : base()
        {
            this.Separator = '.';
        }

        #endregion

        #region ---------- Protected methods ----------

        protected override OpcNodeId Create(
                OpcContext context,
                OpcNamespace nodeNamespace,
                IOpcNodeInfo node,
                OpcName nodeName)
        {
            if (!OpcName.IsNullOrEmpty(nodeName))
                nodeName = nodeName.Namespace.GetName("\"" + nodeName.Value + "\"");

            return base.Create(context, nodeNamespace, node, nodeName);
        }

        #endregion
    }
}
