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

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            webClient = new WebClient();
            doc = new HtmlDocument();

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
                list12ConGiap.Add(new ConGiap("http://tuvi2013.net/wp-content/uploads/2012/02/TUOITY-CONRAN1-150x150.jpg", "Tuổi Thìn", ""));
                list12ConGiap.Add(new ConGiap("http://tuvi2013.net/wp-content/uploads/2012/02/TUOITY-CONRAN1-150x150.jpg", "Tuổi Tỵ", ""));
                list12ConGiap.Add(new ConGiap("http://tuvi2013.net/wp-content/uploads/2012/02/TUOINGO-CONNGUA-150x150.jpg", "Tuổi Ngọ", ""));
                list12ConGiap.Add(new ConGiap("http://tuvi2013.net/wp-content/uploads/2012/02/TUOIMUI-CONDE-150x150.jpg", "Tuổi Mùi", ""));
                list12ConGiap.Add(new ConGiap("http://tuvi2013.net/wp-content/uploads/2012/02/TUOITHAN-CONKHI-150x150.jpg", "Tuổi Thân", ""));
                list12ConGiap.Add(new ConGiap("http://tuvi2013.net/wp-content/uploads/2012/02/TUOIDAU-CONGA-150x150.jpg", "Tuổi Dậu", ""));
                list12ConGiap.Add(new ConGiap("http://tuvi2013.net/wp-content/uploads/2012/02/TUOITUAT-CONCHO-150x150.jpg", "Tuổi Tuất", ""));
                list12ConGiap.Add(new ConGiap("http://tuvi2013.net/wp-content/uploads/2012/02/TUOIHOI-CONLON-150x150.jpg", "Tuổi Hợi", ""));

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
            foreach (HtmlNode div in nodes)
            {
                
            }
        }

        private void list12giap_Tap(object sender, GestureEventArgs e)
        {
            ConGiap cg = (ConGiap)list12giap.SelectedItem;
            System.Diagnostics.Debug.WriteLine(cg.url);
        }


    }
}