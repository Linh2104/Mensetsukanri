using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using System.Collections;

namespace HL_塾管理
{
    public partial class 前提知識 : DockContent
    {
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        //DB接続情報
        private string connectionString = ComClass.connectionString;
        TreeNode parentNode = null;
        static int selectedNode;
        private int count_検索 = 0;



        public 前提知識()
        {
            InitializeComponent();
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

        private void 前提知識_Load(object sender, EventArgs e)
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
	                                            画面 = '前提知識'
                                            AND 有効 = '1'
                                            AND	ID親 = '0'");

            DataTable dt_前提知識_Root = new DataTable();
            dt_前提知識_Root = GetDatatable(sqlcmd);

            if (dt_前提知識_Root.Rows.Count == 0)
            {
                contextMenuStrip1.Items[1].Visible = false;
                contextMenuStrip1.Items[2].Visible = false;
                return;
            }

            treeView1.Nodes.Clear();

            foreach (DataRow dr in dt_前提知識_Root.Rows)
            {
                parentNode = treeView1.Nodes.Add(dr["項目"].ToString());
                PopulateTreeView(Convert.ToInt32(dr["ID項目"].ToString()), parentNode);
            }
            treeView1.ExpandAll();
        }

        /// <summary>
        /// TreeViewのNodesを設定
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="parentNode"></param>
        private void PopulateTreeView(int parentId, TreeNode parentNode)
        {
            string sqlcmd = String.Format ( @"SELECT
                                                ID項目,
	                                            項目,
	                                            ID親
                                            FROM

                                                HL_JUKUKANRI_面接項目管理
                                            WHERE

                                                画面 = '前提知識'
                                            AND ID親 = '{0}'", parentId);
            DataTable dt_前提知識_Node = new DataTable();
            dt_前提知識_Node = GetDatatable(sqlcmd);
            TreeNode ChildNode;

            foreach (DataRow dr in dt_前提知識_Node.Rows)
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

        List<string> ListChild = new List<string>();
        private List<string> GetChildID( int ParentID)
        {
            string sqlcmd = String.Format(@"SELECT
                                                ID項目,
	                                            項目,
	                                            ID親
                                            FROM

                                                HL_JUKUKANRI_面接項目管理
                                            WHERE

                                                画面 = '前提知識'
                                            AND ID親 = '{0}'", ParentID);
            DataTable dtchildlist = new DataTable();
            dtchildlist = GetDatatable(sqlcmd);
            if (dtchildlist.Rows.Count >0)
            {
                foreach (DataRow drow in dtchildlist.Rows)
                {
                    ListChild.Add(drow["ID項目"].ToString());

                    GetChildID(Convert.ToInt32(drow["ID項目"]));
                }
            }
            return ListChild;
        }



        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            txt_備考.Visible = true;
            string sqlcmd = String.Format(@"SELECT
	                                            ID項目,
	                                            備考
                                            FROM
	                                            HL_JUKUKANRI_面接項目管理
                                            WHERE
	                                            画面 = '前提知識'
                                            AND 有効 = '1'
                                            AND	項目 = '{0}'", e.Node.Text);

            DataTable dt_TreeViewSelect = new DataTable();
            dt_TreeViewSelect = GetDatatable(sqlcmd);
            selectedNode = Convert.ToInt32(dt_TreeViewSelect.Rows[0]["ID項目"]);
            txt_備考.Text = dt_TreeViewSelect.Rows[0]["備考"].ToString();
        }

        private void btn_検索実行_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_検索欄.Text))
            {
                TreeViewLoad();
                ((Form1)(Tag)).toolStripStatusLabel2.Text = "";
                txt_備考.Text = "";
                txt_検索欄.Text = "";
            }
            else
            {
                string Search_Key = txt_検索欄.Text.Trim();
                txt_備考.SelectAll();
                txt_備考.SelectionBackColor = Color.White;

                    if (treeView1.Nodes.Count > 0)
                    {
                        if (SearchRecursive(treeView1.Nodes, Search_Key))
                        {
                            treeView1.SelectedNode.Expand();
                            treeView1.Focus();
                        }
                    }

                if (!string.IsNullOrEmpty(txt_備考.Text))
                {
                    int index = 0;

                    while (index < txt_備考.Text.LastIndexOf(txt_検索欄.Text))
                    {
                        txt_備考.Find(txt_検索欄.Text, index, txt_備考.Text.Length, RichTextBoxFinds.None);
                        txt_備考.SelectionBackColor = Color.Yellow;
                        index = txt_備考.Text.IndexOf(txt_備考.Text, index) + 1;

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
                    //node.Text.Select();

                    count_検索++;
                }

                if (SearchRecursive(node.Nodes, SearchKey))
                {
                    return true;
                }
            }

            return false;
        }


        private void 項目追加ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 変更ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 削除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show(String.Format("項目を削除すると、項目の中身も削除されます。\r\n 削除しますか？"), "削除", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (res == DialogResult.OK)
            {
                int ID項目 = selectedNode;
                int result = 0;
                string check = "";

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

                ListChild.Clear();
                GetChildID(ID項目);
                string ID_項目 = string.Join(",", ListChild);
                try
                {
                    sqlcom.CommandText = string.Format("Update HL_JUKUKANRI_面接項目管理 Set 有効 = 0 Where ID項目 in ('{0}')", ID_項目);
                    result = sqlcom.ExecuteNonQuery();

                    if (result == ListChild.Count)
                    {
                        transaction.Commit();

                        TreeViewLoad();
                        ((Form1)(Tag)).toolStripStatusLabel2.ForeColor = Color.Green;
                        ((Form1)(Tag)).toolStripStatusLabel2.Text = string.Format("データを正常に削除しました。");
                    }
                   
                    
                }
                catch (Exception) 
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
        }

        private void txt_検索欄_TextChanged(object sender, EventArgs e)
        {

        }

        private void 前提知識_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form1)(Tag)).m_前提知識Handle = IntPtr.Zero;
            ((Form1)(Tag)).toolStripStatusLabel2.Text = "";
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (treeView1.SelectedNode.Level >2)
            {
                contextMenuStrip1.Visible = false;
            }
            else
            {
                if (e.Button == MouseButtons.Right)
                {
                    treeView1.SelectedNode = e.Node;
                    contextMenuStrip1.Show(treeView1, e.Location);
                    string sqlcmd = String.Format(@"SELECT
	                                            ID項目,
	                                            備考
                                            FROM
	                                            HL_JUKUKANRI_面接項目管理
                                            WHERE
	                                            画面 = '前提知識'
                                            AND 有効 = '1'
                                            AND	項目 = '{0}'", e.Node.Text);

                    DataTable dt_TreeViewSelect = new DataTable();
                    dt_TreeViewSelect = GetDatatable(sqlcmd);
                    selectedNode = Convert.ToInt32(dt_TreeViewSelect.Rows[0]["ID項目"]);
                }
            }
        }

        private void 前提知識_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                panel1.Height = this.Height;
                pnl_検索.Height = this.Height;
            }
        }
    }
}
