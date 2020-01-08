// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace AE
{
    using Opc.UaFx;
    using Opc.UaFx.Client;

    internal class ObservedNode
    {
        public ObservedNode(OpcNodeId parentNodeId, string name)
            : base()
        {
            this.Id = OpcNodeId.Of(name, parentNodeId);
            this.Label = (name + ":").PadRight(16);
        }

        public OpcNodeId Id
        {
            get;
        }

        public OpcEvent Event
        {
            get;
            set;
        }
        
        public string Label
        {
            get;
        }

        public OpcEngineeringUnitInfo Unit
        {
            get;
            private set;
        }

        public OpcValue Value
        {
            get;
            set;
        }


        public void Initialize(OpcClient client)
        {
            var node = client.BrowseNode(this.Id);
            var analogNode = node as OpcAnalogItemNodeInfo;

            if (analogNode != null)
                this.Unit = analogNode.EngineeringUnit;

            this.Value = client.ReadNode(this.Id);
        }
    }
}
