using System;
using System.IO;

namespace ClairObscurConfig
{
    public class FileItem
    {
        public FileAttributes Attributes;
        public string BaseName = "";
        public DirectoryInfo Directory;
        public string DirectoryName = "";
        public bool Exists = false;
        public string Extension = "";
        public string FullName = "";
        public bool IsReadOnly = false;
        public DateTime LastAccessTime;
        public DateTime LastAccessTimeUtc;
        public DateTime LastWriteTime;
        public DateTime LastWriteTimeUtc;
        public long Length = 0;
        public string Name = "";
        public DirectoryInfo Parent;
        public DirectoryInfo Root;

        public FileItem(string InputFile)
        {
            // This "Info" can be file or diectory so start it as dynamic.
            dynamic Info = null;

            // If it's a folder, then create DirectoryInfo.
            if (InputFile.TestPath(true))
            {
                // Most properties will be derived from DirectoryInfo.
                Info = new DirectoryInfo(InputFile);

                // Properties specific to this type.
                this.BaseName = Info.Name;
                this.DirectoryName = Info.FullName;
                this.Parent = Info.Parent;
                this.Root = Info.Root;
            }
            // If it's a file, then create FileInfo.
            else if (InputFile.TestPath(false))
            {
                // Most properties will be derived from FileInfo.
                Info = new FileInfo(InputFile);

                // Properties specific to this type.
                this.BaseName = (Info.Name as string).GetBaseName();
                this.Directory = Info.Directory;
                this.DirectoryName = Info.DirectoryName;
                this.IsReadOnly = Info.IsReadOnly;
                this.Length = Info.Length;
            }
            // Set up the shared properties.
            this.Attributes = Info.Attributes;
            this.Exists = Info.Exists;
            this.Extension = Info.Extension;
            this.FullName = Info.FullName;
            this.LastAccessTime = Info.LastAccessTime;
            this.LastAccessTimeUtc = Info.LastAccessTimeUtc;
            this.LastWriteTime = Info.LastWriteTime;
            this.LastWriteTimeUtc = Info.LastWriteTimeUtc;
            this.Name = Info.Name;
        }
    }
}
