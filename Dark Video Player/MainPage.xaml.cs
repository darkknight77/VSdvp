using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Dark_Video_Player
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static MainPage Current;
        public static Frame rootFrame;
        public MainPage()
        {
            this.InitializeComponent();
            Current = this;
            rootFrame = contentFrame;
            this.NavigationCacheMode = NavigationCacheMode.Required;
            contentFrame.Navigate(typeof(FoldersFiles));
        }



        private void nav_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {

            NavigationViewItem item = args.SelectedItem as NavigationViewItem;

            switch (item.Tag.ToString())
            {

                case "FoldersFiles":
                    contentFrame.Navigate(typeof(FoldersFiles));
                    break;

               
            }


        }

        private void nav_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            Debug.WriteLine("back");

            if (contentFrame.CanGoBack) contentFrame.GoBack();
            
        }
    }
}
