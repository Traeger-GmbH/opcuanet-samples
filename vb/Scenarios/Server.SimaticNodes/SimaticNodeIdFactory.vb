' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports Opc.UaFx

Namespace SimaticNodes
    Public Class SimaticNodeIdFactory
        Inherits OpcNominalNodeIdFactory

        Public Sub New()
            MyBase.New()
            Me.Separator = "."c
        End Sub

        Protected Overrides Function Create(
                context As OpcContext,
                nodeNamespace As OpcNamespace,
                node As IOpcNodeInfo,
                nodeName As OpcName) As OpcNodeId
            If Not OpcName.IsNullOrEmpty(nodeName) Then
                nodeName = nodeName.Namespace.GetName("""" & nodeName.Value & """")
            End If

            Return MyBase.Create(context, nodeNamespace, node, nodeName)
        End Function
    End Class
End Namespace
