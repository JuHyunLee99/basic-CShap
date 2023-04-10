using cs19_interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs19_interface
{
    interface ILogger
    {   
        // 구현부 없음(선언만!, 구현은 클래스에서), public으로 선언
        void WriteLog(string log);
    }

    interface IFormattableLogger : ILogger
    {
        void WriteLog(string format, params object[] args);
    }

    class ConsoleLogger : ILogger   // 구현
    {
        public void WriteLog(string log)
        {
            Console.WriteLine("{0} {1}", DateTime.Now.ToLocalTime(), log);
        }
    }
    class ConsoleLogger2 : IFormattableLogger
    {
        public void WriteLog(string format, params object[] args)
        {
            string message = string.Format(format, args);
            Console.WriteLine("{0}, {1}", DateTime.Now.ToLocalTime(), message);
        }

        public void WriteLog(string log)
        {
            Console.WriteLine("{0} {1}", DateTime.Now.ToLocalTime(), log);
        }
    }

    class Car
    {
        public string Name { get; set; }
        public string Color { get; set; }

        public void Stop()
        {
            Console.WriteLine("정지");
        }
    }
    
    interface IHoverable
    {
        void Hover();   // 물에서 달린다.
    }
    interface IFlyable
    {
        void Fly();     // 날다
    }

    // C#에는 다중상속 불가(죽음의 다이아몬드 문제) -> 인터페이스 사용
    class FlyHoverCar : Car, IFlyable, IHoverable
    {
        public void Fly()
        {
            Console.WriteLine("납니다.");
        }

        public void Hover()
        {
            Console.WriteLine("물에서 달립니다.");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // 인터페이스는 인스턴스 생성 불가(참조는 가능)
            //ILogger logger = new ILogger();
            // 인터페이스를 상속받는 클래스의 인스턴스 생성은 가능
            ILogger logger = new ConsoleLogger();
            logger.WriteLog("안녕~!!!");
            
            IFormattableLogger logger2 = new ConsoleLogger2();
            logger2.WriteLog("{0} x {1} = {2}", 6, 5, 6 * 5);
        }
    }
}
