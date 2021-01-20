using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace AdvansedBillGenerator
{
    class Item
    {
        public String Name;
        public Int32 Count;
        public Single Cost;
        public Boolean IsTax20;
    }
    
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            
            CultureInfo p = new CultureInfo("en-US");
            Random r = new Random();

            String cashier = GetLimitedStringFromUser("Enter cashier second name and initials", 20);
            
            Int32 itemsCount = GetIntFromUser("Enter the number of purchases", 1, 327);
            
            Item[] items = new Item[itemsCount];
            Single total = 0;
            Single tax20 = 0;
            Single tax10 = 0;
            for (Int32 i = 0; i < itemsCount; i++)
            {
                Item item = new Item
                {
                    Name = GetLimitedStringFromUser("Enter position name", 44),
                    Count = GetIntFromUser("Enter the number of purchased items", 1, 1000),
                    Cost = GetSingleFromUser("Enter position cost", 0, 1000000),
                    IsTax20 = GetBoolFromUser("20% tax?"),
                };

                total += item.Count * item.Cost;
                if (item.IsTax20)
                    tax20 += item.Count * item.Cost * (Single) 0.2;
                else
                    tax10 += item.Count * item.Cost * (Single) 0.1;

                items[i] = item;
            }
            
            Single payCash = total;
            Single payCard = 0;
            Single payExchange = 0;
            Single payAdvance = 0;
            Single payCredit = 0;
            
            // Info
            Console.WriteLine(); Console.WriteLine();
            ColorfulWriteLine(new Dictionary<object, ConsoleColor?>()
            {
                {"Colors help:\r\n", null},
                {"\t- Inputted\r\n", ConsoleColor.DarkCyan},
                {"\t- Generated\r\n", ConsoleColor.DarkGreen},
                {"\t- Calculated\r\n", ConsoleColor.DarkRed},
            });
            Console.WriteLine(); Console.WriteLine();
            
            // Bill
            Console.WriteLine(PadBoth("Кассовый чек", 44));
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("ПРИХОД".PadRight(44));
            Console.WriteLine("ООО ГИКБРЕИНС".PadRight(44));
            Console.WriteLine("ИНН 7726381870".PadRight(44));
            Console.WriteLine("115280, г. Москва, ул. Ленинская Слобода, 78".PadRight(44));
            Console.WriteLine("+7 495 495-51-46".PadRight(44));
            Console.ResetColor();
            Console.WriteLine("".PadRight(44));
            ColorfulWriteLine(new Dictionary<object, ConsoleColor?>()
            {
                {DateTime.Now.ToString("dd.MM.yy   HH:mm:ss").PadRight(28), ConsoleColor.DarkRed},
                {"Чек №".PadRight(10), null},
                { r.Next(1,999999).ToString().PadLeft(6, '0'), ConsoleColor.DarkGreen},
            });
            ColorfulWriteLine(new Dictionary<object, ConsoleColor?>()
            {
                {"Кассир:".PadRight(8), null},
                {cashier.PadRight(20), ConsoleColor.DarkCyan},
                {"Смена №".PadRight(10), null},
                { r.Next(1,999999).ToString().PadLeft(6, '0'), ConsoleColor.DarkGreen},
            });
            Console.WriteLine("".PadRight(44));
            ColorfulWriteLine(new Dictionary<object, ConsoleColor?>()
            {
                {"ФН".PadRight(28), null},
                { r.Next(1,99999999).ToString().PadLeft(8, '0'), ConsoleColor.DarkGreen},
                { r.Next(1,99999999).ToString().PadRight(8, '0'), ConsoleColor.DarkGreen},
            });
            ColorfulWriteLine(new Dictionary<object, ConsoleColor?>()
            {
                {"РН ККТ".PadRight(28), null},
                { r.Next(1,99999999).ToString().PadLeft(8, '0'), ConsoleColor.DarkGreen},
                { r.Next(1,99999999).ToString().PadRight(8, '0'), ConsoleColor.DarkGreen},
            });
            ColorfulWriteLine(new Dictionary<object, ConsoleColor?>()
            {
                {"ЗН ККТ".PadRight(34), null},
                { r.Next(1,99999).ToString().PadLeft(5, '0'), ConsoleColor.DarkGreen},
                { r.Next(1,99999).ToString().PadRight(5, '0'), ConsoleColor.DarkGreen},
            });
            ColorfulWriteLine(new Dictionary<object, ConsoleColor?>()
            {
                {"ФД".PadRight(34), null},
                { r.Next(1,99999).ToString().PadLeft(5, '0'), ConsoleColor.DarkGreen},
                { r.Next(1,99999).ToString().PadRight(5, '0'), ConsoleColor.DarkGreen},
            });
            ColorfulWriteLine(new Dictionary<object, ConsoleColor?>()
            {
                {"ФПД".PadRight(34), null},
                { r.Next(1,99999).ToString().PadLeft(5, '0'), ConsoleColor.DarkGreen},
                { r.Next(1,99999).ToString().PadRight(5, '0'), ConsoleColor.DarkGreen},
            });
            Console.WriteLine("".PadRight(44, '-'));
            Console.WriteLine("".PadRight(44));
            foreach (Item item in items)
            {
                ColorfulWriteLine(new Dictionary<object, ConsoleColor?>()
                {
                    {item.Name.PadRight(44), ConsoleColor.DarkCyan},
                });
                ColorfulWriteLine(new Dictionary<object, ConsoleColor?>()
                {
                    {String.Concat(item.Count, " X ", item.Cost.ToString("F", p)).PadRight(28), ConsoleColor.DarkCyan},
                    {String.Concat("=", (item.Count * item.Cost).ToString("F", p)).PadLeft(16), ConsoleColor.DarkCyan},
                });
                if (item.IsTax20)
                    ColorfulWriteLine(new Dictionary<object, ConsoleColor?>()
                    {
                        {"НДС 20%", ConsoleColor.DarkRed},
                        {String.Concat("=", (item.Count * item.Cost * 0.2).ToString("F", p)).PadLeft(37), ConsoleColor.DarkRed},
                    });
                else
                    ColorfulWriteLine(new Dictionary<object, ConsoleColor?>()
                    {
                        {"НДС 10%", ConsoleColor.DarkRed},
                        {String.Concat("=", (item.Count * item.Cost * 0.1).ToString("F", p)).PadLeft(37), ConsoleColor.DarkRed},
                    });
                Console.WriteLine("".PadRight(44));
            }
            Console.WriteLine("".PadRight(44, '-'));
            ColorfulWriteLine(new Dictionary<object, ConsoleColor?>()
            {
                {"ИТОГ", null},
                {String.Concat("=", total.ToString("F", p)).PadLeft(40), ConsoleColor.DarkRed},
            });
            Console.WriteLine("".PadRight(44));
            ColorfulWriteLine(new Dictionary<object, ConsoleColor?>()
            {
                {"НАЛИЧНЫМИ".PadRight(19), null},
                {String.Concat("=", payCash.ToString("F", p)).PadLeft(25), ConsoleColor.DarkRed},
            });
            ColorfulWriteLine(new Dictionary<object, ConsoleColor?>()
            {
                {"БЕЗНАЛИЧНЫМИ".PadRight(19), null},
                {String.Concat("=", payCard.ToString("F", p)).PadLeft(25), ConsoleColor.DarkRed},
            });
            ColorfulWriteLine(new Dictionary<object, ConsoleColor?>()
            {
                {"ОБМЕН".PadRight(19), null},
                {String.Concat("=", payExchange.ToString("F", p)).PadLeft(25), ConsoleColor.DarkRed},
            });
            ColorfulWriteLine(new Dictionary<object, ConsoleColor?>()
            {
                {"ПРЕДОПЛАТА (АВАНС)".PadRight(19), null},
                {String.Concat("=", payAdvance.ToString("F", p)).PadLeft(25), ConsoleColor.DarkRed},
            });
            ColorfulWriteLine(new Dictionary<object, ConsoleColor?>()
            {
                {"ПОСТОПЛАТА (КРЕДИТ)".PadRight(19), null},
                {String.Concat("=", payCredit.ToString("F", p)).PadLeft(25), ConsoleColor.DarkRed},
            });
            Console.WriteLine("".PadRight(44));
            ColorfulWriteLine(new Dictionary<object, ConsoleColor?>()
            {
                {"НАЛОГООБЛОЖЕНИЕ".PadRight(41), null},
                {"ОСН", null},
            });
            Console.WriteLine("".PadRight(44));
            ColorfulWriteLine(new Dictionary<object, ConsoleColor?>()
            {
                {"Сумма НДС 20%", null},
                {String.Concat("=", tax20.ToString("F", p)).PadLeft(31), ConsoleColor.DarkRed},
            });
            ColorfulWriteLine(new Dictionary<object, ConsoleColor?>()
            {
                {"Сумма НДС 10%", null},
                {String.Concat("=", tax10.ToString("F", p)).PadLeft(31), ConsoleColor.DarkRed},
            });
            Console.WriteLine("".PadRight(44, '-'));
            Console.WriteLine("".PadRight(44));
            Console.WriteLine("Сайт ФНС: nalog.ru".PadRight(44));
            Console.WriteLine("ОФД: ООО ПС СТ, ofd.ru".PadRight(44));
            
            
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
        
        static String GetLimitedStringFromUser(String textRequest, Int32 limit)
        {
            ColorfulWriteLine(new Dictionary<object, ConsoleColor?>()
            {
                {textRequest, null},
                {" (chars limit: ", null},
                {limit, ConsoleColor.DarkRed},
                {")", null},
            });
            var userInput = Console.ReadLine();
            if (userInput.Length <= limit)
            {
                return userInput;
            }
            else
            {
                Console.WriteLine("Invalid input (out of range). Try again");
                return GetLimitedStringFromUser(textRequest, limit);
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
        
        static Single GetSingleFromUser(String textRequest, Single minValue, Single maxValue)
        {
            Console.WriteLine(textRequest);
            var userNumber = Console.ReadLine();
            if (Single.TryParse(userNumber, out var number) && number >= minValue && number <= maxValue)
            {
                return number;
            }
            else
            {
                Console.WriteLine("Invalid input. Try again");
                return GetSingleFromUser(textRequest, minValue, maxValue);
            }
        }

        static Boolean GetBoolFromUser(String textRequest)
        {
            Console.WriteLine(textRequest);
            ColorfulWriteLine(new Dictionary<object, ConsoleColor?>()
            {
                {"Enter ", null},
                {"Y", ConsoleColor.DarkCyan},
                {" if yes or ", null},
                {"N", ConsoleColor.DarkCyan},
                {" if no", null},
            });
            switch (@Console.ReadLine().ToLower())
            {
                case "yes":
                case "да":
                case "д":
                case "y":
                    return true;
                case "no":
                case "нет":
                case "н":
                case "n":
                    return false;
                default:
                    Console.WriteLine("Invalid input. Try again");
                    return GetBoolFromUser(textRequest);
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

        static String PadBoth(String source, Int32 length)
        {
            Int32 spaces = length - source.Length;
            Int32 padLeft = spaces/2 + source.Length;
            return source.PadLeft(padLeft).PadRight(length);
        }
    }
}