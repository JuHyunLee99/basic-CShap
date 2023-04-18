using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace number_4_app
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> ht = new Dictionary<string, string>();
            ht["Red"] = "빨간색";
            ht["Orange"] = "주황색";
            ht["Yellow"] = "노란색";
            ht["Green"] = "초록색";
            ht["Blue"] = "파란색";
            ht["Indigo"] = "남색";
            ht["Purple"] = "보라색";

            Console.Write("무지개 색은 ");
            foreach (string key in ht.Values) 
            {
                Console.Write($"{key}, ");
            }
            Console.WriteLine("입니다\n");


            Console.WriteLine("Key와 Value 확인");
            Console.WriteLine($"Puple은 {ht["Purple"]}입니다.");
        }

        
    }
}
