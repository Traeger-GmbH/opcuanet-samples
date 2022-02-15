// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace SimaticNodeSet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Opc.UaFx;
    using Opc.UaFx.Server;

    /// <summary>
    /// 
    /// </summary>
    public class NodeManager : OpcNodeManager
    {
        #region ---------- Public constructors ----------

        public NodeManager()
            : base(
                    // The namespace URIs are adopted from the "Server.SimaticNodeSet2.xml".
                    "urn:SIMATIC.S7-1500.OPC-UA.Application:PLC_1",
                    "http://opcfoundation.org/UA/DI/",
                    "http://www.siemens.com/simatic-s7-opcua")
        {
        }

        #endregion

        #region ---------- Public methods ----------

        public void Start()
        {
            this.InitializeNodes();
            // TODO: Implement simulation logic here.
        }

        #endregion

        #region ---------- Protected methods ----------

        protected override IEnumerable<OpcNodeSet> ImportNodes()
        {
            yield return OpcNodeSet.Load(@".\NodeSets\Opc.Ua.Di.NodeSet2.xml");
            yield return OpcNodeSet.Load(@".\NodeSets\Server.SimaticNodeSet2.xml");
        }

        private void InitializeNodes()
        {
            foreach (var node in this.Nodes) {
                if (node is OpcVariableNode variableNode
                        && variableNode.IsArray
                        && variableNode.Value is null) {
                    this.InitializeNode(variableNode);
                }
            }
        }

        #endregion

        #region ---------- Private static methods ----------

        private static OpcVariableValue<object> ReadArrayElement(
                OpcReadVariableValueContext context,
                OpcVariableValue<object> value)
        {
            if (context.Node is OpcVariableNode elementNode
                    && int.TryParse(elementNode.Name.Value, out var index)
                    && elementNode.Parent is OpcVariableNode arrayNode
                    && arrayNode.Value is Array array) {
                value = new OpcVariableValue<object>(array.GetValue(index));
            }

            return value;
        }

        private static OpcVariableValue<object> WriteArrayElement(
                OpcWriteVariableValueContext context,
                OpcVariableValue<object> value)
        {
            if (context.Node is OpcVariableNode elementNode
                    && int.TryParse(elementNode.Name.Value, out var index)
                    && elementNode.Parent is OpcVariableNode arrayNode
                    && arrayNode.Value is Array array) {
                array.SetValue(value.Value, index);
                arrayNode.UpdateChanges(context, OpcNodeChanges.Value);
            }

            return value;
        }

        #endregion

        #region ---------- Private methods ----------

        private void InitializeNode(OpcVariableNode node)
        {
            var context = this.SystemContext;

            var typeId = node.DataTypeId;
            var type = context.GetDataType(typeId);

            if (type != null) {
                var value = Activator.CreateInstance(type.UnderlyingType);

                foreach (var child in node.Children(context).OfType<OpcVariableNode>()) {
                    if (child.DataTypeId == typeId && int.TryParse(child.Name.Value, out var index)) {
                        child.Value = value;

                        child.ReadVariableValueCallback = ReadArrayElement;
                        child.WriteVariableValueCallback = WriteArrayElement;

                        child.AccessLevel |= OpcAccessLevel.CurrentReadOrWrite;
                        child.UserAccessLevel |= OpcAccessLevel.CurrentReadOrWrite;
                    }
                }

                node.AccessLevel |= OpcAccessLevel.CurrentReadOrWrite;
                node.UserAccessLevel |= OpcAccessLevel.CurrentReadOrWrite;

                node.Value = node.ArrayDimensions.CreateArray(type.UnderlyingType);
                node.ApplyChanges(context, recursive: true);
            }
        }

        #endregion
    }
}
