namespace Umati
{
    using Opc.UaFx;

    [OpcEventType("ns=2;i=1042")]
    internal class MachineToolAlarmCondition : OpcAlarmCondition
    {
        #region ---------- Public constructors ----------

        public MachineToolAlarmCondition(IOpcReadOnlyNodeDataStore dataStore)
            : base(dataStore)
        {
        }

        #endregion

        #region ---------- Public properties ----------

        public string AlarmIdentifier
        {
            get => this.DataStore.Get<string>("2:AlarmIdentifier");
        }

        public string AuxParameters
        {
            get => this.DataStore.Get<string>("2:AuxParameters");
        }

        #endregion
    }
}
