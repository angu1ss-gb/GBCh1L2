using System;
using System.Collections.Generic;
using System.Text;

namespace WorkingHours
{
    internal class Program
    {
        [Flags]
        private enum Days
        {
            None      = 0b00000000, // 0x00
            Monday    = 0b00000001, // 0x01
            Tuesday   = 0b00000010, // 0x02
            Wednesday = 0b00000100, // 0x04
            Thursday  = 0b00001000, // 0x08
            Friday    = 0b00010000, // 0x10
            Saturday  = 0b00100000, // 0x20
            Sunday    = 0b01000000, // 0x40
            Weekdays  = Monday | Tuesday | Wednesday | Thursday | Friday,
            Weekends  = Saturday | Sunday,
            All       = Weekdays | Weekends,
        }
        
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Dictionary<String, Days> companies = new Dictionary<string, Days>()
            {
                {"Company 0", Days.All},
                {"Company 1", Days.Weekdays},
                {"Company 2", Days.Weekends},
                {"Company 3", Days.Monday | Days.Wednesday | Days.Friday},
                {"Company 4", Days.Tuesday | Days.Thursday},
                {"Company 5", Days.Monday | Days.Sunday},
            };

            var requestedDayNumber = GetIntFromUser("Enter the number of the weekday", 1, 7);

            Days requestedDay;
            switch (requestedDayNumber)
            {
                case 1:
                    requestedDay = Days.Monday;
                    break;
                case 2:
                    requestedDay = Days.Tuesday;
                    break;
                case 3:
                    requestedDay = Days.Wednesday;
                    break;
                case 4:
                    requestedDay = Days.Thursday;
                    break;
                case 5:
                    requestedDay = Days.Friday;
                    break;
                case 6:
                    requestedDay = Days.Saturday;
                    break;
                case 7:
                    requestedDay = Days.Sunday;
                    break;
                default:
                    throw new Exception("Unexpected number of the weekday (requestedDayNumber is out of range)");
            }

            Console.WriteLine("Opened companies:");
            foreach (KeyValuePair<String, Days> company in companies)
            {
                if (requestedDay == (company.Value & requestedDay))
                    ColorfulWriteLine(new Dictionary<object, ConsoleColor?>()
                    {
                        {"\t- ", null},
                        {company.Key, ConsoleColor.DarkCyan},
                        {$" (works on {company.Value})", ConsoleColor.DarkRed},
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