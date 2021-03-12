// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace UserIdentities
{
    internal enum LogonProvider : int
    {
        Default = 0, // LOGON32_PROVIDER_DEFAULT
        WinNT35 = 1, // LOGON32_PROVIDER_WINNT35
        WinNT40 = 2, // LOGON32_PROVIDER_WINNT40
        WinNT50 = 3 // LOGON32_PROVIDER_WINNT50
    }
}
