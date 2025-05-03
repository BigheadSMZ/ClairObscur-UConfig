using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace ClairObscurConfig
{
    public partial class Form_OkayForm : Form
    {
        Timer OKTimer = new Timer();

        public Form_OkayForm()
        {
            InitializeComponent();
        }

        private void Form_OkayForm_Load(object sender, EventArgs e)
        {
            // When the form is loaded add the event handler.
            this.OKTimer.Tick += new EventHandler(this.TimerTick);
        }

        private void Form_OkayForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Catch the form trying to close to stop the timer.
            this.OKTimer.Stop();
            e.Cancel = true;

            // Hide is used for parity between close button, X button, and timer expiring.
            this.Hide();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            // Hide when the timer expires.
            this.Close();
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            // Hide when the button is clicked.
            this.Close();
        }

        public void Display(string Title, string Message, int SizeX, int SizeY, int OffsetX, int OffsetY, int TimeOut = 0)
        {
            // Set the dialog properties.
            this.Text = Title;
            this.Label_Message.Text = Message;
            this.Label_Message.Size = new Size(SizeX, SizeY);
            this.Label_Message.Location = new Point(OffsetX, OffsetY);

            // Always play the beep. 
            SystemSounds.Beep.Play();

            // Start the timer using timeout if it was set.
            if (TimeOut > 0)
            {
                // Timeout is a multipler * 1 second.
                this.OKTimer.Interval = (TimeOut * 1000);
                this.OKTimer.Start();
            }
            // Let the user know what's up.
            this.ShowDialog();
        }
    }
}
