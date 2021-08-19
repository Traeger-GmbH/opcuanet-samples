// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace NodeSet
{
    using Opc.UaFx;

    internal static class PackML
    {
        #region ---------- Public static readonly fields ----------

        public static readonly OpcNamespace Namespace = OpcNamespace.Create("http://opcfoundation.org/UA/PackML/");

        #endregion

        #region ---------- Public static properties (object type identifiers) ----------

        public static OpcNodeId BaseStateMachineId { get => Namespace.GetId(3); }
        public static OpcNodeId StatusObjectId { get => Namespace.GetId(4); }
        public static OpcNodeId AdminObjectId { get => Namespace.GetId(5); }
        public static OpcNodeId BaseObjectId { get => Namespace.GetId(6); }

        #endregion

        #region ---------- Public static properties (data type identifiers) ----------

        public static OpcNodeId ProductionMaintenanceModeId { get => Namespace.GetId(11); }

        public static OpcNodeId CountDataId { get => Namespace.GetId(14); }
        public static OpcNodeId CountDataBinaryId { get => Namespace.GetId(69); }
        public static OpcNodeId CountDataXmlId { get => Namespace.GetId(70); }

        public static OpcNodeId AlarmDataId { get => Namespace.GetId(15); }
        public static OpcNodeId AlarmDataBinaryId { get => Namespace.GetId(74); }
        public static OpcNodeId AlarmDataXmlId { get => Namespace.GetId(76); }

        public static OpcNodeId DescriptorDataId { get => Namespace.GetId(16); }
        public static OpcNodeId DescriptorDataBinaryId { get => Namespace.GetId(77); }
        public static OpcNodeId DescriptorDataXmlId { get => Namespace.GetId(78); }

        public static OpcNodeId IngredientsDataId { get => Namespace.GetId(17); }
        public static OpcNodeId IngredientsDataBinaryId { get => Namespace.GetId(79); }
        public static OpcNodeId IngredientsDataXmlId { get => Namespace.GetId(80); }

        public static OpcNodeId ProductDataId { get => Namespace.GetId(18); }
        public static OpcNodeId ProductDataBinaryId { get => Namespace.GetId(81); }
        public static OpcNodeId ProductDataXmlId { get => Namespace.GetId(82); }

        public static OpcNodeId RemoteInterfaceDataId { get => Namespace.GetId(19); }
        public static OpcNodeId RemoteInterfaceDataBinaryId { get => Namespace.GetId(83); }
        public static OpcNodeId RemoteInterfaceDataXmlId { get => Namespace.GetId(84); }

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

        public static void RegisterTypes()
        {
            OpcData.RegisterType<ProductionMaintenanceMode>(ProductionMaintenanceModeId);

            OpcData.RegisterType<PackMLCountData>(
                    CountDataId,
                    OpcEncoding.Binary(CountDataBinaryId),
                    OpcEncoding.Xml(CountDataXmlId));

            OpcData.RegisterType<PackMLAlarmData>(
                    AlarmDataId,
                    OpcEncoding.Binary(AlarmDataBinaryId),
                    OpcEncoding.Xml(AlarmDataXmlId));

            OpcData.RegisterType<PackMLDescriptorData>(
                    DescriptorDataId,
                    OpcEncoding.Binary(DescriptorDataBinaryId),
                    OpcEncoding.Xml(DescriptorDataXmlId));

            OpcData.RegisterType<PackMLIngredientsData>(
                    IngredientsDataId,
                    OpcEncoding.Binary(IngredientsDataBinaryId),
                    OpcEncoding.Xml(IngredientsDataXmlId));

            OpcData.RegisterType<PackMLProductData>(
                    ProductDataId,
                    OpcEncoding.Binary(ProductDataBinaryId),
                    OpcEncoding.Xml(ProductDataXmlId));

            OpcData.RegisterType<PackMLRemoteInterfaceData>(
                    RemoteInterfaceDataId,
                    OpcEncoding.Binary(RemoteInterfaceDataBinaryId),
                    OpcEncoding.Xml(RemoteInterfaceDataXmlId));
        }

        #endregion
    }
}
