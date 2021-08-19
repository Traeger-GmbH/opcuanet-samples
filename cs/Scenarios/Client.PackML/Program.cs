namespace Client
{
    using System;
    using System.Linq;

    using Opc.UaFx;
    using Opc.UaFx.Client;

    public class Program
    {
        public static void Main()
        {
            using (var client = new OpcClient("opc.tcp://localhost:4840")) {
                client.Connect();

                PackML.Namespace.Resolve(client);
                PackML.RegisterTypes();

                var ids = client.TranslatePaths(
                        Opc.Ua.ObjectIds.ObjectsFolder,
                        new[] { PackML.GetName("PackMLObjects"), "Machine1" },
                        new[] { PackML.GetName("PackMLObjects"), "Machine1", PackML.GetName("RemoteCommand") }).ToArray();

                var data = GetRemoteInterfaceData();
                client.CallAction(ids[0] /* Machine1 */, ids[1] /* RemoteCommand */, data);

                Console.WriteLine("Press any key to exit.");
                Console.ReadKey(true);
            }
        }

        private static PackMLRemoteInterfaceData[] GetRemoteInterfaceData()
        {
            return new PackMLRemoteInterfaceData[] {
                new PackMLRemoteInterfaceData {
                    Number = 100,
                    ControlCmdNumber = 1001,
                    CmdValue = 1,
                    Parameter = new PackMLDescriptorData[] {
                        new PackMLDescriptorData {
                            ID = 1,
                            Name = "A",
                            Unit = new OpcEngineeringUnitInfo(4408652, "°C", "degree Celsius"),
                            Value = 230.7f
                        },
                        new PackMLDescriptorData {
                            ID = 2,
                            Name = "B",
                            Unit = new OpcEngineeringUnitInfo(4408652, "°C", "degree Celsius"),
                            Value = 210.2f
                        }
                    }
                },
                new PackMLRemoteInterfaceData {
                    Number = 101,
                    ControlCmdNumber = 1002,
                    CmdValue = 2,
                    Parameter = new PackMLDescriptorData[] {
                        new PackMLDescriptorData {
                            ID = 1,
                            Name = "X",
                            Unit = new OpcEngineeringUnitInfo(5066068, "mm", "millimetre"),
                            Value = 1205
                        },
                        new PackMLDescriptorData {
                            ID = 2,
                            Name = "Y",
                            Unit = new OpcEngineeringUnitInfo(5066068, "mm", "millimetre"),
                            Value = 115
                        },
                        new PackMLDescriptorData {
                            ID = 3,
                            Name = "Z",
                            Unit = new OpcEngineeringUnitInfo(5066068, "mm", "millimetre"),
                            Value = 12
                        }
                    }
                },
            };
        }
    }
}
