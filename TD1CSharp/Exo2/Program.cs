using System;
using System.Collections.Generic;

namespace Exo2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Get user inputs
            Console.WriteLine("First Number :");
            string userInput1 = Console.ReadLine();
            var checkFirstNumber = int.TryParse(userInput1, out int number1);

            Console.WriteLine("Operator (+, -, *, /) :");
            ConsoleKeyInfo userInput2 = Console.ReadKey();
            char checkOperator = userInput2.KeyChar;

            Console.WriteLine("\nSecond Number :");
            string userInput3 = Console.ReadLine();
            var checkSecondNumber = int.TryParse(userInput3, out int number2);

            //Checking valid numbers
            bool numbersCheck = true;
            if (checkFirstNumber && checkSecondNumber) numbersCheck = true;
            else numbersCheck = false;
            
            if (!numbersCheck) Console.WriteLine("You haven't entered valid numbers");
            else
            {
                //Checking operator type
                switch (checkOperator)
                {
                    case '+':
                        Console.WriteLine("Result : " + Addition(number1, number2));
                        break;
                    case '-':
                        Console.WriteLine("Result : " + Substraction(number1, number2));
                        break;
                    case '*':
                        Console.WriteLine("Result : " + Multiplication(number1, number2));
                        break;
                    case '/':
                        Console.WriteLine("Result : " + Division(number1, number2));
                        break;
                    default:
                        Console.WriteLine("You haven't entered a valid operator");
                        break;
                }
            }
        }

        private static int Addition(int number1, int number2)
        {
            return number1 + number2;
        }

        private static int Substraction(int number1, int number2)
        {
            return number1 - number2;
        }

        private static int Multiplication(int number1, int number2)
        {
            return number1 * number2;
        }

        private static int Division(int number1, int number2)
        {
            return number1 / number2;
        }
    }
}
