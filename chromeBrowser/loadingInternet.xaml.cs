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
using System.Threading;
using System.Windows.Threading;

namespace chromeBrowser
{
    /// <summary>
    /// Interaction logic for loadingInternet.xaml
    /// </summary>
    public partial class loadingInternet : Window
    {
        public loadingInternet()
        {
            InitializeComponent();
            this.Activated += new EventHandler(fileLoadProgress_Activated);
            this.Closed += new EventHandler(loadingInternet_Closed);
        }

        private DispatcherTimer checkProgressTimer = new DispatcherTimer();

        private void fileLoadProgress_Activated(Object sender, EventArgs e)
        {
            usbCheck uc = new usbCheck();
            //fc.copySet(SourceFilePath, DestFilePath);
            uc.setParent(this);
            usbcheckThread = new Thread(new ThreadStart(uc.Copy));
            usbcheckThread.Start();

            checkProgressTimer.Interval = TimeSpan.FromMilliseconds(100);
            checkProgressTimer.Start();
            checkProgressTimer.Tick += new EventHandler(checkProgressTimer_Elapsed);
        }

        private void loadingInternet_Closed(Object sender, EventArgs e)
        {
            if (checkProgressTimer != null)
            {
                checkProgressTimer.Stop();
                checkProgressTimer = null;
            }
            if (usbcheckThread != null)
            {
                usbcheckThread.Abort();
                usbcheckThread = null;
            }
        }

        private void checkProgressTimer_Elapsed(Object sender, EventArgs e)
        {
            try
            {
                switch (loadingPercentValue)
                {

                    case 1:
                        Parent.regBtn.IsEnabled = true;
                        Parent.forgotPass.Visibility = System.Windows.Visibility.Hidden;
                        Parent.loginBtn.IsEnabled = false;
                        Parent.resetBtn.Visibility = System.Windows.Visibility.Hidden;
                        Parent.regBtn.Visibility = System.Windows.Visibility.Visible;
                        checkProgressTimer.Stop();
                        Parent.hideloading();
                        this.Close();
                        break;
                    case 2:
                        Parent.regBtn.IsEnabled = false;
                        Parent.forgotPass.Visibility = System.Windows.Visibility.Visible;
                        Parent.loginBtn.IsEnabled = true;
                        Parent.setIsLoginState(true);
                        Parent.resetBtn.Visibility = System.Windows.Visibility.Visible;
                        Parent.regBtn.Visibility = System.Windows.Visibility.Hidden;
                        Parent.resetBtn.IsEnabled = true;
                        checkProgressTimer.Stop();
                        Parent.hideloading();
                        this.Close();
                        break;
                    case 3:
                        Parent.loginBtn.IsEnabled = false;
                        Parent.regBtn.IsEnabled = false;
                        checkProgressTimer.Stop();
                        Parent.hideloading();
                        this.Close();
                        break;
                    case 4:
                        //Parent.loginBtn.IsEnabled = false;
                        //Parent.regBtn.IsEnabled = false;
                        checkProgressTimer.Stop();
                        Parent.showNoInternet();
                        this.Close();
                        break;
                }
            }
            catch (Exception ex)
            {

            }
            
        }


        public void setosition(double top, double left)
        {
            this.Top = top;
            this.Left = left;
            //startCheckUSB();

        }

        Thread usbcheckThread;



        public int loadingPercentValue { get; set; }
        public void setloadingPercentValue(int val)
        {
            this.loadingPercentValue = val;

        }

        public void setparentItem()
        {

        }

        private login Parent;

        public void setParent(login lg)
        {
            Parent = lg;
        }
    }
}
