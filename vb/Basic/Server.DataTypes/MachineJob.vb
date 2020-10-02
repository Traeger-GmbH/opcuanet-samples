'Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports Opc.UaFx

Namespace DataTypes
    'Use the 'OpcDataTypeAttribute' to declare the type as a data type used in OPC UA as well.
    'Use the 'OpcDataTypeEncodingAttribute' to declare the type specific encoding of the data type.
    <OpcDataType("ns=2;s=MachineJob")>
    <OpcDataTypeEncoding("ns=2;s=MachineJob.Binary", Type:=OpcEncodingType.Binary)>
    Public Class MachineJob
        Public Property Number As String

        Public Property DurationSpecified As Boolean
            Get
                Return Me.Duration.HasValue
            End Get
            Set(ByVal value As Boolean)
                Me.Duration = If(value, 0, CType(Nothing, Integer?))
            End Set
        End Property

        'Use the 'OpcDataTypeMemberSwitch' to discard a field depending on the value
        'of another field.
        <OpcDataTypeMemberSwitch(NameOf(DurationSpecified))>
        Public Property Duration As Integer?

        Public Property EstimatedDuration As Integer

        Public Property InProcess As Boolean

        'Use the 'OpcDataTypeMemberLengthAttribute' to define the length of a fixed array field.
        <OpcDataTypeMemberLength(4)>
        Public Property CuttingPositions As Integer()

        Public Property RequiredSetup As MachineSetup

        Public Property ScheduleTime As DateTime
    End Class
End Namespace
