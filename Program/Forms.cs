using System.Diagnostics;
using System.Windows.Forms;

namespace ClairObscurConfig
{
    internal class Forms
    {
        // The main dialog.
        public static Form_MainForm  MainDialog;
        public static Form_OkayForm  OkayDialog;
        public static Form_YesNoForm YesNoDialog;
        public static CheckBox[]     ToggleOptions;

        public static void Initialize()
        {
            // Create the dialogs.
            Forms.MainDialog = new Form_MainForm();

            // Set the title of the window to include the current version.
            Forms.MainDialog.Text = Game.Name + " - Unreal Config v" + Config.AppVersion;

            // Select the radio button based on INI type.
            switch (EngineINI.Type)
            {
                case "Steam" :   { Forms.MainDialog.Radio_Steam.Checked = true; break; }
                case "GamePass": { Forms.MainDialog.Radio_GamePass.Checked = true; break; }
            }
            // Check to see if the game executable is in the same path as the configurator.
            bool ExeExists = Config.GamePath.TestPath();

            // Toggle the game options accordingly.
            Forms.MainDialog.StripItem_LaunchGame.Enabled = ExeExists;
            Forms.MainDialog.StripItem_CloseGame.Enabled  = ExeExists;

            // Set the INI backup path.
            string BackupPath = EngineINI.Path + ".bak";

            // Set availability of backup options depending on INI existence.
            Forms.MainDialog.StripItem_CreateBackup.Enabled = EngineINI.Path.TestPath();
            Forms.MainDialog.StripItem_RestoreBackup.Enabled = BackupPath.TestPath();

            // Create a checkbox array so it's easier to loop through them.
            Forms.ToggleOptions = new CheckBox[18];
            Forms.ToggleOptions[0]  = Forms.MainDialog.CheckBox_AF;
            Forms.ToggleOptions[1]  = Forms.MainDialog.CheckBox_DoF;
            Forms.ToggleOptions[2]  = Forms.MainDialog.CheckBox_Bloom;
            Forms.ToggleOptions[3]  = Forms.MainDialog.CheckBox_MBlur;
            Forms.ToggleOptions[4]  = Forms.MainDialog.CheckBox_LensFlare;
            Forms.ToggleOptions[5]  = Forms.MainDialog.CheckBox_Fog;
            Forms.ToggleOptions[6]  = Forms.MainDialog.CheckBox_VFog;
            Forms.ToggleOptions[7]  = Forms.MainDialog.CheckBox_ChromAb;
            Forms.ToggleOptions[8]  = Forms.MainDialog.CheckBox_Distort;
            Forms.ToggleOptions[9]  = Forms.MainDialog.CheckBox_FilmGrain;
            Forms.ToggleOptions[10] = Forms.MainDialog.CheckBox_ShadQual;
            Forms.ToggleOptions[11] = Forms.MainDialog.CheckBox_ShadRes;
            Forms.ToggleOptions[12] = Forms.MainDialog.CheckBox_Tonemap;
            Forms.ToggleOptions[13] = Forms.MainDialog.CheckBox_GrainQuant;
            Forms.ToggleOptions[14] = Forms.MainDialog.CheckBox_Sharpen;
            Forms.ToggleOptions[15] = Forms.MainDialog.CheckBox_ViewDist;
            Forms.ToggleOptions[16] = Forms.MainDialog.CheckBox_ShadDist;
            Forms.ToggleOptions[17] = Forms.MainDialog.CheckBox_FolDist;

            // Make sure the INI exists before attempting to update values.
            switch (EngineINI.Path.TestPath())
            {
                case true:  { Forms.UpdateValues();   break; }
                case false: { Forms.ToggleGUI(false); break; }
            }
        }

        public static void UpdateValues()
        {
            // Do one more validation so the GUI doesn't crash with invalid values.
            EngineINI.ValidateValues();

            // Update the dialog with the loaded values that need translated. 
            Forms.MainDialog.Combo_AF.SelectedItem         = ValueSwap.Translate_AA(EngineINI.Anist_Val);
            Forms.MainDialog.Combo_DoF.SelectedItem        = ValueSwap.Translate_Scale(EngineINI.Depth_Val);
            Forms.MainDialog.Combo_Bloom.SelectedItem      = ValueSwap.Translate_Scale(EngineINI.Bloom_Val);
            Forms.MainDialog.Combo_MBlur.SelectedItem      = ValueSwap.Translate_Scale(EngineINI.MBlur_Val);
            Forms.MainDialog.Combo_LensFlare.SelectedItem  = ValueSwap.Translate_Scale(EngineINI.LenFl_Val);
            Forms.MainDialog.Combo_Fog.SelectedItem        = ValueSwap.Translate_Binary(EngineINI.FogEf_Val);
            Forms.MainDialog.Combo_VFog.SelectedItem       = ValueSwap.Translate_Binary(EngineINI.VoFog_Val);
            Forms.MainDialog.Combo_ChromAb.SelectedItem    = ValueSwap.Translate_Binary(EngineINI.SCoFr_Val);
            Forms.MainDialog.Combo_Distort.SelectedItem    = ValueSwap.Translate_Binary(EngineINI.Distr_Val);
            Forms.MainDialog.Combo_FilmGrain.SelectedItem  = ValueSwap.Translate_Binary(EngineINI.Grain_Val);
            Forms.MainDialog.Combo_ShadQual.SelectedItem   = ValueSwap.Translate_ShadQual(EngineINI.ShadQ_Val);
            Forms.MainDialog.Combo_ShadRes.SelectedItem    = ValueSwap.Translate_ShadRes(EngineINI.ShadR_Val);
            Forms.MainDialog.Combo_Tonemap.SelectedItem    = EngineINI.TMQua_Val;
            Forms.MainDialog.Combo_GrainQuant.SelectedItem = ValueSwap.Translate_Binary(EngineINI.TMGra_Val);

            // The remaining ones can simply be converted from string to decimal.
            Forms.MainDialog.Num_Sharpen.Value  = Functions.FormatStringDecimal(EngineINI.TMSha_Val);
            Forms.MainDialog.Num_ViewDist.Value = Functions.FormatStringDecimal(EngineINI.ViewD_Val);
            Forms.MainDialog.Num_ShadDist.Value = Functions.FormatStringDecimal(EngineINI.ViewS_Val);
            Forms.MainDialog.Num_FolDist.Value  = Functions.FormatStringDecimal(EngineINI.ViewF_Val);

            // Toggle options based on whether or not values were found in the INI file.
            for (int i = 0; i < Forms.ToggleOptions.Length; i++)
            {
                 Forms.ToggleOptions[i].Checked = EngineINI.NullTracker[i];
            }
        }

