using System;
using System.IO;

namespace ClairObscurConfig
{
    internal partial class EngineINI
    {
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
        public static string TMSha_Val; // - r.Tonemapper.Sharpen
        public static string TMGra_Val; // - r.Tonemapper.GrainQuantization
        public static string ViewD_Val; // - r.ViewDistanceScale
        public static string ViewS_Val; // - r.DFDistanceScale
        public static string ViewF_Val; // - foliage.LODDistanceScale

        public static void DeleteINIFile()
        {
            // Remove the read only attribute.
            File.SetAttributes(Config.INIPath, ~FileAttributes.ReadOnly);

            // Delete the INI file.
            Config.INIPath.RemovePath();
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
            EngineINI.TMSha_Val = "0.6";
            EngineINI.TMGra_Val = "1";
            EngineINI.ViewD_Val = "1.00";
            EngineINI.ViewS_Val = "1.00";
            EngineINI.ViewF_Val = "0.75";

            // Write the values to the INI file.
            EngineINI.WriteINIValues(true);
        }

        public static void LoadINIValues()
        {
            // Track if some values could not be loaded.
            bool ValuesMissing = false;

            // Load the values from the INI file.
            EngineINI.Anist_Val = Config.INIFile.Read(EngineINI.Anist_Str, "SystemSettings"); // - r.MaxAnisotropy
            EngineINI.Depth_Val = Config.INIFile.Read(EngineINI.Depth_Str, "SystemSettings"); // - r.DepthOfFieldQuality
            EngineINI.Bloom_Val = Config.INIFile.Read(EngineINI.Bloom_Str, "SystemSettings"); // - r.BloomQuality
            EngineINI.MBlur_Val = Config.INIFile.Read(EngineINI.MBlur_Str, "SystemSettings"); // - r.MotionBlurQuality
            EngineINI.LenFl_Val = Config.INIFile.Read(EngineINI.LenFl_Str, "SystemSettings"); // - r.LensFlareQuality
            EngineINI.FogEf_Val = Config.INIFile.Read(EngineINI.FogEf_Str, "SystemSettings"); // - r.Fog
            EngineINI.VoFog_Val = Config.INIFile.Read(EngineINI.VoFog_Str, "SystemSettings"); // - r.VolumetricFog
            EngineINI.SCoFr_Val = Config.INIFile.Read(EngineINI.SCoFr_Str, "SystemSettings"); // - r.SceneColorFringeQuality
            EngineINI.Distr_Val = Config.INIFile.Read(EngineINI.Distr_Str, "SystemSettings"); // - r.DisableDistortion
            EngineINI.Grain_Val = Config.INIFile.Read(EngineINI.Grain_Str, "SystemSettings"); // - r.FilmGrain
            EngineINI.ShadQ_Val = Config.INIFile.Read(EngineINI.ShadQ_Str, "SystemSettings"); // - r.ShadowQuality
            EngineINI.ShadR_Val = Config.INIFile.Read(EngineINI.ShadR_Str, "SystemSettings"); // - r.Shadow.MaxResolution
            EngineINI.TMQua_Val = Config.INIFile.Read(EngineINI.TMQua_Str, "SystemSettings"); // - r.Tonemapper.Quality
            EngineINI.TMSha_Val = Config.INIFile.Read(EngineINI.TMSha_Str, "SystemSettings"); // - r.Tonemapper.Sharpen
            EngineINI.TMGra_Val = Config.INIFile.Read(EngineINI.TMGra_Str, "SystemSettings"); // - r.Tonemapper.GrainQuantization
            EngineINI.ViewD_Val = Config.INIFile.Read(EngineINI.ViewD_Str, "SystemSettings"); // - r.ViewDistanceScale
            EngineINI.ViewS_Val = Config.INIFile.Read(EngineINI.ViewS_Str, "SystemSettings"); // - r.DFDistanceScale
            EngineINI.ViewF_Val = Config.INIFile.Read(EngineINI.ViewF_Str, "SystemSettings"); // - foliage.LODDistanceScale

            // If the INI existed, but some values don't exist, then fill them with default values.
            if (EngineINI.Anist_Val == "") { ValuesMissing = true; EngineINI.Anist_Val = "4"; }
            if (EngineINI.Depth_Val == "") { ValuesMissing = true; EngineINI.Depth_Val = "2"; }
            if (EngineINI.Bloom_Val == "") { ValuesMissing = true; EngineINI.Bloom_Val = "2"; }
            if (EngineINI.MBlur_Val == "") { ValuesMissing = true; EngineINI.MBlur_Val = "2"; }
            if (EngineINI.LenFl_Val == "") { ValuesMissing = true; EngineINI.LenFl_Val = "2"; }
            if (EngineINI.FogEf_Val == "") { ValuesMissing = true; EngineINI.FogEf_Val = "1"; }
            if (EngineINI.VoFog_Val == "") { ValuesMissing = true; EngineINI.VoFog_Val = "1"; }
            if (EngineINI.SCoFr_Val == "") { ValuesMissing = true; EngineINI.SCoFr_Val = "0"; }
            if (EngineINI.Distr_Val == "") { ValuesMissing = true; EngineINI.Distr_Val = "0"; }
            if (EngineINI.Grain_Val == "") { ValuesMissing = true; EngineINI.Grain_Val = "0"; }
            if (EngineINI.ShadQ_Val == "") { ValuesMissing = true; EngineINI.ShadQ_Val = "1"; }
            if (EngineINI.ShadR_Val == "") { ValuesMissing = true; EngineINI.ShadR_Val = "1024"; }
            if (EngineINI.TMQua_Val == "") { ValuesMissing = true; EngineINI.TMQua_Val = "5"; }
            if (EngineINI.TMSha_Val == "") { ValuesMissing = true; EngineINI.TMSha_Val = "0.6"; }
            if (EngineINI.TMGra_Val == "") { ValuesMissing = true; EngineINI.TMGra_Val = "1"; }
            if (EngineINI.ViewD_Val == "") { ValuesMissing = true; EngineINI.ViewD_Val = "1.00"; }
            if (EngineINI.ViewS_Val == "") { ValuesMissing = true; EngineINI.ViewS_Val = "1.00"; }
            if (EngineINI.ViewF_Val == "") { ValuesMissing = true; EngineINI.ViewF_Val = "0.75"; }

            // If values had to be appended then save the INI.
            EngineINI.WriteINIValues();
        }
        public static void WriteINIValues(bool NewINI = false)
        {
            // We'll throw an exception if the file doesn't already exist.
            if (!NewINI)
            {
                // Remove the read only attribute.
                File.SetAttributes(Config.INIPath, ~FileAttributes.ReadOnly);
            }
            // Write the values to the INI file.
            Config.INIFile.Write(EngineINI.Anist_Str, EngineINI.Anist_Val, "SystemSettings"); // - r.MaxAnisotropy
            Config.INIFile.Write(EngineINI.Depth_Str, EngineINI.Depth_Val, "SystemSettings"); // - r.DepthOfFieldQuality
            Config.INIFile.Write(EngineINI.Bloom_Str, EngineINI.Bloom_Val, "SystemSettings"); // - r.BloomQuality
            Config.INIFile.Write(EngineINI.MBlur_Str, EngineINI.MBlur_Val, "SystemSettings"); // - r.MotionBlurQuality
            Config.INIFile.Write(EngineINI.LenFl_Str, EngineINI.LenFl_Val, "SystemSettings"); // - r.LensFlareQuality
            Config.INIFile.Write(EngineINI.FogEf_Str, EngineINI.FogEf_Val, "SystemSettings"); // - r.Fog
            Config.INIFile.Write(EngineINI.VoFog_Str, EngineINI.VoFog_Val, "SystemSettings"); // - r.VolumetricFog
            Config.INIFile.Write(EngineINI.SCoFr_Str, EngineINI.SCoFr_Val, "SystemSettings"); // - r.SceneColorFringeQuality
            Config.INIFile.Write(EngineINI.Distr_Str, EngineINI.Distr_Val, "SystemSettings"); // - r.DisableDistortion
            Config.INIFile.Write(EngineINI.Grain_Str, EngineINI.Grain_Val, "SystemSettings"); // - r.FilmGrain
            Config.INIFile.Write(EngineINI.ShadQ_Str, EngineINI.ShadQ_Val, "SystemSettings"); // - r.ShadowQuality
            Config.INIFile.Write(EngineINI.ShadR_Str, EngineINI.ShadR_Val, "SystemSettings"); // - r.Shadow.MaxResolution
            Config.INIFile.Write(EngineINI.TMQua_Str, EngineINI.TMQua_Val, "SystemSettings"); // - r.Tonemapper.Quality
            Config.INIFile.Write(EngineINI.TMSha_Str, EngineINI.TMSha_Val, "SystemSettings"); // - r.Tonemapper.Sharpen
            Config.INIFile.Write(EngineINI.TMGra_Str, EngineINI.TMGra_Val, "SystemSettings"); // - r.Tonemapper.GrainQuantization
            Config.INIFile.Write(EngineINI.ViewD_Str, EngineINI.ViewD_Val, "SystemSettings"); // - r.ViewDistanceScale
            Config.INIFile.Write(EngineINI.ViewS_Str, EngineINI.ViewS_Val, "SystemSettings"); // - r.DFDistanceScale
            Config.INIFile.Write(EngineINI.ViewF_Str, EngineINI.ViewF_Val, "SystemSettings"); // - foliage.LODDistanceScale

            // Add the read only attribute.
            File.SetAttributes(Config.INIPath, FileAttributes.ReadOnly);
        }
    }
}
