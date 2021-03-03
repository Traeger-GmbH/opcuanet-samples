// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace NodeTypes
{
    using System;
    using Opc.UaFx;

    internal class MyComplexVariableNode : OpcDataVariableNode<int>
    {
        #region ---------- Private fields ----------

        private OpcDataVariableNode<string> mandatoryValueNode;
        private OpcDataVariableNode<int> optionalValueNode;

        #endregion

        #region ---------- Public constructors ----------

        public MyComplexVariableNode(
                OpcName name,
                string mandatoryValue,
                int optionalValue = -1)
            : base(name)
        {
            this.InitializeChildren(mandatoryValue, optionalValue);
        }

        public MyComplexVariableNode(
                IOpcNode parent,
                OpcName name,
                string mandatoryValue,
                int optionalValue = -1)
            : base(parent, name)
        {
            this.InitializeChildren(mandatoryValue, optionalValue);
        }

        #endregion

        #region ---------- Public properties ----------

        public string MandatoryValue
        {
            get => this.mandatoryValueNode.Value;
            set => this.mandatoryValueNode.Value = value;
        }

        public bool HasOptionalValue
        {
            get => this.optionalValueNode != null;
        }

        public int OptionalValue
        {
            get
            {
                if (this.optionalValueNode == null)
                    throw new NotSupportedException();

                return this.optionalValueNode.Value;
            }

            set
            {
                if (this.optionalValueNode == null)
                    throw new NotSupportedException();

                this.optionalValueNode.Value = value;
            }
        }

        #endregion

        #region ---------- Protected properties ----------

        protected override OpcNodeId DefaultTypeDefinitionId
        {
            get => "ns=1;s=MyComplexVariableType";
        }

        #endregion

        #region ---------- Private methods ----------

        private void InitializeChildren(string mandatoryValue, int optionalValue)
        {
            this.mandatoryValueNode = new OpcDataVariableNode<string>(
                    parent: this,
                    name: "1:MandatoryValue",
                    value: mandatoryValue);

            if (optionalValue != -1) {
                this.optionalValueNode = new OpcDataVariableNode<int>(
                        parent: this,
                        name: "1:OptionalValue",
                        value: optionalValue);
            }
        }

        #endregion
    }
}
