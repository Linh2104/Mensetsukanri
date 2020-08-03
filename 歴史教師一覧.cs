using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace HL_塾管理
{
    public partial class 歴史教師一覧 : DockContent
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int BringWindowToTop(IntPtr hWnd);
        private AutoSizeFormClass asc = new AutoSizeFormClass();

        private string connectionString = ComClass.connectionString;

        public 歴史教師一覧()
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
        private void 歴史教師一覧_Load(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);

            //一覧表示
            DisplayGridView();
        }

        /// <summary>
        /// 検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisplayGridView()
        {
            gv_view.Rows.Clear();

            //画面値を取得
            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库

            try
            {
                sqlcon.Open();
            }
            catch
            {
                MessageBox.Show("DBサーバーの接続に失敗しました.");

                return;
            }

            string str_sqlcmd = "SELECT [教師コード], [名前], [所属会社], [メイン言語], [開始日], [終了日] FROM[dbo].[HL_JUKUKANRI_教師情報]";

            SqlDataAdapter sqlDa = new SqlDataAdapter(str_sqlcmd, sqlcon);
            DataTable dt = new DataTable();
            sqlDa.Fill(dt);
            try
            {
                //一覧情報設定
                if (dt.Rows.Count > 0)
                {
                    int index = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        this.gv_view.Rows.Add();

                        this.gv_view.Rows[index].Cells["教師コード"].Value = row["教師コード"].ToString();
                        this.gv_view.Rows[index].Cells["名前"].Value = row["名前"].ToString();
                        this.gv_view.Rows[index].Cells["所属会社"].Value = row["所属会社"].ToString();
                        this.gv_view.Rows[index].Cells["言語"].Value = row["メイン言語"].ToString();
                        this.gv_view.Rows[index].Cells["開始日"].Value = string.IsNullOrWhiteSpace(row["開始日"].ToString()) ? "-" : ((DateTime)row["開始日"]).ToString("yyyy-MM-dd");
                        this.gv_view.Rows[index].Cells["終了日"].Value = string.IsNullOrWhiteSpace(row["終了日"].ToString()) ? "-" : ((DateTime)row["終了日"]).ToString("yyyy-MM-dd");

                        index++;
                    }
                }
            }
            catch
            {
                MessageBox.Show("検索処理に失敗しました.");
            }
            finally
            {
                if (sqlcon != null)
                {
                    sqlcon.Close();
                }
            }

            //件数表示
            this.toolStripStatusLabel1_count.Text = string.Format("{0}件", dt.Rows.Count);
        }

        /// <summary>
        /// 当画面変更された時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 歴史教師一覧_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form1)(this.Tag)).m_歴史教師一覧Handle = IntPtr.Zero;
        }

        /// <summary>
        /// 新規画面へ遷移する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 新規ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((Form1)(this.Tag)).m_クラス登録Handle != IntPtr.Zero)
            {
                BringWindowToTop(((Form1)(this.Tag)).m_クラス登録Handle);
                return;
            }
            クラス管理 m_クラス管理 = new クラス管理();
            m_クラス管理.isUpdate = "new";
            m_クラス管理.code_教師コード = gv_view.CurrentRow.Cells["教師コード"].Value.ToString();
            m_クラス管理.Tag = ((Form1)(this.Tag));
            m_クラス管理.Show(((Form1)(this.Tag)).dockPanel1);
            ((Form1)(this.Tag)).m_クラス登録Handle = m_クラス管理.Handle;
        }

        /// <summary>
        /// １つのセルを選択して、そのセルの行の値を取得処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void roomView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    if ((this.gv_view.Rows[e.RowIndex].Selected == false))
                    {
                        this.gv_view.ClearSelection();
                        this.gv_view.Rows[e.RowIndex].Selected = true;
                    }
                    //'只选中一行时设置活动单元格
                    if ((this.gv_view.SelectedRows.Count == 1))
                    {
                        this.gv_view.CurrentCell = this.gv_view.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    }
                }
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

            System.Drawing.Point point = this.gv_view.PointToClient(startPosition);
            DataGridView.HitTestInfo hitinfo;
            hitinfo = this.gv_view.HitTest(point.X, point.Y);

            this.gv_view.ClearSelection();
            if (hitinfo.RowIndex >= 0)
            {
                this.gv_view.Rows[hitinfo.RowIndex].Selected = true;
            }
            else
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// DGVのデータエラーの時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gv_view_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (gv_view.Rows[e.RowIndex].IsNewRow)
            {
                return;
            }
        }

        /// <summary>
        /// DGVのサイズ変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gv_view_SizeChanged(object sender, EventArgs e)
        {
            this.gv_view.Width = 920;
        }
    }
}
