' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System.Collections.Generic

Imports Opc.UaFx
Imports Opc.UaFx.Server

Namespace NodeSet
    Friend Class NodeManager
        Inherits OpcNodeManager

        '---------- Private fields ----------

        Private objectIndex As Integer
        Private objectsNode As OpcObjectNode

        '---------- Public constructors ----------

        Public Sub New()
            MyBase.New(Program.Namespace, PackML.Namespace)
        End Sub

        '---------- Public methods ----------

        Public Sub AddObject()
            Me.objectIndex += 1

            Dim name = $"Machine{Me.objectIndex}"
            Dim id = Me.DefaultNamespace.GetId(name)

            Me.AddNode(New PackMLBaseObjectNode(Me.objectsNode, name, id))
        End Sub

        '---------- Protected methods ----------

        Protected Overrides Iterator Function ImportNodes() As IEnumerable(Of OpcNodeSet)
            ' The data types have to be known before the server starts, otherwise it does not
            ' decode structures which a client passes as method arguments.
            PackML.Namespace.Resolve(Me)
            PackML.RegisterTypes()

            Yield OpcNodeSet.Load(".\Opc.Ua.PackML.NodeSet2.xml")
        End Function

        Protected Overrides Sub ImplementNode(node As IOpcNode)
            Dim objectNode = TryCast(node, OpcObjectNode)

            If objectNode IsNot Nothing AndAlso node.Name.Value = "PackMLObjects" Then
                Me.objectsNode = objectNode
            End If
        End Sub
    End Class
End Namespace
