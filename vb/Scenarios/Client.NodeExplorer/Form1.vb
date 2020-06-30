' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports Opc.UaFx
Imports Opc.UaFx.Client

Public Partial Class Form1
    Private ReadOnly client As OpcClient

    Public Sub New()
        MyBase.New()
        Me.client = New OpcClient()
        Me.InitializeComponent()
    End Sub

    Protected Overrides Sub OnFormClosing(ByVal e As FormClosingEventArgs)
        Me.client.Disconnect()
        MyBase.OnFormClosing(e)
    End Sub

    Private Function Browse() As Boolean
        Dim result = False

        Try
            Dim node = Me.client.BrowseNode(OpcObjectTypes.RootFolder)
            result = Me.Browse(node)
        Catch ex As OpcException
            Me.ShowMessage("Browse", "Failed to browse: " & ex.Message)
        End Try

        Return result
    End Function

    Private Function Browse(ByVal node As OpcNodeInfo) As Boolean
        Me.nodesTreeView.Nodes.Clear()
        Return Me.Browse(node, Me.nodesTreeView.Nodes)
    End Function

    Private Function Browse(
            ByVal node As OpcNodeInfo,
            ByVal treeNodes As TreeNodeCollection) As Boolean
        Dim result = False

        Try
            Dim treeNode = treeNodes.Add(node.DisplayName.Value)

            If TypeOf node Is OpcObjectNodeInfo Then
                treeNode.ImageIndex = 0
                If node.Reference.TypeDefinitionId = Opc.Ua.ObjectTypeIds.FolderType Then
                    treeNode.ImageIndex = 1
                End If
            ElseIf TypeOf node Is OpcMethodNodeInfo Then
                treeNode.ImageIndex = 2
            ElseIf TypeOf node Is OpcVariableNodeInfo Then
                treeNode.ImageIndex = 3
                If node.Reference.ReferenceType = OpcReferenceType.HasProperty Then
                    treeNode.ImageIndex = 4
                End If
            End If

            treeNode.Tag = node
            treeNode.Nodes.Add("Browsing...")

            result = True
        Catch ex As OpcException
            Me.ShowMessage("Browse", "Failed to browse: " & ex.Message)
        End Try

        Return result
    End Function

    Private Function Connect() As Boolean
        Dim result = False

        Try
            Me.client.Connect()
            result = True
        Catch ex As OpcException
            Me.ShowMessage("Connect", "Failed to connect: " & ex.Message)
        End Try

        Return result
    End Function

    Private Sub HandleConnectButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles connectButton.Click
        Me.client.Disconnect()
        Dim serverAddress As Uri = Nothing

        If Uri.TryCreate(Me.serverAddressTextBox.Text, UriKind.Absolute, serverAddress) Then
            Me.client.ServerAddress = serverAddress
            If Me.Connect() Then Me.Browse()
        Else
            Me.ShowMessage("Connect", "Invalid server address.")
        End If
    End Sub

    Private Sub HandleNodesTreeViewAfterExpand(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles nodesTreeView.AfterExpand
        Dim node As OpcNodeInfo = TryCast(e.Node.Tag, OpcNodeInfo)

        If node IsNot Nothing Then
            Dim treeNodes = e.Node.Nodes
            treeNodes.Clear()

            For Each childNode In node.Children()
                If Not Me.Browse(childNode, treeNodes) Then Exit For
            Next
        End If
    End Sub

    Private Sub ShowMessage(ByVal caption As String, ByVal text As String)
        MessageBox.Show(owner:=Me, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Class
