' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports Opc.UaFx

Namespace NodeTypes
    Friend Class MyComplexVariableNode
        Inherits OpcDataVariableNode(Of Integer)

#Region "---------- Private fields ----------"

        Private _mandatoryValueNode As OpcDataVariableNode(Of String)
        Private _optionalValueNode As OpcDataVariableNode(Of Integer)

#End Region

#Region "---------- Public constructors ----------"

        Public Sub New(
                ByVal name As OpcName,
                ByVal mandatoryValue As String,
                ByVal Optional optionalValue As Integer = -1)
            MyBase.New(name)
            Me.InitializeChildren(mandatoryValue, optionalValue)
        End Sub

        Public Sub New(
                ByVal parent As IOpcNode,
                ByVal name As OpcName,
                ByVal mandatoryValue As String,
                ByVal Optional optionalValue As Integer = -1)
            MyBase.New(parent, name)
            Me.InitializeChildren(mandatoryValue, optionalValue)
        End Sub

#End Region

#Region "---------- Public properties ----------"

        Public Property MandatoryValue As String
            Get
                Return Me._mandatoryValueNode.Value
            End Get
            Set(ByVal value As String)
                Me._mandatoryValueNode.Value = value
            End Set
        End Property

        Public ReadOnly Property HasOptionalValue As Boolean
            Get
                Return Me._optionalValueNode IsNot Nothing
            End Get
        End Property

        Public Property OptionalValue As Integer
            Get
                If Me._optionalValueNode Is Nothing Then Throw New NotSupportedException()
                Return Me._optionalValueNode.Value
            End Get

            Set(ByVal value As Integer)
                If Me._optionalValueNode Is Nothing Then Throw New NotSupportedException()
                Me._optionalValueNode.Value = value
            End Set
        End Property

#End Region

#Region "---------- Protected properties ----------"

        Protected Overrides ReadOnly Property DefaultTypeDefinitionId As OpcNodeId
            Get
                Return "ns=1;s=MyComplexVariableType"
            End Get
        End Property

#End Region

#Region "---------- Private methods ----------"

        Private Sub InitializeChildren(ByVal mandatoryValue As String, ByVal optionalValue As Integer)
            Me._mandatoryValueNode = New OpcDataVariableNode(Of String)(
                    parent:=Me,
                    name:="1:MandatoryValue",
                    value:=mandatoryValue)

            If optionalValue <> -1 Then
                Me._optionalValueNode = New OpcDataVariableNode(Of Integer)(
                        parent:=Me,
                        name:="1:OptionalValue",
                        value:=optionalValue)
            End If
        End Sub

#End Region
    End Class
End Namespace
