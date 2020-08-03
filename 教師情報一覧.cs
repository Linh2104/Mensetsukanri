using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace HL_塾管理
{
    public partial class 教師情報一覧 : DockContent
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int BringWindowToTop(IntPtr hWnd);
        private AutoSizeFormClass asc = new AutoSizeFormClass();

        //DB接続情報
        private string connectionString = ComClass.connectionString;

        //画面項目
        //教師コード
        public string code_教師コード = string.Empty;
        //教師名
        private string name_教師 = string.Empty;
        //会社
        private string company = string.Empty;
        //メイン言語
        private string language = string.Empty;
        //入職日
        private string date_入職日txt = string.Empty;
        //退職日
        private string date_退職日txt = string.Empty;
        //国籍
        private string nationality = string.Empty;
        //電話番号
        private string tel = string.Empty;
        //メール
        private string mail = string.Empty;


        //ユーザー職務
      　private string usercheck = "admin";
        //編集開始フラッグ
        private bool change = false;

        public 教師情報一覧()
        {
            InitializeComponent();
        }

        /// <summary>
        /// データが変更することがあったら、DGVが自動的に再表示処理
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
        private void  Usercheck()
        {
            if(((Form1)(this.Tag)).m_ユーザ登録.m_ログイン_職務 == "管理者")
            {
                usercheck = "admin";
            }

            if (((Form1)(this.Tag)).m_ユーザ登録.m_ログイン_職務 == "一般ユーザ")
            {
                usercheck = "user";
            }
        }

        /// <summary>
        /// 画面表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void 教師情報一覧_Load(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);

            Usercheck();
            if (usercheck == "user")
            {
                contextMenuStrip1.Items[0].Visible = false;
                contextMenuStrip1.Items[1].Visible = false;
            }
            

            //一覧表示
            DisplayGridView();
        }

        /// <summary>
        /// セルをクリックする時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gv_teachersInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //列がない場合
            if (e.RowIndex == -1)
            {
                return;
            }
          
        }

        /// <summary>
        /// セルの値を変更する処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gv_teachersInfo_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //データがない、またはセル変更なしの場合
            if (e.RowIndex == -1 || !change)
            {
                return;
            }

            //職務チェック
            Usercheck();

            gv_teachersInfo.CurrentCell.Selected = true;
            code_教師コード = gv_teachersInfo.CurrentRow.Cells["教師コード"].Value.ToString();

            if (code_教師コード != ((Form1)(this.Tag)).m_ユーザ登録.m_ログイン_教師コード && usercheck == "user")
            {
                ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "自分のみの情報しか変更することができない。";
                ((Form1)(this.Tag)).reLoad = false;
                return;
            }

            //Data取得
            code_教師コード = gv_teachersInfo.Rows[e.RowIndex].Cells["教師コード"].Value.ToString();
            name_教師 = gv_teachersInfo.Rows[e.RowIndex].Cells["名前"].Value == null ? "" : gv_teachersInfo.Rows[e.RowIndex].Cells["名前"].Value.ToString().Trim();
            company = gv_teachersInfo.Rows[e.RowIndex].Cells["所属会社"].Value == null ? "" : gv_teachersInfo.Rows[e.RowIndex].Cells["所属会社"].Value.ToString().Trim();
            language = gv_teachersInfo.Rows[e.RowIndex].Cells["メイン言語"].Value.ToString();
            date_入職日txt = gv_teachersInfo.Rows[e.RowIndex].Cells["入職日"].Value.ToString();
            date_退職日txt = gv_teachersInfo.Rows[e.RowIndex].Cells["退職日"].Value.ToString();
            
            nationality = gv_teachersInfo.Rows[e.RowIndex].Cells["国籍"].Value == null ? "" : gv_teachersInfo.Rows[e.RowIndex].Cells["国籍"].Value.ToString();
            tel = gv_teachersInfo.Rows[e.RowIndex].Cells["携帯"].Value == null ? "" : gv_teachersInfo.Rows[e.RowIndex].Cells["携帯"].Value.ToString();
            mail = gv_teachersInfo.Rows[e.RowIndex].Cells["メール"].Value == null ? "" : gv_teachersInfo.Rows[e.RowIndex].Cells["メール"].Value.ToString();
            //date_開始日txt = gv_teachersInfo.Rows[e.RowIndex].Cells["開始日"].Value.ToString();
            //date_終了日txt = gv_teachersInfo.Rows[e.RowIndex].Cells["終了日"].Value.ToString();

            if (!入力Check())
            {
                ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Black;
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "";
                return;
            }
            
            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库

            try
            {
                sqlcon.Open();
            }
            catch
            {
                statusLbl.ForeColor = Color.Red;
                statusLbl.Text = "サーバーがつなげない！インターネット接続をチェックしてください。";
                return;
            }

            SqlTransaction traction = sqlcon.BeginTransaction();

            try
            {
                SqlCommand sqlcom = new SqlCommand();
                sqlcom.Connection = sqlcon;
                sqlcom.Transaction = traction;

                //更新行う
                //教師情報一覧更新
                string sqlCommand = @"Update "
                                  + " HL_JUKUKANRI_教師情報 "
                                  + " Set "
                                  + " 名前 = N'{1}' "
                                  + " , 所属会社 = N'{2}' "
                                  + " , メイン言語 = N'{3}' "
                                   + " , 入塾日 = '{4}' "
                                  + " , 退塾日 =";

                                  sqlCommand += date_退職日txt == "-" ?
                            "null Where 教師コード = '{0}'" :
                            @"'" + date_退職日txt + "' Where 教師コード = '{0}'";

                sqlcom.CommandText = string.Format(sqlCommand, code_教師コード, name_教師, company, language,date_入職日txt);

                int result = sqlcom.ExecuteNonQuery();
                if (result == 1)
                {
                    if (code_教師コード.StartsWith("K"))
                    {
                        //社外教師情報更新
                        sqlCommand = @"Update "
                                   + " HL_JUKUKANRI_社外教師マスタ "
                                   + " Set "
                                   + " 名前 = N'{1}' "
                                   + " , 所属会社 = N'{2}' "
                                   + " , メイン言語 = N'{3}' "
                                   + " , 入塾日 = '{4}' "
                                   + " , 国籍 = N'{5}' "
                                   + " , 携帯 = '{6}' "
                                   + " , メール = '{7}' "
                                   + " , 退塾日 = ";

                        sqlCommand += date_退職日txt == "-" ?
                            "null Where 教師コード = '{0}'" :
                            @"'" + date_退職日txt + "' Where 教師コード = '{0}'";

                        sqlcom.CommandText = string.Format(sqlCommand, code_教師コード, name_教師, company, language, date_入職日txt, nationality, tel, mail);
                    }
                    else
                    {
                        //社員から教師へ変更の更新
                        sqlCommand = @"Update "
                                   + " HL_JUKUKANRI_社内社員教師マスタ "
                                   + " Set "
                                   + " 名前 = N'{1}' "
                                   + " , メイン言語 = N'{2}' "
                                   + " , 入塾日 = '{3}' "
                                   + " , 退塾日 = ";

                        sqlCommand += date_退職日txt == "-" ?
                            "null Where 社員コード = '{0}'" :
                            @"'" + date_退職日txt + "' Where 社員コード = '{0}'";

                        sqlcom.CommandText = string.Format(sqlCommand, code_教師コード, name_教師, language, date_入職日txt);
                    }
                    result = sqlcom.ExecuteNonQuery();
                    if (result != 1)
                    {
                        throw new Exception();
                    }

                    if (!code_教師コード.StartsWith("K"))
                    {
                        sqlcom.CommandText = String.Format(@"UPDATE HL_JINJI_社員情報 SET 国籍 = N'{0}' , 携帯 = '{1}', メール = '{2}' WHERE 社員コード = '{3}' ", nationality, tel, mail, code_教師コード);
                        int result1 = sqlcom.ExecuteNonQuery();
                        if (result1 != 1)
                        {
                            throw new Exception();
                        }
                    }
                    ((Form1)(this.Tag)).reLoad = false;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Green;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = string.Format(@"教師[{0}]の情報が正常に更新されました。", code_教師コード);
                    traction.Commit();
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = string.Format(@"教師[{0}]の更新処理に失敗しました。" + ex.Message, code_教師コード);
                ((Form1)(this.Tag)).reLoad = false;
                traction.Rollback();
            }
            finally
            {
                if (sqlcon != null)
                {
                    sqlcon.Close();
                }
            }

            //DGV再表示
            DisplayGridView();
        }

        /// <summary>
        /// 検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisplayGridView()
        {
            if (((Form1)(this.Tag)).reLoad)
            {
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "";
            }

            change = false;

            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库
            
            try
            {
                sqlcon.Open();
            }
            catch
            {
                statusLbl.ForeColor = Color.Red;
                statusLbl.Text = "サーバーがつなげない！インターネット接続をチェックしてください。";
                return;
            }
            Usercheck();
            string sqlcmd = @"Select
                               T1.教師コード,
                                T1.名前,
                                T1.所属会社,
                                T1.メイン言語,
                                T1.入塾日,
                                T1.退塾日,
                                T3.クラスコード,
                                T3.クラス名,    
                                T2.国籍,
                                T2.携帯,
                                T2.メール
                            From
                                HL_JUKUKANRI_教師情報 As T1
                                Left Join
                                    (
                                        Select
                                            教師コード,
                                            入塾日,
                                            退塾日,
                                            国籍,
                                            携帯,
                                            メール
                                        From
                                            HL_JUKUKANRI_社外教師マスタ c
                                        Union
                                        (
                                            Select
                                                a.社員コード,
                                                a.入塾日,
                                                a.退塾日,
                                                b.国籍,
                                                b.携帯,
                                                b.メール
                                            From
                                                HL_JUKUKANRI_社内社員教師マスタ a
                                                inner join
                                                    HL_JINJI_社員情報 b
                                                on	a.社員コード = b.社員コード
                                        )
                                    ) As T2
                                On	T1.教師コード = T2.教師コード
                            Left join　(select
                                            DISTINCT c.教師コード,　c. 開始日 , c.クラスコード, c.クラス名 
                                        from
                                            HL_JUKUKANRI_クラス履歴 c 
                                            left join HL_JUKUKANRI_教師情報 b 
                                            on c.教師コード = b.教師コード 
                                        where
                                            (c.開始日 >= getdate() or c.終了日 >= getdate()) 
                                            and c.有効 = 1  
                                            and c.開始日 = ( select min(a.開始日) from HL_JUKUKANRI_クラス履歴 a where a.教師コード = c.教師コード)
                                   ) T3 on T1.教師コード = T3.教師コード
                            Where
                                T1.退塾日 >= GETDATE()
                            Or	T1.退塾日 is Null
                            Order By
                            LEFT(T1.教師コード, PATINDEX('%[0-9]%', T1.教師コード) - 1), -- alphabetical sort
                                CONVERT(INT, SUBSTRING(T1.教師コード, PATINDEX('%[0-9]%', T1.教師コード), LEN(T1.教師コード))) -- numerical sort";

            SqlCommand com = new SqlCommand(sqlcmd, sqlcon);
            SqlDataReader reader = com.ExecuteReader();

            int Index = 0;
            gv_teachersInfo.Rows.Clear();

            try
            {
                //一覧情報設定
                while (reader.Read())
                {
                    if (
                        (reader["教師コード"].ToString().IndexOf(this.txt_search.Text) < 0) && 
                        (reader["名前"].ToString().IndexOf(this.txt_search.Text) < 0) && 
                        (reader["所属会社"].ToString().IndexOf(this.txt_search.Text) < 0) && 
                        (reader["メイン言語"].ToString().IndexOf(this.txt_search.Text) < 0) && 
                        (reader["入塾日"].ToString().IndexOf(this.txt_search.Text) < 0) && 
                        (reader["退塾日"].ToString().IndexOf(this.txt_search.Text) < 0) &&
                        (reader["クラス名"].ToString().IndexOf(this.txt_search.Text) < 0) &&
                        (reader["国籍"].ToString().IndexOf(this.txt_search.Text) < 0) &&
                        (reader["携帯"].ToString().IndexOf(this.txt_search.Text) < 0) &&
                        (reader["メール"].ToString().IndexOf(this.txt_search.Text) < 0)

                        )
                    {
                        continue;
                    }
                    gv_teachersInfo.Rows.Add();
                   
                    gv_teachersInfo.Rows[Index].Cells["教師コード"].Value = reader["教師コード"].ToString();
                   
                    gv_teachersInfo.Rows[Index].Cells["名前"].Value = reader["名前"].ToString();
                    gv_teachersInfo.Rows[Index].Cells["所属会社"].Value = reader["所属会社"].ToString();
                    gv_teachersInfo.Rows[Index].Cells["メイン言語"].Value = reader["メイン言語"].ToString();
                    gv_teachersInfo.Rows[Index].Cells["入職日"].Value = ((DateTime)reader["入塾日"]).ToString("yyyy-MM-dd");
                    gv_teachersInfo.Rows[Index].Cells["退職日"].Value = string.IsNullOrWhiteSpace(reader["退塾日"].ToString()) ? "-" : ((DateTime)reader["退塾日"]).ToString("yyyy-MM-dd");
                    gv_teachersInfo.Rows[Index].Cells["クラスコード"].Value = reader["クラスコード"].ToString();
                    gv_teachersInfo.Rows[Index].Cells["クラス"].Value = reader["クラス名"].ToString();
                    gv_teachersInfo.Rows[Index].Cells["国籍"].Value = reader["国籍"].ToString();
                    gv_teachersInfo.Rows[Index].Cells["携帯"].Value = reader["携帯"].ToString();
                    gv_teachersInfo.Rows[Index].Cells["メール"].Value = reader["メール"].ToString();
                    

                    if (Usercheck(gv_teachersInfo.Rows[Index].Cells["教師コード"].Value.ToString()) == "社内" && (usercheck=="admin"|| gv_teachersInfo.Rows[Index].Cells["教師コード"].Value.ToString() == ((Form1)(this.Tag)).m_ユーザ登録.m_ログイン_教師コード))
                    {
                        gv_teachersInfo.Rows[Index].Cells["名前"].ReadOnly = true;
                        gv_teachersInfo.Rows[Index].Cells["名前"].Style.BackColor = Color.White;
                        gv_teachersInfo.Rows[Index].Cells["名前"].Style.ForeColor = Color.Black;

                        gv_teachersInfo.Rows[Index].Cells["所属会社"].ReadOnly = true;
                        gv_teachersInfo.Rows[Index].Cells["所属会社"].Style.BackColor = Color.White;
                        gv_teachersInfo.Rows[Index].Cells["所属会社"].Style.ForeColor = Color.Black;

                        gv_teachersInfo.Rows[Index].Cells["国籍"].ReadOnly = true;
                        gv_teachersInfo.Rows[Index].Cells["国籍"].Style.BackColor = Color.White;
                        gv_teachersInfo.Rows[Index].Cells["国籍"].Style.ForeColor = Color.Black;


                        gv_teachersInfo.Rows[Index].Cells["携帯"].ReadOnly = true;
                        gv_teachersInfo.Rows[Index].Cells["携帯"].Style.BackColor = Color.White;
                        gv_teachersInfo.Rows[Index].Cells["携帯"].Style.ForeColor = Color.Black;

                        gv_teachersInfo.Rows[Index].Cells["メール"].ReadOnly = true;
                        gv_teachersInfo.Rows[Index].Cells["メール"].Style.BackColor = Color.White;
                        gv_teachersInfo.Rows[Index].Cells["メール"].Style.ForeColor = Color.Black;

                    }
                    if(usercheck=="user"&& gv_teachersInfo.Rows[Index].Cells["教師コード"].Value.ToString()!= ((Form1)(this.Tag)).m_ユーザ登録.m_ログイン_教師コード)
                    {
                        gv_teachersInfo.Rows[Index].Cells["名前"].ReadOnly = true;
                        gv_teachersInfo.Rows[Index].Cells["名前"].Style.BackColor = Color.White;
                        gv_teachersInfo.Rows[Index].Cells["名前"].Style.ForeColor = Color.Black;

                        gv_teachersInfo.Rows[Index].Cells["所属会社"].ReadOnly = true;
                        gv_teachersInfo.Rows[Index].Cells["所属会社"].Style.BackColor = Color.White;
                        gv_teachersInfo.Rows[Index].Cells["所属会社"].Style.ForeColor = Color.Black;

                        gv_teachersInfo.Rows[Index].Cells["メイン言語"].ReadOnly = true;
                        gv_teachersInfo.Rows[Index].Cells["メイン言語"].Style.BackColor = Color.White;
                        gv_teachersInfo.Rows[Index].Cells["メイン言語"].Style.ForeColor = Color.Black;

                        gv_teachersInfo.Rows[Index].Cells["国籍"].ReadOnly = true;
                        gv_teachersInfo.Rows[Index].Cells["国籍"].Style.BackColor = Color.White;
                        gv_teachersInfo.Rows[Index].Cells["国籍"].Style.ForeColor = Color.Black;

                        gv_teachersInfo.Rows[Index].Cells["携帯"].ReadOnly = true;
                        gv_teachersInfo.Rows[Index].Cells["携帯"].Style.BackColor = Color.White;
                        gv_teachersInfo.Rows[Index].Cells["携帯"].Style.ForeColor = Color.Black;

                        gv_teachersInfo.Rows[Index].Cells["メール"].ReadOnly = true;
                        gv_teachersInfo.Rows[Index].Cells["メール"].Style.BackColor = Color.White;
                        gv_teachersInfo.Rows[Index].Cells["メール"].Style.ForeColor = Color.Black;

                        gv_teachersInfo.Rows[Index].Cells["入職日"].ReadOnly = true;
                        gv_teachersInfo.Rows[Index].Cells["入職日"].Style.BackColor = Color.White;
                        gv_teachersInfo.Rows[Index].Cells["入職日"].Style.ForeColor = Color.Black;

                        gv_teachersInfo.Rows[Index].Cells["退職日"].ReadOnly = true;
                        gv_teachersInfo.Rows[Index].Cells["退職日"].Style.BackColor = Color.White;
                        gv_teachersInfo.Rows[Index].Cells["退職日"].Style.ForeColor = Color.Black;


                    }
                    Index++;
                }
                //DGVの広さを設定
                int TotalColumnsWidth = 0;
                foreach (DataGridViewColumn dvgcol in gv_teachersInfo.Columns)
                {
                    if (dvgcol.Visible == true)
                    {
                        TotalColumnsWidth += dvgcol.Width;
                    }
                }
                gv_teachersInfo.Width = TotalColumnsWidth + gv_teachersInfo.RowHeadersWidth + 20;
            }
            catch(Exception ex)
            {
                statusLbl.ForeColor = Color.Red;
                statusLbl.Text = "検索処理に失敗しました。"+ex.Message;
                return;
            }
            finally
            {
                if (sqlcon != null)
                {
                    sqlcon.Close();
                    reader.Close();
                }
            }

            change = true;

            //件数表示
            statusLbl.ForeColor = Color.Black;
            statusLbl.Text = string.Format("{0}件", Index);
            ((Form1)(this.Tag)).reLoad = true;
        }

        /// <summary>
        /// 画面変更された時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 教師情報一覧_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form1)(this.Tag)).m_教師情報一覧Handle = IntPtr.Zero;
        }

        /// <summary>
        /// 社外教師情報登録画面へ遷移する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 社外教師情報登録ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((Form1)(this.Tag)).m_社外教師情報登録Handle != IntPtr.Zero)
            {
                BringWindowToTop(((Form1)(this.Tag)).m_社外教師情報登録Handle);
                return;
            }

            社外教師情報 m_NewForm_社外教師情報登録 = new 社外教師情報();
            m_NewForm_社外教師情報登録.isUpdate = false;
            m_NewForm_社外教師情報登録.Tag = ((Form1)(this.Tag));
            m_NewForm_社外教師情報登録.Show(((Form1)(this.Tag)).dockPanel1);
            ((Form1)(this.Tag)).m_社外教師情報登録Handle = m_NewForm_社外教師情報登録.Handle;
            //((Form1)(this.Tag)).toolStripStatusLabel2.Text = "";
        }

        /// <summary>
        /// 社員から教師へ登録画面へ遷移する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 社員から教師へ登録ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((Form1)(this.Tag)).m_社員から教師へ登録Handle != IntPtr.Zero)
            {
                BringWindowToTop(((Form1)(this.Tag)).m_社員から教師へ登録Handle);
                return;
            }

            社員から教師へ変更 m_NewForm_社員から教師へ登録 = new 社員から教師へ変更();
            m_NewForm_社員から教師へ登録.Tag = ((Form1)(this.Tag));
            m_NewForm_社員から教師へ登録.Show(((Form1)(this.Tag)).dockPanel1);
            ((Form1)(this.Tag)).m_社員から教師へ登録Handle = m_NewForm_社員から教師へ登録.Handle;
            //((Form1)(this.Tag)).toolStripStatusLabel2.Text = "";
        }

        /// <summary>
        /// 変更画面へ遷移する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 変更ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int value;
            Usercheck();
            code_教師コード = gv_teachersInfo.CurrentRow.Cells["教師コード"].Value.ToString();

            //職務チェック
            if (code_教師コード != ((Form1)(this.Tag)).m_ユーザ登録.m_ログイン_教師コード && usercheck == "user")
            {

                ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "自分のみの情報しか変更することができない。";
                ((Form1)(this.Tag)).reLoad = false;
                return;
            }

            if (code_教師コード.StartsWith("K"))
            {
                //1人の複数画面で開けないためチェック
                if (((Form1)(this.Tag)).codeDic.ContainsKey(code_教師コード))
                {
                    ((Form1)(this.Tag)).codeDic.TryGetValue(code_教師コード, out value);
                    BringWindowToTop((IntPtr)value);
                    return;
                }

                //社外教師情報変更画面を呼び出す
                社外教師情報 m_NewForm_社外教師情報変更 = new 社外教師情報();
                m_NewForm_社外教師情報変更.isUpdate = true;
                m_NewForm_社外教師情報変更.code_教師コード = code_教師コード;
                m_NewForm_社外教師情報変更.Tag = ((Form1)(this.Tag));
                m_NewForm_社外教師情報変更.Show(((Form1)(this.Tag)).dockPanel1);
                ((Form1)(this.Tag)).m_社外教師情報変更Handle = m_NewForm_社外教師情報変更.Handle;

                int ptr = (int)((Form1)(this.Tag)).m_社外教師情報変更Handle;

                //1人の複数画面で開けないチェックするためその教師のコード入れる。
                if (!((Form1)(this.Tag)).codeDic.ContainsKey(code_教師コード))
                {
                    ((Form1)(this.Tag)).codeDic.Add(code_教師コード, ptr);
                }
            }
            else
            {
                //1人の複数画面で開けないためチェック
                if (((Form1)(this.Tag)).codeDic.ContainsKey(code_教師コード))
                {
                    ((Form1)(this.Tag)).codeDic.TryGetValue(code_教師コード, out value);
                    BringWindowToTop((IntPtr)value);
                    return;
                }

                //社員から教師へ変更画面を呼び出す
                社員から教師へ変更 m_NewForm_社員から教師へ変更 = new 社員から教師へ変更();
                m_NewForm_社員から教師へ変更.isUpdate = true;
                m_NewForm_社員から教師へ変更.code_教師コード = code_教師コード;
                m_NewForm_社員から教師へ変更.Tag = ((Form1)(this.Tag));
                m_NewForm_社員から教師へ変更.Show(((Form1)(this.Tag)).dockPanel1);
                ((Form1)(this.Tag)).m_社員から教師へ変更Handle = m_NewForm_社員から教師へ変更.Handle;
                ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "";

                //1人の複数画面で開けないチェックするためその教師のコード入れる。
                int ptr = (int)((Form1)(this.Tag)).m_社員から教師へ変更Handle;
                if (!((Form1)(this.Tag)).codeDic.ContainsKey(code_教師コード))
                {
                    ((Form1)(this.Tag)).codeDic.Add(code_教師コード, ptr);
                }
            }
        }

        /// <summary>
        ///  一覧から選択行を削除処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 削除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gv_teachersInfo.CurrentCell != null)
            {
                gv_teachersInfo.CurrentCell.Selected = true;
                code_教師コード = gv_teachersInfo.CurrentRow.Cells["教師コード"].Value.ToString();

                //クラス担当中かどうかをチェック
                if (!string.IsNullOrWhiteSpace (gv_teachersInfo.CurrentRow.Cells["クラスコード"].Value.ToString()))
                {
                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = string.Format(@"該当教師[{0}]はクラス担当中であるため、削除できません。", code_教師コード);
                    ((Form1)(this.Tag)).reLoad = false;
                    return;
                }

                SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库

                try
                {
                    sqlcon.Open();
                }
                catch
                {
                    statusLbl.ForeColor = Color.Red;
                    statusLbl.Text = "サーバーがつなげない！インターネット接続をチェックしてください。";
                    return;
                }

                SqlTransaction traction = sqlcon.BeginTransaction();

                try
                {
                    SqlCommand sqlcom = new SqlCommand();
                    sqlcom.Connection = sqlcon;
                    sqlcom.Transaction = traction;

                     //削除行う
                    //教師情報一覧更新
                    sqlcom.CommandText = string.Format(@"Update HL_JUKUKANRI_教師情報 Set 退塾日 = '" + DateTime.Today + "' Where 教師コード = '{0}'", code_教師コード);
                    int result = sqlcom.ExecuteNonQuery();
                    if(result == 1)
                    {
                        //_社外教師マスタまた社員から教師へ変更更新
                        sqlcom.CommandText = code_教師コード.Substring(0, 1) == "K" ?
                          string.Format(@"Update HL_JUKUKANRI_社外教師マスタ Set 退塾日 = '" + DateTime.Today + "' Where 教師コード = '{0}'", code_教師コード) :
                          string.Format(@"Delete From HL_JUKUKANRI_社内社員教師マスタ Where 社員コード = '{0}'", code_教師コード);

                        result = sqlcom.ExecuteNonQuery();
                        if (result != 1)
                        {
                            throw new Exception();
                        }

                        ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Green;
                        ((Form1)(this.Tag)).toolStripStatusLabel2.Text = string.Format(@"教師[{0}]の情報が正常に削除されました。", code_教師コード);
                        ((Form1)(this.Tag)).reLoad = false;
                        traction.Commit();

                        gv_teachersInfo.Rows.Remove(this.gv_teachersInfo.CurrentRow);
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch (Exception ex)
                {
                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = string.Format(@"教師[{0}]の削除処理に失敗しました。" + ex.Message, code_教師コード);
                    ((Form1)(this.Tag)).reLoad = false;
                    traction.Rollback();
                }
                finally
                {
                    if (sqlcon != null)
                    {
                        sqlcon.Close();
                    }
                }
            }
            //DGV再表示
            DisplayGridView();
        }

        /// <summary>
        ///右クリックのメニューを開く処理 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            //メニューの箇所設定
            Point startPosition = Cursor.Position;

            Point point = this.gv_teachersInfo.PointToClient(startPosition);
            DataGridView.HitTestInfo hitinfo;
            hitinfo = this.gv_teachersInfo.HitTest(point.X, point.Y);

            this.gv_teachersInfo.ClearSelection();
            if (hitinfo.RowIndex >= 0)
            {
                this.gv_teachersInfo.Rows[hitinfo.RowIndex].Selected = true;
            }
            else
            {
                e.Cancel = true;
            }
            code_教師コード = gv_teachersInfo.CurrentRow.Cells["教師コード"].Value.ToString();
            if(code_教師コード== ((Form1)(this.Tag)).m_ユーザ登録.m_ログイン_教師コード)
            {
                contextMenuStrip1.Items[0].Visible = true;
            }
        }

        /// <summary>
        /// DGVのデータエラーの時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gv_teachersInfo_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (this.gv_teachersInfo.Rows[e.RowIndex].IsNewRow)
            {
                return;
            }
        }

        /// <summary>
        /// 検索ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_search_Click(object sender, EventArgs e)
        {
            DisplayGridView();
        }


        /// <summary>
        /// 入力チェック
        /// </summary>
        private bool 入力Check()
        {
            //名前チェック
            if (name_教師.Equals(""))
            {
                statusLbl.ForeColor = Color.Red;
                statusLbl.Text = "名前未入力!";
                return false;
            }
            else
            {
                if (name_教師.IndexOf(" ") > 0)
                {
                    statusLbl.ForeColor = Color.Red;
                    statusLbl.Text = "名前に許可されない文字'半角SPACE' が入りました。";
                    return false;
                }
                if (name_教師.IndexOf("　") == -1)
                {
                    statusLbl.ForeColor = Color.Red;
                    statusLbl.Text = "姓と名の間に '全角SPACE' を挿入してください!";
                    return false;
                }
                if (name_教師.IndexOf("　") != name_教師.LastIndexOf("　"))
                {
                    statusLbl.ForeColor = Color.Red;
                    statusLbl.Text = "名前に '全角SPACE' は二つ以上入力しないでください!";
                    return false;
                }
                if (name_教師.IndexOf(",") > 0)
                {
                    statusLbl.ForeColor = Color.Red;
                    statusLbl.Text = "名前に許可されない文字「,」が入りました。";
                    return false;
                }
            }

            //所属会社チェック
            if (company.Equals(""))
            {
                statusLbl.ForeColor = Color.Red;
                statusLbl.Text = "所属会社が未入力!";
                return false;
            }

            //日付チェック
            if (string.IsNullOrWhiteSpace(date_入職日txt))
            {
                statusLbl.ForeColor = Color.Red;
                statusLbl.Text = "入塾日は未入力。";
                return false;
            }
            if (!IsDataTime(date_入職日txt))
            {
                statusLbl.ForeColor = Color.Red;
                statusLbl.Text = "入塾日のフォーマットが間違っています!";
                return false;
            }
            if (date_退職日txt != "-")
            {
                if (!IsDataTime(date_退職日txt))
                {
                    statusLbl.ForeColor = Color.Red;
                    statusLbl.Text = "退塾日のフォーマットが間違っています!";
                    return false;
                }

                //入職日 < 退職日 
                if (Convert.ToDateTime(date_入職日txt) > Convert.ToDateTime(date_退職日txt))
                {
                    statusLbl.ForeColor = Color.Red;
                    statusLbl.Text = "[退塾日]は[入塾日]以後の日を設定してください。";
                    return false;
                }
            }

            //国籍チェック
            if (string.IsNullOrWhiteSpace(nationality))
            {
                statusLbl.ForeColor = Color.Red;
                statusLbl.Text = "国籍を未入力です。もう一度確認してください。";
                return false;
            }

            //携帯番号チェック
            if (string.IsNullOrWhiteSpace(tel))
            {
                statusLbl.ForeColor = Color.Red;
                statusLbl.Text = "携帯番号は未入力。";
                return false;
            }
            if (!IsTel(tel))
            {
                statusLbl.ForeColor = Color.Red;
                statusLbl.Text = "携帯番号のフォーマットが間違っています。もう一度確認してください。";
                return false;
            }


            //メールチェック
            if (string.IsNullOrWhiteSpace(mail))
            {
                statusLbl.ForeColor = Color.Red;
                statusLbl.Text = "メールは未入力。";
                return false;
            }
            if (!IsMail(mail))
            {
                statusLbl.ForeColor = Color.Red;
                statusLbl.Text = "メールのフォーマットが間違っています。もう一度確認してください。";
                return false;
            }
            
            statusLbl.ForeColor = Color.Black;
            statusLbl.Text = "";
            return true;
        }

        /// <summary>
        /// 日付の形式チェック
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        private bool IsDataTime(string strIn)
        {
            return Regex.IsMatch(strIn, @"((^((1[8-9]\d{2})|([2-9]\d{3}))([-\/\._])(10|12|0?[13578])([-\/\._])(3[01]|[12][0-9]|0?[1-9])$)|(^((1[8-9]\d{2})|([2-9]\d{3}))([-\/\._])(11|0?[469])([-\/\._])(30|[12][0-9]|0?[1-9])$)|(^((1[8-9]\d{2})|([2-9]\d{3}))([-\/\._])(0?2)([-\/\._])(2[0-8]|1[0-9]|0?[1-9])$)|(^([2468][048]00)([-\/\._])(0?2)([-\/\._])(29)$)|(^([3579][26]00)([-\/\._])(0?2)([-\/\._])(29)$)|(^([1][89][0][48])([-\/\._])(0?2)([-\/\._])(29)$)|(^([2-9][0-9][0][48])([-\/\._])(0?2)([-\/\._])(29)$)|(^([1][89][2468][048])([-\/\._])(0?2)([-\/\._])(29)$)|(^([2-9][0-9][2468][048])([-\/\._])(0?2)([-\/\._])(29)$)|(^([1][89][13579][26])([-\/\._])(0?2)([-\/\._])(29)$)|(^([2-9][0-9][13579][26])([-\/\._])(0?2)([-\/\._])(29)$))");
        }

        /// <summary>
        /// 電話番号の形式チェック
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        private bool IsTel(string strIn)
        {
            return Regex.IsMatch(strIn, @"^0\d{1,4}-\d{1,4}-\d{4}$");
        }

        /// <summary>
        /// メールの形式チェック
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        private bool IsMail (string strIn)
        {
            return Regex.IsMatch(strIn, @"\A[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\z",System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// セルクリックすると行の値を取得できる処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gv_teachersInfo_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    if ((this.gv_teachersInfo.Rows[e.RowIndex].Selected == false))
                    {
                        this.gv_teachersInfo.ClearSelection();
                        this.gv_teachersInfo.Rows[e.RowIndex].Selected = true;
                    }
                    //'只选中一行时设置活动单元格
                    if ((this.gv_teachersInfo.SelectedRows.Count == 1))
                    {
                        this.gv_teachersInfo.CurrentCell = this.gv_teachersInfo.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    }
                }
            }
        }

        /// <summary>
        /// 右メニューのクラス履歴の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void クラス履歴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Usercheck();
            code_教師コード = gv_teachersInfo.CurrentRow.Cells["教師コード"].Value.ToString();
            name_教師 = gv_teachersInfo.CurrentRow.Cells["名前"].Value.ToString();

            //1人の複数画面を表示チェック
            if (((Form1)(this.Tag)).m_教室マスタ登録Handle != IntPtr.Zero)
            {
                BringWindowToTop(((Form1)(this.Tag)).m_教室マスタ登録Handle);
                return;
            }

            //教師クラス履歴一覧画面を呼び出す
            教師クラス履歴一覧 m_教師クラス履歴一覧 = new 教師クラス履歴一覧();
            m_教師クラス履歴一覧.code_教師コード = code_教師コード;
            m_教師クラス履歴一覧.TeacherName = name_教師;
            m_教師クラス履歴一覧.Tag = ((Form1)(Tag));
            
            ((Form1)(Tag)).m_教師クラス履歴一覧Handle = m_教師クラス履歴一覧.Handle;
            m_教師クラス履歴一覧.ShowDialog();

        }

        private string Usercheck(string code)
        {
            string user = "";
            if (code.Substring(0, 1) == "K")
            {
                user = "社外";
            }
            else
            {
                user = "社内";
            }
            return user;
        }
        /// <summary>
        /// 一般ユーザ登録の場合、右クリックメニューを閉じると、「変更」選択肢はない。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStrip1_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            Usercheck();
            if (usercheck == "user")
            {
                contextMenuStrip1.Items[0].Visible = false;
            }
           
        }
    }
}
