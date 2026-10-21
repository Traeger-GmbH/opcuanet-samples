' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System.Collections.Generic

Imports Opc.UaFx
Imports Opc.UaFx.Server

Namespace NodeSet.Umati
    Friend Class NodeManager
        Inherits OpcNodeManager

        '---------- Public constructors ----------

        Public Sub New()
            MyBase.New(MachineToolExample.Namespace)
        End Sub

        '---------- Public methods ----------

        Public Function AddMachineTool(name As String) As MachineToolNode
            Dim node = New MachineToolNode(name)
            Me.AddNode(node)

            Return node
        End Function

        '---------- Protected methods ----------

        Protected Overrides Iterator Function ImportNodes() As IEnumerable(Of OpcNodeSet)
            Yield OpcNodeSet.Load(".\NodeSets\MachineTool-Example.xml")
        End Function
    End Class
End Namespace
