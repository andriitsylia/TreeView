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
        public static ObservableCollection<DriveModel> Get()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            ObservableCollection<DriveModel> driveCollection = new();
            foreach (var drive in drives)
            {
                DriveModel driveModel = new();
                driveModel.Name = drive.Name;
                driveModel.Type = drive.DriveType.ToString();
                driveModel.IsReady = drive.IsReady;
                if (drive.IsReady)
                {
                    driveModel.Format = drive.DriveFormat;
                    driveModel.Size = drive.TotalSize;
                    driveModel.FreeSpace = drive.TotalFreeSpace;
                    driveModel.VolumeLabel = drive.VolumeLabel != string.Empty ? drive.VolumeLabel: "Local Disk";
                }
                driveCollection.Add(driveModel);
            }

            return driveCollection;
        }
    }
}
