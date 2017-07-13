using Banhuizhushou.B_CLASS;
using Banhuizhushou.B_PAGE;
using System;
using System.Collections.ObjectModel;
using System.IO;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace Banhuizhushou
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
        public ObservableCollection<Imgitem> ImgitemOb { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            coreTitleBar.ExtendViewIntoTitleBar = true;
            Window.Current.SetTitleBar(myTitle);
            var titlebar = ApplicationView.GetForCurrentView().TitleBar;
            titlebar.ButtonBackgroundColor = Color.FromArgb(0, 0, 0, 0);
            titlebar.ButtonForegroundColor = Colors.Black;
            UiBlur.getblur(MyGrid);
            ImgitemOb = new ObservableCollection<Imgitem>();

            AddNewPic.RenderTransform = new TranslateTransform();

        }


        private async void AddNewPic_ClickAsync(object sender, RoutedEventArgs e)
        {
            try
            {
                FileOpenPicker fop = new FileOpenPicker();
                BitmapImage bi = new BitmapImage();

                fop.FileTypeFilter.Add(".jpg");
                fop.FileTypeFilter.Add(".png");
                fop.ViewMode = PickerViewMode.Thumbnail;
                fop.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
                var fopvalue = await fop.PickSingleFileAsync();
                //拿到缩略图
                var slt = await fopvalue.GetThumbnailAsync(Windows.Storage.FileProperties.ThumbnailMode.SingleItem ,190,Windows.Storage.FileProperties.ThumbnailOptions.ResizeThumbnail );
               if(slt != null)
                {
                 await  bi.SetSourceAsync(slt);          
                }
               

                NewMethod(fopvalue,bi);

                if (aba_int <1)
                {
                    AddbuttonAnimation();
                }
                //AddNewPic.Visibility = Visibility.Collapsed;
                //myStoryboard.Begin();
            }
            catch (Exception ex)
            {
                await new MessageDialog("您没有选择文件，或者文件格式不正确:"+ex).ShowAsync();
                AddNewPic.Visibility = Visibility.Visible;

            }

        }



        private void NewMethod(StorageFile fopvalue , BitmapImage Bi)
        {
            ImgitemOb.Add(new Imgitem { Imgpath = fopvalue.Path, Filename = fopvalue.Name,imgSamp =Bi,StorageF = fopvalue });
        }

        private void Picturegrid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var boxs = sender as StackPanel;
            var box = boxs.DataContext as Imgitem;
            //ShowCompactView(box.Imgpath);
            ShowCompactView(box.StorageF); 

        }
        int compactViewId;

        private async void ShowCompactView(string path)
        {
            await CoreApplication.CreateNewView().Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                var frame = new Frame();
                compactViewId = ApplicationView.GetForCurrentView().Id;
                frame.Navigate(typeof(PicinPic), path);
                Window.Current.Content = frame;
                Window.Current.Activate();
                ApplicationView.GetForCurrentView().Title = "CompactOverlay Window";
            });
            bool viewShown = await ApplicationViewSwitcher.TryShowAsViewModeAsync(compactViewId, ApplicationViewMode.CompactOverlay);
        }

        private int aba_int =0;

        private void AddbuttonAnimation()
        {
          
            Storyboard storyboard = new Storyboard();
      
            var position = AddNewPic.TransformToVisual(ListGrid).TransformPoint(new Point());
            var cy = ListGrid.ActualHeight/2 - 80;
            var YAnimation = new DoubleAnimation { Duration = new Duration(TimeSpan.FromSeconds(0.3)), From =0, To = cy ,EnableDependentAnimation =true,EasingFunction = new ExponentialEase()};
            Storyboard.SetTarget(YAnimation, AddNewPic);
            Storyboard.SetTargetProperty(YAnimation, "(UIElement.RenderTransform).(TranslateTransform.Y)");
            storyboard.Children.Add(YAnimation);
            storyboard.Begin();
            aba_int++;

            

        }

        private void Settingbutton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof (SettingPage));
        }
    }
}
