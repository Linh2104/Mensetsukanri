namespace HL_塾管理
{
    partial class 会議室予約
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(会議室予約));
            this.lbl_roomName = new System.Windows.Forms.Label();
            this.lbl_count = new System.Windows.Forms.Label();
            this.lbl_title = new System.Windows.Forms.Label();
            this.txt_title = new System.Windows.Forms.TextBox();
            this.lbl_member = new System.Windows.Forms.Label();
            this.lbl_info = new System.Windows.Forms.Label();
            this.lbl_reservation = new System.Windows.Forms.Label();
            this.txt_member = new System.Windows.Forms.TextBox();
            this.txt_info = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btn_insert = new System.Windows.Forms.Button();
            this.btn_update = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.dtp_date = new System.Windows.Forms.DateTimePicker();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.dtp_endTime = new System.Windows.Forms.DateTimePicker();
            this.dtp_startTime = new System.Windows.Forms.DateTimePicker();
            this.lbl_date = new System.Windows.Forms.Label();
            this.cmb_roomName = new System.Windows.Forms.ComboBox();
            this.cmb_count = new System.Windows.Forms.ComboBox();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_roomName
            // 
            this.lbl_roomName.AutoSize = true;
            this.lbl_roomName.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_roomName.Location = new System.Drawing.Point(41, 178);
            this.lbl_roomName.Name = "lbl_roomName";
            this.lbl_roomName.Size = new System.Drawing.Size(129, 23);
            this.lbl_roomName.TabIndex = 91;
            this.lbl_roomName.Text = "会議室名　[必須]";
            this.lbl_roomName.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl_必須_Paint);
            // 
            // lbl_count
            // 
            this.lbl_count.AutoSize = true;
            this.lbl_count.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_count.Location = new System.Drawing.Point(41, 221);
            this.lbl_count.Name = "lbl_count";
            this.lbl_count.Size = new System.Drawing.Size(99, 23);
            this.lbl_count.TabIndex = 93;
            this.lbl_count.Text = "人数　[必須]";
            this.lbl_count.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl_必須_Paint);
            // 
            // lbl_title
            // 
            this.lbl_title.AutoSize = true;
            this.lbl_title.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_title.Location = new System.Drawing.Point(41, 40);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(129, 23);
            this.lbl_title.TabIndex = 83;
            this.lbl_title.Text = "タイトル　[必須]";
            this.lbl_title.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl_必須_Paint);
            // 
            // txt_title
            // 
            this.txt_title.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_title.Location = new System.Drawing.Point(194, 37);
            this.txt_title.MaxLength = 100;
            this.txt_title.Name = "txt_title";
            this.txt_title.Size = new System.Drawing.Size(557, 30);
            this.txt_title.TabIndex = 84;
            // 
            // lbl_member
            // 
            this.lbl_member.AutoSize = true;
            this.lbl_member.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_member.Location = new System.Drawing.Point(41, 267);
            this.lbl_member.Name = "lbl_member";
            this.lbl_member.Size = new System.Drawing.Size(114, 23);
            this.lbl_member.TabIndex = 95;
            this.lbl_member.Text = "参加者　[必須]";
            this.lbl_member.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl_必須_Paint);
            // 
            // lbl_info
            // 
            this.lbl_info.AutoSize = true;
            this.lbl_info.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_info.Location = new System.Drawing.Point(41, 383);
            this.lbl_info.Name = "lbl_info";
            this.lbl_info.Size = new System.Drawing.Size(40, 23);
            this.lbl_info.TabIndex = 97;
            this.lbl_info.Text = "備考";
            // 
            // lbl_reservation
            // 
            this.lbl_reservation.AutoSize = true;
            this.lbl_reservation.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_reservation.Location = new System.Drawing.Point(41, 131);
            this.lbl_reservation.Name = "lbl_reservation";
            this.lbl_reservation.Size = new System.Drawing.Size(129, 23);
            this.lbl_reservation.TabIndex = 87;
            this.lbl_reservation.Text = "予約時間　[必須]";
            this.lbl_reservation.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl_必須_Paint);
            // 
            // txt_member
            // 
            this.txt_member.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_member.Location = new System.Drawing.Point(194, 264);
            this.txt_member.MaxLength = 500;
            this.txt_member.Multiline = true;
            this.txt_member.Name = "txt_member";
            this.txt_member.Size = new System.Drawing.Size(296, 99);
            this.txt_member.TabIndex = 96;
            // 
            // txt_info
            // 
            this.txt_info.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_info.Location = new System.Drawing.Point(194, 380);
            this.txt_info.MaxLength = 500;
            this.txt_info.Multiline = true;
            this.txt_info.Name = "txt_info";
            this.txt_info.Size = new System.Drawing.Size(296, 84);
            this.txt_info.TabIndex = 98;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label10.Location = new System.Drawing.Point(304, 137);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(25, 23);
            this.label10.TabIndex = 89;
            this.label10.Text = "～";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_insert
            // 
            this.btn_insert.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btn_insert.Location = new System.Drawing.Point(289, 485);
            this.btn_insert.Name = "btn_insert";
            this.btn_insert.Size = new System.Drawing.Size(115, 36);
            this.btn_insert.TabIndex = 99;
            this.btn_insert.Text = "登録";
            this.btn_insert.UseVisualStyleBackColor = true;
            this.btn_insert.Click += new System.EventHandler(this.btn_insert_Click);
            // 
            // btn_update
            // 
            this.btn_update.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btn_update.Location = new System.Drawing.Point(289, 485);
            this.btn_update.Name = "btn_update";
            this.btn_update.Size = new System.Drawing.Size(115, 36);
            this.btn_update.TabIndex = 100;
            this.btn_update.Text = "更新";
            this.btn_update.UseVisualStyleBackColor = true;
            this.btn_update.Click += new System.EventHandler(this.btn_update_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.Location = new System.Drawing.Point(269, 219);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(25, 23);
            this.label7.TabIndex = 113;
            this.label7.Text = "人";
            // 
            // dtp_date
            // 
            this.dtp_date.CustomFormat = "yyyy/MM/dd";
            this.dtp_date.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dtp_date.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_date.Location = new System.Drawing.Point(194, 84);
            this.dtp_date.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dtp_date.Name = "dtp_date";
            this.dtp_date.Size = new System.Drawing.Size(146, 30);
            this.dtp_date.TabIndex = 86;
            this.dtp_date.Value = new System.DateTime(2019, 11, 27, 13, 19, 55, 0);
            this.dtp_date.ValueChanged += new System.EventHandler(this.txt_date_ValueChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 540);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(984, 22);
            this.statusStrip1.TabIndex = 115;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // dtp_endTime
            // 
            this.dtp_endTime.CustomFormat = "HH:mm";
            this.dtp_endTime.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dtp_endTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_endTime.Location = new System.Drawing.Point(351, 131);
            this.dtp_endTime.Name = "dtp_endTime";
            this.dtp_endTime.ShowUpDown = true;
            this.dtp_endTime.Size = new System.Drawing.Size(85, 30);
            this.dtp_endTime.TabIndex = 90;
            this.dtp_endTime.Value = new System.DateTime(2019, 11, 28, 13, 53, 38, 0);
            // 
            // dtp_startTime
            // 
            this.dtp_startTime.CustomFormat = "HH:mm";
            this.dtp_startTime.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dtp_startTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_startTime.Location = new System.Drawing.Point(194, 131);
            this.dtp_startTime.Name = "dtp_startTime";
            this.dtp_startTime.ShowUpDown = true;
            this.dtp_startTime.Size = new System.Drawing.Size(85, 30);
            this.dtp_startTime.TabIndex = 88;
            this.dtp_startTime.Value = new System.DateTime(2019, 11, 27, 14, 17, 0, 0);
            // 
            // lbl_date
            // 
            this.lbl_date.AutoSize = true;
            this.lbl_date.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_date.Location = new System.Drawing.Point(41, 84);
            this.lbl_date.Name = "lbl_date";
            this.lbl_date.Size = new System.Drawing.Size(99, 23);
            this.lbl_date.TabIndex = 85;
            this.lbl_date.Text = "日付　[必須]";
            this.lbl_date.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl_必須_Paint);
            // 
            // cmb_roomName
            // 
            this.cmb_roomName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_roomName.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmb_roomName.FormattingEnabled = true;
            this.cmb_roomName.Location = new System.Drawing.Point(194, 178);
            this.cmb_roomName.Name = "cmb_roomName";
            this.cmb_roomName.Size = new System.Drawing.Size(121, 26);
            this.cmb_roomName.TabIndex = 92;
            this.cmb_roomName.SelectedIndexChanged += new System.EventHandler(this.cmb_roomName_SelectedIndexChanged);
            // 
            // cmb_count
            // 
            this.cmb_count.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_count.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmb_count.FormattingEnabled = true;
            this.cmb_count.Location = new System.Drawing.Point(194, 221);
            this.cmb_count.Name = "cmb_count";
            this.cmb_count.Size = new System.Drawing.Size(66, 26);
            this.cmb_count.TabIndex = 94;
            // 
            // 会議室予約
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 562);
            this.Controls.Add(this.cmb_count);
            this.Controls.Add(this.cmb_roomName);
            this.Controls.Add(this.dtp_endTime);
            this.Controls.Add(this.dtp_startTime);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dtp_date);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btn_update);
            this.Controls.Add(this.btn_insert);
            this.Controls.Add(this.txt_info);
            this.Controls.Add(this.txt_member);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lbl_reservation);
            this.Controls.Add(this.lbl_info);
            this.Controls.Add(this.lbl_member);
            this.Controls.Add(this.lbl_date);
            this.Controls.Add(this.txt_title);
            this.Controls.Add(this.lbl_title);
            this.Controls.Add(this.lbl_count);
            this.Controls.Add(this.lbl_roomName);
            this.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "会議室予約";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "会議室 予約";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.会議室予約_FormClosed);
            this.Load += new System.EventHandler(this.Page_load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_roomName;
        private System.Windows.Forms.Label lbl_count;
        private System.Windows.Forms.Label lbl_title;
        private System.Windows.Forms.TextBox txt_title;
        private System.Windows.Forms.Label lbl_member;
        private System.Windows.Forms.Label lbl_info;
        private System.Windows.Forms.Label lbl_reservation;
        private System.Windows.Forms.TextBox txt_member;
        private System.Windows.Forms.TextBox txt_info;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btn_insert;
        private System.Windows.Forms.Button btn_update;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtp_date;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.DateTimePicker dtp_endTime;
        private System.Windows.Forms.DateTimePicker dtp_startTime;
        private System.Windows.Forms.Label lbl_date;
        private System.Windows.Forms.ComboBox cmb_roomName;
        private System.Windows.Forms.ComboBox cmb_count;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}