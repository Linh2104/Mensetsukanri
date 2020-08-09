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
    public partial class 面接項目追加変更 : Form
    {
        //パターン
        private int pattern;
        //項目ID
        private int ID;
        //親項目ID
        private int FID;
        //親画面名
        //値：項目の場合　前提知識、面接練習；質問の場合　質問
        private string FScreenName; 
        //DB接続情報
        private string connectionString = ComClass.connectionString;
        public 面接項目追加変更()
        {
            InitializeComponent();
        }


        //親画面から追加・変更のパターンを継承
        //F：親項目ID
        //Pt=1：項目追加；Pt=2：項目変更；Pt=3：質問追加；Pt=4：質問変更；（仮設定）

        //追加
        public 面接項目追加変更(int Pt,int F, string S)
        {
            InitializeComponent();
            pattern = Pt;
            FID = F;
            FScreenName = S;
        }

        //項目名
        private string name = string.Empty;
        //項目タイトル
        private string note = string.Empty;
        //変更
        //親画面から項目のタイトルと備考を継承
        public 面接項目追加変更(int id, int Pt, int F, string S,string LN, string N)
        {
            InitializeComponent();
            ID = id;
            pattern = Pt;
            FID = F;
            FScreenName = S;
            name = LN;
            note = N;
        }

        //継承したパターンによる画面初期化が分岐
        private void 面接項目追加変更_Load(object sender, EventArgs e)
        {
            SetPattern(pattern);
        }

        //ラベル設定、変更の場合はテキストボックスの文字列も設定
        private void SetPattern(int P)
        {
            try
            {
                switch (P)
                {
                    case 1:
                        this.Text = "項目追加";
                        l_name.Text = "項目名";
                        b_exe.Text = "追加";
                        break;
                    case 2:
                        this.Text = "項目変更";
                        l_name.Text = "項目名";
                        b_exe.Text = "変更";
                        break;
                    case 3:
                        this.Text = "質問追加";
                        l_name.Text = "質問名";
                        b_exe.Text = "追加";
                        break;
                    case 4:
                        this.Text = "質問変更";
                        l_name.Text = "質問名";
                        b_exe.Text = "変更";
                        break;
                    default:
                        this.Close();
                        break;
                }
                t_name.Text = name;
                t_note.Text = note;
            }
            catch(Exception ex)
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
                        sql_cmd += string.Format("insert  HL_JUKUKANRI_面接項目管理  ([ID項目] ,[項目] ,[ID親] ,[備考] ,[画面] ,[有効])  values (((select isnull(a.ID項目,0) from (select max(ID項目) as ID項目 from HL_JUKUKANRI_面接項目管理) a)+1), '{0}', '{1}', '{2}', '{3}', '1')", l_name.Text, FID, l_note, FScreenName);
                        break;
                    case 2:
                        sql_cmd += string.Format("update  HL_JUKUKANRI_面接項目管理 set 項目='{0}',備考='{1}' where ID項目='{2}'", l_name.Text, l_note, ID);
                        break;
                    case 3:
                        sql_cmd += string.Format("insert  HL_JUKUKANRI_質問また案件管理  ([ID質問] ,[質問] ,[親ID] ,[備考] ,[有効] ,[フラッグ])  values (((select isnull(a.ID項目,0) from (select max(ID項目) as ID項目 from HL_JUKUKANRI_面接項目管理) a)+1), '{0}', '{1}', '{2}', '1', '{3}')", l_name.Text, FID, l_note, FScreenName);
                        break;
                    case 4:
                        sql_cmd += string.Format("update  HL_JUKUKANRI_質問また案件管理 set 項目='{0}',備考='{1}' where ID質問='{2}'", l_name.Text, l_note, ID);
                        break;
                }
                SqlCommand sqlcmd = new SqlCommand(sql_cmd, connection);
                connection.Open();
                sqlcmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show(string.Format("{0}成功しました", this.Text), "結果", MessageBoxButtons.OK);
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(string.Format("{0}失敗しました", this.Text), "結果", MessageBoxButtons.OK);
            }
        }
    }
}
