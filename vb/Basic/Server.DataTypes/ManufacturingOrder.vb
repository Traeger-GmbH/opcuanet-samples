'Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports Opc.UaFx

Namespace DataTypes
    'Use the 'OpcDataTypeAttribute' to declare the type as a data type used in OPC UA as well.
    <OpcDataType("ns=2;s=ManufacturingOrder")>
    <OpcDataTypeEncoding("ns=2;s=ManufacturingOrder.Binary", Type:=OpcEncodingType.Binary)>
    Public Class ManufacturingOrder
        Private _jobs As MachineJob()

        Public Property Article As String

        Public Property Order As String

        Public Property JobsLength As Integer
            Get
                Return _jobs.Length
            End Get
            Set(ByVal value As Integer)
                Array.Resize(_jobs, value)
            End Set
        End Property

        'Use the 'OpcDataTypeMemberLengthAttribute' to define the length of a variable array
        'field using the value of another field.
        <OpcDataTypeMemberLength(NameOf(JobsLength))>
        Public Property Jobs As MachineJob()
            Get
                Return _jobs
            End Get
            Set(ByVal value As MachineJob())
                _jobs = value
            End Set
        End Property
    End Class
End Namespace
