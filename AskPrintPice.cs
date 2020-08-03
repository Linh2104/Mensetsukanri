using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;//添加命名空间

namespace HL_塾管理
{
    public partial class AskPrintPice : Form
    {
        public AskPrintPice()
        {
            InitializeComponent();
        }

        public int pice_back = 0;
        public string print_back = "";

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int pice = 0;
                try
                {
                    pice = Convert.ToInt32(textBox1.Text.Trim());
                    if(pice<=0)
                    {
                        MessageBox.Show("正しい数字を入力してください。");
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("正しい数字を入力してください。");
                    return;
                }
                pice_back = pice;
                print_back = this.comboBox1.Text;
                this.Close();
            }
            catch
            {
                
            }           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pice_back = -1;
            print_back = "";
            this.Close();
        }

        private void AskPrintPice_Load(object sender, EventArgs e)
        {
            textBox1.Text = "1";
            PrintDocument print = new PrintDocument();
            string sDefault = print.PrinterSettings.PrinterName;//默认打印机名

            foreach (string sPrint in PrinterSettings.InstalledPrinters)//获取所有打印机名称
            {
                this.comboBox1.Items.Add(sPrint);
                if (sPrint == sDefault)
                    this.comboBox1.SelectedIndex = this.comboBox1.Items.IndexOf(sPrint);
            }
        }
    }
}
