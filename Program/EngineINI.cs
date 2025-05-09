﻿using System.IO;

namespace ClairObscurConfig
{
    internal partial class EngineINI
    {
        // Stores info about the Engine.ini file.
        public static string  Path;
        public static IniFile File;

        // Used to track if values existed when INI was loaded.
        public static bool[] EmptyValue = new bool[18];

        // Short vars to store the strings as they are in the INI.
        public static string Anist_Str = "r.MaxAnisotropy";
        public static string Depth_Str = "r.DepthOfFieldQuality";
        public static string Bloom_Str = "r.BloomQuality";
        public static string MBlur_Str = "r.MotionBlurQuality";
        public static string LenFl_Str = "r.LensFlareQuality";
        public static string FogEf_Str = "r.Fog";
        public static string VoFog_Str = "r.VolumetricFog";
        public static string SCoFr_Str = "r.SceneColorFringeQuality";
        public static string Distr_Str = "r.DisableDistortion";
        public static string Grain_Str = "r.FilmGrain";
        public static string ShadQ_Str = "r.ShadowQuality";
        public static string ShadR_Str = "r.Shadow.MaxResolution";
        public static string TMQua_Str = "r.Tonemapper.Quality";
        public static string TMSha_Str = "r.Tonemapper.Sharpen";
        public static string TMGra_Str = "r.Tonemapper.GrainQuantization";
        public static string ViewD_Str = "r.ViewDistanceScale";
        public static string ViewS_Str = "r.DFDistanceScale";
        public static string ViewF_Str = "foliage.LODDistanceScale";

        // Vars to store the values.
        public static string Anist_Val; // - r.MaxAnisotropy
        public static string Depth_Val; // - r.DepthOfFieldQuality
        public static string Bloom_Val; // - r.BloomQuality
        public static string MBlur_Val; // - r.MotionBlurQuality
        public static string LenFl_Val; // - r.LensFlareQuality
        public static string FogEf_Val; // - r.Fog
        public static string VoFog_Val; // - r.VolumetricFog
        public static string SCoFr_Val; // - r.SceneColorFringeQuality
        public static string Distr_Val; // - r.DisableDistortion
        public static string Grain_Val; // - r.FilmGrain
        public static string ShadQ_Val; // - r.ShadowQuality
        public static string ShadR_Val; // - r.Shadow.MaxResolution
        public static string TMQua_Val; // - r.Tonemapper.Quality
        public static string TMGra_Val; // - r.Tonemapper.GrainQuantization
        public static string TMSha_Val; // - r.Tonemapper.Sharpen
        public static string ViewD_Val; // - r.ViewDistanceScale
        public static string ViewS_Val; // - r.DFDistanceScale
        public static string ViewF_Val; // - foliage.LODDistanceScale

        public static void InitializeINI(string GameVersion)
        {
            // Default to Steam folder name.
            string FolderName = "Windows";

            // If it's GamePass version change the folder name.
            if (GameVersion == "GamePass") 
            {
                FolderName = "WinGDK";
            }
            // Create the path to the INI if it doesn't exist.
            string BasePath = (Config.AppData + "\\Sandfall\\Saved\\Config\\" + FolderName).CreatePath();

            // Set the path and create the class.
            EngineINI.Path = BasePath + "\\Engine.ini";
            EngineINI.File = new IniFile(EngineINI.Path);

            // Try to load the values.
            EngineINI.LoadINIValues();
        }

        public static void CreateNewINIFile()
        {
            // Set the default values.
            EngineINI.Anist_Val = "4";
            EngineINI.Depth_Val = "2";
            EngineINI.Bloom_Val = "2";
            EngineINI.MBlur_Val = "2";
            EngineINI.LenFl_Val = "2";
            EngineINI.FogEf_Val = "1";
            EngineINI.VoFog_Val = "1";
            EngineINI.SCoFr_Val = "0";
            EngineINI.Distr_Val = "0";
            EngineINI.Grain_Val = "0";
            EngineINI.ShadQ_Val = "1";
            EngineINI.ShadR_Val = "1024";
            EngineINI.TMQua_Val = "5";
            EngineINI.TMGra_Val = "1";
            EngineINI.TMSha_Val = "0.6";
            EngineINI.ViewD_Val = "1.00";
            EngineINI.ViewS_Val = "1.00";
            EngineINI.ViewF_Val = "0.75";

            // Write the values to the INI file.
            EngineINI.WriteINIValues();
        }

        public static void DeleteINIFile()
        {
            // If the INI doesn't exist, it's best to not outright crash.
            if (!EngineINI.Path.TestPath()) { return; }

            // Remove the read only attribute.
            System.IO.File.SetAttributes(EngineINI.Path, ~FileAttributes.ReadOnly);

            // Delete the INI file.
            EngineINI.Path.RemovePath();
        }

