using System;
using System.Globalization;

namespace ClairObscurConfig
{
    internal class Functions
    {
        public static decimal FormatStringDecimal(string Value)
        {
            // The value that will be returned.
            decimal FixedValue;

            // Some countries use commas instead of periods for decimals.
            if (Wildcard.Match(Value, "*,*", true))
            {
                // If a comma is found, replace it with a period.
                Value = Value.Replace(',', '.');
            }
            // Check to see if it has a period.
            if (!Wildcard.Match(Value, "*.*", true))
            {
                // Whole numbers won't have a period, so add one because its needed to split.
                Value += ".00";
            }
            // Now split on the period. This will be used to fix any values with a leading 0 (such as 01.25) or a missing 0 (such as .25).
            string[] PeriodSplit = Value.Split(new char[] { '.' }, 2);

            // Convert the integer part of the string into an actual integer, then back into a string. This will remove any leading 0s, or make it 0 if a number was not found.
            string IntValue = PeriodSplit[0];
            string DecValue = PeriodSplit[1];

            // How the decimal value will be formatted depends on the number of characters available in the decimal value.
            int DecimalPlaces = DecValue.Length;

            // A switch works well here because the range is 0-2, anything beyond, and nothing negative.
            switch (DecimalPlaces)
            {
                case 0:  { FixedValue = Convert.ToDecimal(IntValue + ".00", new CultureInfo("en-US")); break; }
                case 1:  { FixedValue = Convert.ToDecimal(IntValue + "." + DecValue + "0", new CultureInfo("en-US")); break; }
                case 2:  { FixedValue = Convert.ToDecimal(IntValue + "." + DecValue, new CultureInfo("en-US")); break; }
                default: { FixedValue = Convert.ToDecimal(IntValue + "." + DecValue.Substring(0, 2), new CultureInfo("en-US")); break; }
            }
            // Return the new value.
            return FixedValue;
        }
    }
}
