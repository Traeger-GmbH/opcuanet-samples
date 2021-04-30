// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace Client
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using Opc.UaFx;
    using Opc.UaFx.Client;

    public class Program
    {
        public static void Main(string[] args)
        {
            using (var client = new OpcClient("opc.tcp://192.168.0.83:4840")) {
                client.UseDynamic = true;
                client.Connect();

                var watch = Stopwatch.StartNew();
                var value = client.ReadNode("ns=3;s=\"EdgeTest\".\"fromEdge _1\"");
                Console.WriteLine(watch.ElapsedMilliseconds);

                watch.Restart();
                value = client.ReadNode("ns=3;s=\"EdgeTest\".\"fromEdge _1\"");
                Console.WriteLine(watch.ElapsedMilliseconds);
                Console.ReadLine();

                watch.Restart();
                value = client.ReadNode("ns=3;s=\"EdgeTest\".\"fromEdge _1\"");
                Console.WriteLine(watch.ElapsedMilliseconds);
                Console.ReadLine();

                //var browseNode = new OpcBrowseNode("ns=3;s=\"AlleDatenTypen\"", OpcBrowseNodeDegree.Generation);
                //browseNode.Options = OpcBrowseOptions.IncludeCategory;

                //var nodeIds = BrowseNodeIds(client.BrowseNode(browseNode)).ToArray();
            }
        }
    }
}
