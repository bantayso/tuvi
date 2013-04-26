using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ComponentModel;
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

        //public BackgroundWorker bw = new BackgroundWorker();

        public Page1()
        {
            InitializeComponent();

            //bw.WorkerReportsProgress = true;
            //bw.WorkerSupportsCancellation = true;

            //bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            //bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            //bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);

            webClient = new WebClient();
            doc = new HtmlDocument();
            webBrowser.IsScriptEnabled = true;

            webClient.Encoding = Encoding.UTF8;

            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(WebClient_DownloadStringCompleted);
 
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
                node = formatHTML(node);

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

        private HtmlNode formatHTML(HtmlNode node)
        {
            HtmlDocument tempDoc = new HtmlDocument();
            tempDoc.LoadHtml(node.InnerHtml);

            if (!page.Equals("12congiap"))
            {
                HtmlNodeCollection imgs = tempDoc.DocumentNode.SelectNodes("//img");
                if (imgs != null)
                {
                    foreach (HtmlNode img in imgs)
                    {
                        img.SetAttributeValue("src", PAGE_URL + img.Attributes["src"].Value );
                    }
                }
            }
            return tempDoc.DocumentNode;
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

        /*private void bw_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }*/
    }
}