using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TreeView.Models;

namespace TreeView.ViewModels
{
    public class DrivesViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<DriveModel> _drives;
        
        public DrivesViewModel()
        {
            _drives = new ObservableCollection<DriveModel>();
        }

        public DrivesViewModel(ObservableCollection<DriveModel> drives)
        {
            _drives = drives ?? throw new ArgumentNullException(nameof(drives), "Received a null argument");
        }
        public ObservableCollection<DriveModel> Drives
        {
            get
            {
                return _drives;
            }
            set
            {
                if (value == _drives) return;
                _drives = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
