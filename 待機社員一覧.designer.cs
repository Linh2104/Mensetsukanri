namespace HL_塾管理
{
    partial class 待機社員一覧
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
            if (disposing && (components != null))
            {
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(待機社員一覧));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.登録ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rowMergeView = new RowMergeView();
            this.社員コード = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.名前 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.メイン言語 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.在職状態 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.待機状態 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.statusStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rowMergeView)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 525);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 7, 0);
            this.statusStrip1.Size = new System.Drawing.Size(902, 22);
            this.statusStrip1.TabIndex = 19;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.登録ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(123, 26);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // 登録ToolStripMenuItem
            // 
            this.登録ToolStripMenuItem.Name = "登録ToolStripMenuItem";
            this.登録ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.登録ToolStripMenuItem.Text = "教師登録";
            this.登録ToolStripMenuItem.Click += new System.EventHandler(this.登録ToolStripMenuItem_Click);
            // 
            // rowMergeView
            // 
            this.rowMergeView.AllowUserToAddRows = false;
            this.rowMergeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rowMergeView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.rowMergeView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.rowMergeView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.rowMergeView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.社員コード,
            this.名前,
            this.メイン言語,
            this.在職状態,
            this.待機状態});
            this.rowMergeView.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.rowMergeView.DefaultCellStyle = dataGridViewCellStyle7;
            this.rowMergeView.Key = "";
            this.rowMergeView.Location = new System.Drawing.Point(22, 18);
            this.rowMergeView.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.rowMergeView.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("rowMergeView.MergeColumnNames")));
            this.rowMergeView.MergeRowIndex = ((System.Collections.Generic.Dictionary<int, int>)(resources.GetObject("rowMergeView.MergeRowIndex")));
            this.rowMergeView.Name = "rowMergeView";
            this.rowMergeView.NoLink = ((System.Collections.Generic.List<object>)(resources.GetObject("rowMergeView.NoLink")));
            this.rowMergeView.RowHeadersWidth = 82;
            this.rowMergeView.RowTemplate.Height = 23;
            this.rowMergeView.Size = new System.Drawing.Size(819, 479);
            this.rowMergeView.TabIndex = 1;
            this.rowMergeView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.rowMergeView_DataError);
            // 
            // 社員コード
            // 
            dataGridViewCellStyle2.Font = new System.Drawing.Font("メイリオ", 9F);
            this.社員コード.DefaultCellStyle = dataGridViewCellStyle2;
            this.社員コード.HeaderText = "社員コード";
            this.社員コード.MaxInputLength = 20;
            this.社員コード.MinimumWidth = 10;
            this.社員コード.Name = "社員コード";
            this.社員コード.ReadOnly = true;
            // 
            // 名前
            // 
            dataGridViewCellStyle3.Font = new System.Drawing.Font("メイリオ", 9F);
            this.名前.DefaultCellStyle = dataGridViewCellStyle3;
            this.名前.HeaderText = "名前";
            this.名前.MaxInputLength = 100;
            this.名前.MinimumWidth = 10;
            this.名前.Name = "名前";
            this.名前.ReadOnly = true;
            // 
            // メイン言語
            // 
            dataGridViewCellStyle4.Font = new System.Drawing.Font("メイリオ", 9F);
            this.メイン言語.DefaultCellStyle = dataGridViewCellStyle4;
            this.メイン言語.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.メイン言語.DropDownWidth = 8;
            this.メイン言語.HeaderText = "メイン言語";
            this.メイン言語.Items.AddRange(new object[] {
            ".NET",
            "IOS"});
            this.メイン言語.MinimumWidth = 10;
            this.メイン言語.Name = "メイン言語";
            this.メイン言語.ReadOnly = true;
            this.メイン言語.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.メイン言語.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // 在職状態
            // 
            dataGridViewCellStyle5.Font = new System.Drawing.Font("メイリオ", 9F);
            this.在職状態.DefaultCellStyle = dataGridViewCellStyle5;
            this.在職状態.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.在職状態.DropDownWidth = 3;
            this.在職状態.HeaderText = "在職状態";
            this.在職状態.Items.AddRange(new object[] {
            "在職",
            "停職",
            "退職"});
            this.在職状態.MinimumWidth = 10;
            this.在職状態.Name = "在職状態";
            this.在職状態.ReadOnly = true;
            this.在職状態.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.在職状態.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // 待機状態
            // 
            dataGridViewCellStyle6.Font = new System.Drawing.Font("メイリオ", 9F);
            this.待機状態.DefaultCellStyle = dataGridViewCellStyle6;
            this.待機状態.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.待機状態.DropDownWidth = 3;
            this.待機状態.HeaderText = "待機状態";
            this.待機状態.Items.AddRange(new object[] {
            "待機",
            "現場中"});
            this.待機状態.MinimumWidth = 10;
            this.待機状態.Name = "待機状態";
            this.待機状態.ReadOnly = true;
            this.待機状態.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.待機状態.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // 待機社員一覧
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 547);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.rowMergeView);
            this.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "待機社員一覧";
            this.Text = "待機社員一覧";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.待機社員一覧_FormClosed);
            this.Load += new System.EventHandler(this.待機社員一覧_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rowMergeView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public RowMergeView rowMergeView;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 登録ToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn 社員コード;
        private System.Windows.Forms.DataGridViewTextBoxColumn 名前;
        private System.Windows.Forms.DataGridViewComboBoxColumn メイン言語;
        private System.Windows.Forms.DataGridViewComboBoxColumn 在職状態;
        private System.Windows.Forms.DataGridViewComboBoxColumn 待機状態;
    }
}