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
using System.Xml;
using System.Net;
using System.IO;


namespace chromeBrowser
{
    /// <summary>
    /// Interaction logic for SettingPage.xaml
    /// </summary>
    public partial class SettingPage : Window
    {

        public MainWindow parent_win;

        private bool[] icon_status = new bool[28];
        /* private bool istwitter, isfacebook, isinsta, islinkedin, isyoutube, isebay, isamazon, iscry, isrss,
             isblogger, iscnn, isfr, iswiki, ismap; */

        private string userId = "";
        //private string hackEmail = "";
        public SettingPage()
        {
            InitializeComponent();
            // for (int i = 0; i < 14; i++)
            //icon_status[i] = true;
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            this.Left = (screenWidth - this.Width) / 2;
            this.Top = (screenHeight - this.Height) / 2;  
            save_button.AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(save_MouseLeftButtonDown), true);
            //setUserInfo();
        }


        public void setUserInfo()
        {
            userId = parent_win.getUserID();
            if (parent_win.first_Name != null)
                fName.Text = parent_win.first_Name;
            if (parent_win.last_Name != null)
                lName.Text = parent_win.last_Name;
            if (parent_win.email_con != null)
                eMail.Text = parent_win.email_con;
            if (parent_win.phone_num != null)
                phone.Text = parent_win.phone_num;
        }

        public void setUserInfo(string uID)
        //string gMail, string gPass, string aMail, string aPass, string yMail, string yPass, string hMail, string hPass)
        {
            userId = uID;
            if (parent_win.first_Name != null)
                fName.Text = parent_win.first_Name;
            if (parent_win.last_Name != null)
                lName.Text = parent_win.last_Name;
            if (parent_win.email_con != null)
                eMail.Text = parent_win.email_con;
            if (parent_win.phone_num != null)
                phone.Text = parent_win.phone_num;
            /*gmail.Text = gMail;
            gmail_pass.Password = gPass;
            ymail.Text = yMail;
            ymail_pass.Password = yPass;
            amail.Text = aMail;
            amail_pass.Password = aPass;
            hmail.Text = hMail;
            hmail_pass.Password = hPass;
            */
            
            //getHackStateFromServer();
            fName.SelectAll();
            fName.Focus();
        }

        private bool isHackerState = true;

        private void getHackStateFromServer()
        {
            String logincheckurl = "http://wdrive.us/userlogin/getHackState.php?username=" + this.userId;
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
/*
            if ((readBuff.Length > 0) )
            {
                if ('1' == readBuff[0])
                {
                    isHackerState = false;
                    //hackBox.IsEnabled = false;
                    hackLabl.Content = "Hacker Alert is Now OFF";
                    hackEmail = outputData.Substring(1, outputData.Length - 1);
                    hackBox.Text = outputData.Substring(1, outputData.Length - 1); ;// "HACKER NOTIFICATION HAS BEEN TURNED OFF";

                    Uri uri = new Uri("Images/userprofile/hack.png", UriKind.Relative);
                    var bitmap = new BitmapImage(uri);
                    hack_icon.Source = bitmap;
                        //yutub_sel.Visibility = System.Windows.Visibility.Visible;

                    
                    hackBox.Foreground = new SolidColorBrush(Colors.Gray);
                }
                else
                {
                    isHackerState = true;
                    //hackBox.IsEnabled = true;
                    hackLabl.Content = "Hacker Alert is Now ON";
                    hackBox.Text = outputData.Substring(1, outputData.Length - 1);
                    hackEmail = outputData.Substring(1, outputData.Length - 1);
                    Uri uri = new Uri("Images/userprofile/hack-over.png", UriKind.Relative);
                    var bitmap = new BitmapImage(uri);
                    hack_icon.Source = bitmap;
                    hackBox.Foreground = new SolidColorBrush(Colors.Gray);
                }

            }      */
        }


        public void setUserInfo( string uID, string fName_info, string lName_info, string email_info, string phone_info)
           //string gMail, string gPass, string aMail, string aPass, string yMail, string yPass, string hMail, string hPass)
        {
            userId = uID;
            fName.Text = fName_info;
            lName.Text = lName_info;
            eMail.Text = email_info;
            phone.Text = phone_info;
            /*gmail.Text = gMail;
            gmail_pass.Password = gPass;
            ymail.Text = yMail;
            ymail_pass.Password = yPass;
            amail.Text = aMail;
            amail_pass.Password = aPass;
            hmail.Text = hMail;
            hmail_pass.Password = hPass;
            */
            //getHackStateFromServer();
            fName.SelectAll();
            fName.Focus();
        }



