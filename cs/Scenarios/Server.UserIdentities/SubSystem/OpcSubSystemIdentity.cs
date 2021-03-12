// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace UserIdentities
{
    using Opc.UaFx;
    using Opc.UaFx.Server;

    public class OpcSubSystemIdentity : OpcServerIdentity
    {
        #region ---------- Private fields ----------

#pragma warning disable CS0649 // Field is never assigned.
        private int? userId;
#pragma warning restore CS0649  // Field is never assigned.

        #endregion

        #region ---------- Static constructor ----------

        static OpcSubSystemIdentity()
        {
            Generic = new OpcSubSystemIdentity();
        }

        #endregion

        #region ---------- Private constructors ----------

        private OpcSubSystemIdentity()
            : base("Generic-SubSystem-User")
        {
        }

        #endregion

        #region ---------- Public static properties ----------

        public static OpcSubSystemIdentity Generic
        {
            get;
        }

        #endregion

        #region ---------- Public methods ----------

        public override OpcImpersonationContext Impersonate()
        {
            if (this.userId == null)
                throw new OpcException(OpcStatusCode.BadSecurityChecksFailed);

            return new OpcSubSystemImpersonationContext(this.userId.Value);
        }

        public override bool Matches(string userName, string password)
        {
            // TODO: Implemented your custom subsystem logic here.
            // e.g. from the database of a sub-system
            // 
            // this.userId = select u.ID
            //                 from Users u
            //                where u.UserName = @userName
            //                  and u.PasswordHash = MD5.ComputeHash(password);
            // 
            return this.userId != null;
        }

        #endregion
    }
}
