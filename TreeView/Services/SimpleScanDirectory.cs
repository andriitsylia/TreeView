using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TreeView.Models;

namespace TreeView.Services
{
    public static class SimpleScanDirectory
    {
        public static SimpleFolderModel SimpleScan(SimpleFolderModel folder)
        {
            return SimpleScan(folder.Name);
        }

        public static SimpleFolderModel SimpleScan(string directory)
        {
            IEnumerable<string> dirs;
            IEnumerable<string> files;
            List<SimpleFolderModel> subFolders = new();

            //string dirName = directory.Substring(directory.LastIndexOf("\\") + 1);
            SimpleFolderModel folder = new() {Name = directory };//Name = string.IsNullOrEmpty(dirName) ? directory : dirName };

            try
            {
                dirs = Directory.EnumerateDirectories(directory);
                files = Directory.EnumerateFiles(directory);
            }
            catch (UnauthorizedAccessException)
            {
                folder.Name += " *** ACCESS DENIED ***";
                folder.Size = 0;
                folder.Type = FolderType.NoAccessFolder;
                folder.SubFolders = null;
                return folder;
            }
            
            foreach (var dir in dirs)
            {
                subFolders.Add(new SimpleFolderModel() { Name = dir, Type = FolderType.Folder });
            }

            long filesSize = 0;

            foreach (var file in files)
            {
                FileInfo fileInfo = new(file);
                filesSize += fileInfo.Length;
                subFolders.Add(new SimpleFolderModel() { Name = file.Substring(file.LastIndexOf("\\") + 1),
                                                   Size = fileInfo.Length,
                                                   Type = FolderType.File,
                                                   SubFolders = null });
            }

            folder.Size += filesSize;
            folder.Type = FolderType.Folder;
            folder.SubFolders = new List<SimpleFolderModel>(subFolders);
            return folder;
        }

        public static void SimpleStartScan(object folder)
        {
            SimpleRecursiveScan((SimpleFolderModel)folder);
        }

        public static void SimpleRecursiveScan(SimpleFolderModel folder)//string directory)
        {
                List<SimpleFolderModel> subFolders = new();
                IEnumerable<string> dirs = new List<string>();
                IEnumerable<string> files = new List<string>();
                //string dirName = directory.Substring(directory.LastIndexOf("\\") + 1);
                //FolderModel folder = new() { Name = string.IsNullOrEmpty(dirName) ? directory : dirName };
               //if (depth > 0)
               //{
               // int tempDepth = --depth;
                try
                {
                    dirs = Directory.EnumerateDirectories(folder.Name);
                    files = Directory.EnumerateFiles(folder.Name);
                    //dirs = Directory.EnumerateDirectories(directory);
                    //files = Directory.EnumerateFiles(directory);
                }
                catch (UnauthorizedAccessException)
                {
                    folder.Name += " *** ACCESS DENIED ***";
                    folder.Type = FolderType.NoAccessFolder;
                    //return folder;
                }


                foreach (var dir in dirs)
                {
                    SimpleFolderModel tempFolder = new() { Name = dir, Type = FolderType.Folder };// = new() { Name = dir.Substring(dir.LastIndexOf("\\") + 1), };
                    Thread.Sleep(300);
                    SimpleRecursiveScan(tempFolder); //, tempDepth
                    subFolders.Add(tempFolder);
                    folder.Size += tempFolder.Size;
                    //FolderModel tempFolder;// = new() { Name = dir.Substring(dir.LastIndexOf("\\") + 1), };
                    //tempFolder = RecursiveScan(dir); //, tempDepth
                    //subFolders.Add(tempFolder);
                    //folder.Size += tempFolder.Size;
            }

                long filesSize = 0;

                foreach (var file in files)
                {
                    FileInfo fileInfo = new(file);
                    filesSize += fileInfo.Length;
                    subFolders.Add(new SimpleFolderModel()
                    {
                        Name = file.Substring(file.LastIndexOf("\\") + 1),
                        Size = fileInfo.Length,
                        Type = FolderType.File,
                        SubFolders = null
                    });
                }

                folder.Size += filesSize;
                folder.Type = FolderType.Folder;
                folder.SubFolders = new List<SimpleFolderModel>(subFolders);

                //return folder;
            //}
            //else
            //{
            //    return folder;
            //}
        }
    }
}
