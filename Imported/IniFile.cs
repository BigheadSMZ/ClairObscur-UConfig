using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

// Credits to Danny Beckett at stackoverflow.
// https://stackoverflow.com/questions/217902/reading-writing-an-ini-file

namespace ClairObscurConfig
{
    internal class IniFile
    {
        // Store the full path to the INI file and the name of the program.
        string Path;
        string EXE = Assembly.GetExecutingAssembly().GetName().Name;

        // Import dynamic link libraries responsible for INI functions.
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileSection(string Section, byte[] RetVal, int Size, string FilePath);

        // Create a new instance of INI file class.
        public IniFile(string IniPath = null)
        {
            this.Path = new FileInfo(IniPath ?? this.EXE + ".ini").FullName;
        }
        // Read a value from the INI file.
        public string Read(string Key, string Section = null)
        {
            var RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section ?? this.EXE, Key, "", RetVal, 255, this.Path);

            // Convert to string.
            string Value = RetVal.ToString();

            // Some people like to put comments on lines so catch that.
            if (Value.Contains(";") | Value.Contains("#"))
            {
                // Split on semicolon comments.
                string[] SplitValue = Value.Split(';','#');

                // Get only the value.
                Value = SplitValue[0].Trim();
            }
            // Now we can return the proper value.
            return Value;
        }
        // Write an entry and a value to the INI file.
        public void Write(string Key, string Value, string Section = null)
        {
            WritePrivateProfileString(Section ?? this.EXE, Key, Value, this.Path);
        }
        // Delete an entry from the INI file.
        public void DeleteKey(string Key, string Section = null)
        {
            Write(Key, null, Section ?? this.EXE);
        }
        // Combines functionality of Read/Write using condition.
        public void ConditionalWriteDelete(string Key, string Value, string Section = null, bool DoWrite = true)
        {
            switch (DoWrite)
            {
                // If true write the value, if false delete the key.
                case true:  { Write(Key, Value, Section); break; }
                case false: { DeleteKey(Key, Section); break; }
            }
        }
        // Delete entire section from the INI file.
        public void DeleteSection(string Section = null)
        {
            Write(null, null, Section ?? this.EXE);
        }
        // Check if the INI file contains the entry.
        public bool KeyExists(string Key, string Section = null)
        {
            return Read(Key, Section).Length > 0;
        }
        // Gets all the keys in a section.
        public List<string> GetSectionKeys(string Section)
        {
            // Who knows how long the INI will be so use maximum documented size.
            byte[] ByteArray = new byte[32767];

            // This will fill the byte array with values.
            GetPrivateProfileSection(Section ?? this.EXE, ByteArray, 32767, this.Path);

            // Convert the bytes into a string array and shave off the end.
            string[] Chars = Encoding.ASCII.GetString(ByteArray).Trim('\0').Split('\0');

            // Stores a concatenated key.
            string Line = "";

            // Let's use a list since it's easily expandable.
            List<string> Keys = new List<string>();

            // Loop through the array. Each value will be a single character.
            for (int i = 0; i < Chars.Length; i++)
            {
                // While we have a value and not an empty string add the character to the string. The very last line will
                // always be missed since the original string was trimmed, so force the "else if" when we reached the end.
                // This will miss the final character but we are trimming the value off anyway with the split function.
                if (Chars[i] != "" & i != Chars.Length - 1)
                {
                    Line += Chars[i];
                }
                // When an empty character is hit we have the entire line. We still need to shave off the value. There will 
                // be two empty characters after a line is assembled so account for that by checking if "Line" is not empty.
                else if (Line != "")
                {
                    Keys.Add(Line.Split('=')[0]);
                    Line = "";
                }
            }
            // We should now have a list of keys so send it back.
            return Keys;
        }
    }
}