        public static void ValidateValues()
        {
            // Arrays of the acceptable anisotropic filtering and shadow resolution values.
            string[] AFValues   = new string[] { "1", "2", "4", "8", "16" };
            string[] ShadValues = new string[] { "1024", "2048", "4096", "8192" };

            // If the values are not within a certain range, they will crash the GUI.
            EngineINI.Anist_Val = Validate.StrArray(EngineINI.Anist_Val, AFValues, "4");
            EngineINI.Depth_Val = Validate.RangeInt(EngineINI.Depth_Val, 0, 4, "2");
            EngineINI.Bloom_Val = Validate.RangeInt(EngineINI.Bloom_Val, 0, 5, "2");
            EngineINI.MBlur_Val = Validate.RangeInt(EngineINI.MBlur_Val, 0, 4, "2");
            EngineINI.LenFl_Val = Validate.RangeInt(EngineINI.LenFl_Val, 0, 3, "2");
            EngineINI.FogEf_Val = Validate.RangeInt(EngineINI.FogEf_Val, 0, 1, "1");
            EngineINI.VoFog_Val = Validate.RangeInt(EngineINI.VoFog_Val, 0, 1, "1");
            EngineINI.SCoFr_Val = Validate.RangeInt(EngineINI.SCoFr_Val, 0, 1, "0");
            EngineINI.Distr_Val = Validate.RangeInt(EngineINI.Distr_Val, 0, 1, "0");
            EngineINI.Grain_Val = Validate.RangeInt(EngineINI.Grain_Val, 0, 1, "0");
            EngineINI.ShadQ_Val = Validate.RangeInt(EngineINI.ShadQ_Val, 1, 5, "1");
            EngineINI.ShadR_Val = Validate.StrArray(EngineINI.ShadR_Val, ShadValues, "1024");
            EngineINI.TMQua_Val = Validate.RangeInt(EngineINI.TMQua_Val, 0, 5, "5");
            EngineINI.TMGra_Val = Validate.RangeInt(EngineINI.TMGra_Val, 0, 1, "1");
            EngineINI.TMSha_Val = Validate.RangeDec(EngineINI.TMSha_Val, 0, 10.0, "0.6");
            EngineINI.ViewD_Val = Validate.RangeDec(EngineINI.ViewD_Val, 0.40, 10.00, "1.00");
            EngineINI.ViewS_Val = Validate.RangeDec(EngineINI.ViewS_Val, 0.40, 10.00, "1.00");
            EngineINI.ViewF_Val = Validate.RangeDec(EngineINI.ViewF_Val, 0.40, 10.00, "0.75");
        }

        // NOTE: Eventually rewrite the INI keys and values to be stored in an ordered dictionary, which would allow
        // looping in the majority of functions here. But for now, take the lazy way out and check each value one by one.
        // I always make myself the victim of feature creep and fail to foresee these situations...

