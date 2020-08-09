namespace HL_塾管理
{
    partial class 模擬案件一覧変更
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
            this.b_close = new System.Windows.Forms.Button();
            this.b_exe = new System.Windows.Forms.Button();
            this.l_note = new System.Windows.Forms.Label();
            this.t_name = new System.Windows.Forms.TextBox();
            this.l_name = new System.Windows.Forms.Label();
            this.cbox_language = new System.Windows.Forms.ComboBox();
            this.l_language = new System.Windows.Forms.Label();
            this.t_note = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // b_close
            // 
            this.b_close.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.b_close.Location = new System.Drawing.Point(365, 372);
            this.b_close.Name = "b_close";
            this.b_close.Size = new System.Drawing.Size(75, 23);
            this.b_close.TabIndex = 15;
            this.b_close.Text = "閉める";
            this.b_close.UseVisualStyleBackColor = true;
            this.b_close.Click += new System.EventHandler(this.Close_Click);
            // 
            // b_exe
            // 
            this.b_exe.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.b_exe.Location = new System.Drawing.Point(213, 372);
            this.b_exe.Name = "b_exe";
            this.b_exe.Size = new System.Drawing.Size(75, 23);
            this.b_exe.TabIndex = 14;
            this.b_exe.Text = "登録・変更";
            this.b_exe.UseVisualStyleBackColor = true;
            this.b_exe.Click += new System.EventHandler(this.b_exe_Click);
            // 
            // l_note
            // 
            this.l_note.AutoSize = true;
            this.l_note.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.l_note.Location = new System.Drawing.Point(111, 177);
            this.l_note.Name = "l_note";
            this.l_note.Size = new System.Drawing.Size(56, 18);
            this.l_note.TabIndex = 12;
            this.l_note.Text = "案件詳細";
            // 
            // t_name
            // 
            this.t_name.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.t_name.Location = new System.Drawing.Point(195, 114);
            this.t_name.Name = "t_name";
            this.t_name.Size = new System.Drawing.Size(265, 25);
            this.t_name.TabIndex = 11;
            // 
            // l_name
            // 
            this.l_name.AutoSize = true;
            this.l_name.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.l_name.Location = new System.Drawing.Point(111, 117);
            this.l_name.Name = "l_name";
            this.l_name.Size = new System.Drawing.Size(80, 18);
            this.l_name.TabIndex = 10;
            this.l_name.Text = "案件タイトル";
            // 
            // cbox_language
            // 
            this.cbox_language.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbox_language.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cbox_language.FormattingEnabled = true;
            this.cbox_language.Items.AddRange(new object[] {
            "PHP",
            "IOS",
            "C#"});
            this.cbox_language.Location = new System.Drawing.Point(195, 55);
            this.cbox_language.Name = "cbox_language";
            this.cbox_language.Size = new System.Drawing.Size(265, 26);
            this.cbox_language.TabIndex = 9;
            this.cbox_language.SelectedIndexChanged += new System.EventHandler(this.cbox_language_SelectedIndexChanged);
            // 
            // l_language
            // 
            this.l_language.AutoSize = true;
            this.l_language.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.l_language.Location = new System.Drawing.Point(111, 58);
            this.l_language.Name = "l_language";
            this.l_language.Size = new System.Drawing.Size(32, 18);
            this.l_language.TabIndex = 8;
            this.l_language.Text = "言語";
            // 
            // t_note
            // 
            this.t_note.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.t_note.Location = new System.Drawing.Point(195, 172);
            this.t_note.Multiline = true;
            this.t_note.Name = "t_note";
            this.t_note.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.t_note.Size = new System.Drawing.Size(265, 167);
            this.t_note.TabIndex = 16;
            // 
            // 模擬案件一覧変更
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 450);
            this.Controls.Add(this.t_note);
            this.Controls.Add(this.b_close);
            this.Controls.Add(this.b_exe);
            this.Controls.Add(this.l_note);
            this.Controls.Add(this.t_name);
            this.Controls.Add(this.l_name);
            this.Controls.Add(this.cbox_language);
            this.Controls.Add(this.l_language);
            this.Title = "";
            this.Text = "模擬案件追加変更";
            this.Load += new System.EventHandler(this.面接項目追加変更_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button b_close;
        private System.Windows.Forms.Button b_exe;
        private System.Windows.Forms.Label l_note;
        private System.Windows.Forms.TextBox t_name;
        private System.Windows.Forms.Label l_name;
        private System.Windows.Forms.ComboBox cbox_language;
        private System.Windows.Forms.Label l_language;
        private System.Windows.Forms.TextBox t_note;
    }
}