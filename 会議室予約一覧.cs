using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace HL_塾管理
{
    public partial class 会議室予約一覧 : DockContent
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int BringWindowToTop(IntPtr hWnd);

        private AutoSizeFormClass asc = new AutoSizeFormClass();

        //データベース接続情報
        private string connectionString = ComClass.connectionString;

        //会議室名リスト
        private DataTable roomNameList = new DataTable();

        //編集フラグ
        private bool isEditing = false;

        //変更前のセル値
        private string beforValue = "";

        //更新rowID
        private string updataID = string.Empty;

        public 会議室予約一覧()
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
        private void 会議室予約一覧_Load(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);

            //会議室名リスト表示
            roomNameList = SetRoomNameList();

            if (roomNameList == null)
            {
                ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "DBサーバーの接続に失敗しました。";
                ((Form1)(this.Tag)).reLoad = false;
                return;
            }

            //日付設定
            dtp_date_start.Value = DateTime.Now.AddDays(-5);

            dtp_date_end.Value = DateTime.Now.AddDays(25);



            //一覧表示
            DisplayGridView();
        }

        /// <summary>
        /// 会議室名リスト取得と設定
        /// </summary>
        private DataTable SetRoomNameList()
        {
            //会議室名リスト取得と設定
            this.cmb_roomName.Items.Clear();
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
                return null;
            }

            string str_sqlcmd = @"SELECT  会議室名, 定員数  FROM HL_ALL_会議室マスタ";
            DataTable dt = new DataTable();

            try
            {
                SqlDataAdapter sqlDa = new SqlDataAdapter(str_sqlcmd, sqlcon);
                sqlDa.Fill(dt);

                dt.Rows.Add("ALL", 0);
                dt.DefaultView.Sort = "会議室名";
                dt = dt.DefaultView.ToTable();

                cmb_roomName.DisplayMember = "会議室名";
                cmb_roomName.ValueMember = "定員数";
                cmb_roomName.DataSource = dt;

                if (dt.Rows.Count > 0)
                {
                    cmb_roomName.SelectedIndex = 0;
                }
            }
            catch
            {
                ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "会議室名取得処理に失敗しました.";
                ((Form1)(this.Tag)).reLoad = false;
            }
            finally
            {
                if (sqlcon != null)
                {
                    sqlcon.Close();
                }
            }

            return dt;
        }

        private void cmb_roomName_TextChanged(object sender, EventArgs e)
        {
            //一覧表示
            DisplayGridView();
        }

        private void dtp_date_ValueChanged(object sender, EventArgs e)
        {
            //一覧表示
            DisplayGridView();
        }

        /// <summary>
        /// 終了日付変更
        /// </summary>
        private void dtp_date_end_ValueChanged(object sender, EventArgs e)
        {
            if (dtp_date_start.Value.Date > dtp_date_end.Value.Date)
            {
                ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "日付(To)に日付(From)以後の日を入力してください.";
                ((Form1)(this.Tag)).reLoad = false;
                dtp_date_end.Value = dtp_date_start.Value.Date;
                return;               
            }
            //一覧表示
            DisplayGridView();
        }

        /// <summary>
        /// 検索処理
        /// </summary>
        private void DisplayGridView()
        {
            roomView.Rows.Clear();

            //画面値を取得
            string roomName = cmb_roomName.Text;
            DateTime date_start = dtp_date_start.Value;
            DateTime date_end = dtp_date_end.Value;
            DateTime now_time = DateTime.Now;
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

            string str_sqlcmd = "Select R.ID, R.会議室名, R.参加人数, U.氏名 As '予約者',(CONVERT(varchar(10), R.開始時間, 23) )As '日付',  left(CONVERT(varchar(10), R.開始時間, 24) , 5) As 開始時間 , left(CONVERT(varchar(10), R.終了時間, 24) , 5)  As 終了時間, R.タイトル, R.参加者, R.備考, R.予約者 As 予約者_mail  From HL_ALL_会議室予約情報 As R";
            str_sqlcmd += " Left join HL_EIGYO_ユーザ As U On R.予約者 = U.ユーザ ";

            str_sqlcmd += " Where ";
            if (roomName != "ALL")
            {
                str_sqlcmd += string.Format(" 会議室名 = N'{0}' And ", roomName);
            }
            str_sqlcmd += string.Format("CONVERT(varchar(10), 開始時間, 23) >= '{0}' And CONVERT(varchar(10), 終了時間, 23) <= '{1}' Order by '日付' DESC, '開始時間' DESC,  '終了時間', '会議室名' ", date_start.ToString("yyyy-MM-dd"), date_end.ToString("yyyy-MM-dd"));

            SqlDataAdapter sqlDa = new SqlDataAdapter(str_sqlcmd, sqlcon);
            DataTable dt = new DataTable();
            sqlDa.Fill(dt);



            try
            {
                if (dt.Rows.Count > 0)
                {
                    //データ表示
                    int index = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        string gridViewRoomName = "";
                        this.roomView.Rows.Add();
                        this.roomView.Rows[index].Cells["ID"].Value = row["ID"].ToString();
                        ((DataGridViewComboBoxCell)(this.roomView.Rows[index].Cells["会議室名"])).DisplayMember = "会議室名";
                        ((DataGridViewComboBoxCell)(this.roomView.Rows[index].Cells["会議室名"])).ValueMember = "定員数";

                        if (roomNameList.Rows.Count > 0)
                        {
                            DataTable roomNameGrid = roomNameList.AsEnumerable().Where(r => r["会議室名"].ToString() != "ALL").CopyToDataTable();
                            ((DataGridViewComboBoxCell)(this.roomView.Rows[index].Cells["会議室名"])).DataSource = roomNameGrid;
                            gridViewRoomName = roomName == "ALL" ? row["会議室名"].ToString() : roomName;
                            this.roomView.Rows[index].Cells["会議室名"].Value = gridViewRoomName;
                        }

                        List<DataRow> row1 = roomNameList.AsEnumerable().Where(r => r["会議室名"].ToString() == gridViewRoomName).ToList();
                        if (row1.Count > 0)
                        {
                            int maxCount = Convert.ToInt32(row1[0]["定員数"]);
                            DataTable countDt = new DataTable();
                            countDt.Columns.Add("定員数", typeof(string));
                            countDt.Columns.Add("Count", typeof(string));
                            for (int i = 1; i <= maxCount; i++)
                            {
                                countDt.Rows.Add(i.ToString(), i.ToString());
                            }
                            ((DataGridViewComboBoxCell)(this.roomView.Rows[index].Cells["参加人数"])).DisplayMember = "定員数";
                            ((DataGridViewComboBoxCell)(this.roomView.Rows[index].Cells["参加人数"])).ValueMember = "Count";
                            ((DataGridViewComboBoxCell)(this.roomView.Rows[index].Cells["参加人数"])).DataSource = countDt;
                        }

                        //datagridview値取得
                        this.roomView.Rows[index].Cells["参加人数"].Value = row["参加人数"].ToString();
                        this.roomView.Rows[index].Cells["予約者"].Value = row["予約者"].ToString();
                        this.roomView.Rows[index].Cells["日付"].Value = row["日付"].ToString();
                        this.roomView.Rows[index].Cells["開始時間"].Value = row["開始時間"].ToString();
                        this.roomView.Rows[index].Cells["終了時間"].Value = row["終了時間"].ToString();
                        this.roomView.Rows[index].Cells["タイトル"].Value = row["タイトル"].ToString();
                        this.roomView.Rows[index].Cells["参加者"].Value = row["参加者"].ToString();
                        this.roomView.Rows[index].Cells["備考"].Value = row["備考"].ToString();
                        this.roomView.Rows[index].Cells["予約者_mail"].Value = row["予約者_mail"].ToString();

                        //予約時間により行の背景色再描画
                        DateTime time_開始時間 = DateTime.Parse(row["日付"].ToString() +" " + row["開始時間"].ToString());
                        DateTime time_終了時間 = DateTime.Parse(row["日付"].ToString() + " " + row["終了時間"].ToString());
                        DateTime timetoday = DateTime.Now;
                        string timetoday1 = timetoday.ToString("yyyy-MM-dd");
                        string hiduke = row["日付"].ToString();


                        if (time_終了時間 < now_time)
                        {
                            //End
                            this.roomView.Rows[index].DefaultCellStyle.BackColor = Color.Gray;
                            this.roomView.Rows[index].DefaultCellStyle.ForeColor = Color.White;
                        }
                        else if (hiduke == timetoday1)
                        {
                            this.roomView.Rows[index].DefaultCellStyle.BackColor = Color.Yellow;
                            this.roomView.Rows[index].DefaultCellStyle.ForeColor = Color.Red;
                        }
                        else if(time_開始時間 <= now_time && now_time <= time_終了時間)
                        {
                            //Beginning
                            this.roomView.Rows[index].DefaultCellStyle.BackColor = Color.LemonChiffon;
                            this.roomView.Rows[index].DefaultCellStyle.ForeColor = Color.Red;
                        }
                        else
                        {
                            //Future
                            this.roomView.Rows[index].DefaultCellStyle.BackColor = Color.MediumAquamarine;
                            this.roomView.Rows[index].DefaultCellStyle.ForeColor = Color.DarkBlue;
                        }

                        index++;
                    }
                }
                else
                {
                    //datagridview行クリア
                    this.roomView.Rows.Clear();
                }
            }
            catch
            {
                ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "検索処理に失敗しました.";
                ((Form1)(this.Tag)).reLoad = false;
                return;
            }
            finally
            {
                if (sqlcon != null)
                {
                    sqlcon.Close();
                }
            }

            //検索件数表示
            this.toolStripStatusLabel1_count.Text = string.Format("{0}件", dt.Rows.Count);
        }

        /// <summary>
        /// 当画面閉じられた時の処理
        /// </summary>
        private void 会議室予約一覧_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form1)(this.Tag)).m_会議室予約一覧Handle = IntPtr.Zero;
        }

        /// <summary>
        /// 新規画面へ遷移する
        /// </summary>
        private void 新規ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((Form1)(this.Tag)).m_会議室予約登録Handle != IntPtr.Zero)
            {
                BringWindowToTop(((Form1)(this.Tag)).m_会議室予約登録Handle);
                return;
            }

            会議室予約 m_NewForm_会議室予約登録 = new 会議室予約();
            m_NewForm_会議室予約登録.row = this.roomView.CurrentRow;
            m_NewForm_会議室予約登録.Tag = ((Form1)(this.Tag));
            m_NewForm_会議室予約登録.Show(((Form1)(this.Tag)).dockPanel1);
            ((Form1)(this.Tag)).m_会議室予約登録Handle = m_NewForm_会議室予約登録.Handle;
        }

        /// <summary>
        /// 変更画面へ遷移する
        /// </summary>
        private void 変更ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int value;

            //会議ID取得
            updataID = this.roomView.CurrentRow.Cells["ID"].Value.ToString();
            if (((Form1)(this.Tag)).codeDic.ContainsKey(updataID))
            {
                ((Form1)(this.Tag)).codeDic.TryGetValue(updataID, out value);
                BringWindowToTop((IntPtr)value);
                return;
            }

            会議室予約 m_NewForm_会議室予約変更 = new 会議室予約();
            m_NewForm_会議室予約変更.row = this.roomView.CurrentRow;
            m_NewForm_会議室予約変更.isUpdate = true;
            m_NewForm_会議室予約変更.Tag = ((Form1)(this.Tag));
            m_NewForm_会議室予約変更.Show(((Form1)(this.Tag)).dockPanel1);
            ((Form1)(this.Tag)).m_会議室予約変更Handle = m_NewForm_会議室予約変更.Handle;

            int ptr = (int)((Form1)(this.Tag)).m_会議室予約変更Handle;
            if (!((Form1)(this.Tag)).codeDic.ContainsKey(updataID))
            {
                ((Form1)(this.Tag)).codeDic.Add(updataID, ptr);
            }
        }

        /// <summary>
        ///  一覧から選択行を削除処理
        /// </summary>
        private void 削除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.roomView.CurrentCell != null)
            {
                this.roomView.CurrentCell.Selected = true;
                //選択される行の値取得
                string ID = this.roomView.CurrentRow.Cells["ID"].Value.ToString();
                string user = this.roomView.CurrentRow.Cells["予約者_mail"].Value.ToString();

                //予約者ではない場合
                if (user != ((Form1)(this.Tag)).m_ユーザ登録.m_ログイン_ユーザ)
                {
                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "該当行を削除する権限がありません.該当の予約者を連絡してください.";
                    ((Form1)(this.Tag)).reLoad = false;
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
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "DBサーバーの接続に失敗しました。";
                    ((Form1)(this.Tag)).reLoad = false;
                    return;
                }

                SqlCommand sqlcom = new SqlCommand();
                sqlcom.Connection = sqlcon;

                try
                {
                    //登録行う
                    sqlcom.CommandText = string.Format(@"Delete From HL_ALL_会議室予約情報 Where ID = '{0}'", ID);


                    result = sqlcom.ExecuteNonQuery();

                    if (result == 1)
                    {
                        ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Green;
                        ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "予約が正常に削除されました.";
                        ((Form1)(this.Tag)).reLoad = false;
                        sqlcon.Close();
                    }
                }
                catch
                {
                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "予約の削除処理が失敗しました.";
                    ((Form1)(this.Tag)).reLoad = false;
                    return;
                }
                finally
                {
                    if (sqlcon != null)
                    {
                        sqlcon.Close();
                    }
                }

                //選択される行に移す
                this.roomView.Rows.Remove(this.roomView.CurrentRow);
            }
        }

        /// <summary>
        /// datagridview編集する時、セルタイプ取得
        /// </summary>
        private void roomView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
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
        /// 検索textboxのTextValueChange処理
        /// </summary>
        private void TextBox_TextValueChanged()
        {
            DataGridViewRow updateRow = this.roomView.CurrentRow;

            if (updateRow != null)
            {
                string ID = "";
                string columnName = "";
                object[] cellValue = null;

                //画面値取得
                ID = updateRow.Cells[0].Value.ToString();
                columnName = roomView.Columns[roomView.CurrentCell.ColumnIndex].Name;
                cellValue = new string[] { roomView.CurrentCell.EditedFormattedValue.ToString() };

                if (roomView.CurrentCell.ColumnIndex == 4 || roomView.CurrentCell.ColumnIndex == 5 || roomView.CurrentCell.ColumnIndex == 6)
                {
                    try
                    {
                        //日付形式チェック
                        DateTime.Parse(cellValue[0].ToString());
                    }
                    catch
                    {
                        ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                        ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "正しい日付・時間の形式を入力してください.";
                        ((Form1)(this.Tag)).reLoad = false;
                        DisplayGridView();
                        return;
                    }
                }
                //日付の場合
                if (columnName == roomView.Columns[4].Name)
                {
                    cellValue = new object[]
                    {
                    DateTime.Parse( roomView.CurrentCell.EditedFormattedValue.ToString() + " " + roomView.CurrentCell.OwningRow.Cells[5].Value),
                    DateTime.Parse( roomView.CurrentCell.EditedFormattedValue.ToString() + " " + roomView.CurrentCell.OwningRow.Cells[6].Value)
                    };

                }
                //開始日、終了日の場合
                else if (columnName == roomView.Columns[5].Name || columnName == roomView.Columns[6].Name)
                {
                    cellValue = new object[] { DateTime.Parse(roomView.CurrentCell.OwningRow.Cells[4].Value + " " + roomView.CurrentCell.EditedFormattedValue) };
                }

                if (Update_GridViewRow(ID, roomView.Columns[roomView.CurrentCell.ColumnIndex].Name, cellValue))
                {
                    //画面再表示
                    if (columnName == roomView.Columns[4].Name)
                    {
                        string date_edited = roomView.CurrentCell.EditedFormattedValue.ToString();
                        dtp_date_start.Text = date_edited;
                    }
                    else
                    {
                        //datagridview再表示
                        DisplayGridView();
                    }
                    //変更した行が選択状態に設定
                    SetSelectedRow(ID);
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
            try
            {
                DataGridViewRow updateRow = this.roomView.CurrentRow;
                string ID = updateRow.Cells[0].Value.ToString();
                object[] cellValue = null;
                bool countChanged = false;
                //Comboxが会議室名の場合

                if (combox.SelectedItem != null)
                {
                    DataRowView row = (DataRowView)combox.SelectedItem;
                    cellValue = new object[] { row.Row.ItemArray[0].ToString() };

                    if (combox.DisplayMember == roomView.Columns[1].Name)
                    {
                        int count = Convert.ToInt16(row.Row.ItemArray[1]);

                        int before_count = Convert.ToInt16(updateRow.Cells["参加人数"].FormattedValue);

                        if (before_count > count)
                        {
                            ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Green;
                            ((Form1)(this.Tag)).toolStripStatusLabel2.Text = string.Format("会議室の定員数により参加人数が{0}から{1}に変更されます.", before_count, count);
                            ((Form1)(this.Tag)).reLoad = false;
                            cellValue = new object[] { row.Row.ItemArray[0].ToString(), count.ToString() };
                            countChanged = true;
                        }
                    }
                    //エラー発生させないようにif条件
                    if (roomView.CurrentCell.FormattedValue.ToString() != roomView.CurrentCell.EditedFormattedValue.ToString() &&
                        !string.IsNullOrWhiteSpace(roomView.CurrentCell.EditedFormattedValue.ToString()) &&
                        !roomView.CurrentCell.EditedFormattedValue.ToString().Contains("System.Data.DataRowView"))
                    {
                        if (Update_GridViewRow(ID, roomView.Columns[roomView.CurrentCell.ColumnIndex].Name, cellValue, countChanged))
                        {
                            if (updateRow.Cells[1].Selected || updateRow.Cells[1].IsInEditMode)
                            {
                                string RoomName = updateRow.Cells[1].EditedFormattedValue.ToString();
                                cmb_roomName.Text = RoomName;
                            }
                            else
                            {
                                //datagridview再表示
                                DisplayGridView();
                            }
                            //変更した行が選択状態に設定
                            SetSelectedRow(ID);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "予約の更新処理が失敗しました." + ex;
                ((Form1)(this.Tag)).reLoad = false;
                return;
            }
        }

        /// <summary>
        /// イベント終了、削除する
        /// </summary>
        public void combox_Leave(object sender, EventArgs e)
        {
            ComboBox combox = sender as ComboBox;
            //イベント終了、削除する
            combox.SelectedIndexChanged -= new EventHandler(ComboBox_SelectedIndexChanged);
        }

        /// <summary>
        /// 右クリックで一行を選択
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
            else
            {
                DataGridViewSelectionMode oldmode = roomView.SelectionMode;
                roomView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                roomView.ClearSelection();
                roomView.SelectionMode = oldmode;
                this.roomView.CurrentCell = this.roomView.Rows[e.RowIndex].Cells[e.ColumnIndex];
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
        /// 更新処理
        /// </summary>
        private bool Update_GridViewRow(string ID, string columnName, object[] cellValue, bool countChanged = false)
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

            //データチェック
            string errmsg = "";
            switch(columnName)
            {
                case "タイトル":
                    if(string.IsNullOrWhiteSpace(cellValue[0].ToString()))
                    {
                        errmsg = "タイトルを入力してください.";
                    }
                    break;
                case "日付":
                    if(DateTime.Now.Date > DateTime.Parse(roomView.CurrentRow.Cells[4].Value.ToString()))
                    {
                        errmsg = "過去日は設定できません.";
                    }
                    break;
                case "開始時間":
                case "終了時間":
                    DateTime startTime = DateTime.Parse(roomView.CurrentRow.Cells[5].Value.ToString());
                    DateTime endTime = DateTime.Parse(roomView.CurrentRow.Cells[6].Value.ToString());
                    DateTime endDateTime = DateTime.Parse(roomView.CurrentRow.Cells[4].Value.ToString() + " " + roomView.CurrentRow.Cells[6].Value.ToString());
                    if (startTime >= endTime)
                    {
                        errmsg = "予約時間の終了時間は開始時間以後に設定してください.";
                    }
                    else if(endDateTime < DateTime.Now)
                    {
                        errmsg = "予約時間は過去の時間に設定できません.";
                    }
                    else
                    {
                        //予約時間最大二時間制限
                        System.TimeSpan TS = new System.TimeSpan(endTime.Ticks - startTime.Ticks);
                        if (TS.TotalSeconds / 3600 > 2)
                        {
                            errmsg = "会議室の最大予約時間が2時間です。";
                        }
                    }
                    break;
            }

            if (errmsg == "")
            {
                errmsg = dateTimeCheck();
            }

            if (errmsg != "")
            {
                ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = errmsg;
                ((Form1)(this.Tag)).reLoad = false;
                return false;
            }

            try
            {
                //更新行う
                string sql = "Update HL_ALL_会議室予約情報 Set ";
                
                if(columnName == roomView.Columns[2].Name)
                {
                    sql +=  string.Format(columnName + " = {0} ", cellValue[0]);
                }
                else if (countChanged)
                {
                   sql += string.Format(columnName + "= N'{0}', 参加人数 = {1} ", cellValue[0], cellValue[1]);
                }
                else if (columnName == roomView.Columns[4].Name)
                {
                    sql += string.Format("開始時間 = '{0}', 終了時間 = '{1}'", cellValue[0], cellValue[1]);                  
                }
                else
                {
                    sql += string.Format(columnName + " = N'{0}' ", cellValue[0]);
                }
                sql += ", 更新日時 = GetDate()";
                sql += " Where ID = '{0}'";

                sqlcom.CommandText = string.Format(sql, ID);

                result = sqlcom.ExecuteNonQuery();

                if (result != 1)
                {
                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "予約の更新処理が失敗しました.";
                    ((Form1)(this.Tag)).reLoad = false;
                }
                else
                {
                    isUpdate = true;
                }
            }
            catch
            {
                ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "予約の更新処理が失敗しました." ;
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
        /// 重複会議登録チェック
        /// </summary>
        private string dateTimeCheck()
        {
            string errmsg = "";
            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库

            try
            {
                sqlcon.Open();
            }
            catch
            {
                errmsg = "DBサーバーの接続に失敗しました。";
                return errmsg;
            }

            try
            {
                string ID = roomView.CurrentRow.Cells[0].Value.ToString();
                string roomName = roomView.CurrentRow.Cells[1].EditedFormattedValue.ToString();
                DateTime startDateTime = DateTime.Parse(roomView.CurrentRow.Cells[4].Value.ToString() + " " + roomView.CurrentRow.Cells[5].Value.ToString());
                DateTime endDateTime = DateTime.Parse(roomView.CurrentRow.Cells[4].Value.ToString() + " " + roomView.CurrentRow.Cells[6].Value.ToString());

                string sqlcmd = string.Format(@"Select 会議室名  From HL_ALL_会議室予約情報 Where 会議室名 = '{0}'", roomName);

                sqlcmd += string.Format("And ID <> '{0}'", ID);

                sqlcmd += string.Format(@" And ((開始時間 <= '{0}'  And  '{0}' <= 終了時間) Or (開始時間 <= '{1}'  And  '{1}' <= 終了時間)  Or ( '{0}' < 開始時間 And 開始時間 < '{1}'))",
                                                      startDateTime, endDateTime);

                SqlCommand sqlcom = new SqlCommand(sqlcmd, sqlcon);

                SqlDataReader reader = sqlcom.ExecuteReader();

                if (reader.HasRows)
                {
                    errmsg = "予約時間に他の予約が既に登録されている。";
                }
            }
            catch
            {
                errmsg = "予約時間の重複チェック処理に失敗しました。";
            }
            finally
            {
                if (sqlcon != null)
                {
                    sqlcon.Close();
                }
            }

            return errmsg;
        }

        /// <summary>
        ///IDによって行の選択状態設定
        /// </summary>
        private void SetSelectedRow(string ID)
        {
            if (!string.IsNullOrWhiteSpace(ID) && this.roomView.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in roomView.Rows)
                {
                    if (row.Cells[0].Value.ToString() == ID)
                    {
                        this.roomView.Rows[row.Index].Selected = true;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// セル編集開始
        /// </summary>
        private void roomView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex > 2)
            {
                beforValue = roomView.CurrentCell.Value.ToString();
                isEditing = true;
            }

            switch (e.ColumnIndex)
            {
                case 4:
                case 5:
                case 6:
                    //この列はIME無効(半角英数のみ)
                    roomView.ImeMode = ImeMode.Disable;
                    break;
                default:
                    roomView.ImeMode = ImeMode.On;
                    break;
            }
        }


        /// <summary>
        /// datagridviewセル値変更
        /// </summary>
        private void roomView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (isEditing && roomView.CurrentCell.EditedFormattedValue.ToString() != beforValue)
            {
                TextBox_TextValueChanged();
            }
        }

        /// <summary>
        ///　セル値編集終了
        /// </summary>
        private void roomView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            isEditing = false;
        }

        /// <summary>
        /// データがおかしい時return
        /// </summary>
        private void roomView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (roomView.Rows[e.RowIndex].IsNewRow)
                return;
        }
    }
}
