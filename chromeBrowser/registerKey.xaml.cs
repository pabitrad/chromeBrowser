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
    /// Interaction logic for registerKey.xaml
    /// </summary>
    public partial class registerKey : Window
    {
        public registerKey()
        {
            InitializeComponent();
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            this.Left = (screenWidth - this.Width) / 2;
            this.Top = (screenHeight - this.Height) / 2;
            usersubmit.AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(usersubmit_MouseLeftButtonDown), true);
            
        }

        private void closeWin(object sender, MouseButtonEventArgs e)
        {
            var newWindow = new login();
            newWindow.Show();
            this.Close();
        }

        private void userName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((keyName.Text == "ENTER WDRIVE REGISTRATION KEY HERE") || (keyName.Text == "Invalid Key"))
                keyName.Text = "";
            //Color clr = Color.FromRgb(0, 0, 0);//240, 78, 37);
            //keyName.Foreground = new SolidColorBrush(clr);
        }

        private void userName_MouseEnter(object sender, MouseEventArgs e)
        {
            if ((keyName.Text == "ENTER WDRIVE REGISTRATION KEY HERE") || (keyName.Text == "Invalid Key"))
                keyName.Text = "";
            //Color clr = Color.FromRgb(0, 0, 0);//240, 78, 37);
            //keyName.Foreground = new SolidColorBrush(clr);
            keyName.Focus();
        }

        private void userName_MouseLeave(object sender, MouseEventArgs e)
        {
            if (keyName.Text == "")
                keyName.Text = "ENTER WDRIVE REGISTRATION KEY HERE";
        }

        private void usersubmit_MouseEnter(object sender, MouseEventArgs e)
        {
            Color clr = Color.FromRgb(122, 157, 147);//240, 78, 37);
            //usersubmit.Background.Opacity = 0;
            //usersubmit.Background = new SolidColorBrush(clr);
        }

        private void usersubmit_MouseLeave(object sender, MouseEventArgs e)
        {
            Color clr = Color.FromRgb(240, 78, 37);
            //usersubmit.Background = new SolidColorBrush(clr);
        }

        private void usersubmit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((keyName.Text == "ENTER WDRIVE REGISTRATION KEY HERE") || (keyName.Text == "") || (keyName.Text == "Invalid Key"))
                return;
            String logincheckurl = "https://wemagin.com/wdrive/userlogin/checkSerial.php/?serial=" + keyName.Text;            
            HttpWebRequest myHttpWebRequest1 = (HttpWebRequest)WebRequest.Create(logincheckurl);
            myHttpWebRequest1.KeepAlive = false;
             HttpWebResponse myHttpWebResponse1 = (HttpWebResponse)myHttpWebRequest1.GetResponse();
            Stream streamResponse = myHttpWebResponse1.GetResponseStream();
            StreamReader streamRead = new StreamReader(streamResponse);
            Char[] readBuff = new Char[256];
            int count = streamRead.Read(readBuff, 0, 256);
            while (count > 0)
            {
                String outputData = new String(readBuff, 0, count);      
                count = streamRead.Read(readBuff, 0, 256);
            }

            streamResponse.Close();
            streamRead.Close();
            // Release the resources held by response object.
            myHttpWebResponse1.Close();
            //this.Close();

            String result = new string(readBuff);
            if ((result.IndexOf("0")) >= 0)
            {
                var regist  = new regist();
                regist.setUserKey(keyName.Text);
                regist.Show();
                this.Close();
            }
            else 
            {
                Color clr = Color.FromRgb(122, 157, 147);//240, 78, 37);
                keyName.Foreground = new SolidColorBrush(clr);
                keyName.Text = "Invalid Key";
            }

           
        }

        private void keyName_KeyDown(object sender, KeyEventArgs e)
        {
            Color clr = Color.FromRgb(0, 0, 0);//240, 78, 37);
            keyName.Foreground = new SolidColorBrush(clr);
            if (e.Key == Key.Enter)
            {
                if ((keyName.Text == "ENTER WDRIVE REGISTRATION KEY HERE") || (keyName.Text == "") || (keyName.Text == "Invalid Key"))
                    return;
                String logincheckurl = "https://wemagin.com/wdrive/userlogin/checkSerial.php/?serial=" + keyName.Text;
                HttpWebRequest myHttpWebRequest1 = (HttpWebRequest)WebRequest.Create(logincheckurl);
                myHttpWebRequest1.KeepAlive = false;
                HttpWebResponse myHttpWebResponse1 = (HttpWebResponse)myHttpWebRequest1.GetResponse();
                Stream streamResponse = myHttpWebResponse1.GetResponseStream();
                StreamReader streamRead = new StreamReader(streamResponse);
                Char[] readBuff = new Char[256];
                int count = streamRead.Read(readBuff, 0, 256);
                while (count > 0)
                {
                    String outputData = new String(readBuff, 0, count);
                    count = streamRead.Read(readBuff, 0, 256);
                }

                streamResponse.Close();
                streamRead.Close();
                // Release the resources held by response object.
                myHttpWebResponse1.Close();
                //this.Close();

                String result = new string(readBuff);
                if ((result.IndexOf("0")) >= 0)
                {
                    var regist = new regist();
                    //logSuccess.setUserName(this.userName.Text);
                    regist.setUserKey(keyName.Text);
                    regist.Show();
                    this.Close();
                }
                else
                {
                    clr = Color.FromRgb(122, 157, 147);//240, 78, 37);
                    keyName.Foreground = new SolidColorBrush(clr);
                    keyName.Text = "Invalid Key";
                }
            }
            else
            {
                if ((keyName.Text == "ENTER WDRIVE REGISTRATION KEY HERE") || (keyName.Text == "Invalid Key"))
                {
                    keyName.Text = "";
                }
            }
        }


    }

    
}
