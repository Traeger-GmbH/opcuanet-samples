// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace UserIdentities
{
    using Opc.UaFx;

    public class OpcSubSystemImpersonationContext : OpcImpersonationContext
    {
        #region ---------- Private readonly fields ----------

        private readonly int userId;

        #endregion

        #region ---------- Internal constructors ----------

        internal OpcSubSystemImpersonationContext(int userId)
            : base()
        {
            this.userId = userId;
            // TODO: Implement logic to "log-on" user.
        }

        #endregion

        #region ---------- Public methods ----------

        public override void Undo()
        {
            // TODO: Implement logic to "log-out" user.
        }

        #endregion

        #region ---------- Protected methods ----------

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                // TODO: Release any resources used.
            }
        }

        #endregion
    }
}
