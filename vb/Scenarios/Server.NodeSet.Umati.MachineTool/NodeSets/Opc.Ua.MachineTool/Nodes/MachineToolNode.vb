' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports Opc.UaFx

Namespace NodeSet.Umati
    Friend Class MachineToolNode
        Inherits OpcObjectNode

        '---------- Private fields ----------

        Private notificationNode As NotificationNode

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

        Public ReadOnly Property Notification As NotificationNode
            Get
                Return Me.notificationNode
            End Get
        End Property

        '---------- Protected properties ----------

        Protected Overrides ReadOnly Property DefaultTypeDefinitionId As OpcNodeId
            Get
                Return MachineTool.MachineToolType
            End Get
        End Property

        '---------- Protected methods ----------

        Protected Overrides Sub OnBeforeAdded(e As OpcNodeAddedEventArgs)
            ' Objects > Machines > {Organizes:this}
            e.References.Add(OpcNodeReference _
                    .From(Machinery.Machines) _
                    .ToChild(Me))

            MyBase.OnBeforeAdded(e)
        End Sub

        '---------- Private methods ----------

        Private Overloads Sub InitializeChildren()
            Me.notificationNode = New NotificationNode(Me)
            Me.AddChild(OpcContext.Empty, Me.notificationNode)
        End Sub
    End Class
End Namespace
