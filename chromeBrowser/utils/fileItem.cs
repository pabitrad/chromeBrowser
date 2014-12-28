using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input;
//using System.Drawing;
using System.Windows.Media.Imaging;
using System.Timers;
using System.Diagnostics;


namespace chromeBrowser
{
    class fileItem : Canvas
    {
        public fileItem()
        {

            initCanvas();
        }

        public EventHandler buttonCick;
        public EventHandler rightMenu;

        private Image file_icon;
        private Label file_name;
        private String file_path;

        private void initCanvas()
        {
            this.Width = 130;
            this.Height = 25;

            Color clr = Color.FromRgb(255, 255, 255);
            this.Background = new SolidColorBrush(clr);

            file_icon = new Image();
            file_icon.Width = 18;
            file_icon.Height = 18;
            this.Children.Add(file_icon);
            Canvas.SetLeft(file_icon, 1);
            Canvas.SetTop(file_icon, 3);

            file_name = new Label();

            file_name.Width = 102;
            file_name.Height = 25;
            file_name.Padding = new Thickness(2, 0, 0, 0);
            file_name.FontSize = 14;
            this.Children.Add(file_name);
            Canvas.SetLeft(file_name, 22);
            Canvas.SetTop(file_name, 0);
            this.MouseEnter += new MouseEventHandler(click_file);
            this.MouseLeave += new MouseEventHandler(leave_file);
            file_name.MouseDoubleClick += new MouseButtonEventHandler(excute_file);
          }




        public void set_select_item()
        {
            isSelect = true;
            file_name.Width = 102;
            this.Width = 130;
            Color clr = Color.FromRgb(150, 150, 150);
            this.Background = new SolidColorBrush(clr);

        }


        private bool isSelect = false;
        public void set_deselect_item()
        {
            isSelect = false;
            //Canvas.SetLeft(file_name, 22);
            file_icon.Visibility = System.Windows.Visibility.Visible;

            Color clr = Color.FromRgb(255, 255, 255);
            this.Background = new SolidColorBrush(clr);
            Canvas.SetLeft(file_icon, 1);
            //Canvas.SetTop(file_icon, 3);
            Canvas.SetLeft(file_name, 22);
            //Canvas.SetTop(file_name, 0);
        }


        private void click_file(object sender, MouseEventArgs e)
        {
            if (isSelect)
                return;
            //file_name.Width = Double.NaN;
            //this.Width = Double.NaN;

            //this.Height = 25;
            Color clr = Color.FromRgb(240, 240, 240);
            this.Background = new SolidColorBrush(clr);
            Canvas.SetLeft(file_name, 0);
            file_icon.Visibility = System.Windows.Visibility.Hidden;
            //file_name.Content.
            file_name.Width = 130;

            //this.Width = file_name.Width + 30;

        }

        private void leave_file(object sender, MouseEventArgs e)
        {
            if (isSelect)
                return;
            Canvas.SetLeft(file_name, 22);
            file_icon.Visibility = System.Windows.Visibility.Visible;
            Color clr = Color.FromRgb(255, 255, 255);
            this.Background = new SolidColorBrush(clr);
            file_name.Width = 102;
            this.Width = 130;
        }


        public void setFilePath(String filePath)
        {
            file_path = filePath;
        }

        public String getFIlePath()
        {
            return this.file_path;
        }

        private void right_menu(object sender, MouseButtonEventArgs e)
        {

            if (this.rightMenu != null)
                this.rightMenu(this, new EventArgs());
        }

        private void excute_file(object sender, MouseButtonEventArgs e)
        {

            if (this.buttonCick != null)
                this.buttonCick(this, new EventArgs());
        }

        public void changeIconImage(String url)
        {
            Uri uri = new Uri(url, UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            file_icon.Source = bitmap;
        }


        public void setFileIcon(String icon_type)
        {
            switch (icon_type)
            {
                case "default":
                    {
                        changeIconImage("Images/file/default.png");
                    }
                    break;
                case "word":
                    {
                        changeIconImage("Images/file/word.png");
                    }
                    break;

                case "excel":
                    {
                        changeIconImage("Images/file/excelicon.png");
                    }
                    break;
                case "text":
                    {
                        changeIconImage("Images/file/default.png");
                    }
                    break;
                case "pdf":
                    {
                        changeIconImage("Images/file/pdf.jpg");
                    }
                    break;
                case "image":
                    {
                        changeIconImage("Images/file/imagetype.gif");
                    }
                    break;
                case "moive":
                    {
                        changeIconImage("Images/file/video.gif");
                    }
                    break;
                case "music":
                    {
                        changeIconImage("Images/file/music.png");
                    }
                    break;
                case "misc":
                    {
                        changeIconImage("Images/file/edit.png");
                    }
                    break;

            }
        }


        public void setLabelContent(String label_txt)
        {
            file_name.Content = label_txt;
        }

    }
}
