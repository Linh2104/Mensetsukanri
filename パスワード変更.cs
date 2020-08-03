using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing;

namespace HL_塾管理
{
    public partial class パスワード変更 : Form
    {
        private AutoSizeFormClass asc = new AutoSizeFormClass();

        public パスワード変更()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.textBox2.Text = "";
            this.textBox3.Text = "";
            this.textBox4.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox2.Text.Equals(((Form1)(this.Tag)).m_ユーザ登録.m_パスワード))
            {
                if (textBox3.Text == textBox2.Text) 
                {
                    toolStripStatusLabel1.Text = "パスワード（新）がパスワード（旧）と一致しています。直してください。";
                    return;
                }
                if (this.textBox3.Text.Equals(this.textBox4.Text) && textBox3.Text != "" && textBox3.Text != null) 
                {
                    string connectionString = "";

                    connectionString = ComClass.connectionString;  // muhuaizhi updata 20180607

                    SqlConnection conn = new SqlConnection(connectionString);

                    try
                    {
                        conn.Open();
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "サーバーがつなげない！インターネット接続をチェックしてください。";
                        return;
                    }

                    //add lliuxiaoyan 20200427
                    //string sql = string.Format(@"update HL_EIGYO_ユーザ set パスワード = '{0}' where ユーザ = '{1}'",
                    //this.textBox3.Text,((Form1)(this.Tag)).m_ユーザ登録.m_ログイン_ユーザ);

                    string sql = string.Format(@"update HL_JUKUKANRI_塾ユーザマスタ set パスワード = '{0}' where ユーザ名 = N'{1}'",
                    this.textBox3.Text, ((Form1)(this.Tag)).m_ユーザ登録.m_ログイン_ユーザ);
                    // end

                    SqlCommand com = new SqlCommand(sql, conn);
                    try
                    {
                        int result = com.ExecuteNonQuery();
                        if (result == 1)
                        {
                            ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Green;
                            ((Form1)(this.Tag)).toolStripStatusLabel2.Text = string.Format("ユーザ[{0}]のパスワードが正常に変更されました。", ((Form1)(this.Tag)).m_ユーザ登録.m_ログイン_ユーザ);
                            ((Form1)(this.Tag)).reLoad = false;
                            ((Form1)(this.Tag)).m_ユーザ登録.m_パスワード = textBox3.Text;
                            conn.Close();                         
                            this.Close();
                        }                   
                    }
                    catch (Exception ex)
                    {
                        toolStripStatusLabel1.Text= ex.ToString();
                    }
         
                }
                else
                {
                    if (textBox3.Text == "" || textBox3.Text == null)
                    {
                        toolStripStatusLabel1.Text = "パスワード（新）を入力してください。";
                        return;
                    }
                    if (textBox4.Text == "" || textBox4.Text == "")
                    {   
                        toolStripStatusLabel1.Text = "パスワード（確認）を入力してください。";
                        return;
                    }
                    
                    this.toolStripStatusLabel1.Text = "二回入力したパスワードが一致ではありません。";
                }
            }
            else
            {
                if (textBox2.Text == "" || textBox2.Text == null)
                {
                    toolStripStatusLabel1.Text = "パスワード（旧）を入力してください。";
                    return;
                }
                if (textBox2.Text != (((Form1)(this.Tag)).m_ユーザ登録.m_パスワード))
                {
                    this.toolStripStatusLabel1.Text = "パスワード（旧）が間違いました！";
                    return;
                }                             
                
            }
        }

        private void パスワード変更_Load(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);
            this.textBox1.Text = ((Form1)(this.Tag)).m_ユーザ登録.m_ログイン_ユーザ;
        }

        private void パスワード変更_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form1)(this.Tag)).m_パスワード変更Handle = IntPtr.Zero;
        }
    }
}
