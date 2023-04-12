using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs27_delegatechain
{
    delegate void ThereIsAFire(string location); //불났을때 대신 해주는 대리자

    delegate int Calc(int a, int b);

    delegate string Concatenate(string[] args);

    #region <클래스 Sample 람다식 프로퍼티>
    class Sample
    {
        private int valuseA; // 멤버변수

        public int ValueA  // 프로퍼티
        {
            //get { return valuseA; }
            //set { valuseA = value; }
            get => valuseA;
            set => valuseA = value;
        }
    }
    #endregion

    internal class Program
    {
        #region
        static void Call119(string location)
        {
            Console.WriteLine("소방서죠? {0}에 불났어요!!", location);
        }

        static void Shoutout(string location) 
        {
            Console.WriteLine("{0}에 불났어요!", location);
        }

        static void Escape(string location)
        {
            Console.WriteLine("{0}에서 탈출합니다.", location);
        }
        #endregion


        static string ProConcate(string[] args)
        {
            string result = string.Empty;   // =="";
            foreach (string s in args)
            {
                result += s + "/";
            }
            return result; 
        }
        static void Main(string[] args)
        {
            Concatenate concat = new Concatenate(ProConcate);
            var result = concat(args);

            Console.WriteLine(result);

            Console.WriteLine("람다식으로");
            Concatenate concat2 = (arr) =>
            {
                string res = string.Empty;
                foreach (string s in args)
                {
                    result += s + "/";
                }
                return result;
            };
            Console.WriteLine(concat2);

            

            var loc = "우리집";
            Call119(loc);
            Shoutout(loc);
            Escape(loc);

            // 불이 날수도 있으닌깐 미리 준비
            var otherloc = "경찰서";
            ThereIsAFire fire = new ThereIsAFire(Call119);
            fire += new ThereIsAFire(Shoutout);
            fire += new ThereIsAFire(Escape);   // 대리자에 메서드 추가

            fire(otherloc);

            fire -= new ThereIsAFire(Shoutout); // 대리자에서 매서드를 삭제

            fire("다른집");

            // 익명함수
            Calc plus = delegate (int a, int b)
            {
                return a + b; 
            };

            Console.WriteLine(plus(6, 7));

            Calc minus = (a,b)=> { return a-b; };

            Console.WriteLine(minus(67, 9));
        }
    }
}
