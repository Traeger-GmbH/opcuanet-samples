// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace NodeSet
{
    using System;

    using Opc.UaFx;
    using Opc.UaFx.Server;

    /// <summary>
    /// This use case demonstrates how to import and implement an alarm from a nodeset using the
    /// companion specifications from UMATI: In this case 'MachineTool'.
    /// </summary>
    public class Program
    {
        #region ---------- Public static methods ----------

        public static void Main(string[] args)
        {
            // ATTENTION: Modified NodeSets!
            // 
            // The currently used OPC UA Stack does not yet support the new OPC UA v1.04.10
            // related BaseInterfaceType ('i=17602' used in DI and IA) and the OrderedListType
            // ('i=23518' used in IA and MachineTool). To support/import the node sets anyways we've
            // modified the node sets to use the BaseObjectType instead. However this is only a
            // temporary workaround to getting started with the node sets; especially if the
            // BaseInterfaceType and the OrderedListType are not (yet) required in your custom
            // implementation.
            // 
            // Stay tuned for later versions of the SDK to support these node types as well.
            var umatiNodeManager = OpcNodeSetManager.Create(
                    OpcNodeSet.Load(@".\NodeSets\Opc.Ua.IA.NodeSet2.xml"),
                    OpcNodeSet.Load(@".\NodeSets\Opc.Ua.Di.NodeSet2.xml"),
                    OpcNodeSet.Load(@".\NodeSets\Opc.Ua.Machinery.NodeSet2.xml"),
                    OpcNodeSet.Load(@".\NodeSets\Opc.Ua.MachineTool.NodeSet2.xml"));

            var exampleNodeManager = new NodeManager();

            using (var server = new OpcServer("opc.tcp://localhost:4840/", umatiNodeManager, exampleNodeManager)) {
                server.Start();

                Console.WriteLine("Server started.");
                Console.ReadKey(true);
            }
        }

        #endregion
    }
}
