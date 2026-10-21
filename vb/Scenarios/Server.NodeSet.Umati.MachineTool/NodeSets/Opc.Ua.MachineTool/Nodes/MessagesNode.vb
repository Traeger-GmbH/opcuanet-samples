' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System

Imports Opc.UaFx
Imports Opc.UaFx.Server

Namespace NodeSet.Umati
    Friend Class MessagesNode
        Inherits OpcObjectNode

        '---------- Private fields ----------

        Private alertsNode As AlertNode

        '---------- Public constructors ----------

        Public Sub New(parent As IOpcNode)
            MyBase.New(parent, MachineTool.GetName("Messages"))
            Me.InitializeChildren()
        End Sub

        '---------- Public properties ----------

        Public ReadOnly Property Alerts As AlertNode
            Get
                Return Me.alertsNode
            End Get
        End Property

        '---------- Public methods ----------

        Public Sub Report(context As OpcContext, errorCode As String)
            Dim source = Me.Parent?.Parent

            Me.alertsNode.SourceNodeId = source?.Id
            Me.alertsNode.SourceName = source?.Name?.Value

            Me.alertsNode.ErrorCode.Value = errorCode
            DirectCast(context.Owner, OpcServer).ReportEvent(Me.alertsNode)
        End Sub

        '---------- Protected methods ----------

        Protected Overrides Sub OnBeforeAdded(e As OpcNodeAddedEventArgs)
            ' Objects > Server > {HasEventSource:this}
            e.References.Add(OpcNodeReference _
                    .From(Opc.Ua.ObjectIds.Server) _
                    .ToChild(Me, OpcReferenceType.HasEventSource))

            MyBase.OnBeforeAdded(e)
        End Sub

        '---------- Private methods ----------

        Private Overloads Sub InitializeChildren()
            Me.alertsNode = New AlertNode(Me, MachineTool.GetName("Alerts"))
            Me.alertsNode.ConfirmCallback = AddressOf Me.HandleAlertsConfirm

            Me.AddChild(OpcContext.Empty, Me.alertsNode)
        End Sub

        Private Function HandleAlertsConfirm(
                context As OpcNodeContext(Of OpcConditionNode),
                eventId As Byte(),
                comment As OpcText) As OpcStatusCode
            Console.WriteLine("Confirmend: " & comment.Value)
            Return OpcStatusCode.Good
        End Function
    End Class
End Namespace
