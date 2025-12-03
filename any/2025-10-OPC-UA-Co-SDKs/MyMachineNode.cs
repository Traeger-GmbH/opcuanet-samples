// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace Companion.Machinery
{
    using Opc.Ua;

    using Opc.UaFx;
    using Opc.UaFx.Di;
    using Opc.UaFx.Machinery;
    using Opc.UaFx.Machinery.Jobs;

    internal class MyMachineNode : OpcTopologyElementNode
    {
        #region ---------- Private fields ----------

        private OpcFolderNode machineryBuildingBlocksNode;
        private OpcJobManagementNode jobManagementNode;

        #endregion

        #region ---------- Public constructors ----------

        public MyMachineNode(OpcName name)
            : base(name)
        {
            this.InitializeChildren();
        }

        public MyMachineNode(OpcNodeModel model, OpcName name)
            : base(model, name)
        {
            this.InitializeChildren();
        }

        #endregion

        #region ---------- Protected methods ----------

        protected override OpcFunctionalGroupNode? CreateIdentificationNode(OpcName name)
        {
            return new OpcMachineIdentificationNode(parent: this, name: name);
        }

        protected override void OnBeforeAdded(OpcNodeAddedEventArgs e)
        {
            e.References.Add(OpcNodeReference
                .From(OpcMachineryNodes.MachinesNode)
                .ToChild(this, OpcReferenceType.Organizes));

            e.References.Add(OpcNodeReference
                .From(this)
                .To(
                    OpcNodeReference.ToParent(OpcMachineryNodes.MachinesNode, ReferenceTypeIds.HasComponent),
                    OpcNodeReference.ToChild(this.Identification, ReferenceTypeIds.HasAddIn)));

            e.References.Add(OpcNodeReference
                .From(this.machineryBuildingBlocksNode)
                .To(
                    OpcNodeReference.ToChild(this.Identification, ReferenceTypeIds.HasAddIn),
                    OpcNodeReference.ToChild(this.jobManagementNode, ReferenceTypeIds.HasAddIn)));

            base.OnBeforeAdded(e);
        }

        #endregion

        #region ---------- Private methods ----------

        private void InitializeChildren()
        {
            this.machineryBuildingBlocksNode = new OpcFolderNode(
                    this, OpcMachineryNodes.GetName("MachineryBuildingBlocks"));

            this.jobManagementNode = new MyJobManagementNode(
                    this.machineryBuildingBlocksNode, OpcMachineryJobsNodes.GetName("JobManagement"));

            //this.jobManagementNode.JobOrderControl.StoreNode.Callback = this.StoreJob;
        }

        //private ulong StoreJob(
        //        OpcMethodContext methodContext,
        //        OpcISA95JobOrder jobOrder,
        //        LocalizedText[] comment)
        //{
        //    return 0UL;
        //}

        #endregion
    }
}
