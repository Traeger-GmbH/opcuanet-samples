' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports Opc.UaFx

Namespace NodeAccess
    Partial Friend Class NodeManager
        Private Class VariableNode(Of T)
            Inherits OpcDataVariableNode(Of T)

            ' ---------- Private readonly fields ----------

            Private ReadOnly manager As NodeManager

            ' ---------- Public constructors ----------

            Public Sub New(ByVal manager As NodeManager, ByVal parent As IOpcNode, ByVal name As OpcName, ByVal value As T)
                MyBase.New(parent, name, value)
                Me.manager = manager
            End Sub

            ' ---------- Protected methods ----------

            Protected Overrides Function ReadAttributeValueCore(Of TAttribute)(
                    ByVal context As OpcReadAttributeValueContext,
                    ByVal value As OpcAttributeValue(Of TAttribute)) As OpcAttributeValue(Of TAttribute)
                Dim attribute = value.Attribute

                Dim accessLevel As OpcAttributeValue(Of OpcAccessLevel) = Nothing
                Dim writeAccess As OpcAttributeValue(Of OpcAttributeWriteAccess) = Nothing

                If (attribute = OpcAttribute.UserAccessLevel OrElse attribute = OpcAttribute.UserWriteAccess) Then
                    If (Not Me.manager.CanWrite(context.Identity)) Then
                        accessLevel = TryCast(CObj(value), OpcAttributeValue(Of OpcAccessLevel))

                        If (accessLevel IsNot Nothing) Then
                            accessLevel = New OpcAttributeValue(Of OpcAccessLevel)(
                                    value.Attribute,
                                    accessLevel.Value And Not OpcAccessLevel.CurrentWrite)

                            Return CType(CObj(accessLevel), OpcAttributeValue(Of TAttribute))
                        End If

                        writeAccess = TryCast(CObj(value), OpcAttributeValue(Of OpcAttributeWriteAccess))

                        If (writeAccess IsNot Nothing) Then
                            writeAccess = New OpcAttributeValue(Of OpcAttributeWriteAccess)(
                                    value.Attribute,
                                    OpcAttributeWriteAccess.None)

                            Return CType(CObj(writeAccess), OpcAttributeValue(Of TAttribute))
                        End If
                    End If
                End If

                Return MyBase.ReadAttributeValueCore(context, value)
            End Function
        End Class
    End Class
End Namespace
