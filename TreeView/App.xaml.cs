using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TreeView
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs ex)
        {
            MessageBox.Show("An unhandled exception just occurred: " + ex.Exception.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            ex.Handled = true;
        }
    }
}
