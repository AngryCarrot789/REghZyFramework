using System;
using System.Collections.Generic;
using REghZyFramework.CmdLine.Parsing;

namespace REghZyFramework.CmdLine
{
    /// <summary>
    /// A simple helper class for formatting helpful command descriptions
    /// </summary>
    public class HelpOptions
    {
        /// <summary>
        /// The name of the program to be displayed when showing help
        /// </summary>
        public readonly string ProgramName;

        /// <summary>
        /// The list of formatted commands, with the new line characters and stuff
        /// </summary>
        private readonly List<string> Commands;

        public HelpOptions(string programName = "My program")
        {
            ProgramName = programName;
            Commands = new List<string>();
        }

        /// <summary>
        /// Adds a command to the list, and optionally, adds the command to the given <see cref="ArgsParser"/> (to same time)
        /// <para>
        /// Message format when writing: (replace the dots with whitespaces)
        /// </para>
        /// <code>
        ///   ..-[option] [parameter descritpion]
        ///   <code>
        ///   .....[extra info]
        ///   </code>
        /// </code>
        /// </summary>
        /// <param name="option"></param>
        /// <param name="parameter"></param>
        /// <param name="description"></param>
        /// <param name="parser"></param>
        public void AddCommand(string option, ParameterType parameter, string description, ArgsParser parser = null)
        {
            Commands.Add($"  -{option} [{parameter.GetReadableName()}]\n     {description}");
            if (parser != null)
                parser.AddOption(option, parameter);
        }

        ///// <summary>
        ///// Adds a command and aliases (aka alternative names) to the list, 
        ///// and optionally, adds the command to the given <see cref="ArgsParser"/> (to same time)
        ///// <para>
        ///// This function doesn't call <see cref="AddCommand(string, ParameterType, string, ArgsParser)"/>, 
        ///// you should only call that one or this one. That one doesnt support aliases
        ///// </para>
        ///// <para>
        ///// Message format when writing: (replace the dots with whitespaces)
        ///// </para>
        ///// <code>
        /////   .-[option] [parameter descritpion]
        /////   <code>
        /////   ..Aliases: [aliases]
        /////   </code>
        /////   <code>
        /////   ....[extra info]
        /////   </code>
        ///// </code>
        ///// </summary>
        ///// <param name="option"></param>
        ///// <param name="parameter"></param>
        ///// <param name="description"></param>
        ///// <param name="parser"></param>
        ///// <param name="aliases"></param>
        //public void AddCommandWithAliases(string option, ParameterType parameter, string description, ArgsParser parser = null, params string[] aliases)
        //{
        //    Commands.Add(
        //        $" -{option} [{parameter.GetReadableName()}]\n" +
        //        $"  Aliases: {string.Join(", ", aliases)}\n" +
        //        $"    {description}\n");
        //    if (parser != null)
        //    {
        //        parser.AddOption(option, parameter);
        //        parser.AddOptionAliases(option, aliases);
        //    }
        //}

        /// <summary>
        /// Calls <see cref="Write(Action{string})"/> using the <see cref="Console.WriteLine"/> function
        /// </summary>
        public void Write()
        {
            Write(Console.WriteLine);
        }

        /// <summary>
        /// Writes the program name and commands to the given write line function
        /// </summary>
        public void Write(Action<string> writeLineCallback)
        {
            writeLineCallback($"Help for {ProgramName}");

            foreach(string line in Commands)
            {
                writeLineCallback(line);
            }
        }
    }
}
