// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace NodeTypes
{
    using Opc.UaFx;

    internal class MyComplexObjectNode : OpcObjectNode
    {
        #region ---------- Private fields ----------

        private MySimpleVariableNode firstNode;
        private MyComplexVariableNode secondNode;

        #endregion

        #region ---------- Public constructors ----------

        public MyComplexObjectNode(OpcName name)
            : base(name)
        {
            this.InitializeChildren();
        }

        public MyComplexObjectNode(IOpcNode parent, OpcName name)
            : base(parent, name)
        {
            this.InitializeChildren();
        }

        #endregion

        #region ---------- Public properties ----------

        public MySimpleVariableNode FirstNode
        {
            get => this.firstNode;
        }

        public MyComplexVariableNode SecondNode
        {
            get => this.secondNode;
        }

        #endregion

        #region ---------- Protected properties ----------

        protected override OpcNodeId DefaultTypeDefinitionId
        {
            get => "ns=1;s=MyComplexObjectType";
        }

        #endregion

        #region ---------- Private methods ----------

        private void InitializeChildren()
        {
            this.firstNode = new MySimpleVariableNode(
                    parent: this,
                    name: "1:First",
                    value: 10);

            this.secondNode = new MyComplexVariableNode(
                    parent: this,
                    name: "1:Second",
                    mandatoryValue: "A",
                    optionalValue: 20);
        }

        #endregion
    }
}
