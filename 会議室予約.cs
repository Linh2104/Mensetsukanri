using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace HL_塾管理
{
    public partial class 会議室予約 : DockContent
    {
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        private AutoSizeFormClass asc = new AutoSizeFormClass();

        //データベース接続情報
        private string connectionString = ComClass.connectionString;

        //更新フラグ
        public bool isUpdate = false;

        //更新するrow
        public DataRow updateRow = null;

        //datagridview
        public DataGridViewRow row = null;

        //会議ID
        public string ID = null;

        //担当者フラグ
        private bool isRefrence = false;

        //会議予約者
        public string user = "";

        //会議タイトル
        public string title = "";

        //時間
        public DateTime date;

        //会議開始時間
        public string start_time = "";

        //会議終了時間
        public string end_time = "";

        //会議室名
        public string roomName = "";

        //会議人数
        public int count = 0;

        //会議参加者
        public string members = "";

        //備考
        public string info =  ""; 

        public 会議室予約()
        {
            InitializeComponent();
        }

        /// <summary>
        //画面表示
        /// <summary>
        private void Page_load(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);

            //画面区分
            btn_insert.Visible = false;
            btn_update.Visible = false;
            this.Text = isUpdate ? "会議室 予約変更" : "会議室 予約登録";

            //初期表示設定
            if (row == null)
            {
                btn_insert.Visible = true;

                //画面初期設定
                SetDefaultControl();
            }
            else
            {
                //参照データ取得
                GetDataSource();
            }

            isRefrence = true;
        }


        /// <summary>
        /// 新規　初期項目設定
        /// </summary>
        public void SetDefaultControl()
        {
            //初期画面値設定
            this.txt_title.Text = string.Empty;
            DateTime date_now = DateTime.Now;
            this.dtp_date.Text = date_now.ToShortDateString();
            this.dtp_startTime.Value = date_now;
            this.dtp_endTime.Value = date_now.AddHours(1);

            this.txt_member.Text = string.Empty;
            this.txt_info.Text = string.Empty;

            //会議室名_選択リスト取得
            SetRoomNameList();

            //定員数_リスト設定
            SetCountList();
        }

        /// <summary>
        /// 日付_TextChange処理
        /// </summary>
        /// <remarks>妥当性チェック</remarks>
        private void txt_date_ValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(dtp_date.Text))
            {
                DateTime date = DateTime.ParseExact(dtp_date.Text, "yyyy/MM/dd", null);

                //過去日は無効
                if (isRefrence && date < DateTime.Now.Date)
                {
                    toolStripStatusLabel1.ForeColor = Color.Red;
                    toolStripStatusLabel1.Text = "過去日は設定できません。";
                }
            } 
        }

        /// <summary>
        /// 入力チェック
        /// </summary>
        public bool InputCheck()
        {
            bool isError = false;

            if(row != null && user != ((Form1)(this.Tag)).m_ユーザ登録.m_ログイン_ユーザ)
            {
                toolStripStatusLabel1.ForeColor = Color.Red;
                toolStripStatusLabel1.Text = "更新権限がありません。";
           
                return true;
            }

            if (string.IsNullOrWhiteSpace(title))
            {
                toolStripStatusLabel1.ForeColor = Color.Red;
                toolStripStatusLabel1.Text = "タイトルを入力してください。";            
                return true;

            }

            //過去日は無効
            if (isRefrence && date < DateTime.Now.Date)
            {
                toolStripStatusLabel1.ForeColor = Color.Red;
                toolStripStatusLabel1.Text = "過去日は設定できません。";
                return true;
            }

            //開始日＞終了日は無効
            if(!string.IsNullOrWhiteSpace(start_time) && !string.IsNullOrWhiteSpace(end_time))
            {
                if(DateTime.ParseExact(start_time, "HH:mm",null) >= DateTime.ParseExact(end_time, "HH:mm", null))
                {
                    toolStripStatusLabel1.ForeColor = Color.Red;
                    toolStripStatusLabel1.Text = "予約時間の終了時間は開始時間以後に設定してください。";
                    return true;
                }
            }
            //2020/07/02 Liu Liyu start
            //予約時間最大二時間制限
            System.TimeSpan TS = new System.TimeSpan(dtp_endTime.Value.Ticks - dtp_startTime.Value.Ticks);
            if (TS.TotalSeconds / 3600 > 2)
            {
                toolStripStatusLabel1.ForeColor = Color.Red;
                toolStripStatusLabel1.Text = "会議室の最大予約時間が2時間です。";
                return true;
            }
            //2020/07/02 Liu Liyu end

            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库

            try
            {
                sqlcon.Open();
            }
            catch
            {
                toolStripStatusLabel1.ForeColor = Color.Red;
                toolStripStatusLabel1.Text = "DBサーバーの接続に失敗しました。";
                return true;
            }

            //既存チェック開始
            string sqlcmd = string.Format(@"Select 会議室名  From HL_ALL_会議室予約情報 Where 会議室名 = '{0}'", cmb_roomName.Text);
            if (btn_update.Visible)
            {
                sqlcmd += string.Format("And ID <> '{0}'", ID);
            }

            string startDateTime = date.Date.ToShortDateString() + " " + start_time; 
            string startEndTime = date.Date.ToShortDateString() + " " + end_time;
            sqlcmd +=string.Format(@" And ((開始時間 <= '{0}'  And  '{0}' <= 終了時間) Or (開始時間 <= '{1}'  And  '{1}' <= 終了時間)  Or ( '{0}' < 開始時間 And 開始時間 < '{1}'))",
                                                  startDateTime, startEndTime);

            SqlCommand sqlcom = new SqlCommand(sqlcmd, sqlcon);

            try
            {
                SqlDataReader reader = sqlcom.ExecuteReader();

                if (reader.HasRows)
                {
                    toolStripStatusLabel1.ForeColor = Color.Red;
                    toolStripStatusLabel1.Text = "予約時間に他の予約が既に登録されている。";
                    this.dtp_startTime.Focus();
                    isError = true;
                }
            }
            catch
            {
                toolStripStatusLabel1.ForeColor = Color.Red;
                toolStripStatusLabel1.Text = "予約時間の重複チェック処理に失敗しました。";
                isError = true;
            }
            finally
            {
                if (sqlcon != null)
                {
                    sqlcon.Close();
                }
            }

            return isError;
        }

        /// <summary>
        /// 会議室名リスト取得と設定
        /// </summary>
        private void SetRoomNameList()
        {
            //会議室名リスト取得と設定
            //this.cmb_roomName.Items.Clear();
            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库

            try
            {
                sqlcon.Open();
            }
            catch
            {
                toolStripStatusLabel1.ForeColor = Color.Red;
                toolStripStatusLabel1.Text = "DBサーバーの接続に失敗しました。";

                return;
            }

            string str_sqlcmd = @"SELECT  会議室名, 定員数  FROM HL_ALL_会議室マスタ";

            SqlDataAdapter sqlDa = new SqlDataAdapter(str_sqlcmd, sqlcon);
            DataTable dt = new DataTable();
            sqlDa.Fill(dt);

            cmb_roomName.DisplayMember = "会議室名";
            cmb_roomName.ValueMember = "定員数";
            cmb_roomName.DataSource = dt;

        }

        /// <summary>
        /// 人数_リスト設定
        /// </summary>
        private void SetCountList()
        {
            cmb_count.Items.Clear();
            //選択される会議室により定員数を取得する
            int maxCount =  Convert.ToInt32(cmb_roomName.SelectedValue);

            for(int i = 1; i<= maxCount; i ++)
            {
                cmb_count.Items.Add(i);
            }

            //定員数最大値取得
            cmb_count.Text = maxCount.ToString();
        }

        private void cmb_roomName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //リセット：人数リスト
            SetCountList();
        }

        /// <summary>
        /// 参照データ取得
        /// </summary>
        public void GetDataSource()
        {
            if (row != null)
            {
                txt_title.Text = row.Cells["タイトル"].Value.ToString();
                dtp_date.Text = row.Cells["日付"].Value.ToString();
                dtp_startTime.Text=row.Cells["開始時間"].Value.ToString();
                dtp_endTime.Text = row.Cells["終了時間"].Value.ToString();
                SetRoomNameList();
                cmb_roomName.Text = row.Cells["会議室名"].FormattedValue.ToString();
                cmb_count.Text = row.Cells["参加人数"].Value.ToString();
                txt_member.Text = row.Cells["参加者"].Value.ToString();
                txt_info.Text = row.Cells["備考"].Value.ToString();
                ID = row.Cells["ID"].Value.ToString();
                user = row.Cells["予約者_mail"].Value.ToString();

                if (isUpdate)
                {
                    //ラベルテキスト編集
                    btn_update.Visible = true;
                    lbl_title.Text=lbl_title.Text.Replace("[必須]", "");
                    lbl_date.Text = lbl_date.Text.Replace("[必須]", "");
                    lbl_reservation.Text=lbl_reservation.Text.Replace("[必須]", "");
                    lbl_roomName.Text=lbl_roomName.Text.Replace("[必須]", "");
                    lbl_count.Text =lbl_count.Text.Replace("[必須]", "");
                    lbl_member.Text=lbl_member.Text.Replace("[必須]", "");

                    if (user != ((Form1)(this.Tag)).m_ユーザ登録.m_ログイン_ユーザ)
                    {
                        //コンソールを無効
                        txt_title.Enabled = false;
                        dtp_date.Enabled = false;
                        dtp_startTime.Enabled = false;
                        dtp_endTime.Enabled = false;
                        cmb_roomName.Enabled = false;
                        cmb_count.Enabled = false;
                        txt_member.Enabled = false;
                        txt_info.Enabled = false;

                        btn_update.Enabled = false;
                    }
                }
                else
                {
                    btn_insert.Visible = true;
                    btn_update.Visible = false;
                }
            }
        }

        /// <summary>
        /// 登録ボタン
        /// </summary>
        private void btn_insert_Click(object sender, EventArgs e)
        {
            //画面値取得
            user = ((Form1)(this.Tag)).m_ユーザ登録.m_ログイン_ユーザ;
            title = txt_title.Text;
            date = dtp_date.Value;
            start_time = dtp_startTime.Text;
            end_time = dtp_endTime.Text;
            roomName = cmb_roomName.Text;
            count = Convert.ToInt32(cmb_count.Text);
            members = txt_member.Text;
            info = txt_info.Text;
           
            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库

            int result = 0;
            try
            {
                sqlcon.Open();
            }
            catch
            {
                toolStripStatusLabel1.ForeColor = Color.Red;
                toolStripStatusLabel1.Text = "DBサーバーの接続に失敗しました。";

                return;
            }
            SqlCommand sqlcom = new SqlCommand();
            sqlcom.Connection = sqlcon;

            //データチェック
            if(InputCheck())
            {
                return;
            }

            try
            {
                DateTime dateTime_Start = DateTime.Parse(dtp_date.Text + " " + dtp_startTime.Text);
                DateTime dateTime_End = DateTime.Parse(dtp_date.Text + " " + dtp_endTime.Text);
                //登録行う
                sqlcom.CommandText = string.Format(@"Insert Into HL_ALL_会議室予約情報 Values (newID(), '{0}', N'{1}', N'{2}', '{3}', '{4}', {5}, N'{6}',N'{7}', GetDate(), GetDate())",
                                                roomName, user, title, dateTime_Start, dateTime_End, count, members, info);

                result = sqlcom.ExecuteNonQuery();

                if(result == 1)
                {
                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Green;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "予約が正常に登録されました。";
                    ((Form1)(this.Tag)).reLoad = false;
                    if (((Form1)(this.Tag)).m_会議室予約一覧Handle != null)
                    {
                        SendMessage(((Form1)(this.Tag)).m_会議室予約一覧Handle, Form1.CUSTOM_MESSAGE, IntPtr.Zero, IntPtr.Zero);
                    }

                    this.Close();
                }
            }
            catch
            {
                toolStripStatusLabel1.ForeColor = Color.Red;
                toolStripStatusLabel1.Text = "予約の登録処理が失敗しました。";               
            }
            finally
            {
                if (sqlcon != null)
                {
                    sqlcon.Close();
                }
            }
        }

        /// <summary>
        /// 更新ボタン
        /// </summary>
        private void btn_update_Click(object sender, EventArgs e)
        {
            //画面値取得
            string user = ((Form1)(this.Tag)).m_ユーザ登録.m_ログイン_ユーザ;
            title = txt_title.Text;
            date = dtp_date.Value;
            start_time = dtp_startTime.Text;
            end_time = dtp_endTime.Text;
            roomName = cmb_roomName.Text;
            count = Convert.ToInt32(cmb_count.Text);
            members = txt_member.Text;
            info = txt_info.Text;

            SqlConnection sqlcon = new SqlConnection(connectionString); //连接数据库

            int result = 0;
            try
            {
                sqlcon.Open();
            }
            catch
            {
                toolStripStatusLabel1.ForeColor = Color.Red;
                toolStripStatusLabel1.Text = "DBサーバーの接続に失敗しました。";
                return;
            }

            SqlCommand sqlcom = new SqlCommand();
            sqlcom.Connection = sqlcon;

            //データチェック
            if (InputCheck())
            {
                return;
            }

            try
            {
                DateTime dateTime_Start = DateTime.Parse(dtp_date.Text + " " + dtp_startTime.Text);
                DateTime dateTime_End = DateTime.Parse(dtp_date.Text + " " + dtp_endTime.Text);

                //更新行う
                string sql = "Update HL_ALL_会議室予約情報 Set 会議室名 = N'{0}', 予約者 = N'{1}', タイトル =N'{2}', 開始時間='{3}', 終了時間 ='{4}', 参加人数 = {5}, 参加者=N'{6}', 備考 = N'{7}', 更新日時 = GetDate() Where ID = '{8}'";
                sqlcom.CommandText = string.Format(sql, roomName, user, title, dateTime_Start, dateTime_End, count, members, info, ID);

                result = sqlcom.ExecuteNonQuery();

                if (result == 1)
                {
                    ((Form1)(this.Tag)).toolStripStatusLabel2.ForeColor = Color.Green;
                    ((Form1)(this.Tag)).toolStripStatusLabel2.Text = "予約が正常に更新されました。";
                    ((Form1)(this.Tag)).reLoad = false;

                    if (((Form1)(this.Tag)).m_会議室予約一覧Handle != null)
                    {
                        SendMessage(((Form1)(this.Tag)).m_会議室予約一覧Handle, Form1.CUSTOM_MESSAGE, IntPtr.Zero, IntPtr.Zero);
                    }

                    this.Close();
                }
            }
            catch
            {
                toolStripStatusLabel1.ForeColor = Color.Red;
                toolStripStatusLabel1.Text = "予約の更新処理が失敗しました。";
            }
            finally
            {
                if (sqlcon != null)
                {
                    sqlcon.Close();
                }
            }
        }
        /// <summary>
        /// 画面閉じ
        /// </summary> 
        private void 会議室予約_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isUpdate)
            {
                if (((Form1)(this.Tag)).codeDic.ContainsKey(ID))
                {
                    ((Form1)(this.Tag)).codeDic.Remove(ID);
                }

                ((Form1)(this.Tag)).m_会議室予約変更Handle = IntPtr.Zero;
            }
            else
            {
                ((Form1)(this.Tag)).m_会議室予約登録Handle = IntPtr.Zero;
            }
        }

        /// <summary>
        /// ラベル文字色設定
        /// </summary>
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
