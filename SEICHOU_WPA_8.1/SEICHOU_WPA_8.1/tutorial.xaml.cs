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
    public sealed partial class tutorial : Page
    {
        playerProfile usr;
        sender sdr = new sender();
        int counter = 1;

        public tutorial()
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
            sdr.Id = usr.Id;
            sdr.selectedlevel = 0;

            Dialog.Text = "Be careful " + usr.name + "! This monster looks ferocious! I am the village elder, let me at least explain the basics of fighting to you!";
        }

        private void background_Tapped(object sender, TappedRoutedEventArgs ee)
        {

            //counter keeps track of when the player clicks on the screen.
            counter++;
            if (counter == 2)
            {
                background.Visibility = Visibility.Collapsed;
                Dialog.Text = "This is the turn timer(indicated by the orange bar). You have ten seconds to complete your turn. A turn is consists of you eiher picking a particle, or the turn timer running out.";
            }
            else if (counter == 3)
            {
                background1.Visibility = Visibility.Collapsed;
                Dialog.Text = "The yellow bar is your health and the green bar is the monster's health. ";

            }
            else if(counter == 4)
            {
                background2.Visibility = Visibility.Collapsed;
                Dialog.Visibility = Visibility.Collapsed;
                Dialog2.Visibility = Visibility.Visible;
                villageelder.Visibility = Visibility.Collapsed;
                villageelder2.Visibility = Visibility.Visible;
                Dialog2.Text = "Defeat the monster by choosing particles that best fit the sentence! If you answer correctly, the monster's health will decrease! If you answer incorrectly, the monster might attack!";
            }
            else if (counter == 5)
            {

                Dialog2.Text = "After you choose a particle, either a green circle or a red cross will appear in the box you chose. This indicates whether you answer the question right or wrong. The green circle means correct, while the red X means wrong. ";

            }
            else if (counter == 6)
            {
                background3.Visibility = Visibility.Collapsed;

                Dialog.Visibility = Visibility.Visible;
                Dialog2.Visibility = Visibility.Collapsed;
                villageelder.Visibility = Visibility.Visible;
                villageelder2.Visibility = Visibility.Collapsed;

                Dialog.Text = "Each answer you give decreases the count-down or ''CD'' next to the monster's health. When the CD reaches zero, the monster will attack!";

            }
            else if (counter == 7)
            {
                background4.Visibility = Visibility.Collapsed;
                

                Dialog.Text = "Each monster you kill gives you coins. When you have enough coins, you can buy boosts at the shop to help you get pass difficult levels. To use the boosts, just simply tap them.";
            }
            else if (counter == 8)
            {
                background4.Visibility = Visibility.Collapsed;
                background.Visibility = Visibility.Visible;

                Dialog.Text = "The sentence and particles will reset after every turn. All these steps will continue until you or the monster's health reaches zero! Now go and defeat the monster!";
            }
            else if (counter == 9)
            {
                Dialog.Text = "Hint: Each level, except the tutorial, gives you stars. One star: Completing level. Two stars: Picking the wrong particle two time or less. Three stars: Picking all particles correctly. Also, easy has one monster to defeat, medium has two monsters, and hard has three monsters.";

            }
            else
            {
                var rootFrame = Window.Current.Content as Frame;
                rootFrame.Navigate(typeof(battleSequence), sdr);
                Window.Current.Content = rootFrame;
                Window.Current.Activate();
            }
        }


        // There are a total of eight pictures that explain the tutorial, each layegreen on one another. When the screen is tapped, 
        // the current page becomes invisible and the next page becomes visible. The eigth or last page navigates to the battle sequence
        // page

        //private void tutorial1_8_Tapped(object sender, TappedRoutedEventArgs e)
        //{
        //    tutorial1_8.Visibility = Visibility.Collapsed;
        //}

        //private void tutorial2_8_Tapped(object sender, TappedRoutedEventArgs e)
        //{
        //    tutorial2_8.Visibility = Visibility.Collapsed;
        //}

        //private void tutorial3_8_Tapped(object sender, TappedRoutedEventArgs e)
        //{
        //    tutorial3_8.Visibility = Visibility.Collapsed;
        //}

        //private void tutorial4_8_Tapped(object sender, TappedRoutedEventArgs e)
        //{
        //    tutorial4_8.Visibility = Visibility.Collapsed;
        //}

        //private void tutorial5_8_Tapped(object sender, TappedRoutedEventArgs e)
        //{
        //    tutorial5_8.Visibility = Visibility.Collapsed;
        //}

        //private void tutorial6_8_Tapped(object sender, TappedRoutedEventArgs e)
        //{
        //    tutorial6_8.Visibility = Visibility.Collapsed;
        //}

        //private void tutorial7_8_Tapped(object sender, TappedRoutedEventArgs e)
        //{
        //    tutorial7_8.Visibility = Visibility.Collapsed;
        //}

        //private void tutorial8_8_Tapped(object sender, TappedRoutedEventArgs e)
        //{
        //    var rootFrame = Window.Current.Content as Frame;

        //    rootFrame.Navigate(typeof(battleSequence), usr);
        //    Window.Current.Content = rootFrame;
        //    Window.Current.Activate();
        //}
    }
}
