' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Runtime.InteropServices
Imports System.Security

Imports Microsoft.Win32.SafeHandles

Namespace UserIdentities
    <SuppressUnmanagedCodeSecurity>
    Friend Module Advapi32UnsafeNativeMethods
        '---------- Public static methods ----------

        <DllImport("advapi32.dll",
                EntryPoint:="LogonUserW",
                ExactSpelling:=True,
                CallingConvention:=CallingConvention.Winapi,
                CharSet:=CharSet.Unicode,
                SetLastError:=True)>
        Public Function LogonUser(
                lpszUsername As String,
                lpszDomain As String,
                lpszPassword As String,
                dwLogonType As Integer,
                dwLogonProvider As Integer,
                <Out> ByRef phToken As IntPtr) As Boolean
        End Function

        <DllImport("advapi32.dll",
                EntryPoint:="ImpersonateLoggedOnUser",
                ExactSpelling:=True,
                CallingConvention:=CallingConvention.Winapi,
                SetLastError:=True)>
        Public Function ImpersonateLoggedOnUser(
                hToken As SafeAccessTokenHandle) As Boolean
        End Function

        <DllImport("advapi32.dll",
                EntryPoint:="RevertToSelf",
                ExactSpelling:=True,
                CallingConvention:=CallingConvention.Winapi,
                SetLastError:=True)>
        Public Function RevertToSelf() As Boolean
        End Function
    End Module
End Namespace
