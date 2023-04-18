﻿using MySql.Data.MySqlClient;
using Org.BouncyCastle.Math;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using wf13_bookrentalshop.Helpers;

namespace wf13_bookrentalshop
{
    public partial class FrmBooks : Form
    {
        bool isNew = false; // flase(UPDATE) / true(INSERT)
        #region<생성자>
        public FrmBooks()
        {
            InitializeComponent();
        }
        #endregion

        #region <이벤트 핸들러>
        private void FrmGenre_Load(object sender, EventArgs e)
        {
            isNew = true;   // 신규부터 시작
            RefreshData();
            LoadCboData();  // 콤보박스에 들어갈 데이터 로드

            DtpReleaseDate.Format = DateTimePickerFormat.Custom;
            DtpReleaseDate.CustomFormat = "yyyy-MM-dd";

        }


        private void BtnNew_Click(object sender, EventArgs e)
        {
            ClearInputs();

        }
        
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (checkValidation() != true) return;

            SaveDate(); // 데이터 저장/수정
            RefreshData();  // 데이터 재조회
            ClearInputs();  //입력창 클리어
        }

        // 그리드뷰 클릭하면 발생이벤트
        private void DgvResult_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)    // 아무것도 선택안함 -1
            {
                var selData = DgvResult.Rows[e.RowIndex];
                // Debug.WriteLine(selData.ToString());
                Debug.WriteLine(selData.Cells[0].Value);
                Debug.WriteLine(selData.Cells[1].Value);
                TxtBookIdx.Text = selData.Cells[0].Value.ToString();
                TxtAuthor.Text = selData.Cells[1].Value.ToString();
                CboDivision.SelectedValue = selData.Cells[2].Value; // BOO1 == BOO1
                // selData.Cellls[3] 사용안함
                TxtNames.Text = selData.Cells[4].Value.ToString();
                DtpReleaseDate.Value = (DateTime)selData.Cells[5].Value;
                TxtISBN.Text = selData.Cells[6].Value.ToString();
                NudPrice.Text = selData.Cells[7].Value.ToString();
                TxtBookIdx.Focus();
                
                isNew = false; // 수정
            }
        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if( isNew == true)  // 신규
            {
                MessageBox.Show("삭제할 데이터를 선택하세요", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //FK제약조건으로 지울수 없는 데이터인지 먼저 확인
            using(MySqlConnection conn = new MySqlConnection(Commons.ConnString))
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                // 사용한것인지 확인하기 위해 COUNT(*) 사용
                string strChkQurery = "SELECT COUNT(*) FROM rentaltbl WHERE bookIdx = @bookIdx";
                MySqlCommand chkCmd = new MySqlCommand(strChkQurery, conn);
                MySqlParameter prmDivision = new MySqlParameter("@bookIdx", TxtBookIdx.Text);
                chkCmd.Parameters.Add(prmDivision);
                // 컬럼이 하나일경우 ExecuteScalar 쓰고  컬럼 여러개면 ExecuteReader 쓰는게 좋다.
                var result = chkCmd.ExecuteScalar();  
                
                if(result.ToString() != "0")
                {
                    MessageBox.Show("이미 대여 중인 책입니다.","삭제", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                
            }

            // 삭제여부를 물을때 아니오를 누르면 삭제 진행
            if (MessageBox.Show(this, "삭제하시겠습니까", "오류", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            // yes를 누르면 개속 진행
            // INSERT부터 시작
            DeleteDate(); //데이터 삭제 처리
            
            RefreshData();
            ClearInputs();
        }

        private void DgvResult_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DgvResult.ClearSelection(); // 최초의 첫번째열 첫번째셀 선택되어있는것을 해제
        }
        #endregion

        #region<커스텀 메서드>
        private void RefreshData()
        {
            // DB divtbl 데이터 조회 DgvResult 뿌림
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Helpers.Commons.ConnString))
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();

                    // 쿼리 작성
                    var selQuery = @"SELECT b.bookIdx
	                                      , b.Author
	                                      , b.Division
	                                      , d.Names AS DivNames
                                          , b.Names
	                                      , b.ReleaseDate
	                                      , b.ISBN
	                                      , b.Price
                                       FROM bookstbl AS b
                                      INNER JOIN divtbl AS d
                                         ON b.Division = d.Division
                                      ORDER BY b.bookIdx ASC";  // 정렬 추가
                    MySqlDataAdapter adapter = new MySqlDataAdapter(selQuery, conn);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "bookstbl"); // bookstbl DataSet 접근 가능

                    DgvResult.DataSource = ds.Tables[0];
                    //DgvResult.DataSource = ds;
                    //DgvResult.DataMember = "divtbl";
                    // 데이터그리드뷰 컬럼 헤더 제목
                    DgvResult.Columns[0].HeaderText = "책번호";
                    DgvResult.Columns[1].HeaderText = "저자명";
                    DgvResult.Columns[2].HeaderText = "책장르";
                    DgvResult.Columns[3].HeaderText = "책장르";
                    DgvResult.Columns[4].HeaderText = "책제목";
                    DgvResult.Columns[5].HeaderText = "출판일자";
                    DgvResult.Columns[6].HeaderText = "ISBN";
                    DgvResult.Columns[7].HeaderText = "책가격";
                    // 컬럼 넓이 또는 보이기
                    DgvResult.Columns[0].Width = 55;
                    DgvResult.Columns[2].Visible = false;   // B001 코드영역 보일 필요 없음.
                    DgvResult.Columns[5].Width = 78;
                    DgvResult.Columns[7].Width = 80;
                    //컬럼 정렬
                    DgvResult.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DgvResult.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    DgvResult.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"RefreshData() 비정상적 오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCboData()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Commons.ConnString))
                {
                    if(conn.State == ConnectionState.Closed) { conn.Open(); }
                    var query = "SELECT Division, Names FROM divtbl";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader(); ;
                    var temp = new Dictionary<string, string>();
                    while (reader.Read())
                    {
                        temp.Add(reader[0].ToString(), reader[1].ToString());   // (Key)BOO1, (Value) 공포/스릴러
                    }
                    // 콤보박스에 할당
                    CboDivision.DataSource = new BindingSource(temp, null);
                    CboDivision.DisplayMember = "Value";
                    CboDivision.ValueMember = "Key";
                    CboDivision.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"LoadCboData() 비정상적 오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void ClearInputs()
        {
            TxtBookIdx.Text = TxtAuthor.Text = string.Empty;
            TxtNames.Text = TxtISBN.Text = string.Empty;
            CboDivision.SelectedIndex = -1;
            DtpReleaseDate.Value = DateTime.Now;    // 오늘날짜로 초기화
            NudPrice.Value = 0;
            TxtAuthor.Focus();
            isNew = true;   // 신규
        }

        // 입력 검증(Validation check)
        private bool checkValidation()
        {
            var result = true;
            var errorMsg = string.Empty;

            if (string.IsNullOrEmpty(TxtAuthor.Text))
            {
                result = false;
                errorMsg += "● 저자명을 입력하세요.\r\n";
            }

            if (CboDivision.SelectedIndex < 0)
            {
                result = false;
                errorMsg += "● 장르를 선택하세요.\r\n";
            }

            if (string.IsNullOrEmpty(TxtNames.Text))
            {
                result = false;
                errorMsg += "● 책제목을 입력하세요.\r\n";
            }

            if (DtpReleaseDate.Value == null)
            {
                result = false;
                errorMsg += "● 출판일자를 선택하세요.\r\n";
            }

            if (result == false) 
            {
                MessageBox.Show(errorMsg, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return result;     // 메서드 탈출
            }
            else
            {
                return result;
            }
        }

        //isNew = true INSERT / false UPDATE
        private void SaveDate()
        {
            // INSERT부터 시작
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Helpers.Commons.ConnString))
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();

                    var query = "";

                    if (isNew)
                    {
                        query = @"INSERT INTO bookstbl
                                            (Author,
                                            Division,
                                            Names,
                                            ReleaseDate,
                                            ISBN,
                                            Price)
                                            VALUES
                                            (@Author
                                            , @Division
                                            , @Names
                                            , @ReleaseDate
                                            , @ISBN
                                            , @Price)";
                                            
                    }
                    else
                    {
                        query = @"UPDATE bookstbl
                                     SET Author = @Author,
                                     Division = @Division,
                                     Names = @Names,
                                     ReleaseDate = @ReleaseDate,
                                     ISBN = @ISBN,
                                     Price = @Price
                               WHERE bookIdx = @bookIdx";
                    }

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlParameter prmAuthor = new MySqlParameter("@Author", TxtAuthor.Text);
                    MySqlParameter prmDivision = new MySqlParameter("@Division", CboDivision.SelectedValue.ToString());
                    MySqlParameter prmNames = new MySqlParameter("@Names", TxtNames.Text);
                    MySqlParameter prmReleaseDate = new MySqlParameter("@ReleaseDate", DtpReleaseDate.Value);
                    MySqlParameter prmISBN = new MySqlParameter("@ISBN", TxtISBN.Text);
                    MySqlParameter prmPrice = new MySqlParameter("@Price", NudPrice.Value);

                    cmd.Parameters.Add(prmAuthor);
                    cmd.Parameters.Add(prmDivision);
                    cmd.Parameters.Add(prmNames);
                    cmd.Parameters.Add(prmReleaseDate);
                    cmd.Parameters.Add(prmISBN);
                    cmd.Parameters.Add(prmPrice);

                    if(isNew == false)  // update 할 때는 BookIdx 파라미터 추가!!
                    {
                        MySqlParameter prmBookInx = new MySqlParameter("@bookIdx", TxtBookIdx.Text);
                        cmd.Parameters.Add(prmBookInx);
                    }

                    var result = cmd.ExecuteNonQuery(); // INSERT, UPDATE, DELTE

                    if (result != 0)
                    {
                        // 저장 성공
                        MessageBox.Show("저장성공!!", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        // 저장 실패
                        MessageBox.Show("저장실패!!", "저장", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"비정상적 오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void DeleteDate()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Helpers.Commons.ConnString))
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    var query = @"DELETE FROM bookstbl
                                        WHERE bookIdx = @bookIdx";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlParameter prmDivision = new MySqlParameter("@bookIdx", TxtBookIdx.Text);
                    cmd.Parameters.Add(prmDivision);

                    var result = cmd.ExecuteNonQuery(); // INSERT, UPDATE, DELTE

                    if (result != 0)
                    {
                        // 저장 성공
                        MessageBox.Show("삭제성공!!", "삭제", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        // 저장 실패
                        MessageBox.Show("삭제실패!!", "삭제", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"비정상적 오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }






        #endregion

        private void DtpReleaseDate_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void TxtISBN_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
