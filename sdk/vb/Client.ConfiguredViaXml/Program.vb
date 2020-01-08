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

            ' There are different ways to load the client configuration.
            Dim configuration As OpcApplicationConfiguration = Nothing

            ' 1st Way: Load client config using a file path.
            configuration = OpcApplicationConfiguration.LoadClientConfigFile(
                    Path.Combine(Environment.CurrentDirectory, "ClientConfig.xml"))

            ' 2nd Way: Load client config specified in a specific section of your App.config.
            configuration = OpcApplicationConfiguration.LoadClientConfig("Opc.UaFx.Client")

            ' If the server domain name does not match localhost just replace it
            ' e.g. with the IP address or name of the server machine.
            Dim client As New OpcClient("opc.tcp://localhost:4840/SampleServer")

            ' To take use of the loaded client configuration, just set it on the client instance.
            client.Configuration = configuration

            client.Connect()
            client.Disconnect()

            ' In case you are using the OpcClientApplication class, you can explicitly trigger
            ' loading a configuration file using the App.config as the following code does
            ' demonstrate.
            Dim app As New OpcClientApplication("opc.tcp://localhost:4840/SampleServer")
            app.LoadConfiguration()

            ' Alternatively you can assign the manually loaded client configuration on the client
            ' instance used by the application instance, as the following code does demonstrate.
            app.Client.Configuration = configuration

            app.Run()
        End Sub
    End Class
End Namespace
