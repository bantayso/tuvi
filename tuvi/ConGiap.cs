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
    public class ConGiap
    {
        public String image { get; set; }
        public String name {get;set;}
        public String url { get; set; }

        public ConGiap(String _image, String _name, String _url)
        {
            image = _image;
            name = _name;
            url = _url;
        }
    }
}
