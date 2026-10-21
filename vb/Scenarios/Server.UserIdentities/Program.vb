' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System

Imports Opc.UaFx
Imports Opc.UaFx.Server

Namespace UserIdentities
    ''' <summary>
    ''' This use case demonstrates how generic user identities could be implemented using built-in
    ''' ACL functionality and how subclassing can be used to define the generic user identities.
    ''' </summary>
    Public Class Program
        '---------- Public static methods ----------

        Public Shared Sub Main(args As String())
            Dim someNode As New OpcDataVariableNode(Of String)("Hello", value:="Hello World!")

            Using server As New OpcServer("opc.tcp://localhost:4840/", someNode)
                Dim users = server.Security.UserNameAcl

                ' 1. Add the generic user (subsystem) to the UserName-ACL.
                users.AddEntry(OpcSubSystemIdentity.Generic)

                ' 2. Activate the UserName-ACL (this inline disables anonymous access).
                users.IsEnabled = True

                ' 3. Create and add a specialized ACL that implements Windows authentication.
                Dim windowsAcl As New OpcWindowsAccessControlList(server)
                server.Security.AddAccessControlList(windowsAcl)

                server.Start()

                Console.WriteLine("Server started - press any key to exit.")
                Console.ReadKey(True)
            End Using
        End Sub
    End Class
End Namespace
