using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wf13_bookrentalshop
{
    public partial class FrmMain : Form
    {
        #region <각화면 폼>
        FrmGenre frmGenre = null;   // 책장르관리  객체 변수
        #region <생성자>

        public FrmMain()
        {
            InitializeComponent();
        }
        #endregion
        #endregion

        #region < 이벤트 핸들러 영역>

        private void FrmMain_Load(object sender, EventArgs e)
        {
            FrmLogin frm = new FrmLogin();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
        }
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("종료하시겠습니까?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                e.Cancel = false;
                Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }

        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit(); // 전체 프로그램 종료!!
        }

        private void MniGenre_Click(object sender, EventArgs e)
        {
            //FrmGenre frm = new FrmGenre();
            //frm.TopLevel = false;
            //this.Controls.Add(frm);
            //frm.Show();

            frmGenre = ShowActiveForm(frmGenre, typeof(FrmGenre)) as FrmGenre;

        }

        private void MniBookInfo_Click(object sender, EventArgs e)
        {

        }

        private void MniMember_Click(object sender, EventArgs e)
        {

        }

        private void MniRental_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        #endregion

        // 새 자식창을 띄울때 다른 자식창은 전부 다 닫고 시작
        private Form ShowActiveForm(Form form, Type type)
        {
            if (form == null)   // 한번도 창을 안열었으면
            {
                form = (Form)Activator.CreateInstance(type);    // 리프렉션으로 타입에 맞는 창을 새로 생성
                form.MdiParent = this;  // FrmMian이 MDI 부모
                form.WindowState = FormWindowState.Normal;
                form.Show();
            }
            else
            {
                if(form.IsDisposed) // 한번 닫혔다.
                {
                    form = (Form)Activator.CreateInstance(type);    // 리프렉션으로 타입에 맞는 창을 새로 생성
                    form.MdiParent = this;  // FrmMian이 MDI 부모
                    form.WindowState = FormWindowState.Normal;
                    form.Show();
                }
                else
                {
                    form.Activate();    // 화면이 있으면 그 화면을 활성화
                }
            }

            return form;
        }
    }
}
