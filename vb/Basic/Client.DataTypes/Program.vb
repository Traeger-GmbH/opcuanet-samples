'Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports Opc.UaFx.Client
Imports Opc.UaFx.ServerDataTypes.Machines

Namespace DataTypes
    ''' <summary>
    ''' This sample demonstrates how to consume structured data types defined and used by
    ''' the OPC UA server.
    ''' </summary>
    Public Class Program
        '---------- Public static methods ----------

        Public Shared Sub Main(ByVal args As String())
            '''' If the server domain name does not match localhost just replace it
            '''' e.g. with the IP address or name of the server machine.
            Dim client = New OpcClient("opc.tcp://localhost:4840/SampleServer")
            client.Connect()

            '
            ' The data types used in following lines were generated using the OPC Watch.
            ' https://docs.traeger.de/en/software/sdk/opc-ua/net/client.development.guide#generate-data-types
            '

            Dim job = client.ReadNode("ns=2;s=Machines/Machine_1/Job").As(Of MachineJob)()
            Console.WriteLine("Machine 1 - Job")
            PrintJob(job)

            Dim order = client.ReadNode("ns=2;s=Machines/Machine_2/Order").As(Of ManufacturingOrder)()

            Console.WriteLine()
            Console.WriteLine("Machine 2 - Order")
            Console.WriteLine(".Order = {0}", order.Order)
            Console.WriteLine(".Article = {0}", order.Article)

            Console.WriteLine()
            Console.WriteLine("Machine 2 - Order, Jobs")

            For Each orderJob In order.Jobs
                PrintJob(orderJob)
                Console.WriteLine("-"c)
            Next

            client.Disconnect()
            Console.ReadKey(True)
        End Sub

        '---------- Private static methods ----------

        Private Shared Sub PrintJob(ByVal job As MachineJob)
            Console.WriteLine(".Number = {0}", job.Number)

            If job.DurationSpecified Then
                Console.WriteLine(".Duration = {0} (Estimated = {1})", job.Duration, job.EstimatedDuration)
            End If

            Console.WriteLine(".In-Process = {0}", job.InProcess)
            Console.WriteLine(".Required-Setup = {0}", CType(job.RequiredSetup, MachineSetup))
            Console.WriteLine(".Schedule-Setup = {0}", job.ScheduleTime)
        End Sub
    End Class
End Namespace
