using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Documents;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Word = Microsoft.Office.Interop.Word;

namespace HL_塾管理
{
    public partial class 学生情報一覧 : DockContent
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int BringWindowToTop(IntPtr hWnd);
        private AutoSizeFormClass asc = new AutoSizeFormClass();

        //データベース接続情報
        private string connectionString = ComClass.connectionString;

        //セル編集フラグ
        private bool isEditing = false;

        //編集前のセル値
        private string beforValue = "";

        //離塾原因データテーブル
        private DataTable dt_離塾Info = null;

        //書類印刷変数声明
        public struct 書類データ
        {
            public string 氏名;
            public string 郵便番号;
            public string 住所;
            public string 勤務先名称;
            public string 勤務先郵便番号;
            public string 勤務先住所;
            public string 勤務先電話番号;
            public string 勤務先ウェッブサイト;
            public System.Drawing.Image 勤務先商標;
        }
        public 書類データ m_書類データ;
        private bool isPrintOK { get; set; }

        //画面項目
        private string code_クラスコード = "";
        private string code_学生コード = "";
        private List<string> list_学生 = new List<string>();
        private string login_ユーザ = "";
        private string 職務 = "";
        private string login_教師コード = "";
        private int key;

        public 学生情報一覧()
        {
            InitializeComponent();
        }

        protected override void WndProc(ref System.Windows.Forms.Message msg)
        {
            switch (msg.Msg)
            {
                case Form1.CUSTOM_MESSAGE:
                    {
                        DisplayGridView();
                    }
                    break;

                default:
                    base.WndProc(ref msg);
                    break;
            }
        }

        /// <summary>
        ///画面表示
        /// </summary>
        private void 学生情報一覧_Load(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);

            login_ユーザ = ((Form1)(this.Tag)).m_ユーザ登録.m_ログイン_ユーザ;
            login_教師コード = ((Form1)(this.Tag)).m_ユーザ登録.m_ログイン_教師コード;
            職務 = ((Form1)(this.Tag)).m_ユーザ登録.m_ログイン_職務;

            //一覧表示
            //SetList();
            DisplayGridView();

