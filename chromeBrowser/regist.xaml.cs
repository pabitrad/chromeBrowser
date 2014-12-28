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
using System.Windows.Shapes;
using System.Net;
using System.IO;

namespace chromeBrowser
{
    /// <summary>
    /// Interaction logic for regist.xaml
    /// </summary>
    public partial class regist : Window
    {

        private bool isUserIdFailed = true;
        private bool isMailFailed = true;
        private bool isPassCheck = false;
        private bool isPassMathFailed = true;
        private bool isCustomCheck = false;



        public regist()
        {
            InitializeComponent();
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            this.Left = (screenWidth - this.Width) / 2;
            this.Top = (screenHeight - this.Height) / 2;

            usersubmit.AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(usersubmit_MouseLeftButtonDown), true);
            retbtn.AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(retbtn_MouseLeftButtonDown), true);


            userEmail.SelectAll();

            userEmail.Focus();
        }

        private String serial_key = "";
        public void setUserKey(String keys)
        {
            serial_key = keys;
        }

        private void userPass_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            var newWindow = new login();
            newWindow.Show();
            this.Close();
        }

        private void userpass_MouseEnter_1(object sender, MouseEventArgs e)
        {
            if (userpass.Text == "Password")
            {
                userpass.Text = "";
            }

            //Color clr = Color.FromRgb(240, 78, 37);
            // userpass.Background = new SolidColorBrush(clr);
            userpass.SelectAll();
            userpass.Focus();
            checkSubmitState();
        }


        private bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }


        private void userpass_MouseLeave(object sender, MouseEventArgs e)
        {
            //userpass.Background.Opacity = 0;

            if ((userpass.Text == "") || (userpass.Text == "Password"))
            {
                userpass.Text = "Password";
                this.isPassCheck = false;
            }
            else
            {
                int digits = 0 ;
                foreach (var ch in userpass.Text)
                {
                    //if (char.IsLetter(ch)) letters++; //increment letters
                    if (char.IsDigit(ch)) digits++; //increment digits
                }
                if ((userpass.Text.Length > 5) && (digits > 0))
                    this.isPassCheck = true;
                else
                    this.isPassCheck = false;
            }
            checkSubmitState();

        }

        private void userConfirm_MouseEnter(object sender, MouseEventArgs e)
        {
            if (userConfirm.Text == "Confirm Password")
            {
                userConfirm.Text = "";
                //this.isPassCheck = false;
            }

            // Color clr = Color.FromRgb(240, 78, 37);
            // userConfirm.Background = new SolidColorBrush(clr); 
            userConfirm.SelectAll();
            userConfirm.Focus();
            checkSubmitState();
        }

        private void userConfirm_MouseLeave(object sender, MouseEventArgs e)
        {
            // userConfirm.Background.Opacity = 0;
            if (userConfirm.Text == "")
            {
                userConfirm.Text = "Confirm Password";
            }
            if (!checkPassMath())
                isPassMathFailed = true;
            else
                isPassMathFailed = false;
            this.checkSubmitState();
        }

        private void userEmail_MouseEnter(object sender, MouseEventArgs e)
        {
            if (userEmail.Text == "My Email Address")
            {
                userEmail.Text = "";
            }

            // Color clr = Color.FromRgb(240, 78, 37);
            // userEmail.Background = new SolidColorBrush(clr);
            userEmail.SelectAll();

            userEmail.Focus();
        }

        private void userEmail_MouseLeave(object sender, MouseEventArgs e)
        {
            if (userEmail.Text == "")
            {
                userEmail.Text = "My Email Address";
            }
            else
            {
                if ((userEmail.Text.IndexOf("@") < 1) || (userEmail.Text.IndexOf("@") > (userEmail.Text.Length - 3)))
                    isMailFailed = true;
                else
                    isMailFailed = false;
                //this.checkSubmitState();

            }
            this.checkSubmitState();
            // userEmail.Background.Opacity = 0; 
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
            // Color clr1 = Color.FromRgb(255, 255, 255);
            //usersubmit.Foreground = new SolidColorBrush(clr1);

        }

        private void retbtn_MouseEnter(object sender, MouseEventArgs e)
        {
            Color clr = Color.FromRgb(122, 157, 147);
            //retbtn.Background = new SolidColorBrush(clr);
            //retbtn.BorderThickness = new Thickness(0);

        }

        private void retbtn_MouseLeave(object sender, MouseEventArgs e)
        {
            Color clr = Colors.Black;
            //retbtn.Foreground = new SolidColorBrush(clr);
            //retbtn.Background = new SolidColorBrush(clr);
        }

        private void retbtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var newWindow = new login();
            newWindow.Show();
            this.Close();
        }

        private void usersubmit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((isMailFailed) || (isPassMathFailed))
                return;
            if (!checkDataStatus())
                return;
            mailError.Visibility = System.Windows.Visibility.Hidden;
            String sn = "";
            UsbUtils usbUTILS = new UsbUtils();
            String usbnamespace = usbUTILS.getUSBDriver();
            if (usbnamespace.Length > 0)
            {
                String driver_name = usbnamespace.Substring(0, 1);
                USBSN us = new USBSN();
                sn = us.getSerialNumberFromDriveLetter(driver_name);
            }
            else
                return;
            //saveUserAndPass(userName.Text, userpass.Text);
            //last_name.Content = sn;

            String logincheckurl = "https://wemagin.com/wdrive/userlogin/createacct.php?firstname=" + userEmail.Text + "&lastname=" + userEmail.Text + "&username="
                   + userEmail.Text + "&email=" + userEmail.Text + "&password=" + userpass.Text + sn + "&usbid=" + sn + "&sKey=" + this.serial_key + "&subDomain=W-" + this.CustomUrl.Text;

            //String logincheckurl = "http://thesmartwave.net/blue1/webBrowser/createacct.php?firstname=" + userName.Text + "&lastname=" + userName.Text + "&username="
            //+ userName.Text + "&email=" + userEmail.Text + "&password=" + userpass.Text + sn + "&usbid=" + sn;

            //String logincheckurl = "http://localhost/webBrowser/createacct.php?firstname=" + userName.Text + "&lastname=" + userName.Text + "&username="
            //+ userName.Text + "&email=" + userEmail.Text + "&password=" + userpass.Text + sn + "&usbid=" + sn;


            HttpWebRequest myHttpWebRequest1 = (HttpWebRequest)WebRequest.Create(logincheckurl);

            myHttpWebRequest1.KeepAlive = false;
            // Assign the response object of HttpWebRequest to a HttpWebResponse variable.
            HttpWebResponse myHttpWebResponse1 = (HttpWebResponse)myHttpWebRequest1.GetResponse();

            Stream streamResponse = myHttpWebResponse1.GetResponseStream();
            StreamReader streamRead = new StreamReader(streamResponse);
            Char[] readBuff = new Char[256];
            int count = streamRead.Read(readBuff, 0, 256);
            // Console.WriteLine("The contents of the Html page are.......\n");
            while (count > 0)
            {
                String outputData = new String(readBuff, 0, count);
                // Console.Write(outputData);
                count = streamRead.Read(readBuff, 0, 256);
            }

            streamResponse.Close();
            streamRead.Close();
            // Release the resources held by response object.
            myHttpWebResponse1.Close();
            //this.Close();

            String result = new string(readBuff);
            if ((result.IndexOf("customAddressInUse")) >= 0)
            {
                sample.Visibility = System.Windows.Visibility.Hidden;
                address_error.Visibility = System.Windows.Visibility.Visible;
                address_accept.Visibility = System.Windows.Visibility.Hidden;
            }
            else if ((result.IndexOf("USerRegSuccess")) >= 0)
            {
                var logSuccess = new LoginSuccess();
                logSuccess.setUserName(this.userEmail.Text);
                logSuccess.Show();
                this.Close();
            }
            else if ((result.IndexOf("USerRegFailed")) >= 0)
            {
                //MessageBox.Show("Failed");
            }

            else if ((result.IndexOf("USerAlreadyReg")) >= 0)
            {
                isUserIdFailed = true;
            }
            else if ((result.IndexOf("MailAlreadyReg")) >= 0)
            {
                mailError.Visibility = System.Windows.Visibility.Visible;
            }

            checkSubmitState();
            //var newWindow = new login();
            //newWindow.Show();
            //this.Close();
        }


        private void resetItemBackground()
        {

            // userName.Background.Opacity = 0;
            // userpass.Background.Opacity = 0;
            // userConfirm.Background.Opacity = 0;
            // userEmail.Background.Opacity = 0;
        }

        private void userName_KeyDown(object sender, KeyEventArgs e)
        {

            isUserIdFailed = false;
            if ((e.Key == Key.Enter) || (e.Key == Key.Tab))
            {
                //if (userName.Text == "")
                {
                    // userName.Text = "User Name";
                }
                // else
                {
                    isUserIdFailed = userIDCheck();
                }
                if (userpass.Text == "Password")
                {
                    userpass.Text = "";
                }
                userpass.Focus();
                userpass.SelectAll();
            }
            else
            {
                if (userEmail.Text == "My Email Address")
                {
                    userEmail.Text = "";
                }
            }
            checkSubmitState();
        }

        private bool userIDCheck()
        {
            bool isRegID = false;


            String logincheckurl = "https://wemagin.com/wdrive/userlogin/checkUserID.php?username=";// +userName.Text;


            //String logincheckurl = "http://thesmartwave.net/blue1/webBrowser/checkUserID.php?username=" + userName.Text;

            HttpWebRequest myHttpWebRequest1 = (HttpWebRequest)WebRequest.Create(logincheckurl);

            myHttpWebRequest1.KeepAlive = false;
            // Assign the response object of HttpWebRequest to a HttpWebResponse variable.
            HttpWebResponse myHttpWebResponse1 = (HttpWebResponse)myHttpWebRequest1.GetResponse();
            Stream streamResponse = myHttpWebResponse1.GetResponseStream();
            StreamReader streamRead = new StreamReader(streamResponse);
            Char[] readBuff = new Char[256];
            int count = streamRead.Read(readBuff, 0, 256);
            // Console.WriteLine("The contents of the Html page are.......\n");
            while (count > 0)
            {
                String outputData = new String(readBuff, 0, count);
                // Console.Write(outputData);
                count = streamRead.Read(readBuff, 0, 256);
            }
            //Console.WriteLine();
            // Close the Stream object.
            streamResponse.Close();
            streamRead.Close();
            // Release the resources held by response object.
            myHttpWebResponse1.Close();

            if ((readBuff.Length > 0) && (readBuff[0] == '1'))
            {
                isRegID = false;
            }
            else
            {
                isRegID = true;
            }
            return isRegID;

        }

        private void userpass_KeyDown(object sender, KeyEventArgs e)
        {

            isPassCheck = false;
            if ((e.Key == Key.Enter) || (e.Key == Key.Tab))
            {
                if ((userpass.Text == "") || (userpass.Text == "Password"))
                {
                    userpass.Text = "Password";
                    isPassCheck = false;
                }
                else
                {
                    int digits = 0;
                    foreach (var ch in userpass.Text)
                    {
                        //if (char.IsLetter(ch)) letters++; //increment letters
                        if (char.IsDigit(ch)) digits++; //increment digits
                    }
                    if ((userpass.Text.Length > 5) && (digits > 0))
                        this.isPassCheck = true;
                    else
                        this.isPassCheck = false;
                }

                if (userConfirm.Text == "Confirm Password")
                {
                    userConfirm.Text = "";
                }
                userConfirm.Focus();
                userConfirm.SelectAll();

            }
            else
            {
                if (userpass.Text == "Password")
                    userpass.Text = "";
            }
            checkSubmitState();
        }

        private void userConfirm_KeyDown(object sender, KeyEventArgs e)
        {

            isPassMathFailed = false;
            isPassMathFailed = !checkDataStatus();
            if ((e.Key == Key.Enter) || (e.Key == Key.Tab))
            {
                if (userConfirm.Text == "") 
                {
                    userConfirm.Text = "Confirm Password";
                }
                if ((CustomUrl.Text == "") || (CustomUrl.Text == "Custom Address"))
                {
                    CustomUrl.Text = "";
                    CustomUrl.Focus();
                }

                if (!checkPassMath())
                    isPassMathFailed = true;
                else
                    isPassMathFailed = false;
                checkSubmitState();
            }
            else
            {
                if (userConfirm.Text == "Confirm Password")
                    userConfirm.Text = "";
            }
            //checkSubmitState();
        }

        private bool checkPassMath()
        {
            bool isMatch = false;
            if (this.userpass.Text == this.userConfirm.Text)
                isMatch = true;
            else
                isMatch = false;

            return isMatch;
        }
        private bool checkDataStatus()
        {
            bool isCheck = true;
            if (userpass.Text != userConfirm.Text)
                return false;
            isPassMathFailed = false;
            //if ((userName.Text == "") || (userpass.Text == ""))
            //return false;
            checkSubmitState();
            return isCheck;
        }


        private void userEmail_KeyDown(object sender, KeyEventArgs e)
        {
            isMailFailed = false;
            if ((userEmail.Text.IndexOf("@") < 1) || (userEmail.Text.IndexOf("@") > (userEmail.Text.Length - 3)))
                isMailFailed = true;
            this.checkSubmitState();

            if ((e.Key == Key.Enter) || (e.Key == Key.Tab))
            {
                if (userpass.Text == "Password")
                {
                    userpass.Text = "";
                }
                if (userEmail.Text == "")
                {
                    userEmail.Text = "My Email Address";
                }
                userpass.Focus();
                userpass.SelectAll();
            }
            else
            {
                if (userEmail.Text == "My Email Address")
                    userEmail.Text = "";
            }

            //checkSubmitState();
        }


        private void saveUserAndPass(string userid, string userpass)
        {
            try
            {
                //MessageBox.Show("1");
                string userName = userid;
                string userPasses = userpass;

                if (userName.Length % 4 > 0)
                    userName = userName.PadRight(userName.Length + 4 - userName.Length % 4, '=');

                if (userPasses.Length % 4 > 0)
                    userPasses = userPasses.PadRight(userPasses.Length + 4 - userPasses.Length % 4, '=');


                byte[] userBYTE = System.Convert.FromBase64String(userName);
                byte[] userPass = System.Convert.FromBase64String(userPasses);
                //MessageBox.Show("2");
                int newSize = userBYTE.Length + userPass.Length;
                var ms = new MemoryStream(new byte[newSize], 0, newSize, true, true);
                //MessageBox.Show("3");
                ms.Write(userBYTE, 0, userBYTE.Length);
                //MessageBox.Show("4");
                ms.Write(userPass, 0, userPass.Length);
                byte[] merged = ms.GetBuffer();
                // MessageBox.Show("5");
                BinaryWriter Writer = null;
                UsbUtils usbUTILS = new UsbUtils();
                String usbnamespace = usbUTILS.getUSBDriver();

                if (usbnamespace.Length > 0)
                {
                    string iName = "";
                    string folder_name = System.IO.Path.Combine(usbnamespace, "wemagin_v2");
                    if (!Directory.Exists(folder_name))
                    {
                        Directory.CreateDirectory(folder_name);
                    };
                    //MessageBox.Show("6");
                    DirectoryInfo dir = new DirectoryInfo(folder_name);
                    dir.Attributes |= FileAttributes.Hidden;
                    //MessageBox.Show("7");
                    iName = usbnamespace + "wemagin_v2\\info.txt";


                    try
                    {
                        // Create a new stream to write to the file
                        Writer = new BinaryWriter(File.OpenWrite(iName));
                        //MessageBox.Show("8");
                        // Writer raw data                
                        Writer.Write(merged);
                        //MessageBox.Show("9");
                        Writer.Flush();
                        Writer.Close();
                    }
                    catch (Exception ex)
                    {
                        //...
                        MessageBox.Show(ex.ToString());
                    }
                }


            }
            catch (Exception ex)
            {
                ex.ToString();
            }

        }


        private void checkSubmitState()
        {
            /* if (isUserIdFailed)
             {
                 this.userNameFailed.Visibility = System.Windows.Visibility.Visible;
                 this.userNameTrue.Visibility = System.Windows.Visibility.Hidden;
             }
             else
             {
                 this.userNameFailed.Visibility = System.Windows.Visibility.Hidden;
                 this.userNameTrue.Visibility = System.Windows.Visibility.Visible;
             }*/

            if (isPassCheck)
            {
                this.passCheck.Visibility = System.Windows.Visibility.Visible;
                passfail.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                this.passCheck.Visibility = System.Windows.Visibility.Hidden;
                passfail.Visibility = System.Windows.Visibility.Visible;
            }

            if (isPassMathFailed)
            {
                matchPassTrue.Visibility = System.Windows.Visibility.Hidden;
                this.matchPass.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                this.matchPass.Visibility = System.Windows.Visibility.Hidden;
                matchPassTrue.Visibility = System.Windows.Visibility.Visible;
            }
            if (isMailFailed)
            {
                this.validMail.Visibility = System.Windows.Visibility.Visible;
                emailOk.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                emailOk.Visibility = System.Windows.Visibility.Visible;
                this.validMail.Visibility = System.Windows.Visibility.Hidden;
            }

            if (isPassMathFailed || isMailFailed)
            {
                this.usersubmit.IsEnabled = false;
            }
            else
            {
                this.usersubmit.IsEnabled = true;
            }
        }

        private void userConfirm_KeyUp(object sender, KeyEventArgs e)
        {
            this.checkDataStatus();
            this.checkSubmitState();
        }

        private void CustomUrl_MouseLeave(object sender, MouseEventArgs e)
        {
            if ((CustomUrl.Text == "") || (CustomUrl.Text == "Custom Address"))
                CustomUrl.Text = "Custom Address";
        }

        private void CustomUrl_MouseEnter(object sender, MouseEventArgs e)
        {
            sample.Visibility = System.Windows.Visibility.Visible;
            address_error.Visibility = System.Windows.Visibility.Hidden;
            address_accept.Visibility = System.Windows.Visibility.Hidden;
        }

        private void CustomUrl_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void CustomUrl_KeyDown(object sender, KeyEventArgs e)
        {
            isCustomCheck = false;
            if ((e.Key == Key.Enter) || (e.Key == Key.Tab))
            {
                mailError.Visibility = System.Windows.Visibility.Hidden;
                if ((CustomUrl.Text == "") || (CustomUrl.Text == "Custom Address"))
                {
                    CustomUrl.Text = "Custom Address";
                    isCustomCheck = false;
                }
                else
                {
                    if (!isPassMathFailed)
                    {
                        if ((isMailFailed) || (isPassMathFailed))
                            return;
                        //saveUserAndPass(userName.Text, userpass.Text);
                        String sn = "";
                        UsbUtils usbUTILS = new UsbUtils();
                        String usbnamespace = usbUTILS.getUSBDriver();
                        if (usbnamespace.Length > 0)
                        {
                            String driver_name = usbnamespace.Substring(0, 1);
                            USBSN us = new USBSN();
                            sn = us.getSerialNumberFromDriveLetter(driver_name);
                        }
                        else
                            return;

                        //last_name.Content = sn;

                        String logincheckurl = "https://wemagin.com/wdrive/userlogin/createacct.php?firstname=" + userEmail.Text + "&lastname=" + userEmail.Text + "&username="
                            + userEmail.Text + "&email=" + userEmail.Text + "&password=" + userpass.Text + sn + "&usbid=" + sn + "&sKey=" + this.serial_key + "&subDomain=W-" + this.CustomUrl.Text;

                        //String logincheckurl = "http://thesmartwave.net/blue1/webBrowser/createacct.php?firstname=" + userName.Text + "&lastname=" + userName.Text + "&username="
                        //+ userName.Text + "&email=" + userEmail.Text + "&password=" + userpass.Text + sn + "&usbid=" + sn;
                        //String logincheckurl = "http://localhost/webBrowser/createacct.php?firstname=" + userName.Text + "&lastname=" + userName.Text + "&username="
                        //+ userName.Text + "&email=" + userEmail.Text + "&password=" + userpass.Text + sn+ "&usbid=" + sn;
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
                        String result = new string(readBuff);
                        if ((result.IndexOf("customAddressInUse")) >= 0)
                        {
                            sample.Visibility = System.Windows.Visibility.Hidden;
                            address_error.Visibility = System.Windows.Visibility.Visible;
                            address_accept.Visibility = System.Windows.Visibility.Hidden;
                        }
                        else if ((result.IndexOf("USerRegSuccess")) >= 0)
                        {
                            var logSuccess = new LoginSuccess();
                            logSuccess.setUserName(this.userEmail.Text);
                            logSuccess.Show();
                            this.Close();
                        }
                        else if ((result.IndexOf("USerRegFailed")) >= 0)
                        {
                            //MessageBox.Show("Failed");
                        }
                        else if ((result.IndexOf("USerAlreadyReg")) >= 0)
                        {
                            isUserIdFailed = true;
                        }
                        else if ((result.IndexOf("MailAlreadyReg")) >= 0)
                        {
                            mailError.Visibility = System.Windows.Visibility.Visible;
                        }
                    }
                }


            }
            else
            {
                if (CustomUrl.Text == "Custom Address")
                    CustomUrl.Text = "";
            }
            //checkSubmitState();

        }
    }
}