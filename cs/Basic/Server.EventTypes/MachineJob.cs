// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace EventTypes
{
    using Opc.UaFx;

    // Use the 'OpcDataTypeAttribute' to declare the type as a data type used in OPC UA as well.
    [OpcDataType("ns=1;s=MachineJob")]
    // Use the 'OpcDataTypeEncodingAttribute' to declare the type specific encoding of the data type.
    [OpcDataTypeEncoding("ns=1;s=MachineJob.Binary", Type = OpcEncodingType.Binary)]
    public class MachineJob
    {
        public string Number
        {
            get;
            set;
        }

        public MachineSetup RequiredSetup
        {
            get;
            set;
        }
    }
}
