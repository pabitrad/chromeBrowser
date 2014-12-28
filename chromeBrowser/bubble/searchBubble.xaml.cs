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
    /// Interaction logic for searchBubble.xaml
    /// </summary>
    public partial class searchBubble : Window
    {
        //private double searchboxPos = 0;
        private MainWindow parent_win;

        public void setParentWin(MainWindow m_win)
        {
            this.parent_win = m_win;
        }


        public searchBubble()
        {
            InitializeComponent();
            this.Top = 73;
            double win_heights = System.Windows.SystemParameters.PrimaryScreenHeight;
            double win_widths = System.Windows.SystemParameters.PrimaryScreenWidth;
            double add_width = (win_widths - 500) / 2;
            //google_top_line.Width  = addressTextContainer.Width;
            //google_bottom_line.Width = addressTextContainer.Width;
            /*if (add_width < 300)
            {
                //add_border.Width = 300;
                //addressTextContainer.Width = 300;
                searchboxPos = 300 + 270;
                this.Left = 570;

                //addressBack.Width = 500;
            }
            else
            {
                searchboxPos = 800;
                this.Left = 800;
               
               
            }*/
            //this.Deactivated += new EventHandler(searchBubble_Deactivated);
        }

        public void setXPos(double val)
        {
            //searchboxPos = val + 30;
            this.Left = val + 50 ;
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
                    parent_win.setSearchBubble(true);
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
