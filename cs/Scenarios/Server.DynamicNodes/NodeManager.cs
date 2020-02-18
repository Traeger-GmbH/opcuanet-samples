// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace DynamicNodes
{
    using System;
    using System.Collections.Generic;

    using Opc.UaFx;
    using Opc.UaFx.Server;

    internal partial class NodeManager : OpcNodeManager
    {
        #region ---------- Private readonly fields ----------

        private readonly OpcFolderNode rootNode;

        #endregion

        #region ---------- Public constructors ----------

        public NodeManager()
            : base("http://sampleserver/dynamicnodes")
        {
            this.rootNode = new OpcFolderNode(this.DefaultNamespace.GetName("Root"));
        }

        #endregion

        #region ---------- Internal methods ----------

        internal IOpcNode AddNode(params string[] path)
        {
            var context = this.SystemContext;
            var parentNode = (OpcInstanceNode)this.rootNode;

            foreach (var name in path) {
                var childNode = (OpcInstanceNode)parentNode.Child(context, name);

                if (childNode == null) {
                    if (name[0] == '.')
                        childNode = new OpcDataVariableNode<int>(parentNode, name, DateTime.Now.Millisecond);
                    else
                        childNode = new OpcFolderNode(parentNode, name);

                    this.AddNode(context, childNode);

                    if (childNode is OpcVariableNode)
                        return childNode;
                }

                parentNode = childNode;
            }

            return parentNode;
        }

        #endregion

        #region ---------- Protected methods ----------

        protected override IEnumerable<IOpcNode> CreateNodes(OpcNodeReferenceCollection references)
        {
            references.Add(this.rootNode, OpcObjectTypes.ObjectsFolder);
            return new IOpcNode[] { this.rootNode };
        }

        #endregion
    }
}
