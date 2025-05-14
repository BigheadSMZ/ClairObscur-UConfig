using System.IO;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections;
using System.Linq;

namespace ClairObscurConfig
{
    internal partial class EngineINI
    {
        // The path to the INI file.
        public static string Path;
            
        // Instance of the INI file class.
        public static IniFile Class;

        // Used to track if values existed when INI was loaded.
        public static bool[] EmptyValue = new bool[18];

        // Stores the names of all "main" entries and their default values.
        public static List<string> EntriesList;
        public static List<string> DefaultValues;

        // Stores INI entries and active values.
        public static OrderedDictionary MainEntries;
        public static OrderedDictionary MoreEntries;

        public static void Initialize()
        {
            // Default to Steam folder name.
            string FolderName = "Windows";

            // If it's GamePass version change the folder name.
            if (Config.GameVersion == "GamePass")
            {
                FolderName = "WinGDK";
            }
            // Create the path to the INI if it doesn't exist.
            string BasePath = (Config.AppData + "\\Sandfall\\Saved\\Config\\" + FolderName).CreatePath();

            // Set the path and create the class.
            EngineINI.Path = BasePath + "\\Engine.ini";
            EngineINI.Class = new IniFile(EngineINI.Path);

            // Create the list of entries and the hashtables.
            EngineINI.EntriesList   = new List<string> { };
            EngineINI.DefaultValues = new List<string> { };
            EngineINI.MainEntries   = new OrderedDictionary();
            EngineINI.MoreEntries   = new OrderedDictionary();

            // No matter how many times I went over it in my head, I could not think of a way
            // to avoid generating this list. It's just easier to have it around for reference.
            EngineINI.EntriesList.Add("r.MaxAnisotropy");
            EngineINI.EntriesList.Add("r.DepthOfFieldQuality");
            EngineINI.EntriesList.Add("r.BloomQuality");
            EngineINI.EntriesList.Add("r.MotionBlurQuality");
            EngineINI.EntriesList.Add("r.LensFlareQuality");
            EngineINI.EntriesList.Add("r.Fog");
            EngineINI.EntriesList.Add("r.VolumetricFog");
            EngineINI.EntriesList.Add("r.SceneColorFringeQuality");
            EngineINI.EntriesList.Add("r.DisableDistortion");
            EngineINI.EntriesList.Add("r.FilmGrain");
            EngineINI.EntriesList.Add("r.ShadowQuality");
            EngineINI.EntriesList.Add("r.Shadow.MaxResolution");
            EngineINI.EntriesList.Add("r.Tonemapper.Quality");
            EngineINI.EntriesList.Add("r.Tonemapper.GrainQuantization");
            EngineINI.EntriesList.Add("r.Tonemapper.Sharpen");
            EngineINI.EntriesList.Add("r.ViewDistanceScale");
            EngineINI.EntriesList.Add("r.DFDistanceScale");
            EngineINI.EntriesList.Add("foliage.LODDistanceScale");

            // Set the default values.
            EngineINI.DefaultValues.Add("4");
            EngineINI.DefaultValues.Add("2");
            EngineINI.DefaultValues.Add("2");
            EngineINI.DefaultValues.Add("2");
            EngineINI.DefaultValues.Add("2");
            EngineINI.DefaultValues.Add("1");
            EngineINI.DefaultValues.Add("1");
            EngineINI.DefaultValues.Add("0");
            EngineINI.DefaultValues.Add("0");
            EngineINI.DefaultValues.Add("0");
            EngineINI.DefaultValues.Add("1");
            EngineINI.DefaultValues.Add("1024");
            EngineINI.DefaultValues.Add("5");
            EngineINI.DefaultValues.Add("1");
            EngineINI.DefaultValues.Add("0.6");
            EngineINI.DefaultValues.Add("1.00");
            EngineINI.DefaultValues.Add("1.00");
            EngineINI.DefaultValues.Add("0.75");

            // Use the list to add the various fields to the hashtable.
            for (int i = 0; i < EngineINI.EntriesList.Count; i++)
            {
                EngineINI.MainEntries.Add(EngineINI.EntriesList[i], EngineINI.DefaultValues[i]);
            }
        }

        public static void ReloadAdvancedOptions()
        {
            // Clear the values from the advanced hashtable.
            EngineINI.MoreEntries.Clear();

            // Get all keys from the INI file.
            List<string> Keys = EngineINI.Class.GetSectionKeys("SystemSettings");

            // Loop through the keys in the list.
            foreach (string Key in Keys)
            {
                // We only want keys that are not in the list above.
                if (!EngineINI.EntriesList.Contains(Key))
                {
                    // Add it to the other hashtable.
                    EngineINI.MoreEntries.Add(Key, "");
                }
            }
        }

