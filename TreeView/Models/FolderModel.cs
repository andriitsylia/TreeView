using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeView.Models
{
    public class FolderModel
    {
        public string Name { get; set; }
        public long Size { get; set; }
        public ObservableCollection<FolderModel> SubFolders { get; set; }
    }
}
