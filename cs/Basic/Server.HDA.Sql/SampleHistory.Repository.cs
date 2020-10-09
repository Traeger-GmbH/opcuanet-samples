// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace HDA.Sql
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Data;
    using System.Data.SQLite;

    using Opc.UaFx;

    internal partial class SampleHistory<T>
    {
        #region ---------- Private types ----------

        private class Repository
        {
            #region ---------- Private readonly fields ----------

            private readonly SQLiteConnection connection;

            private readonly SQLiteCommand createCommand;
            private readonly SQLiteParameter createTimestamp;
            private readonly SQLiteParameter createTimestampValue;
            private readonly SQLiteParameter createValue;
            private readonly SQLiteParameter createStatusCode;
            private readonly SQLiteParameter createModificationTime;
            private readonly SQLiteParameter createModificationTimeValue;
            private readonly SQLiteParameter createModificationType;
            private readonly SQLiteParameter createModificationUserName;

            private readonly SQLiteCommand deleteCommand;
            private readonly SQLiteParameter deleteTimestampValue;

            private readonly SQLiteCommand deleteRangeCommand;
            private readonly SQLiteParameter deleteRangeStartTimeValue;
            private readonly SQLiteParameter deleteRangeEndTimeValue;

            private readonly SQLiteCommand existsCommand;
            private readonly SQLiteParameter existsTimestampValue;

            private readonly SQLiteCommand readCommand;
            private readonly SQLiteParameter readTimestampValue;

            private readonly SQLiteCommand readRangeCommand;
            private readonly SQLiteParameter readRangeStartTimeValue;
            private readonly SQLiteParameter readRangeEndTimeValue;

            private readonly SQLiteCommand updateCommand;
            private readonly SQLiteParameter updateTimestampValue;
            private readonly SQLiteParameter updateValue;
            private readonly SQLiteParameter updateStatusCode;
            private readonly SQLiteParameter updateModificationTime;
            private readonly SQLiteParameter updateModificationTimeValue;
            private readonly SQLiteParameter updateModificationType;
            private readonly SQLiteParameter updateModificationUserName;

            private readonly object syncRoot;

            #endregion

            #region ---------- Public constructors ----------

            public Repository(SQLiteConnection connection, OpcNodeId nodeId)
                : base()
            {
                this.connection = connection;
                this.syncRoot = new object();

                var nodeIdValue = nodeId.ToString();

                //this.Create(value)
                {
                    this.createCommand = this.connection.CreateCommand();

                    var commandText
                         = "insert into HistoryData ("
                         + "            NodeId, "
                         + "            Timestamp, "
                         + "            TimestampValue, "
                         + "            Value, ";

                    if (!IsModifiedHistory) {
                        commandText
                            += "        StatusCode)";
                    }
                    else {
                        commandText
                            += "        StatusCode, "
                            + "         ModificationTime, "
                            + "         ModificationTimeValue, "
                            + "         ModificationType, "
                            + "         ModificationUserName)";
                    }

                    commandText
                         += "      values ("
                         + "            @nodeId, "
                         + "            @timestamp, "
                         + "            @timestampValue, "
                         + "            @value, ";

                    if (!IsModifiedHistory) {
                        commandText
                            += "        @statusCode)";
                    }
                    else {
                        commandText
                            += "        @statusCode, "
                            + "         @modificationTime, "
                            + "         @modificationTimeValue, "
                            + "         @modificationType, "
                            + "         @modificationUserName)";
                    }

                    this.createCommand.CommandText = commandText;

                    var parameters = this.createCommand.Parameters;
                    parameters.AddWithValue("@nodeId", nodeIdValue);

                    this.createTimestamp = parameters.Add("@timestamp", DbType.DateTime);
                    this.createTimestampValue = parameters.Add("@timestampValue", DbType.Int64);
                    this.createValue = parameters.Add("@value", DbType.Object);
                    this.createStatusCode = parameters.Add("@statusCode", DbType.Int64);

                    if (IsModifiedHistory) {
                        this.createModificationTime = parameters.Add("@modificationTime", DbType.DateTime);
                        this.createModificationTimeValue = parameters.Add("@modificationTimeValue", DbType.Int64);
                        this.createModificationType = parameters.Add("@modificationType", DbType.Byte);
                        this.createModificationUserName = parameters.Add("@modificationUserName", DbType.String);
                    }
                }

                //this.Delete(timestamp)
                {
                    this.deleteCommand = this.connection.CreateCommand();

                    this.deleteCommand.CommandText
                            = "delete from HistoryData "
                            + "      where NodeId = @nodeId "
                            + "        and TimestampValue = @timestampValue";

                    var parameters = this.deleteCommand.Parameters;
                    parameters.AddWithValue("@nodeId", nodeIdValue);

                    this.deleteTimestampValue = parameters.Add("@timestampValue", DbType.Int64);
                }

                //this.Delete(startTime, endTime)
                {
                    this.deleteRangeCommand = this.connection.CreateCommand();

                    this.deleteRangeCommand.CommandText
                            = "delete from HistoryData "
                            + " where NodeId = @nodeId "
                            + "   and TimestampValue >= @startTimeValue "
                            + "   and TimeStampValue <= @endTimeValue";

                    var parameters = this.deleteRangeCommand.Parameters;
                    parameters.AddWithValue("@nodeId", nodeIdValue);

                    this.deleteRangeStartTimeValue = parameters.Add("@startTimeValue", DbType.Int64);
                    this.deleteRangeEndTimeValue = parameters.Add("@endTimeValue", DbType.Int64);
                }

                //this.Exists(timestamp)
                {
                    this.existsCommand = this.connection.CreateCommand();

                    this.existsCommand.CommandText
                            = "select count(1) "
                            + "  from HistoryData hd "
                            + " where hd.NodeId = @nodeId "
                            + "   and hd.TimestampValue = @timestampValue";

                    var parameters = this.existsCommand.Parameters;
                    parameters.AddWithValue("@nodeId", nodeIdValue);

                    this.existsTimestampValue = parameters.Add("@timestampValue", DbType.Int64);
                }

                //this.Read(timestamp)
                {
                    this.readCommand = this.connection.CreateCommand();

                    var commandText
                            = "select hd.TimestampValue, "
                            + "       hd.Value, ";

                    if (!IsModifiedHistory) {
                        commandText
                            += "      hd.StatusCode ";
                    }
                    else {
                        commandText
                            += "      hd.StatusCode, "
                            + "       hd.ModificationTimeValue, "
                            + "       hd.ModificationType, "
                            + "       hd.ModificationUserName ";
                    }

                    commandText
                            += " from HistoryData hd "
                            + " where hd.NodeId = @nodeId "
                            + "   and hd.TimestampValue = @timestampValue";

                    this.readCommand.CommandText = commandText;

                    var parameters = this.readCommand.Parameters;
                    parameters.AddWithValue("@nodeId", nodeIdValue);

                    this.readTimestampValue = parameters.Add("@timestampValue", DbType.Int64);
                }

                //this.Read(startTime, endTime)
                {
                    this.readRangeCommand = this.connection.CreateCommand();

                    var commandText
                            = "select hd.TimestampValue, "
                            + "       hd.Value, ";

                    if (!IsModifiedHistory) {
                        commandText
                            += "      hd.StatusCode ";
                    }
                    else {
                        commandText
                            += "      hd.StatusCode, "
                            + "       hd.ModificationTimeValue, "
                            + "       hd.ModificationType, "
                            + "       hd.ModificationUserName ";
                    }

                    commandText
                            += " from HistoryData hd "
                            + " where hd.NodeId = @nodeId "
                            + "   and hd.TimestampValue >= @startTimeValue "
                            + "   and hd.TimestampValue <= @endTimeValue";

                    this.readRangeCommand.CommandText = commandText;

                    var parameters = this.readRangeCommand.Parameters;
                    parameters.AddWithValue("@nodeId", nodeIdValue);

                    this.readRangeStartTimeValue = parameters.Add("@startTimeValue", DbType.Int64);
                    this.readRangeEndTimeValue = parameters.Add("@endTimeValue", DbType.Int64);
                }

                //this.Update(value)
                {
                    this.updateCommand = this.connection.CreateCommand();

                    var commandText
                         = "update HistoryData "
                         + "   set Value = @value, ";

                    if (!IsModifiedHistory) {
                        commandText
                            += "   StatusCode = @statusCode ";
                    }
                    else {
                        commandText
                            += "   StatusCode = @statusCode, "
                            + "    ModificationTime = @modificationTime, "
                            + "    ModificationTimeValue = @modificationTimeValue, "
                            + "    ModificationType = @modificationType, "
                            + "    ModificationUserName = @modificationUserName ";
                    }

                    commandText
                         += "where NodeId = @nodeId "
                         + "   and TimestampValue = @timestampValue";

                    this.updateCommand.CommandText = commandText;

                    var parameters = this.updateCommand.Parameters;
                    parameters.AddWithValue("@nodeId", nodeIdValue);

                    this.updateTimestampValue = parameters.Add("@timestampValue", DbType.Int64);
                    this.updateValue = parameters.Add("@value", DbType.Object);
                    this.updateStatusCode = parameters.Add("@statusCode", DbType.Int64);

                    if (IsModifiedHistory) {
                        this.updateModificationTime = parameters.Add("@modificationTime", DbType.DateTime);
                        this.updateModificationTimeValue = parameters.Add("@modificationTimeValue", DbType.Int64);
                        this.updateModificationType = parameters.Add("@modificationType", DbType.Byte);
                        this.updateModificationUserName = parameters.Add("@modificationUserName", DbType.String);
                    }
                }
            }

            #endregion

            #region ---------- Public methods ----------

            public bool Create(T value)
            {
                lock (this.syncRoot) {
                    if (!this.Exists(value.Timestamp)) {
                        this.createTimestamp.Value = value.Timestamp;
                        this.createTimestampValue.Value = value.Timestamp.Ticks;
                        this.createValue.Value = value.Value;
                        this.createStatusCode.Value = value.Status.Code;

                        if (value is OpcModifiedHistoryValue modifiedValue) {
                            this.createModificationTime.Value = modifiedValue.ModificationTime;
                            this.createModificationTimeValue.Value = modifiedValue.ModificationTime.Ticks;
                            this.createModificationType.Value = modifiedValue.ModificationType;
                            this.createModificationUserName.Value = modifiedValue.ModificationUserName;
                        }

                        return this.createCommand.ExecuteNonQuery() == 1;
                    }
                }

                return false;
            }

            public bool Delete(DateTime timestamp)
            {
                lock (this.syncRoot) {
                    this.deleteTimestampValue.Value = timestamp.Ticks;
                    return this.deleteCommand.ExecuteNonQuery() == 1;
                }
            }

            public bool Delete(DateTime? startTime, DateTime? endTime)
            {
                lock (this.syncRoot) {
                    this.deleteRangeStartTimeValue.Value = (startTime ?? DateTime.MinValue).Ticks;
                    this.deleteRangeEndTimeValue.Value = (endTime ?? DateTime.MaxValue).Ticks;

                    return this.deleteRangeCommand.ExecuteNonQuery() > 0;
                }
            }

            public bool Exists(DateTime timestamp)
            {
                lock (this.syncRoot) {
                    this.existsTimestampValue.Value = timestamp.Ticks;

                    if (this.existsCommand.ExecuteScalar() is long value)
                        return value == 1;
                }

                return false;
            }

            public T Read(DateTime timestamp)
            {
                lock (this.syncRoot) {
                    this.readTimestampValue.Value = timestamp.Ticks;

                    using (var reader = this.readCommand.ExecuteReader()) {
                        if (reader.Read())
                            return CreateValue(reader.GetValues());
                    }
                }

                return default;
            }

            public IEnumerable<T> Read(DateTime? startTime, DateTime? endTime)
            {
                lock (this.syncRoot) {
                    this.readRangeStartTimeValue.Value = (startTime ?? DateTime.MinValue).Ticks;
                    this.readRangeEndTimeValue.Value = (endTime ?? DateTime.MaxValue).Ticks;

                    using (var reader = this.readRangeCommand.ExecuteReader()) {
                        while (reader.Read())
                            yield return CreateValue(reader.GetValues());
                    }
                }
            }

            public bool Update(T value)
            {
                lock (this.syncRoot) {
                    this.updateTimestampValue.Value = value.Timestamp.Ticks;
                    this.updateValue.Value = value.Value;
                    this.updateStatusCode.Value = value.Status.Code;

                    if (value is OpcModifiedHistoryValue modifiedValue) {
                        this.updateModificationTime.Value = modifiedValue.ModificationTime;
                        this.updateModificationTimeValue.Value = modifiedValue.ModificationTime.Ticks;
                        this.updateModificationType.Value = modifiedValue.ModificationType;
                        this.updateModificationUserName.Value = modifiedValue.ModificationUserName;
                    }

                    return this.updateCommand.ExecuteNonQuery() == 1;
                }
            }

            #endregion

            #region ---------- Private static methods ----------

            private static T CreateValue(NameValueCollection values)
            {
                var timestamp = new DateTime(Convert.ToInt64(values["TimestampValue"]));
                var value = values["Value"];
                var statusCode = (OpcStatusCode)Convert.ToInt64(values["StatusCode"]);

                OpcHistoryModificationInfo modificationInfo = null;

                if (IsModifiedHistory) {
                    var time = new DateTime(Convert.ToInt64(values["ModificationTimeValue"]));
                    var type = (OpcHistoryModificationType)Convert.ToInt32(values["ModificationType"]);
                    var userName = Convert.ToString(values["ModificationUserName"]);

                    modificationInfo = new OpcHistoryModificationInfo(type, userName, time);
                }

                if (modificationInfo == null)
                    return (T)new OpcHistoryValue(value, timestamp, statusCode);

                return (T)(object)new OpcModifiedHistoryValue(
                        value, timestamp, statusCode, modificationInfo);
            }

            #endregion
        }

        #endregion
    }
}
