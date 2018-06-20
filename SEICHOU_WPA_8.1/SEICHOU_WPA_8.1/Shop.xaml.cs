using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
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
    public sealed partial class Shop : Page
    {
        sender sdr;
        playerProfile usr;
        IMobileServiceTable<playerProfile> playerTable = App.MobileService.GetTable<playerProfile>();
        StorageFolder local = ApplicationData.Current.LocalFolder;
      
        public Shop()
        {
            
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            sdr = e.Parameter as sender;
            usr = await playerTable.LookupAsync(sdr.Id);

            progressRing.IsEnabled = true;
            progressRing.Visibility = Visibility.Visible;

            timecover.Visibility = healcover.Visibility = Visibility.Visible;

            
            totaltime.Text = "Time Boost Quantity: " + usr.timerpowerup;
            totalheal.Text = "Heal Boost Quantity: " + usr.healpowerup;
            coins.Text = "Coins: " + usr.coins;
            buyheal.IsEnabled = buytime.IsEnabled = false;

            progressRing.IsEnabled = false;
            progressRing.Visibility = Visibility.Collapsed;

            buyheal.IsEnabled = buytime.IsEnabled = true;
            timecover.Visibility = healcover.Visibility = closedescription.Visibility = description.Visibility = Visibility.Collapsed;
        }

        private void heal_Tapped(object sender, TappedRoutedEventArgs e)
        {
            timecover.Visibility = healcover.Visibility = Visibility.Visible;
            back.IsEnabled = false;
            closedescription.Visibility= description.Visibility = Visibility.Visible;
            description.Text = "Heal Boost:  Heal boost heals your character back to full health when the boost is tapped in game.";
            buyheal.IsEnabled = buytime.IsEnabled = false;    
        }

        private void closedescription_Tapped(object sender, TappedRoutedEventArgs e)
        {
            timecover.Visibility = healcover.Visibility = Visibility.Collapsed;
            back.IsEnabled = true;
            description.Visibility = closedescription.Visibility = Visibility.Collapsed;
            buyheal.IsEnabled = buytime.IsEnabled = true;

        }

        private void time_Tapped(object sender, TappedRoutedEventArgs e)
        {
            timecover.Visibility = healcover.Visibility = Visibility.Visible;
            back.IsEnabled = false;
            closedescription.Visibility = description.Visibility = Visibility.Visible;
            description.Text = "Time Boost:  Time boost stops the time for 5 seconds when the boost is tapped in game.";
            buyheal.IsEnabled = buytime.IsEnabled = false;

        }

        private void back_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(mainMenu), sdr);
            Window.Current.Content = rootFrame;
            Window.Current.Activate();
        }

        private void buytime_Tapped(object sender, TappedRoutedEventArgs e)
        {
            wannabuy.Text = "Do you want to buy the time boost for 1,000 coins?";
            wannabuy.Visibility = yes.Visibility = no.Visibility = Visibility.Visible;
            timecover.Visibility = healcover.Visibility = Visibility.Visible;
            buytime.IsEnabled = buyheal.IsEnabled = false;
            back.IsEnabled = false;
            yes.IsEnabled = false;
            if(usr.coins >= 1)
            {
                yes.IsEnabled = true;
            }
        }

        private async void yes_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (wannabuy.Text == "Do you want to buy the time boost for 1,000 coins?")
            {
                if (usr.coins >= 1000)
                {
                    usr.coins -= 1000;
                    usr.timerpowerup += 1;
                    totaltime.Text = "Time Boost Quantity: " + usr.timerpowerup;
                    await playerTable.UpdateAsync(usr);
                }
                else
                {
                    MessageDialog msg = new MessageDialog("You don't have enough coins to buy this item!");
                    await msg.ShowAsync();
                }
            }
            else
            {
                if (usr.coins >= 1000)
                {
                    usr.coins -= 1000;
                    usr.healpowerup += 1;
                    totalheal.Text = "Heal Boost Quantity: " + usr.healpowerup;
                    await playerTable.UpdateAsync(usr);
                }
                else
                {
                    MessageDialog msg = new MessageDialog("You don't have enough coins to buy this item!");
                    await msg.ShowAsync();
                }
            }

            wannabuy.Visibility = yes.Visibility = no.Visibility = Visibility.Collapsed;
            timecover.Visibility = healcover.Visibility = Visibility.Collapsed;
            back.IsEnabled = true;
            buytime.IsEnabled = buyheal.IsEnabled = true;
            coins.Text = "Coins: " + usr.coins;
        }

        private void no_Tapped(object sender, TappedRoutedEventArgs e)
        {
            wannabuy.Visibility = yes.Visibility = no.Visibility = Visibility.Collapsed;
            timecover.Visibility = healcover.Visibility = Visibility.Collapsed;
            back.IsEnabled = true;
            buytime.IsEnabled = buyheal.IsEnabled = true;
        }

        private void buyheal_Tapped(object sender, TappedRoutedEventArgs e)
        {
            wannabuy.Text = "Do you want to buy the heal boost for 1,000 coins?";
            wannabuy.Visibility = yes.Visibility = no.Visibility = Visibility.Visible;
            timecover.Visibility = healcover.Visibility = Visibility.Visible;
            buytime.IsEnabled = buyheal.IsEnabled = false;
            back.IsEnabled = false;
            yes.IsEnabled = false;
            if (usr.coins >= 1)
            {
                yes.IsEnabled = true;
            }
        }
    }
}
