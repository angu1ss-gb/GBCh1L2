using System;
using System.Collections.Generic;
using System.Text;

namespace MonthTemp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            String[] months =
            {
                "January",
                "February",
                "March",
                "April",
                "May",
                "June",
                "July",
                "August",
                "September",
                "October",
                "November",
                "December",
            };
            
            Decimal avgTemp = (GetDecimalFromUser("Enter min temp") + GetDecimalFromUser("Enter max temp")) / 2;
            
            Console.Write("Average temp is ");
            ColorfulWriteLine(avgTemp, ConsoleColor.DarkCyan);
            
            
            Int32 monthNumber = GetIntFromUser("Enter month number", 1, 12);
            
            Console.Write("Now is ");
            ColorfulWriteLine(months[monthNumber-1], ConsoleColor.DarkCyan);

            if (0 < avgTemp)
            {
                switch (monthNumber)
                            {
                                case 1:
                                case 2:
                                case 12:
                                    Console.WriteLine("Rainy winter");
                                    break;
                            }
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

        static Decimal GetDecimalFromUser(String textRequest)
        {
            Console.WriteLine(textRequest);
            var userNumber = Console.ReadLine();
            if (Decimal.TryParse(userNumber, out var number))
            {
                return number;
            }
            else
            {
                Console.WriteLine("Invalid input. Try again");
                return GetDecimalFromUser(textRequest);
            }
        }
        
        static Int32 GetIntFromUser(String textRequest, Int32 minValue, Int32 maxValue)
        {
            Console.WriteLine(textRequest);
            var userNumber = Console.ReadLine();
            if (Int32.TryParse(userNumber, out var number) && number >= minValue && number <= maxValue)
            {
                return number;
            }
            else
            {
                Console.WriteLine("Invalid input. Try again");
                return GetIntFromUser(textRequest, minValue, maxValue);
            }
        }

        static void ColorfulWriteLine(object line, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(line);
            Console.ResetColor();
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

        static void AppFinish()
        {
            Console.WriteLine();
            ColorfulWriteLine(new Dictionary<object, ConsoleColor?>()
            {
                {"Press ", null},
                {"<Enter>", ConsoleColor.DarkCyan},
                {" to close application...", null},
            });
            Console.Read();
        }
    }
}