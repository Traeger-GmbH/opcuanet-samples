// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace DynamicNodes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Opc.Ua;
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
            return this.AddNode(this.rootNode, path);
        }

        internal IOpcNode AddNode(IOpcNode parentNode, params string[] path)
        {
            var context = this.SystemContext;

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

        internal IOpcNode AddRootNode(params string[] path)
        {
            var newNode = new OpcFolderNode(path[0].TrimStart('$'));

            var reference = OpcNodeReference.To(OpcObjectTypes.ObjectsFolder);
            this.AddNode(this.SystemContext, newNode, reference);

            if (path.Length > 1)
                return this.AddNode(newNode, path.Skip(1).ToArray());

            return newNode;
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
