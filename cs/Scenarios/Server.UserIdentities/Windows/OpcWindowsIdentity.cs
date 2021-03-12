// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace UserIdentities
{
    using System.Security.Principal;

    using Opc.UaFx;
    using Opc.UaFx.Server;

    public class OpcWindowsIdentity : OpcServerIdentity
    {
        #region ---------- Private fields ----------

        private WindowsIdentity identity;

        #endregion

        #region ---------- Static constructor ----------

        static OpcWindowsIdentity()
        {
            Generic = new OpcWindowsIdentity();
        }

        #endregion

        #region ---------- Private constructors ----------

        private OpcWindowsIdentity()
            : base("Generic-Windows-User")
        {
        }

        #endregion

        #region ---------- Public static properties ----------

        public static OpcWindowsIdentity Generic
        {
            get;
        }

        #endregion

        #region ---------- Public methods ----------

        public override OpcImpersonationContext Impersonate()
        {
            if (this.identity == null)
                throw new OpcException(OpcStatusCode.BadSecurityChecksFailed);

            return new OpcWindowsImpersonationContext(this.identity);
        }

        public override bool Matches(string userName, string password)
        {
            var domainName = string.Empty;

            if (!string.IsNullOrEmpty(userName)) {
                int index = userName.IndexOf('\\');

                if (index != -1) {
                    domainName = userName.Substring(0, index);
                    userName = userName.Substring(index + 1);
                }
            }

            // In case the Logon fails it will throw an exception.
            this.identity = Advapi32.Logon(
                    domainName,
                    userName,
                    password,
                    LogonType.Network,
                    LogonProvider.Default);

            return true;
        }

        #endregion
    }
}
