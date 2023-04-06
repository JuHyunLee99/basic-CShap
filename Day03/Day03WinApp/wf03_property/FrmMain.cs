using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wf03_property
{
    public partial class FrmMain : Form
    {

        Random rnd = new Random();        // 트리뷰 파트

        public FrmMain()
        {
            InitializeComponent();
            // 생성자에는 되도록 설정부분을 넣지 마세요.
            // Form_Load() 이벤트 핸들러에 작성할 것
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            GbxMain.Text = "컨트롤 학습";
            var fonts = FontFamily.Families.ToList();   // 내 OS 폰트이름 가져오기
            foreach(var font in fonts)
            {
                CboFontFamily.Items.Add(font.Name);
            }

            // 글자크기 최소값, 최대값 지정
            NudFontSize.Minimum = 5;
            NudFontSize.Maximum = 40;

            // 텍스트박스 초기화
            TxtResult.Text = "Hello, WinForms";
            NudFontSize.Value = 9;  // 글차체 크기를 9로 지정
        }

        private void ChangeFonstStyle()
        {
            if (CboFontFamily.SelectedIndex < 0)
            {
                CboFontFamily.SelectedIndex = 258;  // 디폴트는 나눔고딕 지정
            };

            FontStyle style = FontStyle.Regular;    // 기본
            if (ChkBold.Checked==true)
            {
                style |= FontStyle.Bold;    // Bit연산 or
            }
            if (ChkItalic.Checked == true)
            {
                style |= FontStyle.Italic;    // Bit연산 or
            }

            decimal fontSize = NudFontSize.Value;
            TxtResult.Font = new Font((string)CboFontFamily.SelectedItem, (float)fontSize, style);
        }

        void ChangeIndent()
        {
            if (RdoNormal.Checked)  // 라디오버튼 추가
            {
                TxtResult.Text = TxtResult.Text.Trim();
            }
            else if (RdoIndent.Checked)
            {
                TxtResult.Text = "    " + TxtResult.Text;
            }
        }
        private void CboFontFamily_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeFonstStyle();
        }

        private void ChkBold_CheckedChanged(object sender, EventArgs e)
        {
            ChangeFonstStyle();
        }

        private void ChkItalic_CheckedChanged(object sender, EventArgs e)
        {
            ChangeFonstStyle();
        }

        private void NudFontSize_ValueChanged(object sender, EventArgs e)
        {
            ChangeFonstStyle();
        }

        private void TrbDummy_Scroll(object sender, EventArgs e)
        {
            PgbDummy.Value = TrbDummy.Value;
        }

        private void BtnModal_Click(object sender, EventArgs e)
        {
            Form frm = new Form() {
                Text = "Modal Form",
                Width = 300,
                Height = 200,
                Left = 10,
                Top = 20,
                BackColor = Color.Crimson,
                StartPosition = FormStartPosition.CenterParent
            };
            frm.ShowDialog();   // 모달방식으로 자식창 띄우기
        }

        private void BtnModaless_Click(object sender, EventArgs e)
        {
            Form frm = new Form() {
                Text = "Mmdaless Form",
                Width = 300,
                Height = 200,
                StartPosition = FormStartPosition.CenterScreen, // Modaless는 CenterParent 안먹힘.
                BackColor= Color.GreenYellow
            };
            frm.Show();
        }

        // 기본적으로 MessageBox는 모달.
        private void BtnMsgBox_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(TxtResult.Text); // 기본
            //MessageBox.Show(TxtResult.Text, caption: "메세지창"); // 캡션
            //MessageBox.Show(TxtResult.Text, "메세지창", MessageBoxButtons.AbortRetryIgnore); // 버튼 추가
            //MessageBox.Show(TxtResult.Text, "메세지창",   // 아이콘 추가
            //                MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Warning);
            MessageBox.Show(TxtResult.Text, "메세지창", MessageBoxButtons.YesNo,    // 기본포커스버튼 설정
                            MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            //MessageBox.Show(TxtResult.Text, "메세지창", 
            //MessageBoxButtons.YesNo, MessageBoxIcon.Warning,  // 오른쪽 정렬
            //MessageBoxDefaultButton.Button2, MessageBoxOptions.RightAlign);
        }

        private void BtnAddRoot_Click(object sender, EventArgs e)
        {
            TrvDummy.Nodes.Add(rnd.Next(45).ToString());
            TreeToList();
        }
        
        private void BtnAddChild_Click(object sender, EventArgs e)
        {
            if(TrvDummy.SelectedNode != null)
            {
                TrvDummy.SelectedNode.Nodes.Add(rnd.Next(50,100).ToString());
                TrvDummy.SelectedNode.Expand(); // 트리노드 하위것 펼쳐주기 반대. Collaps
                TreeToList();
            }
        }

        void TreeToList()
        {
            LsvDummy.Items.Clear(); // 리스트뷰, 트리뷰 모든 아이템 제거 초기화
            foreach (TreeNode item in TrvDummy.Nodes)
            {
                TreeToList(item);
            }
        }

        private void TreeToList(TreeNode item)
        {
            LsvDummy.Items.Add(
                new ListViewItem(new []{item.Text, item.FullPath.ToString()}));

            foreach (TreeNode node in item.Nodes)
            {
                TreeToList(node);   // 재귀호출
            }
        }

        private void RdoNormal_CheckedChanged(object sender, EventArgs e)
        {
            ChangeIndent();
        }

        private void RdoIndent_CheckedChanged(object sender, EventArgs e)
        {
            ChangeIndent();
        }

        private void Btnload_Click(object sender, EventArgs e)
        {
            PcbDummy.Image = Bitmap.FromFile("cat.jpg");
        }
        private void PcbDummy_Click(object sender, EventArgs e)
        {
            if (PcbDummy.SizeMode == PictureBoxSizeMode.Zoom)
            {
                PcbDummy.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                PcbDummy.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }


    }
}
