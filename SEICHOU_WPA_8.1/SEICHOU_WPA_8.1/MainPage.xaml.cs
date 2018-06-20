using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace SEICHOU_WPA_8._1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    ///
    public sealed partial class MainPage : Page
    {
        playerProfile usr = new playerProfile();
        sender sdr = new sender();
        IMobileServiceTable<playerProfile> playerTable = App.MobileService.GetTable<playerProfile>();

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.
            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.

            StatusBar statusBar = StatusBar.GetForCurrentView();

            userName_TextBox.Text = "";
            passwordBox.Password = "";

            logIn_Button.Visibility = Visibility.Collapsed;

            userName_TextBox.PlaceholderText = "Enter user name";
            passwordBox.PlaceholderText = "Enter Password";

            // Hide the status bar
            await statusBar.HideAsync();
        }

        private void userName_TextBox_Tapped(object sender, TappedRoutedEventArgs e)
        {
            userName_TextBox.Focus(FocusState.Keyboard);
        }

        private void userName_TextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                InputPane.GetForCurrentView().TryHide();
            }
        }

        private void userName_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckInput();
        }

        private void passwordBox_Tapped(object sender, TappedRoutedEventArgs e)
        {
            passwordBox.Focus(FocusState.Keyboard);
        }

        private void passwordBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                InputPane.GetForCurrentView().TryHide();
            }
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            CheckInput();
        }

        private void CheckInput()
        {
            if (userName_TextBox.Text.Length > 0 && passwordBox.Password.Length > 0)
            {
                logIn_Button.Visibility = Visibility.Visible;
            }
            else
            {
                logIn_Button.Visibility = Visibility.Collapsed;
            }
        }

        private async void logIn_Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var RootFrame = Window.Current.Content as Frame;
            List<playerProfile> existingPlayers = new List<playerProfile>();
            string errorString = null;

            usr.userName = userName_TextBox.Text;
            usr.password = passwordBox.Password;

            progressRing.IsActive = true;
            progressRing.Visibility = Visibility.Visible;

            userName_TextBox.IsEnabled = passwordBox.IsEnabled = logIn_Button.IsEnabled = false;

            try
            {
                existingPlayers = await playerTable.Where(playerProfile => playerProfile.userName == userName_TextBox.Text
                && playerProfile.password == passwordBox.Password).ToListAsync();

                if (existingPlayers.Count() == 0)
                {
                    MessageDialog msg = new MessageDialog("It looks like this is your first time logging in! Would you like to create a character?");

                    msg.Commands.Add(new UICommand("Yes"));
                    msg.Commands.Add(new UICommand("No"));

                    var confirm = await msg.ShowAsync();

                    if (confirm.Label.Equals("Yes"))
                    {
                        RootFrame.Navigate(typeof(characterPage), usr);
                    }
                    else
                    {
                        userName_TextBox.Text = "";
                        passwordBox.Password = "";
                    }

                    logIn_Button.Visibility = Visibility.Collapsed;
                }
                else
                {
                    sdr.Id = existingPlayers[0].Id;
                    sdr.userName = usr.userName;
                    RootFrame.Navigate(typeof(mainMenu), sdr);
                }

                progressRing.IsActive = false;
                progressRing.Visibility = Visibility.Collapsed;

                userName_TextBox.IsEnabled = passwordBox.IsEnabled = logIn_Button.IsEnabled = true;

                Window.Current.Content = RootFrame;
                Window.Current.Activate();
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                errorString = ex.Message;

                progressRing.IsActive = false;
                progressRing.Visibility = Visibility.Collapsed;

                userName_TextBox.IsEnabled = passwordBox.IsEnabled = logIn_Button.IsEnabled = true;
                userName_TextBox.Text = passwordBox.Password = "";

                logIn_Button.Visibility = Visibility.Collapsed;
            }

            if (errorString != null)
            {
                MessageDialog d = new MessageDialog(errorString);
                await d.ShowAsync();
            }      
        }
    }
}
