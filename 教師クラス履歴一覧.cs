using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace HL_塾管理
{
    public partial class 教師クラス履歴一覧 : DockContent
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int BringWindowToTop(IntPtr hWnd);
        private AutoSizeFormClass asc = new AutoSizeFormClass();

        //DB接続情報
        private string connectionString = ComClass.connectionString;
        //教室コードの値
        public string code_教師コード = "";
        //教師名の値
        public string TeacherName = "";

        public 教師クラス履歴一覧()
        {
            InitializeComponent();
        }
        /// <summary>
        /// データが変更することがあったら、一覧画面が自動的に再表示処理
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
        /// 検索処理
        /// </summary>
        private void DisplayGridView()
        {
            if (((Form1)(Tag)).reLoad)
            {
                ((Form1)(Tag)).toolStripStatusLabel2.Text = "";
            }
            DataGridView1.Rows.Clear();

            SqlConnection sqlcon = new SqlConnection(connectionString);

            try
            {
                sqlcon.Open();
            }
            catch
            {
                StatusLabel.ForeColor = Color.Red;
                StatusLabel.Text = "DBサーバーの接続に失敗しました。";
                return;
            }

            string sql_cmd = String.Format(@"SELECT
	                            CL.クラスコード,
	                            CLR.備考,
	                            CL.課程,
	                            CL.開始日,
	                            CL.終了日,
	                            CL.有効
                            FROM
	                            HL_JUKUKANRI_クラス履歴 CL
                            LEFT JOIN 
	                            HL_JUKUKANRI_教室マスタ CLR
                            ON CL.教室コード = CLR.教室コード
                            WHERE
                                CL.有効= 1 AND
	                            教師コード = '{0}'", code_教師コード);

            SqlCommand sqlcomm = new SqlCommand(sql_cmd, sqlcon);
            SqlDataReader reader = sqlcomm.ExecuteReader();
            //件数の値
            int index = 0;


            try
            {
                while (reader.Read())
                {
                    if (
                        (reader["クラスコード"].ToString().IndexOf(txt_searchKey.Text) < 0) &&
                        (reader["備考"].ToString().IndexOf(txt_searchKey.Text) < 0) &&
                        (reader["課程"].ToString().IndexOf(txt_searchKey.Text) < 0) &&
                        (reader["開始日"].ToString().Replace('/', '-').IndexOf(txt_searchKey.Text) < 0) &&
                        (reader["終了日"].ToString().Replace('/', '-').IndexOf(txt_searchKey.Text) < 0) &&
                        (reader["有効"].ToString().IndexOf(txt_searchKey.Text) < 0)
                        )
                    {
                        continue;
                    }

                    //列の値設定
                    DataGridView1.Rows.Add();
                    DataGridView1.Rows[index].Cells["クラスコード"].Value = string.IsNullOrWhiteSpace(reader["クラスコード"].ToString()) ? "-" : reader["クラスコード"].ToString();
                    DataGridView1.Rows[index].Cells["教室"].Value = string.IsNullOrWhiteSpace(reader["備考"].ToString()) ? "-" : reader["備考"].ToString();
                    DataGridView1.Rows[index].Cells["課程"].Value = string.IsNullOrWhiteSpace(reader["課程"].ToString()) ? "-" : reader["課程"].ToString();
                    DataGridView1.Rows[index].Cells["開始日"].Value = string.IsNullOrWhiteSpace(reader["開始日"].ToString()) ? "-" : reader["開始日"].ToString();
                    DataGridView1.Rows[index].Cells["終了日"].Value = string.IsNullOrWhiteSpace(reader["終了日"].ToString()) ? "-" : reader["終了日"].ToString();
                    DataGridView1.Rows[index].Cells["有効"].Value = string.IsNullOrWhiteSpace(reader["有効"].ToString()) ? "-" : reader["有効"].ToString();

                    //クラスの色付き：　灰色ー終了クラス、　黄色ー実施中クラス、　白色ー未実施クラス
                    if (DateTime.Compare(DateTime.Now, Convert.ToDateTime(DataGridView1.Rows[index].Cells["終了日"].Value)) >= 0)
                    {
                        DataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.Gray;
                    }
                    else if (DataGridView1.Rows[index].Cells["有効"].Value.ToString() == "True" &&
                            (DateTime.Compare(DateTime.Now, Convert.ToDateTime(DataGridView1.Rows[index].Cells["開始日"].Value)) >= 0 &&
                            DateTime.Compare(DateTime.Now, Convert.ToDateTime(DataGridView1.Rows[index].Cells["終了日"].Value.ToString() =="-" ? "1000/01/01" : DataGridView1.Rows[index].Cells["終了日"].Value)) <= 0) ||
                            (DateTime.Compare(DateTime.Now, Convert.ToDateTime(DataGridView1.Rows[index].Cells["開始日"].Value)) >= 0 &&
                            DataGridView1.Rows[index].Cells["終了日"].Value.ToString() == "-"))
                    {
                        DataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.Yellow;
                    }

                    index++;
                }
                reader?.Close();
            }
            catch (Exception ex)
            {
                StatusLabel.ForeColor = Color.Red;
                StatusLabel.Text = ex.ToString();
                ((Form1)(Tag)).reLoad = false;
                sqlcon.Close();
                return;

            }
            finally
            {
                sqlcon?.Close();
            }
            //件数設定
            lbl_件数.Text = $"{index}件";
            ((Form1)(Tag)).reLoad = true;
        }

        /// <summary>
        /// 最初画面設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 教師クラス履歴一覧_Load(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);

            lbl_教師名.Text = TeacherName;
            //DGV表示
            DisplayGridView();
        }

        /// <summary>
        /// 検索ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_search_Click(object sender, EventArgs e)
        {
            //DGV再表示
            DisplayGridView();
        }

        /// <summary>
        /// DGVのデータエラー処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (this.DataGridView1.Rows[e.RowIndex].IsNewRow)
            {
                return;
            }
        }

        /// <summary>
        /// 画面閉じる処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 教師クラス履歴一覧_FormClosed(object sender, FormClosedEventArgs e)
        {
            //画面を閉じる時にHandleが０に初期化する
            ((Form1)(this.Tag)).m_教師クラス履歴一覧Handle = IntPtr.Zero;
        }
    }
}
