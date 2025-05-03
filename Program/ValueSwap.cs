using System.Linq;

namespace ClairObscurConfig
{
    internal class ValueSwap
    {
        public static string Translate_AA(string Value)
        {
            // If it ends with an "x" remove it.
            if (Value.EndsWith("x"))
            {
                return Value.Substring(0, Value.Length - 1);
            }
            // Otherwise add the "x" to the end.
            return (Value + "x");
        }

        public static string Translate_ShadRes(string Value)
        {
            // Storing we only need the first half of the resolution.
            if (Value.Contains('x'))
            {
                return Value.Substring(0, 4);
            }
            // The menu displays the width and height.
            return (Value + 'x' + Value);
        }

        public static string Translate_Scale(string Value)
        {
            // Stores the translated value.
            string NewValue = "";

            switch (Value)
            {
                // Text to integer.
                case "Disabled" : { NewValue = "0"; break; }
                case "Low"      : { NewValue = "1"; break; }
                case "Medium"   : { NewValue = "2"; break; }
                case "High"     : { NewValue = "3"; break; }
                case "Very High": { NewValue = "4"; break; }
                case "Ultra"    : { NewValue = "5"; break; }

                // Integer to text.
                case "0": { NewValue = "Disabled" ; break; }
                case "1": { NewValue = "Low"      ; break; }
                case "2": { NewValue = "Medium"   ; break; }
                case "3": { NewValue = "High"     ; break; }
                case "4": { NewValue = "Very High"; break; }
                case "5": { NewValue = "Ultra"    ; break; }
            }
            // Return whatever the new value is.
            return NewValue;
        }

        public static string Translate_ShadQual(string Value)
        {
            // Stores the translated value.
            string NewValue = "";

            switch (Value)
            {
                // Text to integer.
                case "Low":       { NewValue = "0"; break; }
                case "Medium":    { NewValue = "1"; break; }
                case "High":      { NewValue = "2"; break; }
                case "Very High": { NewValue = "3"; break; }
                case "Ultra":     { NewValue = "4"; break; }

                // Integer to text.
                case "0": { NewValue = "Low"      ; break; }
                case "1": { NewValue = "Medium"   ; break; }
                case "2": { NewValue = "High"     ; break; }
                case "3": { NewValue = "Very High"; break; }
                case "4": { NewValue = "Ultra"    ; break; }
            }
            // Return whatever the new value is.
            return NewValue;
        }

        public static string Translate_Binary(string Value)
        {
            // Stores the translated value.
            string NewValue = "";

            switch (Value)
            {
                // Text to integer.
                case "Disabled": { NewValue = "0"; break; }
                case "Enabled" : { NewValue = "1"; break; }

                // Integer to text.
                case "0": { NewValue = "Disabled"; break; }
                case "1": { NewValue = "Enabled" ; break; }
            }
            // Return whatever the new value is.
            return NewValue;
        }
    }
}