using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Net;
using System.IO;


namespace chromeBrowser
{
    /// <summary>
    /// Interaction logic for forgotPass.xaml
    /// </summary>
    public partial class forgotPass : Window
    {

        private login logWin;

        public forgotPass()
        {
            InitializeComponent();
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            this.Left = (screenWidth - this.Width) / 2;
            this.Top = (screenHeight - this.Height) / 2;

            yesBtn.AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(yesBtn_MouseDown), true);
            noBtn.AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(noBtn_MouseDown), true);

        }

        public void setParentWindow(login win)
        {
            logWin = win;
        }

        private void yesBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            login log_win = new login();
            String sn = "";
            UsbUtils usbUTILS = new UsbUtils();
            String usbnamespace = usbUTILS.getUSBDriver();
            try
            {
                if (usbnamespace.Length > 0)
                {
                    String driver_name = usbnamespace.Substring(0, 1);
                    USBSN us = new USBSN();
                    sn = us.getSerialNumberFromDriveLetter(driver_name);


                    String usbcheckurl = "https://wemagin.com/wdrive/userlogin/forgotPass.php?usbID=" + sn;

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
                    myHttpWebResponse1.Close();
                    log_win.Show();
                    this.Close();
                }
                else
                {
                    log_win.Show();
                    this.Close();
                    return;
                }
            }
            catch (Exception exx)
            {

                log_win.Show();
                this.Close();
                return;
            }

            log_win.Show();
            this.Close();

        }

        private void noBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            login log_win = new login();
            log_win.Show();
            this.Close();

        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            login log_win = new login();
            log_win.Show();
            this.Close();
        }
    }
}
