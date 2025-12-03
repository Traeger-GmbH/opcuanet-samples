namespace App
{
    using Opc.UaFx;
    using Opc.UaFx.MachineVision;

    public class VisionSystemNode : OpcVisionSystemNode
    {
        private readonly OpcContext context;


        public VisionSystemNode(OpcContext context, OpcName name)
            : base(DefaultModel ?? OpcNodeModel.None, name)
        {
            this.context = context;
        }


        public static OpcNodeModel? DefaultModel
        {
            get;
            set;
        }


        public void AddResult(string jobId, string content)
        {
            var resultNode = new ResultNode(
                    this.Namespace.GetName(Guid.NewGuid().ToString()));

            resultNode.Value = new Opc.UaFx.MachineVision.OpcResult {
                JobId = new OpcJobId { Id = jobId },
                ResultContent = [content]
            };

            var context = this.context.OfNode(this.ResultManagement!.Results);
            this.ResultManagement!.Results!.AddChild(context, resultNode);
        }


        protected override OpcVisionStateMachineNode? CreateVisionStateMachineNode(OpcName name)
        {
            return null; // Omit upon issue in preview version.
        }


        protected override void OnBeforeAdded(OpcNodeAddedEventArgs e)
        {
            e.References.Add(OpcNodeReference
                    .From(Opc.Ua.ObjectIds.ObjectsFolder)
                    .ToChild(this, OpcReferenceType.Organizes));

            e.References.Add(OpcNodeReference
                    .From(this)
                    .ToParent(
                            Opc.Ua.ObjectIds.ObjectsFolder,
                            Opc.Ua.ReferenceTypeIds.Organizes));

            base.OnBeforeAdded(e);
        }
    }
}
