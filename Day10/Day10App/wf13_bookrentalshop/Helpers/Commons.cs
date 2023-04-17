using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wf13_bookrentalshop.Helpers
{
    internal class Commons
    {
        // static 모든 프로그램상에서 다 사용 가능
        public static readonly string ConnString = "Server=localhost;" + 
                                                   "Port=3306;" + 
                                                   "Database=bookrentalshop;" + 
                                                   "Uid=root;Pwd=12345";


        // 로그인 사용자 아이디 저장 변수
        public static string LoginID = string.Empty;
    }
}
