// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace Methods
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using Opc.UaFx;
    using Opc.UaFx.Server;

    /// <summary>
    /// Represents a sample implementation of a custom OpcNodeManager.
    /// </summary>
    internal class SampleNodeManager : OpcNodeManager
    {
        #region ---------- Public constructors ----------

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleNodeManager"/> class.
        /// </summary>
        public SampleNodeManager()
            : base("http://sampleserver/machines")
        {
        }

        #endregion

        #region ---------- Protected properties ----------

        ////protected override bool UseAsyncMethodCalls
        ////{
        ////    // Async method execution can be enabled for each node manager individually. This
        ////    // results into the use of one worker thread per method being called. Independent
        ////    // whether the method is implemented using the TPL (using Tasks) or not.
        ////    // By default only TPL methods are executed in parallel.
        ////    get => true;
        ////}

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
            var methods = new OpcFolderNode(this.DefaultNamespace.GetName("Methods"));

            // Add new reference to make the node visible beneath the ObjectsFolder
            // (the top most root node within every OPC UA server).
            references.Add(methods, OpcObjectTypes.ObjectsFolder);

            new OpcMethodNode(methods, nameof(Add), new Func<int, int, int>(Add));
            new OpcMethodNode(methods, nameof(Subtract), new Func<int, int, int>(Subtract));
            new OpcMethodNode(methods, nameof(Hello), new Action<string>(Hello));

            var machines = new OpcFolderNode(this.DefaultNamespace.GetName("Machines"));

            // Add new reference to make the node visible beneath the ObjectsFolder
            // (the top most root node within every OPC UA server).
            references.Add(machines, OpcObjectTypes.ObjectsFolder);

            new OpcObjectNode(machines, "Machine_1",
                    new OpcMethodNode(nameof(StartMachine), new Action<OpcMethodContext>(StartMachine)),
                    new OpcMethodNode(nameof(StartMachineAsync), new Func<OpcMethodContext, Task>(StartMachineAsync)),
                    new OpcMethodNode(nameof(StopMachine), new Action<OpcMethodContext>(StopMachine)),
                    new OpcMethodNode(nameof(StopMachineAsync), new Func<OpcMethodContext, Task>(StopMachineAsync)));

            new OpcObjectNode(machines, "Machine_2",
                    new OpcMethodNode(nameof(StartMachine), new Action<OpcMethodContext>(StartMachine)),
                    new OpcMethodNode(nameof(StartMachineAsync), new Func<OpcMethodContext, Task>(StartMachineAsync)),
                    new OpcMethodNode(nameof(StopMachine), new Action<OpcMethodContext>(StopMachine)),
                    new OpcMethodNode(nameof(StopMachineAsync), new Func<OpcMethodContext, Task>(StopMachineAsync)));

            new OpcObjectNode(machines, "Machine_3",
                    new OpcMethodNode(nameof(StartMachine), new Action<OpcMethodContext>(StartMachine)),
                    new OpcMethodNode(nameof(StartMachineAsync), new Func<OpcMethodContext, Task>(StartMachineAsync)),
                    new OpcMethodNode(nameof(StopMachine), new Action<OpcMethodContext>(StopMachine)),
                    new OpcMethodNode(nameof(StopMachineAsync), new Func<OpcMethodContext, Task>(StopMachineAsync)));

            return new IOpcNode[] { methods, machines };
        }

        #endregion

        #region ---------- Private static methods ----------

        // Note: The 'OpcArgument' attributes are optional and can be applied to the parameters
        //       and the return value of the method.
        [return: OpcArgument("result", Description = "The sum of 'a' and 'b'.")]
        private static int Add(int a, int b)
        {
            var result = a + b;
            Console.WriteLine("{0} + {1} = {2}", a, b, result);

            return result;
        }

        // Note: The 'OpcArgument' attributes are optional and can be applied to the parameters
        //       and the return value of the method.
        [return: OpcArgument("result", Description = "The diff of 'a' and 'b'.")]
        private static int Subtract(int a, int b)
        {
            var result = a - b;
            Console.WriteLine("{0} - {1} = {2}", a, b, result);

            return result;
        }

        // Note: The 'OpcArgument' attributes are optional.
        private static void Hello(
                [OpcArgument("name", Description = "The name of the user.")]
                string name)
        {
            Console.WriteLine("Hello {0}!", name);
        }

        // The OpcMethodContext is an optional parameter which is not published to the clients and
        // can be used to control the outcome of the method execution and to query information
        // regarding the method node being called. Note that the context parameter (if used) is to
        // be defined as the first parameter.
        private static void StartMachine(OpcMethodContext context)
        {
            Thread.Sleep(2500); // Simulate start action which takes 2.5s.
            Console.WriteLine("{0} started.", context.Node.Parent.Name.Value);
        }

        private static void StopMachine(OpcMethodContext context)
        {
            Thread.Sleep(3000); // Simulate stop action which takes 3s.
            Console.WriteLine("{0} stopped.", context.Node.Parent.Name.Value);
        }

        private static Task StartMachineAsync(OpcMethodContext context)
        {
            return Task.Delay(2500).ContinueWith((_) => {
                Console.WriteLine("{0} started.", context.Node.Parent.Name.Value);
            }); // Simulate start action which takes 2.5s.
        }

        private static Task StopMachineAsync(OpcMethodContext context)
        {
            return Task.Delay(3000).ContinueWith((_) => {
                Console.WriteLine("{0} stopped.", context.Node.Parent.Name.Value);
            }); // Simulate stop action which takes 3s.
        }

        #endregion
    }
}
