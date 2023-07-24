// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace NodeValueCache
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    using Opc.UaFx;
    using Opc.UaFx.Client;

    public class OpcClientNodeItemManager : IDisposable
    {
        #region ---------- Private readonly fields ----------

        private readonly OpcClient client;

        private readonly Dictionary<string, NodeItem> items;

        private readonly List<(NodeItem item, bool added, object? newSyncObject)> itemChanges;

        private readonly object syncRoot;

        #endregion

        #region ---------- Private fields ----------

        private Thread? workerThread;

        private SemaphoreSlim? workerThreadSemaphore;

        private CancellationTokenSource? workerThreadTcs;

        #endregion

        #region ---------- Public constructors ----------

        public OpcClientNodeItemManager(OpcClient client)
            : base()
        {
            this.client = client;

            this.syncRoot = new();
            this.items = new();
            this.itemChanges = new();
            this.MonitoredItemsPerSubscriptionLimit = 1000;
        }

        #endregion

        #region ---------- public properties ----------

        public int MonitoredItemsPerSubscriptionLimit
        {
            get;
            set;
        }

        #endregion

        #region ---------- Public indexers ----------

        public NodeItem this[string key]
        {
            get => this.GetItem(key);
        }

        #endregion

        #region ---------- Public methods ----------

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Start()
        {
            lock (this.syncRoot) {
                if (this.workerThread is not null)
                    return;

                this.workerThreadTcs = new CancellationTokenSource();
                this.workerThreadSemaphore = new SemaphoreSlim(0);

                this.workerThread = new Thread(
                        () => this.RunWorkerThread(this.workerThreadSemaphore, this.workerThreadTcs.Token));

                this.workerThread.Start();
            }
        }

        public void Stop()
        {
            Thread thread;
            CancellationTokenSource cts;

            lock (this.syncRoot) {
                if (this.workerThread is null)
                    return;

                // Cancel the thread and wait for it to finish.
                thread = this.workerThread;
                cts = this.workerThreadTcs!;

                cts.Cancel();

                this.workerThread = null;
                this.workerThreadTcs = null;

                this.items.Clear();
                this.itemChanges.Clear();
            }

            // Need to wait outside of the lock, to avoid a deadlock.
            thread.Join();
            cts.Dispose();
        }

        public NodeItem AddItem(string key, OpcNodeId nodeId)
        {
            var item = new NodeItem(nodeId);
            this.AddItem(key, item);

            return item;
        }

        public void AddItem(string key, NodeItem item)
        {
            lock (this.syncRoot) {
                if (this.workerThread is null)
                    throw new InvalidOperationException();

                object newItemSyncObject;

                lock (item) {
                    if (item.Manager is not null)
                        throw new InvalidOperationException("Item is already added to a manager.");

                    item.Manager = this;

                    // Create a sync object that we use in the item's event handler to check
                    // whether we are still allowed to raise the event and set the value.
                    // For example, if an item is removed and added again, its value is
                    // cleared, and may only be set again when the new handler raises an
                    // event, not the old handler (as the values between them may be lost).
                    item.SyncObject = newItemSyncObject = new();
                }

                this.items.Add(key, item);
                this.itemChanges.Add((item, true, newItemSyncObject));

                this.workerThreadSemaphore!.Release();
            }
        }

        public void RemoveItem(string key)
        {
            lock (this.syncRoot) {
                if (this.workerThread is null)
                    throw new InvalidOperationException();

                var item = this.items[key];

                this.itemChanges.Add((item, false, null));
                this.items.Remove(key);

                lock (item) {
                    item.Manager = null;
                    item.ValueCore = null;
                }

                this.workerThreadSemaphore!.Release();
            }
        }

        public NodeItem GetItem(string key)
        {
            return this.items[key];
        }

        #endregion

        #region ---------- Protected methods ----------

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                this.Stop();
        }

        #endregion

        #region ---------- Private methods ----------

        private void RunWorkerThread(SemaphoreSlim semaphore, CancellationToken cancellationToken)
        {
            using var sleepSemaphore = new SemaphoreSlim(0);
            var itemsDictionary = new Dictionary<NodeItem, OpcMonitoredItem>();

            // Create list as store for subscriptions in case the amount of monitoredItems > 1000.
            var listOfSubscriptions = new List<OpcSubscription>();
            var touchedSubscriptions = new HashSet<OpcSubscription>();

            // The local list of changes.
            var localChanges = new List<(NodeItem item, bool added, object? newSyncObject)>();

            try {
                while (true) {
                    touchedSubscriptions.Clear();
                    localChanges.Clear();

                    // Wait for changes.
                    semaphore.Wait(cancellationToken);

                    // Need to copy the changes, so that other threads can continue to
                    // call methods on the manager.
                    lock (this.itemChanges) {
                        localChanges.AddRange(this.itemChanges);
                        this.itemChanges.Clear();
                    }

                    var valuesToWrite = new Dictionary<NodeItem, OpcWriteNode>();
                    //client.WriteNodes();

                    foreach (var entry in localChanges) {
                        OpcSubscription? touchedSubscription = default;

                        if (entry.added) {
                            var monitoredItem = new OpcMonitoredItem(entry.item.NodeId, Opc.UaFx.OpcAttribute.Value);

                            monitoredItem.DataChangeReceived += (s, e) => {
                                // We need to lock first on our syncRoot object and then on the
                                // item, because without the first lock, a deadlock would be
                                // possible when the event handler calls a method that locks on our
                                // syncRoot.
                                // The lock on our syncRoot ensures that no other thread can remove
                                // the item from our manager until we raise the event handler, even
                                // when we raise the event handler outside of the item's lock.
                                lock (this.syncRoot) {
                                    lock (entry.item) {
                                        // Check that the item still belongs to this manager and that
                                        // the sync object is still the same.
                                        if (entry.item.Manager != this ||
                                                entry.item.SyncObject != entry.newSyncObject) {
                                            return;
                                        }

                                        // Update the value and raise the event. Updating the
                                        // value needs to be within the lock on the item, as we
                                        // need to acquire it when retrieving the value.
                                        entry.item.ValueCore = e.Item.Value;
                                    }

                                    // We raise the event outside of the item's lock, so that the
                                    // current thread can wait for other threads that also try to
                                    // access the item's value.
                                    // TODO: Retrieve the MulticastDelegate of the event within the
                                    // above lock, and call it outside of the above lock.
                                    entry.item.RaiseValueChanged(entry.item.ValueCore);
                                }
                            };

                            // Find a subscription that doesn't have reached the maximum number
                            // of monitored items.
                            var monitoredItemAdded = false;

                            foreach (var item in listOfSubscriptions) {
                                if (item.MonitoredItems.Count < this.MonitoredItemsPerSubscriptionLimit) {
                                    item.AddMonitoredItem(monitoredItem);
                                    touchedSubscription = item;
                                    monitoredItemAdded = true;
                                    break;
                                }
                            }

                            if (!monitoredItemAdded) {
                                while (true) {
                                    try {
                                        touchedSubscription = this.client.SubscribeNodes();
                                        touchedSubscription.AddMonitoredItem(monitoredItem);
                                        listOfSubscriptions.Add(touchedSubscription);
                                        break;
                                    }
                                    catch (Exception ex) when (ex is not OperationCanceledException) {
                                        // Wait a bit and try again.
                                        sleepSemaphore.Wait(1000, cancellationToken);
                                    }
                                }
                            }

                            itemsDictionary.Add(entry.item, monitoredItem);
                        }
                        else {
                            var monitoredItem = itemsDictionary[entry.item];

                            foreach (var item in listOfSubscriptions) {
                                if (item.MonitoredItems.Contains(monitoredItem)) {
                                    item.RemoveMonitoredItem(monitoredItem);
                                    touchedSubscription = item;
                                    break;
                                }
                            }

                            itemsDictionary.Remove(entry.item);
                        }
                        else  /* 3. Fall: Write Value */ {
                            lock (entry.Item) {
                                valuesToWrite[entry.item] = entry.ValueToWrite;
                            }
                        }

                        // Add the touched subscription if not yet present.
                        touchedSubscriptions.Add(touchedSubscription!);
                    }

                    foreach (var item in touchedSubscriptions) {
                        while (true) {
                            try {
                                item.ApplyChanges();
                                break;
                            }
                            catch (Exception ex) when (ex is not OperationCanceledException) {
                                // Wait a bit and try again.
                                sleepSemaphore.Wait(1000, cancellationToken);
                            }
                        }
                    }

                    if (valuesToWrite.Count > 0) {
                        try {
                            this.client.WriteNodes(valuesToWrite.Values);
                        }
                        catch (OpcException) {
                            // TODO, Ingore.
                        }
                    }

                    // TODO: Check whether the newly added monitored items have IsCreated=True.
                    // Otherwise, set the item value to a value with a Bad status.
                }
            }
            catch (OperationCanceledException) {
                // The token was canceled.
                return;
            }
        }

        #endregion
    }
}
