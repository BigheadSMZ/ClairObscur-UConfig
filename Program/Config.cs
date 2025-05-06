using System;
using System.IO;
using System.Reflection;

namespace ClairObscurConfig
{
    internal class Config
    {
        // Values used across the application.
        public static string  AppName;
        public static string  AppPath;
        public static string  AppData;
        public static string  AppVersion;
        public static string  BasePath;
        public static string  GamePath;
        public static bool    ChkBoxes;

        public static void SetApplicationValues()
        {
            // Get the folder this app is in.
            Config.AppPath  = Assembly.GetExecutingAssembly().Location;
            Config.AppName  = Config.AppPath.GetFileName();
            Config.AppData  = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            Config.BasePath = Path.GetDirectoryName(Config.AppPath);
            Config.GamePath = Game.GetExecutable();
            Config.ChkBoxes = true;

            // Initialize the INI file.
            EngineINI.InitializeINI();

            // We might need these earlier than I planned.
            Forms.OkayDialog  = new Form_OkayForm();
            Forms.YesNoDialog = new Form_YesNoForm();

            // If the INI exists, load the values. If it doesn't exist alert the user.
            switch (EngineINI.Path.TestPath())
            {
                case true : { EngineINI.LoadINIValues(); break; }
                case false: { Forms.PromptININotExist(); break; }
            }
        }
    }
}
