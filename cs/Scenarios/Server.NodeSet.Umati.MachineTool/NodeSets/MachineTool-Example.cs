// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace NodeSet.Umati
{
    using Opc.UaFx;

    internal static class MachineToolExample
    {
        #region ---------- Public const fields ----------

        public const string NamespaceUri = "http://yourorganisation.org/MachineTool-Example/";

        #endregion

        #region ---------- Public static readonly fields ----------

        public static readonly OpcNamespace Namespace = OpcNamespace.Create(NamespaceUri);

        #endregion
    }
}
