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

namespace chromeBrowser
{
    /// <summary>
    /// Interaction logic for lockResult.xaml
    /// </summary>
    public partial class lockResult : Window
    {
        public lockResult()
        {
            InitializeComponent();
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            this.Left = (screenWidth - this.Width) / 2;
            this.Top = (screenHeight - this.Height) / 2;
            // retBtn.MouseLeftButtonDown += new MouseButtonEventHandler(returnParent);
            retBtn.AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(returnParent), true);

        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {

            this.Close();
        }

        private void returnParent(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                this.Close();
        }
    }
}
