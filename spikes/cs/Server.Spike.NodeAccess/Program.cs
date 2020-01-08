// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace NodeAccess
{
    using System;
    using Opc.UaFx.Server;

    /// <summary>
    /// This use case realizes restricted node access using built-in ACL functionality and
    /// subclassing for user dependent node metadata.
    /// </summary>
    public class Program
    {
        #region ---------- Public static methods ----------

        public static void Main(string[] args)
        {
            var manager = new NodeManager();

            using (var server = new OpcServer("opc.tcp://localhost:4840/", manager)) {
                var users = server.Security.UserNameAcl;

                // 1. Add the users to the UserName-ACL.
                var admin = users.AddEntry("Admin", "admin");
                var user = users.AddEntry("User", "user");

                // 2. Setup the user privileges accordingly.
                user.Deny(OpcRequestType.Write);

                // 3. Activate the UserName-ACL (this inline disables anonymous access).
                users.IsEnabled = true;

                // 4. Publish ACL to node manager.
                manager.AccessControl = users;

                server.Start();
                Console.ReadKey(true);
            }
        }

        #endregion
    }
}
