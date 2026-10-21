// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace Client.NodeExplorer
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    using Opc.UaFx;
    using Opc.UaFx.Client;

    public partial class Form1 : Form
    {
        private readonly OpcClient client;


        public Form1()
            : base()
        {
            this.client = new OpcClient();
            this.InitializeComponent();

            this.LoadNodeImages();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            this.client.Disconnect();
            base.OnFormClosing(e);
        }

        private bool Browse()
        {
            var result = false;

            try {
                var node = this.client.BrowseNode(OpcObjectTypes.RootFolder);
                result = this.Browse(node);
            }
            catch (OpcException ex) {
                this.ShowMessage("Browse", "Failed to browse: " + ex.Message);
            }

            return result;
        }

        private bool Browse(OpcNodeInfo node)
        {
            this.nodesTreeView.Nodes.Clear();
            return this.Browse(node, this.nodesTreeView.Nodes);
        }

        private bool Browse(OpcNodeInfo node, TreeNodeCollection treeNodes)
        {
            var result = false;

            try {
                var treeNode = treeNodes.Add(node.DisplayName.Value);

                if (node is OpcObjectNodeInfo) {
                    treeNode.ImageIndex = 0;

                    if (node.Reference.TypeDefinitionId == Opc.Ua.ObjectTypeIds.FolderType)
                        treeNode.ImageIndex = 1;
                }
                else if (node is OpcMethodNodeInfo) {
                    treeNode.ImageIndex = 2;
                }
                else if (node is OpcVariableNodeInfo) {
                    treeNode.ImageIndex = 3;

                    if (node.Reference.ReferenceType == OpcReferenceType.HasProperty)
                        treeNode.ImageIndex = 4;
                }

                treeNode.Tag = node;

                treeNode.Nodes.Add("Browsing...");
                result = true;
            }
            catch (OpcException ex) {
                this.ShowMessage("Browse", "Failed to browse: " + ex.Message);
            }

            return result;
        }

        private bool Connect()
        {
            var result = false;

            try {
                this.client.Connect();
                result = true;
            }
            catch (OpcException ex) {
                this.ShowMessage("Connect", "Failed to connect: " + ex.Message);
            }

            return result;
        }

        private void HandleConnectButtonClick(object sender, EventArgs e)
        {
            this.client.Disconnect();

            if (Uri.TryCreate(this.serverAddressTextBox.Text, UriKind.Absolute, out var serverAddress)) {
                this.client.ServerAddress = serverAddress;

                if (this.Connect())
                    this.Browse();
            }
            else {
                this.ShowMessage("Connect", "Invalid server address.");
            }
        }

        private void HandleNodesTreeViewAfterExpand(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is OpcNodeInfo node) {
                var treeNodes = e.Node.Nodes;
                treeNodes.Clear();

                foreach (var childNode in node.Children()) {
                    if (!this.Browse(childNode, treeNodes))
                        break;
                }
            }
        }

        private void LoadNodeImages()
        {
            // The order defines the image indices used by Browse.
            var names = new[] { "ObjectNode", "FolderNode", "MethodNode", "VariableNode", "PropertyNode" };
            var assembly = typeof(Form1).Assembly;

            foreach (var name in names) {
                var key = name + ".png";

                using (var stream = assembly.GetManifestResourceStream("Client.NodeExplorer.Resources." + key))
                using (var image = Image.FromStream(stream)) {
                    // Image.FromStream needs its stream for the lifetime of the image, so add a copy.
                    this.nodesImageList.Images.Add(key, new Bitmap(image));
                }
            }
        }

        private void ShowMessage(string caption, string text)
        {
            MessageBox.Show(
                    owner: this,
                    text,
                    caption,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
        }
    }
}
