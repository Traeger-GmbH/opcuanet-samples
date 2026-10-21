' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports Opc.UaFx

Namespace Umati
    <OpcEventType(MachineTool.AlertTypeId, MachineTool.NamespaceUri)>
    Friend Class Alert
        Inherits OpcAlarmCondition

        '---------- Public constructors ----------

        Public Sub New(dataStore As IOpcReadOnlyNodeDataStore)
            MyBase.New(dataStore)
        End Sub


        '---------- Public properties ----------

        Public ReadOnly Property ErrorCode As String
            Get
                Return Me.GetValue(Of String)()
            End Get
        End Property
    End Class
End Namespace
