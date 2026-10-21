' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System.Security.Principal

Imports Opc.UaFx
Imports Opc.UaFx.Server

Namespace UserIdentities
    Public Class OpcWindowsIdentity
        Inherits OpcServerIdentity

        '---------- Private fields ----------

        Private ReadOnly identity As WindowsIdentity

        '---------- Public constructors ----------

        Public Sub New(userName As String, identity As WindowsIdentity)
            MyBase.New(userName)

            Me.identity = identity
        End Sub

        '---------- Public methods ----------

        Public Overrides Function Impersonate() As OpcImpersonationContext
            If Me.identity Is Nothing Then
                Throw New OpcException(OpcStatusCode.BadSecurityChecksFailed)
            End If

            Return New OpcWindowsImpersonationContext(Me.identity)
        End Function
    End Class
End Namespace
