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
using System.IO;

namespace chromeBrowser.usb
{
    /// <summary>
    /// Interaction logic for deleteMenu.xaml
    /// </summary>
    public partial class deleteMenu : Window
    {
        
        public deleteMenu()
        {
            InitializeComponent();
            delBtn.AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(TextBox_MouseDown), true);
        }

        private string filePath;
        private UsbPopUp usbWin;

        public void setPosition( double xPos, double yPos)
        {
            this.Top = yPos;
            this.Left = xPos+10;

        }

        public void setParent(UsbPopUp usWin)
        {
            usbWin = usWin;
        }


        private int fileType;
        public void setFileType(int fileTp)
        {
            fileType = fileTp ;
        }

        private void TextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {

                File.Delete(filePath);
                switch (fileType)
                {
                    case 0:
                        this.usbWin.deleteByRightMouse();
                        break;
                    case 1:
                        this.usbWin.deleteByRightMouse();
                        break;
                    case 2:

                        this.usbWin.deleteMusicByRightMouse();
                        break;
                    case 3:
                        this.usbWin.deleteByRightMouse();
                        break;
                    case 4:
                        this.usbWin.deleteByRightMouse();
                        break;
                }

                this.Close();
            }
            catch (Exception ex)
            {
                ex.ToString();
                return;
            }
            //this.Close();
        }

        public void setFilePath(string filePth)
        {
            filePath = filePth;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
           
            
        }


    }
}
