using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace HL_塾管理
{
    partial class クラス選択
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(クラス選択));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dockPanel1 = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.gridView_クラスInfo = new RowMergeView();
            this.加入 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.クラスコード = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.クラス名 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.課程 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.教室コード = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.教室 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.教師名 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.開始日 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.終了日 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.学生コード = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.出勤機コード = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_クラスInfo)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dockPanel1
            // 
            this.dockPanel1.ActiveAutoHideContent = null;
            this.dockPanel1.DockBackColor = System.Drawing.SystemColors.Control;
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Margin = new System.Windows.Forms.Padding(5);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Size = new System.Drawing.Size(356, 156);
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
            // gridView_クラスInfo
            // 
            this.gridView_クラスInfo.AllowUserToAddRows = false;
            this.gridView_クラスInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridView_クラスInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridView_クラスInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.加入,
            this.クラスコード,
            this.クラス名,
            this.課程,
            this.教室コード,
            this.教室,
            this.教師名,
            this.開始日,
            this.終了日,
            this.学生コード,
            this.出勤機コード});
            this.gridView_クラスInfo.Key = "";
            this.gridView_クラスInfo.Location = new System.Drawing.Point(12, 21);
            this.gridView_クラスInfo.Margin = new System.Windows.Forms.Padding(5);
            this.gridView_クラスInfo.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.gridView_クラスInfo.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("gridView_クラスInfo.MergeColumnNames")));
            this.gridView_クラスInfo.MergeRowIndex = ((System.Collections.Generic.Dictionary<int, int>)(resources.GetObject("gridView_クラスInfo.MergeRowIndex")));
            this.gridView_クラスInfo.MinimumSize = new System.Drawing.Size(500, 0);
            this.gridView_クラスInfo.Name = "gridView_クラスInfo";
            this.gridView_クラスInfo.NoLink = ((System.Collections.Generic.List<object>)(resources.GetObject("gridView_クラスInfo.NoLink")));
            this.gridView_クラスInfo.RowTemplate.Height = 23;
            this.gridView_クラスInfo.Size = new System.Drawing.Size(757, 451);
            this.gridView_クラスInfo.TabIndex = 9;
            this.gridView_クラスInfo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridView_クラスInfo_CellContentClick);
            this.gridView_クラスInfo.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.gridView_クラスInfo_DataError);
            // 
            // 加入
            // 
            this.加入.HeaderText = "";
            this.加入.Name = "加入";
            this.加入.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.加入.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // クラスコード
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Meiryo", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.クラスコード.DefaultCellStyle = dataGridViewCellStyle1;
            this.クラスコード.HeaderText = "クラスコード";
            this.クラスコード.Name = "クラスコード";
            this.クラスコード.ReadOnly = true;
            this.クラスコード.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.クラスコード.Visible = false;
            // 
            // クラス名
            // 
            this.クラス名.HeaderText = "クラス名";
            this.クラス名.Name = "クラス名";
            // 
            // 課程
            // 
            this.課程.HeaderText = "課程";
            this.課程.Name = "課程";
            this.課程.ReadOnly = true;
            // 
            // 教室コード
            // 
            this.教室コード.HeaderText = "教室コード";
            this.教室コード.Name = "教室コード";
            this.教室コード.ReadOnly = true;
            this.教室コード.Visible = false;
            // 
            // 教室
            // 
            this.教室.HeaderText = "教室";
            this.教室.Name = "教室";
            this.教室.ReadOnly = true;
            // 
            // 教師名
            // 
            this.教師名.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Meiryo", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.教師名.DefaultCellStyle = dataGridViewCellStyle2;
            this.教師名.FillWeight = 150F;
            this.教師名.HeaderText = "教師名";
            this.教師名.Name = "教師名";
            this.教師名.ReadOnly = true;
            this.教師名.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // 開始日
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.開始日.DefaultCellStyle = dataGridViewCellStyle3;
            this.開始日.HeaderText = "開始日";
            this.開始日.Name = "開始日";
            this.開始日.ReadOnly = true;
            // 
            // 終了日
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.終了日.DefaultCellStyle = dataGridViewCellStyle4;
            this.終了日.HeaderText = "終了日";
            this.終了日.Name = "終了日";
            // 
            // 学生コード
            // 
            this.学生コード.HeaderText = "学生コード";
            this.学生コード.Name = "学生コード";
            this.学生コード.Visible = false;
            // 
            // 出勤機コード
            // 
            this.出勤機コード.HeaderText = "出勤機コード";
            this.出勤機コード.Name = "出勤機コード";
            this.出勤機コード.Visible = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 484);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 25, 0);
            this.statusStrip1.Size = new System.Drawing.Size(781, 22);
            this.statusStrip1.TabIndex = 124;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 17);
            // 
            // クラス選択
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 506);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.gridView_クラスInfo);
            this.Controls.Add(this.dockPanel1);
            this.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(549, 45);
            this.Name = "クラス選択";
            this.Text = "クラス課程選択";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.クラス選択_FormClosed);
            this.Load += new System.EventHandler(this.クラス選択_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridView_クラスInfo)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public RowMergeView gridView_クラスInfo;
        public DockPanel dockPanel1;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private DataGridViewButtonColumn 加入;
        private DataGridViewTextBoxColumn クラスコード;
        private DataGridViewTextBoxColumn クラス名;
        private DataGridViewTextBoxColumn 課程;
        private DataGridViewTextBoxColumn 教室コード;
        private DataGridViewTextBoxColumn 教室;
        private DataGridViewTextBoxColumn 教師名;
        private DataGridViewTextBoxColumn 開始日;
        private DataGridViewTextBoxColumn 終了日;
        private DataGridViewTextBoxColumn 学生コード;
        private DataGridViewTextBoxColumn 出勤機コード;
        private ToolStripStatusLabel toolStripStatusLabel2;
    }
}