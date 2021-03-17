' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.Data
Imports System.Data.SQLite
Imports Opc.UaFx

Namespace HDA.Sql
    Partial Friend Class SampleHistory(Of T As OpcHistoryValue)
        '---------- Private types ----------

        Private Class Repository
            '---------- Private readonly fields ----------

            Private ReadOnly connection As SQLiteConnection

            Private ReadOnly createCommand As SQLiteCommand
            Private ReadOnly createTimestamp As SQLiteParameter
            Private ReadOnly createTimestampValue As SQLiteParameter
            Private ReadOnly createValue As SQLiteParameter
            Private ReadOnly createStatusCode As SQLiteParameter
            Private ReadOnly createModificationTime As SQLiteParameter
            Private ReadOnly createModificationTimeValue As SQLiteParameter
            Private ReadOnly createModificationType As SQLiteParameter
            Private ReadOnly createModificationUserName As SQLiteParameter

            Private ReadOnly deleteCommand As SQLiteCommand
            Private ReadOnly deleteTimestampValue As SQLiteParameter

            Private ReadOnly deleteRangeCommand As SQLiteCommand
            Private ReadOnly deleteRangeStartTimeValue As SQLiteParameter
            Private ReadOnly deleteRangeEndTimeValue As SQLiteParameter

            Private ReadOnly existsCommand As SQLiteCommand
            Private ReadOnly existsTimestampValue As SQLiteParameter

            Private ReadOnly readCommand As SQLiteCommand
            Private ReadOnly readTimestampValue As SQLiteParameter

            Private ReadOnly readRangeCommand As SQLiteCommand
            Private ReadOnly readRangeStartTimeValue As SQLiteParameter
            Private ReadOnly readRangeEndTimeValue As SQLiteParameter

            Private ReadOnly updateCommand As SQLiteCommand
            Private ReadOnly updateTimestampValue As SQLiteParameter
            Private ReadOnly updateValue As SQLiteParameter
            Private ReadOnly updateStatusCode As SQLiteParameter
            Private ReadOnly updateModificationTime As SQLiteParameter
            Private ReadOnly updateModificationTimeValue As SQLiteParameter
            Private ReadOnly updateModificationType As SQLiteParameter
            Private ReadOnly updateModificationUserName As SQLiteParameter

            Private ReadOnly syncRoot As Object

            '---------- Public constructors ----------

            Public Sub New(ByVal connection As SQLiteConnection, ByVal nodeId As OpcNodeId)
                MyBase.New()

                Me.connection = connection
                Me.syncRoot = New Object()

                Dim nodeIdValue = nodeId.ToString()

                ' Me.Create(value)
                If True Then
                    Me.createCommand = Me.connection.CreateCommand()
                    Dim commandText _
                            = "insert into HistoryData (" _
                            & "            NodeId, " _
                            & "            Timestamp, " _
                            & "            TimestampValue, " _
                            & "            Value, "

                    If Not IsModifiedHistory Then
                        commandText _
                                += "        StatusCode)"
                    Else
                        commandText _
                                += "        StatusCode, " _
                                & "         ModificationTime, " _
                                & "         ModificationTimeValue, " _
                                & "         ModificationType, " _
                                & "         ModificationUserName)"
                    End If

                    commandText _
                            += "      values (" _
                            & "            @nodeId, " _
                            & "            @timestamp, " _
                            & "            @timestampValue, " _
                            & "            @value, "

                    If Not IsModifiedHistory Then
                        commandText _
                            += "        @statusCode)"
                    Else
                        commandText _
                            += "        @statusCode, " _
                            & "         @modificationTime, " _
                            & "         @modificationTimeValue, " _
                            & "         @modificationType, " _
                            & "         @modificationUserName)"
                    End If

                    Me.createCommand.CommandText = commandText

                    Dim parameters = Me.createCommand.Parameters
                    parameters.AddWithValue("@nodeId", nodeIdValue)

                    Me.createTimestamp = parameters.Add("@timestamp", DbType.DateTime)
                    Me.createTimestampValue = parameters.Add("@timestampValue", DbType.Int64)
                    Me.createValue = parameters.Add("@value", DbType.Object)
                    Me.createStatusCode = parameters.Add("@statusCode", DbType.Int64)

                    If IsModifiedHistory Then
                        Me.createModificationTime = parameters.Add("@modificationTime", DbType.DateTime)
                        Me.createModificationTimeValue = parameters.Add("@modificationTimeValue", DbType.Int64)
                        Me.createModificationType = parameters.Add("@modificationType", DbType.Byte)
                        Me.createModificationUserName = parameters.Add("@modificationUserName", DbType.String)
                    End If
                End If

                ' Me.Delete(timestamp)
                If True Then
                    Me.deleteCommand = Me.connection.CreateCommand()

                    Me.deleteCommand.CommandText _
                            = "delete from HistoryData " _
                            & "      where NodeId = @nodeId " _
                            & "        and TimestampValue = @timestampValue"

                    Dim parameters = Me.deleteCommand.Parameters
                    parameters.AddWithValue("@nodeId", nodeIdValue)

                    Me.deleteTimestampValue = parameters.Add("@timestampValue", DbType.Int64)
                End If

                ' Me.Delete(startTime, endTime)
                If True Then
                    Me.deleteRangeCommand = Me.connection.CreateCommand()

                    Me.deleteRangeCommand.CommandText _
                            = "delete from HistoryData " _
                            & " where NodeId = @nodeId " _
                            & "   and TimestampValue >= @startTimeValue " _
                            & "   and TimeStampValue <= @endTimeValue"

                    Dim parameters = Me.deleteRangeCommand.Parameters
                    parameters.AddWithValue("@nodeId", nodeIdValue)

                    Me.deleteRangeStartTimeValue = parameters.Add("@startTimeValue", DbType.Int64)
                    Me.deleteRangeEndTimeValue = parameters.Add("@endTimeValue", DbType.Int64)
                End If

                ' this.Exists(timestamp)
                If True Then
                    Me.existsCommand = Me.connection.CreateCommand()
                    Me.existsCommand.CommandText =
                            "select count(1) " _
                            & "  from HistoryData hd " _
                            & " where hd.NodeId = @nodeId " _
                            & "   and hd.TimestampValue = @timestampValue"

                    Dim parameters = Me.existsCommand.Parameters
                    parameters.AddWithValue("@nodeId", nodeIdValue)

                    Me.existsTimestampValue = parameters.Add("@timestampValue", DbType.Int64)
                End If

                ' Me.Read(timestamp)
                If True Then
                    Me.readCommand = Me.connection.CreateCommand()
                    Dim commandText _
                            = "select hd.TimestampValue, " _
                            & "       hd.Value, "

                    If Not IsModifiedHistory Then
                        commandText _
                            += "      hd.StatusCode "
                    Else
                        commandText _
                            += "      hd.StatusCode, " _
                            & "       hd.ModificationTimeValue, " _
                            & "       hd.ModificationType, " _
                            & "       hd.ModificationUserName "
                    End If

                    commandText _
                            += " from HistoryData hd " _
                            & " where hd.NodeId = @nodeId " _
                            & "   and hd.TimestampValue = @timestampValue"

                    Me.readCommand.CommandText = commandText

                    Dim parameters = Me.readCommand.Parameters
                    parameters.AddWithValue("@nodeId", nodeIdValue)

                    Me.readTimestampValue = parameters.Add("@timestampValue", DbType.Int64)
                End If

                ' Me.Read(startTime, endTime)
                If True Then
                    Me.readRangeCommand = Me.connection.CreateCommand()

                    Dim commandText _
                            = "select hd.TimestampValue, " _
                            & "       hd.Value, "

                    If Not IsModifiedHistory Then
                        commandText _
                            += "      hd.StatusCode "
                    Else
                        commandText _
                            += "      hd.StatusCode, " _
                            & "       hd.ModificationTimeValue, " _
                            & "       hd.ModificationType, " _
                            & "       hd.ModificationUserName "
                    End If

                    commandText _
                            += " from HistoryData hd " _
                            & " where hd.NodeId = @nodeId " _
                            & "   and hd.TimestampValue >= @startTimeValue " _
                            & "   and hd.TimestampValue <= @endTimeValue"

                    Me.readRangeCommand.CommandText = commandText

                    Dim parameters = Me.readRangeCommand.Parameters
                    parameters.AddWithValue("@nodeId", nodeIdValue)

                    Me.readRangeStartTimeValue = parameters.Add("@startTimeValue", DbType.Int64)
                    Me.readRangeEndTimeValue = parameters.Add("@endTimeValue", DbType.Int64)
                End If

                ' Me.Update(value)
                If True Then
                    Me.updateCommand = Me.connection.CreateCommand()

                    Dim commandText _
                            = "update HistoryData " _
                            & "   set Value = @value, "

                    If Not IsModifiedHistory Then
                        commandText _
                            += "   StatusCode = @statusCode "
                    Else
                        commandText _
                            += "   StatusCode = @statusCode, " _
                            & "    ModificationTime = @modificationTime, " _
                            & "    ModificationTimeValue = @modificationTimeValue, " _
                            & "    ModificationType = @modificationType, " _
                            & "    ModificationUserName = @modificationUserName "
                    End If

                    commandText +=
                            "where NodeId = @nodeId " _
                            & "   and TimestampValue = @timestampValue"

                    Me.updateCommand.CommandText = commandText

                    Dim parameters = Me.updateCommand.Parameters
                    parameters.AddWithValue("@nodeId", nodeIdValue)

                    Me.updateTimestampValue = parameters.Add("@timestampValue", DbType.Int64)
                    Me.updateValue = parameters.Add("@value", DbType.Object)
                    Me.updateStatusCode = parameters.Add("@statusCode", DbType.Int64)

                    If IsModifiedHistory Then
                        Me.updateModificationTime = parameters.Add("@modificationTime", DbType.DateTime)
                        Me.updateModificationTimeValue = parameters.Add("@modificationTimeValue", DbType.Int64)
                        Me.updateModificationType = parameters.Add("@modificationType", DbType.Byte)
                        Me.updateModificationUserName = parameters.Add("@modificationUserName", DbType.String)
                    End If
                End If
            End Sub

            '---------- Public methods ----------

            Public Function Create(ByVal value As T) As Boolean
                Dim modifiedValue As OpcModifiedHistoryValue = Nothing

                SyncLock Me.syncRoot
                    If Not Me.Exists(value.Timestamp) Then
                        Me.createTimestamp.Value = value.Timestamp
                        Me.createTimestampValue.Value = value.Timestamp.Ticks
                        Me.createValue.Value = value.Value
                        Me.createStatusCode.Value = value.Status.Code

                        modifiedValue = TryCast(value, OpcModifiedHistoryValue)

                        If modifiedValue IsNot Nothing Then
                            Me.createModificationTime.Value = modifiedValue.ModificationTime
                            Me.createModificationTimeValue.Value = modifiedValue.ModificationTime.Ticks
                            Me.createModificationType.Value = modifiedValue.ModificationType
                            Me.createModificationUserName.Value = modifiedValue.ModificationUserName
                        End If

                        Return Me.createCommand.ExecuteNonQuery() = 1
                    End If
                End SyncLock

                Return False
            End Function

            Public Function Delete(ByVal timestamp As DateTime) As Boolean
                SyncLock Me.syncRoot
                    Me.deleteTimestampValue.Value = timestamp.Ticks
                    Return Me.deleteCommand.ExecuteNonQuery() = 1
                End SyncLock
            End Function

            Public Function Delete(ByVal startTime As DateTime?, ByVal endTime As DateTime?) As Boolean
                SyncLock Me.syncRoot
                    Me.deleteRangeStartTimeValue.Value = (If(startTime, DateTime.MinValue)).Ticks
                    Me.deleteRangeEndTimeValue.Value = (If(endTime, DateTime.MaxValue)).Ticks
                    Return Me.deleteRangeCommand.ExecuteNonQuery() > 0
                End SyncLock
            End Function

            Public Function Exists(ByVal timestamp As DateTime) As Boolean
                SyncLock Me.syncRoot
                    Me.existsTimestampValue.Value = timestamp.Ticks

                    Dim value = Me.existsCommand.ExecuteScalar()
                    If TypeOf value Is Long Then Return DirectCast(value, Long) = 1
                End SyncLock

                Return False
            End Function

            Public Function Read(ByVal timestamp As DateTime) As T
                SyncLock Me.syncRoot
                    Me.readTimestampValue.Value = timestamp.Ticks

                    Using reader = Me.readCommand.ExecuteReader()
                        If reader.Read() Then Return CreateHistoryValue(reader.GetValues())
                    End Using
                End SyncLock

                Return CType(Nothing, T) 'default(T)
            End Function

            Public Iterator Function Read(ByVal startTime As DateTime?, ByVal endTime As DateTime?) As IEnumerable(Of T)
                SyncLock Me.syncRoot
                    Me.readRangeStartTimeValue.Value = (If(startTime, DateTime.MinValue)).Ticks
                    Me.readRangeEndTimeValue.Value = (If(endTime, DateTime.MaxValue)).Ticks

                    Using reader = Me.readRangeCommand.ExecuteReader()
                        While reader.Read()
                            Yield CreateHistoryValue(reader.GetValues())
                        End While
                    End Using
                End SyncLock
            End Function

            Public Function Update(ByVal value As T) As Boolean
                Dim modifiedValue As OpcModifiedHistoryValue = Nothing

                SyncLock Me.syncRoot
                    Me.updateTimestampValue.Value = value.Timestamp.Ticks
                    Me.updateValue.Value = value.Value
                    Me.updateStatusCode.Value = value.Status.Code

                    modifiedValue = TryCast(value, OpcModifiedHistoryValue)

                    If modifiedValue IsNot Nothing Then
                        Me.updateModificationTime.Value = modifiedValue.ModificationTime
                        Me.updateModificationTimeValue.Value = modifiedValue.ModificationTime.Ticks
                        Me.updateModificationType.Value = modifiedValue.ModificationType
                        Me.updateModificationUserName.Value = modifiedValue.ModificationUserName
                    End If

                    Return Me.updateCommand.ExecuteNonQuery() = 1
                End SyncLock
            End Function

            Private Shared Function CreateHistoryValue(ByVal values As NameValueCollection) As T
                Dim timestamp = New DateTime(Convert.ToInt64(values("TimestampValue")))
                Dim value = values("Value")
                Dim statusCode = CType(Convert.ToInt64(values("StatusCode")), OpcStatusCode)

                Dim modificationInfo As OpcHistoryModificationInfo = Nothing

                If IsModifiedHistory Then
                    Dim time = New DateTime(Convert.ToInt64(values("ModificationTimeValue")))
                    Dim type = CType(Convert.ToInt32(values("ModificationType")), OpcHistoryModificationType)
                    Dim userName = Convert.ToString(values("ModificationUserName"))

                    modificationInfo = New OpcHistoryModificationInfo(type, userName, time)
                End If

                If modificationInfo Is Nothing Then Return CType(New OpcHistoryValue(value, timestamp, statusCode), T)
                Return CType(CObj(New OpcModifiedHistoryValue(value, timestamp, statusCode, modificationInfo)), T)
            End Function
        End Class
    End Class
End Namespace
