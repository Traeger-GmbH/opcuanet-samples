' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports Opc.UaFx
Imports Opc.UaFx.Server

Namespace HDA.Sql
    ''' <summary>
    ''' This sample demonstrates how to setup an OPC UA server to provide HDA via SQL to its
    ''' nodes provided.
    ''' </summary>
    Public Class Program
        '---------- Public static methods ----------

        Public Shared Sub Main(ByVal args As String())
            Dim manager = New SampleNodeManager()
            ' If the server domain name does not match localhost just replace it
            ' e.g. with the IP address or name of the server machine.

            Dim server As OpcServer = New OpcServer("opc.tcp://localhost:4840/SampleServer", manager)

            ' NOTE: All HDA specific code will be found in the SampleNodeManager.vb.

            server.Start()
            CreateHistoryEntries(manager)
            Console.Write("Server started - press any key to exit.")
            Console.ReadKey(True)
            server.Stop()
        End Sub

        '---------- Private static methods ----------

        Private Shared Sub CreateHistoryEntries(ByVal manager As SampleNodeManager)
            Dim random = New Random()

            If manager.PositionHistorian.History.IsEmpty() Then
                CreateHistoryEntries( _
                        manager.PositionHistorian, _
                        Function(modifier) random.[Next](-1000, 1000) / modifier)
            End If

            manager.PositionHistorian.AutoUpdateHistory = True

            If manager.TemperatureHistorian.History.IsEmpty() Then
                CreateHistoryEntries( _
                        manager.TemperatureHistorian, _
                        Function(modifier) CDbl(random.[Next](-70, 150)) / modifier)
            End If

            manager.TemperatureHistorian.AutoUpdateHistory = True
        End Sub

        Private Shared Sub CreateHistoryEntries(Of T)( _
                ByVal historian As SampleHistorian, _
                ByVal nextValue As Func(Of Integer, T))                
            For second As Integer = 0 To 60 - 1
                Dim timestamp = DateTime.UtcNow.Date.AddHours(6).AddSeconds(second)
                Dim modifier = Math.Max(10, second) / 10
                Dim value = New OpcHistoryValue(nextValue(modifier), timestamp)

                If (second Mod 30) = 0 Then
                    historian.ModifiedHistory.Add(value.CreateModified( _
                            OpcHistoryModificationType.Delete, "Anonymous", value.Timestamp))
                Else
                    historian.History.Add(value)
                End If
            Next
        End Sub
    End Class
End Namespace
