using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs03_object
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //object 형식
            //int == System.Int32
            //long == System.Int64
            long idata = 1024;
            Console.WriteLine(idata);
            Console.WriteLine(idata.GetType());

            object iobj = (object)idata;    // 박싱 : 데이터타입 값을 object로 담아라
            Console.WriteLine(iobj);
            Console.WriteLine(iobj.GetType());

            long udata = (long)iobj;    // 언박싱 : object를 원래 데이터타입으로 바꿔라
            Console.WriteLine(udata);
            Console.WriteLine(udata.GetType());

            double ddata = 3.141592;
            object pobj = (object)ddata;
            double pdata = (double)pobj;

            Console.WriteLine(pobj);
            Console.WriteLine(pobj.GetType());
            Console.WriteLine(pdata);
            Console.WriteLine(pdata.GetType());

            short sdata = 3200;
            int indata = sdata;
            Console.WriteLine(sdata);

            long lndata = long.MaxValue;
            Console.WriteLine(lndata);
            indata = (int)lndata;       // overflow
            Console.WriteLine(indata);

            // float double간 형변환
            float fval = 3.141582f; //float형은 마지막에 f 써줘야 함.
            Console.WriteLine("fval = " + fval);
            double dval = (double)fval; //  정밀도 변화
            Console.WriteLine("dval = " + dval);
            Console.WriteLine(fval == dval);
            Console.WriteLine(dval == 3.141582);

            // 정말 중요!! 실무에서
            int numival = 1024;
            string strival = numival.ToString();    //문자열로 바꾸기
            Console.WriteLine(strival);
            //Console.WriteLine(numival == strival);
            Console.WriteLine(strival.GetType());

            double numdval = 3.14159265358979;
            string strdval = numdval.ToString();
            Console.WriteLine(strdval);

            // 문자열을 숫자로
            // 문자열내에 숫자가 아닌 특수문자나 정수인데 .이 있거나
            string originstr = "34567890";  //"345.67890"이면 예외발생 -> 숫자외 문자없는지 확인
            int convval = Convert.ToInt32(originstr);
            Console.WriteLine(convval);
            float convfloat = float.Parse(originstr); // 소수점 . 있는경우
            Console.WriteLine(convfloat);
            // 예외발생하지 않도록 형변환 방법
            originstr = "123.0f";
            float ffval;
            //TryParse는 예외가 발생하면 값은 0으로 대체, 예외없으면 원래값으로
            float.TryParse(originstr, out ffval);   // 예외발생하지 않게 숫자 변환 (예외시 0)
            Console.WriteLine(ffval);

            const double pi = 3.14159265358979;
            Console.WriteLine(pi);

            //pi = 4.56; // 상수는 변경 불가능
        }
    }
}
