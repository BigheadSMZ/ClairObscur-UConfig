using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static void PromptReloadINI()
        {
            // Ask the user if they wish to reload the INI.
            string Title = "Reload Engine.ini Values?";
            string Message = "Do you wish to reload the \"Engine.ini\" file? Any changes to values since the last save will be lost!";
            bool Choice = Forms.YesNoDialog.Display(Title, Message, 250, 32, 32, 16, true);

            // If the user wants to reload it.
            if (Choice)
            {
                // Reload the INI and update the GUI.
                EngineINI.LoadINIValues();
                Forms.UpdateValues();
            }
        }

        public static void PromptCreateNewINI()
        {
            // Check if the file already exists.
            if (EngineINI.Path.TestPath())
            {
                // Ask the user if they wish to replace it.
                string Title = "Replace Engine.ini?";
                string Message = "The \"Engine.ini\" file already exists. Would you like to delete and replace it with a newly generated file?";
                bool Choice = Forms.YesNoDialog.Display(Title, Message, 250, 32, 28, 16, true);

                // If the user wants to delete it, do so. Otherwise take the exit.
                switch (Choice)
                {
                    case true: { EngineINI.DeleteINIFile(); break; }
                    case false: { return; }
                }
            }
            // All values will exist for a new INI file so uncheck everything.
            for (int i = 0; i < Forms.ToggleOptions.Length; i++)
            {
                EngineINI.NullTracker[i] = false;
                Forms.ToggleOptions[i].Checked = false;
            }
            // Write a new INI file and update the GUI.
            EngineINI.WriteNewINIFile();

            // If the dialog was created then update that too.
            if (Forms.MainDialog != null)
            {
                Forms.UpdateValues();
                Forms.ToggleGUI(true);
            }
        }

        public static void PromptDeleteINI()
        {
            // Ask the user if they wish to delete the INI.
            string Title = "Delete Engine.ini?";
            string Message = "Do you wish to delete the \"Engine.ini\" file? A new file must be created before you can configure options.";
            bool Choice = Forms.YesNoDialog.Display(Title, Message, 260, 32, 22, 16, true);

            // If the user wants to delete it.
            if (Choice)
            {
                // Delete the INI and disable the GUI.
                EngineINI.DeleteINIFile();
                Forms.ToggleGUI(false);
            }
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

        public static void PromptSaveINI()
        {
            // Let the user know their changes were saved.
            string Title = "Saved Engine.ini Changes";
            string Message = "Changes to \"Engine.ini\" have been saved.";
            Forms.OkayDialog.Display(Title, Message, 300, 16, 50, 24, 5);
            EngineINI.WriteINIValues();
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

    }
}
