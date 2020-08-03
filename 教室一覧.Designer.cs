namespace HL_塾管理
{
    partial class 教室一覧
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.変更ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.削除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stoolStripStatusLabel2 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.bt_search = new System.Windows.Forms.Button();
            this.txt_search = new System.Windows.Forms.TextBox();
            this.gv_classroomsInfo = new System.Windows.Forms.DataGridView();
            this.教室コード = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.出勤機コード = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.出勤機場所 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.備考 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1.SuspendLayout();
            this.stoolStripStatusLabel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gv_classroomsInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.変更ToolStripMenuItem,
            this.削除ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(99, 48);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // 変更ToolStripMenuItem
            // 
            this.変更ToolStripMenuItem.Name = "変更ToolStripMenuItem";
            this.変更ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.変更ToolStripMenuItem.Text = "変更";
            this.変更ToolStripMenuItem.Click += new System.EventHandler(this.変更ToolStripMenuItem_Click);
            // 
            // 削除ToolStripMenuItem
            // 
            this.削除ToolStripMenuItem.Name = "削除ToolStripMenuItem";
            this.削除ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.削除ToolStripMenuItem.Text = "削除";
            this.削除ToolStripMenuItem.Click += new System.EventHandler(this.削除ToolStripMenuItem_Click);
            // 
            // stoolStripStatusLabel2
            // 
            this.stoolStripStatusLabel2.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.stoolStripStatusLabel2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel2});
            this.stoolStripStatusLabel2.Location = new System.Drawing.Point(0, 502);
            this.stoolStripStatusLabel2.Name = "stoolStripStatusLabel2";
            this.stoolStripStatusLabel2.Padding = new System.Windows.Forms.Padding(1, 0, 11, 0);
            this.stoolStripStatusLabel2.Size = new System.Drawing.Size(807, 22);
            this.stoolStripStatusLabel2.TabIndex = 3;
            this.stoolStripStatusLabel2.Text = "statusStrip1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 17);
            // 
            // bt_search
            // 
            this.bt_search.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bt_search.Location = new System.Drawing.Point(354, 29);
            this.bt_search.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bt_search.Name = "bt_search";
            this.bt_search.Size = new System.Drawing.Size(86, 34);
            this.bt_search.TabIndex = 4;
            this.bt_search.Text = "検索";
            this.bt_search.UseVisualStyleBackColor = true;
            this.bt_search.Click += new System.EventHandler(this.bt_search_Click);
            // 
            // txt_search
            // 
            this.txt_search.Font = new System.Drawing.Font("メイリオ", 11.5F, System.Drawing.FontStyle.Bold);
            this.txt_search.Location = new System.Drawing.Point(43, 31);
            this.txt_search.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_search.Name = "txt_search";
            this.txt_search.Size = new System.Drawing.Size(265, 30);
            this.txt_search.TabIndex = 5;
            // 
            // gv_classroomsInfo
            // 
            this.gv_classroomsInfo.AllowUserToAddRows = false;
            this.gv_classroomsInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gv_classroomsInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gv_classroomsInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gv_classroomsInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv_classroomsInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.教室コード,
            this.出勤機コード,
            this.出勤機場所,
            this.備考});
            this.gv_classroomsInfo.Location = new System.Drawing.Point(43, 96);
            this.gv_classroomsInfo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 6);
            this.gv_classroomsInfo.MultiSelect = false;
            this.gv_classroomsInfo.Name = "gv_classroomsInfo";
            this.gv_classroomsInfo.RowTemplate.Height = 21;
            this.gv_classroomsInfo.Size = new System.Drawing.Size(715, 320);
            this.gv_classroomsInfo.TabIndex = 6;
            this.gv_classroomsInfo.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.gv_classroomsInfo_CellBeginEdit);
            this.gv_classroomsInfo.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gv_classroomsInfo_CellEndEdit);
            this.gv_classroomsInfo.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gv_classroomsInfo_CellMouseDown);
            this.gv_classroomsInfo.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.gv_classroomsInfo_CellValueChanged);
            this.gv_classroomsInfo.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.gv_classroomsInfo_EditingControlShowing);
            this.gv_classroomsInfo.SizeChanged += new System.EventHandler(this.gv_classroomsInfo_SizeChanged);
            // 
            // 教室コード
            // 
            this.教室コード.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.教室コード.DefaultCellStyle = dataGridViewCellStyle2;
            this.教室コード.HeaderText = "教室コード";
            this.教室コード.Name = "教室コード";
            this.教室コード.Width = 81;
            // 
            // 出勤機コード
            // 
            this.出勤機コード.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            this.出勤機コード.DefaultCellStyle = dataGridViewCellStyle3;
            this.出勤機コード.HeaderText = "出勤機コード";
            this.出勤機コード.Name = "出勤機コード";
            this.出勤機コード.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.出勤機コード.Width = 93;
            // 
            // 出勤機場所
            // 
            this.出勤機場所.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Blue;
            this.出勤機場所.DefaultCellStyle = dataGridViewCellStyle4;
            this.出勤機場所.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.出勤機場所.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.出勤機場所.HeaderText = "出勤機場所";
            this.出勤機場所.Name = "出勤機場所";
            this.出勤機場所.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.出勤機場所.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.出勤機場所.Width = 120;
            // 
            // 備考
            // 
            this.備考.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Blue;
            this.備考.DefaultCellStyle = dataGridViewCellStyle5;
            this.備考.HeaderText = "備考";
            this.備考.MaxInputLength = 100;
            this.備考.Name = "備考";
            this.備考.Width = 120;
            // 
            // 教室一覧
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 524);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.gv_classroomsInfo);
            this.Controls.Add(this.txt_search);
            this.Controls.Add(this.bt_search);
            this.Controls.Add(this.stoolStripStatusLabel2);
            this.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "教室一覧";
            this.Text = "教室一覧";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.教室一覧_FormClosed);
            this.Load += new System.EventHandler(this.教室一覧_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.stoolStripStatusLabel2.ResumeLayout(false);
            this.stoolStripStatusLabel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gv_classroomsInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 変更ToolStripMenuItem;
        //private System.Windows.Forms.ToolStripMenuItem 新規追加ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 削除ToolStripMenuItem;
        private System.Windows.Forms.StatusStrip stoolStripStatusLabel2;
        private System.Windows.Forms.Button bt_search;
        private System.Windows.Forms.TextBox txt_search;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.DataGridView gv_classroomsInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn 教室コード;
        private System.Windows.Forms.DataGridViewTextBoxColumn 出勤機コード;
        private System.Windows.Forms.DataGridViewComboBoxColumn 出勤機場所;
        private System.Windows.Forms.DataGridViewTextBoxColumn 備考;
    }
}