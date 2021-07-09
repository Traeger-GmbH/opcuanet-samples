// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace EventTypes
{
    using Opc.UaFx;

    internal class MachineJobEventNode : OpcEventNode
    {
        #region ---------- Private fields ----------

        private OpcPropertyNode<MachineJob> jobNode;

        #endregion

        #region ---------- Public constructors ----------

        public MachineJobEventNode(OpcName name)
            : base(name)
        {
            this.InitializeChildren();
        }

        public MachineJobEventNode(IOpcNode parent, OpcName name)
            : base(parent, name)
        {
            this.InitializeChildren();
        }

        #endregion

        #region ---------- Public properties ----------

        public OpcPropertyNode<MachineJob> Job
        {
            get => this.jobNode;
        }

        #endregion

        #region ---------- Protected properties ----------

        protected override OpcNodeId DefaultTypeDefinitionId
        {
            get => "ns=1;s=MachineJobEventType";
        }

        #endregion

        #region ---------- Private methods ----------

        private void InitializeChildren()
        {
            this.jobNode = new OpcPropertyNode<MachineJob>(
                    parent: this,
                    name: "1:Job",
                    value: null);

            this.AddChild(OpcContext.Empty, this.jobNode);
        }

        #endregion
    }
}
