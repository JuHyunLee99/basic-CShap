using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wf05_login
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (TxtId.Text == "abcd" && TxtPassword.Text == "1234")
            {
                MessageBox.Show("로그인 성공","성공", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("로그인 실패", "실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
