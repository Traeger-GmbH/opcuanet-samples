namespace Client.NodeExplorer
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.nodesImageList = new System.Windows.Forms.ImageList(this.components);
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.serverAddressTextBox = new System.Windows.Forms.TextBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.nodesTreeView = new System.Windows.Forms.TreeView();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.Controls.Add(this.serverAddressTextBox, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.connectButton, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.nodesTreeView, 0, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(352, 570);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // serverAddressTextBox
            // 
            this.serverAddressTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.serverAddressTextBox.Location = new System.Drawing.Point(3, 4);
            this.serverAddressTextBox.Name = "serverAddressTextBox";
            this.serverAddressTextBox.Size = new System.Drawing.Size(265, 20);
            this.serverAddressTextBox.TabIndex = 0;
            this.serverAddressTextBox.Text = "opc.tcp://localhost:4840";
            // 
            // connectButton
            // 
            this.connectButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.connectButton.Location = new System.Drawing.Point(274, 3);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(75, 23);
            this.connectButton.TabIndex = 1;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.HandleConnectButtonClick);
            // 
            // nodesImageList
            // 
            this.nodesImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("nodesImageList.ImageStream")));
            this.nodesImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.nodesImageList.Images.SetKeyName(0, "ObjectNode.png");
            this.nodesImageList.Images.SetKeyName(1, "MethodNode.png");
            this.nodesImageList.Images.SetKeyName(2, "VariableNode.png");
            this.nodesImageList.Images.SetKeyName(3, "FolderNode.png");
            this.nodesImageList.Images.SetKeyName(4, "FileNode.png");
            this.nodesImageList.Images.SetKeyName(5, "PropertyNode.png");
            // 
            // nodesTreeView
            // 
            this.tableLayoutPanel.SetColumnSpan(this.nodesTreeView, 2);
            this.nodesTreeView.ImageIndex = 0;
            this.nodesTreeView.ImageList = this.nodesImageList;
            this.nodesTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nodesTreeView.Location = new System.Drawing.Point(3, 32);
            this.nodesTreeView.Name = "nodesTreeView";
            this.nodesTreeView.SelectedImageIndex = 0;
            this.nodesTreeView.Size = new System.Drawing.Size(346, 535);
            this.nodesTreeView.TabIndex = 2;
            this.nodesTreeView.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.HandleNodesTreeViewAfterExpand);
            this.nodesTreeView.ImageList = this.nodesImageList;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 570);
            this.Controls.Add(this.tableLayoutPanel);
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.MinimumSize = new System.Drawing.Size(350, 600);
            this.Name = "Client.NodeExplorer";
            this.Size = new System.Drawing.Size(350, 600);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Client.NodeExplorer";
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList nodesImageList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.TextBox serverAddressTextBox;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.TreeView nodesTreeView;
    }
}

