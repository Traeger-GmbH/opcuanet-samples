' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports Opc.UaFx

Namespace UserIdentities
    Public Class OpcSubSystemImpersonationContext
        Inherits OpcImpersonationContext

        '---------- Private readonly fields ----------

        Private ReadOnly userId As Integer

        '---------- Internal constructors ----------

        Friend Sub New(userId As Integer)
            MyBase.New()

            Me.userId = userId
            ' TODO: Implement logic to "log-on" user.
        End Sub

        '---------- Public methods ----------

        Public Overrides Sub Undo()
            ' TODO: Implement logic to "log-out" user.
        End Sub

        '---------- Protected methods ----------

        Protected Overrides Sub Dispose(disposing As Boolean)
            If disposing Then
                ' TODO: Release any resources used.
            End If
        End Sub
    End Class
End Namespace
