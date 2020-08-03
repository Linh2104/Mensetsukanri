using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Runtime.InteropServices;
using System.Threading;

namespace HL_塾管理
{
    public partial class Form1 : Form
    {
        //各画面のハンドル初期値設定、再表示用
        public IntPtr m_パスワード変更Handle = IntPtr.Zero;
        public IntPtr m_会議室予約一覧Handle = IntPtr.Zero;

        //liu rui add 20200526
        public IntPtr m_会議室予約登録Handle = IntPtr.Zero;
        public IntPtr m_会議室予約変更Handle = IntPtr.Zero;
        //end

        public IntPtr m_出勤表エラー記録Handle = IntPtr.Zero;

        //liu rui add 20200526
        public IntPtr m_出勤機学生登録Handle = IntPtr.Zero;
        public IntPtr m_出勤機学生一覧Handle = IntPtr.Zero;
        public IntPtr m_新規学生入塾Handle = IntPtr.Zero;
        public IntPtr m_学生情報変更Handle = IntPtr.Zero;
        public IntPtr m_学生退塾Handle = IntPtr.Zero;
        public IntPtr m_学生情報一覧Handle = IntPtr.Zero;
        public IntPtr m_クラス登録Handle = IntPtr.Zero;
        public IntPtr m_クラス変更Handle = IntPtr.Zero;
        public IntPtr m_クラス参照Handle = IntPtr.Zero;
        //end

        public IntPtr m_教室マスタ登録Handle = IntPtr.Zero;
        public IntPtr m_歴史クラス一覧Handle = IntPtr.Zero;
        
        //liu rui add 20200416
        //liu rui add 20200512
        //liu rui add 20200525
        public IntPtr m_社外教師情報登録Handle = IntPtr.Zero;
        public IntPtr m_社外教師情報変更Handle = IntPtr.Zero;
        public IntPtr m_教師情報一覧Handle = IntPtr.Zero;
        public IntPtr m_社員から教師へ登録Handle = IntPtr.Zero;
        public IntPtr m_社員から教師へ変更Handle = IntPtr.Zero;
        //end

        //liuxiaoyan add 20200420
        public IntPtr m_ログインユーザー登録Handle = IntPtr.Zero;
        //end

        //liuxiaoyan add 20200514
        public IntPtr m_学生出勤一覧Handle = IntPtr.Zero;
        //end

        public IntPtr m_教師選択Handle = IntPtr.Zero;
        public IntPtr m_学生選択Handle = IntPtr.Zero;
        public IntPtr m_クラス選択Handle = IntPtr.Zero;

        //liu rui add 20200526
        //tong 20200514 start
        public IntPtr m_教室一覧Handle = IntPtr.Zero;
        public IntPtr m_教室管理Handle = IntPtr.Zero;
        //tong 20200515 end

        //Linh add 20200616
        public IntPtr m_教師クラス履歴一覧Handle = IntPtr.Zero;
        //end

        //wang add 20200717
        public IntPtr m_出勤記録新規追加画面Handle = IntPtr.Zero;
        //end

        public Dictionary<string, int> codeDic = new Dictionary<string, int>();
        public ユーザ登録 m_ユーザ登録 = null;
        public bool reLoad = true;
        public int m_メール返信中 = 0;

        public const int MAXDAYS = -15;
        public const int UpdateNum = 300;
        public const int WM_CLOSE = 0x0010;
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int BringWindowToTop(IntPtr hWnd);

        public const int CUSTOM_MESSAGE = 0X400 + 2;

        //関連画面を開く用
        //liu rui add 20200526
        会議室予約 m_NewForm_会議室予約 = new 会議室予約();
        //end

        会議室予約一覧 m_NewForm_会議室予約一覧 = new 会議室予約一覧();
        学生情報一覧 m_NewForm_学生情報一覧 = new 学生情報一覧();
        学生情報 m_NewForm_学生情報 = new 学生情報();

        //liu rui add 20200526
        出勤機学生登録 m_NewForm_出勤機学生登録 = new 出勤機学生登録();
        出勤機学生一覧 m_NewForm_出勤機学生一覧 = new 出勤機学生一覧();
        //end

