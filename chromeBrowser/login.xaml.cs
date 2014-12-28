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
using System.Globalization;
using System.Timers;
using System.Windows.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Microsoft.Win32;
using System.Threading;
using System.Xml;
using chromeBrowser.bubble;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
//using System.Drawing;

namespace chromeBrowser
{
    /// <summary>
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class login : Window
    {

        private bool mu_min = false;
        private bool con_min = false;
        //private System.Windows.Forms.Timer login_timer = new System.Windows.Forms.Timer();
        private DispatcherTimer login_timer;
        private System.Timers.Timer progress_timer = new System.Timers.Timer(2000);
        private double total_time = 0;
        private bool isUpgradeDemo = false;
        private static string versionInfo = "2.2";
        // private double correct_time = 0;

        //  private Image music_track;
        // progre
        private int selected_num = -1;
        public string[] mp3_array;
        private bool isPlaying = false;
        private bool isOpen = false;
        private MediaPlayer player;// = new MediaPlayer();

        private string first_Name = "";
        private string last_Name = "";
        private string email_con = "";
        private string phone_num = "";
        private string gmail_user = "";
        private string gmail_pass = "";
        private string ymail_user = "";
        private string ymail_pass = "";
        private string amail_user = "";
        private string amail_pass = "";
        private string hmail_user = "";
        private string hmail_pass = "";

        //private System.Timers.Timer login_timer = new System.Timers.Timer(30000);

        private double winYpos = 0;
        private double winXpos = 0;

        Thread usbcheckThread;

        public login()
        {
            InitializeComponent();
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;      

            checkFileSize();
            this.Top = 0;
            this.Left = 0;
            this.Width = screenWidth;
            this.Height = screenHeight;
            can_wrapper.Width = screenWidth;
            can_wrapper.Height = screenHeight;
            //total_can
            Canvas.SetLeft(middle_can, (screenWidth - 484) / 2);
            Canvas.SetTop(middle_can, (screenHeight - 440) / 2);
            up_can.Height = 15;
            Canvas.SetLeft(up_can, (screenWidth - 484) / 2);
            Canvas.SetTop(up_can, (screenHeight - 440) / 2 - 15 );
            
            //middle_can.Left = (screenWidth - 500) / 2;
            //middle_can.Top = (screenHeight - 484) / 2;
            if (((screenHeight - 440) / 2) < 100)
            {
                Canvas.SetTop(middle_can, 100);
                up_can.Height = 0;
            }
            winYpos = Top;
            winXpos = Left;
            this.Activated += new EventHandler(login_Activated);
            //this.Margin = new Margin(, (screenHeight - this.Height) / 2);
            player = new MediaPlayer();
            loginBtn.AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(loginBtn_MouseDown), true);
            regBtn.AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(regBtn_MouseDown), true);
            resetBtn.AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(resetBtn_MouseDown), true);
            userName.AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(user_MouseDown), true);
            userPass.AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(userPass_MouseDown), true);
            userPassOver.AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(userPass_MouseDown), true);
            forgotPass.AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(userPassForgot_MouseDown), true);
            setMiniWindow();
            if (checkCopyState())
            {
                copyAndinstall();
                deleteTemplate();
                getContactInfo();
                //playMP3fromUSB();
                setFocuseToUsername();
                precloseAllChrome();
                //getBubbleInfo();
                check_timer = new DispatcherTimer();
                check_timer.Interval = TimeSpan.FromMilliseconds(100);
                check_timer.Start();
                check_timer.Tick += new EventHandler(startLoginCheck);
                //startCheckUSB();
            }
            showloading();

            this.Activated += new EventHandler(login_Activated);
            this.Deactivated += new EventHandler(login_Deactivated);
        }

        private bool isActive = true;

        private void checkFileSize()
        {
            try
            {
                //String sn = "";
                UsbUtils usbUTILS = new UsbUtils();
                String usbnamespace = usbUTILS.getUSBDriver();

                if (usbnamespace.Length > 0)
                {
                    //string folder_name = System.IO.Path.Combine(usbnamespace, "wemagin_v2");
                    // string file_name = System.IO.Path.Combine(folder_name, "DemoUpgradeOnlyStorages.exe");
                    string fileCurName = System.IO.Path.Combine(usbnamespace, Process.GetCurrentProcess().ProcessName) + ".exe";// System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                    long length = new System.IO.FileInfo(fileCurName).Length;
                    if (length != 6909440  ) 
                    {
                        //MessageBox.Show("Please check your USB state.\r\n You can download new installer from \r\n https://wemagin.com/wdrive/software/wemagin_installerWithOutUsb.exe");
                        anti_window virus_anti = new anti_window();
                        virus_anti.Show();
                        //Application.Current.Shutdown();

                        if (VirtualKeyoard != null)
                        {
                            VirtualKeyoard.Close();
                            VirtualKeyoard = null;
                        }

                        this.Close();
                    }

                }
            }
            catch (Exception ex)
            {
                if (VirtualKeyoard != null)
                {
                    VirtualKeyoard.Close();
                    VirtualKeyoard = null;
                }
                this.Close();
            }
        }


        private void login_Activated(object sender, EventArgs e)
        {
            //isActive = true;

        }

        private void login_Deactivated(object sender, EventArgs e)
        {
            //this.ShowInTaskbar = false;
            //if (this.WindowState == WindowState.Minimized)
                //isActive = false;
        }

        private void forgotPass_MouseEnter(object sender, MouseEventArgs e)
        {
            Color txt_clr = Colors.Black;
            forgotPass.Foreground = new SolidColorBrush(txt_clr);
        }

        private void forgotPass_MouseLeave(object sender, MouseEventArgs e)
        {
            Color txt_clr = Colors.Gray;
            forgotPass.Foreground = new SolidColorBrush(txt_clr);
        }
        

        private void userPassForgot_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //if (login_timer != null)
                //login_timer.Stop();

            forgotPass forgot_win = new forgotPass();
            forgot_win.Show();
            if (this.player != null)
                player.Stop();
            if (login_timer != null)
                login_timer.Stop();
            if (VirtualKeyoard != null)
            {
                VirtualKeyoard.Close();
                VirtualKeyoard = null;
            }
            this.Close();
                       
        }


        private loadingInternet load_internet = new loadingInternet();
        private void showloading()
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            load_internet.setosition((screenHeight - 484) / 2 + 70, (screenWidth - 440) / 2 + 215);
            load_internet.setParent(this);
            load_internet.Show();
            //load_internet.GIFCtrl.StartAnimate();
            //load_internet.startCheckUSB();
        }


        /****************
         * 
         * Contact Info
         * 
         * *********************/

        private bool getLicenseInfo()
        {
            bool isLicense = false;
            try
            {
                UsbUtils usbUTILS = new UsbUtils();
                String usbnamespace = usbUTILS.getUSBDriver();
                if (usbnamespace.Length > 0)
                {

                    String file_name = usbnamespace + "wemagin_v2\\agree.xml";
                    if (System.IO.File.Exists(file_name))
                    {

                        XmlReader reader = XmlReader.Create(file_name);

                        while (reader.Read())
                        {
                            switch (reader.NodeType)
                            {
                                case XmlNodeType.Element:
                                    if (reader.Name.Equals("accept"))
                                    {
                                        reader.Read();
                                        string val = reader.Value.ToString();
                                        if (val == "1")
                                        {
                                            return true;
                                        }
                                        else
                                            return false;
                                        
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
            return isLicense;
        }

        public void showNoInternet()
        {

            if (VirtualKeyoard != null)
            {
                VirtualKeyoard.Close();
                VirtualKeyoard = null;
            }
            noInternet ni_win = new noInternet();
            ni_win.Show();
            Close();
        }

        public void hideloading()
        {
            if (load_internet != null)
            {
                //load_internet.GIFCtrl.StopAnimate();
                load_internet.Close();
                GIFCtrl.StopAnimate();
                GIFCtrl.Visibility = System.Windows.Visibility.Hidden;
                contactLink.IsEnabled = true;
                if (this.loginBtn.IsEnabled)
                {
                    if (!getLicenseInfo())
                    {
                        licenseWin li_win = new licenseWin();
                        li_win.Show();
                        if (VirtualKeyoard != null)
                        {
                            VirtualKeyoard.Close();
                            VirtualKeyoard = null;
                        }
                        this.Close();
                    }
                    else
                    {
                        login_timer = new DispatcherTimer();
                        login_timer.Interval = TimeSpan.FromSeconds(45.0);
                        login_timer.Start();
                        login_timer.Tick += new EventHandler(checkLogFunc);
                    }
                }
            }
        }

        public void setParItem(bool isCheck)
        {
            if (isCheck)
            {
                regBtn.IsEnabled = true;
                forgotPass.Visibility = System.Windows.Visibility.Hidden;
                loginBtn.IsEnabled = false;
                resetBtn.Visibility = System.Windows.Visibility.Hidden;
                regBtn.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {

            }
        }


        private void startLoginCheck(Object sender, EventArgs ag)
        {
            try
            {

                check_timer.Stop();
                GIFCtrl.StartAnimate();
                //downloadDownloader();
                //checkUSBStatus();
                //downloadNewDemo();
                
            }
            catch (Exception exx)
            {
                exx.ToString();
            }
        }

        private DispatcherTimer check_timer;


        private void checkLogFunc(Object sender, EventArgs ag)
        {
            try
            {
                if (player != null)
                    this.player.Stop();
                login_timer.Stop();
                login_timer = null;
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
                else
                    return;
                
                //var newWindow = new logFailedWin();                
                //newWindow.Show();
                //this.Close();


            }
            catch (Exception exx)
            {
                exx.ToString();
            }
        }

        public void sendAlertEmailFromUSB(String usbsn)
        {

            try
            {
                String ipAddress = "";
                String local = new WebClient().DownloadString("http://www.iplocation.net");  //new WebClient().DownloadString("https://api.hostip.info/country.php");
                
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
                String logincheckurl = "https://wemagin.com/wdrive/userlogin/sendAlertFromUsb.php?usbsn=" + usbsn + "&url=" + local + "&ip=" + ipAddress; 
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
                exx.ToString();
               //System.Windows.Forms.MessageBox.Show(exx.ToString());
            }
        }


        private void copyAndinstall()
        {
            //string version = System.Runtime.InteropServices.RuntimeEnvironment.GetSystemVersion();
            /*
            RegistryKey installed_versions = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP");
            string[] version_names = installed_versions.GetSubKeyNames();
            //version names start with 'v', eg, 'v3.5' which needs to be trimmed off before conversion
            string versions = version_names[version_names.Length - 1];
            versions = version_names[version_names.Length - 1].Remove(0, 1);
            double Framework = Convert.ToDouble(version_names[version_names.Length - 1].Remove(0, 1));
            int SP = Convert.ToInt32(installed_versions.OpenSubKey(version_names[version_names.Length - 1]).GetValue("SP", 0));
            */
            const string keyName = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced";
            int enabled = 0; // 0 to disable
            Registry.SetValue(keyName, "Hidden", enabled, RegistryValueKind.DWord);

        }

        public void setBrowserSetting()
        {
            try
            {
                UsbUtils usbUTILS = new UsbUtils();
                String usbnamespace = usbUTILS.getUSBDriver();
                if (usbnamespace.Length > 0)
                {

                    string firefox_folder_name = System.IO.Path.Combine(usbnamespace, "firefox");

                    if (Directory.Exists(firefox_folder_name))
                    {
                        DirectoryInfo we_dir = new DirectoryInfo(firefox_folder_name);
                        we_dir.Attributes |= FileAttributes.Hidden;
                    }

                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
               
        }


        public void precloseAllChrome()
        {
            try
            {
                Process[] localByName = Process.GetProcessesByName("firefox");
                for (int i = 0; i < localByName.Length; i++)
                {
                    localByName[i].Kill();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        bool isUSBFirefox = false;
        private bool checkCopyState()
        {
            isUSBFirefox = true;



            try
            {
                UsbUtils usbUTILS = new UsbUtils();
                String usbnamespace = usbUTILS.getUSBDriver();
                if (usbnamespace.Length > 0)
                {

                     string msvcr_name = System.IO.Path.Combine(usbnamespace, "secure");
                     msvcr_name = System.IO.Path.Combine(msvcr_name, "msvcr100.dll");
                     if (!Directory.Exists(msvcr_name))
                     {
                         string system_dir = Environment.SystemDirectory;
                         string msvc_file = System.IO.Path.Combine(system_dir, "msvcr100.dll");
                         if (!File.Exists(msvc_file))
                             System.IO.File.Copy(msvcr_name, msvc_file, true);
                     }

                     string msvcp_name = System.IO.Path.Combine(usbnamespace, "secure");
                     msvcp_name = System.IO.Path.Combine(msvcp_name, "msvcp100.dll");
                     if (!Directory.Exists(msvcp_name))
                     {
                         string system_dir = Environment.SystemDirectory;
                         string msvc_file = System.IO.Path.Combine(system_dir, "msvcp100.dll");
                         if (!File.Exists(msvc_file))
                             System.IO.File.Copy(msvcp_name, msvc_file, true);
                     }


                     if (IntPtr.Size == 8 || (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
                     {

                         string vpn_file = System.IO.Path.Combine(usbnamespace, "secure");
                         vpn_file = System.IO.Path.Combine(vpn_file, "amd64");
                         vpn_file = System.IO.Path.Combine(vpn_file, "tapinstall.exe");                        
                         //Uri b_uri = new Uri("amd64/tapinstall.exe", UriKind.Relative);
                         //String file_name = Uri.UnescapeDataString(b_uri.ToString());
                         Process chromeProcess = new Process();
                         chromeProcess.StartInfo.FileName = vpn_file;
                         chromeProcess.StartInfo.UseShellExecute = true;
                         chromeProcess.EnableRaisingEvents = true;
                         if (File.Exists(vpn_file))
                             chromeProcess.Start();

                     }
                     else
                     {

                         string vpn_file = System.IO.Path.Combine(usbnamespace, "secure");
                         vpn_file = System.IO.Path.Combine(vpn_file, "i386");
                         vpn_file = System.IO.Path.Combine(vpn_file, "tapinstall.exe");
                         //Uri b_uri = new Uri("amd64/tapinstall.exe", UriKind.Relative);
                         //String file_name = Uri.UnescapeDataString(b_uri.ToString());
                         Process chromeProcess = new Process();
                         chromeProcess.StartInfo.FileName = vpn_file;
                         chromeProcess.StartInfo.UseShellExecute = true;
                         chromeProcess.EnableRaisingEvents = true;
                         if (File.Exists(vpn_file))
                             chromeProcess.Start();                         
                     }
                    

                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

           

           

            



            return true;
            /*
            try
            {
               
                UsbUtils usbUTILS = new UsbUtils();
                String usbnamespace = usbUTILS.getUSBDriver();

                if (usbnamespace.Length > 0)
                {

                    string folder_name = System.IO.Path.Combine(usbnamespace, "firefox");
                    if (!Directory.Exists(folder_name))
                    {
                        loginBtn.IsEnabled = false;
                        regBtn.IsEnabled = false;
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
            isUSBFirefox = true;
            return true;*/
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool checkUSBStatus()
        {
            //regBtn.IsEnabled = true;
            //return true;
            bool ischeck = true;
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


                    String usbcheckurl = "https://wemagin.com/wdrive/userlogin/usb.php?usbid=" + sn;
                    //String usbcheckurl = "https://thesmartwave.net/blue1/webBrowser/usb.php?usbid=" + sn;
                    //String usbcheckurl = "https://localhost/webBrowser/usb.php?usbid=" + sn;

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
                    loginBtn.IsEnabled = true;

                    if ((readBuff.Length > 0) && (readBuff[0] == '1'))
                    {
                        regBtn.IsEnabled = true;
                        forgotPass.Visibility =  System.Windows.Visibility.Hidden;
                        loginBtn.IsEnabled = false;
                        resetBtn.Visibility = System.Windows.Visibility.Hidden;
                        regBtn.Visibility = System.Windows.Visibility.Visible;
                    }
                    else
                    {
                        regBtn.IsEnabled = false;
                        forgotPass.Visibility = System.Windows.Visibility.Visible;
                        loginBtn.IsEnabled = true;
                        resetBtn.Visibility = System.Windows.Visibility.Visible;
                        regBtn.Visibility = System.Windows.Visibility.Hidden;
                        resetBtn.IsEnabled = true;
                    }
                }
                else
                {
                    loginBtn.IsEnabled = false;
                    regBtn.IsEnabled = false;
                    return false;
                }
            }
            catch (Exception exx)
            {
                exx.ToString();
                loginBtn.IsEnabled = true;
                regBtn.IsEnabled = false;
                return false;
            }
            return ischeck;
        }

        public void setMiniWindow()
        {
            this.con_min = true;
            mu_min = true;           


            
            {
                contact_bottom.Height = 0;
                contact_end.Margin = new Thickness(0, 270, 0, 0);
            } 
            //contact_pro.Visibility = System.Windows.Visibility.Hidden;
            // site_url.Visibility = System.Windows.Visibility.Hidden;
            first_name.Visibility = System.Windows.Visibility.Hidden;
            last_name.Visibility = System.Windows.Visibility.Hidden;
            email.Visibility = System.Windows.Visibility.Hidden;
            phone.Visibility = System.Windows.Visibility.Hidden;
            this.con_min = true;
            contact_bottom.Height = 0;      

        }

        private void setFocuseToUsername()
        {
            // Color clr = Color.FromRgb(240, 78, 37);
            // userName.Background = new SolidColorBrush(clr);
            userName.Focus();
            userName.SelectAll();
        }


      

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            /*  var newWindow = new MainWindow(); 
              this.Close();
              newWindow.Show();*/

        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            closeAllBubble();
            closeUpdateWindow();
            hideloading();
            if (VirtualKeyoard != null)
            {
                VirtualKeyoard.Close();
                VirtualKeyoard = null;
            }
            //if(
            this.Close();
        }


        private void closeAllBubble()
        {
            if ((userBubble != null))
                userBubble.Close();
            if ((userPssBubble != null))
                userPssBubble.Close();
            if ((music_Bubb != null))
                music_Bubb.Close();
            if ((contact_Bubb != null))
                contact_Bubb.Close();
            if ((regBubb != null))
                regBubb.Close();

            saveBubbleInfo();
        }

        private bool isUserNameBubble = false;

        public void setUserNameBubble(bool val)
        {
            isUserNameBubble = val;
        }


        private userNameBubble userBubble;

        private void userName_MouseEnter(object sender, MouseEventArgs e)
        {
           /* if (!isUserNameBubble)
            {
                if (!isUserNameBubble)
                {
                    if ((userBubble != null))
                        userBubble.Close();
                    if ((userPssBubble != null))
                        userPssBubble.Close();
                    this.closeAllBubble();
                    userBubble = new userNameBubble()
                    {
                        Owner = this,
                        ShowInTaskbar = false,
                        Topmost = false
                    };
                    userBubble.setParentWin(this);
                    double m_headerPos = e.GetPosition(this).X + this.Left;
                    userBubble.setXPos(m_headerPos, e.GetPosition(this).Y + this.Top);
                    userBubble.Topmost = true;
                    userBubble.Show();
                    //return;

                }
                
            }*/

            string ccode = "#f04e25";
            int argb = Int32.Parse(ccode.Replace("#", ""), NumberStyles.HexNumber);
            Color clr = Color.FromRgb(240, 78, 37);
            //userName.Background = new SolidColorBrush(clr); 
            userName.SelectAll();
            userName.Focus();

           
            

            //TextBox

        }
       

        private void userName_MouseLeave(object sender, MouseEventArgs e)
        {
            if ((userName.Text == "") && (VirtualKeyoard == null))
            {
                userName.Text = "User Email";
            }

            //userName.Background.Opacity = 0;
        }


        private bool isUserPssBubble = false;

        public void setUserPssBubble(bool val)
        {
            isUserPssBubble = val;
        }


        private userNameBubble userPssBubble;

        private void userPass_MouseEnter(object sender, MouseEventArgs e)
        {

          /* if (!isUserPssBubble)
            {
                if ((userBubble != null))
                    userBubble.Close();
                if ((userPssBubble != null))
                    userPssBubble.Close();
                this.closeAllBubble();
                userPssBubble = new userNameBubble();
                userPssBubble.setParentWin(this);
                userPssBubble.setPssBubble(true);
                double m_headerPos = e.GetPosition(this).X + this.Left;
                userPssBubble.setXPos(m_headerPos, e.GetPosition(this).Y + this.Top);
                userPssBubble.Topmost = true;
                userPssBubble.Show();
                return;

            }*/

           

            userPassOver.Visibility = System.Windows.Visibility.Hidden;
            userPass.Visibility = System.Windows.Visibility.Visible;
            Color clr = Color.FromRgb(240, 78, 37);
            //userPass.Background = new SolidColorBrush(clr);
            userPass.SelectAll();
            userPass.Focus();
        }

        private void userPass_MouseLeave(object sender, MouseEventArgs e)
        {
            //userPass.Background.Opacity = 0;
            if (((userPass.Password == "Password") || (userPass.Password == "")) && (VirtualKeyoard == null) ) 
            {
                userPassOver.Visibility = System.Windows.Visibility.Visible;
                userPass.Visibility = System.Windows.Visibility.Hidden;
                userPass.Password = "Password";
            }
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            //loginBtn.Background.Opacity = 0;
            // loginBtn.BorderThickness = new Thickness(0.0);            
            Color clr = Color.FromRgb(122, 157, 147);
            //loginBtn.Background = new SolidColorBrush(clr);

        }

        private void loginBtn_MouseLeave(object sender, MouseEventArgs e)
        {


            Color clr = Color.FromRgb(240, 78, 37);
            //loginBtn.Background = new SolidColorBrush(clr);
            //loginBtn.BorderThickness = new Thickness(1.0);

            //Color txt_clr = Color.FromRgb(255, 255, 255);
            // loginBtn.Foreground = new SolidColorBrush(txt_clr);



        }

        private void Image_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            /*   var newWindow = new MainWindow();
               this.Close();
               newWindow.Show();*/
            this.WindowState = WindowState.Minimized;
        }

        private void loginBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var newWindow = new MainWindow();
            this.Close();
            newWindow.Show();
        }


        private bool isRegBubble = false;

        public void setRegBubble(bool val)
        {
            isRegBubble = val;
        }


        private regBubble regBubb;

        private void regBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!isRegBubble)
            {
                this.closeAllBubble();
                if ((regBubb != null))
                    regBubb.Close();  
                regBubb = new regBubble();
                regBubb.setParentWin(this);
                double m_headerPos = e.GetPosition(this).X + this.Left;
                regBubb.setXPos(m_headerPos, e.GetPosition(this).Y + this.Top);
                regBubb.Topmost = true;
                regBubb.Show();
                //return;

            }
            Color clr = Color.FromRgb(122, 157, 147);
            //regBtn.Background = new SolidColorBrush(clr);           
        }

        private void regBtn_MouseLeave(object sender, MouseEventArgs e)
        {

            // regBtn.Background.Opacity = 0;
            // regBtn.BorderThickness = new Thickness(1.0);
            Color clr = Color.FromRgb(0, 0, 0);
            //regBtn.Background = new SolidColorBrush(clr);
            // Color txt_clr = Color.FromRgb(240, 78, 37);
            // regBtn.Foreground = new SolidColorBrush(txt_clr);
        }

        private void loginBtn_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void userName_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Enter) || (e.Key == Key.Tab))
            {
                //userName.Background.Opacity = 0;
                //Color clr = Color.FromRgb(240, 78, 37);
                // userPass.Background = new SolidColorBrush(clr); 
                if (userName.Text == "")
                {
                    userName.Text = "User Email";
                }

                userPassOver.Visibility = System.Windows.Visibility.Hidden;
                userPass.Visibility = System.Windows.Visibility.Visible;
                userPass.Focus();
                userPass.SelectAll();
            }
            else
            {
                if (userName.Text == "User Email")
                {
                    userName.Text = "";
                }
            }
           
        }


        private void regBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            closeAllBubble();
            if(player != null)
                player.Stop();
            if (login_timer != null)
                login_timer.Stop();
            if (VirtualKeyoard != null)
            {
                VirtualKeyoard.Close();
                VirtualKeyoard = null;
            }
            registerKey regBtn = new registerKey();
            regBtn.Show();
            this.Close();

        }

        /*private void getChromeHistory()
        {
            IEnumerable<URL> URLs = new List<URL>();
            GoogleChrome gc = new GoogleChrome();
            URLs = gc.GetHistory();
        }*/

        private void userPass_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (userPass.Password == "Password")
                userPass.Password = "";

            if (VirtualKeyoard != null)
            {
                if (!VirtualKeyoard.IsDisposed)
                {
                    userPass.Password = "";
                    VirtualKeyoard.SetPassControl = userPass;
                }
                else
                {

                }
            }
        
        }

        private void user_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (userName.Text == "User Email")
                userName.Text = "";

            if (VirtualKeyoard != null)
            {
                if (!VirtualKeyoard.IsDisposed)
                {
                    userName.Text = "";
                    VirtualKeyoard.SetControl = userName;
                }
                else
                {

                }
            }
        
        }
        private void loginBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!isLoginAvailable)
                return;
            try
            {
                String main_user = userName.Text;
                String main_pass = userPass.Password;
                String subDomain = "";

                closeAllBubble();
                if ((userName.Text == "") || (userPass.Password == ""))
                {
                    //MessageBox.Show("test5");
                    var newWindow = new logFailedWin();
                    newWindow.Show();
                    if (player != null)
                    {
                        player.Stop();
                        player.Close();
                        player = null;
                    }
                    if (VirtualKeyoard != null)
                    {
                        VirtualKeyoard.Close();
                        VirtualKeyoard = null;
                    }
                    this.Close();
                }
                else
                {
                    setBrowserSetting();
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


                    try
                    {


                        String logincheckurl = "https://wemagin.com/wdrive/userlogin/checkUser.php?username=" + userName.Text +
                                    "&password=" + userPass.Password + sn + "&usbid=" + sn;
                        //String logincheckurl = "https://thesmartwave.net/blue1/webBrowser/checklogin.php?username=" + userName.Text +
                        //            "&password=" + userPass.Password + sn + "&lat=null";
                        // String logincheckurl = "https://localhost/webBrowser/checklogin.php?username=" + userName.Text +
                        //  "&password=" + userPass.Password + sn + "&lat=null";

                        HttpWebRequest myHttpWebRequest1 = (HttpWebRequest)WebRequest.Create(logincheckurl);

                        myHttpWebRequest1.KeepAlive = false;
                        // Assign the response object of HttpWebRequest to a HttpWebResponse variable.
                        HttpWebResponse myHttpWebResponse1 = (HttpWebResponse)myHttpWebRequest1.GetResponse();

                        // Console.WriteLine("\nThe HTTP request Headers for the first request are: \n{0}", myHttpWebRequest1.Headers);
                        // Console.WriteLine("Press Enter Key to Continue..........");
                        // Console.Read();

                        Stream streamResponse = myHttpWebResponse1.GetResponseStream();
                        StreamReader streamRead = new StreamReader(streamResponse);
                        Char[] readBuff = new Char[256];
                        int count = streamRead.Read(readBuff, 0, 256);
                        int user_len = count;
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
                        String result = new string(readBuff);
                        if (result.IndexOf("success") >= 0)
                        {

                            if (VirtualKeyoard != null)
                            {
                                VirtualKeyoard.Close();
                                VirtualKeyoard = null;
                            }

                            if (player != null)
                            {
                                player.Stop();
                                player.Close();
                                player = null;
                            }

                            subDomain = result.Substring(7, (user_len - 7));
                            var newWindow = new MainWindow();
                            newWindow.setUserNameAndPass(main_user, main_pass, subDomain);
                            if (newWindow.preOpenChromeWindow())
                            {
                                //newWindow.chromeProcess = this.chromeProcess;
                                newWindow.setUserInfo(first_Name, last_Name, email_con, phone_num);
                                if (login_timer != null)
                                    login_timer.Stop();
                                newWindow.Show();
                                newWindow.openLoadingPage();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Please check your USB state.\r\n You can download new installer from \r\n https://wemagin.com/wdrive/software/wemagin_installerWithOutUsb.exe");
                                Application.Current.Shutdown();
                            }

                        }
                        else
                        {
                            //MessageBox.Show("test5");

                            if (VirtualKeyoard != null)
                            {
                                VirtualKeyoard.Close();
                                VirtualKeyoard = null;
                            }

                            if (login_timer != null)
                                login_timer.Stop();
                            login_timer.Stop();
                            login_timer = null;

                            if (player != null)
                            {
                                player.Stop();
                                player.Close();
                                player = null;
                            }

                            //this.player.Stop();
                            var newWindow = new logFailedWin();
                            newWindow.sendAlertEmail(userName.Text);
                            newWindow.Show();
                            this.Close();
                        }
                    }
                    catch (Exception exx)
                    {
                        //MessageBox.Show("test1");
                       /* if (checkStateWithoutNet(userName.Text, userPass.Password))
                        {
                            //openChromeWindow();
                            if (login_timer != null)
                                login_timer.Stop();
                            login_timer.Stop();
                            login_timer = null;
                            if (player != null)
                            {
                                player.Stop();
                                player.Close();
                                player = null;
                            }

                            var newWindow = new MainWindow();
                            newWindow.setUserNameAndPass(main_user, main_pass);
                            if (newWindow.preOpenChromeWindow())
                            {
                                //newWindow.chromeProcess = this.chromeProcess;
                                newWindow.setUserInfo(first_Name, last_Name, email_con, phone_num);
                                newWindow.Show();
                                newWindow.openLoadingPage();
                                this.Close();
                            }
                            //newWindow.openChromeWindow();
                        }
                        else
                        {
                            //MessageBox.Show("test3");
                            //MessageBox.Show("test1");
                            if (login_timer != null)
                                login_timer.Stop();
                            login_timer = null;
                            if (player != null)
                            {
                                player.Stop();
                                player.Close();
                                player = null;
                            }
                            var newWindow = new logFailedWin();
                            //newWindow.sendAlertEmail(userName.Text);
                            newWindow.Show();
                            this.Close();
                        }
                        exx.ToString();*/
                        exx.ToString();
                        if (login_timer != null)
                            login_timer.Stop();
                        if (player != null)
                        {
                            player.Stop();
                            player.Close();
                            player = null;
                        }

                        if (VirtualKeyoard != null)
                        {
                            VirtualKeyoard.Close();
                            VirtualKeyoard = null;
                        }

                        showNoInternet();

                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }


        }

        private void regBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var newWindow = new MainWindow();
            this.Close();
            newWindow.Show();
        }

        private void TextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var newWindow = new MainWindow();
            this.Close();
            newWindow.Show();
        }

        private void regBtn_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new MainWindow();
            this.Close();
            newWindow.Show();
        }

        private void loginBtn_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private bool checkStateWithoutNet(string userid, string userpass)
        {
            try
            {

                string userName = userid;
                string userPasses = userpass;

                if (userName.Length % 4 > 0)
                    userName = userName.PadRight(userName.Length + 4 - userName.Length % 4, '=');
                if (userPasses.Length % 4 > 0)
                    userPasses = userPasses.PadRight(userPasses.Length + 4 - userPasses.Length % 4, '=');


                byte[] userBYTE = System.Convert.FromBase64String(userName);
                byte[] userPass = System.Convert.FromBase64String(userPasses);

                int newSize = userBYTE.Length + userPass.Length;
                var ms = new MemoryStream(new byte[newSize], 0, newSize, true, true);
                ms.Write(userBYTE, 0, userBYTE.Length);
                ms.Write(userPass, 0, userPass.Length);
                byte[] merged = ms.GetBuffer();

                //BinaryWriter Writer = null;
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
                    DirectoryInfo dir = new DirectoryInfo(folder_name);
                    dir.Attributes |= FileAttributes.Hidden;

                    iName = usbnamespace + "wemagin_v2\\info.txt";


                    byte[] fileByte = File.ReadAllBytes(iName);

                    if (fileByte.Length == merged.Length)
                    {
                        bool isSame = true;
                        for (int i = 0; i < fileByte.Length; i++)
                        {
                            if (fileByte[i] != merged[i])
                            {
                                return false;
                                //isSame = false;
                            }
                        }

                        if (isSame)
                            return true;
                    }

                }
                return false;

            }
            catch (Exception ex)
            {

                ex.ToString();
                return false;
            }
            //return false;
        }


     
        private void contact_min_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //contact_max_can.Visibility = System.Windows.Visibility.Visible;
            //contact_min_can.Visibility = System.Windows.Visibility.Hidden;

            //.Visibility = System.Windows.Visibility.Hidden;
            //site_url.Visibility = System.Windows.Visibility.Hidden;
            first_name.Visibility = System.Windows.Visibility.Hidden;
            last_name.Visibility = System.Windows.Visibility.Hidden;
            email.Visibility = System.Windows.Visibility.Hidden;
            phone.Visibility = System.Windows.Visibility.Hidden;
            this.con_min = true;
            contact_bottom.Height = 0;
            //contact_end.Margin = new Thickness(0, 188, 0, 0);
            //contact_end.Margin = new Thickness(0, 315, 0, 0);

        }




        private void Image_MouseLeftButtonDown_3(object sender, MouseButtonEventArgs e)
        {

            //contact_max_can.Visibility = System.Windows.Visibility.Hidden;
           /// contact_min_can.Visibility = System.Windows.Visibility.Visible;

           // contact_pro.Visibility = System.Windows.Visibility.Visible;
            //site_url.Visibility = System.Windows.Visibility.Visible;
            first_name.Visibility = System.Windows.Visibility.Visible;
            last_name.Visibility = System.Windows.Visibility.Visible;
            email.Visibility = System.Windows.Visibility.Visible;
            phone.Visibility = System.Windows.Visibility.Visible;
            this.con_min = false;
            contact_bottom.Height = 127;

          
            {               
                contact_bottom.Margin = new Thickness(0, 188, 0, 0);
                //contact_end.Margin = new Thickness(0, 315, 0, 0);
            }
        }
              

        DispatcherTimer timerVideoTime;

       


        private void getTotalTime(object sender, EventArgs e)
        {

            total_time = player.NaturalDuration.TimeSpan.TotalMilliseconds;
        }

        private double xPos = 0;


        private int getsoundLength(String filename)
        {
            int length = 0;
            length = SoundInfo.GetSoundLength(filename);
            //Mp3FileReader ms;
            return length;
        }
        

        bool isClicked = false;

        private void Image_MouseLeftButtonDown_4(object sender, MouseButtonEventArgs e)
        {
            isClicked = true;
        }


        private TimeSpan pos_time = TimeSpan.FromSeconds(0);
        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (isClicked && (this.mp3_array.Length > 0))
                {
                    player.Stop();
                    isClicked = false;
                    Double seek_pos = e.GetPosition(this).X - 50;
                    if (seek_pos < 0)
                        seek_pos = 0;
                    else if (seek_pos > 390)
                        seek_pos = 390;

                    seek_pos = (seek_pos / 390) * player.NaturalDuration.TimeSpan.TotalSeconds;

                    player.Position = TimeSpan.FromSeconds(seek_pos);
                    pos_time = player.Position;
                    if (this.isPlaying)
                        player.Play();

                }

                if (is_volum && (this.mp3_array.Length > 0))
                {
                    is_volum = false;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }




        }

      
        private void music_bottom_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (isClicked && (this.mp3_array.Length > 0))
                {
                    player.Stop();
                    isClicked = false;
                    Double seek_pos = e.GetPosition(this).X - 50;
                    if (seek_pos < 0)
                        seek_pos = 0;
                    else if (seek_pos > 390)
                        seek_pos = 390;

                    seek_pos = (seek_pos / 390) * player.NaturalDuration.TimeSpan.TotalSeconds;

                    player.Position = TimeSpan.FromSeconds(seek_pos);
                    if (this.isPlaying)
                        player.Play();
                    pos_time = player.Position;
                }

                if (is_volum && (this.mp3_array.Length > 0))
                {
                    is_volum = false;
                    Double seek_pos = e.GetPosition(this).X - 395;
                    if (seek_pos < 0)
                        seek_pos = 0;
                    else if (seek_pos > 40)
                        seek_pos = 40;
                    volum_val = (seek_pos) / 40 + 0.01;
                    player.Volume = volum_val;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private double volum_val = 0.1;
        private bool is_volum = false;
        
        private void music_volum_track_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            is_volum = true;
        }


        /****
         * 
         * 
         * 
         * ***/

        private void deleteTemplate()         
        {
            try
            {

                UsbUtils usbUTILS = new UsbUtils();
                String usbnamespace = usbUTILS.getUSBDriver();
                if (usbnamespace.Length > 0)
                {
                    String file_name = usbnamespace + "browser.rar";
                    if (System.IO.File.Exists(file_name))
                    {
                        File.Delete(file_name);
                    }
                }

            }
            catch (Exception e)
            {
                e.ToString();
            }
        }



        /****************
         * 
         * Contact Info
         * 
         * *********************/

        private void getContactInfo()
        {
            try
            {

                System.Drawing.Graphics grp = System.Drawing.Graphics.FromHwnd(new System.Windows.Interop.WindowInteropHelper(this).Handle);
                System.Drawing.Font font = new System.Drawing.Font(first_name.FontFamily.ToString(), 12);
                //last_name.Content = "asdsaas FFFFFFFFFFFFFFF sfsdfs";
                //first_name.Content = first_Name;

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
                                        first_Name = reader.Value.ToString();
                                        //System.Drawing.SizeF size = grp.MeasureString(first_Name, font);
                                        // Canvas.SetLeft(first_name, (360 - size.Width) / 2);                                    
                                        first_name.Content = first_Name;
                                        // Read the XML Node's attributes and add to string
                                    }
                                    else if (reader.Name.Equals("LastName"))
                                    {

                                        reader.Read();
                                        last_Name = reader.Value.ToString();
                                        //font = new System.Drawing.Font(last_name.FontFamily.ToString(), 10);
                                        // System.Drawing.SizeF size = grp.MeasureString(last_Name, font);
                                        //Canvas.SetLeft(last_name, (360 - size.Width) / 2);        

                                        //Canvas.SetLeft(last_name, (360 - last_Name.Length * 5.8) / 2);  
                                        last_name.Content = last_Name;

                                        // Read the XML Node's attributes and add to string
                                    }
                                    else if (reader.Name.Equals("Email"))
                                    {
                                        reader.Read();
                                        email_con = reader.Value.ToString();
                                        //font = new System.Drawing.Font(email.FontFamily.ToString(), 10);
                                        //System.Drawing.SizeF size = grp.MeasureString(email_con, font);
                                        //Canvas.SetLeft(email, (360 - size.Width) / 2);       
                                        //Canvas.SetLeft(email, (360 - email_con.Length * 5.8) / 2); 
                                        email.Content = email_con;

                                        // Read the XML Node's attributes and add to string
                                    }
                                    else if (reader.Name.Equals("Phone"))
                                    {

                                        reader.Read();
                                        phone_num = reader.Value.ToString();
                                        //font = new System.Drawing.Font(phone.FontFamily.ToString(), 10);
                                        //System.Drawing.SizeF size = grp.MeasureString(phone_num, font);
                                        //Canvas.SetLeft(phone, (360 - size.Width) / 2);   
                                        //Canvas.SetLeft(phone, (360 - phone_num.Length * 6) / 2); 
                                        phone.Content = phone_num;

                                        // Read the XML Node's attributes and add to string
                                    }

                                    else if (reader.Name.Equals("Gmail"))
                                    {
                                        reader.Read();
                                        //phone_num = reader.Value.ToString();
                                        gmail_user = reader.Value.ToString();

                                        // Read the XML Node's attributes and add to string
                                    }

                                    else if (reader.Name.Equals("Gpass"))
                                    {
                                        reader.Read();
                                        //phone_num = reader.Value.ToString();
                                        gmail_pass = reader.Value.ToString();

                                        // Read the XML Node's attributes and add to string
                                    }

                                    else if (reader.Name.Equals("Ymail"))
                                    {
                                        reader.Read();
                                        //phone_num = reader.Value.ToString();
                                        ymail_user = reader.Value.ToString();

                                        // Read the XML Node's attributes and add to string
                                    }

                                    else if (reader.Name.Equals("Ypass"))
                                    {
                                        reader.Read();
                                        //phone_num = reader.Value.ToString();
                                        ymail_pass = reader.Value.ToString();

                                        // Read the XML Node's attributes and add to string
                                    }

                                    else if (reader.Name.Equals("Amail"))
                                    {
                                        reader.Read();
                                        //phone_num = reader.Value.ToString();
                                        amail_user = reader.Value.ToString();

                                        // Read the XML Node's attributes and add to string
                                    }

                                    else if (reader.Name.Equals("Apass"))
                                    {
                                        reader.Read();
                                        //phone_num = reader.Value.ToString();
                                        amail_pass = reader.Value.ToString();

                                        // Read the XML Node's attributes and add to string
                                    }

                                    else if (reader.Name.Equals("Hmail"))
                                    {
                                        reader.Read();
                                        //phone_num = reader.Value.ToString();
                                        hmail_user = reader.Value.ToString();

                                        // Read the XML Node's attributes and add to string
                                    }

                                    else if (reader.Name.Equals("Hpass"))
                                    {
                                        reader.Read();
                                        //phone_num = reader.Value.ToString();
                                        hmail_pass = reader.Value.ToString();

                                        // Read the XML Node's attributes and add to string
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
        }

        public bool isLoginAvailable = false;

        public void setIsLoginState(bool isState)
        {
            isLoginAvailable  = isState;
        }


        private void userPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (isUSBFirefox && e.Key == Key.Enter && isLoginAvailable)
            {
                String main_user = userName.Text;
                String main_pass = userPass.Password;
                String subDomain = ""; 
                closeAllBubble();


                if ((userName.Text == "") || (userName.Text == "User Email") || (userPass.Password == "") || (userPass.Password == "Password"))
                {
                    //MessageBox.Show("test2");
                    var newWindow = new logFailedWin();
                    newWindow.Show();
                    if (player != null)
                    {
                        player.Stop();
                        player.Close();
                        player = null;
                    }
                    if (VirtualKeyoard != null)
                    {
                        VirtualKeyoard.Close();
                        VirtualKeyoard = null;
                    }

                    this.Close();
                }
                else
                {

                    setBrowserSetting();
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
                    try
                    {




                        String logincheckurl = "https://wemagin.com/wdrive/userlogin/checkUser.php?username=" + userName.Text +
                                    "&password=" + userPass.Password + sn + "&usbid=" + sn;
                        //String logincheckurl = "https://thesmartwave.net/blue1/webBrowser/checklogin.php?username=" + userName.Text +
                        //"&password=" + userPass.Password + sn + "&lat=null";

                        // String logincheckurl = "https://localhost/webBrowser/checklogin.php?username=" + userName.Text +
                        //"&password=" + userPass.Password + sn + "&lat=null";


                        HttpWebRequest myHttpWebRequest1 = (HttpWebRequest)WebRequest.Create(logincheckurl);
                        myHttpWebRequest1.KeepAlive = false;
                        // Assign the response object of HttpWebRequest to a HttpWebResponse variable.
                        HttpWebResponse myHttpWebResponse1 = (HttpWebResponse)myHttpWebRequest1.GetResponse();

                        Stream streamResponse = myHttpWebResponse1.GetResponseStream();
                        StreamReader streamRead = new StreamReader(streamResponse);
                        Char[] readBuff = new Char[256];
                        int count = streamRead.Read(readBuff, 0, 256);
                        int user_len = count;
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
                        String result = new string(readBuff);
                        
                        if (result.IndexOf("success") >= 0)
                        {
                            if (VirtualKeyoard != null)
                            {
                                VirtualKeyoard.Close();
                                VirtualKeyoard = null;
                            }

                            if (player != null)
                            {
                                player.Stop();
                                player.Close();
                                player = null;
                            }

                            subDomain = result.Substring(7, (user_len - 7));
                            var newWindow = new MainWindow();
                            newWindow.setUserNameAndPass(main_user, main_pass, subDomain);
                            if (newWindow.preOpenChromeWindow())
                            {
                                //newWindow.chromeProcess = this.chromeProcess;
                                newWindow.setUserInfo(first_Name, last_Name, email_con, phone_num);
                                if (login_timer != null)
                                    login_timer.Stop();
                                newWindow.Show();
                                newWindow.openLoadingPage();  
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Please check your USB state.\r\n You can download new installer from \r\n https://wemagin.com/wdrive/software/wemagin_installerWithOutUsb.exe");
                                Application.Current.Shutdown();
                            }

                        }
                        else
                        {
                            if (VirtualKeyoard != null)
                            {
                                VirtualKeyoard.Close();
                                VirtualKeyoard = null;
                            }
                            // MessageBox.Show("test3");
                            if (login_timer != null)
                                login_timer.Stop();
                            login_timer.Stop();
                            login_timer = null;

                           var newWindow = new logFailedWin();
                            newWindow.sendAlertEmail(userName.Text);
                            newWindow.Show();
                            if (player != null)
                            {
                                player.Stop();
                                player.Close();
                                player = null;
                            }
                            this.Close();
                        }
                    }
                    catch (Exception exx)
                    {
                        if (login_timer != null)
                            login_timer.Stop();
                        if (player != null)
                        {
                            player.Stop();
                            player.Close();
                            player = null;
                        }
                        if (VirtualKeyoard != null)
                        {
                            VirtualKeyoard.Close();
                            VirtualKeyoard = null;
                        }
                        showNoInternet();
                       
                        //MessageBox.Show("test1");
                        /* if (checkStateWithoutNet(userName.Text, userPass.Password))
                         {
                             //openChromeWindow();
                             if (login_timer != null)
                                 login_timer.Stop();
                             if (player != null)
                             {
                                 player.Stop();
                                 player.Close();
                                 player = null;
                             }

                             login_timer = null;

                             var newWindow = new MainWindow();
                             newWindow.setUserNameAndPass(main_user, main_pass);
                             if (newWindow.preOpenChromeWindow())
                             {
                                 //newWindow.chromeProcess = this.chromeProcess;
                                 newWindow.setUserInfo(first_Name, last_Name, email_con, phone_num);
                                 this.Close();
                                 newWindow.Show();
                             }
                         }
                         else
                         {
                             //MessageBox.Show("test3");
                             //MessageBox.Show("test1");
                             if (login_timer != null)
                                 login_timer.Stop();
                             login_timer = null;
                             if (player != null)
                             {
                                 player.Stop();
                                 player.Close();
                                 player = null;
                             }
                             var newWindow = new logFailedWin();
                             //newWindow.sendAlertEmail(userName.Text);
                             newWindow.Show();
                             this.Close();
                         }*/
                        exx.ToString();
                    }
                }

            }
            else
            {
                if (userPass.Password == "Password")
                    userPass.Password = "";
            }
        
       
        }

       
      
      
        /*
        private void openChromeWindow()
        {
            String sn = "";
            UsbUtils usbUTILS = new UsbUtils();
            String usbnamespace = usbUTILS.getUSBDriver();

            if (usbnamespace.Length > 0)
            {
                 
                string folder_name = System.IO.Path.Combine(usbnamespace, "Google");
                string file_name = System.IO.Path.Combine(folder_name, "chrome.exe");
                Process wordProcess = new Process();
                wordProcess.StartInfo.FileName = file_name;
                wordProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                wordProcess.StartInfo.UseShellExecute = true;
               // wordProcess.
                wordProcess.Start();
                //this.Close();

            }
            else
                return;
        }
        */

        private void contact_header_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           // if (e.GetPosition(contact_header).X >= 473)
             //   return;
            if (!con_min)
            {
               // contact_max_can.Visibility = System.Windows.Visibility.Visible;
                //contact_min_can.Visibility = System.Windows.Visibility.Hidden;

                //contact_pro.Visibility = System.Windows.Visibility.Hidden;
                //site_url.Visibility = System.Windows.Visibility.Hidden;
                first_name.Visibility = System.Windows.Visibility.Hidden;
                last_name.Visibility = System.Windows.Visibility.Hidden;
                email.Visibility = System.Windows.Visibility.Hidden;
                phone.Visibility = System.Windows.Visibility.Hidden;
                this.con_min = true;
                contact_bottom.Height = 0;              
                {

                   // contact_end.Margin = new Thickness(0, 188, 0, 0);
                }
            }
            else
            {
               // contact_max_can.Visibility = System.Windows.Visibility.Hidden;
               // contact_min_can.Visibility = System.Windows.Visibility.Visible;

                //contact_pro.Visibility = System.Windows.Visibility.Visible;
                //site_url.Visibility = System.Windows.Visibility.Visible;
                first_name.Visibility = System.Windows.Visibility.Visible;
                last_name.Visibility = System.Windows.Visibility.Visible;
                email.Visibility = System.Windows.Visibility.Visible;
                phone.Visibility = System.Windows.Visibility.Visible;
                this.con_min = false;
                contact_bottom.Height = 127;                
                {                  
                    contact_bottom.Margin = new Thickness(0, 188, 0, 0);
                    //contact_end.Margin = new Thickness(0, 315, 0, 0);
                }
            }
        }

      
        /*
          private void button1_Click(object sender, RoutedEventArgs e)
        {
           
            
        }
         */

        [DllImport("kernel32")]
        static extern void GetSystemInfo(ref SYSTEM_INFO pSI);


        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEM_INFO
        {
            public uint dwOemId;
            public uint dwPageSize;
            public uint lpMinimumApplicationAddress;
            public uint lpMaximumApplicationAddress;
            public uint dwActiveProcessorMask;
            public uint dwNumberOfProcessors;
            public uint dwProcessorType;
            public uint dwAllocationGranularity;
            public uint dwProcessorLevel;
            public uint dwProcessorRevision;
        }

        private void checkCamera()
        {
            //System.Management.ManagementObject info = default(System.Management.ManagementObject);
            //SYSTEM_INFO buff = new SYSTEM_INFO();
            System.Management.ManagementObjectSearcher search = default(System.Management.ManagementObjectSearcher);
            string deviceName = null;
            search = new System.Management.ManagementObjectSearcher("SELECT * From Win32_PnPEntity");
            foreach (System.Management.ManagementObject info in search.Get())
            {
	            deviceName = Convert.ToString(info.GetPropertyValue("Caption"));
                if (deviceName.IndexOf("speaker") >= 0) 
                {
                    info.SetPropertyValue("Enable", null);
                    //GetSystemInfo(ref buff);
                    //buff.
                    //.Webcam = deviceName;
	            }
            }
            
        }

        private void chromeProcess_Disposed(Object sender, EventArgs ex)
        {
            ex.ToString();
        }

        private Process chromeProcess = new Process();
        private void openChromeWindow()
        {
            //String sn = "";
            UsbUtils usbUTILS = new UsbUtils();
            String usbnamespace = usbUTILS.getUSBDriver();

            if (usbnamespace.Length > 0)
            {

                string folder_name = System.IO.Path.Combine(usbnamespace, "firefox");
                string file_name = System.IO.Path.Combine(folder_name, "firefox.exe");
                chromeProcess.StartInfo.FileName = file_name;
                chromeProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                chromeProcess.StartInfo.UseShellExecute = true;
                chromeProcess.StartInfo.Arguments = "www.google.com";
                // chromeProcess.Disposed += new EventHandler(chromeProcess_Disposed);
                chromeProcess.EnableRaisingEvents = true;
                chromeProcess.Start();                

            }
            else
                return;

        }

