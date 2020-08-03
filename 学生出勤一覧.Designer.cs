namespace HL_塾管理
{
    partial class 学生出勤一覧
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
        //111
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(学生出勤一覧));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_検索 = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.追加toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.削除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dtp_date = new System.Windows.Forms.DateTimePicker();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.rowMergeView1 = new RowMergeView();
            this.学生コード = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.学生名 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.出勤機コード = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.出勤機場所 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.登録コード = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.クラスコード = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.教師名 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.出退勤フラグ = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.出退勤時間 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NewRowフラグ = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.元日付 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rowMergeView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(341, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 28);
            this.label1.TabIndex = 125;
            this.label1.Text = "日付";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(29, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 28);
            this.label2.TabIndex = 126;
            this.label2.Text = "検索";
            // 
            // txt_検索
            // 
            this.txt_検索.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_検索.Location = new System.Drawing.Point(117, 13);
            this.txt_検索.Margin = new System.Windows.Forms.Padding(4);
            this.txt_検索.Name = "txt_検索";
            this.txt_検索.Size = new System.Drawing.Size(166, 36);
            this.txt_検索.TabIndex = 130;
            this.txt_検索.TextChanged += new System.EventHandler(this.txt_検索_TextChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.追加toolStripMenuItem,
            this.削除ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(109, 52);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // 追加toolStripMenuItem
            // 
            this.追加toolStripMenuItem.Name = "追加toolStripMenuItem";
            this.追加toolStripMenuItem.Size = new System.Drawing.Size(108, 24);
            this.追加toolStripMenuItem.Text = "追加";
            this.追加toolStripMenuItem.Click += new System.EventHandler(this.追加toolStripMenuItem_Click);
            // 
            // 削除ToolStripMenuItem
            // 
            this.削除ToolStripMenuItem.Name = "削除ToolStripMenuItem";
            this.削除ToolStripMenuItem.Size = new System.Drawing.Size(108, 24);
            this.削除ToolStripMenuItem.Text = "削除";
            this.削除ToolStripMenuItem.Click += new System.EventHandler(this.削除ToolStripMenuItem_Click);
            // 
            // dtp_date
            // 
            this.dtp_date.CustomFormat = "yyyy年MM月";
            this.dtp_date.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dtp_date.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_date.Location = new System.Drawing.Point(421, 18);
            this.dtp_date.Margin = new System.Windows.Forms.Padding(4);
            this.dtp_date.Name = "dtp_date";
            this.dtp_date.ShowUpDown = true;
            this.dtp_date.Size = new System.Drawing.Size(147, 26);
            this.dtp_date.TabIndex = 134;
            this.dtp_date.ValueChanged += new System.EventHandler(this.dtp_date_ValueChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 623);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.ShowItemToolTips = true;
            this.statusStrip1.Size = new System.Drawing.Size(994, 24);
            this.statusStrip1.TabIndex = 135;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.Black;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 19);
            // 
            // rowMergeView1
            // 
            this.rowMergeView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.rowMergeView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.rowMergeView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.rowMergeView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.学生コード,
            this.学生名,
            this.出勤機コード,
            this.出勤機場所,
            this.登録コード,
            this.クラスコード,
            this.教師名,
            this.出退勤フラグ,
            this.出退勤時間,
            this.NewRowフラグ,
            this.元日付});
            this.rowMergeView1.Key = "";
            this.rowMergeView1.Location = new System.Drawing.Point(34, 63);
            this.rowMergeView1.Margin = new System.Windows.Forms.Padding(4);
            this.rowMergeView1.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.rowMergeView1.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("rowMergeView1.MergeColumnNames")));
            this.rowMergeView1.MergeRowIndex = ((System.Collections.Generic.Dictionary<int, int>)(resources.GetObject("rowMergeView1.MergeRowIndex")));
            this.rowMergeView1.Name = "rowMergeView1";
            this.rowMergeView1.NoLink = ((System.Collections.Generic.List<object>)(resources.GetObject("rowMergeView1.NoLink")));
            this.rowMergeView1.RowTemplate.Height = 23;
            this.rowMergeView1.Size = new System.Drawing.Size(886, 530);
            this.rowMergeView1.TabIndex = 131;
            this.rowMergeView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.rowMergeView1_CellBeginEdit);
            this.rowMergeView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.rowMergeView1_CellEndEdit);
            this.rowMergeView1.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.rowMergeView1_CellMouseDown);
            this.rowMergeView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.rowMergeView1_CellValueChanged);
            this.rowMergeView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.rowMergeView1_DataError);
            this.rowMergeView1.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.rowMergeView1_SortCompare);
            // 
            // 学生コード
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.学生コード.DefaultCellStyle = dataGridViewCellStyle2;
            this.学生コード.HeaderText = "学生コード";
            this.学生コード.Name = "学生コード";
            this.学生コード.ReadOnly = true;
            // 
            // 学生名
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.学生名.DefaultCellStyle = dataGridViewCellStyle3;
            this.学生名.HeaderText = "学生名";
            this.学生名.Name = "学生名";
            this.学生名.ReadOnly = true;
            // 
            // 出勤機コード
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.出勤機コード.DefaultCellStyle = dataGridViewCellStyle4;
            this.出勤機コード.HeaderText = "出勤機コード";
            this.出勤機コード.Name = "出勤機コード";
            this.出勤機コード.ReadOnly = true;
            this.出勤機コード.Visible = false;
            // 
            // 出勤機場所
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.出勤機場所.DefaultCellStyle = dataGridViewCellStyle5;
            this.出勤機場所.HeaderText = "出勤機場所";
            this.出勤機場所.Name = "出勤機場所";
            // 
            // 登録コード
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.登録コード.DefaultCellStyle = dataGridViewCellStyle6;
            this.登録コード.HeaderText = "登録コード";
            this.登録コード.Name = "登録コード";
            this.登録コード.ReadOnly = true;
            // 
            // クラスコード
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.クラスコード.DefaultCellStyle = dataGridViewCellStyle7;
            this.クラスコード.HeaderText = "クラスコード";
            this.クラスコード.Name = "クラスコード";
            this.クラスコード.ReadOnly = true;
            // 
            // 教師名
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.教師名.DefaultCellStyle = dataGridViewCellStyle8;
            this.教師名.HeaderText = "教師名";
            this.教師名.Name = "教師名";
            this.教師名.ReadOnly = true;
            // 
            // 出退勤フラグ
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            this.出退勤フラグ.DefaultCellStyle = dataGridViewCellStyle9;
            this.出退勤フラグ.HeaderText = "出退勤フラグ";
            this.出退勤フラグ.Items.AddRange(new object[] {
            "出勤",
            "退勤"});
            this.出退勤フラグ.Name = "出退勤フラグ";
            this.出退勤フラグ.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.出退勤フラグ.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // 出退勤時間
            // 
            this.出退勤時間.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Blue;
            this.出退勤時間.DefaultCellStyle = dataGridViewCellStyle10;
            this.出退勤時間.HeaderText = "出退勤時間";
            this.出退勤時間.Name = "出退勤時間";
            // 
            // NewRowフラグ
            // 
            this.NewRowフラグ.HeaderText = "NewRowフラグ";
            this.NewRowフラグ.Name = "NewRowフラグ";
            this.NewRowフラグ.Visible = false;
            // 
            // 元日付
            // 
            this.元日付.HeaderText = "元日付";
            this.元日付.Name = "元日付";
            this.元日付.Visible = false;
            // 
            // 学生出勤一覧
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 647);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dtp_date);
            this.Controls.Add(this.rowMergeView1);
            this.Controls.Add(this.txt_検索);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.Name = "学生出勤一覧";
            this.Text = "学生出勤一覧";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.学生出勤一覧_FormClosed);
            this.Load += new System.EventHandler(this.学生出勤一覧_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rowMergeView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_検索;
        public RowMergeView rowMergeView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 追加toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 削除ToolStripMenuItem;
        private System.Windows.Forms.DateTimePicker dtp_date;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 学生コード;
        private System.Windows.Forms.DataGridViewTextBoxColumn 学生名;
        private System.Windows.Forms.DataGridViewTextBoxColumn 出勤機コード;
        private System.Windows.Forms.DataGridViewTextBoxColumn 出勤機場所;
        private System.Windows.Forms.DataGridViewTextBoxColumn 登録コード;
        private System.Windows.Forms.DataGridViewTextBoxColumn クラスコード;
        private System.Windows.Forms.DataGridViewTextBoxColumn 教師名;
        private System.Windows.Forms.DataGridViewComboBoxColumn 出退勤フラグ;
        private System.Windows.Forms.DataGridViewTextBoxColumn 出退勤時間;
        private System.Windows.Forms.DataGridViewCheckBoxColumn NewRowフラグ;
        private System.Windows.Forms.DataGridViewTextBoxColumn 元日付;
    }
}