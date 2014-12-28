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
using System.Net;
using System.IO;
using System.Xml;
using System.ComponentModel;
using System.Threading;
using System.Windows.Threading;
using System.Net.NetworkInformation;
//using System.Threading.Tasks;
//using OpenVpnLib;
using System.Runtime.InteropServices;
using System.Windows.Threading;
using System.Reflection;
using System.Windows.Media.Animation;
using System.Globalization;

namespace chromeBrowser
{
    /// <summary>
    /// Interaction logic for vpnWin.xaml
    /// </summary>
    public partial class vpnWin : Window
    {

        private MainWindow par_win;

        public vpnWin()
        {
            InitializeComponent();
            this.Top = 56;
            //this.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
            this.Left = (System.Windows.SystemParameters.PrimaryScreenWidth - 650) / 2;
            this.Closed +=new EventHandler(vpnWin_Closed);
            this.Activated +=new System.EventHandler(vpnActiveWindow);
            this.Deactivated += new EventHandler(vpnWin_Deactivated);
            IconHelper.RemoveIcon(this);
        }

        


        public void setVpnSetting(bool isCon)
        {
            if (isCon)
            {
                isConnect = true;
                disconnect.Visibility = System.Windows.Visibility.Hidden;
                //conecting.Visibility = System.Windows.Visibility.Hidden;
                connecting_gif.Visibility = System.Windows.Visibility.Hidden;
                connecting_gif.StopAnimate();
                connecting_txt.Visibility = System.Windows.Visibility.Hidden;
                connected.Visibility = System.Windows.Visibility.Visible;
                //conBTN.Visibility = System.Windows.Visibility.Hidden; // = true;
                disconBTN.Visibility = System.Windows.Visibility.Visible;
                conBTN.Visibility = System.Windows.Visibility.Hidden;
                this.ip_address.Text = par_win.getVpnTXT();
                this.location_txt.Text = par_win.getVPN_City(); ;
            }
            else
            {
                isConnect = false;
                disconnect.Visibility = System.Windows.Visibility.Visible;
                //conecting.Visibility = System.Windows.Visibility.Hidden;
                connecting_gif.Visibility = System.Windows.Visibility.Hidden;
                connecting_gif.StopAnimate();
                connecting_txt.Visibility = System.Windows.Visibility.Hidden;
                connected.Visibility = System.Windows.Visibility.Hidden;
                //conBTN.Visibility = System.Windows.Visibility.Hidden; // = true;
                disconBTN.Visibility = System.Windows.Visibility.Hidden;
                conBTN.Visibility = System.Windows.Visibility.Visible;
            }
        }


        public void closeAllThead()
        {

            if (checkProgressTimer != null)
                checkProgressTimer.Stop();
            /*if (checkConnectionTimer != null)
            {
                checkConnectionTimer.Stop();
                UsbUtils usbUTILS = new UsbUtils();
                String usbnamespace = usbUTILS.getUSBDriver();
                if (usbnamespace.Length > 0)
                {
                    String folder_name = System.IO.Path.Combine(usbnamespace, "secure");
                    String path = System.IO.Path.Combine(folder_name, "OpenVpnLib.dll");
                    var DLL = Assembly.LoadFile(path);

                    var theType = DLL.GetType("OpenVpnLib.Connector");
                    if (theType != null)
                    {
                        var con = Activator.CreateInstance(theType);
                        var method = theType.GetMethod("Disconnect");
                        method.Invoke(con, new object[] { folder_name });
                    }
                }

            }*/

            if (usbcheckThread != null)
            {
                usbcheckThread.Abort();
                usbcheckThread = null;
            }
        }

        public void vpnActiveWindow(object sender, EventArgs e)
        {
            //this.Topmost = true;
        }

        public void vpnWin_Deactivated(object sender, EventArgs e)
        {
            if (isConCheck)
                return;
            try
            {
                if (checkProgressTimer != null)
                    checkProgressTimer.Stop();

                if (usbcheckThread != null)
                {
                    usbcheckThread.Abort();
                    usbcheckThread = null;
                }
                this.Close();
            }
            catch (Exception ex)
            {
            }
        }

