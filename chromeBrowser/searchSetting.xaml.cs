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
using System.ComponentModel;
using System.IO;
using System.Xml;

namespace chromeBrowser
{
    /// <summary>
    /// Interaction logic for searchSetting.xaml
    /// </summary>
    public partial class searchSetting : Window
    {
        public MainWindow parent_win;
        public String default_site = "www.google.com";
        public Color clr;
        public int selected_num = 0;
        private BitmapImage sel_img;
        public searchSetting()
        {
            InitializeComponent();
            this.Left = 250;
            this.Top = 55;

            var uri = new Uri("Images/search/google-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            sel_img = bitmap;
            save.AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(save_MouseLeftButtonDown), true);
            //engin_txt.AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(this.engin_txt_MouseDown), true);

        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {


        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void Canvas_MouseMove_1(object sender, MouseEventArgs e)
        {
            /*  Point point = e.GetPosition(this);
              if ((point.X < 2) || (point.X > 695))
              {
                  this.Close();
              }

              if ((point.Y < 5) || (point.Y > 275))
              {
                  this.Close();
              }*/
        }

        private void close_button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void mini_button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void heart_MouseEnter(object sender, MouseEventArgs e)
        {
            // hidden_label.Visibility = System.Windows.Visibility.Visible;
        }

        private void heart_MouseLeave(object sender, MouseEventArgs e)
        {
            // hidden_label.Visibility = System.Windows.Visibility.Hidden;
        }

        private void google_MouseEnter(object sender, MouseEventArgs e)
        {

            this.google_label.Visibility = System.Windows.Visibility.Visible;

            var uri = new Uri("Images/search/google-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            google.Source = bitmap;

        }

        private void google_MouseLeave(object sender, MouseEventArgs e)
        {

            if (this.selected_num != 0)
            {
                var uri = new Uri("Images/search/google.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                google.Source = bitmap;
                this.google_label.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void yahoo_MouseEnter(object sender, MouseEventArgs e)
        {
            this.yahoo_label.Visibility = System.Windows.Visibility.Visible;

            var uri = new Uri("Images/search/yahoo-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            yahoo.Source = bitmap;
        }

        private void yahoo_MouseLeave(object sender, MouseEventArgs e)
        {


            if (this.selected_num != 1)
            {
                var uri = new Uri("Images/search/yahoo.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                yahoo.Source = bitmap;
                this.yahoo_label.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void bing_MouseLeave(object sender, MouseEventArgs e)
        {


            if (this.selected_num != 2)
            {
                var uri = new Uri("Images/search/bing.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                bing.Source = bitmap;
                this.bing_label.Visibility = System.Windows.Visibility.Hidden;
            }

        }

        private void bing_MouseEnter(object sender, MouseEventArgs e)
        {
            this.bing_label.Visibility = System.Windows.Visibility.Visible;
            var uri = new Uri("Images/search/bing-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            bing.Source = bitmap;
        }

        private void aol_MouseEnter(object sender, MouseEventArgs e)
        {
            this.aol_label.Visibility = System.Windows.Visibility.Visible;
            var uri = new Uri("Images/search/aol-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            aol.Source = bitmap;
        }

        private void aol_MouseLeave(object sender, MouseEventArgs e)
        {

            if (this.selected_num != 3)
            {
                var uri = new Uri("Images/search/aol.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                aol.Source = bitmap;
                this.aol_label.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void baidu_MouseEnter(object sender, MouseEventArgs e)
        {
            this.baidu_label.Visibility = System.Windows.Visibility.Visible;
            var uri = new Uri("Images/search/baidu-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            baidu.Source = bitmap;
        }

        private void baidu_MouseLeave(object sender, MouseEventArgs e)
        {


            if (this.selected_num != 4)
            {
                var uri = new Uri("Images/search/baidu.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                baidu.Source = bitmap;
                this.baidu_label.Visibility = System.Windows.Visibility.Hidden;
            }

        }

        private void yandex_MouseEnter(object sender, MouseEventArgs e)
        {
            this.yandex_label.Visibility = System.Windows.Visibility.Visible;

            var uri = new Uri("Images/search/yandex-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            yandex.Source = bitmap;
        }

        private void yandex_MouseLeave(object sender, MouseEventArgs e)
        {


            if (this.selected_num != 5)
            {
                var uri = new Uri("Images/search/yandex.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                yandex.Source = bitmap;
                this.yandex_label.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void naver_MouseEnter(object sender, MouseEventArgs e)
        {
            this.naver_label.Visibility = System.Windows.Visibility.Visible;

            var uri = new Uri("Images/search/naver-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            naver.Source = bitmap;
        }

        private void naver_MouseLeave(object sender, MouseEventArgs e)
        {


            if (this.selected_num != 6)
            {
                var uri = new Uri("Images/search/naver.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                naver.Source = bitmap;
                this.naver_label.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void save_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //parent_win.address_clr = clr;
            saveAllSearchSetting();
            parent_win.selected_search_num = this.selected_num;
            parent_win.add_text = default_site;
            parent_win.search_icon.Source = sel_img;
            parent_win.refeshBrowser();
            this.Close();
        }


        private void saveAllSearchSetting()
        {
            try
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

                    file_name = usbnamespace + "wemagin_v2\\search.xml";
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


                    //FileStream fs = System.IO.File.Create(file_name);
                    //FileStream fs = new FileStream(file_name, FileMode.Create);
                    XmlTextWriter w = new XmlTextWriter(fs, Encoding.UTF8);
                    w.WriteStartDocument();
                    w.WriteStartElement("Search");
                    w.WriteElementString("searchNum", this.selected_num.ToString());
                    w.WriteElementString("defaultSite", this.default_site);
                    w.WriteEndElement();
                    w.Flush();
                    fs.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }


        private void google_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            clr = Color.FromRgb(38, 73, 132);
            default_site = "www.google.com";
            //parent_win.address_clr = clr;
            engin_txt.Text = default_site;
            selected_num = 0;
            setInit();
            var uri = new Uri("Images/toolbar/google_s.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            sel_img = bitmap;
            google.Source = bitmap;
            google_label.Visibility = System.Windows.Visibility.Visible;
        }

        private void setInit()
        {
            var uri = new Uri("Images/search/google.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            google.Source = bitmap;
            this.google_label.Visibility = System.Windows.Visibility.Hidden;

            uri = new Uri("Images/search/yahoo.png", UriKind.Relative);
            bitmap = new BitmapImage(uri);
            yahoo.Source = bitmap;
            this.yahoo_label.Visibility = System.Windows.Visibility.Hidden;

            uri = new Uri("Images/search/bing.png", UriKind.Relative);
            bitmap = new BitmapImage(uri);
            bing.Source = bitmap;
            this.bing_label.Visibility = System.Windows.Visibility.Hidden;

            uri = new Uri("Images/search/aol.png", UriKind.Relative);
            bitmap = new BitmapImage(uri);
            aol.Source = bitmap;
            this.aol_label.Visibility = System.Windows.Visibility.Hidden;

            uri = new Uri("Images/search/baidu.png", UriKind.Relative);
            bitmap = new BitmapImage(uri);
            baidu.Source = bitmap;
            this.baidu_label.Visibility = System.Windows.Visibility.Hidden;

            uri = new Uri("Images/search/yandex.png", UriKind.Relative);
            bitmap = new BitmapImage(uri);
            yandex.Source = bitmap;
            this.yandex_label.Visibility = System.Windows.Visibility.Hidden;

            uri = new Uri("Images/search/naver.png", UriKind.Relative);
            bitmap = new BitmapImage(uri);
            naver.Source = bitmap;
            this.naver_label.Visibility = System.Windows.Visibility.Hidden;
        }

        private void yahoo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //#843d91           
            clr = Color.FromRgb(132, 45, 145);
            default_site = "www.yahoo.com";
            //parent_win.address_clr = clr;
            selected_num = 1;
             engin_txt.Text = default_site;
            setInit();
            var uri = new Uri("Images/search/yahoo-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            yahoo.Source = bitmap;
            yahoo_label.Visibility = System.Windows.Visibility.Visible;

            uri = new Uri("Images/toolbar/yahoo_s.png", UriKind.Relative);
            bitmap = new BitmapImage(uri);
            sel_img = bitmap;
        }

        private void bing_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //#345db1            
            clr = Color.FromRgb(52, 93, 177);
            default_site = "www.bing.com";
            engin_txt.Text = default_site;
            selected_num = 2;
            setInit();
            var uri = new Uri("Images/search/bing-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            bing.Source = bitmap;
            bing_label.Visibility = System.Windows.Visibility.Visible;

            uri = new Uri("Images/toolbar/bing_s.png", UriKind.Relative);
            bitmap = new BitmapImage(uri);
            sel_img = bitmap;

        }

        private void aol_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //#15b2db           
            clr = Color.FromRgb(21, 179, 219);
            default_site = "www.aol.com";
            engin_txt.Text = default_site;
            selected_num = 3;
            setInit();
            var uri = new Uri("Images/search/aol-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            aol.Source = bitmap;
            aol_label.Visibility = System.Windows.Visibility.Visible;

            uri = new Uri("Images/toolbar/rambler_s.png", UriKind.Relative);
            bitmap = new BitmapImage(uri);
            sel_img = bitmap;

        }

        private void baidu_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //304aa0            
            clr = Color.FromRgb(48, 74, 160);
            default_site = "www.baidu.com";
            engin_txt.Text = default_site;
            selected_num = 4;
            setInit();
            var uri = new Uri("Images/search/baidu-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            baidu.Source = bitmap;
            baidu_label.Visibility = System.Windows.Visibility.Visible;

            uri = new Uri("Images/toolbar/baidu_s.png", UriKind.Relative);
            bitmap = new BitmapImage(uri);
            sel_img = bitmap;
        }

        private void yandex_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //cd2126            
            clr = Color.FromRgb(205, 33, 38);
            default_site = "www.yandex.com";
            engin_txt.Text = default_site;
            selected_num = 5;
            setInit();
            var uri = new Uri("Images/search/yandex-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            yandex.Source = bitmap;
            yandex_label.Visibility = System.Windows.Visibility.Visible;

            uri = new Uri("Images/toolbar/yandex_s.png", UriKind.Relative);
            bitmap = new BitmapImage(uri);
            sel_img = bitmap;
        }

        private void naver_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //cd2126            
            clr = Color.FromRgb(205, 33, 38);
            default_site = "www.naver.com";
            engin_txt.Text = default_site;
            selected_num = 6;
            setInit();
            var uri = new Uri("Images/search/naver-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            naver.Source = bitmap;
            naver_label.Visibility = System.Windows.Visibility.Visible;

            uri = new Uri("Images/toolbar/naver_s.png", UriKind.Relative);
            bitmap = new BitmapImage(uri);
            sel_img = bitmap;
        }

        private void TextBox_MouseEnter_1(object sender, MouseEventArgs e)
        {
            Color clr = Color.FromRgb(240, 78, 37);
            //save.Background = new SolidColorBrush(clr);
        }

        private void TextBox_MouseLeave_1(object sender, MouseEventArgs e)
        {
            Color clr = Colors.Black;
            //save.Background = new SolidColorBrush(clr);
        }

        public void setDefaultNum(int index)
        {
            selected_num = index;
            Uri uri;
            BitmapImage bitmap;
            setInit();
            this.engin_txt.Text = this.default_site;
            switch (selected_num)
            {
                case 0:
                    uri = new Uri("Images/search/google-over.png", UriKind.Relative);
                    bitmap = new BitmapImage(uri);
                    google.Source = bitmap;
                    google_label.Visibility = System.Windows.Visibility.Visible;
                    break;
                case 1:
                    uri = new Uri("Images/search/yahoo-over.png", UriKind.Relative);
                    bitmap = new BitmapImage(uri);
                    yahoo.Source = bitmap;
                    yahoo_label.Visibility = System.Windows.Visibility.Visible;
                    break;
                case 2:
                    uri = new Uri("Images/search/bing-over.png", UriKind.Relative);
                    bitmap = new BitmapImage(uri);
                    bing.Source = bitmap;
                    bing_label.Visibility = System.Windows.Visibility.Visible;
                    break;
                case 3:
                    uri = new Uri("Images/search/aol-over.png", UriKind.Relative);
                    bitmap = new BitmapImage(uri);
                    aol.Source = bitmap;
                    aol_label.Visibility = System.Windows.Visibility.Visible;
                    break;
                case 4:
                    uri = new Uri("Images/search/baidu-over.png", UriKind.Relative);
                    bitmap = new BitmapImage(uri);
                    baidu.Source = bitmap;
                    baidu_label.Visibility = System.Windows.Visibility.Visible;
                    break;
                case 5:
                    uri = new Uri("Images/search/yandex-over.png", UriKind.Relative);
                    bitmap = new BitmapImage(uri);
                    yandex.Source = bitmap;
                    yandex_label.Visibility = System.Windows.Visibility.Visible;
                    break;
                case 6:
                    uri = new Uri("Images/search/naver-over.png", UriKind.Relative);
                    bitmap = new BitmapImage(uri);
                    naver.Source = bitmap;
                    naver_label.Visibility = System.Windows.Visibility.Visible;
                    break;
                case 7:
                    uri = new Uri("Images/search/google-over.png", UriKind.Relative);
                    bitmap = new BitmapImage(uri);
                    google.Source = bitmap;
                    google_label.Visibility = System.Windows.Visibility.Visible;
                    break;

            }
        }


        /**
         * search engine setting
         * 
         * ***/

        private void engin_txt_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            //clr = Color.FromRgb(205, 33, 38);

           /*
            default_site = "www.naver.com";
            engin_txt.Text = default_site;
            selected_num = 6;
            setInit();
            var uri = new Uri("Images/search/naver-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            naver.Source = bitmap;
            naver_label.Visibility = System.Windows.Visibility.Visible;

            uri = new Uri("Images/search/naver.png", UriKind.Relative);
            bitmap = new BitmapImage(uri);
            sel_img = bitmap;*/
        }

        private void engin_txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.setInit();
                if ((engin_txt.Text.IndexOf("google") >= 0) || (engin_txt.Text.IndexOf("GOOGLE") >= 0) || (engin_txt.Text.IndexOf("Google") >= 0))
                {
                    default_site = "www.google.com";
                    engin_txt.Text = default_site;
                    selected_num = 0;                    
                    var uri = new Uri("Images/search/google-over.png", UriKind.Relative);
                    var bitmap = new BitmapImage(uri);
                    //sel_img = bitmap;
                    google.Source = bitmap;
                    google_label.Visibility = System.Windows.Visibility.Visible;

                    uri = new Uri("Images/toolbar/google_w.png", UriKind.Relative);
                    bitmap = new BitmapImage(uri);
                    sel_img = bitmap;


                }
                else if ((engin_txt.Text.IndexOf("yahoo") >= 0) || (engin_txt.Text.IndexOf("YAHOO") >= 0) || (engin_txt.Text.IndexOf("Yahoo") >= 0))
                {
                    default_site = "www.yahoo.com";
                    //parent_win.address_clr = clr;
                    engin_txt.Text = default_site;
                    selected_num = 1;                    
                    var uri = new Uri("Images/search/yahoo-over.png", UriKind.Relative);
                    var bitmap = new BitmapImage(uri);
                    yahoo.Source = bitmap;
                    yahoo_label.Visibility = System.Windows.Visibility.Visible;

                    uri = new Uri("Images/toolbar/yahoo_w.png", UriKind.Relative);
                    bitmap = new BitmapImage(uri);
                    sel_img = bitmap;

                }
                else if ((engin_txt.Text.IndexOf("bing") >= 0) || (engin_txt.Text.IndexOf("BING") >= 0) || (engin_txt.Text.IndexOf("Bing") >= 0))
                {
                    //clr = Color.FromRgb(52, 93, 177);
                    default_site = "www.bing.com";
                    engin_txt.Text = default_site;
                    selected_num = 2;
                    //setInit();
                    var uri = new Uri("Images/search/bing-over.png", UriKind.Relative);
                    var bitmap = new BitmapImage(uri);
                    bing.Source = bitmap;
                    bing_label.Visibility = System.Windows.Visibility.Visible;

                    uri = new Uri("Images/toolbar/bing_w.png", UriKind.Relative);
                    bitmap = new BitmapImage(uri);
                    sel_img = bitmap;
                }
                else if ((engin_txt.Text.IndexOf("aol") >= 0) || (engin_txt.Text.IndexOf("Aol") >= 0) || (engin_txt.Text.IndexOf("Aol") >= 0))
                {
                    default_site = "www.aol.com";
                    engin_txt.Text = default_site;
                    selected_num = 3;
                    //setInit();
                    var uri = new Uri("Images/search/aol-over.png", UriKind.Relative);
                    var bitmap = new BitmapImage(uri);
                    aol.Source = bitmap;
                    aol_label.Visibility = System.Windows.Visibility.Visible;

                    uri = new Uri("Images/toolbar/rambler_w.png", UriKind.Relative);
                    bitmap = new BitmapImage(uri);
                    sel_img = bitmap;
                }
                else if ((engin_txt.Text.IndexOf("baidu") >= 0) || (engin_txt.Text.IndexOf("BAIDU") >= 0) || (engin_txt.Text.IndexOf("Baidu") >= 0))
                {
                    default_site = "www.baidu.com";
                    engin_txt.Text = default_site;
                    selected_num = 4;
                    //setInit();
                    var uri = new Uri("Images/search/baidu-over.png", UriKind.Relative);
                    var bitmap = new BitmapImage(uri);
                    baidu.Source = bitmap;
                    baidu_label.Visibility = System.Windows.Visibility.Visible;

                    uri = new Uri("Images/toolbar/baidu_w.png", UriKind.Relative);
                    bitmap = new BitmapImage(uri);
                    sel_img = bitmap;
                }
                else if ((engin_txt.Text.IndexOf("yandex") >= 0) || (engin_txt.Text.IndexOf("Yandex") >= 0) || (engin_txt.Text.IndexOf("YANDEX") >= 0))
                {
                    default_site = "www.yandex.com";
                    engin_txt.Text = default_site;
                    selected_num = 5;
                    //setInit();
                    var uri = new Uri("Images/search/yandex-over.png", UriKind.Relative);
                    var bitmap = new BitmapImage(uri);
                    yandex.Source = bitmap;
                    yandex_label.Visibility = System.Windows.Visibility.Visible;

                    uri = new Uri("Images/toolbar/yandex_w.png", UriKind.Relative);
                    bitmap = new BitmapImage(uri);
                    sel_img = bitmap;
                }
                else if ((engin_txt.Text.IndexOf("naver") >= 0) || (engin_txt.Text.IndexOf("NAVER") >= 0) || (engin_txt.Text.IndexOf("Naver") >= 0))
                {
                    default_site = "www.naver.com";
                    engin_txt.Text = default_site;
                    selected_num = 6;
                    //setInit();
                    var uri = new Uri("Images/search/naver-over.png", UriKind.Relative);
                    var bitmap = new BitmapImage(uri);
                    naver.Source = bitmap;
                    naver_label.Visibility = System.Windows.Visibility.Visible;

                    uri = new Uri("Images/toolbar/naver_w.png", UriKind.Relative);
                    bitmap = new BitmapImage(uri);
                    sel_img = bitmap;
                }
                else
                {

                    String user_enter_address = "";
                    if ((engin_txt.Text.Contains("http://")) || (engin_txt.Text.Contains("https://")))
                    {
                        
                        if (!engin_txt.Text.Contains("www."))
                        {
                            if (engin_txt.Text.Contains("http://"))
                                user_enter_address = "http://www." + engin_txt.Text.Substring(7);
                            else if (engin_txt.Text.Contains("https://"))
                                user_enter_address = "http://www." + engin_txt.Text.Substring(8);                            
                        }
                        else
                        {
                            user_enter_address = engin_txt.Text;
                        }
                    }
                    //webBrowser1.Navigate(addText.Text);
                    else
                    {
                        if (!engin_txt.Text.Contains("www."))
                        {

                            user_enter_address = engin_txt.Text; // "http://www." + addText.Text;
                            if (!user_enter_address.Contains("."))
                            {
                                if (!user_enter_address.Contains(".com") || !user_enter_address.Contains(".org"))
                                    user_enter_address = engin_txt.Text + ".com";
                            }
                            user_enter_address = "http://www." + user_enter_address;                            
                        }
                        else
                        {
                            user_enter_address = "http://" +engin_txt.Text;
                        }
                    }

                    default_site = user_enter_address;
                    engin_txt.Text = default_site;
                    parent_win.defaultSite = default_site;
                    selected_num = 7;
                    //var uri = new Uri("search.png", UriKind.RelativeOrAbsolute);
                    //var bitmap = new BitmapImage(uri);
                    //sel_img = bitmap;
                    //setInit();
                    var uri = new Uri(default_site + "/favicon.ico");
                    search_bitmap = new BitmapImage(uri);
                    search_bitmap.DownloadCompleted += new EventHandler(bitmap_DownloadCompleted);
                    //sel_img = bitmap;
                    //WebClient webClient = new WebClient();
                    //webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                    //webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(webClient_DownloadFileCompleted);          //(client_DownloadFileCompleted);
                    //webClient.DownloadFileAsync(uri, "search.png");                    
                }
            }
        }


        void webClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            //browserText.Text = "Completed";
            //this.Close();
            var length = new System.IO.FileInfo("search.png").Length;
            if (length == 0)
                return;
            var uri = new Uri("search.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            sel_img = bitmap;

        }

        private void engin_txt_MouseEnter(object sender, MouseEventArgs e)
        {           
            engin_txt.SelectAll();
            engin_txt.Focus();
        }

        private void engin_txt_MouseDown(object sender, MouseEventArgs e)
        {
            engin_txt.Text = "";
        }

        private void searchIcon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.setInit();
            if ((engin_txt.Text.IndexOf("google") >= 0) || (engin_txt.Text.IndexOf("GOOGLE") >= 0) || (engin_txt.Text.IndexOf("Google") >= 0))
            {
                default_site = "www.google.com";
                engin_txt.Text = default_site;
                selected_num = 0;
                var uri = new Uri("Images/search/google-over.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                //sel_img = bitmap;
                google.Source = bitmap;
                google_label.Visibility = System.Windows.Visibility.Visible;

                uri = new Uri("Images/toolbar/google_w.png", UriKind.Relative);
                bitmap = new BitmapImage(uri);
                sel_img = bitmap;


            }
            else if ((engin_txt.Text.IndexOf("yahoo") >= 0) || (engin_txt.Text.IndexOf("YAHOO") >= 0) || (engin_txt.Text.IndexOf("Yahoo") >= 0))
            {
                default_site = "www.yahoo.com";
                //parent_win.address_clr = clr;
                engin_txt.Text = default_site;
                selected_num = 1;
                var uri = new Uri("Images/search/yahoo-over.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                yahoo.Source = bitmap;
                yahoo_label.Visibility = System.Windows.Visibility.Visible;

                uri = new Uri("Images/toolbar/yahoo_w.png", UriKind.Relative);
                bitmap = new BitmapImage(uri);
                sel_img = bitmap;

            }
            else if ((engin_txt.Text.IndexOf("bing") >= 0) || (engin_txt.Text.IndexOf("BING") >= 0) || (engin_txt.Text.IndexOf("Bing") >= 0))
            {
                //clr = Color.FromRgb(52, 93, 177);
                default_site = "www.bing.com";
                engin_txt.Text = default_site;
                selected_num = 2;
                //setInit();
                var uri = new Uri("Images/search/bing-over.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                bing.Source = bitmap;
                bing_label.Visibility = System.Windows.Visibility.Visible;

                uri = new Uri("Images/toolbar/bing_w.png", UriKind.Relative);
                bitmap = new BitmapImage(uri);
                sel_img = bitmap;
            }
            else if ((engin_txt.Text.IndexOf("aol") >= 0) || (engin_txt.Text.IndexOf("Aol") >= 0) || (engin_txt.Text.IndexOf("Aol") >= 0))
            {
                default_site = "www.aol.com";
                engin_txt.Text = default_site;
                selected_num = 3;
                //setInit();
                var uri = new Uri("Images/search/aol-over.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                aol.Source = bitmap;
                aol_label.Visibility = System.Windows.Visibility.Visible;

                uri = new Uri("Images/toolbar/rambler_w.png", UriKind.Relative);
                bitmap = new BitmapImage(uri);
                sel_img = bitmap;
            }
            else if ((engin_txt.Text.IndexOf("baidu") >= 0) || (engin_txt.Text.IndexOf("BAIDU") >= 0) || (engin_txt.Text.IndexOf("Baidu") >= 0))
            {
                default_site = "www.baidu.com";
                engin_txt.Text = default_site;
                selected_num = 4;
                //setInit();
                var uri = new Uri("Images/search/baidu-over.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                baidu.Source = bitmap;
                baidu_label.Visibility = System.Windows.Visibility.Visible;

                uri = new Uri("Images/toolbar/baidu_w.png", UriKind.Relative);
                bitmap = new BitmapImage(uri);
                sel_img = bitmap;
            }
            else if ((engin_txt.Text.IndexOf("yandex") >= 0) || (engin_txt.Text.IndexOf("Yandex") >= 0) || (engin_txt.Text.IndexOf("YANDEX") >= 0))
            {
                default_site = "www.yandex.com";
                engin_txt.Text = default_site;
                selected_num = 5;
                //setInit();
                var uri = new Uri("Images/search/yandex-over.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                yandex.Source = bitmap;
                yandex_label.Visibility = System.Windows.Visibility.Visible;

                uri = new Uri("Images/toolbar/yandex_w.png", UriKind.Relative);
                bitmap = new BitmapImage(uri);
                sel_img = bitmap;
            }
            else if ((engin_txt.Text.IndexOf("naver") >= 0) || (engin_txt.Text.IndexOf("NAVER") >= 0) || (engin_txt.Text.IndexOf("Naver") >= 0))
            {
                default_site = "www.naver.com";
                engin_txt.Text = default_site;
                selected_num = 6;
                //setInit();
                var uri = new Uri("Images/search/naver-over.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                naver.Source = bitmap;
                naver_label.Visibility = System.Windows.Visibility.Visible;

                uri = new Uri("Images/toolbar/naver_w.png", UriKind.Relative);
                bitmap = new BitmapImage(uri);
                sel_img = bitmap;
            }
            else
            {

                String user_enter_address = "";
                if ((engin_txt.Text.Contains("http://")) || (engin_txt.Text.Contains("https://")))
                {

                    if (!engin_txt.Text.Contains("www."))
                    {
                        if (engin_txt.Text.Contains("http://"))
                            user_enter_address = "http://www." + engin_txt.Text.Substring(7);
                        else if (engin_txt.Text.Contains("https://"))
                            user_enter_address = "http://www." + engin_txt.Text.Substring(8);
                    }
                    else
                    {
                        user_enter_address = engin_txt.Text;
                    }
                }
                //webBrowser1.Navigate(addText.Text);
                else
                {
                   
                    if (!engin_txt.Text.Contains("www."))
                    {

                        user_enter_address = engin_txt.Text; // "http://www." + addText.Text;
                        if (!user_enter_address.Contains("."))
                        {
                            if (!user_enter_address.Contains(".com") || !user_enter_address.Contains(".org"))
                                user_enter_address = engin_txt.Text + ".com";
                        }
                        user_enter_address = "http://www." + user_enter_address;
                    }
                    else
                    {
                        user_enter_address = "http://"+engin_txt.Text;
                    }
                }

                default_site = user_enter_address;
                engin_txt.Text = default_site;
                parent_win.defaultSite = default_site;
                selected_num = 7;
                //var uri = new Uri("search.png", UriKind.RelativeOrAbsolute);
                //var bitmap = new BitmapImage(uri);
                //sel_img = bitmap;
                //setInit();
                var uri = new Uri(default_site + "/favicon.ico");
                search_bitmap = new BitmapImage(uri);
                //sel_img = search_bitmap;
                search_bitmap.DownloadCompleted += new EventHandler(bitmap_DownloadCompleted);
                //sel_img = bitmap;
                //WebClient webClient = new WebClient();
                //webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                //webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(webClient_DownloadFileCompleted);          //(client_DownloadFileCompleted);
                //webClient.DownloadFileAsync(uri, "search.png");                    
            }
        }


        private BitmapImage search_bitmap;

        void bitmap_DownloadCompleted(object sender, EventArgs e)
        {

            sel_img = search_bitmap;
        }


    }
}
