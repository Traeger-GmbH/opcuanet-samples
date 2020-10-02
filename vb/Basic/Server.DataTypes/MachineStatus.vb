'Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports Opc.UaFx

Namespace DataTypes
    'Use the 'OpcDataTypeAttribute' to declare the type as a data type used in OPC UA as well.
    <OpcDataType("ns=2;s=MachineStatus")>
    Public Enum MachineStatus
        Unknown = 0
        Stopped = 1
        Started = 2

        'Use the 'OpcEnumMemberAttribute' to rename an enum member.
        <OpcEnumMember("Paused by Job")>
        WaitingForOrders = 3

        'Use the 'OpcEnumMemberAttribute' to rename an enum member.
        <OpcEnumMember("Paused by Operator")>
        Suspended = 4
    End Enum
End Namespace
