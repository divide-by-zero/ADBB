namespace GuiAdb
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.PackageName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.PackageCellContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.アンインストールToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.起動ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.device更新ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.iP接続ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.開始ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.終了ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.device更新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iP接続開始ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.開始ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.終了ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.aPKインストールToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.PackageCellContextMenuStrip.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button1.BackgroundImage = global::GuiAdb.Properties.Resources.ic_replay_white_36dp;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Location = new System.Drawing.Point(900, -10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(32, 32);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PackageName});
            this.dataGridView1.Location = new System.Drawing.Point(12, 53);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(920, 351);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellDoubleClick);
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
            this.アンインストールToolStripMenuItem,
            this.起動ToolStripMenuItem});
            this.PackageCellContextMenuStrip.Name = "PackageCellContextMenuStrip";
            this.PackageCellContextMenuStrip.Size = new System.Drawing.Size(146, 48);
            // 
            // アンインストールToolStripMenuItem
            // 
            this.アンインストールToolStripMenuItem.Name = "アンインストールToolStripMenuItem";
            this.アンインストールToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.アンインストールToolStripMenuItem.Text = "アンインストール";
            this.アンインストールToolStripMenuItem.Click += new System.EventHandler(this.アンインストールToolStripMenuItem_Click);
            // 
            // 起動ToolStripMenuItem
            // 
            this.起動ToolStripMenuItem.Name = "起動ToolStripMenuItem";
            this.起動ToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.起動ToolStripMenuItem.Text = "起動";
            this.起動ToolStripMenuItem.Click += new System.EventHandler(this.起動ToolStripMenuItem_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DisplayMember = "DispName";
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
            this.device更新ToolStripMenuItem1,
            this.iP接続ToolStripMenuItem,
            this.aPKインストールToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(944, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // device更新ToolStripMenuItem1
            // 
            this.device更新ToolStripMenuItem1.Name = "device更新ToolStripMenuItem1";
            this.device更新ToolStripMenuItem1.Size = new System.Drawing.Size(78, 20);
            this.device更新ToolStripMenuItem1.Text = "Device更新";
            this.device更新ToolStripMenuItem1.Click += new System.EventHandler(this.Device更新ToolStripMenuItem_Click);
            // 
            // iP接続ToolStripMenuItem
            // 
            this.iP接続ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.開始ToolStripMenuItem1,
            this.終了ToolStripMenuItem1});
            this.iP接続ToolStripMenuItem.Name = "iP接続ToolStripMenuItem";
            this.iP接続ToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.iP接続ToolStripMenuItem.Text = "IP接続";
            // 
            // 開始ToolStripMenuItem1
            // 
            this.開始ToolStripMenuItem1.Name = "開始ToolStripMenuItem1";
            this.開始ToolStripMenuItem1.Size = new System.Drawing.Size(98, 22);
            this.開始ToolStripMenuItem1.Text = "開始";
            this.開始ToolStripMenuItem1.Click += new System.EventHandler(this.開始ToolStripMenuItem_Click);
            // 
            // 終了ToolStripMenuItem1
            // 
            this.終了ToolStripMenuItem1.Name = "終了ToolStripMenuItem1";
            this.終了ToolStripMenuItem1.Size = new System.Drawing.Size(98, 22);
            this.終了ToolStripMenuItem1.Text = "終了";
            this.終了ToolStripMenuItem1.Click += new System.EventHandler(this.終了ToolStripMenuItem_Click);
            // 
            // device更新ToolStripMenuItem
            // 
            this.device更新ToolStripMenuItem.Name = "device更新ToolStripMenuItem";
            this.device更新ToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
            this.device更新ToolStripMenuItem.Text = "Device更新";
            this.device更新ToolStripMenuItem.Click += new System.EventHandler(this.Device更新ToolStripMenuItem_Click);
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
            this.開始ToolStripMenuItem.Click += new System.EventHandler(this.開始ToolStripMenuItem_Click);
            // 
            // 終了ToolStripMenuItem
            // 
            this.終了ToolStripMenuItem.Name = "終了ToolStripMenuItem";
            this.終了ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.終了ToolStripMenuItem.Text = "終了";
            this.終了ToolStripMenuItem.Click += new System.EventHandler(this.終了ToolStripMenuItem_Click);
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
            // aPKインストールToolStripMenuItem
            // 
            this.aPKインストールToolStripMenuItem.Name = "aPKインストールToolStripMenuItem";
            this.aPKインストールToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.aPKインストールToolStripMenuItem.Text = "APKインストール";
            this.aPKインストールToolStripMenuItem.Click += new System.EventHandler(this.APKインストールToolStripMenuItem_Click);
            // 
            // Form1
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
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "ADBB";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.PackageCellContextMenuStrip.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn PackageName;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ContextMenuStrip PackageCellContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem アンインストールToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 起動ToolStripMenuItem;
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
        private System.Windows.Forms.ToolStripMenuItem device更新ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem iP接続ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 開始ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 終了ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aPKインストールToolStripMenuItem;
    }
}

