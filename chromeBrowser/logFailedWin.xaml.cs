using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.IO;

namespace chromeBrowser
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class logFailedWin : Window
    {
        public logFailedWin()
        {
            InitializeComponent();
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            this.Left = (screenWidth - this.Width) / 2;
            this.Top = (screenHeight - this.Height) / 2;
            retBtn.AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(loginBtn_MouseDown), true);
        }


        public void sendAlertEmailFromUSB(String usbsn)
        {

            try
            {
                String local = new WebClient().DownloadString("http://www.iplocation.net");  //new WebClient().DownloadString("http://api.hostip.info/country.php");
                String ipAddress = "";               
                int start_index = local.IndexOf("Your IP Address");
                int end_index = 0;
                ipAddress = local.Substring(start_index, 70);
                start_index = ipAddress.IndexOf("'green'>");
                end_index = ipAddress.IndexOf("</font>");
                ipAddress = ipAddress.Substring(start_index + 8, (end_index - start_index - 8));  
                
                
                
                start_index = local.IndexOf("IPligence</a>");
                end_index = local.IndexOf("Labs</a>");
                if ((start_index < 0) || (end_index < 0))
                    return;
                local = local.Substring(start_index, end_index - start_index);
                start_index = local.IndexOf("http://maps.google.com/maps?q=");
                local = local.Substring(start_index, local.Length - start_index);
                start_index = local.IndexOf("' target='");
                local = local.Substring(0, start_index);
                String logincheckurl = "https://wemagin.com/wdrive/userlogin/sendAlertFromUsbByPass.php?usbsn=" + usbsn + "&url=" + local + "&ip=" + ipAddress; 
                HttpWebRequest myHttpWebRequest1 = (HttpWebRequest)WebRequest.Create(logincheckurl);
                myHttpWebRequest1.KeepAlive = false;
                HttpWebResponse myHttpWebResponse1 = (HttpWebResponse)myHttpWebRequest1.GetResponse();
                Stream streamResponse = myHttpWebResponse1.GetResponseStream();
                StreamReader streamRead = new StreamReader(streamResponse);
                streamResponse.Close();
                streamRead.Close();
                myHttpWebResponse1.Close();

            }
            catch (Exception exx)
            {
                System.Windows.Forms.MessageBox.Show(exx.ToString());
            }
        }



        public void sendAlertEmail(String user_id)
        {

            String sn = "";
            UsbUtils usbUTILS = new UsbUtils();
            String usbnamespace = usbUTILS.getUSBDriver();
            if (usbnamespace.Length > 0)
            {
                String driver_name = usbnamespace.Substring(0, 1);
                USBSN us = new USBSN();
                sn = us.getSerialNumberFromDriveLetter(driver_name);
                sendAlertEmailFromUSB(sn);
            }         



        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //this.WindowState = WindowState.Minimized;

        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            var newWindow = new login();
            newWindow.Show();
            this.Close();


        }

        private void loginBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var newWindow = new login();
            newWindow.Show();
            this.Close();
        }

        private void retBtn_MouseEnter(object sender, MouseEventArgs e)
        {

            //retBtn.Background.Opacity = 0;
            //retBtn.BorderThickness = new Thickness(0.0);

            // Color clr = Color.FromRgb(240, 78, 37);
            // retBtn.Foreground = new SolidColorBrush(clr);
        }

        private void retBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            //Color clr = Color.FromRgb(240, 78, 37);
            //retBtn.Background = new SolidColorBrush(clr); 
            //retBtn.BorderThickness = new Thickness(1.0);

            //Color txt_clr = Color.FromRgb(255, 255, 255);
            // retBtn.Foreground = new SolidColorBrush(txt_clr);
        }

        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var newWindow = new login();
                newWindow.Show();
                this.Close();
            }
        }
    }
}
