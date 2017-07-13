using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace Banhuizhushou.B_CLASS
{
    public  class Imgitem
    {
            public string Imgpath { get; set; }
            public string Filename { get; set; }
            public BitmapImage imgSamp { get; set; }     
        public StorageFile StorageF { get; set; }
    }
}
