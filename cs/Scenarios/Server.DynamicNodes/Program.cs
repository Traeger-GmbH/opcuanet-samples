// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace DynamicNodes
{
    using System;

    using Opc.UaFx;
    using Opc.UaFx.Server;

    /// <summary>
    /// This use case realizes dynamic nodes which are created on-demand by the server depending
    /// on the underlying system needs.
    /// </summary>
    public class Program
    {
        #region ---------- Public static methods ----------

        public static void Main(string[] args)
        {
            var manager = new NodeManager();

            using (var server = new OpcServer("opc.tcp://localhost:4840/", manager)) {
                server.Start();

                Console.WriteLine("Press enter to exit or ...");
                Console.WriteLine("... enter a node path like '/a/b/c' to create a folder node.");
                Console.WriteLine("... enter a path like '/a/b/.d' to create a numeric variable node.");
                Console.WriteLine("... begin the path with '$' instead of a '/' to create a new root node.");
                Console.WriteLine();

                while (true) {
                    var path = Console.ReadLine().Trim().Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

                    if (path.Length == 0)
                        break;

                    var node = default(IOpcNode);

                    if (path[0][0] == '$')
                        node = manager.AddRootNode(path);
                    else
                        node = manager.AddNode(path);

                    Console.WriteLine("Created: '{0}'", node.Id.ToString(OpcNodeIdFormat.Foundation));
                }
            }
        }
    }

    #endregion
}
