using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace SEICHOU_WPA_8._1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class moreResults : Page
    {
        sender sdr;

        // FOR ELDER FEEDBACK
        Stack<char> s = new Stack<char>();
        TextBox fromSentence;
        int cutPos = 0;
        const int MAX_CHAR_LENGTH = 125;

        public moreResults()
        {
            this.InitializeComponent();
            elderTextBox.AddHandler(TappedEvent, new TappedEventHandler(elderTextBox_Tapped), true);
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            sdr = e.Parameter as sender;

            int j, n = 0;
            bool hasWrong = false;

            TextBox wrongAns = new TextBox();

            for (int i = 0; i < sdr.tempS.Count(); i++)
            {
                CreateStackChild(sdr.tempS[i], Colors.White);
                i++;

                for (j = n; j < sdr.tempA.Count(); j++)
                {
                    if (sdr.tempA[j] == "\n")
                    {
                        n = j + 1;

                        if (hasWrong)
                        {
                            stackPanel.Children.Add(new TextBox()
                            {
                                FontFamily = new FontFamily("Honoka Antique-Maru"),
                                Background = null,
                                Foreground = new SolidColorBrush(Colors.Red),
                                IsHitTestVisible = false,
                                IsReadOnly = true,
                                TextWrapping = TextWrapping.Wrap,
                                Text = wrongAns.Text
                            });

                            wrongAns.Text = "";
                            hasWrong = false;
                        }

                        break;
                    }

                    if (j == n)
                    {
                        CreateStackChild(sdr.tempA[j] + " ", Colors.Green);
                    }
                    else
                    {
                        wrongAns.Text += sdr.tempA[j] + " ";
                        hasWrong = true;
                    }
                }
            }
        }

        private void CreateStackChild(string text, Color color)
        {
            TextBox t = new TextBox()
            {
                FontFamily = new FontFamily("Honoka Antique-Maru"),
                Background = null,
                Foreground = new SolidColorBrush(color),
                IsReadOnly = true,
                IsTabStop = false,
                IsTextPredictionEnabled = false,
                TextWrapping = TextWrapping.Wrap,
            };

            if (text.Contains("_"))
            {
                t.AddHandler(TappedEvent, new TappedEventHandler(T_Tapped), true);
            }
            else
            {
                t.IsHitTestVisible = false;
            }

            t.Text += text;

            stackPanel.Children.Add(t);
        }

        private async Task<string> OpenFile(string path, string sentence)
        {
            string line = "";

            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///Assets/More Results" + path));
            using (StreamReader sRead = new StreamReader(await file.OpenStreamForReadAsync()))
            {
                while (!sRead.EndOfStream)
                {
                    line = await sRead.ReadLineAsync();

                    if (sentence.Contains(line))
                    {
                        break;
                    }
                    else
                    {
                        line = "";
                    }
                }

                sRead.Dispose();
            }

            return line;
        }

        private async Task<string> Feedback(string sentence)
        {
            string noun, verb, adjective, place, method, message;
            noun = verb = adjective = place = method = message = "";

            switch (sdr.particle)
            {
                case "を":

                    noun = await OpenFile("/-wo/nouns.txt", sentence);
                    verb = await OpenFile("/-wo/verbs.txt", sentence);

                    message = "'" + sdr.particle + "' is an object marker. In this case '" + noun + "' is the object and '" + verb + 
                        "' is the action that is acting on the object.";

                    break;

                case "に":

                    noun = await OpenFile("/-ni/nouns.txt", sentence);
                    verb = await OpenFile("/-ni/verbs.txt", sentence);

                    message = "'" + sdr.particle + "' indicates the target of a direction. In this case '" + noun + "' is the target and '" 
                        + verb + "' is the form of movement.";

                    break;

                case "が":

                    noun = await OpenFile("/-ga/nouns.txt", sentence);
                    verb = await OpenFile("/-ga/verbs.txt", sentence);
                    adjective = await OpenFile("/-ga/adjectives.txt", sentence);

                    message = "'" + sdr.particle + "' indicates the subject of a sentence and is often used with 'あります'"
                           + " and to describe nouns. ";

                    if (adjective != "")
                    {
                        message += "In this case '" + noun + "' is the subject and '"
                        + adjective + "' describes the subject.";
                    }
                    else
                    {
                        message += "In this case '" + noun + "' is the subject and '"
                        + verb + "' indicates possession of '" + noun + "'.";
                    }

                    break;

                case "で":

                    place = await OpenFile("/-de/places.txt", sentence);
                    method = await OpenFile("/-de/methods.txt", sentence);
                    verb = await OpenFile("/-de/verbs.txt", sentence);

                    message = "'" + sdr.particle + "' indicates a place of action or a method to do an action. ";

                    if (method != "")
                    {
                        message += "In this case you use a '" + method + "' as a means to '" + verb + "'.";
                    }
                    else
                    {
                        message += "In this case '" + place + "' is the location where '" + verb + "' is taking place.";
                    }

                    break;

                case "の":

                    break;

                case "は":

                    break;
            }

            if (stackPanel.Children.Count % 2 != 0 || stackPanel.Children.Count % 3 == 0)
            {
                message = message.Insert(0, "Looks like you had a little trouble with this one. Remember that ");
            }

            return message;
        }

        private async void T_Tapped(object sender, TappedRoutedEventArgs e)
        {
            fromSentence = sender as TextBox;
            string MasterText = await Feedback(fromSentence.Text);

            elder.Visibility = elderTextBox.Visibility = Visibility.Visible;
            FatherGrid.IsHitTestVisible = false;

            foreach (FrameworkElement f in stackPanel.Children)
            {
                f.Opacity = 0.25;
            }
            fromSentence.Opacity = 1.0;
            scrollViewer.IsHitTestVisible = false;

            FatherGrid.Opacity = 0.25;
            elder.Opacity = elderTextBox.Opacity = 1.0;

            if (elderTextBox.Text == "")
            {
                elderTextBox.Text = MasterText;
            }

            if (elderTextBox.Text.Length > MAX_CHAR_LENGTH)
            {
                for (int i = MAX_CHAR_LENGTH; i >= 0; i--)
                {
                    if (elderTextBox.Text[i].Equals('.') || elderTextBox.Text[i].Equals(' '))
                    {
                        cutPos = i;

                        for (int k = elderTextBox.Text.Length - 1; k >= cutPos; k--)
                        {
                            s.Push(elderTextBox.Text[k]);
                        }

                        elderTextBox.Text = elderTextBox.Text.Remove(i);
                        break;
                    }
                }
            }
        }

        private void backButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var rootFrame = Window.Current.Content as Frame;
            rootFrame.GoBack();
        }

        private void elderTextBox_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (s.Count() > MAX_CHAR_LENGTH)
            {
                for (int i = MAX_CHAR_LENGTH; i >= 0; i--)
                {
                    if (s.ElementAt(i) == '.' || s.ElementAt(i) == ' ')
                    {
                        cutPos = i;
                        string temp = new string(s.ToArray()).TrimStart().TrimEnd();
                        int size = 0;

                        elderTextBox.Text = temp.Remove(i);

                        size = temp.Length - i;
                        temp = temp.Remove(0, i);
                        s.Clear();
    
                        while (size - 1 >= 0)
                        {
                            s.Push(temp.ToCharArray()[size - 1]);
                            size--;
                        }

                        break;
                    }
                }
            }
            else if (s.Count() > 0 && s.Count() <= MAX_CHAR_LENGTH)
            {
                elderTextBox.Text = new string(s.ToArray()).TrimStart().TrimEnd();
                s.Clear();
            }
            else
            {
                elder.Visibility = elderTextBox.Visibility = Visibility.Collapsed;
                elderTextBox.Text = "";

                foreach (FrameworkElement f in stackPanel.Children)
                {
                    f.Opacity = 1.0;
                }
                scrollViewer.IsHitTestVisible = true;

                FatherGrid.Opacity = 1.0;
                FatherGrid.IsHitTestVisible = true;
            }
        }
    }
}
