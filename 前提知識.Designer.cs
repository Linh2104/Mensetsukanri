namespace HL_塾管理
{
    partial class 前提知識
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
            this.pnl_検索 = new System.Windows.Forms.Panel();
            this.btn_検索実行 = new System.Windows.Forms.Button();
            this.txt_検索欄 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_備考 = new System.Windows.Forms.Label();
            this.lbl_項目 = new System.Windows.Forms.Label();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.項目追加ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.変更ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.削除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblMsg = new System.Windows.Forms.ToolStripStatusLabel();
            this.txt_備考 = new System.Windows.Forms.RichTextBox();
            this.pnl_検索.SuspendLayout();
            this.panel1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_検索
            // 
            this.pnl_検索.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_検索.Controls.Add(this.btn_検索実行);
            this.pnl_検索.Controls.Add(this.txt_検索欄);
            this.pnl_検索.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.pnl_検索.Location = new System.Drawing.Point(12, 37);
            this.pnl_検索.Name = "pnl_検索";
            this.pnl_検索.Size = new System.Drawing.Size(776, 60);
            this.pnl_検索.TabIndex = 0;
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
            this.txt_検索欄.Location = new System.Drawing.Point(19, 15);
            this.txt_検索欄.Multiline = true;
            this.txt_検索欄.Name = "txt_検索欄";
            this.txt_検索欄.Size = new System.Drawing.Size(232, 29);
            this.txt_検索欄.TabIndex = 0;
            this.txt_検索欄.TextChanged += new System.EventHandler(this.txt_検索欄_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.txt_備考);
            this.panel1.Controls.Add(this.lbl_備考);
            this.panel1.Controls.Add(this.lbl_項目);
            this.panel1.Controls.Add(this.treeView1);
            this.panel1.Location = new System.Drawing.Point(12, 117);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(776, 295);
            this.panel1.TabIndex = 1;
            // 
            // lbl_備考
            // 
            this.lbl_備考.AutoSize = true;
            this.lbl_備考.Location = new System.Drawing.Point(542, 4);
            this.lbl_備考.Name = "lbl_備考";
            this.lbl_備考.Size = new System.Drawing.Size(29, 12);
            this.lbl_備考.TabIndex = 7;
            this.lbl_備考.Text = "備考";
            // 
            // lbl_項目
            // 
            this.lbl_項目.AutoSize = true;
            this.lbl_項目.Location = new System.Drawing.Point(29, 4);
            this.lbl_項目.Name = "lbl_項目";
            this.lbl_項目.Size = new System.Drawing.Size(29, 12);
            this.lbl_項目.TabIndex = 6;
            this.lbl_項目.Text = "項目";
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView1.ContextMenuStrip = this.contextMenuStrip1;
            this.treeView1.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.treeView1.Location = new System.Drawing.Point(3, 18);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(492, 277);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.項目追加ToolStripMenuItem,
            this.変更ToolStripMenuItem,
            this.削除ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(123, 70);
            // 
            // 項目追加ToolStripMenuItem
            // 
            this.項目追加ToolStripMenuItem.Name = "項目追加ToolStripMenuItem";
            this.項目追加ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.項目追加ToolStripMenuItem.Text = "項目追加";
            this.項目追加ToolStripMenuItem.Click += new System.EventHandler(this.項目追加ToolStripMenuItem_Click);
            // 
            // 変更ToolStripMenuItem
            // 
            this.変更ToolStripMenuItem.Name = "変更ToolStripMenuItem";
            this.変更ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.変更ToolStripMenuItem.Text = "変更";
            this.変更ToolStripMenuItem.Click += new System.EventHandler(this.変更ToolStripMenuItem_Click);
            // 
            // 削除ToolStripMenuItem
            // 
            this.削除ToolStripMenuItem.Name = "削除ToolStripMenuItem";
            this.削除ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.削除ToolStripMenuItem.Text = "削除";
            this.削除ToolStripMenuItem.Click += new System.EventHandler(this.削除ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblMsg});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblMsg
            // 
            this.lblMsg.Font = new System.Drawing.Font("Meiryo", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(0, 17);
            // 
            // txt_備考
            // 
            this.txt_備考.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_備考.BackColor = System.Drawing.Color.White;
            this.txt_備考.Location = new System.Drawing.Point(515, 19);
            this.txt_備考.Name = "txt_備考";
            this.txt_備考.Size = new System.Drawing.Size(258, 276);
            this.txt_備考.TabIndex = 8;
            this.txt_備考.Text = "";
            // 
            // 前提知識
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnl_検索);
            this.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Name = "前提知識";
            this.Text = "前提知識";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.前提知識_FormClosed);
            this.Load += new System.EventHandler(this.前提知識_Load);
            this.Resize += new System.EventHandler(this.前提知識_Resize);
            this.pnl_検索.ResumeLayout(false);
            this.pnl_検索.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnl_検索;
        private System.Windows.Forms.Button btn_検索実行;
        private System.Windows.Forms.TextBox txt_検索欄;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_項目;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 項目追加ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 変更ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 削除ToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblMsg;
        private System.Windows.Forms.Label lbl_備考;
        private System.Windows.Forms.RichTextBox txt_備考;
    }
}