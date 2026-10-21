' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports Opc.UaFx

Namespace DataTypes.Complex
    Public Class MachineJobNode
        Inherits OpcDataVariableNode(Of MachineJob)

        '---------- Public constructors ----------

        Public Sub New(parent As IOpcNode, name As OpcName, value As MachineJob)
            MyBase.New(parent, name, value)

            Me.Number = Me.CreateFieldNode(
                    NameOf(MachineJob.Number),
                    Function() Me.Value.Number,
                    Sub(fieldValue) Me.Value.Number = fieldValue)

            Me.Duration = Me.CreateFieldNode(
                    NameOf(MachineJob.Duration),
                    Function() Me.Value.Duration,
                    Sub(fieldValue) Me.Value.Duration = fieldValue)

            Me.EstimatedDuration = Me.CreateFieldNode(
                    NameOf(MachineJob.EstimatedDuration),
                    Function() Me.Value.EstimatedDuration,
                    Sub(fieldValue) Me.Value.EstimatedDuration = fieldValue)

            Me.InProcess = Me.CreateFieldNode(
                    NameOf(MachineJob.InProcess),
                    Function() Me.Value.InProcess,
                    Sub(fieldValue) Me.Value.InProcess = fieldValue)

            Me.RequiredSetup = Me.CreateFieldNode(
                    NameOf(MachineJob.RequiredSetup),
                    Function() Me.Value.RequiredSetup,
                    Sub(fieldValue) Me.Value.RequiredSetup = fieldValue)

            Me.ScheduleTime = Me.CreateFieldNode(
                    NameOf(MachineJob.ScheduleTime),
                    Function() Me.Value.ScheduleTime,
                    Sub(fieldValue) Me.Value.ScheduleTime = fieldValue)
        End Sub

        '---------- Public properties ----------

        Public ReadOnly Property Number As OpcDataVariableNode(Of String)

        Public ReadOnly Property Duration As OpcDataVariableNode(Of Integer)

        Public ReadOnly Property EstimatedDuration As OpcDataVariableNode(Of Integer)

        Public ReadOnly Property InProcess As OpcDataVariableNode(Of Boolean)

        Public ReadOnly Property RequiredSetup As OpcDataVariableNode(Of MachineSetup)

        Public ReadOnly Property ScheduleTime As OpcDataVariableNode(Of DateTime)

        '---------- Public methods ----------

        Protected Overrides Function WriteVariableValueCore(context As OpcWriteVariableValueContext, value As OpcVariableValue(Of Object)) As OpcVariableValue(Of Object)
            Dim typedValue = TryCast(value.Value, MachineJob)

            If typedValue IsNot Nothing Then
                Me.Number.Value = typedValue.Number
                Me.Duration.Value = typedValue.Duration
                Me.EstimatedDuration.Value = typedValue.EstimatedDuration
                Me.InProcess.Value = typedValue.InProcess
                Me.RequiredSetup.Value = typedValue.RequiredSetup
                Me.ScheduleTime.Value = typedValue.ScheduleTime
            End If

            Return MyBase.WriteVariableValueCore(context, value)
        End Function
    End Class
End Namespace
