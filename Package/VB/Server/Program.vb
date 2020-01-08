' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports Opc.UaFx.Server

Namespace Server
    ''' <summary>
    ''' This sample demonstrates how to implement a primitive OPC UA server.
    ''' </summary>
    Public Class Program
        '---------- Public static methods ----------

        Public Shared Sub Main(args As String())
            ' 1st Way: Use the OpcServer class.
            If True Then
                '' The OpcServer class interacts with one Or more OPC UA clients using one of
                '' the registered base addresses of the server. While this class provides the
                '' different OPC UA services defined by OPC UA, it does Not implement a main loop.
                'Dim server As New OpcServer("opc.tcp://localhost:4840/SampleServer", New SampleNodeManager())

                'server.Start()
                'server.Stop()
            End If

            ' 2nd Way: Use the OpcServerApplication class.
            If True Then
                ' The OpcServerApplication class uses a single OpcServer instance which is
                ' wrapped within a main loop.
                '
                ' Remarks
                ' - The app instance does start a main loop when the server has been started.
                ' - Custom startup code have to be implemented within the event handler of the
                '   Started event of the app instance.
                Dim app As New OpcServerApplication("opc.tcp://localhost:4840/SampleServer", New SampleNodeManager())
                app.Run()
            End If

            ' 3rd Way: Use the OpcServerServiceApplication class.
            If True Then
                '' The OpcServerServiceApplication class uses a single OpcServer instance which is
                '' wrapped within a main loop when it is started with an interactive user or in
                '' debug mode. Otherwise it will start the process as a windows service which
                '' allows the application can be registered as a service process.
                ''
                '' Remarks
                '' - The app instance does start a main loop when the server has been started.
                '' - Custom startup code have to be implemented within the event handler of the
                ''   Started event of the app instance.

                'Dim app As New OpcServerServiceApplication("opc.tcp://localhost:4840/SampleServer", New SampleNodeManager())
                'app.Run()
            End If
        End Sub
    End Class
End Namespace
