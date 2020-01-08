// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace AnalogItemNode
{
    using System;
    using Opc.UaFx.Client;

    /// <summary>
    /// This sample demonstrates how to access and work with nodes of the AnalogItemType.
    /// </summary>
    public class Program
    {
        #region ---------- Public static methods ----------

        public static void Main(string[] args)
        {
            //// If the server domain name does not match localhost just replace it
            //// e.g. with the IP address or name of the server machine.

            var client = new OpcClient("opc.tcp://localhost:4840/SampleServer");
            client.Connect();

            // The mapping of the UNECE codes to OPC UA (OpcEngineeringUnitInfo.UnitId) is available here:
            // http://www.opcfoundation.org/UA/EngineeringUnits/UNECE/UNECE_to_OPCUA.csv
            var temperatureNode = client.BrowseNode("ns=2;s=Machine_1/Temperature") as OpcAnalogItemNodeInfo;


            if (temperatureNode != null) {
                var temperatureUnit = temperatureNode.EngineeringUnit;
                var temperatureRange = temperatureNode.EngineeringUnitRange;

                var temperature = client.ReadNode(temperatureNode.NodeId);

                Console.WriteLine(
                        "Temperature: {0} {1}, Range: {3} {1} to {4} {1} ({2})",
                        temperature.Value,
                        temperatureUnit.DisplayName,
                        temperatureUnit.Description,
                        temperatureRange.Low,
                        temperatureRange.High);
            }

            var pressureNode = client.BrowseNode("ns=2;s=Machine_1/Pressure") as OpcAnalogItemNodeInfo;

            if (pressureNode != null) {
                var pressureUnit = pressureNode.EngineeringUnit;
                var pressureInstrumentRange = pressureNode.InstrumentRange;

                var pressure = client.ReadNode(pressureNode.NodeId);

                Console.WriteLine(
                        "Pressure: {0} {1}, Range: {3} {1} to {4} {1} ({2})",
                        pressure.Value,
                        pressureUnit.DisplayName,
                        pressureUnit.Description,
                        pressureInstrumentRange.Low,
                        pressureInstrumentRange.High);
            }

            client.Disconnect();
            Console.ReadKey(true);
        }

        #endregion
    }
}