        public void vpnWin_Closed(object sender, EventArgs e)
        {
            if (checkProgressTimer != null)
                checkProgressTimer.Stop();

            if (usbcheckThread != null)
            {
                usbcheckThread.Abort();
                usbcheckThread = null; 
            }




        }

        public void LoadDllFile(string dllfolder, string libname)
        {
            var currentpath = new StringBuilder(255);
            UnsafeNativeMethods.GetDllDirectory(currentpath.Length, currentpath);

            // use new path
            UnsafeNativeMethods.SetDllDirectory(dllfolder);

            UnsafeNativeMethods.LoadLibrary(libname);

            // restore old path
            UnsafeNativeMethods.SetDllDirectory(currentpath.ToString());
        }

        public void setParent(MainWindow main_win)
        {
            par_win = main_win;
        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            //this.Close();

        }

        private void closeBTN_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            /*for (int i = 0; i < 1; ++i)
            {
                var tg = new TransformGroup();
                var translation = new TranslateTransform(30, 0);
                var translationName = "myTranslation" + translation.GetHashCode();
                RegisterName(translationName, translation);
                tg.Children.Add(translation);
                tg.Children.Add(new RotateTransform(0));
                this.RenderTransform = tg;

                //overCan.Children.Add(movingCan);

                var anim = new DoubleAnimation(56, -700, new Duration(new TimeSpan(0, 0, 0, 1, 0)))
                {
                    EasingFunction = new PowerEase { EasingMode = EasingMode.EaseOut }
                };

                var s = new Storyboard();
                Storyboard.SetTargetName(s, translationName);
                Storyboard.SetTargetProperty(s, new PropertyPath(TranslateTransform.XProperty));
                var storyboardName = "s" + s.GetHashCode();
                Resources.Add(storyboardName, s);

                s.Children.Add(anim);
                s.Begin();
            }*/

            if ( !isConCheck && !closeCompleted)
            {

                FormFadeOut.Begin();
               
            }

            //if (!isConCheck)
               // this.Close();
        }

        private bool closeCompleted = false;


        private void FormFadeOut_Completed(object sender, EventArgs e)
        {
            closeCompleted = true;
            this.Close();
        }



        private void CreateDynamicGridView()
        {
            /*List<User> items = new List<User>();
            items.Add(new User() { Name = "John Doe", Age = 42, Mail = "john@doe-family.com" });
            items.Add(new User() { Name = "Jane Doe", Age = 39, Mail = "jane@doe-family.com" });
            items.Add(new User() { Name = "Sammy Doe", Age = 7, Mail = "sammy.doe@gmail.com" });
            lvUsers.ItemsSource = items;*/
        }

        //public static string str = System.IO.Path.GetTempPath() + ;
        //[DllImport( "secure\\OpenVpnLib.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern int DLLFunction(int Number1, int Number2);


        private DispatcherTimer checkConnectionTimer = new DispatcherTimer();

        private int checkCount = 0;
        private bool isConnect = false;


