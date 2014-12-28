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
using System.Windows.Media.Effects;
using System.IO;
using System.Diagnostics;
using System.Timers;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Threading;
using chromeBrowser.usb;
using System.Windows.Threading;

namespace chromeBrowser
{
    /// <summary>
    /// Interaction logic for UsbPopUp.xaml
    /// </summary>
    public partial class UsbPopUp : Window
    {
        public MainWindow parent_win;


        private List<fileItem> docList = new List<fileItem>();
        private List<fileItem> musicList = new List<fileItem>();
        private List<fileItem> videoList = new List<fileItem>();
        private List<fileItem> imageList = new List<fileItem>();
        private List<fileItem> miscList = new List<fileItem>();

        private bool isDocItem = false;
        private bool isIMGItem = false;
        private bool isMusicItem = false;
        private bool isVideoItem = false;
        private bool isMiscItem = false;

        private int clickDocItem = -1;
        private int clickImgItem = -1;
        private int clickMusItem = -1;
        private int clickVidItem = -1;
        private int clickMisItem = -1;

        private int Doc_first_num = 0;
        private int Img_first_num = 0;
        private int Mus_first_num = 0;
        private int Vid_first_num = 0;
        private int Mis_first_num = 0;

        /*private int doc_selected_num = -1;
        private int img_selected_num = -1;
        private int mus_selected_num = -1;
        private int vid_selected_num = -1;
        private int mis_selected_num = -1;*/


        private int order = 500;
        private DispatcherTimer run_timer;

        public UsbPopUp()
        {
            InitializeComponent();
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            this.Width = screenWidth;
            this.Height = screenHeight;
            this.Left = 0;
            this.Top = 0;
            //this.Width = 1500;
            double bufferX = (screenWidth - 800) / 2;
            double bufferY = (screenHeight - 600) / 2;
            if (bufferY < 0)
                bufferY = 0;

            if (bufferY < 0)
                bufferY = 0;
            //this.Left = (screenWidth - this.Width) / 2;
            //this.Top = (screenHeight - this.Height) / 2;
            loadTimer.Elapsed += new System.Timers.ElapsedEventHandler(viewLoadTimer);
            CanvasBorder.Margin = new Thickness(bufferX, bufferY, bufferX, bufferY);
            setAllCanvas();
            setUsbLabel();
            getInitAllTypeList();
            addAllTypeToCanvas();
        }



        private void getInitAllTypeList()
        {
            //return;
            docList = new List<fileItem>();
            musicList = new List<fileItem>();
            videoList = new List<fileItem>();
            imageList = new List<fileItem>();
            miscList = new List<fileItem>();
            isDocItem = false;
            isIMGItem = false;
            isMusicItem = false;
            isVideoItem = false;
            isMiscItem = false;

            getDOcList();
            getImgList();
            getMusicList();
            getVideoList();
            getMiscList();
        }

        private void setAllCanvas()
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double bufferX = (screenWidth - 800) / 2;
            double bufferY = (screenHeight - 600) / 2;

            if (bufferY < 0)
                bufferY = 0;

            if (bufferY < 0)
                bufferY = 0;

            /////top
            top_can.Width = screenWidth;
            top_can.Height = bufferY;
            top_can.Margin = new Thickness(0, 0, 0, 630 + bufferY);

            //// left
            left_can.Width = bufferX;
            left_can.Height = 630;
            left_can.Margin = new Thickness(0, bufferY, 0, 0);

            ////  right 
            right_can.Width = bufferX;
            right_can.Height = 630;
            right_can.Margin = new Thickness(bufferX + 820, bufferY, 0, 0);

            ////  bottom
            bottom_can.Width = screenWidth;
            bottom_can.Height = bufferY;
            bottom_can.Margin = new Thickness(0, 630 + bufferY, 0, 0);

        }


        private void getAllTypeListFromAddFile()
        {
            //return;
            docList = new List<fileItem>();
            musicList = new List<fileItem>();
            videoList = new List<fileItem>();
            imageList = new List<fileItem>();
            miscList = new List<fileItem>();


            getDOcList();
            getImgList();
            getMusicList();
            getVideoList();
            getMiscList();
        }


        private void getAllTypeList()
        {
            //return;
            docList = new List<fileItem>();
            musicList = new List<fileItem>();
            videoList = new List<fileItem>();
            imageList = new List<fileItem>();
            miscList = new List<fileItem>();
           

            getDOcList();
            getImgList();
            getMusicList();
            getVideoList();
            getMiscList();
        }

