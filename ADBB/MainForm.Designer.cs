namespace ADBB
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.packageDataGrid = new System.Windows.Forms.DataGridView();
            this.PackageName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.PackageCellContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.launchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.uninstallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.deviceUpdateToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.iPConnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ipConnectToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ipDisconnectToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.apkInstallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deviceRebootToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deviceShutdownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.device更新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iP接続開始ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.開始ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.終了ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.packageDataGrid)).BeginInit();
            this.PackageCellContextMenuStrip.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // packageDataGrid
            // 
            this.packageDataGrid.AllowDrop = true;
            this.packageDataGrid.AllowUserToAddRows = false;
            this.packageDataGrid.AllowUserToDeleteRows = false;
            this.packageDataGrid.AllowUserToResizeColumns = false;
            this.packageDataGrid.AllowUserToResizeRows = false;
            this.packageDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.packageDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.packageDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PackageName});
            this.packageDataGrid.Location = new System.Drawing.Point(12, 53);
            this.packageDataGrid.MultiSelect = false;
            this.packageDataGrid.Name = "packageDataGrid";
            this.packageDataGrid.ReadOnly = true;
            this.packageDataGrid.RowHeadersVisible = false;
            this.packageDataGrid.RowTemplate.Height = 21;
            this.packageDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.packageDataGrid.Size = new System.Drawing.Size(920, 351);
            this.packageDataGrid.TabIndex = 1;
            this.packageDataGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellDoubleClick);
            this.packageDataGrid.DragDrop += new System.Windows.Forms.DragEventHandler(this.PackageDataGrid_DragDrop);
            this.packageDataGrid.DragEnter += new System.Windows.Forms.DragEventHandler(this.PackageDataGrid_DragEnter);
            // 
            // PackageName
            // 
            this.PackageName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PackageName.DataPropertyName = "Name";
            this.PackageName.HeaderText = "Package";
            this.PackageName.Name = "PackageName";
            this.PackageName.ReadOnly = true;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(345, 28);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(587, 19);
            this.textBox1.TabIndex = 3;
            // 
            // PackageCellContextMenuStrip
            // 
            this.PackageCellContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.launchToolStripMenuItem,
            this.toolStripSeparator1,
            this.uninstallToolStripMenuItem,
            this.abortToolStripMenuItem});
            this.PackageCellContextMenuStrip.Name = "PackageCellContextMenuStrip";
            this.PackageCellContextMenuStrip.Size = new System.Drawing.Size(146, 76);
            // 
            // launchToolStripMenuItem
            // 
            this.launchToolStripMenuItem.Name = "launchToolStripMenuItem";
            this.launchToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.launchToolStripMenuItem.Text = "起動";
            this.launchToolStripMenuItem.Click += new System.EventHandler(this.起動ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(142, 6);
            // 
            // uninstallToolStripMenuItem
            // 
            this.uninstallToolStripMenuItem.Name = "uninstallToolStripMenuItem";
            this.uninstallToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.uninstallToolStripMenuItem.Text = "アンインストール";
            this.uninstallToolStripMenuItem.Click += new System.EventHandler(this.uninstallToolStripMenuItem_Click);
            // 
            // abortToolStripMenuItem
            // 
            this.abortToolStripMenuItem.Name = "abortToolStripMenuItem";
            this.abortToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.abortToolStripMenuItem.Text = "強制終了";
            this.abortToolStripMenuItem.Click += new System.EventHandler(this.abortToolStripMenuItem2_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DisplayMember = "DispName";
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(56, 27);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(186, 20);
            this.comboBox1.TabIndex = 5;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 407);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(944, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Visible = false;
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar1.Visible = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deviceUpdateToolStripMenuItem1,
            this.iPConnectToolStripMenuItem,
            this.apkInstallToolStripMenuItem,
            this.DeviceToolStripMenuItem,
            this.configToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(944, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // deviceUpdateToolStripMenuItem1
            // 
            this.deviceUpdateToolStripMenuItem1.Name = "deviceUpdateToolStripMenuItem1";
            this.deviceUpdateToolStripMenuItem1.Size = new System.Drawing.Size(78, 20);
            this.deviceUpdateToolStripMenuItem1.Text = "Device更新";
            this.deviceUpdateToolStripMenuItem1.Click += new System.EventHandler(this.DeviceUpdateToolStripMenuItem_Click);
            // 
            // iPConnectToolStripMenuItem
            // 
            this.iPConnectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ipConnectToolStripMenuItem1,
            this.ipDisconnectToolStripMenuItem1});
            this.iPConnectToolStripMenuItem.Name = "iPConnectToolStripMenuItem";
            this.iPConnectToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.iPConnectToolStripMenuItem.Text = "IP接続";
            // 
            // ipConnectToolStripMenuItem1
            // 
            this.ipConnectToolStripMenuItem1.Name = "ipConnectToolStripMenuItem1";
            this.ipConnectToolStripMenuItem1.Size = new System.Drawing.Size(98, 22);
            this.ipConnectToolStripMenuItem1.Text = "開始";
            this.ipConnectToolStripMenuItem1.Click += new System.EventHandler(this.IpConnectToolStripMenuItem_Click);
            // 
            // ipDisconnectToolStripMenuItem1
            // 
            this.ipDisconnectToolStripMenuItem1.Name = "ipDisconnectToolStripMenuItem1";
            this.ipDisconnectToolStripMenuItem1.Size = new System.Drawing.Size(98, 22);
            this.ipDisconnectToolStripMenuItem1.Text = "終了";
            this.ipDisconnectToolStripMenuItem1.Click += new System.EventHandler(this.IpDisconnectToolStripMenuItem_Click);
            // 
            // apkInstallToolStripMenuItem
            // 
            this.apkInstallToolStripMenuItem.Name = "apkInstallToolStripMenuItem";
            this.apkInstallToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.apkInstallToolStripMenuItem.Text = "APKインストール";
            this.apkInstallToolStripMenuItem.Click += new System.EventHandler(this.APKInstallToolStripMenuItem_Click);
            // 
            // DeviceToolStripMenuItem
            // 
            this.DeviceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deviceRebootToolStripMenuItem,
            this.deviceShutdownToolStripMenuItem});
            this.DeviceToolStripMenuItem.Name = "DeviceToolStripMenuItem";
            this.DeviceToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.DeviceToolStripMenuItem.Text = "端末";
            // 
            // deviceRebootToolStripMenuItem
            // 
            this.deviceRebootToolStripMenuItem.Name = "deviceRebootToolStripMenuItem";
            this.deviceRebootToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.deviceRebootToolStripMenuItem.Text = "再起動";
            this.deviceRebootToolStripMenuItem.Click += new System.EventHandler(this.DeviceRebootToolStripMenuItem_Click);
            // 
            // deviceShutdownToolStripMenuItem
            // 
            this.deviceShutdownToolStripMenuItem.Name = "deviceShutdownToolStripMenuItem";
            this.deviceShutdownToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.deviceShutdownToolStripMenuItem.Text = "シャットダウン";
            this.deviceShutdownToolStripMenuItem.Click += new System.EventHandler(this.DeviceShutdownToolStripMenuItem_Click_1);
            // 
            // configToolStripMenuItem
            // 
            this.configToolStripMenuItem.Name = "configToolStripMenuItem";
            this.configToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.configToolStripMenuItem.Text = "設定";
            this.configToolStripMenuItem.Click += new System.EventHandler(this.ConfigToolStripMenuItem_Click);
            // 
            // device更新ToolStripMenuItem
            // 
            this.device更新ToolStripMenuItem.Name = "device更新ToolStripMenuItem";
            this.device更新ToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
            this.device更新ToolStripMenuItem.Text = "Device更新";
            this.device更新ToolStripMenuItem.Click += new System.EventHandler(this.DeviceUpdateToolStripMenuItem_Click);
            // 
            // iP接続開始ToolStripMenuItem
            // 
            this.iP接続開始ToolStripMenuItem.Name = "iP接続開始ToolStripMenuItem";
            this.iP接続開始ToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.iP接続開始ToolStripMenuItem.Text = "IP接続";
            // 
            // 開始ToolStripMenuItem
            // 
            this.開始ToolStripMenuItem.Name = "開始ToolStripMenuItem";
            this.開始ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.開始ToolStripMenuItem.Text = "開始";
            this.開始ToolStripMenuItem.Click += new System.EventHandler(this.IpConnectToolStripMenuItem_Click);
            // 
            // 終了ToolStripMenuItem
            // 
            this.終了ToolStripMenuItem.Name = "終了ToolStripMenuItem";
            this.終了ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.終了ToolStripMenuItem.Text = "終了";
            this.終了ToolStripMenuItem.Click += new System.EventHandler(this.IpDisconnectToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(264, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "package filter";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "device";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 429);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.packageDataGrid);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "ADBB";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.packageDataGrid)).EndInit();
            this.PackageCellContextMenuStrip.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView packageDataGrid;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ContextMenuStrip PackageCellContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem uninstallToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem launchToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem device更新ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iP接続開始ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 開始ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 終了ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deviceUpdateToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem iPConnectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ipConnectToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ipDisconnectToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem apkInstallToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem abortToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn PackageName;
        private System.Windows.Forms.ToolStripMenuItem deviceRebootToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deviceShutdownToolStripMenuItem;
    }
}

