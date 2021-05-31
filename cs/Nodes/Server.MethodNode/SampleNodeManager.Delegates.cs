// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace MethodNode
{
    using Opc.UaFx;

    /// <content>
    /// Defines some delegate types used by the <see cref="SampleNodeManager"/>.
    /// </content>
    internal partial class SampleNodeManager
    {
        public delegate int AddDelegate(OpcMethodContext context, int a, int b);
        public delegate int MultiplyDelegate(int a, int b);

        public delegate void ProcessOutputsDelegate(out string value);
        public delegate string ProcessMultipleOutputsDelegate(out string value);

        public delegate void ProcessInOutputsDelegate(int count, out string value);
        public delegate void ProcessMultipleInOutputsDelegate(
                string first,
                string second,
                out string result,
                out int resultLength);
    }
}
