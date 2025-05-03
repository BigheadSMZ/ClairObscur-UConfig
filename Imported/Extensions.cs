using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

// This is a bunch of extensions I created to make certain tasks much simpler. Unfortunately I lost the majority of my source code for all my
// projects, so this code was originally lost. I managed to recover it via dnSpy and mostly rewrite it to the way it was before. I am importing
// it and making use of it as a means to "back up" this code as I will probably use it in most of my future projects.

namespace ClairObscurConfig
{
    internal static class Extensions
    {
        public static string GetFilePath(this string InputFile)
        {
            // If the value is null or empty then return an empty string.
            if (InputFile == null || InputFile == "") { return ""; }

            // If the path doesn't exist then return an empty string.
            if (!InputFile.TestPath(false)) { return ""; }

            // Start with an empty string for the path.
            string FilePath = "";

            // Split the input file path into an array based on folders.
            string[] PathArray = InputFile.Split('\\');

            // Count the number of parts minus the file name.
            int Count = PathArray.Length - 1;

            // Loop until we reach the last folder.
            for (int i = 0; i < Count; i++)
            {
                // Add back the part of the path.
                FilePath = FilePath + PathArray[i] + "\\";
            }
            // Return the full path minus the last slashes.
            return FilePath.TrimEnd('\\');
        }

        public static bool TestPath(this string InputPath, bool IsDirectory = false)
        {
            // If the value is null or empty then return false.
            if (InputPath == null || InputPath == "") { return false; }

            // Attempt to pull attributes from the file/folder.
            try { FileAttributes Dummy = File.GetAttributes(InputPath); }

            // If the path doesn't exist catch the exception.
            catch (Exception x)
            {
                // All of this crap has popped up depending on the input paramter so catch it all.
                if (x is DirectoryNotFoundException || x is FileNotFoundException || x is ArgumentException) { return false; }
            }
            // The above should already catch most paths or files that don't exist. But any paths or files that make it past
            // the exception, get the type that they are (path or file) then use the respective method to test if they exist.*/
            FileAttributes attr = File.GetAttributes(InputPath);

            // If it's a directory, check for that specific attribute.
            if (attr.HasFlag(FileAttributes.Directory))
            {
                return Directory.Exists(InputPath);
            }
            // If it's a file, check to see if only folder types will pass.
            else if (!IsDirectory)
            {
                return File.Exists(InputPath);
            }
            // If file checks were blocked, then we end up here so return false.
            return false;
        }

        public static string CreatePath(this string InputPath, bool NoReturn = false)
        {
            // If the path is empty then it does not exist.
            if (InputPath == "" || InputPath == null) { return ""; };

            // Check to see if the path does not exist.
            if (!InputPath.TestPath(false))
            {
                // Create the path.
                Directory.CreateDirectory(InputPath);
            }
            // Return the path that was created.
            return InputPath;
        }

        public static void RenamePath(this string Source, string Destination, bool Overwrite = false)
        {
            // If the destination exists and we wan't to overwrite the contents.
            if (Overwrite && Destination.TestPath(true))
            {
                // Remove the path before creating the new one.
                Destination.RemovePath();
            }
            // Move the new name to the destination.
            Directory.Move(Source, Destination);
        }

