using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class resultsScreen : Page
    {
        sender sdr = new sender() { isLoaded = false };
        IMobileServiceTable<playerResults> resultsTable = App.MobileService.GetTable<playerResults>();
        bool first = false, canNavigate = false;
        int row = 0;
        bool[] isDuplicateButton = { false, false, false, false, false, false };

        public resultsScreen()
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

            for (int i = 0; i < sdr.answersRESULTS.Count(); i++)
            {
                if (sdr.answersRESULTS[i] == "\n")
                {
                    switch (sdr.answersRESULTS[i - 1])
                    {
                        case "を":
                            if (isDuplicateButton[0] == false)
                            {
                                showResults("を");
                                createUI("を");
                            }

                            isDuplicateButton[0] = true;
                            break;

                        case "に":
                            if (isDuplicateButton[1] == false)
                            {
                                showResults("に");
                                createUI("に");
                            }

                            isDuplicateButton[1] = true;
                            break;

                        case "が":
                            if (isDuplicateButton[2] == false)
                            {
                                showResults("が");
                                createUI("が");
                            }

                            isDuplicateButton[2] = true;
                            break;

                        case "で":
                            if (isDuplicateButton[3] == false)
                            {
                                showResults("で");
                                createUI("で");
                            }

                            isDuplicateButton[3] = true;
                            break;

                        case "の":
                            if (isDuplicateButton[4] == false)
                            {
                                showResults("の");
                                createUI("の");
                            }

                            isDuplicateButton[4] = true;
                            break;

                        case "は":
                            if (isDuplicateButton[5] == false)
                            {
                                showResults("は");
                                createUI("は");
                            }

                            isDuplicateButton[5] = true;
                            break;
                    }
                }
            }

            canNavigate = true;

            Button mainMenuButton = new Button();

            if (sdr.selectedlevel >= 1)
            {
                mainMenuButton.Content = "Level Select";
            }
            else
            {
                mainMenuButton.Content = "Main Menu";
            }

            if (row > 1)
            {
                Grid.SetRow(mainMenuButton, row);
            }

            mainMenuButton.HorizontalAlignment = HorizontalAlignment.Center;
            mainMenuButton.VerticalAlignment = VerticalAlignment.Bottom;
            mainMenuButton.Tapped += MainMenu_Tapped;

            grid.Children.Add(mainMenuButton);

            if (!sdr.isLoaded)
            {
                playerResults pResults = new playerResults() { userName = sdr.userName };
                //pResults.Id = sdr.Id;

                int n = sdr.answersRESULTS.Count() - 2;
                bool isRight = true;

                for (int i = sdr.sentencesRESULTS.Count() - 2; i >= 0; i--)
                {
                    for (int j = n; j >= 0; j--)
                    {
                        if (sdr.answersRESULTS[j] == "\n")
                        {
                            n = j - 1;
                            i--;
                            isRight = true;
                            break;
                        }
                        if (isRight)
                        {
                            pResults.isRight = true;
                        }
                        else
                        {
                            pResults.isRight = false;
                        }

                        pResults.Id = Guid.NewGuid().ToString();
                        pResults.sentence = sdr.sentencesRESULTS[i];
                        pResults.answer = sdr.answersRESULTS[j];

                        await resultsTable.InsertAsync(pResults);
                        isRight = false;
                    }
                }

                sdr.isLoaded = true;
            }
        }

        private void MainMenu_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //This if statement sends the user to main menu if they just completed the tutorial and if the user selected a level it sends them back to level select
            if (sdr.selectedlevel >= 1)
            {
                sdr.tempA.Clear();
                sdr.tempS.Clear();
                sdr.answersRESULTS.Clear();
                sdr.sentencesRESULTS.Clear();

                grid.Children.Clear();

                var rootFrame = Window.Current.Content as Frame;

                rootFrame.Navigate(typeof(levelselect), sdr);

                Window.Current.Content = rootFrame;
                Window.Current.Activate();
            }
            else
            {
                sdr.tempA.Clear();
                sdr.tempS.Clear();
                sdr.answersRESULTS.Clear();
                sdr.sentencesRESULTS.Clear();

                grid.Children.Clear();

                var rootFrame = Window.Current.Content as Frame;

                rootFrame.Navigate(typeof(mainMenu), sdr);

                Window.Current.Content = rootFrame;
                Window.Current.Activate();
            }

        }

        private void WaButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            showResults("は");
        }

        private void NoButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            showResults("の");
        }

        private void DeButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            showResults("で");
        }

        private void GaButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            showResults("が");
        }

        private void NiButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            showResults("に");
        }

        private void WoButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            showResults("を");
        }

        private void createUI(string s)
        {
            Button b = new Button() { Content = s };
            ProgressBar p = new ProgressBar();
            p.Background = new SolidColorBrush(Color.FromArgb(100, 255, 255, 255));
            p.Background.Opacity = 0.5;
            p.Height = 25;
            p.Width = 225;

            double pTotal = 0, pWrong = 0, pCorrect = 0;

            b.HorizontalAlignment = HorizontalAlignment.Left;
            p.HorizontalAlignment = HorizontalAlignment.Right;

            // "を", "に", "が", "で", "の", "は" 
            switch (s)
            {
                case "を":
                    b.Tapped += WoButton_Tapped;
                    break;
                case "に":
                    b.Tapped += NiButton_Tapped;
                    break;
                case "が":
                    b.Tapped += GaButton_Tapped;
                    break;
                case "で":
                    b.Tapped += DeButton_Tapped;
                    break;
                case "の":
                    b.Tapped += NoButton_Tapped;
                    break;
                case "は":
                    b.Tapped += WaButton_Tapped;
                    break;
            }

            for (int i = 0; i < sdr.tempA.Count(); i++)
            {
                if (sdr.tempA[i] != "\n")
                {
                    pTotal++;
                }
                else
                {
                    pCorrect++;
                }
            }

            p.Maximum = pTotal;
            pWrong = pTotal - pCorrect;
            p.Value = pTotal - pWrong;

            if (p.Value / pTotal == 0 || p.Value / pTotal >= 0.7)
            {
                p.Foreground = new SolidColorBrush(Colors.Green);
            }
            else if (p.Value / pTotal >= 0.4 && p.Value / pTotal < 0.7)
            {
                p.Foreground = new SolidColorBrush(Colors.Yellow);
            }
            else if (p.Value / pTotal > 0 && p.Value / pTotal < 0.4)
            {
                p.Foreground = new SolidColorBrush(Colors.Red);
            }

            if (!first)
            {
                b.VerticalAlignment = VerticalAlignment.Center;
                p.VerticalAlignment = VerticalAlignment.Center;

                RowDefinition start = new RowDefinition();
                start.Height = new GridLength(1, GridUnitType.Star);
                grid.RowDefinitions.Add(start);

                first = true;
            }
            else
            {
                RowDefinition next = new RowDefinition();
                next.Height = new GridLength(1, GridUnitType.Star);
                grid.RowDefinitions.Add(next);

                b.VerticalAlignment = VerticalAlignment.Center;
                p.VerticalAlignment = VerticalAlignment.Center;

                b.Margin = new Thickness(0, -50, 0, 0);
                p.Margin = b.Margin;
            }

            Grid.SetRow(b, row);
            Grid.SetRow(p, row);

            row += 1;

            grid.Children.Add(b);
            grid.Children.Add(p);
        }

        private void showResults(string particle)
        {
            sdr.tempA.Clear();
            sdr.tempS.Clear();

            int i = sdr.answersRESULTS.Count() - 1;
            int j = sdr.sentencesRESULTS.Count() - 1;

            while (i >= 0)
            {
                i--;

                if (i < 0) { break; }

                j--;

                if (sdr.answersRESULTS[i] == particle)
                {
                    while (sdr.answersRESULTS[i] != "\n")
                    {
                        if (i == 0)
                        {
                            sdr.tempA.Add(sdr.answersRESULTS[i]);
                            break;
                        }

                        sdr.tempA.Add(sdr.answersRESULTS[i]);
                        i--;
                    }

                    sdr.tempA.Add("\n");

                    sdr.tempS.Add(sdr.sentencesRESULTS[j]);
                    sdr.tempS.Add("\n");

                    if (i != 0)
                    {
                        j--;
                    }
                }
                else
                {
                    while (sdr.answersRESULTS[i] != "\n" && i != 0)
                    {
                        i--;
                    }

                    if (i != 0)
                    {
                        j--;
                    }
                }
            }

            if (canNavigate)
            {
                var rootFrame = Window.Current.Content as Frame;

                sdr.particle = particle;
                rootFrame.Navigate(typeof(moreResults), sdr);

                Window.Current.Content = rootFrame;
                Window.Current.Activate();
            }
        }
    }
}
