using System;
using System.Windows.Forms;

namespace ClairObscurConfig
{
    public partial class Form_CamForm : Form
    {
        public Form_CamForm()
        {
            InitializeComponent();
        }

        private void CamForm_Shown(object sender, EventArgs e)
        {
            // Normal Camera Settings
            this.NumBox_FOV.Value = Convert.ToDecimal(CameraINI.FieldOfView);
            this.NumBox_TArmLen.Value = Convert.ToDecimal(CameraINI.TargetArmLength);
            this.NumBox_SocOffsetX.Value = Convert.ToDecimal(CameraINI.SocketOffsetX);
            this.NumBox_SocOffsetY.Value = Convert.ToDecimal(CameraINI.SocketOffsetY);
            this.NumBox_SocOffsetZ.Value = Convert.ToDecimal(CameraINI.SocketOffsetZ);
            this.Combo_CamLag.SelectedItem = CameraINI.EnableCameraLag == "1" ? "Enabled" : "Disabled";
            this.NumBox_LagMaxDist.Value = Convert.ToDecimal(CameraINI.CameraLagMaxDistance);
            this.NumBox_LagSpeed.Value = Convert.ToDecimal(CameraINI.CameraLagSpeed);

            // World Map Settings
            this.NumBox_FocalLength.Value = Convert.ToDecimal(CameraINI.WorldMapFocalLength);
            this.NumBox_PartyArmLen.Value = Convert.ToDecimal(CameraINI.PartyArmLength);
            this.NumBox_PartyOffsetX.Value = Convert.ToDecimal(CameraINI.PartyOffsetX);
            this.NumBox_PartyOffsetY.Value = Convert.ToDecimal(CameraINI.PartyOffsetY);
            this.NumBox_PartyOffsetZ.Value = Convert.ToDecimal(CameraINI.PartyOffsetZ);
            this.NumBox_EsquieArmLen.Value = Convert.ToDecimal(CameraINI.EsquieArmLength);
            this.NumBox_EsquieOffsetX.Value = Convert.ToDecimal(CameraINI.EsquieOffsetX);
            this.NumBox_EsquieOffsetY.Value = Convert.ToDecimal(CameraINI.EsquieOffsetY);
            this.NumBox_EsquieOffsetZ.Value = Convert.ToDecimal(CameraINI.EsquieOffsetZ);
        }

        private void Button_SaveExit_Click(object sender, EventArgs e)
        {
            CameraINI.WriteValues();
            this.Close();
        }
        private void Button_DiscardExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Normal Camera Settings
        private void NumBox_FOV_ValueChanged(object sender, EventArgs e)
        {
            CameraINI.FieldOfView = this.NumBox_FOV.Value.ToString();
        }
        private void NumBox_TArmLen_ValueChanged(object sender, EventArgs e)
        {
            CameraINI.TargetArmLength = this.NumBox_TArmLen.Value.ToString();
        }
        private void NumBox_SocOffsetX_ValueChanged(object sender, EventArgs e)
        {
            CameraINI.SocketOffsetX = this.NumBox_SocOffsetX.Value.ToString();
        }
        private void NumBox_SocOffsetY_ValueChanged(object sender, EventArgs e)
        {
            CameraINI.SocketOffsetY = this.NumBox_SocOffsetY.Value.ToString();
        }
        private void NumBox_SocOffsetZ_ValueChanged(object sender, EventArgs e)
        {
            CameraINI.SocketOffsetZ = this.NumBox_SocOffsetZ.Value.ToString();
        }
        private void Combo_CamLag_SelectedIndexChanged(object sender, EventArgs e)
        {
            CameraINI.EnableCameraLag = this.Combo_CamLag.SelectedItem.ToString() == "Enabled" ? "1" : "0";
        }
        private void NumBox_LagMaxDist_ValueChanged(object sender, EventArgs e)
        {
            CameraINI.CameraLagMaxDistance = this.NumBox_LagMaxDist.Value.ToString();
        }
        private void NumBox_LagSpeed_ValueChanged(object sender, EventArgs e)
        {
            CameraINI.CameraLagSpeed = this.NumBox_LagSpeed.Value.ToString();
        }

        // World Map Settings
        private void NumBox_FocalLength_ValueChanged(object sender, EventArgs e)
        {
            CameraINI.WorldMapFocalLength = this.NumBox_FocalLength.Value.ToString();
        }
        private void NumBox_PartyArmLen_ValueChanged(object sender, EventArgs e)
        {
            CameraINI.PartyArmLength = this.NumBox_PartyArmLen.Value.ToString();
        }
        private void NumBox_PartyOffsetX_ValueChanged(object sender, EventArgs e)
        {
            CameraINI.PartyOffsetX = this.NumBox_PartyOffsetX.Value.ToString();
        }
        private void NumBox_PartyOffsetY_ValueChanged(object sender, EventArgs e)
        {
            CameraINI.PartyOffsetY = this.NumBox_PartyOffsetY.Value.ToString();
        }
        private void NumBox_PartyOffsetZ_ValueChanged(object sender, EventArgs e)
        {
            CameraINI.PartyOffsetZ = this.NumBox_PartyOffsetZ.Value.ToString();
        }
        private void NumBox_EsquieArmLen_ValueChanged(object sender, EventArgs e)
        {
            CameraINI.EsquieArmLength = this.NumBox_EsquieArmLen.Value.ToString();
        }
        private void NumBox_EsquieOffsetX_ValueChanged(object sender, EventArgs e)
        {
            CameraINI.EsquieOffsetX = this.NumBox_EsquieOffsetX.Value.ToString();
        }
        private void NumBox_EsquieOffsetY_ValueChanged(object sender, EventArgs e)
        {
            CameraINI.EsquieOffsetY = this.NumBox_EsquieOffsetY.Value.ToString();
        }
        private void NumBox_EsquieOffsetZ_ValueChanged(object sender, EventArgs e)
        {
            CameraINI.EsquieOffsetZ = this.NumBox_EsquieOffsetZ.Value.ToString();
        }
    }
}
