' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Collections.Generic

Imports Opc.UaFx
Imports Opc.UaFx.Server

Namespace DataTypes
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

        '---------- Protected methods ----------

        ''' <summary>
        ''' Creates the nodes provided and associated with the node manager.
        ''' </summary>
        ''' <param name="references">A dictionary used to determine the logical references between
        ''' existing nodes (e.g. OPC default nodes) and the nodes provided by the node
        ''' manager.</param>
        ''' <returns>An enumerable containing the root nodes of the node manager.</returns>
        ''' <remarks>This method will be only called once by the server on start up.</remarks>
        Protected Overrides Iterator Function CreateNodes(ByVal references As OpcNodeReferenceCollection) As IEnumerable(Of IOpcNode)
            Yield New OpcDataTypeNode(Of MachineStatus)()
            Yield New OpcDataTypeNode(Of MachineSetup)()

            Yield New OpcDataTypeNode(Of MachineJob)()
            Yield New OpcDataTypeNode(Of ManufacturingOrder)()

            Dim machines = New OpcFolderNode("Machines")

            'Add new reference to make the node visible beneath the ObjectsFolder
            '(the top most root node within every OPC UA server).
            references.Add(machines, OpcObjectTypes.ObjectsFolder)

            Dim machineOne = New OpcFolderNode(machines, "Machine_1")
            Dim name1 = New OpcDataVariableNode(Of String)(machineOne, "Name", "Machine 1")
            Dim status1 = New OpcDataVariableNode(Of MachineStatus)(machineOne, "Status", MachineStatus.Stopped)
            Dim isActive1 = New OpcDataVariableNode(Of Boolean)(machineOne, "IsActive", False)
            Dim temperature1 = New OpcDataVariableNode(Of Double)(machineOne, "Temperature", 18.3)
            Dim job1 = New OpcDataVariableNode(Of MachineJob)(machineOne, "Job", New MachineJob() With {
                .Number = "JOB001",
                .EstimatedDuration = 12500,
                .InProcess = False,
                .CuttingPositions = New Integer() {1000, 1500, 1570, 2020},
                .RequiredSetup = MachineSetup.Packager,
                .ScheduleTime = DateTime.UtcNow.AddMinutes(10)
            })

            Dim machineTwo = New OpcFolderNode(machines, "Machine_2")
            Dim name2 = New OpcDataVariableNode(Of String)(machineTwo, "Name", "Machine 2")
            Dim status2 = New OpcDataVariableNode(Of MachineStatus)(machineTwo, "Status", MachineStatus.Suspended)
            Dim isActive2 = New OpcDataVariableNode(Of Boolean)(machineTwo, "IsActive", True)
            Dim temperature2 = New OpcDataVariableNode(Of Double)(machineTwo, "Temperature", 20.7)
            Dim order2 = New OpcDataVariableNode(Of ManufacturingOrder)(machineTwo, "Order", New ManufacturingOrder() With {
                .Order = "2020.10.10001",
                .Article = "ART10025",
                .Jobs = {New MachineJob With {
                    .Number = "JOB1001",
                    .Duration = 900,
                    .EstimatedDuration = 1000,
                    .InProcess = False,
                    .CuttingPositions = New Integer(3) {},
                    .RequiredSetup = MachineSetup.Corrugator,
                    .ScheduleTime = DateTime.UtcNow
                }, New MachineJob With {
                    .Number = "JOB1002",
                    .Duration = 510,
                    .EstimatedDuration = 500,
                    .InProcess = True,
                    .CuttingPositions = New Integer() {100, 1300, 1700, 2520},
                    .RequiredSetup = MachineSetup.Cutter,
                    .ScheduleTime = DateTime.UtcNow.AddSeconds(1)
                }, New MachineJob With {
                    .Number = "JOB1003",
                    .Duration = 1030,
                    .EstimatedDuration = 2200,
                    .InProcess = True,
                    .CuttingPositions = New Integer(3) {},
                    .RequiredSetup = MachineSetup.Printer1,
                    .ScheduleTime = DateTime.UtcNow.AddSeconds(2)
                }}
            })

            Yield machines
        End Function
    End Class
End Namespace
