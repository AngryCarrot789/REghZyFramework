using System;
using System.Runtime.Serialization;

namespace REghZyFramework.CmdLine.Parsing.Exceptions
{
    [Serializable]
    public class ParseFailedException : Exception
    {
        public ParseFailedException(string message) : base(message) { }
        public ParseFailedException(string message, Exception inner) : base(message, inner) { }
        protected ParseFailedException(SerializationInfo info,StreamingContext context) : base(info, context) { }

        public ParameterType FailedParameter { get; }
        public string FailedOption { get; }
        public string CustomMessage { get; }
        private string _message;

        public ParseFailedException(string option, ParameterType parameter, string customMessage = null)
        {
            this.FailedParameter = parameter;
            this.FailedOption = option;
            this.CustomMessage = customMessage;
            _message = $"Failed to parse option '{FailedOption}' parsing the parameter type '{FailedParameter}'" +
                    (CustomMessage == null ? "" : (". " + CustomMessage));
        }

        public override string Message
        {
            get => _message;
        }
    }
}
