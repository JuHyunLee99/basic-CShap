using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace cs17_inheritance
{
    // 상속해줄 부모 클래스
    class Parent
    {
        protected string Name;  // 상속할때는 private을 쓰면 안됨

        public Parent(string Name)
        {
            this.Name = Name;
            Console.WriteLine("{0} from Parent 생성자", Name);
        }

        public void ParentMethod()
        {
            Console.WriteLine("{0} from Parent 매서드", Name);
        }
    }
    // 상속받을 자식 클래스
    class Child : Parent 
    {
        public Child(string Name)   : base(Name) // 부모생성자를 먼저 실행한뒤 자신 생성자를 실행
        {
            Console.WriteLine("{0} from Child 생성자", Name);
        }

        public void ChildMethod()
        {
            Console.WriteLine("{0} from child 메서드", Name);
        }
    }

    // 클래스간 형변환 DB처리, DI
    class Mannal    // 포유류
    {
        public void Nurse() // 기르다]
        {
            Console.WriteLine("포유류 기르다");
        }
    }
    class Dogs:Mannal
    {
        public void Bark()
        {
            Console.WriteLine("멍멍!!");
        }
    }
    class Cats:Mannal
    {
        public void Meow()
        {
            Console.WriteLine("야옹~");
        }
    }
    class Elephant : Mannal
    {
        public void Poo()
        {
            Console.WriteLine("푸우~");
        }
    }
    class ZooKeeper
    {
        public void Wash(Mannal mammal) // 매개변수 객체는 참조형식으로 전달
        {
            if (mammal is Elephant)
            {
                var animal = mammal as Elephant;
                Console.WriteLine("코끼리를 씻깁니다.");
                animal.Poo();
            }
            else if (mammal is Dogs)
            {
                var animal = mammal as Dogs;
                Console.WriteLine("강아지를 씻깁니다.");
                animal.Bark();
            }
            else if (mammal is Cats)
            {
                var animal = mammal as Cats;
                Console.WriteLine("고양이를 씻깁니다.");
                animal.Meow();
                animal.Meow();
                animal.Meow();
                animal.Meow();
            }
        }
    }

    internal class Program

    {
        static void Main(string[] args)
        {
            #region <기본상속 개념>
            Parent p = new Parent("p");
            p.ParentMethod();

            Console.WriteLine("자식클래스 생성");
            Child c = new Child("c");
            c.ParentMethod();   // 자식클래스는 부모클래스의 속성, 기능을 모두 쓸 수 있다.
            c.ChildMethod();
            #endregion

            #region <클래스간 형식변환>
            // Mannal mannal = new Mannal();   // 기본
            Mannal mammal = new Dogs(); // 자식클래스가 부모클래스로 변환은 가능
            //Dogs dogs = new Mammal(); // 부모클래스가 자식클래스로 변환은 불가
            Dogs dog2 = new Dogs();
            Cats cat2 = new Cats();
            Elephant el2 = new Elephant();

            //mammal.Back();    // 불가능
            if (mammal is Dogs) // Dogs가 맞는지 확인
            {
                Dogs midDog = mammal as Dogs; // Dogs로 형변환 참조형식 // (Dog)mamall 구신, 값형식에서 사용
                midDog.Bark();
            }

            ZooKeeper keeper = new ZooKeeper();
            keeper.Wash(dog2);
            keeper.Wash(cat2);
            keeper.Wash(el2);
            #endregion

        }
    }
}
