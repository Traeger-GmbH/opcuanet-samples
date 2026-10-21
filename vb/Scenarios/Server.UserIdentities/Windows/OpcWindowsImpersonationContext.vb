' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports System.Security.Principal

Imports Opc.UaFx

Namespace UserIdentities
    Public Class OpcWindowsImpersonationContext
        Inherits OpcImpersonationContext

#If NETFRAMEWORK Then
        '---------- Private readonly fields ----------

        Private ReadOnly impersonationContext As WindowsImpersonationContext
#Else
        '---------- Private fields ----------

        Private isImpersonating As Boolean
#End If

        '---------- Internal constructors ----------

        Friend Sub New(identity As WindowsIdentity)
            MyBase.New()

#If NETFRAMEWORK Then
            Me.impersonationContext = identity.Impersonate()
#Else
            ' .NET no longer offers WindowsImpersonationContext, so the thread impersonates the
            ' user directly and reverts to the process identity on Undo.
            If Not Advapi32UnsafeNativeMethods.ImpersonateLoggedOnUser(identity.AccessToken) Then
                Throw New Win32Exception(Marshal.GetLastWin32Error())
            End If

            Me.isImpersonating = True
#End If
        End Sub

        '---------- Public methods ----------

        Public Overrides Sub Undo()
#If NETFRAMEWORK Then
            Me.impersonationContext.Undo()
#Else
            If Me.isImpersonating Then
                Advapi32UnsafeNativeMethods.RevertToSelf()
                Me.isImpersonating = False
            End If
#End If
        End Sub

        '---------- Protected methods ----------

        Protected Overrides Sub Dispose(disposing As Boolean)
            If disposing Then
#If NETFRAMEWORK Then
                Me.impersonationContext.Dispose()
#Else
                Me.Undo()
#End If
            End If
        End Sub
    End Class
End Namespace
