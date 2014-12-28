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
using System.IO;
using System.Threading;
using System.Timers;
using System.Runtime.InteropServices;
using System.Windows.Threading;


namespace chromeBrowser
{
    /// <summary>
    /// Interaction logic for fileLoadProgress.xaml
    /// </summary>
    /// 

    //public delegate void ProgressChangeDelegate(double Persentage, ref bool Cancel);
    //public delegate void Completedelegate();

    public partial class fileLoadProgress : Window
    {

        private UsbPopUp parentWin;
        public EventHandler StopCopy;

        private DispatcherTimer checkProgressTimer = new DispatcherTimer();       

        public void setParentWindow(UsbPopUp usWin)
        {
            parentWin = usWin;
        }

        public fileLoadProgress()
        {
            InitializeComponent();
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            this.Activated += new EventHandler(fileLoadProgress_Activated);
            Random rnd = new Random();
            var temp = rnd.Next(-100, 100);
            this.Left = (screenWidth - 500) / 2 ;
            this.Top = (screenHeight - 350) / 2 ;
            cancelBtn.AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(cancelBtn_MouseDown), true);  
        }

        public void setPercent(string val)
        {
            //loading_percent.Text = "10";
            this.loading_percent.Text = val;
        }
        Thread oThread;

        private void fileLoadProgress_Activated(Object sender, EventArgs e)
        {
            FileCopy fc = new FileCopy();
            fc.copySet(SourceFilePath, DestFilePath);
            fc.setParent(this);
            oThread = new Thread(new ThreadStart(fc.Copy));
            oThread.Start();
            //checkProgressTimer.Start();
            //checkProgressTimer.Elapsed += new System.Timers.ElapsedEventHandler(checkProgressTimer_Elapsed);

            checkProgressTimer.Interval = TimeSpan.FromMilliseconds(100);
            checkProgressTimer.Start();
            checkProgressTimer.Tick +=  new EventHandler(checkProgressTimer_Elapsed);
            //   login_timer.Start();

            //  login_timer.Tick += new EventHandler(checkLogFunc);

        }

