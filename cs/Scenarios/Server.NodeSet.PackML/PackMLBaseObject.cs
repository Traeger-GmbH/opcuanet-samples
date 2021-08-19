// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace NodeSet
{
    using System;
    using Opc.UaFx;

    /// <summary>
    /// https://reference.opcfoundation.org/v104/PackML/v101/docs/6.3.2
    /// </summary>
    internal class PackMLBaseObjectNode : OpcObjectNode
    {
        #region ---------- Public constructors ----------

        public PackMLBaseObjectNode(IOpcNode parent, OpcName name, OpcNodeId id)
            : base(parent, name, id)
        {
            this.AdminNode = new PackMLAdminObjectNode(this, PackML.GetName("Admin"));
            this.BaseStateMachineNode = new PackMLBaseStateMachineNode(this, PackML.GetName("BaseStateMachine"));
            this.StatusNode = new PackMLStatusObjectNode(this, PackML.GetName("Status"));

            this.SetMachSpeedNode = new OpcMethodNode(this, PackML.GetName(nameof(this.SetMachSpeed)), new Action<float>(this.SetMachSpeed));
            this.SetParameterNode = new OpcMethodNode(this, PackML.GetName(nameof(this.SetParameter)), new Action<PackMLDescriptorData[]>(this.SetParameter));
            this.SetProductNode = new OpcMethodNode(this, PackML.GetName(nameof(this.SetProduct)), new Action<PackMLProductData[]>(this.SetProduct));
            this.SetUnitModeNode = new OpcMethodNode(this, PackML.GetName(nameof(this.SetUnitMode)), new Action<int>(this.SetUnitMode));

            this.RemoteCommandNode = new OpcMethodNode(this, PackML.GetName(nameof(this.RemoteCommand)), new Action<PackMLRemoteInterfaceData[]>(this.RemoteCommand)); // optional
        }

        #endregion

        #region ---------- Public properties ----------

        public PackMLAdminObjectNode AdminNode
        {
            get;
        }

        public PackMLBaseStateMachineNode BaseStateMachineNode
        {
            get;
        }

        public OpcPropertyNode<string> PackMLVersionNode // optional
        {
            get;
            set;
        }

        public OpcMethodNode RemoteCommandNode // optional
        {
            get;
            set;
        }

        public OpcMethodNode SetInterlockNode // optional
        {
            get;
            set;
        }

        public OpcMethodNode SetMachSpeedNode
        {
            get;
        }

        public OpcMethodNode SetParameterNode
        {
            get;
        }

        public OpcMethodNode SetProductNode
        {
            get;
        }

        public OpcMethodNode SetUnitModeNode
        {
            get;
        }

        public PackMLStatusObjectNode StatusNode
        {
            get;
        }

        public OpcPropertyNode<string> TagID // optional
        {
            get;
            set;
        }

        #endregion

        #region ---------- Protected properties ----------

        protected override OpcNodeId DefaultTypeDefinitionId
        {
            get => PackML.BaseObjectId;
        }

        #endregion

        #region ---------- Public methods ----------

        public void RemoteCommand(
                [OpcArgument("RemoteInterface")]
                PackMLRemoteInterfaceData[] remoteInterface)
        {
            Console.WriteLine(new string('-', 100));
            Console.WriteLine("Call to 'RemoteCommand'.");

            for (int itemIndex = 0; itemIndex < remoteInterface.Length; itemIndex++) {
                var item = remoteInterface[itemIndex];

                Console.WriteLine("RemoteInterface[{0}]", itemIndex);
                Console.WriteLine(".Number = {0}", item.Number);
                Console.WriteLine(".ControlCmdNumber = {0}", item.ControlCmdNumber);
                Console.WriteLine(".CmdValue = {0}", item.CmdValue);
                Console.WriteLine(".Parameter");

                for (int parameterIndex = 0; parameterIndex < item.Parameter.Length; parameterIndex++) {
                    var parameter = item.Parameter[parameterIndex];

                    Console.WriteLine("..Parameter[{0}]", parameterIndex);
                    Console.WriteLine("...ID = {0}", parameter.ID);
                    Console.WriteLine("...Name = {0}", parameter.Name);
                    Console.WriteLine("...Value = {0} {1}", parameter.Value, parameter.Unit.DisplayName);
                }
            }

            Console.WriteLine(new string('-', 100));
        }

        public void SetInterlock(
                [OpcArgument("InterlockId")]
                int interlockId,
                [OpcArgument("State")]
                bool state)
        {
            throw new OpcException(OpcStatusCode.BadNotImplemented);
        }

        public void SetMachSpeed(
                [OpcArgument("RequestedMachineSpeed")]
                float requestedMachineSpeed)
        {
            throw new OpcException(OpcStatusCode.BadNotImplemented);
        }

        public void SetParameter(
                [OpcArgument("Parameter")]
                PackMLDescriptorData[] parameter)
        {
            throw new OpcException(OpcStatusCode.BadNotImplemented);
        }

        public void SetProduct(
                [OpcArgument("Product")]
                PackMLProductData[] product)
        {
            throw new OpcException(OpcStatusCode.BadNotImplemented);
        }

        public void SetUnitMode(
                [OpcArgument("RequestedMode")]
                int requestedMode)
        {
            throw new OpcException(OpcStatusCode.BadNotImplemented);
        }

        #endregion
    }
}
