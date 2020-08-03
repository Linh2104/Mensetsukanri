using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Threading;

namespace HL_塾管理
{
    public partial class 出勤表エラー記録 : DockContent
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int BringWindowToTop(IntPtr hWnd);
        private AutoSizeFormClass asc = new AutoSizeFormClass();

        //データベース接続情報
        private string connectionString = ComClass.connectionString;

        //更新フラグ
        private bool isEditing = false;

        //画面値取得
        private string code_selectedRow = "";
        private string date_selectedRow = "";

        public 出勤表エラー記録()
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
        /// datagridviewデータ検索
        /// </summary>
        private void DisplayGridView()
        {
            roomView.Rows.Clear();
            //DisplayGridViewCombo();
            //画面値を取得
            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库

            try
            {
                sqlcon.Open();
            }
            catch
            {
                toolStripStatusLabel1.Text = "";
                toolStripStatusLabel1.Text = "DBサーバーの接続に失敗しました.";
                return;
            }

            string date_日付 = dtp_date.Value.ToString("yyyyMM");

            //add liuxiaoyan 20200422
            //string code_学生コード = txt_学生コード.Text;
            string code_学生コード = cmb_学生コード.Text.Split(' ')[0].Equals("ALL") ? "" : cmb_学生コード.Text.Split(' ')[0];

            //検索開始


            string str_sqlcmd = "select ccc.出勤機コード,bbb.学生コード,ddd.名前,aaa.登録コード,ccc.出退勤フラグ,ccc.出退勤時間 from HL_JINJI_出勤機ダウンロード記録 aaa"
                             + " left join HL_JINJI_出勤機_登録ユーザマスタ bbb on aaa.登録コード = bbb.登録コード "
                             + " left join HL_JINJI_出勤機元記録 ccc on aaa.登録コード = ccc.登録コード and aaa.年 + '/' + aaa.月 + '/' + aaa.日 = CONVERT(date, ccc.出退勤時間)"
                             + " left join HL_JUKUKANRI_学生マスタ ddd on bbb.学生コード = ddd.学生コード "
                             + " left join HL_JUKUKANRI_学生情報 eee on bbb.学生コード = eee.学生コード "
                             + " left join HL_JINJI_社員在職状態 fff on eee.社員コード = fff.社員コード "
                             + " where CONVERT(VARCHAR(6), ccc.出退勤時間, 112) = '" + date_日付 + "' and (bbb.社員コード = null and";
            if (string.IsNullOrWhiteSpace(code_学生コード))
            {
                str_sqlcmd += " bbb.学生コード is not null) ";
            }
            else
            {
                str_sqlcmd += " bbb.学生コード = '" + code_学生コード + "') ";
            }
            str_sqlcmd += " or  (bbb.学生コード is not null and (fff.在職状態 != '在職' or (fff.在職状態 = '停職' and fff.社員種別 = '研修'))) group by ccc.出勤機コード,bbb.学生コード,ddd.名前,aaa.登録コード,ccc.出退勤フラグ,ccc.出退勤時間 order by bbb.学生コード,ccc.出退勤時間";

            SqlDataAdapter sqlDa = new SqlDataAdapter(str_sqlcmd, sqlcon);
            DataTable dt = new DataTable();
            sqlDa.Fill(dt);
            this.toolStripStatusLabel1.Text = string.Format("{0}件", dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                int rowIndex = 0;

                //データ表示
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.roomView.Rows.Add();
                    //roomView.Rows[rowIndex].Cells["出勤機コード"].Value = dt.Rows[i]["出勤機コード"].ToString();
                    roomView.Rows[rowIndex].Cells["出勤機"].Value = dt.Rows[i]["出勤機コード"].ToString();
                    roomView.Rows[rowIndex].Cells["学生コード"].Value = dt.Rows[i]["学生コード"].ToString();
                    roomView.Rows[rowIndex].Cells["学生名"].Value = dt.Rows[i]["名前"];
                    roomView.Rows[rowIndex].Cells["登録コード"].Value = dt.Rows[i]["登録コード"].ToString();
                    switch (dt.Rows[i]["出退勤フラグ"].ToString())
                    {
                        case "0":
                        case "2":
                        case "4":
                            roomView.Rows[rowIndex].Cells["出退勤フラグ"].Value = true;
                            roomView.Rows[rowIndex].Cells["出退勤フラグ"].Style.BackColor = Color.FromArgb(255, 128, 255, 255);
                            roomView.Rows[rowIndex].Cells["出退勤フラグ"].Style.ForeColor = Color.Blue;
                            break;
                        default:
                            roomView.Rows[rowIndex].Cells["出退勤フラグ"].Value = false;
                            roomView.Rows[rowIndex].Cells["出退勤フラグ"].Style.BackColor = Color.FromArgb(255, 128, 255, 255);
                            roomView.Rows[rowIndex].Cells["出退勤フラグ"].Style.ForeColor = Color.Blue;
                            break;
                    }
                    roomView.Rows[rowIndex].Cells["出退勤時間"].Value = dt.Rows[i]["出退勤時間"].ToString() == "" ? "-" : DateTime.Parse(dt.Rows[i]["出退勤時間"].ToString()).ToString("yyyy/MM/dd HH:mm:ss");
                    roomView.Rows[rowIndex].Cells["出退勤時間"].Style.BackColor = Color.FromArgb(255, 128, 255, 255);
                    roomView.Rows[rowIndex].Cells["出退勤時間"].Style.ForeColor = Color.Blue;
                    roomView.Rows[rowIndex].Cells["元日時"].Value = dt.Rows[i]["出退勤時間"].ToString();
                    roomView.Rows[rowIndex].Cells["NewRowフラグ"].Value = false;
                    rowIndex++;
                }
            }

