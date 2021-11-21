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
        private string _shortName;
        private long _size;
        private string _sizeStr;
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

        public string ShortName
        {
            get
            {
                return _shortName;
            }
            set
            {
                if (value == _shortName) return;
                _shortName = value;
                OnPropertyChanged();
            }
        }
        public long Size
        {
            get
            {
                return _size;
            }
            set
            {
                if (value == _size) return;
                _size = value;
                OnPropertyChanged();
            }
        }
        public string SizeStr
        {
            get
            {
                return _sizeStr;
            }
            set
            {
                if (value == _sizeStr) return;
                _sizeStr = value;
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
                OnPropertyChanged();
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
