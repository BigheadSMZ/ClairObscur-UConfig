using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace ClairObscurConfig
{
    public partial class Form_YesNoForm : Form
    {
        // Keeps track of the choice selected.
        public bool YesNoChoice;

        public Form_YesNoForm()
        {
            InitializeComponent();
        }

        private void Form_YesNoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Catch the form trying to close.
            e.Cancel = true;
            this.Hide();
        }

        private void Button_Yes_Click(object sender, EventArgs e)
        {
            // The answer was "Yes".
            this.YesNoChoice = true;
            this.Close();
        }

        private void Button_No_Click(object sender, EventArgs e)
        {
            // The answer was "No".
            this.YesNoChoice = false;
            this.Close();
        }

        public bool Display(string Title, string Message, int SizeX, int SizeY, int OffsetX, int OffsetY, bool PlayBeep = false)
        {
            // Set the dialog properties.
            this.Text = Title;
            this.Label_Message.Text = Message;
            this.Label_Message.Size = new Size(SizeX, SizeY);
            this.Label_Message.Location = new Point(OffsetX, OffsetY);

            // Reset the choice to false to catch "X" button press.
            this.YesNoChoice = false;

            // Always play the beep. 
            if (PlayBeep) { SystemSounds.Beep.Play(); }

            // Let the user know what's up.
            this.ShowDialog();

            // Return the button choice.
            return this.YesNoChoice;
        }
    }
}
