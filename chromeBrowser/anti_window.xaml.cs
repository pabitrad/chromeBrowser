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
using System.Diagnostics;

namespace chromeBrowser
{
    /// <summary>
    /// Interaction logic for anti_window.xaml
    /// </summary>
    public partial class anti_window : Window
    {
        public anti_window()
        {
            InitializeComponent();
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            this.Top = (screenHeight - 261) / 2;
            this.Left = (screenWidth - 599) / 2;
        }

        private void minClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void linkbtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                // true is the default, but it is important not to set it to false
                System.Diagnostics.Process.Start("https://wemagin.com/wdrive/software/wemagin_installerWithOutUsb.exe");
                // myProcess.StartInfo.UseShellExecute = true;
                //myProcess.StartInfo.FileName = "https://some.domain.tld/bla";
                //myProcess.Start();
                this.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine(e.Message);
            }
        }
    }
}
