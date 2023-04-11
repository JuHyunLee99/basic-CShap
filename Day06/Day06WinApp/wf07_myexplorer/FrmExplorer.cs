using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;   // process 클래스 객체
using System.Drawing;   
using System.IO;
using System.Linq;
using System.Security.Principal;    
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wf07_myexplorer
{
    public partial class FrmExplorer : Form
    {
        public FrmExplorer()
        {
            InitializeComponent();
        }

        #region < FrmExplorer_Load 이벤트>
        private void FrmExplorer_Load(object sender, EventArgs e)
        {
            #region <LblPath 현재 사용자 정보>
            // 현재 사용자 정보출력
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            LblPath.Text = identity.Name;
            #endregion

            #region < TrvDrive 드라이브노드, 하위노드 추가>
            // 현재 컴퓨터에 존재하는 드라이브 정보 검색, 트리뷰 추가
            DriveInfo[] dirves = DriveInfo.GetDrives();

            // 트리뷰에 전부 추가
            foreach (DriveInfo drive in dirves)
            {
                // 실제 존재하는 하드드라이브만...
                if (drive.DriveType == DriveType.Fixed)
                {
                    TreeNode rootNode = new TreeNode(drive.Name);
                    rootNode.ImageIndex = 0;
                    rootNode.SelectedImageIndex = 0;
                    TrvDrive.Nodes.Add(rootNode);   // 드라이브 노드 추가
                    FillNodes(rootNode);    // 하위 노드 추가
                }
            }
            TrvDrive.Nodes[0].Expand(); // 트리뷰 노드 확장
            #endregion

            #region <LsvFolder 설정>
            LsvFolder.Columns.Add("이름", (LsvFolder.Width / 2), HorizontalAlignment.Left);
            LsvFolder.Columns.Add("날짜", (LsvFolder.Width / 4), HorizontalAlignment.Left);
            LsvFolder.Columns.Add("유형", (LsvFolder.Width / 10), HorizontalAlignment.Left);
            LsvFolder.Columns.Add("크기", (LsvFolder.Width / 10), HorizontalAlignment.Right);

            LsvFolder.FullRowSelect = true; // 한행을 전부 선택

            // 리스트뷰 설정
            // LsvFolder.View = View.Details;
            cboView.SelectedIndex = 0;  // View.Details로 초기화
            #endregion
        }
        #endregion

        /// <summary>
        /// 하위 폴더 검색, 트리뷰 채우기
        /// </summary>
        /// <param name="curNode"></param>
        #region <FillNodes 메소드>
        private void FillNodes(TreeNode curNode)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(curNode.FullPath);
                // 드라이브 하위폴더
                foreach (var item in dir.GetDirectories())
                {
                    TreeNode newNode = new TreeNode(item.Name);
                    newNode.ImageIndex = 1;
                    newNode.SelectedImageIndex = 2;
                    curNode.Nodes.Add(newNode); // 현재노드의 하위 노드 추가
                    newNode.Nodes.Add("*"); // 하위노드의 하위노드 * 추가
                
                    // *노드 추가하는 이유: 하위노드가 있어야 트리뷰 확장할 수 있어서 임시로 *노드를 추가한다.
                    //  이후 BeforeExpand 이벤트 발생할때 *노드 삭제후 진짜 하위노드 추가
                }
            }
            catch(Exception)
            {
                MessageBox.Show("오류발생!", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        /// <summary>
        /// 트리뷰 노드 확장하기 전 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region <TrvDrive_BeforeExpand 이벤트>
        // sender: 자기자신 객체 내용, e 이벤트와 관련된 속성포함
        private void TrvDrive_BeforeExpand(object sender, TreeViewCancelEventArgs e)    
        {
            if (e.Node.Nodes[0].Text == "*")
            {
                e.Node.Nodes.Clear();
                e.Node.ImageIndex = 1;  //  일반폴더 이미지
                e.Node.SelectedImageIndex= 2;      // 폴더 오픈 이미지
                FillNodes(e.Node);  // 하위폴더를 만들어줌
            }
        }
        #endregion

        /// <summary>
        /// 트리뷰 접기전 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region <TrvDrive_BeforeCollapse 이벤트>
        private void TrvDrive_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.ImageIndex = 1;
            e.Node.SelectedImageIndex = 1;
        }
        #endregion

        /// <summary>
        /// 트리노드를 마우스 클릭이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region <TrvDrive_MouseClick 이벤트>
        private void TrvDrive_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            SetLsvFolder(e.Node.FullPath);
        }
        #endregion

        /// <summary>
        /// 폴더 내 디렉토리 리스트뷰에 리스트업
        /// </summary>
        /// <param name="fullPath"></param>
        #region <SetLsvFolder 메소드>
        private void SetLsvFolder(string fullPath)
        {
            try
            {
                TxtPath.Text = fullPath;    // 현재 경로 입력
                LsvFolder.Items.Clear();    // 기존 리스트 삭제
                DirectoryInfo dir = new DirectoryInfo(fullPath);
                int dirCount = 0;

                foreach (DirectoryInfo item in dir.GetDirectories())    // 현재 디렉토리의 하위디렉토리 리스트업
                {
                    ListViewItem lvi = new ListViewItem(item.Name);

                    lvi.ImageIndex = 1;
                    lvi.Text = item.Name;   // 0인덱스

                    LsvFolder.Items.Add(lvi);
                    LsvFolder.Items[dirCount].SubItems.Add(item.CreationTime.ToString());
                    LsvFolder.Items[dirCount].SubItems.Add("폴더");
                    LsvFolder.Items[dirCount].SubItems.Add(string.Format("{0} files", item.GetFiles().Length.ToString()));

                    dirCount++;
                }   // 폴더 내 디렉토리 리스트뷰에 리스트업

                // 파일 목록 리스트업
                FileInfo[] files = dir.GetFiles();
                int fileCount = dirCount;   // 이전 카운트가 승계

                foreach (FileInfo file in files)
                {
                    ListViewItem lvi = new ListViewItem();

                    lvi.Text= file.Name;
                    lvi.ImageIndex= 4;  // 기본적인 아이콘

                    lvi.ImageIndex = SetExtImg(file.Name);
                    LsvFolder.Items.Add(lvi);

                    if (file.LastAccessTime != null)
                    {
                        LsvFolder.Items[fileCount].SubItems.Add(file.LastWriteTime.ToString());
                    }
                    else
                    {
                        LsvFolder.Items[fileCount].SubItems.Add(file.CreationTime.ToString());
                    }
                    LsvFolder.Items[fileCount].SubItems.Add(file.Attributes.ToString());
                    LsvFolder.Items[fileCount].SubItems.Add(file.Length.ToString());

                    fileCount++;
                }
            }
            catch (Exception) 
            {
                MessageBox.Show("리스트뷰 오류발생!", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        /// <summary>
        /// 리스트뷰 파일 확장자에 따라 아이콘 변경
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        #region <SetExtImg 메소드>
        private int SetExtImg(string name)
        {
            FileInfo fInfo = new FileInfo(name);
            string ext = fInfo.Extension;   // 파일 확장자 반환
            var exVal = 0;

            switch(ext)
            {
                case ".exe":    // 실행파일
                    exVal = 3; // exe아이콘
                    break;
                case ".png":    // 
                case ".PNG":
                case ".jpg":
                case ".gif":
                    exVal = 5;
                    break;

                default:
                    exVal = 4;
                    break;
            }
            return exVal;
        }
        #endregion

        /// <summary>
        /// 리스트뷰 보기 변경이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region <cboView_SelectedIndexChanged 이벤트>
        private void cboView_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboView.SelectedIndex)
            {
                case 0: // Details
                    LsvFolder.View = View.Details; break;
                case 1: // SmallIcon
                    LsvFolder.View = View.SmallIcon; break;
                case 2: // LargeIcon
                    LsvFolder.View = View.LargeIcon; break;
                case 3: // List
                    LsvFolder.View = View.List; break;
                case 4: // Tile
                    LsvFolder.View = View.Tile; break;
                //default: // 위와 같은 경우면 defual는 없어도 됨
                //  LsvFolder.View = View.Details; break;
            }
        }
        #endregion

        /// <summary>
        /// 디렉토리 경로를 입력했을때 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region <TxtPath_KeyPress 이벤트>
        private void TxtPath_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)    // KeyBoard 엔터키 누르면
            {
                try
                {
                    SetLsvFolder(TxtPath.Text);
                }
                catch (Exception) 
                {
                    MessageBox.Show("경로를 찾을 수 없습니다. 맞춤법을 확인하고 다시 시도하십시오", "나의 탐색기",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        /// <summary>
        /// 리스트뷰 파일 더블클릭처리 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        # region <LsvFolder_DoubleClick>
        private void LsvFolder_DoubleClick(object sender, EventArgs e)
        {
            if(LsvFolder.SelectedItems.Count == 1)
            {
                string processPath= TxtPath.Text + "\\" + LsvFolder.SelectedItems[0].Text;

                Process.Start(processPath);
            }

        }
        #endregion
    }
}
