using System;
using REghZyFramework.CmdLine;
using REghZyFramework.CmdLine.Parsing;

namespace REghZyFramework.ADemo
{
    class Program
    {
        public static void Main(string[] args)
        {
            /*
             * args = {
             *     -name hhhh -age 69 -arr okay then ecks dee "no u" -efficiency 21 40
             * }
             */

            foreach(string arg in args)
            {
                Console.WriteLine($"Argument: {arg}");
            }

            ArgsParser parser = new ArgsParser(args);
            HelpOptions help = new HelpOptions("Demo");
            help.AddCommand("doit", ParameterType.Flag, "DO it lol", parser);
            help.AddCommand("delsys32", ParameterType.Flag, "Delete system32", parser);
            help.AddCommand("name", ParameterType.String, "The name of the person", parser);
            help.AddCommand("arr", ParameterType.StringArray, "An array of stuff idek lol", parser);
            help.AddCommand("age", ParameterType.Number, "The age of the person", parser);
            help.AddCommand("efficiency", ParameterType.Range, "The efficiency of the person", parser);
            help.Write();
            CommandOptions options = parser.ForceParse(0);

            if (options.HasFlag("doit"))
            {

            }
            else
            {
                Console.WriteLine("Doesnt have DoIt parameter");
            }

            if (options.HasFlag("delsys32"))
            {
                Console.WriteLine("Deleting system32...");
            }

            if (options.TryGetString("name", out string name))
            {
                Console.WriteLine($"Name: {name}");
            }

            if (options.TryGetNumber("age", out double age))
            {
                Console.WriteLine($"Age: {age}");
            }

            if (options.TryGetRange("efficiency", out double a, out double b))
            {
                Console.WriteLine($"Efficiency: {a}, {b}");
            }

            if (options.TryGetStringArray("arr", out string[] arr))
            {
                foreach (string val in arr)
                {
                    Console.WriteLine(val);
                }
            }

            Console.Read();
        }
    }
}
