using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;

namespace number_5_app
{
    internal class Program
    {
        interface IAnimal
        {
            int Age { get; set; }
            string Name { get; set; }
            void Eat();
            void Sleep();
            void Sound();
        }

        class Dog:IAnimal
        {
            public int Age { get; set; }
            public string Name { get; set; }
            public void Eat() 
            {
                Console.WriteLine($"{Name}이(가) 밥을 먹습니다.");
            }
            public void Sleep() 
            {
                Console.WriteLine($"{Name}이(가) 잠을 잡니다.");
            }
            public void Sound() 
            {
                Console.WriteLine($"{Name}이(가) 멍멍.");
            }
        }
        class Cat : IAnimal
        {
            public int Age { get; set; }
            public string Name { get; set; }
            public void Eat()
            {
                Console.WriteLine($"{Name}이(가) 밥을 먹습니다.");
            }
            public void Sleep()
            {
                Console.WriteLine($"{Name}이(가) 잠을 잡니다.");
            }
            public void Sound()
            {
                Console.WriteLine($"{Name}이(가) 야옹.");
            }
        }
        class Horse : IAnimal
        {
            public int Age { get; set; }
            public string Name { get; set; }
            public void Eat()
            {
                Console.WriteLine($"{Name}이(가) 밥을 먹습니다.");
            }
            public void Sleep()
            {
                Console.WriteLine($"{Name}이(가) 잠을 잡니다.");
            }
            public void Sound()
            {
                Console.WriteLine($"{Name}이(가) 히히");
            }
        }
        static void Main(string[] args)
        {
            Dog dog = new Dog() { Name = "바둑이" };
            dog.Sound();
            Cat cat = new Cat() { Name = "낭만 고양이" };
            cat.Sound();
            Horse horse = new Horse() { Name = "얼룩말" };
            horse.Sound();
        }
    }
}
