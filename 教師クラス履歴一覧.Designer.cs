namespace HL_塾管理
{
    partial class 教師クラス履歴一覧
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txt_searchKey = new System.Windows.Forms.TextBox();
            this.btn_search = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lbl_件数 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.DataGridView1 = new System.Windows.Forms.DataGridView();
            this.クラスコード = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.教室 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.課程 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.開始日 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.終了日 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.有効 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbl_教師名 = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_searchKey
            // 
            this.txt_searchKey.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_searchKey.Location = new System.Drawing.Point(16, 29);
            this.txt_searchKey.Margin = new System.Windows.Forms.Padding(4);
            this.txt_searchKey.Multiline = true;
            this.txt_searchKey.Name = "txt_searchKey";
            this.txt_searchKey.Size = new System.Drawing.Size(309, 38);
            this.txt_searchKey.TabIndex = 1;
            // 
            // btn_search
            // 
            this.btn_search.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btn_search.Location = new System.Drawing.Point(357, 27);
            this.btn_search.Margin = new System.Windows.Forms.Padding(4);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(115, 42);
            this.btn_search.TabIndex = 2;
            this.btn_search.Text = "検索";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbl_件数,
            this.StatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 540);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1065, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lbl_件数
            // 
            this.lbl_件数.Name = "lbl_件数";
            this.lbl_件数.Size = new System.Drawing.Size(0, 17);
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // DataGridView1
            // 
            this.DataGridView1.AllowUserToAddRows = false;
            this.DataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.クラスコード,
            this.教室,
            this.課程,
            this.開始日,
            this.終了日,
            this.有効});
            this.DataGridView1.Location = new System.Drawing.Point(16, 91);
            this.DataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.DataGridView1.Name = "DataGridView1";
            this.DataGridView1.ReadOnly = true;
            this.DataGridView1.RowTemplate.Height = 21;
            this.DataGridView1.Size = new System.Drawing.Size(1033, 419);
            this.DataGridView1.TabIndex = 4;
            this.DataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DataGridView1_DataError);
            // 
            // クラスコード
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.クラスコード.DefaultCellStyle = dataGridViewCellStyle2;
            this.クラスコード.HeaderText = "クラスコード";
            this.クラスコード.Name = "クラスコード";
            this.クラスコード.ReadOnly = true;
            // 
            // 教室
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.教室.DefaultCellStyle = dataGridViewCellStyle3;
            this.教室.HeaderText = "教室";
            this.教室.Name = "教室";
            this.教室.ReadOnly = true;
            // 
            // 課程
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.課程.DefaultCellStyle = dataGridViewCellStyle4;
            this.課程.HeaderText = "課程";
            this.課程.Name = "課程";
            this.課程.ReadOnly = true;
            // 
            // 開始日
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "d";
            dataGridViewCellStyle5.NullValue = null;
            this.開始日.DefaultCellStyle = dataGridViewCellStyle5;
            this.開始日.HeaderText = "開始日";
            this.開始日.Name = "開始日";
            this.開始日.ReadOnly = true;
            // 
            // 終了日
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "d";
            dataGridViewCellStyle6.NullValue = null;
            this.終了日.DefaultCellStyle = dataGridViewCellStyle6;
            this.終了日.HeaderText = "終了日";
            this.終了日.Name = "終了日";
            this.終了日.ReadOnly = true;
            // 
            // 有効
            // 
            this.有効.HeaderText = "有効";
            this.有効.Name = "有効";
            this.有効.ReadOnly = true;
            this.有効.Visible = false;
            // 
            // lbl_教師名
            // 
            this.lbl_教師名.AutoSize = true;
            this.lbl_教師名.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_教師名.Location = new System.Drawing.Point(679, 34);
            this.lbl_教師名.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_教師名.Name = "lbl_教師名";
            this.lbl_教師名.Size = new System.Drawing.Size(0, 28);
            this.lbl_教師名.TabIndex = 6;
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Font = new System.Drawing.Font("メイリオ", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl2.Location = new System.Drawing.Point(566, 34);
            this.lbl2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(88, 28);
            this.lbl2.TabIndex = 7;
            this.lbl2.Text = "教師名：";
            // 
            // 教師クラス履歴一覧
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1065, 562);
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.lbl_教師名);
            this.Controls.Add(this.DataGridView1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btn_search);
            this.Controls.Add(this.txt_searchKey);
            this.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "教師クラス履歴一覧";
            this.Text = "歴史クラス一覧";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.教師クラス履歴一覧_FormClosed);
            this.Load += new System.EventHandler(this.教師クラス履歴一覧_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txt_searchKey;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.DataGridView DataGridView1;
        private System.Windows.Forms.ToolStripStatusLabel lbl_件数;
        private System.Windows.Forms.Label lbl_教師名;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.DataGridViewTextBoxColumn クラスコード;
        private System.Windows.Forms.DataGridViewTextBoxColumn 教室;
        private System.Windows.Forms.DataGridViewTextBoxColumn 課程;
        private System.Windows.Forms.DataGridViewTextBoxColumn 開始日;
        private System.Windows.Forms.DataGridViewTextBoxColumn 終了日;
        private System.Windows.Forms.DataGridViewTextBoxColumn 有効;
    }
}