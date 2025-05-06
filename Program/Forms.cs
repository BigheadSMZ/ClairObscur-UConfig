using System;
using System.Drawing;
using System.Windows.Forms;

namespace ClairObscurConfig
{
    internal partial class Forms
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
        public static void ToggleCheckBoxes(bool State)
        {
            // The amount to nudge the controls.
            int Nudge = 14;

            // Let's use this to save some text. My fingers are tired.
            Form_MainForm MainForm = Forms.MainDialog;

            // Toggle the visibility of checkboxes.
            foreach (CheckBox LoopBox in ToggleOptions)
            {
                // We could loop by type but we already have an array of them.
                LoopBox.Visible = State;
            }
            // Invert it depending on the state.
            if (!State) { Nudge = -14; }

            // Move the controls based on the nudge value.
            foreach (Control ComboLabelNumeric in MainForm.GroupBox_Main.Controls)
            {
                // Get the type of the control.
                Type ControlType = ComboLabelNumeric.GetType();

                // Check the type against the three controls we want to move.
                if (ControlType == typeof(ComboBox) || ControlType == typeof(Label) || ControlType == typeof(NumericUpDown))
                {
                    // If it matches, move the control.
                    ComboLabelNumeric.Location = new Point(ComboLabelNumeric.Location.X + Nudge, ComboLabelNumeric.Location.Y);
                }
            }
        }
        public static void ClearCheckBoxes()
        {
            // Loop through the number of checkboxes.
            for (int i = 0; i < Forms.ToggleOptions.Length; i++)
            {
                // Reset the tracker and the checkbox states.
                EngineINI.NullTracker[i] = false;
                Forms.ToggleOptions[i].Checked = false;
            }
        }
    }
}
