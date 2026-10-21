' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports Opc.UaFx

Namespace Umati
    <OpcEventType("ns=2;i=1042")>
    Friend Class MachineToolAlarmCondition
        Inherits OpcAlarmCondition

        '---------- Public constructors ----------

        Public Sub New(dataStore As IOpcReadOnlyNodeDataStore)
            MyBase.New(dataStore)
        End Sub


        '---------- Public properties ----------

        Public ReadOnly Property AlarmIdentifier As String
            Get
                Return Me.DataStore.Get(Of String)("2:AlarmIdentifier")
            End Get
        End Property

        Public ReadOnly Property AuxParameters As String
            Get
                Return Me.DataStore.Get(Of String)("2:AuxParameters")
            End Get
        End Property
    End Class
End Namespace
