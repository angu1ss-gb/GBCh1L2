using System;
using System.Collections.Generic;
using System.Text;

namespace IsEven
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            if (0 == GetIntFromUser("Enter number") % 2)
            {
                ColorfulWriteLine(new Dictionary<object, ConsoleColor?>()
                {
                    {"Number is ", null},
                    {"even", ConsoleColor.DarkCyan},
                });
            }
            else
            {
                ColorfulWriteLine(new Dictionary<object, ConsoleColor?>()
                {
                    {"Number is ", null},
                    {"odd", ConsoleColor.DarkCyan},
                });
            }


            // User-friendly app finish
            Console.WriteLine();
            ColorfulWriteLine(new Dictionary<object, ConsoleColor?>()
            {
                {"Press ", null},
                {"<Enter>", ConsoleColor.DarkCyan},
                {" to close application...", null},
            });
            Console.Read();
        }
        
        static Int32 GetIntFromUser(String textRequest)
        {
            Console.WriteLine(textRequest);
            var userNumber = Console.ReadLine();
            if (Int32.TryParse(userNumber, out var number))
            {
                return number;
            }
            else
            {
                Console.WriteLine("Invalid input. Try again");
                return GetIntFromUser(textRequest);
            }
        }
        
        static void ColorfulWriteLine(Dictionary<object, ConsoleColor?> dataDictionary)
        {
            foreach (KeyValuePair<object, ConsoleColor?> entry in dataDictionary)
            {
                if (null == entry.Value)
                {
                    Console.Write(entry.Key);
                }
                else
                {
                    Console.ForegroundColor = (ConsoleColor) entry.Value;
                    Console.Write(entry.Key);
                    Console.ResetColor();
                }
            }
            Console.WriteLine();
        }
    }
}