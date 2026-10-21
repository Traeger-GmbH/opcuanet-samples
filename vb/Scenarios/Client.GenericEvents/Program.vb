' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Collections.Generic
Imports System.Linq

Imports Opc.UaFx
Imports Opc.UaFx.Client

Namespace GenericEvents
    ''' <summary>
    ''' This use case demonstrates how to receive events of a custom event type without an own class
    ''' for its fields, using a generic event class that exposes all selected event fields.
    ''' </summary>
    Public Class Program
        Public Shared Sub Main()
            OpcEvent.RegisterType(Of GenericEvent)("ns=2;i=1")

            Using client As New OpcClient("opc.tcp://localhost:4840")
                client.UseDynamic = True
                client.Connect()

                ' Define a filter to only handle the custom event.
                Dim filter = OpcFilter.Using(client) _
                        .FromEvents(OpcNodeId.Parse("ns=2;i=1")) _
                        .Select()

                Dim subscription = client.SubscribeEvent(
                        "ns=2;s=Machine",
                        filter,
                        Sub(sender As Object, e As OpcEventReceivedEventArgs)
                            Dim genericEvent = TryCast(e.Event, GenericEvent)

                            If genericEvent IsNot Nothing Then
                                Print(genericEvent.GetData())
                            End If
                        End Sub)

                Console.Write("Waiting for events (press any key to exit)...")
                Console.ReadKey(True)
            End Using
        End Sub

        Private Shared Sub Print(data As Dictionary(Of String, Object), Optional level As Integer = 0)
            Dim indent = New String("."c, level * 2)

            For Each item In data
                Dim instance = TryCast(item.Value, OpcDataObject)
                Dim instances = TryCast(item.Value, OpcDataObject())

                If instance IsNot Nothing Then
                    Console.WriteLine(indent & item.Key)

                    Dim instanceData = instance.GetFields().ToDictionary(
                            Function(field) field.Name,
                            Function(field) field.Value)

                    Print(instanceData, level + 1)
                ElseIf instances IsNot Nothing Then
                    For index = 0 To instances.Length - 1
                        Console.WriteLine(indent & "{0}[{1}]", item.Key, index)

                        Dim instanceData = instances(index).GetFields().ToDictionary(
                                Function(field) field.Name,
                                Function(field) field.Value)

                        Print(instanceData, level + 1)
                    Next
                Else
                    Console.WriteLine(indent & "{0} = {1}", item.Key, item.Value)
                End If
            Next
        End Sub
    End Class
End Namespace
