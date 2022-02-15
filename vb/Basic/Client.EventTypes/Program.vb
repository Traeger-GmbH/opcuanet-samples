'Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports Opc.UaFx.Client

Namespace EventTypes
    ''' <summary>
    ''' This sample demonstrates how to consume structured data types defined and used by
    ''' the OPC UA server.
    ''' </summary>
    Public Class Program
        '---------- Public static methods ----------

        Public Shared Sub Main(ByVal args As String())
            'If the server domain name does Not match localhost just replace it
            'e.g. with the IP address Or name of the server machine.

            Dim client = New OpcClient("opc.tcp://localhost:4840/SampleServer")
            client.Connect()

            Dim filter = OpcFilter.Using(client) _
                    .FromEvents("ns=1;s=MachineJobEventType") _
                    .Select()

            client.SubscribeNodes(
                    New OpcSubscribeEvent("ns=1;s=Machines/Machine_1", filter, AddressOf HandleEventReceived),
                    New OpcSubscribeEvent("ns=1;s=Machines/Machine_2", filter, AddressOf HandleEventReceived))

            Console.WriteLine("Connected & subscribed!")

            client.CallMethod(
                    "ns=1;s=Machines/Machine_1",
                    "ns=1;s=Machines/Machine_1/StartJob",
                    New MachineJob With {
                        .Number = "0001",
                        .RequiredSetup = MachineSetup.Laminator
                    })

            client.CallMethod(
                    "ns=1;s=Machines/Machine_2",
                    "ns=1;s=Machines/Machine_2/StartJob",
                    New MachineJob With {
                        .Number = "1002",
                        .RequiredSetup = MachineSetup.Packager
                    })

            Console.ReadKey(True)
            client.Disconnect()
        End Sub

        '---------- Private static methods ----------

        Private Shared Sub HandleEventReceived(ByVal sender As Object, ByVal e As OpcEventReceivedEventArgs)
            Dim jobEvent As MachineJobEvent = TryCast(e.Event, MachineJobEvent)

            If jobEvent IsNot Nothing Then
                Console.WriteLine(e.MonitoredItem.NodeId.ValueAsString)

                Dim job = jobEvent.Job
                Console.WriteLine(".Number = {0}", job.Number)
                Console.WriteLine(".Required-Setup = {0}", job.RequiredSetup)

                Console.WriteLine()
            End If
        End Sub
    End Class
End Namespace
