' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Collections.Generic
Imports Opc.UaFx
Imports Opc.UaFx.Server

Namespace Localization
    ''' <summary>
    ''' This use case demonstrates how to provide display names and descriptions of nodes in several
    ''' languages using the globalization resources of the server.
    ''' </summary>
    Public Class Program
        '---------- Public static methods ----------

        Public Shared Sub Main(args As String())
            Dim node As New OpcFolderNode("MyFolder")

            node.DisplayName = New OpcText("MyFolder", "en-US", "MyFolder.DisplayName")
            node.Description = New OpcText("MyFolder Description", "en-US", "MyFolder.Description")

            Using server As New OpcServer("opc.tcp://localhost:4840", node)
                server.Globalization.AddResources(
                        "en-US",
                        New KeyValuePair(Of String, String)("MyFolder.DisplayName", "MyFolder (en)"),
                        New KeyValuePair(Of String, String)("MyFolder.Description", "MyFolder Description (en)"))
                server.Globalization.AddResources(
                        "de-DE",
                        New KeyValuePair(Of String, String)("MyFolder.DisplayName", "Mein Ordner (de)"),
                        New KeyValuePair(Of String, String)("MyFolder.Description", "Meine Ordner Beschreibung (de)"))
                server.Globalization.AddResources(
                        "fr-FR",
                        New KeyValuePair(Of String, String)("MyFolder.DisplayName", "Mon dossier (fr)"),
                        New KeyValuePair(Of String, String)("MyFolder.Description", "Description de mon dossier (fr)"))

                server.Start()

                Console.WriteLine("Server started - press any key to exit.")
                Console.ReadKey(True)
            End Using
        End Sub
    End Class
End Namespace
