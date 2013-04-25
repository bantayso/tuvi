using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using HtmlAgilityPack;
using System.Text;

namespace tuvi
{
    public partial class Page1 : PhoneApplicationPage
    {
        public string PAGE_URL = "http://tuvi.biz/thu-tu-cua-ban-244-detail.htm";
        
        public WebClient webClient;
        public HtmlDocument doc;

        public Page1()
        {
            InitializeComponent();

            webClient = new WebClient();
            doc = new HtmlDocument();
            webBrowser.IsScriptEnabled = true;

            webClient.Encoding = Encoding.UTF8;

            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(WebClient_DownloadStringCompleted);
            webClient.DownloadStringAsync(new Uri(PAGE_URL));
        }

        private void WebClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                String html = e.Result;
                
                doc.LoadHtml(html);

                if (doc.DocumentNode != null)
                {
                    parseHtml();
                }

            }
            else
            {
                System.Diagnostics.Debug.WriteLine(e.Error.Message);
            }
        }

        public void parseHtml()
        {
            //HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//div[@class='content']");
            //foreach (HtmlNode link in nodes)
            //{
            //    System.Diagnostics.Debug.WriteLine("> " + link.InnerText);
            //}
            HtmlNode node = doc.DocumentNode.SelectSingleNode("//div[@class='content']");
            if (node != null)
            {
                string htmlString = "<html><head><meta http-equiv='Content-Type' content='text/html;charset=UTF-8'></head><body>";
                htmlString = htmlString + node.InnerHtml + "</body></html>";
                
                htmlString = ToExtendedASCII(htmlString);
                // load to webview
                
                webBrowser.NavigateToString(htmlString);
                System.Diagnostics.Debug.WriteLine(htmlString.Length);
            }
            
            
        }

        private string ToExtendedASCII(string html)
        {
            const string ExtendedAsciiFormat = "&#{0};";
            string encodedHtml = "";
            char[] chars = html.ToCharArray();

            foreach (char c in chars)
            {
                int ascii = Convert.ToInt32(c);
                if (ascii > 127)
                {
                    encodedHtml += string.Format(ExtendedAsciiFormat, ascii);
                }
                else
                {
                    encodedHtml += c;
                }
            }

            return encodedHtml;
        }
    }
}