        private void checkConnectionTimer_Elapsed(Object sender, EventArgs e)
        {
            checkCount++;
            checkConnectionTimer.Stop();
            try
            {
                /* UsbUtils usbUTILS = new UsbUtils();
                 String usbnamespace = usbUTILS.getUSBDriver();
                 if (usbnamespace.Length > 0)
                 {
                     string file_name = usbnamespace + "secure\\openvpn.log";
                     if (System.IO.File.Exists(file_name))
                     {
                         File.Open(file_name, FileMode.Open);
                         string checkString = File.ReadAllText(file_name);
                         if (checkString.IndexOf("WLVPN SUCCESS: CONNECTED") > 0)
                         {
                             checkConnectionTimer.Stop();
                             //checkConnectionTimer.
                             isConnect = true;
                             disconnect.Visibility = System.Windows.Visibility.Hidden;
                             conecting.Visibility = System.Windows.Visibility.Hidden;
                             connected.Visibility = System.Windows.Visibility.Visible;
                             conBTN.IsEnabled = true;
                             disconBTN.Visibility = System.Windows.Visibility.Visible;
                             conBTN.Visibility = System.Windows.Visibility.Hidden;
                             checkCount = 0;
                             return;
                         }
                     }

                    

                 }
                */
                /* NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
                 IPAddress dnsAdress_compare = IPAddress.Parse("198.18.0.1");
                 foreach (NetworkInterface networkInterface in networkInterfaces)
                 {
                     if (networkInterface.OperationalStatus == OperationalStatus.Up)
                     {
                         IPInterfaceProperties ipProperties = networkInterface.GetIPProperties();
                         IPAddressCollection dnsAddresses = ipProperties.DnsAddresses;

                         if (networkInterface.Description.IndexOf("ISATAP") >= 0)
                         {
                             foreach (IPAddress dnsAdress in dnsAddresses)
                             {
                                 if(dnsAdress.ToString().IndexOf("198.18.0.1")  >= 0 )                                 
                                 {
                                     checkConnectionTimer.Stop();
                                     //checkConnectionTimer.
                                     isConnect = true;
                                     disconnect.Visibility = System.Windows.Visibility.Hidden;
                                     conecting.Visibility = System.Windows.Visibility.Hidden;
                                     connected.Visibility = System.Windows.Visibility.Visible;
                                     conBTN.IsEnabled = true;
                                     disconBTN.Visibility = System.Windows.Visibility.Visible;
                                     conBTN.Visibility = System.Windows.Visibility.Hidden;
                                     checkCount = 0;
                                     return;
                                 }
                             }
                         }
                     }
                 }*/


                 NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
                 foreach (NetworkInterface adapter in adapters)
                 {

                     IPInterfaceProperties adapterProperties = adapter.GetIPProperties();
                     IPAddressCollection dnsServers = adapterProperties.DnsAddresses;
                     if (dnsServers.Count > 0)
                     {

                         if (adapter.Description.IndexOf("ISATAP") >= 0)
                         {
                             //continue;
                             //Console.WriteLine(adapter.Description);
                             foreach (IPAddress dns in dnsServers)
                             {
                                 if (dns.ToString().IndexOf("198.18.0.1") >= 0)
                                 {
                                     checkConnectionTimer.Stop();
                                     this.disconnect.Visibility = System.Windows.Visibility.Hidden;
                                     //this.conecting.Visibility = System.Windows.Visibility.Hidden;
                                     connecting_gif.Visibility = System.Windows.Visibility.Hidden;
                                     connecting_gif.StopAnimate();
                                     connecting_txt.Visibility = System.Windows.Visibility.Hidden;
                                     connected.Visibility = System.Windows.Visibility.Visible;
                                     conBTN.IsEnabled = true;
                                     disconBTN.Visibility = System.Windows.Visibility.Visible;
                                     conBTN.Visibility = System.Windows.Visibility.Hidden;
                                     checkCount = 0;
                                     isConCheck = false;
                                     this.Topmost = false;
                                     par_win.VPN_address(ip_address.Text);
                                     par_win.setVPN_City(this.location_txt.Text);
                                     this.par_win.setVpnIcon(true);
                                     return;
                                 }
                             }
                             //Console.WriteLine();
                         }

                     }
                 }

                /*if (checkCount == 1)
                {
                    checkConnectionTimer.Stop();
                    //checkConnectionTimer.
                    isConnect = true;
                    disconnect.Visibility = System.Windows.Visibility.Hidden;
                    conecting.Visibility = System.Windows.Visibility.Hidden;
                    connected.Visibility = System.Windows.Visibility.Visible;
                    conBTN.IsEnabled = true;
                    disconBTN.Visibility = System.Windows.Visibility.Visible;
                    conBTN.Visibility = System.Windows.Visibility.Hidden;
                    checkCount = 0;
                    this.par_win.setVpnIcon(true);
                    return;
                }*/
                UsbUtils usbUTILS = new UsbUtils();
                String usbnamespace = usbUTILS.getUSBDriver();
                if (usbnamespace.Length > 0)
                {

                    if (checkCount > 30)
                    {
                        checkConnectionTimer.Stop();
                        disconnect.Visibility = System.Windows.Visibility.Visible;
                        //conecting.Visibility = System.Windows.Visibility.Hidden;
                        connecting_gif.Visibility = System.Windows.Visibility.Hidden;
                        connecting_gif.StopAnimate();
                        connecting_txt.Visibility = System.Windows.Visibility.Hidden;
                        connected.Visibility = System.Windows.Visibility.Hidden;
                        conBTN.IsEnabled = true;
                        checkCount = 0;
                        String folder_name = System.IO.Path.Combine(usbnamespace, "secure");
                        String path = System.IO.Path.Combine(folder_name, "OpenVpnLib.dll");
                        var DLL = Assembly.LoadFile(path);

                        var theType = DLL.GetType("OpenVpnLib.Connector");
                        if (theType != null)
                        {
                            var con = Activator.CreateInstance(theType);
                            var method = theType.GetMethod("Disconnect");
                            method.Invoke(con, new object[] { folder_name });
                            isConCheck = false;
                            this.Topmost = false;
                        }
                        this.par_win.setVpnIcon(false);

                    }
                }
 

                
            }
            catch (Exception ex)
            {
                checkConnectionTimer.Start();
            }

            checkConnectionTimer.Start();


           
            //lvUsers.Items.Refresh();
        }

