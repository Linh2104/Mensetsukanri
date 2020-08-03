using System;
using System.Data;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Drawing;

namespace HL_塾管理
{
    public partial class 出勤機学生登録 : DockContent
    {
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        private AutoSizeFormClass asc = new AutoSizeFormClass();

        //データベース接続情報
        private string connectionString = ComClass.connectionString;

        //エラーメッセージ
        private Dictionary<string, string> errmsg = new Dictionary<string, string>();

        public delegate void UpdateDelegate(object sender, UpdateEventArgs args);
        public event UpdateDelegate UpdateEventHandler;

        public class UpdateEventArgs : EventArgs
        {
            public string Data { get; set; }
        }

        public 出勤機学生登録()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 画面表示
        /// </summary>
        private void Page_load(object sender, EventArgs e)
        {
            //asc.controlAutoSize(this);

            //出勤機コードリスト設定
            if (cmb_出勤機コード.Items.Count == 0)
            {
                SetComboxList();
            }
        }

        /// <summary>
        /// 出勤機コードリスト設定
        /// </summary>
        public void SetComboxList()
        {
            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库

            try
            {
                sqlcon.Open();
            }
            catch
            {
                toolStripStatusLabel1.Text = "";
                toolStripStatusLabel1.Text = "DBサーバーの接続に失敗しました。";
                return;
            }

            string str_sqlcmd = @"SELECT 出勤機コード,出勤機コード + ' ' +  場所 as 場所 FROM HL_JINJI_出勤機マスタ ORDER BY 出勤機コード";

            SqlDataAdapter sqlDa = new SqlDataAdapter(str_sqlcmd, sqlcon);
            DataTable dt = new DataTable();
            sqlDa.Fill(dt);

            cmb_出勤機コード.DisplayMember = "場所";
            cmb_出勤機コード.ValueMember = "出勤機コード"; 
            cmb_出勤機コード.DataSource = dt;

            //liuxiaoyan 0612 add 
            string sql = "select a.学生コード, a.学生コード + ' ' + a.名前 as 名前 from HL_JUKUKANRI_学生マスタ as a " +
                         "left join HL_JINJI_出勤機_登録ユーザマスタ as b " +
                         "on a.学生コード=b.学生コード where b.学生コード is null";
            SqlDataAdapter sqlda = new SqlDataAdapter(sql,sqlcon);
            DataTable dt1 = new DataTable();
            sqlda.Fill(dt1);

            cmb_学生コード.DisplayMember = "名前" ;
            cmb_学生コード.ValueMember = "学生コード";
            cmb_学生コード.DataSource = dt1;

            sqlcon.Close();
            //end
        }


        /// <summary>
        /// 入力チェック
        /// </summary>
        public void InputCheck()
        {
            errmsg = new Dictionary<string, string>();
            lbl_Msg.Text = "";
            lbl_Msg.Visible = false;
            cmb_出勤機コード.BackColor = System.Drawing.Color.Empty;
            txt_登録コード.BackColor = System.Drawing.Color.Empty;
            cmb_学生コード.BackColor = System.Drawing.Color.Empty;

            //出勤機コード
            if (string.IsNullOrWhiteSpace(cmb_出勤機コード.Text))
            {
                AddErrMsgList(lbl_出勤機コード.Text, "[出勤機コード]が選択されていません。");
                return;
            }

            //登録コード
            string code_登録コード = txt_登録コード.Text;
            if (string.IsNullOrWhiteSpace(code_登録コード))
            {
                AddErrMsgList(lbl_登録コード.Text, "[登録コード]が入力されていません。");
                return;
            }
            else if (!IsInt(code_登録コード))
            {
                AddErrMsgList(lbl_登録コード.Text, "[登録コード]に数字のみを入力してください。");
                return;
            }
            else
            {
                //存在チェック
                IsExsitedWith登録コード(code_登録コード);
            }

            //学生コード
            string code_学生コード = cmb_学生コード.Text;

            //2020/04/15 liuxiaoyan dd start
            if (errmsg.Count==0)
            {
                if (string.IsNullOrWhiteSpace(code_学生コード))
                {
                    AddErrMsgList(lbl_学生コード.Text, "[学生コード]を入力してください。");
                }

                if (!string.IsNullOrWhiteSpace(code_学生コード))
                {
                    //学生コード 存在チェック
                    IsExsitedWith学生コード(code_学生コード);
                } 
            }
        }

        /// <summary>
        /// Intチェック
        /// </summary>
        public static bool IsInt(string value)
        {
            return Regex.IsMatch(value, @"^[0-9]+$");
        }

        /// <summary>
        /// 登録コード存在チェック
        /// </summary>
        private void IsExsitedWith登録コード(string code_登録コード)
        {
            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库
            try
            {
                sqlcon.Open();
            }
            catch
            {
                AddErrMsgList(lbl_登録コード.Text, "[登録コード]在チェック処理]:DBサーバーの接続に失敗しました。");

                return;
            }

            string str_sqlcmd = string.Format(@"SELECT 登録コード FROM HL_JINJI_出勤機_登録ユーザマスタ WHERE 登録コード = {0}", code_登録コード);

            SqlCommand com = new SqlCommand(str_sqlcmd, sqlcon);
            SqlDataReader reader = com.ExecuteReader();

            if(reader.Read())
            {
                errmsg.Add(lbl_登録コード.Text, string.Format("[登録コード]({0})が既に登録されています。", code_登録コード));
                return;
            }

            if(sqlcon != null)
            {
                sqlcon.Close();
            }
        }

