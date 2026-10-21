' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Collections.Generic
Imports System.Net
Imports System.Net.Sockets

Namespace Server.Playground
    Partial Public Class Program
        '---------- Static constructor ----------

        Shared Sub New()
            AddHandler AppDomain.CurrentDomain.UnhandledException,
                Sub(sender, e)
                    Dim ex = TryCast(e.ExceptionObject, Exception)

                    If ex IsNot Nothing Then
                        Console.WriteLine()
                        Console.WriteLine("FAILURE")
                        Console.Write("- Message: {0}", ex.Message)
                        Console.Write("- StackTrace: {0}", ex.StackTrace)
                        Console.ReadLine()

                        Environment.Exit(ex.HResult)
                    End If
                End Sub
        End Sub

        '---------- Public static methods ----------

        Public Shared Function GetAddress(ParamArray uriStrings As String()) As Uri
            Dim address As New Uri("opc.tcp://localhost:48400/")

            For index = 0 To uriStrings.Length - 1
                Dim result As Uri = Nothing

                If Uri.TryCreate(uriStrings(index), UriKind.Absolute, result) Then
                    address = result
                    Exit For
                End If
            Next

            Return address
        End Function

        Public Shared Iterator Function GetHostAddresses(address As Uri) As IEnumerable(Of String)
            For Each hostAddress In Dns.GetHostAddresses(Dns.GetHostName())
                Dim addressFamily = hostAddress.AddressFamily

                If addressFamily = AddressFamily.InterNetwork Then
                    Dim uriBuilder As New UriBuilder(address)
                    uriBuilder.Host = hostAddress.ToString()

                    Yield uriBuilder.Uri.ToString()
                End If
            Next
        End Function
    End Class
End Namespace
