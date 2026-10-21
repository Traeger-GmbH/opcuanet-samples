Imports System

Imports Opc.Ua
Imports Opc.UaFx

Namespace NodeSet
    Public Class PackMLAlarmData
        Public Property ID As Integer

        Public Property Value As Integer

        Public Property Message As String

        Public Property Category As Integer

        Public Property DateTime As DateTime

        Public Property AckDateTime As DateTime

        Public Property Trigger As Boolean
    End Class

    Public Class PackMLCountData
        Public Property ID As Integer

        Public Property Name As String

        Public Property Unit As OpcEngineeringUnitInfo

        Public Property Count As Integer

        Public Property AccCount As Integer
    End Class

    Public Class PackMLDescriptorData
        Public Property ID As Integer

        Public Property Name As String

        Public Property Unit As OpcEngineeringUnitInfo

        Public Property Value As Single
    End Class

    Public Class PackMLIngredientsData
        Public Property IngredientID As Integer

        Public Property NoOfParameter As Integer

        <OpcDataTypeMemberLength(NameOf(NoOfParameter))>
        Public Property Parameter As PackMLDescriptorData()
    End Class

    Public Class PackMLProductData
        Public Property ProductID As Integer

        Public Property NoOfProcessVariables As Integer

        <OpcDataTypeMemberLength(NameOf(NoOfProcessVariables))>
        Public Property ProcessVariables As PackMLDescriptorData()

        Public Property NoOfIngredients As Integer

        <OpcDataTypeMemberLength(NameOf(NoOfIngredients))>
        Public Property Ingredients As PackMLIngredientsData()
    End Class

    Public Class PackMLRemoteInterfaceData
        Public Property Number As Integer

        Public Property ControlCmdNumber As Integer

        Public Property CmdValue As Integer

        Public Property NoOfParameter As Integer

        <OpcDataTypeMemberLength(NameOf(NoOfParameter))>
        Public Property Parameter As PackMLDescriptorData()
    End Class

    Public Enum ProductionMaintenanceMode
        Invalid = 0
        Produce = 1
        Maintenance = 2
        Manual = 3
    End Enum
End Namespace
