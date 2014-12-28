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

namespace chromeBrowser
{
    /// <summary>
    /// Interaction logic for passChangeSuccess.xaml
    /// </summary>
    public partial class passChangeSuccess : Window
    {
        public passChangeSuccess()
        {
            InitializeComponent();
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            this.Left = (screenWidth - this.Width) / 2;
            this.Top = (screenHeight - this.Height) / 2;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                try
                {
                    var newWindow = new login();
                    newWindow.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    this.Close();
                    ex.ToString();
                }
            }
        }

        private void retBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var newWindow = new login();
                newWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                this.Close();
                ex.ToString();
            }
        }
    }
}
