// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace UserIdentities
{
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Security.Principal;

    using Opc.UaFx;

    public class OpcWindowsImpersonationContext : OpcImpersonationContext
    {
#if NETFRAMEWORK
        #region ---------- Private readonly fields ----------

        private readonly WindowsImpersonationContext impersonationContext;

        #endregion
#else
        #region ---------- Private fields ----------

        private bool isImpersonating;

        #endregion
#endif

        #region ---------- Internal constructors ----------

        internal OpcWindowsImpersonationContext(WindowsIdentity identity)
            : base()
        {
#if NETFRAMEWORK
            this.impersonationContext = identity.Impersonate();
#else
            // .NET no longer offers WindowsImpersonationContext, so the thread impersonates the
            // user directly and reverts to the process identity on Undo.
            if (!Advapi32UnsafeNativeMethods.ImpersonateLoggedOnUser(identity.AccessToken))
                throw new Win32Exception(Marshal.GetLastWin32Error());

            this.isImpersonating = true;
#endif
        }

        #endregion

        #region ---------- Public methods ----------

        public override void Undo()
        {
#if NETFRAMEWORK
            this.impersonationContext.Undo();
#else
            if (this.isImpersonating) {
                Advapi32UnsafeNativeMethods.RevertToSelf();
                this.isImpersonating = false;
            }
#endif
        }

        #endregion

        #region ---------- Protected methods ----------

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
#if NETFRAMEWORK
                this.impersonationContext.Dispose();
#else
                this.Undo();
#endif
            }
        }

        #endregion
    }
}
