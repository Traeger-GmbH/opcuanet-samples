// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace NodeValueCache
{
    using System;

    using Opc.UaFx;

    public class NodeItem
    {
        private OpcValue valueForWrite;

        #region ---------- Private readonly fields ----------

        private readonly OpcNodeId nodeId;

        #endregion

        #region ---------- Public constructors ----------

        public NodeItem(OpcNodeId nodeId)
            : base()
        {
            this.nodeId = nodeId;
        }

        #endregion

        #region ---------- Public events ----------

        public event EventHandler<NodeValueChangedEventArgs>? ValueChanged;

        #endregion

        #region ---------- Public properties ----------

        public OpcNodeId NodeId
        {
            get => this.nodeId;
        }

        public OpcValue? Value
        {
            get
            {
                lock (this)
                    return this.ValueCore;
            }
        }

        public OpcValue? ValueForWrite
        {
            get
            {
                lock (this)
                    return this.valueForWrite;
            }
            set
            {
                lock (this) {
                    this.valueForWrite = value;

                    // Add an entry in the manger list
                    this.Manager.Value.
                }
            }
        }

        #endregion

        #region ---------- Internal properties ----------

        internal OpcValue? ValueCore
        {
            get;
            set;
        }

        internal OpcClientNodeItemManager? Manager
        {
            get;
            set;
        }

        internal object? SyncObject
        {
            get;
            set;
        }

        #endregion

        #region ---------- Internal methods ----------

        internal void RaiseValueChanged(OpcValue value)
        {
            this.OnValueChanged(new NodeValueChangedEventArgs(value));
        }

        #endregion

        #region ---------- Protected methods ----------

        protected virtual void OnValueChanged(NodeValueChangedEventArgs eventArgs)
        {
            this.ValueChanged?.Invoke(this, eventArgs);
        }

        #endregion
    }
}
