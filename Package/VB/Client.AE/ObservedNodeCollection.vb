' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Collections.ObjectModel

Imports Opc.UaFx
Imports Opc.UaFx.Client

Namespace AE
    Friend Class ObservedNodeCollection
        Inherits KeyedCollection(Of String, ObservedNode)

        Public Sub New()
        End Sub

        Public Overloads Function Add(
                client As OpcClient,
                parentNodeId As OpcNodeId,
                name As String) As ObservedNode
            Dim item As New ObservedNode(parentNodeId, name)
            item.Initialize(client)

            Me.Add(item)
            Return item
        End Function

        Protected Overrides Function GetKeyForItem(item As ObservedNode) As String
            Return item.Id.ToString(OpcNodeIdFormat.Foundation)
        End Function
    End Class
End Namespace
