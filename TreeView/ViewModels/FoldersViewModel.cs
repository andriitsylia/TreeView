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
    public class FoldersViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<FolderModel> _folders;

        public FoldersViewModel()
        {
            _folders = new ObservableCollection<FolderModel>();
        }

        public FoldersViewModel(ObservableCollection<FolderModel> folders)
        {
            _folders = folders ?? throw new ArgumentNullException(nameof(folders), "Received a null argument");
        }

        public ObservableCollection<FolderModel> Folders
        { 
            get
            {
                return _folders;
            }
            set
            {
                if (value == _folders) return;
                _folders = value;
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
