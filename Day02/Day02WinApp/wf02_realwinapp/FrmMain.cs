﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wf02_realwinapp
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            MessageBox.Show("버튼클릭!!!", "클릭", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            BtnOK.Text = "클릭됨";
        }

        private void BtnOK_MouseHover(object sender, EventArgs e)
        {
            MessageBox.Show("마우스만 올려도 이벤트가 발생해요!");
        }
    }
}
