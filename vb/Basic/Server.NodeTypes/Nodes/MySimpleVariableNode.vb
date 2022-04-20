'Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports Opc.UaFx

Namespace NodeTypes
    Friend Class MySimpleVariableNode
        Inherits OpcDataVariableNode(Of Integer)

#Region "---------- Public constructors ----------"

        Public Sub New(ByVal name As OpcName, ByVal value As Integer)
            MyBase.New(name, value)
        End Sub

        Public Sub New(ByVal parent As IOpcNode, ByVal name As OpcName, ByVal value As Integer)
            MyBase.New(parent, name, value)
        End Sub

#End Region

#Region "---------- Protected properties ----------"

        Protected Overrides ReadOnly Property DefaultTypeDefinitionId As OpcNodeId
            Get
                Return "ns=1;s=MySimpleVariableType"
            End Get
        End Property

#End Region
    End Class
End Namespace
