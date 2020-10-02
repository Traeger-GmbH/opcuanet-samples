// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace DataTypes
{
    using System;
    using Opc.UaFx;

    // Use the 'FlagsAttribtue' to declare that the members can be combined (note that the value needs to be a multiple of two: 2^n).
    // -> The OPC UA SDK defines the enumeration using a 'EnumValues' property node
    // Use the 'OpcDataTypeAttribute' to declare the type as a data type used in OPC UA as well.
    [Flags]
    [OpcDataType("ns=2;s=MachineSetup")]
    public enum MachineSetup : int
    {
        Manual = 0,
        Corrugator = 1,
        Cutter = 2,

        // Use the 'OpcEnumMemberAttribute' to rename and describe an enum member.
        // Note: Descriptions are only supported for Flag-Enumerations.
        [OpcEnumMember("BW-Printer", Description = "Supports only A1!")]
        Printer1 = 4,

        // Use the 'OpcEnumMemberAttribute' to rename and describe an enum member.
        // Note: Descriptions are only supported for Flag-Enumerations.
        [OpcEnumMember("Color-Printer", Description = "Supports only A2!")]
        Printer2 = 8,

        Laminator = 16,

        // Use the 'OpcEnumMemberAttribute' to rename and describe an enum member.
        // Note: Descriptions are only supported for Flag-Enumerations.
        [OpcEnumMember("Simple Packager", Description = "Delivery packaging line")]
        Packager = Corrugator | Cutter,

        // Use the 'OpcEnumMemberAttribute' to rename and describe an enum member.
        // Note 1: Descriptions are only supported for Flag-Enumerations.
        // Note 2: A missing description on Flag-Enum-Members result into a default description
        //         which enlists the values combined.
        [OpcEnumMember("Display Packager")]
        DisplayLine = Packager | Printer2 | Laminator,

        Finisher = Printer1 | Laminator
    }
}
