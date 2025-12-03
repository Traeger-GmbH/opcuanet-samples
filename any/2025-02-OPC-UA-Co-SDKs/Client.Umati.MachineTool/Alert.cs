namespace Umati
{
    using Opc.UaFx;

    [OpcEventType(id: MachineTool.AlertTypeId, namespaceUri: MachineTool.NamespaceUri)]
    internal class Alert : OpcAlarmCondition
    {
        #region ---------- Public constructors ----------

        public Alert(IOpcReadOnlyNodeDataStore dataStore)
            : base(dataStore)
        {
        }

        #endregion

        #region ---------- Public properties ----------

        public string ErrorCode
        {
            get => this.GetValue<string>();
        }

        #endregion
    }
}
