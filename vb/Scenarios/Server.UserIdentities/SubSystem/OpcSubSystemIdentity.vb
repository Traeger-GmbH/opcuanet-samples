' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports Opc.UaFx
Imports Opc.UaFx.Server

Namespace UserIdentities
    Public Class OpcSubSystemIdentity
        Inherits OpcServerIdentity

        '---------- Private fields ----------

        Private userId As Integer?

        '---------- Static constructor ----------

        Shared Sub New()
            Generic = New OpcSubSystemIdentity()
        End Sub

        '---------- Private constructors ----------

        Private Sub New()
            MyBase.New("Generic-SubSystem-User")
        End Sub

        '---------- Public static properties ----------

        Public Shared ReadOnly Property Generic As OpcSubSystemIdentity

        '---------- Public methods ----------

        Public Overrides Function Impersonate() As OpcImpersonationContext
            If Me.userId Is Nothing Then
                Throw New OpcException(OpcStatusCode.BadSecurityChecksFailed)
            End If

            Return New OpcSubSystemImpersonationContext(Me.userId.Value)
        End Function

        Public Overrides Function Matches(userName As String, password As String) As Boolean
            ' TODO: Implemented your custom subsystem logic here.
            ' e.g. from the database of a sub-system
            '
            ' Me.userId = select u.ID
            '               from Users u
            '              where u.UserName = @userName
            '                and u.PasswordHash = MD5.ComputeHash(password);
            '
            Return Me.userId IsNot Nothing
        End Function
    End Class
End Namespace
