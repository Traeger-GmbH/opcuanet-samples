// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace UserIdentities
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    [SuppressUnmanagedCodeSecurity]
    internal static class Kernel32UnsafeNativeMethods
    {
        #region ---------- Public static methods ----------

        [DllImport("kernel32.dll",
                EntryPoint = "CloseHandle",
                ExactSpelling = true,
                CallingConvention = CallingConvention.Winapi,
                SetLastError = true)]
        public static extern bool CloseHandle(
                IntPtr hObject);

        #endregion
    }
}
