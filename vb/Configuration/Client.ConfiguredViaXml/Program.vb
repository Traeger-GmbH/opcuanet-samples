' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.IO

Imports Opc.UaFx
Imports Opc.UaFx.Client

Namespace ConfiguredViaXml
    ''' <summary>
    ''' This sample demonstrates how to configure an OPC UA client using a XML configuration file.
    ''' </summary>
    Public Class Program
        ' ---------- Public static methods ----------

        Public Shared Sub Main(args As String())
            '''' To simple use the configuration stored within the XML configuration file
            '''' beside the client application you just need to load the configuration file as the
            '''' following code does demonstrate.
            '''' By default it is not necessary to explicitly configure an OPC UA client. But in case
            '''' of advanced and productive scenarios you will have to.

            ' Load client config using a file path.
            Dim configuration = OpcApplicationConfiguration.LoadClientConfigFile(
                    Path.Combine(Environment.CurrentDirectory, "ClientConfig.xml"))

            ' If the server domain name does not match localhost just replace it
            ' e.g. with the IP address or name of the server machine.
            Dim client As New OpcClient("opc.tcp://localhost:4840/SampleServer")

            ' To take use of the loaded client configuration, just set it on the client instance.
            client.Configuration = configuration

            client.Connect()
            client.Disconnect()

            ' In case you are using the OpcClientApplication class, you can assign the
            ' loaded client configuration on the client instance used by the application instance,
            ' as the following code demonstrates.
            Dim app As New OpcClientApplication("opc.tcp://localhost:4840/SampleServer")

            app.Client.Configuration = configuration

            app.Run()
        End Sub
    End Class
End Namespace
