using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace HL_塾管理
{
    public partial class 模擬案件一覧変更 : Form
    {
        //パターン
        private int pattern;
        //項目ID
        private int ID;
        //親項目ID
        private int FID;
        private int temp_idx;
        //親画面名
        //値：案件
        private string FScreenName = "案件";
        //DB接続情報
        private string connectionString = ComClass.connectionString;

        List<LanguageInfo> L_Info = new List<LanguageInfo>();

        public 模擬案件一覧変更()
        {
            InitializeComponent();
        }

        //親画面から追加・変更のパターンを継承
        //F：親項目ID
        //Pt=1：案件追加；Pt=2：案件変更；（仮設定）

        //追加
        public 模擬案件一覧変更(int Pt, int F, int idx, List<LanguageInfo> info)
        {
            InitializeComponent();
            pattern = Pt;
            FID = F;
            L_Info = info;
            temp_idx = idx;
        }

        private string Title = string.Empty;
        private string Note = string.Empty;
        //変更
        //親画面から項目のタイトルと備考を継承
        public 模擬案件一覧変更( int Pt, int id, string LN, string N, List<LanguageInfo> info)
        {
            InitializeComponent();
            ID = id;
            pattern = Pt;
            Title = LN;
            Note = N;
            L_Info = info;
        }

        //画面初期化
        private void 面接項目追加変更_Load(object sender, EventArgs e)
        {
            SetPattern(pattern);
        }

        //継承したパターンによる画面初期化が分岐
        //ラベル設定、変更の場合はテキストボックスの文字列も設定
        private void SetPattern(int P)
        {
            try
            {
                switch (P)
                {
                    case 1:
                        this.Text = "模擬案件登録";
                        b_exe.Text = "登録";
                        cbox_language.DataSource = L_Info;
                        cbox_language.SelectedIndex = temp_idx;
                        break;
                    case 2:
                        this.Text = "模擬案件変更";
                        b_exe.Text = "変更";
                        l_language.Visible = false;
                        cbox_language.Visible = false;
                        l_name.Location = new Point(l_name.Location.X, l_name.Location.Y - 50);
                        t_name.Location = new Point(t_name.Location.X, t_name.Location.Y - 50);
                        break;
                    default:
                        this.Close();
                        break;
                }
                t_name.Text = Title;
                t_note.Text = Note;
            }
            catch (Exception ex)
            {
                MessageBox.Show("画面初期化失敗しました", "結果", MessageBoxButtons.OK);
                this.Close();
            }
        }

        //画面閉める
        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //命令執行
        private void b_exe_Click(object sender, EventArgs e)
        {
            Execute(pattern);
        }

        //パターンによるSQL命令が分岐
        private void Execute(int P)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                string sql_cmd = string.Empty;
                switch (P)
                {
                    case 1:
                        sql_cmd += string.Format("insert  HL_JUKUKANRI_質問また案件管理  ([ID質問] ,[質問] ,[親ID] ,[備考] ,[有効] ,[フラッグ])  values (((select isnull(a.ID質問,0) from (select max(ID質問) as ID質問 from HL_JUKUKANRI_質問また案件管理) a)+1), '{0}', '{1}', '{2}', '1', '{3}')", t_name.Text, FID, t_note.Text, FScreenName);
                        break;
                    case 2:
                        sql_cmd += string.Format("update  HL_JUKUKANRI_質問また案件管理 set 項目='{0}',備考='{1}' where ID質問 ='{2}'", t_name.Text, t_note.Text, ID);
                        break;
                }
                SqlCommand sqlcmd = new SqlCommand(sql_cmd, connection);
                connection.Open();
                sqlcmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show(string.Format("{0}成功しました", this.Text), "結果", MessageBoxButtons.OK);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0}失敗しました", this.Text), "結果", MessageBoxButtons.OK);
            }
        }
       
             
        //選択項目のID獲得
        private void cbox_language_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbox_language.SelectedIndex > -1)
            {
                FID = ((LanguageInfo)cbox_language.SelectedItem).ID;
            }
        }

       
    }



}

