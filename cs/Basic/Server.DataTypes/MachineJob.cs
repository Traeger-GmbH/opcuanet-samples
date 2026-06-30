// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace DataTypes
{
    using System;

    using Opc.UaFx;

    // Use the 'OpcDataTypeAttribute' to declare the type as a data type used in OPC UA as well.
    [OpcDataType("ns=2;s=MachineJob")]
    // Use the 'OpcDataTypeEncodingAttribute' to declare the type specific encoding of the data type.
    [OpcDataTypeEncoding("ns=2;s=MachineJob.Binary", Type = OpcEncodingType.Binary)]
    // Use 'OpcEncodingMaskKind.Explicit' to enable the use of optional fields that are
    // compatible with both the DataTypeDefinition attribute and the Type Dictionary.
    [OpcDataTypeEncodingMask(OpcEncodingMaskKind.Explicit)]
    public class MachineJob
    {
        // Using optional fields with OpcEncodingMaskKind.Explicit requires to declare a
        // Opc.UaFx.Bit field for each optional field at the beginning of the data type, where
        // the order of the Bit fields must correspond to the order of the optional fields.
        public Bit DurationSpecified
        {
            get => this.Duration != null;
            set => this.Duration = value ? (this.Duration ?? 0) : null;
        }

        // Additionally, a Bit array must be declared that reserves the (unused)
        // remaining bits of the 32-bit field.
        [OpcDataTypeMemberLength(31U)]
        public Bit[] Reserved1
        {
            get;
            set;
        }

        public string Number
        {
            get;
            set;
        }

        // Use the 'OpcDataTypeMemberSwitchAttribute' to declare an optional field, so its
        // presence depends on the value of another field.
        // For compatibility with the DataTypeDefinition attribute, the first optional field
        // must refer to the first Bit field, and so on.
        [OpcDataTypeMemberSwitch(nameof(DurationSpecified))]
        public int? Duration
        {
            get;
            set;
        }

        public int EstimatedDuration
        {
            get;
            set;
        }

        public bool InProcess
        {
            get;
            set;
        }

        // Use the 'OpcDataTypeMemberLengthAttribute' to define the length of a fixed array field.
        [OpcDataTypeMemberLength(length: 4)]
        public int[] CuttingPositions
        {
            get;
            set;
        }

        public MachineSetup RequiredSetup
        {
            get;
            set;
        }

        public DateTime ScheduleTime
        {
            get;
            set;
        }
    }
}
