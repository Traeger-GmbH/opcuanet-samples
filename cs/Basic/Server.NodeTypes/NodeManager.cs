// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace NodeTypes
{
    using System.Collections.Generic;

    using Opc.UaFx;
    using Opc.UaFx.Server;

    internal partial class NodeManager : OpcNodeManager
    {
        #region ---------- Public constructors ----------

        public NodeManager()
            : base("http://sampleserver/samplenodetypes")
        {
        }

        #endregion

        #region ---------- Protected methods ----------

        protected override IEnumerable<OpcNodeSet> ImportNodes()
        {
            yield return OpcNodeSet.Load(@".\MyNodeSet.xml");
        }

        protected override IEnumerable<IOpcNode> CreateNodes(OpcNodeReferenceCollection references)
        {
            var variableNodes = new OpcObjectNode("MyVariables");
            references.Add(variableNodes, OpcObjectTypes.ObjectsFolder);

            var simpleVariableNodes = new OpcObjectNode(variableNodes, "Simple");
            new MySimpleVariableNode(simpleVariableNodes, "Var01", 1);
            new MySimpleVariableNode(simpleVariableNodes, "Var02", 2);

            var complexVariableNodes = new OpcObjectNode(variableNodes, "Complex");
            new MyComplexVariableNode(complexVariableNodes, "Var01", "A");
            new MyComplexVariableNode(complexVariableNodes, "Var02", "B", 42);

            var objectNodes = new OpcObjectNode("MyObjects");
            references.Add(objectNodes, OpcObjectTypes.ObjectsFolder);

            var simpleObjectNodes = new OpcObjectNode(objectNodes, "Simple");
            new MySimpleObjectNode(simpleObjectNodes, "Object01");
            new MySimpleObjectNode(simpleObjectNodes, "Object02");

            var complexObjectNodes = new OpcObjectNode(objectNodes, "Complex");
            new MyComplexObjectNode(complexObjectNodes, "Object01");
            new MyComplexObjectNode(complexObjectNodes, "Object02");

            return new[] { variableNodes, objectNodes };
        }

        #endregion
    }
}
