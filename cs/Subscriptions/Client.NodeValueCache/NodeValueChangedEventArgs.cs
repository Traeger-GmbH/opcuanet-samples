// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace NodeValueCache
{
    using System;

    using Opc.UaFx;

    public class NodeValueChangedEventArgs : EventArgs
    {
        #region ---------- Private readonly fields ----------

        private readonly OpcValue value;

        #endregion

        #region ---------- Public constructors ----------

        public NodeValueChangedEventArgs(OpcValue value)
            : base()
        {
            this.value = value;
        }

        #endregion

        #region ---------- Public properties ----------

        public OpcValue Value
        {
            get => this.value;
        }

        #endregion
    }
}
