' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports Opc.UaFx.Client

Namespace Umati
    ''' <summary>
    ''' This use case demonstrates how to subscribe to the alarms of a machine tool according to the
    ''' legacy umati information model and read their specific fields.
    ''' </summary>
    Public Class Program
        Public Shared Sub Main()
            Using client As New OpcClient("opc.tcp://localhost:4840")
                client.Connect()

                Dim filter = OpcFilter.Using(client) _
                        .FromEvents("ns=2;i=1042") _
                        .Select()

                client.SubscribeEvent(
                        "ns=3;i=5031",
                        filter,
                        Sub(sender As Object, e As OpcEventReceivedEventArgs)
                            Dim alarm = TryCast(e.Event, MachineToolAlarmCondition)

                            If alarm IsNot Nothing Then
                                Console.WriteLine(New Guid(alarm.EventId.ToArray()).ToString())
                                Console.WriteLine("- AlarmIdentifier: {0}", alarm.AlarmIdentifier)
                                Console.WriteLine("- AuxParameters: {0}", alarm.AuxParameters)
                            End If
                        End Sub)

                Console.WriteLine("Press any key to exit.")
                Console.ReadKey(True)
            End Using
        End Sub
    End Class
End Namespace
