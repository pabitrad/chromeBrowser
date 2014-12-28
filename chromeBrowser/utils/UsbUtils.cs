using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace chromeBrowser
{
    class UsbUtils
    {
        private DriveInfo usb_drv = null;
        public String getUSBDriver()
        {
            String driver_Name = "";

            try
            {

                DriveInfo[] allDrives = DriveInfo.GetDrives();
                foreach (DriveInfo d in allDrives)
                {
                    // Console.WriteLine("Drive {0}", d.Name);
                    //  Console.WriteLine("  File type: {0}", d.DriveType);

                    if ((d.IsReady == true) && (d.DriveType.ToString() == "Removable")  && (d.DriveFormat != "FAT"))
                    {
                        usb_drv = d;
                        return d.Name;
                    }
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }
            return driver_Name;
        }

        public String getFireFoxDriver()
        {
            String driver_Name = "";

            try
            {

                DriveInfo[] allDrives = DriveInfo.GetDrives();
                foreach (DriveInfo d in allDrives)
                {
                    // Console.WriteLine("Drive {0}", d.Name);
                    //  Console.WriteLine("  File type: {0}", d.DriveType);

                    if ((d.IsReady == true) && (d.DriveType.ToString() == "Removable") && (d.DriveFormat == "FAT"))
                    {
                        //usb_drv = d;
                        return d.Name;
                    }
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }
            return driver_Name;
        }


        public Double getUSBSize()
        {
            return Convert.ToDouble(usb_drv.TotalSize / 1000000000.0);
        }

        public Double getUsedSize()
        {
            return Convert.ToDouble((usb_drv.TotalSize - usb_drv.AvailableFreeSpace) / 1000000000.0);
        }


    }
}
