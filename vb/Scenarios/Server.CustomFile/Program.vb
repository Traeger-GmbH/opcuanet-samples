' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Net

Imports Opc.UaFx
Imports Opc.UaFx.Server

Namespace CustomFile
    ''' <summary>
    ''' This use case represents in which way the IOpcFileInfo interface can be used to implement
    ''' "custom file sources" for a OpcFileNode.
    ''' </summary>
    Public Class Program
        '---------- Public static methods ----------

        Public Shared Sub Main(args As String())
            Dim fileInfo As New FtpFileInfo(
                    path:="ftp://test.rebex.net/readme.txt",
                    credentials:=New NetworkCredential("demo", "password"))

            Using server As New OpcServer(
                    "opc.tcp://localhost:4840/",
                    New OpcFileNode("readme.txt", fileInfo))
                server.Start()

                Console.WriteLine("Server started - press any key to exit.")
                Console.ReadKey(True)
            End Using
        End Sub
    End Class
End Namespace
