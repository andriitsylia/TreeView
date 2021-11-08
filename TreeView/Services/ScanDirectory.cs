using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeView.Models;

namespace TreeView.Services
{
    public static class ScanDirectory
    {
        public static FolderModel Scan(string directory)
        {
            List<FolderModel> subFolders = new();
            List<FolderModel> files = new();

            foreach (var dir in Directory.EnumerateDirectories(directory))
            {
                subFolders.Add(new FolderModel() { Name = dir.Substring(dir.LastIndexOf("\\") + 1)});
            }

            long filesSize= 0;
            foreach (var file in Directory.EnumerateFiles(directory))
            {
                FileInfo fileInfo = new(file);
                filesSize += fileInfo.Length;
                files.Add(new FolderModel() { Name = file.Substring(file.LastIndexOf("\\") + 1),
                                              Size = fileInfo.Length,
                                              SubFolders = null });
            }

            subFolders.Add(new FolderModel() { Name = $"[{files.Count} Files]", 
                                               Size = filesSize,
                                               SubFolders = new ObservableCollection<FolderModel>(files)});

            FolderModel folder = new();
            string dirName = directory.Substring(directory.LastIndexOf("\\") + 1);
            folder.Name = dirName != string.Empty ? dirName : directory;
            folder.SubFolders = new ObservableCollection<FolderModel>(subFolders);

            return folder;
        }

        public static FolderModel RecursiveScan(string directory)
        {
            List<FolderModel> subFolders = new();
            IEnumerable<string> dirs;
            IEnumerable<string> files;
            FolderModel folder = new() { Name = directory };

            try
            {
                dirs = Directory.EnumerateDirectories(directory);
                files = Directory.EnumerateFiles(directory);
            }
            catch (UnauthorizedAccessException)
            {
                folder.Name += " *** ACCESS DENIED ***";
                return folder;
            }
            
            foreach (var dir in dirs)
            {
                FolderModel tempFolder = new() { Name = dir };
                tempFolder = RecursiveScan(tempFolder.Name);
                subFolders.Add(tempFolder);
                folder.Size += tempFolder.Size;
            }

            long filesSize = 0;

            foreach (var file in files)
            {
                FileInfo fileInfo = new(file);
                filesSize += fileInfo.Length;
                subFolders.Add(new FolderModel() { Name = file, //file.Substring(file.LastIndexOf("\\") + 1),
                                                   Size = fileInfo.Length,
                                                   SubFolders = null });
            }

            folder.Size += filesSize;
            folder.SubFolders = new ObservableCollection<FolderModel>(subFolders);

            return folder;
        }
    }
}
