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
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using HtmlAgilityPack;
using System.Text;

namespace tuvi
{
    public partial class Page1 : PhoneApplicationPage
    {
        public string PAGE_URL = "http://tuvi.biz/";
        public string page = "";

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
            //webClient.DownloadStringAsync(new Uri(PAGE_URL));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //if (e.NavigationMode == NavigationMode.Back) return;
            base.OnNavigatedTo(e);
            
            string url = NavigationContext.QueryString["url"];
            page = NavigationContext.QueryString["page"];

            if (!page.Equals("12congiap"))
            {
                url = PAGE_URL + url;
            }

            webClient.DownloadStringAsync(new Uri(url));

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
            string htmlString = "<html><head><meta http-equiv='Content-Type' content='text/html;charset=UTF-8'></head><body>";
            HtmlNode node;
            if (page.Equals("12congiap"))
            {
                node = doc.DocumentNode.SelectSingleNode("//div[@class='entry']");
                node.SelectSingleNode("//h3[@class='related_post_title']").Remove();
                node.SelectSingleNode("//ul[@class='related_post']").Remove();
            }
            else
            {
                node = doc.DocumentNode.SelectSingleNode("//div[@class='content']");
                
            }

            // load to webview
            if (node != null)
            {
                htmlString = htmlString + node.InnerHtml + "</body></html>";
                htmlString = ToExtendedASCII(htmlString);
                webBrowser.NavigateToString(htmlString);
                System.Diagnostics.Debug.WriteLine(htmlString.Length);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("error");
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