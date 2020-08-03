using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace HL_塾管理
{
    public partial class クラス管理 : DockContent
    {
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        private AutoSizeFormClass asc = new AutoSizeFormClass();

        private string connectionString = ComClass.connectionString;

        private Dictionary<string, string> errmsg = new Dictionary<string, string>();
        //更新区分
        public string isUpdate = "new";
        public string code_クラスコード = "";
        public string code_教師コード = "";
        public string code_教室コード = "";
        public string code_学生 = "";
        private string usercheck = "";
        public string 研修フラグ = "";
        public DateTime 入塾日_教師;

        private DataGridViewSelectedRowCollection sourceRowCollection = null;

        Dictionary<string, string> list_学生 = new Dictionary<string, string>();
        Dictionary<string, string> classdate = new Dictionary<string, string>();
        public delegate void UpdateDelegate(object sender, UpdateEventArgs args);
        public event UpdateDelegate UpdateEventHandler;
        public bool isFirst = true;

        public クラス管理()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ユーザーの職務チェック
        /// </summary>
        private void Usercheck()
        {
            if (((Form1)(Tag)).m_ユーザ登録.m_ログイン_職務 == "管理者")
            {
                usercheck = "admin";
            }

            if (((Form1)(Tag)).m_ユーザ登録.m_ログイン_職務 == "一般ユーザ")
            {
                usercheck = "user";
            }
        }

        private void Page_load(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);
            //職務チェック
            Usercheck();

            //liuxiaoyan 0527 start
            Displayright();

            //クラスコード、教室コードリスト設定
            SetClassInfo();

            if (isUpdate == "new")
            {
                if (usercheck == "user")
                {
                    cmb_教師コード.Enabled = false;
                    lbl_教師.Text = lbl_教師.Text.Replace("[必須]", "");
                }
 
                cmb_教師コード.Text = string.IsNullOrWhiteSpace(code_教師コード) ? "" : code_教師コード;
                //月初 月末設定
                dtp_開始日.Value = dtp_開始日.Value.AddDays((dtp_開始日.Value.Day - 1) * -1);
                dtp_終了日.Value = dtp_開始日.Value.AddMonths(4);
                dtp_終了日.Value = new DateTime(dtp_終了日.Value.Year, dtp_終了日.Value.Month, DateTime.DaysInMonth(dtp_終了日.Value.Year, dtp_終了日.Value.Month));
            }
            else
            {
                if (code_教師コード != ((Form1)(Tag)).m_ユーザ登録.m_ログイン_教師コード && usercheck == "user")
                {
                    txt_classname.Enabled = false;
                    cmb_教室コード.Enabled = false;
                    cmb_教師コード.Enabled = false;
                    cmb_課程.Enabled = false;
                    btn_add.Enabled = false;
                    btn_delete.Enabled = false;
                    dgv_left.Enabled = false;
                    dgv_right.Enabled = false;
                    dtp_開始日.Enabled = false;
                    dtp_終了日.Enabled = false;
                    btn_insert.Enabled = false;

                    lbl_classname.Text = lbl_classname.Text.Replace("[必須]", "");
                    lbl_教室コード.Text = lbl_教室コード.Text.Replace("[必須]", "");
                    lbl_教師.Text = lbl_教師.Text.Replace("[必須]", "");
                    lbl_言語.Text = lbl_言語.Text.Replace("[必須]", "");
                    lbl_学生.Text = lbl_学生.Text.Replace("[必須]", "");
                    lbl_開始日.Text = lbl_開始日.Text.Replace("[必須]", "");
                    lbl_終了日.Text = lbl_終了日.Text.Replace("[必須]", "");
                }
                else if (code_教師コード == ((Form1)(Tag)).m_ユーザ登録.m_ログイン_教師コード && usercheck == "user")
                {
                    cmb_教師コード.Enabled = false;
                    lbl_教師.Text = lbl_教師.Text.Replace("[必須]", "");
                }

                chkbx_研修.Checked = 研修フラグ == "True";
                chkbx_研修.Enabled = false;

                Displayleft();
                btn_insert.Text = "更新";
                Text = "クラス情報変更";
            }
        }

        /// <summary>
        /// 教室コードリスト,教師コード設定
        /// </summary>
        public void SetClassInfo()
        {
            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库

            try
            {
                sqlcon.Open();
            }
            catch
            {
                ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(Tag)).toolStripStatusLabel2.Text = "DBサーバーの接続に失敗しました。";
                return;
            }

            string str_sqlcmd = "";
            DataTable dt = new DataTable();

            if (cmb_教室コード.Items.Count == 0)
            {
                //教室コード設定
                str_sqlcmd = @"SELECT 教室コード, 備考 FROM HL_JUKUKANRI_教室マスタ ORDER BY 教室コード";
                SqlDataAdapter sqlDa = new SqlDataAdapter(str_sqlcmd, sqlcon);
                sqlDa.Fill(dt);

                cmb_教室コード.DisplayMember = "備考";
                cmb_教室コード.ValueMember = "教室コード";
                cmb_教室コード.DataSource = dt;

                if (code_教室コード != "")
                {
                    cmb_教室コード.SelectedValue = code_教室コード;
                }
            }

            //教師コードがcmb_教師コードに設定
            //add Linh 20200514 ↓
            DataTable dt2 = new DataTable();
            
            if (cmb_教師コード.Items.Count == 0)
            {
                //教師コード設定

                if (isUpdate == "new" && usercheck == "user")
                {
                    str_sqlcmd = string.Format(@"SELECT 教師コード,(教師コード + '　' + 名前) as 名前 FROM HL_JUKUKANRI_教師情報 WHERE 教師コード = '{0}'", ((Form1)(Tag)).m_ユーザ登録.m_ログイン_教師コード);
                }
                else
                {
                    str_sqlcmd= "SELECT 教師コード,(教師コード + '　' + 名前) as 名前 FROM HL_JUKUKANRI_教師情報 ORDER BY 教師コード";
                }

                SqlDataAdapter sqlDa = new SqlDataAdapter(str_sqlcmd, sqlcon);
                sqlDa.Fill(dt2);
                sqlDa.Dispose();

                cmb_教師コード.DisplayMember = "名前";
                cmb_教師コード.ValueMember = "教師コード";
                cmb_教師コード.DataSource = dt2;

            }
            //add Linh 20200514 ↑

            //クラスコード設定
            if (isUpdate == "new")
            {
                str_sqlcmd = @"select ident_current('HL_JUKUKANRI_クラス履歴')+1 as クラスコード ";
                SqlDataAdapter sqlDa = new SqlDataAdapter(str_sqlcmd, sqlcon);
                dt = new DataTable();
                sqlDa.Fill(dt);
                txt_クラスコード.Text = dt.Rows[0]["クラスコード"].ToString();

            }
            else
            {
                txt_クラスコード.Text = code_クラスコード;

                //add by Linh 20200519　↓
                str_sqlcmd = string.Format(@"Select
                                                a.クラスコード,
                                                a.クラス名,
                                                a.教室コード,
                                                a.教師コード,
                                                (Case When b.名前 is null then c.名前 else b.名前 end) as 名前,
                                                a.課程,
                                                a.学生コード,
                                                a.開始日,
                                                a.終了日
                                            From
                                                HL_JUKUKANRI_クラス履歴 a
                                                Left Join
                                                    HL_JUKUKANRI_社外教師マスタ b
                                                    on	a.教師コード = b.教師コード
                                                Left Join
                                                    HL_JUKUKANRI_社内社員教師マスタ c
                                                    on	a.教師コード = c.社員コード
                                            Where a.クラスコード = {0}", code_クラスコード);
                //add by Linh 20200519　↑

                SqlDataAdapter sqlDa = new SqlDataAdapter(str_sqlcmd, sqlcon);
                dt = new DataTable();
                sqlDa.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                    ((Form1)(Tag)).toolStripStatusLabel2.Text = "クラス[{0}]情報の取得に失敗しました。";
                }
                else
                {
                    txt_classname.Text = dt.Rows[0]["クラス名"].ToString();
                    cmb_教師コード.SelectedValue = dt.Rows[0]["教師コード"].ToString();
                    //code_教師コード = txt_教師名前.Text;
                    string list_student = dt.Rows[0]["学生コード"].ToString();
                    code_学生 = dt.Rows[0]["学生コード"].ToString();

                    if (!string.IsNullOrWhiteSpace(list_student))
                    {
                        list_student = "'" + list_student.Replace(",", "','") + "'";
                        str_sqlcmd = string.Format(@"Select 学生コード, 名前  From HL_JUKUKANRI_学生マスタ Where 学生コード in ({0})", list_student);
                        sqlDa = new SqlDataAdapter(str_sqlcmd, sqlcon);
                        DataTable dt_student = new DataTable();
                        sqlDa.Fill(dt_student);
                    }

                    cmb_教室コード.SelectedValue = dt.Rows[0]["教室コード"].ToString();
                    cmb_課程.Text = dt.Rows[0]["課程"].ToString();
                    dtp_開始日.Text = dt.Rows[0]["開始日"].ToString();
                    dtp_終了日.Text = dt.Rows[0]["終了日"].ToString();
                }
            }
            if (sqlcon != null)
            {
                sqlcon.Close();
            }
        }
        //教室コードチェック
        private bool Classroomcheck()
        {
            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库

            try
            {
                sqlcon.Open();
            }
            catch
            {
                ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(Tag)).toolStripStatusLabel2.Text = "DBサーバーの接続に失敗しました.";
                return false;
            }
            string classroomcode = cmb_教室コード.SelectedValue.ToString();
            string classID = txt_クラスコード.Text;

            try
            {
                string message = "";
                string sql = "";
                if (isUpdate == "new")
                {
                    sql = string.Format(@"select
                                *
                            from
                                HL_JUKUKANRI_クラス履歴
                            where
                                教室コード = '{0}'
                            and 有効 ='1'
                            and 開始日 is not null
                            and 終了日 is not null
                            order by 開始日", classroomcode);
                }
                else
                {
                    sql = string.Format(@"select
                                *
                            from
                                HL_JUKUKANRI_クラス履歴
                            where
                                教室コード = '{0}'
                            and 有効 ='1'
                            and クラスコード != '{1}'
                            and 開始日 is not null
                            and 終了日 is not null
                            order by 開始日", classroomcode, classID);

                }

                SqlDataAdapter sqlDa1 = new SqlDataAdapter(sql, sqlcon);
                DataTable dt1 = new DataTable();
                sqlDa1.Fill(dt1);
                sqlcon.Close();

                //新規
                if (dt1.Rows.Count < 1)
                {
                    return true;
                }
                else
                {
                    // Linh 20200609 クラス期間チェック  start
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        DateTime dts1 = Convert.ToDateTime(dt1.Rows[i]["開始日"].ToString());
                        DateTime dts2 = Convert.ToDateTime(dt1.Rows[i]["終了日"].ToString());

                        //期間内禁止
                        if (dtp_開始日.Value >= dts1 && dtp_開始日.Value <= dts2 ||
                           dtp_終了日.Value >= dts1 && dtp_終了日.Value <= dts2 ||
                           dtp_開始日.Value <= dts1 && dtp_終了日.Value >= dts2)
                        {
                            message = string.Format("教室[{0}]が[{1}～{2}]の期間内にクラス[{3}({4})]が既に使っているため、教室 または 日付を変更してください。"
                                                    , cmb_教室コード.Text, dt1.Rows[i]["開始日"].ToString(), dt1.Rows[i]["終了日"].ToString(), dt1.Rows[i]["クラス名"].ToString(), dt1.Rows[i]["クラスコード"].ToString());
                            break;
                        }
                    }

                    if (message != "")
                    {
                        ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                        ((Form1)(Tag)).toolStripStatusLabel2.Text = message;
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(Tag)).toolStripStatusLabel2.Text = ex.ToString();
                return false;
            }
            // Linh 20200609 クラス期間チェック  end
        }

        private bool Teachercheck()
        {
            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库

            try
            {
                sqlcon.Open();
            }
            catch
            {
                ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(Tag)).toolStripStatusLabel2.Text = "DBサーバーの接続に失敗しました.";
                return false;
            }
            string date_終了日 = dtp_終了日.Value.ToString("yyyy-MM-dd");
            string date_開始日 = dtp_開始日.Value.ToString("yyyy-MM-dd");
            string 教師コード = cmb_教師コード.SelectedValue.ToString();
            string message = "";
            string sql = "";
            if (isUpdate == "new")
            {
                sql = string.Format(@"SELECT
                                *
                            FROM
                                HL_JUKUKANRI_クラス履歴
                            WHERE
                                教師コード = '{0}'
                            and 有効 ='1'
                            and 開始日 is not null
                            and 終了日 is not null
                            order by
                                開始日", 教師コード);
            }
            else
            {
                sql = string.Format(@"SELECT
                                *
                            FROM
                                HL_JUKUKANRI_クラス履歴
                            WHERE
                                教師コード = '{0}'
                            and クラスコード != '{1}'
                            and 有効 ='1'
                            and 開始日 is not null
                            and 終了日 is not null
                            order by
                                開始日", 教師コード, txt_クラスコード.Text);
            }
            try
            {
                SqlDataAdapter sqlDa1 = new SqlDataAdapter(sql, sqlcon);
                DataTable dt1 = new DataTable();
                sqlDa1.Fill(dt1);
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    DateTime dts1 = Convert.ToDateTime(dt1.Rows[i]["開始日"].ToString());
                    DateTime dts2 = Convert.ToDateTime(dt1.Rows[i]["終了日"].ToString());

                    //期間内禁止
                    if (dtp_開始日.Value >= dts1 && dtp_開始日.Value <= dts2 ||
                        dtp_終了日.Value >= dts1 && dtp_終了日.Value <= dts2 ||
                        dtp_開始日.Value <= dts1 && dtp_終了日.Value >= dts2)
                    {
                        message = string.Format("教師[{0}]が[{1}～{2}]の期間内にクラス[{3}({4})]があるため登録できません。教師 または 日付を変更してください。"
                                , cmb_教師コード.Text, dt1.Rows[i]["開始日"].ToString(), dt1.Rows[i]["終了日"].ToString(), dt1.Rows[i]["クラス名"].ToString(), dt1.Rows[i]["クラスコード"].ToString());
                        break;
                    }
                }

                if (message != "")
                {
                    ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                    ((Form1)(Tag)).toolStripStatusLabel2.Text = message;
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(Tag)).toolStripStatusLabel2.Text = ex.ToString();
                return false;
            }
            // 20200610 Linh　教師の期間チェック  end
        }

        /// <summary>
        /// 入力チェック
        /// </summary>
        public bool InputCheck()
        {
            errmsg = new Dictionary<string, string>();
            cmb_教室コード.BackColor = System.Drawing.Color.Empty;
            cmb_教師コード.BackColor = System.Drawing.Color.Empty;// add Linh 20200514
            cmb_課程.BackColor = System.Drawing.Color.Empty;

            //クラス名
            if (string.IsNullOrWhiteSpace(txt_classname.Text))
            {
                AddErrMsgList(lbl_classname.Text, "[クラス名]を入力してください。");
                return false;
            }
            //教室
            if (string.IsNullOrWhiteSpace(cmb_教室コード.Text))
            {
                AddErrMsgList(lbl_教室コード.Text, "[教室]が選択されていません。");
                return false;
            }
            //教師コード
            // add cmb_教師コード by Linh 20200514 ↓
            if (string.IsNullOrWhiteSpace(cmb_教師コード.Text))
            {
                AddErrMsgList(lbl_教師.Text, "教師が入力されていません。");
                return false;
            }
            else
            {
                //存在チェック
                if (!IsExsitedWith教師コード(cmb_教師コード.Text.ToString()))
                {
                    return false;
                }
            }
            // add cmb_教師コード by Linh 20200514 ↑

            //言語
            if (string.IsNullOrWhiteSpace(cmb_課程.Text))
            {
                AddErrMsgList(lbl_言語.Text, "[言語]を入力してください。");
                return false;
            }

            //学生
            if (dgv_left.Rows.Count == 0)
            {
                AddErrMsgList(lbl_学生.Text, "学生少なくても1名が必要です。");
                return false;
            }

            //開始日 < 終了日
            if (dtp_開始日.Value < DateTime.Now.Date)
            {
                AddErrMsgList(lbl_終了日.Text, "[開始日]は過去日に設定できません。");
                return false;
            }

            if (dtp_開始日.Value > dtp_終了日.Value)
            {
                AddErrMsgList(lbl_終了日.Text, "[終了日]は[開始日]以後の日を設定してください。");
                return false;
            }

            return true;
        }

        public static bool IsInt(string value)
        {
            return Regex.IsMatch(value, @"^[0-9]+$");
        }

        private bool IsExsitedWith教師コード(string code_教師コード)
        {
            bool result = true;
            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库
            try
            {
                sqlcon.Open();
            }
            catch
            {
                AddErrMsgList(lbl_教師.Text, "[教師コード]在チェック処理]:DBサーバーの接続に失敗しました。");
                return false;
            }

            string str_sqlcmd = "";
            //社外教師の場合
            if (code_教師コード.Substring(0, 1) == "K")
            {
                str_sqlcmd = string.Format(@"SELECT 教師コード FROM HL_JUKUKANRI_社外教師マスタ WHERE 教師コード = '{0}'", code_教師コード);
            }
            else
            {
                str_sqlcmd = string.Format(@"SELECT 社員コード, 待機状態 FROM HL_JINJI_社員在職状態 WHERE 社員コード = '{0}'", code_教師コード);
            }

            SqlCommand com = new SqlCommand(str_sqlcmd, sqlcon);
            SqlDataReader reader = com.ExecuteReader();

            if (!reader.Read())
            {
                AddErrMsgList(lbl_教師.Text, string.Format("[教師コード]({0})が存在していません。", code_教師コード));
                result = false;
            }
            else
            {
                if (code_教師コード.Substring(0, 1) != "K")
                {
                    if (reader["待機状態"].ToString() != "待機")
                    {
                        AddErrMsgList(lbl_教師.Text, string.Format("[教師コード]({0})が待機状態ではないため、登録できません。[社員から教師へ変更]を参照してください。", code_教師コード));
                        result = false;
                    }
                }
            }

            if (sqlcon != null)
            {
                sqlcon.Close();
                reader.Close();
            }

            return result;
        }

        public class UpdateEventArgs : EventArgs
        {
            public string Data { get; set; }
        }

        protected void raiseUpdate()
        {
            UpdateEventArgs args = new UpdateEventArgs();
            UpdateEventHandler?.Invoke(this, args);
        }

        /// <summary>
        /// 登録ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_insert_Click(object sender, EventArgs e)
        {
            if (dgv_left.Rows.Count > 0)
            {
                list_学生.Clear();
                for (int i = 0; i < dgv_left.Rows.Count; i++)
                {
                    string name = dgv_left.Rows[i].Cells["leftname"].Value.ToString();
                    name = name + "\r\n";
                    list_学生.Add(dgv_left.Rows[i].Cells["leftcode"].Value.ToString(), name);
                }
            }
            //入力チェック
            if (!InputCheck())
            {
                return;
            }
            if (!chkbx_研修.Checked)
            {
                if (!Classroomcheck())
                {
                    return;
                }
                if (!Teachercheck())
                {
                    return;
                }
            }

            if (isUpdate == "new")
            {
                if (dgv_left.Rows.Count == 0)
                {
                    ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                    ((Form1)(Tag)).toolStripStatusLabel2.Text = "学生少なくても1名が必要です。";
                    return;
                }
                else
                {
                    InsertData();
                }
            }
            else
            {
                UpdateData();
            }
            raiseUpdate();
        }

        private void InsertData()
        {
            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库

            int result = 0;
            try
            {
                sqlcon.Open();
            }
            catch
            {
                ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(Tag)).toolStripStatusLabel2.Text = "DBサーバーの接続に失敗しました。";
                return;
            }
            SqlTransaction transaction = sqlcon.BeginTransaction();

            SqlCommand sqlcom = new SqlCommand();
            sqlcom.Connection = sqlcon;
            sqlcom.Transaction = transaction;

            try
            {
                string classname = txt_classname.Text;
                code_教室コード = cmb_教室コード.SelectedValue.ToString();
                code_教師コード = cmb_教師コード.SelectedValue.ToString() == null ? null : cmb_教師コード.SelectedValue.ToString();
                string txt_課程 = cmb_課程.Text;
                string txt_学生 = "";
                string txt_学生名 = "";
                if (list_学生.Count > 0)
                {
                    foreach (string key in list_学生.Keys)
                    {
                        txt_学生 += key + ",";
                    }
                    foreach (string StudentName in list_学生.Values)
                    {
                        txt_学生名 += StudentName + "\r\n";
                    }
                    txt_学生 = txt_学生.Substring(0, txt_学生.Length - 1);
                }
                string check_研修 = chkbx_研修.Checked ? "TRUE" : "FALSE";
                string date_開始日 = dtp_開始日.Value.ToString("yyyy-MM-dd");
                string date_終了日 = dtp_終了日.Value.ToString("yyyy-MM-dd");

                //登録行う
                sqlcom.CommandText = string.Format(@"Insert Into HL_JUKUKANRI_クラス履歴 ([クラス名], [教室コード], [教師コード], [課程], [学生コード], [開始日], [終了日], [研修フラグ], [有効]) Values (N'{0}', '{1}', '{2}',N'{3}', '{4}', '{5}', '{6}', '{7}', 1)",
                                                 classname, code_教室コード, code_教師コード, txt_課程, txt_学生, date_開始日, date_終了日, check_研修);

                result = sqlcom.ExecuteNonQuery();

                if (result == 1)
                {
                    sqlcom.CommandText = string.Format(@"Update HL_JUKUKANRI_教師情報 Set クラスコード = クラスコード + ',' + '{0}' Where 教師コード = '{1}'", txt_クラスコード.Text, code_教師コード);
                    result = sqlcom.ExecuteNonQuery();

                    if (result != 1)
                    {
                        throw new Exception();
                    }

                    if (!string.IsNullOrWhiteSpace(txt_学生))
                    {
                        txt_学生 = "'" + txt_学生.Replace(",", "','") + "'";
                        sqlcom.CommandText = string.Format(@"Update HL_JUKUKANRI_学生クラス Set クラスコード = '{0}' Where 学生コード in ({1})", txt_クラスコード.Text, txt_学生);
                        result = sqlcom.ExecuteNonQuery();
                    }

                    if (result > 0)
                    {
                        transaction.Commit();
                        ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Green;
                        ((Form1)(Tag)).toolStripStatusLabel2.Text = string.Format("クラス[{0}]正常に登録されました。", txt_クラスコード.Text);
                        ((Form1)(Tag)).reLoad = false;

                        Close();
                    }
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(Tag)).toolStripStatusLabel2.Text = "登録処理が失敗しました。" + ex.ToString();
            }
            finally
            {
                if(result > 0)
                {
                    if (((Form1)(Tag)).m_クラス参照Handle != null)
                    {
                        SendMessage(((Form1)(Tag)).m_クラス参照Handle, Form1.CUSTOM_MESSAGE, IntPtr.Zero, IntPtr.Zero);
                    }

                    if (((Form1)(Tag)).m_学生情報一覧Handle != null)
                    {
                        SendMessage(((Form1)(Tag)).m_学生情報一覧Handle, Form1.CUSTOM_MESSAGE, IntPtr.Zero, IntPtr.Zero);
                    }
                }
                if (sqlcon != null)
                {
                    sqlcon.Close();
                }
            }
        }

        private void UpdateData()
        {

            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库

            int result = 0;
            try
            {
                sqlcon.Open();
            }
            catch
            {
                ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(Tag)).toolStripStatusLabel2.Text = "DBサーバーの接続に失敗しました。";
                return;
            }
            SqlTransaction transaction = sqlcon.BeginTransaction();
            SqlCommand sqlcom = new SqlCommand();
            sqlcom.Connection = sqlcon;
            sqlcom.Transaction = transaction;


            try
            {
                string classname = txt_classname.Text;
                code_教室コード = cmb_教室コード.SelectedValue.ToString();
                code_教師コード = cmb_教師コード.SelectedValue.ToString() == null ? null : cmb_教師コード.SelectedValue.ToString();
                string txt_課程 = cmb_課程.Text;
                string txt_学生 = "";
                string txt_学生List = "";
                if (list_学生.Count > 0)
                {
                    foreach (string key in list_学生.Keys)
                    {
                        txt_学生 += key + ",";
                    }

                    txt_学生 = txt_学生.Substring(0, txt_学生.Length - 1);
                    txt_学生List = "'" + txt_学生.Replace(",", "','") + "'";
                }

                string str_sqlcmd = string.Format(@"Select Count(*) as Count From HL_JUKUKANRI_学生クラス Where クラスコード = {0} ", code_クラスコード);
                if (!string.IsNullOrWhiteSpace(txt_学生List))
                {
                    str_sqlcmd += " And 学生コード Not In (" + txt_学生List + ")";
                }
                SqlDataAdapter sqlDa = new SqlDataAdapter(str_sqlcmd, sqlcon);
                sqlDa.SelectCommand.Transaction = transaction;
                DataTable dt = new DataTable();
                sqlDa.Fill(dt);
                if (dt.Rows[0].Field<int>("Count") > 0)
                {
                    sqlcom.CommandText = string.Format(@"Update HL_JUKUKANRI_学生クラス Set クラスコード = Null Where クラスコード = {0}", code_クラスコード);
                    if (!string.IsNullOrWhiteSpace(txt_学生List))
                    {
                        sqlcom.CommandText += " And 学生コード Not In (" + txt_学生List + ")";
                    }
                    int result2 = sqlcom.ExecuteNonQuery();
                    if (result2 != dt.Rows[0].Field<int>("Count"))
                    {
                        ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                        ((Form1)(Tag)).toolStripStatusLabel2.Text = "学生情報の取得に失敗しました。";
                        sqlcon.Close();
                        return;
                    }
                }

                string date_開始日 = dtp_開始日.Value.ToString("yyyy-MM-dd");
                string date_終了日 = dtp_終了日.Value.ToString("yyyy-MM-dd");

                //更新行う
                sqlcom.CommandText = string.Format(@"Update HL_JUKUKANRI_クラス履歴 Set
                                                    [クラス名]=N'{0}',[教室コード] = '{1}', [教師コード]= '{2}', [課程]= N'{3}', [学生コード]= '{4}', [開始日]= '{5}', [終了日]= '{6}' Where クラスコード = {7}",
                                                    classname, code_教室コード, code_教師コード, txt_課程, txt_学生, date_開始日, date_終了日, code_クラスコード);

                result = sqlcom.ExecuteNonQuery();

                if (result == 1)
                {
                    int result2 = -1;
                    if (!string.IsNullOrWhiteSpace(txt_学生))
                    {
                        sqlcom.CommandText = string.Format(@"Update HL_JUKUKANRI_学生クラス Set クラスコード = {0} Where 学生コード in ({1})", code_クラスコード, txt_学生List);
                        result2 = sqlcom.ExecuteNonQuery();
                    }
                    else
                    {
                        result2 = 1;
                    }

                    if (result2 > 0)
                    {
                        ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Green;
                        ((Form1)(Tag)).toolStripStatusLabel2.Text = string.Format("クラス[{0}]正常に更新されました。", txt_クラスコード.Text);
                        ((Form1)(Tag)).reLoad = false;
                    }
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(Tag)).toolStripStatusLabel2.Text = "更新処理が失敗しました。" + ex.ToString();
            }
            finally
            {
                if (result == 1)
                {
                    transaction.Commit();

                    sqlcon?.Close();

                    if (((Form1)(Tag)).m_クラス参照Handle != null)
                    {
                        SendMessage(((Form1)(Tag)).m_クラス参照Handle, Form1.CUSTOM_MESSAGE, IntPtr.Zero, IntPtr.Zero);
                    }

                    if (((Form1)(Tag)).m_学生情報一覧Handle != null)
                    {
                        SendMessage(((Form1)(Tag)).m_学生情報一覧Handle, Form1.CUSTOM_MESSAGE, IntPtr.Zero, IntPtr.Zero);
                    }

                    Close();
                }
                sqlcon?.Close();
            }
        }

        /// <summary>
        ///  エラーメッセージ追加
        /// </summary>
        /// <param name="name"></param>
        /// <param name="msg"></param>
        private void AddErrMsgList(string name, string msg)
        {
            errmsg.Add(name, msg);
            SetErrMsg();
            ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = System.Drawing.Color.Red;
            ((Form1)(Tag)).toolStripStatusLabel2.Text = msg;
        }

        /// <summary>
        /// エラー設定
        /// </summary>
        /// <remarks>エラー表示、文字色設定</remarks>
        private void SetErrMsg()
        {
            foreach (string controlName in errmsg.Keys)
            {
                switch (controlName)
                {
                    case "教室コード":
                        cmb_教室コード.BackColor = System.Drawing.Color.Red;
                        break;
                    case "教師コード":
                        cmb_教師コード.BackColor = System.Drawing.Color.Red;
                        break;
                    case "言語":
                        cmb_課程.BackColor = System.Drawing.Color.Red;
                        break;
                    case "終了日":
                        lbl_終了日.BackColor = System.Drawing.Color.Red;
                        break;
                    default:
                        break;
                }
            }
        }

        private void クラス管理_FormClosed(object sender, FormClosedEventArgs e)
        {
            //((Form1)(Tag)).m_クラス登録Handle = IntPtr.Zero;
            if (isUpdate == "new")
            {
                ((Form1)(Tag)).m_クラス登録Handle = IntPtr.Zero;

            }
            else
            {
                if (((Form1)(Tag)).codeDic.ContainsKey(code_クラスコード))
                {
                    ((Form1)(Tag)).codeDic.Remove(code_クラスコード);
                }

                ((Form1)(Tag)).m_クラス登録Handle = IntPtr.Zero;
            }
        }

        private void Cmb_教師コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_教師コード.Items != null)
            {

                //対象教師のメイン言語を取得する
                SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库
                try
                {
                    sqlcon.Open();
                }
                catch
                {
                    AddErrMsgList(lbl_教師.Text, "[教師コード]在チェック処理]:DBサーバーの接続に失敗しました。");
                    return;
                }

                string str_sqlcmd = "";

                //社外教師の場合
                if (cmb_教師コード.SelectedValue.ToString().Length > 0 && cmb_教師コード.SelectedValue.ToString().Substring(0, 1) == "K")
                {
                    str_sqlcmd = string.Format(@"SELECT　名前, メイン言語 FROM HL_JUKUKANRI_社外教師マスタ WHERE 教師コード = '{0}'", cmb_教師コード.SelectedValue.ToString());
                }
                else
                {
                    str_sqlcmd = string.Format(@"SELECT Em.名前, Sk.メイン言語 FROM HL_JINJI_エンジニアスキル情報 Sk Left Join HL_JINJI_社員マスタ Em On Sk.社員コード = Em.社員コード WHERE Sk.社員コード = '{0}'", cmb_教師コード.Text);
                }

                SqlCommand com = new SqlCommand(str_sqlcmd, sqlcon);
                SqlDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    cmb_課程.SelectedItem = reader["メイン言語"].ToString();
                }
                //end

                if (sqlcon != null)
                {
                    sqlcon.Close();
                }
            }
        }

        //liuxiaoyan 20200527 start
        private void Displayleft()
        {
            dgv_left.Rows.Clear();

            //画面値を取得
            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库

            try
            {
                sqlcon.Open();
            }
            catch
            {
                ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(Tag)).toolStripStatusLabel2.Text = "DBサーバーの接続に失敗しました.";
                return;
            }
            string sqlcmd = "Select "
                   + "　T1.学生コード "
                   + "　, T1.名前 "
                   + " From "
                   + " HL_JUKUKANRI_学生マスタ T1"
                   + " Left Join HL_JUKUKANRI_学生クラス T2 on T1.学生コード = T2.学生コード"
                   + " Inner Join HL_JUKUKANRI_学生情報 T3 on T1.学生コード = T3.学生コード"
                   + " Where T2.クラスコード = " + "'" + code_クラスコード + "'";
            if (chkbx_研修.Checked)
            {
                sqlcmd += " and T3.研修フラグ = 'True' Order by T1.学生コード";
            }
            else
            {
                sqlcmd += " and T3.研修フラグ = 'False' Order by T1.学生コード";
            }
            SqlCommand com = new SqlCommand(sqlcmd, sqlcon);
            SqlDataReader reader = com.ExecuteReader();
            int Index = 0;
            try
            {
                //一覧情報設定
                while (reader.Read())
                {
                    dgv_left.Rows.Add();
                    dgv_left.Rows[Index].Cells["leftcode"].Value = reader["学生コード"].ToString();

                    dgv_left.Rows[Index].Cells["leftname"].Value = reader["名前"].ToString();
                    Index++;
                }

                isFirst = false;
            }
            catch (Exception ex)
            {
                ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(Tag)).toolStripStatusLabel2.Text = "検索処理に失敗しました." + ex.Message;
            }
            finally
            {
                if (sqlcon != null)
                {
                    sqlcon.Close();
                    reader.Close();
                }
            }
        }
        private void Displayright()
        {
            dgv_right.Rows.Clear();

            //画面値を取得
            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库

            try
            {
                sqlcon.Open();
            }
            catch
            {
                ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(Tag)).toolStripStatusLabel2.Text = "DBサーバーの接続に失敗しました.";
                return;
            }
            string sqlcmd = "Select "
                    + "　T1.学生コード "
                    + "　, T1.名前 "
                    + " From "
                    + " HL_JUKUKANRI_学生マスタ T1"
                    + " Left Join HL_JUKUKANRI_学生クラス T2"
                    + " ON T1.学生コード = T2.学生コード"
                    + " Inner Join HL_JUKUKANRI_学生情報 T3 on T1.学生コード = T3.学生コード"
                    + " Where (T2.クラスコード is null or T2.クラスコード = '') ";
            if (chkbx_研修.Checked)
            {
                sqlcmd += " and T3.研修フラグ = 'True' Order by T1.学生コード";
            }
            else
            {
                sqlcmd += " and T3.研修フラグ = 'False' Order by T1.学生コード";
            }

            SqlCommand com = new SqlCommand(sqlcmd, sqlcon);
            SqlDataReader reader = com.ExecuteReader();

            int Index = 0;

            try
            {
                //一覧情報設定
                while (reader.Read())
                {
                    dgv_right.Rows.Add();
                    dgv_right.Rows[Index].Cells["rightcode"].Value = reader["学生コード"].ToString();
                    dgv_right.Rows[Index].Cells["rightname"].Value = reader["名前"].ToString();
                    Index++;
                }

                isFirst = false;
            }
            catch (Exception ex)
            {
                ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(Tag)).toolStripStatusLabel2.Text = "検索処理に失敗しました." + ex.Message;
            }
            finally
            {
                if (sqlcon != null)
                {
                    sqlcon.Close();
                    reader.Close();
                }
            }
        }

        //右から追加
        private void btn_add_Click(object sender, EventArgs e)
        {
            if (dgv_right.Rows.Count > 0)
            {
                int selectedcount = dgv_right.SelectedRows.Count;
                if (selectedcount < 1)
                {
                    ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                    ((Form1)(Tag)).toolStripStatusLabel2.Text = "選択されている学生がないため、追加処理できません。";
                    return;
                }
                foreach (DataGridViewRow item in dgv_right.Rows)
                {
                    if (item.Selected == true)
                    {
                        //左追加
                        int n = dgv_left.Rows.Add();
                        dgv_left.Rows[n].Cells[0].Value = item.Cells[0].Value.ToString();
                        dgv_left.Rows[n].Cells[1].Value = item.Cells[1].Value.ToString();
                    }
                }
                for (int i = dgv_right.SelectedRows.Count; i > 0; i--)
                {
                    //右削除
                    dgv_right.Rows.RemoveAt(dgv_right.SelectedRows[i - 1].Index);
                }
            }
        }

        //左から削除
        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (dgv_left.Rows.Count > 0)
            {
                int selectedcount = dgv_left.SelectedRows.Count;
                if (selectedcount < 1)
                {
                    ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                    ((Form1)(Tag)).toolStripStatusLabel2.Text = "選択されている学生がないため、削除処理できません。";
                    return;
                }
                foreach (DataGridViewRow item in dgv_left.Rows)
                {
                    if (item.Selected == true)
                    {
                        //右追加
                        int n = dgv_right.Rows.Add();
                        dgv_right.Rows[n].Cells[0].Value = item.Cells[0].Value.ToString();
                        dgv_right.Rows[n].Cells[1].Value = item.Cells[1].Value.ToString();
                    }
                }
                for (int i = dgv_left.SelectedRows.Count; i > 0; i--)
                {
                    //左削除
                    dgv_left.Rows.RemoveAt(dgv_left.SelectedRows[i - 1].Index);
                }
            }

        }

        //右から左ドロップ
        private void dgv_right_MouseDown(object sender, MouseEventArgs e)
        {
            //捕获鼠标点击区域的信息
            DataGridView.HitTestInfo hitTestInfo = dgv_right.HitTest(e.X, e.Y);

            if (hitTestInfo.RowIndex > -1)
            {
                if (dgv_right.SelectedRows.Count > 0)
                {
                    sourceRowCollection = dgv_right.SelectedRows;

                    foreach (DataGridViewRow row in sourceRowCollection)
                    {
                        row.DefaultCellStyle.BackColor = dgv_right.DefaultCellStyle.SelectionBackColor;
                        row.DefaultCellStyle.ForeColor = dgv_right.DefaultCellStyle.SelectionForeColor;
                    }
                }
            }
            else
            {
                sourceRowCollection = null;
            }
        }

        private void dgv_right_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (sourceRowCollection == null)
            {
                return;
            }
            if (!sourceRowCollection.Contains(dgv_right.Rows[e.RowIndex]))
            {
                foreach (DataGridViewRow row in sourceRowCollection)
                {
                    row.DefaultCellStyle.BackColor = dgv_right.DefaultCellStyle.BackColor;
                    row.DefaultCellStyle.ForeColor = dgv_right.DefaultCellStyle.ForeColor;
                }
            }
        }
        private void dgv_right_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ((Form1)(Tag)).toolStripStatusLabel2.Text = "";
                if (sourceRowCollection != null)
                {
                    DragDropEffects effect = dgv_right.DoDragDrop(sourceRowCollection, DragDropEffects.Move);
                    if (effect == DragDropEffects.Move)
                    {
                        //在sourceGrid中移除选中行
                        if (sourceRowCollection != null)
                        {
                            foreach (DataGridViewRow row in sourceRowCollection)
                            {
                                dgv_right.Rows.Remove(row);
                            }
                        }
                        //将sourceRowCollection重新置空
                        sourceRowCollection = null;
                    }
                }
            }
        }

        private void dgv_left_DragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(DataGridViewSelectedRowCollection)))
            {

                e.Effect = DragDropEffects.None;
                return;
            }
            else
            {
                e.Effect = DragDropEffects.Move;  //这个值会返回给DoDragDrop方法
            }
        }

        private void dgv_left_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(typeof(DataGridViewSelectedRowCollection)))
                {
                    DataGridViewSelectedRowCollection rowCollection = e.Data.GetData(typeof(DataGridViewSelectedRowCollection)) as DataGridViewSelectedRowCollection;
                    if (rowCollection == null)
                    {
                        return;
                    }
                    //新增行
                    //注意要将鼠标的Point转换到当前工作区域，否则无法得到正确的HitTestInfo
                    Point p = dgv_left.PointToClient(new Point(e.X, e.Y));
                    DataGridView.HitTestInfo hitTestInfo = dgv_left.HitTest(p.X, p.Y);
                    //如果鼠标所在的位置的RowIndex>-1，则在当前位置接入列，否则就在最末尾新增列
                    if (hitTestInfo.RowIndex > -1)
                    {
                        dgv_left.Rows.Insert(hitTestInfo.RowIndex + 1, rowCollection.Count);
                        for (int i = 0; i < rowCollection.Count; i++)
                        {
                            dgv_left.Rows[hitTestInfo.RowIndex + i + 1].Cells["leftcode"].Value = rowCollection[i].Cells["rightcode"].Value;
                            dgv_left.Rows[hitTestInfo.RowIndex + i + 1].Cells["leftname"].Value = rowCollection[i].Cells["rightname"].Value;
                        }
                    }
                    else
                    {
                        foreach (DataGridViewRow row in rowCollection)
                        {
                            int i = dgv_left.Rows.Add();
                            dgv_left.Rows[i].Cells["leftcode"].Value = row.Cells["rightcode"].Value;
                            dgv_left.Rows[i].Cells["leftname"].Value = row.Cells["rightname"].Value;
                        }
                    }
                    int selcol = dgv_left.CurrentCell.ColumnIndex;
                    dgv_left.Sort(dgv_left.Columns[selcol], ListSortDirection.Ascending);
                }
            }
            catch
            {
                ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(Tag)).toolStripStatusLabel2.Text = "正しい場所へドロップしてください。";
                sourceRowCollection = null;
                Displayright();
                dgv_left.Rows.Clear();
                if (isUpdate != "new")
                {
                    Displayleft();
                }
            }
        }

        //左から右ドロップ
        private void dgv_left_MouseDown(object sender, MouseEventArgs e)
        {
            //捕获鼠标点击区域的信息
            DataGridView.HitTestInfo hitTestInfo = dgv_left.HitTest(e.X, e.Y);

            if (hitTestInfo.RowIndex > -1)
            {
                if (dgv_left.SelectedRows.Count > 0)
                {
                    sourceRowCollection = dgv_left.SelectedRows;
                    foreach (DataGridViewRow row in sourceRowCollection)
                    {
                        row.DefaultCellStyle.BackColor = dgv_left.DefaultCellStyle.SelectionBackColor;
                        row.DefaultCellStyle.ForeColor = dgv_left.DefaultCellStyle.SelectionForeColor;
                    }
                }
            }
            else
            {
                sourceRowCollection = null;
            }

        }

        private void dgv_left_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (sourceRowCollection == null)
            {
                return;
            }
            if (!sourceRowCollection.Contains(dgv_left.Rows[e.RowIndex]))
            {
                foreach (DataGridViewRow row in sourceRowCollection)
                {
                    row.DefaultCellStyle.BackColor = dgv_left.DefaultCellStyle.BackColor;
                    row.DefaultCellStyle.ForeColor = dgv_left.DefaultCellStyle.ForeColor;
                }
            }
        }

        private void dgv_left_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ((Form1)(Tag)).toolStripStatusLabel2.Text = "";
                if (sourceRowCollection != null)
                {
                    DragDropEffects effect = dgv_left.DoDragDrop(sourceRowCollection, DragDropEffects.Move);
                    if (effect == DragDropEffects.Move)
                    {
                        //在sourceGrid中移除选中行
                        if (sourceRowCollection != null)
                        {
                            foreach (DataGridViewRow row in sourceRowCollection)
                            {
                                dgv_left.Rows.Remove(row);
                            }
                        }
                        //将sourceRowCollection重新置空
                        sourceRowCollection = null;
                    }
                }
            }
        }

        private void dgv_right_DragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(DataGridViewSelectedRowCollection)))
            {

                e.Effect = DragDropEffects.None;
                return;
            }
            else
            {
                e.Effect = DragDropEffects.Move;  //这个值会返回给DoDragDrop方法
            }
        }

        private void dgv_right_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(typeof(DataGridViewSelectedRowCollection)))
                {
                    DataGridViewSelectedRowCollection rowCollection = e.Data.GetData(typeof(DataGridViewSelectedRowCollection)) as DataGridViewSelectedRowCollection;
                    if (rowCollection == null)
                    {
                        return;
                    }
                    //新增行
                    //注意要将鼠标的Point转换到当前工作区域，否则无法得到正确的HitTestInfo
                    Point p = dgv_right.PointToClient(new Point(e.X, e.Y));
                    DataGridView.HitTestInfo hitTestInfo = dgv_right.HitTest(p.X, p.Y);
                    //如果鼠标所在的位置的RowIndex>-1，则在当前位置接入列，否则就在最末尾新增列
                    if (hitTestInfo.RowIndex > -1)
                    {
                        dgv_right.Rows.Insert(hitTestInfo.RowIndex + 1, rowCollection.Count);
                        for (int i = 0; i < rowCollection.Count; i++)
                        {
                            dgv_right.Rows[hitTestInfo.RowIndex + i + 1].Cells["rightcode"].Value = rowCollection[i].Cells["leftcode"].Value;
                            dgv_right.Rows[hitTestInfo.RowIndex + i + 1].Cells["rightname"].Value = rowCollection[i].Cells["leftname"].Value;
                        }
                    }
                    else
                    {
                        foreach (DataGridViewRow row in rowCollection)
                        {
                            int i = dgv_right.Rows.Add();
                            dgv_right.Rows[i].Cells["rightcode"].Value = row.Cells["leftcode"].Value;
                            dgv_right.Rows[i].Cells["rightname"].Value = row.Cells["leftname"].Value;
                        }
                    }
                    int selcol = dgv_right.CurrentCell.ColumnIndex;
                    dgv_right.Sort(dgv_right.Columns[selcol], ListSortDirection.Ascending);
                }
            }
            catch
            {
                ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(Tag)).toolStripStatusLabel2.Text = "正しい場所へドロップしてください。";
                sourceRowCollection = null;
                Displayright();
                dgv_left.Rows.Clear();
                if (isUpdate != "new")
                {
                    Displayleft();
                }
            }
        }

        private void lbl_必須_Paint(object sender, PaintEventArgs e)
        {
            Label strLbl = (Label)sender;
            string str = strLbl.Text;

            if (str.Contains("必須"))
            {
                strLbl.ForeColor = Color.FromArgb(240, 240, 240);

                Font f = new System.Drawing.Font("メイリオ", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 128);

                int i = str.IndexOf("　[");
                string str_name = str.Substring(0, i);
                string str必須 = str.Substring(i, str.Length - i);

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

        private void dgv_left_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (e.Column.Name == "leftcode" && e.Column.Name == "rightcode")
            {
                e.SortResult = (Convert.ToDouble(e.CellValue1) - Convert.ToDouble(e.CellValue2) > 0) ? 1 : (Convert.ToDouble(e.CellValue1) - Convert.ToDouble(e.CellValue2) < 0) ? -1 : 0;

                e.Handled = true;
            }
        }

        private void chkbx_研修_CheckedChanged(object sender, EventArgs e)
        {
            ((Form1)(Tag)).toolStripStatusLabel2.Text = "";
            Displayleft();
            Displayright();
        }
    }
}