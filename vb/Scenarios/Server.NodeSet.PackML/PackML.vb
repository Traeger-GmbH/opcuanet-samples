' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports Opc.UaFx

Namespace NodeSet
    Friend Module PackML
        '---------- Public static readonly fields ----------

        Public ReadOnly [Namespace] As OpcNamespace = OpcNamespace.Create("http://opcfoundation.org/UA/PackML/")

        '---------- Public static properties (object type identifiers) ----------

        Public ReadOnly Property BaseStateMachineId As OpcNodeId
            Get
                Return [Namespace].GetId(3)
            End Get
        End Property

        Public ReadOnly Property StatusObjectId As OpcNodeId
            Get
                Return [Namespace].GetId(4)
            End Get
        End Property

        Public ReadOnly Property AdminObjectId As OpcNodeId
            Get
                Return [Namespace].GetId(5)
            End Get
        End Property

        Public ReadOnly Property BaseObjectId As OpcNodeId
            Get
                Return [Namespace].GetId(6)
            End Get
        End Property

        '---------- Public static properties (data type identifiers) ----------

        Public ReadOnly Property ProductionMaintenanceModeId As OpcNodeId
            Get
                Return [Namespace].GetId(11)
            End Get
        End Property

        Public ReadOnly Property CountDataId As OpcNodeId
            Get
                Return [Namespace].GetId(14)
            End Get
        End Property

        Public ReadOnly Property CountDataBinaryId As OpcNodeId
            Get
                Return [Namespace].GetId(69)
            End Get
        End Property

        Public ReadOnly Property CountDataXmlId As OpcNodeId
            Get
                Return [Namespace].GetId(70)
            End Get
        End Property

        Public ReadOnly Property AlarmDataId As OpcNodeId
            Get
                Return [Namespace].GetId(15)
            End Get
        End Property

        Public ReadOnly Property AlarmDataBinaryId As OpcNodeId
            Get
                Return [Namespace].GetId(74)
            End Get
        End Property

        Public ReadOnly Property AlarmDataXmlId As OpcNodeId
            Get
                Return [Namespace].GetId(76)
            End Get
        End Property

        Public ReadOnly Property DescriptorDataId As OpcNodeId
            Get
                Return [Namespace].GetId(16)
            End Get
        End Property

        Public ReadOnly Property DescriptorDataBinaryId As OpcNodeId
            Get
                Return [Namespace].GetId(77)
            End Get
        End Property

        Public ReadOnly Property DescriptorDataXmlId As OpcNodeId
            Get
                Return [Namespace].GetId(78)
            End Get
        End Property

        Public ReadOnly Property IngredientsDataId As OpcNodeId
            Get
                Return [Namespace].GetId(17)
            End Get
        End Property

        Public ReadOnly Property IngredientsDataBinaryId As OpcNodeId
            Get
                Return [Namespace].GetId(79)
            End Get
        End Property

        Public ReadOnly Property IngredientsDataXmlId As OpcNodeId
            Get
                Return [Namespace].GetId(80)
            End Get
        End Property

        Public ReadOnly Property ProductDataId As OpcNodeId
            Get
                Return [Namespace].GetId(18)
            End Get
        End Property

        Public ReadOnly Property ProductDataBinaryId As OpcNodeId
            Get
                Return [Namespace].GetId(81)
            End Get
        End Property

        Public ReadOnly Property ProductDataXmlId As OpcNodeId
            Get
                Return [Namespace].GetId(82)
            End Get
        End Property

        Public ReadOnly Property RemoteInterfaceDataId As OpcNodeId
            Get
                Return [Namespace].GetId(19)
            End Get
        End Property

        Public ReadOnly Property RemoteInterfaceDataBinaryId As OpcNodeId
            Get
                Return [Namespace].GetId(83)
            End Get
        End Property

        Public ReadOnly Property RemoteInterfaceDataXmlId As OpcNodeId
            Get
                Return [Namespace].GetId(84)
            End Get
        End Property

        '---------- Public static methods ----------

        Public Function GetId(value As Integer) As OpcNodeId
            Return [Namespace].GetId(value)
        End Function

        Public Function GetName(name As String) As OpcName
            Return [Namespace].GetName(name)
        End Function

        Public Sub RegisterTypes()
            OpcData.RegisterType(Of ProductionMaintenanceMode)(ProductionMaintenanceModeId)

            OpcData.RegisterType(Of PackMLCountData)(
                    CountDataId,
                    OpcEncoding.Binary(CountDataBinaryId),
                    OpcEncoding.Xml(CountDataXmlId))

            OpcData.RegisterType(Of PackMLAlarmData)(
                    AlarmDataId,
                    OpcEncoding.Binary(AlarmDataBinaryId),
                    OpcEncoding.Xml(AlarmDataXmlId))

            OpcData.RegisterType(Of PackMLDescriptorData)(
                    DescriptorDataId,
                    OpcEncoding.Binary(DescriptorDataBinaryId),
                    OpcEncoding.Xml(DescriptorDataXmlId))

            OpcData.RegisterType(Of PackMLIngredientsData)(
                    IngredientsDataId,
                    OpcEncoding.Binary(IngredientsDataBinaryId),
                    OpcEncoding.Xml(IngredientsDataXmlId))

            OpcData.RegisterType(Of PackMLProductData)(
                    ProductDataId,
                    OpcEncoding.Binary(ProductDataBinaryId),
                    OpcEncoding.Xml(ProductDataXmlId))

            OpcData.RegisterType(Of PackMLRemoteInterfaceData)(
                    RemoteInterfaceDataId,
                    OpcEncoding.Binary(RemoteInterfaceDataBinaryId),
                    OpcEncoding.Xml(RemoteInterfaceDataXmlId))
        End Sub
    End Module
End Namespace
