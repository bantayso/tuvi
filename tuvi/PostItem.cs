using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace tuvi
{
    public class PostItem
    {
        public String name { get; set; }
        public String url { get; set; }

        public PostItem(String _name, String _url)
        {
            name = _name;
            url = _url;
        }
    }
}
