using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace cs12_methods
{
    class Calc
    {
        public static int Plus(int a, int b)    
        {
            return a + b;
        }

        public int Minus(int a, int b)
        {
            return a - b;
        }
    }
    internal class Program
    {
        /// <summary>
        /// 실행시 메모리에 최초 올라가야 하기때문에 static이 되어야 하고
        /// 메서드 이름이 Main이면 실행될때 알아서 제일 처음에 시작된다.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            #region <static 메서드>
            int result = Calc.Plus(1, 2); // static은 최초 실행때 메모리에 바로올라기기때문에
            // 클래스를 객체생성할 필요없음(new Calc();). 항상 접근 가능

            //Calc.Minus(3, 2);   // Minus는 static이 아니기때문에 접근불가(객체생성해야 접근가능)
            result = new Calc().Minus(3, 2);
            Console.WriteLine(result);
            #endregion

            #region <call by reference vs call by value 비교>
            int x = 10; int y = 3;
            //ref가 없으면 Call by value
            Swap(ref x, ref y);     // x, y가 가지고 있는 주소를 전달하라 Call by reference == pointer

            Console.WriteLine("x = {0}, y = {1}", x,y);
            Console.WriteLine(GetNumber());

            #endregion

            #region <out 매개변수>

            int divid = 10;
            int divor = 3;
            int rem = 0;
            //result = Divide(divid, divor);
            //Console.WriteLine(result);
            //int rem = Reminder(divid, divor);
            //Console.WriteLine(rem);

            //Divide(divid, divor, ref result, ref rem);
            //Console.WriteLine("나누기값 {0}, 나머지{1}", result, rem);

            Divide(divid, divor, out result, out rem);
            Console.WriteLine("나누기값 {0}, 나머지{1}", result, rem);

            (result, rem) = Divide(20, 6);
            Console.WriteLine("나누기값 {0}, 나머지{1}", result, rem);
            #endregion

            #region <가변길이 매개변수>
            Console.WriteLine(Sum(1,2,3,4,5,6,7,8,9,10));
            #endregion
        }

        //static int Divide(int x, int y)
        //{
        //    return x / y;
        //}
        //static int Reminder(int x, int y)
        //{
        //    return x % y;
        //}

        // 메서드 두개 만들걸 아래처럼 하나로
        //static void Divide(int x, int y, ref int val, ref int rem)
        //{
        //    val = x / y;
        //    rem = x % y;
        //}

        // ref 대신 out 매개변수 사용
        static void Divide(int x, int y, out int val, out int rem)
        {
            val = x / y;
            rem = x % y;
        }
        // return 튜플 사용
        static (int result, int rem) Divide(int x, int y)
        {
            return(x / y, x % y);    // C# 7.0
        }
        // 오버로딩 // 리턴 값만 다른건 오버로딩 안됨.
        static (float result, int rem) Divide(float x, float y)
        {
            return (x / y, (int)(x % y));    // C# 7.0
        }

        //Main 메서드와 같은 레벨에 있는 메서드들은 전부 static이 되어야함 (무조건!)
        public static void Swap(ref int a, ref int b)
        {
            int temp = 0;
            temp = a;
            a = b;
            b = temp;
        }
        static int val = 100;
        public static ref int GetNumber()
        {
            return ref val;  //static메서드에 접근할 수 있는 변수는 static밖에 없음.
        }

        public static int Sum(params int[] args)    // Python 가변길이 매개변수(*arg)랑 비교 
        {
            int sum = 0;
            foreach (var item in args)  
            {
                sum += item;
            }
            return sum;
        }

         
    }
}
