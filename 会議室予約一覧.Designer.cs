using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace HL_塾管理
{
    partial class 会議室予約一覧
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(会議室予約一覧));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lbl_roomName = new System.Windows.Forms.Label();
            this.lbl_date = new System.Windows.Forms.Label();
            this.dtp_date_start = new System.Windows.Forms.DateTimePicker();
            this.cmb_roomName = new System.Windows.Forms.ComboBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.新規ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.変更ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.削除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dockPanel1 = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1_count = new System.Windows.Forms.ToolStripStatusLabel();
            this.dtp_date_end = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.roomView = new RowMergeView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.会議室名 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.参加人数 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.予約者 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.日付 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.開始時間 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.終了時間 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.タイトル = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.参加者 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.備考 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.予約者_mail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.roomView)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_roomName
            // 
            this.lbl_roomName.AutoSize = true;
            this.lbl_roomName.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_roomName.Location = new System.Drawing.Point(22, 9);
            this.lbl_roomName.Name = "lbl_roomName";
            this.lbl_roomName.Size = new System.Drawing.Size(70, 23);
            this.lbl_roomName.TabIndex = 88;
            this.lbl_roomName.Text = "会議室名";
            // 
            // lbl_date
            // 
            this.lbl_date.AutoSize = true;
            this.lbl_date.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_date.Location = new System.Drawing.Point(274, 9);
            this.lbl_date.Name = "lbl_date";
            this.lbl_date.Size = new System.Drawing.Size(40, 23);
            this.lbl_date.TabIndex = 99;
            this.lbl_date.Text = "日付";
            // 
            // dtp_date_start
            // 
            this.dtp_date_start.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dtp_date_start.Location = new System.Drawing.Point(333, 10);
            this.dtp_date_start.Name = "dtp_date_start";
            this.dtp_date_start.Size = new System.Drawing.Size(160, 22);
            this.dtp_date_start.TabIndex = 90;
            this.dtp_date_start.ValueChanged += new System.EventHandler(this.dtp_date_ValueChanged);
            // 
            // cmb_roomName
            // 
            this.cmb_roomName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_roomName.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmb_roomName.FormattingEnabled = true;
            this.cmb_roomName.Location = new System.Drawing.Point(109, 9);
            this.cmb_roomName.Name = "cmb_roomName";
            this.cmb_roomName.Size = new System.Drawing.Size(121, 26);
            this.cmb_roomName.TabIndex = 89;
            this.cmb_roomName.TextChanged += new System.EventHandler(this.cmb_roomName_TextChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新規ToolStripMenuItem,
            this.変更ToolStripMenuItem,
            this.削除ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(99, 70);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // 新規ToolStripMenuItem
            // 
            this.新規ToolStripMenuItem.Name = "新規ToolStripMenuItem";
            this.新規ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.新規ToolStripMenuItem.Text = "新規";
            this.新規ToolStripMenuItem.Click += new System.EventHandler(this.新規ToolStripMenuItem_Click);
            // 
            // 変更ToolStripMenuItem
            // 
            this.変更ToolStripMenuItem.Name = "変更ToolStripMenuItem";
            this.変更ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.変更ToolStripMenuItem.Text = "変更";
            this.変更ToolStripMenuItem.Click += new System.EventHandler(this.変更ToolStripMenuItem_Click);
            // 
            // 削除ToolStripMenuItem
            // 
            this.削除ToolStripMenuItem.Name = "削除ToolStripMenuItem";
            this.削除ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.削除ToolStripMenuItem.Text = "削除";
            this.削除ToolStripMenuItem.Click += new System.EventHandler(this.削除ToolStripMenuItem_Click);
            // 
            // dockPanel1
            // 
            this.dockPanel1.ActiveAutoHideContent = null;
            this.dockPanel1.DockBackColor = System.Drawing.SystemColors.Control;
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Size = new System.Drawing.Size(200, 100);
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
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1_count});
            this.statusStrip1.Location = new System.Drawing.Point(0, 588);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1233, 22);
            this.statusStrip1.TabIndex = 122;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1_count
            // 
            this.toolStripStatusLabel1_count.Name = "toolStripStatusLabel1_count";
            this.toolStripStatusLabel1_count.Size = new System.Drawing.Size(0, 17);
            // 
            // dtp_date_end
            // 
            this.dtp_date_end.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dtp_date_end.Location = new System.Drawing.Point(557, 10);
            this.dtp_date_end.Name = "dtp_date_end";
            this.dtp_date_end.Size = new System.Drawing.Size(160, 22);
            this.dtp_date_end.TabIndex = 124;
            this.dtp_date_end.ValueChanged += new System.EventHandler(this.dtp_date_end_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(513, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 23);
            this.label1.TabIndex = 126;
            this.label1.Text = "～";
            // 
            // roomView
            // 
            this.roomView.AllowUserToAddRows = false;
            this.roomView.AllowUserToResizeColumns = false;
            this.roomView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.roomView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.roomView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.roomView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.会議室名,
            this.参加人数,
            this.予約者,
            this.日付,
            this.開始時間,
            this.終了時間,
            this.タイトル,
            this.参加者,
            this.備考,
            this.予約者_mail});
            this.roomView.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.roomView.DefaultCellStyle = dataGridViewCellStyle11;
            this.roomView.Key = "";
            this.roomView.Location = new System.Drawing.Point(26, 41);
            this.roomView.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.roomView.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("roomView.MergeColumnNames")));
            this.roomView.MergeRowIndex = ((System.Collections.Generic.Dictionary<int, int>)(resources.GetObject("roomView.MergeRowIndex")));
            this.roomView.Name = "roomView";
            this.roomView.NoLink = ((System.Collections.Generic.List<object>)(resources.GetObject("roomView.NoLink")));
            this.roomView.RowTemplate.Height = 23;
            this.roomView.Size = new System.Drawing.Size(1184, 498);
            this.roomView.TabIndex = 9;
            this.roomView.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.roomView_CellBeginEdit);
            this.roomView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.roomView_CellEndEdit);
            this.roomView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.roomView_CellMouseDown);
            this.roomView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.roomView_CellValueChanged);
            this.roomView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.roomView_DataError);
            this.roomView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.roomView_EditingControlShowing);
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.Visible = false;
            this.ID.Width = 5;
            // 
            // 会議室名
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.会議室名.DefaultCellStyle = dataGridViewCellStyle2;
            this.会議室名.HeaderText = "会議室名";
            this.会議室名.Name = "会議室名";
            this.会議室名.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.会議室名.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // 参加人数
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.参加人数.DefaultCellStyle = dataGridViewCellStyle3;
            this.参加人数.HeaderText = "参加人数";
            this.参加人数.Name = "参加人数";
            this.参加人数.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.参加人数.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.参加人数.Width = 80;
            // 
            // 予約者
            // 
            dataGridViewCellStyle4.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.予約者.DefaultCellStyle = dataGridViewCellStyle4;
            this.予約者.HeaderText = "予約者";
            this.予約者.MaxInputLength = 100;
            this.予約者.Name = "予約者";
            this.予約者.ReadOnly = true;
            // 
            // 日付
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle5.Format = "d";
            this.日付.DefaultCellStyle = dataGridViewCellStyle5;
            this.日付.HeaderText = "日付";
            this.日付.MaxInputLength = 10;
            this.日付.Name = "日付";
            this.日付.Width = 80;
            // 
            // 開始時間
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle6.Format = "t";
            this.開始時間.DefaultCellStyle = dataGridViewCellStyle6;
            this.開始時間.HeaderText = "開始時間";
            this.開始時間.MaxInputLength = 5;
            this.開始時間.Name = "開始時間";
            this.開始時間.Width = 80;
            // 
            // 終了時間
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle7.Format = "t";
            this.終了時間.DefaultCellStyle = dataGridViewCellStyle7;
            this.終了時間.HeaderText = "終了時間";
            this.終了時間.MaxInputLength = 5;
            this.終了時間.Name = "終了時間";
            this.終了時間.Width = 80;
            // 
            // タイトル
            // 
            dataGridViewCellStyle8.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.タイトル.DefaultCellStyle = dataGridViewCellStyle8;
            this.タイトル.HeaderText = "タイトル";
            this.タイトル.MaxInputLength = 100;
            this.タイトル.Name = "タイトル";
            this.タイトル.Width = 227;
            // 
            // 参加者
            // 
            dataGridViewCellStyle9.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.参加者.DefaultCellStyle = dataGridViewCellStyle9;
            this.参加者.HeaderText = "参加者";
            this.参加者.MaxInputLength = 500;
            this.参加者.Name = "参加者";
            this.参加者.Width = 210;
            // 
            // 備考
            // 
            this.備考.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.備考.DefaultCellStyle = dataGridViewCellStyle10;
            this.備考.HeaderText = "備考";
            this.備考.MaxInputLength = 500;
            this.備考.Name = "備考";
            // 
            // 予約者_mail
            // 
            this.予約者_mail.HeaderText = "予約者_mail";
            this.予約者_mail.Name = "予約者_mail";
            this.予約者_mail.Visible = false;
            // 
            // 会議室予約一覧
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1233, 610);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtp_date_end);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.cmb_roomName);
            this.Controls.Add(this.dtp_date_start);
            this.Controls.Add(this.lbl_date);
            this.Controls.Add(this.lbl_roomName);
            this.Controls.Add(this.roomView);
            this.Controls.Add(this.dockPanel1);
            this.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1249, 648);
            this.Name = "会議室予約一覧";
            this.Text = "会議室　予約一覧";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.会議室予約一覧_FormClosed);
            this.Load += new System.EventHandler(this.会議室予約一覧_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.roomView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public RowMergeView roomView;
        private System.Windows.Forms.Label lbl_roomName;
        private System.Windows.Forms.Label lbl_date;
        private System.Windows.Forms.DateTimePicker dtp_date_start;
        private System.Windows.Forms.ComboBox cmb_roomName;
        public  DockPanel dockPanel1;
        private StatusStrip statusStrip1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem 削除ToolStripMenuItem;
        private ToolStripStatusLabel toolStripStatusLabel1_count;
        private ToolStripMenuItem 新規ToolStripMenuItem;
        private ToolStripMenuItem 変更ToolStripMenuItem;
        private DateTimePicker dtp_date_end;
        private Label label1;
        private DataGridViewTextBoxColumn ID;
        private DataGridViewComboBoxColumn 会議室名;
        private DataGridViewComboBoxColumn 参加人数;
        private DataGridViewTextBoxColumn 予約者;
        private DataGridViewTextBoxColumn 日付;
        private DataGridViewTextBoxColumn 開始時間;
        private DataGridViewTextBoxColumn 終了時間;
        private DataGridViewTextBoxColumn タイトル;
        private DataGridViewTextBoxColumn 参加者;
        private DataGridViewTextBoxColumn 備考;
        private DataGridViewTextBoxColumn 予約者_mail;
    }
}