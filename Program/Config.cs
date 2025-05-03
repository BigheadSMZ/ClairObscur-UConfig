using System;
using System.IO;
using System.Reflection;

namespace ClairObscurConfig
{
    internal class Config
    {
        // Values used across the application.
        public static string  AppPath;
        public static string  AppData;
        public static string  AppVersion;
        public static string  BasePath;
        public static string  GamePath;
        public static string  INIPath;
        public static IniFile INIFile;

        public static void SetApplicationValues()
        {
            // Get the folder this app is in.
            Config.AppPath  = Assembly.GetExecutingAssembly().Location;
            Config.AppData  = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            Config.BasePath = Path.GetDirectoryName(Config.AppPath);
            Config.INIPath  = Config.AppData + "\\Sandfall\\Saved\\Config\\Windows\\Engine.ini";
            Config.INIFile  = new IniFile(Config.INIPath);
            Config.GamePath = Game.GetExecutable();

            // We might need these earlier than I planned.
            Forms.OkayDialog  = new Form_OkayForm();
            Forms.YesNoDialog = new Form_YesNoForm();

            // If the INI exists, load the values. If it doesn't exist alert the user.
            switch (Config.INIPath.TestPath())
            {
                case true : { EngineINI.LoadINIValues(); break; }
                case false: { Forms.PromptNoINIBoot();   break; }
            }
        }
    }
}
