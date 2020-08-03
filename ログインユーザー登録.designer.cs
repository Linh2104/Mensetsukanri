namespace HL_塾管理
{
    partial class ログインユーザー登録
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
            this.btn_登録 = new System.Windows.Forms.Button();
            this.lbl_teacher = new System.Windows.Forms.Label();
            this.lbl_username = new System.Windows.Forms.Label();
            this.lbl_password = new System.Windows.Forms.Label();
            this.tb_user = new System.Windows.Forms.TextBox();
            this.tb_password = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tb_passwordcheck = new System.Windows.Forms.TextBox();
            this.lbl_passwordcheck = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.cmb_職務 = new System.Windows.Forms.ComboBox();
            this.lbl_職務 = new System.Windows.Forms.Label();
            this.cmb_teacher = new System.Windows.Forms.ComboBox();
            this.lbl_name = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_登録
            // 
            this.btn_登録.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_登録.Location = new System.Drawing.Point(229, 415);
            this.btn_登録.Margin = new System.Windows.Forms.Padding(4);
            this.btn_登録.Name = "btn_登録";
            this.btn_登録.Size = new System.Drawing.Size(188, 56);
            this.btn_登録.TabIndex = 0;
            this.btn_登録.Text = "登録";
            this.btn_登録.UseVisualStyleBackColor = true;
            this.btn_登録.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // lbl_teacher
            // 
            this.lbl_teacher.AutoSize = true;
            this.lbl_teacher.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_teacher.Location = new System.Drawing.Point(45, 53);
            this.lbl_teacher.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_teacher.Name = "lbl_teacher";
            this.lbl_teacher.Size = new System.Drawing.Size(182, 28);
            this.lbl_teacher.TabIndex = 1;
            this.lbl_teacher.Text = "教師コード　[必須]";
            this.lbl_teacher.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl_必須_Paint);
            // 
            // lbl_username
            // 
            this.lbl_username.AutoSize = true;
            this.lbl_username.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_username.Location = new System.Drawing.Point(45, 120);
            this.lbl_username.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_username.Name = "lbl_username";
            this.lbl_username.Size = new System.Drawing.Size(163, 28);
            this.lbl_username.TabIndex = 2;
            this.lbl_username.Text = "ユーザ名　[必須]";
            this.lbl_username.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl_必須_Paint);
            // 
            // lbl_password
            // 
            this.lbl_password.AutoSize = true;
            this.lbl_password.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_password.Location = new System.Drawing.Point(45, 248);
            this.lbl_password.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_password.Name = "lbl_password";
            this.lbl_password.Size = new System.Drawing.Size(182, 28);
            this.lbl_password.TabIndex = 3;
            this.lbl_password.Text = "パスワード　[必須]";
            this.lbl_password.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl_必須_Paint);
            // 
            // tb_user
            // 
            this.tb_user.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_user.Location = new System.Drawing.Point(358, 116);
            this.tb_user.Margin = new System.Windows.Forms.Padding(4);
            this.tb_user.Name = "tb_user";
            this.tb_user.Size = new System.Drawing.Size(280, 36);
            this.tb_user.TabIndex = 6;
            // 
            // tb_password
            // 
            this.tb_password.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_password.Location = new System.Drawing.Point(358, 245);
            this.tb_password.Margin = new System.Windows.Forms.Padding(4);
            this.tb_password.Name = "tb_password";
            this.tb_password.Size = new System.Drawing.Size(280, 36);
            this.tb_password.TabIndex = 7;
            this.tb_password.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_password_KeyPress);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 502);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(738, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // tb_passwordcheck
            // 
            this.tb_passwordcheck.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_passwordcheck.Location = new System.Drawing.Point(358, 316);
            this.tb_passwordcheck.Margin = new System.Windows.Forms.Padding(4);
            this.tb_passwordcheck.Name = "tb_passwordcheck";
            this.tb_passwordcheck.Size = new System.Drawing.Size(280, 36);
            this.tb_passwordcheck.TabIndex = 9;
            this.tb_passwordcheck.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_passwordcheck_KeyPress);
            // 
            // lbl_passwordcheck
            // 
            this.lbl_passwordcheck.AutoSize = true;
            this.lbl_passwordcheck.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_passwordcheck.Location = new System.Drawing.Point(45, 319);
            this.lbl_passwordcheck.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_passwordcheck.Name = "lbl_passwordcheck";
            this.lbl_passwordcheck.Size = new System.Drawing.Size(220, 28);
            this.lbl_passwordcheck.TabIndex = 10;
            this.lbl_passwordcheck.Text = "パスワード確認　[必須]";
            this.lbl_passwordcheck.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl_必須_Paint);
            // 
            // cmb_職務
            // 
            this.cmb_職務.AutoCompleteCustomSource.AddRange(new string[] {
            "管理者",
            "一般ユーザ"});
            this.cmb_職務.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmb_職務.FormattingEnabled = true;
            this.cmb_職務.ImeMode = System.Windows.Forms.ImeMode.On;
            this.cmb_職務.Items.AddRange(new object[] {
            "管理者",
            "一般ユーザ"});
            this.cmb_職務.Location = new System.Drawing.Point(358, 182);
            this.cmb_職務.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_職務.Name = "cmb_職務";
            this.cmb_職務.Size = new System.Drawing.Size(157, 33);
            this.cmb_職務.TabIndex = 151;
            // 
            // lbl_職務
            // 
            this.lbl_職務.AutoSize = true;
            this.lbl_職務.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_職務.Location = new System.Drawing.Point(45, 185);
            this.lbl_職務.Name = "lbl_職務";
            this.lbl_職務.Size = new System.Drawing.Size(130, 25);
            this.lbl_職務.TabIndex = 150;
            this.lbl_職務.Text = "職　務　[必須]";
            this.lbl_職務.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl_必須_Paint);
            // 
            // cmb_teacher
            // 
            this.cmb_teacher.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_teacher.FormattingEnabled = true;
            this.cmb_teacher.Location = new System.Drawing.Point(358, 49);
            this.cmb_teacher.Margin = new System.Windows.Forms.Padding(4);
            this.cmb_teacher.Name = "cmb_teacher";
            this.cmb_teacher.Size = new System.Drawing.Size(160, 36);
            this.cmb_teacher.TabIndex = 152;
            this.cmb_teacher.SelectedIndexChanged += new System.EventHandler(this.cmb_teacher_SelectedIndexChanged);
            // 
            // lbl_name
            // 
            this.lbl_name.AutoSize = true;
            this.lbl_name.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_name.Location = new System.Drawing.Point(528, 53);
            this.lbl_name.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_name.Name = "lbl_name";
            this.lbl_name.Size = new System.Drawing.Size(0, 28);
            this.lbl_name.TabIndex = 153;
            // 
            // ログインユーザー登録
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 524);
            this.Controls.Add(this.lbl_name);
            this.Controls.Add(this.cmb_teacher);
            this.Controls.Add(this.cmb_職務);
            this.Controls.Add(this.lbl_職務);
            this.Controls.Add(this.lbl_passwordcheck);
            this.Controls.Add(this.tb_passwordcheck);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tb_password);
            this.Controls.Add(this.tb_user);
            this.Controls.Add(this.lbl_password);
            this.Controls.Add(this.lbl_username);
            this.Controls.Add(this.lbl_teacher);
            this.Controls.Add(this.btn_登録);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ログインユーザー登録";
            this.Text = "ログインユーザー登録";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ログインユーザー登録_FormClosed_1);
            this.Load += new System.EventHandler(this.ログインユーザー登録_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_登録;
        private System.Windows.Forms.Label lbl_teacher;
        private System.Windows.Forms.Label lbl_username;
        private System.Windows.Forms.Label lbl_password;
        private System.Windows.Forms.TextBox tb_user;
        private System.Windows.Forms.TextBox tb_password;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.TextBox tb_passwordcheck;
        private System.Windows.Forms.Label lbl_passwordcheck;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ComboBox cmb_職務;
        private System.Windows.Forms.Label lbl_職務;
        private System.Windows.Forms.ComboBox cmb_teacher;
        private System.Windows.Forms.Label lbl_name;
    }
}