' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System

Imports Opc.UaFx
Imports Opc.UaFx.Client

Namespace DataTypeNode
    ''' <summary>
    ''' This sample demonstrates how to access and work with nodes which use custom data types.
    ''' </summary>
    Public Class Program
        '---------- Public static methods ----------

        Public Shared Sub Main(args As String())
            '''' If the server domain name does Not match localhost just replace it
            '''' e.g. with the IP address Or name of the server machine.

            Dim client As New OpcClient("opc.tcp://localhost:4840/SampleServer")
            client.Connect()

            Dim statusNode As OpcVariableNodeInfo
            statusNode = CType(client.BrowseNode("ns=2;s=Machine_1/Status"), OpcVariableNodeInfo)

            If (Not statusNode Is Nothing) Then
                Dim statusValues = statusNode.DataType.GetEnumMembers()

                Dim currentStatus = client.ReadNode(statusNode.NodeId)
                Dim currentStatusValue As OpcEnumMember = Nothing

                For Each statusValue As OpcEnumMember In statusValues
                    If statusValue.Value = currentStatus.As(Of Integer) Then
                        currentStatusValue = statusValue
                        Exit For
                    End If
                Next

                Console.WriteLine(
                        "Status: {0} ({1})",
                        currentStatusValue.Value,
                        currentStatusValue.Name)

                Console.WriteLine("-> " + currentStatusValue.Description)

                Console.WriteLine()
                Console.WriteLine("Possible status values...")

                For Each statusValue As OpcEnumMember In statusValues
                    Console.WriteLine("{0} = {1}", statusValue.Value, statusValue.Name)
                Next

                Console.Write("Enter new status: ")
                Dim value = Console.ReadLine()

                Dim newStatus As Integer
                If Integer.TryParse(value, newStatus) Then
                    client.WriteNode(statusNode.NodeId, newStatus)
                End If

                currentStatus = client.ReadNode(statusNode.NodeId)

                For Each statusValue As OpcEnumMember In statusValues
                    If statusValue.Value = currentStatus.As(Of Integer) Then
                        currentStatusValue = statusValue
                        Exit For
                    End If
                Next

                Console.WriteLine()
                Console.WriteLine(
                        "New Status: {0} ({1})",
                        currentStatusValue.Value,
                        currentStatusValue.Name)

                Console.WriteLine("-> " + currentStatusValue.Description)
            End If

            client.Disconnect()
            Console.ReadKey(True)
        End Sub
    End Class
End Namespace
