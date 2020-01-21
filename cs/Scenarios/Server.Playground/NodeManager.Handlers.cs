// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace Server.Playground
{
    using System;
    using Opc.UaFx;

    internal partial class NodeManager
    {
        #region ---------- Private methods ----------

        private OpcVariableValue<object> HandleReadVariableValue(
                OpcReadVariableValueContext context,
                OpcVariableValue<object> value)
        {
            if (context.Identity != null) {
                Console.WriteLine(
                        "\t[{0} (SID='{1}')] Read: {2}.Value = '{3}'",
                        context.Identity?.DisplayName,
                        context.SessionId?.Value,
                        context.Node.Name,
                        value.Value);
            }

            return value;
        }

        private OpcVariableValue<object> HandleWriteVariableValue(
                OpcWriteVariableValueContext context,
                OpcVariableValue<object> value)
        {
            if (context.Identity != null) {
                Console.WriteLine(
                        "\t[{0} (SID='{1}')] Write: {2}.Value = '{3}'",
                        context.Identity?.DisplayName,
                        context.SessionId?.Value,
                        context.Node.Name,
                        value.Value);
            }

            return value;
        }

        #endregion
    }
}
