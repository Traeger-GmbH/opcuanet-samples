<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

   'Form overrides dispose to clean up the component list.
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso (components IsNot Nothing) Then
            components.Dispose()
        End If

        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.nodesImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.tableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.serverAddressTextBox = New System.Windows.Forms.TextBox()
        Me.connectButton = New System.Windows.Forms.Button()
        Me.nodesTreeView = New System.Windows.Forms.TreeView()
        Me.tableLayoutPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'nodesImageList
        '
        Me.nodesImageList.ImageStream = CType(resources.GetObject("nodesImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.nodesImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.nodesImageList.Images.SetKeyName(0, "ObjectNode.png")
        Me.nodesImageList.Images.SetKeyName(1, "FolderNode.png")
        Me.nodesImageList.Images.SetKeyName(2, "MethodNode.png")
        Me.nodesImageList.Images.SetKeyName(3, "VariableNode.png")
        Me.nodesImageList.Images.SetKeyName(4, "PropertyNode.png")
        '
        'tableLayoutPanel
        '
        Me.tableLayoutPanel.ColumnCount = 2
        Me.tableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tableLayoutPanel.Controls.Add(Me.serverAddressTextBox, 0, 0)
        Me.tableLayoutPanel.Controls.Add(Me.connectButton, 1, 0)
        Me.tableLayoutPanel.Controls.Add(Me.nodesTreeView, 0, 1)
        Me.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tableLayoutPanel.Location = New System.Drawing.Point(0, 0)
        Me.tableLayoutPanel.Name = "tableLayoutPanel"
        Me.tableLayoutPanel.RowCount = 2
        Me.tableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tableLayoutPanel.Size = New System.Drawing.Size(334, 561)
        Me.tableLayoutPanel.TabIndex = 0
        '
        'serverAddressTextBox
        '
        Me.serverAddressTextBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.serverAddressTextBox.Location = New System.Drawing.Point(3, 4)
        Me.serverAddressTextBox.Name = "serverAddressTextBox"
        Me.serverAddressTextBox.Size = New System.Drawing.Size(247, 20)
        Me.serverAddressTextBox.TabIndex = 0
        Me.serverAddressTextBox.Text = "opc.tcp://localhost:4840"
        '
        'connectButton
        '
        Me.connectButton.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.connectButton.Location = New System.Drawing.Point(256, 3)
        Me.connectButton.Name = "connectButton"
        Me.connectButton.Size = New System.Drawing.Size(75, 23)
        Me.connectButton.TabIndex = 1
        Me.connectButton.Text = "Connect"
        Me.connectButton.UseVisualStyleBackColor = True
        '
        'nodesTreeView
        '
        Me.tableLayoutPanel.SetColumnSpan(Me.nodesTreeView, 2)
        Me.nodesTreeView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.nodesTreeView.ImageIndex = 0
        Me.nodesTreeView.ImageList = Me.nodesImageList
        Me.nodesTreeView.Location = New System.Drawing.Point(3, 32)
        Me.nodesTreeView.Name = "nodesTreeView"
        Me.nodesTreeView.SelectedImageIndex = 0
        Me.nodesTreeView.Size = New System.Drawing.Size(328, 526)
        Me.nodesTreeView.TabIndex = 2
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(334, 561)
        Me.Controls.Add(Me.tableLayoutPanel)
        Me.MinimumSize = New System.Drawing.Size(350, 600)
        Me.Name = "Form1"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Client.NodeExplorer"
        Me.tableLayoutPanel.ResumeLayout(False)
        Me.tableLayoutPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Private nodesImageList As System.Windows.Forms.ImageList
    Private tableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Private serverAddressTextBox As System.Windows.Forms.TextBox
    Private WithEvents connectButton As System.Windows.Forms.Button
    Private WithEvents nodesTreeView As System.Windows.Forms.TreeView
End Class
