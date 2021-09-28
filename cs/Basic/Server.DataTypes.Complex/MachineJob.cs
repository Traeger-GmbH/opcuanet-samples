// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace DataTypes.Complex
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

        public int Duration
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
