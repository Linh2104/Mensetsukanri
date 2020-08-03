using System;
using System.Data;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace HL_塾管理
{
    public partial class 学生情報 : DockContent
    {
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        //データベース接続情報
        private string connectionString = ComClass.connectionString;
        //更新フラグ
        public string isUpdate = "new";

        //画面項目
        public string code_学生コード = string.Empty;
        public string name_学生 = string.Empty;
        private string katakana = string.Empty;
        private string romaji = string.Empty;
        public DateTime date_生年月日;
        private string myNumber = string.Empty;
        public string sex = string.Empty;
        private DateTime date_入塾日;

        private string code_郵便 = string.Empty;
        private string code_郵便1 = string.Empty;
        private string code_郵便2 = string.Empty;
        private string address = string.Empty;
        public string telPhone = string.Empty;
        public string mailAddress = string.Empty;
        public string country = string.Empty;
        public string name_学校 = string.Empty;
        private string code_クラスコード_Before = "";
        private List<string> list_学生 = new List<string>();
        private string code_出勤機コード = string.Empty;

        public bool flag_応募者入塾 = false;
        public string 社員種別 = string.Empty;
        public string id_応募者 = null;
        public string code_社員 = null;

        //検索データテーブル
        private DataTable dtInfo = null;

        //エラーメッセージ
        private Dictionary<string, string> errmsg = new Dictionary<string, string>();

        public 学生情報()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 画面表示
        /// </summary>
        private void Page_load(object sender, EventArgs e)
        {
            ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "";

            //画面区分
            btn_save.Text = isUpdate == "new" ? "登録" : "更新";
            lbl_研修社員.Visible = 社員種別 == "研修";
            if (isUpdate == "new")
            {
                //新規の学生コード　自動設定
                SetStudentCode();

                cmb_性別.SelectedIndex = 0;
                cmb_国籍.SelectedIndex = 1;

                if (flag_応募者入塾)
                {
                    txt_名前.Text = name_学生;
                    dtp_生年月日.Value = date_生年月日;
                    cmb_性別.Text = sex;
                    cmb_国籍.Text = country;
                    txt_メール.Text = mailAddress;
                    txt_携帯.Text = telPhone;
                    txt_学校名.Text = name_学校;

                    txt_名前.Enabled = false;
                    lbl_名前.Text = lbl_名前.Text.Replace("[必須]", "");
                    dtp_生年月日.Enabled = false;
                    lbl_生年月日.Text = lbl_生年月日.Text.Replace("[必須]", "");
                    cmb_性別.Enabled = false;
                    lbl_性別.Text = lbl_性別.Text.Replace("[必須]", "");
                    cmb_国籍.Enabled = false;
                    lbl_国籍.Text = lbl_国籍.Text.Replace("[必須]", "");

                    txt_メール.Enabled = false;
                    lbl_メール.Text = lbl_メール.Text.Replace("[必須]", "");

                    txt_携帯.Enabled = false;
                    lbl_携帯.Text = lbl_携帯.Text.Replace("[必須]", "");
                    txt_学校名.Enabled = false;
                    lbl_学校名.Text = lbl_学校名.Text.Replace("[必須]", "");
                }
                else if (!string.IsNullOrWhiteSpace(code_社員))
                {
                    if (社員種別 != "研修")
                    {
                        this.toolStripStatusLabel1.ForeColor = Color.Red;
                        this.toolStripStatusLabel1.Text = "研修社員のみ入塾可能です。";
                        btn_save.Enabled = false;
                        return;
                    }
                    //社員情報取得
                    DataTable dt = Get社員Info(code_社員);
                    if (dt.Rows.Count > 0)
                    {
                        txt_学生コード.Text = dt.Rows[0]["学生コード"].ToString();
                        txt_名前.Text = dt.Rows[0]["名前"].ToString();
                        txt_カタカナ.Text = dt.Rows[0]["カタカナ"].ToString();
                        txt_ローマ字表記.Text = dt.Rows[0]["ローマ字表記"].ToString();
                        dtp_生年月日.Text = dt.Rows[0]["生年月日"].ToString();
                        cmb_性別.Text = dt.Rows[0]["性別"].ToString();
                        txt_MyNumber.Text = dt.Rows[0]["MyNumber"].ToString();
                        txt_郵便番号1.Text = dt.Rows[0]["郵便番号"].ToString().Substring(0, 3);
                        txt_郵便番号2.Text = dt.Rows[0]["郵便番号"].ToString().Substring(3, 4);
                        txt_住所.Text = dt.Rows[0]["住所"].ToString();
                        txt_携帯.Text = dt.Rows[0]["携帯"].ToString();
                        txt_メール.Text = dt.Rows[0]["メール"].ToString();
                        cmb_国籍.Text = dt.Rows[0]["国籍"].ToString();
                        txt_学校名.Text = dt.Rows[0]["学校名"].ToString();
                        txt_クラスコード.Text = dt.Rows[0]["クラスコード"].ToString();
                        txt_ClassName.Text = dt.Rows[0]["クラス名"].ToString();
                        code_クラスコード_Before = txt_クラスコード.Text;
                        SetEnabled必須();
                        if (txt_学校名.Text == "")
                        {
                            lbl_学校名.Text += "[必須]";
                            txt_学校名.Enabled = true;
                        }
                        lbl_入塾日.Text += "[必須]";
                        dtp_入塾日.Enabled = true;
                        lbl_離塾日.Visible = false;
                        dtp_離塾日.Visible = false;
                        lbl_離塾原因.Visible = false;
                        cmb_離塾原因.Visible = false;
                    }
                }

                this.Text = "学生入塾";
            }
            else
            {
                //更新時 対象データを取得する
                txt_学生コード.Enabled = false;

                if (isUpdate == "delete")
                {
                    SetEnabled必須();

                    lbl_離塾日.Text = lbl_離塾日.Text + "　[必須]";
                    lbl_離塾原因.Text = lbl_離塾原因.Text + "　[必須]";

                    DataTable dt_離塾Info = new DataTable();
                    dt_離塾Info.Columns.Add("IndexValue");
                    dt_離塾Info.Columns.Add("離塾原因");
                    //0:空白(NULL)；1：卒業；2：キャンセル；3:除籍
                    dt_離塾Info.Rows.Add("0", "");
                    dt_離塾Info.Rows.Add("1", "卒業");
                    dt_離塾Info.Rows.Add("2", "キャンセル");
                    dt_離塾Info.Rows.Add("3", "除籍");
                    txt_クラスコード.Enabled = false;
                    txt_ClassName.Enabled = false;
                    btn_クラス選択.Enabled = false;
                    cmb_離塾原因.DisplayMember = "離塾原因";
                    cmb_離塾原因.ValueMember = "IndexValue";
                    cmb_離塾原因.DataSource = dt_離塾Info;
                    cmb_離塾原因.SelectedIndex = 0;
                    btn_save.Text = "退塾";
                    this.Text = "学生退塾";
                }
                else
                {
                    lbl_MyNumber.Text = lbl_MyNumber.Text.Replace("[必須]", "");
                    txt_MyNumber.Enabled = false;

                    if (社員種別 == "研修")
                    {
                        SetEnabled必須();
                        lbl_入塾日.Text += "[必須]";
                        dtp_入塾日.Enabled = true;
                        lbl_離塾日.Visible = false;
                        dtp_離塾日.Visible = false;
                        lbl_離塾原因.Visible = false;
                        cmb_離塾原因.Visible = false;
                    }

                    btn_save.Text = "更新";
                    this.Text = "学生情報変更";
                }

                if (社員種別 == "研修")
                {
                    DataTable dt = Get社員Info(code_社員);
                    if (dt.Rows.Count > 0)
                    {
                        txt_学生コード.Text = dt.Rows[0]["学生コード"].ToString();
                        txt_名前.Text = dt.Rows[0]["名前"].ToString();
                        txt_カタカナ.Text = dt.Rows[0]["カタカナ"].ToString();
                        txt_ローマ字表記.Text = dt.Rows[0]["ローマ字表記"].ToString();
                        dtp_生年月日.Text = dt.Rows[0]["生年月日"].ToString();
                        cmb_性別.Text = dt.Rows[0]["性別"].ToString();
                        txt_MyNumber.Text = dt.Rows[0]["MyNumber"].ToString();
                        code_郵便 = dt.Rows[0].Field<string>("郵便番号").Replace("〒", "").Replace("-", "");
                        txt_郵便番号1.Text = code_郵便.Substring(0, 3);
                        txt_郵便番号2.Text = code_郵便.Substring(3, 4);
                        txt_住所.Text = dt.Rows[0]["住所"].ToString();
                        txt_携帯.Text = dt.Rows[0]["携帯"].ToString();
                        txt_メール.Text = dt.Rows[0]["メール"].ToString();
                        cmb_国籍.Text = dt.Rows[0]["国籍"].ToString();
                        txt_学校名.Text = dt.Rows[0]["学校名"].ToString();
                        txt_クラスコード.Text = dt.Rows[0]["クラスコード"].ToString();
                        txt_ClassName.Text = dt.Rows[0]["クラス名"].ToString();
                        code_クラスコード_Before = txt_クラスコード.Text;
                    }
                }
                else
                {
                    //学生データ取得
                    GetDataSource();
                }
            }
        }

        private void SetEnabled必須()
        {
            txt_名前.Enabled = false;
            txt_カタカナ.Enabled = false;
            txt_ローマ字表記.Enabled = false;
            dtp_生年月日.Enabled = false;
            cmb_性別.Enabled = false;
            txt_MyNumber.Enabled = false;
            dtp_入塾日.Enabled = false;
            txt_郵便番号1.Enabled = false;
            txt_郵便番号2.Enabled = false;
            txt_住所.Enabled = false;
            txt_携帯.Enabled = false;
            txt_メール.Enabled = false;
            cmb_国籍.Enabled = false;
            txt_学校名.Enabled = false;
            dtp_離塾日.Enabled = true;
            cmb_離塾原因.Enabled = true;
            lbl_離塾日.Visible = true;
            dtp_離塾日.Visible = true;
            lbl_離塾原因.Visible = true;
            cmb_離塾原因.Visible = true;

            //更新時、ラベルテキスト処理
            lbl_名前.Text = lbl_名前.Text.Replace("[必須]", "");
            lbl_カタカナ.Text = lbl_カタカナ.Text.Replace("[必須]", "");
            lbl_ローマ字表記.Text = lbl_ローマ字表記.Text.Replace("[必須]", "");
            lbl_生年月日.Text = lbl_生年月日.Text.Replace("[必須]", "");
            lbl_性別.Text = lbl_性別.Text.Replace("[必須]", "");
            lbl_MyNumber.Text = lbl_MyNumber.Text.Replace("[必須]", "");
            lbl_入塾日.Text = lbl_入塾日.Text.Replace("[必須]", "");
            lbl_郵便番号.Text = lbl_郵便番号.Text.Replace("[必須]", "");
            lbl_住所.Text = lbl_住所.Text.Replace("[必須]", "");
            lbl_携帯.Text = lbl_携帯.Text.Replace("[必須]", "");
            lbl_メール.Text = lbl_メール.Text.Replace("[必須]", "");
            lbl_国籍.Text = lbl_国籍.Text.Replace("[必須]", "");
            lbl_学校名.Text = lbl_学校名.Text.Replace("[必須]", "");
        }

        /// <summary>
        /// 学生コード設定
        /// </summary>
        private void SetStudentCode()
        {
            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库
            try
            {
                sqlcon.Open();
            }
            catch
            {
                AddErrMsgList(lbl_学生コード.Text, "[学生コード]の設定処理:DBサーバーの接続に失敗しました。");
                return;
            }

            //tong sqlcmdを書き直し　20200527
            string str_sqlcmd = @"SELECT (MAX(cast(学生コード as int)) + 1) as 学生コード FROM HL_JUKUKANRI_学生マスタ";

            SqlCommand com = new SqlCommand(str_sqlcmd, sqlcon);
            SqlDataReader reader = com.ExecuteReader();

            if (reader.Read())
            {
                txt_学生コード.Text = reader["学生コード"].ToString() == "" ? "1" : reader["学生コード"].ToString();
            }
            else
            {
                this.toolStripStatusLabel1.ForeColor = Color.Red;
                this.toolStripStatusLabel1.Text = "学生コードの設定が失敗しました。データベースを確認してください。";
            }

            if (sqlcon != null)
            {
                sqlcon.Close();
            }
        }

        /// <summary>
        /// 入力チェック
        /// </summary>
        public bool InputCheck()
        {
            //初期表示設定 更新
            txt_学生コード.BackColor = Color.Empty;
            txt_名前.BackColor = Color.Empty;
            txt_カタカナ.BackColor = Color.Empty;
            txt_ローマ字表記.BackColor = Color.Empty;
            txt_MyNumber.BackColor = Color.Empty;
            txt_郵便番号1.BackColor = Color.Empty;
            txt_郵便番号2.BackColor = Color.Empty;
            txt_住所.BackColor = Color.Empty;
            txt_携帯.BackColor = Color.Empty;
            txt_メール.BackColor = Color.Empty;
            cmb_国籍.BackColor = Color.Empty;
            txt_学校名.BackColor = Color.Empty;
            cmb_離塾原因.BackColor = Color.Empty;
            toolStripStatusLabel1.Text = "";

            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库

            try
            {
                sqlcon.Open();
            }
            catch
            {
                this.toolStripStatusLabel1.ForeColor = Color.Red;
                this.toolStripStatusLabel1.Text = "DBサーバーの接続に失敗しました";

                return false;
            }

            //学生コード
            if (string.IsNullOrWhiteSpace(code_学生コード))
            {
                //必須
                AddErrMsgList(lbl_学生コード.Text, string.Format(@"エラー：{0}が必須です." + Environment.NewLine, lbl_学生コード.Text));
                return false;
            }

            //存在チェック
            string sqlcmd = string.Format(@"Select * From HL_JUKUKANRI_学生マスタ Where 学生コード = '{0}'", code_学生コード);
            SqlCommand sqlcom = new SqlCommand(sqlcmd, sqlcon);

            try
            {
                SqlDataReader reader = sqlcom.ExecuteReader();
                if (isUpdate != "new" && !reader.HasRows)
                {
                    //更新：学生コードが存在しない場合エラー
                    AddErrMsgList(lbl_学生コード.Text, string.Format(@"エラー：{0}[{1}]が登録されていません." + Environment.NewLine, lbl_学生コード.Text, code_学生コード));
                    return false;
                }
                else if (isUpdate == "new" && reader.HasRows)
                {
                    //新規：学生コードがある場合エラー
                    AddErrMsgList(lbl_学生コード.Text, string.Format(@"エラー：{0}[{1}]が既に登録されています." + Environment.NewLine, lbl_学生コード.Text, code_学生コード));
                    return false;
                }
            }
            catch (Exception ex)
            {
                this.toolStripStatusLabel1.ForeColor = Color.Red;
                this.toolStripStatusLabel1.Text = "学生コードのチェック処理にエラーが発生しました。" + ex.Message;

                if (sqlcon != null)
                {
                    sqlcon.Close();
                }
                txt_学生コード.Focus();
                return false;
            }

            if (isUpdate != "delete")
            {

                //名前
                if (string.IsNullOrWhiteSpace(name_学生))
                {
                    //必須
                    AddErrMsgList(lbl_名前.Text, "名前未入力!");
                    return false;
                }
                else
                {
                    if (this.name_学生.IndexOf(" ") > 0)
                    {
                        AddErrMsgList(lbl_名前.Text, "名前に許可されない文字'半角SPACE' が入りました。");
                        return false;
                    }

                    if (this.name_学生.IndexOf("　") == -1)
                    {
                        AddErrMsgList(lbl_名前.Text, "姓と名の間に '全角SPACE' を挿入してください!");
                        return false;
                    }

                    if (this.name_学生.IndexOf("　") != this.name_学生.LastIndexOf("　"))
                    {
                        AddErrMsgList(lbl_名前.Text, "名前に '全角SPACE' は二つ以上入力しないでください!");
                        return false;
                    }

                    if (this.name_学生.IndexOf(",") > 0)
                    {
                        AddErrMsgList(lbl_名前.Text, "名前に許可されない文字「,」が入りました。");
                        return false;
                    }
                }

                //カタカナ
                if (this.katakana.Equals(""))
                {
                    AddErrMsgList(lbl_カタカナ.Text, "カタカナ未入力!");
                    return false;
                }

                if (this.katakana.IndexOf(" ") > 0)
                {
                    AddErrMsgList(lbl_カタカナ.Text, "カタカナに許可されない文字'半角SPACE' が入りました。");
                    return false;
                }

                if (this.katakana.IndexOf("　") == -1)
                {
                    AddErrMsgList(lbl_カタカナ.Text, "カタカナの姓と名の間に '全角SPACE' を挿入してください!");
                    return false;
                }

                if (this.katakana.IndexOf("　") != this.katakana.LastIndexOf("　"))
                {
                    AddErrMsgList(lbl_カタカナ.Text, "カタカナに '全角SPACE' は二つ以上入力しないでください!");
                    return false;
                }

                if (this.katakana.IndexOf(",") > 0)
                {
                    AddErrMsgList(lbl_カタカナ.Text, "カタカナに許可されない文字「,」が入りました。");
                    return false;
                }
                if (!ComClass.IsFullKatakana(this.katakana.Replace("　", "")))
                {
                    AddErrMsgList(lbl_カタカナ.Text, "カタカナは全角カタカナでご入力ください！");
                    return false;
                }

                //ローマ字表記
                if (this.romaji.Equals(""))
                {
                    AddErrMsgList(lbl_ローマ字表記.Text, "ローマ字表記未入力!");
                    return false;
                }

                //liuxiaoyan 
                if (!Isromaji(this.romaji))
                {
                    AddErrMsgList(lbl_ローマ字表記.Text, "ローマ字が間違っています!");
                    return false;
                }

                if (isUpdate == "new")
                {
                    //Mynumber
                    if (!ValidateMyNumber(myNumber))
                    {
                        //不正なMYNumber!MYNumberを再入力してください！
                        AddErrMsgList(lbl_MyNumber.Text, "不正なMYNumber!MYNumberを再入力してください！");
                        return false;
                    }

                    SqlConnection conn = new SqlConnection(connectionString); //连接数据库

                    try
                    {
                        conn.Open();
                    }
                    catch
                    {
                        this.toolStripStatusLabel1.ForeColor = Color.Red;
                        this.toolStripStatusLabel1.Text = "サーバーがつなげない！インターネット接続をチェックしてください。";

                        return false;
                    }

                    string str_sqlcmd = string.Format(@"select * from HL_JUKUKANRI_学生マスタ where  MyNumber = '{0}'",
                        this.myNumber);

                    SqlCommand com = new SqlCommand(str_sqlcmd, conn);

                    SqlDataReader reader = com.ExecuteReader();

                    if (reader.Read())
                    {
                        AddErrMsgList(lbl_MyNumber.Text, "このMyNumberはすでに登録済みです！");
                        return false;
                    }

                    reader.Close();
                    conn.Close();
                }

                //郵便番号
                if (this.code_郵便1.Equals("") || this.code_郵便2.Equals("")) 
                {
                    AddErrMsgList(lbl_郵便番号.Text, "郵便番号未入力!");
                    return false;
                }

                if (!Is郵便番号(this.code_郵便))
                {
                    AddErrMsgList(lbl_郵便番号.Text, "郵便番号が間違っています!");
                    return false;
                }

                //住所
                if (this.address.Equals(""))
                {
                    AddErrMsgList(lbl_住所.Text, "住所未入力!");
                    return false;
                }

                //携帯
                if (this.telPhone.Equals(""))
                {
                    AddErrMsgList(lbl_携帯.Text, "携帯未入力!");
                    return false;
                }
                if (this.telPhone.IndexOf(",") > 0)
                {
                    AddErrMsgList(lbl_携帯.Text, "携帯に許可されない文字「,」が入りました。");
                    return false;
                }
                if (!ISTEL(this.telPhone))
                {
                    AddErrMsgList(lbl_携帯.Text, "携帯のフォーマットが間違っています!");
                    return false;
                }

                //メール
                if (this.mailAddress.Equals(""))
                {
                    AddErrMsgList(lbl_メール.Text, "メール未入力!");
                    return false;
                }
                if (this.mailAddress.IndexOf(",") > 0)
                {
                    AddErrMsgList(lbl_メール.Text, "メールに許可されない文字「,」が入りました。");
                    return false;
                }
                if (!IsValidEmail(this.mailAddress))
                {
                    AddErrMsgList(lbl_メール.Text, "メールのフォーマットが間違っています!");
                    return false;
                }

                //国籍
                if (this.country.Equals(""))
                {
                    AddErrMsgList(lbl_国籍.Text, "国籍未入力!");
                    return false;
                }

                //学校名
                if (this.name_学校.Equals(""))
                {
                    AddErrMsgList(lbl_学校名.Text, "学校名未入力!");
                    return false;
                }
            }
            else
            {
                //離塾原因
                if (cmb_離塾原因.Text == "")
                {
                    AddErrMsgList(lbl_離塾原因.Text, "離塾原因を設定してください。");
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Mynumberチェック
        /// </summary>
        public static bool ValidateMyNumber(string mynumber)
        {
            //12文字でなければ偽
            if (mynumber.Length != 12)
            {
                return false;
            }

            //整数の列挙にして逆転
            var digits = mynumber.Select(e => e - '0').Reverse();

            //（↑で逆転しているので）最初の数字がチェックデジット
            var checkDigit = digits.First();

            var reminder = digits //整数の列挙を
                .Skip(1) //最初のチェックデジットは読み飛ばして
                .Select((e, i) => { var p = e; var q = i <= 5 ? i + 2 : i - 4; return p * q; }) // PとQを計算して積を求めて
                .Sum() % 11; // 合計を求めて11で割った商を出す

            return checkDigit == (reminder == 0 || reminder == 1 ? 0 : 11 - reminder);
        }

        /// <summary>
        /// 携帯番号チェック
        /// </summary>
        private bool ISTEL(string str_url)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_url, @"\A0\d{1,4}-\d{1,4}-\d{4}\z");
        }

        /// <summary>
        /// メールチェック
        /// </summary>
        private bool IsValidEmail(string strIn)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)" + @"|(([\w-]+\.)+))([a-zA-Z]{2,5}|[0-9]{1,3})(\]?)$");
        }

        /// <summary>
        /// 郵便番号チェック
        /// </summary>
        private bool Is郵便番号(string strIn)
        {
            //return System.Text.RegularExpressions.Regex.IsMatch(strIn, @"\d{7}|\d{3}-\d{4}");
            //return System.Text.RegularExpressions.Regex.IsMatch(strIn, "^[0-9]{3}[-]?[0-9]{4}$");
            return System.Text.RegularExpressions.Regex.IsMatch(strIn, "^[0-9]{7}$");

        }

        // liuxiaoyan add 0618
        /// <summary>
        /// ローマ字チェック
        /// </summary>
        private bool Isromaji(string strIn)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(strIn, "^[ A-Za-z]+$");
        }

        //end

        /// <summary>
        /// 参照データ取得
        /// </summary>
        public void GetDataSource()
        {
            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库

            try
            {
                sqlcon.Open();
            }
            catch
            {
                this.toolStripStatusLabel1.ForeColor = Color.Red;
                this.toolStripStatusLabel1.Text = "DBサーバーの接続に失敗しました";
                return;
            }

            //データ取得開始
            string str_sqlcmd = "SELECT"
                            + "　 T1.学生コード "
                            + "　, T1.名前 "
                            + "　, T1.カタカナ "
                            + "　, T1.ローマ字表記 "
                            + "　, T1.生年月日 "
                            + "　, T1.MyNumber "
                            + "　, T1.性別 "
                            + "　, T1.入塾日 "
                            + "　, T1.離塾日 "
                            + "　, T1.離塾原因 "
                            + "  , T3.クラスコード "
                            + "  , T4.クラス名 "
                            + "  , T4.学生コード as クラス_学生"
                            + "  , T2.郵便番号 "
                            + "  , T2.住所 "
                            + "  , T2.携帯 "
                            + "  , T2.メール "
                            + "  , T2.国籍 "
                            + "  , T2.学校名 "
                            + "  , T2.社員コード "
                            + " FROM "
                            + "   HL_JUKUKANRI_学生マスタ AS T1 "
                            + "   LEFT JOIN HL_JUKUKANRI_学生情報 AS T2 "
                            + "     ON T1.学生コード = T2.学生コード "
                            + "   LEFT JOIN HL_JUKUKANRI_学生クラス AS T3 "
                            + "     ON T1.学生コード = T3.学生コード "
                            + "   LEFT JOIN HL_JUKUKANRI_クラス履歴 AS T4 "
                            + "     ON T3.クラスコード = T4.クラスコード "
                            + " WHERE T1.学生コード = '" + code_学生コード + "'";

            try
            {
                SqlDataAdapter sqlDa = new SqlDataAdapter(str_sqlcmd, sqlcon);
                dtInfo = new DataTable();
                sqlDa.Fill(dtInfo);

                if (dtInfo.Rows.Count == 1)
                {
                    //該当項目に値を設定する
                    //学生マスタ
                    txt_学生コード.Text = dtInfo.Rows[0].Field<string>("学生コード");
                    txt_名前.Text = dtInfo.Rows[0].Field<string>("名前");
                    txt_カタカナ.Text = dtInfo.Rows[0].Field<string>("カタカナ");
                    txt_ローマ字表記.Text = dtInfo.Rows[0].Field<string>("ローマ字表記");
                    dtp_生年月日.Value = dtInfo.Rows[0].Field<DateTime>("生年月日");
                    cmb_性別.Text = dtInfo.Rows[0].Field<string>("性別");
                    txt_MyNumber.Text = dtInfo.Rows[0].Field<string>("MyNumber");
                    dtp_入塾日.Value = dtInfo.Rows[0].Field<DateTime>("入塾日");
                    txt_ClassName.Text = string.IsNullOrWhiteSpace(dtInfo.Rows[0].Field<string>("クラス名")) ? "-" : dtInfo.Rows[0].Field<string>("クラス名");

                    if (!string.IsNullOrWhiteSpace(dtInfo.Rows[0]["離塾原因"].ToString()))
                    {
                        int rec = dtInfo.Rows[0].Field<int>("離塾原因");
                        if (rec != 0)
                        {
                            dtp_離塾日.Value = dtInfo.Rows[0].Field<DateTime>("離塾日");
                            cmb_離塾原因.SelectedValue = rec;
                        }
                    }

                    //学生情報
                    code_郵便 = dtInfo.Rows[0].Field<string>("郵便番号");
                    if (code_郵便.IndexOf("〒") >= 0)
                    {
                        code_郵便 = code_郵便.Replace("〒", "");
                    }
                    if (code_郵便.IndexOf("-") >= 0) 
                    {
                        code_郵便 = code_郵便.Replace("-", "");
                    }
                    txt_郵便番号1.Text = code_郵便.Substring(0, 3);
                    txt_郵便番号2.Text = code_郵便.Substring(3, 4);
                    txt_住所.Text = dtInfo.Rows[0].Field<string>("住所");
                    txt_携帯.Text = dtInfo.Rows[0].Field<string>("携帯");
                    txt_メール.Text = dtInfo.Rows[0].Field<string>("メール");
                    cmb_国籍.Text = dtInfo.Rows[0].Field<string>("国籍");

                    //卒業学校
                    txt_学校名.Text = dtInfo.Rows[0].Field<string>("学校名");

                    txt_クラスコード.Text = dtInfo.Rows[0]["クラスコード"].ToString();
                    code_クラスコード_Before = txt_クラスコード.Text;

                    if (txt_ClassName.Text == "-")
                    {
                        txt_ClassName.TextAlign = HorizontalAlignment.Center;
                    }
                    else
                    {
                        //txt_ClassNameのサイズ設定
                        const int x_margin = 0;
                        const int y_margin = 2;
                        Size size = TextRenderer.MeasureText(txt_ClassName.Text, txt_ClassName.Font);
                        txt_ClassName.ClientSize = new Size(size.Width + x_margin, size.Height + y_margin);

                        //btn_クラス選択の箇所設定
                        Point locationOnForm = txt_ClassName.FindForm().PointToClient(txt_ClassName.Parent.PointToScreen(txt_ClassName.Location));

                        btn_クラス選択.Location = new Point(locationOnForm.X + size.Width + 20, locationOnForm.Y);
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                this.toolStripStatusLabel1.ForeColor = Color.Red;
                this.toolStripStatusLabel1.Text = "学生[{0}]の情報の取得に失敗しました。";
            }
            finally
            {
                if (sqlcon != null)
                {
                    sqlcon.Close();
                }
            }
        }

        /// <summary>
        /// 更新ボタン
        /// </summary>
        private void UpdateStudentInfo()
        {
            //画面値取得
            code_学生コード = txt_学生コード.Text;
            name_学生 = txt_名前.Text;
            katakana = txt_カタカナ.Text;
            romaji = txt_ローマ字表記.Text;
            date_生年月日 = dtp_生年月日.Value;
            //myNumber = txt_MyNumber.Text;
            sex = cmb_性別.Text;
            date_入塾日 = dtp_入塾日.Value;
            code_郵便1 = txt_郵便番号1.Text;
            code_郵便2 = txt_郵便番号2.Text;
            code_郵便 = txt_郵便番号1.Text + txt_郵便番号2.Text;
            address = txt_住所.Text;
            telPhone = txt_携帯.Text;
            mailAddress = txt_メール.Text;
            country = cmb_国籍.Text;
            name_学校 = txt_学校名.Text;
            string date_生年月日txt = date_生年月日.ToString("yyyy-MM-dd");
            string date_入塾日txt = date_入塾日.ToString("yyyy-MM-dd");
            //string date_開始日txt = date_開始日.ToString("yyyy-MM-dd");
            string code_クラスコード = txt_クラスコード.Text;
            //入力チェック
            if (!InputCheck())
            {
                return;
            }

            //更新処理
            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库

            int result = 0;
            try
            {
                sqlcon.Open();
            }
            catch
            {
                this.toolStripStatusLabel1.ForeColor = Color.Red;
                this.toolStripStatusLabel1.Text = "DBサーバーの接続に失敗しました。";

                return;
            }
            SqlTransaction transaction = sqlcon.BeginTransaction();
            try
            {
                SqlCommand sqlcom = new SqlCommand();
                sqlcom.Connection = sqlcon;
                sqlcom.Transaction = transaction;

                //更新行う
                if (isUpdate == "update")
                {
                    //学生マスタ更新
                    string sqlCommand = @"UPDATE [dbo].[HL_JUKUKANRI_学生マスタ] SET
                                        [名前] = N'{0}',
                                        [カタカナ] = N'{1}',
                                        [ローマ字表記] = N'{2}',
                                        [生年月日] = '{3}',
                                        [性別] = N'{4}',
                                        [入塾日] = '{5}' 
                                        WHERE 学生コード = '{6}'";
                        sqlcom.CommandText = string.Format(sqlCommand, name_学生, katakana, romaji, date_生年月日, sex, date_入塾日txt, code_学生コード);

                    result = sqlcom.ExecuteNonQuery();

                    if (result == 1)
                    {
                        sqlCommand = @"UPDATE [dbo].[HL_JUKUKANRI_学生情報] SET
                                    [郵便番号] = '{0}',
                                    [住所] = N'{1}',
                                    [携帯] = '{2}',
                                    [メール] = '{3}',
                                    [国籍] = N'{4}',
                                    [学校名] = N'{5}'
                                    WHERE [学生コード] = '{6}' ";

                        sqlcom.CommandText = string.Format(sqlCommand, code_郵便, address, telPhone, mailAddress, country, txt_学校名.Text, code_学生コード);
                        result = sqlcom.ExecuteNonQuery();
                        if (result != 1)
                        {
                            throw new Exception();
                        }

                        if (code_クラスコード_Before != code_クラスコード)
                        {
                            //クラス更新
                            //1.元クラスからメンバー削除
                            if (!string.IsNullOrWhiteSpace(code_クラスコード_Before))
                            {
                                result = Deleteクラス学生(sqlcom);
                                if (result != 1)
                                {
                                    throw new Exception();
                                }
                            }
                            if (!string.IsNullOrWhiteSpace(code_クラスコード))
                            {
                                //2.新クラスにメンバー追加
                                result = Addクラス学生(code_クラスコード, sqlcom);
                                if (result != 1)
                                {
                                    throw new Exception();
                                }
                            }
                            else
                            {
                                sqlCommand = @"UPDATE [dbo].[HL_JUKUKANRI_学生クラス] SET クラスコード = null WHERE 学生コード = '{0}'";
                                sqlcom.CommandText = string.Format(sqlCommand, code_学生コード);

                                result = sqlcom.ExecuteNonQuery();
                            }
                        }
                    }
                }
                else
                {
                    //学生退塾
                    string sqlCommand = @"UPDATE [dbo].[HL_JUKUKANRI_学生マスタ] SET
                                    [離塾日] = '{0}',
                                    [離塾原因] = {1} 
                                    WHERE 学生コード = '{2}'";
                    sqlcom.CommandText = string.Format(sqlCommand, dtp_離塾日.Value.ToShortDateString(), cmb_離塾原因.SelectedValue, code_学生コード);

                    result = sqlcom.ExecuteNonQuery();

                    if (result != 1)
                    {
                        throw new Exception();
                    }

                    if (cmb_離塾原因.Text != "卒業")
                    {
                        result = Deleteクラス学生(sqlcom);

                        if (result != 1)
                        {
                            throw new Exception();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.toolStripStatusLabel1.ForeColor = Color.Red;
                this.toolStripStatusLabel1.Text = "学生情報の更新処理が失敗しました。" + ex.Message;
            }
            finally
            {
                if (result == 1)
                {
                    transaction.Commit();

                    ((Form1)(this.Tag)).reLoad = false;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Green;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = string.Format("学生コード[{0}]が正常に更新されました。", txt_学生コード.Text);

                    if (((Form1)(this.Tag)).m_学生情報一覧Handle != null)
                    {
                        SendMessage(((Form1)(this.Tag)).m_学生情報一覧Handle, Form1.CUSTOM_MESSAGE, IntPtr.Zero, IntPtr.Zero);
                    }

                    this.Close();
                }
                else
                {
                    this.toolStripStatusLabel1.ForeColor = Color.Red;
                    this.toolStripStatusLabel1.Text = "学生情報の更新処理が失敗しました。";
                }

                if (sqlcon != null)
                {
                    sqlcon.Close();
                }
            }

        }

        /// <summary>
        /// クラスに学生を入れる
        /// </summary>
        private int Addクラス学生(string code_クラスコード, SqlCommand sqlcom, bool update = true)
        {
            int result;
            if (!list_学生.Contains(code_学生コード))
            {
                list_学生.Add(code_学生コード);
            }
            list_学生.Sort();
            list_学生.Remove("-");
            if (list_学生.Count > 0)
            {
                code_学生コード = string.Join(",", list_学生);
                if (code_学生コード.Substring(0, 1) == ",")
                {
                    code_学生コード = code_学生コード.Substring(1, code_学生コード.Length - 1);
                }

            }

            sqlcom.CommandText = string.Format(@"UPDATE [dbo].[HL_JUKUKANRI_クラス履歴] SET [学生コード] = '{0}' WHERE [クラスコード] = {1}", code_学生コード, code_クラスコード);
            result = sqlcom.ExecuteNonQuery();
            if (result == 1)
            {
                //学生クラス
                sqlcom.CommandText = string.Format(@"Update HL_JUKUKANRI_学生クラス SET [クラスコード] = '{0}' WHERE [学生コード] = {1}", code_クラスコード, txt_学生コード.Text);
                result = sqlcom.ExecuteNonQuery();
            }
            return result;
        }

        /// <summary>
        /// 学生削除
        /// </summary>
        private int Deleteクラス学生(SqlCommand sqlcom)
        {
            if (!string.IsNullOrEmpty(txt_クラスコード.Text))
            {
                int result;
                List<string> list_学生コード元 = new List<string>();
                list_学生コード元.AddRange(dtInfo.Rows[0].Field<string>("クラス_学生").Split(','));
                list_学生コード元.Remove(code_学生コード);
                list_学生コード元.Remove("-");

                sqlcom.CommandText = string.Format(@"UPDATE [dbo].[HL_JUKUKANRI_クラス履歴] SET [学生コード] = '{0}' WHERE [クラスコード] = {1}", string.Join(",", list_学生コード元), code_クラスコード_Before);
                result = sqlcom.ExecuteNonQuery();

                sqlcom.CommandText = string.Format(@"Update [dbo].[HL_JUKUKANRI_学生クラス]  SET [クラスコード] = NULL WHERE [学生コード] = '{0}'", txt_学生コード.Text);
                result = sqlcom.ExecuteNonQuery();

                return result;
            }
            else
            {
                return 1;
            }

        }

        /// <summary>
        /// 画面閉じ
        /// </summary>
        private void 学生情報_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isUpdate == "new")
            {
                ((Form1)(this.Tag)).m_新規学生入塾Handle = IntPtr.Zero;

            }
            else
            {
                ((Form1)(this.Tag)).m_学生情報変更Handle = IntPtr.Zero;
            }

            if (((Form1)(this.Tag)).codeDic.ContainsKey(code_学生コード))
            {
                ((Form1)(this.Tag)).codeDic.Remove(code_学生コード);
            }
        }

        /// <summary>
        /// 登録・更新処理
        /// </summary>
        private void btn_save_Click(object sender, EventArgs e)
        {
            if (isUpdate != "new")
            {
                UpdateStudentInfo();
                return;
            }
            //画面値取得
            code_学生コード = txt_学生コード.Text;
            name_学生 = txt_名前.Text;
            katakana = txt_カタカナ.Text;
            romaji = txt_ローマ字表記.Text;
            date_生年月日 = dtp_生年月日.Value;
            myNumber = txt_MyNumber.Text;
            sex = cmb_性別.Text;
            date_入塾日 = dtp_入塾日.Value;
            code_郵便1 = txt_郵便番号1.Text;
            code_郵便2 = txt_郵便番号2.Text;
            code_郵便  = txt_郵便番号1.Text + txt_郵便番号2.Text;
            address = txt_住所.Text;
            telPhone = txt_携帯.Text;
            mailAddress = txt_メール.Text;
            country = cmb_国籍.Text;
            name_学校 = txt_学校名.Text;

            //入力チェック
            if (!InputCheck())
            {
                return;
            }
            if (btn_save.Text == "登録")
            {
                //登録処理
                SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库
                string date_生年月日txt = date_生年月日.ToString("yyyy-MM-dd");
                string date_入塾日txt = date_入塾日.ToString("yyyy-MM-dd");
                string date_離塾日txt = null;
                string code_クラスコード = string.IsNullOrWhiteSpace(txt_クラスコード.Text) ? "null" : txt_クラスコード.Text;
                int result = 0;
                try
                {
                    sqlcon.Open();
                }
                catch
                {
                    this.toolStripStatusLabel1.ForeColor = Color.Red;
                    this.toolStripStatusLabel1.Text = "DBサーバーの接続に失敗しました。";
                    return;
                }
                SqlTransaction transaction = sqlcon.BeginTransaction();
                try
                {
                    SqlCommand sqlcom = new SqlCommand();
                    sqlcom.Connection = sqlcon;
                    sqlcom.Transaction = transaction;

                    //登録行う
                    //学生マスタ登録
                    string sqlCommand = @"INSERT INTO[dbo].[HL_JUKUKANRI_学生マスタ] (                   
                                          [学生コード]
                                          , [名前]
                                          , [カタカナ]
                                          , [ローマ字表記]
                                          , [生年月日]
                                          , [MyNumber]
                                          , [性別]
                                          , [入塾日]
                                          , [離塾日]
                                          , [離塾原因]
                                        ) 
                                        VALUES( '{0}', N'{1}', N'{2}', '{3}', '{4}', '{5}', N'{6}', '{7}', ";

                    if (string.IsNullOrWhiteSpace(date_離塾日txt))
                    {
                        sqlCommand += @"null,null)";
                    }
                    else
                    {
                        sqlCommand += @"'" + date_離塾日txt + "',null)";
                    }

                    sqlcom.CommandText = string.Format(sqlCommand, code_学生コード, name_学生, katakana, romaji, date_生年月日, myNumber, sex, date_入塾日txt);

                    result = sqlcom.ExecuteNonQuery();

                    if (result == 1)
                    {

                        //新規学生入塾
                        sqlCommand = @"INSERT INTO[dbo].[HL_JUKUKANRI_学生情報] ( 
                                        [学生コード]
                                        , [郵便番号]
                                        , [住所]
                                        , [学校名]
                                        , [携帯]
                                        , [メール]
                                        , [国籍]
                                        , [研修フラグ]
                                        , [応募者ID])
                                        VALUES( '{0}', '{1}', N'{2}', N'{3}', '{4}', '{5}', N'{6}','False',";

                        if(id_応募者 == null)
                        {
                            sqlCommand += "null)";
                        }
                        else
                        {
                            sqlCommand += @"'" + id_応募者 + "')";
                        }

                        sqlcom.CommandText = string.Format(sqlCommand, code_学生コード, code_郵便, address, name_学校, telPhone, mailAddress, country);
                        result = sqlcom.ExecuteNonQuery();
                        if (result != 1)
                        {
                            throw new Exception();
                        }

                        //クラス登録
                        if (string.IsNullOrWhiteSpace(txt_クラスコード.Text))
                        {
                            sqlcom.CommandText = string.Format(@"INSERT INTO[dbo].[HL_JUKUKANRI_学生クラス] ([学生コード]) VALUES( '{0}')", code_学生コード);
                        }
                        else
                        {
                            sqlcom.CommandText = string.Format(@"INSERT INTO[dbo].[HL_JUKUKANRI_学生クラス] ([学生コード], [クラスコード] ) VALUES( '{0}', '{1}')", code_学生コード, txt_クラスコード.Text);
                        }
                        result = sqlcom.ExecuteNonQuery();
                        if (result != 1)
                        {
                            throw new Exception();
                        }

                        if (!string.IsNullOrWhiteSpace(txt_クラスコード.Text))
                        {
                            result = Addクラス学生(txt_クラスコード.Text, sqlcom);
                            if (result != 1)
                            {
                                throw new Exception();
                            }
                        }

                        ((Form1)(this.Tag)).reLoad = false;
                        ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Green;
                        ((Form1)(this.Tag)).toolStripStatusLabel2.Text = string.Format("学生コード[{0}]が正常に登録されました。", txt_学生コード.Text);

                        transaction.Commit();

                        if (((Form1)(this.Tag)).m_学生情報一覧Handle != null)
                        {
                            SendMessage(((Form1)(this.Tag)).m_学生情報一覧Handle, Form1.CUSTOM_MESSAGE, IntPtr.Zero, IntPtr.Zero);
                        }
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    this.toolStripStatusLabel1.ForeColor = Color.Red;
                    this.toolStripStatusLabel1.Text = "学生情報の登録処理が失敗しました。" + ex.Message;

                    transaction.Rollback();
                }
                finally
                {
                    if (sqlcon != null)
                    {
                        sqlcon.Close();
                    }
                }
            }
        }

        /// <summary>
        ///  エラーメッセージ追加
        /// </summary>
        private void AddErrMsgList(string name, string msg)
        {
            if (name == "学生コード")
            {
                txt_学生コード.BackColor = System.Drawing.Color.Red;
            }
            else if (name == "名前")
            {
                txt_名前.BackColor = System.Drawing.Color.Red;
            }
            else if (name == "カタカナ")
            {
                txt_カタカナ.BackColor = System.Drawing.Color.Red;
            }
            else if (name == "ローマ字表記")
            {
                txt_ローマ字表記.BackColor = System.Drawing.Color.Red;
            }
            else if (name == "MyNumber")
            {
                txt_MyNumber.BackColor = System.Drawing.Color.Red;
            }
            else if (name == "郵便番号")
            {
                txt_郵便番号1.BackColor = System.Drawing.Color.Red;
                txt_郵便番号2.BackColor = System.Drawing.Color.Red;
            }
            else if (name == "住所")
            {
                txt_住所.BackColor = System.Drawing.Color.Red;
            }
            else if (name == "携帯")
            {
                txt_携帯.BackColor = System.Drawing.Color.Red;
            }
            else if (name == "メール")
            {
                txt_メール.BackColor = System.Drawing.Color.Red;
            }
            else if (name == "国籍")
            {
                cmb_国籍.BackColor = System.Drawing.Color.Red;
            }
            else if (name == "学校名")
            {
                txt_学校名.BackColor = System.Drawing.Color.Red;
            }
            else if (name == "離塾原因")
            {
                cmb_離塾原因.BackColor = System.Drawing.Color.Red;
            }
            this.toolStripStatusLabel1.ForeColor = Color.Red;
            this.toolStripStatusLabel1.Text = msg;
        }

        /// <summary>
        /// クラス選択画面へ移転
        /// </summary>
        private void btn_クラス選択_Click(object sender, EventArgs e)
        {
            string before = txt_ClassName.Text;
            クラス選択 m_NewForm_クラス選択 = new クラス選択();
            m_NewForm_クラス選択.code_クラスコード = txt_クラスコード.Text;
            m_NewForm_クラス選択.Tag = ((Form1)(this.Tag));
            m_NewForm_クラス選択.研修フラグ = lbl_研修社員.Visible.ToString();
            m_NewForm_クラス選択.ShowDialog();
            ((Form1)(this.Tag)).m_クラス選択Handle = IntPtr.Zero;
            txt_クラスコード.Text = m_NewForm_クラス選択.code_クラスコード;
            if (txt_クラスコード.Text == "")
            {
                txt_ClassName.Text = "";
                return;
            }

            //選択なし場合
            if (m_NewForm_クラス選択.name_クラス == "")
            {
                txt_ClassName.Text = before;
            }
            //選択有場合
            else
            {
                txt_ClassName.Text = string.IsNullOrWhiteSpace(m_NewForm_クラス選択.name_クラス) ? "-" : m_NewForm_クラス選択.name_クラス;
            }
            list_学生 = m_NewForm_クラス選択.list_学生;
            code_出勤機コード = m_NewForm_クラス選択.code_出勤機;

            if (txt_ClassName.Text == "-")
            {
                txt_ClassName.TextAlign = HorizontalAlignment.Center;
            }
        }

        /// <summary>
        /// ラベル色設定
        /// </summary>
        private void lbl_必須_Paint(object sender, PaintEventArgs e)
        {
            Label strLbl = (Label)sender;
            string str = strLbl.Text;

            if (str.Contains("必須"))
            {
                strLbl.ForeColor = Color.FromArgb(240, 240, 240);

                Font f = new System.Drawing.Font("メイリオ", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));


                int i = str.IndexOf("　[");
                string str_name = str.Substring(0, i);
                string str必須 = str.Substring(i, str.Length - i);

                if (str.Contains("MyNumber"))
                {
                    i = i - 2;
                }

                for (int x = 0; x <= i; x++)
                {
                    str必須 = "   " + str必須;
                }

                Point point = new Point(((Label)sender).Padding.Right, ((Label)sender).Padding.Top);
                TextRenderer.DrawText(e.Graphics, str必須, f, point, Color.Red);
                TextRenderer.DrawText(e.Graphics, str_name, ((Label)sender).Font, point, Color.Black);
            }
            else
            {
                strLbl.ForeColor = Color.Black;
            }
        }

        private void Clearmsg()
        {
            txt_名前.Clear();
            txt_カタカナ.Clear();
            txt_ローマ字表記.Clear();
            dtp_生年月日.Value = DateTime.Now;
            cmb_性別.SelectedIndex = 0;
            txt_MyNumber.Clear();
            txt_郵便番号1.Clear();
            txt_郵便番号2.Clear();
            txt_住所.Clear();
            txt_携帯.Clear();
            txt_メール.Clear();
            cmb_国籍.SelectedIndex = 0;
            txt_学校名.Clear();
            lbl_応募者id.Text = null;
        }

        // chen 2020/07/17 add

        /// <summary>
        /// 社員情報取得
        /// </summary>
        /// <param name="code_社員"></param>
        /// <returns></returns>
        private DataTable Get社員Info(string code_社員)
        {
            DataTable dtInfo = new DataTable();
            SqlConnection conn = new SqlConnection(ComClass.connectionString); //连接数据库

            try
            {
                conn.Open();
            }
            catch
            {
                this.toolStripStatusLabel1.ForeColor = Color.Red;
                this.toolStripStatusLabel1.Text = "入塾エラー：サーバーがつなげない！インターネット接続をチェックしてください。";

                return dtInfo;
            }

            try
            {
                string str_sqlcmd = string.Format(@"select a.名前, a.カタカナ, a.ローマ字表記, a.生年月日
                                                      , a.MYNumber, a.性別, b.郵便番号, b.住所, b.携帯, b.メール
                                                      , b.国籍 , c.学校名, d.学生コード, f.クラスコード, g.クラス名 from HL_JINJI_社員マスタ a 
                                                    left join HL_JINJI_社員情報 b on a.社員コード = b.社員コード
                                                    left join Hl_JINJI_社員卒業学校 c on a.社員コード = c.社員コード
                                                    left join Hl_JUKUKANRI_学生情報 d on a.社員コード = d.社員コード 
                                                    left join Hl_JUKUKANRI_学生クラス f on d.学生コード = f.学生コード 
                                                    left join Hl_JUKUKANRI_クラス履歴 g on g.クラスコード = f.クラスコード 
                                                   Where a.社員コード = '{0}' ", code_社員);
                SqlDataAdapter sqlDa = new SqlDataAdapter(str_sqlcmd, conn);

                sqlDa.Fill(dtInfo);

            }
            catch
            {
                this.toolStripStatusLabel1.ForeColor = Color.Red;
                this.toolStripStatusLabel1.Text = "入塾エラー：社員情報の取得処理に失敗しました。";
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return dtInfo;
        }

        // chen 2020/07/17 add
    }
}
