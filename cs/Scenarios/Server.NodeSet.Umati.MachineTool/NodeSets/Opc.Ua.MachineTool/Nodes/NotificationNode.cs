// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace NodeSet.Umati
{
    using Opc.UaFx;

    internal class NotificationNode : OpcObjectNode
    {
        #region ---------- Private fields ----------

        private MessagesNode messagesNode;

        #endregion

        #region ---------- Public constructors ----------

        public NotificationNode(IOpcNode parent)
            : base(parent, MachineTool.GetName("Notification"))
        {
            this.InitializeChildren();
        }

        #endregion

        #region ---------- Public properties ----------

        public MessagesNode Messages
        {
            get => this.messagesNode;
        }

        #endregion

        #region ---------- Protected properties ----------

        protected override OpcNodeId DefaultTypeDefinitionId
        {
            get => MachineTool.NotificationType;
        }

        #endregion

        #region ---------- Private methods ----------

        private void InitializeChildren()
        {
            this.messagesNode = new MessagesNode(this);
            this.AddChild(OpcContext.Empty, this.messagesNode);
        }

        #endregion
    }
}
