using System.ComponentModel;

using Opc.Ua;
using Opc.UaFx;
using Opc.UaFx.Server;

namespace UserIdentities
{
    internal class OpcWindowsAccessControlList : OpcAccessControlList
    {
        #region ---------- Public constructors ----------

        public OpcWindowsAccessControlList(OpcServerBase owner)
            : base(owner, OpcAccessControlMode.Whitelist, OpcAccessControlMode.Whitelist)
        {
        }

        #endregion

        #region ---------- Protected properties ----------

        protected override OpcAccessControlTokenType TokenType
        {
            // We use the UserName token type.
            get => OpcAccessControlTokenType.UserName;
        }

        #endregion

        #region ---------- Public methods ----------

        public override OpcPrincipal Authenticate(OpcIdentityToken identityToken)
        {
            if (identityToken is OpcUserNameIdentityToken userNameIdentityToken) {
                // Use Windows Authentication for the user.
                string userName = userNameIdentityToken.UserName;
                string domainName = string.Empty;
                string password = userNameIdentityToken.DecryptedPassword;

                if (!string.IsNullOrEmpty(userName)) {
                    int index = userName.IndexOf('\\');

                    if (index != -1) {
                        domainName = userName.Substring(0, index);
                        userName = userName.Substring(index + 1);
                    }
                }

                // In case the Logon fails it will throw an exception.
                try {
                    var windowsIdentity = Advapi32.Logon(
                            domainName,
                            userName,
                            password,
                            LogonType.Network,
                            LogonProvider.Default);

                    // Note: We currently don't explicitly dispose the WindowsIdentity because
                    // we don't know when the server is finished with using the identity, so we
                    // rely on the finalizer to release the handle when the identity is no longer
                    // in use.
                    return new OpcPrincipal(new OpcWindowsIdentity(userName, windowsIdentity));
                }
                catch (Win32Exception) {
                    // Authentication failed.
                    throw new OpcException(
                            this.Owner.SystemContext,
                            StatusCodes.BadIdentityTokenRejected);
                }
            }

            return null;
        }

        public override bool IsOperationAllowed(OpcPrincipal principal, OpcOperationType operation)
        {
            return !this.IsOperationDenied(principal, operation);
        }

        public override bool IsOperationDenied(OpcPrincipal principal, OpcOperationType operation)
        {
            // Don't deny operations. We need to override this as otherwise the base class
            // method would create and add new ACL entries to its internal list every time
            // a user logs on.
            return false;
        }

        public override bool IsEndpointDisabled(OpcPrincipal principal, OpcEndpointIdentity endpoint)
        {
            // Allow all endpoints, see comments above.
            return false;
        }

        public override bool IsEndpointEnabled(OpcPrincipal principal, OpcEndpointIdentity endpoint)
        {
            return !this.IsEndpointDisabled(principal, endpoint);
        }

        #endregion
    }
}
