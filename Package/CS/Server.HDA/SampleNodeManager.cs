// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace HDA
{
    using System;
    using System.Collections.Generic;

    using Opc.UaFx;
    using Opc.UaFx.Server;

    /// <summary>
    /// Represents a sample implementation of a custom OpcNodeManager.
    /// </summary>
    internal class SampleNodeManager : OpcNodeManager
    {
        #region ---------- Private fields ----------

        /// <summary>
        /// Stores an instance of the <see cref="OpcNodeHistorian"/> used to manage the historical
        /// data associated with the 'Position' node.
        /// </summary>
        private OpcNodeHistorian positionHistorian;

        #endregion

        #region ---------- Public constructors ----------

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleNodeManager"/> class.
        /// </summary>
        public SampleNodeManager()
            : base("http://sampleserver/machines")
        {
        }

        #endregion

        #region ---------- Protected methods ----------

        /// <summary>
        /// Creates the nodes provided and associated with the node manager.
        /// </summary>
        /// <param name="references">A dictionary used to determine the logical references between
        /// existing nodes (e.g. OPC default nodes) and the nodes provided by the node
        /// manager.</param>
        /// <returns>An enumerable containing the root nodes of the node manager.</returns>
        /// <remarks>This method will be only called once by the server on start up.</remarks>
        protected override IEnumerable<IOpcNode> CreateNodes(OpcNodeReferenceCollection references)
        {
            // It is necessary to assign to all root nodes one of the namespaces used to
            // identify one of the associated namespaces (see the ctor of the class). This
            // namespace does identify the node as member of the namespace of the node
            // manager. Optionally it is possible to assign namespace to the child nodes
            // too. But by default their missing namespace will be auto-completed through the
            // namespace of their parent node.
            var machineOne = new OpcFolderNode(this.DefaultNamespace.GetName("Machine_1"));

            // Add new reference to make the node visible beneath the ObjectsFolder
            // (the top most root node within every OPC UA server).
            references.Add(machineOne, OpcObjectTypes.ObjectsFolder);

            new OpcDataVariableNode<string>(machineOne, "Name", "Machine 1");
            new OpcDataVariableNode<byte>(machineOne, "Status", 1);

            this.positionHistorian = new OpcNodeHistorian(
                    this, new OpcDataVariableNode<int>(machineOne, "Position", -1));

            SampleNodeManager.CreateHistoryEntries(this.positionHistorian);

            new OpcDataVariableNode<bool>(machineOne, "IsActive", true);
            new OpcDataVariableNode<double>(machineOne, "Temperature", 18.3);

            return new IOpcNode[] { machineOne };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override IOpcNodeHistoryProvider RetrieveNodeHistoryProvider(IOpcNode node)
        {
            if (this.positionHistorian.Node == node)
                return this.positionHistorian;

            return base.RetrieveNodeHistoryProvider(node);
        }

        #endregion

        #region ---------- Private static methods ----------

        /// <summary>
        /// Creates the different entries contained in the raw history and modified raw history
        /// of the node historian used.
        /// </summary>
        /// <param name="historian">The <see cref="OpcNodeHistorian"/> its history
        /// entries being created.</param>
        private static void CreateHistoryEntries(OpcNodeHistorian historian)
        {
            for (int second = 0; second < 3600; second++) {
                var value = new OpcHistoryValue(
                        1000 + second, DateTime.UtcNow.Date.AddHours(6).AddSeconds(second));

                if ((second % 30) == 0) {
                    historian.ModifiedHistory.Add(value.CreateModified(
                            OpcHistoryModificationType.Delete, "Anonymous", value.Timestamp));
                }
                else {
                    historian.History.Add(value);
                }
            }
        }

        #endregion
    }
}
