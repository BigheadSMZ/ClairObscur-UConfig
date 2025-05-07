using System;
using System.IO;
using System.Reflection;

namespace ClairObscurConfig
{
    internal class Config
    {
        // The registry key to where to store option settings.
        public const  string  RegEntry = "HKEY_CURRENT_USER\\Software\\BigheadSoft\\E33 Unreal Edit\\";

        // Stored values from the registry.
        public static bool    DisableCheckBoxes;
        public static bool    CloseOnLaunch;
        public static string  GameVersion;

        // Values used across the application.
        public static string  AppName;
        public static string  AppPath;
        public static string  AppData;
        public static string  AppVersion;
        public static string  BasePath;
        public static string  GamePath;

        public static void SetApplicationValues()
        {
            // Get the folder this app is in.
            Config.AppPath  = Assembly.GetExecutingAssembly().Location;
            Config.AppName  = Config.AppPath.GetFileName();
            Config.AppData  = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            Config.BasePath = Path.GetDirectoryName(Config.AppPath);
            Config.GamePath = Game.GetExecutable();

            // Try to get the values from the registry.
            Config.GameVersion = Functions.GetRegistryValue(Config.RegEntry, "GameVersion", "Steam");
            string LaunchClose = Functions.GetRegistryValue(Config.RegEntry, "CloseOnLaunch", "False");
            string CheckBoxReg = Functions.GetRegistryValue(Config.RegEntry, "DisableCheckBoxes", "False");

            // Try to translate the result for strip item options.
            Boolean.TryParse(LaunchClose, out Config.CloseOnLaunch);
            Boolean.TryParse(CheckBoxReg, out Config.DisableCheckBoxes);

            // Initialize the INI file.
            EngineINI.InitializeINI(Config.GameVersion);
        }
    }
}
