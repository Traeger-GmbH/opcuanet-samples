' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports Opc.UaFx

Namespace DataTypes.Complex
    ' Use the 'OpcDataTypeAttribute' to declare the type as a data type used in OPC UA as well.
    <OpcDataType("ns=2;s=ManufacturingOrder")>
    <OpcDataTypeEncoding("ns=2;s=ManufacturingOrder.Binary", Type:=OpcEncodingType.Binary)>
    Public Class ManufacturingOrder
        Private _jobs As MachineJob() = New MachineJob(2) {}


        Public Property Article As String

        Public Property Order As String


        Public Property JobsLength As Integer
            Get
                Return Me._jobs.Length
            End Get
            Set(value As Integer)
                Array.Resize(Me._jobs, value)
            End Set
        End Property

        ' Use the 'OpcDataTypeMemberLengthAttribute' to define the length of a variable array
        ' field using the value of another field.
        <OpcDataTypeMemberLength(NameOf(JobsLength))>
        Public Property Jobs As MachineJob()
            Get
                Return Me._jobs
            End Get
            Set(value As MachineJob())
                Me._jobs = value
            End Set
        End Property
    End Class
End Namespace
