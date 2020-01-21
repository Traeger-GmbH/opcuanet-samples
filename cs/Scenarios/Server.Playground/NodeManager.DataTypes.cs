// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace Server.Playground
{
    using System;
    using Opc.UaFx;

    internal partial class NodeManager
    {
        #region ---------- Private types ----------

        [OpcDataType("ns=2;s=MyEnum")]
        private enum MyEnum
        {
            Starting = 0,
            Started = 1,
            Stopping = 2,
            Stopped = 3,
            Maintenance = 4
        }

        [Flags]
        [OpcDataType("ns=2;s=MyEnumFlags")]
        private enum MyEnumFlags
        {
            None = 0,
            CanCut = 1,
            CanFold = 2,
            CanPrint = 4,
            CanCorrugate = 8,
            CanLaminate = 16,
        }

        #endregion
    }
}
