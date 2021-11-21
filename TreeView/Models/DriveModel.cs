using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TreeView.Models
{
    public class DriveModel : INotifyPropertyChanged
    {
        private string _name;
        private string _type;
        private string _format;
        private long _size;
        private long _freeSpace;
        private string _volumeLabel;
        private bool _isReady;
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

        public string Type
        {
            get
            {
                return _type;
            }
            set
            {
                if (value == _type) return;
                _type = value;
                OnPropertyChanged();
            }
        }
        public string Format
        {
            get
            {
                return _format;
            }
            set
            {
                if (value == _format) return;
                _format = value;
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
        public long FreeSpace
        {
            get
            {
                return _freeSpace;
            }
            set
            {
                if (value == _freeSpace) return;
                _freeSpace = value;
                OnPropertyChanged();
            }
        }
        public string VolumeLabel
        {
            get
            {
                return _volumeLabel;
            }
            set
            {
                if (value == _volumeLabel) return;
                _volumeLabel = value;
                OnPropertyChanged();
            }
        }
        public bool IsReady
        {
            get
            {
                return _isReady;
            }
            set
            {
                if (value == _isReady) return;
                _isReady = value;
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
