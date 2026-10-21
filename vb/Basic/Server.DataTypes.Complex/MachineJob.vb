' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports Opc.UaFx

Namespace DataTypes.Complex
    ' Use the 'OpcDataTypeAttribute' to declare the type as a data type used in OPC UA as well.
    ' Use the 'OpcDataTypeEncodingAttribute' to declare the type specific encoding of the data type.
    <OpcDataType("ns=2;s=MachineJob")>
    <OpcDataTypeEncoding("ns=2;s=MachineJob.Binary", Type:=OpcEncodingType.Binary)>
    Public Class MachineJob
        Public Property Number As String

        Public Property Duration As Integer

        Public Property EstimatedDuration As Integer

        Public Property InProcess As Boolean

        Public Property RequiredSetup As MachineSetup

        Public Property ScheduleTime As DateTime
    End Class
End Namespace
