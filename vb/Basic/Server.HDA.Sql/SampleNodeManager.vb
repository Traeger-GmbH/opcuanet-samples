' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Collections.Generic
Imports Opc.UaFx
Imports Opc.UaFx.Server

Namespace HDA.Sql
    ''' <summary>
    ''' Represents a sample implementation of a custom OpcNodeManager.
    ''' </summary>
    Friend Class SampleNodeManager
        Inherits OpcNodeManager

        '---------- Private fields ----------

        ''' <summary>
        ''' Stores an instance of the <see cref="SampleHistorian"/> used to manage the historical
        ''' data associated with the 'Position' node.
        ''' </summary>
        Private _positionHistorian As SampleHistorian

        ''' <summary>
        ''' Stores an instance of the <see cref="SampleHistorian"/> used to manage the historical
        ''' data associated with the 'Temperature' node.
        ''' </summary>
        Private _temperatureHistorian As SampleHistorian

        '---------- Public constructors ----------

        ''' <summary>
        ''' Initializes a new instance of the <see cref="SampleNodeManager"/> class.
        ''' </summary>
        Public Sub New()
            MyBase.New("http://sampleserver/machines")
        End Sub

        '---------- Public properties ----------

        Public ReadOnly Property PositionHistorian As SampleHistorian
            Get
                Return Me._positionHistorian
            End Get
        End Property

        Public ReadOnly Property TemperatureHistorian As SampleHistorian
            Get
                Return Me._temperatureHistorian
            End Get
        End Property

        '---------- Protected methods ----------

        ''' <summary>
        ''' Creates the nodes provided and associated with the node manager.
        ''' </summary>
        ''' <param name="references">A dictionary used to determine the logical references between
        ''' existing nodes (e.g. OPC default nodes) and the nodes provided by the node
        ''' manager.</param>
        ''' <returns>An enumerable containing the root nodes of the node manager.</returns>
        ''' <remarks>This method will be only called once by the server on start up.</remarks>
        Protected Overrides Function CreateNodes(ByVal references As OpcNodeReferenceCollection) As IEnumerable(Of IOpcNode)
            ' It is necessary to assign to all root nodes one of the namespaces used to
            ' identify one of the associated namespaces (see the ctor of the class). This
            ' namespace does identify the node as member of the namespace of the node
            ' manager. Optionally it is possible to assign namespace to the child nodes
            ' too. But by default their missing namespace will be auto-completed through the
            ' namespace of their parent node.
            Dim machineOne = New OpcFolderNode(Me.DefaultNamespace.GetName("Machine_1"))

            ' Add new reference to make the node visible beneath the ObjectsFolder
            ' (the top most root node within every OPC UA server).
            references.Add(machineOne, OpcObjectTypes.ObjectsFolder)

            Dim nameNode = New OpcDataVariableNode(Of String)(machineOne, "Name", "Machine 1")
            Dim statusNode = New OpcDataVariableNode(Of Byte)(machineOne, "Status", 1)
            Dim isActiveNode = New OpcDataVariableNode(Of Boolean)(machineOne, "IsActive", True)

            Me._positionHistorian = New SampleHistorian(
                    Me, New OpcDataVariableNode(Of Integer)(machineOne, "Position", -1))

            Me._temperatureHistorian = New SampleHistorian(
                    Me, New OpcDataVariableNode(Of Double)(machineOne, "Temperature", 18.3))

            Return New IOpcNode() {machineOne}
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="node"></param>
        ''' <returns></returns>
        Protected Overrides Function RetrieveNodeHistoryProvider(ByVal node As IOpcNode) As IOpcNodeHistoryProvider
            If Equals(Me._positionHistorian.Node, node) Then Return Me._positionHistorian
            If Equals(Me._temperatureHistorian.Node, node) Then Return Me._temperatureHistorian

            Return MyBase.RetrieveNodeHistoryProvider(node)
        End Function
    End Class
End Namespace