        private bool isConCheck = false;

        private void conBTN_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //checkPing();
            //getPingTime();
            conBTN.IsEnabled = false;
            isConCheck = true ;
            this.Topmost = true;
            disconnect.Visibility = System.Windows.Visibility.Hidden;
            //conecting.Visibility = System.Windows.Visibility.Visible;
            connecting_gif.Visibility = System.Windows.Visibility.Visible;
            connecting_gif.StartAnimate();
            connecting_txt.Visibility = System.Windows.Visibility.Visible;
            connected.Visibility = System.Windows.Visibility.Hidden;
            try
            {

                checkConnectionTimer.Interval = TimeSpan.FromMilliseconds(4000);
                checkConnectionTimer.Start();
                checkConnectionTimer.Tick += new EventHandler(checkConnectionTimer_Elapsed);
                UsbUtils usbUTILS = new UsbUtils();
                String usbnamespace = usbUTILS.getUSBDriver();
                String userPass = par_win.getUserPass();
                String userSub = par_win.getSubDomain();
                if (usbnamespace.Length > 0)
                {
                    string folder_name = System.IO.Path.Combine(usbnamespace, "secure");
                    String path = System.IO.Path.Combine(folder_name , "OpenVpnLib.dll");
                    var DLL = Assembly.LoadFile(path);

                    var theType = DLL.GetType("OpenVpnLib.Connector");
                    if (theType != null)
                    {
                        var con = Activator.CreateInstance(theType);
                        var method = theType.GetMethod("Connect");
                        for (int i = 0; i < items.Count; i++)
                        {
                            if ((ip_address.Text == items[i].ip_Address) && (items[i].Response != "...") && ((items[i].Response != "Down")))
                            {
                                if(isTcp)
                                    method.Invoke(con, new object[] { userSub, userPass, ip_address.Text, folder_name, "tcp", "443" });
                                else
                                    method.Invoke(con, new object[] { userSub, userPass, ip_address.Text, folder_name, "udp", "443" });                                
                            }
                        }                        
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public List<server> items;
        public List<server> items_buff = new List<server>();


        public string checkCountryfromIp(string ip_Add)
        {
            string loaction = "";
            for (int i = 0; i < items_buff.Count; i++)
            {
                server serverItem = items_buff[i];
                if (serverItem.ip_Address == ip_Add)
                {
                    loaction = serverItem.City;
                }
            }
            return loaction;
        }

        public void getServerList()
        {
            String serverList = "http://www.wlvpn.com/serverList.xml";
            try
            {
                items = new List<server>();
                XmlReader reader = XmlReader.Create(serverList);
                while (reader.Read())
                { 
                    server serverItem = new server();
		            if (reader.IsStartElement())
		            {
                        if (reader.IsStartElement())
                        {
                            //return only when you have START tag

                            switch (reader.Name.ToString())
                            {
                                case "server":
                                    string server_name = reader.GetAttribute("name");
                                    serverItem.Server = server_name.Substring(0, (server_name.Length - 10));
                                    serverItem.City = reader.GetAttribute("city");
                                    serverItem.Country = reader.GetAttribute("country");
                                    serverItem.ip_Address = reader.GetAttribute("ip");                                    
                                    serverItem.icon = reader.GetAttribute("icon");
                                    serverItem.state = Convert.ToInt32(reader.GetAttribute("status"));
                                    if (serverItem.state == 0)
                                        serverItem.Response = "Down";
                                    else
                                        serverItem.Response = "...";

                                    items.Add(serverItem);
                                    break;
                            }
                            
                        }
		            }
	              
                }
                items_buff = items;
                lvUsers.ItemsSource = items_buff;
            }
            catch (Exception ex)
            {

            }

            checkPing();
            checkProgressTimer.Interval = TimeSpan.FromMilliseconds(1000);
            checkProgressTimer.Start();
            checkProgressTimer.Tick += new EventHandler(checkProgressTimer_Elapsed);
            
        }
        private void checkProgressTimer_Elapsed(Object sender, EventArgs e)
        {
            bool isCheck = false;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Response == "...")
                {
                    isCheck = true;
                    break;
                }
            }
            if (!isCheck)
                checkProgressTimer.Stop();
            lvUsers.Items.Refresh();
        }


        private DispatcherTimer checkProgressTimer = new DispatcherTimer();       

        BindingListCollectionView blcv;
        GridViewColumnHeader _lastHeaderClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;


        private void Sort(string sortBy, ListSortDirection direction)
        {           
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvUsers.ItemsSource);
            view.SortDescriptions.Add(new SortDescription(sortBy, direction));
        }

