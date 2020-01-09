' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports Opc.UaFx
Imports Opc.UaFx.Server

Namespace NodeAccess
    Partial Friend Class NodeManager
        Inherits OpcNodeManager

        ' ---------- Public constructors ----------

        Public Sub New()
            MyBase.New("http://sampleserver/nodeaccess")
        End Sub

        ' ---------- Public properties ----------

        Public Property AccessControl As OpcAccessControlList

        ' ---------- Internal methods ----------

        Friend Function CanWrite(ByVal sessionIdentity As OpcUserIdentity) As Boolean
            Dim entries = Me.AccessControl.Entries
            Dim serverIdentity As OpcUserIdentity = Nothing

            For Each entry In entries
                serverIdentity = TryCast(entry.Principal.Identity, OpcUserIdentity)

                If (serverIdentity IsNot Nothing) Then
                    If (serverIdentity.DisplayName = sessionIdentity.DisplayName) Then
                        Return entry.IsAllowed(OpcRequestType.Write)
                    End If
                End If
            Next

            Return False
        End Function

        ' ---------- Protected methods ----------

        Protected Overrides Function CreateNodes(ByVal references As OpcNodeReferenceCollection) As IEnumerable(Of IOpcNode)
            Dim folderNode = New OpcFolderNode(Me.DefaultNamespace.GetName("Folder"))

            Dim var01 = New VariableNode(Of Integer)(Me, folderNode, "Var01", value:=42)
            Dim var02 = New VariableNode(Of String)(Me, folderNode, "Var02", value:="Hello World")

            references.Add(folderNode, OpcObjectTypes.ObjectsFolder)
            Return New IOpcNode() {folderNode}
        End Function
    End Class
End Namespace