        public static void RebuildAdvancedOptions()
        {
            // Clear out the hashtable so we can rebuild it from scratch.
            EngineINI.MoreEntries.Clear();

            // Loop through all potential entries.
            for (int i = 0; i < Forms.MaxRows; i++)
            {
                // Get the key and value from the datagridview.
                object Key   = Forms.MainDialog.GridView_Options[0, i].Value;
                object Value = Forms.MainDialog.GridView_Options[1, i].Value;

                // Make sure both of them are not empty or null.
                if ((Key != null) & (Value != null) && (Key.ToString() != "") & (Value.ToString() != ""))
                {
                    // Add the line to the dictionary.
                    EngineINI.MoreEntries.Add(Key, Value);
                }
            }
        }

        public static void CreateNew()
        {
            // Set the default values.
            for (int i = 0; i < EngineINI.MainEntries.Count; ++i)
            {
                EngineINI.MainEntries[i] = EngineINI.DefaultValues[i];
            }
            // Write the values to the INI file.
            EngineINI.WriteValues();
        }

        public static void Delete()
        {
            // If the INI doesn't exist, it's best to not outright crash.
            if (!EngineINI.Path.TestPath()) { return; }

            // Remove the read only attribute.
            File.SetAttributes(EngineINI.Path, ~FileAttributes.ReadOnly);

            // Delete the INI file.
            EngineINI.Path.RemovePath();
        }

        public static void ValidateValues()
        {
            // Arrays of the acceptable anisotropic filtering and shadow resolution values.
            string[] AFValues   = new string[] { "1", "2", "4", "8", "16" };
            string[] ShadValues = new string[] { "1024", "2048", "4096", "8192" };

            // If the values are not within a certain range, they will crash the GUI.
            EngineINI.MainEntries[0]  = Validate.StrArray(EngineINI.MainEntries[0].ToString(), AFValues, "4");          // r.MaxAnisotropy
            EngineINI.MainEntries[1]  = Validate.RangeInt(EngineINI.MainEntries[1].ToString(), 0, 4, "2");              // r.DepthOfFieldQuality
            EngineINI.MainEntries[2]  = Validate.RangeInt(EngineINI.MainEntries[2].ToString(), 0, 5, "2");              // r.BloomQuality
            EngineINI.MainEntries[3]  = Validate.RangeInt(EngineINI.MainEntries[3].ToString(), 0, 4, "2");              // r.MotionBlurQuality
            EngineINI.MainEntries[4]  = Validate.RangeInt(EngineINI.MainEntries[4].ToString(), 0, 3, "2");              // r.LensFlareQuality
            EngineINI.MainEntries[5]  = Validate.RangeInt(EngineINI.MainEntries[5].ToString(), 0, 1, "1");              // r.Fog
            EngineINI.MainEntries[6]  = Validate.RangeInt(EngineINI.MainEntries[6].ToString(), 0, 1, "1");              // r.VolumetricFog
            EngineINI.MainEntries[7]  = Validate.RangeInt(EngineINI.MainEntries[7].ToString(), 0, 1, "0");              // r.SceneColorFringeQuality
            EngineINI.MainEntries[8]  = Validate.RangeInt(EngineINI.MainEntries[8].ToString(), 0, 1, "0");              // r.DisableDistortion
            EngineINI.MainEntries[9]  = Validate.RangeInt(EngineINI.MainEntries[9].ToString(), 0, 1, "0");              // r.FilmGrain
            EngineINI.MainEntries[10] = Validate.RangeInt(EngineINI.MainEntries[10].ToString(), 1, 5, "1");             // r.ShadowQuality
            EngineINI.MainEntries[11] = Validate.StrArray(EngineINI.MainEntries[11].ToString(), ShadValues, "1024");    // r.Shadow.MaxResolution
            EngineINI.MainEntries[12] = Validate.RangeInt(EngineINI.MainEntries[12].ToString(), 0, 5, "5");             // r.Tonemapper.Quality
            EngineINI.MainEntries[13] = Validate.RangeInt(EngineINI.MainEntries[13].ToString(), 0, 1, "1");             // r.Tonemapper.GrainQuantization
            EngineINI.MainEntries[14] = Validate.RangeDec(EngineINI.MainEntries[14].ToString(), 0, 10.0, "0.6");        // r.Tonemapper.Sharpen
            EngineINI.MainEntries[15] = Validate.RangeDec(EngineINI.MainEntries[15].ToString(), 0.40, 10.00, "1.00");   // r.ViewDistanceScale
            EngineINI.MainEntries[16] = Validate.RangeDec(EngineINI.MainEntries[16].ToString(), 0.40, 10.00, "1.00");   // r.DFDistanceScale
            EngineINI.MainEntries[17] = Validate.RangeDec(EngineINI.MainEntries[17].ToString(), 0.40, 10.00, "0.75");   // foliage.LODDistanceScale
        }

