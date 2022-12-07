// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace NodeSet.Umati
{
    using System;

    using Opc.UaFx;
    using Opc.UaFx.Server;

    internal class MessagesNode : OpcObjectNode
    {
        #region ---------- Private fields ----------

        private AlertNode alertsNode;

        #endregion

        #region ---------- Public constructors ----------

        public MessagesNode(IOpcNode parent)
            : base(parent, MachineTool.GetName("Messages"))
        {
            this.InitializeChildren();
        }

        #endregion

        #region ---------- Public properties ----------

        public AlertNode Alerts
        {
            get => this.alertsNode;
        }

        #endregion

        #region ---------- Public methods ----------

        public void Report(OpcContext context, string errorCode)
        {
            var source = this.Parent?.Parent;

            this.alertsNode.SourceNodeId = source?.Id;
            this.alertsNode.SourceName = source?.Name?.Value;

            this.alertsNode.ErrorCode.Value = errorCode;
            ((OpcServer)context.Owner).ReportEvent(this.alertsNode);
        }

        #endregion

        #region ---------- Protected methods ----------

        protected override void OnBeforeAdded(OpcNodeAddedEventArgs e)
        {
            // Objects > Server > {HasEventSource:this}
            e.References.Add(OpcNodeReference
                    .From(Opc.Ua.ObjectIds.Server)
                    .ToChild(this, OpcReferenceType.HasEventSource));

            base.OnBeforeAdded(e);
        }

        #endregion

        #region ---------- Private methods ----------

        private void InitializeChildren()
        {
            this.alertsNode = new AlertNode(this, MachineTool.GetName("Alerts"));
            this.alertsNode.ConfirmCallback = HandleAlertsConfirm;

            this.AddChild(OpcContext.Empty, this.alertsNode);
        }

        private OpcStatusCode HandleAlertsConfirm(
                OpcNodeContext<OpcConditionNode> context,
                byte[] eventId,
                OpcText comment)
        {
            Console.WriteLine("Confirmend: " + comment.Value);
            return OpcStatusCode.Good;
        }

        #endregion
    }
}
