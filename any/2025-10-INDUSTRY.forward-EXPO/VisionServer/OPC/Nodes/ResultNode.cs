namespace App
{
    using Opc.UaFx;
    using Opc.UaFx.MachineVision;

    public class ResultNode : OpcResultNode
    {
        public ResultNode(OpcName name)
            : base(DefaultModel ?? OpcNodeModel.None, name)
        {
        }


        public static OpcNodeModel? DefaultModel
        {
            get;
            set;
        }
    }
}
