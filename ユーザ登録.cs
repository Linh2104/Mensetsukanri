using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Net;

namespace HL_塾管理
{
    // add start by WYY
    public struct ユーザ登録情報
    {
        public string ユーザ名;
        public int 登録回数;
        public ユーザ登録情報(string a, int b)
        {
            ユーザ名 = a;
            登録回数 = b;
        }
    }

    // add end by WYY

    public partial class ユーザ登録 : Form
    {
        private AutoSizeFormClass asc = new AutoSizeFormClass();

        public string m_ログイン_教師コード = "";
        public string m_ログイン_ユーザ = "";
        public string m_ログイン_ユーザ氏名 = "";
        public string m_ログイン_職務 = "";
        public string m_パスワード = "";
        // add start by WYY
        public string ユーザ登録情報ファイル = "C:\\ProgramData\\TEMP\\login.csv";
        public static List<ユーザ登録情報> m_ユーザ登録情報_List = new List<ユーザ登録情報>();
        // add end by WYY
        public Form1 m_MainForm = null;

        public ユーザ登録()
        {
            InitializeComponent();
        }

        public void Writever(string filePathName, string newVer)
        {
            Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
            StreamWriter fileWriter = new StreamWriter(filePathName, false, sjisEnc);

            fileWriter.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace + "," + newVer);

            fileWriter.Flush();
            fileWriter.Close();
        }

        public string getver(string filePathName)
        {
            string ret = "";
            try
            {
                Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
                StreamReader fileReader = new StreamReader(filePathName, sjisEnc);

                string strLine = "";
                while (strLine != null)
                {
                    strLine = fileReader.ReadLine();
                    if (strLine != null && strLine.Length > 0)
                    {
                        if (strLine.Split(',')[0].Trim().Equals(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace))
                        {
                            ret = strLine.Split(',')[1];
                            break;
                        }
                    }
                }
                fileReader.Close();
            }
            catch
            {
                return ret;
            }
            return ret;
        }

