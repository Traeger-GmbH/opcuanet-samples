// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace NodeSet
{
    using Opc.UaFx;

    internal class MachineToolAlarmConditionNode : OpcAlarmConditionNode
    {
        #region ---------- Private readonly fields ----------

        private readonly OpcDataVariableNode<string> alarmIdentifierNode;
        private readonly OpcDataVariableNode<string> auxParametersNode;

        #endregion

        #region ---------- Public constructors ----------

        public MachineToolAlarmConditionNode()
            : base("MachineToolAlarmCondition")
        {
            this.alarmIdentifierNode = new OpcDataVariableNode<string>(this, "2:AlarmIdentifier");
            this.auxParametersNode = new OpcDataVariableNode<string>(this, "2:AuxParameters");

            this.EventTypeId = "ns=2;i=1042";
        }

        #endregion

        #region ---------- Public properties ----------

        public string AlarmIdentifier
        {
            get => this.alarmIdentifierNode.Value;
            set => this.alarmIdentifierNode.Value = value;
        }

        public string AuxParameters
        {
            get => this.auxParametersNode.Value;
            set => this.auxParametersNode.Value = value;
        }

        #endregion

        #region ---------- Protected properties ----------

        protected override OpcNodeId DefaultTypeDefinitionId
        {
            get => "ns=2;i=1042";
        }

        #endregion
    }
}
