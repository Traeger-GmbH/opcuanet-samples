// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace NodeTypes
{
    using Opc.UaFx;

    internal class MySimpleVariableNode : OpcDataVariableNode<int>
    {
        #region ---------- Public constructors ----------

        public MySimpleVariableNode(OpcName name, int value)
            : base(name, value)
        {
        }

        public MySimpleVariableNode(IOpcNode parent, OpcName name, int value)
            : base(parent, name, value)
        {
        }

        #endregion

        #region ---------- Protected properties ----------

        protected override OpcNodeId DefaultTypeDefinitionId
        {
            get => "ns=1;s=MySimpleVariableType";
        }

        #endregion
    }
}