        //liu rui add 20200416
        //liu rui add 20200512
        社外教師情報 m_NewForm_社外教師情報 = new 社外教師情報();
        教師情報一覧 m_NewForm_教師情報一覧 = new 教師情報一覧();
        社員から教師へ変更 m_NewForm_社員から教師へ変更 = new 社員から教師へ変更();
        //end

        //liuxiaoyan add 20200420
        ログインユーザー登録 m_NewForm_教師ログイン = new ログインユーザー登録();

        //liuxiaoyan add 20200514
        学生出勤一覧 m_NewForm_学生出勤一覧 = new 学生出勤一覧();
        //end

        //tong 20200514 start
        教室一覧 m_NewForm_教室一覧 = new 教室一覧();
        教室管理 m_NewForm_教室登録 = new 教室管理();
        //tong 20200515 end

        // Linh add 20200616 
        教師クラス履歴一覧 m_NewForm_教師クラス履歴一覧 = new 教師クラス履歴一覧();
        // end
        public Form1()
        {
            InitializeComponent();

            this.Text = "【HL 塾管理】";

            this.ログインToolStripMenuItem.Enabled = false;

            CheckForIllegalCrossThreadCalls = false;
        }

        /// <summary>
        /// システムログアウト
        /// </summary>
        private void ログアウトToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Text = "【HL 塾管理】";

