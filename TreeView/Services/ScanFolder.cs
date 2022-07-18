using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TreeView.Models;
using System.Diagnostics;

namespace TreeView.Services
{
    public class ScanFolder
    {
        private const string NO_ACCESS_FOLDER = " *** NO ACCESS ***";
        private const string SCAN_ALL_DIRECTORIES = "*";
        private const string SCAN_ALL_FILES = "*";
        private const string DIRECTORY_LEFT_SYMBOL = "[";
        private const string DIRECTORY_RIGHT_SYMBOL = "]";
        private const string DIRECTORY_SLASH = "\\";
        private static void ScanFirstLevelFolder(FolderModel folder)
        {
            try
            {
                folder.SubFolders = ScanSecondLevelFolder(folder.Name);
                if (folder.SubFolders != null)
                {
                    var listDirs = Directory.EnumerateDirectories(folder.Name, SCAN_ALL_DIRECTORIES, new EnumerationOptions() { AttributesToSkip = 0, RecurseSubdirectories = true });
                    var listFiles = Directory.EnumerateFiles(folder.Name, SCAN_ALL_FILES, new EnumerationOptions() { AttributesToSkip = 0, RecurseSubdirectories = true });
                    folder.Size = listFiles.Select(file => new FileInfo(file).Length).Sum();
                    folder.SizeStr = string.Format(new FileSizeFormatProvider(), "{0:fs}", folder.Size);
                    folder.Type = FolderType.Folder;
                    folder.FoldersNumber = listDirs.Select(dir => dir).Count();
                    folder.FilesNumber = listFiles.Select(file => file).Count();
                }
                else
                {
                    folder.ShortName += NO_ACCESS_FOLDER;
                    folder.Type = FolderType.NoAccessFolder;
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                Trace.WriteLine("*** UNAUTHORIZED ACCESS EXCEPTION ***");
                Trace.WriteLine($"{ex.GetType().Name}: {ex.Message}");
                Trace.WriteLine("*****************");
            }
            catch (ArgumentException ex)
            {
                Trace.WriteLine("*** ARGUMENT EXCEPTION ***");
                Trace.WriteLine($"{ex.GetType().Name}: {ex.Message}");
                Trace.WriteLine("*****************");
            }
        }

        private static ObservableCollection<FolderModel> ScanSecondLevelFolder(string folderName)
        {
            ObservableCollection<FolderModel> folders;
            try
            {
                var subfolders = Directory.EnumerateDirectories(folderName);
                var subfiles = Directory.EnumerateFiles(folderName);
                folders = new ObservableCollection<FolderModel>();
                foreach (var folder in subfolders)
                {
                    folders.Add(new FolderModel() { Name = folder,
                                                    ShortName = DIRECTORY_LEFT_SYMBOL + folder[(folder.LastIndexOf(DIRECTORY_SLASH) + 1)..] + DIRECTORY_RIGHT_SYMBOL,
                                                    Size = 0,
                                                    SizeStr = string.Format(new FileSizeFormatProvider(), "{0:fs}", 0),
                                                    Type = FolderType.Folder,
                                                    FoldersNumber = 0,
                                                    FilesNumber = 0,
                                                    SubFolders = null });
                }
                foreach (var file in subfiles)
                {
                    FileInfo fileInfo = new(file);
                    folders.Add(new FolderModel() { Name = file,
                                                    ShortName = file[(file.LastIndexOf(DIRECTORY_SLASH) + 1)..],
                                                    Size = fileInfo.Length,
                                                    SizeStr = string.Format(new FileSizeFormatProvider(), "{0:fs}", fileInfo.Length),
                                                    Type = FolderType.File,
                                                    FoldersNumber = 0,
                                                    FilesNumber = 0,
                                                    SubFolders = null });
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                folders = null;
                Trace.WriteLine("*** UNAUTHORIZED ACCESS EXCEPTION ***");
                Trace.WriteLine($"{ex.GetType().Name}: {ex.Message}");
                Trace.WriteLine("*****************");
            }
            catch (ArgumentException ex)
            {
                folders = null;
                Trace.WriteLine("*** ARGUMENT EXCEPTION ***");
                Trace.WriteLine($"{ex.GetType().Name}: {ex.Message}");
                Trace.WriteLine("*****************");
            }

            return folders;
        }

        public static void Scan(object folder)
        {
            ScanFirstLevelFolder((FolderModel)folder);
        }
    }
}
