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
        List<SimpleFolderModel> simpleFolders;

        public MainWindow()
        {
            InitializeComponent();
            ObservableCollection<DriveModel> drives = GetDriveInfo.Get();
            bSelectDirectory_gScan.ItemsSource = drives;
        }

        private void bSelectDirectory_gScan_Click(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is RibbonMenuItem)
            {
                DriveModel drive = (DriveModel)((RibbonMenuItem)e.OriginalSource).DataContext;
                StartScan(drive.Name);
                //directoryTree.ItemsSource = folders;
                //Thread.Sleep(300);
                folders.Clear();
                FolderModel folder = new FolderModel() { Name = drive.Name, Type = FolderType.Folder, SubFolders = new ObservableCollection<FolderModel>() { new FolderModel() { Name = "*" } } };
                folders.Add(folder);
                directoryTree.ItemsSource = folders;

                //foreach (var subFolder in folders[0].SubFolders)
                //{
                //    if (subFolder.Type != FolderType.File)
                //    {
                //        Thread t = new Thread(new ParameterizedThreadStart(ScanDirectory.StartScan));
                //        t.Start(subFolder);
                //    }
                //}
            }
            else if (sender is RibbonSplitButton)
            {
                //directoryTree.ItemsSource = simpleFolders;
            }
        }

        private void StartScan(object directory)
        {
            //folders = new() { ScanDirectory.Scan((string)directory) };
        }

        private void FolderExpand(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            
        }

        private void directoryTree_Expanded(object sender, RoutedEventArgs e)
        {
            FolderModel folder = (FolderModel)((TreeViewItem)e.OriginalSource).DataContext;
            folder.SubFolders = ScanDirectory.GetSubDirs(folder);
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

        private void directoryTree_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void directoryTree_Collapsed(object sender, RoutedEventArgs e)
        {
            
        }

        private void directoryTree_Selected(object sender, RoutedEventArgs e)
        {
            //FolderModel folder = (FolderModel)((TreeViewItem)e.OriginalSource).DataContext;
            //FolderModel folder = (FolderModel)directoryTree.SelectedItem;
            //MessageBox.Show($"{folder.Name} {folder.Size}");
        }

        private void RibbonWindow_MouseMove(object sender, MouseEventArgs e)
        {
            this.Title = e.GetPosition(this).ToString();
        }

        private void bRefresh_gScan_Click(object sender, RoutedEventArgs e)
        {
            //directoryTree.ItemsSource = simpleFolders;
            directoryTree.Items.Refresh();
        }

        private void bStopScan_gScan_Click(object sender, RoutedEventArgs e)
        {
            //simpleFolders = new List<SimpleFolderModel>() { SimpleScanDirectory.SimpleScan("f:\\transcend") };
            //foreach (var subFolder in simpleFolders[0].SubFolders)
            //{
            //    if (subFolder.Type != FolderType.File)
            //    {
            //        Thread t = new Thread(new ParameterizedThreadStart(SimpleScanDirectory.SimpleStartScan));
            //        t.Start(subFolder);
            //    }
            //}
            //MessageBox.Show("Threads are lunch!");
            //directoryTree.ItemsSource = simpleFolders;
        }
    }
}
