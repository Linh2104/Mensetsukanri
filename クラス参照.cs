using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace HL_塾管理
{
    public partial class クラス参照 : DockContent
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]

        public static extern int BringWindowToTop(IntPtr hWnd);

        private AutoSizeFormClass asc = new AutoSizeFormClass();

        //DB接続情報
        private string connectionString = ComClass.connectionString;
        //教師名のテーブル
        private DataTable dt_TeacherName = null;
        //教室のテーブル
        private DataTable dt_ClassNo = null;

        private DataTable dt_ClassHistory = null;
        //教師コード
        public string codeClass = "";
        //変更前の値
        private string beforValue = "";
        //編集フラッグ
        private bool isEditing = false;
        //クラスリスト
        private Dictionary<string, string> classList = new Dictionary<string, string>();
        //学生リスト
        Dictionary<string, string> list_学生 = new Dictionary<string, string>();

        private int key;

        private string check = "";

        private string usercheck = "";

        public クラス参照()
        {
            InitializeComponent();
        }
        /// <summary>
        /// データが変更することがあれば、DGV再表示
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
        /// ユーザーの職務チェック
        /// </summary>
        private void Usercheck()
        {
            if (((Form1)(this.Tag)).m_ユーザ登録.m_ログイン_職務 == "管理者")
            {
                usercheck = "admin";
            }

            if (((Form1)(this.Tag)).m_ユーザ登録.m_ログイン_職務 == "一般ユーザ")
            {
                usercheck = "user";
            }
        }

        /// <summary>
        /// DBから呼び出すデータをテーブルに作成用
        /// </summary>
        /// <param name="str_cmd"></param>
        /// <returns></returns>
        private DataTable GetDatatable(string str_cmd)
        {
            SqlConnection sqlcon = new SqlConnection(connectionString);
            try
            {
                sqlcon.Open();
            }
            catch
            {
                ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(Tag)).toolStripStatusLabel2.Text = "DBサーバーの接続に失敗しました。";
                ((Form1)(Tag)).reLoad = false;
            }

            try
            {
                DataTable table = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(str_cmd, sqlcon);
                DataSet ds = new DataSet();
                da.Fill(ds);
                table = ds.Tables[0];
                return table;

            }
            catch (Exception ex)
            {
                ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(Tag)).toolStripStatusLabel2.Text = ex.ToString();
            }
            finally
            {
                sqlcon?.Close();
            }

            return null;
        }

        /// <summary>
        /// DGVにある教室、教師名のCMB設定
        /// </summary>
        private void SetData()
        {
            //教室のCMB設定
            string ClassInfoQuery = String.Format (@"SELECT DISTINCT cl.教室コード, cl.備考 from HL_JUKUKANRI_教室マスタ cl
                                            LEFT OUTER JOIN HL_JUKUKANRI_クラス履歴 clr
                                            ON cl.教室コード=clr.教室コード");
            dt_ClassNo = GetDatatable(ClassInfoQuery);

            //教師名のCMB設定
            string TeacherInfoQuery = @"SELECT 教師コード,名前 FROM HL_JUKUKANRI_教師情報 ORDER BY 教師コード";
            dt_TeacherName = GetDatatable(TeacherInfoQuery);

        }

        /// <summary>
        /// DGV設定
        /// </summary>
        private void DisplayGridView()
        {
            rowMergeView1.ScrollBars = ScrollBars.None;

            SetData();

            if (((Form1)(Tag)).reLoad)
            {
                ((Form1)(Tag)).toolStripStatusLabel2.Text = "";
            }
            rowMergeView1.Rows.Clear();

            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库

            try
            {
                sqlcon.Open();
            }
            catch
            {
                ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(Tag)).toolStripStatusLabel2.Text = "DBサーバーの接続に失敗しました.";
                ((Form1)(Tag)).reLoad = false;
                return;
            }


            string str_sqlcmd = string.Format(@"SELECT a.クラスコード,a.クラス名, a.教室コード, a.教師コード, d.備考,(CASE WHEN b.名前 IS NULL THEN c.名前 ELSE b.名前 END) AS 名前, a.課程, a.学生コード, a.開始日, a.終了日
                                                ,学生 = STUFF((SELECT ', ' + t1.名前
                                                 FROM dbo.HL_JUKUKANRI_学生クラス md
                                                 Left Join dbo.HL_JUKUKANRI_学生マスタ t1 on t1.学生コード = md.学生コード
                                                 WHERE a.クラスコード = md.クラスコード
                                                 FOR XML PATH('')), 1, 1, '')
                                                , a.研修フラグ
                                                FROM HL_JUKUKANRI_クラス履歴 a
                                                LEFT JOIN HL_JUKUKANRI_社外教師マスタ b ON a.教師コード = b.教師コード
                                                LEFT JOIN HL_JUKUKANRI_社内社員教師マスタ c ON a.教師コード = c.社員コード
                                                LEFT JOIN HL_JUKUKANRI_教室マスタ d ON a.教室コード = d.教室コード
                                                WHERE a.有効 = 1 ");
            if (cmb_学生.Text == "一般")
            {
                str_sqlcmd += " and a.研修フラグ = 'False'";
            }
            else if (cmb_学生.Text == "研修")
            {
                str_sqlcmd += " and a.研修フラグ = 'True'";
            }

            str_sqlcmd += " ORDER BY a.クラスコード ";

            SqlCommand com = new SqlCommand(str_sqlcmd, sqlcon);
            SqlDataReader reader = com.ExecuteReader();
            int index = 0;
            try
            {
                while (reader.Read())
                {
                    if (
                       (reader["教室コード"].ToString().IndexOf(txt_searchKey.Text) < 0)
                       &&
                       (reader["クラス名"].ToString().IndexOf(txt_searchKey.Text) < 0)
                       &&
                       (reader["クラスコード"].ToString().IndexOf(txt_searchKey.Text) < 0)
                       &&
                       (reader["備考"].ToString().IndexOf(txt_searchKey.Text) < 0)
                       &&
                       (reader["教師コード"].ToString().IndexOf(txt_searchKey.Text) < 0)
                       &&
                       (reader["名前"].ToString().IndexOf(txt_searchKey.Text) < 0)
                       &&
                       (reader["課程"].ToString().IndexOf(txt_searchKey.Text) < 0)
                       &&
                       (reader["学生"].ToString().IndexOf(txt_searchKey.Text) < 0)
                       &&
                       (reader["開始日"].ToString().IndexOf(txt_searchKey.Text) < 0)
                       &&
                       (reader["終了日"].ToString().IndexOf(txt_searchKey.Text) < 0)
                       )
                    {
                        continue;
                    }

                    rowMergeView1.Rows.Add();
                    //set CMB「教室」↓
                    string class_No = string.IsNullOrWhiteSpace(reader["教室コード"].ToString()) ? "-" : reader["教室コード"].ToString();
                    string class_Name = classList.ContainsKey(class_No) ? classList.FirstOrDefault(x => x.Key == class_No).Value : "-";

                    DataTable class_tbl = new DataTable();
                    class_tbl.Clear();
                    class_tbl.Columns.Add("No");
                    class_tbl.Columns.Add("備考");

                    foreach (DataRow dr in dt_ClassNo.Rows)
                    {
                        class_tbl.Rows.Add(dr.ItemArray);
                    }

                    ((DataGridViewComboBoxCell)(rowMergeView1.Rows[index].Cells["教室"])).DisplayMember = "備考";
                    ((DataGridViewComboBoxCell)(rowMergeView1.Rows[index].Cells["教室"])).ValueMember = "教室コード";
                    ((DataGridViewComboBoxCell)(rowMergeView1.Rows[index].Cells["教室"])).DataSource = class_tbl;

                    rowMergeView1.Rows[index].Cells["教室"].Value = classList.ContainsKey(class_No) ? classList.FirstOrDefault(x => x.Key == class_No).Value : "-";
                    //set CMB「教室」↑

                    rowMergeView1.Rows[index].Cells["クラスコード"].Value = string.IsNullOrWhiteSpace(reader["クラスコード"].ToString()) ? "-" : reader["クラスコード"].ToString();
                    rowMergeView1.Rows[index].Cells["クラス名"].Value = string.IsNullOrWhiteSpace(reader["クラス名"].ToString()) ? "-" : reader["クラス名"].ToString();
                    rowMergeView1.Rows[index].Cells["学生コード"].Value = string.IsNullOrWhiteSpace(reader["学生コード"].ToString()) ? "-" : reader["学生コード"].ToString();
                    rowMergeView1.Rows[index].Cells["教師コード"].Value = string.IsNullOrWhiteSpace(reader["教師コード"].ToString()) ? "-" : reader["教師コード"].ToString();
                                        
                    rowMergeView1.Rows[index].Cells["課程"].Value = reader["課程"].ToString().Trim();

                    //set CMB「教師名」↓
                    string teacher_name = string.IsNullOrWhiteSpace(reader["名前"].ToString()) ? "-" : reader["名前"].ToString();

                    DataTable teacher_table = new DataTable();
                    teacher_table.Clear();
                    teacher_table.Columns.Add("Code");
                    teacher_table.Columns.Add("Name");
                    teacher_table.Columns.Add("MainLanguge");
                    foreach (DataRow dr in dt_TeacherName.Rows)
                    {
                        if (rowMergeView1.Rows[index].Cells["教師コード"].Value.ToString() != dr["教師コード"].ToString())
                        {
                            teacher_table.Rows.Add(dr.ItemArray);
                        }
                    }
                    string queryselect = string.Format("Code ='{0}'", rowMergeView1.Rows[index].Cells["教師コード"]);
                    DataRow[] dr1 = teacher_table.Select(queryselect);
                    if (dr1.Length == 0)
                    {
                        teacher_table.Rows.Add(rowMergeView1.Rows[index].Cells["教師コード"].Value.ToString(), teacher_name, rowMergeView1.Rows[index].Cells["課程"].Value);
                    }

                    ((DataGridViewComboBoxCell)(rowMergeView1.Rows[index].Cells["教師名"])).DisplayMember = "Name";

                    ((DataGridViewComboBoxCell)(rowMergeView1.Rows[index].Cells["教師名"])).DataSource = teacher_table;
                    rowMergeView1.Rows[index].Cells["教師名"].Value = string.IsNullOrWhiteSpace(reader["名前"].ToString()) ? "-" : reader["名前"].ToString();
                    //set CMB「教師名」↑

                    rowMergeView1.Rows[index].Cells["学生"].Value = string.IsNullOrWhiteSpace(reader["学生"].ToString()) ? "-" : reader["学生"].ToString();
                    rowMergeView1.Rows[index].Cells["開始日"].Value = string.IsNullOrWhiteSpace(reader["開始日"].ToString()) ? "-" : reader["開始日"].ToString();
                    rowMergeView1.Rows[index].Cells["終了日"].Value = string.IsNullOrWhiteSpace(reader["終了日"].ToString()) ? "-" : reader["終了日"].ToString();
                    rowMergeView1.Rows[index].Cells["研修フラグ"].Value = reader["研修フラグ"].ToString();

                    foreach (DataGridViewCell gvCell in rowMergeView1.Rows[index].Cells)
                    {
                        if (gvCell.Value == null || gvCell.Value.ToString() == "-")
                        {
                            rowMergeView1.Rows[index].Cells[gvCell.ColumnIndex].Style.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
                        }

                        if(rowMergeView1.Rows[index].Cells["研修フラグ"].Value.ToString() == "True")
                        {
                            this.rowMergeView1.Rows[index].Cells["クラスコード"].Style.BackColor = Color.LightGreen;
                            this.rowMergeView1.Rows[index].Cells["学生"].Style.BackColor = Color.LightGreen;
                            this.rowMergeView1.Rows[index].Cells["研修フラグ"].Style.BackColor = Color.LightGreen;
                        }
                    }

                    if (rowMergeView1.Rows[index].Cells["教師コード"].Value.ToString() != ((Form1)(this.Tag)).m_ユーザ登録.m_ログイン_教師コード && usercheck == "user")
                    {
                        rowMergeView1.Rows[index].DefaultCellStyle.BackColor = Color.White;
                        rowMergeView1.Rows[index].DefaultCellStyle.ForeColor = Color.Black;
                        rowMergeView1.Rows[index].Cells["クラス名"].ReadOnly = true;
                        rowMergeView1.Rows[index].Cells["開始日"].ReadOnly = true;
                        rowMergeView1.Rows[index].Cells["終了日"].ReadOnly = true;
                        rowMergeView1.Rows[index].Cells["教室"].ReadOnly = true;
                        rowMergeView1.Rows[index].Cells["教師名"].ReadOnly = true;
                        rowMergeView1.Rows[index].Cells["課程"].ReadOnly = true;
                    }
                    else if (rowMergeView1.Rows[index].Cells["教師コード"].Value.ToString() == ((Form1)(this.Tag)).m_ユーザ登録.m_ログイン_教師コード && usercheck == "user")
                    {
                        rowMergeView1.Rows[index].Cells["教師名"].ReadOnly = true;
                    }

                    index++;
                }

                //DGVの広さを設定
                int TotalColumnsWidth = 0;
                foreach (DataGridViewColumn dvgcol in rowMergeView1.Columns)
                {
                    if (dvgcol.Visible == true)
                    {
                        TotalColumnsWidth += dvgcol.Width;
                    }
                }
                rowMergeView1.Width = TotalColumnsWidth + rowMergeView1.RowHeadersWidth + 2;

                rowMergeView1.ScrollBars = ScrollBars.Both;
            }
            catch (Exception ex)
            {
                ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(Tag)).toolStripStatusLabel2.Text = ex.ToString();
                ((Form1)(Tag)).reLoad = false;
                sqlcon.Close();
                return;
            }
            sqlcon.Close();

            toolStripStatusLabel1.Text = string.Format("{0}件", index);
            ((Form1)(Tag)).reLoad = true;
        }

        #region Class_Info

        private DataTable Class_Info()
        {

            SqlConnection sqlcon = new SqlConnection(connectionString);
            try
            {
                sqlcon.Open();
            }
            catch
            {
                ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(Tag)).toolStripStatusLabel2.Text = "DBサーバーの接続に失敗しました。";
                ((Form1)(Tag)).reLoad = false;
                return null;
            }
            string str_cmd = @"SELECT a.教室コード, a.備考  FROM HL_JUKUKANRI_教室マスタ　a";

            try
            {
                var classDataTable = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(str_cmd, sqlcon);
                DataSet ds = new DataSet();
                da.Fill(ds, "ClassInfo");
                classDataTable = ds.Tables["ClassInfo"];
                return classDataTable;
            }
            catch (Exception ex)
            {
                ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(Tag)).toolStripStatusLabel2.Text = ex.ToString();
                throw;
            }
            finally
            {
                sqlcon?.Close();
            }
        }
        #endregion
        /// <summary>
        /// クラス履歴Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 歴史クラス一覧_Load(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);

            //職務チェック
            Usercheck();

            //教室番号
            var classInfo = Class_Info();
            if (classInfo == null)
            {
                ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(Tag)).toolStripStatusLabel2.Text = "Class info is nulled";
            }
            else
            {
                foreach (DataRow item in classInfo.Rows)
                {
                    classList.Add(item.ItemArray[0].ToString(), item.ItemArray[1].ToString());
                }
            }

            //DGV再表示
            DisplayGridView();
            cmb_学生.SelectedIndex = 0;
        }

        /// <summary>
        /// 当画面変更された時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 歴史クラス一覧_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form1)(Tag)).m_クラス参照Handle = IntPtr.Zero;
            ((Form1)(Tag)).toolStripStatusLabel2.Text = "";
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
            ((Form1)(Tag)).toolStripStatusLabel2.Text = "";
        }

        /// <summary>
        /// 右メニューの変更メニュー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 変更ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "";
            //教室コード取得
            codeClass = rowMergeView1.CurrentRow.Cells["クラスコード"].Value.ToString();
            //複数画面チェック
            if (((Form1)(Tag)).codeDic.ContainsKey(codeClass))
            {
                ((Form1)(Tag)).codeDic.TryGetValue(codeClass, out key);
                BringWindowToTop((IntPtr)key);
                return;
            }

            //クラス管理呼び出す
            クラス管理 m_クラス管理 = new クラス管理();
            m_クラス管理.UpdateEventHandler += クラス管理_UpdateEventHandler;
            m_クラス管理.isUpdate = "update";
            m_クラス管理.code_クラスコード = rowMergeView1.CurrentRow.Cells["クラスコード"].Value.ToString();
            m_クラス管理.code_教師コード= rowMergeView1.CurrentRow.Cells["教師コード"].Value.ToString();
            m_クラス管理.研修フラグ = rowMergeView1.CurrentRow.Cells["研修フラグ"].Value.ToString();
            m_クラス管理.Tag = ((Form1)(Tag));
            m_クラス管理.Show(((Form1)(Tag)).dockPanel1);
            ((Form1)(Tag)).m_クラス変更Handle = m_クラス管理.Handle;

            int ptr = (int)((Form1)(Tag)).m_クラス変更Handle;
            //曜日出した画面が複数画面開かないチェックのため、教室コード入れる。
            if (!((Form1)(Tag)).codeDic.ContainsKey(codeClass))
            {
                ((Form1)(Tag)).codeDic.Add(codeClass, ptr);
            }

        }
        private void クラス管理_UpdateEventHandler(object sender, クラス管理.UpdateEventArgs args)
        {
            DisplayGridView();
        }

        /// <summary>
        /// 右メニューの削除メニュー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 削除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rowMergeView1.CurrentCell != null)
            {
                //職務チェック
                Usercheck();

                string code_教師コード = rowMergeView1.CurrentRow.Cells["教師コード"].Value.ToString();

                if (code_教師コード != ((Form1)(this.Tag)).m_ユーザ登録.m_ログイン_教師コード && usercheck == "user")
                {
                    DisplayGridView();
                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "自分の名前のみの情報しか変更することができない。";
                    return;
                }

                //情報取得
                DateTime date_start = Convert.ToDateTime(rowMergeView1.CurrentRow.Cells["開始日"].Value);
                DateTime date_end = Convert.ToDateTime(rowMergeView1.CurrentRow.Cells["終了日"].Value);
                string code_クラス = rowMergeView1.CurrentRow.Cells["クラスコード"].Value.ToString();
                string OldCode = rowMergeView1.CurrentRow.Cells["教師コード"].Value.ToString();

                //削除したいクラスの担当教師のクラスを取得
                string cmd_old = string.Format("SELECT クラスコード FROM HL_JUKUKANRI_クラス履歴 WHERE 教師コード='{0}' AND 有効 ='1' ", OldCode);
                DataTable dt_old = new DataTable();
                dt_old = GetDatatable(cmd_old);

                //クラスの開始日と終了日チェック
                if (DateTime.Compare(DateTime.Now, date_start) >= 0 && DateTime.Compare(DateTime.Now, date_end) <= 0)
                {
                    isEditing = false;
                    ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                    ((Form1)(Tag)).toolStripStatusLabel2.Text = string.Format(@"途中クラス [{0}] を削除できません。", code_クラス);
                    ((Form1)(Tag)).reLoad = false;
                }
                else
                {
                    rowMergeView1.CurrentCell.Selected = true;

                    SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库

                    int result = 0;
                    try
                    {
                        sqlcon.Open();
                    }
                    catch
                    {
                        ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                        ((Form1)(Tag)).toolStripStatusLabel2.Text = "DBサーバーの接続に失敗しました。";
                        ((Form1)(Tag)).reLoad = false;
                        return;
                    }
                    SqlCommand sqlcom = new SqlCommand();
                    sqlcom.Connection = sqlcon;
                    SqlTransaction transaction = sqlcon.BeginTransaction();
                    sqlcom.Transaction = transaction;

                    //HL_JUKUKANRI_学生クラスに削除されたクラスにいる学生のクラスコードを削除行う
                    if (rowMergeView1.CurrentRow.Cells["学生コード"].Value.ToString() != "-")
                    {
                        try
                        {
                            string[] student_code = rowMergeView1.CurrentRow.Cells["学生コード"].Value.ToString().Split(new char[] { ',' });
                            foreach (string code in student_code)
                            {
                                sqlcom.CommandText = string.Format("Update HL_JUKUKANRI_学生クラス Set クラスコード = NULL Where 学生コード = '{0}'", code);

                                result = sqlcom.ExecuteNonQuery();

                                if (result != 1)
                                {
                                    transaction.Rollback();
                                    ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                                    ((Form1)(Tag)).toolStripStatusLabel2.Text = string.Format("学生コード[{0}]の更新処理が失敗しました.", code);
                                    ((Form1)(Tag)).reLoad = false;
                                    return;
                                }
                            }
                            //HL_JUKUKANRI_クラス履歴 削除行う
                            sqlcom.CommandText = string.Format(@"Update HL_JUKUKANRI_クラス履歴 Set 有効 = 0 Where　クラスコード = {0}", code_クラス);

                            result = sqlcom.ExecuteNonQuery();

                            if (result == 1)
                            {
                                //HL_JUKUKANRI_教師情報にクラス 削除行う
                                List<int> OldTeacherCodeList = (from x in dt_old.AsEnumerable()
                                                                select x.Field<int>("クラスコード")).ToList();
                                OldTeacherCodeList.Remove(Convert.ToInt32(code_クラス));
                                int result1;
                                sqlcom.CommandText = OldTeacherCodeList.Count > 0 ?
                                                    string.Format("UPDATE HL_JUKUKANRI_教師情報 SET クラスコード='{0}' WHERE 教師コード='{1}' ", string.Join(",", OldTeacherCodeList), OldCode) :
                                                    string.Format("UPDATE HL_JUKUKANRI_教師情報 SET クラスコード=NULL WHERE 教師コード='{0}'", OldCode);
                                result1 = sqlcom.ExecuteNonQuery();
                                if (result1 == 1)
                                {
                                    transaction.Commit();
                                    ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Green;
                                    ((Form1)(Tag)).toolStripStatusLabel2.Text = string.Format("クラス [{0}] が正常に削除されました。", code_クラス);
                                    ((Form1)(Tag)).reLoad = false;
                                    rowMergeView1.Rows.Remove(rowMergeView1.CurrentRow);
                                }
                                else
                                {
                                    transaction.Rollback();
                                    ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                                    ((Form1)(Tag)).toolStripStatusLabel2.Text = string.Format("教師 [{0}] のクラス変更が失敗しました。", OldCode);
                                }
                            }
                        }
                        catch
                        {
                            transaction.Rollback();
                            ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                            ((Form1)(Tag)).toolStripStatusLabel2.Text += string.Format("クラス [{0}] 削除処理に失敗しました。", code_クラス);
                            ((Form1)(Tag)).reLoad = false;
                        }
                        finally
                        {
                            sqlcon?.Close();
                        }
                    }
                }
            }
            //DGV再表示
            DisplayGridView();
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

            System.Drawing.Point point = rowMergeView1.PointToClient(startPosition);
            DataGridView.HitTestInfo hitinfo;
            hitinfo = rowMergeView1.HitTest(point.X, point.Y);

            rowMergeView1.ClearSelection();
            if (hitinfo.RowIndex >= 0)
            {
                rowMergeView1.Rows[hitinfo.RowIndex].Selected = true;
            }
            else
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 右メニューの学生選択メニュー処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 学生選択ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((Form1)(Tag)).toolStripStatusLabel2.Text = "";

            //情報取得
            codeClass = rowMergeView1.CurrentRow.Cells["クラスコード"].Value.ToString();
            string[] student_code = rowMergeView1.CurrentRow.Cells["学生コード"].Value.ToString().Split(new char[] { ',' });
            string Selected教師コード = rowMergeView1.CurrentRow.Cells["教師コード"].Value.ToString();
            string クラス名 = rowMergeView1.CurrentRow.Cells["クラス名"].Value.ToString();
            string 研修フラグ = rowMergeView1.CurrentRow.Cells["研修フラグ"].Value.ToString();
            if (student_code[0] == "-")
            {
                student_code = new string[] { };
            }
            //新学生リスト
            string[] Student_new = new string[] { };

            //複数画面が開かないためチェック
            if (((Form1)(Tag)).m_クラス選択Handle != IntPtr.Zero)
            {
                BringWindowToTop(((Form1)(Tag)).m_クラス選択Handle);
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
                ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(Tag)).toolStripStatusLabel2.Text = "DBサーバーの接続に失敗しました.";
                ((Form1)(Tag)).reLoad = false;
            }

            SqlTransaction transaction = sqlcon.BeginTransaction();
            SqlCommand sqlcom = new SqlCommand();
            sqlcom.Connection = sqlcon;
            sqlcom.Transaction = transaction;

            //学生選択画面を呼び出す
            学生選択 m_NewForm_学生選択 = new 学生選択();
            try
            {
                m_NewForm_学生選択.Tag = ((Form1)(Tag));
                //一覧画面からデータを渡す
                m_NewForm_学生選択.code_クラスコード = rowMergeView1.CurrentRow.Cells["クラスコード"].Value.ToString();
                m_NewForm_学生選択.user = ((Form1)(this.Tag)).m_ユーザ登録.m_ログイン_職務;
                m_NewForm_学生選択.Login教師コード = ((Form1)(this.Tag)).m_ユーザ登録.m_ログイン_教師コード;
                m_NewForm_学生選択.Selected教師コード = Selected教師コード;
                m_NewForm_学生選択.クラス名 = クラス名;
                m_NewForm_学生選択.研修フラグ = 研修フラグ;
                m_NewForm_学生選択.list_学生 = new Dictionary<string, string>();
                //旧学生リストを取得
                string Student_code = rowMergeView1.CurrentRow.Cells["学生コード"].Value.ToString().Replace("-", "");
                if (Student_code != "")
                {
                    Student_code = Student_code.Substring(0, 1) == "," ? Student_code.Substring(1, Student_code.Length - 1) : Student_code;
                }
                string[] code = new string[] { };
                //旧学生リストが「ー」ではないと、学生コード別々分ける
                if (Student_code != "-")
                {
                    code = Student_code.Split(',');
                }

                int value = 0;
                list_学生.Clear();
                //list_学生に分けた学生コードを入れる
                foreach (var item in code)
                {
                    list_学生.Add(item, value.ToString());
                    value++;
                }
                m_NewForm_学生選択.list_学生 = list_学生;

                m_NewForm_学生選択.ShowDialog();
                ((Form1)(Tag)).m_学生選択Handle = m_NewForm_学生選択.Handle;
                list_学生 = m_NewForm_学生選択.list_学生;

                if (list_学生.ContainsKey(""))
                {
                    list_学生.Remove("");
                }
                //変更あるかどうか 
                string result_name = "";
                if (list_学生.Count > 0)
                {
                    //新学生リスト作成
                    Student_new = new string[list_学生.Count];
                    int i = 0;
                    foreach (string key in list_学生.Keys)
                    {
                        if (i == 0)
                        {
                            result_name += key;
                        }
                        else
                        {
                            result_name += "," + key;
                        }
                        i++;
                    }
                }

                if (string.IsNullOrWhiteSpace(result_name))
                {
                    ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                    ((Form1)(Tag)).toolStripStatusLabel2.Text = "学生少なくても1名が必要です。";
                    ((Form1)(Tag)).reLoad = false;
                    return;
                }

                if (result_name == Student_code)
                {
                    return;
                }

                //一旦選択クラスの学生がクラスコード削除する
                if (code.Length != 0)
                {
                    sqlcom.CommandText = string.Format("Update HL_JUKUKANRI_クラス履歴 Set 学生コード = NULL Where クラスコード = '{0}'", codeClass);

                    result = sqlcom.ExecuteNonQuery();

                    foreach (string code_old in student_code)
                    {
                        sqlcom.CommandText = string.Format("Update HL_JUKUKANRI_学生クラス Set クラスコード = NULL Where 学生コード = '{0}'", code_old);

                        result = sqlcom.ExecuteNonQuery();

                        if (result != 1)
                        {
                            transaction.Rollback();
                            ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                            ((Form1)(Tag)).toolStripStatusLabel2.Text = string.Format("クラスコード[{0}]の更新処理が失敗しました.", codeClass);
                            return;
                        }
                    }
                }

                if (list_学生.Count > 0)
                {
                    //新学生リスト作成
                    Student_new = new string[list_学生.Count];
                    for (int i = 0; i < list_学生.Count; i++)
                    {
                        foreach (string name in list_学生.Keys)
                        {
                            Student_new.SetValue(name, i);
                            //新学生リストにある学生のクラス更新

                            //学生クラスにデータ更新
                            sqlcom.CommandText = string.Format("Update HL_JUKUKANRI_学生クラス Set クラスコード = '{0}' Where 学生コード = '{1}'", codeClass, name);

                            result = sqlcom.ExecuteNonQuery();

                            if (result != 1)
                            {
                                transaction.Rollback();
                                ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                                ((Form1)(Tag)).toolStripStatusLabel2.Text = string.Format("クラスコード[{0}]の更新処理が失敗しました.", codeClass);
                                return;
                            }

                            i++;
                        }
                    }
                }

                //クラス履歴に選択しているクラスの新学生リストを更新
                if (!string.IsNullOrWhiteSpace(result_name))
                {
                    sqlcom.CommandText = string.Format("Update HL_JUKUKANRI_クラス履歴 Set 学生コード = '{0}' Where クラスコード = '{1}' ", result_name, codeClass);
                    result = sqlcom.ExecuteNonQuery();
                    if (result == 1)
                    {
                        transaction.Commit();
                        DisplayGridView();

                        ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Green;
                        ((Form1)(Tag)).toolStripStatusLabel2.Text = string.Format("クラスコード[{0}]を正常に更新しました", codeClass);
                    }
                    else
                    {
                        DisplayGridView();
                        ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                        ((Form1)(Tag)).toolStripStatusLabel2.Text = string.Format("クラスコード[{0}]を更新できませんでした", codeClass);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(Tag)).toolStripStatusLabel2.Text = ex.ToString();
            }
            finally
            {
                sqlcon?.Close();
            }
        }

        /// <summary>
        /// DGVのデータエラーの時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rowMergeView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (rowMergeView1.Rows[e.RowIndex].IsNewRow)
            {
                return;
            }
        }

        /// <summary>
        /// DGVのセル編集処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rowMergeView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            beforValue = rowMergeView1.CurrentCell.Value.ToString();
            isEditing = true;
        }

        /// <summary>
        /// 現在クラス開始日とクラス終了日とクラス名しか変更できない
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rowMergeView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (isEditing && rowMergeView1.CurrentCell.EditedFormattedValue.ToString() != beforValue)
            {
                string code_Class = rowMergeView1.CurrentRow.Cells["クラスコード"].Value.ToString();
                string cellValue = rowMergeView1.CurrentCell.EditedFormattedValue.ToString();
                string DGV_column_name = rowMergeView1.SelectedCells[0].OwningColumn.HeaderText;
                string DB_column_name = "";
                switch (DGV_column_name)
                {
                    case "クラス開始日":
                        DB_column_name = "開始日";
                        break;
                    case "クラス終了日":
                        DB_column_name = "終了日";
                        break;
                    case "クラス名":
                        DB_column_name = "クラス名";
                        break;
                    default:
                        break;
                }
                string Update_table = "HL_JUKUKANRI_クラス履歴";

                if (!InputCheck(code_Class, DGV_column_name, cellValue))
                {
                    DisplayGridView();
                    return;
                }

                //職務チェック
                Usercheck();

                string code_教師コード = rowMergeView1.CurrentRow.Cells["教師コード"].Value.ToString();

                if (code_教師コード != ((Form1)(this.Tag)).m_ユーザ登録.m_ログイン_教師コード && usercheck == "user")
                {
                    DisplayGridView();
                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "自分の名前のみの情報しか変更することができない。";
                    return;
                }

                //期間変更時に同じ教室を使う他のクラスの期間と重ねるかどうかチェック
                string 研修フラグ = rowMergeView1.CurrentRow.Cells["研修フラグ"].Value.ToString();
                if (研修フラグ == "False")
                {
                    ClassHistoryCheck();
                    if (check == "NG")
                    {
                        DisplayGridView();
                        ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                        ((Form1)(Tag)).toolStripStatusLabel2.Text = string.Format("変更期間にて他のクラスがこの教室にまだあります。クラスコード [{0}] の更新処理が失敗しました。", code_Class);
                        return;
                    }
                }

                DataGridViewRow updateRow = rowMergeView1.CurrentRow;
                if (updateRow != null)
                {
                  Update_GridViewRow(Update_table, code_Class, DB_column_name, cellValue);
                }
            }
        }
        /// <summary>
        /// セルの編集中止処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rowMergeView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            isEditing = false;
        }

        /// <summary>
        /// セルを編集するためのコントロールが処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rowMergeView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
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
        /// セルをクリックして、行の値を取得の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rowMergeView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    if (rowMergeView1.Rows[e.RowIndex].Selected == false)
                    {
                        rowMergeView1.ClearSelection();
                        rowMergeView1.Rows[e.RowIndex].Selected = true;
                    }
                    if (rowMergeView1.SelectedRows.Count == 1)
                    {
                        rowMergeView1.CurrentCell = rowMergeView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    }
                }
            }
        }

        /// <summary>
        /// Combox事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            isEditing = false;
            ComboBox combox = sender as ComboBox;
            combox.Leave += new EventHandler(combox_Leave);

            //職務チェック
            Usercheck();

            string code_教師コード = rowMergeView1.CurrentRow.Cells["教師コード"].Value.ToString();

            if (code_教師コード != ((Form1)(this.Tag)).m_ユーザ登録.m_ログイン_教師コード && usercheck == "user")
            {
                DisplayGridView();
                ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "自分の名前のみの情報しか変更することができない。";
                return;
            }

            if (rowMergeView1.CurrentCell.FormattedValue.ToString() != rowMergeView1.CurrentCell.EditedFormattedValue.ToString())
            {
                DateTime class_startdate = Convert.ToDateTime(rowMergeView1.CurrentRow.Cells["開始日"].Value);
                DateTime class_enddate = Convert.ToDateTime(rowMergeView1.CurrentRow.Cells["終了日"].Value);
                string ColumnHeaderName = rowMergeView1.SelectedCells[0].OwningColumn.HeaderText;
                string 研修フラグ = rowMergeView1.CurrentRow.Cells["研修フラグ"].Value.ToString();

                try
                {
                    DataGridViewRow updateRow = rowMergeView1.CurrentRow;
                    string ID = updateRow.Cells["クラスコード"].Value.ToString();
                    string cellValue = null;
                    string column_name = "";
                    string Update_table = "";
                    #region 教室変更の場合
                    if (ColumnHeaderName == "教室")
                    {
                        column_name = "教室コード";
                        DataRowView row = (DataRowView)combox.SelectedItem;

                        if (combox.SelectedItem != null)
                        {
                            Update_table = "HL_JUKUKANRI_クラス履歴";
                            string new_教室コード= row.Row.ItemArray[0].ToString();

                            //新教室の履歴の期間チェック
                            if (研修フラグ == "False")
                            {
                                List<string> Getdata = NewClass_HistoryCheck(new_教室コード);
                                if (check == "NG")
                                {
                                    string name_クラス = Getdata[0];
                                    string ID_クラス = Getdata[1];
                                    string start_day = Getdata[2];
                                    string end_day = Getdata[3];

                                    string new_教室名 = row.Row.ItemArray[1].ToString();
                                    DisplayGridView();
                                    ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                                    ((Form1)(Tag)).toolStripStatusLabel2.Text = string.Format("教室[{0}]が[{1}～{2}]の期間内にクラス[{3}({4})]が既に使っているため、教室 または 日付を変更してください。", new_教室名, start_day, end_day, ID_クラス, name_クラス);
                                    return;
                                }
                            }
                            Update_GridViewRow(Update_table, ID, column_name, new_教室コード);
                        }
                    }
                    #endregion
                    #region 教師名変更の場合    
                    else if (ColumnHeaderName == "教師名")
                    {
                        {
                            column_name = "教師名";
                            if (combox.SelectedItem != null && column_name == rowMergeView1.Columns[rowMergeView1.CurrentCell.ColumnIndex].Name)
                            {
                                DataRowView row = (DataRowView)combox.SelectedItem;
                                string Value_code = row.Row.ItemArray[0].ToString();
                                string Value_name = row.Row.ItemArray[1].ToString();

                                //旧教師のクラスを取得
                                string OldCode = rowMergeView1.CurrentRow.Cells["教師コード"].Value.ToString();
                                string cmd_old = string.Format("SELECT クラスコード FROM HL_JUKUKANRI_クラス履歴 WHERE 教師コード='{0}' AND 有効 ='1' ", OldCode);
                                DataTable dt_old = new DataTable();
                                dt_old = GetDatatable(cmd_old);
                                //変更前の教師のクラスリストを取得
                                List<int> OldTeacherCodeList = (from x in dt_old.AsEnumerable()
                                                                select x.Field<int>("クラスコード")).ToList();
                                //旧教師のクラスリストから変更したいクラスを削除
                                OldTeacherCodeList.Remove(Convert.ToInt32(ID));

                                //新教師のクラスを取得
                                string cmd_new = string.Format("SELECT クラスコード FROM HL_JUKUKANRI_クラス履歴 WHERE 教師コード='{0}' AND 有効 ='1' ", Value_code);
                                DataTable dt_new = new DataTable();
                                dt_new = GetDatatable(cmd_new);
                                //変更前の教師のクラスリストを取得
                                List<int> NewTeacherCodeList = (from x in dt_new.AsEnumerable()
                                                                select x.Field<int>("クラスコード")).ToList();
                                //新教師のクラスリストに変更したいクラスを追加
                                NewTeacherCodeList.Add(Convert.ToInt32(ID));
                                if (研修フラグ == "False")
                                {
                                    NewTeacher_HistoryCheck(Value_code);
                                    if (check == "NG")
                                    {
                                        List<string> getdata = NewTeacher_HistoryCheck(Value_code);
                                        string start_date = getdata[0];
                                        string end_date = getdata[1];
                                        string name_クラス = getdata[2];
                                        string ID_クラス = getdata[3];

                                        DisplayGridView();
                                        ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                                        ((Form1)(Tag)).toolStripStatusLabel2.Text = string.Format("教師[{0}]が[{1}～{2}]の期間内にクラス[{3}({4})]があるため登録できません。教師 または 日付を変更してください。", Value_name, start_date, end_date, ID_クラス, name_クラス);
                                        return;
                                    }
                                }

                                SqlConnection sqlcon = new SqlConnection(connectionString);

                                try
                                {
                                    sqlcon.Open();
                                }
                                catch
                                {
                                    ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                                    ((Form1)(Tag)).toolStripStatusLabel2.Text = "DBサーバーの接続に失敗しました。";
                                    ((Form1)(Tag)).reLoad = false;
                                    return;
                                }
                                SqlTransaction transaction = sqlcon.BeginTransaction();
                                SqlCommand sqlcom = new SqlCommand();
                                sqlcom.Connection = sqlcon;
                                sqlcom.Transaction = transaction;
                                try
                                {
                                    int result1;
                                    sqlcom.CommandText = OldTeacherCodeList.Count > 0 ?
                                    string.Format("UPDATE HL_JUKUKANRI_教師情報 SET クラスコード='{0}' WHERE 教師コード='{1}' ", string.Join(",", OldTeacherCodeList), OldCode) :
                                    string.Format("UPDATE HL_JUKUKANRI_教師情報 SET クラスコード=NULL WHERE 教師コード='{0}'", OldCode);

                                    result1 = sqlcom.ExecuteNonQuery();
                                    if (result1 == 1)
                                    {
                                        int result2;
                                        sqlcom.CommandText = string.Format("UPDATE HL_JUKUKANRI_教師情報 SET クラスコード='{0}' WHERE 教師コード='{1}' ", string.Join(",", NewTeacherCodeList), Value_code);
                                        result2 = sqlcom.ExecuteNonQuery();
                                        if (result2 == 1)
                                        {
                                            int result3;
                                            sqlcom.CommandText = string.Format("UPDATE HL_JUKUKANRI_クラス履歴 SET 教師コード='{0}' WHERE クラスコード= '{1}' ", Value_code, ID);
                                            result3 = sqlcom.ExecuteNonQuery();
                                            if (result3 == 1)
                                            {
                                                transaction.Commit();
                                                DisplayGridView();

                                                ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Green;
                                                ((Form1)(Tag)).toolStripStatusLabel2.Text = string.Format("教室[{0}]を正常に変更しました。", ID);
                                            }
                                        }
                                    }
                                }
                                catch
                                {
                                    transaction.Rollback();
                                    DisplayGridView();

                                    ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Green;
                                    ((Form1)(Tag)).toolStripStatusLabel2.Text = string.Format("教室[{0}]は変更失敗しました。", ID);
                                }
                                finally
                                {
                                    sqlcon?.Close();
                                }
                            }
                        }
                    }
                    #endregion
                    #region　言語変更の場合
                    else if (ColumnHeaderName == "課程")
                    {
                        if (DateTime.Compare(DateTime.Now, class_startdate) >= 0 &&
                            DateTime.Compare(DateTime.Now, class_enddate) <= 0 && 研修フラグ == "False")
                        {
                            isEditing = false;
                            DisplayGridView();
                            ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                            ((Form1)(Tag)).toolStripStatusLabel2.Text = string.Format("途中のクラス [{0}]　の課程が を変更できません。", ID);
                        }
                        else
                        {
                            try
                            {
                                column_name = "課程";
                                Update_table = "HL_JUKUKANRI_クラス履歴";
                                if (combox.SelectedItem != null)
                                {
                                    cellValue = rowMergeView1.CurrentCell.EditedFormattedValue.ToString();
                                    Update_GridViewRow(Update_table, ID, column_name, cellValue);
                                }

                            }
                            catch
                            {
                                DisplayGridView();
                                ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                                ((Form1)(Tag)).toolStripStatusLabel2.Text = string.Format("クラスコード [{0}] の更新処理が失敗しました。", ID);
                            }
                        }
                    }
                    #endregion
                    
                }
                catch (Exception ex)
                {
                    ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                    ((Form1)(Tag)).toolStripStatusLabel2.Text = ex.Message;
                    ((Form1)(Tag)).reLoad = true;
                }
            }
        }

        /// <summary>
        /// 离开combox时，把事件删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void combox_Leave(object sender, EventArgs e)
        {
            ComboBox combox = sender as ComboBox;
            //做完处理，须撤销动态事件
            combox.SelectedIndexChanged -= new EventHandler(ComboBox_SelectedIndexChanged);
        }

        /// <summary>
        /// DataUpdate
        /// </summary>
        /// <param name="updateTable"></param>
        /// <param name="codeClass"></param>
        /// <param name="columnName"></param>
        /// <param name="cellValue"></param>
        /// <returns></returns>
        private bool Update_GridViewRow(string updateTable, string codeClass, string columnName, string cellValue)
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
                ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(Tag)).toolStripStatusLabel2.Text = "DBサーバーの接続に失敗しました.";
                ((Form1)(Tag)).reLoad = false;

                return isUpdate;
            }
            SqlTransaction transaction = sqlcon.BeginTransaction();
            SqlCommand sqlcom = new SqlCommand();
            sqlcom.Connection = sqlcon;
            sqlcom.Transaction = transaction;

            //データチェック
            string cell_value = cellValue == "-" ? null : cellValue;

            try
            {
                //更新行う
                sqlcom.CommandText = string.Format("Update {0} Set {1} = '{2}' Where クラスコード = '{3}'", updateTable, columnName, cell_value, codeClass);

                result = sqlcom.ExecuteNonQuery();

                if (result == 1)
                {
                    transaction.Commit();
                    DisplayGridView();
                    ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Green;
                    ((Form1)(Tag)).toolStripStatusLabel2.Text = string.Format("クラスコード [{0}] の情報が正常に更新しました。", codeClass);
                    isUpdate = true;
                }
            }
            catch
            {
                transaction.Rollback();
                DisplayGridView();
                ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(Tag)).toolStripStatusLabel2.Text = string.Format("クラスコード [{0}] の更新処理が失敗しました。", codeClass);
            }
            finally
            {
                sqlcon?.Close();
            }
          ((Form1)(Tag)).reLoad = true;
            return isUpdate;
        }

        /// <summary>
        /// 入力チェック
        /// </summary>
        public bool InputCheck(string code_教室, string columnName, string cellValue)
        {

            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库

            try
            {
                sqlcon.Open();
            }
            catch
            {
                ((Form1)(Tag)).toolStripStatusLabel1.ForeColor = Color.Red;
                ((Form1)(Tag)).toolStripStatusLabel1.Text = "DBサーバーの接続に失敗しました";

                return false;
            }

            //存在チェック
            string sqlcmd = string.Format(@"Select * From HL_JUKUKANRI_クラス履歴 Where クラスコード = '{0}'", code_教室);
            SqlCommand sqlcom = new SqlCommand(sqlcmd, sqlcon);

            try
            {
                SqlDataReader reader = sqlcom.ExecuteReader();

                if (!reader.Read())
                {
                    //更新：学生コードが存在しない場合エラー
                    ((Form1)(Tag)).toolStripStatusLabel1.ForeColor = Color.Red;
                    ((Form1)(Tag)).toolStripStatusLabel1.Text = string.Format(@"エラー：クラスコード [{0}] が登録されていません.", code_教室);
                    return false;
                }
            }
            catch (Exception ex)
            {
                ((Form1)(Tag)).toolStripStatusLabel1.ForeColor = Color.Red;
                ((Form1)(Tag)).toolStripStatusLabel1.Text = "クラスコードのチェック処理にエラーが発生しました。" + ex.Message;

                sqlcon?.Close();
                return false;
            }
            //未入力箇所チェック
            string errmsg = "";
            DateTime date;

            switch (columnName)
            {
                case "教師コード":
                    if (string.IsNullOrWhiteSpace(cellValue))
                    {
                        errmsg = "教師コードを入力してください。";
                    }
                    else if (cellValue.IndexOf(" ") > 0)
                    {
                        errmsg = "教師コードに許可されない文字'半角SPACE' が入りました。";
                    }
                    else if (cellValue.IndexOf("　") != cellValue.LastIndexOf("　"))
                    {
                        errmsg = "教師コードに '全角SPACE' は二つ以上入力しないでください!";
                    }
                    else if (cellValue.IndexOf(",") > 0)
                    {
                        errmsg = "教師コードに許可されない文字「,」が入りました。";
                    }
                    break;
                case "クラス名":
                    if (string.IsNullOrWhiteSpace(cellValue))
                    {
                        errmsg = "クラス名が未入力です。";
                    }
                    break;
                case "学生":
                    if (string.IsNullOrWhiteSpace(cellValue))
                    {
                        errmsg = "教師コードを入力してください。";
                    }
                    break;
                case "クラス開始日":
                    //日付の形式と未入力チェック
                    if (string.IsNullOrWhiteSpace(cellValue) || !DateTime.TryParseExact(cellValue, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                    {
                        errmsg = "クラス開始日に [ yyyy-mm-dd ] の様に入力してください。";
                    }
                    //変更したい開始日と終了日の比較
                    else if (DateTime.Compare(DateTime.ParseExact(rowMergeView1.CurrentRow.Cells["終了日"].Value.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture), DateTime.ParseExact(cellValue, "yyyy-MM-dd", CultureInfo.InvariantCulture)) <= 0)
                    {
                        errmsg = "終了日より未来日を入力できません。もう一度確認してください。";
                    }
                    break;
                case "クラス終了日":
                    //日付の形式と未入力チェック
                    if (string.IsNullOrWhiteSpace(cellValue) || !DateTime.TryParseExact(cellValue, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                    {
                        errmsg = "クラス終了日に[ yyyy-mm-dd ] の様に入力してください。";
                    }
                    //変更したい終了日と開始日の比較
                    else if (DateTime.Compare(DateTime.ParseExact(rowMergeView1.CurrentRow.Cells["開始日"].Value.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture), DateTime.ParseExact(cellValue, "yyyy-MM-dd", CultureInfo.InvariantCulture)) >= 0)
                    {
                        errmsg = "開始日より過去日を入力できません。もう一度確認してください。";
                    }
                    break;
            }
            if (errmsg != "")
            {
                ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(Tag)).toolStripStatusLabel2.Text = errmsg + string.Format("クラスコード[{0}]が変更失敗しました。", code_教室);
                ((Form1)(Tag)).reLoad = false;
                return false;
            }
            return true;
        }

        private void ClassHistoryCheck()
        {
            string column_name = rowMergeView1.SelectedCells[0].OwningColumn.HeaderText;
            string name_教室名 = rowMergeView1.CurrentRow.Cells["教室"].Value.ToString();
            string code_クラスコード = rowMergeView1.CurrentRow.Cells["クラスコード"].Value.ToString();
            string str_cmd = string.Format(@"SELECT
	                                ROW_NUMBER() OVER(ORDER BY 開始日) AS [No],
	                                *
                                FROM
	                                (
		                                SELECT
			                                CL.クラスコード,
			                                CL.教室コード,
			                                CLR.備考,
			                                CL.開始日,
			                                CL.終了日
		                                FROM
			                                HL_JUKUKANRI_クラス履歴 CL
			                                LEFT JOIN
				                                HL_JUKUKANRI_教室マスタ CLR
			                                ON	CL.教室コード = CLR.教室コード
		                                WHERE
			                                CL.有効 = '1'
	                                ) AS T1
                                WHERE T1.備考 = '{0}'", name_教室名);
            dt_ClassHistory = GetDatatable(str_cmd);

            string expres = string.Format("クラスコード = '{0}' ", code_クラスコード);
            //時間変更したい教室を、クラス履歴の中で検索して、テーブルを作成
            DataRow[] dr_check = dt_ClassHistory.Select(expres);
            int ClassNo = Convert.ToInt32(dr_check[0][0]);

            switch (column_name)
            {
                case "クラス開始日":
                    DateTime newstartdate = Convert.ToDateTime(rowMergeView1.CurrentCell.EditedFormattedValue.ToString());
                    DateTime enddate = Convert.ToDateTime(rowMergeView1.CurrentRow.Cells["終了日"].Value.ToString());

                    if (DateTime.Compare(newstartdate, enddate) < 0)
                    {
                        if (dt_ClassHistory.Rows.Count == 1)
                        {
                            check = "OK";
                        }
                        else if (dt_ClassHistory.Rows.Count >= 2)
                        {
                            if (ClassNo == 1)
                            {
                                check = DateTime.Compare(newstartdate, enddate) < 0 ? "OK" : "NG";
                            }
                            else
                            {
                                check = DateTime.Compare(newstartdate, Convert.ToDateTime(dt_ClassHistory.Rows[ClassNo - 2]["終了日"])) > 0 ? "OK" : "NG";
                            }
                        }
                    }
                    else
                    {
                        check = "NG";
                    }
                    break;
                case "クラス終了日":
                    DateTime newenddate = Convert.ToDateTime(rowMergeView1.CurrentCell.EditedFormattedValue.ToString());
                    DateTime startdate = Convert.ToDateTime(rowMergeView1.CurrentRow.Cells["開始日"].Value.ToString());

                    if (DateTime.Compare(newenddate, startdate) > 0)
                    {
                        if (dt_ClassHistory.Rows.Count == 1)
                        {
                            check = "OK";
                        }
                        else if (dt_ClassHistory.Rows.Count >= 2)
                        {
                            if (ClassNo == dt_ClassHistory.Rows.Count)
                            {
                                check = DateTime.Compare(newenddate, startdate) > 0 ? "OK" : "NG";
                            }
                            else if (ClassNo >= 1 && ClassNo <= dt_ClassHistory.Rows.Count - 1)
                            {
                                check = DateTime.Compare(newenddate, Convert.ToDateTime(dt_ClassHistory.Rows[ClassNo]["開始日"])) < 0 ? "OK" : "NG";
                            }
                        }
                    }
                    else
                    {
                        check = "NG";
                    }
                    break;
                default:
                    break;
            }
        }

        private List<string> NewClass_HistoryCheck(string new_教室コード)
        {
            string name_クラス = "";
            string ID_クラス = "";
            string start_date = "";
            string end_date = "";
            List<string> list = new List<string>();
            DateTime startdate_change = Convert.ToDateTime(rowMergeView1.CurrentRow.Cells["開始日"].Value);
            DateTime enddate_change = Convert.ToDateTime(rowMergeView1.CurrentRow.Cells["終了日"].Value);
            string str_cmd = String.Format (@"SELECT CL.クラスコード, CL.クラス名, CL.教室コード,CLR.備考, CL.開始日, CL.終了日 
                                FROM HL_JUKUKANRI_クラス履歴 CL
                                LEFT JOIN HL_JUKUKANRI_教室マスタ CLR
                                ON CL.教室コード= CLR.教室コード
                                WHERE CL.有効='1'
                                AND CL.教室コード = '{0}'
                                ORDER BY CL.開始日", new_教室コード);

            DataTable NewClass_History = new DataTable();
            NewClass_History = GetDatatable(str_cmd);

            if (NewClass_History.Rows.Count == 0)
            {
                check = "OK";
            }
            else if (NewClass_History.Rows.Count > 0)
            {
                for (int i = 0; i < NewClass_History.Rows.Count; i++)
                {
                    if ((Convert.ToDateTime(NewClass_History.Rows[i]["開始日"]) <= startdate_change && startdate_change <= Convert.ToDateTime(NewClass_History.Rows[i]["終了日"])) ||
                        (Convert.ToDateTime(NewClass_History.Rows[i]["開始日"]) <= enddate_change && enddate_change <= Convert.ToDateTime(NewClass_History.Rows[i]["終了日"])) ||
                        (Convert.ToDateTime(NewClass_History.Rows[i]["開始日"]) >= startdate_change && enddate_change >= Convert.ToDateTime(NewClass_History.Rows[i]["終了日"])))
                    {
                        check = "NG";
                        name_クラス = NewClass_History.Rows[i]["クラス名"].ToString();
                        ID_クラス= NewClass_History.Rows[i]["クラスコード"].ToString();
                        start_date= NewClass_History.Rows[i]["開始日"].ToString();
                        end_date= NewClass_History.Rows[i]["終了日"].ToString();
                        list.Add(name_クラス);
                        list.Add(ID_クラス);
                        list.Add(start_date);
                        list.Add(end_date);
                        break;
                    }
                    else
                    {
                        check = "OK";
                    }
                }
            }

            return list;
        }

        private List<string> NewTeacher_HistoryCheck(string new_教師コード)
        {
            string start_date = "";
            string end_date = "";
            string name_クラス = "";
            string ID_クラス = "";
            List<string> list = new List<string>();
            DateTime startdate_change = Convert.ToDateTime(rowMergeView1.CurrentRow.Cells["開始日"].Value);
            DateTime enddate_change = Convert.ToDateTime(rowMergeView1.CurrentRow.Cells["終了日"].Value);

            string str_cmd = String.Format (@"select
                                    cl.クラス名,
	                                cl.クラスコード,
	                                cl.教師コード,
	                                a.名前,
	                                cl.開始日,
	                                cl.終了日,
	                                cl.有効
                                from
	                                HL_JUKUKANRI_クラス履歴 cl
	                                left join
		                                (
			                                select
				                                si.社員コード,
				                                si.名前
			                                from
				                                HL_JUKUKANRI_社内社員教師マスタ si
			                                union
			                                select
				                                sg.教師コード,
				                                sg.名前
			                                from
				                                HL_JUKUKANRI_社外教師マスタ sg
		                                ) a
	                                on	cl.教師コード = a.社員コード
                                where
	                                cl.有効 = '1'
                                and	cl.教師コード = '{0}'
                                order by
	                                cl.開始日", new_教師コード);
            DataTable teacher_info = new DataTable();
            teacher_info = GetDatatable(str_cmd);

            if (teacher_info.Rows.Count == 0)
            {
                check = "OK";
            }
            else if (teacher_info.Rows.Count > 0)
            {
                for (int i = 0; i < teacher_info.Rows.Count; i++)
                {
                    if ((Convert.ToDateTime(teacher_info.Rows[i]["開始日"]) <= startdate_change && startdate_change <= Convert.ToDateTime(teacher_info.Rows[i]["終了日"])) ||
                        (Convert.ToDateTime(teacher_info.Rows[i]["開始日"]) <= enddate_change && enddate_change <= Convert.ToDateTime(teacher_info.Rows[i]["終了日"])) ||
                        (Convert.ToDateTime(teacher_info.Rows[i]["開始日"]) >= startdate_change && enddate_change >= Convert.ToDateTime(teacher_info.Rows[i]["終了日"])))
                    {
                        start_date = teacher_info.Rows[i]["開始日"].ToString();
                        end_date = teacher_info.Rows[i]["終了日"].ToString();
                        ID_クラス= teacher_info.Rows[i]["クラスコード"].ToString();
                        name_クラス = teacher_info.Rows[i]["クラス名"].ToString();
                        check = "NG";
                        list.Add(start_date);
                        list.Add(end_date);
                        list.Add(name_クラス);
                        list.Add(ID_クラス);
                        break;
                    }
                    else
                    {
                        check = "OK";
                    }
                }
            }
            return list;
        }

        private void cmb_学生_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayGridView();
        }
    }
}