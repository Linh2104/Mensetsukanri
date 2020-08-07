namespace HL_塾管理
{
    partial class 面接練習
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
            this.components = new System.ComponentModel.Container();
            this.pnl_項目 = new System.Windows.Forms.Panel();
            this.lbl_備考 = new System.Windows.Forms.Label();
            this.rchtxt_備考 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.追加ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.変更ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.削除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblMsg = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnl_検索 = new System.Windows.Forms.Panel();
            this.btn_検索実行 = new System.Windows.Forms.Button();
            this.txt_検索欄 = new System.Windows.Forms.TextBox();
            this.pnl_項目.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.pnl_検索.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_項目
            // 
            this.pnl_項目.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_項目.Controls.Add(this.lbl_備考);
            this.pnl_項目.Controls.Add(this.rchtxt_備考);
            this.pnl_項目.Controls.Add(this.label1);
            this.pnl_項目.Controls.Add(this.treeView1);
            this.pnl_項目.Location = new System.Drawing.Point(15, 75);
            this.pnl_項目.Name = "pnl_項目";
            this.pnl_項目.Size = new System.Drawing.Size(854, 324);
            this.pnl_項目.TabIndex = 0;
            // 
            // lbl_備考
            // 
            this.lbl_備考.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_備考.AutoSize = true;
            this.lbl_備考.Location = new System.Drawing.Point(529, 11);
            this.lbl_備考.Name = "lbl_備考";
            this.lbl_備考.Size = new System.Drawing.Size(29, 12);
            this.lbl_備考.TabIndex = 5;
            this.lbl_備考.Text = "備考";
            // 
            // rchtxt_備考
            // 
            this.rchtxt_備考.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rchtxt_備考.BackColor = System.Drawing.Color.White;
            this.rchtxt_備考.Location = new System.Drawing.Point(478, 30);
            this.rchtxt_備考.Name = "rchtxt_備考";
            this.rchtxt_備考.ReadOnly = true;
            this.rchtxt_備考.Size = new System.Drawing.Size(360, 291);
            this.rchtxt_備考.TabIndex = 6;
            this.rchtxt_備考.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "項目";
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.Location = new System.Drawing.Point(22, 30);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(432, 291);
            this.treeView1.TabIndex = 0;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.追加ToolStripMenuItem,
            this.変更ToolStripMenuItem,
            this.削除ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(99, 70);
            // 
            // 追加ToolStripMenuItem
            // 
            this.追加ToolStripMenuItem.Name = "追加ToolStripMenuItem";
            this.追加ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.追加ToolStripMenuItem.Text = "追加";
            // 
            // 変更ToolStripMenuItem
            // 
            this.変更ToolStripMenuItem.Name = "変更ToolStripMenuItem";
            this.変更ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.変更ToolStripMenuItem.Text = "変更";
            // 
            // 削除ToolStripMenuItem
            // 
            this.削除ToolStripMenuItem.Name = "削除ToolStripMenuItem";
            this.削除ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.削除ToolStripMenuItem.Text = "削除";
            this.削除ToolStripMenuItem.Click += new System.EventHandler(this.削除ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblMsg});
            this.statusStrip1.Location = new System.Drawing.Point(0, 402);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(895, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblMsg
            // 
            this.lblMsg.Font = new System.Drawing.Font("Meiryo", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(0, 17);
            // 
            // pnl_検索
            // 
            this.pnl_検索.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_検索.Controls.Add(this.btn_検索実行);
            this.pnl_検索.Controls.Add(this.txt_検索欄);
            this.pnl_検索.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.pnl_検索.Location = new System.Drawing.Point(15, 9);
            this.pnl_検索.Name = "pnl_検索";
            this.pnl_検索.Size = new System.Drawing.Size(854, 60);
            this.pnl_検索.TabIndex = 3;
            // 
            // btn_検索実行
            // 
            this.btn_検索実行.Location = new System.Drawing.Point(269, 19);
            this.btn_検索実行.Name = "btn_検索実行";
            this.btn_検索実行.Size = new System.Drawing.Size(75, 23);
            this.btn_検索実行.TabIndex = 1;
            this.btn_検索実行.Text = "検索実行";
            this.btn_検索実行.UseVisualStyleBackColor = true;
            this.btn_検索実行.Click += new System.EventHandler(this.btn_検索実行_Click);
            // 
            // txt_検索欄
            // 
            this.txt_検索欄.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txt_検索欄.Location = new System.Drawing.Point(22, 15);
            this.txt_検索欄.Multiline = true;
            this.txt_検索欄.Name = "txt_検索欄";
            this.txt_検索欄.Size = new System.Drawing.Size(229, 29);
            this.txt_検索欄.TabIndex = 0;
            // 
            // 面接練習
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 424);
            this.Controls.Add(this.pnl_検索);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pnl_項目);
            this.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Name = "面接練習";
            this.Text = "面接練習";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.面接練習_FormClosed);
            this.Load += new System.EventHandler(this.面接練習_Load);
            this.pnl_項目.ResumeLayout(false);
            this.pnl_項目.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.pnl_検索.ResumeLayout(false);
            this.pnl_検索.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnl_項目;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblMsg;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 追加ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 変更ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 削除ToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_備考;
        private System.Windows.Forms.Panel pnl_検索;
        private System.Windows.Forms.Button btn_検索実行;
        private System.Windows.Forms.TextBox txt_検索欄;
        private System.Windows.Forms.RichTextBox rchtxt_備考;
    }
}