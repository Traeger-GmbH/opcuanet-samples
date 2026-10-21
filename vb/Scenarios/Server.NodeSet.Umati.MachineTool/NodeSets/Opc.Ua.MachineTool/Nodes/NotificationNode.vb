' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports Opc.UaFx

Namespace NodeSet.Umati
    Friend Class NotificationNode
        Inherits OpcObjectNode

        '---------- Private fields ----------

        Private messagesNode As MessagesNode

        '---------- Public constructors ----------

        Public Sub New(parent As IOpcNode)
            MyBase.New(parent, MachineTool.GetName("Notification"))
            Me.InitializeChildren()
        End Sub

        '---------- Public properties ----------

        Public ReadOnly Property Messages As MessagesNode
            Get
                Return Me.messagesNode
            End Get
        End Property

        '---------- Protected properties ----------

        Protected Overrides ReadOnly Property DefaultTypeDefinitionId As OpcNodeId
            Get
                Return MachineTool.NotificationType
            End Get
        End Property

        '---------- Private methods ----------

        Private Overloads Sub InitializeChildren()
            Me.messagesNode = New MessagesNode(Me)
            Me.AddChild(OpcContext.Empty, Me.messagesNode)
        End Sub
    End Class
End Namespace
