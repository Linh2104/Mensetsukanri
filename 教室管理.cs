using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace HL_塾管理
{
    public partial class 教室管理 : DockContent
    {
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        private AutoSizeFormClass asc = new AutoSizeFormClass();

        //DB接続情報
        private string connectionString = ComClass.connectionString;
        //ERRMSGリスト
        private Dictionary<string, string> errmsg = new Dictionary<string, string>();
        //クラスコード
        public string classnum;
        //出勤機コード
        public string machinemun;
        //備考
        public string remark;
        //編集フラッグ
        public string isUpdate = "new";

        public 教室管理()
        {
            InitializeComponent();
        }

        //出勤機テーブル
        private DataTable dt;

        /// <summary>
        /// 出勤機CMB設定
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
            }

            string str_sqlcmd = @"SELECT 出勤機コード, 場所 FROM HL_JINJI_出勤機マスタ ORDER BY 出勤機コード";
            SqlDataAdapter sqlDa = new SqlDataAdapter(str_sqlcmd, sqlcon);

            dt = new DataTable();
            sqlDa.Fill(dt);

            cmb_出勤機コード.DisplayMember = "場所";
            cmb_出勤機コード.ValueMember = "出勤機コード";
            cmb_出勤機コード.DataSource = dt;
            if(isUpdate=="new")
            {
               cmb_出勤機コード.SelectedValue = 2;
            }
            else
            {
               cmb_出勤機コード.SelectedValue = machinemun;
            }
        }

        /// <summary>
        /// 検索ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_register_Click(object sender, EventArgs e)
        {
            classnum = txt_classroomnum.Text;
            //machinemun = cmb_出勤機コード.Text;
            machinemun = dt.Select($"場所 = '{cmb_出勤機コード.Text}'")[0].ItemArray[0].ToString();
            remark = txt_remark.Text;
            int result = 0;

            InputCheck();

            if (InputCheck() == false)
            {
                return;
            }
            else
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
                }

                SqlCommand sqlcom = new SqlCommand();
                sqlcom.Connection = sqlcon;

                try
                {
                    if (isUpdate == "new")
                    {
                        sqlcom.CommandText = string.Format(@"INSERT INTO HL_JUKUKANRI_教室マスタ (教室コード,出勤機コード,備考)
                                                        VALUES ('{0}','{1}',N'{2}')", classnum, machinemun, remark);
                    }
                    else
                    {
                        sqlcom.CommandText = string.Format(@"update HL_JUKUKANRI_教室マスタ
                                                             set 出勤機コード　= '{0}' ,備考 = N'{1}'
                                                             where 教室コード　= '{2}'", machinemun, remark, classnum);
                    }
                    result = sqlcom.ExecuteNonQuery();
                    if (result == 1)
                    {
                        ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Green;
                        if (isUpdate == "new")
                        {
                            ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "データは正常に追加しました。";
                        }
                        else
                        {
                            ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "データは正常に更新しました。";
                        }
                        ((Form1)(this.Tag)).reLoad = false;

                        if (((Form1)(this.Tag)).m_教室一覧Handle != null)
                        {
                            SendMessage(((Form1)(this.Tag)).m_教室一覧Handle, Form1.CUSTOM_MESSAGE, IntPtr.Zero, IntPtr.Zero);
                        }
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Red;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "DBサーバーの接続に失敗しました。" + ex.Message; ;
                    ((Form1)(this.Tag)).reLoad = false;
                }
                finally
                {
                    if (sqlcon != null)
                    {
                        sqlcon.Close();
                    }
                }
            }        
        }

        /// <summary>
        /// 画面Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 教室管理_Load(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);

            SetComboxList();

            SqlConnection sqlcon = new SqlConnection(connectionString);
            string str_sqlcmd = "select * from HL_JUKUKANRI_教室マスタ order by 教室コード + 0 ASC";
            SqlDataAdapter sqlDa = new SqlDataAdapter(str_sqlcmd, sqlcon);

            DataTable dt = new DataTable();
            sqlDa.Fill(dt);
            sqlcon.Close();
           
            if (isUpdate == "new")
            {
                //最後行のクラスコードを取得
                string maxmun;
                if (dt.Rows.Count == 0)
                {
                    //クラスがない場合、最後行のクラスコード＝０
                    maxmun = "0";
                }
                else
                {
                    maxmun = dt.Rows[dt.Rows.Count - 1][0].ToString();
                }
                //新しいクラスコード発行
                int index = Convert.ToInt16(maxmun) + 1;
                txt_classroomnum.Text = index.ToString();
                txt_classroomnum.Enabled = false;
                lbl_classroomnum.Text = lbl_classroomnum.Text.Replace("[必須]", "");
                this.Text = "教室登録";
            }
            if (isUpdate == "Update")
            {
                //画面設定
                ///shin 20200722 start
                //cmb_出勤機コード.SelectedIndex = Convert.ToInt16(machinemun) - 1;
                ///shin 20200722 end
                txt_classroomnum.Text = classnum;
                txt_classroomnum.Enabled = false;
                lbl_classroomnum.Text = lbl_classroomnum.Text.Replace("[必須]", "");
                txt_remark.Text = remark;
                this.Text = "教室情報変更";
                //bt_regist.Text = "変更";
                bt_regist.Text = "更新";
            }
        }

        /// <summary>
        /// 入力データチェック
        /// </summary>
        /// <returns></returns>
        public bool InputCheck()
        {
            //bool isright = true;
            //数字チェック
            string check = "^[0-9]+$";

            Regex regex = new Regex(check);

            //未入力チェック
            if (cmb_出勤機コード.Text == "")
            {
                toolStripStatusLabel2.ForeColor = Color.Red;
                toolStripStatusLabel2.Text = "出勤機コードを選択してください。";
                //isright = false;
                return false;
            }
            //未入力チェック
            if (txt_classroomnum.Text == "")
            {
                toolStripStatusLabel2.ForeColor = Color.Red;
                toolStripStatusLabel2.Text = "教室コードを入力してください。";
                //isright = false;
                return false;
            }
            //クラスコードが数字しか入力できないチェック
            if (regex.IsMatch(txt_classroomnum.Text) == false)
            {
                toolStripStatusLabel2.ForeColor = Color.Red;
                toolStripStatusLabel2.Text = "教室コード欄に数字だけを入力してください。";
                //isright = false;
                return false;
            }
            //クラスコードは0からスタットチェック
            else if (txt_classroomnum.Text.StartsWith("0"))
            {
                toolStripStatusLabel2.ForeColor = Color.Red;
                toolStripStatusLabel2.Text = "教室コード欄は０から入力できません。もう一度確認してください。";
                //isright = false;
                return false;

            }

            //if (regex.IsMatch(cmb_出勤機コード.Text) == false)
            //{
            //    toolStripStatusLabel2.ForeColor = Color.Red;
            //    toolStripStatusLabel2.Text = "出勤機コード欄に数字だけを選択してください。";
            //    isright = false;
            //}

            //未入力チェック
            if (txt_remark.Text=="")
            {
               toolStripStatusLabel2.ForeColor = Color.Red;
               toolStripStatusLabel2.Text = "備考欄は未入力です。";
                //isright = false;
                return false;
            }
            //return isright;
            return true;
        }

        /// <summary>
        /// 画面が閉じる処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 教室管理_Closed(object sender, FormClosedEventArgs e)
        {
            ((Form1)(this.Tag)).m_教室管理Handle = IntPtr.Zero;
        }

        /// <summary>
        /// 「必須」の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbl_必須_Paint(object sender, PaintEventArgs e)
        {
            Label strLbl = (Label)sender;
            string str = strLbl.Text;

            if (str.Contains("必須"))
            {
                strLbl.ForeColor = Color.FromArgb(240, 240, 240);

                Font f = new System.Drawing.Font("メイリオ", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));


                int i = str.IndexOf("　[");
                string str_name = str.Substring(0, i);
                string str必須 = str.Substring(i, str.Length - i);

                for (int x = 0; x <= i; x++)
                {
                    str必須 = "   " + str必須;
                }

                Point point = new Point(((Label)sender).Padding.Right, ((Label)sender).Padding.Top);
                TextRenderer.DrawText(e.Graphics, str必須, f, point, Color.Red);
                TextRenderer.DrawText(e.Graphics, str_name, ((Label)sender).Font, point, Color.Black);
            }
            else
            {
                strLbl.ForeColor = Color.Black;
            }
        }
    }
}