' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Threading

Imports Opc.UaFx
Imports Opc.UaFx.Server

Namespace DynamicNodeManagers
    Partial Friend Class NodeManager
        Inherits OpcNodeManager

        '---------- Private readonly fields ----------

        Private ReadOnly rootNode As OpcFolderNode


        '---------- Private fields ----------

        Private [exit] As SemaphoreSlim
        Private thread As Thread


        '---------- Public constructors ----------

        Public Sub New(name As String)
            MyBase.New($"http://sampleserver/{name.ToLower()}")
            Me.Name = name
            Me.rootNode = New OpcFolderNode(Me.DefaultNamespace.GetName(name))
        End Sub


        '---------- Public properties ----------

        Public ReadOnly Property Name As String


        '---------- Public methods ----------

        Public Sub Start()
            SyncLock Me.SyncRoot
                If Me.thread Is Nothing Then
                    Me.exit = New SemaphoreSlim(0)
                    Me.thread = New Thread(AddressOf Me.UpdateVariables)

                    Me.thread.Start()
                End If
            End SyncLock
        End Sub

        Public Sub [Stop]()
            SyncLock Me.SyncRoot
                Me.exit?.Release()

                Me.thread?.Join()
                Me.thread = Nothing

                Me.exit?.Dispose()
                Me.exit = Nothing
            End SyncLock
        End Sub


        '---------- Protected methods ----------

        Protected Overrides Iterator Function CreateNodes(references As OpcNodeReferenceCollection) As IEnumerable(Of IOpcNode)
            For Each character In Me.Name
                Dim value = AscW(character)
                Dim variableNode = New OpcDataVariableNode(Of Integer)(Me.rootNode, name:=$"Var{value * 100}", value:=value)
            Next

            references.Add(Me.rootNode, OpcObjectTypes.ObjectsFolder)
            Yield Me.rootNode
        End Function

        Protected Overrides Sub Dispose(disposing As Boolean)
            If disposing Then
                Me.Stop()
            End If

            MyBase.Dispose(disposing)
        End Sub


        '---------- Private methods ----------

        Private Sub UpdateVariables()
            Dim name = Me.Name

            While Not Me.exit.Wait(name.Length * 250)
                Dim variableNodes = Me.rootNode _
                        .Children(Me.SystemContext) _
                        .OfType(Of OpcDataVariableNode(Of Integer))() _
                        .ToArray()

                For index = 0 To variableNodes.Length - 1
                    Dim multiplier = AscW(name(index))
                    variableNodes(index).Value = multiplier * DateTime.Now.Millisecond
                Next

                Me.rootNode.ApplyChanges(Me.SystemContext, recursive:=True)
            End While
        End Sub
    End Class
End Namespace
