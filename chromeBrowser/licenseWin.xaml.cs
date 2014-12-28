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
using System.Xml;
using System.IO;

namespace chromeBrowser
{
    /// <summary>
    /// Interaction logic for licenseWin.xaml
    /// </summary>
    public partial class licenseWin : Window
    {
        public licenseWin()
        {
            InitializeComponent();
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            this.Left = (screenWidth - this.Width) / 2;
            this.Top = (screenHeight - this.Height) / 2;
            yesBtn.AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(yesRtn_MouseLeftButtonDown), true);
            noBtn.AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(noBtn_MouseLeftButtonDown), true);
            
        }
        login parent_win;

        private void yesRtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.isAgree.IsChecked != true)
                return;
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

                    file_name = usbnamespace + "wemagin_v2\\agree.xml";

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
                    w.WriteStartElement("agree");
                    w.WriteElementString("accept", "1");
                    w.WriteEndElement();
                    w.Flush();
                    fs.Close();
                    login lg_win = new login();
                    lg_win.Show();
                }
                this.Close();
            }
            catch (Exception ex)
            {
                this.Close();
                ex.ToString();
            }
            this.Close();
        }

        private void noBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           
           /* try
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

                    file_name = usbnamespace + "wemagin_v2\\agree.xml";

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
                    w.WriteStartElement("agree");
                    w.WriteElementString("accept", "0");
                    w.WriteEndElement();
                    w.Flush();
                    fs.Close();
                }
                if (parent_win != null)
                    parent_win.Close();
                this.Close();
            }
            catch (Exception ex)
            {
                if (parent_win != null)
                    parent_win.Close();
                this.Close();
                this.Close();
                ex.ToString();
            } */
            if (parent_win != null)
                parent_win.Close();
         
            this.Close();
        }

        private void minClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (parent_win != null)
                parent_win.Close();

            this.Close();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isAgree.IsChecked = true;
        }
    }
}
