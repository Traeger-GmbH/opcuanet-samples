' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Collections.Generic
Imports System.Linq

Imports Opc.UaFx
Imports Opc.UaFx.Client

Namespace DataTypeNode
    ''' <summary>
    ''' This sample demonstrates how to access and work with method nodes.
    ''' </summary>
    Public Class Program
        '---------- Public static methods ----------

        Public Shared Sub Main(args As String())
            ' If the server domain name does not match localhost just replace it
            ' e.g. with the IP address or name of the server machine.

            Dim client = New OpcClient("opc.tcp://localhost:4840/SampleServer")
            client.Connect()

            Dim methodsNode = client.BrowseNode("ns=2;s=Methods")
            Dim methods = BrowseMethods(methodsNode).ToList()

            Do
                Console.WriteLine("Enter index of method to call or 'exit':")

                For index = 0 To methods.Count - 1
                    Console.WriteLine("{0}. {1}", index, methods(index).Node.DisplayName.Value)
                Next

                Dim line = Console.ReadLine()

                If line = "exit" Then
                    Exit Do
                End If

                Dim methodIndex As Integer

                If Integer.TryParse(line, methodIndex) Then
                    Dim method = methods(methodIndex)

                    Console.WriteLine()
                    Console.WriteLine("Calling method '{0}' ...", method.Node.DisplayName.Value)

                    Dim inputs = method.Node.GetInputArguments()

                    For Each input As OpcArgumentInfo In inputs
                        Console.WriteLine(
                                vbTab & "[IN] {0} : {1} // {2}",
                                input.Name,
                                OpcDataTypes.GetDataType(input.DataTypeId),
                                input.Description)
                    Next

                    Dim outputs = method.Node.GetOutputArguments()

                    For Each output As OpcArgumentInfo In outputs
                        Console.WriteLine(
                                vbTab & "[OUT] {0} : {1} // {2}",
                                If(String.IsNullOrEmpty(output.Name), "ret", output.Name),
                                OpcDataTypes.GetDataType(output.DataTypeId),
                                output.Description)
                    Next

                    Dim inputArguments = New List(Of Object)()

                    If inputs.Length > 0 Then
                        Console.WriteLine()

                        For Each input As OpcArgumentInfo In inputs
                            Console.Write(
                                    "Enter '{0}' value of '{1}': ",
                                    OpcDataTypes.GetDataType(input.DataTypeId),
                                    input.Name)

                            Dim value = Console.ReadLine()

                            If input.DataTypeId = OpcDataTypes.String Then
                                inputArguments.Add(value)
                            ElseIf input.DataTypeId = OpcDataTypes.Int32 Then
                                inputArguments.Add(Integer.Parse(value))
                            End If
                        Next
                    End If

                    Try
                        Dim outputArguments = client.CallMethod(method.ParentId, method.Node.NodeId, inputArguments)
                        Console.WriteLine("Call succeeded!")

                        For index = 0 To outputs.Length - 1
                            Dim output = outputs(index)

                            Console.WriteLine(
                                    "'{0}' value of '{1}': {2}",
                                    OpcDataTypes.GetDataType(output.DataTypeId),
                                    If(String.IsNullOrEmpty(output.Name), "ret", output.Name),
                                    outputArguments(index))
                        Next
                    Catch ex As OpcException
                        Console.WriteLine("Call failed: {0}", ex.Message)
                    End Try

                    Console.WriteLine()
                End If
            Loop

            client.Disconnect()
        End Sub

        '---------- Private static methods ----------

        Private Shared Iterator Function BrowseMethods(node As OpcNodeInfo) As IEnumerable(Of (ParentId As OpcNodeId, Node As OpcMethodNodeInfo))
            Dim parentId = node.NodeId

            For Each childNode In node.Children()
                Dim methodNode = TryCast(childNode, OpcMethodNodeInfo)

                If methodNode IsNot Nothing Then
                    Yield (parentId, methodNode)
                Else
                    For Each childMethod In BrowseMethods(childNode)
                        Yield childMethod
                    Next
                End If
            Next
        End Function
    End Class
End Namespace
