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
    /// Interaction logic for hackAlertSetting.xaml
    /// </summary>
    public partial class hackAlertSetting : Window
    {
        public hackAlertSetting()
        {
            InitializeComponent();
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            this.Top = (screenHeight - 203 ) / 2 ;
            this.Left = (screenWidth - 484) / 2;
            saveBTN.AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(saveBTN_MouseLeftButtonDown), true);
            
            setHackState();
        }
        
        private MainWindow parent_win;
        public void setMain(MainWindow main_win)
        {
            parent_win = main_win;
        }
        private bool isHack = true;


        public void getHackStateFromServer()
        {
            try
            {
                String logincheckurl = "https://wemagin.com/wdrive/userlogin/getHackState.php?username=" + parent_win.getUserID();
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

                if ((readBuff.Length > 0))
                {
                    if ('1' == readBuff[0])
                    {
                        isHack = false;

                    }
                    else
                    {
                        isHack = true;
                    }

                }
                setHackState();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point pos = e.GetPosition(this);
            if ((pos.X > 295) && (pos.X < 350) && (pos.Y > 90) && (pos.Y <110))
            {
                isHack = !isHack;
                setHackState();
                
            }
        }

        private void setHackState()
        {
            if (isHack)
            {
                hackON.Visibility = System.Windows.Visibility.Visible;
                hackOFF.Visibility = System.Windows.Visibility.Hidden;
                lbOn.Visibility = System.Windows.Visibility.Visible;
                lbOFF.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                hackON.Visibility = System.Windows.Visibility.Hidden ;
                hackOFF.Visibility = System.Windows.Visibility.Visible;
                lbOn.Visibility = System.Windows.Visibility.Hidden ;
                lbOFF.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void minClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void saveBTN_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                UsbUtils usbUTILS = new UsbUtils();
                String usbnamespace = usbUTILS.getUSBDriver();
                String sn = "";
                String driver_name = usbnamespace.Substring(0, 1);
                USBSN us = new USBSN();
                sn = us.getSerialNumberFromDriveLetter(driver_name);
                String logincheckurl = "https://wemagin.com/wdrive/userlogin/updateemail.php?usbid=" + sn + "&email=" + parent_win.getUserID() + "&alertstate=";
                if (this.isHack)
                {
                    logincheckurl += "0";
                }
                else
                {
                    logincheckurl += "1";
                }
                HttpWebRequest myHttpWebRequest1 = (HttpWebRequest)WebRequest.Create(logincheckurl);
                myHttpWebRequest1.KeepAlive = false;
                HttpWebResponse myHttpWebResponse1 = (HttpWebResponse)myHttpWebRequest1.GetResponse();
                myHttpWebResponse1.Close();
            }
            catch (Exception ex)
            {

            }
            this.Close();
        }


    }
}
