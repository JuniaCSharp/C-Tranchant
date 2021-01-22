using System;

namespace Exo1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press the key you want to check : ");
            Console.WriteLine("\n" + ReadChar("Est-ce que je suis ici ?") + "\n");
        }

        static char ReadChar(string charChain)
        {
            ConsoleKeyInfo userInput = Console.ReadKey();
            char userCharInput = userInput.KeyChar;

            foreach (var item in charChain)
            {
                if (item == userCharInput)
                {
                    return item;
                }
            }
            Console.WriteLine("This character isn't in the character chain");
            return '§';
        }
    }
}
