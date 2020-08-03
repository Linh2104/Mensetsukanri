namespace HL_塾管理
{
    partial class 確認ダイヤログ
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(確認ダイヤログ));
            this.btn_OK = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_later = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_OK
            // 
            this.btn_OK.Font = new System.Drawing.Font("MS UI Gothic", 14F);
            this.btn_OK.Location = new System.Drawing.Point(64, 160);
            this.btn_OK.Margin = new System.Windows.Forms.Padding(4);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(129, 46);
            this.btn_OK.TabIndex = 6;
            this.btn_OK.Text = "はい";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 14F);
            this.label2.Location = new System.Drawing.Point(60, 55);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(341, 24);
            this.label2.TabIndex = 7;
            this.label2.Text = "現在、新規クラスの作成しますか？";
            // 
            // btn_later
            // 
            this.btn_later.Font = new System.Drawing.Font("MS UI Gothic", 14F);
            this.btn_later.Location = new System.Drawing.Point(232, 160);
            this.btn_later.Margin = new System.Windows.Forms.Padding(4);
            this.btn_later.Name = "btn_later";
            this.btn_later.Size = new System.Drawing.Size(199, 46);
            this.btn_later.TabIndex = 8;
            this.btn_later.Text = "後に作成します。";
            this.btn_later.UseVisualStyleBackColor = true;
            this.btn_later.Click += new System.EventHandler(this.btn_later_Click);
            // 
            // 確認ダイヤログ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 266);
            this.Controls.Add(this.btn_later);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_OK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(500, 313);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 313);
            this.Name = "確認ダイヤログ";
            this.Text = "教師登録とともに新規クラスの作成が必要です。";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_later;
    }
}