        /*
        public void getContactInfo()
        {
            try
            {

                UsbUtils usbUTILS = new UsbUtils();
                String usbnamespace = usbUTILS.getUSBDriver();
                if (usbnamespace.Length > 0)
                {
                    String file_name = usbnamespace + "wemagin_v2\\contact.xml";
                    if (System.IO.File.Exists(file_name))
                    {

                        XmlReader reader = XmlReader.Create(file_name);

                        while (reader.Read())
                        {
                            switch (reader.NodeType)
                            {
                                case XmlNodeType.Element:
                                    if (reader.Name.Equals("FirstName"))
                                    {
                                        reader.Read();
                                        String buffer = reader.Value.ToString();
                                        if ((buffer != "") && (buffer != " "))
                                            fName.Text = reader.Value.ToString();
                                    }
                                    else if (reader.Name.Equals("LastName"))
                                    {
                                        reader.Read();
                                        String buffer = reader.Value.ToString();
                                        if ((buffer != "") && (buffer != " "))
                                            lName.Text = reader.Value.ToString();

                                    }
                                    else if (reader.Name.Equals("Email"))
                                    {
                                        reader.Read();
                                        String buffer = reader.Value.ToString();
                                        if ((buffer != "") && (buffer != " "))
                                            eMail.Text = reader.Value.ToString();

                                    }
                                    else if (reader.Name.Equals("Phone"))
                                    {
                                        reader.Read();
                                        String buffer = reader.Value.ToString();
                                        if ((buffer != "") && (buffer != " "))
                                            phone.Text = reader.Value.ToString();
                                    }

                                    else if (reader.Name.Equals("Gmail"))
                                    {
                                        reader.Read();
                                        String buffer = reader.Value.ToString();
                                        //if ((buffer != "") && (buffer != " "))
                                           // gmail.Text = reader.Value.ToString();
                                    }

                                    else if (reader.Name.Equals("Gpass"))
                                    {
                                        reader.Read();
                                        String buffer = reader.Value.ToString();
                                       // if ((buffer != "") && (buffer != " "))
                                           // gmail_pass.Password = reader.Value.ToString();
                                    }

                                    else if (reader.Name.Equals("Ymail"))
                                    {
                                        reader.Read();
                                        String buffer = reader.Value.ToString();
                                       // if ((buffer != "") && (buffer != " "))
                                            //ymail.Text = reader.Value.ToString();

                                        // Read the XML Node's attributes and add to string
                                    }

                                    else if (reader.Name.Equals("Ypass"))
                                    {
                                        reader.Read();
                                        String buffer = reader.Value.ToString();
                                        //if ((buffer != "") && (buffer != " "))
                                           // ymail_pass.Password = reader.Value.ToString();

                                        // Read the XML Node's attributes and add to string
                                    }

                                    else if (reader.Name.Equals("Amail"))
                                    {
                                        reader.Read();
                                        String buffer = reader.Value.ToString();
                                        ///if ((buffer != "") && (buffer != " "))
                                            //amail.Text = reader.Value.ToString();

                                        // Read the XML Node's attributes and add to string
                                    }

                                    else if (reader.Name.Equals("Apass"))
                                    {
                                        reader.Read();
                                        String buffer = reader.Value.ToString();
                                        //if ((buffer != "") && (buffer != " "))
                                            //amail_pass.Password = reader.Value.ToString();

                                        // Read the XML Node's attributes and add to string
                                    }

                                    else if (reader.Name.Equals("Hmail"))
                                    {
                                        reader.Read();
                                        String buffer = reader.Value.ToString();
                                        //if ((buffer != "") && (buffer != " "))
                                           // hmail.Text = reader.Value.ToString();
                                    }

                                    else if (reader.Name.Equals("Hpass"))
                                    {
                                        reader.Read();
                                        String buffer = reader.Value.ToString();
                                       // if ((buffer != "") && (buffer != " "))
                                           // hmail_pass.Password = reader.Value.ToString();
                                    }
                                    break;
                            }
                        }


                    }
                    else
                    {
                        //System.IO.Directory.CreateDirectory(file_name);
                    }

                }
            }
            catch (Exception exx)
            {
                exx.ToString();
            }


        }*/


        public void setMain(MainWindow main_win)
        {
            parent_win = main_win;
            fName.SelectAll();
            fName.Focus();
        }

        private void close_icon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {



            //parent_win.tmp.Close();
            parent_win.Opacity = 1;
            parent_win.Effect = null;
            this.Close();
        }

        private void mini_icon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void TextBox_MouseEnter(object sender, MouseEventArgs e)
        {


            if (fName.Text == "First Name" || fName.Text == "")
            {
                fName.Text = "";
                fName.Focus();
            }
            else
            {
                fName.SelectAll();
                fName.Focus();
            }

        }

