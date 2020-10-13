// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace HDA.Sql
{
    using System;
    using System.Collections.Generic;
    using System.Data.SQLite;
    using System.Linq;

    using Opc.UaFx;

    internal partial class SampleHistory<T>
        where T : OpcHistoryValue
    {
        #region ---------- Private readonly fields ----------

        private readonly Repository repository;

        #endregion

        #region ---------- Private constructors ----------

        private SampleHistory(Repository repository)
            : base()
        {
            this.repository = repository;
        }

        #endregion

        #region ---------- Private static properties ----------

        private static bool IsModifiedHistory
        {
            get => typeof(T) == typeof(OpcModifiedHistoryValue);
        }

        #endregion

        #region ---------- Public static methods ----------

        public static SampleHistory<T> Create(string dataSource, OpcNodeId nodeId)
        {
            if (typeof(T) != typeof(OpcHistoryValue) && typeof(T) != typeof(OpcModifiedHistoryValue))
                throw new ArgumentException();

            var builder = new SQLiteConnectionStringBuilder();
            builder.DataSource = dataSource;

            var connection = new SQLiteConnection(
                    builder.ToString(), parseViaFramework: true);

            connection.Open();
            return new SampleHistory<T>(new Repository(connection, nodeId));
        }

        #endregion

        #region ---------- Public indexers ----------

        public T this[DateTime timestamp]
        {
            get => this.repository.Read(timestamp);
        }

        #endregion

        #region ---------- Public methods ----------

        public void Add(T value)
        {
            if (!this.repository.Create(value)) {
                throw new ArgumentException(string.Format(
                        "An item with the timestamp '{0}' does already exist.",
                        value.Timestamp));
            }
        }

        public bool Contains(DateTime timestamp)
        {
            return this.repository.Exists(timestamp);
        }

        public IEnumerable<T> Enumerate(DateTime? startTime, DateTime? endTime)
        {
            AdjustRange(ref startTime, ref endTime);

            foreach (var value in this.repository.Read(startTime, endTime))
                yield return value;
        }

        public bool IsEmpty()
        {
            return !this.Enumerate(startTime: null, endTime: null).Any();
        }

        public void RemoveAt(DateTime timestamp)
        {
            if (!this.repository.Delete(timestamp)) {
                throw new ArgumentOutOfRangeException(string.Format(
                        "The timestamp '{0}' is out of the history range.",
                        timestamp));
            }
        }

        public void RemoveRange(DateTime? startTime, DateTime? endTime)
        {
            AdjustRange(ref startTime, ref endTime);
            this.repository.Delete(startTime, endTime);
        }

        public void Replace(T value)
        {
            if (!this.repository.Update(value)) {
                throw new ArgumentException(string.Format(
                        "An item with the timestamp '{0}' does not exist.",
                        value.Timestamp));
            }
        }

        #endregion

        #region ---------- Private static methods ----------

        private static void AdjustRange(ref DateTime? startTime, ref DateTime? endTime)
        {
            if (startTime == DateTime.MinValue || startTime == DateTime.MaxValue)
                startTime = null;

            if (endTime == DateTime.MinValue || endTime == DateTime.MaxValue)
                endTime = null;

            if (startTime > endTime) {
                var tempTime = startTime;

                startTime = endTime;
                endTime = tempTime;
            }
        }

        #endregion
    }
}
