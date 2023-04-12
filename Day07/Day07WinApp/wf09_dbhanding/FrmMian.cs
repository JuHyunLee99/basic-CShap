using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;    // SQL Server용 DB연결 클라이언트 네임스페이스
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wf09_dbhanding
{
    public partial class FrmMian : Form
    {
        public FrmMian()
        {
            InitializeComponent();
        }

        private void FrmMian_Load(object sender, EventArgs e)
        {
            // 1. 연결문자열 생성
            string connectionString = "Data Source=localhost;Initial Catalog=Northwind;Persist Security Info=True;User ID=sa;Password=12345;";
            // 2. DB연결을 위해서 Connetion객체 생성
            SqlConnection conn = new SqlConnection(connectionString);   // 연결문자열 없이 객체 생성X  // 첫번째 방법
            // conn.ConnectionString = connectionString;    // 두번재 방법

            // 3. 객체를 통해서 DB연결
            conn.Open();

            // 4. DB처리를 위한 쿼리 작성
            string selQuery = @"SELECT CustomerID
                                      ,CompanyName
                                      ,ContactName
                                      ,ContactTitle
                                      ,Address
                                      ,City
                                      ,Region
                                      ,PostalCode
                                      ,Country
                                      ,Phone
                                      ,Fax
                                  FROM Customers";
            //SqlCommand selCmd = new SqlCommand(sqlQuery, conn);
            // selCmd.Connection = conn;
            SqlDataAdapter adapter = new SqlDataAdapter(selQuery, conn);
            
            // 6. 데이터셋으로 전달
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            // 7. 데이터그리드뷰에 바인딩하기 위한 BindingSource 생성
            BindingSource source = new BindingSource();

            // 8. 데이터그리드뷰의 DataSource에 데이터셋을 할당
            source.DataSource = ds.Tables[0];
            DgvNorthwind.DataSource = source;
            
            // 9. DB close
            conn.Close();
        }
    }
}
