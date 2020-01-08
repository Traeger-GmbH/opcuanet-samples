// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace NodeAccess
{
    using System.Collections.Generic;

    using Opc.UaFx;
    using Opc.UaFx.Server;

    internal partial class NodeManager : OpcNodeManager
    {
        #region ---------- Public constructors ----------

        public NodeManager()
            : base("http://sampleserver/nodeaccess")
        {
        }

        #endregion

        #region ---------- Public properties ----------

        public OpcAccessControlList AccessControl
        {
            get;
            set;
        }

        #endregion

        #region ---------- Internal methods ----------

        internal bool CanWrite(OpcUserIdentity sessionIdentity)
        {
            var entries = this.AccessControl.Entries;

            foreach (var entry in entries) {
                if (entry.Principal.Identity is OpcUserIdentity serverIdentity) {
                    if (serverIdentity.DisplayName == sessionIdentity.DisplayName)
                        return entry.IsAllowed(OpcRequestType.Write);
                }
            }

            return false;
        }

        #endregion

        #region ---------- Protected methods ----------

        protected override IEnumerable<IOpcNode> CreateNodes(OpcNodeReferenceCollection references)
        {
            var folderNode = new OpcFolderNode(this.DefaultNamespace.GetName("Folder"));

            new VariableNode<int>(this, folderNode, "Var01", value: 42);
            new VariableNode<string>(this, folderNode, "Var02", value: "Hello World");

            references.Add(folderNode, OpcObjectTypes.ObjectsFolder);
            return new IOpcNode[] { folderNode };
        }

        #endregion
    }
}
