using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace HL_塾管理
{
    public partial class 社員から教師へ変更 : DockContent
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        private AutoSizeFormClass asc = new AutoSizeFormClass();

        //DB接続情報
        private string connectionString = ComClass.connectionString;
        private Dictionary<string, string> dic = new Dictionary<string, string>();

        //更新・権限区分
        public bool isUpdate = false;

        private bool isAdmin = false;

        //画面項目
        public string code_教師コード = string.Empty;

        private string name = string.Empty;
        private string language = string.Empty;
        //private int classCode = 0;
        private string admin = string.Empty;
        private string date_入職日txt = string.Empty;
        private string date_退職日txt = string.Empty;
        private string classname = string.Empty;
        private string classroomid= string.Empty;

        public 社員から教師へ変更()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 画面Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 社員から教師へ変更_Load(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);

            //画面区分
            btn_save.Text = isUpdate ? "更新" : "登録";
            this.Text = isUpdate ? "社員から教師へ更新" : "社員から教師へ登録";

            //初期表示設定 更新・登録
            lbl_コード.Text = code_教師コード;

           

            GetAdmin();
            if (!isAdmin)
            {
                cmb_職務.Visible = false;
                lbl_職務.Visible = false;

                //lbl_入職日.Location = new Point(lbl_クラス.Location.X, lbl_クラス.Location.Y + 50);
                dtp_入職日.Location = new Point(lbl_入職日.Location.X + 175, lbl_入職日.Location.Y);

                //lbl_入職日.Location = new Point(lbl_クラス.Location.X, lbl_クラス.Location.Y +50);
                dtp_入職日.Location = new Point(lbl_入職日.Location.X + 175, lbl_入職日.Location.Y);

                lbl_退職フラグ.Location = new Point(lbl_入職日.Location.X, lbl_入職日.Location.Y + 50);
                chk_退職.Location = new Point(lbl_退職フラグ.Location.X + 175, lbl_退職フラグ.Location.Y);

            }
            //liuxiaoyan add
            if (isUpdate == true)
            {
                //cmb_職務.Visible = false;
                //lbl_職務.Visible = false;
                cmb_名前.Visible = false;
                lbl_名前.Text = lbl_名前.Text.Replace("[必須]", "");
                //lbl_クラス.Text = lbl_クラス.Text.Replace("[必須]", "");
                //txt_クラス名.ReadOnly = true;
                
                
            }
            //end
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
                ssl_errMsg.ForeColor = Color.Red;
                ssl_errMsg.Text = "サーバーがつなげない！インターネット接続をチェックしてください。";
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
                ssl_errMsg.ForeColor = Color.Red;
                ssl_errMsg.Text = string.Format("ユーザ名[{0}]の情報の取得に失敗しました。" + ex.Message, login);
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
                ssl_errMsg.ForeColor = Color.Red;
                ssl_errMsg.Text = "サーバーがつなげない！インターネット接続をチェックしてください。";
                return;
            }

            try
            {
                if (isUpdate)
                {
                    string str_sqlcmd = string.Format(@"Select a.名前, a.メイン言語,a.クラスコード,a.メイン言語,a.入塾日,a.退塾日,a.職務,
                                                      b.MYNumber,b.カタカナ,b.ローマ字表記,b.社員コード,b.性別,b.生年月日,
                                                      c.メール,c.携帯,c.国籍,c.住所,c.郵便番号
                                                      From HL_JUKUKANRI_教師情報 As a
                                                      Left Join HL_JINJI_社員マスタ As b
                                                      On  a.教師コード=b.社員コード
                                                      Left Join HL_JINJI_社員情報 As c
                                                      On a.教師コード=c.社員コード
                                                      Left Join HL_JUKUKANRI_社内社員教師マスタ As d
                                                      On a.教師コード=d.社員コード
                                                      Where a.教師コード = '{0}'", code_教師コード);

                    SqlCommand com = new SqlCommand(str_sqlcmd, sqlcon);
                    SqlDataReader reader = com.ExecuteReader();

                    reader.Read();
                    txt_名前.Text = reader["名前"].ToString();
                    cmb_言語.SelectedItem = reader["メイン言語"].ToString();
                    //lbl_classid.Text = reader["クラスコード"].ToString() == "0" ? "1" : reader["クラスコード"].ToString();
                    cmb_職務.SelectedItem = reader["職務"].ToString().Trim();
                    dtp_入職日.Value = (DateTime)reader["入塾日"];

                    lbl_退職フラグ.Visible = true;
                    chk_退職.Visible = true;
                    btn_save.Location = new Point(lbl_退職フラグ.Location.X + 450, lbl_退職フラグ.Location.Y + 150);

                    //liuxiaoyan add 0604
                    //lbl_コード.Text = reader["社員コード"].ToString();
                    txt_カタカナ.Text = reader["カタカナ"].ToString();
                    //txt_MyNumber.Text = reader["MyNumber"].ToString();
                    txt_性別.Text = reader["性別"].ToString();
                    txt_生年月日.Text = reader["生年月日"].ToString();
                    txt_ローマ字表記.Text = reader["ローマ字表記"].ToString();
                    txt_メール.Text = reader["メール"].ToString();
                    txt_住所.Text = reader["住所"].ToString();
                    txt_国籍.Text = reader["国籍"].ToString();
                    txt_addfront.Text = reader["郵便番号"].ToString().Substring(0,3) ;
                    text_addafter.Text=reader["郵便番号"].ToString().Substring(3,4);
                    txt_携帯.Text = reader["携帯"].ToString();
                    if (!string.IsNullOrWhiteSpace(reader["退塾日"].ToString()))
                    {
                        chk_退職.Checked = true;
                        lbl_退職日.Visible = true;
                        dtp_退職日.Visible = true;
                        dtp_退職日.Value = (DateTime)reader["退塾日"];
                    }

                    reader.Close();

                    string sql_class = @"Select "
                                         + " クラス名 "
                                         + " From "
                                         + " HL_JUKUKANRI_クラス履歴 "
                                         + " Where "
                                         + " 教師コード = '" + code_教師コード + "'";

                    SqlCommand cmd = new SqlCommand(sql_class, sqlcon);
                    SqlDataAdapter da = new SqlDataAdapter(sql_class, sqlcon);
                    DataTable dt1 = new DataTable();
                    da.Fill(dt1);
                    int rowscount = 0;
                    rowscount = dt1.Rows.Count;
                    string[] jyugyo = new string[rowscount];
                    for (int i = 0; i < rowscount; i++)
                    {
                        jyugyo[i] = dt1.Rows[i][0].ToString();
                    }
                    for (int i = 0; i < rowscount; i++)
                    {
                        if (i == 0)
                        {
                            classname = jyugyo[i];
                        }
                        else
                        {
                            classname += "\r\n" + jyugyo[i];
                            //txt_クラス名.Multiline = true;
                            //txt_クラス名.Size = new Size(txt_クラス名.Size.Width, txt_クラス名.Size.Height + 25);
                            //lbl_教室.Location = new Point(lbl_クラス.Location.X, txt_クラス名.Location.Y + txt_クラス名.Size.Height + 25);
                            //cmb_教室.Location = new Point(txt_クラス名.Location.X, txt_クラス名.Location.Y + txt_クラス名.Size.Height + 25);
                            //lbl_職務.Location = new Point(lbl_教室.Location.X, lbl_教室.Location.Y + 50);
                            //cmb_職務.Location = new Point(cmb_教室.Location.X, cmb_教室.Location.Y + 50);
                            lbl_入職日.Location = new Point(lbl_職務.Location.X, lbl_職務.Location.Y + 50);
                            dtp_入職日.Location = new Point(cmb_職務.Location.X, cmb_職務.Location.Y + 50);
                            lbl_退職フラグ.Location = new Point(lbl_入職日.Location.X, lbl_入職日.Location.Y + 50);
                            chk_退職.Location = new Point(dtp_入職日.Location.X, dtp_入職日.Location.Y + 50);
                        }
                    }
                    //txt_クラス名.Text = classname;
                }
                else
                {
                    string sqlcmd = @"Select "
                                  + " T1.社員コード "
                                  + " , T1.名前 "
                                  + " , T2.メイン言語 "
                                  //+ " , T1.入職日 "
                                  + " From "
                                  + " HL_JINJI_社員マスタ AS T1 "
                                  + " Left Join HL_JINJI_エンジニアスキル情報 AS T2 "
                                  + " On T1.社員コード = T2.社員コード "
                                  + " Left Join HL_JINJI_社員在職状態 AS T3 "
                                  + " On T1.社員コード = T3.社員コード "
                                  + " Where 1 = 1 "
                                  + " And T3.待機状態 = '待機' "
                                  + " And T1.社員コード Not In "
                                  + " (Select 社員コード From HL_JUKUKANRI_社内社員教師マスタ) "
                                  + " Group by T1.社員コード, T1.名前, T2.メイン言語, T1.入職日, T3.在職状態";

                    SqlCommand com = new SqlCommand(sqlcmd, sqlcon);
                    SqlDataReader reader = com.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Columns.Add("名前");
                    dt.Columns.Add("社員コード");

                    //dic・cmb設定
                    while (reader.Read())
                    {
                        name = reader["名前"].ToString();
                        code_教師コード = reader["社員コード"].ToString();
                        language = reader["メイン言語"].ToString();
                        //date_入職日txt = reader["入職日"].ToString();

                        string value = string.Format("{0},{1}", language, date_入職日txt);

                        if (!dic.ContainsKey(code_教師コード))
                        {
                            dic.Add(code_教師コード, value);

                            DataRow row = dt.NewRow();
                            row["名前"] = name;
                            row["社員コード"] = code_教師コード;
                            dt.Rows.Add(row);
                        }
                    }

                    reader.Close();

                    cmb_名前.DisplayMember = "名前";
                    cmb_名前.ValueMember = "社員コード";
                    cmb_名前.DataSource = dt;
                    cmb_言語.SelectedIndex = 0;
                    cmb_職務.SelectedIndex = 1;
                    btn_save.Location = new Point(lbl_入職日.Location.X + 450, lbl_入職日.Location.Y + 150);

                    ////クラスコードを取得
                    //string sql_class = @"select ident_current('HL_JUKUKANRI_クラス履歴')+1 as クラスコード ";

                    //SqlCommand cmd = new SqlCommand(sql_class, sqlcon);
                    //SqlDataReader reader2 = cmd.ExecuteReader();

                    //reader2.Read();                    
                    ////classCode = Convert.ToInt16(reader2["クラスコード"].ToString());
                    ////lbl_classid.Text = classCode.ToString();
                    //reader2.Close();


                }
            }
            catch (Exception ex)
            {
                ssl_errMsg.ForeColor = Color.Red;
                ssl_errMsg.Text = string.Format(@"教師[{0}]の情報の取得に失敗しました。" + ex.Message, code_教師コード);
            }
            finally
            {
                if (sqlcon != null)
                {
                    sqlcon.Close();
                }
            }
        }

        //private void GetClassroom()
        //{
        //    SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库

        //    try
        //    {
        //        sqlcon.Open();
        //    }
        //    catch
        //    {
        //        ssl_errMsg.ForeColor = Color.Red;
        //        ssl_errMsg.Text = "サーバーがつなげない！インターネット接続をチェックしてください。";
        //        return;
        //    }

        //    try
        //    {
        //        string sql_class = @"select 教室コード,備考 from HL_JUKUKANRI_教室マスタ";

        //        SqlCommand cmd = new SqlCommand(sql_class, sqlcon);
        //        SqlDataAdapter da = new SqlDataAdapter(sql_class, sqlcon);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        cmb_教室.DisplayMember = "備考";
        //        cmb_教室.ValueMember = "教室コード";
        //        cmb_教室.DataSource = dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        ssl_errMsg.ForeColor = Color.Red;
        //        ssl_errMsg.Text = "教室の情報の取得に失敗しました。" + ex.Message;
        //    }
        //    finally
        //    {
        //        if (sqlcon != null)
        //        {
        //            sqlcon.Close();
        //        }
        //    }
        //}

        /// <summary>
        /// cmb_名前の値を選択する処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmb_名前_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isUpdate)
            {
                return;
            }

            string key = cmb_名前.SelectedValue.ToString();
            string[] array = dic[key].Split(',');

            lbl_コード.Text = key;
            cmb_言語.SelectedItem = array[0];
            //dtp_入職日.Value = Convert.ToDateTime(array[1]);

            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库

            try
            {
                sqlcon.Open();
            }
            catch
            {
                ssl_errMsg.ForeColor = Color.Red;
                ssl_errMsg.Text = "サーバーがつなげない！インターネット接続をチェックしてください。";
                return;
            }
            try
            {
                string str_sqlcmd = string.Format(@"Select * From HL_JINJI_社員マスタ as a
　　　　　　　　　　　　　　　　　　　　　　　　　　Left Join HL_JINJI_社員情報 As b
　　　　　　　　　　　　　　　　　　　　　　　　　　On a.社員コード=b.社員コード
　　　　　　　　　　　　　　　　　　　　　　　　　　Where a.社員コード= '{0}'", lbl_コード.Text);
                SqlCommand com = new SqlCommand(str_sqlcmd, sqlcon);
                SqlDataReader reader = com.ExecuteReader();
                reader.Read();
                txt_カタカナ.Text = reader["カタカナ"].ToString();
                //txt_MyNumber.Text = reader["MyNumber"].ToString();
                txt_性別.Text = reader["性別"].ToString();
                txt_生年月日.Text = reader["生年月日"].ToString();
                txt_ローマ字表記.Text = reader["ローマ字表記"].ToString();
                txt_メール.Text = reader["メール"].ToString();
                txt_住所.Text = reader["住所"].ToString();
                txt_国籍.Text = reader["国籍"].ToString();
                txt_addfront.Text = reader["郵便番号"].ToString().Substring(0, 3);
                text_addafter.Text = reader["郵便番号"].ToString().Substring(3, 4);
                txt_携帯.Text = reader["携帯"].ToString();
            }
            catch (Exception ex)
            {
                ssl_errMsg.ForeColor = Color.Red;
                ssl_errMsg.Text = string.Format(@"教師[{0}]の情報の取得に失敗しました。" + ex.Message, code_教師コード);
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
        /// 退職のcheckBoxを選択する処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chk_退職_CheckedChanged(object sender, EventArgs e)
        {
            //if (chk_退職.Checked)
            //{
            //    lbl_退職日.Visible = true;
            //    lbl_退職日.Location = new Point(lbl_退職フラグ.Location.X, lbl_退職フラグ.Location.Y + 50);
            //    dtp_退職日.Visible = true;
            //    dtp_退職日.Location = new Point(cmb_教室.Location.X, lbl_退職日.Location.Y - 5);
            //    btn_登録.Location = new Point(dtp_退職日.Location.X, lbl_退職日.Location.Y + 55);
            //}
            //else
            //{
            //    lbl_退職日.Visible = false;
            //    dtp_退職日.Visible = false;
            //    btn_登録.Location = new Point(lbl_退職フラグ.Location.X + 150, lbl_退職フラグ.Location.Y + 50);
            //}

            if (chk_退職.Checked)
            {
                lbl_退職日.Visible = true;
                lbl_退職日.Location = new Point(lbl_退職フラグ.Location.X, lbl_退職フラグ.Location.Y + 50);
                dtp_退職日.Visible = true;
                dtp_退職日.Location = new Point(chk_退職.Location.X, chk_退職.Location.Y +50);
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
        /// 画面が閉じる処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 社員から教師へ変更_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isUpdate)
            {
                //一覧画面から渡したデータを削除
                if (((Form1)(this.Tag)).codeDic.ContainsKey(code_教師コード))
                {
                    ((Form1)(this.Tag)).codeDic.Remove(code_教師コード);
                }

                //Handleが０に初期化する
                ((Form1)(this.Tag)).m_社員から教師へ変更Handle = IntPtr.Zero;
            }
            else
            {
                //Handleが０に初期化する
                ((Form1)(this.Tag)).m_社員から教師へ登録Handle = IntPtr.Zero;
            }
        }


        /// <summary>
        /// 登録ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_登録_Click(object sender, EventArgs e)
        {
            code_教師コード = lbl_コード.Text;
            name = cmb_名前.Text.Trim();
            language = cmb_言語.Text;
            admin = cmb_職務.Text;
            //classCode = Convert.ToInt16(lbl_classid.Text);
            date_入職日txt = dtp_入職日.Value.ToString("yyyy-MM-dd");
            date_退職日txt = chk_退職.Checked ? dtp_退職日.Value.ToString("yyyy-MM-dd") : null;
            //classname = txt_クラス名.Text;
            //classroomid = cmb_教室.SelectedValue.ToString();

            if (!InputCheck())
            {
                return;
            }

            if (isUpdate)
            {
                //データ更新
                UpdateTecherInfo();
            }
            else
            {
                //新しいデータを登録
                SaveTecherInfo();
            }
            //DVGUpdate();
        }

        /// <summary>
        /// 更新
        /// </summary>
        private void UpdateTecherInfo()
        {
            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库

            try
            {
                sqlcon.Open();
            }
            catch
            {
                ssl_errMsg.ForeColor = Color.Red;
                ssl_errMsg.Text = "サーバーがつなげない！インターネット接続をチェックしてください。";
                return;
            }

            SqlTransaction traction = sqlcon.BeginTransaction();

            try
            {
                SqlCommand sqlcom = new SqlCommand();
                sqlcom.Connection = sqlcon;
                sqlcom.Transaction = traction;

                //更新行う
                //社員から教師へ変更
                string sqlCommand = @"Update "
                                  + " HL_JUKUKANRI_社内社員教師マスタ "
                                  + " Set "
                                  + " メイン言語 = N'{1}' "
                                  + " , 入塾日 = '{2}' "
                                  + " , 退塾日 = ";
                if (chk_退職.Checked == false)
                {
                    date_退職日txt = null;
                }
                sqlCommand += date_退職日txt == null ? "null " : @"'" + date_退職日txt + "' ";
                sqlCommand += ", 職務 = '{3}' Where 社員コード = '{0}'";

                sqlcom.CommandText = string.Format(sqlCommand, code_教師コード, language, date_入職日txt, admin);

                int result = sqlcom.ExecuteNonQuery();
                if (result == 1)
                {
                    result = 0;
                    //教師情報
                    sqlCommand = @"Update "
                               + " HL_JUKUKANRI_教師情報 "
                               + " Set "
                               + " メイン言語 = '{1}' "
                               + " , 入塾日 = '{2}' "
                               + " , 退塾日 = ";

                    sqlCommand += date_退職日txt == null ? "null " : @"'" + date_退職日txt + "' ";
                    sqlCommand += ", 職務 = N'{3}' Where 教師コード = '{0}'";
                    sqlcom.CommandText = string.Format(sqlCommand, code_教師コード, language, date_入職日txt, admin);
                    result = sqlcom.ExecuteNonQuery();

                    if (result != 1)
                    {
                        throw new Exception();
                    }

                    //((Form1)(this.Tag)).reLoad = true;
                    ((Form1)(this.Tag)).reLoad = false;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Green;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = string.Format(@"教師[{0}]の情報が正常に更新されました。", code_教師コード);
                    traction.Commit();

                    if (((Form1)(this.Tag)).m_教師情報一覧Handle != null)
                    {
                        //データ更新のMSGを一覧画面に送る
                        SendMessage(((Form1)(this.Tag)).m_教師情報一覧Handle, Form1.CUSTOM_MESSAGE, IntPtr.Zero, IntPtr.Zero);
                    }

                    this.Close();
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                ssl_errMsg.ForeColor = Color.Red;
                ssl_errMsg.Text = string.Format(@"教師[{0}]の更新処理に失敗しました。" + ex.Message, code_教師コード);
                traction.Rollback();
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
        /// 登録
        /// </summary>
        private void SaveTecherInfo()
        {
            SqlConnection sqlcon = new SqlConnection(connectionString);

            try
            {
                sqlcon.Open();
            }
            catch
            {
                ssl_errMsg.ForeColor = Color.Red;
                ssl_errMsg.Text = "サーバーがつなげない！インターネット接続をチェックしてください。";
                return;
            }

            SqlTransaction traction = sqlcon.BeginTransaction();

            try
            {
                SqlCommand sqlcom = new SqlCommand();
                sqlcom.Connection = sqlcon;
                sqlcom.Transaction = traction;

                //登録行う
                //社員から教師へ変更
                string sqlCommand = @"Insert Into "
                                  + " HL_JUKUKANRI_社内社員教師マスタ ("
                                  + " 社員コード "
                                  + " , 名前 "
                                  + " , メイン言語 "
                                  + " , 入塾日 "
                                  + " , 退塾日 "
                                  + " , 職務) "
                                  + " Values ('{0}', N'{1}', N'{2}', '{3}', null, N'{4}')";

                sqlcom.CommandText = string.Format(sqlCommand, code_教師コード, name, language, date_入職日txt, admin);

                int result = sqlcom.ExecuteNonQuery();
                if (result == 1)
                {
                    string y = DateTime.Now.Year.ToString();
                    string m = (DateTime.Now.Month + 5).ToString();
                    //date_退職日txt = Convert.ToDateTime(y + "-" + m + "-1").AddDays(-1).ToString("yyyy-MM-dd");
                    date_退職日txt ="-";

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
                               + " Values ('{0}', N'{1}', N'自社', N'{2}', null, '{3}', null, N'{4}')";

                    sqlcom.CommandText = string.Format(sqlCommand, code_教師コード, name, language, DateTime.Now.ToString("yyyy-MM-dd"), admin);

                    result = sqlcom.ExecuteNonQuery();
                    if (result != 1)
                    {
                        throw new Exception();
                    }

                    ////クラス履歴登録
                    //sqlCommand = @"Insert Into HL_JUKUKANRI_クラス履歴 ("
                    //           + " クラス名"
                    //           + " ,教室コード "
                    //           + " , 教師コード "
                    //           + " , 課程 "
                    //           + " , 学生コード "
                    //           + " , 開始日 "
                    //           + " , 終了日 "
                    //           + " , 有効) "
                    //           + " Values ('{0}','{1}', '{2}', '{3}', null, '{4}', null, 1)";

                    //sqlcom.CommandText = string.Format(sqlCommand,classname,classroomid, code_教師コード, language, DateTime.Now.ToString("yyyy-MM-dd"));

                    //result = sqlcom.ExecuteNonQuery();
                    //if (result != 1)
                    //{
                    //    throw new Exception();
                    //}

                    //((Form1)(this.Tag)).reLoad = true;
                    ((Form1)(this.Tag)).reLoad = false;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Green;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = string.Format(@"教師[{0}]の情報が正常に登録されました。", code_教師コード);
                    traction.Commit();

                    if (((Form1)(this.Tag)).m_教師情報一覧Handle != null)
                    {
                        //データ更新のMSGを一覧画面に送る
                        SendMessage(((Form1)(this.Tag)).m_教師情報一覧Handle, Form1.CUSTOM_MESSAGE, IntPtr.Zero, IntPtr.Zero);
                    }

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

                    this.Close();
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                ssl_errMsg.ForeColor = Color.Red;
                ssl_errMsg.Text = string.Format(@"教師[{0}]の登録処理に失敗しました。" + ex.Message, code_教師コード);
                traction.Rollback();
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
        private bool InputCheck()
        {
            if (!isUpdate)
            {
                //名前チェック
                if (name.Equals(""))
                {
                    ssl_errMsg.ForeColor = Color.Red;
                    ssl_errMsg.Text = "名前未入力!";
                    return false;
                }
                else
                {
                    if (name.IndexOf(" ") > 0)
                    {
                        ssl_errMsg.ForeColor = Color.Red;
                        ssl_errMsg.Text = "名前に許可されない文字'半角SPACE' が入りました。";
                        return false;
                    }
                    if (name.IndexOf("　") == -1)
                    {
                        ssl_errMsg.ForeColor = Color.Red;
                        ssl_errMsg.Text = "姓と名の間に '全角SPACE' を挿入してください!";
                        return false;
                    }
                    if (name.IndexOf("　") != cmb_名前.Text.LastIndexOf("　"))
                    {
                        ssl_errMsg.ForeColor = Color.Red;
                        ssl_errMsg.Text = "名前に '全角SPACE' は二つ以上入力しないでください!";
                        return false;
                    }
                    if (name.IndexOf(",") > 0)
                    {
                        ssl_errMsg.ForeColor = Color.Red;
                        ssl_errMsg.Text = "名前に許可されない文字「,」が入りました。";
                        return false;
                    }
                }

                //存在チェック
                SqlConnection sqlcon = new SqlConnection(connectionString);

                try
                {
                    sqlcon.Open();
                }
                catch
                {
                    ssl_errMsg.ForeColor = Color.Red;
                    ssl_errMsg.Text = "サーバーがつなげない！インターネット接続をチェックしてください。";
                    return false;
                }

                string sqlcmd = string.Format(@"Select * From HL_JUKUKANRI_社内社員教師マスタ Where 社員コード = '{0}'", code_教師コード);
                SqlCommand sqlcom = new SqlCommand(sqlcmd, sqlcon);

                try
                {
                    SqlDataReader reader = sqlcom.ExecuteReader();

                    if (reader.HasRows)
                    {
                        //新規：教師コードがある場合エラー
                        ssl_errMsg.ForeColor = Color.Red;
                        ssl_errMsg.Text = string.Format(@"エラー：教師コード[{0}]が既に登録されています.", code_教師コード);
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    ssl_errMsg.ForeColor = Color.Red;
                    ssl_errMsg.Text = "教師コードのチェック処理にエラーが発生しました。" + ex.Message;

                    if (sqlcon != null)
                    {
                        sqlcon.Close();
                    }

                    return false;
                }

            }

            //メイン言語
            if (string.IsNullOrWhiteSpace(cmb_言語.Text))
            {
                ssl_errMsg.ForeColor = Color.Red;
                ssl_errMsg.Text = "[メイン言語]を入力してください。";
                return false;
            }
            //if (string.IsNullOrEmpty(txt_クラス名.Text))
            //{
            //    ssl_errMsg.ForeColor = Color.Red;
            //    ssl_errMsg.Text = "[クラス名]を入力してください。";
            //    return false;
            //}


            if (isUpdate)
            {
                if (chk_退職.Checked == true)
                {
                    //開始日 < 終了日
                    if (dtp_入職日.Value > dtp_退職日.Value)
                    {
                        ssl_errMsg.ForeColor = Color.Red;
                        ssl_errMsg.Text = "[退塾日]は[入塾日]以後の日を設定してください。";
                        return false;
                    }
                }

            }

            return true;
        }

        /// <summary>
        /// 「必須」設定
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