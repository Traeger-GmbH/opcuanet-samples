// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace UserIdentities
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    using Microsoft.Win32.SafeHandles;

    [SuppressUnmanagedCodeSecurity]
    internal static class Advapi32UnsafeNativeMethods
    {
        #region ---------- Public static methods ----------

        [DllImport("advapi32.dll",
                EntryPoint = "LogonUserW",
                ExactSpelling = true,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                SetLastError = true)]
        public static extern bool LogonUser(
                string lpszUsername,
                string lpszDomain,
                string lpszPassword,
                int dwLogonType,
                int dwLogonProvider,
                out IntPtr phToken);

        [DllImport("advapi32.dll",
                EntryPoint = "ImpersonateLoggedOnUser",
                ExactSpelling = true,
                CallingConvention = CallingConvention.Winapi,
                SetLastError = true)]
        public static extern bool ImpersonateLoggedOnUser(
                SafeAccessTokenHandle hToken);

        [DllImport("advapi32.dll",
                EntryPoint = "RevertToSelf",
                ExactSpelling = true,
                CallingConvention = CallingConvention.Winapi,
                SetLastError = true)]
        public static extern bool RevertToSelf();

        #endregion
    }
}
