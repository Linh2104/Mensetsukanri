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
    public partial class 出勤機学生一覧 : DockContent
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int BringWindowToTop(IntPtr hWnd);
        private AutoSizeFormClass asc = new AutoSizeFormClass();

        //データベース接続情報
        private string connectionString = ComClass.connectionString;

        //出勤機コードリスト
        private DataTable list_出勤機コード = new DataTable();

        //更新フラグ
        private bool isEditing = false;

        //変更前のセル値
        private string beforValue = "";

        //出勤機コード
        private string 出勤機コード = "";

        public 出勤機学生一覧()
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
        private void 出勤機学生一覧_Load(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);

            //出勤機コードリストデータゲット
            SetMachineCodeList();

            if (list_出勤機コード == null)
            {
                ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "DBサーバーの接続に失敗しました.";
                ((Form1)(this.Tag)).reLoad = false; 
                return;
            }

            //一覧表示
            DisplayGridView();
        }

        /// <summary>
        /// 出勤機コードリスト設定
        /// </summary>
        private void SetMachineCodeList()
        {
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
            //検索開始
            //string str_sqlcmd = @"SELECT 出勤機コード FROM HL_JINJI_出勤機マスタ ORDER BY 出勤機コード";
            string str_sqlcmd = "SELECT a.出勤機コード, a.場所 FROM HL_JINJI_出勤機マスタ a ORDER BY 出勤機コード";
            try
            {
                SqlDataAdapter sqlDa = new SqlDataAdapter(str_sqlcmd, sqlcon);
                sqlDa.Fill(list_出勤機コード);
                //データ表示
                //cmb_出勤機コード.Items.Add("ALL");
                //for (int i = 0; i < list_出勤機コード.Rows.Count; i++)
                //{
                //    cmb_出勤機コード.Items.Add(list_出勤機コード.Rows[i]["出勤機コード"].ToString());
                //}

                //cmb_出勤機コード.Text = "ALL";
            }
            catch
            {
                ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "出勤機コードリスト取得処理に失敗しました.";
                ((Form1)(this.Tag)).reLoad = false;
            }
            finally
            {
                if (sqlcon != null)
                {
                    sqlcon.Close();
                }
            }
        }

        /// <summary>
        /// 出勤機コード変更
        /// </summary>
        private void cmb_出勤機コード_TextChanged(object sender, EventArgs e)
        {
            //datagridview再表示
            DisplayGridView();
        }

        //日付変更
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
            try
            {
                gridView_出勤機ユーザ.Rows.Clear();
            }
            catch
            {

            }
            gridView_出勤機ユーザ.Rows.Clear();

            //画面値取得
            string search = txt_searchKey.Text;
            //string code_出勤機コード = cmb_出勤機コード.Text;
            SqlConnection sqlcon = new SqlConnection(connectionString);

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

            //検索開始
            string str_sqlcmd = " SELECT a.出勤機コード, b.場所,a.登録コード, a.学生コード,c.名前 as 学生名,f.名前 as 教師名 FROM HL_JINJI_出勤機_登録ユーザマスタ as a" +
                                " left join HL_JINJI_出勤機マスタ as b on a.出勤機コード=b.出勤機コード " +
                                " inner join HL_JUKUKANRI_学生マスタ as c on a.学生コード=c.学生コード " +
                                " left join HL_JUKUKANRI_学生クラス as d on c.学生コード=d.学生コード" +
                                " left join HL_JUKUKANRI_クラス履歴 as e on e.クラスコード=d.クラスコード" +
                                " left join HL_JUKUKANRI_教師情報 as f on e.教師コード=f.教師コード" +
                                " left join HL_JUKUKANRI_学生情報 as g on a.学生コード=g.学生コード" +
                                " left join HL_JINJI_社員在職状態 as h on h.社員コード=g.社員コード" +
                                " WHERE (a.社員コード = null and a.学生コード IS NOT NULL AND a.学生コード !='') or (a.学生コード IS NOT NULL and (h.在職状態 != '在職' or (h.在職状態 = '停職' and h.社員種別 = '研修')))";

            //ログインユーザー権限判断   Wang Qian start 2020/7/17
            if (((Form1)(this.Tag)).m_ユーザ登録.m_ログイン_職務 != "管理者")
            {
                str_sqlcmd += " and f.名前 ='" + ((Form1)(this.Tag)).m_ユーザ登録.m_ログイン_ユーザ氏名 + "'";
            }

            str_sqlcmd += " and (a.出勤機コード like '%" + search + "%' " +
                          " or b.場所 like '%" + search + "%'" +
                          " or a.登録コード like '%" + search + "%'" +
                          " or a.学生コード like '%" + search + "%'" +
                          " or c.名前 like '%" + search + "%'";

            if (((Form1)(this.Tag)).m_ユーザ登録.m_ログイン_職務 == "管理者")
            {
                str_sqlcmd += " or f.名前 like '%" + search + "%'";
            }

            str_sqlcmd += ") ORDER BY 出勤機コード, 登録コード  ";
            //Wang Qian end 2020/7/17

            SqlDataAdapter sqlDa = new SqlDataAdapter(str_sqlcmd, sqlcon);
            DataTable dt = new DataTable();
            sqlDa.Fill(dt);
            try
            {
                if (dt.Rows.Count > 0)
                {
                    int index = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        //データ表示
                        this.gridView_出勤機ユーザ.Rows.Add();
                        if (list_出勤機コード.Rows.Count > 0)
                        {
                            for(int i = 0; i< list_出勤機コード.Rows.Count; i++)
                            {
                                ((DataGridViewComboBoxCell)(this.gridView_出勤機ユーザ.Rows[index].Cells["出勤機"])).Items.Add(list_出勤機コード.Rows[i]["場所"].ToString());
                            }
                            this.gridView_出勤機ユーザ.Rows[index].Cells["出勤機"].Value = row["場所"].ToString();
                        }
                        this.gridView_出勤機ユーザ.Rows[index].Cells["登録コード"].Value = row["登録コード"].ToString();
                        this.gridView_出勤機ユーザ.Rows[index].Cells["学生コード"].Value = row["学生コード"].ToString();
                        this.gridView_出勤機ユーザ.Rows[index].Cells["学生名"].Value = row["学生名"].ToString();
                        this.gridView_出勤機ユーザ.Rows[index].Cells["担当教師"].Value = row["教師名"].ToString();
                        //空白セルに"‐"を入れる
                        foreach (DataGridViewCell gvCell in gridView_出勤機ユーザ.Rows[index].Cells)
                        {
                            if (string.IsNullOrWhiteSpace(gvCell.Value.ToString()) || gvCell.Value.ToString() == "-")
                            {
                                gvCell.Value = "-";
                                gridView_出勤機ユーザ.Rows[index].Cells[gvCell.ColumnIndex].Style.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
                            }

                        }
                        index++;
                    }                 
                }
                else
                {
                    this.gridView_出勤機ユーザ.Rows.Clear();
                }
            }
            catch
            {
                ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "検索処理に失敗しました.";
                ((Form1)(this.Tag)).reLoad = false;
            }
            finally
            {
                if (sqlcon != null)
                {
                    sqlcon.Close();
                }
            }
            //件数表示
            this.toolStrip1.Text = string.Format("{0}件", dt.Rows.Count);
        }

        /// <summary>
        /// 当画面閉じられた時の処理
        /// </summary>
        private void 出勤機学生一覧_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form1)(this.Tag)).m_出勤機学生一覧Handle = IntPtr.Zero;
        }

        /// <summary>
        /// 更新後処理
        /// </summary>
        private void 出勤機学生登録_UpdateEventHandler(object sender, 出勤機学生登録.UpdateEventArgs args)
        {
            //datagridview再表示
            DisplayGridView();
        }

        /// <summary>
        ///  一覧から選択行を削除処理
        /// </summary>
        private void 削除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.gridView_出勤機ユーザ.CurrentCell != null)
            {
                this.gridView_出勤機ユーザ.CurrentCell.Selected = true;
                //画面値取得
                string code_登録コード = this.gridView_出勤機ユーザ.CurrentRow.Cells["登録コード"].Value.ToString();
                string code_学生コード = gridView_出勤機ユーザ.CurrentRow.Cells["学生コード"].EditedFormattedValue.ToString();
                SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库
                try
                {
                    sqlcon.Open();
                }
                catch 
                {
                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "DBサーバーの接続に失敗しました。";
                    ((Form1)(this.Tag)).reLoad = false;
                    return;
                }
                SqlCommand sqlcom = new SqlCommand();
                sqlcom.Connection = sqlcon;

                try
                {
                    //削除行う
                    sqlcom.CommandText = string.Format(@"Delete From HL_JINJI_出勤機_登録ユーザマスタ Where 登録コード = '{0}'", code_登録コード);


                   int result = sqlcom.ExecuteNonQuery();

                    if (result == 1)
                    {
                        ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Green;
                        ((Form1)(this.Tag)).toolStripStatusLabel2.Text = string.Format("学生コード[{0}]の情報が正常に削除されました.", code_学生コード); 
                        sqlcon.Close();
                    }
                }
                catch
                {
                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = string.Format("学生コード[{0}]の削除処理が失敗しました.", code_学生コード);
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
                this.gridView_出勤機ユーザ.Rows.Remove(this.gridView_出勤機ユーザ.CurrentRow);
            }
        }

        /// <summary>
        /// セルタイプをゲット
        /// </summary>
        private void gridView_出勤機ユーザ_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
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
        /// セル値変更
        /// </summary>
        private void TextBox_TextValueChanged()
        {
            DataGridViewRow updateRow = this.gridView_出勤機ユーザ.CurrentRow;
            if (updateRow != null)
            {
                string code_登録コード = "";
                string columnName = "";

                code_登録コード = updateRow.Cells[0].Value.ToString();
                columnName = gridView_出勤機ユーザ.Columns[gridView_出勤機ユーザ.CurrentCell.ColumnIndex].Name;

                if (Update_GridViewRow(gridView_出勤機ユーザ.Columns[gridView_出勤機ユーザ.CurrentCell.ColumnIndex].Name))
                {
                    //datagridview再表示
                    DisplayGridView();
                    //登録コードに該当する行を選択状態に設定
                    SetSelectedRow(code_登録コード);
                }
                else
                {
                    //datagridview再表示
                    DisplayGridView();
                }
            }
        }

        /// <summary>
        /// Comboxイベント处理
        /// </summary>
        public void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combox = sender as ComboBox;
            combox.Leave += new EventHandler(combox_Leave);
            DataGridViewRow updateRow = this.gridView_出勤機ユーザ.CurrentRow;
            if (this.gridView_出勤機ユーザ.CurrentCell.FormattedValue.ToString() != this.gridView_出勤機ユーザ.CurrentCell.EditedFormattedValue.ToString())
            {
                try
                {

                    //画面値取得
                    string code_出勤機コード = updateRow.Cells["出勤機"].Value.ToString();
                    string code_登録コード = updateRow.Cells["登録コード"].Value.ToString();
                    if (Update_GridViewRow(gridView_出勤機ユーザ.Columns[gridView_出勤機ユーザ.CurrentCell.ColumnIndex].Name))
                    {
                        出勤機コード = updateRow.Cells["出勤機"].EditedFormattedValue.ToString();
                    }
                }
                catch (Exception ex)
                {
                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = ex.Message;
                    ((Form1)(this.Tag)).reLoad = false;
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
        /// 右クリックで一行を選択
        /// </summary>
        private void gridView_出勤機ユーザ_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    if ((this.gridView_出勤機ユーザ.Rows[e.RowIndex].Selected == false))
                    {
                        this.gridView_出勤機ユーザ.ClearSelection();
                        this.gridView_出勤機ユーザ.Rows[e.RowIndex].Selected = true;
                    }
                    //'只选中一行时设置活动单元格
                    if ((this.gridView_出勤機ユーザ.SelectedRows.Count == 1))
                    {
                        this.gridView_出勤機ユーザ.CurrentCell = this.gridView_出勤機ユーザ.Rows[e.RowIndex].Cells[e.ColumnIndex];
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

            System.Drawing.Point point = this.gridView_出勤機ユーザ.PointToClient(startPosition);
            DataGridView.HitTestInfo hitinfo;
            hitinfo = this.gridView_出勤機ユーザ.HitTest(point.X, point.Y);

            this.gridView_出勤機ユーザ.ClearSelection();
            //選択状態設定
            if (hitinfo.RowIndex >= 0)
            {
                this.gridView_出勤機ユーザ.Rows[hitinfo.RowIndex].Selected = true;
            }
            else
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        private bool Update_GridViewRow(string columnName)
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
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "DBサーバーの接続に失敗しました。";
                ((Form1)(this.Tag)).reLoad = false;
                return isUpdate;
            }

            SqlCommand sqlcom = new SqlCommand();
            sqlcom.Connection = sqlcon;

            //画面値取得
            //string code_出勤機コード = gridView_出勤機ユーザ.CurrentRow.Cells["出勤機"].EditedFormattedValue.ToString();
            string newcode= gridView_出勤機ユーザ.CurrentRow.Cells["出勤機"].EditedFormattedValue.ToString();
            string code_出勤機コード = list_出勤機コード.Select($"場所 = '{newcode}'")[0][0].ToString();
            int code_登録コード = Convert.ToInt32(gridView_出勤機ユーザ.CurrentRow.Cells["登録コード"].EditedFormattedValue.ToString());
            //2020/04/16 liuxiaoyan dd start4
            string code_学生コード = gridView_出勤機ユーザ.CurrentRow.Cells["学生コード"].EditedFormattedValue.ToString();
            if (string.IsNullOrEmpty(code_学生コード))
            {
                ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "[学生コード]が空白に設定できません。";
                ((Form1)(this.Tag)).reLoad = false;
                return false;
            }
            //dd end

            //データチェック
            bool isError = false;
            switch(columnName)
            {
                case "学生コード":
                    if (!string.IsNullOrEmpty(code_学生コード))
                    {
                        isError = CheckSanmeData(code_学生コード, true);
                    }
                    break;
            }

            if (isError)
            {
                return false;
            }

            try
            {
                //更新行う
                //2020/04/16 liuxiaoyan dd start
                sqlcom.CommandText = string.Format("UPDATE HL_JINJI_出勤機_登録ユーザマスタ SET 出勤機コード = '{0}',  [学生コード] = '{1}' WHERE 登録コード = {2}", code_出勤機コード,  code_学生コード, code_登録コード);
                //dd end
                result = sqlcom.ExecuteNonQuery();

                if (result != 1)
                {
                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = string.Format("学生コード[{0}]の更新処理が失敗しました.", code_学生コード);
                    ((Form1)(this.Tag)).reLoad = false;
                }
                else
                {
                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Green;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = string.Format("学生コード[{0}]の情報が正常に更新されました。.", code_学生コード);
                    ((Form1)(this.Tag)).reLoad = false;
                    isUpdate = true;
                }
            }
            catch
            {
                ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = string.Format("学生コード[{0}]の更新処理が失敗しました.", code_学生コード);
                ((Form1)(this.Tag)).reLoad = false;
            }
            finally
            {
                if (sqlcon != null)
                {
                    sqlcon.Close();
                }
            }
            return isUpdate;
        }

        /// <summary>
        /// 選択状態設定
        /// </summary>
        private void SetSelectedRow(string code_登録コード)
        {
            DisplayGridView();
            if (!string.IsNullOrWhiteSpace(code_登録コード) && this.gridView_出勤機ユーザ.Rows.Count > 0)
            {
                 foreach(DataGridViewRow row in gridView_出勤機ユーザ.Rows)
                {
                    if(row.Cells["登録コード"].Value.ToString() == code_登録コード)
                    {
                        this.gridView_出勤機ユーザ.Rows[row.Index].Selected = true;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// セル値編集開始
        /// </summary>
        private void gridView_出勤機ユーザ_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            beforValue = gridView_出勤機ユーザ.CurrentCell.Value.ToString();
            isEditing = true;

            //IME無効(半角英数のみ)
            gridView_出勤機ユーザ.ImeMode = ImeMode.Disable;
        }

        /// <summary>
        /// セル値変更
        /// </summary>
        private void gridView_出勤機ユーザ_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (isEditing && gridView_出勤機ユーザ.CurrentCell.EditedFormattedValue.ToString() != beforValue)
            {
                //
                TextBox_TextValueChanged();
            }
        }

        /// <summary>
        /// セル値編集終了
        /// </summary>
        private void gridView_出勤機ユーザ_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            isEditing = false;
        }

        /// <summary>
        /// データがおかしい
        /// </summary>
        private void gridView_出勤機ユーザ_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (gridView_出勤機ユーザ.Rows[e.RowIndex].IsNewRow)
                return;
        }

        /// <summary>
        /// 重複データチェック
        /// </summary>
        private bool CheckSanmeData(string code, bool isStudent = false)
        {
            //2020/04/16 liuxiaoyan dd start
            string colName = isStudent ? "学生コード" : "";
            //dd end
            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库
            try
            {
                sqlcon.Open();
            }
            catch
            {
                ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "DBサーバーの接続に失敗しました。";
                ((Form1)(this.Tag)).reLoad = false;
                return true;
            }

            string str_sqlcmd = "SELECT 学生コード FROM HL_JINJI_出勤機_登録ユーザマスタ ";

            if(isStudent)
            {
                str_sqlcmd += " WHERE 学生コード = '{0}'";
            }

            str_sqlcmd = string.Format(str_sqlcmd, code);

            SqlCommand com = new SqlCommand(str_sqlcmd, sqlcon);
            SqlDataReader reader = com.ExecuteReader();

            if (reader.Read())
            {
                ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = string.Format("[{0}]({1})が既に登録されています。", colName, code);
                ((Form1)(this.Tag)).reLoad = false;
                return true;
            }

            if (isStudent)
            {
                if (!reader.Read())
                {
                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = string.Format("[{0}]({1})が登録されていません。[出勤機ユーザ登録]画面で登録してください。", colName, code);
                    ((Form1)(this.Tag)).reLoad = false;
                    return true;
                }
            }

            reader.Close();

            if (sqlcon != null)
            {
                sqlcon.Close();
            }

            return false;
        }

        /// <summary>
        /// datagridviewサイズ変更
        /// </summary>
        private void gridView_出勤機ユーザ_SizeChanged(object sender, EventArgs e)
        {
            this.gridView_出勤機ユーザ.Width = 750;
        }

        /// <summary>
        /// 学生コード、登録コードによってdatagridviewソート
        /// </summary>
        private void gridView_出勤機ユーザ_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (e.Column.Name == "学生コード" || e.Column.Name == "登録コード") 
            {
                e.SortResult = (Convert.ToDouble(e.CellValue1) - Convert.ToDouble(e.CellValue2) > 0) ? 1 : (Convert.ToDouble(e.CellValue1) - Convert.ToDouble(e.CellValue2) < 0) ? -1 : 0;

                e.Handled = true;
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            //DGV再表示
            DisplayGridView();
            //MSG削除
            ((Form1)(Tag)).toolStripStatusLabel2.Text = "";
        }
    }
}
