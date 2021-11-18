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
using TreeView.Services;

namespace TreeView
{
    public partial class MainWindow : RibbonWindow
    {
        ObservableCollection<FolderModel> folders = new();

        public MainWindow()
        {
            InitializeComponent();
            ObservableCollection<DriveModel> drives = GetDriveInfo.Get();
            bSelectDirectory_gScan.ItemsSource = drives;
            //DrivesGrid.ItemsSource = drives;
        }

        private void bSelectDirectory_gScan_Click(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is RibbonMenuItem)
            {
                DriveModel drive = (DriveModel)((RibbonMenuItem)e.OriginalSource).DataContext;
                sbFreeSpace.Text = $"({drive.Name}) Free space: {drive.FreeSpace.ToString()} bytes";
                folders.Clear();
                FolderModel startFolder = new FolderModel() { Name = drive.Name, ShortName = drive.Name };
                Thread t = new Thread(new ParameterizedThreadStart(ScanDirectory.StartScanDirWithSubdirs));
                t.Start(startFolder);
                folders.Add(startFolder);
                directoryTree.ItemsSource = folders;
            }
            else if (sender is RibbonSplitButton)
            {
            }
            sbFilesNumber.Text = $"{folders[0].FilesNumber} files";
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
                        Thread t = new Thread(new ParameterizedThreadStart(ScanDirectory.StartScanDirWithSubdirs));
                        t.Start(subFolder);
                    }
                }
            }
        }

        private void directoryTree_Selected(object sender, RoutedEventArgs e)
        {
            //FolderModel folder = (FolderModel)((TreeViewItem)e.OriginalSource).DataContext;
            //FolderModel folder = (FolderModel)directoryTree.SelectedItem;
            //MessageBox.Show($"{folder.Name} {folder.Size}");
        }

        private void bRefresh_gScan_Click(object sender, RoutedEventArgs e)
        {
            directoryTree.Items.Refresh();
        }
    }
}