            cmb_学生.SelectedIndex = 0;
        }

        /// <summary>
        ///離塾原因リスト取得
        /// </summary>
        private void GetDropDownListInfo()
        {
            //離塾原因リスト情報
            if (dt_離塾Info == null || dt_離塾Info.Rows.Count == 0)
            {
                dt_離塾Info = new DataTable();
                dt_離塾Info.Columns.Add("IndexValue");
                dt_離塾Info.Columns.Add("離塾原因");
                //0:空白(NULL)；1：卒業；2：キャンセル；3:除籍
                dt_離塾Info.Rows.Add("0", "-");
                dt_離塾Info.Rows.Add("1", "卒業");
                dt_離塾Info.Rows.Add("2", "キャンセル");
                dt_離塾Info.Rows.Add("3", "除籍");
            }

        }

        /// <summary>
        /// 検索処理
        /// </summary>
        private void DisplayGridView()
        {
            gv_studentsInfo.ScrollBars = ScrollBars.None;
            gv_studentsInfo.Rows.Clear();

            if (((Form1)(this.Tag)).reLoad)
            {
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "";
            }
            //画面値を取得
            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库

            try
            {
                sqlcon.Open();
            }
            catch
            {
                ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "DBサーバーの接続に失敗しました.";
                ((Form1)(this.Tag)).reLoad = true;
                return;
            }

            string str_sqlcmd = "SELECT"
                            + "　 T1.学生コード "
                            + "　, T1.名前 "
                            + "　, T1.カタカナ "
                            + "　, T1.ローマ字表記 "
                            + "　, T1.生年月日 "
                            + "　, T4.課程 "
                            + "　, T3.クラスコード "
                            + "　, T1.性別 "
                            + "　, T1.入塾日 "
                            + "　, T1.離塾日 "
                            + "　, (Case When T1.離塾原因 = 1 then '卒業' When T1.離塾原因 = 2 then 'キャンセル' When T1.離塾原因 = 3 then '除籍' Else '-' end) as 離塾原因 "
                            + "  , T2.郵便番号 "
                            + "  , T2.住所 "
                            + "  , T2.携帯 "
                            + "  , T2.メール "
                            + "  , T2.国籍 "
                            + "  , T2.学校名 "
                            + "  , T4.学生コード as クラスメンバー"
                            + "  , T2.社員コード"
                            + "  , T6.社員種別 as 研修フラグ"
                            + " FROM "
                            + "   HL_JUKUKANRI_学生マスタ AS T1 "
                            + "   LEFT JOIN HL_JUKUKANRI_学生情報 AS T2 "
                            + "     ON T1.学生コード = T2.学生コード "
                            + "   LEFT JOIN HL_JUKUKANRI_学生クラス AS T3 "
                            + "     ON T1.学生コード = T3.学生コード "
                            + "   LEFT JOIN HL_JUKUKANRI_クラス履歴 AS T4 "
                            + "     ON T3.クラスコード = T4.クラスコード "
                            + "   LEFT JOIN HL_JINJI_応募者情報 AS T5 "
                            + "     ON T2.応募者ID = T5.id "
                            + "   LEFT JOIN HL_JINJI_社員在職状態 T6"
                            + "     ON T2.社員コード = T6.社員コード  Where 1=1";

            if (職務 == "一般ユーザ")
            {
                str_sqlcmd += string.Format(" And T3.クラスコード = '' or T3.クラスコード is null or T4.教師コード = '{0}'", login_教師コード);
            }

            if (cmb_学生.Text == "一般")
            {
                str_sqlcmd += " And T6.社員種別 is null";
            }
            else if (cmb_学生.Text == "研修")
            {
                str_sqlcmd += " And T6.社員種別 ='研修'";
            }

            str_sqlcmd += " ORDER BY T1.学生コード + 0 ASC";
            SqlCommand com = new SqlCommand(str_sqlcmd, sqlcon);
            SqlDataReader reader = com.ExecuteReader();

            int index = 0;
            try
            { 
                while (reader.Read())
                {
                    if (
                       (reader["学生コード"].ToString().IndexOf(this.txt_searchKey.Text) < 0)
                       &&
                       (reader["名前"].ToString().IndexOf(this.txt_searchKey.Text) < 0)
                       &&
                       (reader["カタカナ"].ToString().IndexOf(this.txt_searchKey.Text) < 0)
                       &&
                       (reader["ローマ字表記"].ToString().IndexOf(this.txt_searchKey.Text) < 0)
                       &&
                       (reader["生年月日"].ToString().Replace('/', '-').IndexOf(this.txt_searchKey.Text) < 0)
                       &&
                       (reader["クラスコード"].ToString().IndexOf(this.txt_searchKey.Text) < 0)
                       &&
                       (reader["課程"].ToString().IndexOf(this.txt_searchKey.Text) < 0)
                       &&
                       (reader["性別"].ToString().IndexOf(this.txt_searchKey.Text) < 0)
                       &&
                       (reader["入塾日"].ToString().Replace('/', '-').IndexOf(this.txt_searchKey.Text) < 0)
                       &&
                       (reader["離塾日"].ToString().Replace('/', '-').IndexOf(this.txt_searchKey.Text) < 0)
                       &&
                       (reader["離塾原因"].ToString().IndexOf(this.txt_searchKey.Text) < 0)
                       &&
                       (reader["郵便番号"].ToString().IndexOf(this.txt_searchKey.Text) < 0)
                       &&
                       (reader["住所"].ToString().IndexOf(this.txt_searchKey.Text) < 0)
                       &&
                       (reader["携帯"].ToString().IndexOf(this.txt_searchKey.Text) < 0)
                       &&
                       (reader["メール"].ToString().IndexOf(this.txt_searchKey.Text) < 0)
                       &&
                       (reader["国籍"].ToString().IndexOf(this.txt_searchKey.Text) < 0)
                       &&
                       (reader["学校名"].ToString().IndexOf(this.txt_searchKey.Text) < 0)
                       )
                    {
                        continue;
                    }
                    //データ値表示
                    this.gv_studentsInfo.Rows.Add();
                    this.gv_studentsInfo.Rows[index].Cells["学生コード"].Value = reader["学生コード"].ToString();
                    this.gv_studentsInfo.Rows[index].Cells["名前"].Value = reader["名前"].ToString();
                    this.gv_studentsInfo.Rows[index].Cells["カタカナ"].Value = reader["カタカナ"].ToString();
                    this.gv_studentsInfo.Rows[index].Cells["ローマ字表記"].Value = reader["ローマ字表記"].ToString();
                    this.gv_studentsInfo.Rows[index].Cells["生年月日"].Value = ((DateTime)reader["生年月日"]).ToString("yyyy-MM-dd");
                    //liuxiaoyan 0611 add
                    this.gv_studentsInfo.Rows[index].Cells["クラス課程"].Value = reader["課程"].ToString();
                    this.gv_studentsInfo.Rows[index].Cells["クラスコード"].Value = reader["クラスコード"].ToString();
                    this.gv_studentsInfo.Rows[index].Cells["性別"].Value = reader["性別"].ToString();
                    this.gv_studentsInfo.Rows[index].Cells["入塾日"].Value = string.IsNullOrWhiteSpace(reader["入塾日"].ToString()) ? "" : ((DateTime)reader["入塾日"]).ToString("yyyy-MM-dd");
                    this.gv_studentsInfo.Rows[index].Cells["離塾日"].Value = string.IsNullOrWhiteSpace(reader["離塾日"].ToString()) ? "" : ((DateTime)reader["離塾日"]).ToString("yyyy-MM-dd");

                    //1：卒業；2：キャンセル；3:除籍
                    this.gv_studentsInfo.Rows[index].Cells["離塾原因"].Value = reader["離塾原因"].ToString();
                    string code_郵便 = reader["郵便番号"].ToString();
                    if (code_郵便.IndexOf("-") >= 0)
                    {
                        code_郵便 = code_郵便.Replace("-", "");
                    }
                    code_郵便 = code_郵便.Insert(3, "-");
                    this.gv_studentsInfo.Rows[index].Cells["郵便番号"].Value = "〒" + code_郵便;
                    this.gv_studentsInfo.Rows[index].Cells["住所"].Value = reader["住所"].ToString();
                    this.gv_studentsInfo.Rows[index].Cells["携帯"].Value = reader["携帯"].ToString();
                    this.gv_studentsInfo.Rows[index].Cells["メール"].Value = reader["メール"].ToString();
                    this.gv_studentsInfo.Rows[index].Cells["国籍"].Value = reader["国籍"].ToString();
                    this.gv_studentsInfo.Rows[index].Cells["学校名"].Value = reader["学校名"].ToString();
                    this.gv_studentsInfo.Rows[index].Cells["クラスメンバー"].Value = reader["クラスメンバー"].ToString();
                    this.gv_studentsInfo.Rows[index].Cells["研修"].Value = reader["研修フラグ"].ToString();
                    this.gv_studentsInfo.Rows[index].Cells["社員コード"].Value = reader["社員コード"].ToString();

                    foreach (DataGridViewCell gvCell in this.gv_studentsInfo.Rows[index].Cells)
                    {
                        if (string.IsNullOrWhiteSpace(gvCell.Value.ToString()) || gvCell.Value.ToString() == "-")
                        {
                            gvCell.Value = "-";
                            this.gv_studentsInfo.Rows[index].Cells[gvCell.ColumnIndex].Style.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
                        }
                        //chen 20200720 add
                        //研修社員の場合は編集不可
                        if (this.gv_studentsInfo.Rows[index].Cells["研修"].Value.ToString() == "研修")
                        {
                            if (gv_studentsInfo.Columns[gvCell.ColumnIndex].HeaderText == "学生コード")
                            {
                                this.gv_studentsInfo.Rows[index].Cells[gvCell.ColumnIndex].ToolTipText = "研修";
                            }

                            this.gv_studentsInfo.Rows[index].ReadOnly = true;
                            this.gv_studentsInfo.Rows[index].DefaultCellStyle.BackColor = Color.LightGreen;
                            this.gv_studentsInfo.Rows[index].DefaultCellStyle.ForeColor = Color.Black;
                            this.gv_studentsInfo.Rows[index].Cells["入塾日"].ReadOnly = false;
                            this.gv_studentsInfo.Rows[index].Cells["入塾日"].Style.BackColor = Color.FromArgb(128,255,255);
                            this.gv_studentsInfo.Rows[index].Cells["入塾日"].Style.ForeColor = Color.Blue;
                            this.gv_studentsInfo.Rows[index].Cells["クラス課程"].Style.ForeColor = Color.Blue;
                            this.gv_studentsInfo.Rows[index].Cells["クラス課程"].Style.BackColor = Color.FromArgb(128, 255, 255);

                        }

                        if (this.gv_studentsInfo.Rows[index].Cells["クラス課程"].Value.ToString() == "-")
                        {
                            this.gv_studentsInfo.Rows[index].Cells["クラス課程"].Style.BackColor = Color.Yellow;
                        }
                        //chen 20200720 add

                    }

                    index++;
                }
                gv_studentsInfo.ScrollBars = ScrollBars.Both;
                reader.Close();

            }
            catch(Exception ex)
            {
                ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "検索処理に失敗しました." + ex.Message;
                ((Form1)(this.Tag)).reLoad = false;
                if (sqlcon != null)
                {
                    sqlcon.Close();
                }
                return;
            }

            if (sqlcon != null)
            {
                sqlcon.Close();
            }

            //件数表示
            this.statusLbl_count.Text = string.Format("{0}件", index);
            ((Form1)(this.Tag)).reLoad = true;
        }

        /// <summary>
        /// 画面閉じ
        /// </summary>
        private void 学生情報一覧_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form1)(this.Tag)).m_学生情報一覧Handle = IntPtr.Zero;
        }

        /// <summary>
        /// 新規画面へ遷移する
        /// </summary>
        private void 新規ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((Form1)(this.Tag)).m_新規学生入塾Handle != IntPtr.Zero)
            {
                BringWindowToTop(((Form1)(this.Tag)).m_新規学生入塾Handle);
                return;
            }

            学生情報 m_NewForm_新規学生入塾 = new 学生情報();
            m_NewForm_新規学生入塾.isUpdate = "new";
            m_NewForm_新規学生入塾.Tag = ((Form1)(this.Tag));
            m_NewForm_新規学生入塾.Show(((Form1)(this.Tag)).dockPanel1);
            ((Form1)(this.Tag)).m_新規学生入塾Handle = m_NewForm_新規学生入塾.Handle;
        }

        /// <summary>
        /// 変更画面へ遷移する
        /// </summary>
        private void 変更ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            code_学生コード = gv_studentsInfo.CurrentRow.Cells["学生コード"].Value.ToString();
            if (((Form1)(this.Tag)).codeDic.ContainsKey(code_学生コード))
            {
                ((Form1)(this.Tag)).codeDic.TryGetValue(code_学生コード, out key);
                BringWindowToTop((IntPtr)key);
                return;
            }

            学生情報 m_NewForm_学生情報変更 = new 学生情報();
            m_NewForm_学生情報変更.isUpdate = "update";
            m_NewForm_学生情報変更.code_学生コード = code_学生コード;
            m_NewForm_学生情報変更.社員種別 = gv_studentsInfo.CurrentRow.Cells["研修"].Value.ToString();
            m_NewForm_学生情報変更.code_社員 = gv_studentsInfo.CurrentRow.Cells["社員コード"].Value.ToString();
            m_NewForm_学生情報変更.Tag = ((Form1)(this.Tag));
            m_NewForm_学生情報変更.Show(((Form1)(this.Tag)).dockPanel1);
            ((Form1)(this.Tag)).m_学生情報変更Handle = m_NewForm_学生情報変更.Handle;

            int ptr = (int)((Form1)(this.Tag)).m_学生情報変更Handle;
            if (!((Form1)(this.Tag)).codeDic.ContainsKey(code_学生コード))
            {
                ((Form1)(this.Tag)).codeDic.Add(code_学生コード, ptr);
            }
        }

        /// <summary>
        ///  一覧から選択行を退塾処理
        /// </summary>
        private void 退塾ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string code_学生コード = this.gv_studentsInfo.Rows[this.gv_studentsInfo.SelectedRows[0].Index].Cells["学生コード"].Value.ToString();
            if (((Form1)(this.Tag)).codeDic.ContainsKey(code_学生コード))
            {
                ((Form1)(this.Tag)).codeDic.TryGetValue(code_学生コード, out key);
                BringWindowToTop((IntPtr)key);
                return;
            }
            //add by Linh 20200612 画面を閉じてから再開できないバッグ修正　↑

            学生情報 m_NewForm_学生退塾 = new 学生情報();
            m_NewForm_学生退塾.isUpdate = "delete";
            m_NewForm_学生退塾.code_学生コード = code_学生コード;
            m_NewForm_学生退塾.社員種別 = gv_studentsInfo.CurrentRow.Cells["研修"].Value.ToString();
            m_NewForm_学生退塾.Tag = ((Form1)(this.Tag));
            m_NewForm_学生退塾.Show(((Form1)(this.Tag)).dockPanel1);
            ((Form1)(this.Tag)).m_学生退塾Handle = m_NewForm_学生退塾.Handle;

            //add by Linh 20200612 画面を閉じてから再開できないバッグ修正　↓
            int ptr = (int)((Form1)(this.Tag)).m_学生退塾Handle;
            if (!((Form1)(this.Tag)).codeDic.ContainsKey(code_学生コード))
            {
                ((Form1)(this.Tag)).codeDic.Add(code_学生コード, ptr);
            }
            //add by Linh 20200612 画面を閉じてから再開できないバッグ修正　↑
        }

        /// <summary>
        ///右クリックで一行を選択
        /// </summary>
        private void roomView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    if ((this.gv_studentsInfo.Rows[e.RowIndex].Selected == false))
                    {
                        this.gv_studentsInfo.ClearSelection();
                        this.gv_studentsInfo.Rows[e.RowIndex].Selected = true;
                    }
                    //'只选中一行时设置活动单元格
                    if ((this.gv_studentsInfo.SelectedRows.Count == 1))
                    {
                        this.gv_studentsInfo.CurrentCell = this.gv_studentsInfo.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    }
                }
            }
        }

        /// <summary>
        ///メニューを開く
        /// </summary>
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            System.Drawing.Point startPosition = Cursor.Position;

            System.Drawing.Point point = gv_studentsInfo.PointToClient(startPosition);
            DataGridView.HitTestInfo hitinfo;
            hitinfo = gv_studentsInfo.HitTest(point.X, point.Y);

            gv_studentsInfo.ClearSelection();
            if (hitinfo.RowIndex >= 0)
            {
                gv_studentsInfo.Rows[hitinfo.RowIndex].Selected = true;
            }
            else
            {
                e.Cancel = true;
            }

            if (this.gv_studentsInfo.CurrentRow.Cells["研修"].Value.ToString() == "研修" 
                || string.IsNullOrWhiteSpace(this.gv_studentsInfo.CurrentRow.Cells["社員コード"].Value.ToString()))
            {
                contextMenuStrip1.Items[2].Visible = false;
                contextMenuStrip1.Items[3].Visible = false;
            }
            else
            {
                contextMenuStrip1.Items[2].Visible = true;
                contextMenuStrip1.Items[3].Visible = true;

                string str_卒業日 = "";
                string str離塾原因 = this.gv_studentsInfo.CurrentRow.Cells["離塾原因"].Value.ToString();
                if (str離塾原因 == "卒業")
                {
                    str_卒業日 = this.gv_studentsInfo.CurrentRow.Cells["離塾日"].Value.ToString();
                    if(DateTime.Parse(str_卒業日) > DateTime.Now)
                    {
                        contextMenuStrip1.Items[2].Enabled = true;
                        contextMenuStrip1.Items[3].Enabled = false;
                    }
                    else
                    {
                        contextMenuStrip1.Items[2].Enabled = false;
                        contextMenuStrip1.Items[3].Enabled = true;
                    }
                }
                else if(str離塾原因.Replace("-","") == "")
                {
                    contextMenuStrip1.Items[2].Enabled = true;
                    contextMenuStrip1.Items[3].Enabled = false;
                }
            }

        }

        /// <summary>
        ///更新処理
        /// </summary>
        private bool Update_GridViewRow(string code_学生, string columnName, object[] cellValue)
        {
            bool isUpdate = false;
            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库

            int result = 0;
            try
            {
                sqlcon.Open();
            }
            catch
            {
                ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "DBサーバーの接続に失敗しました.";
                ((Form1)(this.Tag)).reLoad = false;

                return isUpdate;
            }

            SqlCommand sqlcom = new SqlCommand();
            sqlcom.Connection = sqlcon;

            //データチェック
            string cell_value = cellValue[0].ToString();
            cell_value = cell_value == "-" ? null : cell_value;

            if (!InputCheck(code_学生, columnName, cell_value))
            {
                return isUpdate;
            }

            try
            {
                //更新行う
                string updateTable = "";

                //変更場所取得
                switch (columnName)
                {
                    case "名前":
                    case "カタカナ":
                    case "ローマ字表記":
                    case "生年月日":
                    case "性別":
                    case "入塾日":
                        updateTable = "HL_JUKUKANRI_学生マスタ";
                        break;
                    case "郵便番号":
                    case "住所": 
                    case "携帯":
                    case "メール":
                    case "国籍":
                        //20200707 add ou
                        if (columnName == "郵便番号")
                        {
                            if (cell_value.Substring(0, 1) == "〒")
                            {
                                cell_value = cell_value.Substring(1, cell_value.Length - 1);
                                cell_value = cell_value.Replace("-", "");
                            }
                        }
                        updateTable = "HL_JUKUKANRI_学生情報";
                        break;
                    case "学校名":
                        updateTable = "HL_JUKUKANRI_学生卒業学校";
                        break;
                    default:
                        break;
                }
                sqlcom.CommandText = string.Format("Update {0} Set {1} = '{2}' Where 学生コード = '{3}'",　updateTable, columnName, cell_value, code_学生);

                result = sqlcom.ExecuteNonQuery();

                if (result != 1)
                {
                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = string.Format("学生コード[{0}]の更新処理が失敗しました.", code_学生);
                    ((Form1)(this.Tag)).reLoad = false;
                }
                else
                {
                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Green;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = string.Format("学生コード[{0}]の情報が正常に更新しました.", code_学生);
                    isUpdate = true;
                }
            }
            catch
            {
                ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = string.Format("学生コード[{0}]の更新処理が失敗しました.", code_学生);
            }
            finally
            {
                if (sqlcon != null)
                {
                    sqlcon.Close();
                }
            }
            ((Form1)(this.Tag)).reLoad = false;
            return isUpdate;
        }

        /// <summary>
        ///データがおかしい
        /// </summary>
        private void gv_studentsInfo_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (gv_studentsInfo.Rows[e.RowIndex].IsNewRow)
            {
                return;
            }
        }

        /// <summary>
        /// 検索ボタン
        /// </summary>
        private void btn_search_Click(object sender, EventArgs e)
        {
            //datagridview再表示
            DisplayGridView();
        }

        /// <summary>
        ///検索textbox
        /// </summary>
        private void txt_searchKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //datagridview再表示
                DisplayGridView();
            }
        }

        /// <summary>
        ///クラスに学生を入れる
        /// </summary>
        private int Addクラス学生(string code_学生コード, string code_クラスコード, SqlCommand sqlcom)
        {
            int result;
            sqlcom.CommandText = string.Format(@"UPDATE [dbo].[HL_JUKUKANRI_学生クラス] SET [クラスコード] = '{0}' WHERE [学生コード] = {1}", code_クラスコード, code_学生コード);
            result = sqlcom.ExecuteNonQuery();
            if(result!=1)
            {
                return 0; 
            }

            if(!list_学生.Contains(code_学生コード))
            {
                list_学生.Add(code_学生コード);
            }
            list_学生.Sort();
            if (list_学生.Count > 0)
            {
                code_学生コード = string.Join(",", list_学生);
            }
            sqlcom.CommandText = string.Format(@"UPDATE [dbo].[HL_JUKUKANRI_クラス履歴] SET [学生コード] = '{0}' WHERE [クラスコード] = {1}", code_学生コード, code_クラスコード);
            result = sqlcom.ExecuteNonQuery();

            return result;
        }

        /// <summary>
        ///学生削除
        /// </summary>
        private int Deleteクラス学生(string code_学生コード,　string code_クラスコード, SqlCommand sqlcom)
        {
            int result;
            List<string> list_学生コード元 = new List<string>();
            list_学生コード元.AddRange(gv_studentsInfo.CurrentRow.Cells["クラスメンバー"].Value.ToString().Split(','));
            list_学生コード元.Remove(code_学生コード);
            list_学生コード元.Remove("-");

            sqlcom.CommandText = string.Format(@"UPDATE [dbo].[HL_JUKUKANRI_学生クラス] SET [クラスコード] = null WHERE [学生コード] = {0}", code_学生コード);
            result = sqlcom.ExecuteNonQuery();
            if (result != 1)
            {
                return 0;
            }

            sqlcom.CommandText = string.Format(@"UPDATE [dbo].[HL_JUKUKANRI_クラス履歴] SET [学生コード] = '{0}' WHERE [クラスコード] = {1}", string.Join(",", list_学生コード元), code_クラスコード);
            result = sqlcom.ExecuteNonQuery();
            return result;
        }

        /// <summary>
        ///セル編集開始
        /// </summary>
        private void gv_studentsInfo_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            beforValue = gv_studentsInfo.CurrentCell.Value.ToString();
            isEditing = true;
        }

        /// <summary>
        ///画面値変更
        /// </summary>
        private void gv_studentsInfo_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (isEditing && gv_studentsInfo.CurrentCell.EditedFormattedValue.ToString() != beforValue)
            {
                DataGridViewRow updateRow = this.gv_studentsInfo.CurrentRow;
                if (updateRow != null)
                {
                    string code_学生 = "";
                    string columnName = "";
                    object[] cellValue = null;

                    code_学生 = updateRow.Cells[0].Value.ToString();
                    columnName = gv_studentsInfo.Columns[gv_studentsInfo.CurrentCell.ColumnIndex].Name;
                    cellValue = new object[] { gv_studentsInfo.CurrentCell.EditedFormattedValue.ToString() };

                    //更新を行う
                    Update_GridViewRow(code_学生, gv_studentsInfo.Columns[gv_studentsInfo.CurrentCell.ColumnIndex].Name, cellValue);
                    //画面再表示
                    DisplayGridView();
                }
            }

        }

        /// <summary>
        ///セル編集終了
        /// </summary>
        private void gv_studentsInfo_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            isEditing = false;
        }
                                         
        /// <summary>
        /// 入力チェック
        /// </summary>
        public bool InputCheck(string code_学生, string columnName, string cellValue)
        {
            ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "";
            
            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库

            try
            {
                sqlcon.Open();
            }
            catch
            {
                ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "DBサーバーの接続に失敗しました";

                return false;
            }

            //存在チェック
            string sqlcmd = string.Format(@"Select * From HL_JUKUKANRI_学生マスタ Where 学生コード = '{0}'", code_学生);
            SqlCommand sqlcom = new SqlCommand(sqlcmd, sqlcon);

            try
            {
                SqlDataReader reader = sqlcom.ExecuteReader();

                if(!reader.Read())
                {
                    //更新：学生コードが存在しない場合エラー
                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = string.Format(@"エラー：学生コード[{0}]が登録されていません.", code_学生);
                    return false;
                }
            }
            catch (Exception ex)
            {
                ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "学生コードのチェック処理にエラーが発生しました。" + ex.Message;

                if (sqlcon != null)
                {
                    sqlcon.Close();
                }
                return false;
            }

            string errmsg = "";

            //変更したセル値有効チェック
            switch(columnName)
            {
                case "名前":
                    if (string.IsNullOrWhiteSpace(cellValue))
                    {
                        //必須
                        errmsg = "名前未入力!";
                    }
                    else if (cellValue.IndexOf(" ") > 0)
                    {
                        errmsg = "名前に許可されない文字'半角SPACE' が入りました。";
                    }
                    else if (cellValue.IndexOf("　") == -1)
                    {
                        errmsg = "姓と名の間に '全角SPACE' を挿入してください!";
                    }
                    else if (cellValue.IndexOf("　") != cellValue.LastIndexOf("　"))
                    {
                        errmsg = "名前に '全角SPACE' は二つ以上入力しないでください!";
                    }
                    else if (cellValue.IndexOf(",") > 0)
                    {
                        errmsg = "名前に許可されない文字「,」が入りました。";
                    }
                    break;
                case "カタカナ":
                    //カタカナ
                    if (cellValue.Equals(""))
                    {
                        errmsg = "カタカナ未入力!";
                    }
                    else if (cellValue.IndexOf(" ") > 0)
                    {
                        errmsg = "カタカナに許可されない文字'半角SPACE' が入りました。";
                    }
                    else if (cellValue.IndexOf("　") == -1)
                    {
                        errmsg = "カタカナの姓と名の間に '全角SPACE' を挿入してください!";
                    }
                    else if (cellValue.IndexOf("　") != cellValue.LastIndexOf("　"))
                    {
                        errmsg = "カタカナに '全角SPACE' は二つ以上入力しないでください!";
                    }
                    else if (cellValue.IndexOf(",") > 0)
                    {
                        errmsg = "カタカナに許可されない文字「,」が入りました。";
                    }
                    else if (!ComClass.IsFullKatakana(cellValue.Replace("　", "")))
                    {
                        errmsg = "カタカナは全角カタカナでご入力ください！";
                    }
                    break;
                case "ローマ字表記":
                    if (cellValue.Equals(""))
                    {
                        errmsg = "ローマ字表記未入力!";
                    }
                    break;
                case "生年月日":
                    if (string.IsNullOrWhiteSpace(cellValue))
                    {
                        errmsg = "生年月日が未入力です。";
                    }
                    else if (cellValue.IndexOf(",") > 0)
                    {
                        errmsg = "生年月日に許可されない文字「,」が入りました。";
                    }
                    else 
                    {
                        try
                        {
                            //日付形式チェック
                            DateTime.Parse(cellValue);
                        }
                        catch
                        {
                            errmsg = "生年月日に正しい日付の形式を入力してください。";
                        }
                    }
                    break;
                case "郵便番号":
                    //郵便番号
                    if (cellValue.Equals(""))
                    {
                        errmsg = "郵便番号未入力!";
                    }
                    else if (!Is郵便番号(cellValue))
                    {
                        errmsg = "郵便番号が間違っています!";
                    }
                    break;
                case "住所":
                    if (cellValue.Equals(""))
                    {
                        errmsg = "住所未入力!";
                    }
                    break;
                case "携帯":
                    //携帯
                    if (cellValue.Equals(""))
                    {
                        errmsg = "携帯未入力!";
                    }
                    else if (cellValue.IndexOf(",") > 0)
                    {
                        errmsg = "携帯に許可されない文字「,」が入りました。";
                    }
                    else if (!ISTEL(cellValue))
                    {
                        errmsg = "携帯のフォーマットが間違っています!";
                    }
                    break;
                case "メール":
                    if (cellValue.Equals(""))
                    {
                        errmsg = "メール未入力!";
                    }
                    else if (cellValue.IndexOf(",") > 0)
                    {
                        errmsg = "メールに許可されない文字「,」が入りました。";
                    }
                    else if (!IsValidEmail(cellValue))
                    {
                        errmsg = "メールのフォーマットが間違っています!";
                    }
                    break;
                case "国籍":
                    if (cellValue.Equals(""))
                    {
                        errmsg = "国籍未入力!";
                    }
                    break;
                case "学校名":
                    if (cellValue.Equals(""))
                    {
                        errmsg = "学校名未入力!";
                    }
                    break;
                case "入塾日":
                    try
                    {
                        //日付形式チェック
                        DateTime.Parse(cellValue.ToString());
                    }
                    catch
                    {
                        errmsg = "入塾日に正しい日付の形式を入力してください。";
                    }
                    break;
                case "離塾日":
                    try
                    {
                        //日付形式チェック
                        DateTime.Parse(cellValue.ToString());
                    }
                    catch
                    {
                        errmsg = "離塾日に正しい日付の形式を入力してください。";
                    }
                    break;
                 default:
                    break;
            }

            if(errmsg != "")
            {
                ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = errmsg + $"クラス[{code_学生}]が変更失敗しました。";
                ((Form1)(this.Tag)).reLoad = false;
                return false;
            }

            return true;
        }

        /// <summary>
        ///携帯番号チェック
        /// </summary>
        private bool ISTEL(string str_url)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_url, @"\A0\d{1,4}-\d{1,4}-\d{4}\z");
        }

        /// <summary>
        ///メールチェック
        /// </summary>
        private bool IsValidEmail(string strIn)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)" + @"|(([\w-]+\.)+))([a-zA-Z]{2,5}|[0-9]{1,3})(\]?)$");
        }

        /// <summary>
        ///郵便番号チェック
        /// </summary>
        private bool Is郵便番号(string strIn)
        {
            //20200707 add ou
            if (strIn.Substring(0, 1) == "〒")
            {
                strIn = strIn.Substring(1, strIn.Length - 1);
            }
            //return System.Text.RegularExpressions.Regex.IsMatch(strIn, @"\d{7}|\d{3}-\d{4}");
            return System.Text.RegularExpressions.Regex.IsMatch(strIn, "^[0-9]{3}[-]?[0-9]{4}$");
        }

        /// <summary>
        ///セルタイプをゲット
        /// </summary>
        private void gv_studentsInfo_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (e.Control.GetType().Equals(typeof(DataGridViewComboBoxEditingControl)))
            {
                //cellがComboBoxの場合
                DataGridViewComboBoxEditingControl editingControl = e.Control as DataGridViewComboBoxEditingControl;
                editingControl.SelectedIndexChanged += new EventHandler(ComboBox_SelectedIndexChanged);
            }
        }

        /// <summary>
        /// Comboxイベント处理
        /// </summary>
        public void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            isEditing = false;
            ComboBox combox = sender as ComboBox;
            combox.Leave += new EventHandler(combox_Leave);

            if (gv_studentsInfo.CurrentCell.FormattedValue.ToString() != gv_studentsInfo.CurrentCell.EditedFormattedValue.ToString())
            {
                try
                {
                    DataGridViewRow updateRow = this.gv_studentsInfo.CurrentRow;
                    string ID = updateRow.Cells[0].Value.ToString();
                    object[] cellValue = null;

                    //離塾原因の場合
                    if (combox.SelectedItem != null /*&& combox.DisplayMember == gv_studentsInfo.Columns[gv_studentsInfo.CurrentCell.ColumnIndex].Name*/)
                    {
                        DataGridViewComboBoxEditingControl cb = (DataGridViewComboBoxEditingControl)sender;
                        //DataRowView row = (DataRowView)combox.SelectedItem;
                        //cellValue = new object[] { cb.Row.ItemArray[0].ToString() };
                        cellValue = new object[] { cb.SelectedItem.ToString() };

                        Update_GridViewRow(ID, gv_studentsInfo.Columns[gv_studentsInfo.CurrentCell.ColumnIndex].Name, cellValue);
                        DisplayGridView();

                    }
                }
                catch (Exception ex)
                {
                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = ex.Message;
                    ((Form1)(this.Tag)).reLoad = true;
                }
            }
        }

        /// <summary>
        /// イベント終了、削除
        /// </summary>
        public void combox_Leave(object sender, EventArgs e)
        {
            ComboBox combox = sender as ComboBox;
            //イベント終了、削除
            combox.SelectedIndexChanged -= new EventHandler(ComboBox_SelectedIndexChanged);
        }

        /// <summary>
        ///クラス選択画面への移転と後処理
        /// </summary>
        private void クラス課程選択ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((Form1)(this.Tag)).m_クラス選択Handle != IntPtr.Zero)
            {
                BringWindowToTop(((Form1)(this.Tag)).m_クラス選択Handle);
                return;
            }

            クラス選択 m_NewForm_クラス選択 = new クラス選択();
            m_NewForm_クラス選択.Tag = ((Form1)(this.Tag));
            m_NewForm_クラス選択.研修フラグ = (this.gv_studentsInfo.CurrentRow.Cells["研修"].Value.ToString() == "研修").ToString();
            m_NewForm_クラス選択.code_クラスコード = this.gv_studentsInfo.CurrentRow.Cells["クラスコード"].Value.ToString();
            m_NewForm_クラス選択.code_学生 = this.gv_studentsInfo.CurrentRow.Cells["学生コード"].Value.ToString();
            m_NewForm_クラス選択.ShowDialog();
            ((Form1)(this.Tag)).m_クラス参照Handle = IntPtr.Zero;
            code_クラスコード = m_NewForm_クラス選択.code_クラスコード;
            list_学生 = m_NewForm_クラス選択.list_学生;

            if (list_学生.Count == 1 && list_学生[0] == "")
            {
                list_学生.Clear();
            }

            //クラス登録、変更
            string code_学生コード = this.gv_studentsInfo.CurrentRow.Cells["学生コード"].Value.ToString();
            string code_クラスコード_Before = this.gv_studentsInfo.CurrentRow.Cells["クラスコード"].Value.ToString();
            if (code_クラスコード_Before != code_クラスコード)
            {
                SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库

                int result = 0;
                try
                {
                    sqlcon.Open();
                }
                catch
                {
                    this.toolStripStatusLabel1.ForeColor = Color.Red;
                    this.toolStripStatusLabel1.Text = "DBサーバーの接続に失敗しました。";

                    return;
                }
                SqlTransaction transaction = sqlcon.BeginTransaction();
                try
                {
                    SqlCommand sqlcom = new SqlCommand();
                    sqlcom.Connection = sqlcon;
                    sqlcom.Transaction = transaction;

                    //クラス更新

                    //1.元クラスからメンバー削除
                    if (!string.IsNullOrWhiteSpace(code_クラスコード_Before) && code_クラスコード_Before != "-")
                    {
                        result = Deleteクラス学生(code_学生コード, code_クラスコード_Before, sqlcom);
                        if (result != 1)
                        {
                            throw new Exception();
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(code_クラスコード))
                    {
                        //2.新クラスにメンバー追加
                        result = Addクラス学生(code_学生コード, code_クラスコード, sqlcom);
                        if (result != 1)
                        {
                            throw new Exception();
                        }
                    }
                }
                catch (Exception ex)
                {
                    ((Form1)(this.Tag)).reLoad = false;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "学生情報の更新処理が失敗しました。" + ex.Message;
                    ((Form1)(this.Tag)).m_クラス選択Handle = IntPtr.Zero;
                }
                finally
                {
                    if (result == 1)
                    {
                        transaction.Commit();

                        ((Form1)(this.Tag)).reLoad = false;
                        ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Green;
                        ((Form1)(this.Tag)).toolStripStatusLabel2.Text = string.Format("学生コード[{0}]の情報が正常に更新されました。", code_学生コード);
                    }

                    if (sqlcon != null)
                    {
                        sqlcon.Close();
                    }
                }
                list_学生.Clear();
                //datagridview再表示
                DisplayGridView();
            }
        }

        private void 修了見込み証明書ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string 学生コード = this.gv_studentsInfo.CurrentRow.Cells["学生コード"].Value.ToString();
            string name = this.gv_studentsInfo.CurrentRow.Cells["名前"].Value.ToString();
            string code_クラス = this.gv_studentsInfo.CurrentRow.Cells["クラスコード"].Value.ToString();
            if (string.IsNullOrWhiteSpace(code_クラス) || code_クラス == "-")
            {
                ((Form1)(this.Tag)).reLoad = false;
                ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = string.Format("{0}はまだクラスに入っていないです。", name);

                return;
            }

            // 印刷機能生成
            AskPrintPice Formtemp = new AskPrintPice();
            Formtemp.ShowDialog();
            int pice = Formtemp.pice_back;

            if (pice > 0)
            {
                SqlConnection conn = new SqlConnection(connectionString); //连接数据库

                try
                {
                    conn.Open();
                }
                catch
                {
                    ((Form1)(this.Tag)).reLoad = false;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "サーバーがつなげない！インターネット接続をチェックしてください。";
                    return;
                }

                try
                {
                    //修了見込み証明書印刷
                    isPrintOK = true;
                    Print修了見込み証明書(conn, pice, Formtemp.print_back, 学生コード, name, "塾の修了見込み証明書");
                    if (isPrintOK)
                    {
                        //会社データ取得
                        m_書類データ.氏名 = name;
                        Get会社データ(conn);
                        if (isPrintOK)
                        {
                            //封筒印刷
                            Print送信用封筒(Formtemp.print_back);
                            System.Threading.Thread.Sleep(5000);

                            ((Form1)(this.Tag)).reLoad = false;
                            ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Green;
                            ((Form1)(this.Tag)).toolStripStatusLabel2.Text = string.Format("名前:[{0}]修了見込み証明書の印刷が完了しました。", name);
                        }
                    }
                }
                catch (Exception ex)
                {
                    toolStripStatusLabel1.ForeColor = Color.Red;
                    toolStripStatusLabel1.Text = ex.Message;

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            } 
        }

        private void Print修了見込み証明書(SqlConnection conn, int pice, string print_back, object 学生コード, string name, string fileTmpName)
        {
            m_書類データ.郵便番号 = this.gv_studentsInfo.CurrentRow.Cells["郵便番号"].Value.ToString().Replace("〒", "");
            m_書類データ.住所 = this.gv_studentsInfo.CurrentRow.Cells["住所"].Value.ToString();
            string 課程 = this.gv_studentsInfo.CurrentRow.Cells["クラス課程"].Value.ToString();
            //学習期間取得
            string str_sqlcmd = string.Format(@"Select c.開始日, c.終了日 From  HL_JUKUKANRI_学生クラス b
                                                Left Join HL_JUKUKANRI_クラス履歴 c On c.クラスコード = b.クラスコード
                                                Where b.学生コード = '{0}'", 学生コード);

            SqlCommand com = new SqlCommand(str_sqlcmd, conn);
            SqlDataReader reader = com.ExecuteReader();

            string str開始日 = "";
            string str卒業日 = "";

            if (reader.Read())
            {
                str開始日 = reader["開始日"].ToString();
                str卒業日 = reader["終了日"].ToString();
                reader.Close();

                //0:空白(NULL)；1：卒業；2：キャンセル；3:除籍
                string str離塾原因 = this.gv_studentsInfo.CurrentRow.Cells["離塾原因"].Value.ToString().Replace("-", "");
                if (string.IsNullOrWhiteSpace(str離塾原因) || str離塾原因 == "卒業")
                {
                    if (fileTmpName == "塾の修了証明書" && DateTime.Parse(str卒業日) > DateTime.Now)
                    {
                        isPrintOK = false;

                        ((Form1)(this.Tag)).reLoad = false;
                        ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                        ((Form1)(this.Tag)).toolStripStatusLabel2.Text = string.Format("学生[{0}]のクラスが[{1}]までに終了するため、今は「修了証明書」を発行できません。", name, str卒業日);
                        return;
                    }
                }
            }
            else
            {
                reader.Close();
                isPrintOK = false;

                ((Form1)(this.Tag)).reLoad = false;
                ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = string.Format("学生[{0}]のクラス情報がありました。", name);
                return;
            }

            str_sqlcmd =string.Format( @"Select テンプレートファイル From HL_JUKUKANRI_塾管理書類テンプレート Where テンプレートファイル名 = '{0}'", fileTmpName);

            com = new SqlCommand(str_sqlcmd, conn);
            reader = com.ExecuteReader();

            if (reader.Read())
            {
                fileTmpName = fileTmpName.Replace("塾の", "");
                string tmpDir = @"C:\Windows\Temp\";
                if (!tmpDir.Trim().Equals(""))
                {
                    ((Form1)(this.Tag)).reLoad = false;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Black;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = string.Format("名前:[{0}]の{1}証明書を印刷中...", name, fileTmpName);

                    Word.Application oWord = new Word.Application();
                    Word._Document oDoc = null;

                    try
                    {
                        string strファイル名 = name +  "_" + fileTmpName + ".doc";
                        string strFilePath = tmpDir + strファイル名;
                        File.WriteAllBytes(strFilePath, (byte[])reader["テンプレートファイル"]);

                        object oMissing = System.Reflection.Missing.Value;

                        oWord.Visible = false;
                        oWord.DisplayAlerts = Word.WdAlertLevel.wdAlertsNone;
                        oWord.ActivePrinter = print_back;

                        string AAA = this.gv_studentsInfo.CurrentRow.Cells["カタカナ"].Value.ToString();
                        string BBB = name;
                        string CCC = DateTime.Parse(str開始日).Year.ToString();
                        string DD = DateTime.Parse(str開始日).Month.ToString().PadLeft(2, '0');
                        string EE = DateTime.Parse(str開始日).Day.ToString().PadLeft(2, '0');

                        //卒業日
                        string FFF = DateTime.Parse(str卒業日).Year.ToString();
                        string GG = DateTime.Parse(str卒業日).Month.ToString().PadLeft(2, '0');
                        string HH = DateTime.Parse(str卒業日).Day.ToString().PadLeft(2, '0');

                        //発行日
                        string III = DateTime.Now.Year.ToString();
                        string JJ = DateTime.Now.Month.ToString().PadLeft(2, '0');
                        string KK = DateTime.Now.Day.ToString().PadLeft(2, '0');


                        object inFileName = strFilePath;
                        oDoc = oWord.Documents.Add(ref inFileName, ref oMissing, ref oMissing, ref oMissing);

                        ComClass.WordReplace("AAA", AAA, true, oDoc);
                        ComClass.WordReplace("BBB", BBB, true, oDoc);
                        ComClass.WordReplace("CCCC", CCC, true, oDoc);
                        ComClass.WordReplace("DD", DD, true, oDoc);
                        ComClass.WordReplace("EE", EE, true, oDoc);
                        ComClass.WordReplace("FFFF", FFF, true, oDoc);
                        ComClass.WordReplace("GG", GG, true, oDoc);
                        ComClass.WordReplace("HH", GG, true, oDoc);
                        ComClass.WordReplace("III", III, true, oDoc);
                        ComClass.WordReplace("JJ", JJ, true, oDoc);
                        ComClass.WordReplace("KK", KK, true, oDoc);

                        string i = "１";
                        string re = "";
                        switch (課程)
                        {
                            case "Java":
                                re = "①．Java";
                                i = "１．Java ";
                                break;
                            case ".Net":
                                re = "②．C#";
                                i = "２．Ｃ＃";
                                課程 = "Cshap";
                                break;
                            case "php":
                                re = "③．PHP";
                                i = "３．PＨＰ";
                                break;
                            case "Android":
                                re = "④．Android";
                                i = "４．Android";
                                break;
                            case "iOS":
                                re = "⑤．IOS";
                                i = "５．IOS";
                                break;
                        }

                        ComClass.WordReplace(i, re, true, oDoc);
                        object bookMark = 課程;
                        if (oDoc.Bookmarks.Exists(Convert.ToString(bookMark)) == true)
                        {
                            //查找书签
                            oDoc.Bookmarks.get_Item(ref bookMark).Select();
                            oWord.Selection.Font.Color = Word.WdColor.wdColorRed;
                        }

                        oDoc.PrintOut(Type.Missing, Type.Missing, Word.WdPrintOutRange.wdPrintAllDocument, Type.Missing,
                        Type.Missing, Type.Missing, Word.WdPrintOutItem.wdPrintDocumentContent, pice);
                    }
                    catch (Exception error)
                    {
                        isPrintOK = false;
                        throw new Exception(error.Message);
                    }
                    finally
                    {
                        object paramMissing = Type.Missing;
                        if (oDoc != null)
                        {
                            oDoc.Close(false, paramMissing, ref paramMissing);
                            oDoc = null;
                        }
                        if (oWord != null)
                        {
                            oWord.Quit(ref paramMissing, ref paramMissing, ref paramMissing);
                            oWord = null;
                        }
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                    }
                }
            }
            else
            {
                isPrintOK = false;
                ((Form1)(this.Tag)).reLoad = false;
                ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = string.Format("[{0}]のテンプレートがありません。", fileTmpName);
            }
            reader.Close();

        }

        private void 修了証明書ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string 学生コード = this.gv_studentsInfo.CurrentRow.Cells["学生コード"].Value.ToString();
            string name = this.gv_studentsInfo.CurrentRow.Cells["名前"].Value.ToString();
            string code_クラス = this.gv_studentsInfo.CurrentRow.Cells["クラスコード"].Value.ToString();
            if (string.IsNullOrWhiteSpace(code_クラス) || code_クラス == "-")
            {
                ((Form1)(this.Tag)).reLoad = false;
                ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = string.Format("{0}はまだクラスに入っていないです。", name);
                return;
            }

            // 印刷機能生成
            AskPrintPice Formtemp = new AskPrintPice();
            Formtemp.ShowDialog();
            int pice = Formtemp.pice_back;

            if (pice > 0)
            {
                SqlConnection conn = new SqlConnection(connectionString); //连接数据库

                try
                {
                    conn.Open();
                }
                catch
                {
                    ((Form1)(this.Tag)).reLoad = false;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "サーバーがつなげない！インターネット接続をチェックしてください。";
                    return;
                }

                try
                {
                    //塾の修了証明書印刷
                    isPrintOK = true;
                    Print修了見込み証明書(conn, pice, Formtemp.print_back, 学生コード, name, "塾の修了証明書");
                    if (isPrintOK)
                    {
                        //会社データ取得
                        m_書類データ.氏名 = name;
                        Get会社データ(conn);
                        if (isPrintOK)
                        {
                            //封筒印刷
                            Print送信用封筒(Formtemp.print_back);
                            System.Threading.Thread.Sleep(5000);
                            ((Form1)(this.Tag)).reLoad = false;
                            ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Green;
                            ((Form1)(this.Tag)).toolStripStatusLabel2.Text = string.Format("名前:[{0}]修了証明書の印刷が完了しました。", name);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ((Form1)(this.Tag)).reLoad = false;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = ex.Message;

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 会社データを取得
        /// <summary>
        private void Get会社データ(SqlConnection conn)
        {
            string str_sqlcmd = @"Select * From [dbo].[HL_ALL_グループ会社] Where 会社名 = 'HL株式会社'";

            SqlCommand com = new SqlCommand(str_sqlcmd, conn);
            SqlDataReader reader = com.ExecuteReader();

            if (reader.Read())
            {
                m_書類データ.勤務先名称 = reader["会社名"].ToString();
                m_書類データ.勤務先郵便番号 = reader["郵便番号"].ToString();
                m_書類データ.勤務先住所 = reader["住所"].ToString();
                m_書類データ.勤務先電話番号 = reader["電話番号"].ToString();
                m_書類データ.勤務先ウェッブサイト = reader["ウェッブサイト"].ToString();
                m_書類データ.勤務先商標 = ComClass.BufferToImage(reader["商標ファイル"] as byte[]);
            }
            else
            {
                isPrintOK = false;
                ((Form1)(this.Tag)).reLoad = false;
                ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "会社の情報が存在していません。";
            }
            reader.Close();
        }

        /// <summary>
        /// 封筒を印刷
        /// <summary>
        private void Print送信用封筒(string プリンター名)
        {
            if (MessageBox.Show("送信用封筒を印刷しますか？\n「はい」を選択する前に、「角形２号」封筒をプリンターの手差しのところに用意してください。", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ((Form1)(this.Tag)).reLoad = false;
                ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Black;
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "送信用封筒を印刷中...";

                PrintDocument PrintDoc = new PrintDocument();
                PrintDoc.DocumentName = "送信用封筒封筒";

                //設定Printer
                PrintDoc.PrinterSettings.PrinterName = プリンター名;

                //設定Printer用紙
                PaperSize psize = PrintDoc.PrinterSettings.PaperSizes.Cast<PaperSize>().ToList().Find(item => item.Kind == PaperKind.JapaneseEnvelopeKakuNumber2);
                PrintDoc.DefaultPageSettings.PaperSize = psize != null ? psize : new PaperSize("封筒角形2号(240x332mm)", 945, 1307);

                //設定Printer用紙來源
                PaperSource psource = PrintDoc.PrinterSettings.PaperSources.Cast<PaperSource>().ToList().Find(item => item.Kind == PaperSourceKind.Manual);
                PrintDoc.DefaultPageSettings.PaperSource = psource != null ? psource : PrintDoc.DefaultPageSettings.PaperSource;

                //設定Print內容
                PrintDoc.PrintPage += new PrintPageEventHandler(delegate (object sender, PrintPageEventArgs e)
                {
                    string 社員名 = m_書類データ.氏名;
                    string 社員郵便番号 = m_書類データ.郵便番号;
                    string 社員住所 = m_書類データ.住所;
                    string 会社名 = m_書類データ.勤務先名称;
                    string 会社郵便番号 = m_書類データ.勤務先郵便番号;
                    string 会社住所 = m_書類データ.勤務先住所;
                    string 会社電話番号 = m_書類データ.勤務先電話番号;
                    string 会社ウェッブサイト = m_書類データ.勤務先ウェッブサイト;
                    System.Drawing.Image 会社商標 = m_書類データ.勤務先商標.Clone() as System.Drawing.Image;


                    社員名 += "　様";
                    char[] 社員郵便番号array = 社員郵便番号.ToCharArray();
                    社員郵便番号 = string.Empty;
                    for (int i = 0; i < 社員郵便番号array.Count(); ++i)
                    {
                        社員郵便番号 += 社員郵便番号array[i] + " ";
                        if (i == 2) 社員郵便番号 += " ";
                    }

                    社員住所 = 社員住所.Replace("\n", "");
                    社員住所 = 社員住所.Replace("　", " ");
                    while (社員住所.IndexOf("  ") >= 0)
                    {
                        社員住所 = 社員住所.Replace("  ", " ");
                    }

                    会社郵便番号 = "〒" + 会社郵便番号.Insert(3, "-");
                    会社電話番号 = "TEL:" + 会社電話番号;
                    会社商標 = ComClass.GetThumbnail((Bitmap)会社商標, 100, 100);

                    FontFamily fontFamily = new FontFamily("ＭＳ ゴシック");
                    e.Graphics.DrawString(社員名, new System.Drawing.Font(fontFamily, 28), System.Drawing.Brushes.Black, 430, 380, new StringFormat(StringFormatFlags.DirectionVertical));
                    if (社員住所.Length > 30)
                    {
                        e.Graphics.DrawString(社員住所.Substring(0, 30), new System.Drawing.Font(fontFamily, 18), System.Drawing.Brushes.Black, 760, 220, new StringFormat(StringFormatFlags.DirectionVertical));
                        e.Graphics.DrawString(社員住所.Substring(30, 社員住所.Length - 30), new System.Drawing.Font(fontFamily, 18), System.Drawing.Brushes.Black, 700, 220, new StringFormat(StringFormatFlags.DirectionVertical));
                    }
                    else
                    {
                        e.Graphics.DrawString(社員住所, new System.Drawing.Font(fontFamily, 18), System.Drawing.Brushes.Black, 720, 220, new StringFormat(StringFormatFlags.DirectionVertical));
                    }
                    e.Graphics.DrawString(社員郵便番号, new System.Drawing.Font(fontFamily, 19), System.Drawing.Brushes.Black, 620, 143);

                    e.Graphics.DrawImage(会社商標, 80, 940);
                    e.Graphics.DrawString(会社名, new System.Drawing.Font(fontFamily, 18), System.Drawing.Brushes.Blue, 200, 980);
                    e.Graphics.DrawString(会社郵便番号, new System.Drawing.Font(fontFamily, 12), System.Drawing.Brushes.Black, 80, 1060);
                    e.Graphics.DrawString(会社住所, new System.Drawing.Font(fontFamily, 12), System.Drawing.Brushes.Black, 80, 1085);
                    e.Graphics.DrawString(会社電話番号, new System.Drawing.Font(fontFamily, 12), System.Drawing.Brushes.Black, 80, 1110);
                    e.Graphics.DrawString(会社ウェッブサイト, new System.Drawing.Font(fontFamily, 12), System.Drawing.Brushes.Black, 80, 1135);
                });

                PrintDoc.Print();
            }
        }

        private void cmb_学生_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayGridView();
        }
    }
}
