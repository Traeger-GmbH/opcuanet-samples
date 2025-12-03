// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace Companion.Machinery
{
    using Opc.UaFx;
    using Opc.UaFx.ISA95.JobControl;
    using Opc.UaFx.Machinery.Jobs;

    internal class MyJobManagementNode : OpcJobManagementNode
    {
        #region ---------- Public constructors ----------

        public MyJobManagementNode(IOpcNode parent, OpcName name)
            : base(parent, name)
        {
        }

        #endregion

        #region ---------- Protected methods ----------

        protected override OpcISA95JobOrderReceiverObjectNode CreateJobOrderControlNode(OpcName name)
        {
            return new MyJobOrderControlNode(this, name);
        }

        protected override OpcISA95JobResponseProviderObjectNode CreateJobOrderResultsNode(OpcName name)
        {
            return null;
        }

        #endregion
    }
}
