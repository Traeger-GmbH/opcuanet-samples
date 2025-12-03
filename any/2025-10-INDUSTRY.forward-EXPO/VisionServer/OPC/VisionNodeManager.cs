namespace App
{
    using System.Collections.Generic;

    using Opc.UaFx;
    using Opc.UaFx.MachineVision;
    using Opc.UaFx.Server;

    public class VisionNodeManager : OpcNodeManager
    {
        private int nextJobId;
        private VisionSystemNode? visionSystemNode;


        public VisionNodeManager()
            : base("urn:app")
        {
        }


        public void AddResult(string content)
        {
            this.visionSystemNode?.AddResult($"JOB{++this.nextJobId:D3}", content);
        }


        protected override IEnumerable<IOpcNode> CreateNodes(OpcNodeReferenceCollection references)
        {
            this.visionSystemNode = new VisionSystemNode(
                    this.SystemContext,
                    this.DefaultNamespace.GetName("VisionSystem"));

            yield return this.visionSystemNode;
        }


        protected override IEnumerable<OpcNodeSet> ImportNodes()
        {
            {
                var visionSystemType = this.SystemContext
                        .GetNodeType(OpcMachineVisionNodes.VisionSystemType);

                VisionSystemNode.DefaultModel = OpcNodeModel
                        .Of(visionSystemType)
                        .OmitOptionals()
                        .Create(OpcMachineVisionNodes.GetName("ResultManagement"));
            }

            {
                var resultType = this.SystemContext
                        .GetNodeType(OpcMachineVisionNodes.ResultType);

                ResultNode.DefaultModel = OpcNodeModel
                        .Of(resultType)
                        .OmitOptionals()
                        .Create(OpcMachineVisionNodes.GetName("ResultContent"));
            }

            yield break;
        }
    }
}
