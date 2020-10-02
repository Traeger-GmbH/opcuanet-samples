// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace DataTypes
{
    using System;
    using Opc.UaFx;

    // Use the 'OpcDataTypeAttribute' to declare the type as a data type used in OPC UA as well.
    [OpcDataType("ns=2;s=ManufacturingOrder")]
    [OpcDataTypeEncoding("ns=2;s=ManufacturingOrder.Binary", Type = OpcEncodingType.Binary)]
    public class ManufacturingOrder
    {
        private MachineJob[] jobs;


        public string Article
        {
            get;
            set;
        }

        public string Order
        {
            get;
            set;
        }


        public int JobsLength
        {
            get => this.Jobs.Length;
            set => Array.Resize(ref this.jobs, value);
        }

        // Use the 'OpcDataTypeMemberLengthAttribute' to define the length of a variable array
        // field using the value of another field.
        [OpcDataTypeMemberLength(nameof(JobsLength))]
        public MachineJob[] Jobs
        {
            get => this.jobs;
            set => this.jobs = value;
        }
    }
}
