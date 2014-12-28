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
using System.Windows.Media.Effects;
using System.Diagnostics;

using System.Runtime.InteropServices;
using System.Security.Principal;
//using System.Diagnostics;


namespace chromeBrowser
{
    /// <summary>
    /// Interaction logic for LockWindow.xaml
    /// </summary>
    public partial class LockWindow : Window
    {

        private MainWindow parent_win;
        private bool isLocked = true;

        public LockWindow()
        {
            InitializeComponent();
            //EnableTaskManager(true);
            /*intLLKey = SetWindowsHookEx(WH_KEYBOARD_LL, LowLevelKeyboardProc, System.Runtime.InteropServices.Marshal.GetHINSTANCE(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0]).ToInt32(), 0);

            ProcessStartInfo psi = new ProcessStartInfo(System.IO.Path.Combine(Environment.SystemDirectory, "taskmgr.exe"));
            psi.RedirectStandardOutput = false;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.UseShellExecute = true;
            Process.Start(psi);
            */
            //KeyboardHook();
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            this.Height = screenHeight ;
            this.Width = screenWidth;
            this.Left = 0;
            this.Top = 0;
            lock_btn.AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(lock_btn_MouseLeftButtonDown), true);
            user_name.AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(user_name_MouseLeftButtonDown), true);
            user_pass.AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(user_pass_MouseLeftButtonDown), true);
            //this.Opacity = 0.7;
            user_name.Focus();
            user_name.SelectAll();
            setRoundCan(Width, Height);          

        }


        private static void EnableTaskManager(bool enable) 
        {   
            Microsoft.Win32.RegistryKey HKCU = Microsoft.Win32.Registry.CurrentUser;
            Microsoft.Win32.RegistryKey key = HKCU.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System") ;
            key.SetValue("DisableTaskMgr", enable ? 0 : 1, Microsoft.Win32.RegistryValueKind.DWord);
        }

        /*
        #region " Disable Special Keys"*/

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

        private int LowLevelKeyboardProc( int nCode, int wParam,  ref KBDLLHOOKSTRUCT lParam)
        {
            //return CallNextHookEx(0, nCode, wParam, ref lParam);
            try
            {
                bool blnEat = false;
                switch (wParam)
                {
                    case 256:
                    case 257:
                    case 260:
                    case 261:
                        //Alt+Tab, Alt+Esc, Ctrl+Esc, Windows Key
                        if (((lParam.vkCode == 27) && (lParam.flags == 32) && (lParam.flags == 0)) 
                            || ((lParam.vkCode == 9) && (lParam.flags == 32)) ||
                            ((lParam.vkCode == 27) && (lParam.flags == 32)) || 
                            ((lParam.vkCode == 27) && (lParam.flags == 0)) || 
                            ((lParam.vkCode == 91) && (lParam.flags == 1)) || 
                            ((lParam.vkCode == 92) && (lParam.flags == 1)) || 
                            ((true) && (lParam.flags == 32)))
                        {
                            blnEat = true;
                        }
                        break;
                }

                if (blnEat)
                    return 1;
                else
                return CallNextHookEx(0, nCode, wParam, ref lParam);
            }
            catch (Exception ex)
            {
                //return 1;
                ex.ToString();
            }
            return CallNextHookEx(0, nCode, wParam, ref lParam);
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

        /*
        #endregion  
        */
        /*

        private delegate int LowLevelKeyboardProcDelegate(int nCode, int wParam, ref KBDLLHOOKSTRUCT lParam);
        [DllImport("user32.dll", EntryPoint = "SetWindowsHookExA",
        CharSet = CharSet.Auto)]
        private static extern int SetWindowsHookEx(int idHook,
        LowLevelKeyboardProcDelegate lpfn, int hMod, int dwThreadId);
        [DllImport("user32.dll", EntryPoint = "UnHookWindowsHookEx",
       CharSet = CharSet.Auto)]
        private static extern int UnHookWindowsEx(int hHook);
        [DllImport("user32.dll", EntryPoint = "CallNextHookEx",
       CharSet = CharSet.Auto)]
        private static extern int CallNextHookEx(int hHook, int nCode, int
        wParam, ref KBDLLHOOKSTRUCT lParam);
        const int WH_KEYBOARD_LL = 13;
        public struct KBDLLHOOKSTRUCT
        {
            public int vkCode;
            int scanCode;
            public int flags;
            int time;
            int dwExtraInfo;
        }
        private int intLLKey;
        private KBDLLHOOKSTRUCT lParam;

        private int LowLevelKeyboardProc(int nCode, int wParam, ref KBDLLHOOKSTRUCT lParam)
        {
            bool blnEat = false;
            switch (wParam)
            {
                case 256:
                case 257:
                case 260:
                case 261:
                    //Alt+Tab, Alt+Esc, Ctrl+Esc, Windows Key
                    if (((lParam.vkCode == 27) && (lParam.flags == 32) && (lParam.flags == 0)) 
                     || ((lParam.vkCode == 9) && (lParam.flags == 32)) ||
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
        public void KeyboardHook()
        {
            intLLKey = SetWindowsHookEx(WH_KEYBOARD_LL, new
            LowLevelKeyboardProcDelegate(LowLevelKeyboardProc) ,
            System.Runtime.InteropServices.Marshal.GetHINSTANCE(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0]).ToInt32(),0);
        }

        

        [DllImport("user32", EntryPoint = "SetWindowsHookExA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int SetWindowsHookEx(int idHook, LowLevelKeyboardProcDelegate lpfn, int hMod, int dwThreadId);
        [DllImport("user32", EntryPoint = "UnhookWindowsHookEx", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int UnhookWindowsHookEx(int hHook);
        public delegate int LowLevelKeyboardProcDelegate(int nCode, int wParam, ref KBDLLHOOKSTRUCT lParam);
        [DllImport("user32", EntryPoint = "CallNextHookEx", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int CallNextHookEx(int hHook, int nCode, int wParam, ref KBDLLHOOKSTRUCT lParam);
        public const int WH_KEYBOARD_LL = 13;*/

        /*code needed to disable start menu*/
        /*[DllImport("user32.dll")]
        private static extern int FindWindow(string className, string windowText);
        [DllImport("user32.dll")]
        private static extern int ShowWindow(int hwnd, int command);

        private const int SW_HIDE = 0;
        private const int SW_SHOW = 1;
        public struct KBDLLHOOKSTRUCT
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }
        public static int intLLKey;

        public int LowLevelKeyboardProc(int nCode, int wParam, ref KBDLLHOOKSTRUCT lParam)
        {
            bool blnEat = false;

            switch (wParam)
            {
                case 256:
                case 257:
                case 260:
                case 261:
                    //Alt+Tab, Alt+Esc, Ctrl+Esc, Windows Key,
                    blnEat = ((lParam.vkCode == 9) && (lParam.flags == 32)) | ((lParam.vkCode == 27) && (lParam.flags == 32)) | ((lParam.vkCode == 27) && (lParam.flags == 0)) | ((lParam.vkCode == 91) && (lParam.flags == 1)) | ((lParam.vkCode == 92) && (lParam.flags == 1)) | ((lParam.vkCode == 73) && (lParam.flags == 0));
                    break;
            }

            if (blnEat == true)
            {
                return 1;
            }
            else
            {
                return CallNextHookEx(0, nCode, wParam, ref lParam);
            }
        }
        public void KillStartMenu()
        {
            int hwnd = FindWindow("Shell_TrayWnd", "");
            ShowWindow(hwnd, SW_HIDE);
        }


        */
        private void mini_icon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void setRoundCan(double wids, double heights)
        {
            /*top_can.Width = wids;
            top_can.Height = (heights - 240) / 2 + 3;
            top_can.Opacity = 0.7;
            top_can.Effect = new BlurEffect();
            top_can.Margin = new Thickness(0, 0, 0, top_can.Height + 238);
            // Canvas.SetTop(top_can, 0);
            // Canvas.SetLeft(top_can, 0);

            left_can.Height = 244;
            left_can.Width = (wids - 484) / 2+2;
            left_can.Opacity = 0.7;
            left_can.Effect = new BlurEffect();
            left_can.Margin = new Thickness(0, top_can.Height, left_can.Width + 483, top_can.Height-2);*/

            //Canvas.SetTop(left_can, top_can.Height);
            // Canvas.SetLeft(left_can, 0);


           /* right_can.Height = 244;
            right_can.Width = (wids - 484) / 2 +2;
            right_can.Opacity = 0.7;
            right_can.Effect = new BlurEffect();
            //right_can.Margin = new Thickness(left_can.Width + 400, top_can.Height, 0, 0);
            right_can.Margin = new Thickness(left_can.Width + 483, top_can.Height , 0, top_can.Height-2);
            //Canvas.SetTop(right_can, top_can.Height);
            //Canvas.SetRight(right_can, 0);

            bottom_can.Height = (heights - 240) / 2  + 2 ;
            bottom_can.Width = wids;
            bottom_can.Opacity = 0.7;
            bottom_can.Effect = new BlurEffect();
            bottom_can.Margin = new Thickness(0, top_can.Height + 240-2, 0, 0);*/
            
            //Canvas.SetBottom(bottom_can, 0);
            //Canvas.SetLeft(bottom_can, 0);

           
            overcan.Width = wids;
            overcan.Height = heights;
            //overcan.BorderThickness = new Thickness(1);

            over_can.Width = wids;
            over_can.Height = heights;
            over_can.Margin = new Thickness(0, 0, 0, 0);
            center_can.Width = 484;
            center_can.Height = 240;
            //center_can.Margin = new Thickness(left_can.Width, top_can.Height, left_can.Width, top_can.Height);
            //CanvasBorder.BorderThickness = new Thickness(1);
            //Canvas.SetTop(center_can, (heights - 240) / 2);
            //Canvas.SetLeft(center_can, (wids - 484) / 2);
            center_can.Margin = new Thickness((wids - 484) / 2, (heights - 240) / 2, (wids - 484) / 2, (heights - 240) / 2);
        }

        

        public void setMain(MainWindow main_win)
        {
            parent_win = main_win;

            //this.Height = parent_win.Height - 100;



        }

        private void close_icon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!isLocked)
            {
                parent_win.Opacity = 1;
                parent_win.Effect = null;
                this.Close();
            }
            else
            {
                lockResult lockPOP = new lockResult()
                {
                    Owner = this,
                    ShowInTaskbar = false,
                    Topmost = true
                };

                lockPOP.ShowDialog();
            }
        }


        private string real_usr_name = "";
        private string real_usr_pass = "";

        public void setUserNameAndPass(string usr_name, string usr_pass)
        {
            real_usr_name = usr_name;
            real_usr_pass = usr_pass;
        }

        private void lock_btn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((this.real_usr_name == this.user_name.Text) && (this.real_usr_pass == this.user_pass.Password))
            {
                isCloseEnabel = true;
                parent_win.Opacity = 1;
                parent_win.Effect = null;
                this.Close();

            }
            else
            {
                lockResult lockPOP = new lockResult()
                {
                    Owner = this,
                    ShowInTaskbar = false,
                    Topmost = true
                };
                lockPOP.ShowDialog();

                isLocked = true;
                Uri uri = new Uri("Images/lock/unlock_icon.jpg", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                //lock_icon.Source = bitmap;
            }
        }

        private void user_name_MouseEnter(object sender, MouseEventArgs e)
        {
            user_name.Focus();
            user_name.SelectAll();

        }

        private void user_name_MouseLeave(object sender, MouseEventArgs e)
        {
            if (user_name.Text == "")
            {
                user_name.Text = "User Email";
            }
        }

        private void user_pass_MouseEnter(object sender, MouseEventArgs e)
        {
            user_pass.Focus();
            user_pass.SelectAll();
        }

        private void user_name_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Enter) || (e.Key == Key.Tab))
            {
                if (user_name.Text == "")
                {
                    user_name.Text = "User Email";
                }

                user_passOver.Visibility = System.Windows.Visibility.Hidden;
                user_pass.Visibility = System.Windows.Visibility.Visible;

                user_pass.Focus();
                user_pass.SelectAll();
            }
            else
            {
                if (user_name.Text == "User Email")
                {
                    user_name.Text = "";
                }
            }
        }



        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (!isCloseEnabel == true)
            {
               // MessageBox.Show("Even though most Windows allow Alt-F4 to close, I'm not letting you!");
                e.Cancel = true;
            }
          /*  else
            {
                //Content = null; // Remove child from parent - for reuse
                this.RemoveLogicalChild(Content); //this works faster
                base.OnClosing(e);
                { GC.Collect(); };
            }*/
        }


      


        private void TextBox_MouseEnter_1(object sender, MouseEventArgs e)
        {
            Color clr = Color.FromRgb(122, 157, 147);
            //lock_btn.Background = new SolidColorBrush(clr);
        }

        private void TextBox_MouseLeave_1(object sender, MouseEventArgs e)
        {
            Color clr = Color.FromRgb(240, 78, 37);
            //lock_btn.Background = new SolidColorBrush(clr);
        }

        bool isCloseEnabel = false;

        private void user_pass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                isCloseEnabel = true;
                //MessageBox.Show(real_usr_name + "::" + real_usr_pass);
                if ((this.real_usr_name == this.user_name.Text) && (this.real_usr_pass == this.user_pass.Password))
                {
                    /* lockSuccess lockPOP = new lockSuccess()
                     {
                         Owner = this,
                         ShowInTaskbar = false,
                         Topmost = true
                     };
                     lockPOP.ShowDialog(); 
                     isLocked = false;

                     Uri uri = new Uri("Images/lock/unlock-over_icon.jpg", UriKind.Relative);
                     var bitmap = new BitmapImage(uri);
                     lock_icon.Source = bitmap;*/
                    //parent_win.tmp.Close();
                    isCloseEnabel = true;
                    parent_win.Opacity = 1;
                    parent_win.setVisibleTxt(true);
                    parent_win.Effect = null;
                    this.Close();

                }
                else
                {


                    lockResult lockPOP = new lockResult()
                    {
                        Owner = this,
                        ShowInTaskbar = false,
                        Topmost = true
                    };                    
                    lockPOP.ShowDialog();
                    isLocked = true;
                    Uri uri = new Uri("Images/lock/unlock_icon.jpg", UriKind.Relative);
                    var bitmap = new BitmapImage(uri);
                    //lock_icon.Source = bitmap;
                }
            }
        }

        

        private void user_passOver_MouseEnter(object sender, MouseEventArgs e)
        {
            user_passOver.Visibility = System.Windows.Visibility.Hidden;
            user_pass.Visibility = System.Windows.Visibility.Visible;
            user_pass.Focus();
            user_pass.SelectAll();


        }

        private void user_passOver_MouseLeave(object sender, MouseEventArgs e)
        {
            /*if ((user_pass.Password == "Password") || (user_pass.Password == ""))
            {
                user_passOver.Visibility = System.Windows.Visibility.Visible;
                user_pass.Visibility = System.Windows.Visibility.Hidden;
                user_pass.Password = "Password";
            }*/
        }

        private void user_pass_MouseLeave(object sender, MouseEventArgs e)
        {
            if ((user_pass.Password == "Password") || (user_pass.Password == ""))
            {
                user_passOver.Visibility = System.Windows.Visibility.Visible;
                user_pass.Visibility = System.Windows.Visibility.Hidden;
                user_pass.Password = "Password";
            }
        }

        private void user_name_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            user_name.Text = "";
        }

        private void user_pass_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            user_pass.Password = "";
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            /*if ((e.Key == Key.Escape) )//&& ((e.Key == Key.LeftCtrl) || (e.Key == Key.RightCtrl)))
            {
                this.parent_win.exitChromProcess();
            }*/
        }

        private void user_name_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (user_name.Text == "User Email")
            {
                user_name.Text = "";
            }
        }

        private void user_name_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Right) || (e.Key == Key.Left) || (e.Key == Key.Up) || (e.Key == Key.Down))
            {
                if (user_name.Text == "User Email")
                {
                    user_name.Text = "";
                }
            }

        }
    }
}
