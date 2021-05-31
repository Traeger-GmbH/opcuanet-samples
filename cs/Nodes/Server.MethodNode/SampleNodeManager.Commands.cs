// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace MethodNode
{
    using System.Collections.Generic;
    using Opc.UaFx;

    /// <content>
    /// Defines some command types used by the <see cref="SampleNodeManager"/>.
    /// </content>
    internal partial class SampleNodeManager
    {
        public class AddCommand : IOpcMethodCommand
        {
            public bool CanExecute(OpcContext context)
            {
                return true;
            }

            public bool CanUserExecute(OpcContext context)
            {
                return true;
            }

            public void Execute(OpcMethodContext context, IList<object> inputArguments, IList<object> outputArguments)
            {
                // result = a + b
                outputArguments[0] = (int)inputArguments[0] + (int)inputArguments[1];
            }
        }

        public class MultiplyCommand : IOpcMethodCommand
        {
            public bool CanExecute(OpcContext context)
            {
                return true;
            }

            public bool CanUserExecute(OpcContext context)
            {
                return true;
            }

            public void Execute(OpcMethodContext context, IList<object> inputArguments, IList<object> outputArguments)
            {
                // result = a * b
                outputArguments[0] = (int)inputArguments[0] * (int)inputArguments[1];
            }
        }
    }
}
