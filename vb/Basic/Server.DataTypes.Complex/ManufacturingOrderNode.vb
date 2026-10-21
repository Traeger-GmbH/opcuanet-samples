' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports Opc.UaFx

Namespace DataTypes.Complex
    Public Class ManufacturingOrderNode
        Inherits OpcDataVariableNode(Of ManufacturingOrder)

        '---------- Public constructors ----------

        Public Sub New(parent As IOpcNode, name As OpcName, value As ManufacturingOrder)
            MyBase.New(parent, name, value)

            Me.Article = Me.CreateFieldNode(
                    NameOf(ManufacturingOrder.Article),
                    Function() Me.Value.Article,
                    Sub(fieldValue) Me.Value.Article = fieldValue)

            Me.Order = Me.CreateFieldNode(
                    NameOf(ManufacturingOrder.Order),
                    Function() Me.Value.Order,
                    Sub(fieldValue) Me.Value.Order = fieldValue)

            Me.Jobs = Me.CreateFieldNode(
                    NameOf(ManufacturingOrder.Jobs),
                    Function() Me.Value.Jobs,
                    Sub(fieldValue) Me.Value.Jobs = fieldValue,
                    Function(itemParent, itemName, itemValue) New MachineJobNode(itemParent, itemName, itemValue))
        End Sub

        '---------- Public properties ----------

        Public ReadOnly Property Article As OpcDataVariableNode(Of String)

        Public ReadOnly Property Order As OpcDataVariableNode(Of String)

        Public ReadOnly Property Jobs As OpcDataVariableNode(Of MachineJob())

        '---------- Public methods ----------

        Protected Overrides Function WriteVariableValueCore(context As OpcWriteVariableValueContext, value As OpcVariableValue(Of Object)) As OpcVariableValue(Of Object)
            Dim typedValue = TryCast(value.Value, ManufacturingOrder)

            If typedValue IsNot Nothing Then
                Me.Article.Value = typedValue.Article
                Me.Order.Value = typedValue.Order
                Me.Jobs.Value = typedValue.Jobs
            End If

            Return MyBase.WriteVariableValueCore(context, value)
        End Function
    End Class
End Namespace
