// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace NodeAccess
{
    using System;
    using System.Collections.Generic;

    using Opc.UaFx;
    using Opc.UaFx.Server;

    public class SystemIdentity : OpcServerIdentity
    {
        #region ---------- Private fields ----------

        private List<string> allowedNodes;
        private List<string> deniedNodes;

        #endregion

        #region ---------- Public constructors ----------

        public SystemIdentity(string userName, string password)
            : base(userName, password)
        {
            this.Mode = OpcAccessControlMode.Whitelist;
        }

        public SystemIdentity(string userName, string password, OpcAccessControlMode mode)
            : this(userName, password)
        {
            this.Mode = mode;
        }

        #endregion

        #region ---------- Public properties ----------

        public OpcAccessControlMode Mode
        {
            get;
        }

        #endregion

        #region ---------- Public methods ----------

        public SystemIdentity Allow(string identifier)
        {
            if (this.allowedNodes == null)
                this.allowedNodes = new List<string>();

            this.Manage(this.allowedNodes, this.deniedNodes, identifier);
            return this;
        }

        public SystemIdentity Deny(string identifier)
        {
            if (this.deniedNodes == null)
                this.deniedNodes = new List<string>();

            this.Manage(this.deniedNodes, this.allowedNodes, identifier);
            return this;
        }

        public bool IsAllowed(string identifier)
        {
            var mode = this.Mode;

            if (mode == OpcAccessControlMode.Whitelist)
                return !this.deniedNodes?.Contains(identifier) ?? true;

            if (mode == OpcAccessControlMode.Blacklist)
                return this.allowedNodes?.Contains(identifier) ?? true;

            if (mode == OpcAccessControlMode.Explicit)
                return this.allowedNodes?.Contains(identifier) ?? false;

            return false;
        }

        public bool IsDenied(string identifier)
        {
            var mode = this.Mode;

            if (mode == OpcAccessControlMode.Whitelist)
                return this.deniedNodes?.Contains(identifier) ?? false;

            if (mode == OpcAccessControlMode.Blacklist)
                return !this.allowedNodes?.Contains(identifier) ?? true;

            if (mode == OpcAccessControlMode.Explicit)
                return this.deniedNodes?.Contains(identifier) ?? false;

            return false;
        }

        #endregion

        #region ---------- Private methods ----------

        private void Manage(
                List<string> privileges,
                List<string> counterPrivileges,
                string identifier)
        {
            if (!privileges.Contains(identifier)) {
                counterPrivileges?.Remove(identifier);
                privileges.Add(identifier);
            }
        }

        #endregion
    }
}
