namespace HL_塾管理
{
    partial class 出勤機学生登録
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(出勤機学生登録));
            this.lbl_出勤機コード = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.cmb_出勤機コード = new System.Windows.Forms.ComboBox();
            this.lbl_出退勤Info = new System.Windows.Forms.Label();
            this.lbl_登録コード = new System.Windows.Forms.Label();
            this.txt_登録コード = new System.Windows.Forms.TextBox();
            this.lbl_学生コード = new System.Windows.Forms.Label();
            this.btn_insert = new System.Windows.Forms.Button();
            this.lbl_Msg = new System.Windows.Forms.Label();
            this.cmb_学生コード = new System.Windows.Forms.ComboBox();
            this.lbl_学生code = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_出勤機コード
            // 
            this.lbl_出勤機コード.AutoSize = true;
            this.lbl_出勤機コード.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_出勤機コード.Location = new System.Drawing.Point(36, 39);
            this.lbl_出勤機コード.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.lbl_出勤機コード.Name = "lbl_出勤機コード";
            this.lbl_出勤機コード.Size = new System.Drawing.Size(114, 23);
            this.lbl_出勤機コード.TabIndex = 83;
            this.lbl_出勤機コード.Text = "出勤機　[必須]";
            this.lbl_出勤機コード.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl_必須_Paint);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 338);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 25, 0);
            this.statusStrip1.Size = new System.Drawing.Size(527, 22);
            this.statusStrip1.TabIndex = 115;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.Red;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // cmb_出勤機コード
            // 
            this.cmb_出勤機コード.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_出勤機コード.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmb_出勤機コード.FormattingEnabled = true;
            this.cmb_出勤機コード.Location = new System.Drawing.Point(230, 39);
            this.cmb_出勤機コード.Margin = new System.Windows.Forms.Padding(5);
            this.cmb_出勤機コード.Name = "cmb_出勤機コード";
            this.cmb_出勤機コード.Size = new System.Drawing.Size(187, 26);
            this.cmb_出勤機コード.TabIndex = 116;
            this.cmb_出勤機コード.SelectedIndexChanged += new System.EventHandler(this.cmb_出勤機コード_SelectedIndexChanged);
            // 
            // lbl_出退勤Info
            // 
            this.lbl_出退勤Info.AutoSize = true;
            this.lbl_出退勤Info.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_出退勤Info.Location = new System.Drawing.Point(429, 42);
            this.lbl_出退勤Info.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.lbl_出退勤Info.Name = "lbl_出退勤Info";
            this.lbl_出退勤Info.Size = new System.Drawing.Size(0, 18);
            this.lbl_出退勤Info.TabIndex = 117;
            this.lbl_出退勤Info.Visible = false;
            // 
            // lbl_登録コード
            // 
            this.lbl_登録コード.AutoSize = true;
            this.lbl_登録コード.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_登録コード.Location = new System.Drawing.Point(36, 102);
            this.lbl_登録コード.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbl_登録コード.Name = "lbl_登録コード";
            this.lbl_登録コード.Size = new System.Drawing.Size(144, 23);
            this.lbl_登録コード.TabIndex = 118;
            this.lbl_登録コード.Text = "登録コード　[必須]";
            this.lbl_登録コード.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl_必須_Paint);
            // 
            // txt_登録コード
            // 
            this.txt_登録コード.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_登録コード.Location = new System.Drawing.Point(230, 99);
            this.txt_登録コード.Margin = new System.Windows.Forms.Padding(5);
            this.txt_登録コード.MaxLength = 10;
            this.txt_登録コード.Name = "txt_登録コード";
            this.txt_登録コード.Size = new System.Drawing.Size(187, 30);
            this.txt_登録コード.TabIndex = 131;
            // 
            // lbl_学生コード
            // 
            this.lbl_学生コード.AutoSize = true;
            this.lbl_学生コード.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_学生コード.Location = new System.Drawing.Point(36, 166);
            this.lbl_学生コード.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbl_学生コード.Name = "lbl_学生コード";
            this.lbl_学生コード.Size = new System.Drawing.Size(99, 23);
            this.lbl_学生コード.TabIndex = 134;
            this.lbl_学生コード.Text = "学生　[必須]";
            this.lbl_学生コード.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl_必須_Paint);
            // 
            // btn_insert
            // 
            this.btn_insert.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btn_insert.Location = new System.Drawing.Point(175, 266);
            this.btn_insert.Margin = new System.Windows.Forms.Padding(5);
            this.btn_insert.Name = "btn_insert";
            this.btn_insert.Size = new System.Drawing.Size(128, 45);
            this.btn_insert.TabIndex = 136;
            this.btn_insert.Text = "登録";
            this.btn_insert.UseVisualStyleBackColor = true;
            this.btn_insert.Click += new System.EventHandler(this.btn_insert_Click);
            // 
            // lbl_Msg
            // 
            this.lbl_Msg.AutoSize = true;
            this.lbl_Msg.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_Msg.ForeColor = System.Drawing.Color.Red;
            this.lbl_Msg.Location = new System.Drawing.Point(75, 11);
            this.lbl_Msg.Margin = new System.Windows.Forms.Padding(5, 0, 5, 5);
            this.lbl_Msg.Name = "lbl_Msg";
            this.lbl_Msg.Size = new System.Drawing.Size(0, 21);
            this.lbl_Msg.TabIndex = 137;
            this.lbl_Msg.Visible = false;
            // 
            // cmb_学生コード
            // 
            this.cmb_学生コード.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmb_学生コード.FormattingEnabled = true;
            this.cmb_学生コード.Location = new System.Drawing.Point(230, 163);
            this.cmb_学生コード.Name = "cmb_学生コード";
            this.cmb_学生コード.Size = new System.Drawing.Size(187, 31);
            this.cmb_学生コード.TabIndex = 138;
            this.cmb_学生コード.SelectedIndexChanged += new System.EventHandler(this.cmb_学生コード_SelectedIndexChanged);
            this.cmb_学生コード.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.cmb_学生コード_Format);
            // 
            // lbl_学生code
            // 
            this.lbl_学生code.AutoEllipsis = true;
            this.lbl_学生code.AutoSize = true;
            this.lbl_学生code.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_学生code.Location = new System.Drawing.Point(429, 169);
            this.lbl_学生code.Name = "lbl_学生code";
            this.lbl_学生code.Size = new System.Drawing.Size(0, 23);
            this.lbl_学生code.TabIndex = 139;
            this.lbl_学生code.Visible = false;
            // 
            // 出勤機学生登録
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 360);
            this.Controls.Add(this.lbl_学生code);
            this.Controls.Add(this.cmb_学生コード);
            this.Controls.Add(this.lbl_Msg);
            this.Controls.Add(this.btn_insert);
            this.Controls.Add(this.lbl_学生コード);
            this.Controls.Add(this.txt_登録コード);
            this.Controls.Add(this.lbl_登録コード);
            this.Controls.Add(this.lbl_出退勤Info);
            this.Controls.Add(this.cmb_出勤機コード);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lbl_出勤機コード);
            this.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "出勤機学生登録";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "出勤機学生登録";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.出勤機学生登録_FormClosed);
            this.Load += new System.EventHandler(this.Page_load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbl_出勤機コード;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ComboBox cmb_出勤機コード;
        private System.Windows.Forms.Label lbl_出退勤Info;
        private System.Windows.Forms.Label lbl_登録コード;
        private System.Windows.Forms.TextBox txt_登録コード;
        private System.Windows.Forms.Label lbl_学生コード;
        private System.Windows.Forms.Button btn_insert;
        private System.Windows.Forms.Label lbl_Msg;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ComboBox cmb_学生コード;
        private System.Windows.Forms.Label lbl_学生code;
    }
}