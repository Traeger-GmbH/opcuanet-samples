// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace NodeSet
{
    using Opc.UaFx;

    /// <summary>
    /// https://reference.opcfoundation.org/v104/PackML/v101/docs/6.3.3
    /// </summary>
    internal class PackMLStatusObjectNode : OpcObjectNode
    {
        #region ---------- Public constructors ----------

        public PackMLStatusObjectNode(IOpcNode parent, OpcName name)
            : base(parent, name)
        {
            // TODO: Implement according to companion specification.
        }

        #endregion

        #region ---------- Protected properties ----------

        protected override OpcNodeId DefaultTypeDefinitionId
        {
            get => PackML.StatusObjectId;
        }

        #endregion
    }
}
