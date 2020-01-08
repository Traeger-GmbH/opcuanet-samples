// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace DataTypeNode
{
    using System;
    using Opc.UaFx;

    /// <summary>
    /// Defines a sample enumeration which is used for a custom data type node.
    /// </summary>
    // The node identifier is retrieved from the OpcDataTypeAttribute used here.
    // All subsequent usage of the MachineStatus enumeration like defining a custom
    // data type node or using the enumeration type as the data type of data variable 
    // node will query the node identifier information from this attribute.
    // IMPORTANT: The description of the enumeration members is only used when a the
    //            enumeration type defines flags as enumeration members values.
    //            This also implies that the enumeration needs to define the
    //            FlagsAttribute defined by the .NET Framework.
    [Flags]
    [OpcDataType(id: "MachineStatus", namespaceIndex: 2)]
    internal enum MachineStatus : int
    {
        // A enum member name does not have to match the name used by the attribute.
        [OpcEnumMember(
                "Initializing",
            Description = "The machine is about to configure.")]
        Unknown = 0,

        [OpcEnumMember(
                "Stopped",
                Description
                        = "The machine has been stopped. This can be upon no more pending "
                        + "orders, upon failure or upon an operator stopped the machine.")]
        Stopped = 1,

        [OpcEnumMember(
                "Started",
                Description
                        = "The machine is started. This can be upon autostart is enabled "
                        + "or upon an operator started the machine.")]
        Started = 2,

        // A enum member name can also define whitespaces or different capital letters as the
        // language defined enum member name.
        [OpcEnumMember(
                "Waiting for Orders",
                Description
                        = "The machine is in idle and waits for additional orders.")]
        WaitingForOrders = 3,

        // There is also no need to define the optional Description property of the attribute.
        // Additionally in this case the description of the 'SuspendedByOperator' member is used
        // here to, because by specification there can only one member define a specific value.
        // As we have here (e.g. historical newly / additionally defined) enum members with the
        // same value the framework will combine the two by using a combination of their names
        // (separated by ", ") and combining their descriptions (if available, separated by " ").
        // The combination of the members is only used in case there the Flags attribute is
        // not set. In case there the Flags attribute is set and the description is undefined,
        // the description is automatically build up from the combination of member names used
        // for the value.
        [OpcEnumMember("Suspended")]
        Suspended = 4,

        [OpcEnumMember(
                "Suspended by Operator",
                Description = "The machine operator suspended the order processing.")]
        SuspendedByOperator = Suspended,

        [OpcEnumMember(
                "Suspended by Machine",
                Description = "The machine suspended itself upon material load issues.")]
        SuspendedByMachine = 5,

        // The missing description is set up using the name of the Started and Suspended members.
        [OpcEnumMember("IDLE")]
        Idle = Started | Suspended
    }
}
