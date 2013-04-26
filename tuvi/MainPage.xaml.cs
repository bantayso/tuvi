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
    public partial class MainPage : PhoneApplicationPage
    {
        public string PAGE_URL = "http://tuvi.biz/"; // http://tuvi2013.net/chuyen-muc/xem-tu-vi-2013
        public WebClient webClient;
        public HtmlDocument doc;

        public List<PostItem> listTronDoi = new List<PostItem>();
        public List<PostItem> listTinhDuyen = new List<PostItem>();
        public List<PostItem> listPhongThuy = new List<PostItem>();
        public List<PostItem> listXemTuong = new List<PostItem>();
        public List<PostItem> listXemBoi = new List<PostItem>();

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            webClient = new WebClient();
            doc = new HtmlDocument();
            doc.OptionFixNestedTags = true;
            doc.OptionAutoCloseOnEnd = true;
            doc.OptionWriteEmptyNodes = true;

            webClient.Encoding = Encoding.UTF8;

            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(WebClient_DownloadStringCompleted);
            webClient.DownloadStringAsync(new Uri(PAGE_URL));

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
            
            
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                //App.ViewModel.LoadData();
                List<ConGiap> list12ConGiap = new List<ConGiap>();
                list12ConGiap.Add(new ConGiap("http://tuvi2013.net/wp-content/uploads/2012/04/tu-vi-2013-tuoi-ty-150x150.jpg", "Tuổi Tý", "http://tuvi2013.net/xem-tu-vi-2013/tu-vi-tuoi-ty-ti-trong-nam-2013-nam-quy-ty-xem-tu-vi.html"));
                list12ConGiap.Add(new ConGiap("http://tuvi2013.net/wp-content/uploads/2012/02/tuoisuu-contrau-150x150.jpg", "Tuổi Sửu", "http://tuvi2013.net/xem-tu-vi-2013/tu-vi-tuoi-suu-trong-nam-2013-nam-quy-ty-xem-tu-vi.html"));
                list12ConGiap.Add(new ConGiap("http://tuvi2013.net/wp-content/uploads/2012/02/TUOIDAN-CONHO-150x150.jpg", "Tuổi Dần", "http://tuvi2013.net/xem-tu-vi-2013/tu-vi-tuoi-dan-trong-nam-2013-nam-quy-ty-xem-tu-vi.html"));
                list12ConGiap.Add(new ConGiap("http://tuvi2013.net/wp-content/uploads/2012/02/TUOIMAO-CONMEO-150x150.jpg", "Tuổi Mẹo", "http://tuvi2013.net/xem-tu-vi-2013/tu-vi-tuoi-mao-trong-nam-2013-nam-quy-ty-xem-tu-vi.html"));
                list12ConGiap.Add(new ConGiap("http://tuvi2013.net/wp-content/uploads/2012/02/TUOITY-CONRAN1-150x150.jpg", "Tuổi Thìn", "http://tuvi2013.net/xem-tu-vi-2013/tu-vi-tuoi-thin-trong-nam-2013-nam-quy-ty-xem-tu-vi.html"));
                list12ConGiap.Add(new ConGiap("http://tuvi2013.net/wp-content/uploads/2012/02/TUOITY-CONRAN1-150x150.jpg", "Tuổi Tỵ", "http://tuvi2013.net/xem-tu-vi-2013/tu-vi-tuoi-thin-trong-nam-2013-nam-quy-ty-xem-tu-vi.html"));
                list12ConGiap.Add(new ConGiap("http://tuvi2013.net/wp-content/uploads/2012/02/TUOINGO-CONNGUA-150x150.jpg", "Tuổi Ngọ", "http://tuvi2013.net/xem-tu-vi-2013/tu-vi-tuoi-ngo-trong-nam-2013-nam-quy-ty-xem-tu-vi.html"));
                list12ConGiap.Add(new ConGiap("http://tuvi2013.net/wp-content/uploads/2012/02/TUOIMUI-CONDE-150x150.jpg", "Tuổi Mùi", "http://tuvi2013.net/xem-tu-vi-2013/tu-vi-tuoi-mui-trong-nam-2013-nam-quy-ty-xem-tu-vi.html"));
                list12ConGiap.Add(new ConGiap("http://tuvi2013.net/wp-content/uploads/2012/02/TUOITHAN-CONKHI-150x150.jpg", "Tuổi Thân", "http://tuvi2013.net/xem-tu-vi-2013/tu-vi-tuoi-than-trong-nam-2013-nam-quy-ty-xem-tu-vi.html"));
                list12ConGiap.Add(new ConGiap("http://tuvi2013.net/wp-content/uploads/2012/02/TUOIDAU-CONGA-150x150.jpg", "Tuổi Dậu", "http://tuvi2013.net/xem-tu-vi-2013/tu-vi-tuoi-dau-nam-2013-nam-quy-ty-xem-tu-vi.html"));
                list12ConGiap.Add(new ConGiap("http://tuvi2013.net/wp-content/uploads/2012/02/TUOITUAT-CONCHO-150x150.jpg", "Tuổi Tuất", "http://tuvi2013.net/xem-tu-vi-2013/tu-vi-tuoi-tuat-nam-2013-nam-quy-ty-xem-tu-vi.html"));
                list12ConGiap.Add(new ConGiap("http://tuvi2013.net/wp-content/uploads/2012/02/TUOIHOI-CONLON-150x150.jpg", "Tuổi Hợi", "http://tuvi2013.net/xem-tu-vi-2013/tu-vi-tuoi-hoi-nam-2013-nam-quy-ty-xem-tu-vi.html"));

                list12giap.ItemsSource = list12ConGiap;
            }
        }

        private void WebClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                String html = e.Result;

                doc.LoadHtml(html);

                if (doc.DocumentNode != null)
                {
                    // generate detail to panorama
                    parseHtml();
                    lstTronDoi.ItemsSource = listTronDoi;
                    lstTinhDuyen.ItemsSource = listTinhDuyen;
                    lstPhongThuy.ItemsSource = listPhongThuy;
                    lstXemboi.ItemsSource = listXemBoi;
                    lstXemTuong.ItemsSource = listXemTuong;
                }

            }
            else
            {
                System.Diagnostics.Debug.WriteLine(e.Error.Message);
            }
        }

        public void parseHtml()
        {
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//div[@class='news']");
            int i = 0;
            foreach (HtmlNode div in nodes)
            {
                System.Diagnostics.Debug.WriteLine(i + " ");
                if (i == 0)
                {
                    addToList(listTronDoi, div);
                }
                else if (i == 5)
                {
                    addToList(listTinhDuyen, div);
                }
                else if (i == 6)
                {
                    addToList(listPhongThuy, div);
                }
                else if (i == 7)
                {
                    addToList(listXemTuong, div);
                }
                else if (i == 8)
                {
                    addToList(listXemBoi, div);
                } 
                i++;
            }
        }

        private void list12giap_Tap(object sender, GestureEventArgs e)
        {
            ConGiap cg = (ConGiap)list12giap.SelectedItem;
            NavigationService.Navigate(new Uri(string.Format("/PageDetail.xaml?url={0}&page=12congiap", Uri.EscapeUriString(cg.url)), UriKind.Relative));
        }

        public void addToList(List<PostItem> list, HtmlNode node)
        {
            if (list == null || node == null) return;
            
            HtmlDocument tempDoc = new HtmlDocument();
            tempDoc.LoadHtml(node.InnerHtml);

            HtmlNodeCollection nodes = tempDoc.DocumentNode.SelectNodes("//a");
            System.Diagnostics.Debug.WriteLine("\nxxx" + nodes.Count);
            foreach (HtmlNode div in nodes)
            {
                if (!div.GetAttributeValue("href", "").Equals("#"))
                list.Add(new PostItem(div.InnerText, div.GetAttributeValue("href", "")));
                //System.Diagnostics.Debug.WriteLine("@@@ " + div.InnerText + " -- " + div.Attributes["href"]);
            }
            System.Diagnostics.Debug.WriteLine("@@@ ");
        }

        private void lstTronDoi_Tap(object sender, GestureEventArgs e)
        {
            PostItem pi = (PostItem)lstTronDoi.SelectedItem;
            NavigationService.Navigate(new Uri(string.Format("/PageDetail.xaml?url={0}&page=trondoi", Uri.EscapeUriString(pi.url)), UriKind.Relative));
        }

        private void lstXemboi_Tap(object sender, GestureEventArgs e)
        {
            PostItem pi = (PostItem)lstXemboi.SelectedItem;
            NavigationService.Navigate(new Uri(string.Format("/PageDetail.xaml?url={0}&page=xemboi", Uri.EscapeUriString(pi.url)), UriKind.Relative));
        }

        private void lstXemTuong_Tap(object sender, GestureEventArgs e)
        {
            PostItem pi = (PostItem)lstXemTuong.SelectedItem;
            NavigationService.Navigate(new Uri(string.Format("/PageDetail.xaml?url={0}&page=xemtuong", Uri.EscapeUriString(pi.url)), UriKind.Relative));
        }

        private void lstPhongThuy_Tap(object sender, GestureEventArgs e)
        {
            PostItem pi = (PostItem)lstPhongThuy.SelectedItem;
            NavigationService.Navigate(new Uri(string.Format("/PageDetail.xaml?url={0}&page=phongthuy", Uri.EscapeUriString(pi.url)), UriKind.Relative));
        }

        private void lstTinhDuyen_Tap(object sender, GestureEventArgs e)
        {
            PostItem pi = (PostItem)lstTinhDuyen.SelectedItem;
            NavigationService.Navigate(new Uri(string.Format("/PageDetail.xaml?url={0}&page=tinhduyen", Uri.EscapeUriString(pi.url)), UriKind.Relative));
        }
    }
}