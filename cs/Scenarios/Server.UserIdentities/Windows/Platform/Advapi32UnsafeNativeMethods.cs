// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace UserIdentities
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    [SuppressUnmanagedCodeSecurity]
    internal static class Advapi32UnsafeNativeMethods
    {
        #region ---------- Public static methods ----------

        [DllImport("advapi32.dll",
                EntryPoint = "LogonUser",
                CallingConvention = CallingConvention.Winapi,
                SetLastError = true)]
        public static extern bool LogonUser(
                string lpszUsername,
                string lpszDomain,
                string lpszPassword,
                int dwLogonType,
                int dwLogonProvider,
                out IntPtr phToken);

        #endregion
    }
}