        public static void LoadINIValues()
        {
            // If the INI doesn't exist, it's best to not outright crash.
            if (!EngineINI.Path.TestPath()) { return; }

            // Load the values from the INI file.
            EngineINI.Anist_Val = EngineINI.File.Read(EngineINI.Anist_Str, "SystemSettings"); // - r.MaxAnisotropy
            EngineINI.Depth_Val = EngineINI.File.Read(EngineINI.Depth_Str, "SystemSettings"); // - r.DepthOfFieldQuality
            EngineINI.Bloom_Val = EngineINI.File.Read(EngineINI.Bloom_Str, "SystemSettings"); // - r.BloomQuality
            EngineINI.MBlur_Val = EngineINI.File.Read(EngineINI.MBlur_Str, "SystemSettings"); // - r.MotionBlurQuality
            EngineINI.LenFl_Val = EngineINI.File.Read(EngineINI.LenFl_Str, "SystemSettings"); // - r.LensFlareQuality
            EngineINI.FogEf_Val = EngineINI.File.Read(EngineINI.FogEf_Str, "SystemSettings"); // - r.Fog
            EngineINI.VoFog_Val = EngineINI.File.Read(EngineINI.VoFog_Str, "SystemSettings"); // - r.VolumetricFog
            EngineINI.SCoFr_Val = EngineINI.File.Read(EngineINI.SCoFr_Str, "SystemSettings"); // - r.SceneColorFringeQuality
            EngineINI.Distr_Val = EngineINI.File.Read(EngineINI.Distr_Str, "SystemSettings"); // - r.DisableDistortion
            EngineINI.Grain_Val = EngineINI.File.Read(EngineINI.Grain_Str, "SystemSettings"); // - r.FilmGrain
            EngineINI.ShadQ_Val = EngineINI.File.Read(EngineINI.ShadQ_Str, "SystemSettings"); // - r.ShadowQuality
            EngineINI.ShadR_Val = EngineINI.File.Read(EngineINI.ShadR_Str, "SystemSettings"); // - r.Shadow.MaxResolution
            EngineINI.TMQua_Val = EngineINI.File.Read(EngineINI.TMQua_Str, "SystemSettings"); // - r.Tonemapper.Quality
            EngineINI.TMGra_Val = EngineINI.File.Read(EngineINI.TMGra_Str, "SystemSettings"); // - r.Tonemapper.GrainQuantization
            EngineINI.TMSha_Val = EngineINI.File.Read(EngineINI.TMSha_Str, "SystemSettings"); // - r.Tonemapper.Sharpen
            EngineINI.ViewD_Val = EngineINI.File.Read(EngineINI.ViewD_Str, "SystemSettings"); // - r.ViewDistanceScale
            EngineINI.ViewS_Val = EngineINI.File.Read(EngineINI.ViewS_Str, "SystemSettings"); // - r.DFDistanceScale
            EngineINI.ViewF_Val = EngineINI.File.Read(EngineINI.ViewF_Str, "SystemSettings"); // - foliage.LODDistanceScale

            // Tracks if keys were in the INI by checking if values were set.
            EngineINI.EmptyValue[0]  = (EngineINI.Anist_Val == "");
            EngineINI.EmptyValue[1]  = (EngineINI.Depth_Val == "");
            EngineINI.EmptyValue[2]  = (EngineINI.Bloom_Val == "");
            EngineINI.EmptyValue[3]  = (EngineINI.MBlur_Val == "");
            EngineINI.EmptyValue[4]  = (EngineINI.LenFl_Val == "");
            EngineINI.EmptyValue[5]  = (EngineINI.FogEf_Val == "");
            EngineINI.EmptyValue[6]  = (EngineINI.VoFog_Val == "");
            EngineINI.EmptyValue[7]  = (EngineINI.SCoFr_Val == "");
            EngineINI.EmptyValue[8]  = (EngineINI.Distr_Val == "");
            EngineINI.EmptyValue[9]  = (EngineINI.Grain_Val == "");
            EngineINI.EmptyValue[10] = (EngineINI.ShadQ_Val == "");
            EngineINI.EmptyValue[11] = (EngineINI.ShadR_Val == "");
            EngineINI.EmptyValue[12] = (EngineINI.TMQua_Val == "");
            EngineINI.EmptyValue[13] = (EngineINI.TMGra_Val == "");
            EngineINI.EmptyValue[14] = (EngineINI.TMSha_Val == "");
            EngineINI.EmptyValue[15] = (EngineINI.ViewD_Val == "");
            EngineINI.EmptyValue[16] = (EngineINI.ViewS_Val == "");
            EngineINI.EmptyValue[17] = (EngineINI.ViewF_Val == "");

            // Make sure the values are not ones that can crash the GUI.
            EngineINI.ValidateValues();
        }

