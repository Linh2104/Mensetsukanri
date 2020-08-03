using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace HL_塾管理
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            int processCount = 0;
            Process[] pa = Process.GetProcesses();//获取当前进程数组。  
            foreach (Process PTest in pa)
            {
                if (PTest.ProcessName == Process.GetCurrentProcess().ProcessName)
                {
                    processCount += 1;
                }
            }
            if (processCount == 1)  
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new ユーザ登録());
            }
            else
            {
                MessageBox.Show("このプログラムは既に起動しています!", "メッセージ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
