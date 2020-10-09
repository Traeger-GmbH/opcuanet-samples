// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace HDA.Sql
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Opc.UaFx;
    using Opc.UaFx.Server;

    /// <summary>
    /// Represents a sample implementation of a custom IOpcNodeHistoryProvider.
    /// </summary>
    internal class SampleHistorian : IOpcNodeHistoryProvider
    {
        #region ---------- Private readonly fields ----------

        private readonly object syncRoot;

        #endregion

        #region ---------- Private fields ----------

        private bool autoUpdateHistory;

        #endregion

        #region ---------- Public constructors ----------

        public SampleHistorian(OpcNodeManager owner, OpcVariableNode node)
            : base()
        {
            this.Owner = owner;
            this.Node = node;

            this.Node.AccessLevel |= OpcAccessLevel.HistoryReadOrWrite;
            this.Node.UserAccessLevel |= OpcAccessLevel.HistoryReadOrWrite;
            this.Node.IsHistorizing = true;

            this.syncRoot = new object();

            this.History = SampleHistory<OpcHistoryValue>
                    .Create(@".\History.db", node.Id);
            this.ModifiedHistory = SampleHistory<OpcModifiedHistoryValue>
                    .Create(@".\History.Modified.db", node.Id);
        }

        #endregion

        #region ---------- Public properties ----------

        public bool AutoUpdateHistory
        {
            get
            {
                lock (this.syncRoot)
                    return this.autoUpdateHistory;
            }

            set
            {
                lock (this.syncRoot) {
                    if (this.autoUpdateHistory != value) {
                        this.autoUpdateHistory = value;

                        if (this.autoUpdateHistory) {
                            this.Node.BeforeApplyChanges
                                    += this.HandleNodeBeforeApplyChanges;
                        }
                        else {
                            this.Node.BeforeApplyChanges
                                    -= this.HandleNodeBeforeApplyChanges;
                        }
                    }
                }
            }
        }

        public SampleHistory<OpcHistoryValue> History
        {
            get;
        }

        public SampleHistory<OpcModifiedHistoryValue> ModifiedHistory
        {
            get;
        }

        public OpcVariableNode Node
        {
            get;
        }

        public OpcNodeManager Owner
        {
            get;
        }

        #endregion

        #region ---------- Public methods ----------

        public OpcStatusCollection CreateHistory(
                OpcContext context,
                OpcHistoryModificationInfo modificationInfo,
                OpcValueCollection values)
        {
            var results = OpcStatusCollection.Create(OpcStatusCode.Good, values.Count);

            lock (this.syncRoot) {
                var expectedDataType = this.Node.DataTypeId;

                for (int index = 0; index < values.Count; index++) {
                    var result = results[index];
                    var value = OpcHistoryValue.Create(values[index]);

                    if (value.DataTypeId == expectedDataType) {
                        if (this.History.Contains(value.Timestamp)) {
                            result.Update(OpcStatusCode.BadEntryExists);
                        }
                        else {
                            this.History.Add(value);

                            var modifiedValue = value.CreateModified(modificationInfo);
                            this.ModifiedHistory.Add(modifiedValue);

                            result.Update(OpcStatusCode.GoodEntryInserted);
                        }
                    }
                    else {
                        result.Update(OpcStatusCode.BadTypeMismatch);
                    }
                }
            }

            return results;
        }

        public OpcStatusCollection DeleteHistory(
                OpcContext context,
                OpcHistoryModificationInfo modificationInfo,
                IEnumerable<DateTime> times)
        {
            var results = OpcStatusCollection.Create(OpcStatusCode.Good, times.Count());

            lock (this.syncRoot) {
                int index = 0;

                foreach (var time in times) {
                    var result = results[index++];

                    if (this.History.Contains(time)) {
                        var value = this.History[time];
                        this.History.RemoveAt(time);

                        var modifiedValue = value.CreateModified(modificationInfo);
                        this.ModifiedHistory.Add(modifiedValue);
                    }
                    else {
                        result.Update(OpcStatusCode.BadNoEntryExists);
                    }
                }
            }

            return results;
        }

        public OpcStatusCollection DeleteHistory(
                OpcContext context,
                OpcHistoryModificationInfo modificationInfo,
                OpcValueCollection values)
        {
            var results = OpcStatusCollection.Create(OpcStatusCode.Good, values.Count);

            lock (this.syncRoot) {
                for (int index = 0; index < values.Count; index++) {
                    var timestamp = OpcHistoryValue.Create(values[index]).Timestamp;
                    var result = results[index];

                    if (this.History.Contains(timestamp)) {
                        var value = this.History[timestamp];
                        this.History.RemoveAt(timestamp);

                        var modifiedValue = value.CreateModified(modificationInfo);
                        this.ModifiedHistory.Add(modifiedValue);
                    }
                    else {
                        result.Update(OpcStatusCode.BadNoEntryExists);
                    }
                }
            }

            return results;
        }

        public OpcStatusCollection DeleteHistory(
                OpcContext context,
                OpcHistoryModificationInfo modificationInfo,
                DateTime? startTime,
                DateTime? endTime,
                OpcDeleteHistoryOptions options)
        {
            var results = new OpcStatusCollection();

            lock (this.syncRoot) {
                if (options.HasFlag(OpcDeleteHistoryOptions.Modified)) {
                    this.ModifiedHistory.RemoveRange(startTime, endTime);
                }
                else {
                    var values = this.History.Enumerate(startTime, endTime).ToArray();
                    this.History.RemoveRange(startTime, endTime);

                    for (int index = 0; index < values.Length; index++) {
                        var value = values[index];
                        this.ModifiedHistory.Add(value.CreateModified(modificationInfo));

                        results.Add(OpcStatusCode.Good);
                    }
                }
            }

            return results;
        }

        public IEnumerable<OpcHistoryValue> ReadHistory(
                OpcContext context,
                DateTime? startTime,
                DateTime? endTime,
                OpcReadHistoryOptions options)
        {
            lock (this.syncRoot) {
                if (options.HasFlag(OpcReadHistoryOptions.Modified)) {
                    return this.ModifiedHistory
                            .Enumerate(startTime, endTime)
                            .Cast<OpcHistoryValue>()
                            .ToArray();
                }

                return this.History
                        .Enumerate(startTime, endTime)
                        .ToArray();
            }
        }

        public OpcStatusCollection ReplaceHistory(
                OpcContext context,
                OpcHistoryModificationInfo modificationInfo,
                OpcValueCollection values)
        {
            var results = OpcStatusCollection.Create(OpcStatusCode.Good, values.Count);

            lock (this.syncRoot) {
                var expectedDataTypeId = this.Node.DataTypeId;

                for (int index = 0; index < values.Count; index++) {
                    var result = results[index];
                    var value = OpcHistoryValue.Create(values[index]);

                    if (value.DataTypeId == expectedDataTypeId) {
                        if (this.History.Contains(value.Timestamp)) {
                            var oldValue = this.History[value.Timestamp];
                            this.History.Replace(value);

                            var modifiedValue = oldValue.CreateModified(modificationInfo);
                            this.ModifiedHistory.Add(modifiedValue);

                            result.Update(OpcStatusCode.GoodEntryReplaced);
                        }
                        else {
                            result.Update(OpcStatusCode.BadNoEntryExists);
                        }
                    }
                    else {
                        result.Update(OpcStatusCode.BadTypeMismatch);
                    }
                }
            }

            return results;
        }

        public OpcStatusCollection UpdateHistory(
                OpcContext context,
                OpcHistoryModificationInfo modificationInfo,
                OpcValueCollection values)
        {
            var results = OpcStatusCollection.Create(OpcStatusCode.Good, values.Count);

            lock (this.syncRoot) {
                var expectedDataTypeId = this.Node.DataTypeId;

                for (int index = 0; index < values.Count; index++) {
                    var result = results[index];
                    var value = OpcHistoryValue.Create(values[index]);

                    if (value.DataTypeId == expectedDataTypeId) {
                        if (this.History.Contains(value.Timestamp)) {
                            var oldValue = this.History[value.Timestamp];
                            this.History.Replace(value);

                            var modifiedValue = oldValue.CreateModified(modificationInfo);
                            this.ModifiedHistory.Add(modifiedValue);

                            result.Update(OpcStatusCode.GoodEntryReplaced);
                        }
                        else {
                            this.History.Add(value);

                            var modifiedValue = value.CreateModified(modificationInfo);
                            this.ModifiedHistory.Add(modifiedValue);

                            result.Update(OpcStatusCode.GoodEntryInserted);
                        }
                    }
                    else {
                        result.Update(OpcStatusCode.BadTypeMismatch);
                    }
                }
            }

            return results;
        }

        #endregion

        #region ---------- Private methods ----------

        private void HandleNodeBeforeApplyChanges(object sender, OpcNodeChangesEventArgs e)
        {
            var timestamp = this.Node.Timestamp;

            if (timestamp != null && e.IsChangeOf(OpcNodeChanges.Value)) {
                var value = new OpcHistoryValue(this.Node.Value, timestamp.Value);

                if (this.History.Contains(value.Timestamp))
                    this.History.Replace(value);
                else
                    this.History.Add(value);
            }
        }

        #endregion
    }
}
