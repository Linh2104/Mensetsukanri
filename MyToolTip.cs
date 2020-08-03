using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HL_塾管理
{
    public partial class MyToolTip : UserControl
    {
        public TextBox m_TB = null;

        public MyToolTip()
        {
            InitializeComponent();

            this.label2.Visible = false;
        }

        public override string Text
        {
            set
            {
                this.label2.Visible = false;
                this.label1.Text = value;
            }
            get
            {
                return this.label1.Text;
            }
        }

        public void setTextBox(TextBox src)
        {
            this.m_TB = src;
        }

        public string getLine(int Y)
        {
            int LineHeight = (int)(this.label1.Font.SizeInPoints) + this.label1.Font.Height;

            int lineNum = Y / this.label1.Font.Height;

            return this.label1.Text.Split('\n')[lineNum];
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            this.label2.Text = this.getLine(e.Y);

            Point pt = new Point();
            pt.X = this.label1.Location.X;
            int lineNum = e.Y / this.label1.Font.Height;
            pt.Y = this.label1.Location.Y + (lineNum * this.label1.Font.Height);

            this.label2.Location = pt;
            this.label2.Width = this.label1.Width;

            this.label2.Visible = true;
        }

        private void label2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.m_TB.Text = this.label2.Text;

            this.Visible = false;
        }

        private void MyToolTip_VisibleChanged(object sender, EventArgs e)
        {
            this.Size = new Size(0, 0);
        }
    }
}
