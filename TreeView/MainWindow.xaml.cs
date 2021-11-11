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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        ObservableCollection<FolderModel> folders;
        public MainWindow()
        {
            InitializeComponent();
            ObservableCollection<DriveModel> drives = GetDriveInfo.Get();
            bSelectDirectory_gScan.ItemsSource = drives;

            //Thread t = Thread.CurrentThread;

           ///MessageBox.Show($"Имя потока: {t.Name}\r\n"
           //               + $"Запущен ли поток: {t.IsAlive}\r\n"
           //               + $"Приоритет потока: {t.Priority}\r\n"
           //               + $"Статус потока: {t.ThreadState}\r\n"
           //               + $"Домен приложения: {Thread.GetDomain().FriendlyName}");

        }

        private void bSelectDirectory_gScan_Click(object sender, RoutedEventArgs e)
        {
            //Thread t = new Thread(new ParameterizedThreadStart(StartScan));
            if (e.OriginalSource is RibbonMenuItem)
            {
                DriveModel drive = (DriveModel)((RibbonMenuItem)e.OriginalSource).DataContext;
                //MessageBox.Show(drive.Name);
                //t.Start(drive.Name);
                StartScan(drive.Name);
                //ObservableCollection<FolderModel> folders = new() { ScanDirectory.RecursiveScan(drive.Name) };
                //directoryTree.ItemsSource = folders;
            }
            else if (sender is RibbonSplitButton)
            {
                MessageBox.Show("Click!");
            }

        }

        private void StartScan(object directory)
        {
            
            folders = new() { ScanDirectory.RecursiveScan((string)directory, 2) };
            directoryTree.ItemsSource = folders;
        }

        private void FolderExpand(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }

        private void directoryTree_Expanded(object sender, RoutedEventArgs e)
        {
            //FolderModel folder = (FolderModel)((TreeViewItem)e.OriginalSource).DataContext;
            //MessageBox.Show($"{folder.Name}");
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
    }
}
