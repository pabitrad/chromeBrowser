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

namespace chromeBrowser
{
    /// <summary>
    /// Interaction logic for LockWindow.xaml
    /// </summary>
    public partial class settingLock : Window
    {

        private MainWindow parent_win;
        private bool isLocked = true;

        public settingLock()
        {
            InitializeComponent();
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            this.Height = screenHeight - 0;
            this.Width = screenWidth;
            this.Left = 0;
            this.Top = 0;
            lock_btn.AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(lock_btn_MouseLeftButtonDown), true);
            //this.Opacity = 0.7;
            user_name.Focus();
            user_name.SelectAll();
            setRoundCan(Width, Height);

        }
              

        private void mini_icon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void setRoundCan(double wids, double heights)
        {
            top_can.Width = wids;
            top_can.Height = (heights - 350) / 2;
            top_can.Opacity = 0.7;
            top_can.Effect = new BlurEffect();
            top_can.Margin = new Thickness(0, 0, 0, top_can.Height + 350);
            // Canvas.SetTop(top_can, 0);
            // Canvas.SetLeft(top_can, 0);

            left_can.Height = 350;
            left_can.Width = (wids - 400) / 2;
            left_can.Opacity = 0.7;
            left_can.Effect = new BlurEffect();
            left_can.Margin = new Thickness(0, top_can.Height, left_can.Width + 400, top_can.Height);

            //Canvas.SetTop(left_can, top_can.Height);
            // Canvas.SetLeft(left_can, 0);


            right_can.Height = 350;
            right_can.Width = (wids - 400) / 2 - 5;
            right_can.Opacity = 0.7;
            right_can.Effect = new BlurEffect();
            //right_can.Margin = new Thickness(left_can.Width + 400, top_can.Height, 0, 0);
            right_can.Margin = new Thickness(left_can.Width + 405, top_can.Height, 0, top_can.Height);
            //Canvas.SetTop(right_can, top_can.Height);
            //Canvas.SetRight(right_can, 0);

            bottom_can.Height = (heights - 350) / 2 - 5;
            bottom_can.Width = wids;
            bottom_can.Opacity = 0.7;
            bottom_can.Effect = new BlurEffect();
            bottom_can.Margin = new Thickness(0, top_can.Height + 355, 0, 0);
            //Canvas.SetBottom(bottom_can, 0);
            //Canvas.SetLeft(bottom_can, 0);


            center_can.Width = 400;
            center_can.Height = 350;
            //center_can.Margin = new Thickness(left_can.Width, top_can.Height, left_can.Width, top_can.Height);
            CanvasBorder.BorderThickness = new Thickness(1);
            //Canvas.SetTop(center_can, top_can.Height);
            //Canvas.SetLeft(center_can, left_can.Width);

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
                //parent_win.Opacity = 1;
                parent_win.Effect = new BlurEffect();
                SettingPage settingPOP = new SettingPage()
                {
                    Owner = parent_win,
                    ShowInTaskbar = false,
                    Topmost = true
                };
                settingPOP.setMain(this.parent_win);
                //settingPOP.getContactInfo();
                settingPOP.setUserInfo(real_usr_name);//, gmail_user);   
                settingPOP.Show();               
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
                lock_icon.Source = bitmap;
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
                user_name.Text = "User Name";
            }
        }

        private void user_pass_MouseEnter(object sender, MouseEventArgs e)
        {
            user_pass.Focus();
            user_pass.SelectAll();
        }

        private void user_name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (user_name.Text == "")
                {
                    user_name.Text = "User Name";
                }

                user_passOver.Visibility = System.Windows.Visibility.Hidden;
                user_pass.Visibility = System.Windows.Visibility.Visible;
                user_pass.Focus();
                user_pass.SelectAll();
            }
        }



        private void TextBox_MouseEnter_1(object sender, MouseEventArgs e)
        {
            Color clr = Color.FromRgb(122, 157, 147);
            lock_btn.Background = new SolidColorBrush(clr);
        }

        private void TextBox_MouseLeave_1(object sender, MouseEventArgs e)
        {
            Color clr = Color.FromRgb(240, 78, 37);
            lock_btn.Background = new SolidColorBrush(clr);
        }

        bool isCloseEnabel = false;

        private void user_pass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

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
                    //parent_win.Opacity = 1;
                    //parent_win.Effect = null;

                    parent_win.Effect = new BlurEffect();
                    SettingPage settingPOP = new SettingPage()
                    {
                        Owner = parent_win,
                        ShowInTaskbar = false,
                        Topmost = true
                    };
                    
                    settingPOP.setMain(this.parent_win);
                    //settingPOP.getContactInfo();
                    settingPOP.setUserInfo(real_usr_name);//, gmail_user);   
                    settingPOP.Show();
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
                    lock_icon.Source = bitmap;
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F4)
                return;
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.SystemKey == Key.LeftAlt || e.SystemKey == Key.RightAlt)
            {
                AltDown = true;
            }
            else if (e.SystemKey == Key.F4 && AltDown)
            {
                e.Handled = true;
            }
        }


        bool AltDown = false;

        private void Window_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.SystemKey == Key.LeftAlt || e.SystemKey == Key.RightAlt)
            {
                AltDown = false;
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
    }
}
