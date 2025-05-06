namespace ClairObscurConfig
{
    internal partial class Forms
    {
        public static void PromptININotExist()
        {
            // We simply need to let the user know the INI was created.
            string Title = "Missing Engine.ini";
            string Message = "The \"Engine.ini\" file was not found. Generate this file using \"File >> Create Engine.ini\" from the menu bar.";
            Forms.OkayDialog.Display(Title, Message, 260, 32, 24, 16, 8);
        }

        public static void PromptSaveINI()
        {
            // Let the user know their changes were saved.
            string Title = "Saved Engine.ini Changes";
            string Message = "Changes to \"Engine.ini\" have been saved.";
            Forms.OkayDialog.Display(Title, Message, 300, 16, 50, 24, 5);
        }

        public static void PromptGameNotFound()
        {
            // Let the user know their changes were saved.
            string Title = "Game Not Found";
            string Message = "Game executable not found. To launch the game with this configurator, place it alongside \"Expedition33_Steam.exe\", \"Sandfall.exe\", or \"SandFall-Win64-Shipping.exe\".";
            Forms.OkayDialog.Display(Title, Message, 290, 44, 10, 10, 10);
        }

        public static void PromptNoGameProcess()
        {
            // Let the user know their changes were saved.
            string Title = "Game PID Not Found";
            string Message = "Process \"SandFall-Win64-Shipping.exe\" is not running.";
            Forms.OkayDialog.Display(Title, Message, 270, 32, 24, 24, 10);
        }

        public static void PromptAbout()
        {
            // Let the user know their changes were saved.
            string Title = "About";
            string Message = Game.Name + " - Unreal Config v" + Config.AppVersion + "\r\nCreated by: Bighead - bighead.0@gmail.com";
            Forms.OkayDialog.Display(Title, Message, 280, 32, 30, 20, 10);
        }

        public static bool PromptReloadINI()
        {
            // Ask the user if they wish to reload the INI.
            string Title = "Reload Engine.ini Values?";
            string Message = "Do you wish to reload the \"Engine.ini\" file? Any changes to values since the last save will be lost!";
            return Forms.YesNoDialog.Display(Title, Message, 250, 32, 32, 16, true);
        }

        public static bool PromptOverwriteINI()
        {
            // Ask the user if they wish to replace it.
            string Title = "Replace Engine.ini?";
            string Message = "The \"Engine.ini\" file already exists. Would you like to delete and replace it with a newly generated file?";
            return Forms.YesNoDialog.Display(Title, Message, 250, 32, 28, 16, true);
        }

        public static bool PromptDeleteINI()
        {
            // Ask the user if they wish to delete the INI.
            string Title = "Delete Engine.ini?";
            string Message = "Do you wish to delete the \"Engine.ini\" file? A new file must be created before you can configure options.";
            return Forms.YesNoDialog.Display(Title, Message, 260, 32, 22, 16, true);
        }

        public static bool PromptBackupINI()
        {
            // Ask the user if they wish to backup the INI.
            string Title = "Backup Engine.ini?";
            string Message = "Do you wish to back up the \"Engine.ini\" file? This will copy the current INI and append a \".bak\" extension.";
            return Forms.YesNoDialog.Display(Title, Message, 260, 32, 22, 16, true);
        }

        public static bool PromptRestoreINI()
        {
            // Ask the user if they wish to backup the INI.
            string Title = "Restore Engine.ini?";
            string Message = "Do you wish to restore the \"Engine.ini\" from backup? This will overwrite your current INI file settings.";
            return Forms.YesNoDialog.Display(Title, Message, 260, 32, 22, 16, true);
        }
    }
}
