' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports Opc.UaFx

Namespace NodeSet.Umati
    Friend Class AlertNode
        Inherits OpcAlarmConditionNode

        '---------- Private fields ----------

        Private errorCodeNode As OpcPropertyNode(Of String)

        '---------- Public constructors ----------

        Public Sub New(name As OpcName)
            MyBase.New(name)
            Me.InitializeChildren()
        End Sub

        Public Sub New(parent As IOpcNode, name As OpcName)
            MyBase.New(parent, name)
            Me.InitializeChildren()
        End Sub

        '---------- Public properties ----------

        Public ReadOnly Property ErrorCode As OpcPropertyNode(Of String)
            Get
                Return Me.errorCodeNode
            End Get
        End Property

        '---------- Protected properties ----------

        Protected Overrides ReadOnly Property DefaultTypeDefinitionId As OpcNodeId
            Get
                Return MachineTool.AlertType
            End Get
        End Property

        '---------- Private methods ----------

        Private Overloads Sub InitializeChildren()
            Me.errorCodeNode = New OpcPropertyNode(Of String)(
                    parent:=Me,
                    name:=MachineTool.GetName("ErrorCode"),
                    value:=Nothing)

            Me.AddChild(OpcContext.Empty, Me.errorCodeNode)
        End Sub
    End Class
End Namespace
