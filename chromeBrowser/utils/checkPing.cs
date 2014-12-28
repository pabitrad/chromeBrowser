using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;
using System.ComponentModel;
using System.Threading;
using System.Windows.Threading;
using System.Net.NetworkInformation;
//using System.Threading.Tasks;


namespace chromeBrowser
{
    class checkPing
    {

        public void setParent(vpnWin fl)
        {
            this.Parent = fl;
        }

        public vpnWin Parent { get; set; }



        public void Copy()
        {
            List<server> items_buff = new List<server>();
            for (int i = 0; i < Parent.items.Count; i++)
            {
                items_buff.Add(Parent.items[i]);
            }
            //items_buff = Parent.items;
            while (true)
            {                
                try
                {
                    for (int i = 0; i < items_buff.Count; i++)
                    {
                        Ping pingSender = new Ping();
                        IPAddress address = IPAddress.Parse(items_buff[i].ip_Address);
                        PingReply reply = pingSender.Send(address);
                        if (reply.Status == IPStatus.Success)
                        {
                            items_buff[i].Response = reply.RoundtripTime.ToString();
                        }
                        else
                        {
                            items_buff[i].Response = "Down";
                        }
                        
                        //Parent.lvUsers.Items.Refresh();
                    }
                    Parent.items = items_buff;
                    break;
                    
                }
                catch (Exception exx)
                {                    
                    break;
                }
            }
            //completeCopy();
        }
    }
}