            //各画面特定のウインドウにメッセージを送信
            SendMessage(m_会議室予約一覧Handle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            SendMessage(m_会議室予約登録Handle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            SendMessage(this.m_出勤表エラー記録Handle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);

            SendMessage(m_学生情報一覧Handle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            SendMessage(this.m_教室マスタ登録Handle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);

            //liu rui add 20200417
            SendMessage(m_教師情報一覧Handle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            SendMessage(this.m_教師選択Handle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            SendMessage(this.m_クラス参照Handle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            SendMessage(this.m_クラス登録Handle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);

            //tong 20200514 start
            SendMessage(m_教室一覧Handle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            SendMessage(m_教室管理Handle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            //tong 20200515 end

            this.m_ユーザ登録.m_ログイン_ユーザ = "";

            //子画面全部閉じる
            foreach (Form childrenForm in this.MdiChildren)
            {
                childrenForm.Close();
            }
            this.toolStripStatusLabel2.Text = "";
            this.toolStripStatusLabel1.Text = "";

            this.教室ToolStripMenuItem.Enabled = false;
            this.クラス情報管理ToolStripMenuItem.Enabled = false;
            this.教師情報ToolStripMenuItem.Enabled = false;
            this.出勤機ユーザToolStripMenuItem.Enabled = false;
            this.学生管理ToolStripMenuItem.Enabled = false;
            this.会議室ToolStripMenuItem.Enabled = false;
            this.ログインToolStripMenuItem.Enabled = true;
            this.ログアウトToolStripMenuItem.Enabled = false;
            this.パスワードの変更ToolStripMenuItem.Enabled = false;
            this.教師ユーザーログインToolStripMenuItem.Enabled = false;

        }

        /// <summary>
        /// システムログイン
        /// </summary>
        private void ログインToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!this.m_ユーザ登録.Equals(""))
            {
                this.Text += this.m_ユーザ登録.m_ログイン_ユーザ;
            }
            //ログインボタンを無効、ログアウトボタンを有効
            this.ログインToolStripMenuItem.Enabled = false;
            this.ログアウトToolStripMenuItem.Enabled = true;
            this.教室ToolStripMenuItem.Enabled = true;
            this.クラス情報管理ToolStripMenuItem.Enabled = true;
            this.教師情報ToolStripMenuItem.Enabled = true;
            this.出勤機ユーザToolStripMenuItem.Enabled = true;
            this.学生管理ToolStripMenuItem.Enabled = true;
            this.会議室ToolStripMenuItem.Enabled = true;
            this.パスワードの変更ToolStripMenuItem.Enabled = true;
            this.教師ユーザーログインToolStripMenuItem.Enabled = true;

            //各画面にハンドル値を渡す
            SendMessage(this.m_パスワード変更Handle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            SendMessage(this.m_会議室予約一覧Handle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            SendMessage(this.m_会議室予約登録Handle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            SendMessage(this.m_出勤表エラー記録Handle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);

            SendMessage(this.m_学生情報一覧Handle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            SendMessage(this.m_教室マスタ登録Handle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);

            //liu rui add 20200417
            SendMessage(this.m_教師情報一覧Handle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            SendMessage(this.m_教師選択Handle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);

            SendMessage(this.m_クラス参照Handle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            SendMessage(this.m_クラス登録Handle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            SendMessage(this.m_クラス選択Handle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);

            //tong 20200514 start
            SendMessage(this.m_教室一覧Handle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            SendMessage(this.m_教室管理Handle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            //tong 20200515 end

            //Linh add 20200616
            SendMessage(this.m_教師クラス履歴一覧Handle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            //end

            this.m_ユーザ登録.load();
            this.m_ユーザ登録.Show();
        }

        public void ログインHiDE()
        {
            //ログインボタンを有効、ログアウトボタンを無効
            this.ログインToolStripMenuItem.Enabled = true;
            this.ログアウトToolStripMenuItem.Enabled = false;
        }

        /// <summary>
        /// 画面表示
        /// </summary>
        public void load()
        {
            if (!this.m_ユーザ登録.Equals(""))
            {
                this.Text += " " + this.m_ユーザ登録.m_ログイン_ユーザ;

                toolStripStatusLabel1.ForeColor = Color.Black;

                //名前表示
                if (this.m_ユーザ登録.m_ログイン_職務 == "管理者")
                {
                    toolStripStatusLabel1.Text = string.Format("[管理者] {0}様", this.m_ユーザ登録.m_ログイン_ユーザ氏名);
                }
                else
                {
                    toolStripStatusLabel1.Text = string.Format("{0}様", this.m_ユーザ登録.m_ログイン_ユーザ氏名);
                    //start shin 20200718
                    教師ユーザーログインToolStripMenuItem.Visible = false;
                    //end shin 20200718
                }
                Thread thread = new Thread(new ThreadStart(更新スレッド));
                thread.IsBackground = true;
                thread.Start();
            }
        }

        /// <summary>
        /// 各画面にハンドル値を渡して再表示できるように
        /// </summary>
        public void 更新スレッド()
        {
            while (true)
            {
                Thread.Sleep(60000 * 5);

                if (this.m_会議室予約一覧Handle != null)
                {
                    SendMessage(this.m_会議室予約一覧Handle, CUSTOM_MESSAGE, IntPtr.Zero, IntPtr.Zero);
                }

                if (this.m_出勤表エラー記録Handle != null)
                {
                    SendMessage(this.m_出勤表エラー記録Handle, CUSTOM_MESSAGE, IntPtr.Zero, IntPtr.Zero);
                }

                if (this.m_学生情報一覧Handle != null)
                {
                    SendMessage(this.m_学生情報一覧Handle, CUSTOM_MESSAGE, IntPtr.Zero, IntPtr.Zero);
                }

                //liu rui add 20200417
                if (this.m_教師情報一覧Handle != null)
                {
                    SendMessage(this.m_教師情報一覧Handle, CUSTOM_MESSAGE, IntPtr.Zero, IntPtr.Zero);
                }

                //liu rui add 20200417
                if (this.m_教師選択Handle != null)
                {
                    SendMessage(this.m_教師選択Handle, CUSTOM_MESSAGE, IntPtr.Zero, IntPtr.Zero);
                }

                if (this.m_クラス選択Handle != null)
                {
                    SendMessage(this.m_クラス選択Handle, CUSTOM_MESSAGE, IntPtr.Zero, IntPtr.Zero);
                }
                
                //tong 20200514 start
                if (this.m_教室一覧Handle != null)
                {
                    SendMessage(this.m_教室一覧Handle, CUSTOM_MESSAGE, IntPtr.Zero, IntPtr.Zero);
                }

                if (this.m_教室管理Handle != null)
                {
                    SendMessage(this.m_教室管理Handle, CUSTOM_MESSAGE, IntPtr.Zero, IntPtr.Zero);
                }
                //tong 20200515 end

                //Linh add 20200616
                if (this.m_教師クラス履歴一覧Handle != null)
                {
                    SendMessage(this.m_教師クラス履歴一覧Handle, CUSTOM_MESSAGE, IntPtr.Zero, IntPtr.Zero);
                }               
                // end

            }
        }

        /// <summary>
        /// 画面表示
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            load();
        }

        パスワード変更 m_NewForm_パスワード変更 = new パスワード変更();//muhuaizhi add 20190205

        /// <summary>
        /// パスワードの変更画面を開く
        /// </summary>
        private void パスワードの変更ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.m_パスワード変更Handle != IntPtr.Zero)
            {
                BringWindowToTop(this.m_パスワード変更Handle);
                return;
            }

            m_NewForm_パスワード変更 = new パスワード変更();//muhuaizhi updata 20190205

            m_NewForm_パスワード変更.Tag = this;//muhuaizhi updata 20190205
            m_NewForm_パスワード変更.Show();//muhuaizhi updata 20190205
            this.m_パスワード変更Handle = m_NewForm_パスワード変更.Handle;//muhuaizhi updata 20190205
            toolStripStatusLabel2.Text = "";
        }

        /// <summary>
        /// 画面閉じ
        /// </summary>
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.m_ユーザ登録.m_MainForm = null;
            this.m_ユーザ登録.Close();
        }

        /// <summary>
        /// この画面を閉じる
        /// </summary>
        private void 閉じるToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.dockPanel1.ActiveContent.DockHandler.Close();
        }

        /// <summary>
        /// 子画面全部閉じる
        /// </summary>
        private void すべて閉じるToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DockContentCollection contents = this.dockPanel1.Contents;
            int num = 0;
            while (num < contents.Count)
            {
                if (contents[num].DockHandler.DockState == DockState.Document)
                {
                    contents[num].DockHandler.Close();
                }
                else
                {
                    num++;
                }
            }
        }

        /// <summary>
        /// この画面以外の子画面全部閉じる
        /// </summary>
        private void このうんどToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DockContentCollection contents = this.dockPanel1.Contents;
            int num = 0;
            while (num < contents.Count)
            {
                if (contents[num].DockHandler.DockState == DockState.Document && this.dockPanel1.ActiveContent != contents[num])
                {
                    contents[num].DockHandler.Close();
                }
                else
                {
                    num++;
                }
            }
        }

        /// <summary>
        /// システム閉じ
        /// </summary>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("【HL 塾管理】を終了してよろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 会議室予約画面を開く
        /// </summary>
        //liu rui add 20200526
        private void 会議室予約登録ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.m_会議室予約登録Handle != IntPtr.Zero)
            {
                BringWindowToTop(this.m_会議室予約登録Handle);
                return;
            }

            m_NewForm_会議室予約 = new 会議室予約();
            m_NewForm_会議室予約.Tag = this;
            m_NewForm_会議室予約.Show(this.dockPanel1);
            this.m_会議室予約登録Handle = m_NewForm_会議室予約.Handle;
            toolStripStatusLabel2.Text = ""; 
        }

        /// <summary>
        /// 会議室予約一覧画面を開く
        /// </summary>
        private void 会議室予約一覧参照ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.m_会議室予約一覧Handle != IntPtr.Zero)
            {
                BringWindowToTop(this.m_会議室予約一覧Handle);
                return;
            }

            m_NewForm_会議室予約一覧 = new 会議室予約一覧();
            m_NewForm_会議室予約一覧.Tag = this;
            m_NewForm_会議室予約一覧.Show(this.dockPanel1);
            this.m_会議室予約一覧Handle = m_NewForm_会議室予約一覧.Handle;
            toolStripStatusLabel2.Text = "";
        }

        /// <summary>
        /// 学生情報一覧画面を開く
        /// </summary>
        private void 学生情報一覧ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.m_学生情報一覧Handle != IntPtr.Zero)
            {
                BringWindowToTop(this.m_学生情報一覧Handle);
                return;
            }

            m_NewForm_学生情報一覧 = new 学生情報一覧();
            m_NewForm_学生情報一覧.Tag = this;
            m_NewForm_学生情報一覧.Show(this.dockPanel1);
            this.m_学生情報一覧Handle = m_NewForm_学生情報一覧.Handle;
            toolStripStatusLabel2.Text = "";
        }

        /// <summary>
        /// 新規学生入塾画面を開く
        /// </summary>
        private void 新規学生入塾ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.m_新規学生入塾Handle != IntPtr.Zero)
            {
                BringWindowToTop(this.m_新規学生入塾Handle);
                return;
            }

            m_NewForm_学生情報 = new 学生情報();
            m_NewForm_学生情報.Tag = this;
            m_NewForm_学生情報.Show(this.dockPanel1);
            this.m_新規学生入塾Handle = m_NewForm_学生情報.Handle;
            toolStripStatusLabel2.Text = "";
        }

