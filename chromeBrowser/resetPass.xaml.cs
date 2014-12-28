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
    /// Interaction logic for resetPass.xaml
    /// </summary>
    public partial class resetPass : Window
    {
        public resetPass()
        {
            InitializeComponent();
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            this.Left = (screenWidth - this.Width) / 2;
            this.Top = (screenHeight - this.Height) / 2;
            currentPass.AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(currentPass_MouseDown), true);
            retbtn.AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(this.retbtn_MouseDown), true);
            currentPass.AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(this.currentPass_MouseDown), true);
            userpass.AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(this.userpass_MouseDown), true);
            passConfirm.AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(this.passConfirm_MouseDown), true);
            usersubmit.AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(this.usersubmit_MouseDown), true);
        }

        private bool isPass = false;
        private bool isNewPass = false;
        private bool isMatch = false;


        private void currentPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (currentPass.Text == "Current Password")
            {
                currentPass.Text = "";
            }
            if (e.Key == Key.Enter)
            {
                isPass = checkCurrentPass();
                this.checkSubmitState();
                userpass.SelectAll();
                userpass.Focus();
            }
        }

        private void currentPass_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (currentPass.Text == "Current Password")
            {
                currentPass.Text = "";
            }
        }

        private void currentPass_MouseLeave(object sender, MouseEventArgs e)
        {
            if ((currentPass.Text == "") || (currentPass.Text == "Current Password"))
            {
                currentPass.Text = "Current Password";
            }
            else
            {
                isPass = checkCurrentPass();
                this.checkSubmitState();
            }
        }

        private void currentPass_MouseEnter(object sender, MouseEventArgs e)
        {
            currentPass.SelectAll();
            currentPass.Focus();

        }


        /// <summary>
        /// setting all item state such as current pass checking , new pass matching
        /// </summary>
        /// 
        private void checkSubmitState()
        {
            if (isPass)
            {
                failedCurrent.Visibility = System.Windows.Visibility.Hidden;
                CurrentOk.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                failedCurrent.Visibility = System.Windows.Visibility.Visible;
                CurrentOk.Visibility = System.Windows.Visibility.Hidden;
            }

            if (isNewPass)
            {
                passCheck.Visibility = System.Windows.Visibility.Visible;
                failedNewCurrent.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                passCheck.Visibility = System.Windows.Visibility.Hidden;
                failedNewCurrent.Visibility = System.Windows.Visibility.Visible;
            }

            if (isMatch)
            {
                matchPassFail.Visibility = System.Windows.Visibility.Hidden;
                matchPassTrue.Visibility = System.Windows.Visibility.Visible;

            }
            else
            {
                matchPassFail.Visibility = System.Windows.Visibility.Visible;
                matchPassTrue.Visibility = System.Windows.Visibility.Hidden;
            }

            if (isMatch && isNewPass && isPass)
                usersubmit.IsEnabled = true;
            else
                usersubmit.IsEnabled = false;

        }




        /// <summary>
        /// This is function for checking user entered current password.
        /// </summary>
        /// <returns></returns>
        private bool checkCurrentPass()
        {
            bool isPass = false;
            try
            {
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
                    return false;

                String logincheckurl = "http://wdrive.us/userlogin/checkPass.php?password=" + currentPass.Text
                    + sn + "&usbid=" + sn;
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
                myHttpWebResponse1.Close();
                if ((readBuff.Length > 0) && (readBuff[0] == '1'))
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {

            }
            return isPass;
        }

        /// <summary>
        /// wdrive.us/userlogin/resetPass.php/?usbID=000000000272&oldpass=tas000000000272&newpass=tas000000000272
        /// </summary>
        private void resetPassWd()
        {
            try
            {
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

                String logincheckurl = "http://wdrive.us/userlogin/resetPass.php?usbID=" + sn + "&oldpass=" + currentPass.Text
                    + sn + "&newpass=" + userpass.Text + sn;
                HttpWebRequest myHttpWebRequest1 = (HttpWebRequest)WebRequest.Create(logincheckurl);
                myHttpWebRequest1.KeepAlive = false;
                HttpWebResponse myHttpWebResponse1 = (HttpWebResponse)myHttpWebRequest1.GetResponse();
                Stream streamResponse = myHttpWebResponse1.GetResponseStream();
                StreamReader streamRead = new StreamReader(streamResponse);
                Char[] readBuff = new Char[256];
                int count = streamRead.Read(readBuff, 0, 256);
                String outputData = "";
                if (count > 0)
                {
                    outputData = new String(readBuff, 0, count);
                    //count = streamRead.Read(readBuff, 0, 256);
                    //MessageBox.Show(outputData);
                }
                streamResponse.Close();
                streamRead.Close();
                myHttpWebResponse1.Close();
                if (outputData == "ResetSuccess")
                {
                    try
                    {
                        var newWindow = new passChangeSuccess();
                        newWindow.Show();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        this.Close();
                        ex.ToString();
                    }
                }
                if ((readBuff.Length > 0) && (readBuff[0] == '1'))
                {

                    return;
                }
                else
                {
                    return;
                }


            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 
        /// These are functions about userpass TextBox's function such as mouse event key event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void userpass_MouseEnter(object sender, MouseEventArgs e)
        {
            userpass.SelectAll();
            userpass.Focus();

        }



        private bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }


        private void userpass_MouseLeave(object sender, MouseEventArgs e)
        {
            if ((userpass.Text == "New Password") || (userpass.Text == ""))
            {
                userpass.Text = "New Password";
                isNewPass = false;
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
                    this.isNewPass = true;
                else
                    this.isNewPass = false;

                //isNewPass = true;
            }

            checkSubmitState();
        }

        private void userpass_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (userpass.Text == "" || userpass.Text == "New Password")
            {
                userpass.Text = "";
            }

        }

        private void userpass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (userpass.Text == "" || userpass.Text == "New Password")
                {
                    isNewPass = false;
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
                        this.isNewPass = true;
                    else
                        this.isNewPass = false;

                    //isNewPass = true;
                }
                passConfirm.SelectAll();
                passConfirm.Focus();
            }
            else
            {
                if (userpass.Text == "" || userpass.Text == "New Password")
                {
                    userpass.Text = "";
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
                        this.isNewPass = true;
                    else
                        this.isNewPass = false;
                }
            }
            checkSubmitState();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            login lg_win = new login();
            lg_win.Show();
            this.Close();

        }

        private void retbtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            login lg_win = new login();
            lg_win.Show();
            this.Close();
        }


        /// <summary>
        /// 
        /// These are functions about passConfirm TextBox's function such as mouse event key event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void passConfirm_MouseEnter(object sender, MouseEventArgs e)
        {
            passConfirm.SelectAll();
            passConfirm.Focus();
        }

        private void passConfirm_MouseLeave(object sender, MouseEventArgs e)
        {
            if ((passConfirm.Text == "Confirm New Password") || (passConfirm.Text == ""))
            {
                passConfirm.Text = "Confirm New Password";
                isMatch = false;
            }
            else
            {
                if (userpass.Text == passConfirm.Text)
                    isMatch = true;
                else
                    isMatch = false;
            }

            checkSubmitState();
        }

        private void passConfirm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            passConfirm.Text = "";
            passConfirm.Focus();
        }



        private void passConfirm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (passConfirm.Text == "" || passConfirm.Text == "Confirm New Password")
                {
                    isMatch = false;
                }
                else
                {
                    if (userpass.Text == passConfirm.Text)
                    {
                        isMatch = true;
                        checkSubmitState();
                        if (isPass && isMatch && isNewPass)
                            resetPassWd();
                    }
                }
            }
        }


        private void passConfirm_KeyUp(object sender, KeyEventArgs e)
        {
            if (userpass.Text == passConfirm.Text)
            {
                isMatch = true;
                checkSubmitState();
            }
        }

        private void usersubmit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            resetPassWd();
        }
    }
}
