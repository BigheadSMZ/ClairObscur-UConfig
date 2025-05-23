﻿using System;
using System.Windows.Forms;

namespace ClairObscurConfig
{
    internal static class Initialization
    {
        [STAThread]
        static void Main()
        {
            // The version of the configurator. Set here for easy editing.
            Config.AppVersion = "2.1";

            // Enable form visual styles.
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Set the initial values.
            Config.SetApplicationValues();

            // Initialize the INI file.
            EngineINI.Initialize();

            // Create the dialog and show it.
            Forms.Initialize();
            Forms.MainDialog.ShowDialog();
        }
    }
}