        public static void RemovePath(this string InputPath)
        {
            // If the path is empty then it does not exist.
            if (InputPath == "" || InputPath == null) { return; };

            // Check to see if the path exists.
            if (InputPath.TestPath())
            {
                // If the input path is a folder.
                if ((File.GetAttributes(InputPath) & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    // Delete the folder and return.
                    Directory.Delete(InputPath, true);
                    return;
                }
                // If the input path is a file then simply remove it.
                File.Delete(InputPath);
            }
        }

        public static List<string> GetFiles(this string Path, string SearchPatterns = "*.*", bool Recurse = false)
        {
            // Split the search patterns using the commas into a list.
            string[] findPatterns = SearchPatterns.Split(',');

            // Default the search type to searching only the top directory.
            SearchOption searchOption = SearchOption.TopDirectoryOnly;

            // Search all sub-folders if recurse is enabled.
            if (Recurse) { searchOption = SearchOption.AllDirectories; }

            // Grab the files within the folder (and maybe sub-folders).
            return (findPatterns.AsParallel()
                .SelectMany(searchPattern => Directory.EnumerateFiles(Path, searchPattern, searchOption)
                .Where(f => !new FileInfo(f).Attributes.HasFlag(FileAttributes.Hidden | FileAttributes.System)))).EnumToList();
        }

        public static List<string> GetFolders(this string Path, string SearchPattern = "*", bool Recurse = false)
        {
            // Default the search type to searching only the top directory.
            SearchOption SearchOption = SearchOption.TopDirectoryOnly;

            // Search all sub-folders if recurse is enabled.
            if (Recurse) { SearchOption = SearchOption.AllDirectories; }

            // Grab the folders within the folder (and maybe sub-folders).
            return (Directory.GetDirectories(Path, SearchPattern, SearchOption)).EnumToList();
        }

        public static int BoolToInt(this bool Boolean)
        {
            // Simply return the boolean as an int.
            return Boolean ? 1 : 0;
        }

        public static bool IntToBool(this int Int)
        {
            // Simply return the int as a bool.
            return Convert.ToBoolean(Int);
        }

        public static void ClearPath(this string InputPath)
        {
            // If the path is empty then it does not exist.
            if (InputPath == "" || InputPath == null) { return; };

            // Check to see if the path exists.
            if (InputPath.TestPath())
            {
                // Remove all files and folders from the input folder.
                foreach (string LoopFile in InputPath.GetFiles(Recurse: true)) { LoopFile.RemovePath(); }
                foreach (string LoopFile in InputPath.GetFolders(Recurse: true)) { LoopFile.RemovePath(); }
            }
        }

        public static string GetFileName(this string InputFile)
        {
            // If the path is empty then it does not exist.
            if (InputFile == null || InputFile == "") { return ""; }

            // Check to see if the path does not exist.
            if (!InputFile.TestPath(false)) { return ""; }

            // Split the path based on the slashes.
            string[] SplitName = InputFile.Split('\\');

            // Get the last member of the array.
            int Index = SplitName.Length - 1;

            // Return the filename.
            return SplitName[Index];
        }

        public static string GetBaseName(this string FileName)
        {
            // Check to see if the path contains slashes.
            if (FileName.Contains("\\"))
            {
                // Split the value based on slashes.
                string[] SplitSlashes = FileName.Split('\\');

                // Keep only the last value as it should be the base file name.
                FileName = SplitSlashes[SplitSlashes.Count<string>() - 1];
            }
            // Check to see if the filename contains a period.
            if (!FileName.Contains("."))
            {
                // If it does not, just return the string.
                return FileName;
            }
            // Split the input based on periods.
            string[] SplitPeriods = FileName.Split('.');

            // Start with an empty string.
            string NewFileName = "";

            // We want to keep all of the file except the last part (the extension).
            int Loops = SplitPeriods.Count<string>() - 1;

            // Loop by the number of counts.
            for (int i = 0; i < Loops; i++)
            {
                // Add the number of positions that are saved.
                NewFileName += SplitPeriods[i] + ".";
            }
            // Return the crafted string.
            return NewFileName.TrimEnd('.');
        }

        public static string Extend(this string InputString, int Length)
        {
            // Count the number of characters in the input string.
            int Count = InputString.Length;

            // Check the number of characters against the desired amount.
            if (Count < Length)
            {
                // If the string is to be lengthened, find out by how much.
                int AddLength = Length - Count;

                // Loop until the string matches the desired number of characters.
                for (int i = 1; i <= AddLength; i++)
                {
                    // Add an empty space to the end of the string.
                    InputString += " ";
                }
            }
            // Return the modified string.
            return InputString;
        }

        public static string[] StrSplit(this string InputString, string SplitOn)
        {
            // Splits a string based on the input and returns an array.
            return InputString.Split(new string[] { SplitOn }, StringSplitOptions.None);
        }

        public static string FormatNewLines(this string InputString)
        {
            // Split the string into a character array to pick out new lines.
            char[] CharArray = InputString.ToCharArray();

            // Start with an empty string.
            string NewString = "";

            // Loop through each character.
            for (int i = 0; i < CharArray.Length; i++)
            {
                // Search for the newline characters.
                if (CharArray[i] == '\n')
                {
                    // Convert it to the type that can make use of "Format" and add it to the string.
                    NewString += "{0}";
                }
                // If it's just a normal character.
                else
                {
                    // Add it to the string.
                    NewString += CharArray[i].ToString();
                }
            }
            // Return the string with the new format.
            return NewString;
        }

        public static List<string> EnumToList(this IEnumerable<string> EnumArray)
        {
            // Create a new string list.
            List<string> StringList = new List<string> { };

            // Loop through all items in the array.
            foreach (string Item in EnumArray)
            {
                // Add the item to the string array.
                StringList.Add(Item);
            }
            // Return the string array.
            return StringList;
        }

        public static List<string> ArrayToList(this string[] StringArray)
        {
            // Create a new string list.
            List<string> StringList = new List<string> { };

            // Loop through all items in the array.
            for (int i = 0; i < StringArray.Length; i++)
            {
                // Add the item to the string array.
                StringList.Add(StringArray[i]);
            }
            // Return the string array.
            return StringList;
        }
        public static string[] ListToArray(this List<string> StringList)
        {
            // Create a new string array equal in size to the list.
            string[] StringArray = new string[StringList.Count];

            // Loop through all items in the list.
            for (int i = 0; i < StringList.Count; i++)
            {
                // Add the item to the string array.
                StringArray[i] = StringList[i];
            }
            // Return the string array.
            return StringArray;
        }

        public static List<string> ReadLinesToList(this string TextFile)
        {
            // Read out a text file to a generic list instead of a string array.
            return File.ReadAllLines(TextFile).ArrayToList();
        }

        public static void Move<T>(this List<T> GenericList, int OldIndex, int NewIndex)
        {
            // Get the position of the item.
            T ListItem = GenericList[OldIndex];

            // Remove the item from this position.
            GenericList.RemoveAt(OldIndex);

            // Instert the item at the new position.
            GenericList.Insert(NewIndex, ListItem);
        }

        public static T[] Reverse<T>(this T[] OldArray)
        {
            // Create a new array to hold the reversed order.
            T[] NewArray = new T[OldArray.Length];

            // Track the position of the index.
            int Index = 0;

            // Loop through the old array in reverse.
            for (int i = OldArray.Length - 1; i >= 0; i--)
            {
                // Set the current item into the reversed position.
                NewArray[Index] = OldArray[i];

                // Increment the index.
                Index++;
            }
            // Return the reversed array.
            return NewArray;
        }

        public static T[] RemoveAt<T>(this T[] OldArray, int Index)
        {
            // Create a new array with one less index.
            T[] NewArray = new T[OldArray.Length - 1];

            // If the index is beyond the first position.
            if (Index > 0)
            {
                // Copy the array up to that position.
                Array.Copy(OldArray, 0, NewArray, 0, Index);
            }
            // When we reached the position of the index to remove.
            if (Index < OldArray.Length - 1)
            {
                // Copy everything beyond that index.
                Array.Copy(OldArray, Index + 1, NewArray, Index, OldArray.Length - Index - 1);
            }
            // Return the new array with the data removed.
            return NewArray;
        }

        public static T[] Insert<T>(this T[] OldArray, int Index, dynamic Data)
        {
            // Create a new array with one more index.
            T[] NewArray = new T[OldArray.Length + 1];

            // If the index is beyond the first position.
            if (Index > 0)
            {
                // Copy the array up to that position.
                Array.Copy(OldArray, 0, NewArray, 0, Index);
            }
            // Copy the data into the current position.
            NewArray[Index] = Data;

            // Make sure the index falls within the upper bounds.
            if (Index < OldArray.Length - 1)
            {
                // Copy the rest of the data into the new array.
                Array.Copy(OldArray, Index, NewArray, Index + 1, OldArray.Length - Index);
            }
            // Return the new array with the data added.
            return NewArray;
        }

        public static void DoubleBuffer(this Control InputControl, bool Enabled)
        {
            // Get the double buffered property of the control.
            PropertyInfo controlProperty = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);

            // Enable double buffering for the control.
            controlProperty.SetValue(InputControl, Enabled, null);
        }

