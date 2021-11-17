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
            //FolderModel tempFolder = (FolderModel)folder;
            ScanDirWithSubdirs((FolderModel)folder);
        }
        public static void ScanDirWithSubdirs(FolderModel folder)
        {
            IEnumerable<string> listDirs;
            IEnumerable<string> listFiles;

            try
            {
                //IEnumerable<string> listDirs = Directory.EnumerateDirectories(folder.Name, "", new EnumerationOptions() { RecurseSubdirectories = true });
                //IEnumerable<string> listFiles = Directory.EnumerateFiles(folder.Name, "*.*", new EnumerationOptions() { RecurseSubdirectories = true });
                listDirs = Directory.EnumerateDirectories(folder.Name, "", SearchOption.AllDirectories);
                listFiles = Directory.EnumerateFiles(folder.Name, "*.*", SearchOption.AllDirectories);
                folder.Size = (from file in listFiles select new FileInfo(file).Length).Sum();
                folder.FoldersNumber = listDirs.Select(dir => dir).Count();
                folder.FilesNumber = listFiles.Select(file => file).Count();
                folder.SubFolders = new ObservableCollection<FolderModel>() { new FolderModel() { Name = "*" } };
                Thread.Sleep(200);
            }
            catch (UnauthorizedAccessException)
            {

            }

            //folder.Size = (from file in listFiles select new FileInfo(file).Length).Sum();
            //folder.FoldersNumber = listDirs.Select(dir => dir).Count();
            //folder.FilesNumber = listFiles.Select(file => file).Count();
            //folder.SubFolders = new ObservableCollection<FolderModel>() { new FolderModel() { Name = "*" } };
            //Thread.Sleep(200);


            //IEnumerable<string> listFiles = Directory.EnumerateFileSystemEntries(directory, "*.*", SearchOption.AllDirectories);
            //int counterDirs = listDirs.Select(dir => dir).Count();
            //int counterFiles = listFiles.Select(file => file).Count();
            //long sizeFiles = (from file in listFiles select new FileInfo(file).Length).Sum();
            //foreach (var file in list)
            //{
            //    FileInfo fi = new FileInfo(file);
            //    filessize += fi.Length;
            //    ++counter;
            //    //Console.WriteLine($"{++counter}. {file}");
            //}
            //return new FolderModel()
            //{
            //    Name = directory,
            //    Size = sizeFiles,
            //    Type = FolderType.Folder,
            //    FoldersNumber = counterDirs,
            //    FilesNumber = counterFiles,
            //    SubFolders = new ObservableCollection<FolderModel>() { new FolderModel() { Name = "*" } }
            //};
        }
        
        public static ObservableCollection<FolderModel> GetSubDirs(FolderModel folder)
        {
            //IEnumerable<string> subfolders = Directory.EnumerateDirectories(folder.Name, "", new EnumerationOptions() { IgnoreInaccessible = true});
            ObservableCollection<FolderModel> folders;
            try
            {
                IEnumerable<string> subfolders = Directory.EnumerateDirectories(folder.Name);
                IEnumerable<string> subfiles = Directory.EnumerateFiles(folder.Name);
                folders = new ObservableCollection<FolderModel>();
                foreach (var item in subfolders)
                {
                    folders.Add(new FolderModel() { Name = item, 
                                                    Type = FolderType.Folder, 
                                                    SubFolders = new ObservableCollection<FolderModel>() { new FolderModel() { Name = "*" } } });
                }
                foreach (var file in subfiles)
                {
                    FileInfo fileInfo = new(file);
                    folders.Add(new FolderModel()
                    {
                        Name = file,
                        Size = fileInfo.Length,
                        Type = FolderType.File,
                        SubFolders = null
                    });
                }
                //folder.FoldersNumber = subfolders.Select(sfolders => sfolders).Count();
                //folder.FilesNumber = subfiles.Select(sfile => sfile).Count();
            }
            catch (UnauthorizedAccessException)
            {
                folder.Name += " *** ACCESS DENIED ***";
                //folder.Size = 0;
                folder.Type = FolderType.NoAccessFolder;
                //folder.FoldersNumber = 0;
                //folder.FilesNumber = 0;
                //folder.SubFolders = null;
                folders = null;
            }
            
            return folders;
        }

        public static FolderModel Scan(FolderModel folder)
        {
            return Scan(folder.Name);
        }
        public static FolderModel Scan(string directory)
        {
            IEnumerable<string> dirs;
            IEnumerable<string> files;
            List<FolderModel> subFolders = new();

            //string dirName = directory.Substring(directory.LastIndexOf("\\") + 1);
            FolderModel folder = new() {Name = directory };//Name = string.IsNullOrEmpty(dirName) ? directory : dirName };

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
                subFolders.Add(new FolderModel() { Name = dir, Type = FolderType.Folder });
            }

            long filesSize = 0;

            foreach (var file in files)
            {
                FileInfo fileInfo = new(file);
                filesSize += fileInfo.Length;
                subFolders.Add(new FolderModel() { Name = file.Substring(file.LastIndexOf("\\") + 1),
                                                   Size = fileInfo.Length,
                                                   Type = FolderType.File,
                                                   SubFolders = null });
            }

            folder.Size += filesSize;
            folder.Type = FolderType.Folder;
            folder.SubFolders = new ObservableCollection<FolderModel>(subFolders);
            return folder;
        }

        public static void StartScan(object folder)
        {
            RecursiveScan((FolderModel)folder);
        }

        //public static void RecursiveScan(FolderModel folder)
        //{
        //    RecursiveScan(folder.Name);
        //}

        public static void RecursiveScan(FolderModel folder)//string directory)
        {
                List<FolderModel> subFolders = new();
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
                    FolderModel tempFolder = new() { Name = dir, Type = FolderType.Folder };// = new() { Name = dir.Substring(dir.LastIndexOf("\\") + 1), };
                    Thread.Sleep(300);
                    RecursiveScan(tempFolder); //, tempDepth
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
                    subFolders.Add(new FolderModel()
                    {
                        Name = file.Substring(file.LastIndexOf("\\") + 1),
                        Size = fileInfo.Length,
                        Type = FolderType.File,
                        SubFolders = null
                    });
                }

                folder.Size += filesSize;
                folder.Type = FolderType.Folder;
                folder.SubFolders = new ObservableCollection<FolderModel>(subFolders);

                //return folder;
            //}
            //else
            //{
            //    return folder;
            //}
        }
    }
}
