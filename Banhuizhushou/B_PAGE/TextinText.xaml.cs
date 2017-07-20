using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Banhuizhushou.B_PAGE
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class TextinText : Page
    {
        CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
        string TextboxContent;
        public TextinText()
        {
            this.InitializeComponent();
            coreTitleBar.ExtendViewIntoTitleBar = true;
            Window.Current.SetTitleBar(title);
            var titlebar = ApplicationView.GetForCurrentView().TitleBar;
            titlebar.ButtonBackgroundColor = Color.FromArgb(0, 0, 0, 0);
            titlebar.ButtonForegroundColor = Colors.Black;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            TextboxContent = (string)e.Parameter;
            Mytextbox.Text = TextboxContent;
        }
        int L;
        private void LockButton_Click(object sender, RoutedEventArgs e)
        {
            L++;

            if (L % 2 != 0)
            {
                Mytextbox.IsReadOnly = true;
                Tost.Visibility = Visibility.Visible;
            }

            else
            {
                Mytextbox.IsReadOnly = false;
                Tost.Visibility = Visibility.Collapsed;
            }
        }
    }
}
