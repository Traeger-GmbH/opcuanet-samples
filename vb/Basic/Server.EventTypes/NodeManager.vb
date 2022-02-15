'Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Collections.Generic

Imports Opc.UaFx
Imports Opc.UaFx.Server

Namespace EventTypes
    Partial Friend Class NodeManager
        Inherits OpcNodeManager

        '---------- Private fields ----------

        Private machineOne As OpcFolderNode
        Private machineOneJobChanged As MachineJobEventNode

        Private machineTwo As OpcFolderNode
        Private machineTwoJobChanged As MachineJobEventNode

        '---------- Public constructors ----------

        Public Sub New()
            MyBase.New("http://sampleserver/sampleeventtypes")
        End Sub

        '---------- Protected methods ----------

        Protected Overrides Iterator Function ImportNodes() As IEnumerable(Of OpcNodeSet)
            Yield OpcNodeSet.Load(".\MyNodeSet.xml")
        End Function

        Protected Overrides Iterator Function CreateNodes(ByVal references As OpcNodeReferenceCollection) As IEnumerable(Of IOpcNode)
            Yield New OpcDataTypeNode(Of MachineSetup)()
            Yield New OpcDataTypeNode(Of MachineJob)()

            Dim machines = New OpcFolderNode("Machines")
            references.Add(machines, OpcObjectTypes.ObjectsFolder)

            Me.machineOne = New OpcFolderNode(machines, "Machine_1")
            Dim machineOneName = New OpcDataVariableNode(Of String)(Me.machineOne, "Name", "Machine 1")
            Dim machineOneTemperature = New OpcDataVariableNode(Of Double)(Me.machineOne, "Temperature", 18.3)

            Me.machineOneJobChanged = New MachineJobEventNode(Me.machineOne, "JobChanged")
            Me.machineOne.AddNotifier(Me.SystemContext, Me.machineOneJobChanged)

            Dim machineOneStartJob = New OpcMethodNode(Me.machineOne, "StartJob", New Action(Of MachineJob)(AddressOf Me.StartJobAtMachineOne))

            Me.machineTwo = New OpcFolderNode(machines, "Machine_2")
            Dim machineTwoName = New OpcDataVariableNode(Of String)(Me.machineTwo, "Name", "Machine 2")
            Dim machineTwoTemperature = New OpcDataVariableNode(Of Double)(Me.machineTwo, "Temperature", 20.7)

            Me.machineTwoJobChanged = New MachineJobEventNode(Me.machineTwo, "JobChanged")
            Me.machineTwo.AddNotifier(Me.SystemContext, Me.machineTwoJobChanged)

            Dim machineTwoStartJob = New OpcMethodNode(Me.machineTwo, "StartJob", New Action(Of MachineJob)(AddressOf Me.StartJobAtMachineTwo))

            Yield machines
        End Function

        '---------- Private methods ----------

        Private Sub StartJobAtMachineOne(job As MachineJob)
            Console.WriteLine("Started job '{0}' with setup '{1}'.", job.Number, job.RequiredSetup)

            Me.machineOneJobChanged.Job.Value = job
            Me.machineOneJobChanged.ReportEventFrom(Me.SystemContext, Me.machineOne)
        End Sub

        Private Sub StartJobAtMachineTwo(job As MachineJob)
            Console.WriteLine("Started job '{0}' with setup '{1}'.", job.Number, job.RequiredSetup)

            Me.machineTwoJobChanged.Job.Value = job
            Me.machineTwoJobChanged.ReportEventFrom(Me.SystemContext, Me.machineTwo)
        End Sub
    End Class
End Namespace
