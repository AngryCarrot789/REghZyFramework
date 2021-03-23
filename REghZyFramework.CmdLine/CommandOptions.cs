using System;
using System.Collections.Generic;
using REghZyFramework.CmdLine.Parsing;

namespace REghZyFramework.CmdLine
{
    /// <summary>
    /// Contains multiple <see cref="Dictionary{TKey, TValue}"/>-ies which hold parsed command line arguments,
    /// and provides functions for getting those parsed values, and some extra functions, e.g. getting an int array
    /// </summary>
    public class CommandOptions
    {
        public const char ARRAY_SPLITTER = '\u0085';
        public const char RANGE_SPLITTER = '\u001D';

        private readonly HashSet<string> FlagOptions;
        private readonly Dictionary<string, string> StringOptions;
        private readonly Dictionary<string, double> DoubleOptions;
        private readonly Dictionary<string, string[]> StringArrayOptions;
        private readonly Dictionary<string, double[]> DoubleArrayOptions;
        private readonly Dictionary<string, DoublePair> RangeOptions;

        public CommandOptions(
            HashSet<string> flags,
            Dictionary<string, string> strings,
            Dictionary<string, double> doubles,
            Dictionary<string, string[]> stringArrays,
            Dictionary<string, double[]> doubleArrays,
            Dictionary<string, DoublePair> ranges)
        {
            FlagOptions = flags;
            StringOptions = strings;
            DoubleOptions = doubles;
            StringArrayOptions = stringArrays;
            DoubleArrayOptions = doubleArrays;
            RangeOptions = ranges;
        }

        /// <summary>
        /// Returns true if the flag is present, otherwise returns false
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public bool HasFlag(string flag)
        {
            return FlagOptions.Contains(flag);
        }

        /// <summary>
        /// Returns <see langword="true"/> if the option exists, and puts it into <paramref name="value"/>. 
        /// Otherwise, <see langword="false"/> is returned (and <paramref name="value"/> is <see langword="null"/>)
        /// </summary>
        /// <param name="option"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetString(string option, out string value)
        {
            return StringOptions.TryGetValue(option, out value);
        }

        /// <summary>
        /// Returns <see langword="true"/> if the option exists, and puts the number into <paramref name="value"/>. 
        /// Otherwise, <see langword="false"/> is returned (and the integer number is 0)
        /// <para>
        /// This is the same as <see cref="TryGetNumber(string, out double)"/> but the value is floored to an integer
        /// </para>
        /// </summary>
        /// <param name="option"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public bool TryGetNumber(string option, out int number)
        {
            if (DoubleOptions.TryGetValue(option, out double doubleValue))
            {
                number = (int)Math.Floor(doubleValue);
                return true;
            }
            number = 0;
            return false;
        }

        /// <summary>
        /// Returns <see langword="true"/> if the option exists, and puts the number into <paramref name="value"/>. 
        /// Otherwise, <see langword="false"/> is returned (and the number is 0)
        /// </summary>
        /// <param name="option"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public bool TryGetNumber(string option, out double number)
        {
            return DoubleOptions.TryGetValue(option, out number);
        }

        /// <summary>
        /// Returns <see langword="true"/> if the option exists, and puts the range values into <paramref name="start"/> and <paramref name="end"/>.
        /// Otherwise, <see langword="false"/> is returned (and <paramref name="start"/>/<paramref name="end"/> are 0)
        /// </summary>
        /// <param name="option"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public bool TryGetRange(string option, out double start, out double end)
        {
            if (RangeOptions.TryGetValue(option, out DoublePair pair))
            {
                start = pair.Start;
                end = pair.End;
                return true;
            }
            start = end = 0;
            return false;
        }

        /// <summary>
        /// Returns <see langword="true"/> if the option exists, and puts the range values into <paramref name="start"/> and <paramref name="end"/>.
        /// Otherwise, <see langword="false"/> is returned (and <paramref name="start"/>/<paramref name="end"/> are 0).
        /// <para>
        /// This is the same as <see cref="TryGetRange(string, out double, out double)"/> but the values are floored to an integer
        /// </para>
        /// </summary>
        /// <param name="option"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public bool TryGetRange(string option, out int start, out int end)
        {
            if (RangeOptions.TryGetValue(option, out DoublePair pair))
            {
                start = (int)Math.Floor(pair.Start);
                end = (int)Math.Floor(pair.End);
                return true;
            }
            start = end = 0;
            return false;
        }

        /// <summary>
        /// Returns <see langword="true"/> if the option exists and puts the array of values into <paramref name="array"/>.
        /// Otherwise, <see langword="false"/> is returned and <paramref name="array"/> is null
        /// </summary>
        /// <param name="option"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public bool TryGetStringArray(string option, out string[] array)
        {
            return StringArrayOptions.TryGetValue(option, out array);
        }

        /// <summary>
        /// Returns <see langword="true"/> if the option exists and puts the array of numbers into <paramref name="array"/>.
        /// Otherwise, <see langword="false"/> is returned and <paramref name="array"/> is null
        /// <para>
        /// This is the same as <see cref="TryGetNumberArray(string, out double[])"/> but the values
        /// are floored to an integer and put into a new <see langword="int[]"/> array
        /// </para>
        /// </summary>
        /// <param name="option"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public bool TryGetNumberArray(string option, out int[] array)
        {
            if (DoubleArrayOptions.TryGetValue(option, out double[] doubleArray))
            {
                array = new int[doubleArray.Length];
                for(int i = 0; i < doubleArray.Length; i++)
                {
                    array[i] = (int)Math.Floor(doubleArray[i]);
                }
                return true;
            }
            array = null;
            return false;
        }

        /// <summary>
        /// Returns <see langword="true"/> if the option exists and puts the array of numbers into <paramref name="array"/>.
        /// Otherwise, <see langword="false"/> is returned and <paramref name="array"/> is null
        /// </summary>
        /// <param name="option"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public bool TryGetNumberArray(string option, out double[] array)
        {
            return DoubleArrayOptions.TryGetValue(option, out array);
        }

        /// <summary>
        /// Creates a dictionary, the key being the option and the value being the 
        /// value, of all the things. Nothing is referenced so it wont update the options.
        /// <para>
        /// Flags only use the key, the value is empty. String/Number have the key and the value (double is just ToString()'d).
        /// <para>
        /// Arrays are joined using the <see cref="ARRAY_SPLITTER"/> character
        /// Ranges are joined with the <see cref="RANGE_SPLITTER"/> character
        /// </para>
        /// </para>
        /// <para>
        /// not very useful... but eh
        /// </para>
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetAllOptions()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();

            foreach(string flag in FlagOptions)
                dict.TryAdd(flag, "");

            foreach (KeyValuePair<string, string> pair in StringOptions)
                dict.TryAdd(pair.Key, pair.Value);

            foreach (KeyValuePair<string, double> pair in DoubleOptions)
                dict.TryAdd(pair.Key, pair.Value.ToString());

            foreach (KeyValuePair<string, string[]> pair in StringArrayOptions)
                dict.TryAdd(pair.Key, string.Join(ARRAY_SPLITTER, pair.Value));

            foreach (KeyValuePair<string, double[]> pair in DoubleArrayOptions)
                dict.TryAdd(pair.Key, string.Join(ARRAY_SPLITTER, pair.Value));

            foreach (KeyValuePair<string, DoublePair> pair in RangeOptions)
                dict.TryAdd(pair.Key, $"{pair.Value.Start}{RANGE_SPLITTER}{pair.Value.End}");

            return dict;
        }
    }
}
