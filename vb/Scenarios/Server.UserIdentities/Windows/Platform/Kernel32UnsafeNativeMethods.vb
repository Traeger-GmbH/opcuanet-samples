' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Runtime.InteropServices
Imports System.Security

Namespace UserIdentities
    <SuppressUnmanagedCodeSecurity>
    Friend Module Kernel32UnsafeNativeMethods
        '---------- Public static methods ----------

        <DllImport("kernel32.dll",
                EntryPoint:="CloseHandle",
                ExactSpelling:=True,
                CallingConvention:=CallingConvention.Winapi,
                SetLastError:=True)>
        Public Function CloseHandle(
                hObject As IntPtr) As Boolean
        End Function
    End Module
End Namespace