        public static string ShowFileDialog(this string StartPath, string[] FileName, string[] Description)
        {
            // Create the open file dialog.
            OpenFileDialog FileDialog = new OpenFileDialog();

            // An empty string to contain the filters.
            string FilterString = "";

            // Set the initial path it searches.
            FileDialog.InitialDirectory = StartPath;

            // If more than 1 application is provided, loop through all of them.
            for (int i = 0; i < FileName.Length; i++)
            {
                // A string is sent to the filter. Add all programs and descriptions to the filter.
                FilterString += Description[i] + "|" + FileName[i] + "|";
            }
            // Trim the last "|" from the string or we'll get an error message.
            FileDialog.Filter = FilterString.TrimEnd('|');

            // Show the dialog to the user.
            FileDialog.ShowDialog();

            // Return the file that was selected.
            return FileDialog.FileName;
        }

        public static string[] ShowMultiFileDialog(this string StartPath, string[] FileName, string[] Description)
        {
            // Create the open file dialog.
            OpenFileDialog FileDialog = new OpenFileDialog();

            // An empty string to contain the filters.
            string FilterString = "";

            // Set the initial path it searches.
            FileDialog.InitialDirectory = StartPath;

            // Allow selecting multiple files to return.
            FileDialog.Multiselect = true;

            // If more than 1 application is provided, loop through all of them.
            for (int i = 0; i < FileName.Length; i++)
            {
                // A string is sent to the filter. Add all programs and descriptions to the filter.
                FilterString += Description[i] + "|" + FileName[i] + "|";
            }
            // Trim the last "|" from the string or we'll get an error message.
            FileDialog.Filter = FilterString.TrimEnd('|');

            // Show the dialog to the user.
            FileDialog.ShowDialog();

            // Return the file that was selected.
            return FileDialog.FileNames;
        }

