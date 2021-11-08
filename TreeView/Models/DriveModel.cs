using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeView.Models
{
    public class DriveModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Format { get; set; }
        public long Size { get; set; }
        public long FreeSpace { get; set; }
        public string VolumeLabel { get; set; }
        public bool IsReady { get; set; }
    }
}
