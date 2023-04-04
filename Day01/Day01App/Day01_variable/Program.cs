using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01_variable
{
    internal class Program
    {
        static void Main(string[] args)
        {
            byte bdata = byte.MaxValue;
            Console.WriteLine(bdata);

            bdata = byte.MinValue;
            Console.WriteLine(bdata);

            long ldata = long.MaxValue;
            Console.WriteLine(ldata);
            ldata = long.MinValue;
            Console.WriteLine(ldata);

            ldata--;
            Console.WriteLine(ldata);

            int binVal = 0b10;  // 2진수
            Console.WriteLine(binVal);

            int hexVal = 0xFF;
            Console.WriteLine(hexVal);

            float fdata = float.MaxValue;
            Console.WriteLine(fdata);
            fdata = float.MinValue;
            Console.WriteLine(fdata);

            double ddata = double.MaxValue;
            Console.WriteLine(ddata);
        }
    }
}
