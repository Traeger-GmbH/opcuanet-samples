' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports Opc.UaFx

Namespace DataTypeNode
    ' The node identifier is retrieved from the OpcDataTypeAttribute used here.
    ' All subsequent usage of the MachineStatus enumeration like defining a custom
    ' data type node or using the enumeration type as the data type of data variable 
    ' node will query the node identifier information from this attribute.
    ' IMPORTANT: The description of the enumeration members is only used when a the
    '            enumeration type defines flags as enumeration members values.
    '            This also implies that the enumeration needs to define the
    '            FlagsAttribute defined by the .NET Framework.
    ''' <summary>
    ''' Defines a sample enumeration which is used for a custom data type node.
    ''' </summary>
    <Flags>
    <OpcDataType("MachineStatus", 2)>
    Friend Enum MachineStatus As Integer
        ' A enum member name does Not have to match the name used by the attribute.
        <OpcEnumMember(
                "Initializing",
                Description:="The machine is about to configure.")>
        Unknown = 0

        <OpcEnumMember(
                "Stopped",
                Description _
                        :="The machine has been stopped. This can be upon no more pending " _
                        + "orders, upon failure or upon an operator stopped the machine.")>
        Stopped = 1

        <OpcEnumMember(
                "Started",
                Description _
                        :="The machine is started. This can be upon autostart is enabled " _
                        + "or upon an operator started the machine.")>
        Started = 2

        ' A enum member name can also define whitespaces Or different capital letters as the
        ' language defined enum member name.
        <OpcEnumMember(
                "Waiting for Orders",
                Description _
                        :="The machine is in idle and waits for additional orders.")>
        WaitingForOrders = 3

        ' There Is also no need to define the optional Description property of the attribute.
        ' Additionally in this case the description of the 'SuspendedByOperator' member is used
        ' here to, because by specification there can only one member define a specific value.
        ' As we have here (e.g. historical newly / additionally defined) enum members with the
        ' same value the framework will combine the two by using a combination of their names
        ' (separated by ", ") And combining their descriptions (if available, separated by " ").
        <OpcEnumMember("Suspended")>
        Suspended = 4

        <OpcEnumMember(
                "Suspended by Operator",
                Description:="The machine operator suspended the order processing.")>
        SuspendedByOperator = Suspended

        <OpcEnumMember(
                "Suspended by Machine",
                Description:="The machine suspended itself upon material load issues.")>
        SuspendedByMachine = 5

        ' The missing description Is set up using the name of the Started And Suspended members.
        <OpcEnumMember("IDLE")>
        Idle = Started Or Suspended
    End Enum
End Namespace
