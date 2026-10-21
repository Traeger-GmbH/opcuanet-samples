' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports Opc.UaFx
Imports Opc.UaFx.Client
Imports Opc.UaFx.Server

' The following assembly attribute controls whether the obfuscator shall use string encryption.
' However this option does not need to be specified explicitly, because of string encryption is
' enabled / used by default. Therefore this attribute can be used to disable the string encryption
' feature. To disable the string encryption just uncomment the following lines of code.
' Online Reference: https://documentation.help/Eazfuscator.NET/ch04s06.html
'
'''' Imports System.Reflection
'''' <Assembly: Obfuscation(Feature:="string encryption", Exclude:=True)>

Namespace LicenseUsage
    ''' <summary>
    ''' This sample demonstrates how to apply the license keys of the SDK and how to protect them by
    ''' string encryption when the application is obfuscated, here using Eazfuscator.NET.
    ''' </summary>
    ''' <remarks>The Eazfuscator is part of the projects configuration and is executed whenever
    ''' this project is build using the "Release" configuration.</remarks>
    Public Class Program
        '---------- Public static methods ----------

        Public Shared Sub Main()
            ' Use the following line to activate/use the license purchased for ...
            '
            ' - OPC UA Client .NET SDK – Branch Lic.
            ' - OPC UA Client .NET SDK – Single Dev. Lic.
            ' - OPC (UA) Client .NET SDK – Branch Lic.
            ' - OPC (UA) Client .NET SDK – Single Dev. Lic.
            '
            Opc.UaFx.Client.Licenser.LicenseKey = "replace this string with the license key"

            ' Use the following line to activate/use a license purchased for ...
            '
            ' - OPC UA Server .NET SDK – Branch Lic.
            ' - OPC UA Server .NET SDK – Single Dev. Lic.
            '
            Opc.UaFx.Server.Licenser.LicenseKey = "replace this string with the license key"

            ' Use the following line to activate/use a license purchased for ...
            '
            ' - OPC UA Client & Server .NET SDK – Branch Lic.
            ' - OPC UA Client & Server .NET SDK – Single Dev. Lic.
            ' - OPC (UA) CL&UA SV .NET SDK – Branch Lic.
            ' - OPC (UA) CL&UA SV .NET SDK – Single Dev. Lic.
            '
            Opc.UaFx.Licenser.LicenseKey = "replace this string with the license key"

            Using server As New OpcServer(
                    "opc.tcp://localhost:4840",
                    New OpcDataVariableNode("Hello", value:="Hello World!"))

                server.Start()

                Using client As New OpcClient(server.Address)
                    client.Connect()
                    Console.WriteLine(client.ReadNode("ns=2;s=Hello"))
                End Using
            End Using

            Console.WriteLine("Press any key to exit.")
            Console.ReadKey(True)
        End Sub
    End Class
End Namespace
