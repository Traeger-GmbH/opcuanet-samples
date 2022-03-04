// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace GenericEvents
{
    using System.Collections.Generic;
    using Opc.UaFx;

    internal class GenericEvent : OpcEvent
    {
        public GenericEvent(IOpcReadOnlyNodeDataStore dataStore)
            : base(dataStore)
        {
        }

        public Dictionary<string, object> GetData()
        {
            var data = default(Dictionary<string, object>);

            if (this.DataStore is OpcEventNodeView eventView) {
                var operands = eventView.Filter.SelectClause;
                var fields = eventView.Data.Fields;

                data = new Dictionary<string, object>();

                for (int index = 0; index < operands.Count; index++) {
                    var operand = operands[index];
                    var value = new OpcValue(fields[index].Value).Value;

                    data.Add(operand.NodePath.ToString(), value);
                }
            }

            return data;
        }
    }
}
