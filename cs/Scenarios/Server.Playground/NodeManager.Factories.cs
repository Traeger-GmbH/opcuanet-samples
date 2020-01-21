// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace Server.Playground
{
    using System;
    using System.Linq;

    using Opc.UaFx;

    internal partial class NodeManager
    {
        #region ---------- Private methods ----------

        private OpcDataVariableNode<T[]> CreateArrayNode<T>(
                OpcFolderNode nodes,
                T defaultValue,
                Func<T, int, T> randomizer = null)
        {
            var value = Enumerable.Repeat(defaultValue, 5).ToArray();

            if (randomizer == null)
                return this.CreateNode(nodes, value, randomizer: null);

            return this.CreateNode(nodes, value, (oldValue, random) => {
                var index = this.indexRandom.Next(0, oldValue.Length - 1);

                ////var newValue = oldValue;

                // Array values need to be copied, otherwise there is no
                // Value-Changed-Bit set by the node.
                var newValue = new T[oldValue.Length];
                Array.Copy(oldValue, newValue, oldValue.Length);

                newValue[index] = randomizer(oldValue[index], random);
                return newValue;
            });
        }

        private OpcDataVariableNode<T> CreateNode<T>(
                OpcFolderNode nodes,
                T defaultValue,
                Func<T, int, T> randomizer = null)
        {
            var name = typeof(T).Name;
            name = char.ToUpper(name[0]) + name.Substring(1);

            if (typeof(T).IsArray)
                name = typeof(T).GetElementType().Name + "Array";

            var node = new OpcDataVariableNode<T>(nodes, name, defaultValue);

            node.ReadVariableValueCallback = this.HandleReadVariableValue;
            node.WriteVariableValueCallback = this.HandleWriteVariableValue;

            if (randomizer != null) {
                this.tasks.Add(() => {
                    node.Value = randomizer(node.Value, this.valueRandom.Next(0, 183));
                    node.ApplyChanges(this.SystemContext);
                });
            }

            return node;
        }

        #endregion
    }
}
