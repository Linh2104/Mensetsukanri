using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace HL_塾管理
{
    public partial class ログインユーザー登録 : Form
    {
        private AutoSizeFormClass asc = new AutoSizeFormClass();

        public ログインユーザー登録()
        {
            InitializeComponent();
        }

        private string connectionString = ComClass.connectionString;

        private Dictionary<string, string> errmsg = new Dictionary<string, string>();
        private void AddErrMsgList(string name, string msg)
        {
            if (errmsg.ContainsKey(name))
            {
                errmsg[name] += msg + "\r\n";
            }
            else
            {
                errmsg.Add(name, msg);
            }
        }

        //存在チェック
        public void IsExistedWith教師コード(string 登録コード)
        {
            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库
            try
            {
                sqlcon.Open();
            }
            catch
            {
                AddErrMsgList(cmb_teacher.Text, "[教師コード]存在チェック処理]:DBサーバーの接続に失敗しました。");

                return;
            }

            if (cmb_teacher.Text.StartsWith("K") || cmb_teacher.Text.StartsWith("k"))
            {
                string str_sqlcmd = string.Format(@"select * from HL_JUKUKANRI_社外教師マスタ where 教師コード = '{0}' ", cmb_teacher.Text.ToUpper().Trim());

                SqlCommand com = new SqlCommand(str_sqlcmd, sqlcon);

                SqlDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    sqlcon.Close();
                }
                else
                {
                    AddErrMsgList(cmb_teacher.Text, string.Format("[教師コード]({0})が登録されていません。", cmb_teacher.Text));
                    return;
                }

                reader.Close();
            }
            else
            {
                string str_sqlcmd = string.Format(@"select * from HL_JINJI_社員マスタ  where 社員コード = '{0}' ", cmb_teacher.Text.ToUpper().Trim());

                SqlCommand com = new SqlCommand(str_sqlcmd, sqlcon);

                SqlDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    sqlcon.Close();
                }
                else
                {
                    AddErrMsgList(cmb_teacher.Text, string.Format("[教師コード]({0})が登録されていません。", cmb_teacher.Text));
                    return;
                }

                reader.Close();
            }
        }

        //入力チェック
        public void Inputcheck()
        {
            errmsg = new Dictionary<string, string>();

            if (string.IsNullOrWhiteSpace(cmb_teacher.Text))
            {
                AddErrMsgList(cmb_teacher.Text, "教師コードを入力してください。");
                return;
            }
            else
            {
                IsExistedWith教師コード(cmb_teacher.Text);
            }

            if (string.IsNullOrWhiteSpace(tb_user.Text))
            {
                AddErrMsgList(tb_user.Text, "ユーザー名を入力してください。");
                return;
            }
            if (string.IsNullOrWhiteSpace(cmb_職務.Text))
            {
                AddErrMsgList(cmb_職務.Text, "職務を選択してください。");
                return;
            }

            if (string.IsNullOrWhiteSpace(tb_password.Text))
            {
                AddErrMsgList(tb_password.Text, "パスワードを入力してください。");
                return;
            }
            if (string.IsNullOrWhiteSpace(tb_passwordcheck.Text))
            {
                AddErrMsgList(tb_passwordcheck.Text, "パスワードをもう一度入力してください。");
                return;
            }
            else
            {
                if (tb_password.Text != tb_passwordcheck.Text)
                {
                    AddErrMsgList(tb_passwordcheck.Text, "パスワードは一致していません、もう一度確認してください。");
                    return;
                }
            }
        }


        private void SetErrMsg()
        {
            foreach (string controlName in errmsg.Keys)
            {
                switch (controlName)
                {
                    case "教師コード":
                        cmb_teacher.BackColor = System.Drawing.Color.Red;
                        break;
                    case "ユーザー名":
                        tb_user.BackColor = System.Drawing.Color.Red;
                        break;
                    case "パスワード":
                        tb_password.BackColor = System.Drawing.Color.Red;
                        break;
                    case "パスワード確認":
                        tb_passwordcheck.BackColor = System.Drawing.Color.Red;
                        break;

                    default:
                        break;
                }

                toolStripStatusLabel1.Visible = true;
                toolStripStatusLabel1.ForeColor = Color.Red;
                toolStripStatusLabel1.Text += errmsg[controlName] + Environment.NewLine;
            }
        }

        //登録を行う
        private void btn_login_Click(object sender, EventArgs e)
        {
            Inputcheck();
            if (errmsg.Count > 0)
            {
                toolStripStatusLabel1.Text = "";
                SetErrMsg();            
                return;
            }

            int result = 0;
            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库

            try
            {
                sqlcon.Open();
            }
            catch
            {
                toolStripStatusLabel1.ForeColor = Color.Red;
                toolStripStatusLabel1.Text = "DBサーバーの接続に失敗しました。";
                return;
            }
            SqlCommand sqlcom = new SqlCommand();
            sqlcom.Connection = sqlcon;

            string str_sqlcmd = string.Format(@"select * from HL_JUKUKANRI_塾ユーザマスタ where 教師コード = '{0}' ", cmb_teacher.Text.ToUpper().Trim());

            SqlCommand com = new SqlCommand(str_sqlcmd, sqlcon);

            SqlDataReader reader = com.ExecuteReader();

            if (reader.Read())
            {
                sqlcon.Close();
                toolStripStatusLabel1.ForeColor = Color.Red;
                toolStripStatusLabel1.Text = string.Format("[教師コード]({0})が既に登録されています。", cmb_teacher.Text);
            }
            else
            {
                sqlcon.Close();
                sqlcon.Open();
               
                sqlcom.CommandText = string.Format(@"Insert Into HL_JUKUKANRI_塾ユーザマスタ (教師コード,ユーザ名,パスワード, 職務) Values ('{0}', N'{1}', '{2}', N'{3}')",
                                       cmb_teacher.Text.ToUpper().Trim(), tb_user.Text, tb_password.Text, cmb_職務.Text);
                result = sqlcom.ExecuteNonQuery();
               

                sqlcon.Close();

                if (result == 1)
                {
                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Green;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = string.Format("[教師コード]({0})が正常に登録されました。",cmb_teacher.Text);
                    this.Close();
                }
                else
                {
                    toolStripStatusLabel1.ForeColor = Color.Red;
                    toolStripStatusLabel1.Text = "登録処理が失敗しました。";
                }

            }
        }
            
        private void ログインユーザー登録_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form1)(this.Tag)).m_ログインユーザー登録Handle = IntPtr.Zero;
        }

        private void ログインユーザー登録_Load(object sender, EventArgs e)
        {
            //asc.controlAutoSize(this);

            cmb_職務.SelectedIndex = 1;
            //教師コードlist
            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库
            try
            {
                sqlcon.Open();
            }
            catch
            {
                toolStripStatusLabel1.ForeColor = Color.Red;
                toolStripStatusLabel1.Text = "DBサーバーの接続に失敗しました。";
                return;
            }
            string str_sqlcmd = "select a.教師コード　from HL_JUKUKANRI_教師情報 as a " +
                                "left join  HL_JUKUKANRI_塾ユーザマスタ as b " +
                                "on a.教師コード=b.教師コード where b.教師コード is null ";
            SqlCommand com = new SqlCommand(str_sqlcmd, sqlcon);
            SqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                cmb_teacher.Items.Add(reader["教師コード"].ToString());
            }
                    
            sqlcon.Close();

        }

        private void cmb_teacher_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(cmb_teacher.Text))
            {
                SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库
                try
                {
                    sqlcon.Open();
                }
                catch
                {
                    toolStripStatusLabel1.ForeColor = Color.Red;
                    toolStripStatusLabel1.Text = "DBサーバーの接続に失敗しました。";
                    return;
                }
                string str_sqlcmd = "select 名前,職務 from HL_JUKUKANRI_教師情報 where 教師コード= " + "'" + cmb_teacher.Text + "'";

                SqlCommand com = new SqlCommand(str_sqlcmd, sqlcon);
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    lbl_name.Text = reader["名前"].ToString();
                    cmb_職務.Text = reader["職務"].ToString();
                }
                sqlcon.Close();
            }
        }

        private void ログインユーザー登録_FormClosed_1(object sender, FormClosedEventArgs e)
        {
            ((Form1)(this.Tag)).m_ログインユーザー登録Handle = IntPtr.Zero;
        }

        private void tb_password_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 'a' && e.KeyChar <= 'z') || (e.KeyChar >= 'A' && e.KeyChar <= 'Z')
                || (e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar == 8))
            {
                e.Handled = false;
            }
            else
            {
                toolStripStatusLabel1.ForeColor = Color.Red;
                toolStripStatusLabel1.Text = "英数字のみが入力できます。";
                e.Handled = true;
            }
        }

        private void tb_passwordcheck_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 'a' && e.KeyChar <= 'z') || (e.KeyChar >= 'A' && e.KeyChar <= 'Z')
                || (e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar == 8))
            {
                e.Handled = false;
            }
            else
            {
                toolStripStatusLabel1.ForeColor = Color.Red;
                toolStripStatusLabel1.Text = "英数字のみが入力できます。";
                e.Handled = true;
            }
        }

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
