' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Collections.Generic
Imports System.Data.SQLite
Imports System.Linq
Imports Opc.UaFx

Namespace HDA.Sql
    Friend Partial Class SampleHistory(Of T As OpcHistoryValue)
        '---------- Private readonly fields ----------

        Private ReadOnly _repository As Repository

        '---------- Private constructors ----------

        Private Sub New(ByVal repository As Repository)
            MyBase.New()
            Me._repository = repository
        End Sub

        '---------- Private static properties ----------

        Private Shared ReadOnly Property IsModifiedHistory As Boolean
            Get
                Return GetType(T) = GetType(OpcModifiedHistoryValue)
            End Get
        End Property

        '---------- Public static methods ----------

        Public Shared Function Create(ByVal dataSource As String, ByVal nodeId As OpcNodeId) As SampleHistory(Of T)
            If GetType(T) <> GetType(OpcHistoryValue) AndAlso GetType(T) <> GetType(OpcModifiedHistoryValue) Then
                Throw New ArgumentException()
            End If

            Dim builder = New SQLiteConnectionStringBuilder()
            builder.DataSource = dataSource

            Dim connection = New SQLiteConnection( _
                    builder.ToString(), parseViaFramework:=True)

            connection.Open()
            Return New SampleHistory(Of T)(New Repository(connection, nodeId))
        End Function

        '---------- Public indexers ----------

        Default Public ReadOnly Property Item(ByVal timestamp As DateTime) As T
            Get
                Return Me._repository.Read(timestamp)
            End Get
        End Property

        '---------- Public methods ----------

        Public Sub Add(ByVal value As T)
            If Not Me._repository.Create(value) Then
                Throw New ArgumentException(String.Format(
                        "An item with the timestamp '{0}' does already exist.",
                        value.Timestamp))
            End If
        End Sub

        Public Function Contains(ByVal timestamp As DateTime) As Boolean
            Return Me._repository.Exists(timestamp)
        End Function

        Public Iterator Function Enumerate(ByVal startTime As DateTime?, ByVal endTime As DateTime?) As IEnumerable(Of T)
            AdjustRange(startTime, endTime)

            For Each value In Me._repository.Read(startTime, endTime)
                Yield value
            Next
        End Function

        Public Function IsEmpty() As Boolean
            Return Not Me.Enumerate(startTime:=Nothing, endTime:=Nothing).Any()
        End Function

        Public Sub RemoveAt(ByVal timestamp As DateTime)
            If Not Me._repository.Delete(timestamp) Then
                Throw New ArgumentOutOfRangeException(String.Format(
                        "The timestamp '{0}' is out of the history range.",
                        timestamp))
            End If
        End Sub

        Public Sub RemoveRange(ByVal startTime As DateTime?, ByVal endTime As DateTime?)
            AdjustRange(startTime, endTime)
            Me._repository.Delete(startTime, endTime)
        End Sub

        Public Sub Replace(ByVal value As T)
            If Not Me._repository.Update(value) Then
                Throw New ArgumentException(String.Format(
                        "An item with the timestamp '{0}' does not exist.",
                        value.Timestamp))
            End If
        End Sub

        '---------- Private static methods ----------

        Private Shared Sub AdjustRange(ByRef startTime As DateTime?, ByRef endTime As DateTime?)
            If startTime = DateTime.MinValue OrElse startTime = DateTime.MaxValue Then startTime = Nothing
            If endTime = DateTime.MinValue OrElse endTime = DateTime.MaxValue Then endTime = Nothing

            If startTime > endTime Then
                Dim tempTime = startTime

                startTime = endTime
                endTime = tempTime
            End If
        End Sub
    End Class
End Namespace
