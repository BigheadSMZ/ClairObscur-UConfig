﻿using System;
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
        public static bool    DisableToolTips;
        public static bool    SaveOnLaunch;
        public static bool    CloseOnLaunch;
        public static string  GameVersion;
        public static bool    ShowAdvanced;

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
            string LaunchSave  = Functions.GetRegistryValue(Config.RegEntry, "SaveOnLaunch", "False");
            string LaunchClose = Functions.GetRegistryValue(Config.RegEntry, "CloseOnLaunch", "False");
            string CheckBoxReg = Functions.GetRegistryValue(Config.RegEntry, "DisableCheckBoxes", "False");
            string ToolTipReg  = Functions.GetRegistryValue(Config.RegEntry, "DisableToolTips", "False");
            string ShowAdvReg  = Functions.GetRegistryValue(Config.RegEntry, "ShowAdvanced", "False");

            // Try to translate the result for strip item options.
            Boolean.TryParse(LaunchSave,  out Config.SaveOnLaunch);
            Boolean.TryParse(LaunchClose, out Config.CloseOnLaunch);
            Boolean.TryParse(CheckBoxReg, out Config.DisableCheckBoxes);
            Boolean.TryParse(ToolTipReg,  out Config.DisableToolTips);
            Boolean.TryParse(ShowAdvReg,  out Config.ShowAdvanced);
        }
    }
}
