' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports Opc.UaFx

Namespace Server.Playground
    Partial Friend Class NodeManager
        '---------- Private types ----------

        <OpcDataType("ns=2;s=MyEnum")>
        Private Enum MyEnum
            Starting = 0
            Started = 1
            Stopping = 2
            Stopped = 3
            Maintenance = 4
        End Enum

        <Flags>
        <OpcDataType("ns=2;s=MyEnumFlags")>
        Private Enum MyEnumFlags
            None = 0
            CanCut = 1
            CanFold = 2
            CanPrint = 4
            CanCorrugate = 8
            CanLaminate = 16
        End Enum
    End Class
End Namespace
