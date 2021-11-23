using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeView.Models;

namespace TreeView.Services
{
    public static class GetDriveInfo
    {
        private const string NO_NAME_DRIVE_VOLUME_LABEL = "Local Disk";
        public static ObservableCollection<DriveModel> Get()
        {
            var result = new ObservableCollection<DriveModel>();
            foreach (var drive in DriveInfo.GetDrives())
            {
                result.Add(new DriveModel () { Name = drive.Name,
                                               Type = drive.DriveType.ToString(),
                                               IsReady = drive.IsReady,
                                               Format = drive.IsReady ? drive.DriveFormat : null,
                                               Size = drive.IsReady ? drive.TotalSize : 0,
                                               FreeSpace = drive.IsReady ? drive.TotalFreeSpace : 0,
                                               VolumeLabel = drive.IsReady ? drive.VolumeLabel != string.Empty ? drive.VolumeLabel : NO_NAME_DRIVE_VOLUME_LABEL : null });
            }
            return result;
        }
    }
}
