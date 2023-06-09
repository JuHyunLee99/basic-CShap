﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs20_abstractClass
{
    abstract class AbstractParent
    {
        protected void MethodA()
        {
            Console.WriteLine("AbstractParent.MethodA()");
        }

        public void MethodB()    // 클래스랑 동일
        {
            Console.WriteLine("AbstractParent.MethodB()");
        }

        public abstract void MethodC(); // 인터페이스랑 동일!
    }

    class Child : AbstractParent 
    {
        public override void MethodC()  // 재정의(사실은 구현!)
        {
            Console.WriteLine("Child.MethodC() - 추상클래스 구현!");
            MethodA();
        }
    }

    abstract class Mammal // 포유류 최상위 클래스
    {
        public abstract void Sound();
    }

    class Dogs : Mammal
    {
        public override void Sound()
        {
            Console.WriteLine("멍멍!!");
        }
    }
    class Cats : Mammal
    {
        public override void Sound()
        {
            Console.WriteLine("야옹!!");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            AbstractParent parent = new Child();
            parent.MethodC();   // 오버라이딩한거 실행됨.
            parent.MethodB();
            // parent.MethodA(); // protected는 자기자신과 자식클래스내에서만 사용가능
        }
    }
}
