using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input;
//using System.Drawing;
using System.Windows.Media.Imaging;
using System.Timers;
using System.IO;

namespace chromeBrowser
{
    class customCanvas : Canvas
    {

        //private List<WebContainer> webList = new List<WebContainer>();
        //Timer timer = new Timer();
        //int timerEnd = 0;


        public int canIndex = 0;
        public int xPos = 0;
        public int yPos = 0;
        public EventHandler buttonCick;
        public Button buttonchild = new Button();

        public Image buttonImage = new Image();
        public Image iconsImage = new Image();
        public Label tab_url = new Label();
        public BitmapImage bitmap;
        private bool isActive = true;

        public customCanvas()
        {
            // Canvas();
            initCanvas();
            // buttonCick = new EventHandler(); 
        }




        private void initCanvas()
        {
            // int argb = Int32.Parse(ccode.Replace("#", ""), NumberStyles.HexNumber);
            this.Width = 130;
            this.Height = 31;
            System.Windows.Media.Color clr = System.Windows.Media.Color.FromRgb(0, 0, 0);
            this.Background = new SolidColorBrush(clr);
            // Button childButton = new Button();
            /*buttonchild.Width = 20;
            buttonchild.Height = 20;
            this.Children.Add(buttonchild);
            Canvas.SetLeft(buttonchild, 10);
            Canvas.SetTop(buttonchild, 10);
            buttonchild.AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(button_MouseDown), true);*/
            createIconImage();
            iconsImage.Width = 15;
            iconsImage.Height = 15;
            this.Children.Add(iconsImage);
            Canvas.SetLeft(iconsImage, 10);
            Canvas.SetTop(iconsImage, 10);

            createURL();
            tab_url.Width = 70;
            tab_url.Height = 20;
            this.Children.Add(tab_url);
            Canvas.SetLeft(tab_url, 30);
            Canvas.SetTop(tab_url, 7);

            createImageButton();
            buttonImage.Width = 30;
            buttonImage.Height = 30;
            this.Children.Add(buttonImage);
            Canvas.SetRight(buttonImage, 0);
            Canvas.SetTop(buttonImage, 0);
            buttonImage.AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(button_MouseDown), true);

        }

        private void createURL()
        {
            Color clr = Color.FromRgb(0, 0, 0);

            tab_url.Background = new SolidColorBrush(clr);
            Color forge_clr = Color.FromRgb(255, 255, 255);
            tab_url.Foreground = new SolidColorBrush(forge_clr);
            tab_url.Content = "New Tab";
            tab_url.Padding = new Thickness(0);
            tab_url.FontSize = 14;
            tab_url.FontWeight = FontWeights.Bold;

        }

        private void createIconImage()
        {

            //Uri uri = new Uri("http://www.google.com/favicon.ico");
            //var bitmap = new BitmapImage(uri);
            //iconsImage.Source = bitmap; 
        }



