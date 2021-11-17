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
    public enum SimpleFolderType
    {
        File,
        Folder,
        NoAccessFolder
    }

    public class SimpleFolderModel
    {
        public string Name { get; set; }
        public long Size { get; set; }
        public FolderType Type { get; set; }
        public List<SimpleFolderModel> SubFolders { get; set; }
    }
}
