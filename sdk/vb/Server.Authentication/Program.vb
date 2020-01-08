' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports Opc.UaFx.Server

Namespace Authentication
    ''' <summary>
    ''' This sample demonstrates how to setup an OPC UA server with a UserName policy using
    ''' the UserName ACL for custom user name and password authentication.
    ''' </summary>
    Public Class Program
        '---------- Public static methods ----------

        Public Shared Sub Main(args As String())
            ' If the server domain name does not match localhost just replace it
            ' e.g. with the IP address or name of the server machine.
            Dim server As New OpcServer(
                    "opc.tcp://localhost:4840/SampleServer",
                    New SampleNodeManager())

            ' By default an OPC UA server uses the ACL for anonymous authentication. To
            ' support user name and password based authentication the UserName ACL needs
            ' to be enabled.
            server.Security.UserNameAcl.IsEnabled = True

            ' To register a specific user name and password pair add that entry to the
            ' UserName ACL of the OPC UA server.
            Dim entry = server.Security.UserNameAcl.AddEntry("username", "password")

            ' Additionally it is possible to allow and deny specific operations on the entry.
            ' By default all operations are enabled on the entry.
            entry.Deny(OpcRequestType.Write)

            server.Start()
            Console.ReadKey(True)
            server.Stop()
        End Sub
    End Class
End Namespace
