namespace HL_人事
{
    partial class 社員から教師へ登録
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(社員から教師へ登録));
            this.dtp_終了日 = new System.Windows.Forms.DateTimePicker();
            this.lbl_終了日 = new System.Windows.Forms.Label();
            this.dtp_開始日 = new System.Windows.Forms.DateTimePicker();
            this.lbl_開始日 = new System.Windows.Forms.Label();
            this.lbl_言語 = new System.Windows.Forms.Label();
            this.txt_教師コード = new System.Windows.Forms.TextBox();
            this.lbl_教師コード = new System.Windows.Forms.Label();
            this.btn_登録 = new System.Windows.Forms.Button();
            this.lbl_名前 = new System.Windows.Forms.Label();
            this.txt_名前 = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ssl_errMsg = new System.Windows.Forms.ToolStripStatusLabel();
            this.txt_メイン言語 = new System.Windows.Forms.TextBox();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtp_終了日
            // 
            this.dtp_終了日.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dtp_終了日.Location = new System.Drawing.Point(277, 309);
            this.dtp_終了日.Name = "dtp_終了日";
            this.dtp_終了日.Size = new System.Drawing.Size(218, 33);
            this.dtp_終了日.TabIndex = 168;
            // 
            // lbl_終了日
            // 
            this.lbl_終了日.AutoSize = true;
            this.lbl_終了日.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_終了日.Location = new System.Drawing.Point(92, 312);
            this.lbl_終了日.Name = "lbl_終了日";
            this.lbl_終了日.Size = new System.Drawing.Size(63, 25);
            this.lbl_終了日.TabIndex = 167;
            this.lbl_終了日.Text = "終了日";
            // 
            // dtp_開始日
            // 
            this.dtp_開始日.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dtp_開始日.Location = new System.Drawing.Point(277, 247);
            this.dtp_開始日.Name = "dtp_開始日";
            this.dtp_開始日.Size = new System.Drawing.Size(218, 33);
            this.dtp_開始日.TabIndex = 166;
            // 
            // lbl_開始日
            // 
            this.lbl_開始日.AutoSize = true;
            this.lbl_開始日.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_開始日.Location = new System.Drawing.Point(92, 250);
            this.lbl_開始日.Name = "lbl_開始日";
            this.lbl_開始日.Size = new System.Drawing.Size(63, 25);
            this.lbl_開始日.TabIndex = 165;
            this.lbl_開始日.Text = "開始日";
            // 
            // lbl_言語
            // 
            this.lbl_言語.AutoSize = true;
            this.lbl_言語.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_言語.Location = new System.Drawing.Point(92, 188);
            this.lbl_言語.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbl_言語.Name = "lbl_言語";
            this.lbl_言語.Size = new System.Drawing.Size(107, 28);
            this.lbl_言語.TabIndex = 163;
            this.lbl_言語.Text = "メイン言語";
            // 
            // txt_教師コード
            // 
            this.txt_教師コード.Enabled = false;
            this.txt_教師コード.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_教師コード.Location = new System.Drawing.Point(277, 59);
            this.txt_教師コード.Margin = new System.Windows.Forms.Padding(5);
            this.txt_教師コード.MaxLength = 10;
            this.txt_教師コード.Name = "txt_教師コード";
            this.txt_教師コード.Size = new System.Drawing.Size(187, 36);
            this.txt_教師コード.TabIndex = 161;
            // 
            // lbl_教師コード
            // 
            this.lbl_教師コード.AutoSize = true;
            this.lbl_教師コード.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_教師コード.Location = new System.Drawing.Point(92, 62);
            this.lbl_教師コード.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbl_教師コード.Name = "lbl_教師コード";
            this.lbl_教師コード.Size = new System.Drawing.Size(107, 28);
            this.lbl_教師コード.TabIndex = 160;
            this.lbl_教師コード.Text = "教師コード";
            // 
            // btn_登録
            // 
            this.btn_登録.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btn_登録.Location = new System.Drawing.Point(277, 406);
            this.btn_登録.Margin = new System.Windows.Forms.Padding(5);
            this.btn_登録.Name = "btn_登録";
            this.btn_登録.Size = new System.Drawing.Size(137, 42);
            this.btn_登録.TabIndex = 157;
            this.btn_登録.Text = "登録";
            this.btn_登録.UseVisualStyleBackColor = true;
            this.btn_登録.Click += new System.EventHandler(this.btn_登録_Click);
            // 
            // lbl_名前
            // 
            this.lbl_名前.AutoSize = true;
            this.lbl_名前.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_名前.Location = new System.Drawing.Point(92, 125);
            this.lbl_名前.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbl_名前.Name = "lbl_名前";
            this.lbl_名前.Size = new System.Drawing.Size(50, 28);
            this.lbl_名前.TabIndex = 169;
            this.lbl_名前.Text = "名前";
            // 
            // txt_名前
            // 
            this.txt_名前.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_名前.Location = new System.Drawing.Point(277, 122);
            this.txt_名前.Margin = new System.Windows.Forms.Padding(5);
            this.txt_名前.MaxLength = 10;
            this.txt_名前.Name = "txt_名前";
            this.txt_名前.Size = new System.Drawing.Size(187, 36);
            this.txt_名前.TabIndex = 170;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ssl_errMsg});
            this.statusStrip1.Location = new System.Drawing.Point(0, 496);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 9, 0);
            this.statusStrip1.Size = new System.Drawing.Size(722, 22);
            this.statusStrip1.TabIndex = 174;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // ssl_errMsg
            // 
            this.ssl_errMsg.Name = "ssl_errMsg";
            this.ssl_errMsg.Size = new System.Drawing.Size(0, 17);
            // 
            // txt_メイン言語
            // 
            this.txt_メイン言語.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_メイン言語.Location = new System.Drawing.Point(277, 180);
            this.txt_メイン言語.Margin = new System.Windows.Forms.Padding(5);
            this.txt_メイン言語.MaxLength = 50;
            this.txt_メイン言語.Name = "txt_メイン言語";
            this.txt_メイン言語.Size = new System.Drawing.Size(218, 36);
            this.txt_メイン言語.TabIndex = 175;
            // 
            // 社員から教師へ登録
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 518);
            this.Controls.Add(this.txt_メイン言語);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.txt_名前);
            this.Controls.Add(this.lbl_名前);
            this.Controls.Add(this.dtp_終了日);
            this.Controls.Add(this.lbl_終了日);
            this.Controls.Add(this.dtp_開始日);
            this.Controls.Add(this.lbl_開始日);
            this.Controls.Add(this.lbl_言語);
            this.Controls.Add(this.txt_教師コード);
            this.Controls.Add(this.lbl_教師コード);
            this.Controls.Add(this.btn_登録);
            this.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "社員から教師へ登録";
            this.Text = "社員から教師へ登録";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.社員から教師へ登録_FormClosed);
            this.Load += new System.EventHandler(this.社員から教師へ登録_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DateTimePicker dtp_終了日;
        private System.Windows.Forms.Label lbl_終了日;
        private System.Windows.Forms.DateTimePicker dtp_開始日;
        private System.Windows.Forms.Label lbl_開始日;
        private System.Windows.Forms.Label lbl_言語;
        private System.Windows.Forms.TextBox txt_教師コード;
        private System.Windows.Forms.Label lbl_教師コード;
        private System.Windows.Forms.Button btn_登録;
        private System.Windows.Forms.Label lbl_名前;
        private System.Windows.Forms.TextBox txt_名前;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel ssl_errMsg;
        private System.Windows.Forms.TextBox txt_メイン言語;
    }
}