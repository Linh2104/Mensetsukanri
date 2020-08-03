using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace HL_人事
{
    public partial class 社員から教師へ登録 : DockContent
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        private string connectionString = ComClass.connectionString;

        public string code_教師コード = string.Empty;
        private string name = string.Empty;
        private string language = string.Empty;
        public bool isUpdate = false;

        public 社員から教師へ登録()
        {
            InitializeComponent();
        }

        public 社員から教師へ登録(string code_教師コード, string name, string language)
        {
            InitializeComponent();

            this.code_教師コード = code_教師コード;
            this.name = name;
            this.language = language;
        }

        private void 社員から教師へ登録_Load(object sender, EventArgs e)
        {
            txt_教師コード.Text = code_教師コード;
            txt_名前.Text = name;
            txt_メイン言語.Text = language;
            dtp_開始日.Value = DateTime.Today;

            if(isUpdate)
            {
                txt_教師コード.Enabled = false;
                txt_名前.Enabled = false;
                //txt_メイン言語.Enabled = false;
                btn_登録.Text = "更新";
                GetDataSource();
            }
        }

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
                MessageBox.Show("DBサーバーの接続に失敗しました.");

                return;
            }

            string str_sqlcmd = string.Format("SELECT * From HL_JUKUKANRI_教師情報 Where 教師コード = '{0}'", code_教師コード);

            try
            {
                SqlDataAdapter sqlDa = new SqlDataAdapter(str_sqlcmd, sqlcon);
                DataTable dt = new DataTable();
                sqlDa.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    txt_教師コード.Text = code_教師コード;
                    txt_名前.Text = name;
                    txt_メイン言語.Text = language;
                    dtp_開始日.Value = (DateTime)dt.Rows[0]["開始日"];
                    if (!string.IsNullOrWhiteSpace(dt.Rows[0]["終了日"].ToString()))
                    {
                        dtp_終了日.Value = (DateTime)dt.Rows[0]["終了日"];
                    }
                    else
                    {
                        dtp_終了日.Value = DateTime.Today;
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("教師[{0}]の情報の取得に失敗しました。");
            }
            finally
            {
                if (sqlcon != null)
                {
                    sqlcon.Close();
                }
            }
        }

        private void 社員から教師へ登録_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form1)(this.Tag)).m_社員から教師へ登録Handle = IntPtr.Zero;
        }

        private void btn_登録_Click(object sender, EventArgs e)
        {
            if(!InputCheck())
            {
                return;
            }

            if(!isUpdate)
            {
                SaveTecherInfo();
            }
            else
            {
                UpdateTecherInfo();
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        private void UpdateTecherInfo()
        {
            string date_開始日txt = dtp_開始日.Value.ToString("yyyy-MM-dd");
            string date_終了日txt = dtp_終了日.Value.ToString("yyyy-MM-dd");
            language = txt_メイン言語.Text;

            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库

            try
            {
                sqlcon.Open();
            }
            catch
            {
                ssl_errMsg.ForeColor = Color.Red;
                ssl_errMsg.Text = "DBサーバーの接続に失敗しました。";
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
                sqlcom.CommandText = string.Format(@"Update HL_JUKUKANRI_社員から教師へ変更 Set メイン言語 = '{0}', 開始日 = '{1}', 終了日 = '{2}' Where 教師コード = '{3}'", language, date_開始日txt, date_終了日txt, code_教師コード);

                int result = sqlcom.ExecuteNonQuery();
                if (result == 1)
                {

                    sqlcom.CommandText = string.Format(@"Update HL_JUKUKANRI_教師情報 Set メイン言語 = '{0}', 開始日 = '{1}', 終了日 = '{2}' Where 教師コード = '{3}'", language, date_開始日txt, date_終了日txt, code_教師コード);

                    result = sqlcom.ExecuteNonQuery();

                    if (result != 1)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        MessageBox.Show(string.Format(@"教師[{0}]の情報が正常に更新されました。", code_教師コード));
                        traction.Commit();
                    }
                    if (((Form1)(this.Tag)).m_社員から教師へ登録Handle != null)
                    {
                        SendMessage(((Form1)(this.Tag)).m_社員から教師へ登録Handle, Form1.CUSTOM_MESSAGE, IntPtr.Zero, IntPtr.Zero);
                    }

                    this.Close();
                }
                else
                {
                    MessageBox.Show(string.Format(@"教師[{0}]の更新処理に失敗しました。", code_教師コード));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(@"教師[{0}]の更新処理に失敗しました。", code_教師コード));
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
            string date_開始日txt = dtp_開始日.Value.ToString("yyyy-MM-dd");
            string date_終了日txt = dtp_終了日.Value.ToString("yyyy-MM-dd");

            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库

            try
            {
                sqlcon.Open();
            }
            catch
            {
                ssl_errMsg.ForeColor = Color.Red;
                ssl_errMsg.Text = "DBサーバーの接続に失敗しました。";
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
                                    + " HL_JUKUKANRI_社員から教師へ変更 ("
                                    + " 教師コード "
                                    + " , 名前 "
                                    + " , メイン言語 "
                                    + " , 開始日 "
                                    + " , 終了日) "
                                    + " Values ('{0}', '{1}', '{2}', '{3}', '{4}')";

                sqlcom.CommandText = string.Format(sqlCommand, code_教師コード, name, language, date_開始日txt, date_終了日txt);

                int result = sqlcom.ExecuteNonQuery();
                if (result == 1)
                {
                    //教師情報登録
                    sqlCommand = @"Insert Into HL_JUKUKANRI_教師情報 ("
                                 + " 教師コード "
                                 + " , 名前 "
                                 + " , 所属会社 "
                                 + " , メイン言語 "
                                 + " , 開始日 "
                                 + " , 終了日) "
                                 + " Values ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')";


                    sqlcom.CommandText = string.Format(sqlCommand, code_教師コード, name, "社内", language, date_開始日txt, date_終了日txt);

                    result = sqlcom.ExecuteNonQuery();
                    if (result != 1)
                    {
                        throw new Exception();
                    }

                    MessageBox.Show(string.Format(@"教師[{0}]の情報が正常に登録されました。", code_教師コード));
                    traction.Commit();

                    if (((Form1)(this.Tag)).m_社員から教師へ登録Handle != null)
                    {
                        SendMessage(((Form1)(this.Tag)).m_社員から教師へ登録Handle, Form1.CUSTOM_MESSAGE, IntPtr.Zero, IntPtr.Zero);
                    }

                    this.Close();
                }
                else
                {
                    MessageBox.Show(string.Format(@"教師[{0}]の登録処理に失敗しました。", code_教師コード));
                }
            }
            catch  (Exception ex)
            {
                MessageBox.Show(string.Format(@"教師[{0}]の登録処理に失敗しました。", code_教師コード));
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
                    if (name.IndexOf("　") != txt_名前.Text.LastIndexOf("　"))
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
            }

            //メイン言語
            if (string.IsNullOrWhiteSpace(txt_メイン言語.Text))
            {
                ssl_errMsg.ForeColor = Color.Red;
                ssl_errMsg.Text = "[メイン言語]を入力してください。";
                return false;
            }

            //開始日 < 終了日 
            if (dtp_開始日.Value > dtp_終了日.Value)
            {
                ssl_errMsg.ForeColor = Color.Red;
                ssl_errMsg.Text = "[終了日]は[開始日]以後の日を設定してください。";
                return false;
            }

            return true;
        }

    }

}
