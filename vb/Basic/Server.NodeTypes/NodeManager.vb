'Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System.Collections.Generic

Imports Opc.UaFx
Imports Opc.UaFx.Server

Namespace NodeTypes
    Partial Friend Class NodeManager
        Inherits OpcNodeManager

#Region "---------- Public constructors ----------"

        Public Sub New()
            MyBase.New("http://sampleserver/samplenodetypes")
        End Sub

#End Region

#Region "---------- Protected methods ----------"

        Protected Overrides Iterator Function ImportNodes() As IEnumerable(Of OpcNodeSet)
            Yield OpcNodeSet.Load(".\MyNodeSet.xml")
        End Function

        Protected Overrides Function CreateNodes(ByVal references As OpcNodeReferenceCollection) As IEnumerable(Of IOpcNode)
            Dim variableNodes = New OpcObjectNode("MyVariables")
            references.Add(variableNodes, OpcObjectTypes.ObjectsFolder)

            Dim simpleVariableNodes = New OpcObjectNode(variableNodes, "Simple")
            Dim simpleVar01Node = New MySimpleVariableNode(simpleVariableNodes, "Var01", 1)
            Dim simpleVar02Node = New MySimpleVariableNode(simpleVariableNodes, "Var02", 2)

            Dim complexVariableNodes = New OpcObjectNode(variableNodes, "Complex")
            Dim complexVar01Node = New MyComplexVariableNode(complexVariableNodes, "Var01", "A")
            Dim complexVar02Node = New MyComplexVariableNode(complexVariableNodes, "Var02", "B", 42)

            Dim objectNodes = New OpcObjectNode("MyObjects")
            references.Add(objectNodes, OpcObjectTypes.ObjectsFolder)

            Dim simpleObjectNodes = New OpcObjectNode(objectNodes, "Simple")
            Dim simpleObject01Node = New MySimpleObjectNode(simpleObjectNodes, "Object01")
            Dim simpleObject02Node = New MySimpleObjectNode(simpleObjectNodes, "Object02")

            Dim complexObjectNodes = New OpcObjectNode(objectNodes, "Complex")
            Dim complexObject01Node = New MyComplexObjectNode(complexObjectNodes, "Object01")
            Dim complexObject02Node = New MyComplexObjectNode(complexObjectNodes, "Object02")

            Return {variableNodes, objectNodes}
        End Function

#End Region
    End Class
End Namespace
