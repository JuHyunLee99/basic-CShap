using MySql.Data.MySqlClient;
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
    public partial class FrmLogin : Form
    {

        private bool isLogined = false; // 로그인 성공 여부 변수
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            isLogined = LoginProcess(); // 로그인 성공하면 true

            if (isLogined)
            {
                this.Close();
            }
            else
            {
                return;
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            //Application.Exit();
            Environment.Exit(0);    // 가장 완벽하게 프로그램 종료
        }

        private void FrmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {       // 창종료버튼이나 Alt+F4 했을 때 프로그램 종료하기  
            if (isLogined != true)  // 로그인 안되었을때 창을 닫으면 프로그램 모두 종료
            {
                Environment.Exit(0);    // 없으면 로그인폼 닫았을때 메인폼이 나타남
            }
        }

        private void TxtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13) // 엔터키 누르면
            {
                BtnLogin_Click(sender, e);  // 버튼 클릭 이벤트 핸들러 호출
            }
        }


        // DB userTbl에서 정보화확인 로그인 처리
        private bool LoginProcess()
        {
            // Vailidation check
            if (string.IsNullOrEmpty(TxtUserId.Text))
            {
                MessageBox.Show("유저아이디를 입력하세요", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrEmpty(TxtPassword.Text))
            {
                MessageBox.Show("패스워드를 입력하세요", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            string strUserId = "";
            string strPassword = "";

            try
            {
                // DB처리     
                using (MySqlConnection conn = new MySqlConnection(Helpers.Commons.ConnString))
                {
                    conn.Open();

                    #region <<DB쿼리를 위한 구성>>
                    string selQuery = @"SELECT userId
	                                         , password
                                          FROM usertbl
                                         WHERE USERId=@userID
                                           AND password = @password;";
                    MySqlCommand selCmd = new MySqlCommand(selQuery, conn);
                    // @userID, @password 파라미터 할당
                    MySqlParameter prmUserID = new MySqlParameter("@userID", TxtUserId.Text);
                    MySqlParameter prmPassword = new MySqlParameter ("@password", TxtPassword.Text);
                    selCmd.Parameters.Add(prmUserID);
                    selCmd.Parameters.Add(prmPassword);
                    #endregion

                    MySqlDataReader reader = selCmd.ExecuteReader();    // 실행한 다음에 userId, password
                    if (reader.Read())
                    {
                        strUserId = reader["userId"] != null ? reader["userId"].ToString() : "-";
                        strPassword = reader["password"] != null ? reader["password"].ToString() : "-";
                        //MessageBox.Show($"{strUserId}/{strPassword}");
                        return true;
                    }
                    else
                    {
                        MessageBox.Show($"로그인 정보가 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }   // con.Close(); 
               
            }
            catch (Exception ex)
            {
                MessageBox.Show($"비정상적 오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            
        }


        // 유저아이디 텍스트박스에서 엔터를 치면 패스워드 텍스트박스로 포커스 이동
        private void TxtUserId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) 
            {
                TxtPassword.Focus();
            }
        }
    }
}
