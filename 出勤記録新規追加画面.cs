using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace HL_塾管理
{
    public partial class 出勤記録新規追加画面 : Form
    {
        private string m_学生コード = "";
        private string m_出勤機コード_登録コード = "";
        public DataGridViewRow m_dgvRow = null;

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, ref 出勤記録新規追加STRUCT lParam);

        public 出勤記録新規追加画面()
        {
            InitializeComponent();

            学生コードInit();
            this.rdb_出勤.Select();
            this.txt_出退勤時間.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        }

        public void Init(string str学生コード)
        {
            m_学生コード = str学生コード;
            if (!m_学生コード.Equals("ALL"))
            {
                for (int i = 0; i < this.cmb_学生コード.Items.Count; i++)
                {
                    if (m_学生コード.Equals(this.cmb_学生コード.Items[i].ToString().Split(' ')[0]))
                    {
                        this.cmb_学生コード.SelectedIndex = i;
                        this.cmb_学生コード.Enabled = false;
                        break;
                    }
                }
            }
        }

        private void 学生コードInit()
        {
            string connectionString = "";

            connectionString = ComClass.connectionString;

            SqlConnection conn = new SqlConnection(connectionString); //连接数据库

            try
            {
                conn.Open();
            }
            catch
            {
                this.toolStripStatusLabel1.ForeColor = Color.Red;
                this.toolStripStatusLabel1.Text = "サーバーがつなげない！インターネット接続をチェックしてください。";

                return;
            }

            string str_sqlcmd = string.Format(@"select * from HL_JINJI_出勤機_登録ユーザマスタ bbb inner join HL_JUKUKANRI_学生マスタ ddd on bbb.学生コード = ddd.学生コード and 離塾日 is NULL");

            SqlCommand com = new SqlCommand(str_sqlcmd, conn);

            SqlDataReader reader = com.ExecuteReader();

            this.cmb_学生コード.Items.Clear();

            while (reader.Read())
            {
                this.cmb_学生コード.Items.Add(reader["学生コード"].ToString() + " " + reader["名前"].ToString());
            }

            if (this.cmb_学生コード.Items.Count > 0)
            {
                this.cmb_学生コード.SelectedIndex = 0;
            }

            reader.Close();
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            クリア();
        }

        public void クリア()
        {
            this.txt_出退勤時間.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        }

        private bool 入力チェック()
        {
            if (this.txt_出退勤時間.Text.Equals(""))
            {
                this.toolStripStatusLabel1.ForeColor = Color.Red;
                this.toolStripStatusLabel1.Text = "出退勤時間未入力!";
                return false;
            }

            try
            {
                DateTime dateTime = DateTime.Parse(this.txt_出退勤時間.Text);
            }
            catch
            {
                this.toolStripStatusLabel1.ForeColor = Color.Red;
                this.toolStripStatusLabel1.Text = "正しい時間を入力してください。";

                return false;
            }

            return true;
        }

        private void get出勤機コード_登録コード(string str学生コード)
        {
            string connectionString = "";

            connectionString = ComClass.connectionString;

            SqlConnection conn = new SqlConnection(connectionString); //连接数据库

            try
            {
                conn.Open();
            }
            catch
            {
                this.toolStripStatusLabel1.ForeColor = Color.Red;
                this.toolStripStatusLabel1.Text = "サーバーがつなげない！インターネット接続をチェックしてください。";

                return;
            }

            string str_sqlcmd = string.Format(@"select top 1 * from HL_JINJI_出勤機_登録ユーザマスタ where 学生コード = '{0}'", str学生コード);

            SqlCommand com = new SqlCommand(str_sqlcmd, conn);

            SqlDataReader reader = com.ExecuteReader();

            while (reader.Read())
            {
                m_出勤機コード_登録コード = reader["出勤機コード"].ToString() + " " + reader["登録コード"].ToString();
            }

            reader.Close();
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (入力チェック() == false)
            {
                return;
            }

            string connectionString = "";

            connectionString = ComClass.connectionString;

            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
            }
            catch
            {
                this.toolStripStatusLabel1.ForeColor = Color.Red;
                this.toolStripStatusLabel1.Text = "サーバーがつなげない！インターネット接続をチェックしてください。";

                return;
            }

            DateTime dateTime = DateTime.Parse(this.txt_出退勤時間.Text);

            string sql = string.Format(@"
                delete HL_JINJI_出勤機元記録 where 出勤機コード = '{0}' and 登録コード = {1} and 出退勤時間 = '{6}'
                delete HL_JINJI_出勤機ダウンロード記録 where 出勤機コード = '{0}' and 登録コード = {1} and 年 = '{3}' and 月 = '{4}' and 日 = '{5}'
                insert into HL_JINJI_出勤機ダウンロード記録 values({0},{1},{3},{4},{5},'新規')
                insert into HL_JINJI_出勤機元記録 values({0},{1},{2},'{6}')",
            this.m_出勤機コード_登録コード.Split(' ')[0], this.m_出勤機コード_登録コード.Split(' ')[1],
            this.rdb_出勤.Checked?0:1, dateTime.Year,dateTime.Month,dateTime.Day,
            this.txt_出退勤時間.Text);

            SqlCommand com = new SqlCommand(sql, conn);

            try
            {
                com.ExecuteNonQuery();
                conn.Close();

                this.toolStripStatusLabel1.ForeColor = Color.Blue;
                this.toolStripStatusLabel1.Text = "出勤記録が正常に追加されました。";

                出勤記録新規追加STRUCT tmp_出勤記録新規追加STRUCT = new 出勤記録新規追加STRUCT();
                tmp_出勤記録新規追加STRUCT.comboBoxText = this.cmb_学生コード.Text;

                long result = SendMessage(((Form1)(this.Tag)).m_出勤表エラー記録Handle, Form1.CUSTOM_MESSAGE, IntPtr.Zero, ref tmp_出勤記録新規追加STRUCT).ToInt64();

                this.Close();
            }
            catch (Exception ex)
            {
                if (ex.Message.Length >= 0)
                {
                    this.toolStripStatusLabel1.ForeColor = Color.Red;
                    this.toolStripStatusLabel1.Text = "出勤記録追加が失敗しました。";
                }

                conn.Close();
            }
        }

        private void 出勤記録新規追加画面_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form1)(this.Tag)).m_出勤記録新規追加画面Handle = IntPtr.Zero;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            get出勤機コード_登録コード(this.cmb_学生コード.Text.Split(' ')[0]);
        }
    }
}
