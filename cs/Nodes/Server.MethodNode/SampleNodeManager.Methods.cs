// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace MethodNode
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Opc.UaFx;
    using Opc.UaFx.Server;

    /// <content>
    /// Defines some methods used by the <see cref="SampleNodeManager"/>.
    /// </content>
    internal partial class SampleNodeManager
    {
        #region ---------- Private static methods ----------

        private static int AddByDelegate(
                OpcMethodContext context,
                int a,
                int b)
        {
            // result = a + b
            return a + b;
        }

        private static void AddByCommand(
                OpcMethodContext context,
                IList<object> inputArguments,
                IList<object> outputArguments)
        {
            // result = a + b
            outputArguments[0] = (int)inputArguments[0] + (int)inputArguments[1];
        }



        private static int MultiplyByDelegate(
                int a,
                int b)
        {
            // result = a * b
            return a * b;
        }

        private static void MultiplyByCommand(
                OpcMethodContext context,
                IList<object> inputArguments,
                IList<object> outputArguments)
        {
            // result = a * b
            outputArguments[0] = (int)inputArguments[0] * (int)inputArguments[1];
        }




        private static void ProcessInputs(int count, string value)
        {
            if (string.IsNullOrEmpty(value))
                value = "Hello World!";

            for (int index = 0; index < count; index++)
                Console.WriteLine(value);
        }

        private static void ProcessInputsWithMetadata(
                [OpcArgument("COUNT", Description = "Number of Console.WriteLines executed.")]
                int count,
                [OpcArgument("value", Description = "Value to be printed to the console.")]
                string value)
        {
            ProcessInputs(count, value);
        }



        private static void ProcessOutputs(out string value)
        {
            value = "Hello World!";
        }

        private static void ProcessOutputsWithMetadata(
                [OpcArgument("value", Description = "The value provided.")]
                out string value)
        {
            value = "Hello World!";
        }



        private static string ProcessMultipleOutputs(
                out string value)
        {
            value = "Hello World via Parameter!";
            return "Hello World via Return!";
        }

        [return: OpcArgument("value2", Description = "The value2 provided.")]
        private static string ProcessMultipleOutputsWithMetadata(
                [OpcArgument("value1", Description = "The value1 provided.")]
                out string value)
        {
            return ProcessMultipleOutputs(out value);
        }



        private static void ProcessInOutputs(int count, out string value)
        {
            if (count == 0) {
                value = "Hello World";
            }
            else {
                var builder = new StringBuilder();

                for (int index = 0; index < count; index++)
                    builder.AppendFormat("{0}. ", index + 1);

                builder.AppendFormat(" -- Hello again!");
                value = builder.ToString();
            }
        }

        private static void ProcessInOutputsWithMetadata(
                [OpcArgument("COUNT", Description = "Countdown to hello.")]
                int count,
                [OpcArgument("value", Description = "The countdown.")]
                out string value)
        {
            ProcessInOutputs(count, out value);
        }



        private static void ProcessMultipleInOutputs(
                string first,
                string second,
                out string result,
                out int resultLength)
        {
            result = string.Join(", ", first, second);
            resultLength = result.Length;
        }

        private static void ProcessMultipleInOutputsWithMetadata(
                [OpcArgument("First", Description = "The value joined first.")]
                string first,
                [OpcArgument("Second", Description = "The value joined second.")]
                string second,
                [OpcArgument("Result", Description = "The result value.")]
                out string result,
                [OpcArgument("Result Length", Description = "The length of the result.")]
                out int resultLength)
        {
            ProcessMultipleInOutputs(first, second, out result, out resultLength);
        }



        public static void ProcessWithContext(OpcMethodContext context)
        {
            Console.WriteLine("Just called from session '{0}'!", context.SessionId);
        }

        public static void ProcessWithoutContext()
        {
            Console.WriteLine("Just called!");
        }

        #endregion
    }
}
