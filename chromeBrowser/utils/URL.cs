using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chromeBrowser
{
    class URL
    {
        string url;
        string title;
        string browser;
        public URL(string url, string title, string browser)
        {
            this.url = url;
            this.title = title;
            this.browser = browser;
        }

        public string getData()
        {
            return browser + " - " + title + " - " + url;
        }

    }
    
}
