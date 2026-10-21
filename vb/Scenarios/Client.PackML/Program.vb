' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Linq

Imports Opc.UaFx
Imports Opc.UaFx.Client

Namespace Client
    ''' <summary>
    ''' This use case demonstrates how to call a method of the PackML companion specification with
    ''' structured arguments, using data types generated from the PackML nodeset.
    ''' </summary>
    Public Class Program
        Public Shared Sub Main()
            Using client As New OpcClient("opc.tcp://localhost:4840")
                client.Connect()

                PackML.Namespace.Resolve(client)
                PackML.RegisterTypes()

                Dim ids = client.TranslatePaths(
                        Global.Opc.Ua.ObjectIds.ObjectsFolder,
                        New OpcName() {PackML.GetName("PackMLObjects"), "Machine1"},
                        New OpcName() {PackML.GetName("PackMLObjects"), "Machine1", PackML.GetName("RemoteCommand")}).ToArray()

                Dim data = GetRemoteInterfaceData()
                client.CallAction(
                        ids(0), ' Machine1
                        ids(1), ' RemoteCommand
                        data)

                Console.WriteLine("Press any key to exit.")
                Console.ReadKey(True)
            End Using
        End Sub

        Private Shared Function GetRemoteInterfaceData() As PackMLRemoteInterfaceData()
            Return New PackMLRemoteInterfaceData() {
                New PackMLRemoteInterfaceData With {
                    .Number = 100,
                    .ControlCmdNumber = 1001,
                    .CmdValue = 1,
                    .Parameter = New PackMLDescriptorData() {
                        New PackMLDescriptorData With {
                            .ID = 1,
                            .Name = "A",
                            .Unit = New OpcEngineeringUnitInfo(4408652, "°C", "degree Celsius"),
                            .Value = 230.7F
                        },
                        New PackMLDescriptorData With {
                            .ID = 2,
                            .Name = "B",
                            .Unit = New OpcEngineeringUnitInfo(4408652, "°C", "degree Celsius"),
                            .Value = 210.2F
                        }
                    }
                },
                New PackMLRemoteInterfaceData With {
                    .Number = 101,
                    .ControlCmdNumber = 1002,
                    .CmdValue = 2,
                    .Parameter = New PackMLDescriptorData() {
                        New PackMLDescriptorData With {
                            .ID = 1,
                            .Name = "X",
                            .Unit = New OpcEngineeringUnitInfo(5066068, "mm", "millimetre"),
                            .Value = 1205
                        },
                        New PackMLDescriptorData With {
                            .ID = 2,
                            .Name = "Y",
                            .Unit = New OpcEngineeringUnitInfo(5066068, "mm", "millimetre"),
                            .Value = 115
                        },
                        New PackMLDescriptorData With {
                            .ID = 3,
                            .Name = "Z",
                            .Unit = New OpcEngineeringUnitInfo(5066068, "mm", "millimetre"),
                            .Value = 12
                        }
                    }
                }
            }
        End Function
    End Class
End Namespace
