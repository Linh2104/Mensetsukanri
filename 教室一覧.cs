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
    public partial class 教室一覧 : DockContent
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int BringWindowToTop(IntPtr hWnd);
        private AutoSizeFormClass asc = new AutoSizeFormClass();

        //DB接続情報
        private string connectionString = ComClass.connectionString;
        //出勤機テーブル
        private DataTable dtmachinenum = new DataTable();
        //教室コード
        private string classroomcode;
        //出勤機コード
        private string machinecode;
        //備考
        private string remark;
        //編集フラッグ
        private bool change = false;
        //ログイン職務
        public string user = "";
        public 教室一覧()
        {
            InitializeComponent();
        }

        /// <summary>
        ///  データが変更することがあれば、DGV再表示
        /// </summary>
        /// <param name="msg"></param>
        protected override void WndProc(ref Message msg)
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
        ///　DGVの出勤機コードのCMB設定
        /// </summary>
        public void SetComboxList()
        {
            SqlConnection sqlcon = new SqlConnection(connectionString);
            try
            {
                sqlcon.Open();
            }
            catch (Exception ex)
            {
                ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "DBサーバーの接続に失敗しました." + ex.Message; ;
                ((Form1)(this.Tag)).reLoad = false;
                return;
            }
            //string str_sqlcmd = @"SELECT 出勤機コード FROM HL_JINJI_出勤機マスタ ORDER BY 出勤機コード";
           
            string str_sqlcmd = "SELECT 出勤機コード, 場所 FROM HL_JINJI_出勤機マスタ ORDER BY 出勤機コード";
             /// shin start 20200721
             ///string str_sqlcmd = "SELECT  場所 FROM HL_JINJI_出勤機マスタ ORDER BY 出勤機コード";
            ///shin end 20200721
            SqlDataAdapter sqlDa = new SqlDataAdapter(str_sqlcmd, sqlcon);
            sqlDa.Fill(dtmachinenum);            
        }

        /// <summary>
        /// 画面Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 教室一覧_Load(object sender, EventArgs e)
        {
            user = ((Form1)(this.Tag)).m_ユーザ登録.m_ログイン_職務;
            if(user=="一般ユーザ")
            {
                変更ToolStripMenuItem.Visible = false;
                削除ToolStripMenuItem.Visible = false;
                ///shin 20200721 start
                gv_classroomsInfo.ReadOnly = true;
                gv_classroomsInfo.Columns["備考"].DefaultCellStyle.BackColor = Color.White;
                gv_classroomsInfo.Columns["備考"].DefaultCellStyle.ForeColor = Color.Black;
                gv_classroomsInfo.Columns["出勤機場所"].DefaultCellStyle.BackColor = Color.White;
                gv_classroomsInfo.Columns["出勤機場所"].DefaultCellStyle.ForeColor = Color.Black;
                gv_classroomsInfo.Columns["出勤機コード"].DefaultCellStyle.BackColor = Color.White;
                gv_classroomsInfo.Columns["出勤機コード"].DefaultCellStyle.ForeColor = Color.Black;
                ///shin 20200721 end
            }
            asc.controlAutoSize(this);

            //DGV再表示
            DisplayGridView();
        }

        /// <summary>
        /// DGVの設定
        /// </summary>
        private void DisplayGridView()
        {
            if (dtmachinenum.Rows.Count == 0)
            {
                SetComboxList();
            }

            gv_classroomsInfo.Rows.Clear();

            if (((Form1)(this.Tag)).reLoad)
            {
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "";
            }

            SqlConnection sqlcon = new SqlConnection(connectionString);
            //情報取得
            try
            {
                sqlcon.Open();
            }
            catch (Exception ex)
            {
                ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "DBサーバーの接続に失敗しました." + ex.Message; ;
                ((Form1)(this.Tag)).reLoad = false;
            }

            //string sqlcmd = "select * from HL_JUKUKANRI_教室マスタ order by 教室コード + 0 ASC";
            string sqlcmd = @"select
	                            a.教室コード,
	                            a.出勤機コード,
	                            b.場所,
	                            a.備考
                            from
	                            HL_JUKUKANRI_教室マスタ a
	                            inner join
		                            HL_JINJI_出勤機マスタ b
	                            on	a.出勤機コード = b.出勤機コード
                            order by
	                            教室コード + 0 ASC";

            SqlCommand com = new SqlCommand(sqlcmd, sqlcon);
            SqlDataReader reader = com.ExecuteReader();

            //件数
            int Index = 0;
            try
            {
                while (reader.Read())
                {
                    if (
                        (reader["教室コード"].ToString().IndexOf(this.txt_search.Text) < 0) &&
                        (reader["出勤機コード"].ToString().IndexOf(this.txt_search.Text) < 0) &&
                        (reader["場所"].ToString().IndexOf(this.txt_search.Text) < 0) &&
                        (reader["備考"].ToString().IndexOf(this.txt_search.Text) < 0)
                        )
                    {
                        continue;
                    }

                    this.gv_classroomsInfo.Rows.Add();
                    this.gv_classroomsInfo.Rows[Index].Cells["教室コード"].Value = reader["教室コード"].ToString().Trim();
                    ///shin start 20200721
                    //((DataGridViewComboBoxCell)(this.gv_classroomsInfo.Rows[Index].Cells["出勤機コード"])).DisplayMember = "出勤機コード";
                    ///shin end 20200721
                    ((DataGridViewComboBoxCell)(this.gv_classroomsInfo.Rows[Index].Cells["出勤機場所"])).DisplayMember = "場所";
                    if (dtmachinenum.Rows.Count > 0)
                    {
                        ///shin start 20200721
                        //((DataGridViewComboBoxCell)(this.gv_classroomsInfo.Rows[Index].Cells["出勤機コード"])).DataSource = dtmachinenum;
                        ///shin end 20200721
                        ((DataGridViewComboBoxCell)(this.gv_classroomsInfo.Rows[Index].Cells["出勤機場所"])).DataSource = dtmachinenum;
                    }
                    ///shin start 20200721
                    //((DataGridViewComboBoxCell)(this.gv_classroomsInfo.Rows[Index].Cells["出勤機コード"])).Value = reader["出勤機コード"].ToString().Trim();                   
                    //this.gv_classroomsInfo.Rows[Index].Cells["出勤機場所"].Value = reader["場所"].ToString().Trim(); 
                    ///shin end 20200721
                    ((DataGridViewComboBoxCell)(this.gv_classroomsInfo.Rows[Index].Cells["出勤機場所"])).Value = reader["場所"].ToString().Trim();
                    this.gv_classroomsInfo.Rows[Index].Cells["出勤機コード"].Value = reader["出勤機コード"].ToString().Trim();
                    this.gv_classroomsInfo.Rows[Index].Cells["備考"].Value = reader["備考"].ToString().Trim();

                    Index++;
                }

                //DGVの広さを設定
                int TotalColumnsWidth = 0;
                foreach (DataGridViewColumn dvgcol in gv_classroomsInfo.Columns)
                {
                    if (dvgcol.Visible == true)
                    {
                        TotalColumnsWidth += dvgcol.Width;
                    }
                }
                gv_classroomsInfo.Width = TotalColumnsWidth + gv_classroomsInfo.RowHeadersWidth + 2;

                //件数設定
                toolStripStatusLabel2.ForeColor = Color.Black;
                toolStripStatusLabel2.Text = string.Format("{0}件", gv_classroomsInfo.Rows.Count.ToString());
                reader.Close();
            }
            catch (Exception ex)
            {
                ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "DBサーバーの接続に失敗しました." + ex.Message; ;
                ((Form1)(this.Tag)).reLoad = false;
            }
            finally
            {
                if (sqlcon != null)
                {
                    sqlcon.Close();
                    sqlcon.Dispose();
                }
            }

            ((Form1)(this.Tag)).reLoad = true;
        }

        /// <summary>
        /// 画面閉じる処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 教室一覧_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Handleが０に初期化する
            ((Form1)(this.Tag)).m_教室一覧Handle = IntPtr.Zero;
        }

        /// <summary>
        /// 右メニューの 変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 変更ToolStripMenuItem_Click(object sender, EventArgs e)
        {           
            //複数画面チェック
            if (((Form1)(this.Tag)).m_教室管理Handle != IntPtr.Zero)
            {
                BringWindowToTop(((Form1)(this.Tag)).m_教室管理Handle);
                return;
            }

            //教室管理画面呼び出す
            this.gv_classroomsInfo.CurrentCell.Selected = true;
            教室管理 m_NewForm_教室変更 = new 教室管理();
            string classroomnum = this.gv_classroomsInfo.CurrentRow.Cells["教室コード"].Value.ToString();
            string machinenum = this.gv_classroomsInfo.CurrentRow.Cells["出勤機コード"].Value.ToString();
            string remark = this.gv_classroomsInfo.CurrentRow.Cells["備考"].Value.ToString();

            //引数を渡す
            m_NewForm_教室変更.isUpdate = "Update";
            m_NewForm_教室変更.classnum = classroomnum;
            m_NewForm_教室変更.machinemun = machinenum;
            m_NewForm_教室変更.remark = remark;
            m_NewForm_教室変更.Tag = ((Form1)(this.Tag));
            ((Form1)(this.Tag)).m_教室管理Handle = m_NewForm_教室変更.Handle;
            m_NewForm_教室変更.ShowDialog();

            //DGV再表示
            DisplayGridView();
        }

        /// <summary>
        /// 右メニューの新規追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void 新規追加ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    //複数画面チェック
        //    if (((Form1)(this.Tag)).m_教室管理Handle != IntPtr.Zero)
        //    {
        //        BringWindowToTop(((Form1)(this.Tag)).m_教室管理Handle);
        //        return;
        //    }

        //    //教室管理画面呼び出す
        //    教室管理 m_NewForm_教室登録 = new 教室管理();
        //    m_NewForm_教室登録.Tag = ((Form1)(this.Tag));
        //    ((Form1)(this.Tag)).m_教室管理Handle = m_NewForm_教室登録.Handle;
        //    m_NewForm_教室登録.ShowDialog();

        //    //DGV再表示
        //    //DisplayGridView();
        //}
        
        /// <summary>
        /// 右メニューの削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 削除ToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            if (this.gv_classroomsInfo.CurrentCell != null)
            {
                this.gv_classroomsInfo.CurrentCell.Selected = true;
                string code = this.gv_classroomsInfo.CurrentRow.Cells["教室コード"].Value.ToString();

                SqlConnection sqlcon = new SqlConnection(connectionString);
                try
                {
                    sqlcon.Open();
                }
                catch (Exception ex)
                {
                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "DBサーバーの接続に失敗しました." + ex.Message; ;
                    ((Form1)(this.Tag)).reLoad = false;
                }
                SqlCommand sqlcom = new SqlCommand();
                sqlcom.Connection = sqlcon;

                try
                {
                    //教室マスタにデータ削除
                    sqlcom.CommandText = string.Format(@"delete from HL_JUKUKANRI_教室マスタ where 教室コード='{0}'", code);

                    int result = sqlcom.ExecuteNonQuery();

                    if (result == 1)
                    {
                        ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Green;
                        ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "正常に削除しました。";
                    }
                }
                catch (Exception ex)
                {
                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "DBサーバーの接続に失敗しました." + ex.Message; ;
                    ((Form1)(this.Tag)).reLoad = false;
                }
                finally
                {
                    if (sqlcon != null)
                    {
                        sqlcon.Close();
                    }
                }

                //件数再表示
                this.gv_classroomsInfo.Rows.Remove(this.gv_classroomsInfo.CurrentRow);
                toolStripStatusLabel2.Text = string.Format("{0}件", gv_classroomsInfo.Rows.Count.ToString());
            }
        }

        /// <summary>
        /// 右メニューを開く処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            //メニューの箇所設定
            System.Drawing.Point startPosition = Cursor.Position;

            System.Drawing.Point point = this.gv_classroomsInfo.PointToClient(startPosition);
            DataGridView.HitTestInfo hitinfo;
            hitinfo = this.gv_classroomsInfo.HitTest(point.X, point.Y);

            this.gv_classroomsInfo.ClearSelection();
            if (hitinfo.RowIndex >= 0)
            {
                this.gv_classroomsInfo.Rows[hitinfo.RowIndex].Selected = true;
            }
            else
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 検索ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_search_Click(object sender, EventArgs e)
        {
            //DGV再表示
            DisplayGridView();
            ((Form1)(Tag)).toolStripStatusLabel2.Text = "";
        }

        /// <summary>
        /// セルをクリックして、行の値を取得の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gv_classroomsInfo_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    if ((this.gv_classroomsInfo.Rows[e.RowIndex].Selected == false))
                    {
                        this.gv_classroomsInfo.ClearSelection();
                        this.gv_classroomsInfo.Rows[e.RowIndex].Selected = true;
                    }

                    if ((this.gv_classroomsInfo.SelectedRows.Count == 1))
                    {
                        this.gv_classroomsInfo.CurrentCell = this.gv_classroomsInfo.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    }
                    contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                }
            }
        }

        private void gv_classroomsInfo_SizeChanged(object sender, EventArgs e)
        {
            //this.gv_classroomsInfo.Width = 525;
        }
        #region

        /// <summary>
        /// Update DGV
        /// </summary>
        /// <param name="code_出勤機コード"></param>
        /// <param name="code_教室コード"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        private bool Update_GridViewRow(string code_出勤機コード, string code_教室コード, string remark)
        {
            bool isUpdate = false;
            SqlConnection sqlcon = new SqlConnection(connectionString);

            int result = 0;
            try
            {
                sqlcon.Open();
            }
            catch (Exception ex)
            {
                ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "DBサーバーの接続に失敗しました." + ex.Message; ;
                ((Form1)(this.Tag)).reLoad = false;
                return isUpdate;
            }
            SqlCommand sqlcom = new SqlCommand();
            sqlcom.Connection = sqlcon;

            if (string.IsNullOrEmpty(code_教室コード))
            {
                ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "[教室コード]が空白を設定できません。";
                ((Form1)(this.Tag)).reLoad = false;
                return false;
            }

            try
            {                
                sqlcom.CommandText = string.Format("UPDATE HL_JUKUKANRI_教室マスタ SET 出勤機コード = '{0}',  備考 = '{1}' WHERE 教室コード = '{2}'", code_出勤機コード, remark, code_教室コード);

                result = sqlcom.ExecuteNonQuery();

                if (result != 1)
                {
                    //DGV再表示
                    DisplayGridView();

                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "対象ユーザの更新処理が失敗しました.";
                    isUpdate = false;
                }
                else
                { 
                    //DGV再表示
                    DisplayGridView();

                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Green;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "正常に更新しました。";
                    //isUpdate = true;                  
                }
            }
            catch (Exception ex)
            {
                ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "対象ユーザの更新処理が失敗しました." + ex.Message;
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

        //private void TextBox_TextValueChanged()
        //{
        //    DataGridViewRow updateRow = this.gv_classroomsInfo.CurrentRow;
        //    if (updateRow != null)
        //    {
        //        string code_教室コード = "";
        //        string columnName = "";

        //        code_教室コード = updateRow.Cells[0].Value.ToString();
        //        columnName = gv_classroomsInfo.Columns[gv_classroomsInfo.CurrentCell.ColumnIndex].Name;

        //        if (Update_GridViewRow(gv_classroomsInfo.Columns[gv_classroomsInfo.CurrentCell.ColumnIndex].Name))
        //        {
        //            DisplayGridView();
        //            SetSelectedRow(code_教室コード);
        //        }
        //        else
        //        {
        //            DisplayGridView();
        //        }
        //    }
        //}

        //private void SetSelectedRow(string code)
        //{
        //    DisplayGridView();
        //    if (!string.IsNullOrWhiteSpace(code) && this.gv_classroomsInfo.Rows.Count > 0)
        //    {
        //        foreach (DataGridViewRow row in gv_classroomsInfo.Rows)
        //        {
        //            if (row.Cells["教室コード"].Value.ToString() == code)
        //            {
        //                this.gv_classroomsInfo.Rows[row.Index].Selected = true;
        //                break;
        //            }
        //        }
        //    }
        //}
        #endregion

        //編集前の情報取得
        private string beforeValue;
        private void gv_classroomsInfo_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            beforeValue = gv_classroomsInfo.CurrentCell.Value.ToString();
            change = true;
        }
        /// <summary>
        /// セルが変更の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gv_classroomsInfo_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //データがない場合
            if (e.RowIndex == -1)
            {
                return;
            }
            if(gv_classroomsInfo.Columns[gv_classroomsInfo.CurrentCell.ColumnIndex].Name!="出勤機場所")
            { 
                //備考が空白である場合
                if (string.IsNullOrWhiteSpace(gv_classroomsInfo.CurrentCell.EditedFormattedValue.ToString()))
                {
                    //DGV再表示
                    DisplayGridView();
                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "備考を未入力です。";
                    ((Form1)(this.Tag)).reLoad = false;
                }
                else
                {
                    if (change && gv_classroomsInfo.CurrentCell.EditedFormattedValue.ToString() != beforeValue)
                    {
                        //情報取得
                        classroomcode = gv_classroomsInfo.CurrentRow.Cells[0].Value.ToString();
                        machinecode = gv_classroomsInfo.CurrentRow.Cells[1].Value.ToString();
                        remark = gv_classroomsInfo.CurrentRow.Cells[3].Value.ToString();                    
                        //DGV更新
                        Update_GridViewRow(machinecode, classroomcode, remark);
                    }
                    return;
                }
            }
        }

 		/// <summary>
        /// CMBからたちさる処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Combobox_Leave(object sender, EventArgs e)
        {
            ComboBox combox = sender as ComboBox;
            combox.SelectedIndexChanged -= new EventHandler(ComboBox_SelectedIndexChanged);
        }

        /// <summary>
        /// セルを編集するためのコントロールが処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gv_classroomsInfo_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
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
        /// CMBの値を選択する処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combox = sender as ComboBox;
            combox.Leave += new EventHandler(Combobox_Leave);

            DataRowView row = (DataRowView)combox.SelectedItem;
            string code_出勤機 = row[0].ToString();
            string code_Class = gv_classroomsInfo.CurrentRow.Cells["教室コード"].EditedFormattedValue.ToString();
            string remark = gv_classroomsInfo.CurrentRow.Cells["備考"].EditedFormattedValue.ToString();
            string basyo= gv_classroomsInfo.CurrentRow.Cells["出勤機場所"].EditedFormattedValue.ToString();
            if (combox.SelectedItem != null && code_出勤機 != beforeValue)
            {              
                Update_GridViewRow(code_出勤機, code_Class, remark);
            }
            else
            {
                //DGV再表示
                DisplayGridView();
            }
        }

        private void gv_classroomsInfo_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            change = false;
        }
    }
}