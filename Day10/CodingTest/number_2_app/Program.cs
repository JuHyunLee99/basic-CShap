using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace number_2_app
{
    class Boiler
    {
        public string Brand { get; set; }
        private byte voltage;
        public byte Voltage
        {
            get { return this.voltage; }
            set
            {
                if (value != 110 && value != 220)
                {
                    value = 0;
                    Console.WriteLine("110V나 220V만 가능. 다시 설정하도록!");
                }
                voltage = value;

            }
        }
        private int temperature;
        public int Temperature
        {
            get { return this.temperature; }
            set
            {
                if (value <= 5)
                {
                    value = 5;
                }
                else if (value >= 70)
                {
                    value = 70;
                }
                    temperature = value;
            }
        }

        public void PrintAll()
        {
            Console.WriteLine($"Brand: {Brand} Voltage: {Voltage}V Temperature: {Temperature}℃");
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Boiler kitturami = new Boiler() { Brand = "귀뚜라미", Voltage = 220, Temperature = 45 };
            kitturami.PrintAll();
        }

    }
}
