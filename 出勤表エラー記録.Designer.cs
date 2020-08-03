using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace HL_塾管理
{
    partial class 出勤表エラー記録
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(出勤表エラー記録));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.追加ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.削除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dockPanel1 = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.lbl_date = new System.Windows.Forms.Label();
            this.dtp_date = new System.Windows.Forms.DateTimePicker();
            this.roomView = new RowMergeView();
            this.学生コード = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.学生名 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.出勤機 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.登録コード = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.出退勤フラグ = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.出退勤時間 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.元日時 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NewRowフラグ = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.lbl_学生コード = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1_count = new System.Windows.Forms.ToolStripStatusLabel();
            this.btn_エラー修正 = new System.Windows.Forms.Button();
            this.btn_出勤記録新規追加 = new System.Windows.Forms.Button();
            this.cmb_学生コード = new System.Windows.Forms.ComboBox();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.roomView)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.追加ToolStripMenuItem,
            this.削除ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(148, 52);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // 追加ToolStripMenuItem
            // 
            this.追加ToolStripMenuItem.Name = "追加ToolStripMenuItem";
            this.追加ToolStripMenuItem.Size = new System.Drawing.Size(147, 24);
            this.追加ToolStripMenuItem.Text = "コピー・追加";
            this.追加ToolStripMenuItem.Click += new System.EventHandler(this.追加ToolStripMenuItem_Click);
            // 
            // 削除ToolStripMenuItem
            // 
            this.削除ToolStripMenuItem.Name = "削除ToolStripMenuItem";
            this.削除ToolStripMenuItem.Size = new System.Drawing.Size(147, 24);
            this.削除ToolStripMenuItem.Text = "削除";
            this.削除ToolStripMenuItem.Click += new System.EventHandler(this.削除ToolStripMenuItem_Click);
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
            // lbl_date
            // 
            this.lbl_date.AutoSize = true;
            this.lbl_date.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_date.Location = new System.Drawing.Point(30, 24);
            this.lbl_date.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbl_date.Name = "lbl_date";
            this.lbl_date.Size = new System.Drawing.Size(50, 28);
            this.lbl_date.TabIndex = 125;
            this.lbl_date.Text = "日付";
            // 
            // dtp_date
            // 
            this.dtp_date.CustomFormat = "yyyy年MM月";
            this.dtp_date.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dtp_date.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_date.Location = new System.Drawing.Point(112, 24);
            this.dtp_date.Margin = new System.Windows.Forms.Padding(5);
            this.dtp_date.Name = "dtp_date";
            this.dtp_date.ShowUpDown = true;
            this.dtp_date.Size = new System.Drawing.Size(195, 26);
            this.dtp_date.TabIndex = 124;
            this.dtp_date.ValueChanged += new System.EventHandler(this.dtp_date_ValueChanged);
            // 
            // roomView
            // 
            this.roomView.AllowUserToAddRows = false;
            this.roomView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.roomView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.roomView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.roomView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.学生コード,
            this.学生名,
            this.出勤機,
            this.登録コード,
            this.出退勤フラグ,
            this.出退勤時間,
            this.元日時,
            this.NewRowフラグ});
            this.roomView.ContextMenuStrip = this.contextMenuStrip1;
            this.roomView.Key = "";
            this.roomView.Location = new System.Drawing.Point(35, 70);
            this.roomView.Margin = new System.Windows.Forms.Padding(5);
            this.roomView.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.roomView.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("roomView.MergeColumnNames")));
            this.roomView.MergeRowIndex = ((System.Collections.Generic.Dictionary<int, int>)(resources.GetObject("roomView.MergeRowIndex")));
            this.roomView.MinimumSize = new System.Drawing.Size(1220, 0);
            this.roomView.Name = "roomView";
            this.roomView.NoLink = ((System.Collections.Generic.List<object>)(resources.GetObject("roomView.NoLink")));
            this.roomView.RowHeadersWidth = 51;
            this.roomView.RowTemplate.Height = 23;
            this.roomView.Size = new System.Drawing.Size(1220, 574);
            this.roomView.TabIndex = 9;
            this.roomView.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.roomView_CellBeginEdit);
            this.roomView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.roomView_CellEndEdit);
            this.roomView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.roomView_CellMouseDown);
            this.roomView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.roomView_CellValueChanged);
            // 
            // 学生コード
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.学生コード.DefaultCellStyle = dataGridViewCellStyle2;
            this.学生コード.HeaderText = "学生コード";
            this.学生コード.MaxInputLength = 500;
            this.学生コード.MinimumWidth = 6;
            this.学生コード.Name = "学生コード";
            this.学生コード.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.学生コード.Width = 99;
            // 
            // 学生名
            // 
            this.学生名.HeaderText = "学生名";
            this.学生名.MinimumWidth = 6;
            this.学生名.Name = "学生名";
            this.学生名.Width = 150;
            // 
            // 出勤機
            // 
            this.出勤機.HeaderText = "出勤機";
            this.出勤機.MinimumWidth = 6;
            this.出勤機.Name = "出勤機";
            this.出勤機.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.出勤機.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.出勤機.Width = 150;
            // 
            // 登録コード
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.登録コード.DefaultCellStyle = dataGridViewCellStyle3;
            this.登録コード.HeaderText = "登録コード";
            this.登録コード.MaxInputLength = 100;
            this.登録コード.MinimumWidth = 6;
            this.登録コード.Name = "登録コード";
            this.登録コード.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.登録コード.Width = 99;
            // 
            // 出退勤フラグ
            // 
            this.出退勤フラグ.HeaderText = "出☑退□勤フラグ";
            this.出退勤フラグ.MinimumWidth = 6;
            this.出退勤フラグ.Name = "出退勤フラグ";
            this.出退勤フラグ.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.出退勤フラグ.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.出退勤フラグ.Width = 113;
            // 
            // 出退勤時間
            // 
            this.出退勤時間.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle4.Format = "d";
            this.出退勤時間.DefaultCellStyle = dataGridViewCellStyle4;
            this.出退勤時間.HeaderText = "出退勤時間";
            this.出退勤時間.MaxInputLength = 19;
            this.出退勤時間.MinimumWidth = 6;
            this.出退勤時間.Name = "出退勤時間";
            this.出退勤時間.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // 元日時
            // 
            this.元日時.FillWeight = 1F;
            this.元日時.HeaderText = "元日時";
            this.元日時.MinimumWidth = 6;
            this.元日時.Name = "元日時";
            this.元日時.Visible = false;
            this.元日時.Width = 81;
            // 
            // NewRowフラグ
            // 
            this.NewRowフラグ.FillWeight = 1F;
            this.NewRowフラグ.HeaderText = "NewRowフラグ";
            this.NewRowフラグ.MinimumWidth = 6;
            this.NewRowフラグ.Name = "NewRowフラグ";
            this.NewRowフラグ.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.NewRowフラグ.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.NewRowフラグ.Visible = false;
            this.NewRowフラグ.Width = 123;
            // 
            // lbl_学生コード
            // 
            this.lbl_学生コード.AutoSize = true;
            this.lbl_学生コード.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_学生コード.Location = new System.Drawing.Point(348, 27);
            this.lbl_学生コード.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbl_学生コード.Name = "lbl_学生コード";
            this.lbl_学生コード.Size = new System.Drawing.Size(107, 28);
            this.lbl_学生コード.TabIndex = 129;
            this.lbl_学生コード.Text = "学生コード";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel1_count});
            this.statusStrip1.Location = new System.Drawing.Point(0, 668);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1341, 22);
            this.statusStrip1.TabIndex = 132;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel1_count
            // 
            this.toolStripStatusLabel1_count.Name = "toolStripStatusLabel1_count";
            this.toolStripStatusLabel1_count.Size = new System.Drawing.Size(0, 17);
            // 
            // btn_エラー修正
            // 
            this.btn_エラー修正.Font = new System.Drawing.Font("メイリオ", 10.8F, System.Drawing.FontStyle.Bold);
            this.btn_エラー修正.Location = new System.Drawing.Point(822, 17);
            this.btn_エラー修正.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_エラー修正.Name = "btn_エラー修正";
            this.btn_エラー修正.Size = new System.Drawing.Size(168, 49);
            this.btn_エラー修正.TabIndex = 192;
            this.btn_エラー修正.Text = "エラー修正";
            this.btn_エラー修正.UseVisualStyleBackColor = true;
            this.btn_エラー修正.Click += new System.EventHandler(this.btn_エラー修正_Click);
            // 
            // btn_出勤記録新規追加
            // 
            this.btn_出勤記録新規追加.Font = new System.Drawing.Font("メイリオ", 10.8F, System.Drawing.FontStyle.Bold);
            this.btn_出勤記録新規追加.Location = new System.Drawing.Point(998, 17);
            this.btn_出勤記録新規追加.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_出勤記録新規追加.Name = "btn_出勤記録新規追加";
            this.btn_出勤記録新規追加.Size = new System.Drawing.Size(235, 49);
            this.btn_出勤記録新規追加.TabIndex = 196;
            this.btn_出勤記録新規追加.Text = "出勤記録新規追加";
            this.btn_出勤記録新規追加.UseVisualStyleBackColor = true;
            this.btn_出勤記録新規追加.Click += new System.EventHandler(this.btn_出勤記録新規追加_Click);
            // 
            // cmb_学生コード
            // 
            this.cmb_学生コード.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_学生コード.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmb_学生コード.FormattingEnabled = true;
            this.cmb_学生コード.ImeMode = System.Windows.Forms.ImeMode.On;
            this.cmb_学生コード.Items.AddRange(new object[] {
            "ALL"});
            this.cmb_学生コード.Location = new System.Drawing.Point(510, 24);
            this.cmb_学生コード.Margin = new System.Windows.Forms.Padding(2);
            this.cmb_学生コード.Name = "cmb_学生コード";
            this.cmb_学生コード.Size = new System.Drawing.Size(218, 33);
            this.cmb_学生コード.TabIndex = 197;
            this.cmb_学生コード.SelectedIndexChanged += new System.EventHandler(this.cmb_学生コード_SelectedIndexChanged);
            // 
            // 出勤表エラー記録
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1341, 690);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.cmb_学生コード);
            this.Controls.Add(this.btn_出勤記録新規追加);
            this.Controls.Add(this.btn_エラー修正);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lbl_学生コード);
            this.Controls.Add(this.dtp_date);
            this.Controls.Add(this.lbl_date);
            this.Controls.Add(this.roomView);
            this.Controls.Add(this.dockPanel1);
            this.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.Name = "出勤表エラー記録";
            this.Text = "出勤表エラー記録";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.出勤表エラー記録_FormClosed);
            this.Load += new System.EventHandler(this.出勤表エラー記録_Load);
            this.SizeChanged += new System.EventHandler(this.出勤表エラー記録_SizeChanged);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.roomView)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public RowMergeView roomView;
        public DockPanel dockPanel1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem 削除ToolStripMenuItem;
        private ToolStripMenuItem 追加ToolStripMenuItem;
        private Label lbl_date;
        private DateTimePicker dtp_date;
        private Label lbl_学生コード;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabel1_count;
        private Button btn_エラー修正;
        private Button btn_出勤記録新規追加;
        private ComboBox cmb_学生コード;
        private DataGridViewTextBoxColumn 学生コード;
        private DataGridViewTextBoxColumn 学生名;
        private DataGridViewTextBoxColumn 出勤機;
        private DataGridViewTextBoxColumn 登録コード;
        private DataGridViewCheckBoxColumn 出退勤フラグ;
        private DataGridViewTextBoxColumn 出退勤時間;
        private DataGridViewTextBoxColumn 元日時;
        private DataGridViewCheckBoxColumn NewRowフラグ;
    }
}