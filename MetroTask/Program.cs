using System;
using System.Collections.Generic;

namespace MetroTask
{
    class Program
    {
        static List<string> goods;

        static void Main(string[] args)
        {
            goods = new List<string>()
            {
                "Beer",
                "Cigarettes",
                "Condoms"
            };

            var input = string.Empty;
            var isAppRunning = true;
            
            do
            {
                input = Console.ReadLine();
                if (input.Equals("exit"))
                {
                    isAppRunning = false;
                } 
                else if (input.Equals("display"))
                {
                    Display();
                }
                else
                {
                    System.Console.WriteLine("You entered some dich");
                }
            }
            while(isAppRunning);
        }

        static void Display()
        {
            System.Console.WriteLine("Welcome to the vending machine! Current products are available: ");
            Program.goods.ForEach(_ => System.Console.WriteLine(_));
            System.Console.WriteLine(@"To turn off vending machine use 'exit' command");
        }
    }
}