        //获取外网IP
        private string GetExternalIP()
        {
            string direction = "";

            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://www.jec12.com/index.php/Home/Api/getIp");
                request.Timeout = 10000;
                request.UserAgent = "Code Sample Web Client";
                request.Credentials = CredentialCache.DefaultCredentials;
                using (WebResponse response = request.GetResponse())
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    direction = stream.ReadToEnd();
                }
            }
            catch
            {
                direction = "" + ComClass.getStrIP() + "";
            }
            return direction;
        }

        // add start by WYY
        public void ユーザ登録情報_save(string fileName)
        {
            CSVCLS rCsv = new CSVCLS();
            List<String[]> ss = new List<string[]>();

            foreach (ユーザ登録情報 tmp in m_ユーザ登録情報_List)
            {
                String[] line = { tmp.ユーザ名, tmp.登録回数.ToString() };

                ss.Add(line);
            }
            rCsv.WriteCSV(fileName, ss);
        }

        public void ユーザ登録情報_load(string fileName)
        {
            asc.controlAutoSize(this);

            CSVCLS rCsv = new CSVCLS();
            List<String[]> ss = rCsv.ReadCSV(fileName);

            m_ユーザ登録情報_List.Clear();
            foreach (String[] line in ss)
            {
                ユーザ登録情報 tmp = new ユーザ登録情報(line[0], int.Parse(line[1]));
                m_ユーザ登録情報_List.Add(tmp);
            }

            m_ユーザ登録情報_List.Sort(delegate (ユーザ登録情報 x, ユーザ登録情報 y) { return y.登録回数 - x.登録回数; });
            this.comboBox1.Items.Clear();
            for (int i = 0; i < m_ユーザ登録情報_List.Count(); i++)
            {
                this.comboBox1.Items.Add(m_ユーザ登録情報_List[i].ユーザ名);
            }

            if (this.comboBox1.Items.Count > 0)
            {
                this.comboBox1.SelectedIndex = 0;
            }

        }
        // add end by WYY         

        public bool login()
        {
            bool ret = false;
            string connectionString = "";

            connectionString = ComClass.connectionString;  // muhuaizhi updata 20180607  

            SqlConnection conn = new SqlConnection(connectionString); //连接数据库

            try
            {
                conn.Open();
            }
            catch
            {
                MessageBox.Show("サーバーがつなげない！インターネット接続をチェックしてください。");

                return ret;
            }

            
            string str_sqlcmd = string.Format(@"select t1.*, t2.名前 from HL_JUKUKANRI_塾ユーザマスタ as t1 
                                                    left join HL_JUKUKANRI_教師情報 as t2 on t1.教師コード = t2.教師コード 
                                                    where t1.ユーザ名 = N'{0}' and t1.パスワード = '{1}'",
                this.comboBox1.Text.ToLower().Trim(), this.textBox2.Text.ToLower().Trim());

            SqlCommand com = new SqlCommand(str_sqlcmd, conn);

            SqlDataReader reader = com.ExecuteReader();

            if (reader.Read())
            {
                m_ログイン_ユーザ = reader["ユーザ名"].ToString();
                m_ログイン_ユーザ氏名 = reader["名前"].ToString();
                m_ログイン_教師コード = reader["教師コード"].ToString();
                m_パスワード = reader["パスワード"].ToString();
                m_ログイン_職務 = reader["職務"].ToString();
                ret = true;
            }
            else
            {
                ret = false;
            }

            reader.Close();
            conn.Close();


            return ret;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.comboBox1.Text = "";
            this.textBox2.Text = "";
        }

        private void メニュー表示(Form1 m_MainForm)
        {
            if (m_ログイン_職務.IndexOf("管理") >= 0)
            {
                m_MainForm.社員から教師へ変更ToolStripMenuItem.Visible = true;
                m_MainForm.社外教師情報登録ToolStripMenuItem.Visible = true;
                m_MainForm.教室登録ToolStripMenuItem.Visible = true;
            }
            else
            {
                m_MainForm.社員から教師へ変更ToolStripMenuItem.Visible = false;
                m_MainForm.社外教師情報登録ToolStripMenuItem.Visible = false;
                m_MainForm.教室登録ToolStripMenuItem.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.Items.Count == 0)
            {
                load();
            }

            if (login())
            {
                this.label3.Text = "";
                this.Hide();

                if (m_MainForm == null)
                {
                    m_MainForm = new Form1();
                    m_MainForm.m_ユーザ登録 = this;
                    m_MainForm.Show();
                }
                else
                {
                    m_MainForm.load();
                }

                メニュー表示(m_MainForm);
                //m_MainForm.任務一覧表示();

                // add start by WYY
                int i = 0;
                bool flag = false;
                foreach (ユーザ登録情報 tmp in m_ユーザ登録情報_List)
                {
                    if (this.comboBox1.Text.Equals(tmp.ユーザ名))
                    {
                        ユーザ登録情報 tmp1 = new ユーザ登録情報(tmp.ユーザ名, tmp.登録回数 + 1);
                        m_ユーザ登録情報_List[i] = tmp1;
                        ユーザ登録情報_save(ユーザ登録情報ファイル);
                        flag = true;
                        break;
                    }
                    i++;
                }

                if (!flag)
                {
                    ユーザ登録情報 tmp1 = new ユーザ登録情報(this.comboBox1.Text, 1);
                    m_ユーザ登録情報_List.Add(tmp1);
                    ユーザ登録情報_save(ユーザ登録情報ファイル);
                }
                // add end by WYY

                this.comboBox1.Text = "";
                this.textBox2.Text = "";

            }
            else
            {
                this.label3.Text = "ユーザ名またはパスワードが間違いました。";
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }

        public void load()
        {
            string dir = "C:\\ProgramData\\TEMP";

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            // add start by WYY
            ユーザ登録情報_load(ユーザ登録情報ファイル);
            // add end by WYY
        }

        private void ユーザ登録_Load(object sender, EventArgs e)
        {
            load();
        }

        private void ユーザ登録_Activated(object sender, EventArgs e)
        {
            this.comboBox1.Focus();
        }

        private void ユーザ登録_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_MainForm != null)
            {
                this.label3.Text = "";
                this.comboBox1.Text = "";
                this.textBox2.Text = "";
                this.Hide();

                m_MainForm.ログインHiDE();

                e.Cancel = true;
                //throw new Exception();
            }
        }
    }
}
