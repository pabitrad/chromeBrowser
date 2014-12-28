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
using System.Windows.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;


namespace chromeBrowser
{
    /// <summary>
    /// Interaction logic for firefoxLoading.xaml
    /// </summary>
    public partial class firefoxLoading : Window
    {
        public firefoxLoading()
        {
            InitializeComponent();
            double win_heights = System.Windows.SystemParameters.PrimaryScreenHeight ;
            double win_widths = System.Windows.SystemParameters.PrimaryScreenWidth;
            this.Activated += new EventHandler(login_Activated);
            this.Topmost = true ;
            this.Top = 0;
            this.Left = 0;
            this.Width = win_widths;
            this.Height = win_heights;
            //stack.Width = win_widths;
            pro_can.Width = 320;
            Canvas.SetTop(pro_can, (this.Height - 100) / 2 - 50);
            Canvas.SetLeft(pro_can, (this.Width - 320) / 2);

            real_progress.Width = 0;
            Canvas.SetTop(real_progress, (this.Height - 100) / 2 - 50);
            Canvas.SetLeft(real_progress, (this.Width - 320) / 2);

            Canvas.SetTop(est_bg, (this.Height - 112) / 2 - 20);
            Canvas.SetLeft(est_bg, (this.Width - 624) / 2);

            Canvas.SetLeft(loading_txt, (this.Width - 450) / 2);

            if(win_widths > 900)
            {
                Canvas.SetTop(GIFCtrl, (this.Height - 200) / 2);
                Canvas.SetLeft(GIFCtrl, (this.Width - 900) / 2);
                Canvas.SetTop(loading_txt, (this.Height - 200) / 2);
            }
            else
            {
                GIFCtrl.Width = 800;
                Canvas.SetTop(GIFCtrl, (this.Height - 200) / 2);
                Canvas.SetLeft(GIFCtrl, (this.Width - 800) / 2);
                Canvas.SetTop(loading_txt, (this.Height - 200) / 2);
            }

            
            //Canvas.SetTop(stack, (this.Height - 100) / 2 - 20);
            //Canvas.SetLeft(stack, (this.Width - 320) / 2);
            checkLoading();

        }

        /* 
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr parentHandle,
        IntPtr childAfter, string className, IntPtr windowTitle);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd,
            int msg, int wParam, StringBuilder ClassName);
        */
        



        private void login_Activated(object sender, EventArgs e)
        {
            //GIFCtrl.StartAnimate();
        }

        private MainWindow pare_win;

        public void setParenteWin(MainWindow pareWin)
        {
            pare_win = pareWin;
        }

        private DispatcherTimer checkProgressTimer = new DispatcherTimer();

        public void checkLoading()
        {
            checkProgressTimer.Interval = TimeSpan.FromMilliseconds(100);
            checkProgressTimer.Start();
            checkProgressTimer.Tick += new EventHandler(checkProgressTimer_Elapsed);
        }

        private int widthCount = 0;


