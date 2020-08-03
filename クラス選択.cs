using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace HL_塾管理
{
    public partial class クラス選択 : DockContent
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        //データベース接続情報
        private string connectionString = ComClass.connectionString;

        //画面項目
        public string code_クラスコード = "";
        public List<string> list_学生 = new List<string>();
        public string code_出勤機 = "";
        public string code_学生 = "";
        public string name_クラス = "";
        public string 研修フラグ = "False";

        //新旧フラグ
        public bool isFirst = true;

        public クラス選択()
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
        /// 画面表示
        /// </summary>
        private void クラス選択_Load(object sender, EventArgs e)
        {
            //一覧表示
            DisplayGridView();
        }

        /// <summary>
        ///dtp値変更
        /// </summary>
        private void dtp_date_ValueChanged(object sender, EventArgs e)
        {
            //datagridview再表示
            DisplayGridView();
        }

        /// <summary>
        /// 検索処理
        /// </summary>
        private void DisplayGridView()
        {

            this.toolStripStatusLabel1.Text = "";
            //画面値を取得
            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库

            try
            {
                sqlcon.Open();
            }
            catch
            {
                this.toolStripStatusLabel1.ForeColor = Color.Black;
                this.toolStripStatusLabel1.Text = "DBサーバーの接続に失敗しました.";
                return;
            }

            //検索開始
            string sqlcmd = string.Format(@"Select a.クラスコード, a.クラス名 ,a.教室コード, c.備考,b.名前 as 教師名,　a.課程, a.開始日, a.終了日, a.学生コード, 
                                                   c.出勤機コード From HL_JUKUKANRI_クラス履歴 a Left Join HL_JUKUKANRI_教師情報 b on a.教師コード = b.教師コード 
                                                   Left Join HL_JUKUKANRI_教室マスタ c on a.教室コード = c.教室コード 
                                                   Where a.有効 = 1 and (a.終了日 > Getdate() or a.終了日 is null) And c.備考 is not null And a.研修フラグ = '{0}'", 研修フラグ);

            //ソート
            sqlcmd += " Order by a.開始日, a.終了日";

            SqlCommand com = new SqlCommand(sqlcmd, sqlcon);
            SqlDataReader reader = com.ExecuteReader();

            int Index = 0;
            gridView_クラスInfo.Rows.Clear();

            try
            {
                //一覧情報設定
                while (reader.Read())
                {
                    this.gridView_クラスInfo.Rows.Add();
                    DataGridViewButtonCell buttonCell = (DataGridViewButtonCell)this.gridView_クラスInfo.Rows[Index].Cells["加入"];
                    if (code_クラスコード == reader["クラスコード"].ToString())
                    {
                        buttonCell.Value = "脱退";
                        buttonCell.ReadOnly = true;
                        this.gridView_クラスInfo.Rows[Index].DefaultCellStyle.BackColor = Color.Yellow;
                    }
                    else
                    {
                        buttonCell.Value = "加入";
                        buttonCell.ReadOnly = false;
                    }
                    //データ表示
                    this.gridView_クラスInfo.Rows[Index].Cells["クラスコード"].Value = reader["クラスコード"].ToString();
                    this.gridView_クラスInfo.Rows[Index].Cells["クラス名"].Value = string.IsNullOrWhiteSpace(reader["クラス名"].ToString()) ? "-" : reader["クラス名"].ToString();
                    this.gridView_クラスInfo.Rows[Index].Cells["教室コード"].Value = reader["教室コード"].ToString();
                    this.gridView_クラスInfo.Rows[Index].Cells["教室"].Value = reader["備考"].ToString();
                    this.gridView_クラスInfo.Rows[Index].Cells["教師名"].Value = reader["教師名"].ToString();
                    this.gridView_クラスInfo.Rows[Index].Cells["課程"].Value = reader["課程"].ToString();
                    this.gridView_クラスInfo.Rows[Index].Cells["開始日"].Value = reader["開始日"].ToString();
                    this.gridView_クラスInfo.Rows[Index].Cells["終了日"].Value = reader["終了日"].ToString();
                    this.gridView_クラスInfo.Rows[Index].Cells["学生コード"].Value = reader["学生コード"].ToString();
                    this.gridView_クラスInfo.Rows[Index].Cells["出勤機コード"].Value = reader["出勤機コード"].ToString();

                    foreach (DataGridViewCell gvCell in gridView_クラスInfo.Rows[Index].Cells)
                    {
                        if (gvCell.Value == null || gvCell.Value.ToString() == "-")
                        {
                            gridView_クラスInfo.Rows[Index].Cells[gvCell.ColumnIndex].Style.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
                        }
                    }

                    Index++;
                }
                isFirst = false;
            }
            catch (Exception ex)
            {
                this.toolStripStatusLabel1.ForeColor = Color.Black;
                this.toolStripStatusLabel1.Text = "検索処理に失敗しました." + ex.Message;
            }
            finally
            {
                if (sqlcon != null)
                {
                    sqlcon.Close();
                    reader.Close();
                }
            }

            if (this.toolStripStatusLabel1.Text != "")
            {
                return;
            }

            //件数表示
            this.toolStripStatusLabel1.ForeColor = Color.Black;
            this.toolStripStatusLabel1.Text = string.Format("{0}件", Index);
        }

        /// <summary>
        /// 画面閉じ
        /// </summary>
        private void クラス選択_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form1)(this.Tag)).m_クラス選択Handle = IntPtr.Zero;
        }

        /// <summary>
        /// データがおかしい
        /// </summary>
        private void gridView_クラスInfo_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (gridView_クラスInfo.Rows[e.RowIndex].IsNewRow)
                return;
        }

        /// <summary>
        /// datagridviewボタンクリック
        /// </summary>
        private void gridView_クラスInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                //加入の場合
                if (gridView_クラスInfo.CurrentRow.Cells["加入"].Value.ToString() == "加入")
                {
                    if (!TimeCheck())
                    {
                        toolStripStatusLabel2.ForeColor = Color.Red;
                        toolStripStatusLabel2.Text = "開始したクラスに加入できません。他のクラスを選択してください。";
                        return;
                    }

                    //画面値取得
                    code_クラスコード = gridView_クラスInfo.CurrentRow.Cells["クラスコード"].Value.ToString();
                    string code_学生List = gridView_クラスInfo.CurrentRow.Cells["学生コード"].Value.ToString();
                    list_学生.AddRange(code_学生List.Split(','));
                    code_出勤機 = gridView_クラスInfo.CurrentRow.Cells["出勤機コード"].Value.ToString().Trim();
                    name_クラス = gridView_クラスInfo.CurrentRow.Cells["クラス名"].Value.ToString();
                }
                else
                {
                    code_クラスコード = "";
                }

                this.Close();
            }
        }

        private bool TimeCheck()
        {
            DateTime startdate = Convert.ToDateTime(gridView_クラスInfo.CurrentRow.Cells["開始日"].Value);
            DateTime enddate = Convert.ToDateTime(gridView_クラスInfo.CurrentRow.Cells["終了日"].Value);
            if (DateTime.Compare(DateTime.Now, startdate) >= 0 &&
                DateTime.Compare(DateTime.Now, enddate) <= 0)
            {

                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
