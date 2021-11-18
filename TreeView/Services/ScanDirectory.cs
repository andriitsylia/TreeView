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
    public static class ScanDirectory
    {
        public static void StartScanDirWithSubdirs(object folder)
        {
            ScanDirWithSubdirs((FolderModel)folder);
        }

        public static void ScanDirWithSubdirs(FolderModel folder)
        {
            try
            {
                folder.SubFolders = GetSubDirs(folder.Name);
                if (folder.SubFolders != null)
                {
                    var listDirs = Directory.EnumerateDirectories(folder.Name, "*", new EnumerationOptions() { AttributesToSkip = 0, RecurseSubdirectories = true });
                    Thread.Sleep(200);
                    var listFiles = Directory.EnumerateFiles(folder.Name, "*", new EnumerationOptions() { AttributesToSkip = 0, RecurseSubdirectories = true });
                    Thread.Sleep(200);
                    folder.Size = listFiles.Select(file => new FileInfo(file).Length).Sum();
                    folder.Type = FolderType.Folder;
                    folder.FoldersNumber = listDirs.Select(dir => dir).Count();
                    folder.FilesNumber = listFiles.Select(file => file).Count();
                }
                else
                {
                    folder.ShortName += " *** NO ACCESS ***";
                    folder.Type = FolderType.NoAccessFolder;
                }
            }
            catch (UnauthorizedAccessException)
            {
            }
        }

        public static ObservableCollection<FolderModel> GetSubDirs(string folderName)
        {
            ObservableCollection<FolderModel> folders;
            try
            {
                var subfolders = Directory.EnumerateDirectories(folderName);
                var subfiles = Directory.EnumerateFiles(folderName);
                folders = new ObservableCollection<FolderModel>();
                foreach (var folder in subfolders)
                {
                    folders.Add(new FolderModel()
                    {
                        Name = folder,
                        ShortName = "[" + folder[(folder.LastIndexOf("\\") + 1)..] + "]",
                        Size = 0,
                        Type = FolderType.Folder,
                        FoldersNumber = 0,
                        FilesNumber = 0,
                        SubFolders = null
                    });
                }
                foreach (var file in subfiles)
                {
                    FileInfo fileInfo = new(file);
                    folders.Add(new FolderModel()
                    {
                        Name = file,
                        ShortName = file[(file.LastIndexOf("\\") + 1)..],
                        Size = fileInfo.Length,
                        Type = FolderType.File,
                        FoldersNumber = 0,
                        FilesNumber = 0,
                        SubFolders = null
                    });
                }
            }
            catch (UnauthorizedAccessException)
            {
                folders = null;
            }
            return folders;
        }
    }
}
