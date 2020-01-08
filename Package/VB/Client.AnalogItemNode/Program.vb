' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports Opc.UaFx.Client

Namespace AnalogItemNode
    ''' <summary>
    ''' This sample demonstrates how to access and work with nodes of the AnalogItemType.
    ''' </summary>
    Public Class Program
        '---------- Public static methods ----------

        Public Shared Sub Main(args As String())
            '''' If the server domain name does Not match localhost just replace it
            '''' e.g. with the IP address Or name of the server machine.

            Dim client As New OpcClient("opc.tcp://localhost:4840/SampleServer")
            client.Connect()

            '''' The mapping of the UNECE codes to OPC UA (OpcEngineeringUnitInfo.UnitId) is available here:
            '''' http://www.opcfoundation.org/UA/EngineeringUnits/UNECE/UNECE_to_OPCUA.csv

            Dim temperatureNode As OpcAnalogItemNodeInfo
            temperatureNode = CType(client.BrowseNode("ns=2;s=Machine_1/Temperature"), OpcAnalogItemNodeInfo)

            If (Not temperatureNode Is Nothing) Then
                Dim temperatureUnit = temperatureNode.EngineeringUnit
                Dim temperatureRange = temperatureNode.EngineeringUnitRange
                Dim temperature = client.ReadNode(temperatureNode.NodeId)

                Console.WriteLine(
                        "Temperature: {0} {1}, Range: {3} {1} to {4} {1} ({2})",
                        temperature.Value,
                        temperatureUnit.DisplayName,
                        temperatureUnit.Description,
                        temperatureRange.Low,
                        temperatureRange.High)
            End If

            Dim pressureNode As OpcAnalogItemNodeInfo
            pressureNode = CType(client.BrowseNode("ns=2;s=Machine_1/Pressure"), OpcAnalogItemNodeInfo)

            If (Not pressureNode Is Nothing) Then
                Dim pressureUnit = pressureNode.EngineeringUnit
                Dim pressureInstrumentRange = pressureNode.InstrumentRange
                Dim pressure = client.ReadNode(pressureNode.NodeId)

                Console.WriteLine(
                        "Pressure: {0} {1}, Range: {3} {1} to {4} {1} ({2})",
                        pressure.Value,
                        pressureUnit.DisplayName,
                        pressureUnit.Description,
                        pressureInstrumentRange.Low,
                        pressureInstrumentRange.High)
            End If

            client.Disconnect()
            Console.ReadKey(True)
        End Sub
    End Class
End Namespace
