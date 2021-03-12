// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace UserIdentities
{
    internal enum LogonType : int
    {
        Interactive = 2, // LOGON32_LOGON_INTERACTIVE
        Network = 3, // LOGON32_LOGON_NETWORK
        Batch = 4, // LOGON32_LOGON_BATCH
        Service = 5, // LOGON32_LOGON_SERVICE
        Unlock = 7, // LOGON32_LOGON_UNLOCK
        NetworkCleartext = 8, // LOGON32_LOGON_NETWORK_CLEARTEXT
        NewCredentials = 9, // LOGON32_LOGON_NEW_CREDENTIALS
    }
}