        private bool isFinish = false;

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //cancelFlag = true;
            //if (isFinish)
            try
            {
                
                oThread.Abort();
                parentWin.setStopCopy();
                if (this.StopCopy != null)
                    this.StopCopy(this, new EventArgs());
                if (File.Exists(DestFilePath))
                    File.Delete(DestFilePath);
                
                this.Close();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private int process_counter = 0;
        private int process_order = 0;

        private void checkProgressTimer_Elapsed(Object sender,EventArgs e)
        {
            try
            {
                if (oThread.ThreadState == ThreadState.Aborted)
                {
                    File.Delete(DestFilePath);
                    checkProgressTimer.Stop();
                    this.Close();
                }

                process_counter++;
                if (process_counter == 10)
                {
                    process_counter = 0;
                    process_order++;
                    if (process_order == 8)
                        process_order = 0;
                }

                //using (FileStream source = new FileStream(DestFilePath, FileMode.Open, FileAccess.Read))
                {
                    //long fileLength = source.Length;
                    real_progress.Width = loadingPercentValue * 4;
                    if (process_order == 0)
                    {
                        firstloading.Visibility = System.Windows.Visibility.Hidden;
                        secondloading.Visibility = System.Windows.Visibility.Hidden;
                        thirdloading.Visibility = System.Windows.Visibility.Hidden;
                        fourthloading.Visibility = System.Windows.Visibility.Hidden;
                        fifthloading.Visibility = System.Windows.Visibility.Hidden;
                        sixthloading.Visibility = System.Windows.Visibility.Hidden;
                        seventhloading.Visibility = System.Windows.Visibility.Hidden;
                    }
                    if (process_order > 0)
                        firstloading.Visibility = System.Windows.Visibility.Visible;
                    if (process_order > 1)
                        secondloading.Visibility = System.Windows.Visibility.Visible;
                    if (process_order > 2)
                        thirdloading.Visibility = System.Windows.Visibility.Visible;
                    if (process_order > 3)
                        fourthloading.Visibility = System.Windows.Visibility.Visible;
                    if (process_order > 4)
                        fifthloading.Visibility = System.Windows.Visibility.Visible;
                    if (process_order > 5)
                        sixthloading.Visibility = System.Windows.Visibility.Visible;
                    if (process_order > 6)
                        seventhloading.Visibility = System.Windows.Visibility.Visible;

                    /*
                    if (loadingPercentValue > 5)
                        firstloading.Visibility = System.Windows.Visibility.Visible;
                    if (loadingPercentValue > 15)
                        secondloading.Visibility = System.Windows.Visibility.Visible;
                    if (loadingPercentValue > 30)
                        thirdloading.Visibility = System.Windows.Visibility.Visible;
                    if (loadingPercentValue > 50)
                       fourthloading.Visibility = System.Windows.Visibility.Visible;
                    if (loadingPercentValue > 70)
                        fifthloading.Visibility = System.Windows.Visibility.Visible;
                    if (loadingPercentValue > 80)
                        sixthloading.Visibility = System.Windows.Visibility.Visible;
                    if (loadingPercentValue > 90)
                        seventhloading.Visibility = System.Windows.Visibility.Visible;*/

                    loading_percent.Text = FileLoadingValue.ToString() + "MB of " + FileSizeValue.ToString() + "MB copied";//Convert.ToInt32(loadingPercentValue).ToString();
                    //int per_val = Convert.ToInt32(loading_percent);
                    if (loadingPercentValue == 100)
                    {
                        //oThread.Suspend();
                        isFinish = true;
                        checkProgressTimer.Stop();                        
                        this.Close();
                    }

                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }


        public void copySet(string Source, string Dest)
        {
            this.SourceFilePath = Source;
            this.DestFilePath = Dest;
            loading_name.Text = System.IO.Path.GetFileName(Source);
            //this.Parent = this;
            //OnProgressChanged += delegate { };
            //OnComplete += delegate { };
        }


        public void setLoad(double val, double size)
        {
            //FileSizeValue =  val;
            FileSizeValue = Math.Round(size/1000, 1);
            FileLoadingValue =Math.Round(val/1000, 1);
            
        }

        public void Copy()
        {
            try
            {
                byte[] buffer = new byte[1024 * 1024]; // 1MB buffer
                bool cancelFlag = false;

                using (FileStream source = new FileStream(SourceFilePath, FileMode.Open, FileAccess.Read))
                {
                    long fileLength = source.Length;
                    using (FileStream dest = new FileStream(DestFilePath, FileMode.CreateNew, FileAccess.Write))
                    {
                        
                        long totalBytes = 0;
                        int currentBlockSize = 0;

                        while ((currentBlockSize = source.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            totalBytes += currentBlockSize;
                            double persentage = (double)totalBytes * 100.0 / fileLength;
                            //fileLoadProgress newParent = (fileLoadProgress)Parent;
                            //newParent.loading_percent.Text = persentage.ToString();
                            dest.Write(buffer, 0, currentBlockSize);
                            cancelFlag = false;
                            //OnProgressChanged(persentage, ref cancelFlag);

                            if (cancelFlag == true)
                            {
                                // Delete dest file here
                                break;
                            }
                            System.Threading.Thread.Sleep(100);
                        }
                        
                    }
                   
                }
                
                //OnComplete();
            }
            catch (Exception ex)
            {
                
            }
            //completeCopy();
        }


         public void completeCopy()
         {
             this.Close();
         }
        
        public string SourceFilePath { get; set; }
        public string DestFilePath { get; set; }
        

        public double FileSizeValue { get; set; }
        public double FileLoadingValue { get; set; }

        public Window Parent { get; set; }

        //public event ProgressChangeDelegate OnProgressChanged;
        //public event Completedelegate OnComplete;
        public double loadingPercentValue { get; set; }
        public void setloadingPercentValue(double val)
        {
            this.loadingPercentValue = val;
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            //Copy();

           
        }

        private void cancel_MouseEnter(object sender, MouseEventArgs e)
        {
            Color clr = Color.FromRgb(122, 157, 147);
            cancelBtn.Background = new SolidColorBrush(clr);

        }

        private void cancel_MouseLeave(object sender, MouseEventArgs e)
        {
            Color clr = Color.FromRgb(240, 78, 37);
            cancelBtn.Background = new SolidColorBrush(clr);           

        }

        private void cancelBtn_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {

                oThread.Abort();
                parentWin.setStopCopy();
                if (this.StopCopy != null)
                    this.StopCopy(this, new EventArgs());
                if (File.Exists(DestFilePath))
                    File.Delete(DestFilePath);                
                this.Close();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        
    }
}
