using System.Collections.Generic;
using System.Diagnostics;

namespace ClairObscurConfig
{
    internal class Game
    {
        public static string GetExecutable()
        {
            // Stores a list of files in the current path.
            List<string> Files = Config.BasePath.GetFiles();

            // Loop through the files.
            foreach (string File in Files)
            {
                // Get the current file as a FileItem which is more useful than FileInfo.
                FileItem FileProperties = new FileItem(File);

                // If the name of the file matches what we are looking for.
                if (Wildcard.Match(FileProperties.BaseName, "Expedition33*"))
                {
                    // It must be an executable and not be this configurator.
                    if (FileProperties.Extension == ".exe" && FileProperties.BaseName != "Expedition33_Config")
                    {
                        // After a rigid code adventure, we have the game executable.
                        return FileProperties.FullName;
                    }
                }
                // Let's also look for Sandfall which is the GamePass version or the "main" executable in both versions.
                if (Wildcard.Match(FileProperties.BaseName, "Sandfall*"))
                {
                    // It must be an executable and not be this configurator.
                    if (FileProperties.Extension == ".exe")
                    {
                        // After a rigid code adventure, we have the game executable.
                        return FileProperties.FullName;
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
                if (Proc.ProcessName == "SandFall-Win64-Shipping")
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
