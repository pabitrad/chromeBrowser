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
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

using CatenaLogic.Windows.Presentation.WebcamPlayer;
using System.Collections.ObjectModel;
using System.Xml;
using System.IO;
using System.Diagnostics;

namespace chromeBrowser
{
    /// <summary>
    /// Interaction logic for toolbar.xaml
    /// </summary>
    public partial class toolbar : Window
    {
        public toolbar()
        {
            InitializeComponent();
            double win_widths = System.Windows.SystemParameters.PrimaryScreenWidth;
            this.Left = win_widths - 235;
            this.Top = 56;
            //setColorPallet();
        }

        public MainWindow parent_win;
        private string real_usr_name, real_usr_pass;
        public void setInfo(string u_id , string u_pass)
        {
            real_usr_name = u_id;
            real_usr_pass = u_pass;
        }

        public void setMain(MainWindow main_win)
        {
            parent_win = main_win;
           
        }

        private void lock_icon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            parent_win.setVisibleTxt(false);
            parent_win.Opacity = 0.3;
            //this.Effect = new BlurEffect();
            LockWindow lockPOP = new LockWindow()
            {
                Owner = parent_win,
                ShowInTaskbar = false,
                Topmost = true
            };
           
            lockPOP.setUserNameAndPass(real_usr_name, real_usr_pass);
            lockPOP.setMain(parent_win);
            lockPOP.ShowDialog();
            this.Close();
        }

