// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace NodeSet.Umati
{
    using Opc.UaFx;

    internal static class Machinery
    {
        #region ---------- Public const fields ----------

        public const string NamespaceUri = "http://opcfoundation.org/UA/Machinery/";

        public const int MachinesId = 1001;

        #endregion

        #region ---------- Public static readonly fields ----------

        public static readonly OpcNamespace Namespace = OpcNamespace.Create(NamespaceUri);

        #endregion

        #region ---------- Public static properties (object identifiers) ----------

        public static OpcNodeId Machines { get => Namespace.GetId(MachinesId); }

        #endregion
    }
}
