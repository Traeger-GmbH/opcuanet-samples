' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System.Collections.Generic
Imports Opc.UaFx

Namespace GenericEvents
    Friend Class GenericEvent
        Inherits OpcEvent

        Public Sub New(dataStore As IOpcReadOnlyNodeDataStore)
            MyBase.New(dataStore)
        End Sub

        Public Function GetData() As Dictionary(Of String, Object)
            Dim data As Dictionary(Of String, Object) = Nothing
            Dim eventView = TryCast(Me.DataStore, OpcEventNodeView)

            If eventView IsNot Nothing Then
                Dim operands = eventView.Filter.SelectClause
                Dim fields = eventView.Data.Fields

                data = New Dictionary(Of String, Object)()

                For index = 0 To operands.Count - 1
                    Dim operand = operands(index)
                    Dim value = New OpcValue(fields(index).Value).Value

                    data.Add(operand.NodePath.ToString(), value)
                Next
            End If

            Return data
        End Function
    End Class
End Namespace
