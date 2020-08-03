using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace HL_塾管理
{
    public partial class 学生選択 : DockContent
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        private AutoSizeFormClass asc = new AutoSizeFormClass();

        private string connectionString = ComClass.connectionString;
        public Dictionary<string, string> list_学生 = new Dictionary<string, string>();
        public bool isFirst = true;
        public string code_クラスコード = "";
        public string Selected教師コード = "";
        public string Login教師コード = "";
        public string user = "";
        public string クラス名 = "";
        public string 研修フラグ = "";

        public 学生選択()
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 学生選択_Load(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);

            //一覧表示
            DisplayGridView();

            lbl_クラス名.Text = "クラス名: " + クラス名;
        }

        private void dtp_date_ValueChanged(object sender, EventArgs e)
        {
            DisplayGridView();
        }

        /// <summary>
        /// 検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisplayGridView()
        {
            gridView_学生Info.Rows.Clear();

            //画面値を取得
            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库

            try
            {
                sqlcon.Open();
            }
            catch
            {
                toolStripStatusLabel1.ForeColor = Color.Red;
                toolStripStatusLabel1.Text = "DBサーバーの接続に失敗しました.";
                return;
            }

            string sqlcmd = "";

            if (user != "一般ユーザ" || Login教師コード==Selected教師コード)
            {
                sqlcmd = string.Format(@"Select "
                + "　T1.学生コード "
                + "　, T1.名前 "
                + "　, T1.入塾日 "
                + " From "
                + " HL_JUKUKANRI_学生マスタ T1"
                + " Left Join HL_JUKUKANRI_学生クラス T2 on T1.学生コード = T2.学生コード"
                + " Left Join HL_JUKUKANRI_学生情報 T3 on T1.学生コード = T3.学生コード"
                + " Where (T2.クラスコード is null or T2.クラスコード = {0}) and (T1.離塾原因 is NULL or T1.離塾原因 = 1) and T3.研修フラグ = '{1}'"
                + " Order by T1.学生コード", code_クラスコード, 研修フラグ);
            }
            else
            {             
                if (Login教師コード != Selected教師コード) 
                {
                    sqlcmd = string.Format(@"Select "
                            + "　T1.学生コード "
                            + "　, T1.名前 "
                            + "　, T1.入塾日 "
                            + " From "
                            + " HL_JUKUKANRI_学生マスタ T1"
                            + " Left Join HL_JUKUKANRI_学生クラス T2 on T1.学生コード = T2.学生コード"
                            + " Left Join HL_JUKUKANRI_学生情報 T3 on T1.学生コード = T3.学生コード"
                            + " Where  T2.クラスコード = {0} and (T1.離塾原因 is NULL or T1.離塾原因 = 1) and T3.研修フラグ = '{1}'"
                            + " Order by T1.学生コード", code_クラスコード, 研修フラグ);

                    this.gridView_学生Info.Columns["選択"].Visible = false;
                }
            }

            SqlCommand com = new SqlCommand(sqlcmd, sqlcon);
            SqlDataReader reader = com.ExecuteReader();

            int Index = 0;
            gridView_学生Info.Rows.Clear();

            try
            {
                //一覧情報設定
                while (reader.Read())
                {
                    this.gridView_学生Info.Rows.Add();
                    this.gridView_学生Info.Rows[Index].Cells["学生コード"].Value = reader["学生コード"].ToString();
                    if (isFirst && list_学生.Count > 0 && list_学生.ContainsKey(reader["学生コード"].ToString()))
                    //if (code_クラスコード == reader["クラスコード"].ToString())
                    {
                        this.gridView_学生Info.Rows[Index].Cells["選択"].Value = "True";
                        this.gridView_学生Info.Rows[Index].DefaultCellStyle.BackColor = Color.Yellow;
                    }
                    else
                    {
                        this.gridView_学生Info.Rows[Index].Cells["選択"].Value = "False";
                    }
                    this.gridView_学生Info.Rows[Index].Cells["名前"].Value = reader["名前"].ToString();
                    this.gridView_学生Info.Rows[Index].Cells["入塾日"].Value = ((DateTime)reader["入塾日"]).ToString("yyyy-MM-dd");
                    Index++;
                }

                isFirst = false;
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.ForeColor = Color.Red;
                toolStripStatusLabel1.Text = "検索処理に失敗しました." + ex.Message;
            }
            finally
            {
                if (sqlcon != null)
                {
                    sqlcon.Close();
                    reader.Close();
                }
            }

            //件数表示
            this.toolStripStatusLabel1.ForeColor = Color.Black;
            this.toolStripStatusLabel1.Text = string.Format("{0}件", Index);
        }

        /// <summary>
        /// 当画面変更された時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 学生選択_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form1)(this.Tag)).m_学生選択Handle = IntPtr.Zero;
        }

        private void gridView_学生Info_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (gridView_学生Info.Rows[e.RowIndex].IsNewRow)
                return;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (((Form1)(this.Tag)).m_クラス登録Handle != null)
            {
                SendMessage(((Form1)(this.Tag)).m_クラス登録Handle, Form1.CUSTOM_MESSAGE, IntPtr.Zero, IntPtr.Zero);
            }
            if (((Form1)(this.Tag)).m_クラス変更Handle != null)
            {
                SendMessage(((Form1)(this.Tag)).m_クラス変更Handle, Form1.CUSTOM_MESSAGE, IntPtr.Zero, IntPtr.Zero);
            }

            this.Close();
        }

        private void gridView_学生Info_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            //"CheckBox"列ならば
            if (!isFirst && dgv.Columns[e.ColumnIndex].Name == "選択")
            {
                string code = this.gridView_学生Info.CurrentRow.Cells["学生コード"].Value.ToString();

                if (this.gridView_学生Info.CurrentRow.Cells["選択"].Value.ToString() == "True")
                {
                    if (!list_学生.ContainsKey(code))
                    {
                        string name = this.gridView_学生Info.CurrentRow.Cells["名前"].Value.ToString();

                        list_学生.Add(code, name);
                    }
                }
                else
                {
                    if (list_学生.ContainsKey(code))
                    {
                        list_学生.Remove(code);
                    }
                }
            }
        }

        private void gridView_学生Info_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gridView_学生Info.Columns[e.ColumnIndex].Name == "選択")
            {
                bool Ischecked = (bool)gridView_学生Info.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue;

                if (Ischecked)
                {
                    gridView_学生Info.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;
                }
                else
                {
                    gridView_学生Info.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                }
            }
        }
    }
}
