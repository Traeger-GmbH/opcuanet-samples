' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports Opc.UaFx.Server

Namespace NodeAccess
    ''' <summary>
    ''' This use case realizes restricted node access using built-in ACL functionality And
    ''' subclassing for user dependent node metadata.
    ''' </summary>
    Public Class Program
        '---------- Public static methods ----------

        Public Shared Sub Main(ByVal args As String())
            Dim manager = New NodeManager()

            Using server = New OpcServer("opc.tcp://localhost:4840/", manager)
                Dim users = server.Security.UserNameAcl

                ' 1. Add the users to the UserName-ACL.
                Dim admin = users.AddEntry("Admin", "admin")
                Dim user = users.AddEntry("User", "user")

                ' 2. Setup the user privileges accordingly.
                user.Deny(OpcRequestType.Write)

                ' 3. Activate the UserName-ACL (this inline disables anonymous access).
                users.IsEnabled = True

                ' 4. Publish ACL to node manager.
                manager.AccessControl = users

                server.Start()
                Console.ReadKey(True)
            End Using
        End Sub
    End Class
End Namespace
