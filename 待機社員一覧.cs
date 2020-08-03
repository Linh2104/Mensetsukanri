using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace HL_塾管理
{
    public partial class 待機社員一覧 : DockContent
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int BringWindowToTop(IntPtr hWnd);
        private AutoSizeFormClass asc = new AutoSizeFormClass();

        private string connectionString = ComClass.connectionString;

        public 待機社員一覧()
        {
            InitializeComponent();
        }

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

        private void 待機社員一覧_Load(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);
            DisplayGridView();
        }

        private void DisplayGridView()
        {
            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库

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

            string sqlcmd = @"Select "
                            + "　T1.社員コード "
                            + "　, T1.名前 "
                            + "　, T2.メイン言語 "
                            + "　, T3.在職状態 "
                            + "　, T3.待機状態 "
                            + " From "
                            + " HL_JINJI_社員マスタ AS T1 "
                            + " Left Join HL_JINJI_エンジニアスキル情報 AS T2 "
                            + "   On T1.社員コード = T2.社員コード "
                            + " Left Join HL_JINJI_社員在職状態 AS T3 "
                            + "   On T1.社員コード = T3.社員コード "
                            + " Where 1 = 1 "
                            + " And T3.待機状態 = '待機' "
                            + " And T1.社員コード Not In "
                            + " (Select 教師コード From HL_JUKUKANRI_社内社員教師マスタ) "
                            + " Group by T1.社員コード, T1.名前, T2.メイン言語, T3.在職状態, T3.待機状態 ";

            SqlCommand com = new SqlCommand(sqlcmd, sqlcon);
            SqlDataReader reader = com.ExecuteReader();

            int Index = 0;
            rowMergeView.Rows.Clear();

            try
            {
                //一覧情報設定
                while (reader.Read())
                {
                    this.rowMergeView.Rows.Add();
                    this.rowMergeView.Rows[Index].Cells["社員コード"].Value = reader["社員コード"].ToString();
                    this.rowMergeView.Rows[Index].Cells["名前"].Value = reader["名前"].ToString();
                    this.rowMergeView.Rows[Index].Cells["メイン言語"].Value = reader["メイン言語"].ToString();
                    this.rowMergeView.Rows[Index].Cells["在職状態"].Value = reader["在職状態"].ToString();
                    this.rowMergeView.Rows[Index].Cells["待機状態"].Value = reader["待機状態"].ToString();

                    Index++;
                }
            }
            catch
            {
                this.toolStripStatusLabel1.ForeColor = Color.Red;
                this.toolStripStatusLabel1.Text = "検索処理に失敗しました.";
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

        private void 待機社員一覧_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form1)(this.Tag)).m_社員から教師へ変更Handle = IntPtr.Zero;
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            Point startPosition = Cursor.Position;

            Point point = this.rowMergeView.PointToClient(startPosition);
            DataGridView.HitTestInfo hitinfo;
            hitinfo = this.rowMergeView.HitTest(point.X, point.Y);

            this.rowMergeView.ClearSelection();
            if (hitinfo.RowIndex >= 0)
            {
                this.rowMergeView.Rows[hitinfo.RowIndex].Selected = true;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void rowMergeView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (rowMergeView.Rows[e.RowIndex].IsNewRow)
            {
                return;
            }
        }

        private void 登録ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.rowMergeView.CurrentCell != null)
            {
                this.rowMergeView.CurrentCell.Selected = true;
                string code_教師コード = this.rowMergeView.CurrentRow.Cells["社員コード"].Value.ToString();

                if (((Form1)(this.Tag)).m_社員から教師へ変更Handle != IntPtr.Zero)
                {
                    BringWindowToTop(((Form1)(this.Tag)).m_社員から教師へ変更Handle);
                    return;
                }
                else
                {
                    社員から教師へ変更 m_NewForm_社員から教師へ変更 = new 社員から教師へ変更();
                    m_NewForm_社員から教師へ変更.isUpdate = false;
                    m_NewForm_社員から教師へ変更.code_教師コード = code_教師コード;
                    m_NewForm_社員から教師へ変更.Tag = ((Form1)(this.Tag));
                    m_NewForm_社員から教師へ変更.Show(((Form1)(this.Tag)).dockPanel1);
                    ((Form1)(this.Tag)).m_社員から教師へ変更Handle = m_NewForm_社員から教師へ変更.Handle;
                }
            }
        }
    }
}
