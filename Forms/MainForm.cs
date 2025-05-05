using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ClairObscurConfig
{
    public partial class Form_MainForm : Form
    {
        bool ControlHeld;
        bool AllowToggle;

        public Form_MainForm()
        {
            InitializeComponent();
        }
        private void Form_MainForm_Shown(object sender, EventArgs e)
        {
            // Prevents the radio button events from firing until after the dialog is shown. This is a cheap and dirty workaround to
            // avoid "double loading" the INI files and also spamming the "INI doesn't exist" message if it's not found.
            this.AllowToggle = true;
        }
        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        //   Main Dialog - Game Type GroupBox
        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        private void Radio_Steam_CheckedChanged(object sender, EventArgs e)
        {
            // Get the check state.
            if ((sender as RadioButton).Checked & this.AllowToggle)
            {
                // Update the INI properties.
                EngineINI.Type = "Steam";
                EngineINI.Path = Config.AppData + "\\Sandfall\\Saved\\Config\\Windows\\Engine.ini";
                EngineINI.File = new IniFile(EngineINI.Path);

                // Try to load and toggle the GUI based on existence.
                switch (EngineINI.Path.TestPath())
                {
                    case true:  
                    { 
                        EngineINI.LoadINIValues(); 
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
            }
        }
        private void Radio_GamePass_CheckedChanged(object sender, EventArgs e)
        {
            // Get the check state.
            if ((sender as RadioButton).Checked & this.AllowToggle)
            {
                // Update the INI properties.
                EngineINI.Type = "GamePass";
                EngineINI.Path = Config.AppData + "\\Sandfall\\Saved\\Config\\WinGDK\\Engine.ini";
                EngineINI.File = new IniFile(EngineINI.Path);

                // Try to load and toggle the GUI based on existence.
                switch (EngineINI.Path.TestPath())
                {
                    case true:
                    {
                        EngineINI.LoadINIValues();
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
            }
        }
        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        //   Main Dialog - Options GroupBox
        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        private void Combo_AF_SelectedIndexChanged(object sender, EventArgs e)
        {
            string NewValue = (sender as ComboBox).SelectedItem.ToString();
            EngineINI.Anist_Val = ValueSwap.Translate_AA(NewValue);
        }
        private void Combo_DoF_SelectedIndexChanged(object sender, EventArgs e)
        {
            string NewValue = (sender as ComboBox).SelectedItem.ToString();
            EngineINI.Depth_Val = ValueSwap.Translate_Scale(NewValue);
        }
        private void Combo_Bloom_SelectedIndexChanged(object sender, EventArgs e)
        {
            string NewValue = (sender as ComboBox).SelectedItem.ToString();
            EngineINI.Bloom_Val = ValueSwap.Translate_Scale(NewValue);
        }
        private void Combo_MBlur_SelectedIndexChanged(object sender, EventArgs e)
        {
            string NewValue = (sender as ComboBox).SelectedItem.ToString();
            EngineINI.MBlur_Val = ValueSwap.Translate_Scale(NewValue);
        }
        private void Combo_LensFlare_SelectedIndexChanged(object sender, EventArgs e)
        {
            string NewValue = (sender as ComboBox).SelectedItem.ToString();
            EngineINI.LenFl_Val = ValueSwap.Translate_Scale(NewValue);
        }
        private void Combo_Fog_SelectedIndexChanged(object sender, EventArgs e)
        {
            string NewValue = (sender as ComboBox).SelectedItem.ToString();
            EngineINI.FogEf_Val = ValueSwap.Translate_Binary(NewValue);
        }
        private void Combo_VFog_SelectedIndexChanged(object sender, EventArgs e)
        {
            string NewValue = (sender as ComboBox).SelectedItem.ToString();
            EngineINI.VoFog_Val = ValueSwap.Translate_Binary(NewValue);
        }
        private void Combo_ChromAb_SelectedIndexChanged(object sender, EventArgs e)
        {
            string NewValue = (sender as ComboBox).SelectedItem.ToString();
            EngineINI.SCoFr_Val = ValueSwap.Translate_Binary(NewValue);
        }
        private void Combo_Distort_SelectedIndexChanged(object sender, EventArgs e)
        {
            string NewValue = (sender as ComboBox).SelectedItem.ToString();
            EngineINI.Distr_Val = ValueSwap.Translate_Binary(NewValue);
        }
        private void Combo_FilmGrain_SelectedIndexChanged(object sender, EventArgs e)
        {
            string NewValue = (sender as ComboBox).SelectedItem.ToString();
            EngineINI.Grain_Val = ValueSwap.Translate_Binary(NewValue);
        }
        private void Combo_ShadQual_SelectedIndexChanged(object sender, EventArgs e)
        {
            string NewValue = (sender as ComboBox).SelectedItem.ToString();
            EngineINI.ShadQ_Val = ValueSwap.Translate_ShadQual(NewValue);
        }
        private void Combo_ShadRes_SelectedIndexChanged(object sender, EventArgs e)
        {
            string NewValue = (sender as ComboBox).SelectedItem.ToString();
            EngineINI.ShadR_Val = ValueSwap.Translate_ShadRes(NewValue);
        }
        private void Combo_Tonemap_SelectedIndexChanged(object sender, EventArgs e)
        {
            string NewValue = (sender as ComboBox).SelectedItem.ToString();
            EngineINI.TMQua_Val = NewValue;
        }
        private void Combo_GrainQuant_SelectedIndexChanged(object sender, EventArgs e)
        {
            string NewValue = (sender as ComboBox).SelectedItem.ToString();
            EngineINI.TMGra_Val = ValueSwap.Translate_Binary(NewValue);
        }
        private void Num_Sharpen_ValueChanged(object sender, EventArgs e)
        {
            string NewValue = Convert.ToString((sender as NumericUpDown).Value);
            EngineINI.TMSha_Val = NewValue;
        }
        private void Num_ViewDist_ValueChanged(object sender, EventArgs e)
        {
            string NewValue = Convert.ToString((sender as NumericUpDown).Value);
            EngineINI.ViewD_Val = NewValue;
        }
        private void Num_ShadDist_ValueChanged(object sender, EventArgs e)
        {
            string NewValue = Convert.ToString((sender as NumericUpDown).Value);
            EngineINI.ViewS_Val = NewValue;
        }
        private void Num_FolDist_ValueChanged(object sender, EventArgs e)
        {
            string NewValue = Convert.ToString((sender as NumericUpDown).Value);
            EngineINI.ViewF_Val = NewValue;
        }
        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        //   MenuStrip - File
        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        private void StripItem_SaveINI_Click(object sender, EventArgs e)
        {
            Forms.PromptSaveINI();
        }
        private void StripItem_ReloadINI_Click(object sender, EventArgs e)
        {
            Forms.PromptReloadINI();
        }
        private void StripItem_DeleteINI_Click(object sender, EventArgs e)
        {
            Forms.PromptDeleteINI();
        }
        private void StripItem_CreateINI_Click(object sender, EventArgs e)
        {
            Forms.PromptCreateNewINI();
        }
        private void StripItem_Base_Click(object sender, EventArgs e)
        {
            if (!Config.BasePath.TestPath()) { return; }
            Forms.OpenFileExplorer(Config.BasePath);
        }
        private void StripItem_AppData_Click(object sender, EventArgs e)
        {
            if (!(Config.AppData + "\\Sandfall").TestPath()) { return; }
            Forms.OpenFileExplorer(Config.AppData + "\\Sandfall");
        }
        private void StripItem_EngineINI_Click(object sender, EventArgs e)
        {
            if (!EngineINI.Path.TestPath()) { return; }
            Forms.OpenFileExplorer(EngineINI.Path.GetFilePath());
        }
        private void StripItem_ExitNoSave_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void StripItem_ExitSave_Click(object sender, EventArgs e)
        {
            EngineINI.WriteINIValues();
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
        }
        private void Button_Reload_Click(object sender, EventArgs e)
        {
            Forms.PromptReloadINI();
        }
        private void Button_Launch_Click(object sender, EventArgs e)
        {
            Game.Launch();
        }
        private void Button_Exit_Click(object sender, EventArgs e)
        {
            // The INI must exist and the Control key not held.
            if (EngineINI.Path.TestPath() & !this.ControlHeld)
            {
                // Update the INI file.
                EngineINI.WriteINIValues();
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
                Forms.MainDialog.Button_ExitSave.Text = "Exit";
            }
        }
        private void Form_MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            // Must be Control Key and the INI must exist.
            if (e.KeyCode == Keys.ControlKey & EngineINI.Path.TestPath())
            {
                // Restore the save text on the Exit button.
                this.ControlHeld = false;
                Forms.MainDialog.Button_ExitSave.Text = "Save / Exit";
            }
        }
    }
}