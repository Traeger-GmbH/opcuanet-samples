// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace Companion.Machinery
{
    using Opc.Ua;

    using Opc.UaFx;
    using Opc.UaFx.ISA95.JobControl;

    internal class MyJobOrderControlNode : OpcISA95JobOrderReceiverObjectNode
    {
        #region ---------- Public constructors ----------

        public MyJobOrderControlNode(IOpcNode parent, OpcName name)
            : base(parent, name)
        {
        }

        #endregion

        #region ---------- Public methods ----------

        public override ulong Store(
                OpcMethodContext methodContext,
                OpcISA95JobOrder jobOrder,
                LocalizedText[] comment)
        {
            return 0UL;
        }

        #endregion
    }
}
