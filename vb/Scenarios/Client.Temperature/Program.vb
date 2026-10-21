Imports System
Imports System.Threading

Imports Opc.UaFx.Client

Namespace Client
    ''' <summary>
    ''' This sample demonstrates how to connect to an OPC UA server and read the value of a node
    ''' once per second.
    ''' </summary>
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
