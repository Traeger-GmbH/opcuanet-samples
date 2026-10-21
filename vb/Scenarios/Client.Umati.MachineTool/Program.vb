' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports Opc.UaFx.Client

Namespace Umati
    ''' <summary>
    ''' This use case demonstrates how to receive and confirm alerts of a machine tool according to
    ''' the OPC UA companion specification for machine tools (umati).
    ''' </summary>
    Public Class Program
        Public Shared Sub Main()
            Using client As New OpcClient("opc.tcp://localhost:4840")
                client.Connect()

                ' Resolve namespaceIndex of namespace.
                MachineTool.Namespace.Resolve(client)

                Dim filter = OpcFilter.Using(client) _
                        .FromEvents(MachineTool.AlertType) _
                        .Select()

                client.SubscribeEvent(
                        Global.Opc.Ua.ObjectIds.Server,
                        filter,
                        Sub(sender As Object, e As OpcEventReceivedEventArgs)
                            Dim alert = TryCast(e.Event, Alert)

                            If alert IsNot Nothing Then
                                Console.WriteLine(New Guid(alert.EventId.ToArray()).ToString())
                                Console.WriteLine("- Source: {0}", alert.SourceName)
                                Console.WriteLine("- ErrorCode: {0}", alert.ErrorCode)

                                alert.Confirm(client, "Confirmed by the operator.")
                            End If
                        End Sub)

                Console.WriteLine("Press any key to exit.")
                Console.ReadKey(True)
            End Using
        End Sub
    End Class
End Namespace
