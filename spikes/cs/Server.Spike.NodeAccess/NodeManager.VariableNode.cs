// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace NodeAccess
{
    using Opc.UaFx;

    internal partial class NodeManager
    {
        private class VariableNode<T> : OpcDataVariableNode<T>
        {
            #region ---------- Private readonly fields ----------

            private readonly NodeManager manager;

            #endregion

            #region ---------- Public constructors ----------

            public VariableNode(NodeManager manager, IOpcNode parent, OpcName name, T value)
                : base(parent, name, value)
            {
                this.manager = manager;
            }

            #endregion

            #region ---------- Protected methods ----------

            protected override OpcAttributeValue<TAttribute> ReadAttributeValueCore<TAttribute>(
                    OpcReadAttributeValueContext context,
                    OpcAttributeValue<TAttribute> value)
            {
                var attribute = value.Attribute;

                // By default a user can not longer write to any node/attribute as soon the
                // OpcRequestType.Write is already denied (see Program.cs).
                //
                // To additionally inform the client/user that its access is restricted we shall
                // provide the according user dependent metadata for the UserAccessLevel and
                // UserWriteAccess attributes as well.
                if (attribute == OpcAttribute.UserAccessLevel || attribute == OpcAttribute.UserWriteAccess) {
                    if (!this.manager.CanWrite(context.Identity)) {
                        if (value is OpcAttributeValue<OpcAccessLevel> accessLevel) {
                            accessLevel = new OpcAttributeValue<OpcAccessLevel>(
                                    value.Attribute, accessLevel.Value & ~OpcAccessLevel.CurrentWrite);

                            return (OpcAttributeValue<TAttribute>)(object)accessLevel;
                        }

                        if (value is OpcAttributeValue<OpcAttributeWriteAccess> writeAccess) {
                            writeAccess = new OpcAttributeValue<OpcAttributeWriteAccess>(
                                    value.Attribute, OpcAttributeWriteAccess.None);

                            return (OpcAttributeValue<TAttribute>)(object)writeAccess;
                        }
                    }
                }

                return base.ReadAttributeValueCore(context, value);
            }

            #endregion
        }
    }
}
