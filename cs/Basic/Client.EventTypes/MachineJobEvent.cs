// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace EventTypes
{
    using Opc.UaFx;

    [OpcEventType("ns=1;s=MachineJobEventType")]
    public class MachineJobEvent : OpcEvent
    {
        #region ---------- Public constructors ----------

        public MachineJobEvent(IOpcReadOnlyNodeDataStore dataStore)
            : base(dataStore)
        {
        }

        #endregion

        #region ---------- Public properties ----------

        public MachineJob Job
        {
            get => this.DataStore.Get<MachineJob>($"1:{nameof(this.Job)}");
        }

        #endregion
    }
}
