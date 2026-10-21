' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Diagnostics

Imports Opc.UaFx
Imports Opc.UaFx.Client

Namespace Methods
    ''' <summary>
    ''' This sample demonstrates how to perform method calls using the OPC UA server
    ''' provided method nodes.
    ''' </summary>
    Public Class Program
        '---------- Public static methods ----------

        Public Shared Sub Main(args As String())
            ' If the server domain name does not match localhost just replace it
            ' e.g. with the IP address or name of the server machine.

            Dim client = New OpcClient("opc.tcp://localhost:4840/SampleServer")
            client.Connect()

            ' Call a method using its objectId and methodId passing the parameters required.
            client.CallMethod("ns=2;s=Methods", "ns=2;s=Methods/Hello", "John")

            ' Call a method using its objectId and methodId passing the typed parameters required.
            client.CallAction(Of String)("ns=2;s=Methods", "ns=2;s=Methods/Hello", "May")

            ' Call a method using its objectId and methodId passing the parameters required.
            Dim sum1 = CInt(client.CallMethod("ns=2;s=Methods", "ns=2;s=Methods/Add", 5, 3)(0))

            ' Call a method using its objectId and methodId passing the typed parameters required.
            ' Using this way the user code does not need to manually select the result from the
            ' results and cast it according to the type of value returned.
            Dim sum2 = client.CallFunc(Of Integer, Integer, Integer)("ns=2;s=Methods", "ns=2;s=Methods/Add", 5, 3)

            ' Call a method using its objectId and methodId passing the parameters required.
            Dim diff1 = CInt(client.CallMethod("ns=2;s=Methods", "ns=2;s=Methods/Subtract", 8, 7)(0))

            ' Call a method using its objectId and methodId passing the typed parameters required.
            ' Using this way the user code does not need to manually select the result from the
            ' results and cast it according to the type of value returned.
            Dim diff2 = client.CallFunc(Of Integer, Integer, Integer)("ns=2;s=Methods", "ns=2;s=Methods/Subtract", 8, 7)

            Console.WriteLine("Starting machines in sequence.")
            Dim watch = Stopwatch.StartNew()

            ' It is possible to call multiple methods using one request, like the following one is used
            ' to start multiple machines using just one request.
            client.CallMethods(
                    New OpcCallMethod("ns=2;s=Machines/Machine_1", "ns=2;s=Machines/Machine_1/StartMachine"),
                    New OpcCallMethod("ns=2;s=Machines/Machine_2", "ns=2;s=Machines/Machine_2/StartMachine"),
                    New OpcCallMethod("ns=2;s=Machines/Machine_3", "ns=2;s=Machines/Machine_3/StartMachine"))

            watch.Stop()
            Console.WriteLine("Started machines after: {0} ms", watch.ElapsedMilliseconds)

            watch.Restart()
            Console.WriteLine("Stopping machines in sequence.")

            ' It is possible to call multiple methods using one request, like the following one is used
            ' to start multiple machines using just one request.
            client.CallMethods(
                    New OpcCallMethod("ns=2;s=Machines/Machine_1", "ns=2;s=Machines/Machine_1/StopMachine"),
                    New OpcCallMethod("ns=2;s=Machines/Machine_2", "ns=2;s=Machines/Machine_2/StopMachine"),
                    New OpcCallMethod("ns=2;s=Machines/Machine_3", "ns=2;s=Machines/Machine_3/StopMachine"))

            watch.Stop()
            Console.WriteLine("Stopped machines after: {0} ms", watch.ElapsedMilliseconds)

            watch.Restart()
            Console.WriteLine("Starting machines in parallel.")

            ' It the server provides asynchronous methods (the methods do not necessarily express
            ' that in their name) the clients response to "CallMethods" is processed much faster.
            client.CallMethods(
                    New OpcCallMethod("ns=2;s=Machines/Machine_1", "ns=2;s=Machines/Machine_1/StartMachineAsync"),
                    New OpcCallMethod("ns=2;s=Machines/Machine_2", "ns=2;s=Machines/Machine_2/StartMachineAsync"),
                    New OpcCallMethod("ns=2;s=Machines/Machine_3", "ns=2;s=Machines/Machine_3/StartMachineAsync"))

            watch.Stop()
            Console.WriteLine("Started machines after: {0} ms", watch.ElapsedMilliseconds)

            watch.Restart()
            Console.WriteLine("Stopping machines in parallel.")

            ' It the server provides asynchronous methods (the methods do not necessarily express
            ' that in their name) the clients response to "CallMethods" is processed much faster.
            client.CallMethods(
                    New OpcCallMethod("ns=2;s=Machines/Machine_1", "ns=2;s=Machines/Machine_1/StopMachineAsync"),
                    New OpcCallMethod("ns=2;s=Machines/Machine_2", "ns=2;s=Machines/Machine_2/StopMachineAsync"),
                    New OpcCallMethod("ns=2;s=Machines/Machine_3", "ns=2;s=Machines/Machine_3/StopMachineAsync"))

            watch.Stop()
            Console.WriteLine("Stopped machines after: {0} ms", watch.ElapsedMilliseconds)

            client.Disconnect()
            Console.ReadKey(True)
        End Sub
    End Class
End Namespace