        private void TextBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (fName.Text == "")
            {
                //fName.Text = "First Name";

            }

        }

        private void lName_MouseEnter(object sender, MouseEventArgs e)
        {
            if (lName.Text == "Last Name" || lName.Text == "")
            {
                lName.Text = "";
                lName.Focus();
            }
            else
            {
                lName.SelectAll();
                lName.Focus();
            }
        }

        private void lName_MouseLeave(object sender, MouseEventArgs e)
        {
            if (lName.Text == "")
            {
                // lName.Text = "Last Name";
            }
        }

        private void eMail_MouseEnter(object sender, MouseEventArgs e)
        {
            if (eMail.Text == "Email" || eMail.Text == "")
            {
                eMail.Text = "";
                eMail.Focus();
            }
            else
            {
                eMail.SelectAll();
                eMail.Focus();
            }
        }

        private void eMail_MouseLeave(object sender, MouseEventArgs e)
        {
            if (eMail.Text == "")
            {
                // eMail.Text = "Email";
            }
        }

        private void phone_MouseEnter(object sender, MouseEventArgs e)
        {
            if (phone.Text == "Phone Number" || phone.Text == " ")
            {
                phone.Text = "";
                phone.Focus();
            }
            else
            {
                phone.SelectAll();
                phone.Focus();
            }
        }

        private void phone_MouseLeave(object sender, MouseEventArgs e)
        {
            if (phone.Text == "")
            {
                //phone.Text = "Phone Number";

            }
        }

        private void gmail_MouseEnter(object sender, MouseEventArgs e)
        {
           /* if (gmail.Text == "Gmail" || gmail.Text == " ")
            {
                gmail.Text = "";
                gmail.Focus();
            }
            else
            {
                gmail.SelectAll();
                gmail.Focus();
            }*/
        }

        private void gmail_MouseLeave(object sender, MouseEventArgs e)
        {
          /* if (gmail.Text == "")
            {
                gmail.Text = "Gmail";

            }*/
        }

        private void gmail_pass_MouseEnter(object sender, MouseEventArgs e)
        {
           /* if (gmail_pass.Password == "Password" || gmail_pass.Password == " ")
            {
                gmail_pass.Password = "";
                gmail_pass.Focus();
            }
            else
            {
                gmail_pass.SelectAll();
                gmail_pass.Focus();
            }*/

        }

        private void gmail_pass_MouseLeave(object sender, MouseEventArgs e)
        {
           /* if (gmail_pass.Password == "")
            {
                gmail_pass.Password = "Password";

            }*/
        }

        private void upload_icon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            UsbUtils usbUTILS = new UsbUtils();
            String usbnamespace = usbUTILS.getUSBDriver();
            String file_name = "";
            String tabInfo = "";
            if (usbnamespace.Length > 0)
            {
                file_name = usbnamespace + "wemagin_v2\\contact.xml";

                FileStream fs;
                if (!System.IO.File.Exists(file_name))
                {
                    fs = System.IO.File.Create(file_name);

                }
                else
                {
                    fs = System.IO.File.Create(file_name);                   
                }


                XmlTextWriter w = new XmlTextWriter(fs, Encoding.UTF8);
                w.WriteStartDocument();
                w.WriteStartElement("Contact");
                 if ((fName.Text != "First Name") && (fName.Text != "") && (fName.Text != " "))
                    w.WriteElementString("FirstName", this.fName.Text);
                else
                    w.WriteElementString("FirstName", "");

                if ((lName.Text != "Last Name") && (lName.Text != "") && (lName.Text != " "))
                    w.WriteElementString("LastName", this.lName.Text);
                else
                    w.WriteElementString("LastName", "");

                if ((eMail.Text != "Email") && (eMail.Text != "") && (eMail.Text != " "))
                    w.WriteElementString("Email", this.eMail.Text);
                else
                    w.WriteElementString("Email", "");

                if ((eMail.Text != "Phone Number") && (eMail.Text != "") && (eMail.Text != " "))
                    w.WriteElementString("Phone", this.phone.Text);
                else
                    w.WriteElementString("Phone", "");

                w.WriteEndElement();

                w.Flush();
                fs.Close();

                tabInfo = usbnamespace + "wemagin_v2\\tabIcon.xml";

                if (!System.IO.File.Exists(tabInfo))
                {
                    fs = System.IO.File.Create(tabInfo);

                }
                else
                {
                    fs = System.IO.File.Create(tabInfo);
                    // Console.WriteLine("File \"{0}\" already exists.", fileName);
                    // return;
                }
                w = new XmlTextWriter(fs, Encoding.UTF8);
                w.WriteStartDocument();
                w.WriteStartElement("TabInfo");
                w.WriteElementString("tabIconInfo", makeStringfromTabInfo());

                w.WriteEndElement();

                w.Flush();
                fs.Close();

                parent_win.setUserInfo(fName.Text, lName.Text, eMail.Text, phone.Text);//, gmail.Text, gmail_pass.Password,
                //amail.Text, amail_pass.Password, ymail.Text, ymail_pass.Password, hmail.Text, hmail_pass.Password);
            }
            parent_win.Opacity = 1;
            parent_win.Effect = null;
            this.Close();
        }


        private String makeStringfromTabInfo()
        {
            String result = Convert.ToString(icon_status[0]);
            for (int i = 1; i < 28; i++)
            {
                result += "||||";
                result += Convert.ToString(icon_status[i]);

            }
            return result;
        }



        private void TextBox_MouseEnter_1(object sender, MouseEventArgs e)
        {
            //Color clr = Color.FromRgb(240, 78, 37);
           // save_button.Background = new SolidColorBrush(clr);
        }

        private void TextBox_MouseLeave_1(object sender, MouseEventArgs e)
        {
           // Color clr = Colors.Black;
            //save_button.Background = new SolidColorBrush(clr);
        }

        // private void retbtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)

        private void save_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UsbUtils usbUTILS = new UsbUtils();
            String usbnamespace = usbUTILS.getUSBDriver();

            String file_name = "";
            String tabInfo = "";
            if (usbnamespace.Length > 0)
            {
                string folder_name = System.IO.Path.Combine(usbnamespace, "wemagin_v2");
                if (!Directory.Exists(folder_name))
                {
                    Directory.CreateDirectory(folder_name);
                    DirectoryInfo dir = new DirectoryInfo(folder_name);
                    dir.Attributes |= FileAttributes.Hidden;
                };

                file_name = usbnamespace + "wemagin_v2\\contact.xml";

                FileStream fs;
                if (!System.IO.File.Exists(file_name))
                {
                    fs = System.IO.File.Create(file_name);

                }
                else
                {
                    fs = System.IO.File.Create(file_name);
                    // Console.WriteLine("File \"{0}\" already exists.", fileName);
                    // return;
                }

                XmlTextWriter w = new XmlTextWriter(fs, Encoding.UTF8);
                w.WriteStartDocument();
                w.WriteStartElement("Contact");
                 if ((fName.Text != "First Name") && (fName.Text != "") && (fName.Text != " "))
                    w.WriteElementString("FirstName", this.fName.Text);
                else
                    w.WriteElementString("FirstName", "");

                if ((lName.Text != "Last Name") && (lName.Text != "") && (lName.Text != " "))
                    w.WriteElementString("LastName", this.lName.Text);
                else
                    w.WriteElementString("LastName", "");

                if ((eMail.Text != "Email") && (eMail.Text != "") && (eMail.Text != " "))
                    w.WriteElementString("Email", this.eMail.Text);
                else
                    w.WriteElementString("Email", "");

                if ((eMail.Text != "Phone Number") && (eMail.Text != "") && (eMail.Text != " "))
                    w.WriteElementString("Phone", this.phone.Text);
                else
                    w.WriteElementString("Phone", "");

                w.WriteEndElement();

                w.Flush();
                fs.Close();

                tabInfo = usbnamespace + "wemagin_v2\\tabIcon.xml";

                if (!System.IO.File.Exists(tabInfo))
                {
                    fs = System.IO.File.Create(tabInfo);

                }
                else
                {
                    fs = System.IO.File.Create(tabInfo);
                    // Console.WriteLine("File \"{0}\" already exists.", fileName);
                    // return;
                }
                w = new XmlTextWriter(fs, Encoding.UTF8);
                w.WriteStartDocument();
                w.WriteStartElement("TabInfo");
                w.WriteElementString("tabIconInfo", makeStringfromTabInfo());

                w.WriteEndElement();

                w.Flush();
                fs.Close();

                parent_win.setUserInfo(fName.Text, lName.Text, eMail.Text, phone.Text);//, gmail.Text, gmail_pass.Password,
                /*
                try
                {

                    String sn = "";
                    String driver_name = usbnamespace.Substring(0, 1);
                    USBSN us = new USBSN();
                    sn = us.getSerialNumberFromDriveLetter(driver_name);
                    String logincheckurl = ""; //"http://wdrive.us/userlogin/updateemail.php?usbid=" + sn + "&email=" + this.hackBox.Text + "&alertstate=";
                    if (this.isHackerState)
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

                }*/                
            }
            parent_win.Opacity = 1;
            parent_win.Effect = null;
            this.Close();
        }





        /*******
         * 
         * set twitter icon
         * 
         * **************/
        /*
        private void twitter_button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            icon_status[0] = !icon_status[0];
            settwitterStatus(icon_status[0]);
            //this.parent_win.resetIconPosition(icon_status);
        }

        public void setIconStatus(bool[] isStatus)
        {
            icon_status = isStatus;
        }

        public void settwitterStatus(bool status)
        {
            if (status)
            {
                Uri uri = new Uri("Images/twit_icn-over.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                twitter_button.Source = bitmap;
                // twitter_sel.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                Uri uri = new Uri("Images/iconSetting/twit_icn-over-op.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                twitter_button.Source = bitmap;
                // twitter_sel.Visibility = System.Windows.Visibility.Hidden;
            }


        }


        private void twitter_button_MouseEnter(object sender, MouseEventArgs e)
        {

            {
                Uri uri = new Uri("Images/twit_icn-over.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                twitter_button.Source = bitmap;
                //twitter_sel.Visibility = System.Windows.Visibility.Visible;

            }
        }

        private void twitter_button_MouseLeave(object sender, MouseEventArgs e)
        {
            settwitterStatus(icon_status[0]);
        }
        */

        /*********
         * 
         * Map Icon Status
         * *****************/
       /* private void map_button_MouseEnter(object sender, MouseEventArgs e)
        {
            Uri uri = new Uri("Images/right_icon4-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            map_button.Source = bitmap;
            //map_sel.Visibility = System.Windows.Visibility.Visible;

        }

        private void map_button_MouseLeave(object sender, MouseEventArgs e)
        {
            setMapStatus(icon_status[13]);
        }

        private void map_button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            icon_status[13] = !icon_status[13];
            setMapStatus(icon_status[13]);
            this.parent_win.resetIconPosition(icon_status);
        }

        public void setMapStatus(bool status)
        {
            if (status)
            {
                Uri uri = new Uri("Images/right_icon4-over.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                map_button.Source = bitmap;
                //map_sel.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                Uri uri = new Uri("Images/iconSetting/right_icon4-over-op.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                map_button.Source = bitmap;
                //map_sel.Visibility = System.Windows.Visibility.Hidden;
            }


        }
        */
        /****
         * 
         * set facebook Icon
         * 
         * ******/

      /*  public void setfaceStatus(bool status)
        {
            if (status)
            {
                Uri uri = new Uri("Images/fb_icn-over.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                face_button.Source = bitmap;
                //face_sel.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                Uri uri = new Uri("Images/iconSetting/fb_icn-over-op.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                face_button.Source = bitmap;
                //face_sel.Visibility = System.Windows.Visibility.Hidden;

            }
        }

        private void face_button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            icon_status[1] = !icon_status[1];
            setfaceStatus(icon_status[1]);
            this.parent_win.resetIconPosition(icon_status);
        }

        private void face_button_MouseEnter(object sender, MouseEventArgs e)
        {
            Uri uri = new Uri("Images/fb_icn-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            face_button.Source = bitmap;

        }

        private void face_button_MouseLeave(object sender, MouseEventArgs e)
        {
            setfaceStatus(icon_status[1]);
        }
        */

        /****
        * 
        * set Insta Icon
        * 
        * ******/
        /*
        public void setinstaStatus(bool status)
        {
            if (status)
            {
                Uri uri = new Uri("Images/instagrame_icn-over.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                insta_button.Source = bitmap;
                //insta_sel.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                Uri uri = new Uri("Images/iconSetting/instagrame_icn-over-op.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                insta_button.Source = bitmap;
                //insta_sel.Visibility = System.Windows.Visibility.Hidden;

            }
        }

        private void insta_button_MouseEnter(object sender, MouseEventArgs e)
        {
            Uri uri = new Uri("Images/instagrame_icn-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            insta_button.Source = bitmap;
        }

        private void insta_button_MouseLeave(object sender, MouseEventArgs e)
        {
            setinstaStatus(icon_status[2]);
        }

        private void insta_button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            icon_status[2] = !icon_status[2];
            setinstaStatus(icon_status[2]);
            this.parent_win.resetIconPosition(icon_status);
        }

        */

        /****
        * 
        * set Insta Icon
        * 
        * ******/
        /*
        public void setinnStatus(bool status)
        {
            if (status)
            {
                Uri uri = new Uri("Images/inn-over.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                inn_button.Source = bitmap;
                //inn_sel.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                Uri uri = new Uri("Images/iconSetting/inn-over-op.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                inn_button.Source = bitmap;
                //inn_sel.Visibility = System.Windows.Visibility.Hidden;

            }
        }


        private void inn_button_MouseEnter(object sender, MouseEventArgs e)
        {
            Uri uri = new Uri("Images/inn-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            inn_button.Source = bitmap;
        }

        private void inn_button_MouseLeave(object sender, MouseEventArgs e)
        {
            setinnStatus(icon_status[3]);
        }

        private void inn_button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            icon_status[3] = !icon_status[3];
            setinnStatus(icon_status[3]);
            this.parent_win.resetIconPosition(icon_status);
        }*/

        /****
      * 
      * set Insta Icon
      * 
      * ******/
        /*
        public void setyutubeStatus(bool status)
        {
            if (status)
            {
                Uri uri = new Uri("Images/youtube_icon-over.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                yutub_button.Source = bitmap;
                //yutub_sel.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                Uri uri = new Uri("Images/iconSetting/youtube_icon-over-op.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                yutub_button.Source = bitmap;
                //yutub_sel.Visibility = System.Windows.Visibility.Hidden;

            }
        }

        private void yutub_button_MouseEnter(object sender, MouseEventArgs e)
        {
            Uri uri = new Uri("Images/youtube_icon-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            yutub_button.Source = bitmap;

        }

        private void yutub_button_MouseLeave(object sender, MouseEventArgs e)
        {
            setyutubeStatus(icon_status[4]);
        }

        private void yutub_button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            icon_status[4] = !icon_status[4];
            setyutubeStatus(icon_status[4]);
            this.parent_win.resetIconPosition(icon_status);
        }

        */
        /****
           * 
           * set Ebay Icon
           * 
           * ******/
        /*
        public void setebayStatus(bool status)
        {
            if (status)
            {
                Uri uri = new Uri("Images/right_icon7-over.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                ebay_button.Source = bitmap;
                //ebay_sel.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                Uri uri = new Uri("Images/iconSetting/right_icon7-over-op.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                ebay_button.Source = bitmap;
                //ebay_sel.Visibility = System.Windows.Visibility.Hidden;

            }
        }

        private void ebay_button_MouseEnter(object sender, MouseEventArgs e)
        {
            Uri uri = new Uri("Images/right_icon7-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            ebay_button.Source = bitmap;

        }

        private void ebay_button_MouseLeave(object sender, MouseEventArgs e)
        {
            setebayStatus(icon_status[5]);
        }

        private void ebay_button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            icon_status[5] = !icon_status[5];
            setebayStatus(icon_status[5]);
            this.parent_win.resetIconPosition(icon_status);
        }
        */

        /****
         * 
         * set Amazon Icon
         * 
         * ******/
        /*
        public void seteamazonStatus(bool status)
        {
            if (status)
            {
                Uri uri = new Uri("Images/right_icon5-over.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                amazon_button.Source = bitmap;
                //amazon_sel.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                Uri uri = new Uri("Images/iconSetting/right_icon5-over-op.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                amazon_button.Source = bitmap;
                //amazon_sel.Visibility = System.Windows.Visibility.Hidden;

            }
        }
        private void amazon_button_MouseEnter(object sender, MouseEventArgs e)
        {
            Uri uri = new Uri("Images/right_icon5-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            amazon_button.Source = bitmap;

        }

        private void amazon_button_MouseLeave(object sender, MouseEventArgs e)
        {
            seteamazonStatus(icon_status[6]);
        }

        private void amazon_button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            icon_status[6] = !icon_status[6];
            seteamazonStatus(icon_status[6]);
            this.parent_win.resetIconPosition(icon_status);
        }

        */
        /****
           * 
           * set Cry Icon
           * 
           * ******/
        /*
        public void setecryStatus(bool status)
        {
            if (status)
            {
                Uri uri = new Uri("Images/right_icon6-over.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                cry_button.Source = bitmap;
                //cry_sel.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                Uri uri = new Uri("Images/iconSetting/right_icon6-over-op.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                cry_button.Source = bitmap;
                //cry_sel.Visibility = System.Windows.Visibility.Hidden;

            }
        }

        private void cry_button_MouseEnter(object sender, MouseEventArgs e)
        {
            Uri uri = new Uri("Images/right_icon6-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            cry_button.Source = bitmap;

        }

        private void cry_button_MouseLeave(object sender, MouseEventArgs e)
        {
            setecryStatus(icon_status[7]);
        }

        private void cry_button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            icon_status[7] = !icon_status[7];
            setecryStatus(icon_status[7]);
            this.parent_win.resetIconPosition(icon_status);
        }
        */

        /****
          * 
          * set Rss Icon
          * 
          * ******/
        /*
        public void seterssStatus(bool status)
        {
            if (status)
            {
                Uri uri = new Uri("Images/rss-over.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                rss_button.Source = bitmap;
                //rss_sel.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                Uri uri = new Uri("Images/iconSetting/rss-over-op.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                rss_button.Source = bitmap;
                //rss_sel.Visibility = System.Windows.Visibility.Hidden;

            }
        }

        private void rss_button_MouseEnter(object sender, MouseEventArgs e)
        {
            Uri uri = new Uri("Images/rss-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            rss_button.Source = bitmap;
        }

        private void rss_button_MouseLeave(object sender, MouseEventArgs e)
        {
            seterssStatus(icon_status[8]);
        }

        private void rss_button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            icon_status[8] = !icon_status[8];
            seterssStatus(icon_status[8]);
            this.parent_win.resetIconPosition(icon_status);
        }
        */
        /****
        * 
        * set Blogger Icon
        * 
        * ******/
        /*
        public void setebloggerStatus(bool status)
        {
            if (status)
            {
                Uri uri = new Uri("Images/blogger-over.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                blogger_button.Source = bitmap;
                //blogger_sel.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                Uri uri = new Uri("Images/iconSetting/blogger-over-op.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                blogger_button.Source = bitmap;
                //blogger_sel.Visibility = System.Windows.Visibility.Hidden;

            }
        }


        private void blogger_button_MouseEnter(object sender, MouseEventArgs e)
        {
            Uri uri = new Uri("Images/blogger-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            blogger_button.Source = bitmap;
        }

        private void blogger_button_MouseLeave(object sender, MouseEventArgs e)
        {
            setebloggerStatus(icon_status[9]);
        }

        private void blogger_button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            icon_status[9] = !icon_status[9];
            setebloggerStatus(icon_status[9]);
            this.parent_win.resetIconPosition(icon_status);
        }
        */

        /****
      * 
      * set CNN Icon
      * 
      * ******/
        /*
        public void seteCNNStatus(bool status)
        {
            if (status)
            {
                Uri uri = new Uri("Images/cnn_icon-over.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                cnn_button.Source = bitmap;
                //cnn_sel.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                Uri uri = new Uri("Images/iconSetting/cnn_icon-over-op.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                cnn_button.Source = bitmap;
                //cnn_sel.Visibility = System.Windows.Visibility.Hidden;

            }
        }


        private void cnn_button_MouseEnter(object sender, MouseEventArgs e)
        {
            Uri uri = new Uri("Images/cnn_icon-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            cnn_button.Source = bitmap;
        }

        private void cnn_button_MouseLeave(object sender, MouseEventArgs e)
        {
            seteCNNStatus(icon_status[10]);
        }

        private void cnn_button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            icon_status[10] = !icon_status[10];
            seteCNNStatus(icon_status[10]);
            this.parent_win.resetIconPosition(icon_status);
        }


        */
        /****
        * 
        * set Flick Icon
        * 
        * ******/
        /*
        public void seteFlickstatus(bool status)
        {
            if (status)
            {
                Uri uri = new Uri("Images/right_icon8-over.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                flick_button.Source = bitmap;
                //flick_sel.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                Uri uri = new Uri("Images/iconSetting/right_icon8-over-op.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                flick_button.Source = bitmap;
                //flick_sel.Visibility = System.Windows.Visibility.Hidden;

            }
        }

        private void flick_button_MouseEnter(object sender, MouseEventArgs e)
        {
            Uri uri = new Uri("Images/right_icon8-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            flick_button.Source = bitmap;
        }

        private void flick_button_MouseLeave(object sender, MouseEventArgs e)
        {
            seteFlickstatus(icon_status[11]);
        }

        private void flick_button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            icon_status[11] = !icon_status[11];
            seteFlickstatus(icon_status[11]);
            this.parent_win.resetIconPosition(icon_status);
        }

        */
        /****
        * 
        * set Wiki Icon
        * 
        * ******/
        /*
        public void seteWikistatus(bool status)
        {
            if (status)
            {
                Uri uri = new Uri("Images/wikipedia_icon-over.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                wiki_button.Source = bitmap;
                //wiki_sel.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                Uri uri = new Uri("Images/iconSetting/wikipedia_icon-over-op.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                wiki_button.Source = bitmap;
                //wiki_sel.Visibility = System.Windows.Visibility.Hidden;

            }
        }

        private void wiki_button_MouseEnter(object sender, MouseEventArgs e)
        {
            Uri uri = new Uri("Images/wikipedia_icon-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            wiki_button.Source = bitmap;
        }

        private void wiki_button_MouseLeave(object sender, MouseEventArgs e)
        {
            seteWikistatus(icon_status[12]);
        }

        private void wiki_button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            icon_status[12] = !icon_status[12];
            seteWikistatus(icon_status[12]);
            this.parent_win.resetIconPosition(icon_status);
        }

        */
        /****
        * 
        * set QQ Icon
        * 
        * ******/
        /*
        public void seteQQstatus(bool status)
        {
            if (status)
            {
                Uri uri = new Uri("Images/Icon/qq-over.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                qq_button.Source = bitmap;
                //wiki_sel.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                Uri uri = new Uri("Images/Icon/qq-gray.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                qq_button.Source = bitmap;
                //wiki_sel.Visibility = System.Windows.Visibility.Hidden;

            }
        }

        private void qq_button_MouseEnter(object sender, MouseEventArgs e)
        {
            Uri uri = new Uri("Images/Icon/qq-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            qq_button.Source = bitmap;
        }

        private void qq_button_MouseLeave(object sender, MouseEventArgs e)
        {
            seteQQstatus(icon_status[14]);
        }

        private void qq_button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            icon_status[14] = !icon_status[14];
            seteQQstatus(icon_status[14]);
            this.parent_win.resetIconPosition(icon_status);
        }

        */

        /****
          * 
          * set Printrest Icon
          * 
          * ******/
        /*
        public void seteprintreststatus(bool status)
        {
            if (status)
            {
                Uri uri = new Uri("Images/Icon/printrest-over.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                printrest_button.Source = bitmap;
                //wiki_sel.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                Uri uri = new Uri("Images/Icon/printrest-gray.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                printrest_button.Source = bitmap;
                //wiki_sel.Visibility = System.Windows.Visibility.Hidden;

            }
        }


        private void printrest_button_MouseEnter(object sender, MouseEventArgs e)
        {
            Uri uri = new Uri("Images/Icon/printrest-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            printrest_button.Source = bitmap;
        }

        private void printrest_button_MouseLeave(object sender, MouseEventArgs e)
        {
            seteprintreststatus(icon_status[15]);
        }

        private void printrest_button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            icon_status[15] = !icon_status[15];
            seteprintreststatus(icon_status[15]);
            this.parent_win.resetIconPosition(icon_status);
        }

        */
        /****
          * 
          * set YELP Icon
          * 
          * ******/
        /*
        public void setyelpstatus(bool status)
        {
            if (status)
            {
                Uri uri = new Uri("Images/Icon/yelp-over.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                yelp_button.Source = bitmap;
                //wiki_sel.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                Uri uri = new Uri("Images/Icon/yelp-gray.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                yelp_button.Source = bitmap;
                //wiki_sel.Visibility = System.Windows.Visibility.Hidden;

            }
        }


        private void yelp_button_MouseEnter(object sender, MouseEventArgs e)
        {
            Uri uri = new Uri("Images/Icon/yelp-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            yelp_button.Source = bitmap;
        }

        private void yelp_button_MouseLeave(object sender, MouseEventArgs e)
        {
            setyelpstatus(icon_status[16]);
        }

        private void yelp_button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            icon_status[16] = !icon_status[16];
            setyelpstatus(icon_status[16]);
            this.parent_win.resetIconPosition(icon_status);
        }

        */
        /****
            * 
            * set foxNews Icon
            * 
            * ******/
        /*
        public void setfoxNewsstatus(bool status)
        {
            if (status)
            {
                Uri uri = new Uri("Images/Icon/foxNews-over.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                foxNews_button.Source = bitmap;
                //wiki_sel.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                Uri uri = new Uri("Images/Icon/foxNews-gray.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                foxNews_button.Source = bitmap;
                //wiki_sel.Visibility = System.Windows.Visibility.Hidden;

            }
        }

        private void foxNews_button_MouseEnter(object sender, MouseEventArgs e)
        {
            Uri uri = new Uri("Images/Icon/foxNews-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            foxNews_button.Source = bitmap;
        }

        private void foxNews_button_MouseLeave(object sender, MouseEventArgs e)
        {
            setfoxNewsstatus(icon_status[17]);
        }

        private void foxNews_button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            icon_status[17] = !icon_status[17];
            setfoxNewsstatus(icon_status[17]);
            this.parent_win.resetIconPosition(icon_status);
        }
        */
        /****
                * 
                * set Pandora Icon
                * 
                * ******/
        /*
        public void setpandoraMusicstatus(bool status)
        {
            if (status)
            {
                Uri uri = new Uri("Images/Icon/pandoraMusic-over.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                pandoraMusic_button.Source = bitmap;
                //wiki_sel.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                Uri uri = new Uri("Images/Icon/pandoraMusic-gray.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                pandoraMusic_button.Source = bitmap;
                //wiki_sel.Visibility = System.Windows.Visibility.Hidden;

            }
        }

        private void pandoraMusic_button_MouseEnter(object sender, MouseEventArgs e)
        {
            Uri uri = new Uri("Images/Icon/pandoraMusic-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            pandoraMusic_button.Source = bitmap;
        }

        private void pandoraMusic_button_MouseLeave(object sender, MouseEventArgs e)
        {
            setpandoraMusicstatus(icon_status[18]);
        }

        private void pandoraMusic_button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            icon_status[18] = !icon_status[18];
            setpandoraMusicstatus(icon_status[18]);
            this.parent_win.resetIconPosition(icon_status);
        }


        */
        /****
                   * 
                   * set Paypal Icon
                   * 
                   * ******/
        /*
        public void setPaypalstatus(bool status)
        {
            if (status)
            {
                Uri uri = new Uri("Images/Icon/paypal-over.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                paypal_button.Source = bitmap;
                //wiki_sel.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                Uri uri = new Uri("Images/Icon/paypal-gray.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                paypal_button.Source = bitmap;
                //wiki_sel.Visibility = System.Windows.Visibility.Hidden;

            }
        }
        private void paypal_button_MouseEnter(object sender, MouseEventArgs e)
        {
            Uri uri = new Uri("Images/Icon/paypal-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            paypal_button.Source = bitmap;
        }



        private void paypal_button_MouseLeave(object sender, MouseEventArgs e)
        {
            setPaypalstatus(icon_status[19]);
        }

        private void paypal_button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            icon_status[19] = !icon_status[19];
            setPaypalstatus(icon_status[19]);
            this.parent_win.resetIconPosition(icon_status);
        }



        /****
                   * 
                   * set ESPN Icon
                   * 
                   * ******/
        /*
        public void setEspnstatus(bool status)
        {
            if (status)
            {
                Uri uri = new Uri("Images/Icon/espn-over.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                espn_button.Source = bitmap;
                //wiki_sel.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                Uri uri = new Uri("Images/Icon/espn-gray.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                espn_button.Source = bitmap;
                //wiki_sel.Visibility = System.Windows.Visibility.Hidden;

            }
        }

        private void espn_button_MouseEnter(object sender, MouseEventArgs e)
        {
            Uri uri = new Uri("Images/Icon/espn-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            espn_button.Source = bitmap;
        }

        private void espn_button_MouseLeave(object sender, MouseEventArgs e)
        {
            setEspnstatus(icon_status[20]);
        }

        private void espn_button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            icon_status[20] = !icon_status[20];
            setEspnstatus(icon_status[20]);
            this.parent_win.resetIconPosition(icon_status);
        }


        /****
        * 
        * set Imdb Icon
        * 
        * ******/
        /*
        public void setIMDBstatus(bool status)
        {
            if (status)
            {
                Uri uri = new Uri("Images/Icon/imdb-over.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                imdb_button.Source = bitmap;
                //wiki_sel.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                Uri uri = new Uri("Images/Icon/imdb-gray.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                imdb_button.Source = bitmap;
                //wiki_sel.Visibility = System.Windows.Visibility.Hidden;
            }
        }
        private void imdb_button_MouseEnter(object sender, MouseEventArgs e)
        {
            Uri uri = new Uri("Images/Icon/imdb-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            imdb_button.Source = bitmap;
        }

        private void imdb_button_MouseLeave(object sender, MouseEventArgs e)
        {
            setIMDBstatus(icon_status[21]);
        }

        private void imdb_button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            icon_status[21] = !icon_status[21];
            setIMDBstatus(icon_status[21]);
            this.parent_win.resetIconPosition(icon_status);
        }


        /****
                     * 
                     * set WordPress Icon
                     * 
                     * ******/
        /*
        public void setWordPressstatus(bool status)
        {
            if (status)
            {
                Uri uri = new Uri("Images/Icon/wordpress-over.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                wordpress_button.Source = bitmap;
                //wiki_sel.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                Uri uri = new Uri("Images/Icon/wordpress-gray.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                wordpress_button.Source = bitmap;
                //wiki_sel.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void wordpress_button_MouseEnter(object sender, MouseEventArgs e)
        {
            Uri uri = new Uri("Images/Icon/wordpress-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            wordpress_button.Source = bitmap;
        }

        private void wordpress_button_MouseLeave(object sender, MouseEventArgs e)
        {
            setWordPressstatus(icon_status[22]);
        }

        private void wordpress_button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            icon_status[22] = !icon_status[22];
            setWordPressstatus(icon_status[22]);
            this.parent_win.resetIconPosition(icon_status);
        }

        /****
                        * 
                        * set netflix Icon
                        * 
                        * ******/
        /*
        public void setnetflixstatus(bool status)
        {
            if (status)
            {
                Uri uri = new Uri("Images/Icon/Netflix-over.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                netflix_button.Source = bitmap;
                //wiki_sel.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                Uri uri = new Uri("Images/Icon/Netflix-gray.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                netflix_button.Source = bitmap;
                //wiki_sel.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void netflix_button_MouseEnter(object sender, MouseEventArgs e)
        {
            Uri uri = new Uri("Images/Icon/Netflix-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            netflix_button.Source = bitmap;
        }

        private void netflix_button_MouseLeave(object sender, MouseEventArgs e)
        {
            setnetflixstatus(this.icon_status[23]);
        }

        private void netflix_button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            icon_status[23] = !icon_status[23];
            setWordPressstatus(icon_status[23]);
            this.parent_win.resetIconPosition(icon_status);
        }

        /****
        * 
        * set GMAIL Icon
        * 
        * ******/
        /*
        public void setGmailstatus(bool status)
        {
            if (status)
            {
                Uri uri = new Uri("Images/userprofile/gmail-over.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                gmail_icon.Source = bitmap;
                gmail.IsEnabled = true;
                gmail_pass.IsEnabled = true;
                //wiki_sel.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                Uri uri = new Uri("Images/userprofile/gmail-gray.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                gmail_icon.Source = bitmap;
                gmail.IsEnabled = false;
                gmail_pass.IsEnabled = false;
                //wiki_sel.Visibility = System.Windows.Visibility.Hidden;
            }
        }





        private void gmail_icon_MouseEnter(object sender, MouseEventArgs e)
        {
            Uri uri = new Uri("Images/userprofile/gmail-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            gmail_icon.Source = bitmap;
            //gmail.IsEnabled = true;
            // gmail_pass.IsEnabled = true;
        }



        private void gmail_icon_MouseLeave(object sender, MouseEventArgs e)
        {
            setGmailstatus(icon_status[24]);
        }

        private void gmail_icon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            icon_status[24] = !icon_status[24];
            setGmailstatus(icon_status[24]);
            this.parent_win.resetIconPosition(icon_status);
        }


        /****
       * 
       * set AOL Icon
       * 
       * ******/
        /*
        public void setAmailstatus(bool status)
        {
            if (status)
            {
                Uri uri = new Uri("Images/userprofile/aol-over.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                amail_icon.Source = bitmap;
                amail.IsEnabled = true;
                amail_pass.IsEnabled = true;
                //wiki_sel.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                Uri uri = new Uri("Images/userprofile/aol-gray.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                amail_icon.Source = bitmap;
                amail.IsEnabled = false;
                amail_pass.IsEnabled = false;
                //wiki_sel.Visibility = System.Windows.Visibility.Hidden;
            }
        }



        private void amail_icon_MouseEnter(object sender, MouseEventArgs e)
        {
            Uri uri = new Uri("Images/userprofile/aol-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            amail_icon.Source = bitmap;
            //amail.IsEnabled = true;
            //amail_pass.IsEnabled = true;
        }

        private void amail_icon_MouseLeave(object sender, MouseEventArgs e)
        {
            setAmailstatus(icon_status[25]);
        }

        private void amail_icon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            icon_status[25] = !icon_status[25];
            setAmailstatus(icon_status[25]);
            this.parent_win.resetIconPosition(icon_status);
        }




        /****
         * 
         * set YAHOO Icon
         * 
         * ******/
        /*
        public void setYmailstatus(bool status)
        {
            if (status)
            {
                Uri uri = new Uri("Images/userprofile/yao-over.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                ymail_icon.Source = bitmap;
                ymail.IsEnabled = true;
                ymail_pass.IsEnabled = true;
                //wiki_sel.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                Uri uri = new Uri("Images/userprofile/yao-gray.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                ymail_icon.Source = bitmap;
                ymail.IsEnabled = false;
                ymail_pass.IsEnabled = false;
                //wiki_sel.Visibility = System.Windows.Visibility.Hidden;
            }
        }


        private void ymail_icon_MouseEnter(object sender, MouseEventArgs e)
        {
            Uri uri = new Uri("Images/userprofile/yao-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            ymail_icon.Source = bitmap;
        }

        private void ymail_icon_MouseLeave(object sender, MouseEventArgs e)
        {
            setYmailstatus(this.icon_status[26]);
        }

        private void ymail_icon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            icon_status[26] = !icon_status[26];
            setYmailstatus(this.icon_status[26]);
            this.parent_win.resetIconPosition(icon_status);
        }


        /****
            * 
            * set HOT Mail Icon
            * 
            * ******/
        /*
        public void setHmailstatus(bool status)
        {
            if (status)
            {
                Uri uri = new Uri("Images/userprofile/hotmail-over.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                hmail_icon.Source = bitmap;
                hmail.IsEnabled = true;
                hmail_pass.IsEnabled = true;
                //wiki_sel.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                Uri uri = new Uri("Images/userprofile/hotmail-gray.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                hmail_icon.Source = bitmap;
                hmail.IsEnabled = false;
                hmail_pass.IsEnabled = false;
                //wiki_sel.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void hmail_icon_MouseEnter(object sender, MouseEventArgs e)
        {
            Uri uri = new Uri("Images/userprofile/hotmail-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            hmail_icon.Source = bitmap;
        }

        private void hmail_icon_MouseLeave(object sender, MouseEventArgs e)
        {
            setHmailstatus(this.icon_status[27]);
        }

        private void hmail_icon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            icon_status[27] = !icon_status[27];
            setHmailstatus(this.icon_status[27]);
            this.parent_win.resetIconPosition(icon_status);
        }




        public void setIcons(bool[] icons_state)
        {
            icon_status = icons_state;

            this.settwitterStatus(icon_status[0]);
            this.setfaceStatus(icon_status[1]);
            this.setinstaStatus(icon_status[2]);
            this.setinnStatus(icon_status[3]);
            this.setyutubeStatus(icon_status[4]);
            this.setebayStatus(icon_status[5]);
            this.seteamazonStatus(icon_status[6]);
            this.setecryStatus(icon_status[7]);
            this.seterssStatus(icon_status[8]);
            this.setebloggerStatus(icon_status[9]);
            this.seteCNNStatus(icon_status[10]);
            this.seteFlickstatus(icon_status[11]);
            this.seteWikistatus(icon_status[12]);
            this.setMapStatus(icon_status[13]);
            this.seteQQstatus(icon_status[14]);
            this.seteprintreststatus(icon_status[15]);
            this.setyelpstatus(icon_status[16]);
            this.setfoxNewsstatus(icon_status[17]);
            this.setpandoraMusicstatus(icon_status[18]);
            this.setPaypalstatus(icon_status[19]);
            this.setEspnstatus(icon_status[20]);
            this.setIMDBstatus(icon_status[21]);
            this.setWordPressstatus(icon_status[22]);
            this.setnetflixstatus(icon_status[23]);
            this.setGmailstatus(icon_status[24]);
            this.setAmailstatus(icon_status[25]);
            this.setYmailstatus(icon_status[26]);
            this.setHmailstatus(icon_status[27]);
        }
        */
        private void fName_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void fName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                lName.SelectAll();
                lName.Focus();
            }
        }

        private void lName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                eMail.SelectAll();
                eMail.Focus();
            }
        }

        private void eMail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                phone.SelectAll();
                phone.Focus();
            }
        }
        /*
        private void phone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                gmail.SelectAll();
                gmail.Focus();
            }
        }

        private void gmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                gmail_pass.SelectAll();
                gmail_pass.Focus();
            }
        }

        private void amail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                amail_pass.SelectAll();
                amail_pass.Focus();
            }
        }

        private void gmail_pass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                amail.SelectAll();
                amail.Focus();
            }
        }

        private void amail_pass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ymail.SelectAll();
                ymail.Focus();
            }
        }

        private void ymail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ymail_pass.SelectAll();
                ymail_pass.Focus();
            }
        }

        private void ymail_pass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                hmail.SelectAll();
                hmail.Focus();
            }
        }

        private void hmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                hmail_pass.SelectAll();
                hmail_pass.Focus();
            }
        }
        */
        private void hmail_pass_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void hack_icon_MouseEnter(object sender, MouseEventArgs e)
        {
            //Uri uri = new Uri("Images/userprofile/hack-over.png", UriKind.Relative);
            //var bitmap = new BitmapImage(uri);
            //hack_icon.Source = bitmap;
        }

        private void hack_icon_MouseLeave(object sender, MouseEventArgs e)
        {
           /* if (isHackerState)
            {                
                Uri uri = new Uri("Images/userprofile/hackOn.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                hack_icon.Source = bitmap;
            }
            else
            {                
                Uri uri = new Uri("Images/userprofile/hack.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                hack_icon.Source = bitmap;
            }*/
        }

        private void hack_icon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           /* isHackerState = !isHackerState;
            if (isHackerState)
            {
                    //hackBox.IsEnabled = true;
                    hackBox.Text = hackEmail;
                    hackLabl.Content = "Hacker Alert is Now ON";
                    Uri uri = new Uri("Images/userprofile/hackOn.png", UriKind.Relative);
                    var bitmap = new BitmapImage(uri);
                    hack_icon.Source = bitmap;
            }
            else
            {
                //hackBox.IsEnabled = false;
                hackLabl.Content = "Hacker Alert is Now OFF";
                Color clr = Color.FromRgb(93, 93, 93);
                hackBox.Background = new SolidColorBrush(clr);  
                Uri uri = new Uri("Images/userprofile/hack.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                hack_icon.Source = bitmap;
            }

            if (!isHackerState)
            {
                Color clr = Color.FromRgb(220, 220, 220);
                hackBox.Foreground = new SolidColorBrush(clr);
                clr = Color.FromRgb(93, 93, 93);
                hackBox.Background = new SolidColorBrush(clr);
            }
            else
            {
                //Color clr = Color.FromRgb(93, 93, 93);
                hackBox.Background = new SolidColorBrush(Colors.Transparent);
                hackBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
            */
        }

        private void hackBox_MouseEnter(object sender, MouseEventArgs e)
        {
           // Color clr = Color.FromRgb(122, 157, 147);
           // hackBox.Foreground = new SolidColorBrush(clr);

            
        }

        private void hackBox_MouseLeave(object sender, MouseEventArgs e)
        {
            /*if (!isHackerState)
            {
                Color clr = Color.FromRgb(93, 93, 93);
                hackBox.Background = new SolidColorBrush(clr);
                hackBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
            else
            {
                hackBox.Background = new SolidColorBrush(Colors.Transparent);
                hackBox.Foreground = new SolidColorBrush(Colors.Gray);
            }*/
        }

        private void hackBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (!isHackerState)
            {
                
                //hackBox.Text = "HACKER NOTIFICATION HAS BEEN TURNED OFF";
                return;
            }
                
        }

        private void hackBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!isHackerState)
            {
                //hackBox.Text = "HACKER NOTIFICATION HAS BEEN TURNED OFF";
                return;
            }
        }

        private void hackBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (!isHackerState)
            {

                //hackBox.Text = "HACKER NOTIFICATION HAS BEEN TURNED OFF";
                return;
            }
        }

    }
}
