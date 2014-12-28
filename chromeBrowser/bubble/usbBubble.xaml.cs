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
    /// Interaction logic for usbBubble.xaml
    /// </summary>
    public partial class usbBubble : Window
    {
        private double searchboxPos = 0;
        private MainWindow parent_win;

        public void setParentWin(MainWindow m_win)
        {
            this.parent_win = m_win;
        }


        public usbBubble()
        {
            InitializeComponent();
            this.Left = 15;
            this.Top = 54;
            //this.Deactivated += new EventHandler(searchBubble_Deactivated);
        }

        public void setXPos(double val)
        {
            searchboxPos = val + 30;
            this.Left = val + 25 ;
        }

        private void searchBubble_Deactivated(Object sender, EventArgs ex)
        {
            try
            {
                this.Close();
            }
            catch (Exception e)
            {
            }
        }

        private void bubble_close_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if ((bool)isCamCls.IsChecked)
                    parent_win.setUsbBubble(true);
                //parent_win.setUsbBubble(true);
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
