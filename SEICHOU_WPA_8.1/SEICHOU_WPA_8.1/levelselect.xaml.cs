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
    public sealed partial class levelselect : Page
    {
        sender sdr;
        playerProfile usr;
        levels lookUpData;
        IMobileServiceTable<playerProfile> playerTable = App.MobileService.GetTable<playerProfile>();
        IMobileServiceTable<levels> levelsTable = App.MobileService.GetTable<levels>();
        int difficulty;
        StorageFolder local = ApplicationData.Current.LocalFolder;

        public levelselect()
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
            loading.Visibility =loadingtext.Visibility = Visibility.Visible;
            loadingtext.Text = "Loading.";

            sdr = e.Parameter as sender;          

            Easy.IsEnabled = Medium.IsEnabled = Hard.IsEnabled = false;

            usr = await playerTable.LookupAsync(sdr.Id);
            lookUpData = await levelsTable.LookupAsync(sdr.Id);

            Easytab.Visibility = Visibility.Visible;
            MediumTab.Visibility = HardTab.Visibility = Visibility.Collapsed;

            MedRequirements.Visibility = HardRequirements.Visibility = Visibility.Collapsed;

            buttonbackgroundeasy1.Visibility = buttonbackgroundeasy2.Visibility = buttonbackgroundeasy3.Visibility = buttonbackgroundeasy4.Visibility = Visibility.Visible;
            buttonbackgroundmedium1.Visibility = buttonbackgroundmedium2.Visibility = buttonbackgroundmedium3.Visibility = buttonbackgroundmedium4.Visibility = Visibility.Collapsed;
            buttonbackgroundhard1.Visibility = buttonbackgroundhard2.Visibility = buttonbackgroundhard3.Visibility = buttonbackgroundhard4.Visibility = Visibility.Collapsed;
            Exitbtn.Background = new SolidColorBrush(Windows.UI.Colors.Gray);
            MainMenubtn.Background = new SolidColorBrush(Windows.UI.Colors.Gray);
            difficulty = 1;

            lookUpData.TotalEasy = lookUpData.L1 + lookUpData.L2 + lookUpData.L3 + lookUpData.L4;
            lookUpData.TotalMedium = lookUpData.L5 + lookUpData.L6 + lookUpData.L7 + lookUpData.L8;
            lookUpData.TotalHard = lookUpData.L9 + lookUpData.L10 + lookUpData.L11 + lookUpData.L12;
            await System.Threading.Tasks.Task.Delay(800);
            loadingtext.Text = "Loading..";
            JObject jo = new JObject();

            jo.Add("id", lookUpData.Id);
            jo.Add("TotalEasy", lookUpData.TotalEasy);
            jo.Add("TotalMedium", lookUpData.TotalMedium);
            jo.Add("TotalHard", lookUpData.TotalHard);

            await levelsTable.UpdateAsync(jo);

            starcount.Text = "" + lookUpData.TotalEasy + "/12";
            if (lookUpData.L1 == 1)
            {
                FirstStarLeft.Opacity = 1;
                FirstStarMid.Opacity = .2;
                FirstStarRight.Opacity = .2;
            }
            else if (lookUpData.L1 == 2)
            {
                FirstStarLeft.Opacity = 1;
                FirstStarMid.Opacity = 1;
                FirstStarRight.Opacity = .2;
            }
            else if (lookUpData.L1 == 3)
            {
                FirstStarLeft.Opacity = 1;
                FirstStarMid.Opacity = 1;
                FirstStarRight.Opacity = 1;
            }
            else
            {
                FirstStarLeft.Opacity = .2;
                FirstStarMid.Opacity = .2;
                FirstStarRight.Opacity = .2;
            }
            //This checks the stars for the second stage
            if (lookUpData.L2 == 1)
            {
                SecondStarLeft.Opacity = 1;
                SecondStarMid.Opacity = .2;
                SecondStarRight.Opacity = .2;
            }
            else if (lookUpData.L2 == 2)
            {
                SecondStarLeft.Opacity = 1;
                SecondStarMid.Opacity = 1;
                SecondStarRight.Opacity = .2;
            }
            else if (lookUpData.L2 == 3)
            {
                SecondStarLeft.Opacity = 1;
                SecondStarMid.Opacity = 1;
                SecondStarRight.Opacity = 1;
            }
            else
            {
                SecondStarLeft.Opacity = .2;
                SecondStarMid.Opacity = .2;
                SecondStarRight.Opacity = .2;
            }

            if (lookUpData.L3 == 1)
            {
                ThirdStarLeft.Opacity = 1;
                ThirdStarMid.Opacity = .2;
                ThirdStarRight.Opacity = .2;
            }
            else if (lookUpData.L3 == 2)
            {
                ThirdStarLeft.Opacity = 1;
                ThirdStarMid.Opacity = 1;
                ThirdStarRight.Opacity = .2;
            }
            else if (lookUpData.L3 == 3)
            {
                ThirdStarLeft.Opacity = 1;
                ThirdStarMid.Opacity = 1;
                ThirdStarRight.Opacity = 1;
            }
            else
            {
                ThirdStarLeft.Opacity = .2;
                ThirdStarMid.Opacity = .2;
                ThirdStarRight.Opacity = .2;
            }

            if (lookUpData.L4 == 1)
            {
                FourthStarLeft.Opacity = 1;
                FourthStarMid.Opacity = .2;
                FourthStarRight.Opacity = .2;
            }
            else if (lookUpData.L4 == 2)
            {
                FourthStarLeft.Opacity = 1;
                FourthStarMid.Opacity = 1;
                FourthStarRight.Opacity = .2;
            }
            else if (lookUpData.L4 == 3)
            {
                FourthStarLeft.Opacity = 1;
                FourthStarMid.Opacity = 1;
                FourthStarRight.Opacity = 1;
            }
            else
            {
                FourthStarLeft.Opacity = .2;
                FourthStarMid.Opacity = .2;
                FourthStarRight.Opacity = .2;
            }


            if (ThirdStarLeft.Opacity == 1)
            {
                button.IsEnabled = button1.IsEnabled = button2.IsEnabled = button3.IsEnabled = true;
            }
            else if (SecondStarLeft.Opacity == 1)
            {
                button.IsEnabled = button1.IsEnabled = button2.IsEnabled = true;
                button3.IsEnabled = false;
            }
            else if (FirstStarLeft.Opacity == 1)
            {
                button.IsEnabled = button1.IsEnabled = true;
                button2.IsEnabled = button3.IsEnabled = false;
            }
            else
            {
                button.IsEnabled = true;
                button1.IsEnabled = button2.IsEnabled = button3.IsEnabled = false;
            }

            coins.Text = "Coins: " + usr.coins;

            Easy.IsEnabled = Medium.IsEnabled = Hard.IsEnabled = true;
            
            loadingtext.Text = "Loading...";
            loading.Visibility = loadingtext.Visibility = Visibility.Collapsed;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

            //sets resetgame to 1 which will reset the game so everything is new when the user enters the level again
            sdr.resetgame = 1;
            //This sets the selected level to the level the user is on. The difficult is based on if easy is clicked then it will set the level
            //to 1 and if medium is set, the difficulty will be 2 so it will set the selected level to 5 and so on.
            if (difficulty == 1)
            {
                sdr.selectedlevel = 1;
            }
            else if (difficulty == 2)
            {
                sdr.selectedlevel = 5;
            }
            else if (difficulty == 3)
            {
                sdr.selectedlevel = 9;
            }
            var rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(battleSequence), sdr);
            Window.Current.Content = rootFrame;
            Window.Current.Activate();

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            sdr.resetgame = 1;
            if (difficulty == 1)
            {
                sdr.selectedlevel = 2;
            }
            else if (difficulty == 2)
            {
                sdr.selectedlevel = 6;
            }
            else if (difficulty == 3)
            {
                sdr.selectedlevel = 10;
            }
            var rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(battleSequence), sdr);
            Window.Current.Content = rootFrame;
            Window.Current.Activate();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            sdr.resetgame = 1;
            if (difficulty == 1)
            {
                sdr.selectedlevel = 3;
            }
            else if (difficulty == 2)
            {
                sdr.selectedlevel = 7;
            }
            else if (difficulty == 3)
            {
                sdr.selectedlevel = 11;
            }
            var rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(battleSequence), sdr);
            Window.Current.Content = rootFrame;
            Window.Current.Activate();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            sdr.resetgame = 1;
            if (difficulty == 1)
            {
                sdr.selectedlevel = 4;
            }
            else if (difficulty == 2)
            {
                sdr.selectedlevel = 8;
            }
            else if (difficulty == 3)
            {
                sdr.selectedlevel = 12;
            }
            var rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(battleSequence), sdr);
            Window.Current.Content = rootFrame;
            Window.Current.Activate();
        }

        //This is the easy tab
        private void button4_Click(object sender, RoutedEventArgs e)
        {
            starcount.Text = "" + lookUpData.TotalEasy + "/12";
            MedRequirements.Visibility = HardRequirements.Visibility = Visibility.Collapsed;
            difficulty = 1;
            Easytab.Visibility = Visibility.Visible;
            MediumTab.Visibility = Visibility.Collapsed;
            HardTab.Visibility = Visibility.Collapsed;
            buttonbackgroundeasy1.Visibility = buttonbackgroundeasy2.Visibility = buttonbackgroundeasy3.Visibility = buttonbackgroundeasy4.Visibility = Visibility.Visible;
            buttonbackgroundmedium1.Visibility = buttonbackgroundmedium2.Visibility = buttonbackgroundmedium3.Visibility = buttonbackgroundmedium4.Visibility = Visibility.Collapsed;
            buttonbackgroundhard1.Visibility = buttonbackgroundhard2.Visibility = buttonbackgroundhard3.Visibility = buttonbackgroundhard4.Visibility = Visibility.Collapsed;
            //Easy.Background = new SolidColorBrush(Windows.UI.Colors.Black);
            //Medium.Background = new SolidColorBrush(Windows.UI.Colors.Gray);
            //Hard.Background = new SolidColorBrush(Windows.UI.Colors.Gray);
            button.Content = "Level 1";
            button1.Content = "Level 2";
            button2.Content = "Level 3";
            button3.Content = "Level 4";
            if (lookUpData.L1 == 1)
            {
                FirstStarLeft.Opacity = 1;
                FirstStarMid.Opacity = .2;
                FirstStarRight.Opacity = .2;
            }
            else if (lookUpData.L1 == 2)
            {
                FirstStarLeft.Opacity = 1;
                FirstStarMid.Opacity = 1;
                FirstStarRight.Opacity = .2;
            }
            else if (lookUpData.L1 == 3)
            {
                FirstStarLeft.Opacity = 1;
                FirstStarMid.Opacity = 1;
                FirstStarRight.Opacity = 1;
            }
            else
            {
                FirstStarLeft.Opacity = .2;
                FirstStarMid.Opacity = .2;
                FirstStarRight.Opacity = .2;
            }
            //This checks the stars for the second stage
            if (lookUpData.L2 == 1)
            {
                SecondStarLeft.Opacity = 1;
                SecondStarMid.Opacity = .2;
                SecondStarRight.Opacity = .2;
            }
            else if (lookUpData.L2 == 2)
            {
                SecondStarLeft.Opacity = 1;
                SecondStarMid.Opacity = 1;
                SecondStarRight.Opacity = .2;
            }
            else if (lookUpData.L2 == 3)
            {
                SecondStarLeft.Opacity = 1;
                SecondStarMid.Opacity = 1;
                SecondStarRight.Opacity = 1;
            }
            else
            {
                SecondStarLeft.Opacity = .2;
                SecondStarMid.Opacity = .2;
                SecondStarRight.Opacity = .2;
            }

            if (lookUpData.L3 == 1)
            {
                ThirdStarLeft.Opacity = 1;
                ThirdStarMid.Opacity = .2;
                ThirdStarRight.Opacity = .2;
            }
            else if (lookUpData.L3 == 2)
            {
                ThirdStarLeft.Opacity = 1;
                ThirdStarMid.Opacity = 1;
                ThirdStarRight.Opacity = .2;
            }
            else if (lookUpData.L3 == 3)
            {
                ThirdStarLeft.Opacity = 1;
                ThirdStarMid.Opacity = 1;
                ThirdStarRight.Opacity = 1;
            }
            else
            {
                ThirdStarLeft.Opacity = .2;
                ThirdStarMid.Opacity = .2;
                ThirdStarRight.Opacity = .2;
            }

            if (lookUpData.L4 == 1)
            {
                FourthStarLeft.Opacity = 1;
                FourthStarMid.Opacity = .2;
                FourthStarRight.Opacity = .2;
            }
            else if (lookUpData.L4 == 2)
            {
                FourthStarLeft.Opacity = 1;
                FourthStarMid.Opacity = 1;
                FourthStarRight.Opacity = .2;
            }
            else if (lookUpData.L4 == 3)
            {
                FourthStarLeft.Opacity = 1;
                FourthStarMid.Opacity = 1;
                FourthStarRight.Opacity = 1;
            }
            else
            {
                FourthStarLeft.Opacity = .2;
                FourthStarMid.Opacity = .2;
                FourthStarRight.Opacity = .2;
            }


            //Checks the stars so it can determine whether certain buttons are enabled or not
            if (ThirdStarLeft.Opacity == 1)
            {
                button.IsEnabled = button1.IsEnabled = button2.IsEnabled = button3.IsEnabled = true;
            }
            else if (SecondStarLeft.Opacity == 1)
            {
                button.IsEnabled = button1.IsEnabled = button2.IsEnabled = true;
                button3.IsEnabled = false;
            }
            else if (FirstStarLeft.Opacity == 1)
            {
                button.IsEnabled = button1.IsEnabled = true;
                button2.IsEnabled = button3.IsEnabled = false;
            }
            else
            {
                button.IsEnabled = true;
                button1.IsEnabled = button2.IsEnabled = button3.IsEnabled = false;
            }
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            HardRequirements.Visibility = Visibility.Collapsed;
            button.Content = "Level 5";
            button1.Content = "Level 6";
            button2.Content = "Level 7";
            button3.Content = "Level 8";
            starcount.Text = "" + lookUpData.TotalMedium + "/12";
            difficulty = 2;
            Easytab.Visibility = Visibility.Collapsed;
            MediumTab.Visibility = Visibility.Visible;
            HardTab.Visibility = Visibility.Collapsed;
            buttonbackgroundeasy1.Visibility = buttonbackgroundeasy2.Visibility = buttonbackgroundeasy3.Visibility = buttonbackgroundeasy4.Visibility = Visibility.Visible;
            buttonbackgroundmedium1.Visibility = buttonbackgroundmedium2.Visibility = buttonbackgroundmedium3.Visibility = buttonbackgroundmedium4.Visibility = Visibility.Collapsed;
            buttonbackgroundhard1.Visibility = buttonbackgroundhard2.Visibility = buttonbackgroundhard3.Visibility = buttonbackgroundhard4.Visibility = Visibility.Collapsed;
            if (lookUpData.L5 == 1)
            {
                FirstStarLeft.Opacity = 1;
                FirstStarMid.Opacity = .2;
                FirstStarRight.Opacity = .2;
            }
            else if (lookUpData.L5 == 2)
            {
                FirstStarLeft.Opacity = 1;
                FirstStarMid.Opacity = 1;
                FirstStarRight.Opacity = .2;
            }
            else if (lookUpData.L5 == 3)
            {
                FirstStarLeft.Opacity = 1;
                FirstStarMid.Opacity = 1;
                FirstStarRight.Opacity = 1;
            }
            else
            {
                FirstStarLeft.Opacity = .2;
                FirstStarMid.Opacity = .2;
                FirstStarRight.Opacity = .2;
            }
            //This checks the stars for the second stage
            if (lookUpData.L6 == 1)
            {
                SecondStarLeft.Opacity = 1;
                SecondStarMid.Opacity = .2;
                SecondStarRight.Opacity = .2;
            }
            else if (lookUpData.L6 == 2)
            {
                SecondStarLeft.Opacity = 1;
                SecondStarMid.Opacity = 1;
                SecondStarRight.Opacity = .2;
            }
            else if (lookUpData.L6 == 3)
            {
                SecondStarLeft.Opacity = 1;
                SecondStarMid.Opacity = 1;
                SecondStarRight.Opacity = 1;
            }
            else
            {
                SecondStarLeft.Opacity = .2;
                SecondStarMid.Opacity = .2;
                SecondStarRight.Opacity = .2;
            }

            if (lookUpData.L7 == 1)
            {
                ThirdStarLeft.Opacity = 1;
                ThirdStarMid.Opacity = .2;
                ThirdStarRight.Opacity = .2;
            }
            else if (lookUpData.L7 == 2)
            {
                ThirdStarLeft.Opacity = 1;
                ThirdStarMid.Opacity = 1;
                ThirdStarRight.Opacity = .2;
            }
            else if (lookUpData.L7 == 3)
            {
                ThirdStarLeft.Opacity = 1;
                ThirdStarMid.Opacity = 1;
                ThirdStarRight.Opacity = 1;
            }
            else
            {
                ThirdStarLeft.Opacity = .2;
                ThirdStarMid.Opacity = .2;
                ThirdStarRight.Opacity = .2;
            }

            if (lookUpData.L8 == 1)
            {
                FourthStarLeft.Opacity = 1;
                FourthStarMid.Opacity = .2;
                FourthStarRight.Opacity = .2;
            }
            else if (lookUpData.L8 == 2)
            {
                FourthStarLeft.Opacity = 1;
                FourthStarMid.Opacity = 1;
                FourthStarRight.Opacity = .2;
            }
            else if (lookUpData.L8 == 3)
            {
                FourthStarLeft.Opacity = 1;
                FourthStarMid.Opacity = 1;
                FourthStarRight.Opacity = 1;
            }
            else
            {
                FourthStarLeft.Opacity = .2;
                FourthStarMid.Opacity = .2;
                FourthStarRight.Opacity = .2;
            }
            if (lookUpData.TotalEasy < 10)
            {
                MedRequirements.Visibility = Visibility.Visible;
                button.IsEnabled = button1.IsEnabled = button2.IsEnabled = button3.IsEnabled = false;
            }
            else
            {
                MedRequirements.Visibility = Visibility.Collapsed;

                if (ThirdStarLeft.Opacity == 1)
                {
                    button.IsEnabled = button1.IsEnabled = button2.IsEnabled = button3.IsEnabled = true;
                }
                else if (SecondStarLeft.Opacity == 1)
                {
                    button.IsEnabled = button1.IsEnabled = button2.IsEnabled = true;
                    button3.IsEnabled = false;
                }
                else if (FirstStarLeft.Opacity == 1)
                {
                    button.IsEnabled = button1.IsEnabled = true;
                    button2.IsEnabled = button3.IsEnabled = false;
                }
                else if (lookUpData.L4 > 0)
                {
                    button.IsEnabled = true;
                    button1.IsEnabled = button2.IsEnabled = button3.IsEnabled = false;
                }
                else
                {
                    button.IsEnabled = button1.IsEnabled = button2.IsEnabled = button3.IsEnabled = false;
                }

            }
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            MedRequirements.Visibility = Visibility.Collapsed;
            button.Content = "Level 9";
            button1.Content = "Level 10";
            button2.Content = "Level 11";
            button3.Content = "Final Boss";
            starcount.Text = "" + lookUpData.TotalHard + "/12";
            difficulty = 3;
            Easytab.Visibility = Visibility.Collapsed;
            MediumTab.Visibility = Visibility.Collapsed;
            HardTab.Visibility = Visibility.Visible;
            buttonbackgroundeasy1.Visibility = buttonbackgroundeasy2.Visibility = buttonbackgroundeasy3.Visibility = buttonbackgroundeasy4.Visibility = Visibility.Visible;
            buttonbackgroundmedium1.Visibility = buttonbackgroundmedium2.Visibility = buttonbackgroundmedium3.Visibility = buttonbackgroundmedium4.Visibility = Visibility.Collapsed;
            buttonbackgroundhard1.Visibility = buttonbackgroundhard2.Visibility = buttonbackgroundhard3.Visibility = buttonbackgroundhard4.Visibility = Visibility.Collapsed;
            if (lookUpData.L9 == 1)
            {
                FirstStarLeft.Opacity = 1;
                FirstStarMid.Opacity = .2;
                FirstStarRight.Opacity = .2;
            }
            else if (lookUpData.L9 == 2)
            {
                FirstStarLeft.Opacity = 1;
                FirstStarMid.Opacity = 1;
                FirstStarRight.Opacity = .2;
            }
            else if (lookUpData.L9 == 3)
            {
                FirstStarLeft.Opacity = 1;
                FirstStarMid.Opacity = 1;
                FirstStarRight.Opacity = 1;
            }
            else
            {
                FirstStarLeft.Opacity = .2;
                FirstStarMid.Opacity = .2;
                FirstStarRight.Opacity = .2;
            }
            //This checks the stars for the second stage
            if (lookUpData.L10 == 1)
            {
                SecondStarLeft.Opacity = 1;
                SecondStarMid.Opacity = .2;
                SecondStarRight.Opacity = .2;
            }
            else if (lookUpData.L10 == 2)
            {
                SecondStarLeft.Opacity = 1;
                SecondStarMid.Opacity = 1;
                SecondStarRight.Opacity = .2;
            }
            else if (lookUpData.L10 == 3)
            {
                SecondStarLeft.Opacity = 1;
                SecondStarMid.Opacity = 1;
                SecondStarRight.Opacity = 1;
            }
            else
            {
                SecondStarLeft.Opacity = .2;
                SecondStarMid.Opacity = .2;
                SecondStarRight.Opacity = .2;
            }


            if (lookUpData.L11 == 1)
            {
                ThirdStarLeft.Opacity = 1;
                ThirdStarMid.Opacity = .2;
                ThirdStarRight.Opacity = .2;
            }
            else if (lookUpData.L11 == 2)
            {
                ThirdStarLeft.Opacity = 1;
                ThirdStarMid.Opacity = 1;
                ThirdStarRight.Opacity = .2;
            }
            else if (lookUpData.L11 == 3)
            {
                ThirdStarLeft.Opacity = 1;
                ThirdStarMid.Opacity = 1;
                ThirdStarRight.Opacity = 1;
            }
            else
            {
                ThirdStarLeft.Opacity = .2;
                ThirdStarMid.Opacity = .2;
                ThirdStarRight.Opacity = .2;
            }

            if (lookUpData.L12 == 1)
            {
                FourthStarLeft.Opacity = 1;
                FourthStarMid.Opacity = .2;
                FourthStarRight.Opacity = .2;
            }
            else if (lookUpData.L12 == 2)
            {
                FourthStarLeft.Opacity = 1;
                FourthStarMid.Opacity = 1;
                FourthStarRight.Opacity = .2;
            }
            else if (lookUpData.L12 == 3)
            {
                FourthStarLeft.Opacity = 1;
                FourthStarMid.Opacity = 1;
                FourthStarRight.Opacity = 1;
            }
            else
            {
                FourthStarLeft.Opacity = .2;
                FourthStarMid.Opacity = .2;
                FourthStarRight.Opacity = .2;
            }


            if (lookUpData.TotalMedium < 10)
            {
                HardRequirements.Visibility = Visibility.Visible;
                button.IsEnabled = button1.IsEnabled = button2.IsEnabled = button3.IsEnabled = false;
            }
            else
            {
                HardRequirements.Visibility = Visibility.Collapsed;

                if (ThirdStarLeft.Opacity == 1)
                {
                    button.IsEnabled = button1.IsEnabled = button2.IsEnabled = button3.IsEnabled = true;
                }
                else if (SecondStarLeft.Opacity == 1)
                {
                    button.IsEnabled = button1.IsEnabled = button2.IsEnabled = true;
                    button3.IsEnabled = false;
                }
                else if (FirstStarLeft.Opacity == 1)
                {
                    button.IsEnabled = button1.IsEnabled = true;
                    button2.IsEnabled = button3.IsEnabled = false;
                }
                else if (lookUpData.L8 > 0)
                {
                    button.IsEnabled = true;
                    button1.IsEnabled = button2.IsEnabled = button3.IsEnabled = false;
                }
                else
                {
                    button.IsEnabled = button1.IsEnabled = button2.IsEnabled = button3.IsEnabled = false;
                }

            }
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            var rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(mainMenu), sdr);
            Window.Current.Content = rootFrame;
            Window.Current.Activate();
        }
    }
}
