// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace NodeSet.Umati
{
    using Opc.UaFx;

    internal static class MachineTool
    {
        #region ---------- Public const fields ----------

        public const string NamespaceUri = "http://opcfoundation.org/UA/MachineTool/";

        #endregion

        #region ---------- Public static readonly fields ----------

        public static readonly OpcNamespace Namespace = OpcNamespace.Create(NamespaceUri);

        public const int NotificationTypeId = 7;
        public const int MachineToolTypeId = 13;
        public const int AlertTypeId = 39;

        #endregion

        #region ---------- Public static properties (object type identifiers) ----------

        public static OpcNodeId NotificationType { get => Namespace.GetId(NotificationTypeId); }

        public static OpcNodeId MachineToolType { get => Namespace.GetId(MachineToolTypeId); }

        public static OpcNodeId AlertType { get => Namespace.GetId(AlertTypeId); }

        #endregion

        #region ---------- Public static methods ----------

        public static OpcNodeId GetId(int value)
        {
            return Namespace.GetId(value);
        }

        public static OpcName GetName(string name)
        {
            return Namespace.GetName(name);
        }

        #endregion
    }
}
