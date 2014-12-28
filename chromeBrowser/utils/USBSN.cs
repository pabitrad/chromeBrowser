using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Management;
using System.Text.RegularExpressions;

namespace chromeBrowser
{
    class USBSN
    {
        string _serialNumber;
        string _driveLetter;

        public string getSerialNumberFromDriveLetter(string driveLetter)
        {
            this._driveLetter = driveLetter.ToUpper();

            if (!this._driveLetter.Contains(":"))
            {
                this._driveLetter += ":";
            }

            matchDriveLetterWithSerial();

            return this._serialNumber;
        }

        private void matchDriveLetterWithSerial()
        {
            try
            {
                string[] diskArray;
                string driveNumber;
                string driveLetter;

                System.Management.ManagementObjectSearcher searcher1 = new System.Management.ManagementObjectSearcher("SELECT * FROM Win32_LogicalDiskToPartition");
                foreach (ManagementObject dm in searcher1.Get())
                {
                    diskArray = null;
                    driveLetter = getValueInQuotes(dm["Dependent"].ToString());
                    diskArray = getValueInQuotes(dm["Antecedent"].ToString()).Split(',');
                    driveNumber = diskArray[0].Remove(0, 6).Trim();
                    if (driveLetter == this._driveLetter)
                    {
                        /* This is where we get the drive serial */
                        ManagementObjectSearcher disks = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
                        foreach (ManagementObject disk in disks.Get())
                        {

                            if (disk["Name"].ToString() == ("\\\\.\\PHYSICALDRIVE" + driveNumber) & disk["InterfaceType"].ToString() == "USB")
                            {
                                this._serialNumber = parseSerialFromDeviceID(disk["PNPDeviceID"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private string parseSerialFromDeviceID(string deviceId)
        {
            string[] splitDeviceId = deviceId.Split('\\');
            string[] serialArray;
            string serial;
            int arrayLen = splitDeviceId.Length - 1;

            serialArray = splitDeviceId[arrayLen].Split('&');
            serial = serialArray[0];

            return serial;
        }

        private string getValueInQuotes(string inValue)
        {
            string parsedValue = "";

            int posFoundStart = 0;
            int posFoundEnd = 0;

            posFoundStart = inValue.IndexOf("\"");
            posFoundEnd = inValue.IndexOf("\"", posFoundStart + 1);

            parsedValue = inValue.Substring(posFoundStart + 1, (posFoundEnd - posFoundStart) - 1);

            return parsedValue;
        }
    }
}
