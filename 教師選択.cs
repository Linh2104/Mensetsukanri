using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace HL_塾管理
{
    public partial class 教師選択 : DockContent
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        private AutoSizeFormClass asc = new AutoSizeFormClass();

        //DB接続情報
        private string connectionString = ComClass.connectionString;
        //教師コード
        public string code_教師コード = "";

        public 教師選択()
        {
            InitializeComponent();
        }

        /// <summary>
        /// データが変更することがあれば、DGV再表示
        /// </summary>
        /// <param name="msg"></param>
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
        private void 教師選択_Load(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);

            //一覧表示
            DisplayGridView();
        }

        /// <summary>
        /// カレンダーに日付を変更する処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            gridView_教師Info.Rows.Clear();

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
            string sqlcmd = @"Select "
                + "　教師コード "
                + "　, 名前 "
                + "　, 所属会社 "
                + "　, メイン言語 "
                + "　, 開始日 "
                + "　, 終了日 "
                + " From "
                + " HL_JUKUKANRI_教師情報 ";

            SqlCommand com = new SqlCommand(sqlcmd, sqlcon);
            SqlDataReader reader = com.ExecuteReader();

            int Index = 0;
            gridView_教師Info.Rows.Clear();

            try
            {
                //一覧情報設定
                while (reader.Read())
                {
                    this.gridView_教師Info.Rows.Add();
                    this.gridView_教師Info.Rows[Index].Cells["教師コード"].Value = reader["教師コード"].ToString();
                    this.gridView_教師Info.Rows[Index].Cells["名前"].Value = reader["名前"].ToString();
                    this.gridView_教師Info.Rows[Index].Cells["課程"].Value = reader["メイン言語"].ToString();
                    this.gridView_教師Info.Rows[Index].Cells["期間"].Value = ((DateTime)reader["開始日"]).ToString("yyyy-MM-dd") + " ～ " + (string.IsNullOrWhiteSpace(reader["終了日"].ToString()) ? "" :((DateTime)reader["終了日"]).ToString("yyyy-MM-dd"));
                    Index++;
                }
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

            //件数表示
            this.toolStripStatusLabel1.ForeColor = Color.Black;
            this.toolStripStatusLabel1.Text = string.Format("{0}件", Index);
        }

        /// <summary>
        /// 画面を閉じる処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 教師選択_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Handleが０に初期化する
            ((Form1)(this.Tag)).m_教師選択Handle = IntPtr.Zero;
        }

        /// <summary>
        /// DGVのデータエラーの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView_教師情報_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (gridView_教師Info.Rows[e.RowIndex].IsNewRow)
            {
                return;
            }
        }
        /// <summary>
        /// DGVの「選択」ボタンを押下する処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView_教師Info_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            //"Button"列ならば、ボタンがクリックされた
            if (dgv.Columns[e.ColumnIndex].Name == "選択")
            {
                code_教師コード = this.gridView_教師Info.CurrentRow.Cells["教師コード"].Value.ToString();

                //複数画面を開けないためチェック
                if (((Form1)(this.Tag)).m_教師選択Handle != null)
                {
                    SendMessage(((Form1)(this.Tag)).m_教師選択Handle, Form1.CUSTOM_MESSAGE, IntPtr.Zero, IntPtr.Zero);                    
                }

                //ボタンをクリックしてから、画面閉じる
                this.Close();
            }
        }
    }
}