/*
        public String GetActiveFileNameTitle()
        {
            IntPtr hWnd = GetForegroundWindow();
            uint procId = 0;
            GetWindowThreadProcessId(hWnd, out procId);
            var proc = Process.GetProcessById((int)procId);
            if (proc != null)
            {
                return proc.MainModule.FileVersionInfo.ProductName;
            }

        }
        */

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
                     string fileCurName = System.IO.Path.Combine(usbnamespace ,Process.GetCurrentProcess().ProcessName);// System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
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




        private void downloadNewDemo()
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

        private void userName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private bool isMusicBubble = false;

        public void setMusciBubble(bool isMusicBbb)
        {
            isMusicBubble = isMusicBbb;
        }

        private musciBubble music_Bubb;
        private void music_header_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!isMusicBubble)
            {
                if ((music_Bubb != null))
                    music_Bubb.Close();
                if ((contact_Bubb != null))
                    contact_Bubb.Close();
                this.closeAllBubble();
                music_Bubb = new musciBubble();
                music_Bubb.setParentWin(this);
                music_Bubb.Topmost = true;
                //music_Bubb.setPssBubble(true);
               // music_Bubb.setXPos(this.Left + 60, e.GetPosition(contact_header).Y + this.Top);
               // music_Bubb.Show();
                return;
            }
        }


        private bool isContactBubble = false;

        public void setContactBubble(bool isContBbb)
        {
            isContactBubble = isContBbb;
        }

        private contactInfoBubble  contact_Bubb;

        private void contact_header_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!isContactBubble)
            {
                if ((music_Bubb != null))
                    music_Bubb.Close();
                if ((contact_Bubb != null))
                    contact_Bubb.Close();
                this.closeAllBubble();
                contact_Bubb = new contactInfoBubble();
                contact_Bubb.setParentWin(this);
                contact_Bubb.Topmost = true;
                double m_headerPos = e.GetPosition(this).X + this.Left;
                contact_Bubb.setXPos(m_headerPos, e.GetPosition(this).Y + this.Top);
                contact_Bubb.Show();
                return;
            }

        }

        private void saveBubbleInfo()
        {
            try
            {
                 UsbUtils usbUTILS = new UsbUtils();
                String usbnamespace = usbUTILS.getUSBDriver();

                String file_name = "";
                //String tabInfo = "";
                if (usbnamespace.Length > 0)
                {
                    string folder_name = System.IO.Path.Combine(usbnamespace, "wemagin_v2");
                    if (!Directory.Exists(folder_name))
                    {
                        Directory.CreateDirectory(folder_name);
                        DirectoryInfo dir = new DirectoryInfo(folder_name);
                        dir.Attributes |= FileAttributes.Hidden;
                    };

                    file_name = usbnamespace + "wemagin_v2\\bubble.xml";

                    FileStream fs;
                    fs = System.IO.File.Create(file_name);

                    XmlTextWriter w = new XmlTextWriter(fs, Encoding.UTF8);
                    w.WriteStartDocument();
                    w.WriteStartElement("Contact");
                    if (isUserNameBubble)
                        w.WriteElementString("userBubble", "1");
                    else
                        w.WriteElementString("userBubble", "0");

                    if (isUserPssBubble)
                        w.WriteElementString("passBubble", "1");
                    else
                        w.WriteElementString("passBubble", "0");

                    if (isMusicBubble)
                        w.WriteElementString("musicBubble", "1");
                    else
                        w.WriteElementString("musicBubble", "0");

                    if (isContactBubble)
                        w.WriteElementString("contactBubble", "1");
                    else
                        w.WriteElementString("contactBubble", "0");


                    w.WriteEndElement();

                    w.Flush();
                    fs.Close();
                }
                    
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }


        private void getBubbleInfo()
        {
            try
            {

                UsbUtils usbUTILS = new UsbUtils();
                String usbnamespace = usbUTILS.getUSBDriver();
                if (usbnamespace.Length > 0)
                {

                    String file_name = usbnamespace + "wemagin_v2\\bubble.xml";
                    if (System.IO.File.Exists(file_name))
                    {

                        XmlReader reader = XmlReader.Create(file_name);

                        while (reader.Read())
                        {
                            switch (reader.NodeType)
                            {
                                case XmlNodeType.Element:
                                    if (reader.Name.Equals("userBubble"))
                                    {
                                        reader.Read();
                                        if (reader.Value.ToString() == "1")
                                            isUserNameBubble = true;                                                                               
                                    }
                                    else if (reader.Name.Equals("passBubble"))
                                    {

                                        reader.Read();
                                        if (reader.Value.ToString() == "1")
                                            isUserPssBubble = true; 

                                        // Read the XML Node's attributes and add to string
                                    }
                                    else if (reader.Name.Equals("musicBubble"))
                                    {
                                        reader.Read();
                                        if (reader.Value.ToString() == "1")
                                            isMusicBubble = true; 

                                        
                                    }
                                    else if (reader.Name.Equals("contactBubble"))
                                    {

                                        reader.Read();
                                        if (reader.Value.ToString() == "1")
                                            isContactBubble = true; 
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
        }

        private void userPassOver_KeyDown(object sender, KeyEventArgs e)
        {
            this.userPassOver.Visibility = System.Windows.Visibility.Hidden;
            this.userPass.Visibility = System.Windows.Visibility.Visible;
            userPass.SelectAll();
        }

        
       

       

        private void userName_KeyUp(object sender, KeyEventArgs e)
        {
            if (userName.Text.IndexOf("User Email") > 0)
            {
                userName.Text = userName.Text.Substring(0, userName.Text.Length - 9);
                userName.Select(userName.Text.Length , 1);
                
            }
                //userName.Text = "";
        }

       

        
        bool isclickLogin = false;

        Point clickMovingpos;

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //if (e.GetPosition(contact_header).X >= 450)
            //    return;
            isclickLogin = true;
            clickMovingpos = e.GetPosition(this);
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isclickLogin = false;
        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            isclickLogin = false;
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isclickLogin)
                return;

            Point pos = e.GetPosition(this);
            this.Top += pos.Y - clickMovingpos.Y;
            this.Left += pos.X - clickMovingpos.X;
            closeAllBubble();
            closeUpdateWindow();
        }

        private void closeUpdateWindow()
        {
            Process[] demoByName = Process.GetProcessesByName("DemoUpgradeUseNewUI");
            if (demoByName.Length > 0)
            {
                demoByName[0].Kill();
            }

        }


        private void resetBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            closeAllBubble();
            if (login_timer != null)
                login_timer.Stop();
            if (player != null)
                player.Stop();
            resetPass resetPassWin = new resetPass();
            resetPassWin.Show();
            this.Close();

        }

        void DoDynamicAnimation()
        {
            for (int i = 0; i < 1; ++i)
            {
                /*var e = new Button { Width = 16, Height = 16 }; //new Ellipse { Width = 16, Height = 16, Fill = SystemColors.HighlightBrush };
                Canvas.SetLeft(e, Mouse.GetPosition(this).X);
                Canvas.SetTop(e, Mouse.GetPosition(this).Y);*/

                var tg = new TransformGroup();
                var translation = new TranslateTransform(0, 0);
                var translationName = "myTranslation" + translation.GetHashCode();
                RegisterName(translationName, translation);
                tg.Children.Add(translation);
                tg.Children.Add(new RotateTransform(0));
                contact_bottom.RenderTransform = tg;
                contact_end.RenderTransform = tg;
                //overCan.Children.Add(movingCan);

                var anim = new DoubleAnimation(-101, 0, new Duration(new TimeSpan(0, 0, 0, 0, 500)))
                {
                    EasingFunction = new PowerEase { EasingMode = EasingMode.EaseOut }
                };

                var s = new Storyboard();
                Storyboard.SetTargetName(s, translationName);
                Storyboard.SetTargetProperty(s, new PropertyPath(TranslateTransform.YProperty));
                var storyboardName = "s" + s.GetHashCode();
                Resources.Add(storyboardName, s);

                s.Children.Add(anim);

                s.Completed +=
                    (sndr, evtArgs) =>
                    {
                        //contact_end.Visibility = System.Windows.Visibility.Visible;
                    };
                s.Begin();
            }
        }


        void DoDynamicAnimationBack()
        {
            for (int i = 0; i < 1; ++i)
            {
                /*var e = new Button { Width = 16, Height = 16 }; //new Ellipse { Width = 16, Height = 16, Fill = SystemColors.HighlightBrush };
                Canvas.SetLeft(e, Mouse.GetPosition(this).X);
                Canvas.SetTop(e, Mouse.GetPosition(this).Y);*/

                var tg = new TransformGroup();
                var translation = new TranslateTransform(0, 0);
                var translationName = "myTranslation" + translation.GetHashCode();
                RegisterName(translationName, translation);
                tg.Children.Add(translation);
                tg.Children.Add(new RotateTransform(0));
                contact_bottom.RenderTransform = tg;
                contact_end.RenderTransform = tg;
                //overCan.Children.Add(movingCan);

                var anim = new DoubleAnimation(0, -125, new Duration(new TimeSpan(0, 0, 0, 0, 500)))
                {
                    EasingFunction = new PowerEase { EasingMode = EasingMode.EaseOut }
                };

                var s = new Storyboard();
                Storyboard.SetTargetName(s, translationName);
                Storyboard.SetTargetProperty(s, new PropertyPath(TranslateTransform.YProperty));
                var storyboardName = "s" + s.GetHashCode();
                Resources.Add(storyboardName, s);

                s.Children.Add(anim);

                s.Completed +=
                    (sndr, evtArgs) =>
                    {
                        //contact_end.Margin = new Thickness(0, 272, 0, 0);
                        contact_bottom.Height = 0; 
                        //contact_end.Margin = new Thickness(0, 272, 0, 0);
                        first_name.Visibility = System.Windows.Visibility.Hidden;
                        last_name.Visibility = System.Windows.Visibility.Hidden;
                        email.Visibility = System.Windows.Visibility.Hidden;
                        phone.Visibility = System.Windows.Visibility.Hidden;
                    };
                s.Begin();
            }
        }


        private void contactLink_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.con_min = !con_min;
            //mu_min = true;

            if (con_min)
            {
                DoDynamicAnimationBack();
            }
            else
            {
                //contact_end.Visibility = System.Windows.Visibility.Hidden;
                contact_bottom.Height = 125;
                contact_bottom.Margin = new Thickness(0, 270, 0, 0);
                contact_end.Margin = new Thickness(0, 395, 0, 0);
                first_name.Visibility = System.Windows.Visibility.Visible;
                last_name.Visibility = System.Windows.Visibility.Visible;
                email.Visibility = System.Windows.Visibility.Visible;
                phone.Visibility = System.Windows.Visibility.Visible;
                DoDynamicAnimation();
            }
        }

        private void userName_FocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (userName.Text == "User Email")
            {
                if (userName.IsFocused)
                 userName.Text = "";
            }
        }

        private void userName_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (userName.Text == "User Email")
            {
                //if (userName.IsFocused)
                   // userName.Text = "";
            }
        }

        private void userName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Right) || (e.Key == Key.Left) || (e.Key == Key.Up) || (e.Key == Key.Down))
            {
                if (userName.Text == "User Email")
                {
                    userName.Text = "";
                }
            }
        }


         
        private Keybord VirtualKeyoard;

        private void emailKey_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            userName.Text = "";


           /* if (VirtualKeyoard.IsDisposed)
            {               
                VirtualKeyoard.Close();
                VirtualKeyoard = null;  
            }*/
            /*if (VirtualKeyoard != null)
            {
                VirtualKeyoard.Close();
                VirtualKeyoard = null;
            }*/
            if (VirtualKeyoard == null)
            {
                VirtualKeyoard = new Keybord();
                VirtualKeyoard.SetLogWindow(this);
                VirtualKeyoard.SetControl = userName;
                VirtualKeyoard.Location = new System.Drawing.Point(Convert.ToInt32(Canvas.GetLeft(middle_can)), Convert.ToInt32(Canvas.GetTop(middle_can)) + 320);
                VirtualKeyoard.Show();
            }
            else
            {
                if (VirtualKeyoard.IsDisposed)
                {
                    VirtualKeyoard.Close();
                    VirtualKeyoard = null;
                    VirtualKeyoard = new Keybord();
                    VirtualKeyoard.SetLogWindow(this);
                    VirtualKeyoard.SetControl = userName;
                    VirtualKeyoard.Location = new System.Drawing.Point(Convert.ToInt32(Canvas.GetLeft(middle_can)), Convert.ToInt32(Canvas.GetTop(middle_can)) + 320);
                    VirtualKeyoard.Show();
                }
                else
                //VirtualKeyoard.SetLogWindow(this);
                   VirtualKeyoard.SetControl = userName;
            }
        }

        private void passKey_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            userPassOver.Visibility = System.Windows.Visibility.Hidden;
            userPass.Visibility = System.Windows.Visibility.Visible;
            userPass.Password = "";

           

            if (VirtualKeyoard != null)
            {
                if (VirtualKeyoard.IsDisposed)
                {
                    VirtualKeyoard.Close();
                    VirtualKeyoard = null;
                    VirtualKeyoard = new Keybord();
                    VirtualKeyoard.SetLogWindow(this);
                    VirtualKeyoard.SetPassControl = userPass;
                    VirtualKeyoard.Location = new System.Drawing.Point(Convert.ToInt32(Canvas.GetLeft(middle_can)), Convert.ToInt32(Canvas.GetTop(middle_can)) + 320);
                    VirtualKeyoard.Show();
                }
                else
                    //VirtualKeyoard.SetLogWindow(this);
                    VirtualKeyoard.SetPassControl = userPass;
            }
            else
            {
                VirtualKeyoard = new Keybord();
                VirtualKeyoard.SetLogWindow(this);
                VirtualKeyoard.SetPassControl = userPass;
                VirtualKeyoard.Location = new System.Drawing.Point(Convert.ToInt32(Canvas.GetLeft(middle_can)), Convert.ToInt32(Canvas.GetTop(middle_can)) + 320 );
                VirtualKeyoard.Show();
            }

        }

        private void userName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (VirtualKeyoard != null)
            {
                if (!VirtualKeyoard.IsDisposed)
                {
                    userName.Text = "";
                    VirtualKeyoard.SetControl = userName;
                }
                else
                {

                }
            }
        }

        private void userPass_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (VirtualKeyoard != null)
            {
                if (!VirtualKeyoard.IsDisposed)
                {
                    userPass.Password = "";
                    VirtualKeyoard.SetPassControl = userPass;
                }
                else
                {

                }
            }
        }

        private void userPassOver_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (VirtualKeyoard != null)
            {
                if (!VirtualKeyoard.IsDisposed)
                {
                    userPass.Password = "";
                    VirtualKeyoard.SetPassControl = userPass;
                }
                else
                {

                }
            }
        }

        private void userPass_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void userName_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (VirtualKeyoard != null)
            {
                if (!VirtualKeyoard.IsDisposed)
                {
                    userName.Text = "";
                    VirtualKeyoard.SetControl = userName;
                }
                else
                {

                }
            }
        }

        public void enterKeyPressFromVirtualKey()
        {
            //if ((e.Key == Key.Enter) || (e.Key == Key.Tab))
            {
                //userName.Background.Opacity = 0;
                //Color clr = Color.FromRgb(240, 78, 37);
                // userPass.Background = new SolidColorBrush(clr); 
                if (userName.Text == "")
                {
                    userName.Text = "User Email";
                }

                userPassOver.Visibility = System.Windows.Visibility.Hidden;
                userPass.Visibility = System.Windows.Visibility.Visible;
                userPass.Focus();
                userPass.SelectAll();

                if ((VirtualKeyoard != null) && (!VirtualKeyoard.IsDisposed))
                {
                    userPass.Password = "";
                    VirtualKeyoard.SetPassControl = this.userPass;

                }

            }
           
        }


        public void loginByKeyboard()
        {
            if (!isLoginAvailable)
                return;
            try
            {
                String main_user = userName.Text;
                String main_pass = userPass.Password;
                String subDomain = "";

                closeAllBubble();
                if ((userName.Text == "") || (userPass.Password == ""))
                {
                    //MessageBox.Show("test5");
                    var newWindow = new logFailedWin();
                    newWindow.Show();
                    if (player != null)
                    {
                        player.Stop();
                        player.Close();
                        player = null;
                    }
                    if (VirtualKeyoard != null)
                    {
                        VirtualKeyoard.Close();
                        VirtualKeyoard = null;
                    }
                    this.Close();
                }
                else
                {
                    setBrowserSetting();
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


                    try
                    {


                        String logincheckurl = "https://wemagin.com/wdrive/userlogin/checkUser.php?username=" + userName.Text +
                                    "&password=" + userPass.Password + sn + "&usbid=" + sn;                        

                        HttpWebRequest myHttpWebRequest1 = (HttpWebRequest)WebRequest.Create(logincheckurl);

                        myHttpWebRequest1.KeepAlive = false;
                        HttpWebResponse myHttpWebResponse1 = (HttpWebResponse)myHttpWebRequest1.GetResponse();
                        Stream streamResponse = myHttpWebResponse1.GetResponseStream();
                        StreamReader streamRead = new StreamReader(streamResponse);
                        Char[] readBuff = new Char[256];
                        int count = streamRead.Read(readBuff, 0, 256);
                        int user_len = count;
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
                        String result = new string(readBuff);
                        if (result.IndexOf("success") >= 0)
                        {

                            if (VirtualKeyoard != null)
                            {
                                VirtualKeyoard.Close();
                                VirtualKeyoard = null;
                            }

                            if (player != null)
                            {
                                player.Stop();
                                player.Close();
                                player = null;
                            }

                            subDomain = result.Substring(7, (user_len - 7));
                            var newWindow = new MainWindow();
                            newWindow.setUserNameAndPass(main_user, main_pass, subDomain);
                            if (newWindow.preOpenChromeWindow())
                            {
                                //newWindow.chromeProcess = this.chromeProcess;
                                newWindow.setUserInfo(first_Name, last_Name, email_con, phone_num);
                                if (login_timer != null)
                                    login_timer.Stop();
                                newWindow.Show();
                                newWindow.openLoadingPage();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Please check your USB state.\r\n You can download new installer from \r\n https://wemagin.com/wdrive/software/wemagin_installerWithOutUsb.exe");
                                Application.Current.Shutdown();
                            }

                        }
                        else
                        {
                            //MessageBox.Show("test5");

                            if (VirtualKeyoard != null)
                            {
                                VirtualKeyoard.Close();
                                VirtualKeyoard = null;
                            }

                            if (login_timer != null)
                                login_timer.Stop();
                            login_timer.Stop();
                            login_timer = null;

                            if (player != null)
                            {
                                player.Stop();
                                player.Close();
                                player = null;
                            }

                            //this.player.Stop();
                            var newWindow = new logFailedWin();
                            newWindow.sendAlertEmail(userName.Text);
                            newWindow.Show();
                            this.Close();
                        }
                    }
                    catch (Exception exx)
                    {                       
                        exx.ToString();
                        if (login_timer != null)
                            login_timer.Stop();
                        if (player != null)
                        {
                            player.Stop();
                            player.Close();
                            player = null;
                        }

                        if (VirtualKeyoard != null)
                        {
                            VirtualKeyoard.Close();
                            VirtualKeyoard = null;
                        }

                        showNoInternet();

                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }        
        }

    }


    

    class Employee
    {
        int _id;
        string _firstName;
        string _lastName;
        int _salary;

        public Employee(int id, string firstName, string lastName, int salary)
        {
            this._id = id;
            this._firstName = firstName;
            this._lastName = lastName;
            this._salary = salary;
        }

        public int Id { get { return _id; } }
        public string FirstName { get { return _firstName; } }
        public string LastName { get { return _lastName; } }
        public int Salary { get { return _salary; } }

        /*
           
        */
    }
   
}
