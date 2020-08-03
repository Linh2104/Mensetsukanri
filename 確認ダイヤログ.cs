using System;
using System.Windows.Forms;

namespace HL_塾管理
{
    public partial class 確認ダイヤログ : Form
    {
        public bool result = false;

        public 確認ダイヤログ()
        {
            InitializeComponent();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            result = true;
            this.Close();
        }

        private void btn_later_Click(object sender, EventArgs e)
        {
            result = false;
            this.Close();
        }
    }
}
