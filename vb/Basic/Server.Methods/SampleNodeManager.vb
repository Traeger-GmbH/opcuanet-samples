' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Collections.Generic
Imports System.Threading
Imports System.Threading.Tasks

Imports Opc.UaFx
Imports Opc.UaFx.Server

Namespace Methods
    ''' <summary>
    ''' Represents a sample implementation of a custom OpcNodeManager.
    ''' </summary>
    Friend Class SampleNodeManager
        Inherits OpcNodeManager

        '---------- Public constructors ----------

        ''' <summary>
        ''' Initializes a new instance of the <see cref="SampleNodeManager"/> class.
        ''' </summary>
        Public Sub New()
            MyBase.New("http://sampleserver/machines")
        End Sub

        '---------- Protected properties ----------

        'Protected Overrides ReadOnly Property UseAsyncMethodCalls As Boolean
        '    ' Async method execution can be enabled for each node manager individually. This
        '    ' results into the use of one worker thread per method being called. Independent
        '    ' whether the method is implemented using the TPL (using Tasks) or not.
        '    ' By default only TPL methods are executed in parallel.
        '    Get
        '        Return True
        '    End Get
        'End Property

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
            Dim methods = New OpcFolderNode(Me.DefaultNamespace.GetName("Methods"))

            ' Add new reference to make the node visible beneath the ObjectsFolder
            ' (the top most root node within every OPC UA server).
            references.Add(methods, OpcObjectTypes.ObjectsFolder)

            Dim addMethod = New OpcMethodNode(methods, NameOf(Add), New Func(Of Integer, Integer, Integer)(AddressOf Add))
            Dim subtractMethod = New OpcMethodNode(methods, NameOf(Subtract), New Func(Of Integer, Integer, Integer)(AddressOf Subtract))
            Dim helloMethod = New OpcMethodNode(methods, NameOf(Hello), New Action(Of String)(AddressOf Hello))

            Dim machines = New OpcFolderNode(Me.DefaultNamespace.GetName("Machines"))

            ' Add new reference to make the node visible beneath the ObjectsFolder
            ' (the top most root node within every OPC UA server).
            references.Add(machines, OpcObjectTypes.ObjectsFolder)

            Dim machineOne = New OpcObjectNode(machines, "Machine_1",
                    New OpcMethodNode(NameOf(StartMachine), New Action(Of OpcMethodContext)(AddressOf StartMachine)),
                    New OpcMethodNode(NameOf(StartMachineAsync), New Func(Of OpcMethodContext, Task)(AddressOf StartMachineAsync)),
                    New OpcMethodNode(NameOf(StopMachine), New Action(Of OpcMethodContext)(AddressOf StopMachine)),
                    New OpcMethodNode(NameOf(StopMachineAsync), New Func(Of OpcMethodContext, Task)(AddressOf StopMachineAsync)))

            Dim machineTwo = New OpcObjectNode(machines, "Machine_2",
                    New OpcMethodNode(NameOf(StartMachine), New Action(Of OpcMethodContext)(AddressOf StartMachine)),
                    New OpcMethodNode(NameOf(StartMachineAsync), New Func(Of OpcMethodContext, Task)(AddressOf StartMachineAsync)),
                    New OpcMethodNode(NameOf(StopMachine), New Action(Of OpcMethodContext)(AddressOf StopMachine)),
                    New OpcMethodNode(NameOf(StopMachineAsync), New Func(Of OpcMethodContext, Task)(AddressOf StopMachineAsync)))

            Dim machineThree = New OpcObjectNode(machines, "Machine_3",
                    New OpcMethodNode(NameOf(StartMachine), New Action(Of OpcMethodContext)(AddressOf StartMachine)),
                    New OpcMethodNode(NameOf(StartMachineAsync), New Func(Of OpcMethodContext, Task)(AddressOf StartMachineAsync)),
                    New OpcMethodNode(NameOf(StopMachine), New Action(Of OpcMethodContext)(AddressOf StopMachine)),
                    New OpcMethodNode(NameOf(StopMachineAsync), New Func(Of OpcMethodContext, Task)(AddressOf StopMachineAsync)))

            Return New IOpcNode() {methods, machines}
        End Function

        '---------- Private static methods ----------

        ' Note: The 'OpcArgument' attributes are optional and can be applied to the parameters
        '       and the return value of the method.
        Private Shared Function Add(a As Integer, b As Integer) As <OpcArgument("result", Description:="The sum of 'a' and 'b'.")> Integer
            Dim result = a + b
            Console.WriteLine("{0} + {1} = {2}", a, b, result)

            Return result
        End Function

        ' Note: The 'OpcArgument' attributes are optional and can be applied to the parameters
        '       and the return value of the method.
        Private Shared Function Subtract(a As Integer, b As Integer) As <OpcArgument("result", Description:="The diff of 'a' and 'b'.")> Integer
            Dim result = a - b
            Console.WriteLine("{0} - {1} = {2}", a, b, result)

            Return result
        End Function

        ' Note: The 'OpcArgument' attributes are optional.
        Private Shared Sub Hello(
                <OpcArgument("name", Description:="The name of the user.")>
                name As String)
            Console.WriteLine("Hello {0}!", name)
        End Sub

        ' The OpcMethodContext is an optional parameter which is not published to the clients and
        ' can be used to control the outcome of the method execution and to query information
        ' regarding the method node being called. Note that the context parameter (if used) is to
        ' be defined as the first parameter.
        Private Shared Sub StartMachine(context As OpcMethodContext)
            Thread.Sleep(2500) ' Simulate start action which takes 2.5s.
            Console.WriteLine("{0} started.", context.Node.Parent.Name.Value)
        End Sub

        Private Shared Sub StopMachine(context As OpcMethodContext)
            Thread.Sleep(3000) ' Simulate stop action which takes 3s.
            Console.WriteLine("{0} stopped.", context.Node.Parent.Name.Value)
        End Sub

        Private Shared Function StartMachineAsync(context As OpcMethodContext) As Task
            ' Simulate start action which takes 2.5s.
            Return Task.Delay(2500).ContinueWith(
                    Sub(antecedent) Console.WriteLine("{0} started.", context.Node.Parent.Name.Value))
        End Function

        Private Shared Function StopMachineAsync(context As OpcMethodContext) As Task
            ' Simulate stop action which takes 3s.
            Return Task.Delay(3000).ContinueWith(
                    Sub(antecedent) Console.WriteLine("{0} stopped.", context.Node.Parent.Name.Value))
        End Function
    End Class
End Namespace
