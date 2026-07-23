' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports Opc.UaFx

Namespace DataTypes
    ' Use the 'OpcDataTypeAttribute' to declare the type as a data type used in OPC UA as well.
    ' Use the 'OpcDataTypeEncodingAttribute' to declare the type specific encoding of the data type.
    ' Use 'OpcEncodingMaskKind.Explicit' to enable the use of optional fields that are
    ' compatible with both the DataTypeDefinition attribute And the Type Dictionary.
    <OpcDataType("ns=2;s=MachineJob")>
    <OpcDataTypeEncoding("ns=2;s=MachineJob.Binary", Type:=OpcEncodingType.Binary)>
    <OpcDataTypeEncodingMask(OpcEncodingMaskKind.Explicit)>
    Public Class MachineJob
        ' Using optional fields with OpcEncodingMaskKind.Explicit requires to declare a
        ' Opc.UaFx.Bit field for each optional field at the beginning of the data type, where
        ' the order of the Bit fields must correspond to the order of the optional fields.
        Public Property DurationSpecified As Bit
            Get
                Return Me.Duration.HasValue
            End Get
            Set(value As Bit)
                Me.Duration = If(value, If(Me.Duration, 0), CType(Nothing, Integer?))
            End Set
        End Property

        ' Additionally, a Bit array must be declared that reserves the (unused)
        ' remaining bits of the 32-bit field.
        <OpcDataTypeMemberLength(31UI)>
        Public Property Reserved1 As Bit()

        Public Property Number As String

        ' Use the 'OpcDataTypeMemberSwitchAttribute' to declare an optional field, so its
        ' presence depends on the value of another field.
        ' For compatibility with the DataTypeDefinition attribute, the first optional field
        ' must refer to the first Bit field, And so on.
        <OpcDataTypeMemberSwitch(NameOf(DurationSpecified))>
        Public Property Duration As Integer?

        Public Property EstimatedDuration As Integer

        Public Property InProcess As Boolean

        ' Use the 'OpcDataTypeMemberLengthAttribute' to define the length of a fixed array field.
        <OpcDataTypeMemberLength(4)>
        Public Property CuttingPositions As Integer()

        Public Property RequiredSetup As MachineSetup

        Public Property ScheduleTime As Date
    End Class
End Namespace
