// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace Client.SignalR
{
    using System;
    using System.Threading;
    using System.Threading.Channels;

    using Microsoft.AspNetCore.SignalR;

    using Opc.UaFx;
    using Opc.UaFx.Client;

    public class SignalRHub : Hub
    {
        #region ---------- Private static fields ----------

        private readonly OpcClient client;

        #endregion

        #region ---------- Public constructors ----------

        public SignalRHub()
        {
            this.client = new OpcClient("opc.tcp://localhost:4840");
            this.client.Connect();
        }

        #endregion

        #region ---------- Public methods ----------

        public ChannelReader<object> SubscribeNodeValueChanges(
                string nodeId,
                CancellationToken cancellationToken)
        {
            var channel = Channel.CreateUnbounded<object>();

            if (this.client.State == OpcClientState.Connected) {
                var subscription = this.client.SubscribeDataChange(nodeId, HandleDataChange);
                cancellationToken.Register(() => subscription.Unsubscribe());
            }
            else {
                channel.Writer.TryComplete(error: new HubException("Unexpected OPC Client state: " + client.State));
            }

            async void HandleDataChange(object sender, OpcDataChangeReceivedEventArgs e)
            {
                try {
                    if (!cancellationToken.IsCancellationRequested) {
                        await channel.Writer.WriteAsync(
                                e.Item.Value.Value,
                                cancellationToken);
                    }
                }
                catch (Exception exception) {
                    channel.Writer.TryComplete(error: exception);
                }
            }

            return channel.Reader;
        }

        #endregion

        #region ---------- Public methods ----------

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                this.client.Dispose();
            }
        }

        #endregion
    }
}
