' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Collections.Generic

Imports Opc.UaFx
Imports Opc.UaFx.Server

Namespace ClientAddedNodesWithHistory
    Friend Class NodeManager
        Inherits OpcNodeManager

        '---------- Private readonly fields ----------

        Private ReadOnly historians As Dictionary(Of IOpcNode, OpcNodeHistorian)


        '---------- Public constructors ----------

        Public Sub New()
            MyBase.New("http://sampleserver/clientnodeswithhistory")
            Me.historians = New Dictionary(Of IOpcNode, OpcNodeHistorian)()
        End Sub


        '---------- Protected methods ----------

        Protected Overrides Sub AddNode(
                context As OpcContext,
                node As IOpcNode,
                references As IEnumerable(Of IOpcNodeReferenceAware))
            MyBase.AddNode(context, node, references)

            ' "Catch" nodes added by the client and "attach" the on-demand timestamp determination
            ' (see HandleWriteVariableValue) and a simple in-memory historian which shall
            ' automatically fill its historical values whenever a node value is written.
            Dim variableNode = TryCast(node, OpcVariableNode)

            If variableNode IsNot Nothing AndAlso variableNode.AccessLevel.HasFlag(OpcAccessLevel.HistoryRead) Then
                variableNode.WriteVariableValueCallback = AddressOf Me.HandleWriteVariableValue
                variableNode.IsHistorizing = True

                Dim historian = New OpcNodeHistorian(Me, variableNode)
                historian.AutoUpdateHistory = True

                Me.historians.Add(node, historian)
            End If
        End Sub

        Protected Overrides Function CreateNodes(references As OpcNodeReferenceCollection) As IEnumerable(Of IOpcNode)
            ' We just want to define a simple "entry point" for client added nodes.
            Dim machineNode = New OpcObjectNode(Me.DefaultNamespace.GetName("Machine"))
            references.Add(machineNode, OpcObjectTypes.ObjectsFolder)

            Return New IOpcNode() {machineNode}
        End Function

        Protected Overrides Function RetrieveNodeHistoryProvider(node As IOpcNode) As IOpcNodeHistoryProvider
            ' Determine existing historian of the node the history provider is requested for.
            Dim historian As OpcNodeHistorian = Nothing

            If Me.historians.TryGetValue(node, historian) Then
                Return historian
            End If

            Return MyBase.RetrieveNodeHistoryProvider(node)
        End Function


        '---------- Private methods ----------

        Private Function HandleWriteVariableValue(
                context As OpcWriteVariableValueContext,
                value As OpcVariableValue(Of Object)) As OpcVariableValue(Of Object)
            ' A client may not pass a timestamp associated with a new value or may use the same
            ' timestamp as already used by the variable node.
            '
            ' Upon we want to enforce that every value written to the node uses its own timestamp
            ' we update the variable value with the current time to assure that every write
            ' results into a new entry in the history of the node.
            If Not value.Timestamp.HasValue OrElse Nullable.Equals(value.Timestamp, context.Node.Timestamp) Then
                value = New OpcVariableValue(Of Object)(value.Value, DateTime.UtcNow, value.Status)
            End If

            Return value
        End Function
    End Class
End Namespace