        private void checkProgressTimer_Elapsed(Object sender, EventArgs e)
        {
            widthCount++;
            double rate = widthCount / 90.0;
            //if (widthCount % 90.0 == 0)
            {
                double xStep = -68 + 67 / 9 * widthCount;
                double yStep = 52 + 13 * Math.Cos((xStep - 68) / 65 * Math.PI);
                Canvas.SetLeft(hand, xStep);
                Canvas.SetTop(hand, yStep);

            }
            if (rate > 1) rate = 1;
            //this.hand.Visibility = System.Windows.Visibility.Visible;
            switch (widthCount)
            {
                case 9 :    
                    //checkProgressTimer.Stop();
                    //closeWindow();
                    //Canvas.SetLeft(hand, -2);
                    //Canvas.SetTop(hand, 53);
                   // first_foot.Visibility = System.Windows.Visibility.Visible;
                    break;
                case 18:                   
                    //Canvas.SetLeft(hand, 66);
                    //Canvas.SetTop(hand, 83);
                    //second_foot.Visibility = System.Windows.Visibility.Visible;
                    break;
                case 27 :
                    //Canvas.SetLeft(hand,131);
                    //Canvas.SetTop(hand, 54);
                   // third_foot.Visibility = System.Windows.Visibility.Visible;
                    break;
                case 36 :
                    //this.hand.Visibility = System.Windows.Visibility.Visible;
                   // Canvas.SetLeft(hand, 198);
                   // Canvas.SetTop(hand, 85);
                    //fourth_foot.Visibility = System.Windows.Visibility.Visible;
                    break;
                case 47 :
                    //Canvas.SetLeft(hand, 262);
                    //Canvas.SetTop(hand, 54);
                   // fifth_foot.Visibility = System.Windows.Visibility.Visible;
                    break;
                case 55 :
                    //this.hand.Visibility = System.Windows.Visibility.Visible;
                   // Canvas.SetLeft(hand, 319);
                    //Canvas.SetTop(hand, 85);
                    //sixth_foot.Visibility = System.Windows.Visibility.Visible;
                    break;
                case 64 :
                    //Canvas.SetLeft(hand, 383);
                    //Canvas.SetTop(hand, 54);
                   // seventh_foot.Visibility = System.Windows.Visibility.Visible;
                    break;
                case 72 :
                    //this.hand.Visibility = System.Windows.Visibility.Visible;
                    //Canvas.SetLeft(hand, 438);
                    //Canvas.SetTop(hand, 85);
                   // eightth_foot.Visibility = System.Windows.Visibility.Visible;
                    break;
                    
                case 73 :
                    //Canvas.SetLeft(hand, 507);
                    //Canvas.SetTop(hand, 54);
                    this.GIFCtrl.StopAnimate();
                   // nineth_foot.Visibility = System.Windows.Visibility.Visible;
                    break;
                case 85 :
                    //this.hand.Visibility = System.Windows.Visibility.Visible;
                    //Canvas.SetLeft(hand, 562);
                    //Canvas.SetTop(hand, 85);
                   
                    //tenth_foot.Visibility = System.Windows.Visibility.Visible;
                    break;
                    
            }

            if (widthCount > 72)
            {
                GIFCtrl.StopAnimate();
            }
            //real_progress.Width = 320 * rate;
            if (widthCount > 87)
            {
                Process[] localByName = Process.GetProcessesByName("firefox");
                if (localByName.Length > 0)
                {
                    var placement = GetPlacement(localByName[0].MainWindowHandle);
                    String fire_act_state = placement.showCmd.ToString();
                    if (fire_act_state != "Hide")
                    {
                        checkProgressTimer.Stop();
                        closeWindow();
                    }

                }
            }

           /* if (widthCount > 86)
            {
                checkProgressTimer.Stop();
                closeWindow();
            }*/
        }


        private static WINDOWPLACEMENT GetPlacement(IntPtr hwnd)
        {
            WINDOWPLACEMENT placement = new WINDOWPLACEMENT();
            placement.length = Marshal.SizeOf(placement);
            GetWindowPlacement(hwnd, ref placement);
            return placement;
        }

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetWindowPlacement(
            IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        internal struct WINDOWPLACEMENT
        {
            public int length;
            public int flags;
            public ShowWindowCommands showCmd;
            public System.Drawing.Point ptMinPosition;
            public System.Drawing.Point ptMaxPosition;
            public System.Drawing.Rectangle rcNormalPosition;
        }

        internal enum ShowWindowCommands : int
        {
            Hide = 0,
            Normal = 1,
            Minimized = 2,
            Maximized = 3,
        }

        
        public void startAnimation()
        {
            GIFCtrl.StartAnimate();
        }

        public void closeWindow()
        {
            while (true)
            {
                try
                {
                    this.Close();
                    pare_win.setFireSate();
                    
                }
                catch (Exception ex)
                {
                    this.Close();
                }
                break;
            }

        }
    }
}
