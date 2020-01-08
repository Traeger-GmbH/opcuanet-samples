' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System

Imports Opc.UaFx
Imports Opc.UaFx.Client

Namespace HDA
    ''' <summary>
    ''' This sample demonstrates how to perform history data access (HDA) on the data provided by
    ''' the OPC UA server.
    ''' </summary>
    Public Class Program
        '---------- Public static methods ----------

        Public Shared Sub Main(args As String())
            '''' If the server domain name does not match localhost just replace it
            '''' e.g. with the IP address or name of the server machine.

            Dim client As New OpcClient("opc.tcp://localhost:4840/SampleServer")
            client.Connect()

            ' Read the historical 'Raw' data.
            ' - for one specific node.
            ' - the whole history in one request.
            Dim rawHistory = client.ReadNodeHistory(
                    DateTime.UtcNow.Date.AddHours(6),
                    DateTime.UtcNow.Date.AddHours(6).AddSeconds(10),
                    "ns=2;s=Machine_1/Position")

            Console.WriteLine("Read the historical 'Raw' data...")

            For Each item As OpcHistoryValue In rawHistory
                Console.WriteLine("{0} - {1}", item.Timestamp, item.Value)
            Next

            ' Read the historical 'Raw' data page wise.
            ' - for one specific node.
            ' - the whole history partitioned into multiple requests.
            Dim rawHistoryNavigator = client.ReadNodeHistory(
                    DateTime.UtcNow.Date.AddHours(6),
                    DateTime.UtcNow.Date.AddHours(6).AddSeconds(10),
                    2,
                    "ns=2;s=Machine_1/Position")

            Console.WriteLine("Read the historical 'Raw' data page wise...")

            Do
                For Each item As OpcHistoryValue In rawHistoryNavigator
                    Console.WriteLine("{0} - {1}", item.Timestamp, item.Value)
                Next

                Console.Write("Press any key to read the next page...")
                Console.ReadKey()
                Console.WriteLine()
            Loop While rawHistoryNavigator.MoveNextPage()

            ' Read the historical 'ModifiedRaw' data.
            ' - for one specific node.
            ' - the whole history in one request.
            Dim modifiedRawHistory = client.ReadNodeHistoryModified(
                    DateTime.UtcNow.Date.AddHours(6),
                    DateTime.UtcNow.Date.AddHours(6).AddSeconds(300),
                    "ns=2;s=Machine_1/Position")

            Console.WriteLine("Read the historical 'ModifiedRaw' data...")

            For Each item As OpcModifiedHistoryValue In modifiedRawHistory
                Console.WriteLine("{0} - {1}", item.Timestamp, item.Value)
                Console.WriteLine("\t[{0}] by {1}", item.ModificationType, item.ModificationUserName)
            Next

            ' Read the historical 'ModifiedRaw' data page wise.
            ' - for one specific node.
            ' - the whole history partitioned into multiple requests.
            Dim modifiedRawHistoryNavigator = client.ReadNodeHistoryModified(
                    DateTime.UtcNow.Date.AddHours(6),
                    DateTime.UtcNow.Date.AddHours(6).AddSeconds(300),
                    2,
                    "ns=2;s=Machine_1/Position")

            Console.WriteLine("Read the historical 'ModifiedRaw' data page wise...")

            Do
                For Each item As OpcModifiedHistoryValue In modifiedRawHistoryNavigator
                    Console.WriteLine("{0} - {1}", item.Timestamp, item.Value)
                    Console.WriteLine("\t[{0}] by {1}", item.ModificationType, item.ModificationUserName)
                Next

                Console.Write("Press any key to read the next page...")
                Console.ReadKey()
                Console.WriteLine()
            Loop While modifiedRawHistoryNavigator.MoveNextPage()

            ' Read the historical data 'at time'.
            ' - for one specific node.
            ' - the whole history for each time specified.
            Dim atTimeHistory = client.ReadNodeHistoryAtTime(
                    New DateTime() _
                    {
                        DateTime.UtcNow.Date.AddHours(6),
                        DateTime.UtcNow.Date.AddHours(6).AddSeconds(10),
                        DateTime.UtcNow.Date.AddHours(6).AddSeconds(20),
                        DateTime.UtcNow.Date.AddHours(6).AddSeconds(30),
                        DateTime.UtcNow.Date.AddHours(6).AddSeconds(40),
                        DateTime.UtcNow.Date.AddHours(6).AddSeconds(50)
                    },
                    "ns=2;s=Machine_1/Position")

            Console.WriteLine("Read the historical data 'at time'...")

            For Each item As OpcHistoryValue In atTimeHistory
                Console.WriteLine("{0} - {1}", item.Timestamp, item.Value)
            Next

            ' Read the historical data 'processed'.
            ' - for one specific node.
            ' - the whole history is processed on the server side.
            Dim processedHistory = client.ReadNodeHistoryProcessed(
                    DateTime.UtcNow.Date.AddHours(6),
                    DateTime.UtcNow.Date.AddHours(6).AddMinutes(30),
                    OpcAggregateType.Maximum,
                    "ns=2;s=Machine_1/Position")

            Console.WriteLine("Read the historical data 'processed'...")

            For Each item As OpcHistoryValue In processedHistory
                Console.WriteLine("{0} - {1}", item.Timestamp, item.Value)
            Next

            client.Disconnect()
            Console.ReadKey(True)
        End Sub
    End Class
End Namespace
