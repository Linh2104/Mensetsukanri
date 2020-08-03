using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace HL_塾管理
{
    partial class 教師情報一覧
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(教師情報一覧));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.変更ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.削除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.クラス履歴ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.txt_search = new System.Windows.Forms.TextBox();
            this.btn_search = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.gv_teachersInfo = new RowMergeView();
            this.教師コード = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.名前 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.所属会社 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.メイン言語 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.国籍 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.携帯 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.メール = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.入職日 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.退職日 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.クラス = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.クラスコード = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gv_teachersInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.変更ToolStripMenuItem,
            this.削除ToolStripMenuItem,
            this.クラス履歴ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(197, 94);
            this.contextMenuStrip1.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.contextMenuStrip1_Closed);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // 変更ToolStripMenuItem
            // 
            this.変更ToolStripMenuItem.Name = "変更ToolStripMenuItem";
            this.変更ToolStripMenuItem.Size = new System.Drawing.Size(196, 30);
            this.変更ToolStripMenuItem.Text = "変更";
            this.変更ToolStripMenuItem.Click += new System.EventHandler(this.変更ToolStripMenuItem_Click);
            // 
            // 削除ToolStripMenuItem
            // 
            this.削除ToolStripMenuItem.Name = "削除ToolStripMenuItem";
            this.削除ToolStripMenuItem.Size = new System.Drawing.Size(196, 30);
            this.削除ToolStripMenuItem.Text = "削除";
            this.削除ToolStripMenuItem.Click += new System.EventHandler(this.削除ToolStripMenuItem_Click);
            // 
            // クラス履歴ToolStripMenuItem
            // 
            this.クラス履歴ToolStripMenuItem.Name = "クラス履歴ToolStripMenuItem";
            this.クラス履歴ToolStripMenuItem.Size = new System.Drawing.Size(196, 30);
            this.クラス履歴ToolStripMenuItem.Text = "歴史クラス一覧";
            this.クラス履歴ToolStripMenuItem.Click += new System.EventHandler(this.クラス履歴ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLbl});
            this.statusStrip1.Location = new System.Drawing.Point(0, 794);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 24, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1969, 22);
            this.statusStrip1.TabIndex = 122;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLbl
            // 
            this.statusLbl.Name = "statusLbl";
            this.statusLbl.Size = new System.Drawing.Size(0, 17);
            // 
            // txt_search
            // 
            this.txt_search.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_search.Location = new System.Drawing.Point(34, 30);
            this.txt_search.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.txt_search.MaxLength = 120;
            this.txt_search.Name = "txt_search";
            this.txt_search.Size = new System.Drawing.Size(330, 41);
            this.txt_search.TabIndex = 2;
            // 
            // btn_search
            // 
            this.btn_search.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btn_search.Location = new System.Drawing.Point(415, 30);
            this.btn_search.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(144, 50);
            this.btn_search.TabIndex = 123;
            this.btn_search.Text = "検索";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(820, 42);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 18);
            this.label1.TabIndex = 124;
            // 
            // gv_teachersInfo
            // 
            this.gv_teachersInfo.AllowUserToAddRows = false;
            this.gv_teachersInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gv_teachersInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gv_teachersInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv_teachersInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.教師コード,
            this.名前,
            this.所属会社,
            this.メイン言語,
            this.国籍,
            this.携帯,
            this.メール,
            this.入職日,
            this.退職日,
            this.クラス,
            this.クラスコード});
            this.gv_teachersInfo.ContextMenuStrip = this.contextMenuStrip1;
            this.gv_teachersInfo.Key = "";
            this.gv_teachersInfo.Location = new System.Drawing.Point(34, 88);
            this.gv_teachersInfo.Margin = new System.Windows.Forms.Padding(5);
            this.gv_teachersInfo.MaximumSize = new System.Drawing.Size(1671, 1093);
            this.gv_teachersInfo.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.gv_teachersInfo.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("gv_teachersInfo.MergeColumnNames")));
            this.gv_teachersInfo.MergeRowIndex = ((System.Collections.Generic.Dictionary<int, int>)(resources.GetObject("gv_teachersInfo.MergeRowIndex")));
            this.gv_teachersInfo.Name = "gv_teachersInfo";
            this.gv_teachersInfo.NoLink = ((System.Collections.Generic.List<object>)(resources.GetObject("gv_teachersInfo.NoLink")));
            this.gv_teachersInfo.RowHeadersWidth = 25;
            this.gv_teachersInfo.RowTemplate.Height = 23;
            this.gv_teachersInfo.Size = new System.Drawing.Size(1671, 613);
            this.gv_teachersInfo.TabIndex = 5;
            this.gv_teachersInfo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gv_teachersInfo_CellClick);
            this.gv_teachersInfo.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gv_teachersInfo_CellMouseDown);
            this.gv_teachersInfo.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.gv_teachersInfo_CellValueChanged);
            this.gv_teachersInfo.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.gv_teachersInfo_DataError);
            // 
            // 教師コード
            // 
            this.教師コード.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.教師コード.DefaultCellStyle = dataGridViewCellStyle1;
            this.教師コード.HeaderText = "教師コード";
            this.教師コード.MaxInputLength = 20;
            this.教師コード.MinimumWidth = 10;
            this.教師コード.Name = "教師コード";
            this.教師コード.Width = 120;
            // 
            // 名前
            // 
            this.名前.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("メイリオ", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Blue;
            this.名前.DefaultCellStyle = dataGridViewCellStyle2;
            this.名前.HeaderText = "名前";
            this.名前.MaxInputLength = 100;
            this.名前.MinimumWidth = 10;
            this.名前.Name = "名前";
            this.名前.Width = 75;
            // 
            // 所属会社
            // 
            this.所属会社.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("メイリオ", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Blue;
            this.所属会社.DefaultCellStyle = dataGridViewCellStyle3;
            this.所属会社.HeaderText = "所属会社";
            this.所属会社.MaxInputLength = 100;
            this.所属会社.MinimumWidth = 10;
            this.所属会社.Name = "所属会社";
            this.所属会社.Width = 91;
            // 
            // メイン言語
            // 
            this.メイン言語.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("メイリオ", 9F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Blue;
            this.メイン言語.DefaultCellStyle = dataGridViewCellStyle4;
            this.メイン言語.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.メイン言語.HeaderText = "メイン言語";
            this.メイン言語.Items.AddRange(new object[] {
            ".Net",
            "iOS",
            "php",
            "pyhton",
            "Java",
            "Android",
            "Salesforce"});
            this.メイン言語.MinimumWidth = 10;
            this.メイン言語.Name = "メイン言語";
            this.メイン言語.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.メイン言語.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.メイン言語.Width = 93;
            // 
            // 国籍
            // 
            this.国籍.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Blue;
            this.国籍.DefaultCellStyle = dataGridViewCellStyle5;
            this.国籍.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.国籍.HeaderText = "国籍";
            this.国籍.Items.AddRange(new object[] {
            "アメリカ籍",
            "ベトナム籍",
            "日本籍",
            "中国籍",
            "台湾籍",
            "インド籍",
            "韓国籍",
            "ネパール籍"});
            this.国籍.Name = "国籍";
            this.国籍.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.国籍.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.国籍.Width = 75;
            // 
            // 携帯
            // 
            this.携帯.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Blue;
            this.携帯.DefaultCellStyle = dataGridViewCellStyle6;
            this.携帯.HeaderText = "携帯";
            this.携帯.MaxInputLength = 13;
            this.携帯.Name = "携帯";
            this.携帯.Width = 75;
            // 
            // メール
            // 
            this.メール.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Blue;
            this.メール.DefaultCellStyle = dataGridViewCellStyle7;
            this.メール.HeaderText = "メール";
            this.メール.MaxInputLength = 200;
            this.メール.Name = "メール";
            this.メール.Width = 80;
            // 
            // 入職日
            // 
            this.入職日.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle8.Format = "d";
            dataGridViewCellStyle8.NullValue = null;
            this.入職日.DefaultCellStyle = dataGridViewCellStyle8;
            this.入職日.HeaderText = "入塾日";
            this.入職日.MaxInputLength = 20;
            this.入職日.MinimumWidth = 50;
            this.入職日.Name = "入職日";
            this.入職日.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.入職日.Width = 91;
            // 
            // 退職日
            // 
            this.退職日.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle9.Format = "d";
            dataGridViewCellStyle9.NullValue = null;
            this.退職日.DefaultCellStyle = dataGridViewCellStyle9;
            this.退職日.HeaderText = "退塾日";
            this.退職日.MaxInputLength = 20;
            this.退職日.MinimumWidth = 50;
            this.退職日.Name = "退職日";
            // 
            // クラス
            // 
            this.クラス.HeaderText = "クラス";
            this.クラス.Name = "クラス";
            this.クラス.ReadOnly = true;
            // 
            // クラスコード
            // 
            this.クラスコード.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.クラスコード.HeaderText = "クラスコード";
            this.クラスコード.Name = "クラスコード";
            this.クラスコード.Visible = false;
            // 
            // 教師情報一覧
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1969, 816);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_search);
            this.Controls.Add(this.gv_teachersInfo);
            this.Controls.Add(this.txt_search);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(77, 79);
            this.Name = "教師情報一覧";
            this.Text = "教師情報一覧";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.教師情報一覧_FormClosed);
            this.Load += new System.EventHandler(this.教師情報一覧_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gv_teachersInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private StatusStrip statusStrip1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem 削除ToolStripMenuItem;
        private ToolStripStatusLabel statusLbl;
        private ToolStripMenuItem 変更ToolStripMenuItem;
        private TextBox txt_search;
        public RowMergeView gv_teachersInfo;
        private Button btn_search;
        private ToolStripMenuItem クラス履歴ToolStripMenuItem;
        private Label label1;
        private DataGridViewTextBoxColumn 教師コード;
        private DataGridViewTextBoxColumn 名前;
        private DataGridViewTextBoxColumn 所属会社;
        private DataGridViewComboBoxColumn メイン言語;
        private DataGridViewComboBoxColumn 国籍;
        private DataGridViewTextBoxColumn 携帯;
        private DataGridViewTextBoxColumn メール;
        private DataGridViewTextBoxColumn 入職日;
        private DataGridViewTextBoxColumn 退職日;
        private DataGridViewTextBoxColumn クラス;
        private DataGridViewTextBoxColumn クラスコード;
    }
}