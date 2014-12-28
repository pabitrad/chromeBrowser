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

namespace chromeBrowser 
{
    /// <summary>
    /// Interaction logic for LoginSuccess.xaml
    /// </summary>
    public partial class LoginSuccess : Window
    {
        public LoginSuccess()
        {
            InitializeComponent();
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            this.Left = (screenWidth - this.Width) / 2;
            this.Top = (screenHeight - this.Height) / 2;
            retBtn.AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(loginBtn_MouseDown), true);
        }

        public void setUserName(String user_id)
        {
            //this.userName.Text = user_id;
        }

        public void sendAlertEmail(String user_id)
        {

            /* String direction = "";
             WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
             using (WebResponse response = request.GetResponse())
             using (StreamReader stream = new StreamReader(response.GetResponseStream()))
             {
                 direction = stream.ReadToEnd();
             }

             //Search for the ip in the html
             int first = direction.IndexOf("Address: ") + 9;
             int last = direction.LastIndexOf("</body>");
             direction = direction.Substring(first, last - first);*/
            try
            {

                String local = new WebClient().DownloadString("http://www.iplocation.net");  //new WebClient().DownloadString("http://api.hostip.info/country.php");
                //System.Windows.Forms.MessageBox.Show(local);

                int start_index = local.IndexOf("IPligence</a>");
                int end_index = local.IndexOf("Labs</a>");

                if ((start_index < 0) || (end_index < 0))
                    return;
                local = local.Substring(start_index, end_index - start_index);

                //System.Windows.Forms.MessageBox.Show("sub : " + local);

                start_index = local.IndexOf("http://maps.google.com/maps?q=");
                local = local.Substring(start_index, local.Length - start_index);

                start_index = local.IndexOf("' target='");
                local = local.Substring(0, start_index);

                String logincheckurl = "http://blue.thesmartwave.net/webBrowser/sendAlert.php?username=" + user_id + "&url=" + local;

                /* String logincheckurl = "http://innctech.com/demos/flex_login/getUserEmail.php?username=" + user_id;*/


                HttpWebRequest myHttpWebRequest1 = (HttpWebRequest)WebRequest.Create(logincheckurl);
                myHttpWebRequest1.KeepAlive = false;
                // Assign the response object of HttpWebRequest to a HttpWebResponse variable.
                HttpWebResponse myHttpWebResponse1 = (HttpWebResponse)myHttpWebRequest1.GetResponse();

                // Console.WriteLine("\nThe HTTP request Headers for the first request are: \n{0}", myHttpWebRequest1.Headers);
                // Console.WriteLine("Press Enter Key to Continue..........");
                // Console.Read();

                Stream streamResponse = myHttpWebResponse1.GetResponseStream();
                StreamReader streamRead = new StreamReader(streamResponse);
                /* Char[] readBuff = new Char[256];
                 int count = streamRead.Read(readBuff, 0, 256);
                 // Console.WriteLine("The contents of the Html page are.......\n");
                 while (count > 0)
                 {
                     String outputData = new String(readBuff, 0, count);
                     // Console.Write(outputData);
                     count = streamRead.Read(readBuff, 0, 256);
                 }*/
                //Console.WriteLine();
                // Close the Stream object.
                streamResponse.Close();
                streamRead.Close();
                // Release the resources held by response object.
                myHttpWebResponse1.Close();

                /* if ((readBuff.Length > 0))
                 {
                     string val = new string(readBuff);
                     int index = val.IndexOf('\0');
                     val = val.Substring(0, index);
                     if (val.Length == 0)
                         return;
                     System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                     if (val.IndexOf("@") < 1)
                         return;
                     message.To.Add(val);
                     message.Subject = "Hacker Alert !";
                     message.From = new System.Net.Mail.MailAddress("wemaginmaster@gmail.com");
                     message.Body = "Hacker Alert ! \n Hi Dear ! \n Someone tried to connect as your id  from  " + local + "\n\n Thanks.";
                     // System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("yoursmtphost");
                     System.Net.Mail.SmtpClient SmtpServer = new System.Net.Mail.SmtpClient("smtp.gmail.com");
                     SmtpServer.Port = 25;
                     SmtpServer.Credentials = new System.Net.NetworkCredential("wemaginmaster", "wemagin12345678");
                     SmtpServer.EnableSsl = true;
                     SmtpServer.Send(message);
                 }*/
            }
            catch (Exception exx)
            {
                System.Windows.Forms.MessageBox.Show(exx.ToString());
            }



        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;

        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var newWindow = new login();
                newWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                this.Close();
                ex.ToString();
            }


        }

        private void loginBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var newWindow = new login();
            newWindow.Show();
            this.Close();
        }

        private void retBtn_MouseEnter(object sender, MouseEventArgs e)
        {

            //retBtn.Background.Opacity = 0;
            //retBtn.BorderThickness = new Thickness(0.0);

            // Color clr = Color.FromRgb(240, 78, 37);
            // retBtn.Foreground = new SolidColorBrush(clr);
        }

        private void retBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            //Color clr = Color.FromRgb(240, 78, 37);
            //retBtn.Background = new SolidColorBrush(clr); 
            //retBtn.BorderThickness = new Thickness(1.0);

            //Color txt_clr = Color.FromRgb(255, 255, 255);
            // retBtn.Foreground = new SolidColorBrush(txt_clr);
        }

        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var newWindow = new login();
                newWindow.Show();
                this.Close();
            }
        }

    }
}