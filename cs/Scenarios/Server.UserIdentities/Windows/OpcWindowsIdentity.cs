// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace UserIdentities
{
    using System.Security.Principal;

    using Opc.UaFx;
    using Opc.UaFx.Server;

    public class OpcWindowsIdentity : OpcServerIdentity
    {
        #region ---------- Private fields ----------

        private readonly WindowsIdentity identity;

        #endregion

        #region ---------- Public constructors ----------

        public OpcWindowsIdentity(string userName, WindowsIdentity identity)
            : base(userName)
        {
            this.identity = identity;
        }

        #endregion

        #region ---------- Public methods ----------

        public override OpcImpersonationContext Impersonate()
        {
            if (this.identity == null)
                throw new OpcException(OpcStatusCode.BadSecurityChecksFailed);

            return new OpcWindowsImpersonationContext(this.identity);
        }

        #endregion
    }
}
