using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace SEICHOU_WPA_8._1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class mainMenu : Page
    {
        sender sdr;

        StorageFolder local = ApplicationData.Current.LocalFolder;


        public mainMenu()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            sdr = e.Parameter as sender;

            //playerProfileButton.Content = usr.name;
            //string line = "";
            //var savesFolder = await local.CreateFolderAsync("Saves", CreationCollisionOption.OpenIfExists);

            //Stream s = await savesFolder.OpenStreamForReadAsync(usr.fileName);
            //using (StreamReader r = new StreamReader(s))
            //{
            //    while (line != null)
            //    {
            //        line = r.ReadLine();

            //        if (line.Contains("NAME: "))
            //        {
            //            playerProfileButton.Content = line.Remove(0, 6);
            //            r.Dispose();
            //            break;
            //        }
            //    }
            //}
        }

        private void stageSelectionButton_Click(object sender, RoutedEventArgs e)
        {
            var rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(levelselect), sdr);
            Window.Current.Content = rootFrame;
            Window.Current.Activate();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private void Shopselectionbutton_Click(object sender, RoutedEventArgs e)
        {
            var rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(Shop), sdr);
            Window.Current.Content = rootFrame;
            Window.Current.Activate();
        }

        private void logout_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(MainPage), sdr);
            Window.Current.Content = rootFrame;
            Window.Current.Activate();
        }

        private void Help_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(Helppage), sdr);
            Window.Current.Content = rootFrame;
            Window.Current.Activate();
        }
    }
}