        private void objImage_DownloadCompleted(object sender, EventArgs e)
        {
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
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

                iName = usbnamespace + "wemagin_v2\\test.png";


                try
                {
                    encoder.Frames.Add(BitmapFrame.Create((BitmapImage)sender));

                    using (var filestream = new FileStream(iName, FileMode.Create))
                        encoder.Save(filestream);
                }
                catch
                {
                    //...

                }
            }


        }


        public void changeIconImage(String url)
        {
            Uri uri = new Uri(url);
            bitmap = new BitmapImage(uri);
            iconsImage.Source = bitmap;
            //bitmap.DownloadCompleted += objImage_DownloadCompleted;




        }


        private void createImageButton()
        {
            buttonImage.Width = 10;
            buttonImage.Height = 10;


            // StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Image/ Tulips.jpg"));


            Uri uri = new Uri("Images/tab/big_close.png", UriKind.Relative);

            // , UriKind.Relative);
            var bitmap = new BitmapImage(uri);

            buttonImage.Source = bitmap;
            buttonImage.MouseEnter += new MouseEventHandler(changeImage);
            buttonImage.MouseLeave += new MouseEventHandler(changeLeaveImage);

        }


        private void changeImage(object sender, MouseEventArgs e)
        {
            if (isActive)
            {
                var uri = new Uri("Images/tab/close_over-over.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                buttonImage.Source = bitmap;
            }
            else
            {
                var uri = new Uri("Images/tab/big_close-over.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                buttonImage.Source = bitmap;
            }
            //Canvas.SetZIndex(buttonImage, 1000);
            // buttonImage.MouseEnter += new MouseEventHandler(changeImage);
        }



        private void changeLeaveImage(object sender, MouseEventArgs e)
        {

            if (isActive)
            {
                var uri = new Uri("Images/tab/big_close.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                buttonImage.Source = bitmap;
            }
            else
            {
                var uri = new Uri("Images/tab/small_x_close.png", UriKind.Relative);
                var bitmap = new BitmapImage(uri);
                buttonImage.Source = bitmap;
            }
            // Canvas.SetZIndex(buttonImage, 1000);


            // buttonImage.MouseEnter += new MouseEventHandler(changeImage);
        }


        private void button_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.buttonCick != null)
                this.buttonCick(this, new EventArgs());
        }


        public Button retChildeButton()
        {
            return buttonchild;
        }

        public void makeResize()
        {
            // int argb = Int32.Parse(ccode.Replace("#", ""), NumberStyles.HexNumber);
            this.Width = 130;
            this.Height = 20;
            System.Windows.Media.Color clr = System.Windows.Media.Color.FromRgb(50, 50, 50);
            this.Background = new SolidColorBrush(clr);
            tab_url.Background = new SolidColorBrush(clr);
            Canvas.SetLeft(iconsImage, 3);
            Canvas.SetTop(iconsImage, 3);

            isActive = false;

            tab_url.Height = 15;
            Canvas.SetLeft(tab_url, 30);
            Canvas.SetTop(tab_url, 3);


            var uri = new Uri("Images/tab/small_x_close.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            buttonImage.Source = bitmap;



            buttonImage.Width = 20;
            buttonImage.Height = 20;
            Canvas.SetRight(buttonImage, 0);
            Canvas.SetTop(buttonImage, 0);

            //buttonImage.MouseEnter += new MouseEventHandler(changeImage);
            //buttonImage.MouseLeave += new MouseEventHandler(changeLeaveImage);
            //Canvas.SetZIndex(buttonImage, 1000);
            tab_url.FontSize = 10;
            tab_url.FontWeight = FontWeights.Light;
        }


        public void makeToOrgsize()
        {
            // int argb = Int32.Parse(ccode.Replace("#", ""), NumberStyles.HexNumber);
            isActive = true;
            this.Width = 130;
            this.Height = 31;
            System.Windows.Media.Color clr = System.Windows.Media.Color.FromRgb(0, 0, 0);
            this.Background = new SolidColorBrush(clr);
            tab_url.Background = new SolidColorBrush(clr);
            Canvas.SetLeft(iconsImage, 10);
            Canvas.SetTop(iconsImage, 10);



            tab_url.Height = 20;
            Canvas.SetLeft(tab_url, 30);
            Canvas.SetTop(tab_url, 7);

            var uri = new Uri("Images/tab/big_close.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            buttonImage.Source = bitmap;
            buttonImage.Width = 30;
            buttonImage.Height = 30;
            Canvas.SetRight(buttonImage, 0);
            Canvas.SetTop(buttonImage, 0);
            // buttonImage.MouseEnter += new MouseEventHandler(changeImage);
            // buttonImage.MouseLeave += new MouseEventHandler(changeLeaveImage);

            tab_url.FontSize = 14;
            tab_url.FontWeight = FontWeights.Bold;
        }
    }
}
