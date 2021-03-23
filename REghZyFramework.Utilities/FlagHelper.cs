namespace REghZyFramework.Utilities
{
    /// <summary>
    /// Provides helpful functions for performing bitwise operations to integers, useful for creating flags
    /// </summary>
    public static class FlagHelper
    {
        public static bool HasFlag(int flags, int flag)
        {
            return (flags & flag) == flag;
        }

        public static int CombineFlags(int a, int b)
        {
            return a | b;
        }

        public static int AddFlag(int flags, int newFlag)
        {
            return CombineFlags(flags, newFlag);
        }

        public static int RemoveFlag(int flags, int flag)
        {
            return flags & ~flag;
        }
    }
}
