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
    public partial class regBubble : Window
    {
        //private double searchboxPos = 0;
        private login parent_win;

        public void setParentWin(login m_win)
        {
            this.parent_win = m_win;
        }


        public regBubble()
        {
            InitializeComponent();
            this.Left = 0;
            this.Top = 30;
            //this.Deactivated += new EventHandler(searchBubble_Deactivated);
        }

        public void setXPos(double xVal , double yVal)
        {
            //searchboxPos = val + 30;
            this.Left = xVal + 25;
            this.Top = yVal;
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
                parent_win.setRegBubble(true);
                this.Close();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
