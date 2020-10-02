// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace ClientAddedNodesWithHistory
{
    using System;
    using System.Collections.Generic;

    using Opc.UaFx;
    using Opc.UaFx.Server;

    internal class NodeManager : OpcNodeManager
    {
        #region ---------- Private readonly fields ----------

        private readonly Dictionary<IOpcNode, OpcNodeHistorian> historians;

        #endregion

        #region ---------- Public constructors ----------

        public NodeManager()
            : base("http://sampleserver/clientnodeswithhistory")
        {
            this.historians = new Dictionary<IOpcNode, OpcNodeHistorian>();
        }

        #endregion

        #region ---------- Protected methods ----------

        protected override void AddNode(
                OpcContext context,
                IOpcNode node,
                IEnumerable<IOpcNodeReferenceAware> references)
        {
            base.AddNode(context, node, references);

            // "Catch" nodes added by the client and "attach" the on-demand timestamp determination
            // (see HandleWriteVariableValue) and a simple in-memory historian which shall
            // automatically fill its historical values whenever a node value is written.
            if (node is OpcVariableNode variableNode && variableNode.AccessLevel.HasFlag(OpcAccessLevel.HistoryRead)) {
                variableNode.WriteVariableValueCallback = this.HandleWriteVariableValue;
                variableNode.IsHistorizing = true;

                var historian = new OpcNodeHistorian(this, variableNode);
                historian.AutoUpdateHistory = true;

                this.historians.Add(node, historian);
            }
        }

        protected override IEnumerable<IOpcNode> CreateNodes(OpcNodeReferenceCollection references)
        {
            // We just want to define a simple "entry point" for client added nodes.
            var machineNode = new OpcObjectNode(this.DefaultNamespace.GetName("Machine"));
            references.Add(machineNode, OpcObjectTypes.ObjectsFolder);

            return new IOpcNode[] { machineNode };
        }

        protected override IOpcNodeHistoryProvider RetrieveNodeHistoryProvider(IOpcNode node)
        {
            // Determine existing historian of the node the history provider is requested for.
            if (this.historians.TryGetValue(node, out var historian))
                return historian;

            return base.RetrieveNodeHistoryProvider(node);
        }

        #endregion

        #region ---------- Private methods ----------

        private OpcVariableValue<object> HandleWriteVariableValue(
                OpcWriteVariableValueContext context,
                OpcVariableValue<object> value)
        {
            // A client may not pass a timestamp associated with a new value or may use the same
            // timestamp as already used by the variable node.
            //
            // Upon we want to enforce that every value written to the node uses its own timestamp
            // we update the variable value with the current time to assure that every write
            // results into a new entry in the history of the node.
            if (value.Timestamp == null || value.Timestamp == context.Node.Timestamp)
                value = new OpcVariableValue<object>(value.Value, DateTime.UtcNow, value.Status);

            return value;
        }

        #endregion
    }
}
