' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports Opc.Ua
Imports Opc.UaFx
Imports Opc.UaFx.Server

Namespace DynamicNodes
    Partial Friend Class NodeManager
        Inherits OpcNodeManager

        '---------- Private readonly fields ----------

        Private ReadOnly rootNode As OpcFolderNode

        '---------- Public constructors ----------

        Public Sub New()
            MyBase.New("http://sampleserver/dynamicnodes")

            Me.rootNode = New OpcFolderNode(Me.DefaultNamespace.GetName("Root"))
        End Sub

        '---------- Internal methods ----------

        Friend Overloads Function AddNode(ParamArray path As String()) As IOpcNode
            Return Me.AddNode(Me.rootNode, path)
        End Function

        Friend Overloads Function AddNode(parentNode As IOpcNode, ParamArray path As String()) As IOpcNode
            Dim context = Me.SystemContext

            For Each name In path
                Dim childNode = DirectCast(parentNode.Child(context, name), OpcInstanceNode)

                If childNode Is Nothing Then
                    If name(0) = "."c Then
                        childNode = New OpcDataVariableNode(Of Integer)(parentNode, name, DateTime.Now.Millisecond)
                    Else
                        childNode = New OpcFolderNode(parentNode, name)
                    End If

                    Me.AddNode(context, childNode)

                    If TypeOf childNode Is OpcVariableNode Then
                        Return childNode
                    End If
                End If

                parentNode = childNode
            Next

            Return parentNode
        End Function

        Friend Function AddRootNode(ParamArray path As String()) As IOpcNode
            Dim newNode As New OpcFolderNode(path(0).TrimStart("$"c))

            Dim reference = OpcNodeReference.To(OpcObjectTypes.ObjectsFolder)
            Me.AddNode(Me.SystemContext, newNode, reference)

            If path.Length > 1 Then
                Return Me.AddNode(newNode, path.Skip(1).ToArray())
            End If

            Return newNode
        End Function

        '---------- Protected methods ----------

        Protected Overrides Function CreateNodes(references As OpcNodeReferenceCollection) As IEnumerable(Of IOpcNode)
            references.Add(Me.rootNode, OpcObjectTypes.ObjectsFolder)
            Return New IOpcNode() {Me.rootNode}
        End Function
    End Class
End Namespace
