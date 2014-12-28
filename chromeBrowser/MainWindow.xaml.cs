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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using Microsoft.Win32;
using System.Data.Sql;
using System.Globalization;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Media.Effects;
using System.Net;
using System.IO;
using System.Timers;
using System.Windows.Threading;
using System.Runtime.InteropServices;
using System.Xml;
using System.Data;
using System.Windows.Media.Animation;
using chromeBrowser.bubble;
using CatenaLogic.Windows.Presentation.WebcamPlayer;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Reflection;


//SynchronizationContext.Current.Post;



namespace chromeBrowser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        private System.Timers.Timer chromeCheckTimer = new System.Timers.Timer(7000);
        private System.Timers.Timer checkMiniTimer = new System.Timers.Timer(20);
        private System.Timers.Timer moveingButton  = new System.Timers.Timer(10);
        private System.Timers.Timer moveMoonTimer = new System.Timers.Timer(200);
        private System.Timers.Timer chromeSizeTimer = new System.Timers.Timer(10000);
        private System.Timers.Timer checkFireOpen = new System.Timers.Timer(100);
        private System.Timers.Timer checkFireExit = new System.Timers.Timer(100);
        private System.Timers.Timer checkFirFoxState = new System.Timers.Timer(500);
        private CapPlayer cmPlayer;

        public bool isCam = true;
        
        public void setCameraState(bool isBlock)
        {
            isCam = isBlock;
        }

        private byte r_col = 255;
        private byte g_col = 255;
        private byte b_col = 255;

        private int color_type = 0;
        public int  getColor()
        {
            return color_type ;
        }

        public void setColorIndex(int index)
        {
            color_type = index;
        }

        public void setColor(byte r_color, byte g_color, byte b_color)
        {
            r_col = r_color;
            g_col = g_color;
            b_col = b_color;
        }

        private System.Windows.Forms.NotifyIcon notifier = new System.Windows.Forms.NotifyIcon();


        public MainWindow()
        {
            InitializeComponent();
            resetIcon();
            //preOpenChromeWindow();
            getSearchEnginFromUSB();
            this.Height = 95;
            this.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
            this.Top = 0;
            this.Left = 0;
            //checkCamState();
            //openChromeWindow();
            //checkWindowSize();
            //playMP3fromUSB();
            getBubbleInfo();            
            center_logo.MouseLeftButtonDown += new MouseButtonEventHandler(center_logo_MouseLeftButtonDown);
            checkFirFoxState.Start();
            checkFirFoxState.Elapsed +=new ElapsedEventHandler(checkFirFoxState_Elapsed);
            this.Topmost = true;
            //this.Activated += new EventHandler(MainWindow_Activated);  
            //this.Deactivated += new EventHandler(MainWindow_Deactivated);
            checkCamState();
            //checkFireShow();
            getCrl();
            //ShowInTaskbar = false;
            //this.notifier.MouseDown += new System.Windows.Forms.MouseEventHandler(notifier_MouseDown);
            //this.notifier.Icon = chromeBrowser.Properties.Resources.cloud_norm_over;
            //this.notifier.Visible = true;

        }


        void notifier_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                //ContextMenu menu = (ContextMenu)this.FindResource("NotifierContextMenu");
                //menu.IsOpen = true;
            }
        }


        private void Menu_Open(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Open");
        }

        private void Menu_Close(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Close");
        }
        


        private void getSearchEnginFromUSB()
        {
            try
            {
                UsbUtils usbUTILS = new UsbUtils();
                String usbnamespace = usbUTILS.getUSBDriver();
                if (usbnamespace.Length > 0)
                {

                    String file_name = usbnamespace + "wemagin_v2\\search.xml";
                    if (System.IO.File.Exists(file_name))
                    {

                        XmlReader reader = XmlReader.Create(file_name);

                        while (reader.Read())
                        {
                            switch (reader.NodeType)
                            {
                                case XmlNodeType.Element:
                                    if (reader.Name.Equals("searchNum"))
                                    {
                                        reader.Read();
                                        this.selected_search_num = Convert.ToInt32(reader.Value.ToString());
                                    }
                                    else if (reader.Name.Equals("defaultSite"))
                                    {
                                        reader.Read();
                                        this.defaultSite = reader.Value.ToString().Replace("WWW.", "www.");
                                        this.addText.Text = defaultSite;
                                    }
                                    break;
                            }
                        }


                    }
                    else
                    {
                        //System.IO.Directory.CreateDirectory(file_name);                       
                    }


                    file_name = usbnamespace + "secure\\openvpn.log";
                    if (System.IO.File.Exists(file_name))
                    {
                        File.Open(file_name, FileMode.Create);
                        //File.
                    }
                    checkSearchImage();


                }
            }
            catch (Exception exx)
            {
                exx.ToString();
            }

        }

        private Boolean isFireShow = false;
        private void checkFireShow()
        {

            checkFireOpen.Start();
            checkFireOpen.Elapsed += new System.Timers.ElapsedEventHandler(checkFireShowWindow);
        }



        private void checkFireShowWindow(Object sender, ElapsedEventArgs ag)
        {
            if (!isFireShow)
            {
                Process[] localByName = Process.GetProcessesByName("firefox");
                if (localByName.Length > 0)
                {
                    if (miniCapture.GetActived(localByName[0].MainWindowHandle))
                    {
                        checkFireOpen.Stop();
                        isFireShow = true;
                        checkProgressTimer.Interval = TimeSpan.FromMilliseconds(10000);
                        checkProgressTimer.Start();
                        checkProgressTimer.Tick += new EventHandler(checkProgressTimer_Elapsed);
                    }
                }


            }
        }


        firefoxLoading fire_win;


       

        
        public void openLoadingPage()
        {           

            fire_win = new firefoxLoading()
            {
                Owner = this,
                ShowInTaskbar = false,
                Topmost = false
            };
            fire_win.setParenteWin(this);
            fire_win.Show();
            fire_win.startAnimation();
        }

        

        private void checkFirFoxState_Elapsed(Object sender, EventArgs e)
        {
            Process[] localByName = Process.GetProcessesByName("firefox");
            if (localByName.Length > 0)
            {
                checkFirFoxState.Stop();
                checkFirFoxState = null;
            }
            checkFirFoxState = new System.Timers.Timer(500);
            checkFirFoxState.Start();
            checkFirFoxState.Elapsed += new ElapsedEventHandler(checkFirFoxStates_Elapsed);
        }



        /***
         * when user close firefox , call this function to close toolbar
         * 
         * ****/
        private bool isExit = false;
        private void exitAppFromFirefox()
        {
            if (isExit)
                return;
            isExit = true;
            try
            {
                Process[]  localByName = Process.GetProcessesByName("camBlock");
                for (int i = 0; i < localByName.Length; i++)
                {
                    localByName[i].Kill();
                }

                /*localByName = Process.GetProcessesByName("openvpn");
                if (localByName.Length > 0)
                {
                    for (int i = 0; i < localByName.Length; i++)
                    {
                        //localByName[i].CloseMainWindow();
                        localByName[i].Kill();
                    }
                    System.Threading.Thread.Sleep(1000);
                }
                localByName = Process.GetProcessesByName("secure");
                for (int i = 0; i < localByName.Length; i++)
                {
                    localByName[i].Kill();
                }*/

                localByName = Process.GetProcessesByName("plugin-container");
                for (int i = 0; i < localByName.Length; i++)
                {
                    localByName[i].Kill();
                }

                string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                documentsFolder += "\\Mozilla\\Firefox\\Profiles";
                DirectoryInfo dir = new DirectoryInfo(documentsFolder);
                while (true)
                {
                    if (dir.Exists)
                    {
                        //  Directory.Delete(documentsFolder, true);
                        //  break;
                        string[] subFolders = System.IO.Directory.GetDirectories(documentsFolder);
                        string proFolder = "";
                        if (subFolders.Length > 0)
                        {
                            for (int i = 0; i < subFolders.Length; i++)
                            {

                                proFolder = subFolders[i];
                                File.Delete(proFolder + "\\sessionstore.js");
                                File.Delete(proFolder + "\\search.js");
                                File.Delete(proFolder + "\\content-prefs.sqlite");
                                File.Delete(proFolder + "\\cookies.sqlite");
                                File.Delete(proFolder + "\\downloads.sqlite");
                                File.Delete(proFolder + "\\extensions.sqlite");
                                File.Delete(proFolder + "\\formhistory.sqlite");
                                File.Delete(proFolder + "\\healthreport.sqlite");
                                File.Delete(proFolder + "\\permissions.sqlite");
                                File.Delete(proFolder + "\\places.sqlite");
                                File.Delete(proFolder + "\\signons.sqlite");
                                File.Delete(proFolder + "\\webappsstore.sqlite");
                                if (File.Exists(proFolder + "\\sessionCheckpoints.json"))
                                    File.Delete(proFolder + "\\sessionCheckpoints.json");
                                if (File.Exists(proFolder + "\\sessionstore.js"))
                                    File.Delete(proFolder + "\\sessionstore.js");
                                if (File.Exists(proFolder + "\\sessionstore.bak"))
                                    File.Delete(proFolder + "\\sessionstore.bak");
                            }
                            break;
                        }

                    }


                }

                //Path.GetPathRoot(Environment.SystemDirectory);
                documentsFolder = System.IO.Path.GetPathRoot(Environment.SystemDirectory);
                documentsFolder += "SecureIt";
                dir = new DirectoryInfo(documentsFolder);
                if (dir.Exists)
                {
                    while (true)
                    {
                        try
                        {
                            Directory.Delete(documentsFolder, true);
                        }
                        catch (Exception evpn)
                        {
                            evpn.ToString();
                            continue;
                        }
                        break;
                    }
                }


               
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            CloseProgressTimer.Interval = TimeSpan.FromMilliseconds(100);
            CloseProgressTimer.Start();
            CloseProgressTimer.Tick += new EventHandler(CloseProgressTimer_Elapsed);
        }
        private void checkFirFoxStates_Elapsed(Object sender, EventArgs e)
        {
            Process[] localByName = Process.GetProcessesByName("firefox");
            if (localByName == null || localByName.Length == 0)
            {
                checkFirFoxState.Stop();
                exitAppFromFirefox();
                
            }
        }

        private DispatcherTimer checkProgressTimer = new DispatcherTimer();

        private void checkProgressTimer_Elapsed(Object sender, EventArgs e)
        {
            checkProgressTimer.Stop();
            isMini = false;
            Process[] localByName = Process.GetProcessesByName("firefox");
            if (miniCapture.GetActived(localByName[0].MainWindowHandle))
            {
                //ShowWindowAsync(localByName[0].MainWindowHandle, SW_SHOWMAXIMIZED);
                MoveWindow(localByName[0].MainWindowHandle, -5, Convert.ToInt32(55 * (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height / System.Windows.SystemParameters.WorkArea.Height)), System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width + 10, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - 55, true);
            }
            //fire_win.closeWindow();
        }

        public void checkWindowSize()
        {
            chromeSizeTimer.Start();
            chromeSizeTimer.Elapsed += new System.Timers.ElapsedEventHandler(checkFireWindow);           

        }

        private bool isCheckSize = false;
        private void checkFireWindow(Object sender, ElapsedEventArgs ag)
        {
            
            if (!isCheckSize && (this.chromeProcess != null) && !this.chromeProcess.HasExited && (chromeProcess.Handle != null))
            {
                chromeSizeTimer.Stop();
                isCheckSize = true;
                Size s = GetControlSize(chromeProcess.MainWindowHandle);
                double wid = s.Width;
                double hei = s.Height;

                double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
                double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
                if ((screenHeight == hei) && (screenWidth != wid))
                {
                    //ShowWindowAsync(chromeProcess.MainWindowHandle, SW_SHOWMAXIMIZED);
                    MoveWindow(chromeProcess.MainWindowHandle, 0, 55, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - 55, true);
                   
                }
                //if()
            }

                    
        }


        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        public static Size GetControlSize(IntPtr hWnd)
        {
            RECT pRect;
            Size cSize = new Size();
            // get coordinates relative to window
            GetWindowRect(hWnd, out pRect);
            cSize.Width = pRect.Right - pRect.Left;
            cSize.Height = pRect.Bottom - pRect.Top;
            return cSize;
        }




        #region " Disable Special Keys"

        private delegate int LowLevelKeyboardProcDelegate(int nCode, int
            wParam, ref KBDLLHOOKSTRUCT lParam);

        [DllImport("user32.dll", EntryPoint = "SetWindowsHookExA", CharSet = CharSet.Ansi)]
        private static extern int SetWindowsHookEx(
           int idHook,
           LowLevelKeyboardProcDelegate lpfn,
           int hMod,
           int dwThreadId);

        [DllImport("user32.dll")]
        private static extern int UnhookWindowsHookEx(int hHook);


        [DllImport("user32.dll", EntryPoint = "CallNextHookEx", CharSet = CharSet.Ansi)]
        private static extern int CallNextHookEx(
            int hHook, int nCode,
            int wParam, ref KBDLLHOOKSTRUCT lParam);


        const int WH_KEYBOARD_LL = 13;
        private int intLLKey;
        private KBDLLHOOKSTRUCT lParam;


        private struct KBDLLHOOKSTRUCT
        {
            public int vkCode;
            int scanCode;
            public int flags;
            int time;
            int dwExtraInfo;
        }

        private int LowLevelKeyboardProc(
            int nCode, int wParam,
            ref KBDLLHOOKSTRUCT lParam)
        {
            bool blnEat = false;
            switch (wParam)
            {
                case 256:
                case 257:
                case 260:
                case 261:
                    //Alt+Tab, Alt+Esc, Ctrl+Esc, Windows Key
                    if (((lParam.vkCode == 27) && (lParam.flags == 32) && (lParam.flags == 0)) || ((lParam.vkCode == 9) && (lParam.flags == 32)) ||
                    ((lParam.vkCode == 27) && (lParam.flags == 32)) || ((lParam.vkCode ==
                    27) && (lParam.flags == 0)) || ((lParam.vkCode == 91) && (lParam.flags
                    == 1)) || ((lParam.vkCode == 92) && (lParam.flags == 1)) || ((true) &&
                    (lParam.flags == 32)))
                    {
                        blnEat = true;
                    }
                    break;
            }

            if (blnEat)
                return 1;
            else return CallNextHookEx(0, nCode, wParam, ref lParam);

        }

        private void KeyboardHook()
        {
            intLLKey = SetWindowsHookEx(WH_KEYBOARD_LL, new LowLevelKeyboardProcDelegate(LowLevelKeyboardProc),
                System.Runtime.InteropServices.Marshal.GetHINSTANCE(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0]).ToInt32(), 0);
        }

        private void ReleaseKeyboardHook()
        {
            intLLKey = UnhookWindowsHookEx(intLLKey);
        }


        #endregion



        private bool isActiveReady = false;
        private const int SW_SHOWNORMAL = 1;
        private const int SW_SHOWMINIMIZED = 2;
        private const int SW_SHOWMAXIMIZED = 3;
        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);


        public String add_text = "www.google.com";
        public int selected_search_num = 0;


      

        private void MainWindow_Activated(Object sender , EventArgs e)
        {
           
            try
            {
                
                isCheckingFocus = true;
                if (isDeactive)
                {
                    Process[] localByName = Process.GetProcessesByName("firefox");
                    isDeactive = false;
                    SetForegroundWindow(localByName[0].MainWindowHandle);
                    SetForegroundWindow(Process.GetCurrentProcess().MainWindowHandle);
                }
                isCheckingFocus = false;
                //if (!miniCapture.GetMinimized(localByName[0].MainWindowHandle))                
                //   return;
                //ShowWindowAsync(localByName[0].MainWindowHandle, SW_SHOWNORMAL);
                //MoveWindow(localByName[0].MainWindowHandle, 0, 31, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - 31, true);
               // HideMinimizeAndMaximizeButtons(localByName[0].MainWindowHandle);
                //MoveWindow(localByName[0].MainWindowHandle, -5, Convert.ToInt32(55 * (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height / System.Windows.SystemParameters.WorkArea.Height)), System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width + 10, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - 55, true);

            }
            catch (Exception exx)
            {
                exx.ToString();
            }
        }


        bool isDeactive = false;
        bool isCheckingFocus = false;
        private void MainWindow_Deactivated(Object sender, EventArgs e)
        {
            if(!isCheckingFocus)
                isDeactive = true;

            //if (this.WindowState == WindowState.Minimized)
                //isDeactive = true;
            //Process[] localByName = Process.GetProcessesByName("firefox");
            //if ((localByName != null) && (localByName.Length > 0))
                //ShowWindowAsync(localByName[0].MainWindowHandle, SW_SHOWMINIMIZED);

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

        //[DllImport("user32.dll")]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);
        //private miniCapture miniCap = new miniCapture();

        private bool isMini = false;
        private bool isChecking = false;

        private void checkFireExit_Elapsed(Object sender, ElapsedEventArgs ag)
        {
            if (!chromeProcess.HasExited)
                return;
            checkFireExit.Stop();
            try
            {
                Process[] localByName = Process.GetProcessesByName("camBlock");
                for (int i = 0; i < localByName.Length; i++)
                {
                    localByName[i].Kill();
                }

                /*localByName = Process.GetProcessesByName("openvpn");
                for (int i = 0; i < localByName.Length; i++)
                {
                    //localByName[i].CloseMainWindow();
                    localByName[i].Kill();
                }
                System.Threading.Thread.Sleep(1000);
                localByName = Process.GetProcessesByName("secure");
                for (int i = 0; i < localByName.Length; i++)
                {
                    localByName[i].Kill();
                }
                */

            }
            catch (Exception ex)
            {
                ex.ToString();
            }


            if (searchBox != null)
                searchBox.Close();
            if (tool_bar != null)
                tool_bar.Close();
            closeAllBubble();
            this.cleanHistory();
        }

        private void checkMiniTimer_Elapsed(Object sender, ElapsedEventArgs ag)
        {
            //if (isChecking)
                //return;
            //checkMiniTimer.Stop();
            Process[] localByName = Process.GetProcessesByName("firefox");
            //chromeProcess = localByName[0];
            try
            {
                if (isMini)
                {

                    if (miniCapture.GetActived(localByName[0].MainWindowHandle))
                    {
                        checkMiniTimer.Stop();
                        isMini = false;
                        //System.Windows.Forms.MessageBox.Show("max");                        
                        SetForegroundWindow(Process.GetCurrentProcess().MainWindowHandle);
                        SendKeys.SendWait("{DOWN}");
                        isLogo = false;
                        
                        //this.moveFirefoxWindow();
                        for (int i = 1; i < 12; i++)
                        {
                            //MoveWindow(localByName[0].MainWindowHandle, 0, 30 - i * 3, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - 31 + i * 3, true);
                            MoveWindow(Process.GetCurrentProcess().MainWindowHandle, 0, -55 + i * 5, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, 95, true);
                            System.Threading.Thread.Sleep(30);
                        }
                        
                        //moveMoon(false);
                        System.Threading.Thread.Sleep(200);
                        moveMoon(false);
                        //moveMoonTimer.Start();
                        //moveMoonTimer.Elapsed += new System.Timers.ElapsedEventHandler(moveMoonTimer_Elapsed);
                        checkMiniTimer.Start();
                      
                    }
                   
                    return;
                }


                if (miniCapture.GetMinimized(localByName[0].MainWindowHandle))
                {
                    checkMiniTimer.Stop();
                    isMini = true;
                    isLogo = true;
                    //System.Windows.Forms.MessageBox.Show("mini");
                    SetForegroundWindow(Process.GetCurrentProcess().MainWindowHandle);
                    SendKeys.SendWait("{DOWN}");
                    
                    
                    //this.moveFirefoxWindow();
                    for (int i = 1; i < 12; i++)
                    {
                        //MoveWindow(localByName[0].MainWindowHandle, 0, 30 - i * 3, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - 31 + i * 3, true);
                        MoveWindow(Process.GetCurrentProcess().MainWindowHandle, 0,  - i * 5, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, 95, true);
                        System.Threading.Thread.Sleep(30);
                    } 
                   
                   
                    System.Threading.Thread.Sleep(200);
                    moveMoon(true);
                    //moveMoonTimer.Start();
                    //moveMoonTimer.Elapsed += new System.Timers.ElapsedEventHandler(moveMoonTimer_Elapsed);
                    //checkMiniTimer.Start();
                    checkMiniTimer.Start();
                }
            }
            catch (Exception ex)
            {
                checkMiniTimer.Start();
                ex.ToString();
            }
        }


        private void moveMoonTimer_Elapsed(Object sender, ElapsedEventArgs ag)
        {
            //moveMoonTimer.Stop();
           // moveMoon(true);

        }

        private void moveMoon(bool upWard)
        {

            try
            {
                
                this.Dispatcher.Invoke((Action)(() =>
                {
                    SetForegroundWindow(Process.GetCurrentProcess().MainWindowHandle);
                    SendKeys.SendWait("{DOWN}");
                    //System.Windows.Forms.MessageBox.Show("test");
                    if (upWard)
                    {
                        //System.Threading.Thread.Sleep(200);

                        Duration _duration = new Duration(TimeSpan.FromMilliseconds(700));
                        DoubleAnimation animation0 = new DoubleAnimation();
                        animation0.From = -41;
                        animation0.To = 13;
                        animation0.Duration = _duration;
                        //animation0.Completed += SlideCompleted;
                        center_logo.BeginAnimation(TopProperty, animation0);

                        //Canvas.SetTop(center_logo , 10);



                    }
                    else
                    {
                        //System.Threading.Thread.Sleep(200);

                        Duration _duration = new Duration(TimeSpan.FromMilliseconds(700));
                        DoubleAnimation animation0 = new DoubleAnimation();
                        animation0.From = 13;
                        animation0.To = -41;
                        animation0.Duration = _duration;
                        //animation0.Completed += SlideCompleted;
                        center_logo.BeginAnimation(TopProperty, animation0);

                    }
                }));
               
                ////checkMiniTimer.Start();
            }
            catch (Exception exx)
            {
               // checkMiniTimer.Start();
                exx.ToString();
            }

        }



        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }

        private void chromeMaxFunc(Object sender, ElapsedEventArgs ag)
        {
            try
            {
                if (!isMove && (this.chromeProcess != null) && !this.chromeProcess.HasExited && (chromeProcess.Handle!=null))
                {
                    

                    isMove = true;
                    //ShowWindowAsync(chromeProcess.MainWindowHandle, SW_SHOWMAXIMIZED);
                    MoveWindow(chromeProcess.MainWindowHandle, 0, 55, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - 55, true);
                    chromeCheckTimer.Stop();

                }
            }
            catch (Exception exx)
            {
                exx.ToString();
            }
        }

        public bool isVPN = false;

        private double searchboxPos = 0;
        private void resetIcon()
        {
            double win_heights = System.Windows.SystemParameters.PrimaryScreenHeight;
            double win_widths = System.Windows.SystemParameters.PrimaryScreenWidth;
            addressTextContainer.Width = (win_widths - 500) / 2;
            //google_top_line.Width  = addressTextContainer.Width;
            //google_bottom_line.Width = addressTextContainer.Width;
            if (addressTextContainer.Width < 300)
            {
                //add_border.Width = 300;
                addressTextContainer.Width = 300;                
                searchboxPos = addressTextContainer.Width + 270;
                search_canvas.Width = 250;
                this.search_text.Width = 190;
                Canvas.SetLeft(this.search_canvas, addressTextContainer.Width + 270);

                //addressBack.Width = 500;
            }
            else
            {
                searchboxPos = addressTextContainer.Width + 300;
                Canvas.SetLeft(this.search_canvas, addressTextContainer.Width + 300);
                search_canvas.Width = 300;
                this.search_text.Width = 240;
            }
            google_top_line.X2 = addressTextContainer.Width;
            google_bottom_line.X2 = addressTextContainer.Width;
            search_top_line.X2 = search_canvas.Width;
            search_bottom_line.X2 = search_canvas.Width;
            addText.Width = addressTextContainer.Width - 50;
            addressBack.Width = addressTextContainer.Width;
            //search_canvas.Width = addressTextContainer.Width;
            //search_text.Width = addressTextContainer.Width - 25;
           
            //centerLogo.Margin = new Thickness(addressTextContainer.Width * 2 +250 , 25, 0 , 0);
        }

        public Process chromeProcess = new Process() ;
        private bool isChromeOpen = false;


        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool GetWindowRect(IntPtr hWnd, ref Rectangle rect);

        private void chromeProcess_Disposed(Object sender, EventArgs ex)
        {
            ex.ToString();
        }

        private string vpn_address = "";

        public void VPN_address(string vpn_add)
        {
            vpn_address = vpn_add;
        }

        public string getVpnTXT()
        {
            return vpn_address;
        }

        private vpnWin vpn_win;

        public bool getVpnProperty()
        {
            return isVPN;
        }

        public void setVpnIcon( bool  isCon)
        {
            isVPN = isCon;
            if (isCon)
            {
                if (!isCam)
                    Canvas.SetRight(vpnblock, 197);
                else
                    Canvas.SetRight(vpnblock, 138);
                this.vpnblock.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {                
                this.vpnblock.Visibility = System.Windows.Visibility.Hidden;
            }

        }

        private string VPN_City = "";

        public void setVPN_City(string vpn_city)
        {
            VPN_City = vpn_city;
        }

        public string getVPN_City()
        {
            return VPN_City; 
        }

        public void openVpn()
        {
            if (vpn_win != null)
                vpn_win.Close();
            vpn_win = new vpnWin();
            vpn_win.setParent(this);
            vpn_win.Show();
            //vpn_win.getServerList();
            //vpn_win.setVpnSetting(isVPN);
            

           /* if (!isVPN)
            {
                try
                {                   
                    UsbUtils usbUTILS = new UsbUtils();
                    String usbnamespace = usbUTILS.getUSBDriver();
                    if (usbnamespace.Length > 0)
                    {

                        string folder_name = System.IO.Path.Combine(usbnamespace, "secure");
                        string file_name =  System.IO.Path.Combine(folder_name, "secure.exe");  
                        Process proc = new Process();
                        proc.StartInfo.WorkingDirectory = folder_name;//Program.NCBIBlastDirectory;
                        proc.StartInfo.FileName = file_name;                       
                        proc.Start();
                        if (!isCam)
                            Canvas.SetRight(vpnblock, 197);
                        else
                            Canvas.SetRight(vpnblock, 138);
                            
                        this.vpnblock.Visibility = System.Windows.Visibility.Visible;
                        isVPN = !isVPN;
                    }
                  
                }
                catch (Exception ex)
                {
                    ex.ToString();                   
                }
            }
            else
            {
                try
                {
                     

                    Process[]  localByName = Process.GetProcessesByName("openvpn");
                    for (int i = 0; i < localByName.Length; i++)
                    {
                        //localByName[i].CloseMainWindow();
                        localByName[i].Kill();
                    }
                    System.Threading.Thread.Sleep(500);

                    localByName = Process.GetProcessesByName("secure");
                    for (int i = 0; i < localByName.Length; i++)
                    {
                        localByName[i].Kill();
                    }

                    
                    vpnblock.Visibility = System.Windows.Visibility.Hidden;
                    if (!isCam)
                        Canvas.SetRight(camblock, 138);

                    isVPN = !isVPN;
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }*/
        }


        public bool  preOpenChromeWindow()
        {
            //bool isBrowser = false;
            try
            {


                //String sn = "";
                UsbUtils usbUTILS = new UsbUtils();
                String usbnamespace = usbUTILS.getUSBDriver();

                if (usbnamespace.Length > 0)
                {

                    string folder_name = System.IO.Path.Combine(usbnamespace, "firefox");
                    string file_name = System.IO.Path.Combine(folder_name, "firefox.exe");

                    //string file_name = System.IO.Path.Combine(usbnamespace, "firefox.exe");
                    chromeProcess.StartInfo.FileName = file_name;
                    chromeProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                    chromeProcess.StartInfo.UseShellExecute = true;
                    chromeProcess.StartInfo.Arguments = this.defaultSite;// "www.google.com";
                    //chromeProcess.Disposed += new EventHandler(chromeProcess_Exited);                   
                    chromeProcess.EnableRaisingEvents = true;
                    chromeProcess.Start();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
            //return false;
        }

        public void openChromeWindow()
        {
            chromeCheckTimer.Start();
            chromeCheckTimer.Elapsed += new System.Timers.ElapsedEventHandler(chromeMaxFunc);
            //checkMiniTimer.Start();
            //checkMiniTimer.Elapsed += new System.Timers.ElapsedEventHandler(checkMiniTimer_Elapsed);

        }

        private void chromeProcess_Exited(object sender, EventArgs e)
        {

            try
            {
                Process[] firefox_pro = Process.GetProcessesByName("firefox");
                if (firefox_pro.Length > 0)
                    return;



                Process[] localByName = Process.GetProcessesByName("camBlock");
                for (int i = 0; i < localByName.Length; i++)
                {
                    localByName[i].Kill();
                }

                /*localByName = Process.GetProcessesByName("openvpn");
                if (localByName.Length > 0)
                {
                    for (int i = 0; i < localByName.Length; i++)
                    {
                        //localByName[i].CloseMainWindow();
                        localByName[i].Kill();
                    }
                    System.Threading.Thread.Sleep(1000);
                }
                localByName = Process.GetProcessesByName("secure");
                for (int i = 0; i < localByName.Length; i++)
                {
                    localByName[i].Kill();
                }*/


            }
            catch (Exception ex)
            {
                ex.ToString();
            }


            if (searchBox != null)
                searchBox.Close();
            if (tool_bar != null)
                tool_bar.Close();
            //closeAllBubble();
            cleanHistory();
            //this.exitChromProcess();
            
        }

        private void chromeProcess_OutputDataReceived(object sender, EventArgs e)
        {
            e.ToString();
        }

        private string real_usr_name = "";
        private string real_usr_pass = "";
        private string real_usr_sub = "";

        public string getSubDomain()
        {
            return real_usr_sub;
        }

        public string getUserPass()
        {
            return real_usr_pass;
        }



        public void setUserNameAndPass(string usr_name, string usr_pass, string usr_sub)
        {
            real_usr_name = usr_name;
            real_usr_pass = usr_pass;
            real_usr_sub  = usr_sub;
        }

        public string getUserID()
        {
            return real_usr_name;
        }

        private bool isUSBBubble = false;

        public void setUsbBubble(bool val)
        {
            isUSBBubble = val;
        }


        private usbBubble usbBubb;

      

       

        private void closeBTN_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            try
            {
                Process[] localByName = Process.GetProcessesByName("camBlock");
                for (int i = 0; i < localByName.Length; i++)
                {
                    localByName[i].Kill();
                }

                /*localByName = Process.GetProcessesByName("openvpn");
                if (localByName.Length > 0)
                {
                    for (int i = 0; i < localByName.Length; i++)
                    {
                        //localByName[i].CloseMainWindow();
                        localByName[i].Kill();
                    }
                    System.Threading.Thread.Sleep(1000);                    
                }
                localByName = Process.GetProcessesByName("secure");
                for (int i = 0; i < localByName.Length; i++)
                {
                    localByName[i].Kill();
                }*/

                
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
           

            if (searchBox != null)
                searchBox.Close();
            if (tool_bar != null)
                tool_bar.Close();
            closeAllBubble();
            this.exitChromProcess();
            
            
        }

        public void exitChromProcess()
        {
            try
            {

                Process[] localByName = Process.GetProcessesByName("plugin-container");
                for (int i = 0; i < localByName.Length; i++)
                {
                    localByName[i].Kill();
                }

                localByName = Process.GetProcessesByName("firefox");
                for (int i = 0; i < localByName.Length; i++)
                {
                    localByName[i].Kill();
                }
                cleanHistory();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
           
        }

        private void cleanHistory()
        {
            

                try
                {

                    UsbUtils usbUTILS = new UsbUtils();
                    String usbnamespace = usbUTILS.getUSBDriver();
                    if (usbnamespace.Length > 0 && isVPN)
                    {
                        String folder_name = System.IO.Path.Combine(usbnamespace, "secure");
                        String path = System.IO.Path.Combine(folder_name, "OpenVpnLib.dll");
                        var DLL = Assembly.LoadFile(path);
                        var theType = DLL.GetType("OpenVpnLib.Connector");
                            if (theType != null)
                            {
                                var con = Activator.CreateInstance(theType);
                                var method = theType.GetMethod("Disconnect");
                                method.Invoke(con, new object[] { folder_name });
                            }

                        
                    }

                    string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    documentsFolder += "\\Mozilla";
                    DirectoryInfo dir = new DirectoryInfo(documentsFolder);
                    while (true)
                    {
                        try
                        {

                            if (dir.Exists)
                            {
                               Directory.Delete(documentsFolder, true);
                               break;
                                /*
                               string[] subFolders = System.IO.Directory.GetDirectories(documentsFolder);
                               string proFolder = "";
                               if (subFolders.Length > 0)
                               {
                                   for (int i = 0; i < subFolders.Length; i++)
                                   {

                                       proFolder = subFolders[i];
                                       File.Delete(proFolder + "\\sessionstore.js");
                                       File.Delete(proFolder + "\\search.js");
                                       File.Delete(proFolder + "\\content-prefs.sqlite");
                                       File.Delete(proFolder + "\\cookies.sqlite");
                                       File.Delete(proFolder + "\\downloads.sqlite");
                                       File.Delete(proFolder + "\\extensions.sqlite");
                                       File.Delete(proFolder + "\\formhistory.sqlite");
                                       File.Delete(proFolder + "\\healthreport.sqlite");
                                       File.Delete(proFolder + "\\permissions.sqlite");
                                       File.Delete(proFolder + "\\places.sqlite");
                                       File.Delete(proFolder + "\\signons.sqlite");
                                       File.Delete(proFolder + "\\webappsstore.sqlite");
                                       if (File.Exists(proFolder + "\\sessionCheckpoints.json"))
                                           File.Delete(proFolder + "\\sessionCheckpoints.json");
                                       if (File.Exists(proFolder + "\\sessionstore.js"))
                                           File.Delete(proFolder + "\\sessionstore.js");
                                       if (File.Exists(proFolder + "\\sessionstore.bak"))
                                           File.Delete(proFolder + "\\sessionstore.bak");
                                   }
                                   break;
                               }   */
                              
                            }
                        }
                        catch(Exception ep)
                        {
                            ep.ToString();
                            continue;
                        }

                    }
                    //Path.GetPathRoot(Environment.SystemDirectory);
                    documentsFolder = System.IO.Path.GetPathRoot(Environment.SystemDirectory);
                    documentsFolder += "SecureIt";
                    dir = new DirectoryInfo(documentsFolder);
                    if (dir.Exists)
                    {
                        while (true)
                        {
                            try
                            {
                                Directory.Delete(documentsFolder, true);
                            }
                            catch (Exception evpn)
                            {
                                evpn.ToString();
                                continue;
                            }
                            break;
                        }
                    }


                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    startInfo.FileName = "cmd.exe";
                    startInfo.Arguments = "/C ipconfig /flushdns";
                    process.StartInfo = startInfo;
                    process.Start();


                }
                catch (Exception ex)
                {
                    ex.ToString();
                }


                CloseProgressTimer.Interval = TimeSpan.FromMilliseconds(100);
                CloseProgressTimer.Start();
                CloseProgressTimer.Tick += new EventHandler(CloseProgressTimer_Elapsed);
            
        }


        private DispatcherTimer CloseProgressTimer = new DispatcherTimer();

        private void CloseProgressTimer_Elapsed(Object sender, EventArgs e)
        {
            CloseProgressTimer.Stop();
            if (tool_bar != null)
                tool_bar.Close();
            closeAllBubble();
            if (vpn_win != null)
            {
                vpn_win.closeAllThead();
                vpn_win.Close();
                vpn_win = null;
            }
            this.Close();
        }

        private void SearChBox_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {

            if (mainVirKey != null && !mainVirKey.IsDisposed)
                return;

            Color clr = Color.FromRgb(129, 129, 129);
            if (search_text.Text == "")
            {
                search_text.Text = "Clickless Search";
                
                search_text.Foreground = new SolidColorBrush(clr);
            }
            else
                search_text.Foreground = new SolidColorBrush(clr);

            Uri b_uri = new Uri("Images/toolbar/search_s.png", UriKind.Relative);
            var b_bitmap = new BitmapImage(b_uri);
            button1.Source = b_bitmap;

            Uri f_uri = new Uri("Images/toolbar/fnd_b.png", UriKind.Relative);
            var f_bitmap = new BitmapImage(f_uri);
            fnd.Source = f_bitmap;

            fnd.Width = 25;
            fnd.Height = 9;
            Canvas.SetRight(fnd, 15);
            Canvas.SetTop(fnd, 9);

            //Uri uri = new Uri("Images/toolbar/line_s.png", UriKind.Relative);
            //var bitmap = new BitmapImage(uri);
           // search_line.Source = bitmap;
            //search_line.Height = 27;
            //Canvas.SetLeft(search_line ,  27);
            fnd.Visibility = System.Windows.Visibility.Visible;
            search_div_line.Visibility = System.Windows.Visibility.Hidden ;
            search_line.Visibility = System.Windows.Visibility.Visible;

            button1.Width = 14;
            button1.Height = 14;            
            Canvas.SetLeft(button1, 6);
            Canvas.SetTop(button1, 6);
            search_canvas.Height = 27;
            search_text.Height = 25;
            //search_canvas.Height = 23;
            search_text.FontSize = 15;
            search_text.Padding = new Thickness(1,1, 0, 0);
            Canvas.SetTop(search_text, 0);
            //search_canvas.Background = new SolidColorBrush(Colors.White);
            //search_text.Background = new SolidColorBrush(Colors.White);
            Color clrs = Color.FromRgb(35, 35, 36);
            //#5d5d5d
            search_canvas.Background = new SolidColorBrush(clrs);
            search_text.Background = new SolidColorBrush(clrs);
            search_text.SelectionBrush = new SolidColorBrush(Colors.Transparent);
            search_line.Height = 27;
            Canvas.SetLeft(search_line, 27);
            search_left_line.Visibility = System.Windows.Visibility.Hidden;
            search_right_line.Visibility = System.Windows.Visibility.Hidden;
            search_top_line.Visibility = System.Windows.Visibility.Hidden;
            search_bottom_line.Visibility = System.Windows.Visibility.Hidden;

            if (mainVirKey != null && mainVirKey.IsDisposed)
            {
                mainVirKey.Close();
                mainVirKey = null;
            }

        }

        public void setSearchBubble(bool val)
        {
            isSearchBubble = val;
        }
        
        private bool isSearchBubble = false;
        private searchBubble searchBox;

        private bool isAddressText = true;


        private void search_text_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {

           /* if (!isSearchBubble)
            {
                //isFirstMouseOverOfSearchBox = true;
                closeAllBubble();
                //if ((searchBox != null))
                    //searchBox.Close();               
                searchBox = new searchBubble();               
                searchBox.setParentWin(this);
                searchBox.setXPos(searchboxPos);
                searchBox.Topmost = true;
                searchBox.Show();
                //return;

            }*/

          /************
           * 
           * Address Text
           * 
           * 
           * ************************/

            addressBack.Visibility = System.Windows.Visibility.Hidden;
            addText.Height = 25;
            addressTextContainer.Height = 27;
            search_icon.Width = 14;
            search_icon.Height = 14;
            Canvas.SetLeft(search_icon, 7);
            Canvas.SetTop(search_icon, 7);
            google_div_line.Visibility = System.Windows.Visibility.Hidden;
            google_line.Visibility = System.Windows.Visibility.Visible;            
            Canvas.SetLeft(addText, 40);
            Canvas.SetTop(addText, 0);
            addText.FontSize = 15;
            addText.Padding = new Thickness(1, 1, 0, 0);
            Canvas.SetTop(addText, 0);
            Color tclr = Color.FromRgb(129, 129, 129);
            addText.Foreground = new SolidColorBrush(tclr);
            Color clr = Color.FromRgb(35, 35, 36);
            addressTextContainer.Background = new SolidColorBrush(clr);
            addText.Background = new SolidColorBrush(clr);
            addText.SelectionBrush = new SolidColorBrush(Colors.Transparent);
            isMouseOverOfAddressText = false;

            this.checkSearchBiggerImage();
            google_left_line.Visibility = System.Windows.Visibility.Hidden;
            google_right_line.Visibility = System.Windows.Visibility.Hidden;
            google_top_line.Visibility = System.Windows.Visibility.Hidden;
            google_bottom_line.Visibility = System.Windows.Visibility.Hidden;
            if (addText.Text == "")
            {
                addText.Text = this.defaultSite;
            }


            /*****************************************/


          fnd.Width = 54;
          fnd.Height = 17;
          Canvas.SetRight(fnd, 20);
          Canvas.SetTop(fnd, 21);
          this.ForceCursor = true;
          /*Uri uri = new Uri("Images/toolbar/line_se.png", UriKind.Relative);
           var bitmap = new BitmapImage(uri);
           search_line.Source = bitmap;*/


           if (r_col != 255)
           {
               Uri b_uri = new Uri("Images/toolbar/search_w.png", UriKind.Relative);
               var b_bitmap = new BitmapImage(b_uri);
               button1.Source = b_bitmap;

               Uri f_uri = new Uri("Images/toolbar/fnd_w.png", UriKind.Relative);
               var f_bitmap = new BitmapImage(f_uri);
               fnd.Source = f_bitmap;

           }
           else
           {
               Uri b_uri = new Uri("Images/toolbar/search_s.png", UriKind.Relative);
               var b_bitmap = new BitmapImage(b_uri);
               button1.Source = b_bitmap;
           }

           if (search_text.Text == "Clickless Search")
                search_text.Text = "";
            search_text.Focus();
            button1.Width = 28;
            button1.Height = 28;
            Canvas.SetLeft(button1, 8);
            Canvas.SetTop(button1, 16);
            fnd.Visibility = System.Windows.Visibility.Hidden;
            search_div_line.Visibility = System.Windows.Visibility.Visible;
            search_line.Visibility = System.Windows.Visibility.Hidden;

            //search_line.Height = 60;
            //Canvas.SetLeft(search_line, 40);
            
            search_text.SelectionStart = 0;
            search_text.SelectionLength = search_text.Text.Length;
            search_text.SelectAll();
            search_text.Height = 60;
            search_canvas.Height = 60;
            search_text.FontSize = 30;
            Canvas.SetLeft(search_text, 60);
            search_text.Padding = new Thickness(0, 10, 0, 0);
            search_text.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
            //Canvas.SetTop(search_text, 10);

            string ccode = "#007a9d93";
            int argb = Int32.Parse(ccode.Replace("#", ""), NumberStyles.HexNumber);
            clr = Color.FromRgb(r_col, g_col, b_col);

            if (r_col == 255)
                search_text.Foreground = new SolidColorBrush(Colors.Black);
            else
                search_text.Foreground = new SolidColorBrush(Colors.White);

            search_canvas.Background = new SolidColorBrush(clr);
            search_text.SelectionBrush = new SolidColorBrush(clr);
            search_text.Background = new SolidColorBrush(clr);
            search_left_line.Visibility = System.Windows.Visibility.Visible;
            search_right_line.Visibility = System.Windows.Visibility.Visible;
            search_top_line.Visibility = System.Windows.Visibility.Visible;
            search_bottom_line.Visibility = System.Windows.Visibility.Visible;
            search_text.MouseLeftButtonDown += new MouseButtonEventHandler(this.search_icon_MouseLeftButtonDown);
            search_text.Text = "";
            if (mainVirKey == null )
            {
                isAddressText = false;
                mainVirKey = new MainVirtualKeyboard();
                mainVirKey.SetMainWindow(this);
                //addText.Text = "";
                mainVirKey.SetControl = this.search_text;
                mainVirKey.setReciever(2);
                mainVirKey.Location = new System.Drawing.Point(100, 150);
                mainVirKey.Show();
            }
            else if ((mainVirKey != null) && (mainVirKey.IsDisposed) && (isAddressText))
            {
                isAddressText = false;
                mainVirKey = new MainVirtualKeyboard();
                mainVirKey.SetMainWindow(this);
                //addText.Text = "";
                mainVirKey.SetControl = this.search_text;
                mainVirKey.setReciever(2);
                mainVirKey.Location = new System.Drawing.Point(200, 150);
                mainVirKey.Show();
            }
            else
            {
                isAddressText = false;
                mainVirKey.SetControl = this.search_text;
            }


        }


        private void search_text_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                /*if ((addText.Text.Contains("http://")) || (addText.Text.Contains("https://")))
                    webBrowser1.Navigate(addText.Text);
                else
                   webBrowser1.Navigate("http://" + addText.Text);*/
                // webBrowser1.Navigate("http://www.google.com/search?q=" + search_text.Text);

                //System.Windows.Forms.WebBrowser webbrs = (System.Windows.Forms.WebBrowser)windowsFormsHST.Child;
                //webbrs.Navigate("http://www.google.com/search?q=" + search_text.Text);
                
                //Uri uri = new Uri("Images/toolbar/line_s.png", UriKind.Relative);
                //var bitmap = new BitmapImage(uri);
                //search_line.Source = bitmap;

                //search_line.Height = 27;
                //Canvas.SetLeft(search_line, 27);
                search_div_line.Visibility = System.Windows.Visibility.Hidden;
                search_line.Visibility = System.Windows.Visibility.Visible;

                fnd.Visibility = System.Windows.Visibility.Visible;
                //search_div_line.Visibility = System.Windows.Visibility.Hidden;
                //search_line.Visibility = System.Windows.Visibility.Hidden;

                Uri b_uri = new Uri("Images/toolbar/search_s.png", UriKind.Relative);
                var b_bitmap = new BitmapImage(b_uri);
                button1.Source = b_bitmap;

                Uri f_uri = new Uri("Images/toolbar/fnd_b.png", UriKind.Relative);
                var f_bitmap = new BitmapImage(f_uri);
                fnd.Source = f_bitmap;

                
                fnd.Width = 25;
                fnd.Height = 9;
                Canvas.SetRight(fnd, 15);
                Canvas.SetTop(fnd, 9);
                button1.Width = 14;
                button1.Height = 14;
                Canvas.SetLeft(button1, 6);
                Canvas.SetTop(button1, 6);
                search_canvas.Height = 27;
                search_text.Height = 25;
                search_text.FontSize = 15;
                search_text.Padding = new Thickness(1, 1, 0, 0);
                Canvas.SetTop(search_text, 0);
                // search_canvas.Background = new SolidColorBrush(Colors.White);
                //search_text.Background = new SolidColorBrush(Colors.White);

                Color clrs = Color.FromRgb(35, 35, 36);
                //#5d5d5d
                search_canvas.Background = new SolidColorBrush(clrs);
                search_text.Background = new SolidColorBrush(clrs);
                search_text.SelectionBrush = new SolidColorBrush(Colors.Transparent);

                search_line.Height = 27;
                Canvas.SetLeft(search_line, 27);
                search_left_line.Visibility = System.Windows.Visibility.Hidden;
                search_right_line.Visibility = System.Windows.Visibility.Hidden;
                search_top_line.Visibility = System.Windows.Visibility.Hidden;
                search_bottom_line.Visibility = System.Windows.Visibility.Hidden;
                Color clr = Color.FromRgb(129, 129, 129);
                if (search_text.Text == "")
                {
                    search_text.Text = "Clickless Search";
                   
                    search_text.Foreground = new SolidColorBrush(clr);
                }
                else
                    search_text.Foreground = new SolidColorBrush(clr);

                refeshBrowser();

            }
            else
            {
                if (search_text.Text == "Clickless Search")
                    search_text.Text = "";

            }
        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
            isActiveReady = true;
            IntPtr hWnd = chromeProcess.MainWindowHandle;
            ShowWindowAsync(hWnd, SW_SHOWMINIMIZED);
          

        }

        private void cloudImg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            navWebbrowser("http://www.wcloudbackup.com/");
        }

        private void backImg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void forwardImg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void homeImg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private bool isMouseOverOfAddressText = false;


        private bool isUrlBubble = false;

        public void setUrlBubble(bool isUrlBubb)
        {
            isUrlBubble = isUrlBubb;
        }

        private urlBubble urlBubb;

        private void addText_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            /*if (!isUrlBubble)
            {
                //if ((urlBubb != null))
                    //urlBubb.Close();
                closeAllBubble();
                urlBubb = new urlBubble();
                urlBubb.setParentWin(this);
                urlBubb.Topmost = true;
                urlBubb.Show();
            }*/
        }

        private void addText_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //if ((this.searchPop != null) && (searchPop.IsActive))
                //return;
            
            Point pos = e.GetPosition(addText)   ;
            double x_pos = pos.X  ;

           
            if (x_pos < 10)
            {
                isMouseOverOfAddressText = false;
                return;
            }

            if (x_pos > 40 && x_pos > addText.Text.Length * 8)
            {
                isMouseOverOfAddressText = false;
                return;
            }

            if (isMouseOverOfAddressText)
                return;



            /**************
             * 
             * Setting SearchBOx
             * 
             * ******************/

            Color clr = Color.FromRgb(129, 129, 129);
            if (search_text.Text == "")
            {
                search_text.Text = "Clickless Search";

                search_text.Foreground = new SolidColorBrush(clr);
            }
            else
                search_text.Foreground = new SolidColorBrush(clr);

            Uri b_uri = new Uri("Images/toolbar/search_s.png", UriKind.Relative);
            var b_bitmap = new BitmapImage(b_uri);
            button1.Source = b_bitmap;

            Uri f_uri = new Uri("Images/toolbar/fnd_b.png", UriKind.Relative);
            var f_bitmap = new BitmapImage(f_uri);
            fnd.Source = f_bitmap;

            fnd.Width = 25;
            fnd.Height = 9;
            Canvas.SetRight(fnd, 15);
            Canvas.SetTop(fnd, 9);

            //Uri uri = new Uri("Images/toolbar/line_s.png", UriKind.Relative);
            //var bitmap = new BitmapImage(uri);
            // search_line.Source = bitmap;
            //search_line.Height = 27;
            //Canvas.SetLeft(search_line ,  27);
            fnd.Visibility = System.Windows.Visibility.Visible;
            search_div_line.Visibility = System.Windows.Visibility.Hidden;
            search_line.Visibility = System.Windows.Visibility.Visible;

            button1.Width = 14;
            button1.Height = 14;
            Canvas.SetLeft(button1, 6);
            Canvas.SetTop(button1, 6);
            search_canvas.Height = 27;
            search_text.Height = 25;
            //search_canvas.Height = 23;
            search_text.FontSize = 15;
            search_text.Padding = new Thickness(1, 1, 0, 0);
            Canvas.SetTop(search_text, 0);
            //search_canvas.Background = new SolidColorBrush(Colors.White);
            //search_text.Background = new SolidColorBrush(Colors.White);
            Color clrs = Color.FromRgb(35, 35, 36);
            //#5d5d5d
            search_canvas.Background = new SolidColorBrush(clrs);
            search_text.Background = new SolidColorBrush(clrs);
            search_text.SelectionBrush = new SolidColorBrush(Colors.Transparent);
            search_line.Height = 27;
            Canvas.SetLeft(search_line, 27);
            search_left_line.Visibility = System.Windows.Visibility.Hidden;
            search_right_line.Visibility = System.Windows.Visibility.Hidden;
            search_top_line.Visibility = System.Windows.Visibility.Hidden;
            search_bottom_line.Visibility = System.Windows.Visibility.Hidden;

            /********************************/





            if (r_col != 255)
            {
                /*Uri uri = new Uri("Images/toolbar/google_w.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                search_icon.Source = bitmap;*/
                this.checkSearchBiggerImage();
            }
            else
                this.checkSearchImage();

            /*Uri uris = new Uri("Images/toolbar/line_se.png", UriKind.Relative);
            var bitmaps = new BitmapImage(uris);
            google_line.Source = bitmaps;*/


            isMouseOverOfAddressText = true;
            search_icon.Width = 28;
            search_icon.Height = 28;
            Canvas.SetLeft(search_icon, 6);
            Canvas.SetTop(search_icon, 16);
            google_div_line.Visibility = System.Windows.Visibility.Visible;
            google_line.Visibility = System.Windows.Visibility.Hidden;         

            Canvas.SetLeft(addText, 50);
            addressTextContainer.Height = 60;
            addText.Height = 60;
            addText.FontSize = 30;
            addText.Padding = new Thickness(0, 5, 0, 0);
            addText.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
            string ccode = "#007a9d93";
            int argb = Int32.Parse(ccode.Replace("#", ""), NumberStyles.HexNumber);
            clr = Color.FromRgb(r_col, g_col, b_col);
            addText.Focus();
            addText.SelectionStart = 0;
            addText.SelectionLength = addText.Text.Length;
            addText.SelectAll();

            if(r_col == 255)
                addText.Foreground = new SolidColorBrush(Colors.Black);
            else
                addText.Foreground = new SolidColorBrush(Colors.White);

            addText.SelectionBrush = new SolidColorBrush(clr);
            addressTextContainer.Background = new SolidColorBrush(clr);
            addText.Background = new SolidColorBrush(clr);
            addressBack.Visibility = System.Windows.Visibility.Visible;
            google_left_line.Visibility = System.Windows.Visibility.Visible;
            google_right_line.Visibility = System.Windows.Visibility.Visible;
            google_top_line.Visibility = System.Windows.Visibility.Visible;
            google_bottom_line.Visibility = System.Windows.Visibility.Visible;

            if (mainVirKey == null)
            {
                isAddressText = true;
                mainVirKey = new MainVirtualKeyboard();
                mainVirKey.SetMainWindow(this);
                addText.Text = "";
                mainVirKey.SetControl = this.addText;
                mainVirKey.setReciever(1);
                mainVirKey.Location = new System.Drawing.Point(200, 150);
                mainVirKey.Show();
            }
            else if((mainVirKey != null) && (mainVirKey.IsDisposed) && (!isAddressText ))
            {
                isAddressText = true;
                mainVirKey = new MainVirtualKeyboard();
                mainVirKey.SetMainWindow(this);
                addText.Text = "";
                mainVirKey.SetControl = this.addText;
                mainVirKey.setReciever(1);
                mainVirKey.Location = new System.Drawing.Point(200, 150);
                mainVirKey.Show();
            }
            else
            {
                isAddressText = true;
                addText.Text = "";
                mainVirKey.SetControl = this.addText;
                //if(mainVirKey)
            }
        }



        public void UpdateAllTextBox()
        {

            if (addText.Text == "")
                addText.Text = this.defaultSite;
            addressBack.Visibility = System.Windows.Visibility.Hidden;
            addText.Height = 25;
            addressTextContainer.Height = 27;
            //search_icon.Width = 28;
            //search_icon.Height = 27;

            search_icon.Width = 14;
            search_icon.Height = 14;
            Canvas.SetLeft(search_icon, 7);
            Canvas.SetTop(search_icon, 7);

            //google_line.Height = 27;
            //Canvas.SetLeft(google_line, 27);

            google_div_line.Visibility = System.Windows.Visibility.Hidden;
            google_line.Visibility = System.Windows.Visibility.Visible;


            Canvas.SetLeft(addText, 40);
            Canvas.SetTop(addText, 0);
            addText.FontSize = 15;
            addText.Padding = new Thickness(1, 1, 0, 0);
            Canvas.SetTop(addText, 0);
            Color tclr = Color.FromRgb(129, 129, 129);

            addText.Foreground = new SolidColorBrush(tclr);
            Color clr = Color.FromRgb(35, 35, 36);
            //#5d5d5d
            addressTextContainer.Background = new SolidColorBrush(clr);
            addText.Background = new SolidColorBrush(clr);
            addText.SelectionBrush = new SolidColorBrush(Colors.Transparent);
            isMouseOverOfAddressText = false;

            this.checkSearchBiggerImage();
            /*Uri uri = new Uri("Images/toolbar/google_s.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            search_icon.Source = bitmap;*/

            google_left_line.Visibility = System.Windows.Visibility.Hidden;
            google_right_line.Visibility = System.Windows.Visibility.Hidden;
            google_top_line.Visibility = System.Windows.Visibility.Hidden;
            google_bottom_line.Visibility = System.Windows.Visibility.Hidden;



            clr = Color.FromRgb(129, 129, 129);
            if (search_text.Text == "")
            {
                search_text.Text = "Clickless Search";

                search_text.Foreground = new SolidColorBrush(clr);
            }
            else
                search_text.Foreground = new SolidColorBrush(clr);

            Uri b_uri = new Uri("Images/toolbar/search_s.png", UriKind.Relative);
            var b_bitmap = new BitmapImage(b_uri);
            button1.Source = b_bitmap;

            Uri f_uri = new Uri("Images/toolbar/fnd_b.png", UriKind.Relative);
            var f_bitmap = new BitmapImage(f_uri);
            fnd.Source = f_bitmap;

            fnd.Width = 25;
            fnd.Height = 9;
            Canvas.SetRight(fnd, 15);
            Canvas.SetTop(fnd, 9);

            //Uri uri = new Uri("Images/toolbar/line_s.png", UriKind.Relative);
            //var bitmap = new BitmapImage(uri);
            // search_line.Source = bitmap;
            //search_line.Height = 27;
            //Canvas.SetLeft(search_line ,  27);
            fnd.Visibility = System.Windows.Visibility.Visible;
            search_div_line.Visibility = System.Windows.Visibility.Hidden;
            search_line.Visibility = System.Windows.Visibility.Visible;

            button1.Width = 14;
            button1.Height = 14;
            Canvas.SetLeft(button1, 6);
            Canvas.SetTop(button1, 6);
            search_canvas.Height = 27;
            search_text.Height = 25;
            //search_canvas.Height = 23;
            search_text.FontSize = 15;
            search_text.Padding = new Thickness(1, 1, 0, 0);
            Canvas.SetTop(search_text, 0);
            //search_canvas.Background = new SolidColorBrush(Colors.White);
            //search_text.Background = new SolidColorBrush(Colors.White);
            Color clrs = Color.FromRgb(35, 35, 36);
            //#5d5d5d
            search_canvas.Background = new SolidColorBrush(clrs);
            search_text.Background = new SolidColorBrush(clrs);
            search_text.SelectionBrush = new SolidColorBrush(Colors.Transparent);
            search_line.Height = 27;
            Canvas.SetLeft(search_line, 27);
            search_left_line.Visibility = System.Windows.Visibility.Hidden;
            search_right_line.Visibility = System.Windows.Visibility.Hidden;
            search_top_line.Visibility = System.Windows.Visibility.Hidden;
            search_bottom_line.Visibility = System.Windows.Visibility.Hidden;


            //Point pos = e.GetPosition(addText);


        }

        public void OpenBrowserWithURL()
        {

            isMouseOverOfAddressText = false;

                string[] buffer = addText.Text.Split(' ');
                if (buffer.Length > 1)
                {
                    String search_txt = "";
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        if (i == 0)
                        {
                            search_txt += buffer[i];
                        }
                        else
                        {
                            search_txt += "+" + buffer[i];
                        }
                    }

                    switch (selected_search_num)
                    {
                        case 0:
                            if (search_txt == "")
                            {
                                navWebbrowser("http://www.google.com/");
                                addText.Text = "http://www.google.com/";
                            }
                            else
                            {
                                navWebbrowser("http://www.google.com/search?q=" + search_txt);
                                addText.Text = "http://www.google.com/search?q=" + search_txt;//tst+a+c
                            }
                            break;
                        case 1:
                            if (search_txt == "")
                            {
                                navWebbrowser("http://search.yahoo.com/");
                                addText.Text = "http://search.yahoo.com/";
                            }
                            else
                            {
                                navWebbrowser("http://search.yahoo.com/search?p=" + search_txt);
                                addText.Text = "http://search.yahoo.com/search?p=" + search_txt;
                            }
                            break;
                        case 2:
                            if (search_txt == "")
                            {
                                navWebbrowser("http://www.bing.com/");
                                addText.Text = "http://www.bing.com/";
                            }
                            else
                            {
                                navWebbrowser("http://www.bing.com/search?q=" + search_txt);
                                addText.Text = "http://www.bing.com/search?q=" + search_txt;
                            }
                            break;
                        case 3:
                            if (search_txt == "")
                            {
                                navWebbrowser("http://search.aol.com/");
                                addText.Text = "http://search.aol.com/";
                            }
                            else
                            {
                                navWebbrowser("http://search.aol.com/aol/search?q=" + search_txt);
                                addText.Text = "http://search.aol.com/aol/search?q=" + search_txt;
                            }
                            break;
                        case 4:
                            if (search_txt == "")
                            {
                                navWebbrowser("http://www.baidu.com/");
                                addText.Text = "http://www.baidu.com/";
                            }
                            else
                            {
                                navWebbrowser("http://www.baidu.com/s?wd=" + search_txt);
                                addText.Text = "http://www.baidu.com/s?wd=" + search_txt;
                            }
                            break;
                        case 5:
                            if (search_txt == "")
                            {
                                navWebbrowser("http://www.yandex.com/");
                                addText.Text = "http://www.yandex.com/";
                            }
                            else
                            {
                                navWebbrowser("http://www.yandex.com/yandsearch?text=" + search_txt);
                                addText.Text = "http://www.yandex.com/yandsearch?text=" + search_txt;
                            }
                            break;
                        case 6:
                            if (search_txt == "")
                            {
                                navWebbrowser("http://www.naver.com/");
                                addText.Text = "http://www.naver.com/";
                            }
                            else
                            {
                                //http://search.naver.com/search.naver?query=task
                                navWebbrowser("http://search.naver.com/search.naver?query=" + search_txt);
                                addText.Text = "http://search.naver.com/search.naver?query=" + search_txt;
                            }
                            break;
                        case 7:
                            if (search_txt == "")
                            {
                                navWebbrowser(defaultSite);
                                addText.Text = defaultSite;
                            }
                            else
                            {
                                //http://search.naver.com/search.naver?query=task
                                navWebbrowser(defaultSite + "/search?q=" + search_txt);
                                addText.Text = defaultSite + "/search?q=" + search_txt;
                            }
                            break;
                    }
                }
                else
                {

                    if (!addText.Text.Contains("."))
                    {

                        String user_enter_address = "";
                        //for (int i = 0; i < buffer.Length; i++)
                        {
                            user_enter_address = "http://www." + addText.Text + ".com";
                            chromeProcess.StartInfo.Arguments = user_enter_address;
                            chromeProcess.Start();
                        }
                    }
                    else if ((addText.Text.Contains("http://")) || (addText.Text.Contains("https://")))
                    {
                        String user_enter_address = "";
                        if (!addText.Text.Contains("www."))
                        {
                            if (addText.Text.Contains("http://"))
                                user_enter_address = "http://www." + addText.Text.Substring(7);
                            else if (addText.Text.Contains("https://"))
                                user_enter_address = "http://www." + addText.Text.Substring(8);
                            //Process.Start("firefox.exe", );
                            chromeProcess.StartInfo.Arguments = user_enter_address;
                            chromeProcess.Start();
                            ///System.Windows.Forms.WebBrowser webbrs = (System.Windows.Forms.WebBrowser)windowsFormsHST.Child;
                            //webbrs.Navigate(user_enter_address);
                        }
                        else
                        {
                            //System.Windows.Forms.WebBrowser webbrs = (System.Windows.Forms.WebBrowser)windowsFormsHST.Child;
                            //webbrs.Navigate(addText.Text);
                            chromeProcess.StartInfo.Arguments = addText.Text;
                            chromeProcess.Start();
                        }
                    }
                    //webBrowser1.Navigate(addText.Text);
                    else
                    {
                        String user_enter_address = "";
                        if (!addText.Text.Contains("www."))
                        {

                            user_enter_address = addText.Text; // "http://www." + addText.Text;
                            if (!user_enter_address.Contains("."))
                            {
                                if (!user_enter_address.Contains(".com") || !user_enter_address.Contains(".org"))
                                    user_enter_address = addText.Text + ".com";
                            }
                            user_enter_address = "http://www." + user_enter_address;
                            chromeProcess.StartInfo.Arguments = addText.Text;
                            chromeProcess.Start();
                            //System.Windows.Forms.WebBrowser webbrs = (System.Windows.Forms.WebBrowser)windowsFormsHST.Child;
                            //webbrs.Navigate(user_enter_address);
                        }
                        else
                        {
                            //System.Windows.Forms.WebBrowser webbrs = (System.Windows.Forms.WebBrowser)windowsFormsHST.Child;
                            //webbrs.Navigate("http://" + addText.Text);
                            chromeProcess.StartInfo.Arguments = "http://" + addText.Text;
                            chromeProcess.Start();
                        }
                    }
                }
            
        }

        MainVirtualKeyboard mainVirKey;

        private void addText_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {


            /*Uri uris = new Uri("Images/toolbar/line_s.png", UriKind.Relative);
            var bitmaps = new BitmapImage(uris);
            google_line.Source = bitmaps;
            */
            if (mainVirKey != null && !mainVirKey.IsDisposed)
                return;

            if (addText.Text == "")
            {
                addText.Text = this.defaultSite;
            }


            addressBack.Visibility = System.Windows.Visibility.Hidden;
            addText.Height = 25;
            addressTextContainer.Height = 27;
            //search_icon.Width = 28;
            //search_icon.Height = 27;

            search_icon.Width = 14;
            search_icon.Height = 14;
            Canvas.SetLeft(search_icon, 7);
            Canvas.SetTop(search_icon, 7);

            //google_line.Height = 27;
            //Canvas.SetLeft(google_line, 27);

            google_div_line.Visibility = System.Windows.Visibility.Hidden;
            google_line.Visibility = System.Windows.Visibility.Visible;


            Canvas.SetLeft(addText, 40);
            Canvas.SetTop(addText, 0);
            addText.FontSize = 15;
            addText.Padding = new Thickness(1, 1, 0, 0);
            Canvas.SetTop(addText, 0);
            Color tclr = Color.FromRgb(129, 129, 129);

            addText.Foreground = new SolidColorBrush(tclr);
            Color clr = Color.FromRgb(35, 35, 36);
            //#5d5d5d
            addressTextContainer.Background = new SolidColorBrush(clr);
            addText.Background = new SolidColorBrush(clr);
            addText.SelectionBrush = new SolidColorBrush(Colors.Transparent);
            isMouseOverOfAddressText = false;

            this.checkSearchBiggerImage();
            /*Uri uri = new Uri("Images/toolbar/google_s.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            search_icon.Source = bitmap;*/

            google_left_line.Visibility = System.Windows.Visibility.Hidden;
            google_right_line.Visibility = System.Windows.Visibility.Hidden;
            google_top_line.Visibility = System.Windows.Visibility.Hidden;
            google_bottom_line.Visibility = System.Windows.Visibility.Hidden;
            Point pos = e.GetPosition(addText);
            if(pos.X < 0 || pos.X > 270)
               closeAllBubble();

            if (mainVirKey != null && mainVirKey.IsDisposed)
            {
                mainVirKey.Close();
                mainVirKey = null;
            }
        }

        private void addText_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                /*Uri uris = new Uri("Images/toolbar/line_s.png", UriKind.Relative);
                var bitmaps = new BitmapImage(uris);
                google_line.Source = bitmaps;*/

                google_div_line.Visibility = System.Windows.Visibility.Hidden;
                google_line.Visibility = System.Windows.Visibility.Visible;

                this.checkSearchBiggerImage();

                /*Uri uri = new Uri("Images/toolbar/google_w.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                search_icon.Source = bitmap;
                */
                addressBack.Visibility = System.Windows.Visibility.Hidden;
                addText.Height = 25;
                addressTextContainer.Height = 27;
                addText.FontSize = 15;
                Color tclr = Color.FromRgb(129, 129, 129);
                addText.Foreground = new SolidColorBrush(tclr);
                addText.Padding = new Thickness(1, 1, 0, 0);
                Canvas.SetTop(addText, 0);                
                search_icon.Width = 14;
                search_icon.Height = 14;
                Canvas.SetLeft(search_icon, 7);
                Canvas.SetTop(search_icon, 7);

                google_line.Height = 27;
                Canvas.SetLeft(google_line, 27);
                Canvas.SetLeft(addText, 40);
                Color clr = Color.FromRgb(35, 35, 36);
                //#5d5d5d
                addressTextContainer.Background = new SolidColorBrush(clr);
                addText.Background = new SolidColorBrush(clr);
                addText.SelectionBrush = new SolidColorBrush(Colors.Transparent);

                google_left_line.Visibility = System.Windows.Visibility.Hidden;
                google_right_line.Visibility = System.Windows.Visibility.Hidden;
                google_top_line.Visibility = System.Windows.Visibility.Hidden;
                google_bottom_line.Visibility = System.Windows.Visibility.Hidden;
                isMouseOverOfAddressText = false;

                string[] buffer = addText.Text.Split(' ');
                if (buffer.Length > 1)
                {
                    String search_txt = "";
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        if (i == 0)
                        {
                            search_txt += buffer[i];
                        }
                        else
                        {
                            search_txt += "+" + buffer[i];
                        }
                    }

                    switch (selected_search_num)
                    {
                        case 0:
                            if (search_txt == "")
                            {
                                navWebbrowser("http://www.google.com/");
                                addText.Text = "http://www.google.com/";
                            }
                            else
                            {
                                navWebbrowser("http://www.google.com/search?q=" + search_txt);
                                addText.Text = "http://www.google.com/search?q=" + search_txt;//tst+a+c
                            }
                            break;
                        case 1:
                            if (search_txt == "")
                            {
                                navWebbrowser("http://search.yahoo.com/");
                                addText.Text = "http://search.yahoo.com/";
                            }
                            else
                            {
                                navWebbrowser("http://search.yahoo.com/search?p=" + search_txt);
                                addText.Text = "http://search.yahoo.com/search?p=" + search_txt;
                            }
                            break;
                        case 2:
                            if (search_txt == "")
                            {
                                navWebbrowser("http://www.bing.com/");
                                addText.Text = "http://www.bing.com/";
                            }
                            else
                            {
                                navWebbrowser("http://www.bing.com/search?q=" + search_txt);
                                addText.Text = "http://www.bing.com/search?q=" + search_txt;
                            }
                            break;
                        case 3:
                            if (search_txt == "")
                            {
                                navWebbrowser("http://search.aol.com/");
                                addText.Text = "http://search.aol.com/";
                            }
                            else
                            {
                                navWebbrowser("http://search.aol.com/aol/search?q=" + search_txt);
                                addText.Text = "http://search.aol.com/aol/search?q=" + search_txt;
                            }
                            break;
                        case 4:
                            if (search_txt == "")
                            {
                                navWebbrowser("http://www.baidu.com/");
                                addText.Text = "http://www.baidu.com/";
                            }
                            else
                            {
                                navWebbrowser("http://www.baidu.com/s?wd=" + search_txt);
                                addText.Text = "http://www.baidu.com/s?wd=" + search_txt;
                            }
                            break;
                        case 5:
                            if (search_txt == "")
                            {
                                navWebbrowser("http://www.yandex.com/");
                                addText.Text = "http://www.yandex.com/";
                            }
                            else
                            {
                                navWebbrowser("http://www.yandex.com/yandsearch?text=" + search_txt);
                                addText.Text = "http://www.yandex.com/yandsearch?text=" + search_txt;
                            }
                            break;
                        case 6:
                            if (search_txt == "")
                            {
                                navWebbrowser("http://www.naver.com/");
                                addText.Text = "http://www.naver.com/";
                            }
                            else
                            {
                                //http://search.naver.com/search.naver?query=task
                                navWebbrowser("http://search.naver.com/search.naver?query=" + search_txt);
                                addText.Text = "http://search.naver.com/search.naver?query=" + search_txt;
                            }
                            break;
                        case 7:
                            if (search_txt == "")
                            {
                                navWebbrowser(defaultSite);
                                addText.Text = defaultSite;
                            }
                            else
                            {
                                //http://search.naver.com/search.naver?query=task
                                navWebbrowser(defaultSite + "/search?q=" + search_txt);
                                addText.Text = defaultSite + "/search?q=" + search_txt;
                            }
                            break;
                    }
                }
                else
                {

                    if (!addText.Text.Contains("."))
                    {

                        String user_enter_address = "";
                        //for (int i = 0; i < buffer.Length; i++)
                        {
                            user_enter_address = "http://www." + addText.Text + ".com";
                            chromeProcess.StartInfo.Arguments = user_enter_address;
                            chromeProcess.Start();
                        }
                    }
                    else if ((addText.Text.Contains("http://")) || (addText.Text.Contains("https://")))
                    {
                        String user_enter_address = "";
                        if (!addText.Text.Contains("www."))
                        {
                            if (addText.Text.Contains("http://"))
                                user_enter_address = "http://www." + addText.Text.Substring(7);
                            else if (addText.Text.Contains("https://"))
                                user_enter_address = "http://www." + addText.Text.Substring(8);
                            //Process.Start("firefox.exe", );
                            chromeProcess.StartInfo.Arguments = user_enter_address;
                            chromeProcess.Start();
                            ///System.Windows.Forms.WebBrowser webbrs = (System.Windows.Forms.WebBrowser)windowsFormsHST.Child;
                            //webbrs.Navigate(user_enter_address);
                        }
                        else
                        {
                            //System.Windows.Forms.WebBrowser webbrs = (System.Windows.Forms.WebBrowser)windowsFormsHST.Child;
                            //webbrs.Navigate(addText.Text);
                            chromeProcess.StartInfo.Arguments = addText.Text;
                            chromeProcess.Start();
                        }
                    }
                    //webBrowser1.Navigate(addText.Text);
                    else
                    {
                        String user_enter_address = "";
                        if (!addText.Text.Contains("www."))
                        {

                            user_enter_address = addText.Text; // "http://www." + addText.Text;
                            if (!user_enter_address.Contains("."))
                            {
                                if (!user_enter_address.Contains(".com") || !user_enter_address.Contains(".org"))
                                    user_enter_address = addText.Text + ".com";
                            }
                            user_enter_address = "http://www." + user_enter_address;
                            chromeProcess.StartInfo.Arguments = addText.Text;
                            chromeProcess.Start();
                            //System.Windows.Forms.WebBrowser webbrs = (System.Windows.Forms.WebBrowser)windowsFormsHST.Child;
                            //webbrs.Navigate(user_enter_address);
                        }
                        else
                        {
                            //System.Windows.Forms.WebBrowser webbrs = (System.Windows.Forms.WebBrowser)windowsFormsHST.Child;
                            //webbrs.Navigate("http://" + addText.Text);
                            chromeProcess.StartInfo.Arguments = "http://" + addText.Text;
                            chromeProcess.Start();
                        }
                    }
                }
            }
        }

        /**
         * Media_player
         * 
         * **/

        /****
        * 
        * MP3 Player
        * 
        * ********************/

        public int selected_num = -1;
        public string[] mp3_array = new string[0];
        private bool isPlaying = false;
        private bool isOpen = false;
        private MediaPlayer player;// = new MediaPlayer();
        private double volum_val = 0.1;
        private bool is_volum = false;
        private bool is_play_check = false;
        private bool isMp_canvas_Selected = false;

       



        private string[] getFilesWithDate(string folder)
        {
            string[] arr;//= new string[10];
            arr = Directory.GetFiles(folder);
            DateTime[] creationTimes = new DateTime[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                creationTimes[i] = File.GetLastWriteTime(arr[i]);//new FileInfo(arr[i]).CreationTime;
            Array.Sort(creationTimes, arr);
            //DateTime time = File.GetLastWriteTime(arr[0]);
            return arr;
        }
     

        private void lock_button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {       

            this.Opacity = 0.3;
            this.Effect = new BlurEffect();
            LockWindow lockPOP = new LockWindow()
            {
                Owner = this,
                ShowInTaskbar = false,
                Topmost = true
            };
            
            lockPOP.setUserNameAndPass(real_usr_name, real_usr_pass); 
            lockPOP.setMain(this);
            lockPOP.ShowDialog();

        }

        private bool isMove = false;


        [DllImport("user32.dll")]
        private static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        private void Grid_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
           
            SetForegroundWindow(Process.GetCurrentProcess().MainWindowHandle);
            SendKeys.SendWait("{RIGHT}");
            //this.WindowState = System.Windows.WindowState.Minimized;

        }

        private void chrome_button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (chromeProcess.HasExited)
            {
                chromeProcess.Start();
                
            }
            else
            {
                chromeProcess.Kill();
            }
        }

        private bool isLogo = false;
        private bool ischrome = false;
        private void center_logo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //center_logo.Opacity = 0.01;
                //center_logo.MouseLeftButtonUp += new MouseButtonEventHandler(center_logo_MouseLeftButtonUp);
                //if (!isLogo)
                {
                   // ischrome = true;
                }
               //else
                {
                    isMini = false;
                    this.moveFirefoxWindow();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

           
            
        }

        private int movingStep = 0;

        private void moveWbutton(Object sender, ElapsedEventArgs ex)
        {
            movingStep++;
            if (movingStep > 29)
                moveingButton.Stop();
            Canvas.SetTop(center_logo, -28 + movingStep);
        }

        private bool isSliderComplete = false;

        private void SlideCompleted(Object sender, EventArgs e)
        {
            //isSliderComplete = true;
            Point centerPos = GetCursorPosition();           
            double xPos = this.Width - 155;

            //System.Windows.Forms.MessageBox.Show(centerPos.X.ToString() + "::" + xPos.ToString());
            if ((xPos < centerPos.X) && ((xPos + 25) > centerPos.X) && (2 < centerPos.Y) && ( 30 > centerPos.Y))
            {
                Uri uri = new Uri("Images/chrome/chrome-over.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                center_logo.Source = bitmap;
                //center_logo.MouseLeftButtonDown += new MouseButtonEventHandler(center_logo_MouseLeftButtonDown);
            }

            SetForegroundWindow(Process.GetCurrentProcess().MainWindowHandle);
            SendKeys.SendWait("{DOWN}");
            SetCursorPos(Convert.ToInt32(centerPos.X -1), Convert.ToInt32(centerPos.Y));
        }

      


        private void SlideDownCompleted(Object sender, EventArgs e)
        {
            //isSliderComplete = true;
            Point centerPos = GetCursorPosition();
            double xPos = this.Width - 140;
            
            SetForegroundWindow(Process.GetCurrentProcess().MainWindowHandle);
            SendKeys.SendWait("{DOWN}");
            
            //System.Windows.Forms.MessageBox.Show(centerPos.X.ToString() + "::" + xPos.ToString());
            if ((xPos < centerPos.X) && ((xPos + 40) > centerPos.X) && (10 < centerPos.Y) && (41 > centerPos.Y))
            {
                Uri uri = new Uri("Images/chrome/chrome-over.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                center_logo.Source = bitmap;
            }

            SetForegroundWindow(Process.GetCurrentProcess().MainWindowHandle);
            SendKeys.SendWait("{DOWN}");
            SetCursorPos(Convert.ToInt32(centerPos.X + 1), Convert.ToInt32(centerPos.Y));

        }


      

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        private static extern int SetWindowLong32(IntPtr hWnd, int nIndex, int dwNewLong);


        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int X, int Y);


        [DllImport("user32.dll")]
        internal extern static int SetWindowLong(IntPtr hwnd, int index, int value);

        [DllImport("user32.dll")]
        internal extern static IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll")]
        internal extern static int GetWindowLong(IntPtr hwnd, int index);

        //[DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        //static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
        internal static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        internal extern static int SetClassLong(System.IntPtr hWnd, int index, int value);

        [DllImport("user32.dll")]
        internal extern static uint GetClassLong(IntPtr hWnd, int nIndex);

        

        [DllImport("user32.dll")]
        static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

        [DllImport("user32.dll")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll")]
        static extern IntPtr RemoveMenu(IntPtr hMenu, uint nPosition, uint wFlags);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);
    

        internal const UInt32 SC_CLOSE = 0xF060;
        internal const UInt32 MF_ENABLED = 0x00000000;
        internal const UInt32 MF_GRAYED = 0x00000001;
        internal const UInt32 MF_DISABLED = 0x00000002;
        internal const uint MF_BYCOMMAND = 0x00000000;


        internal static void HideMinimizeAndMaximizeButtons(IntPtr hwnd)
        {            
            try
            {
                const int WS_BORDER = 8388608;
                const int WS_DLGFRAME = 4194304;
                const int WS_CAPTION = WS_BORDER | WS_DLGFRAME;
                const int WS_SYSMENU = 524288;
                const int WS_THICKFRAME = 262144;
                const int WS_MINIMIZE = 536870912;
                const int WS_MAXIMIZEBOX = 65536;
                const int GWL_STYLE = -16;
                const int GWL_EXSTYLE = -20;
                const int WS_EX_DLGMODALFRAME = 0x1;
                const int SWP_NOMOVE = 0x2;
                const int SWP_NOSIZE = 0x1;
                const int SWP_FRAMECHANGED = 0x20;
                const uint MF_BYPOSITION = 0x400;
                const uint MF_REMOVE = 0x1000;

                /*
                int Style = 0;
                Style = GetWindowLong(hwnd, GWL_STYLE);
                Style = Style & ~WS_CAPTION;
                Style = Style & ~WS_SYSMENU;
                Style = Style & ~WS_THICKFRAME;
                Style = Style & ~WS_MINIMIZE;
                Style = Style & ~WS_MAXIMIZEBOX;
                SetWindowLong(hwnd, GWL_STYLE, Style);
                Style = GetWindowLong(hwnd, GWL_EXSTYLE);
                SetWindowLong(hwnd, GWL_EXSTYLE, Style | WS_EX_DLGMODALFRAME);
                SetWindowPos(hwnd, new IntPtr(0), 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_FRAMECHANGED);*/

                
                int Style = 0;
                if (IntPtr.Size == 8)
                {
                    IntPtr style = GetWindowLongPtr(hwnd, GWL_STYLE);
                    Style = style.ToInt32();
                    //System.Windows.MessageBox.Show(Style.ToString());
                }
                else
                    Style = GetWindowLong(hwnd, GWL_STYLE);
                //System.Windows.MessageBox.Show(Style.ToString());
                Style = Style & ~WS_CAPTION;
                Style = Style & ~WS_SYSMENU;
                Style = Style & ~WS_THICKFRAME;
                Style = Style & ~WS_MINIMIZE;
                Style = Style & ~WS_MAXIMIZEBOX;
                IntPtr xAsIntPtr = new IntPtr(Style);
                if (IntPtr.Size == 8)
                    SetWindowLongPtr(hwnd, GWL_STYLE, xAsIntPtr);
                else
                    SetWindowLong(hwnd, GWL_STYLE, Style);
                if (IntPtr.Size == 8)
                {
                    IntPtr style = GetWindowLongPtr(hwnd, GWL_STYLE);
                    Style = style.ToInt32();
                    //System.Windows.MessageBox.Show("2 : " + Style.ToString());
                }
                else
                    Style = GetWindowLong(hwnd, GWL_STYLE);

                xAsIntPtr = new IntPtr(Style | WS_EX_DLGMODALFRAME);
                if (IntPtr.Size == 8)
                    SetWindowLongPtr(hwnd, GWL_STYLE, xAsIntPtr);
                else
                    SetWindowLong(hwnd, GWL_STYLE, Style);
                //SetWindowLong(hwnd, GWL_EXSTYLE, Style | WS_EX_DLGMODALFRAME);
                SetWindowPos(hwnd, new IntPtr(0), 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_FRAMECHANGED);


                //IntPtr style = GetWindowLongPtr(hwnd, GWL_STYLE);
                //int buff_style = (int) style;
                //IntPtr xAsIntPtr = new IntPtr(buff_style & ~WS_CAPTION);
                //SetWindowLongPtr(hwnd, GWL_STYLE, xAsIntPtr); 
                //long value = GetWindowLong(hwnd, GWL_STYLE);

                //SetWindowLong(hwnd, GWL_STYLE, (int)(value & ~WS_MINIMIZEBOX & ~WS_MAXIMIZEBOX));            
                //SetClassLong(hwnd, GCL_STYLE,  (int)GetClassLong(hwnd, GCL_STYLE | CS_NOCLOSE) );

                //int style = GetWindowLong(hwnd, GWL_STYLE);
                //SetWindowLong(hwnd, GWL_STYLE, ( style & ~WS_CAPTION )); 


                //int style = GetWindowLong(hwnd, GWL_STYLE);
                //SetWindowLong(hwnd, GWL_STYLE, ( style & ~WS_CAPTION )); 


                //IntPtr hSystemMenu = GetSystemMenu(hwnd, false);
                //EnableMenuItem(hSystemMenu, SC_CLOSE, (uint)(MF_ENABLED));
                //RemoveMenu(hSystemMenu, SC_CLOSE, MF_GRAYED);
            }
            catch (Exception hide_ex)
            {
                System.Windows.MessageBox.Show(hide_ex.ToString());
            }

        }

        public void setFireSate()
        {

            Process[] localByName = Process.GetProcessesByName("firefox");
            String fire_act_state = localByName[0].StartInfo.WindowStyle.ToString();
            //ShowWindowAsync(localByName[0].MainWindowHandle, SW_SHOWMAXIMIZED);
            MoveWindow(localByName[0].MainWindowHandle, -5, Convert.ToInt32(55 * (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height / System.Windows.SystemParameters.WorkArea.Height)), System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width+10, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - 55, true);
            setVisibleTxt(true);
            SetForegroundWindow(Process.GetCurrentProcess().MainWindowHandle);
            MoveWindow(Process.GetCurrentProcess().MainWindowHandle, 0, 0, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, 95 * Convert.ToInt32((System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height / System.Windows.SystemParameters.WorkArea.Height)), true);
            //checkFireExit.Start();
            //checkFireExit.Elapsed += new ElapsedEventHandler(checkFireExit_Elapsed);
            //chromeProcess.Exited += new EventHandler(chromeProcess_Exited);
            HideMinimizeAndMaximizeButtons(localByName[0].MainWindowHandle);
            this.Topmost = false;
            //chromeProcess.Disposed += new EventHandler(chromeProcess_Exited);
            //System.Windows.MessageBox.Show(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Top.ToString());
        }


        private void moveFirefoxWindow()
        {
            try
            {
                Process[] localByName = Process.GetProcessesByName("firefox");              //this.chromeProcess = localByName[0];                
                //ShowWindowAsync(localByName[0].MainWindowHandle, SW_SHOWMAXIMIZED);
                if (!isLogo)
                {
                    isLogo = true;
                    center_logo.Visibility = System.Windows.Visibility.Hidden;
                    MoveWindow(localByName[0].MainWindowHandle, -5, Convert.ToInt32(55 * (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height / System.Windows.SystemParameters.WorkArea.Height)), System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width + 10, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - 55, true);
           
                    for (int i = 1; i < 12; i++)
                    {
                        //MoveWindow(localByName[0].MainWindowHandle, 0, 30 - i * 3, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - 31 + i * 3, true);
                        MoveWindow(Process.GetCurrentProcess().MainWindowHandle, 0, -i * 5 * Convert.ToInt32(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height / System.Windows.SystemParameters.WorkArea.Height), System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, 95 * Convert.ToInt32((System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height / System.Windows.SystemParameters.WorkArea.Height)), true);
                        System.Threading.Thread.Sleep(30);
                    }
                   
                    center_logo.Visibility = System.Windows.Visibility.Visible;
                    System.Threading.Thread.Sleep(300);

                    Duration _duration = new Duration(TimeSpan.FromMilliseconds(500));
                    DoubleAnimation animation0 = new DoubleAnimation();
                    //animation0. = System.Type.GetType("Top");

                    animation0.From = -41;
                    animation0.To = 13;
                    animation0.Duration = _duration;
                    animation0.Completed += SlideCompleted;
                    center_logo.BeginAnimation(TopProperty, animation0);

                }
                else
                {
                    isLogo = false;
                    buttonOver.Visibility = System.Windows.Visibility.Visible;
                    SetForegroundWindow(Process.GetCurrentProcess().MainWindowHandle);
                    System.Threading.Thread.Sleep(100);
                    Duration _duration1 = new Duration(TimeSpan.FromMilliseconds(300));
                    DoubleAnimation animation1 = new DoubleAnimation();
                    animation1.From = -55;
                    animation1.To = 0;
                    animation1.Duration = _duration1;
                    animation1.Completed += SlideThisCompleted;
                    this.BeginAnimation(TopProperty, animation1);
                }
            }
            catch (Exception exx)
            {
                exx.ToString();
            }

        }



        private void SlideThisCompleted(Object sender, EventArgs e)
        {

            buttonOver.Visibility = System.Windows.Visibility.Hidden;

            Process[] localByName = Process.GetProcessesByName("firefox");
            //MoveWindow(localByName[0].MainWindowHandle, 0, 31, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - 31, true);
            MoveWindow(localByName[0].MainWindowHandle, -5, Convert.ToInt32(55 * (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height / System.Windows.SystemParameters.WorkArea.Height)), System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width + 10, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - 55, true);
           
            center_logo.Visibility = System.Windows.Visibility.Visible;
            center_logo.Opacity = 1;
            System.Threading.Thread.Sleep(200);
            Duration _duration = new Duration(TimeSpan.FromMilliseconds(400));
            DoubleAnimation animation0 = new DoubleAnimation();
            //animation0. = System.Type.GetType("Top");

            animation0.From = 13;
            animation0.To = -41;
            animation0.Duration = _duration;
            animation0.Completed += SlideDownCompleted;
            center_logo.BeginAnimation(TopProperty, animation0);
        }

        private void moveWindow()
        {
            try
            {
                if (!isLogo)
                {
                    //this.Effect = new Slider();
                    isLogo = true;
                    /*chromeProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                    chromeProcess.Refresh();
                    wrapper.Visibility = System.Windows.Visibility.Hidden;
                    MoveWindow(chromeProcess.MainWindowHandle, 0, 0, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height, true);
                   */
                    Canvas.SetTop(center_logo, 0);
                    System.Threading.Thread.Sleep(10);
                    for (int i = 1; i < 12; i++)
                    {
                        //Canvas.SetTop(wrapper, -i);
                        //wrapper.Margin = new Thickness(0, -i, 0, 25+i);
                        //SetForegroundWindow(Process.GetCurrentProcess().MainWindowHandle);
                        
                        MoveWindow(Process.GetCurrentProcess().MainWindowHandle, 0, -i*5, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, 95, true);
                        MoveWindow(chromeProcess.MainWindowHandle, 0, 55- i*5, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - 55 + i*5, true);
                        System.Threading.Thread.Sleep(1);
                    }
                    
                    //center_logo.Margin = new Thickness(0, 31, 0, -31);
                }
                else
                {
                    isLogo = false;
                    /*wrapper.Visibility = System.Windows.Visibility.Visible;
                    chromeProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal;                    
                    chromeProcess.Refresh();
                    MoveWindow(chromeProcess.MainWindowHandle, 0, 31, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - 31, true);
                    */
                    Canvas.SetTop(center_logo, -41);
                    System.Threading.Thread.Sleep(10);
                    for (int i = 1; i < 12; i++)
                    {                        
                        MoveWindow(Process.GetCurrentProcess().MainWindowHandle, 0, i*5-55, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, 95, true);
                        MoveWindow(chromeProcess.MainWindowHandle, 0, i*5, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - i*5, true);
                        System.Threading.Thread.Sleep(1);
                    }
                }
            }
            catch (Exception exx)
            {
                exx.ToString();
            }

        }

        public void setVisibleTxt(bool state)
        {
            string url = "";
            if (!state)
                url = "Images/toolbar/visible_text.png";
            else
                url = "Images/toolbar/invisible_text.png";
            Uri uri = new Uri(url, UriKind.Relative);
            var bitmap = new BitmapImage(uri);          

            visibleTxt.Source = bitmap;

        }

        private void setting_button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            //this.Effect = new BlurEffect();
            /*toolbar tool_bar = new toolbar()
            {
                Owner = this,
                ShowInTaskbar = false,
                Topmost = true
            };
            tool_bar.setMain(this);
            tool_bar.setInfo(this.real_usr_name , this.real_usr_pass);
            tool_bar.ShowDialog();
             * */


            /*this.Effect = new BlurEffect();
            toolbar tool_bar = new toolbar()
            {
                Owner = this,
                ShowInTaskbar = false,
                Topmost = true
            };

            //settingPOP.getContactInfo();
            //settingPOP.setUserInfo(real_usr_name, first_Name, last_Name, email_con, phone_num);//, gmail_user);
            tool_bar.setMain(this);
            tool_bar.ShowDialog();*/


            /*
            settingLock settingLck = new settingLock()
            {
                Owner = this,
                ShowInTaskbar = false,
                Topmost = true
            };
            settingLck.setMain(this);
            settingLck.setUserNameAndPass(this.real_usr_name, this.real_usr_pass);
            settingLck.ShowDialog();
            */
        }


        public string first_Name = "";
        public string last_Name = "";
        public string email_con = "";
        public string phone_num = "";

        public void setUserInfo(string fName, string lName, string email_info, string phone_info)/*,
            string gMail, string gPass, string aMail, string aPass, string yMail, string yPass, string hMail, string hPass)*/
        {
            first_Name = fName;
            last_Name = lName;
            email_con = email_info;
            phone_num = phone_info;           
        }


        public void navWebbrowser(String url)
        {
            //Process.Start("firefox", url);
            //this.chromeProcess.Start();
            try
            {
                chromeProcess.StartInfo.Arguments = url;
                chromeProcess.Start();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }


        public string defaultSite = "www.google.com";
        public void refeshBrowser()
        {
            //System.Windows.Forms.WebBrowser webbrs = (System.Windows.Forms.WebBrowser)windowsFormsHST.Child;
            string search_txt = "";
            if (search_text.Text == "Clickless Search")
                search_txt = "";
            else
                search_txt = search_text.Text;

            
            String[] queryString = search_txt.Split(' ');
            search_txt = "";
            for (int i = 0; i < queryString.Length; i++)
            {
                if (i == 0)
                {
                    search_txt += queryString[i];
                }
                else
                {
                    search_txt += "+" + queryString[i];
                }
            }

                switch (selected_search_num)
                {
                    case 0:
                        if (search_txt == "")
                        {
                            navWebbrowser("http://www.google.com/");
                            addText.Text = "http://www.google.com/";
                        }
                        else
                        {
                            navWebbrowser("http://www.google.com/search?q=" + search_txt);
                            addText.Text = "http://www.google.com/search?q=" + search_txt;//tst+a+c
                        }
                        break;
                    case 1:
                        if (search_txt == "")
                        {
                            navWebbrowser("http://search.yahoo.com/");
                            addText.Text = "http://search.yahoo.com/";
                        }
                        else
                        {
                            navWebbrowser("http://search.yahoo.com/search?p=" + search_txt);
                            addText.Text = "http://search.yahoo.com/search?p=" + search_txt;
                        }
                        break;
                    case 2:
                        if (search_txt == "")
                        {
                            navWebbrowser("http://www.bing.com/");
                            addText.Text = "http://www.bing.com/";
                        }
                        else
                        {
                            navWebbrowser("http://www.bing.com/search?q=" + search_txt);
                            addText.Text = "http://www.bing.com/search?q=" + search_txt;
                        }
                        break;
                    case 3:
                        if (search_txt == "")
                        {
                            navWebbrowser("http://search.aol.com/");
                            addText.Text = "http://search.aol.com/";
                        }
                        else
                        {
                            navWebbrowser("http://search.aol.com/aol/search?q=" + search_txt);
                            addText.Text = "http://search.aol.com/aol/search?q=" + search_txt;
                        }
                        break;
                    case 4:
                        if (search_txt == "")
                        {
                            navWebbrowser("http://www.baidu.com/");
                            addText.Text = "http://www.baidu.com/";
                        }
                        else
                        {
                            navWebbrowser("http://www.baidu.com/s?wd=" + search_txt);
                            addText.Text = "http://www.baidu.com/s?wd=" + search_txt;
                        }
                        break;
                    case 5:
                        if (search_txt == "")
                        {
                            navWebbrowser("http://www.yandex.com/");
                            addText.Text = "http://www.yandex.com/";
                        }
                        else
                        {
                            navWebbrowser("http://www.yandex.com/yandsearch?text=" + search_txt);
                            addText.Text = "http://www.yandex.com/yandsearch?text=" + search_txt;
                        }
                        break;
                    case 6:
                        if (search_txt == "")
                        {
                            navWebbrowser("http://www.naver.com/");
                            addText.Text = "http://www.naver.com/";
                        }
                        else
                        {
                            //http://search.naver.com/search.naver?query=task
                            navWebbrowser("http://search.naver.com/search.naver?query=" + search_txt);
                            addText.Text = "http://search.naver.com/search.naver?query=" + search_txt;
                        }
                        break;
                    case 7:
                        if (search_txt == "")
                        {
                            navWebbrowser(defaultSite);
                            addText.Text = defaultSite;
                        }
                        else
                        {
                            //http://search.naver.com/search.naver?query=task
                            navWebbrowser(defaultSite +"/search?q=" + search_txt);
                            addText.Text = defaultSite + "/search?q=" + search_txt;
                        }
                        break;
                }
        }

        private searchSetting searchPop;
        private void search_icon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           if ((searchPop != null))
                searchPop.Close();
            searchPop = new searchSetting();
            searchPop.default_site = this.defaultSite;
            //searchPop.selected_num = selected_search_num;
            searchPop.setDefaultNum(selected_search_num);
            searchPop.parent_win = this;
            searchPop.Show();

            /*this.Opacity = 0.3;
            this.Effect = new BlurEffect();
            LockWindow lockPOP = new LockWindow()
            {
                Owner = this,
                ShowInTaskbar = false,
                Topmost = true
            };

            lockPOP.setUserNameAndPass(real_usr_name, real_usr_pass);
            lockPOP.setMain(this);
            lockPOP.ShowDialog();*/
        }

        private void center_logo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //System.Threading.Thread.Sleep(10);
           /* Uri uri = new Uri("Images/chrome/chromeButton-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            center_logo.Source = bitmap;
            */
            //center_logo.Opacity = 0.05;
            if (ischrome)
            {
                ischrome = false;
                moveFirefoxWindow();
            }

        }

        private void center_logo_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Uri uri = new Uri("Images/chrome/chrome.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            center_logo.Source = bitmap;
        }

        private void center_logo_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Uri uri = new Uri("Images/chrome/chrome-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            center_logo.Source = bitmap;
        }

        private bool isLogoBubble = false;

        public void setLogoBubble(bool isLogBubb)
        {
            isLogoBubble = isLogBubb; 
        }

        private logoBubble logoBubble;
        


        private void center_logo_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if(!isLogoBubble)
            {
                //if (logoBubble != null)
                    //logoBubble.Close();
                closeAllBubble();
                /*logoBubble = new logoBubble();
                logoBubble.setParentWin(this);
                logoBubble.Topmost = true;
                logoBubble.Show(); */

            }
            Uri uri = new Uri("Images/chrome/chrome-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            center_logo.Source = bitmap;
        }

        private void center_logo_MouseLeave_1(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Uri uri = new Uri("Images/chrome/chrome.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            center_logo.Source = bitmap;
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public static implicit operator Point(POINT point)
            {
                return new Point(point.X, point.Y);
            }
        }

        /// <summary>
        /// Retrieves the cursor's position, in screen coordinates.
        /// </summary>
        /// <see>See MSDN documentation for further information.</see>
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        public static Point GetCursorPosition()
        {
            POINT lpPoint;
            GetCursorPos(out lpPoint);
            //bool success = User32.GetCursorPos(out lpPoint);
            // if (!success)

            return lpPoint;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (isSliderComplete)
            {
                isSliderComplete = false;
                Point centerPos = GetCursorPosition();
                double xPos = this.Width - 155;
                if ((xPos < centerPos.X) && ((xPos + 25) > centerPos.X) && (2 < centerPos.Y) && (30 > centerPos.Y))
                {
                    this.moveFirefoxWindow();
                }
            }
        }

        private void Grid_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            isSliderComplete = false;
        }

      
        private CapDevice[] cmDeviceArr;
        private void checkCamState()
        {
            
        }

        public static readonly DependencyProperty SelectedWebcamProperty =
        DependencyProperty.Register("SelectedWebcam", typeof(CapDevice), typeof(MainWindow), new UIPropertyMetadata(null));

        [StructLayout(LayoutKind.Sequential)]
        public struct PixelColor
        {
            public byte Blue;
            public byte Green;
            public byte Red;
            public byte Alpha;
        }

        public byte[] GetPixels(BitmapSource source)
        {
            if (source.Format != PixelFormats.Bgra32)
                source = new FormatConvertedBitmap(source, PixelFormats.Bgra32, null, 0);
            byte[] pixelByteArray = new byte[0];
            PixelColor[,] result = new PixelColor[0, 0];
            int width = 0;
            int height = 0;
            try
            {
                width = source.PixelWidth;
                height = source.PixelHeight;
                int nStride = (source.PixelWidth * source.Format.BitsPerPixel + 7) / 8;
                pixelByteArray = new byte[source.PixelHeight * nStride];
                source.CopyPixels(pixelByteArray, nStride, 0);
            }
            catch (Exception ex)
            {
                //System.Windows.MessageBox.Show("ee:" + ex.ToString());
                ex.ToString();
            }
            try
            {

                //source.CopyPixels(pixelByteArray, width * 4, 0);
            }
            catch (Exception exxx)
            {
                //System.Windows.MessageBox.Show("exxx:" + exxx.ToString());
                exxx.ToString();
            }
            return pixelByteArray;
        }

        private CapDevice capDev = new CapDevice();
        public void setCamBlock()
        {

            //isVPN = !isVPN;
            //if (isVPN)
            {
                try
                {
                    UsbUtils usbUTILS = new UsbUtils();
                    String usbnamespace = usbUTILS.getUSBDriver();
                    if (usbnamespace.Length > 0)
                    {

                        string folder_name = System.IO.Path.Combine(usbnamespace, "secure");
                        string file_name = System.IO.Path.Combine(folder_name, "camBlock.exe");
                        Process proc = new Process();
                        proc.StartInfo.WorkingDirectory = folder_name;//Program.NCBIBlastDirectory;                       
                        proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        proc.StartInfo.FileName = file_name;
                        proc.Start();
                        if (isVPN)
                            Canvas.SetRight(camblock, 197);
                        else
                            Canvas.SetRight(camblock, 138);
                        camblock.Visibility = System.Windows.Visibility.Visible;
                        isCam = !isCam;
                      
                    }

                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }
           
            //cmDeviceArr = capDev.Devices;
            
            /*this.cmPlayer = new CapPlayer();
            cmPlayer.Width = 1;
            cmPlayer.Height = 1;

            
            if (cmDeviceArr.Length > 0)
            {
                cmPlayer.Device = cmDeviceArr[0];
                camWrapper.Children.Add(cmPlayer);
            }
            else
            {
                //System.Windows.Forms.MessageBox.Show("Someone is using your Camera");
            }*/
        }

        public void setCamUnBlock()
        {

             
            {
                try
                {
                    Process[] localByName = Process.GetProcessesByName("camBlock");
                    for (int i = 0; i < localByName.Length; i++)
                    {
                        localByName[i].Kill();
                    }

                    camblock.Visibility = System.Windows.Visibility.Hidden;
                    if (isVPN)
                        Canvas.SetRight(vpnblock, 138);
                    isCam = !isCam;
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }
            /* while (true)
             {
                 try
                 {
                   
                     //cmPlayer.Dispose();
                     //System.Threading.Thread.Sleep(500);  
                     //sCapDevice.
                     //camWrapper.Children.Remove(cmPlayer);
                     //cmPlayer = null;
                                     
                    
                     //cmPlayer.Height = 0;
                     //cmPlayer.Width = 0;  
                     if((cmDeviceArr != null) && (cmDeviceArr.Length > 0))
                     {
                         for (int i = 0; i < cmDeviceArr.Length; i++)
                         {
                             cmDeviceArr[i].Dispose();
                             cmDeviceArr[i]  = null;
                         }
                         capDev.DelDevices();
                         cmDeviceArr = null;
                         capDev = null;
                         break;
                        
                     }
                     else
                      break;
                 }
                 catch (Exception ex)
                 {
                     continue;
                 }
             }*/
        }


        private void camDetecter_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
          /*  try
            {
                if (isCam)
                {

                    cmDeviceArr = CapDevice.Devices;
                    this.cmPlayer = new CapPlayer();
                    cmPlayer.Width = 1;
                    cmPlayer.Height = 1;
                    Uri uri = new Uri("Images/camera/camera_disable.png", UriKind.Relative);
                    var bitmaps = new BitmapImage(uri);
                    camDetecter.Source = bitmaps;
                    isCam = !isCam;
                    if (cmDeviceArr.Length > 0)
                    {
                        cmPlayer.Device = cmDeviceArr[0];
                        camWrapper.Children.Add(cmPlayer);
                        
                       
                    }
                    else
                    {
                        //System.Windows.Forms.MessageBox.Show("Someone is using your Camera");
                    }
                }
                else
                {
                    Uri uri = new Uri("Images/camera/camera_enable.png", UriKind.Relative);
                    var bitmapss = new BitmapImage(uri);
                    camDetecter.Source = bitmapss;
                    isCam = !isCam;   
                 
                    cmPlayer.Height = 0;
                    cmPlayer.Width = 0;
                    cmPlayer.Dispose();
                    camWrapper.Children.Remove(cmPlayer);
                    cmPlayer = null;
                    //cmDeviceArr = new CapDevice(); 
                    cmDeviceArr = null;

                }
            }
            catch (Exception ex)
            {
                
                //addText.Text = "Camera Error";
                //addText.Text = cmDeviceArr[0].MonikerString;
                ex.ToString();
            }
            */
        }


        /*public Boolean AllowClose
        {
            get { return (Boolean)GetValue(AllowCloseProperty); }
            set { SetValue(AllowCloseProperty, value); }
        }

        public static readonly DependencyProperty AllowCloseProperty =
           DependencyProperty.Register("AllowClose", typeof(Boolean),
           typeof(MyUserControl), new UIPropertyMetadata(false));
        */
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            //if (!myUserControl.AllowClose)
            {
                //MessageBox.Show("Even though most Windows allow Alt-F4 to close, I'm not letting you!");
               // e.Cancel = true;
            }
            /*  else
              {
                  //Content = null; // Remove child from parent - for reuse
                  this.RemoveLogicalChild(Content); //this works faster
                  base.OnClosing(e);
                  { GC.Collect(); };
              }*/
        }


        protected override void OnPreviewKeyDown(System.Windows.Input.KeyEventArgs e)
        {
            if ((Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt)) && Keyboard.IsKeyDown(Key.F4))
                e.Handled = true;
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.F4)
                return;
        }


        public bool isCameraBubble = false;

       
        public void setCameraBubble(bool isCamBubb)
        {
            isCameraBubble = isCamBubb;

        }

        public void setClrBubble(bool isClrBubb)
        {
            isclrBubble = isClrBubb;

        }

        private cameraBubble camBubble;

        public void showCamTutorial()
        {
           /* if (!isCameraBubble)
            {
                if (camBubble != null)
                    camBubble.Close();
                closeAllBubble();
                camBubble = new cameraBubble();
                camBubble.setParentWin(this);
                camBubble.Topmost = true;
                camBubble.Show();
            }*/
        }


        public void showVpnTutorial()
        {
            /*if (!isLogoBubble)
            {
                if (logoBubble != null)
                    logoBubble.Close();
                closeAllBubble();
                logoBubble = new  logoBubble();
                logoBubble.setParentWin(this);
                logoBubble.Topmost = true;
                logoBubble.Show();
            }*/
        }

        private clrBubble clrBubble;

        private bool isclrBubble = false;

        public void showClrTutorial()
        {
           /* if (!isclrBubble)
            {
                if (clrBubble != null)
                    clrBubble.Close();
                closeAllBubble();
                clrBubble = new clrBubble();
                clrBubble.setParentWin(this);
                clrBubble.Topmost = true;
                clrBubble.Show();
            }*/
        }

        public void showLockBubble()
        {
           /* if (!isLockBubble)
            {
                if (lockBubble != null)
                    lockBubble.Close();
                closeAllBubble();
                lockBubble = new lockBubble();
                lockBubble.setParentWin(this);
                lockBubble.Topmost = true;
                lockBubble.Show();
            }*/
        }

        public void showCloudBubble()
        {
           /* if (!isCloudBubble)
            {
                if (cloudBubb != null)
                    cloudBubb.Close();
                closeAllBubble();
                cloudBubb = new cloudIconBubble();
                cloudBubb.setParentWin(this);
                cloudBubb.Topmost = true;
                cloudBubb.Show(); 
            }*/
        }

        
        private void camDetecter_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
           /* if (!isCameraBubble)
            {
                if (camBubble != null)
                    camBubble.Close();
                closeAllBubble();
                camBubble = new cameraBubble();
                camBubble.setParentWin(this);
                camBubble.Topmost = true;
                camBubble.Show();  
            }


            if (isCam)
            {
                Uri uri = new Uri("Images/camera/camera_enable.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                camDetecter.Source = bitmap;
            }
            else
            {
                Uri uri = new Uri("Images/camera/camera_disable.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                camDetecter.Source = bitmap;
            }
            */

        }


        /*****
         * 
         *Camera detect 
         * 
         * ************/


        private void camDetecter_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
           /* if (isCam)
            {
                Uri uri = new Uri("Images/camera/camera_over.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                camDetecter.Source = bitmap;
            }
            else
            {
                Uri uri = new Uri("Images/camera/camera_disable.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                camDetecter.Source = bitmap;
            }*/
        }

        private void usbButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
          /*  if (!isUSBBubble)
            {
                //if ((usbBubb != null))
                    //usbBubb.Close();
                closeAllBubble();
                usbBubb = new usbBubble();
                usbBubb.setParentWin(this);
                usbBubb.Topmost = true;
                usbBubb.Show();                
            }*/
            UpdateAllTextBox();
        }

        private bool isCloudBubble = false;

        public void setCloudBubble(bool isCloudBubb)
        {
            isCloudBubble = isCloudBubb;

        }

        private cloudIconBubble cloudBubb;

       

        private void cloudImg_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!isCloudBubble)
            {
                //if ((cloudBubb != null))
                    //cloudBubb.Close();
                closeAllBubble();
                /*cloudBubb = new cloudIconBubble();
                cloudBubb.setParentWin(this);
                cloudBubb.Topmost = true;
                cloudBubb.Show();     */           
            }
        }


        
        private googleIconBubble googleBubble;

        private bool isgoogleBubble = false;

        public void setGoogleBubble(bool isGoogleBubb)
        {
            isgoogleBubble = isGoogleBubb;
        }

        private void search_icon_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
          /*  switch (selected_search_num)
            {
                case 0:
                    var uri = new Uri("Images/toolbar/google_s.png", UriKind.Relative);
                    var bitmap = new BitmapImage(uri);
                    this.search_icon.Source = bitmap;
                    break;
                case 1:
                    uri = new Uri("Images/search/yahoo-over.png", UriKind.Relative);
                    bitmap = new BitmapImage(uri);
                    this.search_icon.Source = bitmap;
                    break;

                case 2:
                    uri = new Uri("Images/search/bing-over.png", UriKind.Relative);
                    bitmap = new BitmapImage(uri);
                    this.search_icon.Source = bitmap;
                    break;

                case 3:
                    uri = new Uri("Images/search/aol-over.png", UriKind.Relative);
                    bitmap = new BitmapImage(uri);
                    this.search_icon.Source = bitmap;
                    break;

                case 4:
                    uri = new Uri("Images/search/baidu-over.png", UriKind.Relative);
                    bitmap = new BitmapImage(uri);
                    this.search_icon.Source = bitmap;
                    break;
                case 5:
                    uri = new Uri("Images/search/yandex-over.png", UriKind.Relative);
                    bitmap = new BitmapImage(uri);
                    this.search_icon.Source = bitmap;
                    break;
                case 6:
                    uri = new Uri("Images/search/naver-over.png", UriKind.Relative);
                    bitmap = new BitmapImage(uri);
                    this.search_icon.Source = bitmap;
                    break;
                case 7:
                    uri = new Uri(defaultSite + "/favicon.ico");
                    search_bitmap = new BitmapImage(uri);
                    search_bitmap.DownloadCompleted += new EventHandler(bitmap_DownloadCompleted);
                    break;
            }*/
            this.checkSearchBiggerImage();
            search_icon.Width = 14;
            search_icon.Height = 14;
            /*
            if (!isgoogleBubble)
            {
                //if ((googleBubble != null))
                   // googleBubble.Close();
                closeAllBubble();
                googleBubble = new googleIconBubble();
                googleBubble.setParentWin(this);
                googleBubble.Topmost = true;
                googleBubble.Show();
            }*/
        }


        private void checkSearchImage()
        {

            switch (selected_search_num)
            {
                case 0:
                    var uri = new Uri("Images/toolbar/google_s.png", UriKind.Relative);
                    var bitmap = new BitmapImage(uri);
                    this.search_icon.Source = bitmap;
                    break;
                case 1:
                    uri = new Uri("Images/toolbar/yahoo_s.png", UriKind.Relative);
                    bitmap = new BitmapImage(uri);
                    this.search_icon.Source = bitmap;
                    break;

                case 2:
                    uri = new Uri("Images/toolbar/bing_s.png", UriKind.Relative);
                    bitmap = new BitmapImage(uri);
                    this.search_icon.Source = bitmap;
                    break;

                case 3:
                    uri = new Uri("Images/toolbar/rambler_s.png", UriKind.Relative);
                    bitmap = new BitmapImage(uri);
                    this.search_icon.Source = bitmap;
                    break;

                case 4:
                    uri = new Uri("Images/toolbar/baidu_s.png", UriKind.Relative);
                    bitmap = new BitmapImage(uri);
                    this.search_icon.Source = bitmap;
                    break;
                case 5:
                    uri = new Uri("Images/toolbar/yandex_s.png", UriKind.Relative);
                    bitmap = new BitmapImage(uri);
                    this.search_icon.Source = bitmap;
                    break;
                case 6:
                    uri = new Uri("Images/toolbar/naver_s.png", UriKind.Relative);
                    bitmap = new BitmapImage(uri);
                    this.search_icon.Source = bitmap;
                    break;
                case 7:
                    uri = new Uri(defaultSite + "/favicon.ico");
                    search_bitmap = new BitmapImage(uri);
                    search_bitmap.DownloadCompleted += new EventHandler(bitmap_DownloadCompleted);
                    break;
            }
        }

        private void checkSearchBiggerImage()
        {

            switch (selected_search_num)
            {
                case 0:
                    var uri = new Uri("Images/toolbar/google_w.png", UriKind.Relative);
                    var bitmap = new BitmapImage(uri);
                    this.search_icon.Source = bitmap;
                    break;
                case 1:
                    uri = new Uri("Images/toolbar/yahoo_w.png", UriKind.Relative);
                    bitmap = new BitmapImage(uri);
                    this.search_icon.Source = bitmap;
                    break;

                case 2:
                    uri = new Uri("Images/toolbar/bing_w.png", UriKind.Relative);
                    bitmap = new BitmapImage(uri);
                    this.search_icon.Source = bitmap;
                    break;

                case 3:
                    uri = new Uri("Images/toolbar/rambler_w.png", UriKind.Relative);
                    bitmap = new BitmapImage(uri);
                    this.search_icon.Source = bitmap;
                    break;

                case 4:
                    uri = new Uri("Images/toolbar/baidu_w.png", UriKind.Relative);
                    bitmap = new BitmapImage(uri);
                    this.search_icon.Source = bitmap;
                    break;
                case 5:
                    uri = new Uri("Images/toolbar/yandex_w.png", UriKind.Relative);
                    bitmap = new BitmapImage(uri);
                    this.search_icon.Source = bitmap;
                    break;
                case 6:
                    uri = new Uri("Images/toolbar/naver_w.png", UriKind.Relative);
                    bitmap = new BitmapImage(uri);
                    this.search_icon.Source = bitmap;
                    break;
                case 7:
                    uri = new Uri(defaultSite + "/favicon.ico");
                    search_bitmap = new BitmapImage(uri);
                    search_bitmap.DownloadCompleted += new EventHandler(bitmap_DownloadCompleted);
                    break;
            }
        }



        private BitmapImage search_bitmap;

        void bitmap_DownloadCompleted(object sender, EventArgs e)
        {

            search_icon.Source = search_bitmap;
        }


        private musicBubble musicBubble;

        private bool isMusicBubble = false;

        public void setMusicBubble(bool isMusicBubb)
        {
            isMusicBubble = isMusicBubb;
        }

        //private musicBubble musicBubble;

        private void mp_title_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            
            if (!isMusicBubble)
            {
                //if ((musicBubble != null))
                   // musicBubble.Close();
                closeAllBubble();
                /*musicBubble = new musicBubble();
                musicBubble.setParentWin(this);
                musicBubble.Topmost = true;
                musicBubble.Show();*/
            }

        }

        private bool isLockBubble = false;
        public void setLockBubble(bool isLockBubb)
        {
            isLockBubble = isLockBubb;
        }

        private lockBubble lockBubble;

        private void lock_button_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!isLockBubble)
            {
                //if (lockBubble != null)
                   /// lockBubble.Close();
                closeAllBubble();
                /*lockBubble = new lockBubble();
                lockBubble.setParentWin(this);
                lockBubble.Topmost = true;
                lockBubble.Show();*/
            }

        }

        private bool isSettingBubble = false;

        public void setSettingBubble(bool isSettBubble)
        {
            isSettingBubble = isSettBubble;
        }

        private settingBubble settingBubble;
        private toolbar tool_bar;
        private void setting_button_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //if (!isSettingBubble)
            {
               // if (settingBubble != null)
                   // settingBubble.Close();
                UpdateAllTextBox();
                if (tool_bar != null)
                    tool_bar.Close();
                tool_bar = null;
                tool_bar = new toolbar();
               // {
                  //  Owner = this,
                   // ShowInTaskbar = false,
                    //Topmost = true
                //};
                tool_bar.setMain(this);
                tool_bar.setInfo(this.real_usr_name, this.real_usr_pass);
                tool_bar.setCamIcon();
                tool_bar.setColorPallet();
                tool_bar.Show();
                this.Topmost = true;
                //tool_bar.DoDynamicAnimationPare();
                //closeAllBubble();
                /*settingBubble = new settingBubble();
                settingBubble.setParentWin(this);
                settingBubble.Topmost = true;
                settingBubble.Show();*/
            }
        }


        public void closeAllBubble()
        {
            saveBubbleInfo();
            if (this.usbBubb != null)
                usbBubb.Close();
            if (searchBox != null)
                searchBox.Close();
            if ((cloudBubb != null))
                cloudBubb.Close();
            if (urlBubb != null)
                urlBubb.Close();
            if (googleBubble != null)
                googleBubble.Close();
            if ((musicBubble != null))
                musicBubble.Close();
            if (logoBubble != null)
                logoBubble.Close();
            if (camBubble != null)
                camBubble.Close();
            if (lockBubble != null)
                lockBubble.Close();
            if (settingBubble != null)
                settingBubble.Close();
            if (closeBubble != null)
                closeBubble.Close();
            if (clrBubble != null)
                clrBubble.Close();
        }



        private void saveBubbleInfo()
        {
            try
            {
                UsbUtils usbUTILS = new UsbUtils();
                String usbnamespace = usbUTILS.getUSBDriver();

                String file_name = "";
               // String tabInfo = "";
                if (usbnamespace.Length > 0)
                {
                    string folder_name = System.IO.Path.Combine(usbnamespace, "wemagin_v2");
                    if (!Directory.Exists(folder_name))
                    {
                        Directory.CreateDirectory(folder_name);
                        DirectoryInfo dir = new DirectoryInfo(folder_name);
                        dir.Attributes |= FileAttributes.Hidden;
                    };

                    file_name = usbnamespace + "wemagin_v2\\mainbubble.xml";

                    FileStream fs;
                    fs = System.IO.File.Create(file_name);

                    XmlTextWriter w = new XmlTextWriter(fs, Encoding.UTF8);
                    w.WriteStartDocument();
                    w.WriteStartElement("Contact");
                    
                    if (isUSBBubble)
                        w.WriteElementString("usbBubble", "1");
                    else
                        w.WriteElementString("usbBubble", "0");

                    if (isCloudBubble)
                        w.WriteElementString("cloudBubble", "1");
                    else
                        w.WriteElementString("cloudBubble", "0");

                    if (isgoogleBubble)
                        w.WriteElementString("googleBubble", "1");
                    else
                        w.WriteElementString("googleBubble", "0");

                    if (isUrlBubble)
                        w.WriteElementString("urlBubble", "1");
                    else
                        w.WriteElementString("urlBubble", "0");


                    if (isSearchBubble)
                        w.WriteElementString("searchBubble", "1");
                    else
                        w.WriteElementString("searchBubble", "0");

                    if (isMusicBubble)
                        w.WriteElementString("musicBubble", "1");
                    else
                        w.WriteElementString("musicBubble", "0");

                    if (isLogoBubble)
                        w.WriteElementString("logoBubble", "1");
                    else
                        w.WriteElementString("logoBubble", "0");


                    if (isCameraBubble)
                        w.WriteElementString("cameraBubble", "1");
                    else
                        w.WriteElementString("cameraBubble", "0");

                    if (isclrBubble)
                        w.WriteElementString("clrBubble", "1");
                    else
                        w.WriteElementString("clrBubble", "0");


                    if(isLockBubble)
                        w.WriteElementString("lockBubble", "1");
                    else
                        w.WriteElementString("lockBubble", "0");

                    if (isSettingBubble)
                        w.WriteElementString("settingBubble", "1");
                    else
                        w.WriteElementString("settingBubble", "0");
                    
                    if (isCloseBubble)
                        w.WriteElementString("closeBubble", "1");
                    else
                        w.WriteElementString("closeBubble", "0");

                    
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

                    String file_name = usbnamespace + "wemagin_v2\\mainbubble.xml";
                    if (System.IO.File.Exists(file_name))
                    {

                        XmlReader reader = XmlReader.Create(file_name);

                        while (reader.Read())
                        {
                            switch (reader.NodeType)
                            {
                                case XmlNodeType.Element:
                                    if (reader.Name.Equals("usbBubble"))
                                    {
                                        reader.Read();
                                        if (reader.Value.ToString() == "1")
                                            isUSBBubble = true;
                                    }
                                    else if (reader.Name.Equals("cloudBubble"))
                                    {

                                        reader.Read();
                                        if (reader.Value.ToString() == "1")
                                            isCloudBubble = true;

                                        // Read the XML Node's attributes and add to string
                                    }
                                    else if (reader.Name.Equals("googleBubble"))
                                    {
                                        reader.Read();
                                        if (reader.Value.ToString() == "1")
                                            isgoogleBubble = true;


                                    }
                                    else if (reader.Name.Equals("urlBubble"))
                                    {

                                        reader.Read();
                                        if (reader.Value.ToString() == "1")
                                            isUrlBubble = true;
                                    }                           
                                    else if (reader.Name.Equals("searchBubble"))
                                    {

                                        reader.Read();
                                        if (reader.Value.ToString() == "1")
                                            isSearchBubble = true;
                                    }

                                    else if (reader.Name.Equals("musicBubble"))
                                    {

                                        reader.Read();
                                        if (reader.Value.ToString() == "1")
                                            isMusicBubble = true;
                                    }

                                    else if (reader.Name.Equals("logoBubble"))
                                    {

                                        reader.Read();
                                        if (reader.Value.ToString() == "1")
                                            isLogoBubble = true;
                                    }
                                    else if (reader.Name.Equals("cameraBubble"))
                                    {

                                        reader.Read();
                                        if (reader.Value.ToString() == "1")
                                            isCameraBubble = true;
                                    }
                                    else if (reader.Name.Equals("clrBubble"))
                                    {
                                        reader.Read();
                                        if (reader.Value.ToString() == "1")
                                            isclrBubble = true;
                                    }                                         
                                    else if (reader.Name.Equals("lockBubble"))
                                    {

                                        reader.Read();
                                        if (reader.Value.ToString() == "1")
                                            isLockBubble = true;
                                    }
                                    else if (reader.Name.Equals("settingBubble"))
                                    {

                                        reader.Read();
                                        if (reader.Value.ToString() == "1")
                                            isSettingBubble = true;
                                    }
                                    else if (reader.Name.Equals("closeBubble"))
                                    {

                                        reader.Read();
                                        if (reader.Value.ToString() == "1")
                                            isCloseBubble = true;
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


        private bool isCloseBubble = false;

        public void setCloseBubble(bool isClsBubble)
        {
            isCloseBubble = isClsBubble;
        }

        private closeBubble closeBubble;

        private void Image_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!isCloseBubble)
            {
                //if (closeBubble != null)
                    //closeBubble.Close();

                closeAllBubble();
                /*closeBubble = new closeBubble();
                closeBubble.setParentWin(this);
                closeBubble.Topmost = true;
                closeBubble.Show();*/
            }
        }

        private void Window_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //closeAllBubble();
            if (((e.GetPosition(this).X < (this.Width - 140)) || (e.GetPosition(this).X > (this.Width - 90))) && (tool_bar != null))
            {
                tool_bar.Close();
                tool_bar =  null ;
            }
        }

        private void search_icon_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Point pos = e.GetPosition(search_icon);
            if (pos.X > 14 || pos.X < 0)
                closeAllBubble();
            
        }

        private void usbImg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Process[] localByName = Process.GetProcessesByName("firefox");
                //ShowWindowAsync(localByName[0].MainWindowHandle, SW_SHOWMAXIMIZED);
                //MoveWindow(localByName[0].MainWindowHandle, 0, 31, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - 31, true);
                HideMinimizeAndMaximizeButtons(localByName[0].MainWindowHandle);
                MoveWindow(localByName[0].MainWindowHandle, -5, Convert.ToInt32(55 * (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height / System.Windows.SystemParameters.WorkArea.Height)), System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width+10, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - 55, true);
                
            }
            catch (Exception exx)
            {
                exx.ToString();
            }

        }

        private void search_text_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (search_text.Text == "Clickless Search")
                search_text.Text = "";
                search_text.Focus();
        }

        private void Mini_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDeactive = true;
            this.WindowState = System.Windows.WindowState.Minimized;
            Process[] localByName = Process.GetProcessesByName("firefox");
            ShowWindowAsync(localByName[0].MainWindowHandle, SW_SHOWMINIMIZED);
        }

        public void camblockSetting(bool state)
        {
           
            if (isCam)
            {
               
                setCamBlock();
                //Uri uris = new Uri("Images/toolbar/cam_block.png", UriKind.Relative);
                //var bitmaps = new BitmapImage(uris);
                //camblock.Source = bitmaps;
            }
            else
            {
               
                this.setCamUnBlock();
                
                /*
                Uri uris = new Uri("Images/toolbar/main_cam.png", UriKind.Relative);
                var bitmaps = new BitmapImage(uris);
                camblock.Source = bitmaps;*/
            }
        }


        public void getCrl()
        {
            try
            {
                UsbUtils usbUTILS = new UsbUtils();
                String usbnamespace = usbUTILS.getUSBDriver();
                if (usbnamespace.Length > 0)
                {

                    String file_name = usbnamespace + "wemagin_v2\\clr.xml";
                    if (System.IO.File.Exists(file_name))
                    {

                        XmlReader reader = XmlReader.Create(file_name);

                        while (reader.Read())
                        {
                            switch (reader.NodeType)
                            {
                                case XmlNodeType.Element:
                                    if (reader.Name.Equals("col"))
                                    {
                                        reader.Read();
                                        string val = reader.Value.ToString();

                                        color_type = Int32.Parse(val);
                                        setColVal();
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

        private void setColVal()
        {
            switch (this.color_type)
            {
                case 0 :
                    setColor(255, 255, 255);
                    break;
                case 1:
                    setColor(223, 43, 42);
                    break;
                case 2:
                    setColor(41, 165, 223);
                    break;
                case 3:
                    setColor(237, 159, 32);
                    break;
                case 4:
                    setColor(219, 31, 199);
                    break;
                case 5:
                    setColor(4, 4, 4);
                    break;

            }
        }

        private void usbImg_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Point pos = e.GetPosition(usbImg);
            if(pos.X > 67)
            this.closeAllBubble();
        }

        private void search_text_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            
            Point pos = e.GetPosition(search_text);
            if (pos.X > 200 || pos.X < 0)
                closeAllBubble();
        }

        private void visibleTxt_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            UpdateAllTextBox();
        }

        private void Image_MouseEnter_1(object sender, System.Windows.Input.MouseEventArgs e)
        {
            UpdateAllTextBox();
        }

        
       
    }

   
}