        public static void WriteINIValues()
        {
            // We'll throw an exception if the file doesn't already exist.
            if (EngineINI.Path.TestPath())
            {
                // Remove the read only attribute.
                System.IO.File.SetAttributes(EngineINI.Path, ~FileAttributes.ReadOnly);
            }
            // Reduce some bloat by storing this in a variable.
            Form_MainForm MainForm = Forms.MainDialog;

            // Write the values to the INI file.
            EngineINI.File.ConditionalWriteDelete(EngineINI.Anist_Str, EngineINI.Anist_Val, "SystemSettings", !MainForm.CheckBox_AF.Checked);         // - r.MaxAnisotropy
            EngineINI.File.ConditionalWriteDelete(EngineINI.Depth_Str, EngineINI.Depth_Val, "SystemSettings", !MainForm.CheckBox_DoF.Checked);        // - r.DepthOfFieldQuality
            EngineINI.File.ConditionalWriteDelete(EngineINI.Bloom_Str, EngineINI.Bloom_Val, "SystemSettings", !MainForm.CheckBox_Bloom.Checked);      // - r.BloomQuality
            EngineINI.File.ConditionalWriteDelete(EngineINI.MBlur_Str, EngineINI.MBlur_Val, "SystemSettings", !MainForm.CheckBox_MBlur.Checked);      // - r.MotionBlurQuality
            EngineINI.File.ConditionalWriteDelete(EngineINI.LenFl_Str, EngineINI.LenFl_Val, "SystemSettings", !MainForm.CheckBox_LensFlare.Checked);  // - r.LensFlareQuality
            EngineINI.File.ConditionalWriteDelete(EngineINI.FogEf_Str, EngineINI.FogEf_Val, "SystemSettings", !MainForm.CheckBox_Fog.Checked);        // - r.Fog
            EngineINI.File.ConditionalWriteDelete(EngineINI.VoFog_Str, EngineINI.VoFog_Val, "SystemSettings", !MainForm.CheckBox_VFog.Checked);       // - r.VolumetricFog
            EngineINI.File.ConditionalWriteDelete(EngineINI.SCoFr_Str, EngineINI.SCoFr_Val, "SystemSettings", !MainForm.CheckBox_ChromAb.Checked);    // - r.SceneColorFringeQuality
            EngineINI.File.ConditionalWriteDelete(EngineINI.Distr_Str, EngineINI.Distr_Val, "SystemSettings", !MainForm.CheckBox_Distort.Checked);    // - r.DisableDistortion
            EngineINI.File.ConditionalWriteDelete(EngineINI.Grain_Str, EngineINI.Grain_Val, "SystemSettings", !MainForm.CheckBox_FilmGrain.Checked);  // - r.FilmGrain
            EngineINI.File.ConditionalWriteDelete(EngineINI.ShadQ_Str, EngineINI.ShadQ_Val, "SystemSettings", !MainForm.CheckBox_ShadQual.Checked);   // - r.ShadowQuality
            EngineINI.File.ConditionalWriteDelete(EngineINI.ShadR_Str, EngineINI.ShadR_Val, "SystemSettings", !MainForm.CheckBox_ShadRes.Checked);    // - r.Shadow.MaxResolution
            EngineINI.File.ConditionalWriteDelete(EngineINI.TMQua_Str, EngineINI.TMQua_Val, "SystemSettings", !MainForm.CheckBox_Tonemap.Checked);    // - r.Tonemapper.Quality
            EngineINI.File.ConditionalWriteDelete(EngineINI.TMGra_Str, EngineINI.TMGra_Val, "SystemSettings", !MainForm.CheckBox_GrainQuant.Checked); // - r.Tonemapper.GrainQuantization
            EngineINI.File.ConditionalWriteDelete(EngineINI.TMSha_Str, EngineINI.TMSha_Val, "SystemSettings", !MainForm.CheckBox_Sharpen.Checked);    // - r.Tonemapper.Sharpen
            EngineINI.File.ConditionalWriteDelete(EngineINI.ViewD_Str, EngineINI.ViewD_Val, "SystemSettings", !MainForm.CheckBox_ViewDist.Checked);   // - r.ViewDistanceScale
            EngineINI.File.ConditionalWriteDelete(EngineINI.ViewS_Str, EngineINI.ViewS_Val, "SystemSettings", !MainForm.CheckBox_ShadDist.Checked);   // - r.DFDistanceScale
            EngineINI.File.ConditionalWriteDelete(EngineINI.ViewF_Str, EngineINI.ViewF_Val, "SystemSettings", !MainForm.CheckBox_FolDist.Checked);    // - foliage.LODDistanceScale

            // Add the read only attribute.
            System.IO.File.SetAttributes(EngineINI.Path, FileAttributes.ReadOnly);
        }

        public static void Backup()
        {
            // Set the INI backup path.
            string BackupPath = EngineINI.Path + ".bak";

            // Make sure the INI file exists.
            if (EngineINI.Path.TestPath() & Forms.PromptBackupINI())
            {
                // Save current values when creating a backup.
                EngineINI.WriteINIValues();
                 
                // If a backup exists, remove it before creating a new one.
                if (BackupPath.TestPath())
                {
                    // If the read-only attribute was retained, remove it so it can be deleted.
                    System.IO.File.SetAttributes(BackupPath, ~FileAttributes.ReadOnly);
                    System.IO.File.Delete(BackupPath);
                }
                // Copy it using ".bak" extension.
                System.IO.File.Copy(EngineINI.Path, BackupPath, true);

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
                // Delete the current INI and remove the ".bak" extension from the backup.
                System.IO.File.SetAttributes(EngineINI.Path, ~FileAttributes.ReadOnly);
                System.IO.File.Delete(EngineINI.Path);
                System.IO.File.Move(BackupPath, EngineINI.Path);
                System.IO.File.SetAttributes(EngineINI.Path, FileAttributes.ReadOnly);

                // Disable the backup option since a backup no longer exists.
                Forms.MainDialog.StripItem_RestoreBackup.Enabled = false;

                // Reload the values and update the values on the GUI.
                EngineINI.LoadINIValues();
                Forms.UpdateValues();
            }
        }
    }
}
