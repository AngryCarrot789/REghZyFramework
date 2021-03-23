using System;
using System.Collections.Generic;
using System.Text;

namespace REghZyFramework.CmdLine.Parsing.Helpers
{
    /// <summary>
    /// A helper class for making parsing easier... ish
    /// </summary>
    public static class ParserHelper
    {
        /// <summary>
        /// Returns a substring of <paramref name="numberForced"/> which removes the last character. 
        /// Useful if the user forces the number to be a double or int
        /// </summary>
        /// <param name="numberForced"></param>
        /// <returns></returns>
        public static bool TryGetForcedNumber(string numberForced, out double value)
        {
            string number = numberForced.Substring(0, numberForced.Length - 2);
            if (double.TryParse(number, out value))
                return true;
            value = 0;
            return false;
        }

        public static bool TryParseNumber(string number, out double value)
        {
            return double.TryParse(number, out value);
        }

        /// <summary>
        /// Tries to parse a string as a number that wants to force the output 
        /// number as an integer (a double but floored) or a double, or no force at all
        /// </summary>
        /// <param name="number"></param>
        /// <param name="doubleValue"></param>
        /// <returns></returns>
        public static bool TryParseForceOrUnforceNumber(string number, out double doubleValue)
        {
            if (IsForceInteger(number))
            {
                if (TryGetForcedNumber(number, out double value))
                {
                    doubleValue = Math.Floor(value);
                    return true;
                }
            }
            else if (IsForceDouble(number))
            {
                if (TryGetForcedNumber(number, out doubleValue))
                    return true;
            }
            else
            {
                if (TryParseNumber(number, out doubleValue))
                    return true;
            }
            doubleValue = 0;
            return false;
        }

        /// <summary>
        /// Whether the last character of <paramref name="argument"/> is equal to the <paramref name="endChar"/>
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="endChar"></param>
        /// <returns></returns>
        public static bool EndsWith(string argument, char endChar)
        {
            return argument[argument.Length - 1] == endChar;
        }

        /// <summary>
        /// Whether the argument wants the value to be a double (e.g. end starts with d or D)
        /// </summary>
        /// <param name="argument"></param>
        /// <returns></returns>
        public static bool IsForceDouble(string argument)
        {
            return EndsWith(argument, 'd') || EndsWith(argument, 'D');
        }

        /// <summary>
        /// Whether the argument wants the value to be a integer by flooring it down (e.g. end starts with i or I)
        /// </summary>
        /// <param name="argument"></param>
        /// <returns></returns>
        public static bool IsForceInteger(string argument)
        {
            return EndsWith(argument, 'i') || EndsWith(argument, 'I');
        }
    }
}
