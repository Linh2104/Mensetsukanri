using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace HL_塾管理
{
    public partial class 社外教師情報 : DockContent
    {
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        private AutoSizeFormClass asc = new AutoSizeFormClass();

        //DB接続情報
        private string connectionString = ComClass.connectionString;

        //更新・権限区分
        public bool isUpdate = false;

        private bool isAdmin = false;

        //画面項目
        public string code_教師コード = string.Empty;

        private string name_教師 = string.Empty;
        private string katakana = string.Empty;
        private string romaji = string.Empty;
        private string sex = string.Empty;
        private DateTime date_生年月日;
        private string myNumber = string.Empty;
        private string company = string.Empty;
        private string code_郵便 = string.Empty;
        private string adddress = string.Empty;
        private string telPhone = string.Empty;
        private string mailAddress = string.Empty;
        private string country = string.Empty;
        private string language = string.Empty;
        private string admin = string.Empty;
        private string classname = string.Empty;
        private string classroomid = string.Empty;
        private DateTime date_入職日;
        private DateTime date_退職日;
      
        public 社外教師情報()
        {
            InitializeComponent();
        }

        /// <summary>
        /// DGV　Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 社外教師情報_Load(object sender, EventArgs e)
        {
            //asc.controlAutoSize(this);

            //画面区分
            btn_save.Text = isUpdate ? "更新" : "登録";
            this.Text = isUpdate ? "社外教師情報更新" : "社外教師情報登録";

            //初期表示設定 更新・登録
            GetAdmin();
            if (!isAdmin)
            {

                cmb_職務.Visible = false;
                lbl_職務.Visible = false;

                //lbl_入職日.Location = new Point(lbl_クラス.Location.X, lbl_クラス.Location.Y + 50);
                dtp_入職日.Location = new Point(lbl_入職日.Location.X + 120, lbl_入職日.Location.Y - 5);

                lbl_退職フラグ.Location = new Point(lbl_入職日.Location.X, lbl_入職日.Location.Y + 50);
                chk_退職.Location = new Point(lbl_退職フラグ.Location.X + 120, lbl_退職フラグ.Location.Y);
            }
            if (isUpdate == true)
            {
                //txt_クラス名.ReadOnly = true;
                lbl_MyNumber.Text = lbl_MyNumber.Text.Replace("[必須]", "");
            }
            //DGV再表示
            //GetClassroom();
            GetDataSource();
        }

        /// <summary>
        /// 権限区分
        /// </summary>
        private void GetAdmin()
        {
            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库

            try
            {
                sqlcon.Open();
            }
            catch
            {
                tsl_errMsg.ForeColor = Color.Red;
                tsl_errMsg.Text = "サーバーがつなげない！インターネット接続をチェックしてください。";
                return;
            }

            string login = ((Form1)(this.Tag)).m_ユーザ登録.m_ログイン_ユーザ;
            try
            {
                string sql_user = @"Select 教師コード, 職務 From HL_JUKUKANRI_塾ユーザマスタ Where ユーザ名 = '" + login + "'";

                SqlCommand com = new SqlCommand(sql_user, sqlcon);
                SqlDataReader reader = com.ExecuteReader();

                reader.Read();
                isAdmin = reader["職務"].ToString() == "管理者" ? true : false;
                reader.Close();
            }
            catch (Exception ex)
            {
                tsl_errMsg.ForeColor = Color.Red;
                tsl_errMsg.Text = string.Format("ユーザ名[{0}]の情報の取得に失敗しました。" + ex.Message, login);
                return;
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
        /// 参照データ取得
        /// </summary>
        private void GetDataSource()
        {
            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库

            try
            {
                sqlcon.Open();
            }
            catch
            {
                tsl_errMsg.ForeColor = Color.Red;
                tsl_errMsg.Text = "サーバーがつなげない！インターネット接続をチェックしてください。";
                return;
            }

            try
            {
                if (!isUpdate)
                {
                    string sql_code = @"Select 'K' + Convert(nvarchar, (Max(Convert(int, Substring(教師コード, 2, Len(教師コード)))) + 1)) As 教師コード From HL_JUKUKANRI_社外教師マスタ";

                    SqlCommand com = new SqlCommand(sql_code, sqlcon);
                    SqlDataReader reader = com.ExecuteReader();

                    reader.Read();
                    lbl_コード.Text = string.IsNullOrWhiteSpace(reader["教師コード"].ToString()) ? "K1" : reader["教師コード"].ToString();
                    reader.Close();

                    cmb_性別.SelectedIndex = 0;
                    cmb_言語.SelectedItem = ".Net";
                    cmb_国籍.SelectedItem = "中国籍";
                    cmb_職務.SelectedIndex = 1;
                    btn_save.Location = new Point(lbl_入職日.Location.X + 450, lbl_入職日.Location.Y + 150);
                }
                else
                {
                    string sql_up = @"Select *"
                                  + " From "
                                  + " HL_JUKUKANRI_社外教師マスタ "
                                  + " Where 教師コード = '" + code_教師コード + "'";

                    SqlCommand com = new SqlCommand(sql_up, sqlcon);
                    SqlDataReader reader = com.ExecuteReader();

                    reader.Read();

                    //該当項目に値を設定する
                    lbl_コード.Text = reader["教師コード"].ToString();
                    txt_名前.Text = reader["名前"].ToString();
                    txt_カタカナ.Text = reader["カタカナ"].ToString();
                    txt_ローマ字表記.Text = reader["ローマ字表記"].ToString();
                    cmb_性別.Text = reader["性別"].ToString();
                    dtp_生年月日.Value = (DateTime)reader["生年月日"];
                    txt_MyNumber.Text = reader["MyNumber"].ToString();
                    txt_MyNumber.Enabled = false;
                    txt_所属会社.Text = reader["所属会社"].ToString();

                    //Wang Qian start 2020/7/20
                    txt_郵便番号.Text = reader["郵便番号"].ToString().Substring(0,3);
                    txt_郵便番号4.Text= reader["郵便番号"].ToString().Substring(4);
                    //Wang Qian end 2020/7/20

                    txt_住所.Text = reader["住所"].ToString();
                    txt_携帯.Text = reader["携帯"].ToString();
                    txt_メール.Text = reader["メール"].ToString();
                    cmb_国籍.Text = reader["国籍"].ToString();
                    cmb_言語.Text = reader["メイン言語"].ToString();
                    cmb_職務.SelectedItem = reader["職務"].ToString().Trim();
                    dtp_入職日.Value = (DateTime)reader["入塾日"];

                    lbl_退職フラグ.Visible = true;
                    chk_退職.Visible = true;
                    btn_save.Location = new Point(lbl_退職フラグ.Location.X + 450, lbl_退職フラグ.Location.Y + 150);

                    if (!string.IsNullOrWhiteSpace(reader["退塾日"].ToString()))
                    {
                        chk_退職.Checked = true;
                        lbl_退職日.Visible = true;
                        dtp_退職日.Visible = true;
                        dtp_退職日.Value = (DateTime)reader["退塾日"];
                    }

                    txt_名前.Focus();
                    reader.Close();

                }
            }
            catch (Exception ex)
            {
                tsl_errMsg.ForeColor = Color.Red;
                tsl_errMsg.Text = string.Format("教師[{0}]の情報の取得に失敗しました。" + ex.Message, code_教師コード);
                return;
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
        /// 画面閉じる処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 社外教師情報_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isUpdate)
            {
                //一覧画面から渡したデータを削除
                if (((Form1)(this.Tag)).codeDic.ContainsKey(code_教師コード))
                {
                    ((Form1)(this.Tag)).codeDic.Remove(code_教師コード);
                }
                //Handleが０に初期化する
                ((Form1)(this.Tag)).m_社外教師情報変更Handle = IntPtr.Zero;
            }
            else
            {
                //Handleが０に初期化する
                ((Form1)(this.Tag)).m_社外教師情報登録Handle = IntPtr.Zero;
            }
        }

        /// <summary>
        /// 退職のCheckBoxを選択する画面設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chk_退職_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_退職.Checked)
            {
                lbl_退職日.Visible = true;
                lbl_退職日.Location = new Point(lbl_退職フラグ.Location.X, lbl_退職フラグ.Location.Y + 50);
                dtp_退職日.Visible = true;
                dtp_退職日.Location = new Point(lbl_退職日.Location.X + 120, lbl_退職日.Location.Y - 5);
                btn_save.Location = new Point(lbl_退職フラグ.Location.X + 450, lbl_退職フラグ.Location.Y + 150);
            }
            else
            {
                lbl_退職日.Visible = false;
                dtp_退職日.Visible = false;
                btn_save.Location = new Point(lbl_退職フラグ.Location.X + 450, lbl_退職フラグ.Location.Y + 150);
            }
        }

        /// <summary>
        /// 登録・更新処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_save_Click(object sender, EventArgs e)
        {
            //画面値取得
            code_教師コード = lbl_コード.Text;
            name_教師 = txt_名前.Text.Trim();
            katakana = txt_カタカナ.Text.Trim();
            romaji = txt_ローマ字表記.Text.Trim();
            sex = cmb_性別.Text;
            date_生年月日 = dtp_生年月日.Value;
            myNumber = txt_MyNumber.Text;
            company = txt_所属会社.Text.Trim();
            code_郵便 = txt_郵便番号.Text.Trim() + txt_郵便番号4.Text.Trim();
            adddress = txt_住所.Text.Trim();
            telPhone = txt_携帯.Text.Trim();
            mailAddress = txt_メール.Text.Trim();
            country = cmb_国籍.Text;
            language = cmb_言語.Text;
            admin = cmb_職務.Text;
            date_入職日 = dtp_入職日.Value;
          
            if (chk_退職.Checked)
            {
                date_退職日 = dtp_退職日.Value;
            }            
            //入力チェック
            if (!入力Check())
            {
                return;
            }

            //登録処理
            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库
            string date_入職日txt = date_入職日.ToString("yyyy-MM-dd");
            string date_退職日txt = chk_退職.Checked ? date_退職日.ToString("yyyy-MM-dd") : null;

            try
            {
                sqlcon.Open();
            }
            catch
            {
                tsl_errMsg.ForeColor = Color.Red;
                tsl_errMsg.Text = "サーバーがつなげない！インターネット接続をチェックしてください。";
                return;
            }

            SqlTransaction transaction = sqlcon.BeginTransaction();
            SqlCommand sqlcom = new SqlCommand();
            sqlcom.Connection = sqlcon;
            sqlcom.Transaction = transaction;

            try
            {
                //登録
                if (btn_save.Text == "登録")
                {
                    string y = DateTime.Now.Year.ToString();
                    string m = (DateTime.Now.Month + 5).ToString();
                    date_退職日txt = Convert.ToDateTime(y + "-" + m + "-1").AddDays(-1).ToString("yyyy-MM-dd");

                    //社外教師マスタ登録
                    string sqlCommand = @"Insert Into HL_JUKUKANRI_社外教師マスタ ("
                                      + " 教師コード "
                                      + " , 名前 "
                                      + " , カタカナ "
                                      + " , ローマ字表記 "
                                      + " , 性別 "
                                      + " , 生年月日 "
                                      + " , MyNumber "
                                      + " , 所属会社 "
                                      + " , 郵便番号 "
                                      + " , 住所 "
                                      + " , 携帯 "
                                      + " , メール "
                                      + " , 国籍 "
                                      + " , メイン言語 "
                                      + " , 入塾日 "
                                      + " , 退塾日 "
                                      + " , 職務) "
                                      + " Values ('{0}', N'{1}', N'{2}', '{3}', N'{4}', '{5}', '{6}', N'{7}' "
                                      + " , '{8}', N'{9}', '{10}', '{11}', N'{12}', N'{13}', '{14}',null,N'{15}')";

                    sqlcom.CommandText = string.Format(sqlCommand, code_教師コード, name_教師, katakana, romaji, sex, date_生年月日,
                        myNumber, company, code_郵便, adddress, telPhone, mailAddress, country, language, date_入職日txt, admin);

                    int result = sqlcom.ExecuteNonQuery();
                    if (result == 1)
                    {
                        result = 0;
                        //教師情報登録
                        sqlCommand = @"Insert Into HL_JUKUKANRI_教師情報 ("
                                   + " 教師コード "
                                   + " , 名前 "
                                   + " , 所属会社 "
                                   + " , メイン言語 "
                                   + " , クラスコード "
                                   + " , 入塾日 "
                                   + " , 退塾日 "
                                   + " , 職務) "
                                   + " Values ('{0}', N'{1}', N'{2}', N'{3}', null, '{4}', null ,N'{5}')";

                        sqlcom.CommandText = string.Format(sqlCommand, code_教師コード, name_教師, company, language, date_入職日txt, admin);

                        result = sqlcom.ExecuteNonQuery();

                        ((Form1)(this.Tag)).reLoad = false;
                        ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Green;
                        ((Form1)(this.Tag)).toolStripStatusLabel2.Text = string.Format(@"教師[{0}]の情報が正常に登録されました。", code_教師コード);
                        transaction.Commit();

                        確認ダイヤログ dlg = new 確認ダイヤログ();
                        dlg.ShowDialog();
                        if (dlg.result)
                        {
                            クラス管理 m_クラス管理 = new クラス管理();
                            m_クラス管理.isUpdate = "new";
                            m_クラス管理.cmb_教師コード.Enabled = false;
                            m_クラス管理.code_教師コード = code_教師コード;
                            m_クラス管理.Tag = ((Form1)(Tag));
                            m_クラス管理.Show(((Form1)(Tag)).dockPanel1);
                            ((Form1)(Tag)).m_クラス登録Handle = m_クラス管理.Handle;
                        }

                        if (((Form1)(this.Tag)).m_教師情報一覧Handle != null)
                        {
                            SendMessage(((Form1)(this.Tag)).m_教師情報一覧Handle, Form1.CUSTOM_MESSAGE, IntPtr.Zero, IntPtr.Zero);
                        }

                        this.Close();
                    }
                }

                //更新
                if (btn_save.Text == "更新")
                {
                    //社外教師マスタ更新
                    string sqlCommand = @"Update "
                                      + " HL_JUKUKANRI_社外教師マスタ "
                                      + " Set "
                                      + " 名前 = N'{1}' "
                                      + " , カタカナ = N'{2}' "
                                      + " , ローマ字表記 = '{3}' "
                                      + " , 性別 = '{4}' "
                                      + " , 生年月日 = '{5}' "
                                      + " , 所属会社 = N'{6}' "
                                      + " , 郵便番号 = '{7}' "
                                      + " , 住所 = N'{8}' "
                                      + " , 携帯 = '{9}' "
                                      + " , メール = '{10}' "
                                      + " , 国籍 = N'{11}' "
                                      + " , メイン言語 = N'{12}' "
                                      + " , 入塾日 = '{13}' "
                                      + " , 退塾日 = ";

                    sqlCommand += date_退職日txt == null ? "null " : @"'" + date_退職日txt + "' ";
                    sqlCommand += ", 職務 = '{14}' Where 教師コード = '{0}'";

                    sqlcom.CommandText = string.Format(sqlCommand, code_教師コード, name_教師, katakana, romaji, sex, date_生年月日,
                        company, code_郵便, adddress, telPhone, mailAddress, country, language, date_入職日txt, admin);

                    int result = sqlcom.ExecuteNonQuery();
                    if (result == 1)
                    {
                        //教師情報更新
                        sqlCommand = @"Update "
                                   + " HL_JUKUKANRI_教師情報 "
                                   + " Set "
                                   + " 名前 = N'{1}' "
                                   + " , 所属会社 = N'{2}' "
                                   + " , メイン言語 =N'{3}' "
                                   + " , 入塾日 = '{4}' "
                                   + " , 退塾日 = ";

                        sqlCommand += date_退職日txt == null ? "null " : @"'" + date_退職日txt + "' ";
                        sqlCommand += ", 職務 = N'{5}' Where 教師コード = '{0}'";

                        sqlcom.CommandText = string.Format(sqlCommand, code_教師コード, name_教師, company, language, date_入職日txt, admin);

                        result = sqlcom.ExecuteNonQuery();
                        if (result != 1)
                        {
                            throw new Exception();
                        }

                        ((Form1)(this.Tag)).reLoad = false;
                        ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Green;
                        ((Form1)(this.Tag)).toolStripStatusLabel2.Text = string.Format(@"教師[{0}]の情報が正常に更新されました。", code_教師コード);
                        transaction.Commit();

                        //一覧画面にデータ更新MSGを送る
                        if (((Form1)(this.Tag)).m_教師情報一覧Handle != null)
                        {
                            SendMessage(((Form1)(this.Tag)).m_教師情報一覧Handle, Form1.CUSTOM_MESSAGE, IntPtr.Zero, IntPtr.Zero);
                        }

                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                tsl_errMsg.ForeColor = Color.Red;
                tsl_errMsg.Text = "教師情報の処理が失敗しました。" + ex.Message;
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

        /// <summary>
        /// 入力チェック
        /// </summary>
        private bool 入力Check()
        {
            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库

            try
            {
                sqlcon.Open();
            }
            catch
            {
                tsl_errMsg.ForeColor = Color.Red;
                tsl_errMsg.Text = "サーバーがつなげない！インターネット接続をチェックしてください。";
                return false;
            }

            //教師コード
            if (string.IsNullOrWhiteSpace(lbl_コード.Text))
            {
                //必須
                tsl_errMsg.ForeColor = Color.Red;
                tsl_errMsg.Text = string.Format(@"エラー：{0}が必須です.", lbl_教師コード.Text);
                return false;
            }
            else
            {
                //存在チェック
                string sqlcmd = string.Format(@"Select * From HL_JUKUKANRI_社外教師マスタ Where 教師コード = '{0}'", code_教師コード);
                SqlCommand sqlcom = new SqlCommand(sqlcmd, sqlcon);

                try
                {
                    SqlDataReader reader = sqlcom.ExecuteReader();
                    if (isUpdate && !reader.HasRows)
                    {
                        //更新：教師コードが存在しない場合エラー
                        tsl_errMsg.ForeColor = Color.Red;
                        tsl_errMsg.Text = string.Format(@"エラー：{0}[{1}]が登録されていません.", lbl_教師コード.Text, code_教師コード);
                        return false;
                    }
                    else if (!isUpdate && reader.HasRows)
                    {
                        //新規：教師コードがある場合エラー
                        tsl_errMsg.ForeColor = Color.Red;
                        tsl_errMsg.Text = string.Format(@"エラー：{0}[{1}]が既に登録されています.", lbl_教師コード.Text, code_教師コード);
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    tsl_errMsg.ForeColor = Color.Red;
                    tsl_errMsg.Text = "教師コードのチェック処理にエラーが発生しました。" + ex.Message;

                    if (sqlcon != null)
                    {
                        sqlcon.Close();
                    }

                    txt_名前.Focus();
                    return false;
                }
            }

            //名前チェック
            if (name_教師.Equals(""))
            {
                tsl_errMsg.ForeColor = Color.Red;
                tsl_errMsg.Text = "名前未入力!";
                return false;
            }
            else
            {
                if (name_教師.IndexOf(" ") > 0)
                {
                    tsl_errMsg.ForeColor = Color.Red;
                    tsl_errMsg.Text = "名前に許可されない文字'半角SPACE' が入りました。";
                    return false;
                }
                if (name_教師.IndexOf("　") == -1)
                {
                    tsl_errMsg.ForeColor = Color.Red;
                    tsl_errMsg.Text = "姓と名の間に '全角SPACE' を挿入してください!";
                    return false;
                }
                if (name_教師.IndexOf("　") != txt_名前.Text.LastIndexOf("　"))
                {
                    tsl_errMsg.ForeColor = Color.Red;
                    tsl_errMsg.Text = "名前に '全角SPACE' は二つ以上入力しないでください!";
                    return false;
                }
                if (name_教師.IndexOf(",") > 0)
                {
                    tsl_errMsg.ForeColor = Color.Red;
                    tsl_errMsg.Text = "名前に許可されない文字「,」が入りました。";
                    return false;
                }
            }
            
            //カタカナチェック
            if (katakana.Equals(""))
            {
                tsl_errMsg.ForeColor = Color.Red;
                tsl_errMsg.Text = "カタカナが未入力!";
                return false;
            }
            else
            {
                if (katakana.IndexOf(" ") > 0)
                {
                    tsl_errMsg.ForeColor = Color.Red;
                    tsl_errMsg.Text = "カタカナに許可されない文字'半角SPACE' が入りました。";
                    return false;
                }
                if (katakana.IndexOf("　") == -1)
                {
                    tsl_errMsg.ForeColor = Color.Red;
                    tsl_errMsg.Text = "カタカナの姓と名の間に '全角SPACE' を挿入してください!";
                    return false;
                }
                if (katakana.IndexOf("　") != katakana.LastIndexOf("　"))
                {
                    tsl_errMsg.ForeColor = Color.Red;
                    tsl_errMsg.Text = "カタカナに '全角SPACE' は二つ以上入力しないでください!";
                    return false;
                }
                if (katakana.IndexOf(",") > 0)
                {
                    tsl_errMsg.ForeColor = Color.Red;
                    tsl_errMsg.Text = "カタカナに許可されない文字「,」が入りました。";
                    return false;
                }
                if (!ComClass.IsFullKatakana(katakana.Replace("　", "")))
                {
                    tsl_errMsg.ForeColor = Color.Red;
                    tsl_errMsg.Text = "カタカナは全角カタカナでご入力ください！";
                    return false;
                }
            }

            //ローマ字表記チェック
            if (romaji.Equals(""))
            {
                tsl_errMsg.ForeColor = Color.Red;
                tsl_errMsg.Text = "ローマ字表記が未入力!";
                return false;
            }
            else
            {
                if (romaji.IndexOf(",") > 0)
                {
                    tsl_errMsg.ForeColor = Color.Red;
                    tsl_errMsg.Text = "ローマ字表記に許可されない文字「,」が入りました。";
                    return false;
                }
                //20200707 add ou
                if (!Isromaji(this.romaji))
                {
                    tsl_errMsg.ForeColor = Color.Red;
                    tsl_errMsg.Text = "ローマ字が間違っています!";
                    return false;
                }
            }

            if (!isUpdate)
            {
                //MyNumberチェック
                if (myNumber.Equals(""))
                {
                    tsl_errMsg.ForeColor = Color.Red;
                    tsl_errMsg.Text = "MyNumberが未入力!";
                    return false;
                }
                else
                {
                    if (!ValidateMyNumber(myNumber))
                    {
                        tsl_errMsg.ForeColor = Color.Red;
                        tsl_errMsg.Text = "不正なMyNumber!MyNumberを再入力してください！";
                        return false;
                    }
                    else
                    {
                        SqlConnection conn = new SqlConnection(connectionString); //连接数据库

                        try
                        {
                            conn.Open();
                        }
                        catch
                        {
                            tsl_errMsg.ForeColor = Color.Red;
                            tsl_errMsg.Text = "サーバーがつなげない！インターネット接続をチェックしてください。";
                            return false;
                        }

                        string str_sqlcmd = string.Format(@"Select * From HL_JUKUKANRI_社外教師マスタ Where MyNumber = '{0}'", myNumber);
                        SqlCommand com = new SqlCommand(str_sqlcmd, conn);
                        SqlDataReader reader = com.ExecuteReader();

                        if (reader.Read())
                        {
                            tsl_errMsg.ForeColor = Color.Red;
                            tsl_errMsg.Text = "このMyNumberはすでに登録済みです！";
                            return false;
                        }

                        reader.Close();
                        conn.Close();
                    }
                }
            }

            //所属会社チェック
            if (company.Equals(""))
            {
                tsl_errMsg.ForeColor = Color.Red;
                tsl_errMsg.Text = "所属会社が未入力!";
                return false;
            }

            //郵便番号チェック
            if (code_郵便.Equals(""))
            {
                tsl_errMsg.ForeColor = Color.Red;
                tsl_errMsg.Text = "郵便番号が未入力!";
                return false;
            }
            else
            {
                if (!Is郵便番号(code_郵便))
                {
                    tsl_errMsg.ForeColor = Color.Red;
                    tsl_errMsg.Text = "郵便番号が間違っています!";
                    return false;
                }
            }

            //住所チェック
            if (adddress.Equals(""))
            {
                tsl_errMsg.ForeColor = Color.Red;
                tsl_errMsg.Text = "住所が未入力!";
                return false;
            }

            //携帯チェック
            if (telPhone.Equals(""))
            {
                tsl_errMsg.ForeColor = Color.Red;
                tsl_errMsg.Text = "携帯が未入力!";
                return false;
            }
            else
            {
                if (telPhone.IndexOf(",") > 0)
                {
                    tsl_errMsg.ForeColor = Color.Red;
                    tsl_errMsg.Text = "携帯に許可されない文字「,」が入りました。";
                    return false;
                }
                if (!ISTEL(telPhone))
                {
                    tsl_errMsg.ForeColor = Color.Red;
                    tsl_errMsg.Text = "携帯のフォーマットが間違っています!";
                    return false;
                }
            }

            //メールチェック
            if (mailAddress.Equals(""))
            {
                tsl_errMsg.ForeColor = Color.Red;
                tsl_errMsg.Text = "メールが未入力!";
                return false;
            }
            else
            {
                if (mailAddress.IndexOf(",") > 0)
                {
                    tsl_errMsg.ForeColor = Color.Red;
                    tsl_errMsg.Text = "メールに許可されない文字「,」が入りました。";
                    return false;
                }
                if (!IsValidEmail(mailAddress))
                {
                    tsl_errMsg.ForeColor = Color.Red;
                    tsl_errMsg.Text = "メールのフォーマットが間違っています!";
                    return false;
                }
            }

            //国籍
            if (cmb_国籍.SelectedIndex == -1)
            {
                tsl_errMsg.ForeColor = Color.Red;
                tsl_errMsg.Text = "国籍を選んでください!";
                return false;
            }

            //メイン言語
            if (cmb_言語.SelectedIndex == -1)
            {
                tsl_errMsg.ForeColor = Color.Red;
                tsl_errMsg.Text = "メイン言語を選んでください!";
                return false;
            }

            //開始日 < 終了日
            if (isUpdate)
            {
                if (chk_退職.Checked == true)
                {
                    if (dtp_入職日.Value > dtp_退職日.Value)
                    {
                        tsl_errMsg.ForeColor = Color.Red;
                        tsl_errMsg.Text = "[終了日]は[開始日]以後の日を設定してください。";
                        return false;
                    }
                }
            }

            tsl_errMsg.ForeColor = Color.Black;
            tsl_errMsg.Text = "";
            return true;
        }

        private bool ValidateMyNumber(string mynumber)
        {
            //12文字でなければ偽
            if (mynumber.Length != 12)
                return false;

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
        /// 郵便番号の形式チェック
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        private bool Is郵便番号(string strIn)
        {
            return Regex.IsMatch(strIn, @"\d{7}|\d{3}-\d{4}");
        }

        /// <summary>
        /// 携帯電話の形式チェック
        /// </summary>
        /// <param name="str_url"></param>
        /// <returns></returns>
        private bool ISTEL(string str_url)
        {
            return Regex.IsMatch(str_url, @"\A0\d{1,4}-\d{1,4}-\d{4}\z");
        }

        /// <summary>
        /// メールの形式チェック
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        private bool IsValidEmail(string strIn)
        {
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)" + @"|(([\w-]+\.)+))([a-zA-Z]{2,5}|[0-9]{1,3})(\]?)$");
        }

        // 20200707 add ou
        /// <summary>
        /// ローマ字チェック
        /// </summary>
        private bool Isromaji(string strIn)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(strIn, "^[ A-Za-z]+$");
        }

        /// <summary>
        /// 「必須」画面設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
    }
}