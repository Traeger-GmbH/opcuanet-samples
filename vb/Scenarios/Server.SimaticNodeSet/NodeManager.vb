' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Collections.Generic
Imports System.Linq

Imports Opc.UaFx
Imports Opc.UaFx.Server

Namespace SimaticNodeSet
    ''' <summary>
    '''
    ''' </summary>
    Public Class NodeManager
        Inherits OpcNodeManager

        '---------- Public constructors ----------

        Public Sub New()
            ' The namespace URIs are adopted from the "Server.SimaticNodeSet2.xml".
            MyBase.New(
                    "urn:SIMATIC.S7-1500.OPC-UA.Application:PLC_1",
                    "http://opcfoundation.org/UA/DI/",
                    "http://www.siemens.com/simatic-s7-opcua")
        End Sub

        '---------- Public methods ----------

        Public Sub Start()
            Me.InitializeNodes()
            ' TODO: Implement simulation logic here.
        End Sub

        '---------- Protected methods ----------

        Protected Overrides Iterator Function ImportNodes() As IEnumerable(Of OpcNodeSet)
            Yield OpcNodeSet.Load(".\NodeSets\Opc.Ua.Di.NodeSet2.xml")
            Yield OpcNodeSet.Load(".\NodeSets\Server.SimaticNodeSet2.xml")
        End Function

        Private Sub InitializeNodes()
            For Each node In Me.Nodes
                Dim variableNode = TryCast(node, OpcVariableNode)

                If variableNode IsNot Nothing _
                        AndAlso variableNode.IsArray _
                        AndAlso variableNode.Value Is Nothing Then
                    Me.InitializeNode(variableNode)
                End If
            Next
        End Sub

        '---------- Private static methods ----------

        Private Shared Function ReadArrayElement(
                context As OpcReadVariableValueContext,
                value As OpcVariableValue(Of Object)) As OpcVariableValue(Of Object)
            Dim elementNode = TryCast(context.Node, OpcVariableNode)
            Dim index As Integer

            If elementNode IsNot Nothing AndAlso Integer.TryParse(elementNode.Name.Value, index) Then
                Dim arrayNode = TryCast(elementNode.Parent, OpcVariableNode)
                Dim arrayValue = TryCast(arrayNode?.Value, Array)

                If arrayValue IsNot Nothing Then
                    value = New OpcVariableValue(Of Object)(arrayValue.GetValue(index))
                End If
            End If

            Return value
        End Function

        Private Shared Function WriteArrayElement(
                context As OpcWriteVariableValueContext,
                value As OpcVariableValue(Of Object)) As OpcVariableValue(Of Object)
            Dim elementNode = TryCast(context.Node, OpcVariableNode)
            Dim index As Integer

            If elementNode IsNot Nothing AndAlso Integer.TryParse(elementNode.Name.Value, index) Then
                Dim arrayNode = TryCast(elementNode.Parent, OpcVariableNode)
                Dim arrayValue = TryCast(arrayNode?.Value, Array)

                If arrayValue IsNot Nothing Then
                    arrayValue.SetValue(value.Value, index)
                    arrayNode.UpdateChanges(context, OpcNodeChanges.Value)
                End If
            End If

            Return value
        End Function

        '---------- Private methods ----------

        Private Sub InitializeNode(node As OpcVariableNode)
            Dim context = Me.SystemContext

            Dim typeId = node.DataTypeId
            Dim type = context.GetDataType(typeId)

            If type IsNot Nothing Then
                Dim value = Activator.CreateInstance(type.UnderlyingType)

                For Each child In node.Children(context).OfType(Of OpcVariableNode)()
                    Dim index As Integer

                    If child.DataTypeId = typeId AndAlso Integer.TryParse(child.Name.Value, index) Then
                        child.Value = value

                        child.ReadVariableValueCallback = AddressOf ReadArrayElement
                        child.WriteVariableValueCallback = AddressOf WriteArrayElement

                        child.AccessLevel = child.AccessLevel Or OpcAccessLevel.CurrentReadOrWrite
                        child.UserAccessLevel = child.UserAccessLevel Or OpcAccessLevel.CurrentReadOrWrite
                    End If
                Next

                node.AccessLevel = node.AccessLevel Or OpcAccessLevel.CurrentReadOrWrite
                node.UserAccessLevel = node.UserAccessLevel Or OpcAccessLevel.CurrentReadOrWrite

                node.Value = node.ArrayDimensions.CreateArray(type.UnderlyingType)
                node.ApplyChanges(context, recursive:=True)
            End If
        End Sub
    End Class
End Namespace