        public static void ToggleGUI(bool Enabled)
        {
            // Set the INI backup path.
            string BackupPath = EngineINI.Path + ".bak";

            // Set availability of backup options depending on INI existence.
            Forms.MainDialog.StripItem_CreateBackup.Enabled = EngineINI.Path.TestPath();
            Forms.MainDialog.StripItem_RestoreBackup.Enabled = BackupPath.TestPath();

            // Disable the options that can throw exceptions if there is no INI file.
            Forms.MainDialog.GroupBox_Main.Enabled         = Enabled;
            Forms.MainDialog.StripItem_SaveINI.Enabled     = Enabled;
            Forms.MainDialog.StripItem_ReloadINI.Enabled   = Enabled;
            Forms.MainDialog.StripItem_DeleteINI.Enabled   = Enabled;
            Forms.MainDialog.StripItem_ExitSave.Enabled    = Enabled;
            Forms.MainDialog.StripItem_PresetLow.Enabled   = Enabled;
            Forms.MainDialog.StripItem_PresetMed.Enabled   = Enabled;
            Forms.MainDialog.StripItem_PresetHigh.Enabled  = Enabled;
            Forms.MainDialog.StripItem_PresetUltra.Enabled = Enabled;
            Forms.MainDialog.StripItem_SharpClear.Enabled  = Enabled;
            Forms.MainDialog.StripItem_SoftAmb.Enabled     = Enabled;
            Forms.MainDialog.Button_Save.Enabled           = Enabled;
            Forms.MainDialog.Button_Reload.Enabled         = Enabled;

            // If there is no INI file then remove "Save" from the exit button.
            switch (Enabled)
            {
                case true:  { Forms.MainDialog.Button_ExitSave.Text = "Save / Exit"; break; }
                case false: { Forms.MainDialog.Button_ExitSave.Text = "Exit"; break; }
            }
        }

        public static void OpenFileExplorer(string Location)
        {
            Process FileExplorer = new Process();
            FileExplorer.StartInfo.FileName = Location;
            FileExplorer.StartInfo.UseShellExecute = true;
            FileExplorer.StartInfo.Verb = "open";
            FileExplorer.Start();
        }

        public static void PromptININotExist()
        {
            // We simply need to let the user know the INI was created.
            string Title   = "Missing Engine.ini";
            string Message = "The \"Engine.ini\" file was not found. Generate this file using \"File >> Create Engine.ini\" from the menu bar.";
            Forms.OkayDialog.Display(Title, Message, 260, 32, 24, 16, 8);
        }

        public static void PromptReloadINI()
        {
            // Ask the user if they wish to reload the INI.
            string Title   = "Reload Engine.ini Values?";
            string Message = "Do you wish to reload the \"Engine.ini\" file? Any changes to values since the last save will be lost!";
            bool Choice    = Forms.YesNoDialog.Display(Title, Message, 250, 32, 32, 16, true);

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
                string Title   = "Replace Engine.ini?";
                string Message = "The \"Engine.ini\" file already exists. Would you like to delete and replace it with a newly generated file?";
                bool Choice    = Forms.YesNoDialog.Display(Title, Message, 250, 32, 28, 16, true);

                // If the user wants to delete it, do so. Otherwise take the exit.
                switch (Choice)
                {
                    case true: { EngineINI.DeleteINIFile(); break; }
                    case false: { return; }
                }
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
            string Title   = "Delete Engine.ini?";
            string Message = "Do you wish to delete the \"Engine.ini\" file? A new file must be created before you can configure options.";
            bool Choice    = Forms.YesNoDialog.Display(Title, Message, 260, 32, 22, 16, true);

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
            string Title   = "Backup Engine.ini?";
            string Message = "Do you wish to back up the \"Engine.ini\" file? This will copy the current INI and append a \".bak\" extension.";
            return Forms.YesNoDialog.Display(Title, Message, 260, 32, 22, 16, true);
        }
        
        public static bool PromptRestoreINI()
        {
            // Ask the user if they wish to backup the INI.
            string Title   = "Restore Engine.ini?";
            string Message = "Do you wish to restore the \"Engine.ini\" from backup? This will overwrite your current INI file settings.";
            return Forms.YesNoDialog.Display(Title, Message, 260, 32, 22, 16, true);
        }

        public static void PromptSaveINI()
        {
            // Let the user know their changes were saved.
            string Title   = "Saved Engine.ini Changes";
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
