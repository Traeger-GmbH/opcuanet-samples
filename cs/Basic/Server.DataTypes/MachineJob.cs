// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace DataTypes
{
    using System;
    using Opc.UaFx;

    // Use the 'OpcDataTypeAttribute' to declare the type as a data type used in OPC UA as well.
    [OpcDataType("ns=2;s=MachineJob")]
    // Use the 'OpcDataTypeEncodingAttribute' to declare the type specific encoding of the data type.
    [OpcDataTypeEncoding("ns=2;s=MachineJob.Binary", Type = OpcEncodingType.Binary)]
    public class MachineJob
    {
        public string Number
        {
            get;
            set;
        }

        public bool DurationSpecified
        {
            get => this.Duration.HasValue;
            set => this.Duration = value ? 0 : (int?)null;
        }

        // Use the 'OpcDataTypeMemberSwitch' to discard a field depending on the value
        // of another field.
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