        出勤表エラー記録 m_NewForm_出勤表エラー記録 = new 出勤表エラー記録();

        /// <summary>
        /// 出勤表エラー記録画面を開く
        /// </summary>
        private void 出勤表エラー記録ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.m_出勤表エラー記録Handle != IntPtr.Zero)
            {
                BringWindowToTop(this.m_出勤表エラー記録Handle);
                return;
            }

            m_NewForm_出勤表エラー記録 = new 出勤表エラー記録();
            m_NewForm_出勤表エラー記録.Tag = this;
            m_NewForm_出勤表エラー記録.Show(this.dockPanel1);
            this.m_出勤表エラー記録Handle = m_NewForm_出勤表エラー記録.Handle;
            toolStripStatusLabel1.Text = "";
        }

        /// <summary>
        /// 出勤機学生登録画面を開く
        /// </summary>
        //liu rui add 20200526
        private void 出勤機学生登録ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.m_出勤機学生登録Handle != IntPtr.Zero)
            {
                BringWindowToTop(this.m_出勤機学生登録Handle);
                return;
            }

            m_NewForm_出勤機学生登録 = new 出勤機学生登録();
            m_NewForm_出勤機学生登録.Tag = this;
            this.m_出勤機学生登録Handle = m_NewForm_出勤機学生登録.Handle;
            m_NewForm_出勤機学生登録.ShowDialog();
        }

        /// <summary>
        /// 出勤機学生一覧画面を開く
        /// </summary>
        //liu rui add 20200526
        private void 出勤機学生一覧ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.m_出勤機学生一覧Handle != IntPtr.Zero)
            {
                BringWindowToTop(this.m_出勤機学生一覧Handle);
                return;
            }

            m_NewForm_出勤機学生一覧 = new 出勤機学生一覧();
            m_NewForm_出勤機学生一覧.Tag = this;
            m_NewForm_出勤機学生一覧.Show(this.dockPanel1);
            this.m_出勤機学生一覧Handle = m_NewForm_出勤機学生一覧.Handle;
            toolStripStatusLabel2.Text = "";
        }

        /// <summary>
        /// 社外教師情報画面を開く
        /// </summary>
        //liu rui add 20200416
        private void 社外教師情報ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.m_社外教師情報登録Handle != IntPtr.Zero)
            {
                BringWindowToTop(this.m_社外教師情報登録Handle);
                return;
            }

            m_NewForm_社外教師情報 = new 社外教師情報();
            m_NewForm_社外教師情報.Tag = this;
            m_NewForm_社外教師情報.Show(this.dockPanel1);
            this.m_社外教師情報登録Handle = m_NewForm_社外教師情報.Handle;
            toolStripStatusLabel2.Text = "";
        }

        /// <summary>
        /// 教師情報一覧画面を開く
        /// </summary>
        //liu rui add 20200417
        private void 教師情報一覧ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.m_教師情報一覧Handle != IntPtr.Zero)
            {
                BringWindowToTop(this.m_教師情報一覧Handle);
                return;
            }

            m_NewForm_教師情報一覧 = new 教師情報一覧();
            m_NewForm_教師情報一覧.Tag = this;
            m_NewForm_教師情報一覧.Show(this.dockPanel1);
            this.m_教師情報一覧Handle = m_NewForm_教師情報一覧.Handle;
            toolStripStatusLabel2.Text = "";
        }

        /// <summary>
        /// 教師ユーザーログイン画面を開く
        /// </summary>
        //liu rui add 20200526
        //20200420 LIUXIAOYAN ADD
        private void 教師ユーザーログインToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.m_ログインユーザー登録Handle != IntPtr.Zero)
            {
                BringWindowToTop(this.m_ログインユーザー登録Handle);
                return;
            }

            m_NewForm_教師ログイン = new ログインユーザー登録();
            m_NewForm_教師ログイン.Tag = this;            
            this.m_ログインユーザー登録Handle = m_NewForm_教師ログイン.Handle;
            m_NewForm_教師ログイン.ShowDialog();
        }

        /// <summary>
        /// 出勤表エラー記録画面を開く
        /// </summary>
        //add liuxiaoyan 20200422
        private void 出勤表エラー記録ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.m_出勤表エラー記録Handle != IntPtr.Zero)
            {
                BringWindowToTop(this.m_出勤表エラー記録Handle);
                return;
            }

            m_NewForm_出勤表エラー記録 = new 出勤表エラー記録();
            m_NewForm_出勤表エラー記録.Tag = this;
            m_NewForm_出勤表エラー記録.Show(this.dockPanel1);
            this.m_出勤表エラー記録Handle = m_NewForm_出勤表エラー記録.Handle;
            toolStripStatusLabel2.Text = "";
        }

        /// <summary>
        /// 社員から教師へ登録画面を開く
        /// </summary>
        //liu rui add 20200423
        //liu rui add 20200512
        private void 社員から教師へ変更ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.m_社員から教師へ登録Handle != IntPtr.Zero)
            {
                BringWindowToTop(this.m_社員から教師へ登録Handle);
                return;
            }

            m_NewForm_社員から教師へ変更 = new 社員から教師へ変更();
            m_NewForm_社員から教師へ変更.Tag = this;
            m_NewForm_社員から教師へ変更.Show(this.dockPanel1);
            this.m_社員から教師へ登録Handle = m_NewForm_社員から教師へ変更.Handle;
            toolStripStatusLabel2.Text = "";
        }

        クラス管理 m_NewForm_クラス管理 = new クラス管理();

        /// <summary>
        ///新規クラス画面を開く
        /// </summary>
        private void 新規クラスToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.m_クラス登録Handle != IntPtr.Zero)
            {
                BringWindowToTop(this.m_クラス登録Handle);
                return;
            }

            m_NewForm_クラス管理 = new クラス管理();
            m_NewForm_クラス管理.Tag = this;
            m_NewForm_クラス管理.Show(this.dockPanel1);
            this.m_クラス登録Handle = m_NewForm_クラス管理.Handle;
            toolStripStatusLabel2.Text = "";
        }

        クラス参照 m_NewForm_クラス参照 = new クラス参照();

        /// <summary>
        /// クラス参照画面を開く
        /// </summary>
        private void クラス参照ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.m_クラス参照Handle != IntPtr.Zero)
            {
                BringWindowToTop(this.m_クラス参照Handle);
                return;
            }

            m_NewForm_クラス参照 = new クラス参照();
            m_NewForm_クラス参照.Tag = this;
            m_NewForm_クラス参照.Show(this.dockPanel1);
            this.m_クラス参照Handle = m_NewForm_クラス参照.Handle;
            toolStripStatusLabel2.Text = "";
        }

        /// <summary>
        /// 学生出勤一覧画面を開く
        /// </summary>
        private void 学生出勤一覧ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.m_学生出勤一覧Handle != IntPtr.Zero)
            {
                BringWindowToTop(this.m_学生出勤一覧Handle);
                return;
            }

            m_NewForm_学生出勤一覧 = new 学生出勤一覧();
            m_NewForm_学生出勤一覧.Tag = this;
            m_NewForm_学生出勤一覧.Show(this.dockPanel1);
            this.m_学生出勤一覧Handle = m_NewForm_学生出勤一覧.Handle;
            toolStripStatusLabel2.Text = "";
        }

        /// <summary>
        /// 教室一覧画面を開く
        /// </summary>
        //tong 20200514 start
        private void 教室一覧ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.m_教室一覧Handle != IntPtr.Zero)
            {
                BringWindowToTop(this.m_教室一覧Handle);
                return;
            }

            m_NewForm_教室一覧 = new 教室一覧();
            m_NewForm_教室一覧.Tag = this;
            m_NewForm_教室一覧.Show(this.dockPanel1);
            this.m_教室一覧Handle = m_NewForm_教室一覧.Handle;
            toolStripStatusLabel2.Text = "";
        }

        /// <summary>
        /// 教室登録画面を開く
        /// </summary>
        //liu rui add 20200526
        private void 教室登録ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.m_教室管理Handle != IntPtr.Zero)
            {
                BringWindowToTop(this.m_教室管理Handle);
                return;
            }

            m_NewForm_教室登録 = new 教室管理();
            m_NewForm_教室登録.Tag = this;
            this.m_教室管理Handle = m_NewForm_教室登録.Handle;
            m_NewForm_教室登録.ShowDialog();
        }
        //tong 20200515 end*
    }
}
