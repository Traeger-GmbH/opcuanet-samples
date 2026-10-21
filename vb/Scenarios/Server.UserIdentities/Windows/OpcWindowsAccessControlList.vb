Imports System.ComponentModel

Imports Opc.Ua
Imports Opc.UaFx
Imports Opc.UaFx.Server

Namespace UserIdentities
    Friend Class OpcWindowsAccessControlList
        Inherits OpcAccessControlList

        '---------- Public constructors ----------

        Public Sub New(owner As OpcServerBase)
            MyBase.New(owner, OpcAccessControlMode.Whitelist, OpcAccessControlMode.Whitelist)
        End Sub

        '---------- Protected properties ----------

        Protected Overrides ReadOnly Property TokenType As OpcAccessControlTokenType
            ' We use the UserName token type.
            Get
                Return OpcAccessControlTokenType.UserName
            End Get
        End Property

        '---------- Public methods ----------

        Public Overrides Function Authenticate(identityToken As OpcIdentityToken) As OpcPrincipal
            Dim userNameIdentityToken = TryCast(identityToken, OpcUserNameIdentityToken)

            If userNameIdentityToken IsNot Nothing Then
                ' Use Windows Authentication for the user.
                Dim userName As String = userNameIdentityToken.UserName
                Dim domainName As String = String.Empty
                Dim password As String = userNameIdentityToken.DecryptedPassword

                If Not String.IsNullOrEmpty(userName) Then
                    Dim index As Integer = userName.IndexOf("\"c)

                    If index <> -1 Then
                        domainName = userName.Substring(0, index)
                        userName = userName.Substring(index + 1)
                    End If
                End If

                ' In case the Logon fails it will throw an exception.
                Try
                    Dim windowsIdentity = Advapi32.Logon(
                            domainName,
                            userName,
                            password,
                            LogonType.Network,
                            LogonProvider.Default)

                    ' Note: We currently don't explicitly dispose the WindowsIdentity because
                    ' we don't know when the server is finished with using the identity, so we
                    ' rely on the finalizer to release the handle when the identity is no longer
                    ' in use.
                    Return New OpcPrincipal(New OpcWindowsIdentity(userName, windowsIdentity))
                Catch ex As Win32Exception
                    ' Authentication failed.
                    Throw New OpcException(
                            Me.Owner.SystemContext,
                            StatusCodes.BadIdentityTokenRejected)
                End Try
            End If

            Return Nothing
        End Function

        Public Overrides Function IsOperationAllowed(principal As OpcPrincipal, operation As OpcOperationType) As Boolean
            Return Not Me.IsOperationDenied(principal, operation)
        End Function

        Public Overrides Function IsOperationDenied(principal As OpcPrincipal, operation As OpcOperationType) As Boolean
            ' Don't deny operations. We need to override this as otherwise the base class
            ' method would create and add new ACL entries to its internal list every time
            ' a user logs on.
            Return False
        End Function

        Public Overrides Function IsEndpointDisabled(principal As OpcPrincipal, endpoint As OpcEndpointIdentity) As Boolean
            ' Allow all endpoints, see comments above.
            Return False
        End Function

        Public Overrides Function IsEndpointEnabled(principal As OpcPrincipal, endpoint As OpcEndpointIdentity) As Boolean
            Return Not Me.IsEndpointDisabled(principal, endpoint)
        End Function
    End Class
End Namespace
