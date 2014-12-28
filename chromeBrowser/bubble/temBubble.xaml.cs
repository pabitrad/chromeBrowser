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

namespace chromeBrowser.bubble
{
    /// <summary>
    /// Interaction logic for temBubble.xaml
    /// </summary>
    public partial class temBubble : Window
    {
        public temBubble()
        {
            InitializeComponent();
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            this.Left = screenWidth - 535;
            this.Top = 250;
        }

        private MainWindow parent_win;

        public void setParentWin(MainWindow m_win)
        {
            this.parent_win = m_win;

        }

        private void bubble_close_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if ((bool)isCamCls.IsChecked)
                    parent_win.setCameraBubble(true);
                this.Close();
            }
            catch (Exception ex)
            {
            }
        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            parent_win.closeAllBubble();
        }
    }
}
