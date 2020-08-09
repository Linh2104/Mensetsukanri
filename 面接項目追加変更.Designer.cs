namespace HL_塾管理
{
    partial class 面接項目追加変更
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
            this.l_name = new System.Windows.Forms.Label();
            this.l_note = new System.Windows.Forms.Label();
            this.b_exe = new System.Windows.Forms.Button();
            this.t_name = new System.Windows.Forms.TextBox();
            this.t_note = new System.Windows.Forms.TextBox();
            this.b_close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // l_name
            // 
            this.l_name.AutoSize = true;
            this.l_name.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.l_name.Location = new System.Drawing.Point(113, 66);
            this.l_name.Name = "l_name";
            this.l_name.Size = new System.Drawing.Size(80, 18);
            this.l_name.TabIndex = 0;
            this.l_name.Text = "項目・質問名";
            // 
            // l_note
            // 
            this.l_note.AutoSize = true;
            this.l_note.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.l_note.Location = new System.Drawing.Point(113, 125);
            this.l_note.Name = "l_note";
            this.l_note.Size = new System.Drawing.Size(32, 18);
            this.l_note.TabIndex = 1;
            this.l_note.Text = "備考";
            // 
            // b_exe
            // 
            this.b_exe.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.b_exe.Location = new System.Drawing.Point(127, 286);
            this.b_exe.Name = "b_exe";
            this.b_exe.Size = new System.Drawing.Size(136, 27);
            this.b_exe.TabIndex = 2;
            this.b_exe.Text = "追加・変更";
            this.b_exe.UseVisualStyleBackColor = true;
            this.b_exe.Click += new System.EventHandler(this.b_exe_Click);
            // 
            // t_name
            // 
            this.t_name.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.t_name.Location = new System.Drawing.Point(194, 63);
            this.t_name.Name = "t_name";
            this.t_name.Size = new System.Drawing.Size(290, 25);
            this.t_name.TabIndex = 3;
            // 
            // t_note
            // 
            this.t_note.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.t_note.Location = new System.Drawing.Point(194, 122);
            this.t_note.Multiline = true;
            this.t_note.Name = "t_note";
            this.t_note.Size = new System.Drawing.Size(290, 113);
            this.t_note.TabIndex = 4;
            // 
            // b_close
            // 
            this.b_close.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.b_close.Location = new System.Drawing.Point(348, 286);
            this.b_close.Name = "b_close";
            this.b_close.Size = new System.Drawing.Size(136, 27);
            this.b_close.TabIndex = 5;
            this.b_close.Text = "閉める";
            this.b_close.UseVisualStyleBackColor = true;
            this.b_close.Click += new System.EventHandler(this.Close_Click);
            // 
            // 面接項目追加変更
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 363);
            this.Controls.Add(this.b_close);
            this.Controls.Add(this.t_note);
            this.Controls.Add(this.t_name);
            this.Controls.Add(this.b_exe);
            this.Controls.Add(this.l_note);
            this.Controls.Add(this.l_name);
            this.Name = "面接項目追加変更";
            this.Text = "項目追加変更";
            this.Load += new System.EventHandler(this.面接項目追加変更_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label l_name;
        private System.Windows.Forms.Label l_note;
        private System.Windows.Forms.Button b_exe;
        private System.Windows.Forms.TextBox t_name;
        private System.Windows.Forms.TextBox t_note;
        private System.Windows.Forms.Button b_close;
    }
}