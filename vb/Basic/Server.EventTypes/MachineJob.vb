'Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports Opc.UaFx

Namespace EventTypes
    'Use the 'OpcDataTypeAttribute' to declare the type as a data type used in OPC UA as well.
    'Use the 'OpcDataTypeEncodingAttribute' to declare the type specific encoding of the data type.
    <OpcDataType("ns=1;s=MachineJob")>
    <OpcDataTypeEncoding("ns=1;s=MachineJob.Binary", Type:=OpcEncodingType.Binary)>
    Public Class MachineJob
        Public Property Number As String
        Public Property RequiredSetup As MachineSetup
    End Class
End Namespace