        private void Canvas_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!isOut)
            this.Close();
        }

        private void cloud_icon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            parent_win.navWebbrowser("http://www.wcloudbackup.com/");
            this.Close();
        }

        

        private void cam_icon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            parent_win.camblockSetting(true);
        }

        private void pallet_red_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            parent_win.setColor(223, 43, 42);
        }

        private void pallet_white_MouseEnter(object sender, MouseButtonEventArgs e)
        {
            parent_win.setColor( 255, 255, 255);
        }

        private void pallet_blue_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            parent_win.setColor(41, 165, 223);
        }

        private void pallet_yellow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            parent_win.setColor(237, 159, 32);
        }

        private void pallet_gray_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            parent_win.setColor(4, 4, 4);
        }

        private void pallet_pink_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            parent_win.setColor(219, 31, 199);
        }

        private void person_icon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SettingPage setWin = new SettingPage()
            {
                Owner = parent_win,
                ShowInTaskbar = false,
                Topmost = true
            };

            setWin.setMain(parent_win);
            setWin.setUserInfo();
            setWin.ShowDialog();
            this.Close();
        }

        private void vpn_icon_MouseEnter(object sender, MouseEventArgs e)
        {
            Uri uris = new Uri("Images/setting/vpn-over.png", UriKind.Relative);
            var bitmaps = new BitmapImage(uris);
            vpn_icon.Source = bitmaps;
            parent_win.showVpnTutorial();
        }

        private void vpn_icon_MouseLeave(object sender, MouseEventArgs e)
        {
            Uri uris = new Uri("Images/setting/vpn.png", UriKind.Relative);
            var bitmaps = new BitmapImage(uris);
            vpn_icon.Source = bitmaps;

            Point pos = e.GetPosition(vpn_icon);
            if (pos.Y < 0 || pos.Y > 20 || pos.X > 55)
                parent_win.closeAllBubble();
        }

        private void vpn_icon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Uri uris = new Uri("Images/setting/vpn-down.png", UriKind.Relative);
            var bitmaps = new BitmapImage(uris);
            vpn_icon.Source = bitmaps;
        }


        public Process vpn_chromeProcess = new Process();
        



        private void vpn_icon_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Uri uris = new Uri("Images/setting/vpn-over.png", UriKind.Relative);
            var bitmaps = new BitmapImage(uris);
            vpn_icon.Source = bitmaps;
            parent_win.openVpn();

        }

       

        private void cam_icon_MouseEnter(object sender, MouseEventArgs e)
        {
            Uri uris = new Uri("Images/setting/video-over.png", UriKind.Relative);
            var bitmaps = new BitmapImage(uris);
            cam_icon.Source = bitmaps;

            parent_win.showCamTutorial();

        }

        private void cam_icon_MouseLeave(object sender, MouseEventArgs e)
        {
            if (parent_win.isCam)
            {
                Uri uris = new Uri("Images/setting/video.png", UriKind.Relative);
                var bitmaps = new BitmapImage(uris);
                cam_icon.Source = bitmaps;
            }
            else
            {
                Uri uris = new Uri("Images/setting/video-over.png", UriKind.Relative);
                var bitmaps = new BitmapImage(uris);
                cam_icon.Source = bitmaps;
            }

            Point pos = e.GetPosition(cam_icon);
            if (pos.Y < 0 || pos.Y > 24 || pos.X > 55)
                parent_win.closeAllBubble();
        }

        public void setCamIcon()
        {
            if (parent_win.isCam)
            {
                Uri uris = new Uri("Images/setting/video.png", UriKind.Relative);
                var bitmaps = new BitmapImage(uris);
                cam_icon.Source = bitmaps;
            }
            else
            {
                Uri uris = new Uri("Images/setting/video-over.png", UriKind.Relative);
                var bitmaps = new BitmapImage(uris);
                cam_icon.Source = bitmaps;
            }
        }

        private void saveColor(int val)
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

                    file_name = usbnamespace + "wemagin_v2\\clr.xml";

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
                    w.WriteStartElement("Color");
                    w.WriteElementString("col", val.ToString());
                    w.WriteEndElement();
                    w.Flush();
                    fs.Close();
                    //login lg_win = new login();
                    //lg_win.Show();
                }
                this.Close();
            }
            catch (Exception ex)
            {
                this.Close();
                ex.ToString();
            }
            //this.Close();
        }


        public void setColorPallet()
        {
            Uri uris;
            BitmapImage bitmaps;
            switch (parent_win.getColor())
            {
                case 0 :
                    uris = new Uri("Images/setting/white_color_pallet.png", UriKind.Relative);
                    bitmaps = new BitmapImage(uris);
                    color_pallet.Source = bitmaps;                    
                    break;
                case 1:
                    uris = new Uri("Images/setting/red_color_pallet.png", UriKind.Relative);
                    bitmaps = new BitmapImage(uris);
                    color_pallet.Source = bitmaps;  
                    break;
                case 2:
                    uris = new Uri("Images/setting/blue_color_pallet.png", UriKind.Relative);
                    bitmaps = new BitmapImage(uris);
                    color_pallet.Source = bitmaps;  
                    break;
                case 3:
                    uris = new Uri("Images/setting/yellow_color_pallet.png", UriKind.Relative);
                    bitmaps = new BitmapImage(uris);
                    color_pallet.Source = bitmaps;  
                    break;
                    
                case 4:
                    uris = new Uri("Images/setting/pink_color_pallet.png", UriKind.Relative);
                    bitmaps = new BitmapImage(uris);
                    color_pallet.Source = bitmaps;  
                    break;                    
                case 5:
                    uris = new Uri("Images/setting/black_color_pallet.png", UriKind.Relative);
                    bitmaps = new BitmapImage(uris);
                    color_pallet.Source = bitmaps;  
                    break;
            }

            
        }

        private void color_pallet_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            //movingCan.Visibility = System.Windows.Visibility.Visible;
            //Canvas.SetLeft(movingCan, 200);
           //System.Threading.Thread.Sleep(100);
            for (int i = 0; i < 61; i++)
            {
               // System.Threading.Thread.Sleep(50);
               // Canvas.SetLeft(movingCan, 120 - i * 2);
            }
            DoDynamicAnimation();
            //MoveTo(movingCan, 1 , 1);

        }


        private bool isOut = false;


        public void DoDynamicAnimationPare()
        {
            return;
            for (int i = 0; i < 1; ++i)
            {
                /*var e = new Button { Width = 16, Height = 16 }; //new Ellipse { Width = 16, Height = 16, Fill = SystemColors.HighlightBrush };
                Canvas.SetLeft(e, Mouse.GetPosition(this).X);
                Canvas.SetTop(e, Mouse.GetPosition(this).Y);*/

                var tg = new TransformGroup();
                var translation = new TranslateTransform(0, 50);
                var translationName = "myTranslation" + translation.GetHashCode();
                RegisterName(translationName, translation);
                tg.Children.Add(translation);
                tg.Children.Add(new RotateTransform(90.0));
                overCan.RenderTransform = tg;

                //overCan.Children.Add(movingCan);

                var anim = new DoubleAnimation(0, 100, new Duration(new TimeSpan(0, 0, 0, 1, 0)))
                {
                    EasingFunction = new PowerEase { EasingMode = EasingMode.EaseOut }
                };

                var s = new Storyboard();
                Storyboard.SetTargetName(s, translationName);
                Storyboard.SetTargetProperty(s, new PropertyPath(TranslateTransform.XProperty));
                var storyboardName = "s" + s.GetHashCode();
                Resources.Add(storyboardName, s);

                s.Children.Add(anim);

                /*s.Completed +=
                    (sndr, evtArgs) =>
                    {
                        overCan.Children.Remove(e);
                        Resources.Remove(storyboardName);
                        UnregisterName(translationName);
                    };*/
                s.Begin();
            }
        }


        void DoDynamicAnimation()
        {
            for (int i = 0; i < 1; ++i)
            {
                /*var e = new Button { Width = 16, Height = 16 }; //new Ellipse { Width = 16, Height = 16, Fill = SystemColors.HighlightBrush };
                Canvas.SetLeft(e, Mouse.GetPosition(this).X);
                Canvas.SetTop(e, Mouse.GetPosition(this).Y);*/

                var tg = new TransformGroup();
                var translation = new TranslateTransform(30, 0);
                var translationName = "myTranslation" + translation.GetHashCode();
                RegisterName(translationName, translation);
                tg.Children.Add(translation);
                tg.Children.Add(new RotateTransform(0));
                movingCan.RenderTransform = tg;

                //overCan.Children.Add(movingCan);

                var anim = new DoubleAnimation(0, -101, new Duration(new TimeSpan(0, 0, 0, 1, 0)))
                {
                    EasingFunction = new PowerEase { EasingMode = EasingMode.EaseOut }
                };

                var s = new Storyboard();
                Storyboard.SetTargetName(s, translationName);
                Storyboard.SetTargetProperty(s, new PropertyPath(TranslateTransform.XProperty));
                var storyboardName = "s" + s.GetHashCode();
                Resources.Add(storyboardName, s);

                s.Children.Add(anim);

                /*s.Completed +=
                    (sndr, evtArgs) =>
                    {
                        overCan.Children.Remove(e);
                        Resources.Remove(storyboardName);
                        UnregisterName(translationName);
                    };*/
                s.Begin();
            }
        }


        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            
            Point mov_pos = e.GetPosition(this);
            if (mov_pos.Y > 205)
                isOut = true;
            else
               isOut = false;
            
        
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
                movingCan.RenderTransform = tg;

                //overCan.Children.Add(movingCan);

                var anim = new DoubleAnimation(-101, 0, new Duration(new TimeSpan(0, 0, 0, 1, 0)))
                {
                    EasingFunction = new PowerEase { EasingMode = EasingMode.EaseOut }
                };

                var s = new Storyboard();
                Storyboard.SetTargetName(s, translationName);
                Storyboard.SetTargetProperty(s, new PropertyPath(TranslateTransform.XProperty));
                var storyboardName = "s" + s.GetHashCode();
                Resources.Add(storyboardName, s);

                s.Children.Add(anim);

                s.Completed +=
                    (sndr, evtArgs) =>
                    {
                        /*overCan.Children.Remove(e);
                        Resources.Remove(storyboardName);
                        UnregisterName(translationName);*/
                        saveColor(parent_win.getColor());
                    };
                s.Begin();
            }
        }

        private void white_clr_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            parent_win.setColor(255, 255, 255);
            parent_win.setColorIndex(0);
            setColorPallet();
            DoDynamicAnimationBack();
        }

        private void red_clr_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            parent_win.setColor(223, 43, 42);
            //parent_win.setColor(255, 255, 255);
            parent_win.setColorIndex(1);
            setColorPallet();
            DoDynamicAnimationBack();
        }

        private void blue_clr_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            parent_win.setColor(41, 165, 223);
            parent_win.setColorIndex(2);
            setColorPallet();
            DoDynamicAnimationBack();
        }

        private void yellow_clr_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {           
            
            parent_win.setColor(237, 159, 32);
            parent_win.setColorIndex(3);
            setColorPallet();
            DoDynamicAnimationBack();
        }

        private void pink_clr_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            parent_win.setColor(219, 31, 199);
            parent_win.setColorIndex(4);
            setColorPallet();
            DoDynamicAnimationBack();
        }

        private void block_clr_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            parent_win.setColor(4, 4, 4);
            parent_win.setColorIndex(5);
            setColorPallet();
            DoDynamicAnimationBack();
        }

        private void alert_icon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            hackAlertSetting hackPop = new hackAlertSetting()
            {
                Owner = parent_win,
                ShowInTaskbar = false,
                Topmost = true
            };

            //hackPop.setUserNameAndPass(real_usr_name, real_usr_pass);
            hackPop.setMain(parent_win);
            hackPop.getHackStateFromServer();
            hackPop.ShowDialog();
            this.Close();
        }

        private void color_pallet_MouseEnter(object sender, MouseEventArgs e)
        {
            parent_win.showClrTutorial();
        }

        private void lock_icon_MouseEnter(object sender, MouseEventArgs e)
        {
            parent_win.showLockBubble();
        }

        private void cloud_icon_MouseEnter(object sender, MouseEventArgs e)
        {
            parent_win.showCloudBubble();
        }

        private void lock_icon_MouseLeave(object sender, MouseEventArgs e)
        {
            Point pos = e.GetPosition(lock_icon);
            if (pos.Y < 0 || pos.Y > 29 || pos.X > 55)
                parent_win.closeAllBubble();
        }

        private void cloud_icon_MouseLeave(object sender, MouseEventArgs e)
        {
            Point pos = e.GetPosition(cloud_icon);
            if (pos.Y < 0 || pos.Y > 26 || pos.X > 55)
                parent_win.closeAllBubble();
        }

        private void color_pallet_MouseLeave(object sender, MouseEventArgs e)
        {
            Point pos = e.GetPosition(color_pallet);
            if (pos.Y < 0 || pos.Y > 25 || pos.X > 25)
                parent_win.closeAllBubble();
        }

        private void DoubleAnimation_Completed(object sender, EventArgs e)
        {
            parent_win.Topmost = false;
        }



    }
}
