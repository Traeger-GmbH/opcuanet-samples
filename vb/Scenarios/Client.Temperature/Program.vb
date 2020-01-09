Imports System
Imports System.Threading

Imports Opc.UaFx.Client

Namespace Client
    Public Class Program
        Public Shared Sub Main()
            Using client = New OpcClient("opc.tcp://localhost:4840")
                client.Connect()

                While True
                    Dim temperature = client.ReadNode("ns=2;s=Temperature")
                    Console.WriteLine("Current Temperature is {0} °C", temperature)

                    Thread.Sleep(1000)
                End While
            End Using
        End Sub
    End Class
End Namespace
