' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Collections.Generic

Imports Opc.UaFx
Imports Opc.UaFx.Server

Namespace NodeSet
    Partial Friend Class NodeManager
        Inherits OpcNodeManager

        '---------- Private fields ----------

        Private alertsNode As OpcObjectNode
        Private machineToolAlarmNode As MachineToolAlarmConditionNode

        '---------- Public constructors ----------

        Public Sub New()
            MyBase.New("http://www.umati.info", "http://www.umati.info/example")
        End Sub

        '---------- Public methods ----------

        Public Overloads Sub ReportEvent()
            Dim eventId = Guid.NewGuid()
            Me.machineToolAlarmNode.EventId = eventId.ToByteArray()

            Me.machineToolAlarmNode.AlarmIdentifier = "MyAlarm"
            Me.machineToolAlarmNode.AuxParameters = "a;b;c;d"

            Dim context = Me.SystemContext

            Me.machineToolAlarmNode.ChangeIsConfirmed(context, False)
            Me.machineToolAlarmNode.ChangeIsAcked(context, False)
            Me.machineToolAlarmNode.ChangeIsActive(context, True)

            Me.machineToolAlarmNode.ReportEventFrom(context, Me.alertsNode)
            Console.WriteLine("Event {0} reported.", eventId.ToString())
        End Sub

        '---------- Protected methods ----------

        Protected Overrides Iterator Function ImportNodes() As IEnumerable(Of OpcNodeSet)
            Yield OpcNodeSet.Load(".\umati_EMO2019.xml")
            Yield OpcNodeSet.Load(".\umati-instances_EMO2019.xml")
        End Function

        Protected Overrides Sub ImplementNode(node As IOpcNode)
            Dim objectNode = TryCast(node, OpcObjectNode)

            If objectNode IsNot Nothing AndAlso node.Name.Value = "Alerts" Then
                Me.alertsNode = objectNode
            End If

            MyBase.ImplementNode(node)
        End Sub

        Protected Overrides Iterator Function CreateNodes(references As OpcNodeReferenceCollection) As IEnumerable(Of IOpcNode)
            Me.machineToolAlarmNode = New MachineToolAlarmConditionNode()
            Me.alertsNode.AddNotifier(Me.SystemContext, Me.machineToolAlarmNode)

            Yield Me.machineToolAlarmNode
        End Function
    End Class
End Namespace
