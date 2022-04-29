// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace NodeSet.Umati
{
    using Opc.UaFx;

    internal class MachineToolNode : OpcObjectNode
    {
        #region ---------- Private fields ----------

        private NotificationNode notificationNode;

        #endregion

        #region ---------- Public constructors ----------

        public MachineToolNode(OpcName name)
            : base(name)
        {
            this.InitializeChildren();
        }

        public MachineToolNode(IOpcNode parent, OpcName name)
            : base(parent, name)
        {
            this.InitializeChildren();
        }

        #endregion

        #region ---------- Public properties ----------

        public NotificationNode Notification
        {
            get => this.notificationNode;
        }

        #endregion

        #region ---------- Protected properties ----------

        protected override OpcNodeId DefaultTypeDefinitionId
        {
            get => MachineTool.MachineToolType;
        }

        #endregion

        #region ---------- Protected methods ----------

        protected override void OnBeforeAdded(OpcNodeAddedEventArgs e)
        {
            // Objects > Machines > {Organizes:this}
            e.References.Add(OpcNodeReference
                    .From(Machinery.Machines)
                    .ToChild(this));

            base.OnBeforeAdded(e);
        }

        #endregion

        #region ---------- Private methods ----------

        private void InitializeChildren()
        {
            this.notificationNode = new NotificationNode(this);
            this.AddChild(OpcContext.Empty, this.notificationNode);
        }

        #endregion
    }
}
