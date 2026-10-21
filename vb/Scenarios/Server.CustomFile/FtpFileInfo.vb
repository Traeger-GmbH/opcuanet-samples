' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.IO
Imports System.Net

Imports Opc.UaFx

Namespace CustomFile
    ''' <summary>
    ''' Provides a simple implementation of the <see cref="IOpcFileInfo"/> interface to access FTP files
    ''' as read-only.
    ''' </summary>
    Friend Class FtpFileInfo
        Implements IOpcFileInfo, IDisposable

        '---------- Private readonly fields ----------

        Private ReadOnly credentials As ICredentials
        Private ReadOnly syncRoot As Object

        '---------- Private fields ----------

        Private path As Uri
        Private response As FtpWebResponse

        '---------- Public constructors ----------

        Public Sub New(path As String, credentials As ICredentials)
            Me.New(New Uri(path), credentials)
        End Sub

        Public Sub New(path As Uri, credentials As ICredentials)
            MyBase.New()

            If path Is Nothing Then
                Throw New ArgumentNullException()
            End If

            If Not path.Scheme.Equals(Uri.UriSchemeFtp, StringComparison.OrdinalIgnoreCase) Then
                Throw New ArgumentException()
            End If

            Me.path = path
            Me.credentials = credentials

            Me.syncRoot = New Object()
        End Sub

        '---------- Public properties ----------

        Public ReadOnly Property Exists As Boolean Implements IOpcFileInfo.Exists
            Get
                SyncLock Me.syncRoot
                    Me.Refresh()
                    Return Me.response IsNot Nothing
                End SyncLock
            End Get
        End Property

        Public ReadOnly Property IsReadOnly As Boolean Implements IOpcFileInfo.IsReadOnly
            Get
                Return True
            End Get
        End Property

        Public ReadOnly Property Length As Long Implements IOpcFileInfo.Length
            Get
                SyncLock Me.syncRoot
                    If Not Me.Exists Then
                        Throw New IOException()
                    End If

                    Dim contentLength = Me.response.ContentLength

                    If contentLength < 0 Then
                        contentLength = 0
                    End If

                    Return contentLength
                End SyncLock
            End Get
        End Property

        '---------- Public methods ----------

        Public Sub Dispose() Implements IDisposable.Dispose
            Me.Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        Public Function GetMimeType() As String Implements IOpcFileInfo.GetMimeType
            SyncLock Me.syncRoot
                If Not Me.Exists Then
                    Throw New IOException()
                End If

                ' FtpWebResponse throws NotImplementedException.
                ''''Return Me.response.ContentType

                ' Therefore we do not know the type of content and just use the mime type
                ' for binary documents. This might be enhanced on-demand.
                Return "application/octet-stream"
            End SyncLock
        End Function

        Public Function Open(mode As OpcFileMode) As Stream Implements IOpcFileInfo.Open
            If mode <> OpcFileMode.Read Then
                Throw New ArgumentException()
            End If

            SyncLock Me.syncRoot
                If Not Me.Exists Then
                    Throw New IOException()
                End If

                Return Me.response?.GetResponseStream()
            End SyncLock
        End Function

        Public Sub Refresh() Implements IOpcFileInfo.Refresh
            SyncLock Me.syncRoot
                If Me.path Is Nothing Then
                    Throw New ObjectDisposedException(Me.GetType().Name)
                End If

                Me.response?.Dispose()
                Me.response = Nothing

                ' .NET offers no successor to FtpWebRequest, and the sample avoids a third party
                ' FTP library.
#Disable Warning SYSLIB0014
                Dim request = TryCast(WebRequest.Create(Me.path), FtpWebRequest)
#Enable Warning SYSLIB0014

                If request IsNot Nothing Then
                    request.Credentials = Me.credentials
                    request.Method = WebRequestMethods.Ftp.DownloadFile

                    Me.response = TryCast(request.GetResponse(), FtpWebResponse)
                End If
            End SyncLock
        End Sub

        '---------- Protected methods ----------

        Protected Overridable Sub Dispose(disposing As Boolean)
            If disposing Then
                SyncLock Me.syncRoot
                    Me.response?.Dispose()
                    Me.response = Nothing

                    Me.path = Nothing
                End SyncLock
            End If
        End Sub
    End Class
End Namespace
