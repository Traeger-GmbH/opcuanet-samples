' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System.Collections.Generic

Imports Opc.UaFx
Imports Opc.UaFx.Server

Namespace ConfiguredViaCode
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

        ' ---------- Protected methods ----------

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
            Dim machineOne As New OpcFolderNode(Me.DefaultNamespace.GetName("Machine_1"))

            ' Add new reference to make the node visible beneath the ObjectsFolder
            ' (the top most root node within every OPC UA server).
            references.Add(machineOne, OpcObjectTypes.ObjectsFolder)

            Dim name As New OpcDataVariableNode(Of String)(machineOne, "Name", "Machine 1")
            Dim status As New OpcDataVariableNode(Of Byte)(machineOne, "Status", 1)
            Dim position As New OpcDataVariableNode(Of SByte)(machineOne, "Position", -1)
            Dim isActive As New OpcDataVariableNode(Of Boolean)(machineOne, "IsActive", True)
            Dim temperature As New OpcDataVariableNode(Of Double)(machineOne, "Temperature", 18.3)

            Return New IOpcNode() {machineOne}
        End Function
    End Class
End Namespace
