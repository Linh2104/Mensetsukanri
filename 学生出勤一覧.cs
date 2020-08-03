using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace HL_塾管理
{
    public partial class 学生出勤一覧 : DockContent
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int BringWindowToTop(IntPtr hWnd);
        private AutoSizeFormClass asc = new AutoSizeFormClass();

        //データベース接続情報
        private string connectionString = ComClass.connectionString;

        //セル編集フラグ
        private bool isEditing = false;

        //ログイン職務
        public string user = "";

        //datagridview行
        public DataGridViewRow row = null;


        public 学生出勤一覧()
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

        private void 学生出勤一覧_Load(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);

            //datagridview表示
            DisplayGridView();
        }

        /// <summary>
        /// 画面閉じ
        /// </summary>
        private void 学生出勤一覧_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form1)(this.Tag)).m_学生出勤一覧Handle = IntPtr.Zero;
        }

        /// <summary>
        /// 検索dtp値変更
        /// </summary>
        private void dtp_date_ValueChanged(object sender, EventArgs e)
        {
            //datagridview再表示
            DisplayGridView();
        }

        /// <summary>
        /// 
        /// </summary>
        private void txt_検索_TextChanged(object sender, EventArgs e)
        {
            //datagridview再表示
            DisplayGridView();
        }

        /// <summary>
        /// datagridviewデータ取得
        /// </summary>
        private void DisplayGridView()
        {
            if (((Form1)(this.Tag)).reLoad)
            {
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "";
            }
            rowMergeView1.Rows.Clear();
            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库
           
            try
            {
                sqlcon.Open();
            }
            catch
            {
                ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "DBサーバーの接続に失敗しました.";
                ((Form1)(this.Tag)).reLoad = false;
                return;
            }

            //画面値ゲット、検索開始
            string str_sqlcmd = "";
            string date_日付 = dtp_date.Value.ToString("yyyyMM");
            string search = txt_検索.Text;
            user = ((Form1)(this.Tag)).m_ユーザ登録.m_ログイン_職務;
            string 教師コード = ((Form1)(this.Tag)).m_ユーザ登録.m_ログイン_教師コード;

            str_sqlcmd = @"select a.学生コード,b.名前 as 学生名,c.出勤機コード,e.場所,c.登録コード,f.クラスコード, h.名前 as 教師名 ,
                                (Case When c.出退勤フラグ in (0,2,4) then '出勤'　Else '退勤' End) as 出退勤フラグ ,
                                c.出退勤時間 " +
                                "from HL_JINJI_出勤機_登録ユーザマスタ as a " +
                                "inner join HL_JUKUKANRI_学生マスタ as b  " +
                                "on a.学生コード=b.学生コード " +
                                "left join HL_JINJI_出勤機マスタ as e " +
                                "on a.出勤機コード=e.出勤機コード " +
                                "left join HL_JINJI_出勤機元記録 as c " +
                                "on a.登録コード=c.登録コード " +
                                " left join HL_JUKUKANRI_学生クラス f on f.学生コード = b.学生コード  " +
                                " left join HL_JUKUKANRI_クラス履歴 g on g.クラスコード = f.クラスコード   " +
                                " left join HL_JUKUKANRI_教師情報 h on h.教師コード = g.教師コード      " + 
                                " left join HL_JINJI_社員在職状態 i on i.社員コード = a.社員コード " +
                                "where (a.学生コード is not null and a.社員コード = null or (a.学生コード is not null and (i.在職状態 != '在職' or (i.在職状態 = '停職' and i.社員種別 = '研修'))) )" +
                                "and CONVERT(VARCHAR(6), c.出退勤時間, 112) = '" + date_日付 + "' ";
            if (user=="一般ユーザ")
            {
                str_sqlcmd += " and g.教師コード='" + 教師コード + "' ";
                rowMergeView1.Columns["教師名"].Visible = false;
            }
            str_sqlcmd += " order by c.出退勤時間";

            SqlCommand com = new SqlCommand(str_sqlcmd, sqlcon);
            SqlDataReader reader = com.ExecuteReader();
            int Index = 0;
            while (reader.Read())
            {
                if (
                    (reader["学生コード"].ToString().IndexOf(search) < 0)
                    &&
                    (reader["学生名"].ToString().IndexOf(search) < 0)
                    &&
                    (reader["場所"].ToString().IndexOf(search) < 0)
                    &&
                    (reader["登録コード"].ToString().IndexOf(search) < 0)
                    &&
                    (reader["クラスコード"].ToString().IndexOf(search) < 0)
                    &&
                    (reader["教師名"].ToString().IndexOf(search) < 0)
                    &&
                    (reader["出退勤フラグ"].ToString().IndexOf(search) < 0)
                    &&
                    (reader["出退勤時間"].ToString().IndexOf(search) < 0)
                    )
                {
                    continue;
                }

                this.rowMergeView1.Rows.Add();
                rowMergeView1.Rows[Index].Cells["学生コード"].Value = reader["学生コード"].ToString();
                rowMergeView1.Rows[Index].Cells["学生名"].Value = reader["学生名"].ToString();
                rowMergeView1.Rows[Index].Cells["出勤機コード"].Value = reader["出勤機コード"].ToString();
                rowMergeView1.Rows[Index].Cells["出勤機場所"].Value = reader["場所"].ToString();
                rowMergeView1.Rows[Index].Cells["登録コード"].Value = reader["登録コード"].ToString();
                rowMergeView1.Rows[Index].Cells["クラスコード"].Value = reader["クラスコード"].ToString();
                rowMergeView1.Rows[Index].Cells["教師名"].Value = reader["教師名"].ToString();
                rowMergeView1.Rows[Index].Cells["出退勤フラグ"].Value = reader["出退勤フラグ"].ToString();
                rowMergeView1.Rows[Index].Cells["出退勤時間"].Value = DateTime.Parse(reader["出退勤時間"].ToString()).ToString("yyyy/MM/dd HH:mm:ss");
                rowMergeView1.Rows[Index].Cells["NewRowフラグ"].Value = false;
                rowMergeView1.Rows[Index].Cells["元日付"].Value = reader["出退勤時間"].ToString();

                //空白セルに"‐"を入れる
                foreach (DataGridViewCell gvCell in rowMergeView1.Rows[Index].Cells)
                {
                    if (string.IsNullOrWhiteSpace(gvCell.Value.ToString()) || gvCell.Value.ToString() == "-")
                    {
                        gvCell.Value = "-";
                        rowMergeView1.Rows[Index].Cells[gvCell.ColumnIndex].Style.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
                    }

                }
                Index++;
            }
            if (sqlcon != null)

            {
                sqlcon.Close();
            }

            //件数表示
            toolStripStatusLabel1.Text = string.Format("{0}件", Index);
            ((Form1)(this.Tag)).reLoad = true;
        }

        /// <summary>
        ///  右クリックで一行を選択
        /// </summary>
        private void rowMergeView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    if ((this.rowMergeView1.Rows[e.RowIndex].Selected == false))
                    {
                        this.rowMergeView1.ClearSelection();
                        this.rowMergeView1.Rows[e.RowIndex].Selected = true;
                    }
                    //'只选中一行时设置活动单元格
                    if ((this.rowMergeView1.SelectedRows.Count == 1))
                    {
                        this.rowMergeView1.CurrentCell = this.rowMergeView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    }
                    contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                }

            }
        }

        /// <summary>
        /// メニューを開く
        /// </summary>
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            System.Drawing.Point startPosition = Cursor.Position;

            System.Drawing.Point point = this.rowMergeView1.PointToClient(startPosition);
            DataGridView.HitTestInfo hitinfo;
            hitinfo = this.rowMergeView1.HitTest(point.X, point.Y);

            this.rowMergeView1.ClearSelection();
            if (hitinfo.RowIndex >= 0)
            {
                this.rowMergeView1.Rows[hitinfo.RowIndex].Selected = true;
            }
            else
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 画面値変更、更新処理
        /// </summary>
        private void rowMergeView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (isEditing && this.rowMergeView1.CurrentCell != null)
            {
                this.rowMergeView1.CurrentCell.Selected = true;
                //画面値取得
                string code_出勤機コード = this.rowMergeView1.CurrentRow.Cells["出勤機コード"].Value.ToString();
                string code_登録コード = this.rowMergeView1.CurrentRow.Cells["登録コード"].Value.ToString();            
                string 出退勤flag = "";
                string date_元日時 = this.rowMergeView1.CurrentRow.Cells["元日付"].Value.ToString();
                string 教師名 = this.rowMergeView1.CurrentRow.Cells["教師名"].Value.ToString();
                
                switch (this.rowMergeView1.CurrentRow.Cells["出退勤フラグ"].Value.ToString())
                {
                    case "出勤":
                        出退勤flag = "0";
                        break;
                    case "退勤":
                        出退勤flag = "1";
                        break;
                    default:
                        出退勤flag = "";
                        break;
                }
                string date_出退勤時間 = "";              
                try
                {
                    //日付形式チェック
                    date_出退勤時間 = this.rowMergeView1.CurrentRow.Cells["出退勤時間"].Value.ToString();
                    DateTime dateTime = DateTime.ParseExact(date_出退勤時間, "yyyy/MM/dd HH:mm:ss",null);
                }
                catch
                {
                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "正しい時間を入力してください。";
                    ((Form1)(this.Tag)).reLoad = false;
                    int index_col = rowMergeView1.CurrentCell.ColumnIndex;
                    int index_row = rowMergeView1.CurrentCell.RowIndex;

                    //datagridview再表示
                    DisplayGridView();

                    rowMergeView1.Rows[index_row].Cells[index_col].Selected = true;
                    return;
                }
                //新旧フラグ
                bool isNew = false;
                if (this.rowMergeView1.CurrentRow.Cells["NewRowフラグ"].Value != null)
                {
                    isNew = this.rowMergeView1.CurrentRow.Cells["NewRowフラグ"].Value.ToString() == "True" ? true : false;
                }
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
                    return;
                }
                SqlCommand sqlcom = new SqlCommand();
                sqlcom.Connection = sqlcon;

                try
                {
                    if (isNew)
                    {
                        //重複チェック
                        string sql = string.Format(@"Select * from HL_JINJI_出勤機元記録 Where 出勤機コード = '{0}' and 登録コード = '{1}' and 出退勤フラグ = {2} and 出退勤時間 = '{3}'", code_出勤機コード, code_登録コード, 出退勤flag, date_出退勤時間);
                        SqlDataAdapter sqlDa = new SqlDataAdapter(sql, sqlcon);
                        DataTable dt = new DataTable();
                        sqlDa.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                            ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "重複データが追加できません。";
                            ((Form1)(this.Tag)).reLoad = false;
                      
                        }
                        else
                        {
                            //出勤機元記録に登録
                            sqlcom.CommandText = string.Format(@"Insert Into HL_JINJI_出勤機元記録 Values('{0}', '{1}', {2}, '{3}')", code_出勤機コード, code_登録コード, 出退勤flag, date_出退勤時間);

                            result = sqlcom.ExecuteNonQuery();

                            if (result == 1)
                            {
                                ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Green;
                                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "出勤機元記録が正常に追加されました.";
                                ((Form1)(this.Tag)).reLoad = false;                               
                                sqlcon.Close();
                            }
                        }
                    }
                    else
                    {
                        //出勤機元記録から更新行う
                        string sql_update = @"Update HL_JINJI_出勤機元記録 Set "
                                          + " 出勤機コード = '{0}', "
                                          + " 出退勤フラグ = {1}, "
                                          + " 出退勤時間 = '{2}' "
                                          + "Where 登録コード = '{3}' and 出退勤時間 = '{4}'";

                        sqlcom.CommandText = string.Format(sql_update, code_出勤機コード, 出退勤flag, date_出退勤時間, code_登録コード, date_元日時);

                        result = sqlcom.ExecuteNonQuery();

                        if (result == 1)
                        {
                            ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Green;
                            ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "出勤機元記録が正常に更新されました.";
                            ((Form1)(this.Tag)).reLoad = false;
                          
                            sqlcon.Close();
                        }
                    }

                }
                catch
                {
                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "出勤機元記録の更新処理が失敗しました.";
                    ((Form1)(this.Tag)).reLoad = false;
                }
                finally
                {
                    if (sqlcon != null)
                    {
                        sqlcon.Close();
                    }
                }

                //datagridview再表示
                DisplayGridView();
            }
        }

        /// <summary>
        /// セル編集開始
        /// </summary>
        private void rowMergeView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            isEditing = true;
        }

        /// <summary>
        /// セル編集終了
        /// </summary>
        private void rowMergeView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            isEditing = false;
        }

        /// <summary>
        /// 出勤追加
        /// </summary>
        private void 追加toolStripMenuItem_Click(object sender, EventArgs e)
        {
            //一番下に一行追加、値の設定
            int currentIndex = this.rowMergeView1.CurrentRow.Index;
            int rowIndex = this.rowMergeView1.Rows.Add();

            this.rowMergeView1.CurrentRow.Selected = false;
            this.rowMergeView1.Rows[rowIndex].Cells["学生コード"].Value = this.rowMergeView1.Rows[currentIndex].Cells["学生コード"].Value;
            this.rowMergeView1.Rows[rowIndex].Cells["学生名"].Value = this.rowMergeView1.Rows[currentIndex].Cells["学生名"].Value;
            this.rowMergeView1.Rows[rowIndex].Cells["出勤機コード"].Value = this.rowMergeView1.Rows[currentIndex].Cells["出勤機コード"].Value;
            this.rowMergeView1.Rows[rowIndex].Cells["出勤機場所"].Value = this.rowMergeView1.Rows[currentIndex].Cells["出勤機場所"].Value;
            this.rowMergeView1.Rows[rowIndex].Cells["登録コード"].Value = this.rowMergeView1.Rows[currentIndex].Cells["登録コード"].Value;
            this.rowMergeView1.Rows[rowIndex].Cells["クラスコード"].Value = this.rowMergeView1.Rows[currentIndex].Cells["クラスコード"].Value;
            this.rowMergeView1.Rows[rowIndex].Cells["教師名"].Value = this.rowMergeView1.Rows[currentIndex].Cells["教師名"].Value;
            this.rowMergeView1.Rows[rowIndex].Cells["出退勤フラグ"].Value = this.rowMergeView1.Rows[currentIndex].Cells["出退勤フラグ"].Value;
            this.rowMergeView1.Rows[rowIndex].Cells["出退勤時間"].Value = DateTime.Parse(this.rowMergeView1.Rows[currentIndex].Cells["出退勤時間"].Value.ToString()).AddSeconds(1).ToString("yyyy/MM/dd HH:mm:ss");
            this.rowMergeView1.Rows[rowIndex].Cells["NewRowフラグ"].Value = true;
            this.rowMergeView1.Rows[rowIndex].Selected = true;
            this.rowMergeView1.Rows[rowIndex].Cells["元日付"].Value= this.rowMergeView1.Rows[currentIndex].Cells["出退勤時間"].Value;
            this.rowMergeView1.FirstDisplayedScrollingRowIndex = rowIndex;
        }

        /// <summary>
        /// 出勤削除
        /// </summary>
        private void 削除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.rowMergeView1.CurrentCell != null)
            {
                this.rowMergeView1.CurrentCell.Selected = true;
                string 出退勤flag = "";
                //画面値取得
                switch (this.rowMergeView1.CurrentRow.Cells["出退勤フラグ"].Value.ToString())
                {
                    case "出勤":
                        出退勤flag = "0";
                        break;
                    case "退勤":
                        出退勤flag = "1";
                        break;
                    default:
                        出退勤flag = "";
                        break;
                }
                string date_出退勤時間 = this.rowMergeView1.CurrentRow.Cells["出退勤時間"].Value.ToString();

                //新旧フラグ
                bool isNew = false;

                if (this.rowMergeView1.CurrentRow.Cells["NewRowフラグ"].Value != null)
                {
                    isNew = this.rowMergeView1.CurrentRow.Cells["NewRowフラグ"].Value.ToString() == "True" ? true : false;
                }

                if (isNew)
                {
                    //datagridview再表示
                    DisplayGridView();
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
                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "DBサーバーの接続に失敗しました.";
                    ((Form1)(this.Tag)).reLoad = false;
                    return;
                }
                SqlCommand sqlcom = new SqlCommand();
                sqlcom.Connection = sqlcon;

                try
                {
                    //出勤機元記録から削除行う
                    sqlcom.CommandText = string.Format(@"Delete From HL_JINJI_出勤機元記録 Where 出退勤フラグ = '{0}' and 出退勤時間 = '{1}'", 出退勤flag, date_出退勤時間);

                    result = sqlcom.ExecuteNonQuery();

                    if (result == 1)
                    {
                        ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Green;
                        ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "出勤機元記録が正常に削除されました.";
                        ((Form1)(this.Tag)).reLoad = false;
                        sqlcon.Close();
                    }
                }
                catch
                {
                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "出勤機元記録の削除処理が失敗しました.";
                    ((Form1)(this.Tag)).reLoad = false;
                }
                finally
                {
                    if (sqlcon != null)
                    {
                        sqlcon.Close();
                    }
                }

                //選択する行に移す
                this.rowMergeView1.Rows.Remove(this.rowMergeView1.CurrentRow);
            }
        }

        /// <summary>
        /// データがおかしい時
        /// </summary>
        private void rowMergeView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
            ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "データの取得処理が失敗しました、データベースをチェックしてください。";
            ((Form1)(this.Tag)).reLoad = false;
        }

        /// <summary>
        /// 学生コードによってdatagridviewソート
        /// </summary>
        private void rowMergeView1_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (e.Column.Name == "学生コード")
            {
                e.SortResult = (Convert.ToDouble(e.CellValue1) - Convert.ToDouble(e.CellValue2) > 0) ? 1 : (Convert.ToDouble(e.CellValue1) - Convert.ToDouble(e.CellValue2) < 0) ? -1 : 0;

                e.Handled = true;
            }
        }
    }
}
