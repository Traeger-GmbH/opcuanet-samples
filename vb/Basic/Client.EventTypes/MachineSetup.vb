'Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports Opc.UaFx

Namespace EventTypes
    'Use the 'FlagsAttribtue' to declare that the members can be combined (note that the value needs to be a multiple of two: 2^n).
    '-> The OPC UA SDK defines the enumeration using a 'EnumValues' property node
    'Use the 'OpcDataTypeAttribute' to declare the type as a data type used in OPC UA as well.
    <Flags>
    <OpcDataType("ns=1;s=MachineSetup")>
    Public Enum MachineSetup
        Manual = 0
        Corrugator = 1
        Cutter = 2

        'Use the 'OpcEnumMemberAttribute' to rename and describe an enum member.
        'Note: Descriptions are only supported For Flag-Enumerations.
        <OpcEnumMember("BW-Printer", Description:="Supports only A1!")>
        Printer1 = 4

        'Use the 'OpcEnumMemberAttribute' to rename and describe an enum member.
        'Note: Descriptions are only supported For Flag-Enumerations.
        <OpcEnumMember("Color-Printer", Description:="Supports only A2!")>
        Printer2 = 8
        Laminator = 16

        'Use the 'OpcEnumMemberAttribute' to rename and describe an enum member.
        'Note: Descriptions are only supported For Flag-Enumerations.
        <OpcEnumMember("Simple Packager", Description:="Delivery packaging line")>
        Packager = Corrugator Or Cutter

        'Use the 'OpcEnumMemberAttribute' to rename and describe an enum member.
        'Note 1: Descriptions are only supported For Flag-Enumerations.
        'Note 2: A missing description On Flag-Enum-Members result into a Default description
        '        which enlists the values combined.
        <OpcEnumMember("Display Packager")>
        DisplayLine = Packager Or Printer2 Or Laminator

        Finisher = Printer1 Or Laminator
    End Enum
End Namespace
