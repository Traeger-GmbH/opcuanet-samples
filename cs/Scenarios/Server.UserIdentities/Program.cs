// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace UserIdentities
{
    using System;

    using Opc.UaFx;
    using Opc.UaFx.Server;

    /// <summary>
    /// This use case demonstrates how generic user identities could be implemented using built-in
    /// ACL functionality and how subclassing can be used to define the generic user identities.
    /// </summary>
    public class Program
    {
        #region ---------- Public static methods ----------

        public static void Main(string[] args)
        {
            var someNode = new OpcDataVariableNode<string>("Hello", value: "Hello World!");

            using (var server = new OpcServer("opc.tcp://localhost:4840/", someNode)) {
                var users = server.Security.UserNameAcl;

                // 1. Add the generic users to the UserName-ACL.
                users.AddEntry(OpcWindowsIdentity.Generic);
                users.AddEntry(OpcSubSystemIdentity.Generic);

                // 2. Activate the UserName-ACL (this inline disables anonymous access).
                users.IsEnabled = true;

                server.Start();

                Console.WriteLine("Server started - press any key to exit.");
                Console.ReadKey(true);
            }
        }

        #endregion
    }
}
