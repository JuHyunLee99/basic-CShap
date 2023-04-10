using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace cs18_override
{
    class ArmorSuite //
    {
        public virtual void Init()   // public virtual...
        {
            Console.WriteLine("기본 아머슈트");
        }
    }

    class IronMan : ArmorSuite 
    {
        public override void Init() // public override
        {
            base.Init();    // 부모클래스의 Init() 실행 가능
            Console.WriteLine("리펄서 빔");
        }
    }
    class WarMachine : ArmorSuite
    {
        public override void Init() 
        {
            // base.Init();
            Console.WriteLine("미니건");
            Console.WriteLine("돌격소총");
        }
    }

    class Parent
    {
        public void CurrentMethod()
        {
            Console.WriteLine("부모클래스 메서드");
        }
    }
    class Child : Parent
    {
        public new void CurrentMethod()             // public new (다형성x)
        {   // 기반 클래스의 메소드를 감추고 파생 클래스 구현만 표시
            Console.WriteLine("자식클래스 매서드");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("아머슈트 생산");
            ArmorSuite suite = new ArmorSuite();
            suite.Init();

            Console.WriteLine("워머신 생산");
            ArmorSuite machine = new WarMachine();
            machine.Init(); // 오버라이딩한거 출력

            Console.WriteLine("아이어맨 생산");
            IronMan iron = new IronMan();
            iron.Init();    

            Parent parent = new Parent();
            parent.CurrentMethod();

            Child child = new Child();
            child.CurrentMethod();

        }
    }
}
