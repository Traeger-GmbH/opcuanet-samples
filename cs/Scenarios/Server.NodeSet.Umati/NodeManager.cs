// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace NodeSet
{
    using System;
    using System.Collections.Generic;

    using Opc.UaFx;
    using Opc.UaFx.Server;

    internal partial class NodeManager : OpcNodeManager
    {
        #region ---------- Private fields ----------

        private OpcObjectNode alertsNode;
        private MachineToolAlarmConditionNode machineToolAlarmNode;

        #endregion

        #region ---------- Public constructors ----------

        public NodeManager()
            : base("http://www.umati.info", "http://www.umati.info/example")
        {
        }

        #endregion

        #region ---------- Public methods ----------

        public void ReportEvent()
        {
            var eventId = Guid.NewGuid();
            this.machineToolAlarmNode.EventId = eventId.ToByteArray();

            this.machineToolAlarmNode.AlarmIdentifier = "MyAlarm";
            this.machineToolAlarmNode.AuxParameters = "a;b;c;d";

            var context = this.SystemContext;

            this.machineToolAlarmNode.ChangeIsConfirmed(context, false);
            this.machineToolAlarmNode.ChangeIsAcked(context, false);
            this.machineToolAlarmNode.ChangeIsActive(context, true);

            this.machineToolAlarmNode.ReportEventFrom(context, this.alertsNode);
            Console.WriteLine("Event {0} reported.", eventId.ToString());
        }

        #endregion

        #region ---------- Protected methods ----------

        protected override IEnumerable<OpcNodeSet> ImportNodes()
        {
            yield return OpcNodeSet.Load(@".\umati_EMO2019.xml");
            yield return OpcNodeSet.Load(@".\umati-instances_EMO2019.xml");
        }

        protected override void ImplementNode(IOpcNode node)
        {
            if (node is OpcObjectNode objectNode && node.Name.Value == "Alerts")
                this.alertsNode = objectNode;

            base.ImplementNode(node);
        }

        protected override IEnumerable<IOpcNode> CreateNodes(OpcNodeReferenceCollection references)
        {
            this.machineToolAlarmNode = new MachineToolAlarmConditionNode();
            this.alertsNode.AddNotifier(this.SystemContext, this.machineToolAlarmNode);

            yield return this.machineToolAlarmNode;
        }

        #endregion
    }
}
