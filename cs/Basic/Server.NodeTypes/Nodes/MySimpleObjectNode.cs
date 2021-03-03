// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace NodeTypes
{
    using Opc.UaFx;

    internal class MySimpleObjectNode : OpcObjectNode
    {
        #region ---------- Public constructors ----------

        public MySimpleObjectNode(OpcName name)
            : base(name)
        {
        }

        public MySimpleObjectNode(IOpcNode parent, OpcName name)
            : base(parent, name)
        {
        }

        #endregion

        #region ---------- Protected properties ----------

        protected override OpcNodeId DefaultTypeDefinitionId
        {
            get => "ns=1;s=MySimpleObjectType";
        }

        #endregion
    }
}
