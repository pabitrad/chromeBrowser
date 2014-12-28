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
    /// Interaction logic for noInternet.xaml
    /// </summary>
    public partial class noInternet : Window
    {
        public noInternet()
        {
            InitializeComponent();
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            this.Top = (screenHeight - 240) / 2;
            this.Left = (screenWidth - 600) / 2;
        }

        private void minClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            login lg_win = new login();
            lg_win.Show();
            this.Close();
        }
    }
}
