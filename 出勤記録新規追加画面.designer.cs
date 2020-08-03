namespace HL_塾管理
{
    partial class 出勤記録新規追加画面
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(出勤記録新規追加画面));
            this.btn_クリア = new System.Windows.Forms.Button();
            this.btn_登録 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_出退勤時間 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.cmb_学生コード = new System.Windows.Forms.ComboBox();
            this.lbl_社員コード = new System.Windows.Forms.Label();
            this.rdb_出勤 = new System.Windows.Forms.RadioButton();
            this.rdb_退勤 = new System.Windows.Forms.RadioButton();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_クリア
            // 
            this.btn_クリア.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold);
            this.btn_クリア.Location = new System.Drawing.Point(431, 228);
            this.btn_クリア.Margin = new System.Windows.Forms.Padding(4);
            this.btn_クリア.Name = "btn_クリア";
            this.btn_クリア.Size = new System.Drawing.Size(129, 40);
            this.btn_クリア.TabIndex = 22;
            this.btn_クリア.Text = "クリア";
            this.btn_クリア.UseVisualStyleBackColor = true;
            this.btn_クリア.Click += new System.EventHandler(this.button2_Click);
            // 
            // btn_登録
            // 
            this.btn_登録.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold);
            this.btn_登録.Location = new System.Drawing.Point(272, 228);
            this.btn_登録.Margin = new System.Windows.Forms.Padding(4);
            this.btn_登録.Name = "btn_登録";
            this.btn_登録.Size = new System.Drawing.Size(129, 40);
            this.btn_登録.TabIndex = 21;
            this.btn_登録.Text = "登録";
            this.btn_登録.UseVisualStyleBackColor = true;
            this.btn_登録.Click += new System.EventHandler(this.button1_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(75, 108);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(126, 28);
            this.label7.TabIndex = 16;
            this.label7.Text = "出退勤フラグ";
            // 
            // txt_出退勤時間
            // 
            this.txt_出退勤時間.Font = new System.Drawing.Font("MS UI Gothic", 14F);
            this.txt_出退勤時間.Location = new System.Drawing.Point(272, 162);
            this.txt_出退勤時間.Margin = new System.Windows.Forms.Padding(4);
            this.txt_出退勤時間.MaxLength = 20;
            this.txt_出退勤時間.Name = "txt_出退勤時間";
            this.txt_出退勤時間.Size = new System.Drawing.Size(287, 31);
            this.txt_出退勤時間.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(75, 166);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(107, 28);
            this.label8.TabIndex = 18;
            this.label8.Text = "出退勤時間";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 308);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(663, 22);
            this.statusStrip1.TabIndex = 20;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // cmb_学生コード
            // 
            this.cmb_学生コード.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_学生コード.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmb_学生コード.FormattingEnabled = true;
            this.cmb_学生コード.Items.AddRange(new object[] {
            "ALL"});
            this.cmb_学生コード.Location = new System.Drawing.Point(272, 42);
            this.cmb_学生コード.Margin = new System.Windows.Forms.Padding(4);
            this.cmb_学生コード.Name = "cmb_学生コード";
            this.cmb_学生コード.Size = new System.Drawing.Size(287, 34);
            this.cmb_学生コード.TabIndex = 136;
            this.cmb_学生コード.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // lbl_社員コード
            // 
            this.lbl_社員コード.AutoSize = true;
            this.lbl_社員コード.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_社員コード.Location = new System.Drawing.Point(75, 48);
            this.lbl_社員コード.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_社員コード.Name = "lbl_社員コード";
            this.lbl_社員コード.Size = new System.Drawing.Size(107, 28);
            this.lbl_社員コード.TabIndex = 135;
            this.lbl_社員コード.Text = "学生コード";
            // 
            // rdb_出勤
            // 
            this.rdb_出勤.AutoSize = true;
            this.rdb_出勤.Location = new System.Drawing.Point(272, 110);
            this.rdb_出勤.Margin = new System.Windows.Forms.Padding(4);
            this.rdb_出勤.Name = "rdb_出勤";
            this.rdb_出勤.Size = new System.Drawing.Size(58, 19);
            this.rdb_出勤.TabIndex = 137;
            this.rdb_出勤.TabStop = true;
            this.rdb_出勤.Text = "出勤";
            this.rdb_出勤.UseVisualStyleBackColor = true;
            // 
            // rdb_退勤
            // 
            this.rdb_退勤.AutoSize = true;
            this.rdb_退勤.Location = new System.Drawing.Point(443, 110);
            this.rdb_退勤.Margin = new System.Windows.Forms.Padding(4);
            this.rdb_退勤.Name = "rdb_退勤";
            this.rdb_退勤.Size = new System.Drawing.Size(58, 19);
            this.rdb_退勤.TabIndex = 138;
            this.rdb_退勤.TabStop = true;
            this.rdb_退勤.Text = "退勤";
            this.rdb_退勤.UseVisualStyleBackColor = true;
            // 
            // 出勤記録新規追加画面
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 330);
            this.Controls.Add(this.rdb_退勤);
            this.Controls.Add(this.rdb_出勤);
            this.Controls.Add(this.cmb_学生コード);
            this.Controls.Add(this.lbl_社員コード);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.txt_出退勤時間);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btn_クリア);
            this.Controls.Add(this.btn_登録);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "出勤記録新規追加画面";
            this.ShowIcon = false;
            this.Text = "出勤記録新規追加画面";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.出勤記録新規追加画面_FormClosed);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_クリア;
        private System.Windows.Forms.Button btn_登録;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_出退勤時間;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        public System.Windows.Forms.ComboBox cmb_学生コード;
        private System.Windows.Forms.Label lbl_社員コード;
        private System.Windows.Forms.RadioButton rdb_出勤;
        private System.Windows.Forms.RadioButton rdb_退勤;
    }
}