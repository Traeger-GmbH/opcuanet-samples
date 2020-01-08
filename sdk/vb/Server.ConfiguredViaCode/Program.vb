' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.IO

Imports Opc.Ua
Imports Opc.UaFx
Imports Opc.UaFx.Server

Namespace ConfiguredViaCode
    ''' <summary>
    ''' This sample demonstrates how to configure an OPC UA server using in code configuration.
    ''' </summary>
    Public Class Program
        ' ---------- Public static methods ----------"

        Public Shared Sub Main(args As String())
            '''' To simple use the in code configuration you just need to configure your server
            '''' instance using the Configuration property of it.
            '''' By default it is not necessary to explicitly configure an OPC UA server. But in case
            '''' of advanced and productive scenarios you will have to.

            ' If the server domain name does not match localhost just replace it
            ' e.g. with the IP address or name of the server machine.
            Dim server As New OpcServer(
                    "opc.tcp://localhost:4840/SampleServer",
                    New SampleNodeManager())

            '''' There are different ways to configure the server instance.

            ' 1st Way: Use the default configuration which does use information from the environment
            '         to setup the server. To use the default configuration you either do not take
            '         any changes on the Configuration property of the server instance or set its
            '         value to null (Nothing in Visual Basic).
            server.Configuration = Nothing

            ' 2nd Way: Instantiate a default configuration, manipulate it and set these instance as
            '         the new configuration to use by the server.
            Dim configuration As New OpcApplicationConfiguration(OpcApplicationType.Server)
            configuration.ServerConfiguration.MaxSessionCount = 10

            ' ApplicationName
            '
            ' Using the default configuration or a new instance of the OpcApplicationConfiguration
            ' means, that the server uses the value specified in the AssemblyTitleAttribute
            ' of your executable as the ApplicationName. This value should maybe changed to the
            ' ApplicationName specified in your server certificate. To do that just use a line of
            ' code like the following one.
            server.Configuration.ApplicationName = "My Application Name"

            ' Certificate Stores
            '
            ' Using the default configuration or a new instance of the OpcApplicationConfiguration
            ' means, that the server uses the path
            ' "%CommonApplicationData%\OPC Foundation\CertificateStores\" as the root directory of
            ' - the "MachineDefault" application certificates store
            ' - the "RejectedCertificates" certificates store
            ' - the "UA Certificate Authorities" trusted issuer certificates store
            ' - the "UA Applications" trusted peer certificates store.
            ' While the first directory "%CommonApplicationData%" does point to (on a default
            ' windows installation to the C: drive) the path "C:\ProgramData" it is obvious that
            ' this directories are accessable by any user of the system and that the certificate
            ' stores are not in the same directory as your server application.
            ' In some scenarios it would be necessary to change the location of the used
            ' certificate store directories like the following code does demonstrate.
            Dim securityConfiguration As SecurityConfiguration = server.Configuration.SecurityConfiguration

            securityConfiguration.ApplicationCertificate.StorePath _
                    = Path.Combine(Environment.CurrentDirectory, "App Certificates")
            securityConfiguration.RejectedCertificateStore.StorePath _
                    = Path.Combine(Environment.CurrentDirectory, "Rejected Certificates")
            securityConfiguration.TrustedIssuerCertificates.StorePath _
                    = Path.Combine(Environment.CurrentDirectory, "Trusted Issuer Certificates")
            securityConfiguration.TrustedIssuerCertificates.StorePath _
                    = Path.Combine(Environment.CurrentDirectory, "Trusted Peer Certificates")

            ' In case you want to take use of one of the SpecialFolders defined by the
            ' Environment.SpecialFolder enumeration you can use the enumeration members name as
            ' a placeholder in your custom StorePath like the following code does demonstrate.
            securityConfiguration.ApplicationCertificate.StorePath _
                    = "%LocalApplicationData%\My Application\App Certificates"
            securityConfiguration.RejectedCertificateStore.StorePath _
                    = "%LocalApplicationData%\My Application\Rejected Certificates"
            securityConfiguration.TrustedIssuerCertificates.StorePath _
                    = "%LocalApplicationData%\My Application\Trusted Issuer Certificates"
            securityConfiguration.TrustedPeerCertificates.StorePath _
                    = "%LocalApplicationData%\My Application\Trusted Peer Certificates"

            '''' It is not necessary that all certificate stores have to point to the same root
            '''' directory as above. Each store can also point to a totally different directory.

            server.Configuration = configuration

            ' 3rd Way: Directly change the default configuration of the server instance using the
            '         Configuration property.
            server.Configuration.ServerConfiguration.MaxSessionCount = 10

            server.Start()
            server.Stop()

            ' In case you are using the OPC UA server (Service) Application class, you can directly
            ' configure your server/application using the Configuration property of the
            ' application instance as the following code does demonstrate.
            Dim app As New OpcServerApplication(
                    "opc.tcp://localhost:4840/SampleServer",
                    New SampleNodeManager())

            app.Configuration.ServerConfiguration.MaxSessionCount = 10
            app.Run()
        End Sub
    End Class
End Namespace
