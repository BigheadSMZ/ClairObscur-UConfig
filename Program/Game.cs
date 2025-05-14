using System.Collections.Generic;
using System.Diagnostics;

namespace ClairObscurConfig
{
    internal class Game
    {
        // The name of the game this configurator is made for.
        public static string Name   = "Clair Obscur: Expedition 33";

        // Executable names that the configurator looks for to launch the game. An asterisk "*" allows a wildcard match.
        static string[] Executables = new string[] { "Expedition33*", "Sandfall*" };

        // When using the "Task Kill" option on the menu, this is the name of the process to kill.
        static string TaskKillName  = "SandFall-Win64-Shipping";

        public static string GetExecutable()
        {
            // Stores a list of files in the current path.
            List<string> Files = Config.BasePath.GetFiles();

            // Loop through the files.
            foreach (string File in Files)
            {
                // Get the current file as a FileItem which is more useful than FileInfo.
                FileItem FileProperties = new FileItem(File);

                // Skip the current file if it's the running application.
                if (FileProperties.Name == Config.AppName) { continue; }

                // Loop through the executables listed.
                foreach (string ExeName in Game.Executables)
                {
                    // If the name of the file matches what we are looking for.
                    if (Wildcard.Match(FileProperties.BaseName, ExeName))
                    {
                        // It must be an executable and not be this configurator.
                        if (FileProperties.Extension == ".exe")
                        {
                            // After a rigid code adventure, we have the game executable.
                            return FileProperties.FullName;
                        }
                    }
                }
            }
            // If we got here, sadly the executable is not here.
            return "";
        }

        public static void Launch()
        {
            // The path to the game must exist or it can't be launched.
            if (Config.GamePath.TestPath())
            {
                // If the option was checked to save on launch.
                if (Config.SaveOnLaunch)
                {
                    EngineINI.WriteValues();
                }
                // Get info on the game executable.
                FileItem GameItem = new FileItem(Config.GamePath);

                // Create the process and start it.
                Process GameProcess = new Process();
                GameProcess.StartInfo.WorkingDirectory = GameItem.DirectoryName;
                GameProcess.StartInfo.FileName = GameItem.FullName;
                GameProcess.StartInfo.Arguments = "";
                GameProcess.StartInfo.UseShellExecute = false;
                GameProcess.StartInfo.RedirectStandardOutput = false;
                GameProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                GameProcess.Start();

                // Close the main dialog if the option to do so was toggled.
                if (Config.CloseOnLaunch)
                {
                    Forms.MainDialog.Close();
                }
            }
            // Let the user know it should be within the same folder.
            else
            {
                Forms.PromptGameNotFound();
            }
        }

        public static void EndProcess()
        {
            // Get a list of running processes.
            Process[] ProcRunning = Process.GetProcesses();

            // Loop through all processes.
            foreach (Process Proc in ProcRunning)
            {
                // Match the game process.
                if (Proc.ProcessName == Game.TaskKillName)
                {
                    // Kill it and exit.
                    Proc.Kill();
                    return;
                }
            }
            // If the game isn't running alert the user.
            Forms.PromptNoGameProcess();
        }
    }
}
