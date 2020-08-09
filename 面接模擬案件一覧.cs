using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace HL_塾管理
{
    public partial class 面接模擬案件一覧 : DockContent
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int BringWindowToTop(IntPtr hWnd);
        private AutoSizeFormClass asc = new AutoSizeFormClass();

        //DB接続情報
        private string connectionString = ComClass.connectionString;
        //ログイン職務
        public string user = "";

        public 面接模擬案件一覧()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.UpdateStyles();
        }

        protected override void WndProc(ref Message msg)
        {
            switch (msg.Msg)
            {
                case Form1.CUSTOM_MESSAGE:
                    {
                        LanguageSet();
                        CaseSet();
                    }
                    break;

                default:
                    base.WndProc(ref msg);
                    break;
            }
        }
        //画面初期化
        private void 面接模擬案件一覧_Load(object sender, EventArgs e)
        {
            LanguageSet();
        }

        //リストボックス右クリック
        private void listBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int listBoxItemTotalHeight = listBox1.Items.Count * listBox1.ItemHeight;            
                if(e.Y > listBoxItemTotalHeight)
                {
                    listBox1.ContextMenuStrip = contextMenuStrip1;
                    contextMenuStrip1.Items[1].Visible = false;
                    contextMenuStrip1.Items[2].Visible = false;
                }
                else
                {
                    listBox1.ContextMenuStrip = contextMenuStrip1;
                    contextMenuStrip1.Items[1].Visible = true;
                    contextMenuStrip1.Items[2].Visible = true;
                }
                int index = listBox1.IndexFromPoint(e.Location);
                if (index >= 0)
                {
                    listBox1.SelectedIndex = index;
                    this.contextMenuStrip1.Show(this.listBox1, e.Location);
                }
            }
        }

        List<LanguageInfo> L_Info = new List<LanguageInfo>();

        //コンボボックスのアイテム取得
        private void LanguageSet()
        {
            try
            {
                L_Info.Clear();
                SqlConnection connection = new SqlConnection(connectionString);
                string sql_cmd = "select ID項目,項目 from HL_JUKUKANRI_面接項目管理 where 画面='模擬案件'";
                SqlCommand cmd = new SqlCommand(sql_cmd, connection);
                connection.Open();
                SqlDataReader myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    L_Info.Add(new LanguageInfo
                    {
                        Language= Convert.ToString(myReader["項目"]),
                        ID = Convert.ToInt32(Convert.ToString(myReader["ID項目"]))
                    });
                }
                cbox_language.DataSource = L_Info;
            }
            catch(Exception ex)
            {

            }
        }

        private int FID = 0;
        private void cbox_language_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbox_language.SelectedIndex > -1)
            {
                FID = ((LanguageInfo)cbox_language.SelectedItem).ID;
            }
            CaseSet();
        }

        List<int> IDlist = new List<int>();
        private void CaseSet()
        {
            try
            {
                listBox1.Items.Clear();
                IDlist.Clear();
                SqlConnection connection = new SqlConnection(connectionString);
                string sql_cmd =string.Format("select ID質問,質問 from HL_JUKUKANRI_質問また案件管理 where 親ID='{0}' and 有効='1'", FID);
                SqlCommand cmd = new SqlCommand(sql_cmd, connection);
                connection.Open();
                SqlDataReader myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    listBox1.Items.Add(Convert.ToString(myReader["質問"]));
                    IDlist.Add(Convert.ToInt32(Convert.ToString(myReader["ID質問"])));
                }
                listBox1.SelectedIndex = -1;
            }
            catch (Exception ex)
            {

            }
        }

        int QID = 0;
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if(listBox1.SelectedIndex == -1)
                {
                    textBox1.Text = "";
                    return;
                }
                QID = IDlist[listBox1.SelectedIndex];
                SqlConnection connection = new SqlConnection(connectionString);
                string sql_cmd = string.Format("select 備考 from HL_JUKUKANRI_質問また案件管理 where ID質問='{0}'", QID);
                SqlCommand cmd = new SqlCommand(sql_cmd, connection);
                connection.Open();
                SqlDataReader myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    textBox1.Text=Convert.ToString(myReader["備考"]);                    
                }
            }
            catch (Exception ex)
            {
                textBox1.Text = "情報取得失敗した";
            }
        }

        private void 質問追加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            模擬案件一覧変更 frm = new 模擬案件一覧変更(1, FID, cbox_language.SelectedIndex, L_Info);
            frm.ShowDialog();
            CaseSet();
        }

        private void 変更ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            模擬案件一覧変更 frm = new 模擬案件一覧変更(2, QID, listBox1.SelectedItem.ToString(), textBox1.Text, L_Info);
            frm.ShowDialog();
            CaseSet();
        }

        private void 削除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CaseDelete();
            CaseSet();
        }

        private void CaseDelete()
        {
            try
            {
                QID = IDlist[listBox1.SelectedIndex];
                SqlConnection connection = new SqlConnection(connectionString);
                string sql_cmd = string.Format("update  HL_JUKUKANRI_質問また案件管理 set 有効='0' where ID質問='{0}' ", QID);
                SqlCommand sqlcmd = new SqlCommand(sql_cmd, connection);
                connection.Open();
                sqlcmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show(string.Format("削除成功しました", this.Text), "結果", MessageBoxButtons.OK);                
            }
            catch (Exception ex)
            {

            }
        }
    }

    class ListBoxNew : ListBox
    {
        public ListBoxNew()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.EnableNotifyMessage, true);
        }

        protected override void OnNotifyMessage(Message m)
        {
            if (m.Msg != 0x14)
            {
                base.OnNotifyMessage(m);
            }
        }
    }
    public struct LanguageInfo
    {
        public string Language;
        public int ID;
        public override string ToString()
        {
            return Language;
        }
    }
}
