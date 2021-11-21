using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TreeView.Models;
using TreeView.ViewModels;
using TreeView.Services;

namespace TreeView
{
    public partial class MainWindow : RibbonWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DrivesViewModel drives = new DrivesViewModel();
            drives.Drives = GetDriveInfo.Get();
            bSelectDirectory_gScan.ItemsSource = drives.Drives;
        }

        private void bSelectDirectory_gScan_Click(object sender, RoutedEventArgs e)
        {
            FoldersViewModel folders;
            if (e.OriginalSource is RibbonMenuItem)
            {
                DriveModel drive = (DriveModel)((RibbonMenuItem)e.OriginalSource).DataContext;
                sbFreeSpace.Text = string.Format(new FileSizeFormatProvider(), " ({0}) free space: {1:fs}", drive.Name, drive.FreeSpace);
                folders = new FoldersViewModel();
                FolderModel startFolder = new FolderModel() { Name = drive.Name, ShortName = drive.Name };
                Thread t = new Thread(new ParameterizedThreadStart(ScanFolder.Scan));
                t.Start(startFolder);
                folders.Folders.Add(startFolder);
                directoryTree.ItemsSource = folders.Folders;
            }
            else if (sender is RibbonSplitButton)
            {
            }
        }

        private void directoryTree_Expanded(object sender, RoutedEventArgs e)
        {
            FolderModel folder = (FolderModel)((TreeViewItem)e.OriginalSource).DataContext;
            if (folder.SubFolders != null)
            {
                foreach (var subFolder in folder.SubFolders)
                {
                    if (subFolder.Type != FolderType.File)
                    {
                        Thread t = new Thread(new ParameterizedThreadStart(ScanFolder.Scan));
                        t.Start(subFolder);
                    }
                }
            }
        }

        private void RibbonApplicationMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