        public static void LoadValues()
        {
            // If the INI doesn't exist, it's best to not outright crash.
            if (!EngineINI.Path.TestPath()) { return; }

            // Before we load the values we need to figure out which values are actually in the INI file.
            EngineINI.ReloadAdvancedOptions();

            // Loop through the main hashtable entries.
            for (int i = 0; i < EngineINI.MainEntries.Count; i++)
            {
                // Get the name of the key.
                string KeyName = EngineINI.MainEntries.Cast<DictionaryEntry>().ElementAt(i).Key.ToString();

                // Use the key to reference the position and retrieve the INI value.
                EngineINI.MainEntries[KeyName] = EngineINI.Class.Read(KeyName, "SystemSettings");

                // Tracks if keys were in the INI by checking if values were set.
                EngineINI.EmptyValue[i] = (EngineINI.MainEntries[KeyName].ToString() == "");
            }
            // Loop through the additional hashtable entries if they exist.
            for (int i = 0; i < EngineINI.MoreEntries.Count; i++)
            {
                // Get the name of the key.
                string KeyName = EngineINI.MoreEntries.Cast<DictionaryEntry>().ElementAt(i).Key.ToString();

                // Use the key to reference the position and retrieve the INI value.
                EngineINI.MoreEntries[KeyName] = EngineINI.Class.Read(KeyName, "SystemSettings");
            }
            // Make sure the values are not ones that can crash the GUI.
            EngineINI.ValidateValues();
        }

        public static void WriteValues()
        {
            // We'll throw an exception if the file doesn't already exist.
            if (EngineINI.Path.TestPath())
            {
                // Remove the read only attribute.
                File.SetAttributes(EngineINI.Path, ~FileAttributes.ReadOnly);
            }
            // Compile the two keys from the two hashtables together into a single list.
            List<string> ValidEntries = new List<string> { };

            // Rebuild the hashtable from the datagridview just before updating the INI to get the most recent values.
            EngineINI.RebuildAdvancedOptions();

            // Loop through the main hashtable entries.
            for (int i = 0; i < EngineINI.MainEntries.Count; i++)
            {
                // Get the name of the key.
                string Key   = EngineINI.MainEntries.Cast<DictionaryEntry>().ElementAt(i).Key.ToString();
                string Value = EngineINI.MainEntries[Key].ToString();

                // Use the key to reference the position and store the new value.
                EngineINI.Class.ConditionalWriteDelete(Key, Value, "SystemSettings", !Forms.ChkBoxArray[i].Checked);

                // Add all active main entries to the list.
                ValidEntries.Add(Key);
            }
            // Loop through the advanced hashtable entries.
            for (int i = 0; i < EngineINI.MoreEntries.Count; i++)
            {
                // Get the name of the key.
                string Key   = EngineINI.MoreEntries.Cast<DictionaryEntry>().ElementAt(i).Key.ToString();
                string Value = EngineINI.MoreEntries[Key].ToString();

                // Use the key to reference the position and store the new value.
                EngineINI.Class.Write(Key, Value, "SystemSettings");

                // Add all active advanced entries to the list.
                ValidEntries.Add(Key);
            }
            // Get a list of all entries currently in the INI file.
            List<string> ExistEntries = EngineINI.Class.GetSectionKeys("SystemSettings");

            // Loop through those entries.
            foreach (string Entry in ExistEntries)
            {
                // Use the valid entries list as a filter to know which entries to remove.
                if (!ValidEntries.Contains(Entry))
                {
                    // If the key is not in the validation list then remove it from the INI.
                    EngineINI.Class.DeleteKey(Entry, "SystemSettings");
                }
            }
            // Add the read only attribute.
            File.SetAttributes(EngineINI.Path, FileAttributes.ReadOnly);
        }

        public static void Backup()
        {
            // Set the INI backup path.
            string BackupPath = EngineINI.Path + ".bak";

            // Make sure the INI file exists.
            if (EngineINI.Path.TestPath() & Forms.PromptBackupINI())
            {
                // Save current values when creating a backup.
                EngineINI.WriteValues();
                 
                // If a backup exists, remove it before creating a new one.
                if (BackupPath.TestPath())
                {
                    // If the read-only attribute was retained, remove it so it can be deleted.
                    File.SetAttributes(BackupPath, ~FileAttributes.ReadOnly);
                    BackupPath.RemovePath();
                }
                // Copy it using ".bak" extension.
                File.Copy(EngineINI.Path, BackupPath, true);

                // Disable the backup option since a backup no longer exists.
                Forms.MainDialog.StripItem_RestoreBackup.Enabled = true;
            }
        }

        public static void Restore()
        {
            // Set the INI backup path.
            string BackupPath = EngineINI.Path + ".bak";

            // Make sure the backup file exists.
            if (BackupPath.TestPath() & Forms.PromptRestoreINI())
            {
                // Make sure the INI exists before setting attributes.
                if (EngineINI.Path.TestPath())
                {
                    File.SetAttributes(EngineINI.Path, ~FileAttributes.ReadOnly);
                }
                // Rename the backup to the main INI file.
                BackupPath.RenamePath(EngineINI.Path, true);
                File.SetAttributes(EngineINI.Path, FileAttributes.ReadOnly);

                // Disable the backup option since a backup no longer exists.
                Forms.MainDialog.StripItem_RestoreBackup.Enabled = false;

                // Reload the values and update the values on the GUI.
                EngineINI.LoadValues();
                Forms.UpdateValues();
                Forms.ToggleGUI(true);
            }
        }
    }
}
