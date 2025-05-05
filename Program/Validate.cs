using System;
using System.Linq;

namespace ClairObscurConfig
{
    internal class Validate
    {
        // Checks an array of string values. Easy way to validate multiple values that don't fall within a range. 
        public static string StrArray(string ToCheck, string[] StringArray, string Fallback)
        {
            // If the value is in the array, return the value.
            if (StringArray.Any(ToCheck.Contains))
            {
                return ToCheck;
            }
            // If it's not in the array, return the fallback value.
            return Fallback;
        }
        // Checks if a string as an integer is within range of a low and high value. Low and High are valid.
        public static string RangeInt(string ToCheck, int Low, int High, string Fallback)
        {
            // Stores the parsed value.
            int Parsed = 0;

            // Try to parse the string.
            if (Int32.TryParse(ToCheck, out Parsed))
            {
                // If the value was parsed and its in range, it's good.
                if (Parsed >= Low & Parsed <= High)
                {
                    return ToCheck;
                }
                // If it's not within range, return the fallback.
                return Fallback;
            }
            // If it couldn't be parsed as an integer, return the fallback.
            else
            {
                return Fallback; ;
            }
        }
        // Checks if a string as a double is within range of a low and high value. Low and High are valid.
        public static string RangeDec(string ToCheck, double Low, double High, string Fallback)
        {
            // Stores the parsed value.
            double Parsed = 0;

            // Try to parse the string.
            if (Double.TryParse(ToCheck, out Parsed))
            {
                // If the value was parsed and its in range, it's good.
                if (Parsed >= Low & Parsed <= High)
                {
                    return ToCheck;
                }
                // If it's not within range, return the fallback.
                return Fallback;
            }
            // If it couldn't be parsed as a double, return the fallback.
            else
            {
                return Fallback; ;
            }
        }
    }
}