        private bool isCity = false;
        private void city_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            items_buff = this.items;
            if (!isCity)
            {
                items_buff.Sort(delegate(server x, server y)
                {
                    return x.City.CompareTo(y.City);
                });
            }
            else
            {
                items_buff.Sort(delegate(server x, server y)
                {
                    return y.City.CompareTo(x.City);
                });
            }

            isCity = !isCity;

            lvUsers.ItemsSource = items_buff;
            lvUsers.Items.Refresh();

        }

        public void setVPNAddress(string add_txt)
        {
            ip_address.Text = add_txt;
            this.location_txt.Text = checkCountryfromIp(add_txt);
        }

        private server buffer_row = new server();

        private void lvUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            
            try
            {
                if (lvUsers.SelectedItem != null)
                {
                    if (lvUsers.SelectedItem is server)
                    {
                        var row = (server)lvUsers.SelectedItem;
                        buffer_row = row;
                        if (row != null)
                        {
                            if (par_win.getVpnProperty())
                                return;
                            if (isConCheck)
                                return;

                            ip_address.Text = row.ip_Address;
                            location_txt.Text = row.City ;

                        }
                    }
                }
            }
            catch (Exception)
            {
            }

        }

        bool isCountry = false;

        private void country_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            items_buff = this.items;
            if (!isCountry)
            {
                items_buff.Sort(delegate(server x, server y)
                {
                    return x.Country.CompareTo(y.Country);
                });
            }
            else
            {
                items_buff.Sort(delegate(server x, server y)
                {
                    return y.Country.CompareTo(x.Country);
                });
            }

            isCountry = !isCountry;
            lvUsers.ItemsSource = items_buff;
            lvUsers.Items.Refresh();
        }


        bool isServer = false;
        private void server_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            items_buff = this.items;
            if (!isServer)
            {
                items_buff.Sort(delegate(server x, server y)
                {
                    return x.Server.CompareTo(y.Server);
                });
            }
            else
            {
                items_buff.Sort(delegate(server x, server y)
                {
                    return y.Server.CompareTo(x.Server);
                });
            }

            isServer = !isServer;
            lvUsers.ItemsSource = items_buff;
            lvUsers.Items.Refresh();
        }

        bool isResponse = false;
        private void response_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            items_buff = this.items;

            if (!isResponse)
            {
                items_buff.Sort(delegate(server x, server y)
                {
                    return x.Response.CompareTo(y.Response);
                });
            }
            else
            {
                items_buff.Sort(delegate(server x, server y)
                {
                    return y.Response.CompareTo(x.Response);
                });
            }

            isResponse = !isResponse;         


            lvUsers.ItemsSource = items_buff;
            lvUsers.Items.Refresh();
        }


       /* private Task<PingReply> pingAsync(IPAddress ipaddress)
        {
            var tcs = new TaskCompletionSource<PingReply>();
            try
            {

                AutoResetEvent are = new AutoResetEvent(false);

                Ping ping = new Ping();
                ping.PingCompleted += (obj, sender) =>
                {
                    tcs.SetResult(sender.Reply);
                };
                ping.SendAsync(ipaddress, new object { });

            }
            catch (Exception)
            {
            }
            return tcs.Task;
        }
        */


        Thread usbcheckThread;

        private void checkPing()
        {
            checkPing uc = new checkPing();
            uc.setParent(this);
            usbcheckThread = new Thread(new ThreadStart(uc.Copy));
            usbcheckThread.Start();
        }

        private void getPingTime()
        {
            for (int i = 0; i < items.Count; i++)
            {
                Ping pingSender = new Ping();
                IPAddress address = IPAddress.Parse(items[i].ip_Address);
                PingReply reply = pingSender.Send(address);
                if (reply.Status == IPStatus.Success)
                {
                    items[i].Response = reply.RoundtripTime.ToString();
                }
                else
                {
                    items[i].Response = "Down";
                }
            }

            lvUsers.ItemsSource = items;
            lvUsers.Items.Refresh();


          /*  List<Task<PingReply>> pingTasks = new List<Task<PingReply>>();

            //addStatus("Scanning Network");
            for (int i = 0; i < items.Count; i++)
            {
                IPAddress address = IPAddress.Parse(items[i].ip_Address);
                pingTasks.Add(pingAsync(address));
            }

            Task.WaitAll(pingTasks.ToArray());
            
            //scanTargetDelegate d = null;
            IAsyncResult r = null;

            for (int i = 0; i < items.Count; i++)
            {
                var pingTask = pingTasks[i];
            
                if (pingTask.Result.Status.Equals(IPStatus.Success))
                {
                    items[i].Response = pingTask.Result.RoundtripTime.ToString();
                }
                else
                {
                    items[i].Response = "Down";
                }
            }
            lvUsers.ItemsSource = items;
            lvUsers.Items.Refresh();*/
            

        }

        private bool isTcp = false;

        private void tcp_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tcp.Visibility = System.Windows.Visibility.Hidden;
            tcp_on.Visibility = System.Windows.Visibility.Visible;
            udp.Visibility = System.Windows.Visibility.Visible;
            udp_on.Visibility = System.Windows.Visibility.Hidden;
            isTcp = true;
        }

        private void tcp_on_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tcp.Visibility = System.Windows.Visibility.Hidden;
            tcp_on.Visibility = System.Windows.Visibility.Visible;
            udp.Visibility = System.Windows.Visibility.Visible;
            udp_on.Visibility = System.Windows.Visibility.Hidden;
            isTcp = true;
        }

        private void udp_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tcp.Visibility = System.Windows.Visibility.Visible;
            tcp_on.Visibility = System.Windows.Visibility.Hidden;
            udp.Visibility = System.Windows.Visibility.Hidden;
            udp_on.Visibility = System.Windows.Visibility.Visible;
            isTcp = false;
        }

        private void udp_on_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tcp.Visibility = System.Windows.Visibility.Visible;
            tcp_on.Visibility = System.Windows.Visibility.Hidden;
            udp.Visibility = System.Windows.Visibility.Hidden;
            udp_on.Visibility = System.Windows.Visibility.Visible;
            isTcp = false;
        }

        private void DoubleAnimation_Completed(object sender, System.EventArgs e)
        {
            setVpnSetting(par_win.isVPN);
            getServerList();
        }

        private void disconBTN_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                UsbUtils usbUTILS = new UsbUtils();
                String usbnamespace = usbUTILS.getUSBDriver();
                if (usbnamespace.Length > 0)
                {
                    string folder_name = System.IO.Path.Combine(usbnamespace, "secure");
                    String path = System.IO.Path.Combine(folder_name , "OpenVpnLib.dll");
                    var DLL = Assembly.LoadFile(path);

                    var theType = DLL.GetType("OpenVpnLib.Connector");
                    if (theType != null)
                    {
                        var con = Activator.CreateInstance(theType);
                        var method = theType.GetMethod("Disconnect");
                        method.Invoke(con, new object[] { folder_name});

                        isConnect = false;
                        disconnect.Visibility = System.Windows.Visibility.Visible;
                        //conecting.Visibility = System.Windows.Visibility.Hidden;
                        connecting_gif.Visibility = System.Windows.Visibility.Hidden;
                        connecting_txt.Visibility = System.Windows.Visibility.Hidden;
                        connected.Visibility = System.Windows.Visibility.Hidden;

                        disconBTN.Visibility = System.Windows.Visibility.Hidden;
                        conBTN.Visibility = System.Windows.Visibility.Visible;
                        this.par_win.setVpnIcon(false);
                        ip_address.Text = buffer_row.ip_Address;
                        location_txt.Text = buffer_row.City;
                    }

                  
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }


        /***********
         * 
         * connect by double click
         * 
         * **************/
        private void lvUsers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            conBTN.IsEnabled = false;
            isConCheck = true;
            this.Topmost = true;
            disconnect.Visibility = System.Windows.Visibility.Hidden;
            //conecting.Visibility = System.Windows.Visibility.Visible;
            connecting_gif.Visibility = System.Windows.Visibility.Visible;
            connecting_gif.StartAnimate();
            connecting_txt.Visibility = System.Windows.Visibility.Visible;
            connected.Visibility = System.Windows.Visibility.Hidden;
            try
            {

                checkConnectionTimer.Interval = TimeSpan.FromMilliseconds(4000);
                checkConnectionTimer.Start();
                checkConnectionTimer.Tick += new EventHandler(checkConnectionTimer_Elapsed);
                UsbUtils usbUTILS = new UsbUtils();
                String usbnamespace = usbUTILS.getUSBDriver();
                String userPass = par_win.getUserPass();
                String userSub = par_win.getSubDomain();
                if (usbnamespace.Length > 0)
                {
                    string folder_name = System.IO.Path.Combine(usbnamespace, "secure");
                    String path = System.IO.Path.Combine(folder_name, "OpenVpnLib.dll");
                    var DLL = Assembly.LoadFile(path);

                    var theType = DLL.GetType("OpenVpnLib.Connector");
                    if (theType != null)
                    {
                        var con = Activator.CreateInstance(theType);
                        var method = theType.GetMethod("Connect");
                        for (int i = 0; i < items.Count; i++)
                        {
                            if ((ip_address.Text == items[i].ip_Address) && (items[i].Response != "...") && ((items[i].Response != "Down")))
                            {
                                if (isTcp)
                                    method.Invoke(con, new object[] { userSub, userPass, ip_address.Text, folder_name, "tcp", "443" });
                                else
                                    method.Invoke(con, new object[] { userSub, userPass, ip_address.Text, folder_name, "udp", "443" });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

        }





    }


    public class UnsafeNativeMethods {
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool SetDllDirectory(string lpPathName);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern int GetDllDirectory(int bufsize, StringBuilder buf);

    [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern IntPtr LoadLibrary(string librayName);

    [DllImport("mylibrary")]
    public static extern void InitMyLibrary();
}

    public class server
    {
        public string City { get; set; }

        public string Country { get; set; }

        public string Server { get; set; }

        public string Response { get; set; }

        public string ip_Address { get; set; }
        public string icon { get; set; }

        public int state { get; set; }
    }

    

    public class BoolToImageConverter : IValueConverter
    {
        public string FalsePath { get; set; }
        public string TruePath { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //if (System.Convert.ToString(value).IndexOf("US") >= 0)
            {
                return new BitmapImage(new Uri(System.Convert.ToString(value)));
            }
            //return new BitmapImage(new Uri("Images/file/edit.png", UriKind.Relative));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
