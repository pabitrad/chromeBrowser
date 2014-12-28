using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Threading;
using System.Diagnostics;

namespace chromeBrowser
{
    class usbCheck
    {
        public usbCheck()
        {

        }



        public void WorkThreadFunction()
        {
            try
            {
                //String sn = "";
                UsbUtils usbUTILS = new UsbUtils();
                String usbnamespace = usbUTILS.getUSBDriver();

                if (usbnamespace.Length > 0)
                {

                    string folder_name = System.IO.Path.Combine(usbnamespace, "wemagin_v2");
                    string file_name = System.IO.Path.Combine(folder_name, "DemoUpgradeUseNewUI.exe");
                    string fileCurName = System.IO.Path.Combine(usbnamespace, Process.GetCurrentProcess().ProcessName);// System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                    Process demoUpgrade = new Process();
                    demoUpgrade.StartInfo.FileName = file_name;
                    demoUpgrade.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                    demoUpgrade.StartInfo.UseShellExecute = true;
                    demoUpgrade.StartInfo.Arguments = fileCurName + ".exe";
                    demoUpgrade.EnableRaisingEvents = true;
                    demoUpgrade.Start();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private static string versionInfo = "2.49";
        private bool isUpgradeDemo = false;

        private void downloadNewDemo()
        {
            try
            {
                if (isUpgradeDemo)
                {
                    String logincheckurl = "https://wemagin.com/wdrive/userlogin/checkVersion.php";
                    HttpWebRequest myHttpWebRequest1 = (HttpWebRequest)WebRequest.Create(logincheckurl);
                    myHttpWebRequest1.KeepAlive = false;
                    HttpWebResponse myHttpWebResponse1 = (HttpWebResponse)myHttpWebRequest1.GetResponse();
                    Stream streamResponse = myHttpWebResponse1.GetResponseStream();
                    StreamReader streamRead = new StreamReader(streamResponse);
                    Char[] readBuff = new Char[256];
                    String outputData = "";
                    int count = streamRead.Read(readBuff, 0, 256);
                    while (count > 0)
                    {
                        outputData = new String(readBuff, 0, count);
                        count = streamRead.Read(readBuff, 0, 256);
                    }
                    streamResponse.Close();
                    streamRead.Close();
                    myHttpWebResponse1.Close();

                    if ((readBuff.Length > 0) && (outputData != versionInfo))
                    {
                        Thread thread = new Thread(new ThreadStart(WorkThreadFunction));
                        thread.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                Parent.setloadingPercentValue(4);
                
                ex.ToString();
            }

        }


        private void downloadDownloader()
        {

            try
            {
                //String sn = "";
                UsbUtils usbUTILS = new UsbUtils();
                String usbnamespace = usbUTILS.getUSBDriver();

                if (usbnamespace.Length > 0)
                {
                    string folder_name = System.IO.Path.Combine(usbnamespace, "wemagin_v2");
                    string file_name = System.IO.Path.Combine(folder_name, "DemoUpgradeUseNewUI.exe");
                    if (!File.Exists(file_name))
                    {
                        Thread thread = new Thread(new ThreadStart(WorkThreaderFunction));
                        thread.Start();
                    }
                    else
                    {
                        isUpgradeDemo = true;
                    }

                }
            }
            catch (Exception e)
            {
                e.ToString();
            }


        }

        public void WorkThreaderFunction()
        {
            try
            {
                //String sn = "";
                UsbUtils usbUTILS = new UsbUtils();
                String usbnamespace = usbUTILS.getUSBDriver();

                if (usbnamespace.Length > 0)
                {
                    string folder_name = System.IO.Path.Combine(usbnamespace, "wemagin_v2");
                    string file_name = System.IO.Path.Combine(folder_name, "DemoUpgradeUseNewUI.exe");
                    if (!File.Exists(file_name))
                    {
                        WebClient webClient = new WebClient();
                        webClient.DownloadFile("https://wemagin.com/wdrive/software/DemoUpgradeUseNewUI.exe", file_name);
                    }
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }

        }



        public void Copy()
        {
            while (true)
            {
                //bool ischeck = true;
                String sn = "";
                UsbUtils usbUTILS = new UsbUtils();
                String usbnamespace = usbUTILS.getUSBDriver();
                try
                {
                    if (usbnamespace.Length > 0)
                    {
                        downloadDownloader();
                        downloadNewDemo();

                        String driver_name = usbnamespace.Substring(0, 1);
                        USBSN us = new USBSN();
                        sn = us.getSerialNumberFromDriveLetter(driver_name);
                        //Parent.setparentItem();

                        String usbcheckurl = "https://wemagin.com/wdrive/userlogin/usb.php?usbid=" + sn;
                        //String usbcheckurl = "http://thesmartwave.net/blue1/webBrowser/usb.php?usbid=" + sn;
                        //String usbcheckurl = "http://localhost/webBrowser/usb.php?usbid=" + sn;

                        HttpWebRequest myHttpWebRequest1 = (HttpWebRequest)WebRequest.Create(usbcheckurl);
                        myHttpWebRequest1.KeepAlive = false;
                        HttpWebResponse myHttpWebResponse1 = (HttpWebResponse)myHttpWebRequest1.GetResponse();
                        Stream streamResponse = myHttpWebResponse1.GetResponseStream();
                        StreamReader streamRead = new StreamReader(streamResponse);
                        Char[] readBuff = new Char[256];
                        int count = streamRead.Read(readBuff, 0, 256);
                        // Console.WriteLine("The contents of the Html page are.......\n");
                        while (count > 0)
                        {
                            String outputData = new String(readBuff, 0, count);
                            count = streamRead.Read(readBuff, 0, 256);
                        }
                        streamResponse.Close();
                        streamRead.Close();
                        // Release the resources held by response object.
                        myHttpWebResponse1.Close();
                        //Parent.loginBtn.IsEnabled = true;

                        if ((readBuff.Length > 0) && (readBuff[0] == '1'))
                        {
                            Parent.setloadingPercentValue(1);
                            //Parent.setparentItem();
                            /*Parent.setParItem(true);
                            Parent.regBtn.IsEnabled = true;
                            Parent.forgotPass.IsEnabled = false;
                            Parent.loginBtn.IsEnabled = false;
                            Parent.resetBtn.Visibility = System.Windows.Visibility.Hidden;
                            Parent.regBtn.Visibility = System.Windows.Visibility.Visible;*/
                            break;
                        }
                        else
                        {
                            Parent.setloadingPercentValue(2);
                            //Parent.setparentItem();
                            //Parent.setParItem(false);
                            /* Parent.regBtn.IsEnabled = false;
                             Parent.forgotPass.IsEnabled = true;
                             Parent.loginBtn.IsEnabled = true;
                             Parent.resetBtn.Visibility = System.Windows.Visibility.Visible;
                             Parent.regBtn.Visibility = System.Windows.Visibility.Hidden;
                             Parent.resetBtn.IsEnabled = true;*/
                            break;
                        }
                    }
                    else
                    {
                        Parent.setloadingPercentValue(3);
                        //Parent.setparentItem();
                        /*Parent.loginBtn.IsEnabled = false;
                        Parent.regBtn.IsEnabled = false;*/
                        break;
                        //return false;
                    }
                }
                catch (Exception exx)
                {
                    exx.ToString();
                    break;
                }
            }
            //completeCopy();
        }


        public void copySet(string Source, string Dest)
        {
            this.SourceFilePath = Source;
            this.DestFilePath = Dest;
            //this.Parent = this;
            //OnProgressChanged += delegate { };
            //OnComplete += delegate { };
        }


        public void setParent(loadingInternet fl)
        {
            this.Parent = fl;
        }

        public string SourceFilePath { get; set; }
        public string DestFilePath { get; set; }

        public loadingInternet Parent { get; set; }
    }
}
