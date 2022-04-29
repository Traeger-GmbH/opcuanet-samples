// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace NodeSet.Umati
{
    using Opc.UaFx;

    internal class AlertNode : OpcAlarmConditionNode
    {
        #region ---------- Private fields ----------

        private OpcPropertyNode<string> errorCodeNode;

        #endregion

        #region ---------- Public constructors ----------

        public AlertNode(OpcName name)
            : base(name)
        {
            this.InitializeChildren();
        }

        public AlertNode(IOpcNode parent, OpcName name)
            : base(parent, name)
        {
            this.InitializeChildren();
        }

        #endregion

        #region ---------- Public properties ----------

        public OpcPropertyNode<string> ErrorCode
        {
            get => this.errorCodeNode;
        }

        #endregion

        #region ---------- Protected properties ----------

        protected override OpcNodeId DefaultTypeDefinitionId
        {
            get => MachineTool.AlertType;
        }

        #endregion

        #region ---------- Private methods ----------

        private void InitializeChildren()
        {
            this.errorCodeNode = new OpcPropertyNode<string>(
                    parent: this,
                    name: MachineTool.GetName("ErrorCode"),
                    value: null);

            this.AddChild(OpcContext.Empty, this.errorCodeNode);
        }

        #endregion
    }
}
