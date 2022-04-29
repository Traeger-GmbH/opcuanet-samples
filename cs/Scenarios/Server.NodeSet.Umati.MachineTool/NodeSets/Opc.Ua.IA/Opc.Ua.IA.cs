// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace NodeSet.Umati
{
    using Opc.UaFx;

    internal static class IA
    {
        #region ---------- Public const fields ----------

        public const string NamespaceUri = "http://opcfoundation.org/UA/IA/";

        #endregion

        #region ---------- Public static readonly fields ----------

        public static readonly OpcNamespace Namespace = OpcNamespace.Create(NamespaceUri);

        #endregion
    }
}
