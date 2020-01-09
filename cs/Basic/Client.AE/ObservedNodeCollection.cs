// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace AE
{
    using System.Collections.ObjectModel;

    using Opc.UaFx;
    using Opc.UaFx.Client;

    internal class ObservedNodeCollection : KeyedCollection<string, ObservedNode>
    {
        public ObservedNodeCollection()
            : base()
        {
        }

        public ObservedNode Add(OpcClient client, OpcNodeId parentNodeId, string name)
        {
            var item = new ObservedNode(parentNodeId, name);
            item.Initialize(client);

            this.Add(item);
            return item;
        }

        protected override string GetKeyForItem(ObservedNode item)
        {
            return item.Id.ToString(OpcNodeIdFormat.Foundation);
        }
    }
}
