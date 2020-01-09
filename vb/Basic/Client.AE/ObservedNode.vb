' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System

Imports Opc.UaFx
Imports Opc.UaFx.Client

Namespace AE
    Friend Class ObservedNode
        Public Sub New(parentNodeId As OpcNodeId, name As String)
            Me.Id = OpcNodeId.Of(name, parentNodeId)
            Me.Label = (name + ":").PadRight(16)
        End Sub

        Public ReadOnly Property Id As OpcNodeId
        Public Property EventData As OpcEvent
        Public Property Label As String
        Public Property Unit As OpcEngineeringUnitInfo
        Public Property Value As OpcValue


        Public Sub Initialize(client As OpcClient)
            Dim node = client.BrowseNode(Me.Id)
            Dim analogNode = TryCast(node, OpcAnalogItemNodeInfo)

            If (Not analogNode Is Nothing) Then
                Me.Unit = analogNode.EngineeringUnit
            End If

            Me.Value = client.ReadNode(Me.Id)
        End Sub
    End Class
End Namespace
