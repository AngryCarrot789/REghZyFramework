namespace REghZyFramework.Utilities
{
    public static class MathsHelper
    {
        public const float PI = 3.141592653589f;
        public const float PI_NEGATIVE = -3.141592653589f;
        public const float PI_DOUBLE = 6.283185307178f;
        public const float PI_DOUBLE_NEGATIVE = -6.283185307178f;
        public const float PI_HALF = 1.5707963267945f;
        public const float PI_HALF_NEGATIVE = -1.5707963267945f;
        public const float DEG_TO_RAD_CONST = 57.29577951309679f;

        /// <summary>
        /// Clamps a given value between 2 other values. 
        /// so Clamp(25, 0, 100) will always be between 0 and 100, never below or above
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static float Clamp(this float value, float min, float max)
        {
            if (value < min)
                return min;
            if (value > max)
                return max;
            return value;
        }

        public static int Clamp(this int value, int min, int max)
        {
            if (value < min)
                return min;
            if (value > max)
                return max;
            return value;
        }

        public static bool IsBetween(this int value, int min, int max)
        {
            return value >= min && value <= max;
        }

        public static bool IsOutside(this int value, int min, int max)
        {
            return value < min || value > max;
        }

        /// <summary>
        /// Looks at both value1 and value2 and returns the smallest one. so Min(4, 6) returns 4
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static float Min(this float value1, float value2)
        {
            return value1 < value2 ? value1 : value2;
        }

        /// <summary>
        /// Looks at both value1 and value2 and returns the biggest one. so Max(4, 6) returns 6
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static float Max(this float value1, float value2)
        {
            return value1 > value2 ? value1 : value2;
        }

        public static float Min3(float a, float b, float c)
        {
            float n = Min(a, b);
            return Min(n, c);
        }

        public static float Max3(float a, float b, float c)
        {
            float n = Max(a, b);
            return Max(n, c);
        }

        public static float DegreesToRadians(float degrees)
        {
            return degrees / DEG_TO_RAD_CONST;
        }

        public static float RadiansToDegrees(float radians)
        {
            return radians * DEG_TO_RAD_CONST;
        }
    }
}
