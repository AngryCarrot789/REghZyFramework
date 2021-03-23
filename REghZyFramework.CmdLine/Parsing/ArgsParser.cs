using System.Collections.Generic;
using REghZyFramework.CmdLine.Parsing.Exceptions;
using REghZyFramework.CmdLine.Parsing.Helpers;

namespace REghZyFramework.CmdLine.Parsing
{
    /// <summary>
    /// A parser for converting an array of arguments into a <see cref="CommandOptions"/> 
    /// class for an easier way to use command line arguments
    /// </summary>
    public class ArgsParser
    { 
        public const char DEFAULT_OPTION_STARTER = '-';

        /// <summary>
        /// The character that is used to determind option parameters from the value parameters (e.g, with a value of '-', -myFlag, -someonesName jake)
        /// </summary>
        public char OptionStarter;
        private readonly string[] Arguments;
        private readonly Dictionary<string, ParameterType> OptionsToParse;

        private readonly HashSet<string> FlagOptions;
        private readonly Dictionary<string, string> StringOptions;
        private readonly Dictionary<string, double> DoubleOptions;
        private readonly Dictionary<string, string[]> StringArrayOptions;
        private readonly Dictionary<string, double[]> DoubleArrayOptions;
        private readonly Dictionary<string, DoublePair> RangeOptions;

        public ArgsParser(string[] args, char optionStarter = DEFAULT_OPTION_STARTER)
        {
            OptionStarter = optionStarter;
            this.Arguments = args;
            this.OptionsToParse = new Dictionary<string, ParameterType>(args.Length > 0 ? (args.Length / 2) : 0);

            FlagOptions = new HashSet<string>();
            StringOptions = new Dictionary<string, string>();
            DoubleOptions = new Dictionary<string, double>();
            StringArrayOptions = new Dictionary<string, string[]>();
            DoubleArrayOptions = new Dictionary<string, double[]>();
            RangeOptions = new Dictionary<string, DoublePair>();
        }

        /// <summary>
        /// Adds an option that will be parsed based on the given <see cref="ParameterType"/> 
        /// when the <see cref="Parse(int)"/> or <see cref="ForceParse(int)"/> function is called
        /// </summary>
        /// <param name="optionName"></param>
        /// <param name="parameter"></param>
        public void AddOption(string optionName, ParameterType parameter)
        {
            OptionsToParse.Add(optionName, parameter);
        }

        ///// <summary>
        ///// Adds aliases (aka alternative names) to an existing option
        ///// </summary>
        ///// <param name="optionName"></param>
        ///// <param name="aliases"></param>
        //public void AddOptionAliases(string optionName, params string[] aliases)
        //{
        //    if (OptionsToParse.TryGetValue(optionName, out ParameterType parameter))
        //    {
        //        for (int i = 0; i < aliases.Length; i++)
        //        {
        //            OptionsToParse.Add(aliases[i], parameter);
        //        }
        //    }
        //}

        /// <summary>
        /// Parses all of the options if possible. Throws exceptions with detailed info if a parse fails due 
        /// to user error 
        /// <para>
        /// (e.g. forgetting a - at the start of an option, or adding 3 numbers to a number range)
        /// </para>
        /// </summary>
        /// <param name="startIndex">An offset to the arguments</param>
        /// <returns></returns>
        public CommandOptions Parse(int startIndex = 0)
        {
            for (int i = startIndex; i < Arguments.Length; i++)
            {
                string option = Arguments[i];
                if (!IsOption(option))
                    throw new ParseFailedException(option, ParameterType._None, $"Starting option '{option}' didn't begin with '{OptionStarter}'. This is user-error");

                option = option.Substring(1);
                if (OptionsToParse.TryGetValue(option, out ParameterType parameter))
                {
                    switch (parameter)
                    {
                        case ParameterType.Flag:
                            ParseNextFlag(option);
                            break;
                        case ParameterType.String:
                            ParseNextString(option, ref i);
                            break;
                        case ParameterType.Number:
                            ParseNextNumber(option, ref i, true);
                            break;
                        case ParameterType.Range:
                            ParseNextRange(option, ref i, true);
                            break;
                        case ParameterType.StringArray:
                            ParseNextStringArray(option, ref i);
                            break;
                        case ParameterType.NumberArray:
                            ParseNextNumberArray(option, ref i, true);
                            break;
                        default:
                            break;
                    }
                }
            }
            return new CommandOptions(
                FlagOptions, StringOptions, DoubleOptions,
                StringArrayOptions, DoubleArrayOptions, RangeOptions);
        }

