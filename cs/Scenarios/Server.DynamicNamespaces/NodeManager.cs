// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace DynamicNodeManagers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    using Opc.UaFx;
    using Opc.UaFx.Server;

    internal partial class NodeManager : OpcNodeManager
    {
        #region ---------- Private readonly fields ----------

        private readonly OpcFolderNode rootNode;

        #endregion

        #region ---------- Private fields ----------

        private SemaphoreSlim exit;
        private Thread thread;

        #endregion

        #region ---------- Public constructors ----------

        public NodeManager(string name)
            : base($"http://sampleserver/{name.ToLower()}")
        {
            this.Name = name;
            this.rootNode = new OpcFolderNode(this.DefaultNamespace.GetName(name));
        }

        #endregion

        #region ---------- Public properties ----------

        public string Name
        {
            get;
        }

        #endregion

        #region ---------- Public methods ----------

        public void Start()
        {
            lock (this.SyncRoot) {
                if (this.thread == null) {
                    this.exit = new SemaphoreSlim(0);
                    this.thread = new Thread(this.UpdateVariables);

                    this.thread.Start();
                }
            }
        }

        public void Stop()
        {
            lock (this.SyncRoot) {
                this.exit?.Release();

                this.thread?.Join();
                this.thread = null;

                this.exit?.Dispose();
                this.exit = null;
            }
        }

        #endregion

        #region ---------- Protected methods ----------

        protected override IEnumerable<IOpcNode> CreateNodes(OpcNodeReferenceCollection references)
        {
            foreach (var character in this.Name) {
                var value = (int)character;
                new OpcDataVariableNode<int>(this.rootNode, name: $"Var{value * 100}", value: value);
            }

            references.Add(this.rootNode, OpcObjectTypes.ObjectsFolder);
            yield return this.rootNode;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                this.Stop();

            base.Dispose(disposing);
        }

        #endregion

        #region ---------- Private methods ----------

        private void UpdateVariables()
        {
            var name = this.Name;

            while (!this.exit.Wait(name.Length * 250)) {
                var variableNodes = this.rootNode
                        .Children(this.SystemContext)
                        .OfType<OpcDataVariableNode<int>>()
                        .ToArray();

                for (int index = 0; index < variableNodes.Length; index++) {
                    var multiplier = (int)name[index];
                    variableNodes[index].Value = multiplier * DateTime.Now.Millisecond;
                }

                this.rootNode.ApplyChanges(this.SystemContext, recursive: true);
            }
        }

        #endregion
    }
}
