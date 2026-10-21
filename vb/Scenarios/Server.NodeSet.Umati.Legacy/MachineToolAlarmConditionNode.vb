' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports Opc.UaFx

Namespace NodeSet
    Friend Class MachineToolAlarmConditionNode
        Inherits OpcAlarmConditionNode

        '---------- Private readonly fields ----------

        Private ReadOnly alarmIdentifierNode As OpcDataVariableNode(Of String)
        Private ReadOnly auxParametersNode As OpcDataVariableNode(Of String)

        '---------- Public constructors ----------

        Public Sub New()
            MyBase.New("MachineToolAlarmCondition")

            Me.alarmIdentifierNode = New OpcDataVariableNode(Of String)(Me, "2:AlarmIdentifier")
            Me.auxParametersNode = New OpcDataVariableNode(Of String)(Me, "2:AuxParameters")

            Me.EventTypeId = "ns=2;i=1042"
        End Sub

        '---------- Public properties ----------

        Public Property AlarmIdentifier As String
            Get
                Return Me.alarmIdentifierNode.Value
            End Get
            Set(value As String)
                Me.alarmIdentifierNode.Value = value
            End Set
        End Property

        Public Property AuxParameters As String
            Get
                Return Me.auxParametersNode.Value
            End Get
            Set(value As String)
                Me.auxParametersNode.Value = value
            End Set
        End Property

        '---------- Protected properties ----------

        Protected Overrides ReadOnly Property DefaultTypeDefinitionId As OpcNodeId
            Get
                Return "ns=2;i=1042"
            End Get
        End Property
    End Class
End Namespace