        public static void MoveSelectedItemUp(this ListBox InputListBox)
        {
            // Move an item up in a ListBox.
            Extensions.MoveSelectedItem(InputListBox, -1);
        }

        public static void MoveSelectedItemDown(this ListBox InputListBox)
        {
            // Move an item down in a ListBox.
            Extensions.MoveSelectedItem(InputListBox, 1);
        }

        private static void MoveSelectedItem(ListBox InputListBox, int Direction)
        {
            // If an item is not selected or the index falls below 0 somewhow just return.
            if (InputListBox.SelectedItem == null || InputListBox.SelectedIndex < 0) { return; }

            // Calculate the new index based on the value for direction.
            int NewIndex = InputListBox.SelectedIndex + Direction;

            // Make sure the new index does not fall outside of the lower bounds.
            if (NewIndex < 0 || NewIndex >= InputListBox.Items.Count) { return; }

            // Get the currently selected item.
            object Selected = InputListBox.SelectedItem;

            // Convert the ListBox item into a Checked ListBox item.
            CheckedListBox CheckedListBox = InputListBox as CheckedListBox;

            // Default the value of the checkstate to unchecked.
            CheckState Checked = CheckState.Unchecked;

            // If the ListBox isn't null.
            if (CheckedListBox != null)
            {
                // Get the actual state of the Checked ListBox.
                Checked = CheckedListBox.GetItemCheckState(CheckedListBox.SelectedIndex);
            }
            // Remove the current ListBox item.
            InputListBox.Items.Remove(Selected);

            // Insert it back at the new position.
            InputListBox.Items.Insert(NewIndex, Selected);

            // Set the selection to the newly inserted item.
            InputListBox.SetSelected(NewIndex, true);

            // If the ListBox isn't null.
            if (CheckedListBox != null)
            {
                // Set the checked state back to what it was.
                CheckedListBox.SetItemCheckState(NewIndex, Checked);
            }
        }
    }
}
