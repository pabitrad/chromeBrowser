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
using System.Windows.Media.Animation;

namespace chromeBrowser
{
    /// <summary>
    /// Interaction logic for clrSetting.xaml
    /// </summary>
    public partial class clrSetting : Window
    {
        public clrSetting()
        {
            InitializeComponent();

            double win_widths = System.Windows.SystemParameters.PrimaryScreenWidth;
            this.Left = win_widths - 250;
            this.Top = 200;

        }
        private MainWindow parent_win;

        public void setManiWin(MainWindow main_win)
        {
            parent_win = main_win;
        }

        public void setAnimationWin()
        {

        }

        public void aniColorWin()
        {
            Duration _duration = new Duration(TimeSpan.FromMilliseconds(500));
            DoubleAnimation animation0 = new DoubleAnimation();
            //animation0.st = System.Type.GetType("Top");

            animation0.From = 200;
            animation0.To = 300;
            animation0.Duration = _duration;
            //animation0.Completed += SlideCompleted;
            this.BeginAnimation(TopProperty, animation0);
        }
    }
}
