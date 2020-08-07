using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;


namespace HL_塾管理
{
    public partial class 面接練習 : DockContent
    {
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        //DB接続情報
        private string connectionString = ComClass.connectionString;
        TreeNode parentNode = null;
        static int selectedNode;
        private int count_検索 = 0;

        public 面接練習()
        {
            InitializeComponent();
        }


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

        private void 面接練習_Load(object sender, EventArgs e)
        {
            TreeViewLoad();
        }

        /// <summary>
        /// TreeView のRootsを設定
        /// </summary>
        private void TreeViewLoad()
        {
            string sqlcmd = string.Format(@"SELECT
	                                            ID項目,
	                                            項目,
	                                            ID親
                                            FROM
	                                            HL_JUKUKANRI_面接項目管理
                                            WHERE
	                                            画面 = '面接練習'
                                            AND 有効 = '1'
                                            AND	ID親 = '0'");

            DataTable dt_面接練習_Root = new DataTable();
            dt_面接練習_Root = GetDatatable(sqlcmd);

            if (dt_面接練習_Root.Rows.Count == 0)
            {
                contextMenuStrip1.Items[1].Visible = false;
                contextMenuStrip1.Items[2].Visible = false;
                return;
            }

            treeView1.Nodes.Clear();

            foreach (DataRow dr in dt_面接練習_Root.Rows)
            {
                parentNode = treeView1.Nodes.Add(dr["項目"].ToString());
                PopulateTreeView(Convert.ToInt32(dr["ID項目"].ToString()), parentNode);
            }
            treeView1.ExpandAll();
        }
        List<int> List_子項目 = new List<int>();
        /// <summary>
        /// TreeViewのNodesを設定
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="parentNode"></param>
        private void PopulateTreeView(int parentId, TreeNode parentNode)
        {
            string sqlcmd = String.Format(@"SELECT
                                                ID項目,
	                                            項目,
	                                            ID親
                                            FROM

                                                HL_JUKUKANRI_面接項目管理
                                            WHERE
                                                画面 = '面接練習'
                                            AND 有効 = '1'
                                            AND ID親 = '{0}'", parentId);
            DataTable dt_面接練習_Node = new DataTable();
            dt_面接練習_Node = GetDatatable(sqlcmd);
            TreeNode ChildNode;
            if (dt_面接練習_Node.Rows.Count == 0)
            {
                List_子項目.Add(parentId);
                string sqlcmd2 = string.Format(@"select
	                                                b.ID質問, b.質問 , b.親ID , b.備考
                                                from HL_JUKUKANRI_質問また案件管理 b
                                                where b.有効 = '1'
                                                and b.親ID = '{0}'", parentId);
                DataTable dt_質問 = new DataTable();
                dt_質問 = GetDatatable(sqlcmd2);

                if (dt_質問.Rows.Count > 0)
                {
                    foreach (DataRow dr質問 in dt_質問.Rows)
                    {
                        ChildNode = parentNode.Nodes.Add(dr質問["質問"].ToString());
                    }
                }
            }
            else
            {
                //TreeNode ChildNode;

                foreach (DataRow dr in dt_面接練習_Node.Rows)
                {
                    if (parentNode == null)
                    {
                        ChildNode = treeView1.Nodes.Add(dr["項目"].ToString());
                    }
                    else
                    {
                        ChildNode = parentNode.Nodes.Add(dr["項目"].ToString());
                    }
                    PopulateTreeView(Convert.ToInt32(dr["ID項目"].ToString()), ChildNode);
                }
            }
            
        }

        List<string> ListChild_項目 = new List<string>();
        List<string> ListChild_質問 = new List<string>();
        private List<string> GetChildID_項目(int ParentID)
        {
            string sqlcmd = String.Format(@"SELECT
                                                ID項目,
	                                            項目,
	                                            ID親
                                            FROM
                                                HL_JUKUKANRI_面接項目管理
                                            WHERE
                                                画面 = '面接練習'
                                            AND ID親 = '{0}'", ParentID);
            DataTable dtchildlist_項目 = new DataTable();
            dtchildlist_項目 = GetDatatable(sqlcmd);
            if (dtchildlist_項目.Rows.Count > 0)
            {
                foreach (DataRow drow_項目 in dtchildlist_項目.Rows)
                {
                    ListChild_項目.Add(drow_項目["ID項目"].ToString());


                    GetChildID_項目(Convert.ToInt32(drow_項目["ID項目"]));
                }
            }

            return ListChild_項目;

        }
        private List<string> GetChildID_質問(List<string> List_ChildID_項目)
        {
            foreach (var item in List_ChildID_項目)
            {
                string sqlcmd2 = String.Format(@"SELECT ID質問, 質問, 親ID FROM HL_JUKUKANRI_質問また案件管理 WHERE 有効 = '1' AND 親ID = '{0}'", item);
                DataTable dtchildlist_質問 = new DataTable();
                dtchildlist_質問 = GetDatatable(sqlcmd2);
                if (dtchildlist_質問.Rows.Count > 0)
                {
                    foreach (DataRow drow_質問 in dtchildlist_質問.Rows)
                    {
                        ListChild_質問.Add(drow_質問["ID質問"].ToString());
                    }
                }
            }

            return ListChild_質問;
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            treeView1.SelectedNode = e.Node;

            if (treeView1.SelectedNode.Level > 2)
            {
                contextMenuStrip1.Visible = false;
            }
            else
            {
                string sqlcmd = "";
                DataTable dt_TreeViewSelect = new DataTable();

                if (treeView1.SelectedNode.Level < 2)
                {
                    sqlcmd = String.Format(@"SELECT
	                                            a.ID項目,
	                                            a.備考
                                            FROM
	                                            HL_JUKUKANRI_面接項目管理 a
                                            WHERE
	                                            a.画面 = '面接練習'
                                            AND a.有効 = '1'
                                            AND	a.項目 = '{0}'", e.Node.Text);
                    dt_TreeViewSelect = GetDatatable(sqlcmd);

                    if (e.Button == MouseButtons.Right)
                    {
                        contextMenuStrip1.Show(treeView1, e.Location);
                        selectedNode = Convert.ToInt32(dt_TreeViewSelect.Rows[0]["ID項目"]);

                        if (treeView1.SelectedNode.Level == 0)
                        {
                            contextMenuStrip1.Items[0].Visible = true;
                            contextMenuStrip1.Items[0].Text = "項目追加";
                            contextMenuStrip1.Items[1].Text = "項目変更";
                            contextMenuStrip1.Items[2].Text = "項目削除";
                        }
                        else if (treeView1.SelectedNode.Level == 1)
                        {
                            contextMenuStrip1.Items[0].Visible = true;
                            contextMenuStrip1.Items[0].Text = "質問追加";
                            contextMenuStrip1.Items[1].Text = "項目変更";
                            contextMenuStrip1.Items[2].Text = "項目削除";
                        }

                    }
                    else if (e.Button == MouseButtons.Left)
                    {
                        rchtxt_備考.Text = dt_TreeViewSelect.Rows[0]["備考"].ToString();
                    }
                }
                else if (treeView1.SelectedNode.Level == 2)
                {
                    sqlcmd = String.Format(@"select
	                                            b.ID質問, b.備考
                                            from HL_JUKUKANRI_質問また案件管理 b
                                            where b.有効 = '1'
                                            and b.質問 = '{0}'", e.Node.Text);
                    dt_TreeViewSelect = GetDatatable(sqlcmd);

                    if (e.Button == MouseButtons.Right)
                    {
                        contextMenuStrip1.Show(treeView1, e.Location);
                        selectedNode = Convert.ToInt32(dt_TreeViewSelect.Rows[0]["ID質問"]);

                        contextMenuStrip1.Items[0].Visible = false;
                        contextMenuStrip1.Items[1].Text = "質問変更";
                        contextMenuStrip1.Items[2].Text = "質問削除";

                    }
                    else if (e.Button == MouseButtons.Left)
                    {
                        rchtxt_備考.Text = dt_TreeViewSelect.Rows[0]["備考"].ToString();
                    }
                }
            }
        }
        private void 削除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sqlcmd = "";
            int result;

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

            SqlCommand sqlcom = new SqlCommand();
            sqlcom.Connection = sqlcon;
            SqlTransaction transaction = sqlcon.BeginTransaction();
            sqlcom.Transaction = transaction;

            if (contextMenuStrip1.Items[2].Text == "質問削除")
            {
                try
                {
                    sqlcmd = String.Format(@"Update HL_JUKUKANRI_質問また案件管理 Set 有効 = '0' Where ID質問= '{0}'", selectedNode);

                    sqlcom.CommandText = sqlcmd;
                    result = sqlcom.ExecuteNonQuery();

                    if (result == 1)
                    {

                        TreeViewLoad();
                        //lstv_質問.Items.RemoveAt(lstv_質問.SelectedIndices[0]);
                        rchtxt_備考.Text = "";

                        transaction.Commit();


                        ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Green;
                        ((Form1)(Tag)).toolStripStatusLabel2.Text = string.Format("データを正常に削除しました。");
                    }

                }
                catch
                {
                    transaction.Rollback();

                    ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                    ((Form1)(Tag)).toolStripStatusLabel2.Text = string.Format("データを削除出来ませんでした。");
                    return;
                }
                finally
                {
                    sqlcon?.Close();
                }

            }
            else if (contextMenuStrip1.Items[2].Text == "項目削除")
            {
                DialogResult res = MessageBox.Show(String.Format("項目を削除すると、項目の中身も削除されます。\r\n 削除しますか？"), "削除", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (res == DialogResult.OK)
                {
                    ListChild_項目.Clear();
                    ListChild_質問.Clear();
                    ListChild_項目.Add(selectedNode.ToString());
                    GetChildID_項目(selectedNode);
                    GetChildID_質問(ListChild_項目);

                    string ID_項目 = string.Join(",", ListChild_項目);
                    string ID_質問 = string.Join(",", ListChild_質問);
                    try
                    {
                        sqlcmd = String.Format(@"Update HL_JUKUKANRI_面接項目管理 set 有効 = '0' Where ID項目 in ({0}) ", ID_項目);

                        sqlcom.CommandText = sqlcmd;

                        result = sqlcom.ExecuteNonQuery();

                        if (result == 1)
                        {
                            string sqlcmd2 = String.Format(@"Update HL_JUKUKANRI_質問また案件管理 set 有効 = '0' Where ID質問 in ({0}) ", ID_質問);

                            sqlcom.CommandText = sqlcmd2;
                            result = sqlcom.ExecuteNonQuery();

                            if (result == ListChild_質問.Count)
                            {
                                transaction.Commit();

                                TreeViewLoad();
                                ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Green;
                                ((Form1)(Tag)).toolStripStatusLabel2.Text = string.Format("データを正常に削除しました。");
                            }
                        }
                    }
                    catch
                    {
                        transaction.Rollback();

                        ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                        ((Form1)(Tag)).toolStripStatusLabel2.Text = string.Format("データを削除出来ませんでした。");
                        return;
                    }
                    finally
                    {
                        sqlcon?.Close();
                    }

                }
                else if (res == DialogResult.Cancel)
                {
                    return;
                }
            }
        }


        private void 面接練習_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form1)(Tag)).m_面接練習Handle = IntPtr.Zero;
            ((Form1)(Tag)).toolStripStatusLabel2.Text = "";
        }

        private void btn_検索実行_Click(object sender, EventArgs e)
        {
            count_検索 = 0;
            TreeViewLoad();
            rchtxt_備考.SelectAll();
            rchtxt_備考.SelectionBackColor = Color.White;

            if (string.IsNullOrWhiteSpace(txt_検索欄.Text))
            {
                ((Form1)(Tag)).toolStripStatusLabel2.Text = "";
                rchtxt_備考.Text = "";
                txt_検索欄.Text = "";
            }
            else
            {
                string Search_Key = txt_検索欄.Text.Trim();

                if (treeView1.Nodes.Count > 0)
                {
                    if (SearchRecursive(treeView1.Nodes, Search_Key))
                    {
                        treeView1.SelectedNode.Expand();
                        treeView1.Focus();
                    }
                }

                if (!string.IsNullOrWhiteSpace(rchtxt_備考.Text))
                {
                    int index = 0;

                    while (index< rchtxt_備考.Text.LastIndexOf(txt_検索欄.Text))
                    {
                        
                        rchtxt_備考.Find(txt_検索欄.Text, index, rchtxt_備考.Text.Length, RichTextBoxFinds.None);
                        rchtxt_備考.SelectionBackColor = Color.Yellow;
                        index = rchtxt_備考.Text.IndexOf(txt_検索欄.Text, index) + 1;
                        count_検索++;
                    }
                }

                lblMsg.Text = String.Format("検索結果： {0} 件", count_検索);
            }
        }

        private bool SearchRecursive(IEnumerable nodes, string SearchKey)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Text.Contains(SearchKey))
                {
                    //treeView1.SelectedNode = node;
                    node.BackColor = Color.Yellow;

                    count_検索++;
                }

                if (SearchRecursive(node.Nodes, SearchKey))
                {
                    return true;
                }
            }
             
            return false;
        }

    }
}
