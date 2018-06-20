using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace SEICHOU_WPA_8._1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class characterPage : Page
    {
        playerProfile usr = new playerProfile();
        levels lvl = new levels();
        IMobileServiceTable<playerProfile> playerTable = App.MobileService.GetTable<playerProfile>();
        IMobileServiceTable<levels> levelsTable = App.MobileService.GetTable<levels>();
        //List<String> filesInSavesFolder = new List<String>();
        //StorageFolder local = ApplicationData.Current.LocalFolder;

        //bool isMale;

        public characterPage()
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
            usr = e.Parameter as playerProfile;

            createButton.Visibility = Visibility.Collapsed;

            maleButton.Background = new SolidColorBrush(Windows.UI.Colors.Blue);
            malePlayer.Visibility = Visibility.Visible;

            femalePlayer.Visibility = Visibility.Collapsed;
            usr.gender = 'M';
            //isMale = true;
        }

        //private async Task CheckFileNameAsync(string name)
        //{
        //    bool fileExists = false;
        //    var s = await local.CreateFolderAsync("Saves", CreationCollisionOption.OpenIfExists);
        //    var f = await s.GetFilesAsync();

        //    foreach (StorageFile sf in f)
        //    {
        //        if (name + ".txt" == sf.Name)
        //        {
        //            fileExists = true;
        //            break;
        //        }
        //    }

        //    if (fileExists)
        //    {
        //        MessageDialog msg = new MessageDialog("A file with that name already exists! Please enter a new name.");
        //        await msg.ShowAsync();

        //        playerName_TextBox.Text = "";
        //    }
        //    else
        //    {
        //        maleButton.Focus(FocusState.Programmatic);
        //    }
        //}

        //private async Task CreateSaveAsync(string name, string password)
        //{
        //    var saves = await local.CreateFolderAsync("Saves", CreationCollisionOption.OpenIfExists);
        //    var file = await saves.CreateFileAsync(name + ".txt", CreationCollisionOption.OpenIfExists);

        //    var stream = await file.OpenAsync(FileAccessMode.ReadWrite);
        //    using (var outputStream = stream.GetOutputStreamAt(0)) //Use stream.size to start writing from the end of a file
        //    {
        //        DataWriter dataWriter = new DataWriter(outputStream);
        //        dataWriter.WriteString("NAME: " + name + "\nPASSWORD: " + password);
        //        usr.fileName = name + ".txt";

        //        if (isMale)
        //        {
        //            dataWriter.WriteString("\nGENDER: Male");
        //        }
        //        else
        //        {
        //            dataWriter.WriteString("\nGENDER: Female");
        //        }

        //        for (int i = 1; i <= 12; i++)
        //        {
        //            dataWriter.WriteString("\nLEVEL " + i + ": " + 0);

        //        }
        //        await dataWriter.StoreAsync();
        //        await outputStream.FlushAsync();
        //        stream.Dispose();
        //    }

        //}

        // Opens keyboard when the name text box is pressed
        private void playerName_TextBox_Tapped(object sender, TappedRoutedEventArgs e)
        {
            playerName_TextBox.Focus(FocusState.Keyboard);
        }

        // Allows names with any characters that are greater than zero in length
        private /*async*/ void playerName_TextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            CheckInput(e);

            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                //await CheckFileNameAsync(playerName_TextBox.Text);
                maleButton.Focus(FocusState.Programmatic);
            }
        }

        //private void passwordBox_Tapped(object sender, TappedRoutedEventArgs e)
        //{
        //    passwordBox.Focus(FocusState.Keyboard);
        //}

        //private void passwordBox_KeyDown(object sender, KeyRoutedEventArgs e)
        //{
        //    CheckInput(e);

        //    if (e.Key == Windows.System.VirtualKey.Enter)
        //    {
        //        maleButton.Focus(FocusState.Programmatic);
        //    }
        //}

        private void CheckInput(KeyRoutedEventArgs e)
        {
            if (playerName_TextBox.Text.Length > 0 /*&& passwordBox.Password.Length > 0*/)
            {
                createButton.Visibility = Visibility.Visible;
            }
            else
            {
                createButton.Visibility = Visibility.Collapsed;
            }
        }

        // Toggles between male and female avatars when their respective buttons are pressed
        private void maleButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            maleButton.Background = new SolidColorBrush(Windows.UI.Colors.Blue);
            femaleButton.Background = new SolidColorBrush(Windows.UI.Colors.Black);

            malePlayer.Visibility = Visibility.Visible;
            femalePlayer.Visibility = Visibility.Collapsed;

            usr.gender = 'M';
            //isMale = true;
        }

        private void femaleButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            femaleButton.Background = new SolidColorBrush(Windows.UI.Colors.Pink);
            maleButton.Background = new SolidColorBrush(Windows.UI.Colors.Black);

            femalePlayer.Visibility = Visibility.Visible;
            malePlayer.Visibility = Visibility.Collapsed;

            usr.gender = 'F';
            //isMale = false;
        }

        // Navigates to the tutorial page
        public async void createButton_Click(object sender, RoutedEventArgs e)
        {
            //await CreateSaveAsync(playerName_TextBox.Text, passwordBox.Password);

            usr.Id = Guid.NewGuid().ToString();
            usr.name = playerName_TextBox.Text;

            lvl.Id = usr.Id;
            usr.coins = 0;
            usr.healpowerup = 0;
            usr.timerpowerup = 0;
            progressRing.IsActive = true;
            progressRing.Visibility = Visibility.Visible;

            await playerTable.InsertAsync(usr);
            await levelsTable.InsertAsync(lvl);

            var rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(tutorial), usr);

            progressRing.IsActive = false;
            progressRing.Visibility = Visibility.Collapsed;

            Window.Current.Content = rootFrame;
            Window.Current.Activate();
        }
    }
}
