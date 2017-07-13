using Banhuizhushou.B_CLASS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Banhuizhushou.B_PAGE
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class PicinPic : Page
    {
        CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
        private TranslateTransform tt;

        public PicinPic()
        {
            this.InitializeComponent();
            UiBlur.getblur(MyBorder);
            coreTitleBar.ExtendViewIntoTitleBar = true;
            //可以尝试实时监测鼠标焦点的位置来判断是MyBorder来做标题栏还是title做标题栏
            Window.Current.SetTitleBar(title);

            var titlebar = ApplicationView.GetForCurrentView().TitleBar;
            titlebar.ButtonBackgroundColor = Color.FromArgb(0, 0, 0, 0);
            titlebar.ButtonForegroundColor = Colors.Black;

            tt = new TranslateTransform();
            ShowImge.RenderTransform = tt;

            MyScrollviewer.IsZoomInertiaEnabled = false;
        }
        //string imgpath;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
             //imgpath = (string )  e.Parameter;
           var  Imgsf = (StorageFile )  e.Parameter;
            showpathimg(Imgsf);
            var cc = ShowImge.Height;
            ShowImge.Height = RootGrid.Height;
          
        }
        private async void showpathimg(StorageFile a)
        {
            //var filepath = await StorageFile.GetFileFromPathAsync(a.Path);
            BitmapImage bi = new BitmapImage();
            await bi.SetSourceAsync(await a.OpenAsync(FileAccessMode.Read));
            ShowImge.Source = bi;
        }

        private void RootGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ShowImge.Height  = e.NewSize.Height ;
        }

        private void ShowImge_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (MyScrollviewer.IsZoomChainingEnabled==true)
            {
                tt.X += e.Delta.Translation.X;
                tt.Y += e.Delta.Translation.Y;
            }
        }
    }
}
