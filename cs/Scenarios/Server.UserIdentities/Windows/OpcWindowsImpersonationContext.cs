// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace UserIdentities
{
    using System.Security.Principal;

    using Opc.UaFx;

    public class OpcWindowsImpersonationContext : OpcImpersonationContext
    {
        #region ---------- Private readonly fields ----------

        private readonly WindowsImpersonationContext impersonationContext;

        #endregion

        #region ---------- Internal constructors ----------

        internal OpcWindowsImpersonationContext(WindowsIdentity identity)
            : base()
        {
            this.impersonationContext = identity.Impersonate();
        }

        #endregion

        #region ---------- Public methods ----------

        public override void Undo()
        {
            this.impersonationContext.Undo();
        }

        #endregion

        #region ---------- Protected methods ----------

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                this.impersonationContext.Dispose();
            }
        }

        #endregion
    }
}
