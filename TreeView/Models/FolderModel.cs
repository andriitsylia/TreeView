using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TreeView.Models
{
    public enum FolderType
    {
        File,
        Folder,
        NoAccessFolder
    }

    public class FolderModel : INotifyPropertyChanged
    {
        private string _name;
        private long _size;
        private FolderType _type;
        private int _foldersNumber;
        private int _filesNumber;
        private ObservableCollection<FolderModel> _subFolder;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
            }
        }
        
        public long Size
        {
            get
            {
                if (SubFolders != null)
                {
                    _size += SubFolders.Select(sf => sf.Size).Sum();
                    //foreach (var subFolders in SubFolders)
                    //{
                    //    _size += subFolders.Size;
                    //}
                }
                return _size;
            }
            set
            {
                if (value == _size) return;
                _size = value;
                OnPropertyChanged();
            }
        }
        
        public FolderType Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        public int FoldersNumber
        {
            get
            {
                return _foldersNumber;
            }
            set
            {
                if (value == _foldersNumber) return;
                _foldersNumber = value;
                OnPropertyChanged();
            }
        }

        public int FilesNumber
        {
            get
            {
                return _filesNumber;
            }
            set
            {
                if (value == _filesNumber) return;
                _filesNumber = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<FolderModel> SubFolders
        {
            get
            {
                return _subFolder;
            }
            set
            {
                if (value == _subFolder) return;
                _subFolder = value;
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
