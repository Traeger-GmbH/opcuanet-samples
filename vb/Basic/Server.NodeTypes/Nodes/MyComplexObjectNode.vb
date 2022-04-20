'Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports Opc.UaFx

Namespace NodeTypes
    Friend Class MyComplexObjectNode
        Inherits OpcObjectNode

#Region "---------- Private fields ----------"

        Private _firstNode As MySimpleVariableNode
        Private _secondNode As MyComplexVariableNode

#End Region

#Region "---------- Public constructors ----------"

        Public Sub New(ByVal name As OpcName)
            MyBase.New(name)
            Me.InitializeChildren()
        End Sub

        Public Sub New(ByVal parent As IOpcNode, ByVal name As OpcName)
            MyBase.New(parent, name)
            Me.InitializeChildren()
        End Sub

#End Region

#Region "---------- Public properties ----------"

        Public ReadOnly Property FirstNode As MySimpleVariableNode
            Get
                Return Me._firstNode
            End Get
        End Property

        Public ReadOnly Property SecondNode As MyComplexVariableNode
            Get
                Return Me._secondNode
            End Get
        End Property

#End Region

#Region "---------- Protected properties ----------"

        Protected Overrides ReadOnly Property DefaultTypeDefinitionId As OpcNodeId
            Get
                Return "ns=1;s=MyComplexObjectType"
            End Get
        End Property

#End Region

#Region "---------- Private methods ----------"

        Private Sub InitializeChildren()
            Me._firstNode = New MySimpleVariableNode(
                    parent:=Me,
                    name:="1:First",
                    value:=10)

            Me._secondNode = New MyComplexVariableNode(
                    parent:=Me,
                    name:="1:Second",
                    mandatoryValue:="A",
                    optionalValue:=20)
        End Sub

#End Region
    End Class
End Namespace
