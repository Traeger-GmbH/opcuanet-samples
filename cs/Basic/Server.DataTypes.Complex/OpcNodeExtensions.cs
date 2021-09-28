// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace DataTypes.Complex
{
    using System;
    using System.Linq;

    using Opc.UaFx;

    /// <summary>
    /// 
    /// </summary>
    public static class OpcNodeExtensions
    {
        #region ---------- Public static methods ----------

        public static OpcDataVariableNode<T> CreateFieldNode<T>(
                this IOpcNode parent,
                OpcName name,
                Func<T> getter,
                Action<T> setter)
        {
            return CreateFieldNode(
                    () => new OpcDataVariableNode<T>(parent, name),
                    getter,
                    setter);
        }

        public static OpcDataVariableNode<T[]> CreateFieldNode<TNode, T>(
                this IOpcNode parent,
                OpcName name,
                Func<T[]> getter,
                Action<T[]> setter,
                Func<IOpcNode, OpcName, T, TNode> itemConstructor)
            where TNode : OpcDataVariableNode<T>
            where T : new()
        {
            var fieldNode = new OpcDataVariableNode<T[]>(parent, name);

            fieldNode.ReadVariableValueCallback = ReadFieldValue<T[]>;
            fieldNode.WriteVariableValueCallback = WriteFieldValue<T[]>;

            var value = getter();

            for (var index = 0; index < value.Length; index++) {
                var itemIndex = index;

                CreateFieldNode(
                        () => itemConstructor(fieldNode, $"[{index}]", value[index]),
                        () => getter()[itemIndex],
                        (value) => getter()[itemIndex] = value);
            }

            fieldNode.Tag = (getter, setter);
            return fieldNode;
        }

        #endregion

        #region ---------- Private static methods ----------

        private static TNode CreateFieldNode<TNode, T>(
                Func<TNode> constructor,
                Func<T> getter,
                Action<T> setter)
            where TNode : OpcDataVariableNode<T>
        {
            var fieldNode = constructor();

            fieldNode.ReadVariableValueCallback = ReadFieldValue<T>;
            fieldNode.WriteVariableValueCallback = WriteFieldValue<T>;

            fieldNode.Tag = (getter, setter);
            return fieldNode;
        }

        private static OpcVariableValue<object> ReadFieldValue<T>(
                OpcReadVariableValueContext context,
                OpcVariableValue<object> value)
        {
            if (context.Node.Tag is ValueTuple<Func<T>, Action<T>> property)
                value = new OpcVariableValue<object>(property.Item1());

            return value;
        }

        private static OpcVariableValue<object> WriteFieldValue<T>(
                OpcWriteVariableValueContext context,
                OpcVariableValue<object> value)
        {
            if (context.Node.Tag is ValueTuple<Func<T>, Action<T>> property) {
                property.Item2((T)value.Value);
                ((OpcNode)context.Node.Parent).UpdateChanges(context, OpcNodeChanges.Value);
            }

            return value;
        }

        #endregion
    }
}
