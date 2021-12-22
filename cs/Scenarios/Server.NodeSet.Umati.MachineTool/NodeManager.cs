// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace NodeSet
{
    using System;
    using System.Collections.Generic;
    using Opc.UaFx;
    using Opc.UaFx.Server;

    public class NodeManager : OpcNodeManager
    {
        #region ---------- Public constructors ----------

        public NodeManager()
            : base("http://yourorganisation.org/MachineTool-Example/")
        {
        }

        #endregion

        #region ---------- Protected methods ----------

        protected override IEnumerable<OpcNodeSet> ImportNodes()
        {
            yield return OpcNodeSet.Load(@".\NodeSets\Machinetool-Example.xml");
        }

        #endregion
    }
}
