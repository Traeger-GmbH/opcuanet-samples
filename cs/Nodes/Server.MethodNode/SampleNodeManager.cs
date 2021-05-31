// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace MethodNode
{
    using System;
    using System.Collections.Generic;

    using Opc.UaFx;
    using Opc.UaFx.Server;

    /// <summary>
    /// Represents a sample implementation of a custom OpcNodeManager.
    /// </summary>
    internal partial class SampleNodeManager : OpcNodeManager
    {
        #region ---------- Public constructors ----------

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleNodeManager"/> class.
        /// </summary>
        public SampleNodeManager()
            : base("http://sampleserver/methods")
        {
        }

        #endregion

        #region ---------- Protected methods ----------

        /// <summary>
        /// Creates the nodes provided and associated with the node manager.
        /// </summary>
        /// <param name="references">A dictionary used to determine the logical references between
        /// existing nodes (e.g. OPC default nodes) and the nodes provided by the node
        /// manager.</param>
        /// <returns>An enumerable containing the root nodes of the node manager.</returns>
        /// <remarks>This method will be only called once by the server on start up.</remarks>
        protected override IEnumerable<IOpcNode> CreateNodes(OpcNodeReferenceCollection references)
        {
            // It is necessary to assign to all root nodes one of the namespaces used to
            // identify one of the associated namespaces (see the ctor of the class). This
            // namespace does identify the node as member of the namespace of the node
            // manager. Optionally it is possible to assign namespace to the child nodes
            // too. But by default their missing namespace will be auto-completed through the
            // namespace of their parent node.
            var methods = new OpcFolderNode("Methods");

            // Add new reference to make the node visible beneath the ObjectsFolder
            // (the top most root node within every OPC UA server).
            references.Add(methods, OpcObjectTypes.ObjectsFolder);

            {
                var methodsByDelegate = new OpcFolderNode(methods, "Delegates");

                // Define a method node using an existing generic delegate type receiving information
                // about the context within the method is being called by a client.
                new OpcMethodNode(
                        methodsByDelegate,
                        "Add",
                        new Func<OpcMethodContext, int, int, int>(AddByDelegate));

                // Define a method node using a custom / specific delegate type receiving information
                // about the context within the method is being called by a client.
                new OpcMethodNode(
                        methodsByDelegate,
                        "Add2",
                        new AddDelegate(AddByDelegate));

                // Define a method node using an existing generic delegate type without receiving
                // information about the context within the method is being called by a client.
                new OpcMethodNode(
                        methodsByDelegate,
                        "Multiply",
                        new Func<int, int, int>(MultiplyByDelegate));

                // Define a method node using a custom / specific delegate type without receiving
                // information about the context within the method is being called by a client.
                new OpcMethodNode(
                        methodsByDelegate,
                        "Multiply2",
                        new MultiplyDelegate(MultiplyByDelegate));

                {
                    var moreMethods = new OpcFolderNode(methodsByDelegate, "More");

                    new OpcMethodNode(
                            moreMethods,
                            "ProcessInputs",
                            new Action<int, string>(ProcessInputs));

                    new OpcMethodNode(
                            moreMethods,
                            "ProcessInputsWithMetadata",
                            new Action<int, string>(ProcessInputsWithMetadata));



                    new OpcMethodNode(
                            moreMethods,
                            "ProcessOutputs",
                            new ProcessOutputsDelegate(ProcessOutputs));

                    new OpcMethodNode(
                            moreMethods,
                            "ProcessOutputsWithMetadata",
                            new ProcessOutputsDelegate(ProcessOutputsWithMetadata));



                    new OpcMethodNode(
                            moreMethods,
                            "ProcessMultipleOutputs",
                            new ProcessMultipleOutputsDelegate(ProcessMultipleOutputs));

                    new OpcMethodNode(
                            moreMethods,
                            "ProcessMultipleOutputsWithMetadata",
                            new ProcessMultipleOutputsDelegate(ProcessMultipleOutputsWithMetadata));



                    new OpcMethodNode(
                            moreMethods,
                            "ProcessInOutputs",
                            new ProcessInOutputsDelegate(ProcessInOutputs));

                    new OpcMethodNode(
                            moreMethods,
                            "ProcessInOutputsWithMetadata",
                            new ProcessInOutputsDelegate(ProcessInOutputsWithMetadata));



                    new OpcMethodNode(
                            moreMethods,
                            "ProcessMultipleInOutputs",
                            new ProcessMultipleInOutputsDelegate(ProcessMultipleInOutputs));

                    new OpcMethodNode(
                            moreMethods,
                            "ProcessMultipleInOutputsWithMetadata",
                            new ProcessMultipleInOutputsDelegate(ProcessMultipleInOutputsWithMetadata));



                    new OpcMethodNode(
                            moreMethods,
                            "ProcessWithContext",
                            new Action<OpcMethodContext>(ProcessWithContext));

                    new OpcMethodNode(
                            moreMethods,
                            "ProcessWithoutContext",
                            new Action(ProcessWithoutContext));
                }
            }

            {
                var methodsByCommand = new OpcFolderNode(methods, "Commands");

                // Define a method node using a method to that the call is to be delegated while
                // receiving information about the context of the call.
                new OpcMethodNode(
                        methodsByCommand,
                        "Add",
                        new OpcMethodDelegateCommand(AddByCommand),
                        inputArguments: new[] {
                            new OpcArgument("a", OpcDataType.Int32),
                            new OpcArgument("b", OpcDataType.Int32)
                        },
                        outputArguments: new[] {
                            new OpcArgument("result", OpcDataType.Int32)
                        });

                // Define a method node using a custom command instance to that the call is to be
                // delegated while receiving information about the context of the call.
                new OpcMethodNode(
                        methodsByCommand,
                        "Add2",
                        new AddCommand(),
                        inputArguments: new[] {
                            new OpcArgument("a", OpcDataType.Int32),
                            new OpcArgument("b", OpcDataType.Int32)
                        },
                        outputArguments: new[] {
                            new OpcArgument("result", OpcDataType.Int32)
                        });

                // Define a method node using a method to that the call is to be delegated while
                // receiving information about the context of the call.
                new OpcMethodNode(
                        methodsByCommand,
                        "Multiply",
                        new OpcMethodDelegateCommand(MultiplyByCommand),
                        inputArguments: new[] {
                            new OpcArgument("a", OpcDataType.Int32),
                            new OpcArgument("b", OpcDataType.Int32)
                        },
                        outputArguments: new[] {
                            new OpcArgument("result", OpcDataType.Int32)
                        });

                // Define a method node using a custom command instance to that the call is to be
                // delegated while receiving information about the context of the call.
                new OpcMethodNode(
                        methodsByCommand,
                        "Multiply2",
                        new MultiplyCommand(),
                        inputArguments: new[] {
                            new OpcArgument("a", OpcDataType.Int32),
                            new OpcArgument("b", OpcDataType.Int32)
                        },
                        outputArguments: new[] {
                            new OpcArgument("result", OpcDataType.Int32)
                        });
            }

            return new IOpcNode[] { methods };
        }

        #endregion
    }
}