        private string[] getFilesWithDate(string folder)
        {
            string[] arr ;//= new string[10];
            arr = Directory.GetFiles(folder);
            DateTime[] creationTimes = new DateTime[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                creationTimes[i] = File.GetLastWriteTime(arr[i]);//new FileInfo(arr[i]).CreationTime;
            Array.Sort(creationTimes, arr);
            //DateTime time = File.GetLastWriteTime(arr[0]);
            return arr;
        }





        private void getDOcList()
        {

            if (isFileCheck)
                return;
            try
            {
                UsbUtils usbUTILS = new UsbUtils();
                String usbnamespace = usbUTILS.getUSBDriver();
                string[] doc_array;
                if (usbnamespace.Length > 0)
                {
                    try
                    {
                        doc_array = getFilesWithDate(usbnamespace + "wemagin_v2\\Docs");

                        if (doc_array.Length > 0)
                        {
                            for (int i = 0; i < doc_array.Length; i++)
                            {
                                //int last_index = doc_array[i].LastIndexOf("\\");
                                fileItem doc_itm = new fileItem();

                                doc_itm.setLabelContent(System.IO.Path.GetFileName(doc_array[i]));
                                doc_itm.setFilePath(doc_array[i]);
                                switch (System.IO.Path.GetExtension(doc_array[i]))
                                {
                                    case ".doc":
                                        {
                                            doc_itm.setFileIcon("word");
                                        }
                                        break;
                                    case ".xls":
                                        {
                                            doc_itm.setFileIcon("excel");
                                        }
                                        break;
                                    case ".docx":
                                        {
                                            doc_itm.setFileIcon("word");
                                        }
                                        break;
                                    case ".xlsx":
                                        {
                                            doc_itm.setFileIcon("excel");
                                        }
                                        break;
                                    case ".pdf":
                                        {
                                            doc_itm.setFileIcon("pdf");
                                        }
                                        break;

                                    case ".ppt":
                                        {
                                            doc_itm.setFileIcon("default");
                                        }
                                        break;
                                    case ".pub":
                                        {
                                            doc_itm.setFileIcon("default");
                                        }
                                        break;
                                    case ".one":
                                        {
                                            doc_itm.setFileIcon("default");
                                        }
                                        break;

                                    case ".txt":
                                        {
                                            doc_itm.setFileIcon("text");
                                        }
                                        break;
                                }
                                this.docList.Add(doc_itm);

                                //this.titleBack.Content = mp3_array[0].Substring(last_index + 1, mp3_array[0].Length - last_index - 1);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

        }


        private void getImgList()
        {

            if (isFileCheck)
                return;
            try
            {
                UsbUtils usbUTILS = new UsbUtils();
                String usbnamespace = usbUTILS.getUSBDriver();
                string[] img_array;
                if (usbnamespace.Length > 0)
                {
                    try
                    {
                        img_array = getFilesWithDate(usbnamespace + "wemagin_v2\\Pictures");
                        if (img_array.Length > 0)
                        {
                            for (int i = 0; i < img_array.Length; i++)
                            {
                                //int last_index = img_array[i].LastIndexOf("\\");
                                fileItem img_itm = new fileItem();
                                img_itm.setFileIcon("image");
                                img_itm.setLabelContent(System.IO.Path.GetFileName(img_array[i]));
                                img_itm.setFilePath(img_array[i]);
                                // img_itm.setLabelContent(img_array[i].Substring(last_index + 1, img_array[i].Length - last_index - 1));
                                this.imageList.Add(img_itm);
                                //this.titleBack.Content = mp3_array[0].Substring(last_index + 1, mp3_array[0].Length - last_index - 1);
                            }
                        }

                    }
                    catch (Exception e)
                    {
                        e.ToString();
                    }

                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }


        private void getMusicList()
        {

            if (isFileCheck)
                return;
            try
            {
                UsbUtils usbUTILS = new UsbUtils();
                String usbnamespace = usbUTILS.getUSBDriver();
                string[] mus_array;
                if (usbnamespace.Length > 0)
                {
                    try
                    {
                        mus_array = getFilesWithDate(usbnamespace + "wemagin_v2\\Music");
                        if (mus_array.Length > 0)
                        {
                            for (int i = 0; i < mus_array.Length; i++)
                            {
                                //int last_index = mus_array[i].LastIndexOf("\\");
                                fileItem mus_itm = new fileItem();
                                mus_itm.setFileIcon("music");
                                mus_itm.setLabelContent(System.IO.Path.GetFileName(mus_array[i]));
                                mus_itm.setFilePath(mus_array[i]);
                                // mus_itm.setLabelContent(mus_array[i].Substring(last_index + 1, mus_array[i].Length - last_index - 1));
                                this.musicList.Add(mus_itm);
                                //this.titleBack.Content = mp3_array[0].Substring(last_index + 1, mp3_array[0].Length - last_index - 1);
                            }
                        }

                    }
                    catch (Exception e)
                    {
                        e.ToString();
                    }

                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }




        public void getMusicList(string[] musicOfParentArr)
        {

            //if (isFileCheck)
                //return;
            try
            {
                //UsbUtils usbUTILS = new UsbUtils();
               // String usbnamespace = usbUTILS.getUSBDriver();
                string[] mus_array;
               // if (usbnamespace.Length > 0)
                {
                    try
                    {
                        mus_array = musicOfParentArr;//Directory.GetFiles(usbnamespace + "wemagin_v2\\Music");
                        if (mus_array.Length > 0)
                        {
                            for (int i = 0; i < mus_array.Length; i++)
                            {
                                //int last_index = mus_array[i].LastIndexOf("\\");
                                fileItem mus_itm = new fileItem();
                                mus_itm.setFileIcon("music");
                                mus_itm.setLabelContent(System.IO.Path.GetFileName(mus_array[i]));
                                mus_itm.setFilePath(mus_array[i]);
                                // mus_itm.setLabelContent(mus_array[i].Substring(last_index + 1, mus_array[i].Length - last_index - 1));
                                this.musicList.Add(mus_itm);
                                //this.titleBack.Content = mp3_array[0].Substring(last_index + 1, mp3_array[0].Length - last_index - 1);
                            }
                        }

                    }
                    catch (Exception e)
                    {
                        e.ToString();
                    }

                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            setMusicList();
        }

        private void getVideoList()
        {

            if (isFileCheck)
                return;
            try
            {
                UsbUtils usbUTILS = new UsbUtils();
                String usbnamespace = usbUTILS.getUSBDriver();
                string[] vid_array;
                if (usbnamespace.Length > 0)
                {
                    try
                    {
                        vid_array = getFilesWithDate(usbnamespace + "wemagin_v2\\Movie");
                        if (vid_array.Length > 0)
                        {
                            for (int i = 0; i < vid_array.Length; i++)
                            {
                                //int last_index = vid_array[i].LastIndexOf("\\");
                                fileItem vid_itm = new fileItem();
                                vid_itm.setFileIcon("moive");
                                vid_itm.setLabelContent(System.IO.Path.GetFileName(vid_array[i]));
                                vid_itm.setFilePath(vid_array[i]);
                                //vid_itm.setLabelContent(vid_array[i].Substring(last_index + 1, vid_array[i].Length - last_index - 1));
                                this.videoList.Add(vid_itm);
                                //this.titleBack.Content = mp3_array[0].Substring(last_index + 1, mp3_array[0].Length - last_index - 1);
                            }
                        }

                    }
                    catch (Exception e)
                    {
                        e.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void getMiscList()
        {

            if (isFileCheck)
                return;
            try
            {
                UsbUtils usbUTILS = new UsbUtils();
                String usbnamespace = usbUTILS.getUSBDriver();
                string[] misc_array;
                if (usbnamespace.Length > 0)
                {
                    try
                    {
                        misc_array = getFilesWithDate(usbnamespace + "wemagin_v2\\Missc");
                        if (misc_array.Length > 0)
                        {
                            for (int i = 0; i < misc_array.Length; i++)
                            {
                                //int last_index = misc_array[i].LastIndexOf("\\");
                                fileItem misc_itm = new fileItem();
                                misc_itm.setFileIcon("misc");
                                misc_itm.setLabelContent(System.IO.Path.GetFileName(misc_array[i]));
                                misc_itm.setFilePath(misc_array[i]);
                                this.miscList.Add(misc_itm);
                            }
                        }

                    }
                    catch (Exception e)
                    {
                        e.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void addAllTypeToCanvas()
        {

            setDOcList();
            setImgList();
            setMusicList();
            setVideoList();
            setMiscList();
        }


        private void setDOcList()
        {

            if (isFileCheck)
                return;
            try
            {
                //doc_list.Children.r 
                if (clickDocItem >= docList.Count)
                    clickDocItem = docList.Count - 1;

                if ((Doc_first_num + 18 < docList.Count))
                {
                    for (int i = Doc_first_num; i < Doc_first_num + 18; i++)
                    {
                        doc_list.Children.Remove(docList[i]);
                        doc_list.Children.Add(docList[i]);
                        Canvas.SetLeft(docList[i], 7);
                        //Canvas.SetZIndex(webBuffer.webCan, 0);
                        Canvas.SetTop(docList[i], 25 * (i - Doc_first_num));
                        if (this.clickDocItem == i)
                            docList[i].set_select_item();
                        docList[i].buttonCick += new EventHandler(mini_window);
                        docList[i].MouseLeftButtonDown += new MouseButtonEventHandler(checkClickDocsItem);                        
                        docList[i].MouseRightButtonDown += new MouseButtonEventHandler(showDocMenu);


                    }
                }
                else
                {

                    for (int i = Doc_first_num; i < docList.Count; i++)
                    {
                        doc_list.Children.Remove(docList[i]);
                        doc_list.Children.Add(docList[i]);
                        Canvas.SetLeft(docList[i], 7);
                        //Canvas.SetZIndex(webBuffer.webCan, 0);
                        Canvas.SetTop(docList[i], 25 * (i - Doc_first_num));
                        if (this.clickDocItem == i)
                            docList[i].set_select_item();
                        docList[i].buttonCick += new EventHandler(mini_window);
                        docList[i].MouseLeftButtonDown += new MouseButtonEventHandler(checkClickDocsItem);                        
                        docList[i].MouseRightButtonDown += new MouseButtonEventHandler(showDocMenu);


                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }



        [DllImport("User32", CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndParent);

        fileItem run_file;
        Process runFileProcess;
        //bool isLock = false;

        private void checkLogFunc(Object sender, EventArgs ag)
        {
            try
            {
                
                run_timer.Stop();
                run_timer = null;
                //isLock = false;
                run_file = null;
            }
            catch (Exception ex)
            {
            }
        }


        private void mini_window(object sender, EventArgs e)
        {
            try
            {
                //if (isLock)
                    //return;
                if (isFileCheck)
                    return;               

               
                this.parent_win.Topmost = false;
                this.Topmost = false;
                fileItem fi = (fileItem)sender;
                if ((run_file != null) && (run_file == fi))
                    return;
                run_file = fi;
                run_timer = new DispatcherTimer();
                run_timer.Interval = TimeSpan.FromMilliseconds(1000);
                run_timer.Start();
                run_timer.Tick += new EventHandler(checkLogFunc);               
                
                runFileProcess = new Process();
                runFileProcess.StartInfo.FileName = fi.getFIlePath();
                runFileProcess.StartInfo.UseShellExecute = true;
                runFileProcess.Start();
            }
            catch (Exception ex)
            {

            }

            //this.parent_win.Topmost = false;
            //SetParent(wordProcess.MainWindowHandle, Process.GetCurrentProcess().MainWindowHandle);           
            // this.parent_win.WindowState = System.Windows.WindowState.Minimized;
            // this.WindowState = System.Windows.WindowState.Minimized;
        }


        private void setImgList()
        {

            if (isFileCheck)
                return;
            try
            {
                if (clickImgItem >= imageList.Count)
                    clickImgItem = imageList.Count - 1;
                if ((Img_first_num + 18 < imageList.Count))
                {
                    for (int i = Img_first_num; i < this.Img_first_num + 18; i++)
                    {
                        pic_list.Children.Remove(imageList[i]);
                        pic_list.Children.Add(imageList[i]);
                        //imageList[i].buttonCick -= new EventHandler(mini_window);

                        Canvas.SetLeft(imageList[i], 7);
                        //Canvas.SetZIndex(webBuffer.webCan, 0);
                        Canvas.SetTop(imageList[i], 25 * (i - Img_first_num));
                        if (this.clickImgItem == i)
                            imageList[i].set_select_item();
                        imageList[i].buttonCick += new EventHandler(mini_window);
                        imageList[i].MouseLeftButtonDown += new MouseButtonEventHandler(checkClickImgsItem);                        
                        imageList[i].MouseRightButtonDown += new MouseButtonEventHandler(showImageMenu);
                    }
                }
                else
                {
                    for (int i = Img_first_num; i < this.imageList.Count; i++)
                    {
                        pic_list.Children.Remove(imageList[i]);
                        pic_list.Children.Add(imageList[i]);

                        Canvas.SetLeft(imageList[i], 7);
                        //Canvas.SetZIndex(webBuffer.webCan, 0);
                        Canvas.SetTop(imageList[i], 25 * (i - Img_first_num));
                        if (this.clickImgItem == i)
                            imageList[i].set_select_item();
                        imageList[i].buttonCick += new EventHandler(mini_window);
                        imageList[i].MouseLeftButtonDown += new MouseButtonEventHandler(checkClickImgsItem);
                        imageList[i].MouseRightButtonDown += new MouseButtonEventHandler(showImageMenu);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

        }


        private void setMusicList()
        {

            if (isFileCheck)
                return;
            try
            {
                if (clickMusItem >= musicList.Count)
                    clickMusItem = musicList.Count - 1;
                if ((Mus_first_num + 18 < musicList.Count))
                {
                    for (int i = Mus_first_num; i < Mus_first_num + 18; i++)
                    {
                        music_list.Children.Remove(musicList[i]);
                        music_list.Children.Add(musicList[i]);
                        Canvas.SetLeft(musicList[i], 7);
                        Canvas.SetTop(musicList[i], 25 * (i - Mus_first_num));
                        if (this.clickMusItem == i)
                            musicList[i].set_select_item();
                        musicList[i].buttonCick += new EventHandler(play_music);
                        musicList[i].MouseLeftButtonDown += new MouseButtonEventHandler(checkClickMuicsItem);                        
                        musicList[i].MouseRightButtonDown += new MouseButtonEventHandler(showMusicMenu);
                    }
                }
                else
                {
                    for (int i = Mus_first_num; i < this.musicList.Count; i++)
                    {
                        music_list.Children.Remove(musicList[i]);
                        music_list.Children.Add(musicList[i]);
                        Canvas.SetLeft(musicList[i], 7);
                        Canvas.SetTop(musicList[i], 25 * (i - Mus_first_num));
                        if (this.clickMusItem == i)
                            musicList[i].set_select_item();
                        musicList[i].buttonCick += new EventHandler(play_music);
                        musicList[i].MouseLeftButtonDown += new MouseButtonEventHandler(checkClickMuicsItem);
                        musicList[i].MouseRightButtonDown += new MouseButtonEventHandler(showMusicMenu);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }



        private void play_music(object sender, EventArgs e)
        {
            //this.parent_win.setPlayer();
            //fileItem fi = (fileItem)sender;
            //parent_win.playFile(fi.getFIlePath());

        }


        private void setVideoList()
        {
            try
            {
                if (clickVidItem >= videoList.Count)
                    clickVidItem = videoList.Count - 1;

                if ((this.Vid_first_num + 18 < videoList.Count))
                {
                    for (int i = Vid_first_num; i < Vid_first_num + 18; i++)
                    {
                        movie_list.Children.Remove(this.videoList[i]);
                        movie_list.Children.Add(this.videoList[i]);
                        Canvas.SetLeft(videoList[i], 7);
                        Canvas.SetTop(videoList[i], 25 * (i - Vid_first_num));
                        if (this.clickVidItem == i)
                            videoList[i].set_select_item();
                        videoList[i].buttonCick += new EventHandler(mini_window);                        
                        videoList[i].MouseLeftButtonDown += new MouseButtonEventHandler(checkClickVideosItem);
                        videoList[i].MouseRightButtonDown += new MouseButtonEventHandler(showVideoMenu);
                        // musicList[i].MouseLeftButtonDown += new MouseButtonEventHandler(checkClickMuicsItem);
                    }
                }
                else
                {
                    for (int i = Vid_first_num; i < this.videoList.Count; i++)
                    {
                        movie_list.Children.Remove(this.videoList[i]);
                        movie_list.Children.Add(this.videoList[i]);
                        Canvas.SetLeft(videoList[i], 7);
                        Canvas.SetTop(videoList[i], 25 * (i - Vid_first_num));
                        if (this.clickVidItem == i)
                            videoList[i].set_select_item();
                        videoList[i].buttonCick += new EventHandler(mini_window);                        
                        videoList[i].MouseLeftButtonDown += new MouseButtonEventHandler(checkClickVideosItem);
                        videoList[i].MouseRightButtonDown += new MouseButtonEventHandler(showVideoMenu);
                        // musicList[i].MouseLeftButtonDown += new MouseButtonEventHandler(checkClickMuicsItem);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void setMiscList()
        {

            if (isFileCheck)
                return;
            try
            {
                if (clickMisItem >= miscList.Count)
                    clickMisItem = miscList.Count - 1;

                if (this.Mis_first_num + 18 < this.miscList.Count)
                {
                    for (int i = Mis_first_num; i < Mis_first_num + 18; i++)
                    {
                        misc_list.Children.Remove(this.miscList[i]);
                        misc_list.Children.Add(this.miscList[i]);
                        Canvas.SetLeft(miscList[i], 7);
                        Canvas.SetTop(miscList[i], 25 * (i - Mis_first_num));
                        if (this.clickMisItem == i)
                            miscList[i].set_select_item();
                        miscList[i].buttonCick += new EventHandler(mini_window);
                        miscList[i].MouseLeftButtonDown += new MouseButtonEventHandler(this.checkClickMiscItem);                        
                        miscList[i].MouseRightButtonDown += new MouseButtonEventHandler(showMiscMenu);
                        
                    }
                }
                else
                {
                    for (int i = Mis_first_num; i < this.miscList.Count; i++)
                    {
                        misc_list.Children.Remove(this.miscList[i]);
                        misc_list.Children.Add(this.miscList[i]);
                        Canvas.SetLeft(miscList[i], 7);
                        Canvas.SetTop(miscList[i], 25 * (i - Mis_first_num));
                        if (this.clickMisItem == i)
                            miscList[i].set_select_item();
                        miscList[i].buttonCick += new EventHandler(mini_window);                        
                        miscList[i].MouseLeftButtonDown += new MouseButtonEventHandler(this.checkClickMiscItem);
                        miscList[i].MouseRightButtonDown += new MouseButtonEventHandler(showMiscMenu);
                        //miscList[i].MouseRightButtonDown += new MouseButtonEventHandler(showMenu);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }


        private deleteMenu delWin ;


        private void showMusicMenu(object sender, EventArgs e)
        {
            try
            {
                fileItem clickItem = (fileItem)sender;
                setAllDeselect();
                if (click_item != null)
                    click_item.set_deselect_item();
                setAllDeselect();
                click_item = clickItem;
                click_item.set_select_item();
                Canvas.SetZIndex(clickItem, order);
                order++;
                for (int i = 0; i < this.musicList.Count; i++)
                {
                    if (clickItem == musicList[i])
                    {
                        this.clickMusItem = i;
                    }
                }
            }

            catch (Exception ex)
            {

            }

            fileItem fi = (fileItem)(sender);


            if (delWin != null)
            {
                delWin.Close();
            }
            delWin = new deleteMenu()
            /*{
                Owner = this,
                ShowInTaskbar = false,
                Topmost = false
            }*/;
            delWin.setParent(this);
            delWin.setFilePath(fi.getFIlePath());
            delWin.setFileType(2);
            delWin.setPosition(Mouse.GetPosition(this).X, Mouse.GetPosition(this).Y);
            delWin.Topmost = true;
            delWin.Show();
        }


        private void showDocMenu(object sender, EventArgs e)
        {
            try
            {
                fileItem clickItem = (fileItem)sender;
                setAllDeselect();
                if (click_item != null)
                    click_item.set_deselect_item();
                click_item = clickItem;
                click_item.set_select_item();
                Canvas.SetZIndex(clickItem, order);
                order++;
                for (int i = 0; i < this.docList.Count; i++)
                {
                    if (clickItem == docList[i])
                    {
                        this.clickDocItem = i;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            fileItem fi = (fileItem)(sender);


            if (delWin != null)
            {
                delWin.Close();
            }
            delWin = new deleteMenu();
            /*{
                Owner = this,
                ShowInTaskbar = false,
                Topmost = false
            };*/
            delWin.setParent(this);
            delWin.setFilePath(fi.getFIlePath());
            delWin.setFileType(0);
            delWin.setPosition(Mouse.GetPosition(this).X, Mouse.GetPosition(this).Y);
            delWin.Topmost = true;
            delWin.Show();
        }


        private void showImageMenu(object sender, EventArgs e)
        {
            try
            {
                fileItem clickItem = (fileItem)sender;
                setAllDeselect();
                if (click_item != null)
                    click_item.set_deselect_item();
                click_item = clickItem;
                click_item.set_select_item();
                Canvas.SetZIndex(clickItem, order);
                order++;
                for (int i = 0; i < this.imageList.Count; i++)
                {
                    if (clickItem == imageList[i])
                    {
                        this.clickImgItem = i;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            fileItem fi = (fileItem)(sender);


            if (delWin != null)
            {
                delWin.Close();
            }
            delWin = new deleteMenu();
            /*{
                Owner = this,
                ShowInTaskbar = false,
                Topmost = false
            };*/
            delWin.setParent(this);
            delWin.setFilePath(fi.getFIlePath());
            delWin.setFileType(1);
            delWin.setPosition(Mouse.GetPosition(this).X, Mouse.GetPosition(this).Y);
            delWin.Topmost = true;
            delWin.Show();
        }


        private void showVideoMenu(object sender, EventArgs e)
        {
            try
            {
                fileItem clickItem = (fileItem)sender;
                setAllDeselect();
                if (click_item != null)
                    click_item.set_deselect_item();
                click_item = clickItem;
                click_item.set_select_item();
                Canvas.SetZIndex(clickItem, order);
                order++;
                for (int i = 0; i < this.videoList.Count; i++)
                {
                    if (clickItem == videoList[i])
                    {
                        this.clickVidItem = i;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            fileItem fi = (fileItem)(sender);


            if (delWin != null)
            {
                delWin.Close();
            }
            delWin = new deleteMenu()
            /*{
                Owner = this,
                ShowInTaskbar = false,
                Topmost = false
            }*/;
            delWin.setParent(this);
            delWin.setFilePath(fi.getFIlePath());
            delWin.setFileType(3);
            delWin.setPosition(Mouse.GetPosition(this).X, Mouse.GetPosition(this).Y);
            delWin.Topmost = true;
            delWin.Show();
        }


        private void showMiscMenu(object sender, EventArgs e)
        {
            try
            {
                fileItem clickItem = (fileItem)sender;
                setAllDeselect();
                if (click_item != null)
                    click_item.set_deselect_item();
                click_item = clickItem;
                click_item.set_select_item();
                Canvas.SetZIndex(clickItem, order);
                order++;
                for (int i = 0; i < this.miscList.Count; i++)
                {
                    if (clickItem == miscList[i])
                    {
                        this.clickMisItem = i;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            fileItem fi = (fileItem)(sender);


            if (delWin != null)
            {
                delWin.Close();
            }
            delWin = new deleteMenu()
            /*{
                Owner = this,
                ShowInTaskbar = false,
                Topmost = false
            }*/;
            delWin.setParent(this);
            delWin.setFilePath(fi.getFIlePath());
            delWin.setFileType(4);
            delWin.setPosition(Mouse.GetPosition(this).X, Mouse.GetPosition(this).Y);
            delWin.Topmost = true;
            delWin.Show();
        }



        public void setMain(MainWindow main_win)
        {
            parent_win = main_win;
        }
        private void webForward_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (isFileCheck)
                return;
            if (delWin != null)
                delWin.Close();
           // parent_win.setPlayer(this.getFilePathsOfMusic());
            parent_win.Opacity = 1;
            parent_win.Effect = null;
            this.Close();

        }

        private void usbMin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (isFileCheck)
                return;
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void doc_folder_MouseEnter(object sender, MouseEventArgs e)
        {
            if (isFileCheck)
                return;
            doc_top.Visibility = System.Windows.Visibility.Visible;
        }

        private void doc_folder_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isFileCheck)
                return;
            doc_top.Visibility = System.Windows.Visibility.Hidden;
        }

        private void pic_folder_MouseEnter(object sender, MouseEventArgs e)
        {
            if (isFileCheck)
                return;
            pic_top.Visibility = System.Windows.Visibility.Visible;
        }

        private void pic_folder_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isFileCheck)
                return;
            pic_top.Visibility = System.Windows.Visibility.Hidden;
        }

        private void music_folder_MouseEnter(object sender, MouseEventArgs e)
        {
            if (isFileCheck)
                return;
            music_top.Visibility = System.Windows.Visibility.Visible;
        }

        private void music_folder_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isFileCheck)
                return;
            music_top.Visibility = System.Windows.Visibility.Hidden;
        }


        private void movie_folder_MouseEnter(object sender, MouseEventArgs e)
        {
            if (isFileCheck)
                return;
            movies_top.Visibility = System.Windows.Visibility.Visible;
        }

        private void movie_folder_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isFileCheck)
                return;
            movies_top.Visibility = System.Windows.Visibility.Hidden;
        }


        private void misc_folder_MouseEnter(object sender, MouseEventArgs e)
        {
            if (isFileCheck)
                return;
            misc_top.Visibility = System.Windows.Visibility.Visible;
        }

        private void misc_folder_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isFileCheck)
                return;
            misc_top.Visibility = System.Windows.Visibility.Hidden;
        }

        private bool isDeleteItem = false;


        private void bin_folder_MouseEnter(object sender, MouseEventArgs e)
        {
            if (isFileCheck)
                return;
            Uri uri = new Uri("Images/usb/binDown.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            bin_folder.Source = bitmap;

            // bin_bottom.Visibility = System.Windows.Visibility.Visible;
            isDeleteItem = true;
        }

        private void bin_folder_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isFileCheck)
                return;
            Uri uri = new Uri("Images/usb/bin.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            bin_folder.Source = bitmap;

            // bin_bottom.Visibility = System.Windows.Visibility.Hidden;
            isDeleteItem = false;
        }

        private bool isFileCheck = false;


        private void copyFiles(string source, string destFile)
        {
            if (isStopCopy)
                return;

            fileLoadProgress lp = new fileLoadProgress()
            {
                Owner = this,
                ShowInTaskbar = false,
                Topmost = false
            };
            lp.setParentWindow(this);
            lp.copySet(source, destFile);
            lp.StopCopy += new EventHandler(stopCopyFile);  
            //lp.Copy();
            lp.ShowDialog();    
        }

        private void stopCopyFile(object sender, EventArgs e)
        {
            isStopCopy = true;
        }

        private bool isStopCopy = false;

        public void setStopCopy()
        {
            isStopCopy = true;
        }

        private void addUsb_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (isFileCheck)
                return;
            isStopCopy = false;
            try
            {
                UsbUtils usbUTILS = new UsbUtils();
                String usbnamespace = usbUTILS.getUSBDriver();
                if (usbnamespace.Length > 0)
                {

                    string folder_name = System.IO.Path.Combine(usbnamespace, "wemagin_v2");
                    if (!Directory.Exists(folder_name))
                    {
                        Directory.CreateDirectory(folder_name);
                        DirectoryInfo dir = new DirectoryInfo(folder_name);
                        dir.Attributes |= FileAttributes.Hidden;
                    };

                    //int size = -1;
                    System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
                    openFileDialog1.Multiselect = true;
                    openFileDialog1.Filter =/*"Images (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|" +*/"All files (*.*)|*.*";
                    openFileDialog1.Title = "USB File Browser";
                    System.Windows.Forms.DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
                    if (result == System.Windows.Forms.DialogResult.OK) // Test result.
                    {

                        isFileCheck = true;
                        foreach (String file in openFileDialog1.FileNames)
                        {

                            //string file = openFileDialog1.FileName;
                            string file_type = System.IO.Path.GetExtension(file);
                            string real_fileName = System.IO.Path.GetFileName(file);
                            string dest_file = "";                          

                                switch (file_type)
                                {
                                    case ".txt":
                                    case ".doc":
                                    case ".pdf":
                                    case ".docx":
                                    case ".xlsx":
                                    case ".xls":
                                    case ".pub":
                                    case ".one":
                                    case ".ppt":
                                        {
                                            if (System.IO.File.Exists(usbnamespace + "wemagin_v2\\Docs"))
                                            {

                                                dest_file = usbnamespace + "wemagin_v2\\Docs\\" + real_fileName;
                                                copyFiles(file, dest_file);
                                                //DateTime time = new DateTime(2010, 3, 14, 2, 30, 00);
                                                //File.SetCreationTime(dest_file, time);
                                                //System.IO.File.Copy(file, dest_file, true);                                       
                                                //File.SetAttributes(dest_file, FileAttributes.Hidden);
                                               /* fileLoadProgress lp = new fileLoadProgress()
                                                {
                                                    Owner = this,
                                                    ShowInTaskbar = false,
                                                    Topmost = false
                                                };

                                                lp.copySet(file, dest_file);
                                                lp.ShowDialog();    */
                                            }
                                            else
                                            {
                                                System.IO.Directory.CreateDirectory(usbnamespace + "wemagin_v2\\Docs");
                                                dest_file = usbnamespace + "wemagin_v2\\Docs\\" + real_fileName;
                                                //System.IO.File.Copy(file, dest_file, true);
                                                //File.SetAttributes(dest_file, FileAttributes.Hidden);
                                                copyFiles(file, dest_file);
                                                //DateTime time = new DateTime(2010, 3, 14, 2, 30, 00);
                                                //File.SetCreationTime(dest_file, time);
                                            }

                                        }
                                        break;                                  
                                    case ".png":
                                    case ".psd":
                                    case ".bmp":
                                    case ".jpg":
                                    case ".jpeg":
                                    case ".gif":

                                        {
                                            if (System.IO.File.Exists(usbnamespace + "wemagin_v2\\Pictures"))
                                            {
                                                dest_file = usbnamespace + "wemagin_v2\\Pictures\\" + real_fileName;
                                                copyFiles(file, dest_file);
                                                //System.IO.File.Copy(file, dest_file, true);
                                                //File.SetAttributes(dest_file, FileAttributes.Hidden);
                                            }
                                            else
                                            {
                                                System.IO.Directory.CreateDirectory(usbnamespace + "wemagin_v2\\Pictures");
                                                dest_file = usbnamespace + "wemagin_v2\\Pictures\\" + real_fileName;
                                                copyFiles(file, dest_file);
                                                //System.IO.File.Copy(file, dest_file, true);
                                                //File.SetAttributes(dest_file, FileAttributes.Hidden);
                                            }

                                        }
                                        break;                

                                    case ".wav":
                                    case ".mp3":
                                    case ".wma":
                                        {

                                            if (System.IO.File.Exists(usbnamespace + "wemagin_v2\\Music"))
                                            {
                                                dest_file = usbnamespace + "wemagin_v2\\Music\\" + real_fileName;
                                                copyFiles(file, dest_file);
                                                //System.IO.File.Copy(file, dest_file, true);
                                                //File.SetAttributes(dest_file, FileAttributes.Hidden);
                                            }
                                            else
                                            {
                                                System.IO.Directory.CreateDirectory(usbnamespace + "wemagin_v2\\Music");
                                                dest_file = usbnamespace + "wemagin_v2\\Music\\" + real_fileName;
                                                copyFiles(file, dest_file);
                                                //System.IO.File.Copy(file, dest_file, true);
                                                //File.SetAttributes(dest_file, FileAttributes.Hidden);
                                            }
                                            fileItem mus_itm = new fileItem();
                                            mus_itm.setFileIcon("music");
                                            mus_itm.setLabelContent(System.IO.Path.GetFileName(dest_file));
                                            mus_itm.setFilePath(dest_file);
                                            // mus_itm.setLabelContent(mus_array[i].Substring(last_index + 1, mus_array[i].Length - last_index - 1));
                                            this.musicList.Add(mus_itm);
                                            //parent_win.setPlayer(this.getFilePathsOfMusic());
                                            // parent_win.mp3_array.Concat(dest_file);                                   
                                            //this.parent_win.mp3_array = Directory.GetFiles(usbnamespace + "wemagin_v2\\Music");

                                        }
                                        break;
                                    /*
                                    case ".mp3":
                                        {
                                            if (System.IO.File.Exists(usbnamespace + "wemagin_v2\\Music"))
                                            {
                                                dest_file = usbnamespace + "wemagin_v2\\Music\\" + real_fileName;
                                                System.IO.File.Copy(file, dest_file, true);
                                                File.SetAttributes(dest_file, FileAttributes.Hidden);
                                            }
                                            else
                                            {
                                                System.IO.Directory.CreateDirectory(usbnamespace + "wemagin_v2\\Music");
                                                dest_file = usbnamespace + "wemagin_v2\\Music\\" + real_fileName;
                                                System.IO.File.Copy(file, dest_file, true);
                                                File.SetAttributes(dest_file, FileAttributes.Hidden);
                                            }
                                            parent_win.mp3_array = Directory.GetFiles(usbnamespace + "wemagin_v2\\Music");
                                        }
                                        break;
                                    case ".wma":
                                        {
                                            if (System.IO.File.Exists(usbnamespace + "wemagin_v2\\Music"))
                                            {
                                                dest_file = usbnamespace + "wemagin_v2\\Music\\" + real_fileName;
                                                System.IO.File.Copy(file, dest_file, true);
                                                File.SetAttributes(dest_file, FileAttributes.Hidden);
                                            }
                                            else
                                            {
                                                System.IO.Directory.CreateDirectory(usbnamespace + "wemagin_v2\\Music");
                                                dest_file = usbnamespace + "wemagin_v2\\Music\\" + real_fileName;
                                                System.IO.File.Copy(file, dest_file, true);
                                                File.SetAttributes(dest_file, FileAttributes.Hidden);
                                            }
                                            parent_win.mp3_array = Directory.GetFiles(usbnamespace + "wemagin_v2\\Music");
                                        }
                                        break;*/
                                    case ".mpg":
                                    case ".asf":
                                    case ".mpeg":
                                    case ".avi":
                                    case ".wmv":
                                    case ".rm":
                                    case ".flv":
                                    case ".mp4":
                                    case ".m4v":
                                    case ".VOB":
                                    case ".mkv":
                                        {
                                            if (System.IO.File.Exists(usbnamespace + "wemagin_v2\\Movie"))
                                            {
                                                dest_file = usbnamespace + "wemagin_v2\\Movie\\" + real_fileName;
                                                copyFiles(file, dest_file);
                                                //System.IO.File.Copy(file, dest_file, true);
                                                //File.SetAttributes(dest_file, FileAttributes.Hidden);
                                            }
                                            else
                                            {
                                                System.IO.Directory.CreateDirectory(usbnamespace + "wemagin_v2\\Movie");
                                                dest_file = usbnamespace + "wemagin_v2\\Movie\\" + real_fileName;
                                                copyFiles(file, dest_file);
                                                //System.IO.File.Copy(file, dest_file, true);
                                                //File.SetAttributes(dest_file, FileAttributes.Hidden);
                                            }
                                        }
                                        break;                                      
                                    default:
                                        {
                                            //loadTimer.Start();
                                            if (System.IO.File.Exists(usbnamespace + "wemagin_v2\\Missc"))
                                            {
                                                dest_file = usbnamespace + "wemagin_v2\\Missc\\" + real_fileName;
                                                //System.IO.File.Copy(file, dest_file, true);
                                                copyFiles(file, dest_file);
                                                //File.SetAttributes(dest_file, FileAttributes.Hidden);
                                            }
                                            else
                                            {
                                                System.IO.Directory.CreateDirectory(usbnamespace + "wemagin_v2\\Missc");
                                                dest_file = usbnamespace + "wemagin_v2\\Missc\\" + real_fileName;
                                                copyFiles(file, dest_file);
                                                //System.IO.File.Copy(file, dest_file, true);
                                                //File.SetAttributes(dest_file, FileAttributes.Hidden);
                                            }
                                        }
                                        break;
                                }
                                isFileCheck = false;
                                this.clean_All_ListItme();
                                setUsbLabel();
                                getAllTypeListFromAddFile();
                                addAllTypeToCanvas();                            
                        }
                    }
                }
                // Console.WriteLine(size); // <-- Shows file size in debugging mode.
                // Console.WriteLine(result); // <-- For debugging use.

            }
            catch (Exception ex)
            {
                isFileCheck = false;
                ex.ToString();
            }
        }

        private void addMusicFiles(String file , String real_fileName)
        {
            try
            {
                UsbUtils usbUTILS = new UsbUtils();
                String usbnamespace = usbUTILS.getUSBDriver();
                String dest_file = "";
                if (usbnamespace.Length > 0)
                {
                    if (System.IO.File.Exists(usbnamespace + "wemagin_v2\\Music"))
                    {
                        dest_file = usbnamespace + "wemagin_v2\\Music\\" + real_fileName;
                        System.IO.File.Copy(file, dest_file, true);
                        File.SetAttributes(dest_file, FileAttributes.Hidden);
                    }
                    else
                    {
                        System.IO.Directory.CreateDirectory(usbnamespace + "wemagin_v2\\Music");
                        dest_file = usbnamespace + "wemagin_v2\\Music\\" + real_fileName;
                        System.IO.File.Copy(file, dest_file, true);
                        File.SetAttributes(dest_file, FileAttributes.Hidden);
                    }

                    isFileCheck = false;
                    setUsbLabel();
                    //getAllTypeList();
                    addAllTypeToCanvas();

                }
                else
                {
                }
            }
            catch (Exception ex)
            {

            }
        }



        private System.Timers.Timer loadTimer = new System.Timers.Timer(1000);

        private void viewLoadTimer(object sender, ElapsedEventArgs e)
        {
           // MessageBox.Show("1");
        }

        public void deleteMusicByRightMouse()
        {
            try
            {
                if (delWin != null)
                    this.delWin.Close();

                for (int i = 0; i < this.musicList.Count; i++)
                    music_list.Children.Remove(musicList[i]);
                this.musicList.Remove(musicList[clickMusItem]);

                this.setUsbLabel();
                clean_All_ListItme();
                getAllTypeList();
                addAllTypeToCanvas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void deleteByRightMouse()
        {
            try
            {
                if (delWin != null)
                    this.delWin.Close();               

                this.setUsbLabel();
                clean_All_ListItme();
                getAllTypeListFromAddFile();
                addAllTypeToCanvas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //this.run_file = null;
            try
            {
                isScrollClick = false;
                if (isDeleteItem)
                {
                    isFileCheck = true;
                    if (isDocItem && (clickDocItem >= 0))
                    {
                        File.Delete(docList[clickDocItem].getFIlePath());
                    }

                    if (isIMGItem && (clickImgItem >= 0))
                    {
                        File.Delete(imageList[clickImgItem].getFIlePath());

                    }

                    if (isMusicItem && (clickMusItem >= 0))
                    {
                        File.Delete(musicList[clickMusItem].getFIlePath());
                        for (int i = 0; i < this.musicList.Count; i++)
                             music_list.Children.Remove(musicList[i]);
                        this.musicList.Remove(musicList[clickMusItem]);

                    }

                    if (isVideoItem && (clickVidItem >= 0))
                    {
                        File.Delete(videoList[clickVidItem].getFIlePath());

                    }

                    if (isMiscItem && (clickMisItem >= 0))
                    {
                        File.Delete(miscList[clickMisItem].getFIlePath());
                    }
                }
                else
                {

                    if (isDocItem && (clickDocItem >= 0))
                    {
                        Point pos = e.GetPosition(this.doc_list);
                        checkDocPosChange(pos, clickDocItem);
                    }                    

                    if (isIMGItem && (clickImgItem >= 0))
                    {
                        Point pos = e.GetPosition(pic_list);
                        checkIMGPosChange(pos, clickImgItem);
                    }

                    if (isMusicItem && (clickMusItem >= 0))
                    {
                        Point pos = e.GetPosition(this.music_list);
                        checkMusicPosChange(pos, clickMusItem);
                    }

                    if (isVideoItem && (clickVidItem >= 0))
                    {
                        Point pos = e.GetPosition(this.movie_list);
                        checkVIDEOPosChange(pos, clickVidItem);
                    }

                    if (isMiscItem && (clickMisItem >= 0))
                    {
                        Point pos = e.GetPosition(misc_list);
                        checkMiscPosChange(pos, clickMisItem);
                    }



                    isFileCheck = false;
                    this.setUsbLabel();
                    clean_All_ListItme();
                    getAllTypeList();
                    addAllTypeToCanvas();
                    return;
                }
                isFileCheck = false;
                
                this.setUsbLabel();
            }
            catch (Exception ex)
            {
                isFileCheck = false;
                ex.ToString();
            }
            clean_All_ListItme();
            this.getAllTypeListFromAddFile();
            //getAllTypeList();
            addAllTypeToCanvas();
        }

        private bool checkDocPosChange(Point pos, int movingItem)
        {
            if ((pos.X > 0) && (pos.X < 120) && (pos.Y > 0) && (pos.Y < 450))
            {

                int pos_index = Convert.ToInt32(pos.Y / 25);
                if (pos_index > docList.Count)
                {
                    pos_index = docList.Count - 1;
                }
                List<fileItem> docBufferList = docList;
                docList = new List<fileItem>();
                for (int i = 0; i < docBufferList.Count; i++)
                {
                    docBufferList[i].set_deselect_item();
                }
                this.clickDocItem = pos_index;
                if (movingItem > pos_index)
                {
                    for (int i = 0; i < pos_index; i++)
                    {
                        docList.Add(docBufferList[i]);
                    }
                    docList.Add(docBufferList[movingItem]);
                    for (int i = pos_index; i < movingItem; i++)
                    {
                        docList.Add(docBufferList[i]);
                    }
                    for (int i = movingItem + 1; i < docBufferList.Count; i++)
                    {
                        docList.Add(docBufferList[i]);
                    }
                }
                else if (movingItem < pos_index)
                {
                    for (int i = 0; i < movingItem; i++)
                    {
                        docList.Add(docBufferList[i]);
                    }

                    for (int i = movingItem + 1; i < pos_index; i++)
                    {
                        docList.Add(docBufferList[i]);
                    }

                    docList.Add(docBufferList[movingItem]);

                    for (int i = pos_index; i < docBufferList.Count; i++)
                    {
                        docList.Add(docBufferList[i]);
                    }
                }
                else if (movingItem == pos_index)
                {
                    for (int i = 0; i < docBufferList.Count; i++)
                    {
                        docList.Add(docBufferList[i]);
                    }
                    //return true;
                }


                DateTime time = new DateTime(2010, 3, 16, 3, 31, 00);
                for (int i = 0; i < docList.Count; i++)
                {
                    int minutes_val = (i + 1) / 58 + 1;
                    int second_val = (i + 1) % 58 + 1;
                    time = new DateTime(2014, 1, minutes_val, 1, second_val, 0);
                    File.SetLastWriteTime(docList[i].getFIlePath(), time);
                }

                return true;


            }
            return false;
        }


        private bool checkIMGPosChange(Point pos, int movingItem)
        {
            if ((pos.X > 0) && (pos.X < 120) && (pos.Y > 0) && (pos.Y < 450))
            {

                int pos_index = Convert.ToInt32(pos.Y / 25);
                if (pos_index > imageList.Count)
                {
                    pos_index = imageList.Count - 1;
                }
                List<fileItem> imgBufferList = imageList;
                imageList = new List<fileItem>();
                for (int i = 0; i < imgBufferList.Count; i++)
                {
                    imgBufferList[i].set_deselect_item();
                }
                this.clickImgItem = pos_index;
                if (movingItem > pos_index)
                {
                    for (int i = 0; i < pos_index; i++)
                    {
                        imageList.Add(imgBufferList[i]);
                    }
                        imageList.Add(imgBufferList[movingItem]);
                    for (int i = pos_index; i < movingItem; i++)
                    {
                        imageList.Add(imgBufferList[i]);
                    }
                    for (int i = movingItem + 1; i < imgBufferList.Count; i++)
                    {
                        imageList.Add(imgBufferList[i]);
                    }
                }
                else if (movingItem < pos_index)
                {
                    for (int i = 0; i < movingItem; i++)
                    {
                        imageList.Add(imgBufferList[i]);
                    }

                    for (int i = movingItem + 1; i < pos_index; i++)
                    {
                        imageList.Add(imgBufferList[i]);
                    }

                        imageList.Add(imgBufferList[movingItem]);

                    for (int i = pos_index; i < imgBufferList.Count; i++)
                    {
                        imageList.Add(imgBufferList[i]);
                    }
                }
                else if (movingItem == pos_index)
                {
                    for (int i = 0; i < imgBufferList.Count; i++)
                    {
                        imageList.Add(imgBufferList[i]);
                    }
                    //return true;
                }


                DateTime time = new DateTime(2010, 3, 16, 3, 31, 00);
                for (int i = 0; i < imageList.Count; i++)
                {
                    int minutes_val = (i + 1) / 58 + 1;
                    int second_val = (i + 1) % 58 + 1;
                    time = new DateTime(2014, 1, minutes_val, 1, second_val, 0);
                    File.SetLastWriteTime(imageList[i].getFIlePath(), time);
                }

                return true;


            }
            return false;
        }

        private void checkVIDEOPosChange(Point pos, int movingItem)
        {
            if ((pos.X > 0) && (pos.X < 120) && (pos.Y > 0) && (pos.Y < 450))
            {

                int pos_index = Convert.ToInt32(pos.Y / 25);
                if (pos_index > this.videoList.Count)
                {
                    pos_index = videoList.Count - 1;
                }
                List<fileItem> videoBufferList = videoList;
                videoList = new List<fileItem>();
                for (int i = 0; i < videoBufferList.Count; i++)
                {
                    videoBufferList[i].set_deselect_item();
                }

                this.clickVidItem = pos_index;

                if (movingItem > pos_index)
                {
                    for (int i = 0; i < pos_index; i++)
                    {
                        videoList.Add(videoBufferList[i]);
                    }
                    videoList.Add(videoBufferList[movingItem]);
                    for (int i = pos_index; i < movingItem; i++)
                    {
                        videoList.Add(videoBufferList[i]);
                    }
                    for (int i = movingItem + 1; i < videoBufferList.Count; i++)
                    {
                        videoList.Add(videoBufferList[i]);
                    }
                }
                else if (movingItem < pos_index)
                {
                    for (int i = 0; i < movingItem; i++)
                    {
                        videoList.Add(videoBufferList[i]);
                    }

                    for (int i = movingItem + 1; i < pos_index; i++)
                    {
                        videoList.Add(videoBufferList[i]);
                    }
                        videoList.Add(videoBufferList[movingItem]);

                    for (int i = pos_index; i < videoBufferList.Count; i++)
                    {
                        videoList.Add(videoBufferList[i]);
                    }
                }
                else if (movingItem == pos_index)
                {
                    for (int i = 0; i < videoBufferList.Count; i++)
                    {
                        videoList.Add(videoBufferList[i]);
                    }
                    //return true;
                }


                DateTime time = new DateTime(2010, 3, 16, 3, 31, 00);
                for (int i = 0; i < videoList.Count; i++)
                {
                    int minutes_val = (i + 1) / 58 + 1;
                    int second_val = (i + 1) % 58 + 1;
                    time = new DateTime(2014, 1, minutes_val, 1, second_val, 0);
                    File.SetLastWriteTime(videoList[i].getFIlePath(), time);
                }

                return ;


            }
            return ;
        }


        private void checkMiscPosChange(Point pos, int movingItem)
        {
            if ((pos.X > 0) && (pos.X < 120) && (pos.Y > 0) && (pos.Y < 450))
            {

                int pos_index = Convert.ToInt32(pos.Y / 25);
                if (pos_index > this.miscList.Count)
                {
                    pos_index = miscList.Count - 1;
                }
                List<fileItem> miscBufferList = miscList;
                miscList = new List<fileItem>();
                for (int i = 0; i < miscBufferList.Count; i++)
                {
                    miscBufferList[i].set_deselect_item();
                }

                this.clickMisItem = pos_index;

                if (movingItem > pos_index)
                {
                    for (int i = 0; i < pos_index; i++)
                    {
                        miscList.Add(miscBufferList[i]);
                    }
                    miscList.Add(miscBufferList[movingItem]);
                    for (int i = pos_index; i < movingItem; i++)
                    {
                        miscList.Add(miscBufferList[i]);
                    }
                    for (int i = movingItem + 1; i < miscBufferList.Count; i++)
                    {
                        miscList.Add(miscBufferList[i]);
                    }
                }
                else if (movingItem < pos_index)
                {
                    for (int i = 0; i < movingItem; i++)
                    {
                        miscList.Add(miscBufferList[i]);
                    }

                    for (int i = movingItem + 1; i < pos_index; i++)
                    {
                        miscList.Add(miscBufferList[i]);
                    }
                    miscList.Add(miscBufferList[movingItem]);

                    for (int i = pos_index; i < miscBufferList.Count; i++)
                    {
                        miscList.Add(miscBufferList[i]);
                    }
                }
                else if (movingItem == pos_index)
                {
                    for (int i = 0; i < miscBufferList.Count; i++)
                    {
                        miscList.Add(miscBufferList[i]);
                    }
                    //return true;
                }


                DateTime time = new DateTime(2010, 3, 16, 3, 31, 00);
                for (int i = 0; i < miscList.Count; i++)
                {
                    int minutes_val = (i + 1) / 58 + 1;
                    int second_val = (i + 1) % 58 + 1;
                    time = new DateTime(2014, 1, minutes_val, 1, second_val, 0);
                    File.SetLastWriteTime(miscList[i].getFIlePath(), time);
                }

                return;
            }
            return;
        }


        private bool checkMusicPosChange(Point pos , int movingItem)
        {
            if((pos.X > 0) && (pos.X < 120) && (pos.Y > 0) && (pos.Y < 450))
            {

                int pos_index = Convert.ToInt32(pos.Y / 25);
                if (pos_index > musicList.Count)
                {
                    pos_index = musicList.Count - 1;
                }
                List<fileItem> musicBufferList = musicList;
                musicList = new List<fileItem>();
                for (int i = 0; i < musicBufferList.Count; i++)
                {
                    musicBufferList[i].set_deselect_item();
                }
                this.clickMusItem = pos_index;
                    if (movingItem > pos_index)
                    {
                        for (int i = 0; i < pos_index; i++)
                        {
                            musicList.Add(musicBufferList[i]);
                        }
                        musicList.Add(musicBufferList[movingItem]);
                        for (int i = pos_index; i < movingItem; i++)
                        {
                            musicList.Add(musicBufferList[i]);
                        }
                        for (int i = movingItem + 1; i < musicBufferList.Count; i++)
                        {
                            musicList.Add(musicBufferList[i]);
                        }
                    }
                    else if (movingItem < pos_index)
                    {
                        for (int i = 0; i < movingItem ; i++)
                        {
                            musicList.Add(musicBufferList[i]);
                        }
                       
                        for (int i = movingItem + 1; i < pos_index; i++)
                        {
                            musicList.Add(musicBufferList[i]);
                        }

                        musicList.Add(musicBufferList[movingItem]);

                        for (int i = pos_index ; i < musicBufferList.Count; i++)
                        {
                            musicList.Add(musicBufferList[i]);
                        }
                    }
                    else if (movingItem ==pos_index)
                    {
                        for (int i = 0; i < musicBufferList.Count; i++)
                        {
                            musicList.Add(musicBufferList[i]);
                        }
                        //return true;
                    }
                    //parent_win.setMusicInit();
                    DateTime time = new DateTime(2010, 3, 16, 3, 31, 00);
                    for (int i = 0; i < musicList.Count; i++)
                    {
                        int minutes_val = (i+1) / 58 + 1;
                        int second_val = (i+1) % 58 +1;
                        time = new DateTime(2014, 1, minutes_val, 1, second_val, 0);
                        File.SetLastWriteTime(musicList[i].getFIlePath(), time);
                    }

                return true;


            }
            return false;
        }


        private string[] getFilePathsOfMusic()
        {
            int music_Length = this.musicList.Count;
            string[] musicParentArray = new string[music_Length];// string[];
            for (int i = 0; i < musicParentArray.Length; i++)
            {
                musicParentArray[i] = musicList[i].getFIlePath();
            }

                return musicParentArray;
        }


        private void Canvas_MouseLeave(object sender, MouseEventArgs e)
        {

            if (isFileCheck)
                return;
            try
            {
                clean_All_ListItme();
                isScrollClick = false;
                getAllTypeList();
                addAllTypeToCanvas();
            }
            catch (Exception ex)
            {
            }

        }


        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {

            if (isFileCheck)
                return;

            //run_file = null;

            if (isDocItem && (clickDocItem >= 0))
            {
                Point pos = e.GetPosition(doc_list);
                if (Math.Abs(pos.Y- Canvas.GetTop(docList[clickDocItem])) > 15)
                {
                    Canvas.SetLeft(docList[clickDocItem], pos.X + 5);
                    Canvas.SetTop(docList[clickDocItem], pos.Y + 5);
                }
                
            }

            if (isIMGItem && (clickImgItem >= 0))
            {
                Point pos = e.GetPosition(pic_list);
                if (Math.Abs(pos.Y - Canvas.GetTop(imageList[clickImgItem])) > 15)
                {
                    Canvas.SetLeft(imageList[clickImgItem], pos.X + 5);
                    Canvas.SetTop(imageList[clickImgItem], pos.Y + 5);
                }
            }

            if (isMusicItem && (clickMusItem >= 0))
            {
                Point pos = e.GetPosition(music_list);
                if (Math.Abs(pos.Y - Canvas.GetTop(musicList[clickMusItem])) > 15)
                {
                    Canvas.SetLeft(musicList[clickMusItem], pos.X + 5);
                    Canvas.SetTop(musicList[clickMusItem], pos.Y + 5);
                }
            }

            if (isVideoItem && (clickVidItem >= 0))
            {
                Point pos = e.GetPosition(movie_list);
                if (Math.Abs(pos.Y - Canvas.GetTop(videoList[clickVidItem])) > 15)
                {
                    Canvas.SetLeft(videoList[clickVidItem], pos.X + 5);
                    Canvas.SetTop(videoList[clickVidItem], pos.Y + 5);
                }
            }

            if (isMiscItem && (clickMisItem >= 0))
            {
                Point pos = e.GetPosition(misc_list);
                if (Math.Abs(pos.Y - Canvas.GetTop(miscList[clickMisItem])) > 15)
                {
                    Canvas.SetLeft(miscList[clickMisItem], pos.X + 5);
                    Canvas.SetTop(miscList[clickMisItem], pos.Y + 5);
                }
            }

            double pos_off = 0;
            if ((clickDocItem >= 0) && (isScrollClick))
            {
                Point pos = e.GetPosition(scroll_container);
                if ((pos.Y + this.scroll_bar.Height) > 460)
                {
                    pos_off = 450 - scroll_bar.Height;
                    Canvas.SetTop(scroll_bar, (460 - scroll_bar.Height));
                }
                else if (pos.Y < 10)
                {
                    Canvas.SetTop(scroll_bar, 10);
                    pos_off = 0;
                }
                else
                {
                    Canvas.SetTop(scroll_bar, pos.Y);
                    pos_off = pos.Y - 10;
                }
                this.Doc_first_num = Convert.ToInt32(this.docList.Count * pos_off / 450);
                for (int i = 0; i < docList.Count; i++)
                    doc_list.Children.Remove(docList[i]);
                this.setDOcList();
            }

            if ((clickImgItem >= 0) && (isScrollClick))
            {
                Point pos = e.GetPosition(scroll_container);
                if ((pos.Y + this.scroll_bar.Height) > 460)
                {
                    pos_off = 450 - scroll_bar.Height;
                    Canvas.SetTop(scroll_bar, (460 - scroll_bar.Height));
                }
                else if ((pos.Y) < 10)
                {
                    pos_off = 0;
                    Canvas.SetTop(scroll_bar, 10);
                }
                else
                {
                    pos_off = pos.Y - 10 ;
                    Canvas.SetTop(scroll_bar, pos.Y);
                }
                this.Img_first_num = Convert.ToInt32(this.imageList.Count * pos_off / 450);
                for (int i = 0; i < imageList.Count; i++)
                    doc_list.Children.Remove(imageList[i]);
                this.setImgList();
            }


            if ((clickMusItem >= 0) && (isScrollClick))
            {
                Point pos = e.GetPosition(scroll_container);
                if ((pos.Y + this.scroll_bar.Height) > 460)
                {
                    pos_off = 450 - scroll_bar.Height;
                    Canvas.SetTop(scroll_bar, (460 - scroll_bar.Height));
                }
                else if ((pos.Y < 10))
                {
                    pos_off = 0;
                    Canvas.SetTop(scroll_bar, 10);
                }
                else
                {
                    pos_off = pos.Y - 10;
                    Canvas.SetTop(scroll_bar, pos.Y);
                }

                this.Mus_first_num = Convert.ToInt32(this.musicList.Count * pos_off / 450);
                for (int i = 0; i < this.musicList.Count; i++)
                    this.music_list.Children.Remove(musicList[i]);
                this.setMusicList();

            }


            if ((clickVidItem >= 0) && (isScrollClick))
            {
                Point pos = e.GetPosition(scroll_container);
                if ((pos.Y + this.scroll_bar.Height) > 460)
                {
                    pos_off = 450 - scroll_bar.Height;
                    Canvas.SetTop(scroll_bar, (460 - scroll_bar.Height));
                }
                else if ((pos.Y) < 10)
                {
                    pos_off = 0;
                    Canvas.SetTop(scroll_bar, 10);
                }
                else
                {
                    pos_off = pos.Y - 10 ;
                    Canvas.SetTop(scroll_bar, pos.Y);
                }
                this.Vid_first_num = Convert.ToInt32(this.videoList.Count * pos_off / 450);
                for (int i = 0; i < this.videoList.Count; i++)
                    this.music_list.Children.Remove(videoList[i]);
                this.setVideoList();
            }


            if ((clickMisItem >= 0) && (isScrollClick))
            {
                Point pos = e.GetPosition(scroll_container);
                if ((pos.Y + this.scroll_bar.Height) > 460)
                {
                    pos_off = 450 - scroll_bar.Height;
                    Canvas.SetTop(scroll_bar, (460 - scroll_bar.Height));
                }
                else if ((pos.Y) < 10)
                {
                    pos_off = 0;
                    Canvas.SetTop(scroll_bar, 10);
                }
                else
                {
                    pos_off = pos.Y - 10 ;
                    Canvas.SetTop(scroll_bar, pos.Y);
                }

                this.Mis_first_num = Convert.ToInt32(this.miscList.Count * pos_off / 450);
                for (int i = 0; i < this.miscList.Count; i++)
                    this.music_list.Children.Remove(miscList[i]);
                this.setMiscList();
            }

            if ((e.GetPosition(this).X < 10) || (e.GetPosition(this).X > (this.Width - 20)) || (e.GetPosition(this).Y < 10) || (e.GetPosition(this).Y > (this.Height - 20)))
                resetItems();

        }

        private void checkClickDocsItem(object sender, MouseButtonEventArgs e)
        {
            if (isFileCheck)
                return;

            

            try
            {
                fileItem clickItem = (fileItem)sender;
                //Point position = clickItem.PointToScreen(new Point(0d, 0d));
                setAllDeselect();
                if (click_item != null)
                    click_item.set_deselect_item();
                click_item = clickItem;
                click_item.set_select_item();
                Canvas.SetZIndex(clickItem, order);
                order++;
                for (int i = 0; i < this.docList.Count; i++)
                {
                    if (clickItem == docList[i])
                    {
                        this.clickDocItem = i;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }


        private fileItem click_item;

        private void checkClickImgsItem(object sender, MouseButtonEventArgs e)
        {
            if (isFileCheck)
                return;
            try
            {
                fileItem clickItem = (fileItem)sender;
                setAllDeselect();
                if (click_item != null)
                    click_item.set_deselect_item();
                click_item = clickItem;
                click_item.set_select_item();
                Canvas.SetZIndex(clickItem, order);
                order++;
                for (int i = 0; i < this.imageList.Count; i++)
                {
                    if (clickItem == imageList[i])
                    {
                        this.clickImgItem = i;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void checkClickMuicsItem(object sender, MouseButtonEventArgs e)
        {
            if (isFileCheck)
                return;
            try
            {
                fileItem clickItem = (fileItem)sender;
                setAllDeselect();
                if (click_item != null)
                    click_item.set_deselect_item();
                setAllDeselect();
                click_item = clickItem;
                click_item.set_select_item();
                Canvas.SetZIndex(clickItem, order);
                order++;
                for (int i = 0; i < this.musicList.Count; i++)
                {
                    if (clickItem == musicList[i])
                    {
                        this.clickMusItem = i;
                    }
                }
            }

            catch (Exception ex)
            {

            }
        }


        private void setAllDeselect()
        {

            clickDocItem = -1;
            clickImgItem = -1;
            clickMusItem = -1;
            clickVidItem = -1;
            clickMisItem = -1;

            for (int i = 0; i < this.musicList.Count; i++)            
                musicList[i].set_deselect_item();
            for (int i = 0; i < this.docList.Count; i++)
                docList[i].set_deselect_item();
            for (int i = 0; i < this.imageList.Count; i++)
                imageList[i].set_deselect_item();
            for (int i = 0; i < this.miscList.Count; i++)
                miscList[i].set_deselect_item();
            for (int i = 0; i < this.videoList.Count; i++)
                videoList[i].set_deselect_item();
            
        }

        private void checkClickVideosItem(object sender, MouseButtonEventArgs e)
        {
            if (isFileCheck)
                return;
            try
            {
                fileItem clickItem = (fileItem)sender;
                setAllDeselect();
                if (click_item != null)
                    click_item.set_deselect_item();
                click_item = clickItem;
                click_item.set_select_item();
                Canvas.SetZIndex(clickItem, order);
                order++;
                for (int i = 0; i < this.videoList.Count; i++)
                {
                    if (clickItem == videoList[i])
                    {
                        this.clickVidItem = i;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void checkClickMiscItem(object sender, MouseButtonEventArgs e)
        {
            if (isFileCheck)
                return;
            try
            {
                fileItem clickItem = (fileItem)sender;
                setAllDeselect();
                if (click_item != null)
                    click_item.set_deselect_item();
                click_item = clickItem;
                click_item.set_select_item();
                if (click_item != null)
                    click_item.set_deselect_item();
                click_item = clickItem;
                click_item.set_select_item();
                Canvas.SetZIndex(clickItem, order);
                order++;
                for (int i = 0; i < this.miscList.Count; i++)
                {
                    if (clickItem == miscList[i])
                    {
                        this.clickMisItem = i;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void doc_list_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (isFileCheck)
                return;
            try
            {
                clean_List_Itme();
                isDocItem = true;
                if (docList.Count > 18)
                {
                    scroll_bar.Visibility = System.Windows.Visibility.Visible;
                    scroll_bar.Height = 450 * 18 / (docList.Count);
                    Color clr = Color.FromRgb(40, 40, 40);
                    scroll_bar.Background = new SolidColorBrush(clr);
                }
                else
                    scroll_bar.Visibility = System.Windows.Visibility.Hidden;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void pic_list_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (isFileCheck)
                return;
            try
            {
                clean_List_Itme();
                this.isIMGItem = true;
                if (imageList.Count > 18)
                {
                    scroll_bar.Visibility = System.Windows.Visibility.Visible;
                    scroll_bar.Height = 450 * 18 / imageList.Count;
                    Color clr = Color.FromRgb(40, 40, 40);
                    scroll_bar.Background = new SolidColorBrush(clr);
                }
                else
                    scroll_bar.Visibility = System.Windows.Visibility.Hidden;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }


        private void music_list_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (isFileCheck)
                return;
            try
            {
                clean_List_Itme();
                this.isMusicItem = true;
                if (musicList.Count > 18)
                {
                    scroll_bar.Visibility = System.Windows.Visibility.Visible;
                    scroll_bar.Height = 450 * 18 / (musicList.Count);
                    Color clr = Color.FromRgb(40, 40, 40);
                    scroll_bar.Background = new SolidColorBrush(clr);
                }
                else
                    scroll_bar.Visibility = System.Windows.Visibility.Hidden;

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }


        private void movie_list_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (isFileCheck)
                return;
            try
            {
                clean_List_Itme();
                this.isVideoItem = true;
                if (videoList.Count > 18)
                {
                    scroll_bar.Visibility = System.Windows.Visibility.Visible;
                    scroll_bar.Height = 450 * 18 / (videoList.Count);
                    Color clr = Color.FromRgb(40, 40, 40);
                    scroll_bar.Background = new SolidColorBrush(clr);
                }
                else
                    scroll_bar.Visibility = System.Windows.Visibility.Hidden;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }


        private void misc_list_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (isFileCheck)
                return;
            try
            {
                clean_List_Itme();
                this.isMiscItem = true;
                if (this.miscList.Count > 18)
                {
                    scroll_bar.Height = 450 * 18 / (miscList.Count);
                    Color clr = Color.FromRgb(40, 40, 40);
                    scroll_bar.Background = new SolidColorBrush(clr);
                    scroll_bar.Visibility = System.Windows.Visibility.Visible;
                    //scroll_img.Height = 480 * 18 / (miscList.Count);
                }
                else
                    scroll_bar.Visibility = System.Windows.Visibility.Hidden;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void clean_List_Itme()
        {
            isDocItem = false;
            isIMGItem = false;
            isMusicItem = false;
            isVideoItem = false;
            isMiscItem = false;
        }


        private void clean_All_ListItme()
        {
            for (int i = 0; i < docList.Count; i++)
                doc_list.Children.Remove(docList[i]);
            for (int i = 0; i < this.imageList.Count; i++)
                pic_list.Children.Remove(imageList[i]);
            for (int i = 0; i < this.musicList.Count; i++)
                music_list.Children.Remove(musicList[i]);
            for (int i = 0; i < this.videoList.Count; i++)
                movie_list.Children.Remove(this.videoList[i]);
            for (int i = 0; i < this.miscList.Count; i++)
                misc_list.Children.Remove(this.miscList[i]);

            isDocItem = false;
            isIMGItem = false;
            isMusicItem = false;
            isVideoItem = false;
            isMiscItem = false;
        }


        private void resetItems()
        {
            clean_All_ListItme();
            getAllTypeList();
            addAllTypeToCanvas();
        }


        private void init_ALL_LIST()
        {
            Canvas.SetZIndex(doc_list, 0);
            Canvas.SetZIndex(pic_list, 0);
            Canvas.SetZIndex(music_list, 0);
            Canvas.SetZIndex(movie_list, 0);
            Canvas.SetZIndex(misc_list, 0);
        }


        private void doc_list_MouseEnter(object sender, MouseEventArgs e)
        {
            if (isFileCheck)
                return;

            Uri uri = new Uri("Images/usb/docThmb-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            doc_top.Source = bitmap;
            init_ALL_LIST();
            Canvas.SetZIndex(doc_list, 1000);
            doc_title.Visibility = System.Windows.Visibility.Visible;
        }

        private void pic_list_MouseEnter(object sender, MouseEventArgs e)
        {
            if (isFileCheck)
                return;
            Uri uri = new Uri("Images/usb/picThumb-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            pic_top.Source = bitmap;

            init_ALL_LIST();
            Canvas.SetZIndex(pic_list, 1000);
            img_title.Visibility = System.Windows.Visibility.Visible;
        }

        private void music_list_MouseEnter(object sender, MouseEventArgs e)
        {
            if (isFileCheck)
                return;
            Uri uri = new Uri("Images/usb/musicThumb-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            music_top.Source = bitmap;

            init_ALL_LIST();
            Canvas.SetZIndex(music_list, 1000);
            music_title.Visibility = System.Windows.Visibility.Visible;
        }

        private void movie_list_MouseEnter(object sender, MouseEventArgs e)
        {
            if (isFileCheck)
                return;
            Uri uri = new Uri("Images/usb/moviThumb-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            movies_top.Source = bitmap;

            init_ALL_LIST();
            Canvas.SetZIndex(movie_list, 1000);
            movie_title.Visibility = System.Windows.Visibility.Visible;
        }

        private void misc_list_MouseEnter(object sender, MouseEventArgs e)
        {
            if (isFileCheck)
                return;
            Uri uri = new Uri("Images/usb/miscThumb-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            misc_top.Source = bitmap;
            init_ALL_LIST();
            Canvas.SetZIndex(misc_list, 1000);
            misc_title.Visibility = System.Windows.Visibility.Visible;
        }


        private bool isScrollClick = false;
        private void scroll_bar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isScrollClick = true;
        }

        private void scroll_bar_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isScrollClick = false;
        }

        private void Canvas_MouseLeave_1(object sender, MouseEventArgs e)
        {
            isScrollClick = false;
        }

        private void Canvas_MouseMove_1(object sender, MouseEventArgs e)
        {

        }

        private void scroll_container_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (isFileCheck)
                return;
            try
            {
                double pos_off = 0;
                if ((clickDocItem >= 0))
                {
                    Point pos = e.GetPosition(scroll_container);
                    if ((pos.Y + this.scroll_bar.Height) > 460)
                    {
                        pos_off = 450 - scroll_bar.Height;
                        Canvas.SetTop(scroll_bar, (460 - scroll_bar.Height));
                    }
                    else if (pos.Y < 10)
                    {
                        Canvas.SetTop(scroll_bar, 10);
                        pos_off = 0;
                    }
                    else
                    {
                        Canvas.SetTop(scroll_bar, pos.Y);
                        pos_off = pos.Y - 10;
                    }
                    this.Doc_first_num = Convert.ToInt32(this.docList.Count * pos_off / 450);
                    for (int i = 0; i < docList.Count; i++)
                        doc_list.Children.Remove(docList[i]);
                    this.setDOcList();
                }

                if ((clickImgItem >= 0))
                {
                    Point pos = e.GetPosition(scroll_container);
                    if ((pos.Y + this.scroll_bar.Height) > 460)
                    {
                        pos_off = 450 - scroll_bar.Height;
                        Canvas.SetTop(scroll_bar, (460 - scroll_bar.Height));
                    }
                    else if ((pos.Y) < 10)
                    {
                        pos_off = 0;
                        Canvas.SetTop(scroll_bar, 10);
                    }
                    else
                    {
                        pos_off = pos.Y - 10;
                        Canvas.SetTop(scroll_bar, pos.Y);
                    }
                    this.Img_first_num = Convert.ToInt32(this.imageList.Count * pos_off / 450);
                    for (int i = 0; i < imageList.Count; i++)
                        doc_list.Children.Remove(imageList[i]);
                    this.setImgList();
                }


                if ((clickMusItem >= 0))
                {
                    Point pos = e.GetPosition(scroll_container);
                    if ((pos.Y + this.scroll_bar.Height) > 460)
                    {
                        pos_off = 450 - scroll_bar.Height;
                        Canvas.SetTop(scroll_bar, (460 - scroll_bar.Height));
                    }
                    else if ((pos.Y < 10))
                    {
                        pos_off = 0;
                        Canvas.SetTop(scroll_bar, 10);
                    }
                    else
                    {
                        pos_off = pos.Y - 10;
                        Canvas.SetTop(scroll_bar, pos.Y);
                    }

                    this.Mus_first_num = Convert.ToInt32(this.musicList.Count * pos_off / 450);
                    for (int i = 0; i < this.musicList.Count; i++)
                        this.music_list.Children.Remove(musicList[i]);
                    this.setMusicList();

                }


                if ((clickVidItem >= 0))
                {
                    Point pos = e.GetPosition(scroll_container);
                    if ((pos.Y + this.scroll_bar.Height) > 460)
                    {
                        pos_off = 450 - scroll_bar.Height;
                        Canvas.SetTop(scroll_bar, (460 - scroll_bar.Height));
                    }
                    else if ((pos.Y) < 10)
                    {
                        pos_off = 0;
                        Canvas.SetTop(scroll_bar, 10);
                    }
                    else
                    {
                        pos_off = pos.Y - 10;
                        Canvas.SetTop(scroll_bar, pos.Y);
                    }
                    this.Vid_first_num = Convert.ToInt32(this.videoList.Count * pos_off / 450);
                    for (int i = 0; i < this.videoList.Count; i++)
                        this.music_list.Children.Remove(videoList[i]);
                    this.setVideoList();
                }


                if ((clickMisItem >= 0))
                {
                    Point pos = e.GetPosition(scroll_container);
                    if ((pos.Y + this.scroll_bar.Height) > 460)
                    {
                        pos_off = 450 - scroll_bar.Height;
                        Canvas.SetTop(scroll_bar, (460 - scroll_bar.Height));
                    }
                    else if ((pos.Y) < 10)
                    {
                        pos_off = 0;
                        Canvas.SetTop(scroll_bar, 10);
                    }
                    else
                    {
                        pos_off = pos.Y - 10;
                        Canvas.SetTop(scroll_bar, pos.Y);
                    }

                    this.Mis_first_num = Convert.ToInt32(this.miscList.Count * pos_off / 450);
                    for (int i = 0; i < this.miscList.Count; i++)
                        this.music_list.Children.Remove(miscList[i]);
                    this.setMiscList();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void Canvas_MouseEnter(object sender, MouseEventArgs e)
        {
            if (isFileCheck)
                return;
            Uri uri = new Uri("Images/usb/binDown.png", UriKind.Relative);
            BitmapImage bitmap = new BitmapImage(uri);
            bin_folder.Source = bitmap;
            //bin_bottom.Visibility = System.Windows.Visibility.Visible;
            isDeleteItem = true;
        }

        private void Canvas_MouseLeave_2(object sender, MouseEventArgs e)
        {
            if (isFileCheck)
                return;
            Uri uri = new Uri("Images/usb/bin.png", UriKind.Relative);
            BitmapImage bitmap = new BitmapImage(uri);
            bin_folder.Source = bitmap;
            //bin_bottom.Visibility = System.Windows.Visibility.Hidden;
            isDeleteItem = false;
        }


        /******
         * 
         * Progress bar
         * 
         * *******/

        private String used_SizeLB = "";
        private String used_RateLB = "";

        private void setUsbLabel()
        {
            double used_Size = 0;
            double used_Rate = 0;
            UsbUtils us = new UsbUtils();
            us.getUSBDriver();
            used_Size = us.getUSBSize();
            used_SizeLB = used_Size.ToString();
            int index = used_SizeLB.IndexOf('.');
            if (index > 0)
                used_SizeLB = used_SizeLB.Substring(0, index + 2);
            else
                used_SizeLB += ".0";

            used_SizeLB += "GB";

            used_Rate = us.getUsedSize() / us.getUSBSize() * 100;
            if (used_Rate < 30)
            {
                usb_per.Padding = new Thickness(3, 5, 0, 0);
                usb_per.Width = 30;
            }
            else
            {
                usb_per.Padding = new Thickness(used_Rate - 30, 5, 0, 0);
                usb_per.Width = used_Rate;
            }
            used_RateLB = used_Rate.ToString();
            index = used_RateLB.IndexOf('.');
            if (index > 0)
                used_RateLB = used_RateLB.Substring(0, index);
            used_RateLB += "%";
            usb_per.Text = used_RateLB;
            usb_totalSize.Text = used_SizeLB;
        }

        private void Canvas_MouseEnter_1(object sender, MouseEventArgs e)
        {
            if (isFileCheck)
                return;
            Uri uri = new Uri("Images/usb/progUp.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            //usb_size.Source = bitmap;
            //prog_lb.Content = used_RateLB;
        }

        private void Canvas_MouseLeave_3(object sender, MouseEventArgs e)
        {
            if (isFileCheck)
                return;
            Uri uri = new Uri("Images/usb/prog.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            //usb_size.Source = bitmap;
            //prog_lb.Content = used_SizeLB;
        }

        private void doc_list_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isFileCheck)
                return;
            Uri uri = new Uri("Images/usb/docThmb.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            doc_top.Source = bitmap;
            doc_title.Visibility = System.Windows.Visibility.Hidden;
        }

        private void doc_top_MouseEnter(object sender, MouseEventArgs e)
        {
            if (isFileCheck)
                return;
            Uri uri = new Uri("Images/usb/docThmb-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            doc_top.Source = bitmap;
            doc_title.Visibility = System.Windows.Visibility.Visible;
        }

        private void doc_top_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isFileCheck)
                return;
            Uri uri = new Uri("Images/usb/docThmb.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            doc_top.Source = bitmap;
            doc_title.Visibility = System.Windows.Visibility.Hidden;
        }

        private void pic_list_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isFileCheck)
                return;
            Uri uri = new Uri("Images/usb/picThumb.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            pic_top.Source = bitmap;
            img_title.Visibility = System.Windows.Visibility.Hidden;
        }

        private void music_list_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isFileCheck)
                return;
            Uri uri = new Uri("Images/usb/musicThumb.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            music_top.Source = bitmap;
            music_title.Visibility = System.Windows.Visibility.Hidden;
        }

        private void movie_list_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isFileCheck)
                return;
            Uri uri = new Uri("Images/usb/moviThumb.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            movies_top.Source = bitmap;
            movie_title.Visibility = System.Windows.Visibility.Hidden;
        }

        private void misc_list_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isFileCheck)
                return;
            Uri uri = new Uri("Images/usb/miscThumb.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            misc_top.Source = bitmap;
            misc_title.Visibility = System.Windows.Visibility.Hidden;
        }

        private void pic_top_MouseEnter(object sender, MouseEventArgs e)
        {
            if (isFileCheck)
                return;
            Uri uri = new Uri("Images/usb/picThumb-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            pic_top.Source = bitmap;
            img_title.Visibility = System.Windows.Visibility.Visible;
        }

        private void pic_top_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isFileCheck)
                return;
            Uri uri = new Uri("Images/usb/picThumb.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            pic_top.Source = bitmap;
            img_title.Visibility = System.Windows.Visibility.Hidden;
        }

        private void music_top_MouseEnter(object sender, MouseEventArgs e)
        {
            if (isFileCheck)
                return;
            Uri uri = new Uri("Images/usb/musicThumb-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            music_top.Source = bitmap;
            music_title.Visibility = System.Windows.Visibility.Visible;
        }

        private void music_top_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isFileCheck)
                return;
            Uri uri = new Uri("Images/usb/musicThumb.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            music_top.Source = bitmap;
            music_title.Visibility = System.Windows.Visibility.Hidden;
        }

        private void movies_top_MouseEnter(object sender, MouseEventArgs e)
        {
            if (isFileCheck)
                return;
            Uri uri = new Uri("Images/usb/moviThumb-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            movies_top.Source = bitmap;
            movie_title.Visibility = System.Windows.Visibility.Visible;
        }

        private void movies_top_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isFileCheck)
                return;
            Uri uri = new Uri("Images/usb/moviThumb.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            movies_top.Source = bitmap;
            movie_title.Visibility = System.Windows.Visibility.Hidden;
        }

        private void misc_top_MouseEnter(object sender, MouseEventArgs e)
        {
            if (isFileCheck)
                return;
            Uri uri = new Uri("Images/usb/miscThumb-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            misc_top.Source = bitmap;
            misc_title.Visibility = System.Windows.Visibility.Visible;
        }

        private void misc_top_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isFileCheck)
                return;
            Uri uri = new Uri("Images/usb/miscThumb.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            misc_top.Source = bitmap;
            misc_title.Visibility = System.Windows.Visibility.Hidden;
        }

        private void scroll_bar_MouseEnter(object sender, MouseEventArgs e)
        {
            if (isFileCheck)
                return;
            Color clr = Color.FromRgb(122, 157, 146);
            scroll_bar.Background = new SolidColorBrush(clr);
        }

        private void scroll_bar_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isFileCheck)
                return;
            Color clr = Color.FromRgb(40, 40, 40);
            scroll_bar.Background = new SolidColorBrush(clr);
        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void usbClose_MouseEnter(object sender, MouseEventArgs e)
        {
            //Uri uri = new Uri("Images/usb/closeDown.png", UriKind.Relative);
            //BitmapImage bitmap = new BitmapImage(uri);
            //usbClose.Source = bitmap;
        }

        private void usbClose_MouseLeave(object sender, MouseEventArgs e)
        {
            //Uri uri = new Uri("Images/usb/closeUp.png", UriKind.Relative);
            //BitmapImage bitmap = new BitmapImage(uri);
            //usbClose.Source = bitmap;
        }

        private void usb_per_MouseEnter(object sender, MouseEventArgs e)
        {
            if (isFileCheck)
                return;

            Uri uri = new Uri("Images/usb/proCanvas_over.png", UriKind.Relative);
            BitmapImage bitmap = new BitmapImage(uri);
            proBack.Source = bitmap;
        }

        private void Canvas_MouseLeave_4(object sender, MouseEventArgs e)
        {
            if (isFileCheck)
                return;
            Uri uri = new Uri("Images/usb/proCanvas.png", UriKind.Relative);
            BitmapImage bitmap = new BitmapImage(uri);
            proBack.Source = bitmap;
        }

        private void doc_title_MouseEnter(object sender, MouseEventArgs e)
        {
            Uri uri = new Uri("Images/usb/docThmb-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            doc_top.Source = bitmap;
            doc_title.Visibility = System.Windows.Visibility.Visible;
        }

        private void img_title_MouseEnter(object sender, MouseEventArgs e)
        {
            Uri uri = new Uri("Images/usb/picThumb-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            pic_top.Source = bitmap;
            img_title.Visibility = System.Windows.Visibility.Visible;
        }

        private void music_title_MouseEnter(object sender, MouseEventArgs e)
        {
            Uri uri = new Uri("Images/usb/musicThumb-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            music_top.Source = bitmap;
            music_title.Visibility = System.Windows.Visibility.Visible;
        }

        private void movie_title_MouseEnter(object sender, MouseEventArgs e)
        {
            Uri uri = new Uri("Images/usb/moviThumb-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            movies_top.Source = bitmap;
            movie_title.Visibility = System.Windows.Visibility.Visible;
        }

        private void misc_title_MouseEnter(object sender, MouseEventArgs e)
        {
            Uri uri = new Uri("Images/usb/miscThumb-over.png", UriKind.Relative);
            var bitmap = new BitmapImage(uri);
            misc_top.Source = bitmap;
            misc_title.Visibility = System.Windows.Visibility.Visible;
        }



    }
}
