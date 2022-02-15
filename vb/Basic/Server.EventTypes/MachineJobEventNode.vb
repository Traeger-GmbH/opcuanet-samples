'Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports Opc.UaFx

Namespace EventTypes
    Friend Class MachineJobEventNode
        Inherits OpcEventNode

        '---------- Private fields ----------

        Private jobNode As OpcPropertyNode(Of MachineJob)

        '---------- Public constructors ----------

        Public Sub New(ByVal name As OpcName)
            MyBase.New(name)
            Me.InitializeChildren()
        End Sub

        Public Sub New(ByVal parent As IOpcNode, ByVal name As OpcName)
            MyBase.New(parent, name)
            Me.InitializeChildren()
        End Sub

        '---------- Public properties ----------

        Public ReadOnly Property Job As OpcPropertyNode(Of MachineJob)
            Get
                Return Me.jobNode
            End Get
        End Property

        '---------- Protected properties ----------

        Protected Overrides ReadOnly Property DefaultTypeDefinitionId As OpcNodeId
            Get
                Return "ns=1;s=MachineJobEventType"
            End Get
        End Property

        '---------- Private methods ----------

        Private Sub InitializeChildren()
            Me.jobNode = New OpcPropertyNode(Of MachineJob)(parent:=Me, name:="1:Job", value:=Nothing)
            Me.AddChild(OpcContext.Empty, Me.jobNode)
        End Sub
    End Class
End Namespace
