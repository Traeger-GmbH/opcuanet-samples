// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace Umati
{
    using Opc.UaFx;

    internal static class MachineTool
    {
        #region ---------- Public const fields ----------

        public const string NamespaceUri = "http://opcfoundation.org/UA/MachineTool/";

        #endregion

        #region ---------- Public static readonly fields ----------

        public static readonly OpcNamespace Namespace = OpcNamespace.Create(NamespaceUri);

        public const int AlertTypeId = 39;

        #endregion

        #region ---------- Public static properties (object type identifiers) ----------

        public static OpcNodeId AlertType { get => Namespace.GetId(AlertTypeId); }

        #endregion
    }
}
