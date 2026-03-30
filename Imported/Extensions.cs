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
            // Make sure it's not null, empty, and that it exists.
            if (InputFile == null || InputFile == "" || !InputFile.TestPath(false))
                return "";

            // Split the path on slashes and count the array size.
            string FilePath = "";
            string[] PathArray = InputFile.Split('\\');
            int Count = PathArray.Length - 1;

            // Loop until we reached the final index.
            for (int i = 0; i < Count; i++)
                FilePath = FilePath + PathArray[i] + "\\";

            // Return the path minus the file name.
            return FilePath.TrimEnd('\\');
        }

        public static bool TestPath(this string InputPath, bool IsDirectory = false)
        {
            // If the value is null or empty then return false.
            if (InputPath == null || InputPath == "")
                return false;

            // Attempt to pull attributes from the file/folder.
            try {
                FileAttributes Dummy = File.GetAttributes(InputPath);
            }
            // Catch all known exception types.
            catch (Exception x) {
                if (x is DirectoryNotFoundException || x is FileNotFoundException || x is ArgumentException || x is NotSupportedException)
                    return false;
            }
            // The above should already catch most paths or files that don't exist. But any paths or files that make it past
            // the exception, get the type that they are (path or file) then use the respective method to test if they exist.*/
            if (File.GetAttributes(InputPath).HasFlag(FileAttributes.Directory))
                return Directory.Exists(InputPath);
            else if (!IsDirectory)
                return File.Exists(InputPath);

            // If file checks were blocked with IsDirectory, we end up here so return false.
            return false;
        }

        public static string CreatePath(this string InputPath, bool NoReturn = false)
        {
            // If the path is empty then it does not exist.
            if (InputPath == "" || InputPath == null)
                return "";

            // Check to see if the path does not exist.
            if (!InputPath.TestPath(false))
                Directory.CreateDirectory(InputPath);

            // Return the path that was created unless NoReturn flag is set.
            if (!NoReturn)
                return InputPath;
            return null;
        }

        public static void RenamePath(this string Source, string Destination, bool Overwrite = false)
        {
            // If the destination exists and we wan't to overwrite the contents.
            if (Overwrite && Destination.TestPath(true))
                Destination.RemovePath();
            else
                return;

            // Move the new name to the destination.
            Directory.Move(Source, Destination);
        }

        public static void RemovePath(this string InputPath)
        {
            // If the path is empty then it does not exist.
            if (InputPath == null || InputPath == "")
                return;

            // If the path exists call the type needed to remove it.
            if (InputPath.TestPath())
                if (File.GetAttributes(InputPath) == FileAttributes.Directory)
                    Directory.Delete(InputPath, true);
                else
                    File.Delete(InputPath);
        }

        public static void MovePath(this string SourcePath, string DestinationPath, bool Overwrite)
        {
            // If the path is empty then it does not exist.
            if (SourcePath == null || SourcePath == "" || DestinationPath == null || DestinationPath == "")
                return;

            // The path exists so let's try to move it.
            if (SourcePath.TestPath())
            {
                // The destination already exists so either remove it or exit.
                if (DestinationPath.TestPath() & Overwrite)
                    DestinationPath.RemovePath();
                else
                    return;

                // Move the file to the new destination.
                File.Move(SourcePath, DestinationPath);
            }
        }

        public static List<string> GetFiles(this string Path, string SearchPatterns = "*.*", bool Recurse = false)
        {
            // Split the search patterns using the commas into a list.
            string[] findPatterns = SearchPatterns.Split(',');

            // Search all sub-folders if recurse is enabled.
            SearchOption searchOption = SearchOption.TopDirectoryOnly;
            if (Recurse) searchOption = SearchOption.AllDirectories;

            // Grab the files within the folder (and maybe sub-folders).
            return (findPatterns.AsParallel()
                .SelectMany(searchPattern => Directory.EnumerateFiles(Path, searchPattern, searchOption)
                .Where(f => !new FileInfo(f).Attributes.HasFlag(FileAttributes.Hidden | FileAttributes.System))))
                .EnumToList();
        }

        public static List<string> GetFolders(this string Path, string SearchPattern = "*", bool Recurse = false)
        {
            // Search all sub-folders if recurse is enabled.
            SearchOption SearchOption = SearchOption.TopDirectoryOnly;
            if (Recurse) SearchOption = SearchOption.AllDirectories;

            // Get the folder and potentially all subfolders.
            return (Directory.GetDirectories(Path, SearchPattern, SearchOption)).EnumToList();
        }

        public static int BoolToInt(this bool Boolean)
        {
            return Boolean ? 1 : 0;
        }

        public static bool IntToBool(this int Int)
        {
            return Convert.ToBoolean(Int);
        }

        public static void ClearPath(this string InputPath)
        {
            // If the path is empty then it does not exist.
            if (InputPath == "" || InputPath == null)
                return;

            // If the path exists remove all files and folders but do not remove the InputPath.
            if (InputPath.TestPath())
            {
                foreach (string LoopFile in InputPath.GetFiles(Recurse: true))
                    LoopFile.RemovePath();
                foreach (string LoopFile in InputPath.GetFolders(Recurse: true))
                    LoopFile.RemovePath();
            }
        }

        public static string GetFileName(this string InputFile)
        {
            // If the path is null, empty, or doesn't exist.
            if (InputFile == null || InputFile == "" || !InputFile.TestPath(false))
                return "";

            // Return the last string in the array which is the filename.
            string[] SplitName = InputFile.Split('\\');
            return SplitName[SplitName.Length - 1];
        }

        public static string GetBaseName(this string FileName)
        {
            // If the file does not have an extension.
            if (!FileName.Contains("."))
                return FileName;

            // When working with a path we want to keep only the filename.
            if (FileName.Contains("\\"))
            {
                string[] SplitSlashes = FileName.Split('\\');
                FileName = SplitSlashes[SplitSlashes.Length - 1];
            }
            // Some filenames may contain periods that are not separating the extension.
            string NewFileName = "";
            string[] SplitPeriods = FileName.Split('.');
            
            // Loop adding back text after periods besides the extension.
            int Loops = SplitPeriods.Length - 1;
            for (int i = 0; i < Loops; i++)
                NewFileName += SplitPeriods[i] + ".";

            // Return the result.
            return NewFileName.TrimEnd('.');
        }

        public static string Extend(this string InputString, int Length)
        {
            // Check the number of characters against the desired amount.
            if (InputString.Length < Length)
            {
                // Loop until the desired number of characters is added.
                int AddLength = Length - InputString.Length;
                for (int i = 1; i <= AddLength; i++)
                    InputString += " ";
            }
            // Return the modified string.
            return InputString;
        }

        public static string[] StrSplit(this string InputString, string SplitOn)
        {
            // Splits a string by the input string.
            return InputString.Split(new string[] { SplitOn }, StringSplitOptions.None);
        }

        public static string FormatNewLines(this string InputString)
        {
            // Split the string into a character array to pick out new lines.
            char[] CharArray = InputString.ToCharArray();
            string NewString = "";

            // Replace newline characters with ones that can use "Format".
            for (int i = 0; i < CharArray.Length; i++)
            {
                if (CharArray[i] == '\n')
                    NewString += "{0}";
                else
                    NewString += CharArray[i].ToString();
            }
            // Return the string where all '\n' were replaced with '{0}'.
            return NewString;
        }

        public static List<string> EnumToList(this IEnumerable<string> EnumArray)
        {
            // Convert "IEnumerable" to "List<string>".
            List<string> StringList = new List<string> { };
            foreach (string Item in EnumArray)
                StringList.Add(Item);
            return StringList;
        } 

        public static List<string> ArrayToList(this string[] StringArray)
        {
            // Convert "string[]" to "List<string>".
            List<string> StringList = new List<string> { };
            for (int i = 0; i < StringArray.Length; i++)
                StringList.Add(StringArray[i]);
            return StringList;
        }

        public static string[] ListToArray(this List<string> StringList)
        {
            // Convert "List<string>" to "string[]".
            string[] StringArray = new string[StringList.Count];
            for (int i = 0; i < StringList.Count; i++)
                StringArray[i] = StringList[i];
            return StringArray;
        }

        public static List<string> ReadLinesToList(this string TextFile)
        {
            // Get a text file as "List<string>" instead of "string[]".
            return File.ReadAllLines(TextFile).ArrayToList();
        }

        public static void Move<T>(this List<T> GenericList, int OldIndex, int NewIndex)
        {
            // Move the position of an item in a generic "List<T>".
            T ListItem = GenericList[OldIndex];
            GenericList.RemoveAt(OldIndex);
            GenericList.Insert(NewIndex, ListItem);
        }

        public static T[] Reverse<T>(this T[] OldArray)
        {
            // Create a new array to hold the reversed order.
            T[] NewArray = new T[OldArray.Length];

            // Loop through the old array in reverse and build the new array ascending.
            int Index = 0;
            for (int i = OldArray.Length - 1; i >= 0; i--)
            {
                NewArray[Index] = OldArray[i];
                Index++;
            }
            // Return the reversed array.
            return NewArray;
        }

        public static T[] RemoveAt<T>(this T[] OldArray, int Index)
        {
            // Create a new array with one less index.
            T[] NewArray = new T[OldArray.Length - 1];

            // If the index is beyond the first position copy the array up to that position.
            if (Index > 0)
                Array.Copy(OldArray, 0, NewArray, 0, Index);

            // When we reached the position of the index to remove copy everything beyond that index.
            if (Index < OldArray.Length - 1)
                Array.Copy(OldArray, Index + 1, NewArray, Index, OldArray.Length - Index - 1);

            // Return the new array with the data removed.
            return NewArray;
        }

        public static T[] Insert<T>(this T[] OldArray, int Index, dynamic Data)
        {
            // Create a new array with one more index.
            T[] NewArray = new T[OldArray.Length + 1];

            // If the index is beyond the first position copy the array up to that position.
            if (Index > 0)
                Array.Copy(OldArray, 0, NewArray, 0, Index);

            // Copy the data into the current position.
            NewArray[Index] = Data;

            // If the index falls within the upper bounds copy the rest of the data into the new array.
            if (Index < OldArray.Length - 1)
                Array.Copy(OldArray, Index, NewArray, Index + 1, OldArray.Length - Index);

            // Return the new array with the data added.
            return NewArray;
        }

        public static void DoubleBuffer(this Control InputControl, bool Enabled)
        {
            // Enable double buffering for the control.
            PropertyInfo controlProperty = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            controlProperty.SetValue(InputControl, Enabled, null);
        }

        public static string ShowFileDialog(this string StartPath, string[] FileName, string[] Description)
        {
            // Create the open file dialog.
            OpenFileDialog FileDialog = new OpenFileDialog();
            FileDialog.InitialDirectory = StartPath;

            // Concatenate the filter if multiple files and show the dialog.
            string FilterString = "";
            for (int i = 0; i < FileName.Length; i++)
                FilterString += Description[i] + "|" + FileName[i] + "|";
            FileDialog.Filter = FilterString.TrimEnd('|');
            FileDialog.ShowDialog();

            // Return the file that was selected.
            return FileDialog.FileName;
        }

        public static string[] ShowMultiFileDialog(this string StartPath, string[] FileName, string[] Description)
        {
            // Create the open file dialog with multi-select.
            OpenFileDialog FileDialog = new OpenFileDialog();
            FileDialog.InitialDirectory = StartPath;
            FileDialog.Multiselect = true;

            // Concatenate the filter if multiple files and show the dialog.
            string FilterString = "";
            for (int i = 0; i < FileName.Length; i++)
                FilterString += Description[i] + "|" + FileName[i] + "|";
            FileDialog.Filter = FilterString.TrimEnd('|');
            FileDialog.ShowDialog();

            // Return the files that were selected.
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
            // The index can't be null or lower than zero, and the new index must be within the upper and lower bounds.
            if (InputListBox.SelectedItem == null || InputListBox.SelectedIndex < 0 ||
                InputListBox.SelectedIndex + Direction < 0 || InputListBox.SelectedIndex + Direction >= InputListBox.Items.Count)
                return;

            // Calculate the new index based on the value for direction.
            int NewIndex = InputListBox.SelectedIndex + Direction;

            // See if it's a checked listbox and if it is, get the current check state before it's removed.
            CheckedListBox CheckedListBox = InputListBox as CheckedListBox;
            CheckState Checked = CheckState.Unchecked;
            if (CheckedListBox != null)
                Checked = CheckedListBox.GetItemCheckState(CheckedListBox.SelectedIndex);

            // Remove the old item and insert and select the new item.
            object Selected = InputListBox.SelectedItem;

            InputListBox.Items.Remove(Selected);
            InputListBox.Items.Insert(NewIndex, Selected);
            InputListBox.SetSelected(NewIndex, true);

            // If it was a checked listbox then restore the checkstate.
            if (CheckedListBox != null)
                CheckedListBox.SetItemCheckState(NewIndex, Checked);
        }
    }
}
