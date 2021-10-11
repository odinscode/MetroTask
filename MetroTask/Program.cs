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
                "beer",
                "cigarettes",
                "condoms"
            };

            var input = string.Empty;
            var isAppRunning = true;

            do
            {
                Display();
                input = Console.ReadLine().Trim().ToLower();
                if (input.Equals("exit"))
                {
                    isAppRunning = false;
                } 
                else if (input.Equals("display"))
                {
                    Display();
                }
                else if (input.Contains(' ') && input.StartsWith("buy"))
                {
                    Buy(input.Substring(input.IndexOf(' ')));
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

        static void Buy(string productName)
        {
            if (goods.Contains(productName.TrimStart()))
            {
                System.Console.WriteLine($"Thanks for purchasing {productName}");
            }
            else
            {
                System.Console.WriteLine($"{productName} is not availabe");
            }
        }
    }
}
