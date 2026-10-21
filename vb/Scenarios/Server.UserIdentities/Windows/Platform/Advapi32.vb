' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports System.Security.Principal

Namespace UserIdentities
    Friend Module Advapi32
        '---------- Public static methods ----------

        Public Function Logon(
                domain As String,
                userName As String,
                password As String,
                logonType As LogonType,
                logonProvider As LogonProvider) As WindowsIdentity
            Dim userToken As IntPtr = IntPtr.Zero
            Dim result As Boolean = Advapi32UnsafeNativeMethods.LogonUser(
                    userName, domain, password, CInt(logonType), CInt(logonProvider), userToken)

            If Not result Then
                Throw New Win32Exception(Marshal.GetLastWin32Error())
            End If

            Try
                Return New WindowsIdentity(userToken)
            Finally
                ' We always need to release the token ourselves, because WindowsIdentity will
                ' have duplicated it.
                Kernel32UnsafeNativeMethods.CloseHandle(userToken)
            End Try
        End Function
    End Module
End Namespace
