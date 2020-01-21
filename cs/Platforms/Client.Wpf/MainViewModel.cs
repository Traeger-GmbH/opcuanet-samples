namespace Client.Wpf
{
    using System;

    using Opc.UaFx;
    using Opc.UaFx.Client;

    public class MainViewModel : ViewModel
    {
        #region ---------- Private fields ----------

        private Uri address;
        private string addressStatus;

        private OpcClient client;
        private OpcSubscription subscription;

        private OpcNodeId nodeId;
        private string nodeStatus;
        private object nodeValue;

        #endregion

        #region ---------- Public constructors ----------

        public MainViewModel()
            : base()
        {
            this.address = new Uri("opc.tcp://localhost:4840");
            this.addressStatus = "Enter address of server.";

            this.nodeId = "ns=2;s=MyNode";
            this.nodeStatus = "Enter a node identifier and click 'Subscribe'.";

            this.SubscribeCommand = new DelegateCommand(
                    execute: (_) => this.Subscribe(),
                    canExecute: (_) => this.AddressIsValid && this.NodeIdIsValid);
        }

        #endregion

        #region ---------- Public properties ----------

        public string Address
        {
            get => this.address?.ToString();
            set
            {
                this.RaisePropertyChanging(nameof(this.Address));
                this.RaisePropertyChanging(nameof(this.AddressIsValid));

                if (Uri.TryCreate(value, UriKind.Absolute, out var result)
                        && (result.Scheme == "opc.tcp" || result.Scheme == "https")) {
                    this.subscription?.Unsubscribe();
                    this.client?.Disconnect();

                    this.client = null;
                    this.address = result;

                    this.RaisePropertyChanged(nameof(this.Address));
                    this.RaisePropertyChanged(nameof(this.AddressIsValid));

                    this.SubscribeCommand.RaiseCanExecuteChanged();
                }
                else {
                    this.AddressStatus = "Invalid address uri.";
                }
            }
        }

        public bool AddressIsValid
        {
            get => this.address != null;
        }

        public string AddressStatus
        {
            get => this.addressStatus;
            set
            {
                this.RaisePropertyChanging(nameof(this.AddressStatus));
                this.addressStatus = value;
                this.RaisePropertyChanged(nameof(this.AddressStatus));
            }
        }

        public string NodeId
        {
            get => this.nodeId?.ToString();
            set
            {
                this.RaisePropertyChanging(nameof(this.NodeId));
                this.RaisePropertyChanging(nameof(this.NodeIdIsValid));

                if (OpcNodeId.TryParse(value, out var result)) {
                    this.nodeId = result;

                    this.RaisePropertyChanged(nameof(this.NodeId));
                    this.RaisePropertyChanged(nameof(this.NodeIdIsValid));

                    this.SubscribeCommand.RaiseCanExecuteChanged();
                }
                else {
                    this.NodeStatus = "Invalid node identifier.";
                }
            }
        }

        public bool NodeIdIsValid
        {
            get => this.nodeId != null && this.nodeId != OpcNodeId.Null;
        }

        public string NodeStatus
        {
            get => this.nodeStatus;
            set
            {
                this.RaisePropertyChanging(nameof(this.NodeStatus));
                this.nodeStatus = value;
                this.RaisePropertyChanged(nameof(this.NodeStatus));
            }
        }

        public object NodeValue
        {
            get => this.nodeValue;
            set
            {
                this.RaisePropertyChanging(nameof(this.NodeValue));
                this.nodeValue = value;
                this.RaisePropertyChanged(nameof(this.NodeValue));
            }
        }

        public DelegateCommand SubscribeCommand
        {
            get;
        }

        #endregion

        #region ---------- Public methods ----------

        public void Subscribe()
        {
            try {
                if (this.client == null) {
                    this.client = new OpcClient(this.address);
                    this.client.Connect();
                }

                if (this.subscription != null)
                    this.subscription.Unsubscribe();

                this.subscription = this.client.SubscribeDataChange(
                        this.nodeId,
                        this.HandleDataChangeReceived);

                var monitoredItem = this.subscription.MonitoredItems[0];
                this.NodeStatus = monitoredItem.Status.Error.Description;
            }
            catch (Exception ex) {
                this.NodeStatus = ex.Message;
            }
        }

        #endregion

        #region ---------- Private methods ----------

        private void HandleDataChangeReceived(object sender, OpcDataChangeReceivedEventArgs e)
        {
            var value = e.Item.Value;

            this.NodeStatus = value.Status.Description;
            this.NodeValue = $"{value.Value} ({e.MonitoredItem.NodeId})";
        }

        #endregion
    }
}