        /// <summary>
        /// Parses all of the options if possible. Does not throw exceptions if a parse fails, the error will simply be ignored and 
        /// </summary>
        /// <param name="startIndex">An offset to the arguments</param>
        /// <returns></returns>
        public CommandOptions ForceParse(int startIndex = 0)
        {
            for (int i = startIndex; i < Arguments.Length; i++)
            {
                string option = Arguments[i];
                if (!IsOption(option))
                    continue;

                option = option.Substring(1);
                if (OptionsToParse.TryGetValue(option, out ParameterType parameter))
                {
                    switch (parameter)
                    {
                        case ParameterType.Flag:
                            ParseNextFlag(option);
                            break;
                        case ParameterType.String:
                            ParseNextString(option, ref i);
                            break;
                        case ParameterType.Number:
                            ParseNextNumber(option, ref i);
                            break;
                        case ParameterType.Range:
                            ParseNextRange(option, ref i);
                            break;
                        case ParameterType.StringArray:
                            ParseNextStringArray(option, ref i);
                            break;
                        case ParameterType.NumberArray:
                            ParseNextNumberArray(option, ref i);
                            break;
                        default:
                            break;
                    }
                }
            }
            return new CommandOptions(
                FlagOptions, StringOptions, DoubleOptions, 
                StringArrayOptions, DoubleArrayOptions, RangeOptions);
        }

        private void ParseNextFlag(string option)
        {
            FlagOptions.Add(option);
        }

        private void ParseNextString(string option, ref int currentIndex)
        {
            StringOptions.Add(option, Arguments[++currentIndex]);
        }

        private void ParseNextNumber(string option, ref int currentIndex, bool throwException = false)
        {
            string argument = Arguments[++currentIndex];
            if (ParserHelper.TryParseForceOrUnforceNumber(argument, out double value))
                DoubleOptions.Add(option, value);
            else if (throwException)
                throw new ParseFailedException(option, ParameterType.Number, "Failed to parse a number");
        }

        private void ParseNextRange(string option, ref int currentIndex, bool throwException = false)
        {
            if (ParserHelper.TryParseForceOrUnforceNumber(Arguments[++currentIndex], out double a))
            {
                if (ParserHelper.TryParseForceOrUnforceNumber(Arguments[++currentIndex], out double b))
                    RangeOptions.Add(option, new DoublePair(a, b));
                else if (throwException)
                    throw new ParseFailedException(option, ParameterType.Number, "Failed to parse the end/last number of the range");
            }
            else if (throwException)
                throw new ParseFailedException(option, ParameterType.Number, "Failed to parse the start/first number of the range");
        }

        private void ParseNextStringArray(string option, ref int currentIndex)
        {
            List<string> strList = new List<string>();
            while (true)
            {
                string arg = Arguments[++currentIndex];
                if (IsOption(arg))
                {
                    currentIndex--;
                    break;
                }
                strList.Add(arg);
            }
            StringArrayOptions.Add(option, strList.ToArray());
        }

        private void ParseNextNumberArray(string option, ref int currentIndex, bool throwException = false)
        {
            List<double> doubleList = new List<double>();
            while (true)
            {
                string arg = Arguments[++currentIndex];
                if (IsOption(arg))
                {
                    currentIndex--;
                    break;
                }
                if (ParserHelper.TryParseForceOrUnforceNumber(arg, out double doubleValue))
                    doubleList.Add(doubleValue);
                else if (throwException)
                    throw new ParseFailedException(option, ParameterType.NumberArray, "Failed to parse a number in the array. Index: " + doubleList.Count);
            }
            DoubleArrayOptions.Add(option, doubleList.ToArray());
        }

        private bool IsOption(string argument)
        {
            return argument[0] == OptionStarter;
        }
    }
}
