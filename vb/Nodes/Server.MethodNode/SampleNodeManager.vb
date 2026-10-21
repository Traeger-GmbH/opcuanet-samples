' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Collections.Generic

Imports Opc.UaFx
Imports Opc.UaFx.Server

Namespace MethodNode
    ''' <summary>
    ''' Represents a sample implementation of a custom OpcNodeManager.
    ''' </summary>
    Partial Friend Class SampleNodeManager
        Inherits OpcNodeManager

        '---------- Public constructors ----------

        ''' <summary>
        ''' Initializes a new instance of the <see cref="SampleNodeManager"/> class.
        ''' </summary>
        Public Sub New()
            MyBase.New("http://sampleserver/methods")
        End Sub

        '---------- Protected methods ----------

        ''' <summary>
        ''' Creates the nodes provided and associated with the node manager.
        ''' </summary>
        ''' <param name="references">A dictionary used to determine the logical references between
        ''' existing nodes (e.g. OPC default nodes) and the nodes provided by the node
        ''' manager.</param>
        ''' <returns>An enumerable containing the root nodes of the node manager.</returns>
        ''' <remarks>This method will be only called once by the server on start up.</remarks>
        Protected Overrides Function CreateNodes(references As OpcNodeReferenceCollection) As IEnumerable(Of IOpcNode)
            ' It is necessary to assign to all root nodes one of the namespaces used to
            ' identify one of the associated namespaces (see the ctor of the class). This
            ' namespace does identify the node as member of the namespace of the node
            ' manager. Optionally it is possible to assign namespace to the child nodes
            ' too. But by default their missing namespace will be auto-completed through the
            ' namespace of their parent node.
            Dim methods = New OpcFolderNode("Methods")

            ' Add new reference to make the node visible beneath the ObjectsFolder
            ' (the top most root node within every OPC UA server).
            references.Add(methods, OpcObjectTypes.ObjectsFolder)

            Dim methodsByDelegate = New OpcFolderNode(methods, "Delegates")

            ' Define a method node using an existing generic delegate type receiving information
            ' about the context within the method is being called by a client.
            Dim addNode = New OpcMethodNode(
                    methodsByDelegate,
                    "Add",
                    New Func(Of OpcMethodContext, Integer, Integer, Integer)(AddressOf AddByDelegate))

            ' Define a method node using a custom / specific delegate type receiving information
            ' about the context within the method is being called by a client.
            Dim add2Node = New OpcMethodNode(
                    methodsByDelegate,
                    "Add2",
                    New AddDelegate(AddressOf AddByDelegate))

            ' Define a method node using an existing generic delegate type without receiving
            ' information about the context within the method is being called by a client.
            Dim multiplyNode = New OpcMethodNode(
                    methodsByDelegate,
                    "Multiply",
                    New Func(Of Integer, Integer, Integer)(AddressOf MultiplyByDelegate))

            ' Define a method node using a custom / specific delegate type without receiving
            ' information about the context within the method is being called by a client.
            Dim multiply2Node = New OpcMethodNode(
                    methodsByDelegate,
                    "Multiply2",
                    New MultiplyDelegate(AddressOf MultiplyByDelegate))

            Dim moreMethods = New OpcFolderNode(methodsByDelegate, "More")

            Dim processInputsNode = New OpcMethodNode(
                    moreMethods,
                    "ProcessInputs",
                    New Action(Of Integer, String)(AddressOf ProcessInputs))

            Dim processInputsWithMetadataNode = New OpcMethodNode(
                    moreMethods,
                    "ProcessInputsWithMetadata",
                    New Action(Of Integer, String)(AddressOf ProcessInputsWithMetadata))



            Dim processOutputsNode = New OpcMethodNode(
                    moreMethods,
                    "ProcessOutputs",
                    New ProcessOutputsDelegate(AddressOf ProcessOutputs))

            Dim processOutputsWithMetadataNode = New OpcMethodNode(
                    moreMethods,
                    "ProcessOutputsWithMetadata",
                    New ProcessOutputsDelegate(AddressOf ProcessOutputsWithMetadata))



            Dim processMultipleOutputsNode = New OpcMethodNode(
                    moreMethods,
                    "ProcessMultipleOutputs",
                    New ProcessMultipleOutputsDelegate(AddressOf ProcessMultipleOutputs))

            Dim processMultipleOutputsWithMetadataNode = New OpcMethodNode(
                    moreMethods,
                    "ProcessMultipleOutputsWithMetadata",
                    New ProcessMultipleOutputsDelegate(AddressOf ProcessMultipleOutputsWithMetadata))



            Dim processInOutputsNode = New OpcMethodNode(
                    moreMethods,
                    "ProcessInOutputs",
                    New ProcessInOutputsDelegate(AddressOf ProcessInOutputs))

            Dim processInOutputsWithMetadataNode = New OpcMethodNode(
                    moreMethods,
                    "ProcessInOutputsWithMetadata",
                    New ProcessInOutputsDelegate(AddressOf ProcessInOutputsWithMetadata))



            Dim processMultipleInOutputsNode = New OpcMethodNode(
                    moreMethods,
                    "ProcessMultipleInOutputs",
                    New ProcessMultipleInOutputsDelegate(AddressOf ProcessMultipleInOutputs))

            Dim processMultipleInOutputsWithMetadataNode = New OpcMethodNode(
                    moreMethods,
                    "ProcessMultipleInOutputsWithMetadata",
                    New ProcessMultipleInOutputsDelegate(AddressOf ProcessMultipleInOutputsWithMetadata))



            Dim processWithContextNode = New OpcMethodNode(
                    moreMethods,
                    "ProcessWithContext",
                    New Action(Of OpcMethodContext)(AddressOf ProcessWithContext))

            Dim processWithoutContextNode = New OpcMethodNode(
                    moreMethods,
                    "ProcessWithoutContext",
                    New Action(AddressOf ProcessWithoutContext))

            Dim methodsByCommand = New OpcFolderNode(methods, "Commands")

            ' Define a method node using a method to that the call is to be delegated while
            ' receiving information about the context of the call.
            Dim addCommandNode = New OpcMethodNode(
                    methodsByCommand,
                    "Add",
                    New OpcMethodDelegateCommand(AddressOf AddByCommand),
                    New OpcArgument() {
                        New OpcArgument("a", OpcDataType.Int32),
                        New OpcArgument("b", OpcDataType.Int32)
                    },
                    New OpcArgument() {
                        New OpcArgument("result", OpcDataType.Int32)
                    })

            ' Define a method node using a custom command instance to that the call is to be
            ' delegated while receiving information about the context of the call.
            Dim add2CommandNode = New OpcMethodNode(
                    methodsByCommand,
                    "Add2",
                    New AddCommand(),
                    New OpcArgument() {
                        New OpcArgument("a", OpcDataType.Int32),
                        New OpcArgument("b", OpcDataType.Int32)
                    },
                    New OpcArgument() {
                        New OpcArgument("result", OpcDataType.Int32)
                    })

            ' Define a method node using a method to that the call is to be delegated while
            ' receiving information about the context of the call.
            Dim multiplyCommandNode = New OpcMethodNode(
                    methodsByCommand,
                    "Multiply",
                    New OpcMethodDelegateCommand(AddressOf MultiplyByCommand),
                    New OpcArgument() {
                        New OpcArgument("a", OpcDataType.Int32),
                        New OpcArgument("b", OpcDataType.Int32)
                    },
                    New OpcArgument() {
                        New OpcArgument("result", OpcDataType.Int32)
                    })

            ' Define a method node using a custom command instance to that the call is to be
            ' delegated while receiving information about the context of the call.
            Dim multiply2CommandNode = New OpcMethodNode(
                    methodsByCommand,
                    "Multiply2",
                    New MultiplyCommand(),
                    New OpcArgument() {
                        New OpcArgument("a", OpcDataType.Int32),
                        New OpcArgument("b", OpcDataType.Int32)
                    },
                    New OpcArgument() {
                        New OpcArgument("result", OpcDataType.Int32)
                    })

            Return New IOpcNode() {methods}
        End Function
    End Class
End Namespace