        /// <summary>
        /// 学生コード存在チェック
        /// </summary>
        private void IsExsitedWith学生コード(string code_学生コード)
        {
            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库
            try
            {
                sqlcon.Open();
            }
            catch
            {
                AddErrMsgList(lbl_学生コード.Text, "[学生コード存在チェック処理]:DBサーバーの接続に失敗しました。");

                return;
            }

            string str_sqlcmd = string.Format(@"SELECT 学生コード FROM HL_JINJI_出勤機_登録ユーザマスタ WHERE 学生コード = '{0}'", code_学生コード);

            SqlCommand com = new SqlCommand(str_sqlcmd, sqlcon);
            SqlDataReader reader = com.ExecuteReader();

            if (reader.Read())
            {
                errmsg.Add(lbl_学生コード.Text, string.Format("[学生コード]({0})が既に登録されています。", code_学生コード));
                return;
            }

            if (sqlcon != null)
            {
                sqlcon.Close();
            }
        }
        /// <summary>
        /// 登録ボタン
        /// </summary>
        private void btn_insert_Click(object sender, EventArgs e)
        {
            //入力チェック
            InputCheck();
            if(errmsg.Count > 0)
            {
                toolStripStatusLabel1.Text = "";
                //エラーメッセージ表示
                SetErrMsg();
                return;
            }

            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库

            int result = 0;
            try
            {
                sqlcon.Open();
            }
            catch
            {
                toolStripStatusLabel1.Text = "";
                toolStripStatusLabel1.Text = "DBサーバーの接続に失敗しました。";
                return;
            }
            SqlCommand sqlcom = new SqlCommand();
            sqlcom.Connection = sqlcon;

            try
            {
                //画面値取得
                string code_出勤機コード = cmb_出勤機コード.SelectedValue.ToString();
                int code_登録コード = Convert.ToInt32(txt_登録コード.Text);
                string code_学生コード = lbl_学生code.Text == null ? null : lbl_学生code.Text;

                //登録行う
                sqlcom.CommandText = string.Format(@"Insert Into HL_JINJI_出勤機_登録ユーザマスタ Values ('{0}', {1}, {2},'{3}')",
                                                  code_出勤機コード, code_登録コード,"NULL", code_学生コード);

                result = sqlcom.ExecuteNonQuery();

                if (result == 1)
                {
                    //tong start 20200527
                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Green;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "正常に登録されました。";
                    //tong end 20200527

                    if (((Form1)(this.Tag)).m_出勤機学生一覧Handle != null)
                    {
                        SendMessage(((Form1)(this.Tag)).m_出勤機学生一覧Handle, Form1.CUSTOM_MESSAGE, IntPtr.Zero, IntPtr.Zero);
                    }

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = "";
                toolStripStatusLabel1.Text = "登録処理が失敗しました。" + ex.Message;
            }
            finally
            {
                if (sqlcon != null)
                {
                    raiseUpdate();
                    sqlcon.Close();
                }
            }
        }

        /// <summary>
        ///  エラーメッセージ追加
        /// </summary>
        private void AddErrMsgList(string name, string msg)
        {
            if(errmsg.ContainsKey(name))
            {
                errmsg[name] += msg + "\r\n";
            }
            else
            {
                errmsg.Add(name, msg);
            }
        }

        /// <summary>
        /// エラー設定
        /// </summary>
        /// <remarks>エラー表示、文字色設定</remarks>
        private void SetErrMsg()
        {
            foreach (string controlName in errmsg.Keys)
            {
                switch(controlName)
                {
                    case "出勤機コード":
                        cmb_出勤機コード.BackColor = System.Drawing.Color.Red;
                        break;
                    case "登録コード":
                        txt_登録コード.BackColor = System.Drawing.Color.Red;
                        break;
                    case "学生コード":
                        cmb_学生コード.BackColor = System.Drawing.Color.Red;
                        break;
                    default:
                        break;
                }

                toolStripStatusLabel1.Visible = true;
                toolStripStatusLabel1.Text += errmsg[controlName] + Environment.NewLine;                
            }
        }

        //liu rui add 20200526
        /// <summary>
        /// 画面閉じ
        /// </summary>
        private void 出勤機学生登録_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form1)(this.Tag)).m_出勤機学生登録Handle = IntPtr.Zero;
        }

        /// <summary>
        /// 出勤機list変更
        /// </summary>
        private void cmb_出勤機コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmb_出勤機コード.Items != null)
            {
                lbl_出退勤Info.Text = cmb_出勤機コード.SelectedValue.ToString();
            }
        }

        /// <summary>
        /// ラベル文字色設定
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

        protected void raiseUpdate()
        {
            UpdateEventArgs args = new UpdateEventArgs();
            UpdateEventHandler?.Invoke(this, args);
        }

        private void cmb_学生コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_学生コード.Items != null)
            {
                lbl_学生code.Text = cmb_学生コード.SelectedValue.ToString();
            }
        }

        private void cmb_学生コード_Format(object sender, ListControlConvertEventArgs e)
        {

        }
    }
}
