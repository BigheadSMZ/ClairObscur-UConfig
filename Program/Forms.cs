using System;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ClairObscurConfig
{
    internal partial class Forms
    {
        // The main dialog.
        public static Form_MainForm  MainDialog;
        public static Form_OkayForm  OkayDialog;
        public static Form_YesNoForm YesNoDialog;

        // Checkbox array is simple. Textbox will also contain numeric up/down
        // which complicates things. But I want them all to be in a single array.
        public static CheckBox[]     ChkBoxArray;

        // The number of rows to add to the datagridview. I don't think we'll need beyond 1000.
        public static int MaxRows = 1000;

        // Temporarily restricts "CellValueChanged" event from firing.
        public static bool CellEventBlock = false;

        public static void Initialize()
        {
            // Create the dialogs.
            Forms.MainDialog  = new Form_MainForm();
            Forms.OkayDialog  = new Form_OkayForm();
            Forms.YesNoDialog = new Form_YesNoForm();

            // Set the title of the window to include the current version.
            Forms.MainDialog.Text = Game.Name + " - Unreal Config v" + Config.AppVersion;

            // Check to see if the game executable is in the same path as the configurator.
            bool ExeExists = Config.GamePath.TestPath();

            // Toggle the game options accordingly.
            Forms.MainDialog.StripItem_LaunchGame.Enabled = ExeExists;
            Forms.MainDialog.StripItem_CloseGame.Enabled  = ExeExists;

            // Set the check state for "Options" toolstrip items.
            Forms.MainDialog.StripItem_HideCheckBox.Checked = Config.DisableCheckBoxes;
            Forms.MainDialog.StripItem_LaunchSave.Checked   = Config.SaveOnLaunch;
            Forms.MainDialog.StripItem_LaunchClose.Checked  = Config.CloseOnLaunch;
            Forms.MainDialog.StripItem_NoToolTips.Checked   = Config.DisableToolTips;

            // Set the INI backup path.
            string BackupPath = EngineINI.Path + ".bak";

            // Set availability of backup options depending on INI existence.
            Forms.MainDialog.StripItem_CreateBackup.Enabled  = EngineINI.Path.TestPath();
            Forms.MainDialog.StripItem_RestoreBackup.Enabled = BackupPath.TestPath();

            // Create array to reference the checkboxes.
            Forms.ChkBoxArray = new CheckBox[18];
            Forms.ChkBoxArray[0]  = Forms.MainDialog.CheckBox_AF;
            Forms.ChkBoxArray[1]  = Forms.MainDialog.CheckBox_DoF;
            Forms.ChkBoxArray[2]  = Forms.MainDialog.CheckBox_Bloom;
            Forms.ChkBoxArray[3]  = Forms.MainDialog.CheckBox_MBlur;
            Forms.ChkBoxArray[4]  = Forms.MainDialog.CheckBox_LensFlare;
            Forms.ChkBoxArray[5]  = Forms.MainDialog.CheckBox_Fog;
            Forms.ChkBoxArray[6]  = Forms.MainDialog.CheckBox_VFog;
            Forms.ChkBoxArray[7]  = Forms.MainDialog.CheckBox_ChromAb;
            Forms.ChkBoxArray[8]  = Forms.MainDialog.CheckBox_Distort;
            Forms.ChkBoxArray[9]  = Forms.MainDialog.CheckBox_FilmGrain;
            Forms.ChkBoxArray[10] = Forms.MainDialog.CheckBox_ShadQual;
            Forms.ChkBoxArray[11] = Forms.MainDialog.CheckBox_ShadRes;
            Forms.ChkBoxArray[12] = Forms.MainDialog.CheckBox_Tonemap;
            Forms.ChkBoxArray[13] = Forms.MainDialog.CheckBox_GrainQuant;
            Forms.ChkBoxArray[14] = Forms.MainDialog.CheckBox_Sharpen;
            Forms.ChkBoxArray[15] = Forms.MainDialog.CheckBox_ViewDist;
            Forms.ChkBoxArray[16] = Forms.MainDialog.CheckBox_ShadDist;
            Forms.ChkBoxArray[17] = Forms.MainDialog.CheckBox_FolDist;

            // Set the enabled state of the GUI depending if the INI exists.
            Forms.ToggleGUI(EngineINI.Path.TestPath());
        }

        public static void PopulateGridView()
        {
            // Prevent the CellValueChanged event from firing.
            Forms.CellEventBlock = true;

            // Clear the rows.
            Forms.MainDialog.GridView_Options.Rows.Clear();

            // Default to 100 rows. It's doubtful anyone will need more than that.
            Forms.MainDialog.GridView_Options.RowCount = Forms.MaxRows;

            // Loop through the additional entries.
            for (int i = 0; i < EngineINI.MoreEntries.Count; i++)
            {
                // Get the name of the key.
                string KeyName = EngineINI.MoreEntries.Cast<DictionaryEntry>().ElementAt(i).Key.ToString();

                // Add the key and the value to the datagridview.
                Forms.MainDialog.GridView_Options[0, i].Value = KeyName;
                Forms.MainDialog.GridView_Options[1, i].Value = EngineINI.MoreEntries[KeyName];
            }
            // Restore allowing the event to fire.
            Forms.CellEventBlock = false;
        }

        public static void UpdateValues()
        {
            // Do one more validation so the GUI doesn't crash with invalid values.
            EngineINI.ValidateValues();

            // Update the dialog with the loaded values that need translated. 
            Forms.MainDialog.Combo_AF.SelectedItem         = ValueSwap.Translate_AA(EngineINI.MainEntries[0].ToString());
            Forms.MainDialog.Combo_DoF.SelectedItem        = ValueSwap.Translate_Scale(EngineINI.MainEntries[1].ToString());
            Forms.MainDialog.Combo_Bloom.SelectedItem      = ValueSwap.Translate_Scale(EngineINI.MainEntries[2].ToString());
            Forms.MainDialog.Combo_MBlur.SelectedItem      = ValueSwap.Translate_Scale(EngineINI.MainEntries[3].ToString());
            Forms.MainDialog.Combo_LensFlare.SelectedItem  = ValueSwap.Translate_Scale(EngineINI.MainEntries[4].ToString());
            Forms.MainDialog.Combo_Fog.SelectedItem        = ValueSwap.Translate_Binary(EngineINI.MainEntries[5].ToString());
            Forms.MainDialog.Combo_VFog.SelectedItem       = ValueSwap.Translate_Binary(EngineINI.MainEntries[6].ToString());
            Forms.MainDialog.Combo_ChromAb.SelectedItem    = ValueSwap.Translate_Binary(EngineINI.MainEntries[7].ToString());
            Forms.MainDialog.Combo_Distort.SelectedItem    = ValueSwap.Translate_Binary(EngineINI.MainEntries[8].ToString());
            Forms.MainDialog.Combo_FilmGrain.SelectedItem  = ValueSwap.Translate_Binary(EngineINI.MainEntries[9].ToString());
            Forms.MainDialog.Combo_ShadQual.SelectedItem   = ValueSwap.Translate_ShadQual(EngineINI.MainEntries[10].ToString());
            Forms.MainDialog.Combo_ShadRes.SelectedItem    = ValueSwap.Translate_ShadRes(EngineINI.MainEntries[11].ToString());
            Forms.MainDialog.Combo_Tonemap.SelectedItem    = EngineINI.MainEntries[12].ToString();
            Forms.MainDialog.Combo_GrainQuant.SelectedItem = ValueSwap.Translate_Binary(EngineINI.MainEntries[13].ToString());

            // The remaining ones can simply be converted from string to decimal.
            Forms.MainDialog.Num_Sharpen.Value  = Functions.FormatStringDecimal(EngineINI.MainEntries[14].ToString());
            Forms.MainDialog.Num_ViewDist.Value = Functions.FormatStringDecimal(EngineINI.MainEntries[15].ToString());
            Forms.MainDialog.Num_ShadDist.Value = Functions.FormatStringDecimal(EngineINI.MainEntries[16].ToString());
            Forms.MainDialog.Num_FolDist.Value  = Functions.FormatStringDecimal(EngineINI.MainEntries[17].ToString());

            // See if checkboxes are enabled.
            if (!Config.DisableCheckBoxes)
            {
                // Loop through the array of checkboxes.
                for (int i = 0; i < Forms.ChkBoxArray.Length; i++)
                {
                    // Toggle options based on whether or not values were found in the INI file.
                    Forms.ChkBoxArray[i].Checked = EngineINI.EmptyValue[i];
                }
            }
            // Populate the DataGridView with options not found on the main GUI.
            Forms.PopulateGridView();
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
            Forms.MainDialog.StripItem_EditINI.Enabled     = Enabled;
            Forms.MainDialog.StripItem_ExitSave.Enabled    = Enabled;
            Forms.MainDialog.StripItem_PresetLow.Enabled   = Enabled;
            Forms.MainDialog.StripItem_PresetMed.Enabled   = Enabled;
            Forms.MainDialog.StripItem_PresetHigh.Enabled  = Enabled;
            Forms.MainDialog.StripItem_PresetUltra.Enabled = Enabled;
            Forms.MainDialog.StripItem_SharpClear.Enabled  = Enabled;
            Forms.MainDialog.StripItem_SoftAmb.Enabled     = Enabled;
            Forms.MainDialog.GroupBox_Main.Enabled         = Enabled;
            Forms.MainDialog.GroupBox_Advanced.Enabled     = Enabled;
            Forms.MainDialog.Button_Save.Enabled           = Enabled;
            Forms.MainDialog.Button_Reload.Enabled         = Enabled;

            // If there is no INI file then remove "Save" from the exit button.
            switch (Enabled)
            {
                case true:  { Forms.MainDialog.Button_ExitSave.Text = "Save / Exit"; break; }
                case false: { Forms.MainDialog.Button_ExitSave.Text = "Exit"; break; }
            }
        }

        public static void ToggleCheckBoxes(bool ToggleState)
        {
            // Track through a variable to make it simpler.
            Config.DisableCheckBoxes = ToggleState;

            // The amount to nudge the controls.
            int Nudge = 14;

            // Toggle the visibility of checkboxes.
            foreach (CheckBox LoopBox in Forms.ChkBoxArray)
            {
                // We could loop by type but we already have an array of them.
                LoopBox.Visible = !ToggleState;

                // Negative state is hiding checkboxes. Force them unchecked.
                if (ToggleState) { LoopBox.Checked = false; }
            }
            // Invert it depending on the state.
            if (ToggleState) { Nudge = -14; }

            // Move the controls based on the nudge value.
            foreach (Control ComboLabelNumeric in Forms.MainDialog.GroupBox_Main.Controls)
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
            // Store the decision in the registry.
            Functions.SetRegistryValue(Config.RegEntry, "DisableCheckBoxes", (ToggleState).ToString());

            // Update the values on the GUI.
            Forms.UpdateValues();
        }

        public static void ToggleLaunchSave(bool ToggleState)
        {
            // Store the decision in the registry.
            Functions.SetRegistryValue(Config.RegEntry, "SaveOnLaunch", (ToggleState).ToString());
        }

        public static void ToggleLaunchClose(bool ToggleState)
        {
            // Store the decision in the registry.
            Functions.SetRegistryValue(Config.RegEntry, "CloseOnLaunch", (ToggleState).ToString());
        }

        public static void ToggleGlobalToolTips(bool ToggleState)
        {
            // Store the decision in the registry.
            Functions.SetRegistryValue(Config.RegEntry, "DisableToolTips", (ToggleState).ToString());

            // Enable or disable the global tooltip.
            switch (ToggleState)
            {
                case true:  { Forms.MainDialog.GlobalToolTip.Active = false; break; }
                case false: { Forms.MainDialog.GlobalToolTip.Active = true; break; }
            }
        }

        public static void ClearCheckBoxes()
        {
            // Loop through the number of checkboxes.
            for (int i = 0; i < Forms.ChkBoxArray.Length; i++)
            {
                // Reset the tracker and the checkbox states.
                EngineINI.EmptyValue[i] = false;
                Forms.ChkBoxArray[i].Checked = false;
            }
        }

        public static void DataGridView_ParseTextBoxText()
        {
            // Prevent the CellValueChanged event from also firing.
            Forms.CellEventBlock = true;

            // Grab all the lines from the rich textbox.
            string[] Lines = Forms.MainDialog.RichTextBox_AddOptions.Lines;

            // Some parsed data may alter main entries which means additional stuff needs to be done.
            bool MainEntriesUpdated = false;

            // Loop through the lines.
            for (int i = 0; i < Lines.Length; i++)
            {
                // Count how many times an equals (=) appears.
                int EqualsCount = Lines[i].Count(equals => equals == '=');

                // Only allow a single equals sign and zero comments.
                if (EqualsCount != 1 | Lines[i].Contains(';') | Lines[i].Contains('#')) { continue; }

                // There should be an equals sign so split on that and limit to two results.
                string[] SplitLine = Lines[i].Split(new char[] { '=' }, 2);

                // The most basic validation possible: are both values not null.
                if ((SplitLine[0] != null) & (SplitLine[1] != null) && (SplitLine[0] != "") & (SplitLine[1] != ""))
                {
                    // Remove any whitespace surrounding the strings.
                    string Key   = SplitLine[0].Trim();
                    string Value = SplitLine[1].Trim();

                    // If it's one of the options already covered by the main dialog.
                    if (EngineINI.MainEntries.Contains(Key))
                    {
                        // For now blindly set whatever the new value is.
                        EngineINI.MainEntries[Key] = Value;

                        // Remember that we need to do more stuff later.
                        MainEntriesUpdated = true;
                    }
                    else
                    {
                        // If the key exists, update the value. If it doesn't exist, then add it to the hashtable.
                        switch (EngineINI.MoreEntries.Contains(Key))
                        {
                            case true:  { EngineINI.MoreEntries[Key] = Value; break; }
                            case false: { EngineINI.MoreEntries.Add(Key, Value); break; }
                        }
                    }
                }
            }
            // Reset the text.
            Forms.MainDialog.RichTextBox_AddOptions.Text = "";

            // Update the data grid view.
            Forms.PopulateGridView();

            // It's time to take care of those main entries that were changed.
            if (MainEntriesUpdated)
            {
                // Update the form with new values which will also validate/fix invalid values.
                Forms.UpdateValues();
            }
            // Restore allowing the event to fire.
            Forms.CellEventBlock = false;
        }

        public static void DataGridView_DeleteEntry(Keys PressedKey)
        {
            // Prevent the CellValueChanged event from firing.
            Forms.CellEventBlock = true;

            // The delete key was pressed.
            if (PressedKey == Keys.Delete)
            {
                // Get the cell that is currently selected.
                DataGridViewCell Cell = Forms.MainDialog.GridView_Options.SelectedCells[0];

                // Get the row and column of the cell.
                int Row = Cell.RowIndex;
                int Col = Cell.ColumnIndex;

                // If its the variable name then wipe out both columns.
                if (Col == 0)
                {
                    // Clear out the entire row.
                    Forms.MainDialog.GridView_Options[0, Row].Value = null;
                    Forms.MainDialog.GridView_Options[1, Row].Value = null;

                    // Update both the hashtable and resort the datagridview.
                    EngineINI.RebuildAdvancedOptions();
                    Forms.PopulateGridView();
                }
                // If it's just the value then wipe only that out.
                else if (Col == 1)
                {
                    // Clear out only the value.
                    Forms.MainDialog.GridView_Options[1, Row].Value = null;
                }
            }
            // Restore allowing the event to fire.
            Forms.CellEventBlock = false;
        }

        public static void DataGridView_ValueChanged(int RowIndex)
        {
            // Checked at the end to see if the hashtable and datagridview should be updated.
            bool PerformUpdate = false;

            // This ended up being a very problematic method because the event fires whenever the cell is changed for any
            // reason. Unfortunately I don't think it's possible to detect whether a cells value was edited programatically
            // or by the user. The quick and dirty fix is to just use a boolean to know when the event is allowed to fire.
            // I think headers can trigger a negative value so prevent that as well by checking if its less than 0.
            if (Forms.CellEventBlock | RowIndex < 0) { return; }

            // See if the user cleared the entire line (both key and value).
            if (Forms.MainDialog.GridView_Options[0, RowIndex].Value == null & Forms.MainDialog.GridView_Options[1, RowIndex].Value == null)
            {
                // Peform an update on the hashtable and datagridview.
                PerformUpdate = true;
            }
            // See if both columns on the same row where either was edited both contain data.
            else if (Forms.MainDialog.GridView_Options[0, RowIndex].Value != null & Forms.MainDialog.GridView_Options[1, RowIndex].Value != null)
            {
                // Get the current entry in the first column.
                string Key = Forms.MainDialog.GridView_Options[0, RowIndex].Value.ToString();

                // Check to see if the hashtable already contains this key.
                if (EngineINI.MoreEntries.Contains(Key))
                {
                    // Reset the cell with empty text.
                    Forms.MainDialog.GridView_Options[0, RowIndex].Value = "";

                    // Show the user an error message to let them know what's going on.
                    Forms.PromptEntryExists();
                }
                // If the hashtable does not already have this key then add it.
                else
                {
                    // Peform an update on the hashtable and datagridview.
                    PerformUpdate = true;
                }
            }
            // If one of the paths triggered an update.
            if (PerformUpdate)
            {
                // Update both the hashtable and datagridview which will sort it.
                EngineINI.RebuildAdvancedOptions();
                Forms.PopulateGridView();
            }
        }
        public static void DataGridView_ClearAll()
        {
            // Clear the rows.
            Forms.MainDialog.GridView_Options.Rows.Clear();

            // Default to 100 rows. It's doubtful anyone will need more than that.
            Forms.MainDialog.GridView_Options.RowCount = Forms.MaxRows;

            // Clear all items out of the hashtable.
            EngineINI.MoreEntries.Clear();
        }
    }
}
