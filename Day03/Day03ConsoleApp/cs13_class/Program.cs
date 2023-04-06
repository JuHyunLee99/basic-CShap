using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs13_class
{
    class Cat   // private 이라도 같은 cs13_class 안에 있기때문에 접근가능
    {
        #region <생성자>
        /// <summary>
        /// 기본 생성자
        /// </summary>
        public Cat()
        {
            Name = string.Empty;
            Color = string.Empty;
            Age = 0;
        }
        /// <summary>
        ///  사용자 지정 생성자
        /// </summary>
        /// <param name="name"></param>
        /// <param name="color"></param>
        /// <param name="age"></param>
        public Cat(string name, string color = "흰색", sbyte age = 1)
        {
            Name = name;
            Color = color;
            Age = age;
        }
        #endregion

        #region <멤버변수 - 속석>
        public string Name; // 고양이 이름
        public string Color; //색상
        public sbyte Age; // 고양이 0~255
        #endregion

        #region <멤버메서드 - 기능>
        public void Meow() 
        {
            Console.WriteLine("{0} 야옹~!!", Name);
        }

        public void Run()
        {
            Console.WriteLine("{0} 달린다.", Name);
        }
        #endregion
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Cat helloKitty = new Cat(); // holloKitty라는 이름의 객체를 생성
            helloKitty.Name = "헬로키티";
            helloKitty.Color = "흰색";
            helloKitty.Age = 50;
            helloKitty.Meow();
            helloKitty.Run();
            // 객체를 생성하면서 속성 초기화
            Cat nero = new Cat() {
                Name = "검은고양이 네로",
                Color = "검은색",
                Age = 27
            };
            nero.Meow();
            nero.Run();

            Console.WriteLine("{0}의 색상은 {1}, 나이는 {2}세입니다.",
                                helloKitty.Name, helloKitty.Color, helloKitty.Age);

            Console.WriteLine("{0}의 색상은 {1}, 나이는 {2}세입니다.",
                                nero.Name, nero.Color, nero.Age);

            //기본 생성자
            Cat yaongi = new Cat();
            Console.WriteLine("{0}의 색상은 {1}, 나이는 {2}세 입니다.",
                                yaongi.Name, yaongi.Color, yaongi.Age);
            // 사용자 지정 생성자
            Cat norangyee = new Cat("노랑이", "노란색");
            Console.WriteLine("{0}의 색상은 {1}, 나이는 {2}세 입니다",
                norangyee.Name, norangyee.Color, norangyee.Age);
        }
    }
}
