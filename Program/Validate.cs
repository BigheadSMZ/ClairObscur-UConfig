using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClairObscurConfig
{
    internal class Validate
    {
        public static string StringInArray(string ToCheck, string[] StringArray, string Fallback)
        {
            if (StringArray.Any(ToCheck.Contains))
            {
                return ToCheck;
            }
            return Fallback;
        }

        public static string RangeInt(string ToCheck, int Low, int High, string Fallback)
        {
            int Parsed = 0;

            if (Int32.TryParse(ToCheck, out Parsed))
            {
                if (Parsed >= Low & Parsed <= High)
                {
                    return ToCheck;
                }
                return Fallback;
            }
            else
            {
                return Fallback; ;
            }
        }

        public static string RangeDec(string ToCheck, double Low, double High, string Fallback)
        {
            double Parsed = 0;

            if (Double.TryParse(ToCheck, out Parsed))
            {
                if (Parsed >= Low & Parsed <= High)
                {
                    return ToCheck;
                }
                return Fallback;
            }
            else
            {
                return Fallback; ;
            }
        }
    }
}
