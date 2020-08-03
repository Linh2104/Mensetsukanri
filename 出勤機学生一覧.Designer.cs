using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace HL_塾管理
{
    partial class 出勤機学生一覧
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
            WeifenLuo.WinFormsUI.Docking.DockPanelSkin dockPanelSkin1 = new WeifenLuo.WinFormsUI.Docking.DockPanelSkin();
            WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin autoHideStripSkin1 = new WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient1 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin dockPaneStripSkin1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient dockPaneStripGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient2 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient2 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient3 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient dockPaneStripToolWindowGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient4 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient5 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient3 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient6 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient7 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(出勤機学生一覧));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.削除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dockPanel1 = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.gridView_出勤機ユーザ = new RowMergeView();
            this.btn_search = new System.Windows.Forms.Button();
            this.txt_searchKey = new System.Windows.Forms.TextBox();
            this.出勤機 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.登録コード = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.学生コード = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.学生名 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.担当教師 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_出勤機ユーザ)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.削除ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(109, 28);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // 削除ToolStripMenuItem
            // 
            this.削除ToolStripMenuItem.Name = "削除ToolStripMenuItem";
            this.削除ToolStripMenuItem.Size = new System.Drawing.Size(108, 24);
            this.削除ToolStripMenuItem.Text = "削除";
            this.削除ToolStripMenuItem.Click += new System.EventHandler(this.削除ToolStripMenuItem_Click);
            // 
            // dockPanel1
            // 
            this.dockPanel1.ActiveAutoHideContent = null;
            this.dockPanel1.DockBackColor = System.Drawing.SystemColors.Control;
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Margin = new System.Windows.Forms.Padding(9, 8, 9, 8);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Size = new System.Drawing.Size(633, 244);
            dockPanelGradient1.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient1.StartColor = System.Drawing.SystemColors.ControlLight;
            autoHideStripSkin1.DockStripGradient = dockPanelGradient1;
            tabGradient1.EndColor = System.Drawing.SystemColors.Control;
            tabGradient1.StartColor = System.Drawing.SystemColors.Control;
            tabGradient1.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            autoHideStripSkin1.TabGradient = tabGradient1;
            dockPanelSkin1.AutoHideStripSkin = autoHideStripSkin1;
            tabGradient2.EndColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient2.StartColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient2.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripGradient1.ActiveTabGradient = tabGradient2;
            dockPanelGradient2.EndColor = System.Drawing.SystemColors.Control;
            dockPanelGradient2.StartColor = System.Drawing.SystemColors.Control;
            dockPaneStripGradient1.DockStripGradient = dockPanelGradient2;
            tabGradient3.EndColor = System.Drawing.SystemColors.ControlLight;
            tabGradient3.StartColor = System.Drawing.SystemColors.ControlLight;
            tabGradient3.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripGradient1.InactiveTabGradient = tabGradient3;
            dockPaneStripSkin1.DocumentGradient = dockPaneStripGradient1;
            tabGradient4.EndColor = System.Drawing.SystemColors.ActiveCaption;
            tabGradient4.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient4.StartColor = System.Drawing.SystemColors.GradientActiveCaption;
            tabGradient4.TextColor = System.Drawing.SystemColors.ActiveCaptionText;
            dockPaneStripToolWindowGradient1.ActiveCaptionGradient = tabGradient4;
            tabGradient5.EndColor = System.Drawing.SystemColors.Control;
            tabGradient5.StartColor = System.Drawing.SystemColors.Control;
            tabGradient5.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripToolWindowGradient1.ActiveTabGradient = tabGradient5;
            dockPanelGradient3.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient3.StartColor = System.Drawing.SystemColors.ControlLight;
            dockPaneStripToolWindowGradient1.DockStripGradient = dockPanelGradient3;
            tabGradient6.EndColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tabGradient6.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient6.StartColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tabGradient6.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripToolWindowGradient1.InactiveCaptionGradient = tabGradient6;
            tabGradient7.EndColor = System.Drawing.Color.Transparent;
            tabGradient7.StartColor = System.Drawing.Color.Transparent;
            tabGradient7.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            dockPaneStripToolWindowGradient1.InactiveTabGradient = tabGradient7;
            dockPaneStripSkin1.ToolWindowGradient = dockPaneStripToolWindowGradient1;
            dockPanelSkin1.DockPaneStripSkin = dockPaneStripSkin1;
            this.dockPanel1.Skin = dockPanelSkin1;
            this.dockPanel1.TabIndex = 120;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStrip1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 1000);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 44, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1360, 22);
            this.statusStrip1.TabIndex = 122;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(0, 17);
            // 
            // gridView_出勤機ユーザ
            // 
            this.gridView_出勤機ユーザ.AllowUserToAddRows = false;
            this.gridView_出勤機ユーザ.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridView_出勤機ユーザ.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridView_出勤機ユーザ.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.出勤機,
            this.登録コード,
            this.学生コード,
            this.学生名,
            this.担当教師});
            this.gridView_出勤機ユーザ.ContextMenuStrip = this.contextMenuStrip1;
            this.gridView_出勤機ユーザ.Key = "";
            this.gridView_出勤機ユーザ.Location = new System.Drawing.Point(44, 100);
            this.gridView_出勤機ユーザ.Margin = new System.Windows.Forms.Padding(9, 8, 9, 8);
            this.gridView_出勤機ユーザ.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.gridView_出勤機ユーザ.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("gridView_出勤機ユーザ.MergeColumnNames")));
            this.gridView_出勤機ユーザ.MergeRowIndex = ((System.Collections.Generic.Dictionary<int, int>)(resources.GetObject("gridView_出勤機ユーザ.MergeRowIndex")));
            this.gridView_出勤機ユーザ.Name = "gridView_出勤機ユーザ";
            this.gridView_出勤機ユーザ.NoLink = ((System.Collections.Generic.List<object>)(resources.GetObject("gridView_出勤機ユーザ.NoLink")));
            this.gridView_出勤機ユーザ.RowTemplate.Height = 23;
            this.gridView_出勤機ユーザ.Size = new System.Drawing.Size(1152, 872);
            this.gridView_出勤機ユーザ.TabIndex = 9;
            this.gridView_出勤機ユーザ.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.gridView_出勤機ユーザ_CellBeginEdit);
            this.gridView_出勤機ユーザ.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridView_出勤機ユーザ_CellEndEdit);
            this.gridView_出勤機ユーザ.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridView_出勤機ユーザ_CellMouseDown);
            this.gridView_出勤機ユーザ.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridView_出勤機ユーザ_CellValueChanged);
            this.gridView_出勤機ユーザ.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.gridView_出勤機ユーザ_DataError);
            this.gridView_出勤機ユーザ.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.gridView_出勤機ユーザ_EditingControlShowing);
            this.gridView_出勤機ユーザ.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.gridView_出勤機ユーザ_SortCompare);
            this.gridView_出勤機ユーザ.SizeChanged += new System.EventHandler(this.gridView_出勤機ユーザ_SizeChanged);
            // 
            // btn_search
            // 
            this.btn_search.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btn_search.Location = new System.Drawing.Point(449, 29);
            this.btn_search.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(153, 52);
            this.btn_search.TabIndex = 136;
            this.btn_search.Text = "検索";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // txt_searchKey
            // 
            this.txt_searchKey.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_searchKey.Location = new System.Drawing.Point(44, 31);
            this.txt_searchKey.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.txt_searchKey.Name = "txt_searchKey";
            this.txt_searchKey.Size = new System.Drawing.Size(352, 36);
            this.txt_searchKey.TabIndex = 137;
            // 
            // 出勤機
            // 
            this.出勤機.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Aqua;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Blue;
            this.出勤機.DefaultCellStyle = dataGridViewCellStyle1;
            this.出勤機.HeaderText = "出勤機";
            this.出勤機.Name = "出勤機";
            this.出勤機.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.出勤機.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // 登録コード
            // 
            this.登録コード.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.登録コード.DefaultCellStyle = dataGridViewCellStyle2;
            this.登録コード.HeaderText = "登録コード";
            this.登録コード.Name = "登録コード";
            this.登録コード.ReadOnly = true;
            this.登録コード.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // 学生コード
            // 
            this.学生コード.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.学生コード.DefaultCellStyle = dataGridViewCellStyle3;
            this.学生コード.HeaderText = "学生コード";
            this.学生コード.MaxInputLength = 500;
            this.学生コード.Name = "学生コード";
            this.学生コード.ReadOnly = true;
            // 
            // 学生名
            // 
            this.学生名.HeaderText = "学生名";
            this.学生名.Name = "学生名";
            this.学生名.ReadOnly = true;
            // 
            // 担当教師
            // 
            this.担当教師.HeaderText = "担当教師";
            this.担当教師.Name = "担当教師";
            this.担当教師.ReadOnly = true;
            // 
            // 出勤機学生一覧
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1360, 1022);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.txt_searchKey);
            this.Controls.Add(this.btn_search);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.gridView_出勤機ユーザ);
            this.Controls.Add(this.dockPanel1);
            this.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(9, 8, 9, 8);
            this.MaximizeBox = false;
            this.Name = "出勤機学生一覧";
            this.Text = "出勤機学生一覧";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.出勤機学生一覧_FormClosed);
            this.Load += new System.EventHandler(this.出勤機学生一覧_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_出勤機ユーザ)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public RowMergeView gridView_出勤機ユーザ;
        public DockPanel dockPanel1;
        private StatusStrip statusStrip1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem 削除ToolStripMenuItem;
        private ToolStripStatusLabel toolStrip1;
        private Button btn_search;
        private TextBox txt_searchKey;
        private DataGridViewComboBoxColumn 出勤機;
        private DataGridViewTextBoxColumn 登録コード;
        private DataGridViewTextBoxColumn 学生コード;
        private DataGridViewTextBoxColumn 学生名;
        private DataGridViewTextBoxColumn 担当教師;
    }
}