namespace REghZyFramework.CmdLine.Parsing
{
    public enum ParameterType
    {
        _None = 0,
        Flag = 1,
        String = 2,
        Number = 4,
        Range = 8,
        StringArray = 16,
        NumberArray = 32
    }

    public static class ParameterTypeExtensions
    {
        public static string GetReadableName(this ParameterType parameter)
        {
            switch (parameter)
            {
                case ParameterType._None:
                    return "None";
                case ParameterType.Flag:
                    return "A flag";
                case ParameterType.String:
                    return "A string value";
                case ParameterType.Number:
                    return "A single number";
                case ParameterType.Range:
                    return "A Number range between two values";
                case ParameterType.StringArray:
                    return "An array of text";
                case ParameterType.NumberArray:
                    return "An array of numbers";
                default:
                    return "";
            }
        }
    }
}
