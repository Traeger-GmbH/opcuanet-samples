// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace Server.Playground
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    using Opc.UaFx;
    using Opc.UaFx.Server;

    internal partial class NodeManager : OpcNodeManager
    {
        #region ---------- Private readonly fields ----------

        private readonly Random indexRandom;
        private readonly Random valueRandom;

        private readonly List<Action> tasks;

        #endregion

        #region ---------- Public constructors ----------

        public NodeManager()
            : base("http://sampleserver/")
        {
            this.indexRandom = new Random();
            this.valueRandom = new Random();

            this.tasks = new List<Action>();
        }

        #endregion

        #region ---------- Public methods ----------

        public void Simulate(SemaphoreSlim semaphore)
        {
            while (!semaphore.Wait(TimeSpan.FromSeconds(3))) {
                foreach (var task in this.tasks)
                    task.Invoke();
            }
        }

        #endregion

        #region ---------- Protected methods ----------

        protected override IEnumerable<IOpcNode> CreateNodes(OpcNodeReferenceCollection references)
        {
            var nodes = new OpcFolderNode(this.DefaultNamespace.GetName("Nodes"));
            references.Add(nodes, OpcObjectTypes.ObjectsFolder);

            yield return nodes;

            this.CreateStaticNodes(nodes);
            this.CreateDynamicNodes(nodes);
            this.CreateMethodNodes(nodes);

            yield return new OpcDataTypeNode<MyEnum>();
            yield return new OpcDataTypeNode<MyEnumFlags>();
        }

        #endregion

        #region ---------- Private methods ----------

        private void CreateDynamicNodes(OpcFolderNode nodes)
        {
            var dynamicNodes = new OpcFolderNode(nodes, "Dynamic");

            this.CreateNode<byte>(dynamicNodes, 1, (old, random) => unchecked((byte)(DateTime.Now.Second * random / (int)Math.Max((int)old, 1))));
            this.CreateArrayNode<byte>(dynamicNodes, 1, (old, random) => unchecked((byte)(DateTime.Now.Second * random / (int)Math.Max((int)old, 1))));

            this.CreateNode<sbyte>(dynamicNodes, 2, (old, random) => unchecked((sbyte)(DateTime.Now.Second * random / (int)Math.Max((int)old, 1))));
            this.CreateArrayNode<sbyte>(dynamicNodes, 2, (old, random) => unchecked((sbyte)(DateTime.Now.Second * random / (int)Math.Max((int)old, 1))));

            this.CreateNode<short>(dynamicNodes, 3, (old, random) => unchecked((short)(DateTime.Now.Second * random / (int)Math.Max((int)old, 1))));
            this.CreateArrayNode<short>(dynamicNodes, 3, (old, random) => unchecked((short)(DateTime.Now.Second * random / (int)Math.Max((int)old, 1))));

            this.CreateNode<ushort>(dynamicNodes, 4, (old, random) => unchecked((ushort)(DateTime.Now.Second * random / (int)Math.Max((int)old, 1))));
            this.CreateArrayNode<ushort>(dynamicNodes, 4, (old, random) => unchecked((ushort)(DateTime.Now.Second * random / (int)Math.Max((int)old, 1))));

            this.CreateNode<int>(dynamicNodes, 5, (old, random) => unchecked((int)(DateTime.Now.Second * random / (int)Math.Max((int)old, 1))));
            this.CreateArrayNode<int>(dynamicNodes, 5, (old, random) => unchecked((int)(DateTime.Now.Second * random / (int)Math.Max((int)old, 1))));

            this.CreateNode<uint>(dynamicNodes, 6u, (old, random) => unchecked((uint)(DateTime.Now.Second * random / (int)Math.Max((int)old, 1))));
            this.CreateArrayNode<uint>(dynamicNodes, 6u, (old, random) => unchecked((uint)(DateTime.Now.Second * random / (int)Math.Max((int)old, 1))));

            this.CreateNode<long>(dynamicNodes, 7L, (old, random) => unchecked((long)(DateTime.Now.Second * random / (int)Math.Max((int)old, 1))));
            this.CreateArrayNode<long>(dynamicNodes, 7L, (old, random) => unchecked((long)(DateTime.Now.Second * random / (int)Math.Max((int)old, 1))));

            this.CreateNode<ulong>(dynamicNodes, 8u, (old, random) => unchecked((ulong)(DateTime.Now.Second * random / (int)Math.Max((int)old, 1))));
            this.CreateArrayNode<ulong>(dynamicNodes, 8u, (old, random) => unchecked((ulong)(DateTime.Now.Second * random / (int)Math.Max((int)old, 1))));

            this.CreateNode<float>(dynamicNodes, 9.10f, (old, random) => unchecked((float)(DateTime.Now.Second * random) / (int)Math.Max((int)old, 1)));
            this.CreateArrayNode<float>(dynamicNodes, 9.10f, (old, random) => unchecked((float)(DateTime.Now.Second * random / (int)Math.Max((int)old, 1))));

            this.CreateNode<double>(dynamicNodes, 11.12, (old, random) => unchecked((double)(DateTime.Now.Second * random / (int)Math.Max((int)old, 1))));
            this.CreateArrayNode<double>(dynamicNodes, 11.12, (old, random) => unchecked((double)(DateTime.Now.Second * random / (int)Math.Max((int)old, 1))));

            var stringValue = this.RandomString();

            this.CreateNode<string>(dynamicNodes, stringValue, this.RandomString);
            this.CreateArrayNode<string>(dynamicNodes, stringValue, this.RandomString);

            this.CreateNode<MyEnum>(dynamicNodes, MyEnum.Starting, this.RandomEnum);
            //this.CreateArrayNode<MyEnum>(dynamicNodes, MyEnum.Maintenance, this.RandomEnum);

            this.CreateNode<MyEnumFlags>(dynamicNodes, MyEnumFlags.CanCut | MyEnumFlags.CanLaminate, this.RandomEnum);
            //this.CreateArrayNode<MyEnumFlags>(dynamicNodes, MyEnumFlags.CanFold | MyEnumFlags.CanPrint, this.RandomEnum);
        }

        private void CreateMethodNodes(OpcFolderNode nodes)
        {
            var methodNodes = new OpcFolderNode(nodes, "Methods");

            new OpcMethodNode(methodNodes, "Add", new Func<int, int, int>(this.Add));
            new OpcMethodNode(methodNodes, "Divide", new Func<int, int, int>(this.Divide));
            new OpcMethodNode(methodNodes, "Multiply", new Func<int, int, int>(this.Multiply));
            new OpcMethodNode(methodNodes, "Power", new Func<int, int, int>(this.Power));
            new OpcMethodNode(methodNodes, "Subtract", new Func<int, int, int>(this.Substract));
        }

        private void CreateStaticNodes(OpcFolderNode nodes)
        {
            var staticNodes = new OpcFolderNode(nodes, "Static");

            this.CreateNode<byte>(staticNodes, 1);
            this.CreateArrayNode<byte>(staticNodes, 1);

            this.CreateNode<sbyte>(staticNodes, 2);
            this.CreateArrayNode<sbyte>(staticNodes, 2);

            this.CreateNode<short>(staticNodes, 3);
            this.CreateArrayNode<short>(staticNodes, 3);

            this.CreateNode<ushort>(staticNodes, 4);
            this.CreateArrayNode<ushort>(staticNodes, 4);

            this.CreateNode<int>(staticNodes, 5);
            this.CreateArrayNode<int>(staticNodes, 5);

            this.CreateNode<uint>(staticNodes, 6u);
            this.CreateArrayNode<uint>(staticNodes, 6u);

            this.CreateNode<long>(staticNodes, 7L);
            this.CreateArrayNode<long>(staticNodes, 7L);

            this.CreateNode<ulong>(staticNodes, 8u);
            this.CreateArrayNode<ulong>(staticNodes, 8u);

            this.CreateNode<float>(staticNodes, 9.10f);
            this.CreateArrayNode<float>(staticNodes, 9.10f);

            this.CreateNode<double>(staticNodes, 11.12);
            this.CreateArrayNode<double>(staticNodes, 11.12);

            var stringValue = this.RandomString();

            this.CreateNode<string>(staticNodes, stringValue);
            this.CreateArrayNode<string>(staticNodes, stringValue);

            this.CreateNode<MyEnum>(staticNodes, MyEnum.Maintenance);
            //this.CreateArrayNode<MyEnum>(staticNodes, MyEnum.Starting);

            this.CreateNode<MyEnumFlags>(staticNodes, MyEnumFlags.CanCut | MyEnumFlags.CanFold);
            //this.CreateArrayNode<MyEnumFlags>(staticNodes, MyEnumFlags.CanPrint | MyEnumFlags.CanCorrugate);
        }

        #endregion
    }
}
