' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports Opc.UaFx
Imports Opc.UaFx.Server

Namespace SimaticNodes
    ''' <summary>
    ''' This use case demonstrates how to define a custom factory for node identifiers to
    ''' define / use node identifiers using the "SIEMENS SIMATIC" style of dot separated and quoted
    ''' node names to construct the node identifier of a node.
    ''' </summary>
    Public Class Program
        Public Shared Sub Main()
            OpcNodeId.Factory = New SimaticNodeIdFactory()

            Dim db1 = New OpcFolderNode(
                    "DataBlock1",
                    New OpcDataVariableNode(Of Byte)("MaxByte", value:=Byte.MaxValue),
                    New OpcDataVariableNode(Of Short)("MaxInt", value:=Short.MaxValue),
                    New OpcDataVariableNode(Of Integer)("MaxDInt", value:=Integer.MaxValue))

            Dim db2 = New OpcFolderNode(
                    "DataBlock2",
                    New OpcObjectNode(
                            "MyStruct",
                            New OpcDataVariableNode(Of Integer)("FieldA", value:=10),
                            New OpcDataVariableNode(Of Integer)("FieldB", value:=20)))

            Using server = New OpcServer("opc.tcp://localhost:4840/", db1, db2)
                server.Start()

                Console.WriteLine("Server started - press any key to exit.")
                Console.ReadKey(True)
            End Using
        End Sub
    End Class
End Namespace
