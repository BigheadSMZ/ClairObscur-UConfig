using System.IO;

namespace ClairObscurConfig
{
    internal partial class EngineINI
    {
        public static string  Path;
        public static string  Type;
        public static IniFile File;

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

        public static void InitializeINI()
        {
            // The paths the INI can be found.
            string SteamBase    = Config.AppData + "\\Sandfall\\Saved\\Config\\Windows";
            string GamePassBase = Config.AppData + "\\Sandfall\\Saved\\Config\\WinGDK";
            string SteamPath    = SteamBase + "\\Engine.ini";
            string GamePassPath = GamePassBase + "\\Engine.ini";

            // Just to be safe, create the base paths if they don't exist.
            SteamBase.CreatePath(true);
            GamePassBase.CreatePath(true);

            // Try the GamePass path first.
            if (GamePassPath.TestPath()) 
            {
                EngineINI.Type = "GamePass";
                EngineINI.Path = GamePassPath;
                EngineINI.File = new IniFile(GamePassPath);
            }
            // If it doesn't exist, default to Steam path.
            EngineINI.Type = "Steam";
            EngineINI.Path = SteamPath;
            EngineINI.File = new IniFile(SteamPath);
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

        public static void WriteNewINIFile()
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
            EngineINI.WriteINIValues(true);
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

            // Make sure the values are not ones that can crash the GUI.
            EngineINI.ValidateValues();
        }

        public static void WriteINIValues(bool NewINI = false)
        {
            // We'll throw an exception if the file doesn't already exist.
            if (!NewINI)
            {
                // Remove the read only attribute.
                System.IO.File.SetAttributes(EngineINI.Path, ~FileAttributes.ReadOnly);
            }
            // Write the values to the INI file.
            EngineINI.File.Write(EngineINI.Anist_Str, EngineINI.Anist_Val, "SystemSettings"); // - r.MaxAnisotropy
            EngineINI.File.Write(EngineINI.Depth_Str, EngineINI.Depth_Val, "SystemSettings"); // - r.DepthOfFieldQuality
            EngineINI.File.Write(EngineINI.Bloom_Str, EngineINI.Bloom_Val, "SystemSettings"); // - r.BloomQuality
            EngineINI.File.Write(EngineINI.MBlur_Str, EngineINI.MBlur_Val, "SystemSettings"); // - r.MotionBlurQuality
            EngineINI.File.Write(EngineINI.LenFl_Str, EngineINI.LenFl_Val, "SystemSettings"); // - r.LensFlareQuality
            EngineINI.File.Write(EngineINI.FogEf_Str, EngineINI.FogEf_Val, "SystemSettings"); // - r.Fog
            EngineINI.File.Write(EngineINI.VoFog_Str, EngineINI.VoFog_Val, "SystemSettings"); // - r.VolumetricFog
            EngineINI.File.Write(EngineINI.SCoFr_Str, EngineINI.SCoFr_Val, "SystemSettings"); // - r.SceneColorFringeQuality
            EngineINI.File.Write(EngineINI.Distr_Str, EngineINI.Distr_Val, "SystemSettings"); // - r.DisableDistortion
            EngineINI.File.Write(EngineINI.Grain_Str, EngineINI.Grain_Val, "SystemSettings"); // - r.FilmGrain
            EngineINI.File.Write(EngineINI.ShadQ_Str, EngineINI.ShadQ_Val, "SystemSettings"); // - r.ShadowQuality
            EngineINI.File.Write(EngineINI.ShadR_Str, EngineINI.ShadR_Val, "SystemSettings"); // - r.Shadow.MaxResolution
            EngineINI.File.Write(EngineINI.TMQua_Str, EngineINI.TMQua_Val, "SystemSettings"); // - r.Tonemapper.Quality
            EngineINI.File.Write(EngineINI.TMGra_Str, EngineINI.TMGra_Val, "SystemSettings"); // - r.Tonemapper.GrainQuantization
            EngineINI.File.Write(EngineINI.TMSha_Str, EngineINI.TMSha_Val, "SystemSettings"); // - r.Tonemapper.Sharpen
            EngineINI.File.Write(EngineINI.ViewD_Str, EngineINI.ViewD_Val, "SystemSettings"); // - r.ViewDistanceScale
            EngineINI.File.Write(EngineINI.ViewS_Str, EngineINI.ViewS_Val, "SystemSettings"); // - r.DFDistanceScale
            EngineINI.File.Write(EngineINI.ViewF_Str, EngineINI.ViewF_Val, "SystemSettings"); // - foliage.LODDistanceScale

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
