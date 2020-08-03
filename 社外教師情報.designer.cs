namespace HL_塾管理
{
    partial class 社外教師情報
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(社外教師情報));
            this.lbl_言語 = new System.Windows.Forms.Label();
            this.lbl_郵便番号 = new System.Windows.Forms.Label();
            this.cmb_国籍 = new System.Windows.Forms.ComboBox();
            this.lbl_性別 = new System.Windows.Forms.Label();
            this.txt_携帯 = new System.Windows.Forms.TextBox();
            this.txt_ローマ字表記 = new System.Windows.Forms.TextBox();
            this.lbl_携帯 = new System.Windows.Forms.Label();
            this.lbl_ローマ字表記 = new System.Windows.Forms.Label();
            this.lbl_メール = new System.Windows.Forms.Label();
            this.cmb_性別 = new System.Windows.Forms.ComboBox();
            this.txt_郵便番号 = new System.Windows.Forms.TextBox();
            this.txt_MyNumber = new System.Windows.Forms.TextBox();
            this.txt_住所 = new System.Windows.Forms.TextBox();
            this.lbl_MyNumber = new System.Windows.Forms.Label();
            this.lbl_国籍 = new System.Windows.Forms.Label();
            this.dtp_退職日 = new System.Windows.Forms.DateTimePicker();
            this.txt_メール = new System.Windows.Forms.TextBox();
            this.lbl_退職日 = new System.Windows.Forms.Label();
            this.lbl_住所 = new System.Windows.Forms.Label();
            this.chk_退職 = new System.Windows.Forms.CheckBox();
            this.lbl_退職フラグ = new System.Windows.Forms.Label();
            this.dtp_入職日 = new System.Windows.Forms.DateTimePicker();
            this.lbl_入職日 = new System.Windows.Forms.Label();
            this.dtp_生年月日 = new System.Windows.Forms.DateTimePicker();
            this.lbl_生年月日 = new System.Windows.Forms.Label();
            this.txt_カタカナ = new System.Windows.Forms.TextBox();
            this.lbl_カタカナ = new System.Windows.Forms.Label();
            this.txt_名前 = new System.Windows.Forms.TextBox();
            this.lbl_名前 = new System.Windows.Forms.Label();
            this.lbl_教師コード = new System.Windows.Forms.Label();
            this.tsl_errMsg = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.cmb_言語 = new System.Windows.Forms.ComboBox();
            this.txt_所属会社 = new System.Windows.Forms.TextBox();
            this.lbl_所属会社 = new System.Windows.Forms.Label();
            this.lbl_コード = new System.Windows.Forms.Label();
            this.lbl_職務 = new System.Windows.Forms.Label();
            this.cmb_職務 = new System.Windows.Forms.ComboBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.lbl_郵便番号ハイフン = new System.Windows.Forms.Label();
            this.txt_郵便番号4 = new System.Windows.Forms.TextBox();
            this.lbl_郵便記号 = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_言語
            // 
            this.lbl_言語.AutoSize = true;
            this.lbl_言語.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_言語.Location = new System.Drawing.Point(78, 500);
            this.lbl_言語.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_言語.Name = "lbl_言語";
            this.lbl_言語.Size = new System.Drawing.Size(164, 25);
            this.lbl_言語.TabIndex = 13;
            this.lbl_言語.Text = "メイン言語　[必須]";
            this.lbl_言語.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl_必須_Paint);
            // 
            // lbl_郵便番号
            // 
            this.lbl_郵便番号.AutoSize = true;
            this.lbl_郵便番号.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_郵便番号.Location = new System.Drawing.Point(82, 300);
            this.lbl_郵便番号.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_郵便番号.Name = "lbl_郵便番号";
            this.lbl_郵便番号.Size = new System.Drawing.Size(147, 25);
            this.lbl_郵便番号.TabIndex = 8;
            this.lbl_郵便番号.Text = "郵便番号　[必須]";
            this.lbl_郵便番号.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl_必須_Paint);
            // 
            // cmb_国籍
            // 
            this.cmb_国籍.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_国籍.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmb_国籍.FormattingEnabled = true;
            this.cmb_国籍.ImeMode = System.Windows.Forms.ImeMode.On;
            this.cmb_国籍.Items.AddRange(new object[] {
            "日本籍",
            "中国籍",
            "アメリカ籍",
            "韓国籍",
            "台湾籍",
            "インド籍",
            "ベトナム籍",
            "メキシコ籍"});
            this.cmb_国籍.Location = new System.Drawing.Point(325, 428);
            this.cmb_国籍.Margin = new System.Windows.Forms.Padding(2);
            this.cmb_国籍.Name = "cmb_国籍";
            this.cmb_国籍.Size = new System.Drawing.Size(186, 33);
            this.cmb_国籍.TabIndex = 13;
            // 
            // lbl_性別
            // 
            this.lbl_性別.AutoSize = true;
            this.lbl_性別.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_性別.Location = new System.Drawing.Point(82, 167);
            this.lbl_性別.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_性別.Name = "lbl_性別";
            this.lbl_性別.Size = new System.Drawing.Size(113, 25);
            this.lbl_性別.TabIndex = 5;
            this.lbl_性別.Text = "性別　[必須]";
            this.lbl_性別.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl_必須_Paint);
            // 
            // txt_携帯
            // 
            this.txt_携帯.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_携帯.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txt_携帯.Location = new System.Drawing.Point(325, 362);
            this.txt_携帯.Margin = new System.Windows.Forms.Padding(2);
            this.txt_携帯.MaxLength = 13;
            this.txt_携帯.Name = "txt_携帯";
            this.txt_携帯.Size = new System.Drawing.Size(186, 33);
            this.txt_携帯.TabIndex = 11;
            // 
            // txt_ローマ字表記
            // 
            this.txt_ローマ字表記.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_ローマ字表記.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txt_ローマ字表記.Location = new System.Drawing.Point(1302, 97);
            this.txt_ローマ字表記.Margin = new System.Windows.Forms.Padding(2);
            this.txt_ローマ字表記.MaxLength = 50;
            this.txt_ローマ字表記.Name = "txt_ローマ字表記";
            this.txt_ローマ字表記.Size = new System.Drawing.Size(186, 33);
            this.txt_ローマ字表記.TabIndex = 3;
            // 
            // lbl_携帯
            // 
            this.lbl_携帯.AutoSize = true;
            this.lbl_携帯.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_携帯.Location = new System.Drawing.Point(82, 370);
            this.lbl_携帯.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_携帯.Name = "lbl_携帯";
            this.lbl_携帯.Size = new System.Drawing.Size(113, 25);
            this.lbl_携帯.TabIndex = 10;
            this.lbl_携帯.Text = "携帯　[必須]";
            this.lbl_携帯.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl_必須_Paint);
            // 
            // lbl_ローマ字表記
            // 
            this.lbl_ローマ字表記.AutoSize = true;
            this.lbl_ローマ字表記.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_ローマ字表記.Location = new System.Drawing.Point(1063, 100);
            this.lbl_ローマ字表記.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_ローマ字表記.Name = "lbl_ローマ字表記";
            this.lbl_ローマ字表記.Size = new System.Drawing.Size(181, 25);
            this.lbl_ローマ字表記.TabIndex = 4;
            this.lbl_ローマ字表記.Text = "ローマ字表記　[必須]";
            this.lbl_ローマ字表記.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl_必須_Paint);
            // 
            // lbl_メール
            // 
            this.lbl_メール.AutoSize = true;
            this.lbl_メール.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_メール.Location = new System.Drawing.Point(607, 370);
            this.lbl_メール.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_メール.Name = "lbl_メール";
            this.lbl_メール.Size = new System.Drawing.Size(130, 25);
            this.lbl_メール.TabIndex = 11;
            this.lbl_メール.Text = "メール　[必須]";
            this.lbl_メール.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl_必須_Paint);
            // 
            // cmb_性別
            // 
            this.cmb_性別.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_性別.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmb_性別.FormattingEnabled = true;
            this.cmb_性別.Items.AddRange(new object[] {
            "男",
            "女"});
            this.cmb_性別.Location = new System.Drawing.Point(325, 162);
            this.cmb_性別.Margin = new System.Windows.Forms.Padding(2);
            this.cmb_性別.Name = "cmb_性別";
            this.cmb_性別.Size = new System.Drawing.Size(62, 33);
            this.cmb_性別.TabIndex = 4;
            // 
            // txt_郵便番号
            // 
            this.txt_郵便番号.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_郵便番号.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txt_郵便番号.Location = new System.Drawing.Point(368, 297);
            this.txt_郵便番号.Margin = new System.Windows.Forms.Padding(2);
            this.txt_郵便番号.MaxLength = 3;
            this.txt_郵便番号.Name = "txt_郵便番号";
            this.txt_郵便番号.Size = new System.Drawing.Size(62, 33);
            this.txt_郵便番号.TabIndex = 8;
            // 
            // txt_MyNumber
            // 
            this.txt_MyNumber.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_MyNumber.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txt_MyNumber.Location = new System.Drawing.Point(325, 230);
            this.txt_MyNumber.Margin = new System.Windows.Forms.Padding(2);
            this.txt_MyNumber.MaxLength = 12;
            this.txt_MyNumber.Name = "txt_MyNumber";
            this.txt_MyNumber.Size = new System.Drawing.Size(186, 33);
            this.txt_MyNumber.TabIndex = 6;
            // 
            // txt_住所
            // 
            this.txt_住所.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_住所.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txt_住所.Location = new System.Drawing.Point(806, 297);
            this.txt_住所.Margin = new System.Windows.Forms.Padding(2);
            this.txt_住所.MaxLength = 100;
            this.txt_住所.Name = "txt_住所";
            this.txt_住所.Size = new System.Drawing.Size(544, 33);
            this.txt_住所.TabIndex = 10;
            // 
            // lbl_MyNumber
            // 
            this.lbl_MyNumber.AutoSize = true;
            this.lbl_MyNumber.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_MyNumber.Location = new System.Drawing.Point(82, 233);
            this.lbl_MyNumber.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_MyNumber.Name = "lbl_MyNumber";
            this.lbl_MyNumber.Size = new System.Drawing.Size(177, 25);
            this.lbl_MyNumber.TabIndex = 7;
            this.lbl_MyNumber.Text = "MyNumber　[必須]";
            this.lbl_MyNumber.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl_必須_Paint);
            // 
            // lbl_国籍
            // 
            this.lbl_国籍.AutoSize = true;
            this.lbl_国籍.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_国籍.Location = new System.Drawing.Point(82, 432);
            this.lbl_国籍.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_国籍.Name = "lbl_国籍";
            this.lbl_国籍.Size = new System.Drawing.Size(113, 25);
            this.lbl_国籍.TabIndex = 12;
            this.lbl_国籍.Text = "国籍　[必須]";
            this.lbl_国籍.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl_必須_Paint);
            // 
            // dtp_退職日
            // 
            this.dtp_退職日.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dtp_退職日.Location = new System.Drawing.Point(326, 745);
            this.dtp_退職日.Margin = new System.Windows.Forms.Padding(2);
            this.dtp_退職日.Name = "dtp_退職日";
            this.dtp_退職日.Size = new System.Drawing.Size(186, 33);
            this.dtp_退職日.TabIndex = 18;
            this.dtp_退職日.Value = new System.DateTime(2020, 11, 30, 0, 0, 0, 0);
            this.dtp_退職日.Visible = false;
            // 
            // txt_メール
            // 
            this.txt_メール.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_メール.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txt_メール.Location = new System.Drawing.Point(806, 367);
            this.txt_メール.Margin = new System.Windows.Forms.Padding(2);
            this.txt_メール.MaxLength = 200;
            this.txt_メール.Name = "txt_メール";
            this.txt_メール.Size = new System.Drawing.Size(364, 33);
            this.txt_メール.TabIndex = 12;
            // 
            // lbl_退職日
            // 
            this.lbl_退職日.AutoSize = true;
            this.lbl_退職日.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_退職日.Location = new System.Drawing.Point(82, 753);
            this.lbl_退職日.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_退職日.Name = "lbl_退職日";
            this.lbl_退職日.Size = new System.Drawing.Size(130, 25);
            this.lbl_退職日.TabIndex = 16;
            this.lbl_退職日.Text = "退塾日　[必須]";
            this.lbl_退職日.Visible = false;
            this.lbl_退職日.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl_必須_Paint);
            // 
            // lbl_住所
            // 
            this.lbl_住所.AutoSize = true;
            this.lbl_住所.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_住所.Location = new System.Drawing.Point(607, 300);
            this.lbl_住所.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_住所.Name = "lbl_住所";
            this.lbl_住所.Size = new System.Drawing.Size(113, 25);
            this.lbl_住所.TabIndex = 10;
            this.lbl_住所.Text = "住所　[必須]";
            this.lbl_住所.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl_必須_Paint);
            // 
            // chk_退職
            // 
            this.chk_退職.AutoSize = true;
            this.chk_退職.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chk_退職.Location = new System.Drawing.Point(326, 696);
            this.chk_退職.Margin = new System.Windows.Forms.Padding(2);
            this.chk_退職.Name = "chk_退職";
            this.chk_退職.Size = new System.Drawing.Size(18, 17);
            this.chk_退職.TabIndex = 17;
            this.chk_退職.UseVisualStyleBackColor = true;
            this.chk_退職.Visible = false;
            this.chk_退職.CheckedChanged += new System.EventHandler(this.chk_退職_CheckedChanged);
            // 
            // lbl_退職フラグ
            // 
            this.lbl_退職フラグ.AutoSize = true;
            this.lbl_退職フラグ.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_退職フラグ.Location = new System.Drawing.Point(82, 691);
            this.lbl_退職フラグ.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_退職フラグ.Name = "lbl_退職フラグ";
            this.lbl_退職フラグ.Size = new System.Drawing.Size(46, 25);
            this.lbl_退職フラグ.TabIndex = 15;
            this.lbl_退職フラグ.Text = "退塾";
            this.lbl_退職フラグ.Visible = false;
            this.lbl_退職フラグ.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl_必須_Paint);
            // 
            // dtp_入職日
            // 
            this.dtp_入職日.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dtp_入職日.Location = new System.Drawing.Point(326, 635);
            this.dtp_入職日.Margin = new System.Windows.Forms.Padding(2);
            this.dtp_入職日.Name = "dtp_入職日";
            this.dtp_入職日.Size = new System.Drawing.Size(186, 33);
            this.dtp_入職日.TabIndex = 16;
            // 
            // lbl_入職日
            // 
            this.lbl_入職日.AutoSize = true;
            this.lbl_入職日.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_入職日.Location = new System.Drawing.Point(82, 635);
            this.lbl_入職日.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_入職日.Name = "lbl_入職日";
            this.lbl_入職日.Size = new System.Drawing.Size(130, 25);
            this.lbl_入職日.TabIndex = 14;
            this.lbl_入職日.Text = "入塾日　[必須]";
            this.lbl_入職日.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl_必須_Paint);
            // 
            // dtp_生年月日
            // 
            this.dtp_生年月日.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dtp_生年月日.Location = new System.Drawing.Point(806, 158);
            this.dtp_生年月日.Margin = new System.Windows.Forms.Padding(2);
            this.dtp_生年月日.Name = "dtp_生年月日";
            this.dtp_生年月日.Size = new System.Drawing.Size(186, 33);
            this.dtp_生年月日.TabIndex = 5;
            // 
            // lbl_生年月日
            // 
            this.lbl_生年月日.AutoSize = true;
            this.lbl_生年月日.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_生年月日.Location = new System.Drawing.Point(607, 167);
            this.lbl_生年月日.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_生年月日.Name = "lbl_生年月日";
            this.lbl_生年月日.Size = new System.Drawing.Size(147, 25);
            this.lbl_生年月日.TabIndex = 6;
            this.lbl_生年月日.Text = "生年月日　[必須]";
            this.lbl_生年月日.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl_必須_Paint);
            // 
            // txt_カタカナ
            // 
            this.txt_カタカナ.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_カタカナ.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.txt_カタカナ.Location = new System.Drawing.Point(806, 97);
            this.txt_カタカナ.Margin = new System.Windows.Forms.Padding(2);
            this.txt_カタカナ.MaxLength = 50;
            this.txt_カタカナ.Name = "txt_カタカナ";
            this.txt_カタカナ.Size = new System.Drawing.Size(186, 33);
            this.txt_カタカナ.TabIndex = 2;
            // 
            // lbl_カタカナ
            // 
            this.lbl_カタカナ.AutoSize = true;
            this.lbl_カタカナ.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_カタカナ.Location = new System.Drawing.Point(607, 100);
            this.lbl_カタカナ.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_カタカナ.Name = "lbl_カタカナ";
            this.lbl_カタカナ.Size = new System.Drawing.Size(147, 25);
            this.lbl_カタカナ.TabIndex = 3;
            this.lbl_カタカナ.Text = "カタカナ　[必須]";
            this.lbl_カタカナ.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl_必須_Paint);
            // 
            // txt_名前
            // 
            this.txt_名前.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_名前.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txt_名前.Location = new System.Drawing.Point(325, 97);
            this.txt_名前.Margin = new System.Windows.Forms.Padding(2);
            this.txt_名前.MaxLength = 100;
            this.txt_名前.Name = "txt_名前";
            this.txt_名前.Size = new System.Drawing.Size(186, 33);
            this.txt_名前.TabIndex = 1;
            // 
            // lbl_名前
            // 
            this.lbl_名前.AutoSize = true;
            this.lbl_名前.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_名前.Location = new System.Drawing.Point(82, 100);
            this.lbl_名前.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_名前.Name = "lbl_名前";
            this.lbl_名前.Size = new System.Drawing.Size(113, 25);
            this.lbl_名前.TabIndex = 2;
            this.lbl_名前.Text = "名前　[必須]";
            this.lbl_名前.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl_必須_Paint);
            // 
            // lbl_教師コード
            // 
            this.lbl_教師コード.AutoSize = true;
            this.lbl_教師コード.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_教師コード.Location = new System.Drawing.Point(82, 38);
            this.lbl_教師コード.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_教師コード.Name = "lbl_教師コード";
            this.lbl_教師コード.Size = new System.Drawing.Size(97, 25);
            this.lbl_教師コード.TabIndex = 1;
            this.lbl_教師コード.Text = "教師コード";
            // 
            // tsl_errMsg
            // 
            this.tsl_errMsg.Name = "tsl_errMsg";
            this.tsl_errMsg.Size = new System.Drawing.Size(0, 17);
            this.tsl_errMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsl_errMsg});
            this.statusStrip1.Location = new System.Drawing.Point(0, 866);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 10, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1528, 22);
            this.statusStrip1.TabIndex = 153;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // cmb_言語
            // 
            this.cmb_言語.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_言語.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmb_言語.FormattingEnabled = true;
            this.cmb_言語.ImeMode = System.Windows.Forms.ImeMode.On;
            this.cmb_言語.Items.AddRange(new object[] {
            ".Net",
            "iOS",
            "php",
            "pyhton",
            "Java",
            "Android",
            "Salesforce"});
            this.cmb_言語.Location = new System.Drawing.Point(325, 497);
            this.cmb_言語.Margin = new System.Windows.Forms.Padding(2);
            this.cmb_言語.Name = "cmb_言語";
            this.cmb_言語.Size = new System.Drawing.Size(186, 33);
            this.cmb_言語.TabIndex = 14;
            // 
            // txt_所属会社
            // 
            this.txt_所属会社.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_所属会社.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.txt_所属会社.Location = new System.Drawing.Point(806, 230);
            this.txt_所属会社.Margin = new System.Windows.Forms.Padding(2);
            this.txt_所属会社.MaxLength = 100;
            this.txt_所属会社.Name = "txt_所属会社";
            this.txt_所属会社.Size = new System.Drawing.Size(364, 33);
            this.txt_所属会社.TabIndex = 7;
            // 
            // lbl_所属会社
            // 
            this.lbl_所属会社.AutoSize = true;
            this.lbl_所属会社.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_所属会社.Location = new System.Drawing.Point(607, 233);
            this.lbl_所属会社.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_所属会社.Name = "lbl_所属会社";
            this.lbl_所属会社.Size = new System.Drawing.Size(147, 25);
            this.lbl_所属会社.TabIndex = 155;
            this.lbl_所属会社.Text = "所属会社　[必須]";
            this.lbl_所属会社.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl_必須_Paint);
            // 
            // lbl_コード
            // 
            this.lbl_コード.AutoSize = true;
            this.lbl_コード.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_コード.Location = new System.Drawing.Point(321, 38);
            this.lbl_コード.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_コード.Name = "lbl_コード";
            this.lbl_コード.Size = new System.Drawing.Size(0, 25);
            this.lbl_コード.TabIndex = 158;
            // 
            // lbl_職務
            // 
            this.lbl_職務.AutoSize = true;
            this.lbl_職務.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_職務.Location = new System.Drawing.Point(82, 568);
            this.lbl_職務.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_職務.Name = "lbl_職務";
            this.lbl_職務.Size = new System.Drawing.Size(113, 25);
            this.lbl_職務.TabIndex = 160;
            this.lbl_職務.Text = "職務　[必須]";
            this.lbl_職務.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl_必須_Paint);
            // 
            // cmb_職務
            // 
            this.cmb_職務.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_職務.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmb_職務.FormattingEnabled = true;
            this.cmb_職務.Items.AddRange(new object[] {
            "管理者",
            "一般ユーザ"});
            this.cmb_職務.Location = new System.Drawing.Point(325, 565);
            this.cmb_職務.Margin = new System.Windows.Forms.Padding(2);
            this.cmb_職務.Name = "cmb_職務";
            this.cmb_職務.Size = new System.Drawing.Size(186, 33);
            this.cmb_職務.TabIndex = 15;
            // 
            // btn_save
            // 
            this.btn_save.Font = new System.Drawing.Font("メイリオ", 10.8F, System.Drawing.FontStyle.Bold);
            this.btn_save.Location = new System.Drawing.Point(522, 818);
            this.btn_save.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(186, 48);
            this.btn_save.TabIndex = 166;
            this.btn_save.Text = "登録";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // lbl_郵便番号ハイフン
            // 
            this.lbl_郵便番号ハイフン.AutoSize = true;
            this.lbl_郵便番号ハイフン.Location = new System.Drawing.Point(435, 305);
            this.lbl_郵便番号ハイフン.Name = "lbl_郵便番号ハイフン";
            this.lbl_郵便番号ハイフン.Size = new System.Drawing.Size(22, 15);
            this.lbl_郵便番号ハイフン.TabIndex = 167;
            this.lbl_郵便番号ハイフン.Text = "－";
            // 
            // txt_郵便番号4
            // 
            this.txt_郵便番号4.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_郵便番号4.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txt_郵便番号4.Location = new System.Drawing.Point(462, 297);
            this.txt_郵便番号4.Margin = new System.Windows.Forms.Padding(2);
            this.txt_郵便番号4.MaxLength = 4;
            this.txt_郵便番号4.Name = "txt_郵便番号4";
            this.txt_郵便番号4.Size = new System.Drawing.Size(92, 33);
            this.txt_郵便番号4.TabIndex = 9;
            // 
            // lbl_郵便記号
            // 
            this.lbl_郵便記号.AutoSize = true;
            this.lbl_郵便記号.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_郵便記号.Location = new System.Drawing.Point(323, 298);
            this.lbl_郵便記号.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_郵便記号.Name = "lbl_郵便記号";
            this.lbl_郵便記号.Size = new System.Drawing.Size(31, 28);
            this.lbl_郵便記号.TabIndex = 168;
            this.lbl_郵便記号.Text = "〒";
            // 
            // 社外教師情報
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1549, 879);
            this.Controls.Add(this.lbl_郵便記号);
            this.Controls.Add(this.txt_郵便番号4);
            this.Controls.Add(this.lbl_郵便番号ハイフン);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.lbl_職務);
            this.Controls.Add(this.cmb_職務);
            this.Controls.Add(this.lbl_コード);
            this.Controls.Add(this.txt_所属会社);
            this.Controls.Add(this.lbl_所属会社);
            this.Controls.Add(this.cmb_言語);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lbl_言語);
            this.Controls.Add(this.lbl_郵便番号);
            this.Controls.Add(this.cmb_国籍);
            this.Controls.Add(this.lbl_性別);
            this.Controls.Add(this.txt_携帯);
            this.Controls.Add(this.txt_ローマ字表記);
            this.Controls.Add(this.lbl_携帯);
            this.Controls.Add(this.lbl_ローマ字表記);
            this.Controls.Add(this.lbl_メール);
            this.Controls.Add(this.cmb_性別);
            this.Controls.Add(this.txt_郵便番号);
            this.Controls.Add(this.txt_MyNumber);
            this.Controls.Add(this.txt_住所);
            this.Controls.Add(this.lbl_MyNumber);
            this.Controls.Add(this.lbl_国籍);
            this.Controls.Add(this.dtp_退職日);
            this.Controls.Add(this.txt_メール);
            this.Controls.Add(this.lbl_退職日);
            this.Controls.Add(this.lbl_住所);
            this.Controls.Add(this.chk_退職);
            this.Controls.Add(this.lbl_退職フラグ);
            this.Controls.Add(this.dtp_入職日);
            this.Controls.Add(this.lbl_入職日);
            this.Controls.Add(this.dtp_生年月日);
            this.Controls.Add(this.lbl_生年月日);
            this.Controls.Add(this.txt_カタカナ);
            this.Controls.Add(this.lbl_カタカナ);
            this.Controls.Add(this.txt_名前);
            this.Controls.Add(this.lbl_名前);
            this.Controls.Add(this.lbl_教師コード);
            this.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "社外教師情報";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "19";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.社外教師情報_FormClosed);
            this.Load += new System.EventHandler(this.社外教師情報_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbl_言語;
        private System.Windows.Forms.Label lbl_郵便番号;
        private System.Windows.Forms.ComboBox cmb_国籍;
        private System.Windows.Forms.Label lbl_性別;
        private System.Windows.Forms.TextBox txt_携帯;
        private System.Windows.Forms.TextBox txt_ローマ字表記;
        private System.Windows.Forms.Label lbl_携帯;
        private System.Windows.Forms.Label lbl_ローマ字表記;
        private System.Windows.Forms.Label lbl_メール;
        private System.Windows.Forms.ComboBox cmb_性別;
        private System.Windows.Forms.TextBox txt_郵便番号;
        private System.Windows.Forms.TextBox txt_MyNumber;
        private System.Windows.Forms.TextBox txt_住所;
        private System.Windows.Forms.Label lbl_MyNumber;
        private System.Windows.Forms.Label lbl_国籍;
        private System.Windows.Forms.DateTimePicker dtp_退職日;
        private System.Windows.Forms.TextBox txt_メール;
        private System.Windows.Forms.Label lbl_退職日;
        private System.Windows.Forms.Label lbl_住所;
        private System.Windows.Forms.CheckBox chk_退職;
        private System.Windows.Forms.Label lbl_退職フラグ;
        private System.Windows.Forms.DateTimePicker dtp_入職日;
        private System.Windows.Forms.Label lbl_入職日;
        private System.Windows.Forms.DateTimePicker dtp_生年月日;
        private System.Windows.Forms.Label lbl_生年月日;
        private System.Windows.Forms.TextBox txt_カタカナ;
        private System.Windows.Forms.Label lbl_カタカナ;
        private System.Windows.Forms.TextBox txt_名前;
        private System.Windows.Forms.Label lbl_名前;
        private System.Windows.Forms.Label lbl_教師コード;
        private System.Windows.Forms.ToolStripStatusLabel tsl_errMsg;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ComboBox cmb_言語;
        private System.Windows.Forms.TextBox txt_所属会社;
        private System.Windows.Forms.Label lbl_所属会社;
        private System.Windows.Forms.Label lbl_コード;
        private System.Windows.Forms.Label lbl_職務;
        private System.Windows.Forms.ComboBox cmb_職務;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Label lbl_郵便番号ハイフン;
        private System.Windows.Forms.TextBox txt_郵便番号4;
        private System.Windows.Forms.Label lbl_郵便記号;
    }
}