            //選択する行に移す
            SetSelectedRow(code_selectedRow, date_selectedRow);
            code_selectedRow = "";
            date_selectedRow = "";

            if (sqlcon != null)

            {
                sqlcon.Close();
            }
        }

        /// <summary>
        /// 初期コンボボックス表示
        /// </summary>
        private void 学生コードInit()
        {
            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库

            try
            {
                sqlcon.Open();
            }
            catch
            {
                toolStripStatusLabel1.Text = "";
                toolStripStatusLabel1.Text = "DBサーバーの接続に失敗しました。";
                return;
            }

            string date_日付 = dtp_date.Value.ToString("yyyyMM");

            string str_sqlcmd = string.Format(@"select distinct aaa.出勤機コード,bbb.学生コード,aaa.登録コード,ddd.名前 from HL_JINJI_出勤機ダウンロード記録 aaa
            left join HL_JINJI_出勤機_登録ユーザマスタ bbb on aaa.登録コード = bbb.登録コード 
            left join HL_JINJI_出勤機元記録 ccc on aaa.登録コード = ccc.登録コード and aaa.年 + '/' + aaa.月 + '/' + aaa.日 = CONVERT(date, ccc.出退勤時間)
            inner join HL_JUKUKANRI_学生マスタ ddd on bbb.学生コード = ddd.学生コード
            where aaa.年 = {0} and aaa.月 = {1}", this.dtp_date.Value.Year, this.dtp_date.Value.Month);

            SqlCommand com = new SqlCommand(str_sqlcmd, sqlcon);

            SqlDataReader reader = com.ExecuteReader();

            this.cmb_学生コード.Items.Clear();

            this.cmb_学生コード.Items.Add("ALL");
            while (reader.Read())
            {
                this.cmb_学生コード.Items.Add(reader["学生コード"].ToString() + " " + reader["名前"].ToString());
            }

            if (this.cmb_学生コード.Items.Count > 0)
            {
                this.cmb_学生コード.SelectedIndex = 0;
            }

            reader.Close();
            sqlcon.Close();
        }

        /// <summary>
        /// 画面表示
        /// </summary>
        private void 出勤表エラー記録_Load(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);
            this.toolStripStatusLabel1.Text = "0件";
            //初期cmb設定
            学生コードInit();
            //datagridview表示
            DisplayGridView();
        }

        /// <summary>
        /// 画面閉じ
        /// </summary>
        private void 出勤表エラー記録_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form1)(this.Tag)).m_出勤表エラー記録Handle = IntPtr.Zero;
        }

        /// <summary>
        /// 情報追加
        /// </summary>
        private void 追加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //一行追加とセル値設定
            int currentIndex = this.roomView.CurrentRow.Index;
            int rowIndex = this.roomView.Rows.Add();
            Thread.Sleep(500);

            this.roomView.CurrentRow.Selected = false;
            this.roomView.Rows[rowIndex].Cells["学生コード"].Value = this.roomView.Rows[currentIndex].Cells["学生コード"].Value;
            this.roomView.Rows[rowIndex].Cells["学生名"].Value = this.roomView.Rows[currentIndex].Cells["学生名"].Value;
            this.roomView.Rows[rowIndex].Cells["出勤機"].Value = this.roomView.Rows[currentIndex].Cells["出勤機"].Value;
            this.roomView.Rows[rowIndex].Cells["登録コード"].Value = this.roomView.Rows[currentIndex].Cells["登録コード"].Value;
            this.roomView.Rows[rowIndex].Cells["出退勤フラグ"].Value = this.roomView.Rows[currentIndex].Cells["出退勤フラグ"].Value;
            this.roomView.Rows[rowIndex].Cells["出退勤時間"].Value = DateTime.Parse(this.roomView.Rows[currentIndex].Cells["出退勤時間"].Value.ToString()).AddSeconds(1).ToString("yyyy/MM/dd HH:mm:ss");
            this.roomView.Rows[rowIndex].Cells["元日時"].Value = this.roomView.Rows[rowIndex].Cells["出退勤時間"].Value;
            this.roomView.Rows[rowIndex].Cells["NewRowフラグ"].Value = true;
            this.roomView.Rows[rowIndex].Selected = true;
            this.roomView.FirstDisplayedScrollingRowIndex = rowIndex;

            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库

            int result = 0;
            try
            {
                sqlcon.Open();
            }
            catch
            {
                this.toolStripStatusLabel1.ForeColor = Color.Red;
                this.toolStripStatusLabel1.Text = "サーバーがつなげない！インターネット接続をチェックしてください。";

                return;
            }
            SqlCommand sqlcom = new SqlCommand();
            sqlcom.Connection = sqlcon;

            try
            {
                string month = this.roomView.Rows[rowIndex].Cells["出退勤時間"].Value.ToString().Split('/')[1];
                if (month.Substring(0,1)=="0")
                {
                    month = month.Substring(1, 1);
                }
                //出勤機元記録に登録
                sqlcom.CommandText = string.Format(@"
                    delete HL_JINJI_出勤機元記録 where 出勤機コード = '{0}' and 登録コード = {1} and 出退勤時間 = '{3}'
                    delete HL_JINJI_出勤機ダウンロード記録 where 出勤機コード = '{0}' and 登録コード = {1} and 年 = '{4}' and 月 = '{5}' and 日 = '{6}'
                    Insert Into HL_JINJI_出勤機元記録 Values('{0}', '{1}', {2}, '{3}')
                    insert into HL_JINJI_出勤機ダウンロード記録 values('{0}', '{1}','{4}','{5}','{6}','新規')",
                    this.roomView.Rows[rowIndex].Cells["出勤機"].Value.ToString(), this.roomView.Rows[rowIndex].Cells["登録コード"].Value.ToString(),
                    this.roomView.Rows[rowIndex].Cells["出退勤フラグ"].Value.ToString().Equals("TRUE") ? 0 : 1, this.roomView.Rows[rowIndex].Cells["出退勤時間"].Value.ToString(),
                    this.roomView.Rows[rowIndex].Cells["出退勤時間"].Value.ToString().Split('/')[0], month,
                    this.roomView.Rows[rowIndex].Cells["出退勤時間"].Value.ToString().Split('/')[2].Split(' ')[0]);

                result = sqlcom.ExecuteNonQuery();

                if (result == 1)
                {
                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Green;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "出勤機元記録が正常に追加されました。";                    
                    //this.toolStripStatusLabel1.ForeColor = Color.Blue;
                    //this.toolStripStatusLabel1.Text = "出勤機元記録が正常に追加されました.";
                    sqlcon.Close();
                }
            }
            catch
            {
                ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "出勤機元記録の更新処理が失敗しました。";
                //this.toolStripStatusLabel1.ForeColor = Color.Red;
                //this.toolStripStatusLabel1.Text = "出勤機元記録の更新処理が失敗しました.";
            }
            finally
            {
                if (sqlcon != null)
                {
                    sqlcon.Close();
                }
            }

            string str学生 = this.cmb_学生コード.Text;

            学生コードInit();

            this.cmb_学生コード.Text = str学生;

            this.toolStripStatusLabel1.ForeColor = Color.Black;
            this.toolStripStatusLabel1.Text = "";
        }

        /// <summary>
        ///  一覧から選択行を削除処理
        /// </summary>
        private void 削除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.roomView.CurrentCell != null)
            {
                this.roomView.CurrentCell.Selected = true;
                //画面値取得
                string code_登録コード = this.roomView.CurrentRow.Cells["登録コード"].Value.ToString();
                string date_出退勤時間 = this.roomView.CurrentRow.Cells["出退勤時間"].Value.ToString();

                //新旧フラグ、チェック
                bool isNew = false;
                if (this.roomView.CurrentRow.Cells["元日時"].Value != null)
                {
                    isNew = this.roomView.CurrentRow.Cells["NewRowフラグ"].Value.ToString() == "True" ? true : false;
                }

                if(isNew)
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
                    toolStripStatusLabel1.Text = "";
                    toolStripStatusLabel1.Text = "DBサーバーの接続に失敗しました.";
                    return;
                }
                SqlCommand sqlcom = new SqlCommand();
                sqlcom.Connection = sqlcon;

                try
                {
                    //出勤機元記録から削除行う
                    sqlcom.CommandText = string.Format(@"Delete From HL_JINJI_出勤機元記録 Where 登録コード = '{0}' and 出退勤時間 = '{1}'", code_登録コード, date_出退勤時間);

                    result = sqlcom.ExecuteNonQuery();

                    if (result == 1)
                    {
                        ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "";
                        ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "出勤機元記録が正常に削除されました。";
                        ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Green;
                        sqlcon.Close();
                    }
                }
                catch
                {
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "";
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "出勤機元記録の削除処理が失敗しました。";
                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                }
                finally
                {
                    if (sqlcon != null)
                    {
                        sqlcon.Close();
                    }
                }

                //選択する行に移す
                this.roomView.Rows.Remove(this.roomView.CurrentRow);
            }
        }

        /// <summary>
        /// 右クリックで一行選択状態設定
        /// </summary>
        private void roomView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    if ((this.roomView.Rows[e.RowIndex].Selected == false))
                    {
                        this.roomView.ClearSelection();
                        this.roomView.Rows[e.RowIndex].Selected = true;
                    }
                    //'只选中一行时设置活动单元格
                    if ((this.roomView.SelectedRows.Count == 1))
                    {
                        this.roomView.CurrentCell = this.roomView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    }
                }
            }
        }

        /// <summary>
        /// メニューを開く
        /// </summary>
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            System.Drawing.Point startPosition = Cursor.Position;

            System.Drawing.Point point = this.roomView.PointToClient(startPosition);
            DataGridView.HitTestInfo hitinfo;
            hitinfo = this.roomView.HitTest(point.X, point.Y);

            this.roomView.ClearSelection();
            //選択状態設定
            if (hitinfo.RowIndex >= 0)
            {
                this.roomView.Rows[hitinfo.RowIndex].Selected = true;
            }
            else
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 日付変更
        /// </summary>
        private void dtp_date_ValueChanged(object sender, EventArgs e)
        {
            学生コードInit();
            //datagridview再表示
            DisplayGridView();

            //MSG削除
            ((Form1)(Tag)).toolStripStatusLabel2.Text = "";
        }

        /// <summary>
        /// セル値変更
        /// </summary>
        private void roomView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (isEditing && this.roomView.CurrentCell != null)
            {
                this.roomView.CurrentCell.Selected = true;
                //画面値取得
                string code_出勤機コード = this.roomView.CurrentRow.Cells["出勤機"].Value.ToString();
                string code_登録コード = this.roomView.CurrentRow.Cells["登録コード"].Value.ToString();
                code_selectedRow = code_出勤機コード;
                int flag_出勤機フラグ = this.roomView.CurrentRow.Cells["出退勤フラグ"].Value.ToString() == "True" ? 0 : 1;
                string date_出退勤時間 = "";
                try
                {
                    //日付形式チェック
                    date_出退勤時間 = this.roomView.CurrentRow.Cells["出退勤時間"].Value.ToString();
                    DateTime dateTime = DateTime.ParseExact(date_出退勤時間, "yyyy/MM/dd HH:mm:ss", null);
                }
                catch
                {
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "";
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "正しい時間を入力してください。";
                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                    int index_col = roomView.CurrentCell.ColumnIndex;
                    int index_row = roomView.CurrentCell.RowIndex;
                    //datagridview再表示
                    DisplayGridView();

                    roomView.Rows[index_row].Cells[index_col].Selected = true;
                    return;
                }

                date_selectedRow = date_出退勤時間;
                string date_元日時 = this.roomView.CurrentRow.Cells["元日時"].Value.ToString();

                //新旧フラグ、チェック
                bool isNew = false;
                if (this.roomView.CurrentRow.Cells["NewRowフラグ"].Value != null)
                {
                    isNew = this.roomView.CurrentRow.Cells["NewRowフラグ"].Value.ToString() == "True" ? true : false;
                }
                SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库

                int result = 0;
                try
                {
                    sqlcon.Open();
                }
                catch
                {
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "";
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "DBサーバーの接続に失敗しました。";
                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                    return;
                }
                SqlCommand sqlcom = new SqlCommand();
                sqlcom.Connection = sqlcon;

                try
                {
                    if (isNew)
                    {
                        //重複チェック
                        sqlcom.CommandText = string.Format(@"Select * from HL_JINJI_出勤機元記録 Where 出勤機コード = '{0}' and 登録コード = '{1}' and 出退勤フラグ = {2} and 出退勤時間 = '{3}'", code_出勤機コード, code_登録コード, flag_出勤機フラグ, date_出退勤時間);

                        result = sqlcom.ExecuteNonQuery();

                        if (result == 1)
                        {
                            ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "";
                            ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "重複データが追加できません。";
                            ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                        }
                        else
                        {
                            //出勤機元記録に登録
                            sqlcom.CommandText = string.Format(@"Insert Into HL_JINJI_出勤機元記録 Values('{0}', '{1}', {2}, '{3}')", code_出勤機コード, code_登録コード, flag_出勤機フラグ, date_出退勤時間);

                            result = sqlcom.ExecuteNonQuery();

                            if (result == 1)
                            {
                                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "";
                                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "出勤機元記録が正常に追加されました。";
                                ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Green;
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

                        sqlcom.CommandText = string.Format(sql_update, code_出勤機コード, flag_出勤機フラグ, date_出退勤時間, code_登録コード, date_元日時);

                        result = sqlcom.ExecuteNonQuery();

                        if (result == 1)
                        {
                            ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "";
                            ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "出勤機元記録が正常に更新されました。";
                            ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Green;
                            sqlcon.Close();
                        }
                    }
                }
                catch
                {
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "";
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "出勤機元記録の更新処理が失敗しました。";
                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
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
        private void roomView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            isEditing = true;
        }

        /// <summary>
        /// セル編集終了
        /// </summary>
        private void roomView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            isEditing = false;
        }

        ////add liuxiaoyan 20200422
        ///// <summary>
        ///// 検索textbox値変更
        ///// </summary>
        //private void txt_学生コード_TextChanged(object sender, EventArgs e)
        //{
        //    //datagridview再表示
        //    DisplayGridView();
        //}
        ////　end

        ///// <summary>
        ///// 検索textbox値変更
        ///// </summary>
        //private void txt_社員コード_TextChanged(object sender, EventArgs e)
        //{
        //    //datagridview再表示
        //    DisplayGridView();
        //}

        /// <summary>
        /// datagridviewサイズ変更
        /// </summary>
        private void 出勤表エラー記録_SizeChanged(object sender, EventArgs e)
        {
            this.roomView.Width = 800;
        }

        /// <summary>
        /// 行の選択状態設定
        /// </summary>
        private void SetSelectedRow(string code, string dateTime)
        {
            if (!string.IsNullOrWhiteSpace(code) && !string.IsNullOrWhiteSpace(dateTime) && this.roomView.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in roomView.Rows)
                {
                    if (row.Cells[2].Value.ToString() == code && row.Cells[4].Value.ToString() == dateTime)
                    {
                        this.roomView.Rows[row.Index].Selected = true;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// エラー修正
        /// </summary>
        private void btn_エラー修正_Click(object sender, EventArgs e)
        {
            if (cmb_学生コード.Text.Split(' ')[0].Equals("ALL"))
            {
                for (int i = 1; i < cmb_学生コード.Items.Count; i++)
                {
                    出勤表エラーを修正する(cmb_学生コード.Items[i].ToString().Split(' ')[0]);
                }
            }
            else
            {
                出勤表エラーを修正する(cmb_学生コード.Text.Split(' ')[0]);
            }
        }

        /// <summary>
        /// 出勤表エラー修正
        /// </summary>
        private void 出勤表エラーを修正する(string code_学生コード)
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
            SqlCommand sqlcom = new SqlCommand();
            sqlcom.Connection = sqlcon;

            try
            {
                //sqlcom.CommandText = string.Format(@"delete HL_JINJI_出勤機ダウンロード記録 where 年 = {0} and 月 = {1} 
                //    and 登録コード in (select 登録コード from HL_JINJI_出勤機_登録ユーザマスタ where 社員コード like '%{2}%')",
                //    this.dateTimePicker1.Value.Year, this.dateTimePicker1.Value.Month, code_社員コード);
                sqlcom.CommandText = string.Format(@"delete HL_JINJI_出勤機ダウンロード記録 where 年 = {0} and 月 = {1}
                and 登録コード = (select 登録コード from HL_JINJI_出勤機_登録ユーザマスタ where 学生コード = {2})",
                     this.dtp_date.Value.Year, this.dtp_date.Value.Month, code_学生コード);

                string sql = sqlcom.CommandText;

                result = sqlcom.ExecuteNonQuery();

                if (result == 1)
                {
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "";
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "出勤記録エラーが正常に修正されました。";
                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Green;
                    //this.toolStripStatusLabel1.ForeColor = Color.Blue;
                    //this.toolStripStatusLabel1.Text = "出勤機元記録が正常に更新されました。";

                    sqlcon.Close();
                }
            }
            catch (Exception)
            {

                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "";
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "出勤記録エラー修正処理が失敗しました。";
                ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
            }

            //Insert_JINJI_出勤表(code_学生コード);
            学生コードInit();
            DisplayGridView();
        }

        /// <summary>
        /// 出勤表登録
        /// </summary>
        private void Insert_JINJI_出勤表(string code_学生コード)
        {
            string connectionString = ComClass.connectionString;
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

            //対象年月
            int year = this.dtp_date.Value.Year;
            int month = this.dtp_date.Value.Month;
            Dictionary<int, int[]> yearMonthList = new Dictionary<int, int[]>();//bug可能性あり
            yearMonthList.Add(1, new int[] { year, month });

            foreach (int[] list in yearMonthList.Values)
            {
                year = list[0];
                month = list[1];

                //出勤表データ情報取得
                string sql2 = string.Format(@" SELECT T1.出勤機コード, T1.登録コード, T3.社員コード, T3.名前, 
                    (CASE WHEN T1.出退勤フラグ IN ('0', '2', '4') THEN '出社' 
                    WHEN T1.出退勤フラグ IN ('1', '3', '5') THEN '退社' ELSE '無効' END) AS '出退勤フラグ', T1.出退勤時間
                    FROM HL_JINJI_出勤機元記録 AS T1
                    LEFT JOIN HL_JINJI_出勤機_登録ユーザマスタ AS T2 ON T1.登録コード = T2.登録コード
                    LEFT JOIN HL_JINJI_社員マスタ AS T3 ON T2.社員コード = T3.社員コード
                    WHERE  YEAR( DATEADD(YEAR,0,T1.出退勤時間)) = '{0}' and   MONTH( DATEADD(MONTH,0,T1.出退勤時間)) = '{1}' AND T2.社員コード like '%{2}%'
                    ORDER BY 登録コード, 出退勤時間", year, month, code_学生コード);

                System.Data.DataTable dt = new System.Data.DataTable();

                try
                {
                    SqlDataAdapter sqlDa = new SqlDataAdapter(sql2, conn);

                    sqlDa.Fill(dt);
                }
                catch (Exception ex)
                {
                    this.toolStripStatusLabel1.ForeColor = Color.Red;
                    this.toolStripStatusLabel1.Text = ex.Message;
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    System.Data.DataTable dataInfo = new System.Data.DataTable();
                    string code = "";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i == 0 || code != dt.Rows[i]["登録コード"].ToString())
                        {
                            code = dt.Rows[i]["登録コード"].ToString();
                            dataInfo = dt.AsEnumerable().Where(r => r["登録コード"].ToString() == code).CopyToDataTable();

                            int code_出勤機コード = Convert.ToInt32(dt.Rows[i]["出勤機コード"].ToString());

                            Insert_出勤機ダウンロード記録(code_出勤機コード, code, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), DateTime.Now.Day.ToString(), "");

                        }
                    }

                    //すべて正常の場合
                    this.toolStripStatusLabel1.ForeColor = Color.Blue;
                    this.toolStripStatusLabel1.Text = "出勤表アプロード処理完了しました。";
                }
            }
        }

        /// <summary>
        /// ダウンロードエラー登録処理
        /// </summary>
        /// <remarks>出勤表の情報をダウンロード中にエラーがある場合、エラー情報をDBに格納する</remarks>
        /// <param name="code_出勤機"></param>
        /// <param name="code_登録コード"></param>
        /// <param name="day"></param>
        /// <param name="errmsg"></param>
        private void Insert_出勤機ダウンロード記録(int code_出勤機, string code_登録コード, string strYear, string strMon, string strDay, string errmsg)
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
                this.toolStripStatusLabel1.Text = "異常：サーバーがつなげない！インターネット接続をチェックしてください。";
                return;
            }

            string str_sqlcmd = "INSERT INTO [dbo].[HL_JINJI_出勤機ダウンロード記録] ( ";
            str_sqlcmd += "   [出勤機コード] ";
            if (code_登録コード != "")
            {
                str_sqlcmd += "   , [登録コード]";
            }
            str_sqlcmd += "   , [年] ";
            str_sqlcmd += "   , [月] ";
            str_sqlcmd += "   , [日] ";
            str_sqlcmd += "   , [エラー] )";
            str_sqlcmd += " VALUES (  ";
            str_sqlcmd += code_出勤機;
            if (code_登録コード != "")
            {
                str_sqlcmd += "   ," + Convert.ToInt32(code_登録コード);
            }
            str_sqlcmd += "   , '" + strYear + "' ";
            str_sqlcmd += "   , '" + strMon + "' ";
            str_sqlcmd += "   , '" + strDay + "' ";
            str_sqlcmd += "   , '" + errmsg + "' )";

            try
            {
                SqlCommand com = new SqlCommand(str_sqlcmd, conn);
                int recnt = com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                this.toolStripStatusLabel1.ForeColor = Color.Red;
                this.toolStripStatusLabel1.Text = ex.Message;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        private void cmb_学生コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            //datagridview再表示
            DisplayGridView();
        }

        private void btn_出勤記録新規追加_Click(object sender, EventArgs e)
        {
            if (((Form1)(this.Tag)).m_出勤記録新規追加画面Handle != IntPtr.Zero)
            {
                BringWindowToTop(((Form1)(this.Tag)).m_出勤記録新規追加画面Handle);
                return;
            }

            出勤記録新規追加画面 m_NewForm_出勤記録新規追加画面 = new 出勤記録新規追加画面();
            m_NewForm_出勤記録新規追加画面.Tag = ((Form1)(this.Tag));
            m_NewForm_出勤記録新規追加画面.Init(this.cmb_学生コード.Text.Split(' ')[0]);
            m_NewForm_出勤記録新規追加画面.Show();

            ((Form1)(this.Tag)).m_出勤記録新規追加画面Handle = m_NewForm_出勤記録新規追加画面.Handle;
            toolStripStatusLabel1.Text = "";
        }
    }
}
