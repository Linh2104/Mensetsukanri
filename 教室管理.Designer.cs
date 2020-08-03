namespace HL_塾管理
{
    partial class 教室管理
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
            this.bt_regist = new System.Windows.Forms.Button();
            this.txt_remark = new System.Windows.Forms.TextBox();
            this.txt_classroomnum = new System.Windows.Forms.TextBox();
            this.cmb_出勤機コード = new System.Windows.Forms.ComboBox();
            this.lbl_machinenum = new System.Windows.Forms.Label();
            this.lbl_classroomnum = new System.Windows.Forms.Label();
            this.lbl_remark = new System.Windows.Forms.Label();
            this.toolStripStatusLabel1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbl_Msg = new System.Windows.Forms.Label();
            this.toolStripStatusLabel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bt_regist
            // 
            this.bt_regist.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold);
            this.bt_regist.Location = new System.Drawing.Point(204, 296);
            this.bt_regist.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bt_regist.Name = "bt_regist";
            this.bt_regist.Size = new System.Drawing.Size(135, 48);
            this.bt_regist.TabIndex = 0;
            this.bt_regist.Text = "登録";
            this.bt_regist.UseVisualStyleBackColor = true;
            this.bt_regist.Click += new System.EventHandler(this.bt_register_Click);
            // 
            // txt_remark
            // 
            this.txt_remark.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold);
            this.txt_remark.Location = new System.Drawing.Point(309, 188);
            this.txt_remark.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_remark.MaxLength = 100;
            this.txt_remark.Name = "txt_remark";
            this.txt_remark.Size = new System.Drawing.Size(248, 36);
            this.txt_remark.TabIndex = 1;
            // 
            // txt_classroomnum
            // 
            this.txt_classroomnum.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold);
            this.txt_classroomnum.Location = new System.Drawing.Point(309, 118);
            this.txt_classroomnum.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_classroomnum.MaxLength = 20;
            this.txt_classroomnum.Name = "txt_classroomnum";
            this.txt_classroomnum.Size = new System.Drawing.Size(248, 36);
            this.txt_classroomnum.TabIndex = 2;
            // 
            // cmb_出勤機コード
            // 
            this.cmb_出勤機コード.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_出勤機コード.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold);
            this.cmb_出勤機コード.FormattingEnabled = true;
            this.cmb_出勤機コード.Location = new System.Drawing.Point(309, 46);
            this.cmb_出勤機コード.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.cmb_出勤機コード.Name = "cmb_出勤機コード";
            this.cmb_出勤機コード.Size = new System.Drawing.Size(248, 36);
            this.cmb_出勤機コード.TabIndex = 117;
            // 
            // lbl_machinenum
            // 
            this.lbl_machinenum.AutoSize = true;
            this.lbl_machinenum.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold);
            this.lbl_machinenum.Location = new System.Drawing.Point(28, 50);
            this.lbl_machinenum.Name = "lbl_machinenum";
            this.lbl_machinenum.Size = new System.Drawing.Size(144, 28);
            this.lbl_machinenum.TabIndex = 118;
            this.lbl_machinenum.Text = "出勤機　[必須]";
            this.lbl_machinenum.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl_必須_Paint);
            // 
            // lbl_classroomnum
            // 
            this.lbl_classroomnum.AutoSize = true;
            this.lbl_classroomnum.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold);
            this.lbl_classroomnum.Location = new System.Drawing.Point(28, 121);
            this.lbl_classroomnum.Name = "lbl_classroomnum";
            this.lbl_classroomnum.Size = new System.Drawing.Size(182, 28);
            this.lbl_classroomnum.TabIndex = 119;
            this.lbl_classroomnum.Text = "教室コード　[必須]";
            this.lbl_classroomnum.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl_必須_Paint);
            // 
            // lbl_remark
            // 
            this.lbl_remark.AutoSize = true;
            this.lbl_remark.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold);
            this.lbl_remark.Location = new System.Drawing.Point(28, 191);
            this.lbl_remark.Name = "lbl_remark";
            this.lbl_remark.Size = new System.Drawing.Size(144, 28);
            this.lbl_remark.TabIndex = 120;
            this.lbl_remark.Text = "備　考　[必須]";
            this.lbl_remark.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl_必須_Paint);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStripStatusLabel1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel2});
            this.toolStripStatusLabel1.Location = new System.Drawing.Point(0, 496);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Padding = new System.Windows.Forms.Padding(1, 0, 11, 0);
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(809, 22);
            this.toolStripStatusLabel1.TabIndex = 121;
            this.toolStripStatusLabel1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 17);
            // 
            // lbl_Msg
            // 
            this.lbl_Msg.AutoSize = true;
            this.lbl_Msg.Location = new System.Drawing.Point(29, 296);
            this.lbl_Msg.Name = "lbl_Msg";
            this.lbl_Msg.Size = new System.Drawing.Size(0, 15);
            this.lbl_Msg.TabIndex = 123;
            this.lbl_Msg.Visible = false;
            // 
            // 教室管理
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 518);
            this.Controls.Add(this.lbl_Msg);
            this.Controls.Add(this.toolStripStatusLabel1);
            this.Controls.Add(this.lbl_remark);
            this.Controls.Add(this.lbl_classroomnum);
            this.Controls.Add(this.lbl_machinenum);
            this.Controls.Add(this.cmb_出勤機コード);
            this.Controls.Add(this.txt_classroomnum);
            this.Controls.Add(this.txt_remark);
            this.Controls.Add(this.bt_regist);
            this.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(827, 565);
            this.Name = "教室管理";
            this.Text = "教室情報";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.教室管理_Closed);
            this.Load += new System.EventHandler(this.教室管理_Load);
            this.toolStripStatusLabel1.ResumeLayout(false);
            this.toolStripStatusLabel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_regist;
        private System.Windows.Forms.TextBox txt_remark;
        private System.Windows.Forms.TextBox txt_classroomnum;
        private System.Windows.Forms.ComboBox cmb_出勤機コード;
        private System.Windows.Forms.Label lbl_machinenum;
        private System.Windows.Forms.Label lbl_classroomnum;
        private System.Windows.Forms.Label lbl_remark;
        private System.Windows.Forms.StatusStrip toolStripStatusLabel1;
        private System.Windows.Forms.Label lbl_Msg;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
    }
}