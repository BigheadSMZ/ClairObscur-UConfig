using System;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace ClairObscurConfig
{
    public partial class Form_MainForm : Form
    {
        // Set "true" when Control key is held to enable extra functions.
        private bool ControlHeld = false;

        // Tracks the click of the rich textbox.
        private bool ClickClear = false;      

        public Form_MainForm()
        {
            InitializeComponent();
        }
        private void Form_MainForm_Load(object sender, EventArgs e)
        {
            // If the option to hide checkboxes was pulled from the registry, call the function to toggle them. This can't be done
            // until the form is visible, so no better time to apply the changes to the controls than when it's first shown.
            if (Config.DisableCheckBoxes)
            {
                Forms.ToggleCheckBoxes(Config.DisableCheckBoxes);
            }
            // If the user disabled tooltips, now is a decent time to apply it.
            if (Config.DisableToolTips)
            {
                Forms.MainDialog.GlobalToolTip.Active = false;
            }
            // Select the game version radio button based on stored user value. This will also perform the initial INI load and update the GUI.
            switch (Config.GameVersion)
            {
                case "Steam":    { this.Radio_Steam.Checked    = true; break; }
                case "GamePass": { this.Radio_GamePass.Checked = true; break; }
            }
            // Select the advanced options radio button based on stored user value. This will also set the initial GUI size.
            switch (Config.ShowAdvanced)
            {
                case true:  { this.Radio_AdvShow.Checked = true; break; }
                case false: { this.Radio_AdvHide.Checked = true; break; }
            }
            // This is the one safe place to call the center to screen function. We need to resize after the dialog has
            // been created but before it is shown. This should give the correct results no matter the users settings.
            this.CenterToScreen();
        }
        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        //   Main Dialog - Game Type GroupBox
        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        private void Radio_CheckedChanged(bool Checked, string Version, string INIPath)
        {
            // Cancel if it's not checked.
            if (!Checked) { return; }

            // The path needs to be created if it doesn't exist.
            INIPath.CreatePath(true);

            // Update the INI properties.
            EngineINI.Path = INIPath + "\\Engine.ini";
            EngineINI.Class = new IniFile(EngineINI.Path);

            // Try to load and toggle the GUI based on existence.
            switch (EngineINI.Path.TestPath())
            {
                case true:
                {
                    EngineINI.LoadValues();
                    Forms.ToggleGUI(true);
                    Forms.UpdateValues();
                    break;
                }
                case false:
                {
                    Forms.PromptININotExist();
                    Forms.ToggleGUI(false);
                    break;
                }
            }
            // Store the decision in the registry.
            Functions.SetRegistryValue(Config.RegEntry, "GameVersion", Version);
        }
        private void Radio_Steam_CheckedChanged(object sender, EventArgs e)
        {
            // Steam version of the game was toggled.
            bool   Checked = (sender as RadioButton).Checked;
            string Version = "Steam";
            string INIPath = Config.AppData + "\\Sandfall\\Saved\\Config\\Windows";
            this.Radio_CheckedChanged(Checked, Version, INIPath);
        }
        private void Radio_GamePass_CheckedChanged(object sender, EventArgs e)
        {
            // GamePass version of the game was toggled.
            bool   Checked = (sender as RadioButton).Checked;
            string Version = "GamePass";
            string INIPath = Config.AppData + "\\Sandfall\\Saved\\Config\\WinGDK";
            this.Radio_CheckedChanged(Checked, Version, INIPath);
        }
        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        //   Main Dialog - Advanced Options GroupBox
        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-

        private void Radio_AdvShow_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                Functions.SetRegistryValue(Config.RegEntry, "ShowAdvanced", "True");
            }
            this.Button_Launch.Location = new Point(798, 600);
            this.Button_ExitSave.Location = new Point(924, 600);
            this.Size = new Size(1072, 676);
            this.CenterToScreen();
        }

        private void Radio_AdvHide_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                Functions.SetRegistryValue(Config.RegEntry, "ShowAdvanced", "False");
            }
            this.Button_Launch.Location = new Point(263, 600);
            this.Button_ExitSave.Location = new Point(389, 600);
            this.Size = new Size(536, 676);
            this.CenterToScreen();
        }

        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        //   Main Dialog - Options CheckBoxes
        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        private void CheckBox_CheckAllClicked(object sender, MouseEventArgs e)
        {
            // Get state of checkbox.
            bool CheckState = (sender as CheckBox).Checked;

            // Check for the control key being held.
            if (this.ControlHeld)
            {
                // If it is, copy the check state to all checkboxes.
                foreach (Control Checkbox in this.GroupBox_Main.Controls)
                {
                    if (Checkbox.GetType() == typeof(CheckBox))
                    {
                        (Checkbox as CheckBox).Checked = CheckState;
                    }
                }
            }
        }
        private void CheckBox_AF_CheckedChanged(object sender, EventArgs e)
        {
            this.Combo_AF.Enabled = !(sender as CheckBox).Checked;
            this.Label_AF.Enabled = !(sender as CheckBox).Checked;
        }
        private void CheckBox_DoF_CheckedChanged(object sender, EventArgs e)
        {
            this.Combo_DoF.Enabled = !(sender as CheckBox).Checked;
            this.Label_DoF.Enabled = !(sender as CheckBox).Checked;
        }
        private void CheckBox_Bloom_CheckedChanged(object sender, EventArgs e)
        {
            this.Combo_Bloom.Enabled = !(sender as CheckBox).Checked;
            this.Label_Bloom.Enabled = !(sender as CheckBox).Checked;
        }
        private void CheckBox_MBlur_CheckedChanged(object sender, EventArgs e)
        {
            this.Combo_MBlur.Enabled = !(sender as CheckBox).Checked;
            this.Label_MBlur.Enabled = !(sender as CheckBox).Checked;
        }
        private void CheckBox_LensFlare_CheckedChanged(object sender, EventArgs e)
        {
            this.Combo_LensFlare.Enabled = !(sender as CheckBox).Checked;
            this.Label_LensFlare.Enabled = !(sender as CheckBox).Checked;
        }
        private void CheckBox_Fog_CheckedChanged(object sender, EventArgs e)
        {
            this.Combo_Fog.Enabled = !(sender as CheckBox).Checked;
            this.Label_Fog.Enabled = !(sender as CheckBox).Checked;
        }
        private void CheckBox_VFog_CheckedChanged(object sender, EventArgs e)
        {
            this.Combo_VFog.Enabled = !(sender as CheckBox).Checked;
            this.Label_VFog.Enabled = !(sender as CheckBox).Checked;
        }
        private void CheckBox_ChromAb_CheckedChanged(object sender, EventArgs e)
        {
            this.Combo_ChromAb.Enabled = !(sender as CheckBox).Checked;
            this.Label_ChromAb.Enabled = !(sender as CheckBox).Checked;
        }
        private void CheckBox_Distort_CheckedChanged(object sender, EventArgs e)
        {
            this.Combo_Distort.Enabled = !(sender as CheckBox).Checked;
            this.Label_Distort.Enabled = !(sender as CheckBox).Checked;
        }
        private void CheckBox_FilmGrain_CheckedChanged(object sender, EventArgs e)
        {
            this.Combo_FilmGrain.Enabled = !(sender as CheckBox).Checked;
            this.Label_FilmGrain.Enabled = !(sender as CheckBox).Checked;
        }
        private void CheckBox_ShadQual_CheckedChanged(object sender, EventArgs e)
        {
            this.Combo_ShadQual.Enabled = !(sender as CheckBox).Checked;
            this.Label_ShadQual.Enabled = !(sender as CheckBox).Checked;
        }
        private void CheckBox_ShadRes_CheckedChanged(object sender, EventArgs e)
        {
            this.Combo_ShadRes.Enabled = !(sender as CheckBox).Checked;
            this.Label_ShadRes.Enabled = !(sender as CheckBox).Checked;
        }
        private void CheckBox_Tonemap_CheckedChanged(object sender, EventArgs e)
        {
            this.Combo_Tonemap.Enabled = !(sender as CheckBox).Checked;
            this.Label_Tonemap.Enabled = !(sender as CheckBox).Checked;
        }
        private void CheckBox_GrainQuant_CheckedChanged(object sender, EventArgs e)
        {
            this.Combo_GrainQuant.Enabled = !(sender as CheckBox).Checked;
            this.Label_GrainQuant.Enabled = !(sender as CheckBox).Checked;
        }
        private void CheckBox_Sharpen_CheckedChanged(object sender, EventArgs e)
        {
            this.Num_Sharpen.Enabled   = !(sender as CheckBox).Checked;
            this.Label_Sharpen.Enabled = !(sender as CheckBox).Checked;
        }
        private void CheckBox_ViewDist_CheckedChanged(object sender, EventArgs e)
        {
            this.Num_ViewDist.Enabled   = !(sender as CheckBox).Checked;
            this.Label_ViewDist.Enabled = !(sender as CheckBox).Checked;
        }
        private void CheckBox_ShadDist_CheckedChanged(object sender, EventArgs e)
        {
            this.Num_ShadDist.Enabled   = !(sender as CheckBox).Checked;
            this.Label_ShadDist.Enabled = !(sender as CheckBox).Checked;
        }
        private void CheckBox_FolDist_CheckedChanged(object sender, EventArgs e)
        {
            this.Num_FolDist.Enabled   = !(sender as CheckBox).Checked;
            this.Label_FolDist.Enabled = !(sender as CheckBox).Checked;
        }
        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        //   Main Dialog - Options GroupBox
        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        private void Combo_AF_SelectedIndexChanged(object sender, EventArgs e)
        {
            string NewValue = (sender as ComboBox).SelectedItem.ToString();
            EngineINI.MainEntries[0] = ValueSwap.Translate_AA(NewValue);
        }
        private void Combo_DoF_SelectedIndexChanged(object sender, EventArgs e)
        {
            string NewValue = (sender as ComboBox).SelectedItem.ToString();
            EngineINI.MainEntries[1] = ValueSwap.Translate_Scale(NewValue);
        }
        private void Combo_Bloom_SelectedIndexChanged(object sender, EventArgs e)
        {
            string NewValue = (sender as ComboBox).SelectedItem.ToString();
            EngineINI.MainEntries[2] = ValueSwap.Translate_Scale(NewValue);
        }
        private void Combo_MBlur_SelectedIndexChanged(object sender, EventArgs e)
        {
            string NewValue = (sender as ComboBox).SelectedItem.ToString();
            EngineINI.MainEntries[3] = ValueSwap.Translate_Scale(NewValue);
        }
        private void Combo_LensFlare_SelectedIndexChanged(object sender, EventArgs e)
        {
            string NewValue = (sender as ComboBox).SelectedItem.ToString();
            EngineINI.MainEntries[4] = ValueSwap.Translate_Scale(NewValue);
        }
        private void Combo_Fog_SelectedIndexChanged(object sender, EventArgs e)
        {
            string NewValue = (sender as ComboBox).SelectedItem.ToString();
            EngineINI.MainEntries[5] = ValueSwap.Translate_Binary(NewValue);
        }
        private void Combo_VFog_SelectedIndexChanged(object sender, EventArgs e)
        {
            string NewValue = (sender as ComboBox).SelectedItem.ToString();
            EngineINI.MainEntries[6] = ValueSwap.Translate_Binary(NewValue);
        }
        private void Combo_ChromAb_SelectedIndexChanged(object sender, EventArgs e)
        {
            string NewValue = (sender as ComboBox).SelectedItem.ToString();
            EngineINI.MainEntries[7] = ValueSwap.Translate_Binary(NewValue);
        }
        private void Combo_Distort_SelectedIndexChanged(object sender, EventArgs e)
        {
            string NewValue = (sender as ComboBox).SelectedItem.ToString();
            EngineINI.MainEntries[8] = ValueSwap.Translate_Binary(NewValue);
        }
        private void Combo_FilmGrain_SelectedIndexChanged(object sender, EventArgs e)
        {
            string NewValue = (sender as ComboBox).SelectedItem.ToString();
            EngineINI.MainEntries[9] = ValueSwap.Translate_Binary(NewValue);
        }
        private void Combo_ShadQual_SelectedIndexChanged(object sender, EventArgs e)
        {
            string NewValue = (sender as ComboBox).SelectedItem.ToString();
            EngineINI.MainEntries[10] = ValueSwap.Translate_ShadQual(NewValue);
        }
        private void Combo_ShadRes_SelectedIndexChanged(object sender, EventArgs e)
        {
            string NewValue = (sender as ComboBox).SelectedItem.ToString();
            EngineINI.MainEntries[11] = ValueSwap.Translate_ShadRes(NewValue);
        }
        private void Combo_Tonemap_SelectedIndexChanged(object sender, EventArgs e)
        {
            string NewValue = (sender as ComboBox).SelectedItem.ToString();
            EngineINI.MainEntries[12] = NewValue;
        }
        private void Combo_GrainQuant_SelectedIndexChanged(object sender, EventArgs e)
        {
            string NewValue = (sender as ComboBox).SelectedItem.ToString();
            EngineINI.MainEntries[13] = ValueSwap.Translate_Binary(NewValue);
        }
        private void Num_Sharpen_ValueChanged(object sender, EventArgs e)
        {
            string NewValue = Convert.ToString((sender as NumericUpDown).Value);
            EngineINI.MainEntries[14] = NewValue;
        }
        private void Num_ViewDist_ValueChanged(object sender, EventArgs e)
        {
            string NewValue = Convert.ToString((sender as NumericUpDown).Value);
            EngineINI.MainEntries[15] = NewValue;
        }
        private void Num_ShadDist_ValueChanged(object sender, EventArgs e)
        {
            string NewValue = Convert.ToString((sender as NumericUpDown).Value);
            EngineINI.MainEntries[16] = NewValue;
        }
        private void Num_FolDist_ValueChanged(object sender, EventArgs e)
        {
            string NewValue = Convert.ToString((sender as NumericUpDown).Value);
            EngineINI.MainEntries[17] = NewValue;
        }
        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        //   MenuStrip - File
        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        private void StripItem_CreateINI_Click(object sender, EventArgs e)
        {
            // If INI file exists.
            if (EngineINI.Path.TestPath())
            {
                // Prompt the user to overwrite.
                switch (Forms.PromptOverwriteINI())
                {
                    // If they do not wish to overwrite, get out of here.
                    case true:  { EngineINI.Delete(); break; }
                    case false: { return; }
                }
            }
            // Create the new INI file.
            EngineINI.CreateNew();

            // Reset checkboxes, update values, and enable the GUI.
            Forms.ClearCheckBoxes();
            Forms.UpdateValues();
            Forms.ToggleGUI(true);
        }
        private void StripItem_SaveINI_Click(object sender, EventArgs e)
        {
            // Save the INI and let the user know it was saved.
            EngineINI.WriteValues();
            Forms.PromptSaveINI();
        }
        private void StripItem_ReloadINI_Click(object sender, EventArgs e)
        {
            // If the user wants to reload it.
            if (Forms.PromptReloadINI())
            {
                // Reload the INI values and update the GUI.
                EngineINI.LoadValues();
                Forms.UpdateValues();
            }
        }
        private void StripItem_DeleteINI_Click(object sender, EventArgs e)
        {
            // If the user wants to delete it.
            if (Forms.PromptDeleteINI())
            {
                // Delete the INI and disable the GUI.
                EngineINI.Delete();
                Forms.ToggleGUI(false);
            }
        }
        private void StripItem_EditINI_Click(object sender, EventArgs e)
        {
            // Test the path to the INI.
            if (EngineINI.Path.TestPath())
            {
                // Remove the read-only flag so it can be edited.
                File.SetAttributes(EngineINI.Path, ~FileAttributes.ReadOnly);

                // Launch it with the default Windows app for editing INI files.
                Functions.OpenFileExplorer(EngineINI.Path);
            }
        }
        private void StripItem_Base_Click(object sender, EventArgs e)
        {
            // Open the folder this application is located in file explorer.
            if (!Config.BasePath.TestPath()) { return; }
            Functions.OpenFileExplorer(Config.BasePath);
        }
        private void StripItem_AppData_Click(object sender, EventArgs e)
        {
            // Open the base AppData\Sandfall folder in file explorer.
            if (!(Config.AppData + "\\Sandfall").TestPath()) { return; }
            Functions.OpenFileExplorer(Config.AppData + "\\Sandfall");
        }
        private void StripItem_EngineINI_Click(object sender, EventArgs e)
        {
            // Open the INI file location in AppData\Sandfall in file explorer.
            if (!EngineINI.Path.TestPath()) { return; }
            Functions.OpenFileExplorer(EngineINI.Path.GetFilePath());
        }
        private void StripItem_ExitNoSave_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void StripItem_ExitSave_Click(object sender, EventArgs e)
        {
            EngineINI.WriteValues();
            this.Close();
        }
        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        //   MenuStrip - Backup
        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        private void StripItem_CreateBackup_Click(object sender, EventArgs e)
        {
            EngineINI.Backup();
        }
        private void StripItem_RestoreBackup_Click(object sender, EventArgs e)
        {
            EngineINI.Restore();
        }
        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        //   MenuStrip - Presets
        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        private void StripItem_PresetLow_Click(object sender, EventArgs e)
        {
            EngineINI.LoadPreset_Low();
            Forms.UpdateValues();
        }
        private void StripItem_PresetMed_Click(object sender, EventArgs e)
        {
            EngineINI.LoadPreset_Medium();
            Forms.UpdateValues();
        }
        private void StripItem_PresetHigh_Click(object sender, EventArgs e)
        {
            EngineINI.LoadPreset_High();
            Forms.UpdateValues();
        }
        private void StripItem_PresetUltra_Click(object sender, EventArgs e)
        {
            EngineINI.LoadPreset_Ultra();
            Forms.UpdateValues();
        }
        private void StripItem_SharpClear_Click(object sender, EventArgs e)
        {
            EngineINI.LoadPreset_SharpClear();
            Forms.UpdateValues();
        }
        private void StripItem_SoftAmb_Click(object sender, EventArgs e)
        {
            EngineINI.LoadPreset_SoftAmbient();
            Forms.UpdateValues();
        }
        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        //   MenuStrip - Game
        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        private void StripItem_LaunchGame_Click(object sender, EventArgs e)
        {
            Game.Launch();
        }
        private void StripItem_CloseGame_Click(object sender, EventArgs e)
        {
            Game.EndProcess();
        }
        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        //   MenuStrip - Options
        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        private void StripItem_HideCheckBox_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem MenuOption = (sender as ToolStripMenuItem);

            // Get the check state of the option.
            bool CheckState = MenuOption.Checked;

            // Invert the check state.
            MenuOption.Checked = !CheckState;

            // Toggle the CheckBoxes.
            Forms.ToggleCheckBoxes(!CheckState);
        }
        private void StripItem_LaunchSave_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem MenuOption = (sender as ToolStripMenuItem);

            // Get the check state of the option.
            bool CheckState = MenuOption.Checked;

            // Invert the check state.
            MenuOption.Checked = !CheckState;

            // Toggle the option.
            Forms.ToggleLaunchSave(!CheckState);
        }
        private void StripItem_LaunchClose_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem MenuOption = (sender as ToolStripMenuItem);

            // Get the check state of the option.
            bool CheckState = MenuOption.Checked;

            // Invert the check state.
            MenuOption.Checked = !CheckState;

            // Toggle the option.
            Forms.ToggleLaunchClose(!CheckState);
        }
        private void StripItem_NoToolTips_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem MenuOption = (sender as ToolStripMenuItem);

            // Get the check state of the option.
            bool CheckState = MenuOption.Checked;

            // Invert the check state.
            MenuOption.Checked = !CheckState;

            // Toggle the option.
            Forms.ToggleGlobalToolTips(!CheckState);
        }
        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        //   MenuStrip - Help
        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        private void StripItem_HelpAbout_Click(object sender, EventArgs e)
        {
            Forms.PromptAbout();
        }
        private void StripItem_GitHub_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/BigheadSMZ/ClairObscur-UConfig");
        }
        private void StripItem_NexusMods_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.nexusmods.com/clairobscurexpedition33/mods/119");
        }
        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        //   Main Dialog - Bottom Buttons
        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        private void Button_Save_Click(object sender, EventArgs e)
        {
            Forms.PromptSaveINI();
            EngineINI.WriteValues();
        }
        private void Button_Reload_Click(object sender, EventArgs e)
        {
            if (Forms.PromptReloadINI())
            {
                EngineINI.LoadValues();
                Forms.UpdateValues();
            }
        }
        private void Button_Launch_Click(object sender, EventArgs e)
        {
            Game.Launch();
        }
        private void Button_Exit_Click(object sender, EventArgs e)
        {
            // The INI must exist and the Control key must not be held.
            if (EngineINI.Path.TestPath() && !this.ControlHeld)
            {
                // Update the INI file.
                EngineINI.WriteValues();
            }
            // Close the dialog no matter what.
            this.Close();
        }
        private void Form_MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            // Must be Control Key, the INI must exist, and the variable must be false.
            if (e.KeyCode == Keys.ControlKey & EngineINI.Path.TestPath() & !this.ControlHeld)
            {
                // Change the button text of the Exit button to not save.
                e.SuppressKeyPress = true;
                this.ControlHeld = true;
                this.Button_ExitSave.Text = "Exit";
            }
        }
        private void Form_MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            // Must be Control Key and the INI must exist.
            if (e.KeyCode == Keys.ControlKey & EngineINI.Path.TestPath())
            {
                // Restore the save text on the Exit button.
                this.ControlHeld = false;
                this.Button_ExitSave.Text = "Save / Exit";
            }
        }
        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        //   Advanced Dialog
        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        private void RichTextBox_AddOptions_MouseClick(object sender, MouseEventArgs e)
        {
            // Clear the text on the first click.
            if (!this.ClickClear)
            {
                this.RichTextBox_AddOptions.Text = "";
                this.ClickClear = true;
            }
        }
        private void GridView_Options_KeyDown(object sender, KeyEventArgs e)
        {
            // Handle the delete key being pressed.
            Forms.DataGridView_DeleteEntry(e.KeyCode);
        }
        private void GridView_Options_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Handle when a value on the datagridview changes.
            Forms.DataGridView_ValueChanged(e.RowIndex);
        }
        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        //   Advanced Dialog: Buttons
        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        private void Button_Bonus_Click(object sender, EventArgs e)
        {
            // Get the button.
            Button BonusButton = (sender as Button);

            // If the menu isn't currently visible.
            if (!this.CMenuStrip_Collections.Visible)
            {
                // Show it twice. This works around an initial bug where it has no size since it wasn't already shown.
                this.CMenuStrip_Collections.Show();
                this.CMenuStrip_Collections.Show(BonusButton, new Point(0, -1 * this.CMenuStrip_Collections.Size.Height));
            }
        }
        private void Button_ClearAll_Click(object sender, EventArgs e)
        {
            // Ask the user to clear all options.
            if (Forms.PromptClearAll())
            {
                // If they accepted than do it.
                Forms.DataGridView_ClearAll();
            }
        }
        private void Button_RestoreText_Click(object sender, EventArgs e)
        {
            // Allow wiping out the text on a click again.
            this.ClickClear = false;

            // The text that was on the textbox before it was wiped.
            string TextBoxText = "Copy and paste multiple lines of \"Engine.ini\" options and values here\r\n" +
                                 "and click \"Parse\" to import them into the options above.Any potential\r\n" +
                                 "duplicate entries will be automatically excluded. When saving the INI,\r\n" +
                                 "all options and values above will also be written to the INI file.";
            // Restore the text.
            this.RichTextBox_AddOptions.Text = TextBoxText;
        }
        private void Button_Parse_Click(object sender, EventArgs e)
        {
            Forms.DataGridView_ParseTextBoxText();
        }
        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        //   Advanced Dialog: Collections StripMenu
        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        private void StripItem_NoLumen_Click(object sender, EventArgs e)
        {
            // Don't let a click wipe it out.
            this.ClickClear = true;

            // Set on the textbox.
            this.RichTextBox_AddOptions.Text = EngineINI.Collection_DisableLumen(); ;
        }
        private void StripItem_EnhLumen_Click(object sender, EventArgs e)
        {
            // Don't let a click wipe it out.
            this.ClickClear = true;

            // Set on the textbox.
            this.RichTextBox_AddOptions.Text = EngineINI.Collection_EnhLumen();
        }
        private void StripItem_Clarity_Click(object sender, EventArgs e)
        {
            // Don't let a click wipe it out.
            this.ClickClear = true;

            // Set on the textbox.
            this.RichTextBox_AddOptions.Text = EngineINI.Collection_PerfectClarity();
        }
        private void StripItem_UltiHighEnd_Click(object sender, EventArgs e)
        {
            // Don't let a click wipe it out.
            this.ClickClear = true;

            // Set on the textbox.
            this.RichTextBox_AddOptions.Text = EngineINI.Collection_UltimateHighEnd();
        }
        private void StripItem_6GBVRAM_Click(object sender, EventArgs e)
        {
            // Don't let a click wipe it out.
            this.ClickClear = true;

            // Set on the textbox.
            this.RichTextBox_AddOptions.Text = EngineINI.Collection_6GBVRAM();
        }
        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        //   Advanced Dialog: RichTextBox StripMenu
        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        private void StripItem_Cut_Click(object sender, EventArgs e)
        {
            this.RichTextBox_AddOptions.Cut();
        }
        private void StripItem_Copy_Click(object sender, EventArgs e)
        {
            this.RichTextBox_AddOptions.Copy();
        }
        private void StripItem_Paste_Click(object sender, EventArgs e)
        {
            this.RichTextBox_AddOptions.Paste(DataFormats.GetFormat(DataFormats.Text));
        }
        private void StripItem_Delete_Click(object sender, EventArgs e)
        {
            this.RichTextBox_AddOptions.SelectedText = "";
        }
        private void StripItem_Clear_Click(object sender, EventArgs e)
        {
            this.RichTextBox_AddOptions.Text = "";
        }
    }
}