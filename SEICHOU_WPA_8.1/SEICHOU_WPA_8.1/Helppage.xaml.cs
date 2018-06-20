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
    public sealed partial class Helppage : Page
    {
        sender sdr;
        playerProfile usr;
        IMobileServiceTable<playerProfile> playerTable = App.MobileService.GetTable<playerProfile>();
        StorageFolder local = ApplicationData.Current.LocalFolder;
        int counter = 1;
        public Helppage()
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
            description.Text = "This is the turn timer(indicated by the orange bar). You have ten seconds to complete your turn. A turn is consists of you either picking a particle, or the turn timer running out.";
            previous.IsEnabled = false;
        }

        private void next_Tapped(object sender, TappedRoutedEventArgs e)
        {
            counter++;

            if (counter == 8)
            {
                next.IsEnabled = false;
            }
            else
            {
                previous.IsEnabled = true;
            }
            if (counter == 2)
            {
                background1.Visibility = Visibility.Collapsed;
                description.Text = "The yellow bar is your health and the green bar is the monster's health. ";

            }
            else if (counter == 3)
            {
                background2.Visibility = Visibility.Collapsed;
                description.Text = "Defeat the monster by choosing particles that best fit the sentence! If you answer correctly, the monster's health will decrease! If you answer incorrectly, the monster might attack!";
            }
            else if (counter == 4)
            {

                description.Text = "After you choose a particle, either a green circle or a red cross will appear in the box you chose. This indicates whether you answer the question right or wrong. The green circle means correct, while the red X means wrong. ";

            }
            else if (counter == 5)
            {
                background3.Visibility = Visibility.Collapsed;
                description.Text = "Each answer you give decreases the count-down or ''CD'' next to the monster's health. When the CD reaches zero, the monster will attack!";

            }
            else if (counter == 6)
            {
                background4.Visibility = Visibility.Collapsed;
                description.Visibility = Visibility.Visible;
                description2.Visibility = Visibility.Collapsed;
                description.Text = "Each monster you kill gives you coins. When you have enough coins, you can buy boosts at the shop to help you get pass difficult levels. To use the boosts, just simply tap them.";
            }
            else if (counter == 7)
            {
                background5.Visibility = Visibility.Collapsed;
                description.Height = 115;
                description.Visibility = Visibility.Collapsed;
                description2.Visibility = Visibility.Visible;
                description2.Text = "The sentence and particles will reset after every turn. All these steps will continue until you or the monster's health reaches zero! Now go and defeat the monster!";
            }
            else
            {
                description.Height = 150;
                description.Visibility = Visibility.Collapsed;
                description2.Visibility = Visibility.Visible;
                description2.Text = "Hint: Each level, except the tutorial, gives you stars. One star: Completing level. Two stars: Picking the wrong particle two time or less. Three stars: Picking all particles correctly. Also, easy has one monster to defeat, medium has two monsters, and hard has three monsters.";
            }
        }


        private void previous_Tapped(object sender, TappedRoutedEventArgs e)
        {
            counter--;

            if (counter == 1)
            {
                previous.IsEnabled = false;
            }
            else
            {
                next.IsEnabled = true;
            }
            if (counter == 1)
            {
                background1.Visibility = Visibility.Visible;
                description.Text = "This is the turn timer(indicated by the orange bar). You have ten seconds to complete your turn. A turn is consists of you eiher picking a particle, or the turn timer running out.";

            }
            else if (counter == 2)
            {
                background2.Visibility = Visibility.Visible;
                description.Text = "The yellow bar is your health and the green bar is the monster's health. ";

            }
            else if (counter == 3)
            {
                background3.Visibility = Visibility.Visible;
                description.Text = "Defeat the monster by choosing particles that best fit the sentence! If you answer correctly, the monster's health will decrease! If you answer incorrectly, the monster might attack!";
            }
            else if (counter == 4)
            {
                background3.Visibility = Visibility.Visible;
                description.Text = "After you choose a particle, either a green circle or a red cross will appear in the box you chose. This indicates whether you answer the question right or wrong. The green circle means correct, while the red X means wrong. ";

            }
            else if (counter == 5)
            {
                background4.Visibility = Visibility.Visible;
                description.Text = "Each answer you give decreases the count-down or ''CD'' next to the monster's health. When the CD reaches zero, the monster will attack!";

            }
            else if (counter == 6)
            {
                background5.Visibility = Visibility.Visible;
                description.Visibility = Visibility.Visible;
                description2.Visibility = Visibility.Collapsed;
                description.Text = "Each monster you kill gives you coins. When you have enough coins, you can buy boosts at the shop to help you get pass difficult levels. To use the boosts, just simply tap them.";
            }
            else if (counter == 7)
            {
                background5.Visibility = Visibility.Collapsed;
                description.Height = 115;
                description.Visibility = Visibility.Collapsed;
                description2.Visibility = Visibility.Visible;

                description2.Text = "The sentence and particles will reset after every turn. All these steps will continue until you or the monster's health reaches zero! Now go and defeat the monster!";
            }
            else
            {
                description.Height = 150;
                description.Visibility = Visibility.Collapsed;
                description2.Visibility = Visibility.Visible;

                description2.Text = "Hint: Each level, except the tutorial, gives you stars. One star: Completing level. Two stars: Picking the wrong particle two time or less. Three stars: Picking all particles correctly. Also, easy has one monster to defeat, medium has two monsters, and hard has three monsters.";
            }
        }

        private void mainmenu_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(mainMenu), sdr);
            Window.Current.Content = rootFrame;
            Window.Current.Activate();
        }
    }
}
