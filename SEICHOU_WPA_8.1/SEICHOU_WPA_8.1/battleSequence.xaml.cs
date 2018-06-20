using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace SEICHOU_WPA_8._1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class battleSequence : Page
    {
        int attacknumber = 1;
        int cooldownnumber = 1;
        // Stores particles, sentences, and answers of the sentences in their own list. The sentences and answers are ordered in such a way
        // that each sentence correspond to an element in answers. For example, the answer to sentence[0] is stored in answer[0].
        List<String> particles, sentences, answers, saveparticles;
        StorageFolder local = ApplicationData.Current.LocalFolder;
        Random r;
        int index, NumOfCorrect, NumOfWrong;
        bool copy;
        double resetmonsterhealth;
        int numberofenemies;
        string prevSentence = "";
        // Controls the count down timer (t) for each turn on the top left corner of the screen.
        // t starts at 5 seconds and resets after each turn.
        private DispatcherTimer t;
        private int t_time = 0;
        int coinamount = 0;
        double playerfullhealth = 0;
        bool ischeckingparticle = false;

        //playerProfile usr = new playerProfile();
        sender sdr = new sender();
        levels lookUpData = new levels();
        IMobileServiceTable<levels> levelsTable = App.MobileService.GetTable<levels>();
        IMobileServiceTable<playerProfile> playerTable = App.MobileService.GetTable<playerProfile>();
        playerProfile usr;
        public battleSequence()
        {
            this.InitializeComponent();
            r = new Random(DateTime.Now.Millisecond);

            particles = new List<String>() { "を", "に", "が", "で", "の", "は" };
            saveparticles = new List<String>();
            sentences = new List<String>() { "シャワー _____ あびます.", "               いた    \r\nのど _____ 痛いです.",
             "かんこう\r\n観光ルートをガイドブック _____ \r\nしら\r\n調べます.", "かぜ\r\n風邪 _____ ひきました.", "アレルギー _____ あります.",
            "いしゃ     い\r\n医者 _____ 行きます."};
            answers = new List<String>() { "を", "が", "で", "を", "が", "に" };

            index = 0;

            this.NavigationCacheMode = NavigationCacheMode.Required;
            t = new DispatcherTimer();
            t.Interval = new TimeSpan(0, 0, 1);
            t.Tick += timer_Tick;
            t.Start();
        }

        void timer_Tick(object sender, object e)
        {

            if (t_time <= 0)
            {
                DecreaseParticlesTimer.Begin();
                particleButtonLEFT.IsEnabled = particleButtonMIDDLE.IsEnabled = particleButtonRIGHT.IsEnabled = particleButtonRIGHTIGHT.IsEnabled = true;
                t.Stop();
            }
            else
            {
                t_time -= 0;
                //    timer.Text = t_time.ToString();
            }
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            start.Text = "Ready";
            coverscreen.Visibility = start.Visibility = Visibility.Visible;
            if(sdr.selectedlevel != 12)
            {
                monsterHealth.Maximum = 2;
                monsterHealth.Value = 2;
            }
           else
            {
                monsterHealth.Maximum = 10;
                monsterHealth.Value = 1;
            }
            sdr = e.Parameter as sender;
            usr = await playerTable.LookupAsync(sdr.Id);
            coinamount = 0;
            Coincount.Text = "Coins: " + coinamount;
            playerfullhealth = playerHealth.Value;
            covertime.Visibility = Visibility.Collapsed;
            coverheal.Visibility = Visibility.Collapsed;
            coinamountdisplay.Visibility = Visibility.Collapsed;
            totalcoinafteramountdisplay.Visibility = Visibility.Collapsed;

            resetmonsterhealth = monsterHealth.Value;
            //monsterCountDown.Text = "CD: " + 2;
            //***************************************************************ADD SENTENCES *******************************************************
            int monsternumber = r.Next(1, 5);
            if (sdr.selectedlevel > 0)
            {
                if (usr.healpowerup > 0)
                {
                    heal.Visibility = Visibility.Visible;
                }
                else
                {
                    heal.Visibility = Visibility.Collapsed;
                }
                if (usr.timerpowerup > 0)
                {
                    stoptime.Visibility = Visibility.Visible;
                }
                else
                {
                    stoptime.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                heal.Visibility = stoptime.Visibility = Visibility.Collapsed;
            }
            //first if statment is for hard tab
            if (sdr.selectedlevel >= 9)
            {
                if (sdr.selectedlevel == 12)
                {
                    monster.Source = new BitmapImage(new Uri("ms-appx:/Assets/slimes/slimeboss.png"));

                    //monster.Visibility = Visibility.Collapsed;
                    //finalboss.Visibility = Visibility.Visible;
                    numberofenemies = 1;
                    image.Opacity = 0;
                    image2.Opacity = 100;
                    cooldownnumber = 5;
                    attacknumber = 12;
                }
                else
                {
                    numberofenemies = 3;
                    image.Opacity = 0;
                    image2.Opacity = 100;

                    if (monsternumber == 1)
                    {
                        monster.Source = new BitmapImage(new Uri("ms-appx:/Assets/Battle Sequence/blueslime.png"));
                        cooldownnumber = 2;
                        attacknumber = 4;
                    }
                    else if (monsternumber == 2)
                    {
                        monster.Source = new BitmapImage(new Uri("ms-appx:/Assets/Battle Sequence/greenslime.png"));
                        cooldownnumber = 3;
                        attacknumber = 4;
                    }
                    else if (monsternumber == 3)
                    {
                        monster.Source = new BitmapImage(new Uri("ms-appx:/Assets/Battle Sequence/yellowslime.png"));
                        cooldownnumber = 3;
                        attacknumber = 3;
                    }
                    else if (monsternumber == 4)
                    {
                        monster.Source = new BitmapImage(new Uri("ms-appx:/Assets/Battle Sequence/redslime.png"));
                        cooldownnumber = 4;
                        attacknumber = 6;
                    }
                    else
                    {
                        monster.Source = new BitmapImage(new Uri("ms-appx:/Assets/Battle Sequence/purpleslime.png"));
                        cooldownnumber = 1;
                        attacknumber = 3;
                    }
                }
            }
            else if (sdr.selectedlevel >= 5)
            {
                numberofenemies = 2;
                image.Opacity = 0;
                image2.Opacity = 100;
                //particles = new List<String>() { "を", "に", "が", "で", "の", "は" };
                //saveparticles = new List<String>();
                //sentences = new List<String>() { "シャワー _____ あびます.", "                  い    \r\nのど _____ 痛いです.",
                // "かんこう\r\n観光ルートをガイドブック _____ \r\nし\r\n調べます." };
                //answers = new List<String>() { "を", "が", "で" };
                if (monsternumber == 1)
                {
                    monster.Source = new BitmapImage(new Uri("ms-appx:/Assets/slimes/teenslimeblue.png"));
                    cooldownnumber = 2;
                    attacknumber = 3;
                }
                else if (monsternumber == 2)
                {
                    monster.Source = new BitmapImage(new Uri("ms-appx:/Assets/slimes/teenslimegreen.png"));
                    cooldownnumber = 3;
                    attacknumber = 3;
                }
                else if (monsternumber == 3)
                {
                    monster.Source = new BitmapImage(new Uri("ms-appx:/Assets/slimes/teenslimeyellow.png"));
                    cooldownnumber = 3;
                    attacknumber = 2;
                }
                else if (monsternumber == 4)
                {
                    monster.Source = new BitmapImage(new Uri("ms-appx:/Assets/slimes/teenslimered.png"));
                    cooldownnumber = 4;
                    attacknumber = 5;
                }
                else
                {
                    monster.Source = new BitmapImage(new Uri("ms-appx:/Assets/slimes/teenslimepurple.png"));
                    cooldownnumber = 1;
                    attacknumber = 2;
                }
            }
            else if (sdr.selectedlevel >= 1)
            {
                numberofenemies = 1;
                image.Opacity = 0;
                image2.Opacity = 100;
                //particles = new List<String>() { "を", "に", "が", "で", "の", "は" };
                //saveparticles = new List<String>();
                //sentences = new List<String>() { "シャワー _____ あびます.", "                  い    \r\nのど _____ 痛いです.",
                // "かんこう\r\n観光ルートをガイドブック _____ \r\nし\r\n調べます." };
                //answers = new List<String>() { "を", "が", "で" };
                if (monsternumber == 1)
                {
                    monster.Source = new BitmapImage(new Uri("ms-appx:/Assets/slimes/bebeslimeblue.png"));
                    cooldownnumber = 2;
                    attacknumber = 2;
                }
                else if (monsternumber == 2)
                {
                    monster.Source = new BitmapImage(new Uri("ms-appx:/Assets/slimes/bebeslimegreen.png"));
                    cooldownnumber = 3;
                    attacknumber = 2;             
                }
                else if (monsternumber == 3)
                {
                    monster.Source = new BitmapImage(new Uri("ms-appx:/Assets/slimes/bebeslimepurple.png"));
                    cooldownnumber = 1;
                    attacknumber = 1;
                }
                else if (monsternumber == 4)
                {
                    monster.Source = new BitmapImage(new Uri("ms-appx:/Assets/slimes/bebeslimered.png"));
                    cooldownnumber = 4;
                    attacknumber = 3;
                }
                else
                {
                    monster.Source = new BitmapImage(new Uri("ms-appx:/Assets/slimes/bebeslimeyellow.png"));
                    cooldownnumber = 3;
                    attacknumber = 1;
                }
            }
            else
            {
                numberofenemies = 1;
                image.Opacity = 100;
                image2.Opacity = 0;
                attacknumber = 1;
                cooldownnumber = 2;
                //particles = new List<String>() { "を", "に", "が", "で", "の", "は" };
                //saveparticles = new List<String>();
                //sentences = new List<String>() { "シャワー _____ あびます.", "                  い    \r\nのど _____ 痛いです.",
                // "かんこう\r\n観光ルートをガイドブック _____ \r\nし\r\n調べます." };
                //answers = new List<String>() { "を", "が", "で" };
                //Adds monster image to screen
                //var image = new BitmapImage(new Uri("ms-appx:///Assets/Battle Sequence/yellowslime.png"));
                //animatedImage.Source = image;
            }
            monsterCountDown.Text = "CD: " + cooldownnumber;
            if (usr.gender == 'M')
            {
                avatar.Source = new BitmapImage(new Uri("ms-appx:/Assets/Battle Sequence/PlayerMaleTN.jpg"));
                resultAvatar.Source = new BitmapImage(new Uri("ms-appx:/Assets/PlayerMale.png"));
            }
            else
            {
                avatar.Source = new BitmapImage(new Uri("ms-appx:/Assets/Battle Sequence/PlayerFemaleTN.jpg"));
                resultAvatar.Source = new BitmapImage(new Uri("ms-appx:/Assets/PlayerFemale.png"));
            }

            //if (avatar.Source == null && resultAvatar.Source == null)
            //{
            //    string line = "";
            //    var savesFolder = await local.CreateFolderAsync("Saves", CreationCollisionOption.OpenIfExists);

            //    Stream s = await savesFolder.OpenStreamForReadAsync(usr.fileName);
            //    using (StreamReader r = new StreamReader(s))
            //    {
            //        while (line != null)
            //        {
            //            line = r.ReadLine();

            //            if (line.Contains("Male"))
            //            {
            //                avatar.Source = new BitmapImage(new Uri("ms-appx:/Assets/Battle Sequence/PlayerMaleTN.jpg"));
            //                resultAvatar.Source = new BitmapImage(new Uri("ms-appx:/Assets/PlayerMale.png"));

            //                break;
            //            }
            //            else if (line.Contains("Female"))
            //            {
            //                avatar.Source = new BitmapImage(new Uri("ms-appx:/Assets/Battle Sequence/PlayerFemaleTN.jpg"));
            //                resultAvatar.Source = new BitmapImage(new Uri("ms-appx:/Assets/PlayerFemale.png"));

            //                break;
            //            }
            //        }

            //        r.Dispose();
            //    }
            //}

            /**This resets the whole game. This makes if statement does:
            -collapses resultscreen
            -makes the reset true which makes the timers back to the start and makes sentences and particles.
            -makes the monsters cd back to 2
            */
            

            //Disables the particle buttons
            particleButtonLEFT.IsEnabled = particleButtonMIDDLE.IsEnabled = particleButtonRIGHT.IsEnabled = particleButtonRIGHTIGHT.IsEnabled = false;
            resultScreen.Visibility = resultsButton.Visibility = resultAvatar.Visibility = tryagainMessageTextBox.Visibility = tryagainButton.Visibility = Visibility.Collapsed;
            Coincount.Visibility = 0;
            // Sets the sentence above the monster to a random sentence in the sentences list. Index is set to the answer of the sentence.
            sentencesTextBox.Text = sentences[r.Next(0, sentences.Count)];
            index = sentences.IndexOf(sentencesTextBox.Text);

            sdr.sentencesRESULTS.Add(sentencesTextBox.Text);
            sdr.sentencesRESULTS.Add("\n");
            copy = true;

            int temp = r.Next(1, 12);

            if (temp <= 3)
            {
                particleButtonLEFT.Content = answers[index];
                particles.Remove(particleButtonLEFT.Content.ToString());

                particleButtonMIDDLE.Content = particles[r.Next(0, particles.Count)];
                particles.Remove(particleButtonMIDDLE.Content.ToString());

                particleButtonRIGHT.Content = particles[r.Next(0, particles.Count)];
                particles.Remove(particleButtonRIGHT.Content.ToString());

                particleButtonRIGHTIGHT.Content = particles[r.Next(0, particles.Count)];
                particles.Remove(particleButtonRIGHTIGHT.Content.ToString());

            }
            else if (temp >= 4 && temp <= 6)
            {
                particleButtonMIDDLE.Content = answers[index];
                particles.Remove(particleButtonMIDDLE.Content.ToString());

                particleButtonLEFT.Content = particles[r.Next(0, particles.Count)];
                particles.Remove(particleButtonLEFT.Content.ToString());

                particleButtonRIGHT.Content = particles[r.Next(0, particles.Count)];
                particles.Remove(particleButtonRIGHT.Content.ToString());

                particleButtonRIGHTIGHT.Content = particles[r.Next(0, particles.Count)];
                particles.Remove(particleButtonRIGHTIGHT.Content.ToString());
            }
            else if (temp >= 7 && temp <= 9)
            {
                particleButtonRIGHT.Content = answers[index];
                particles.Remove(particleButtonRIGHT.Content.ToString());

                particleButtonRIGHTIGHT.Content = particles[r.Next(0, particles.Count)];
                particles.Remove(particleButtonRIGHTIGHT.Content.ToString());

                particleButtonLEFT.Content = particles[r.Next(0, particles.Count)];
                particles.Remove(particleButtonLEFT.Content.ToString());

                particleButtonMIDDLE.Content = particles[r.Next(0, particles.Count)];
                particles.Remove(particleButtonMIDDLE.Content.ToString());
            }
            else
            {
                particleButtonRIGHTIGHT.Content = answers[index];
                particles.Remove(particleButtonRIGHTIGHT.Content.ToString());

                particleButtonLEFT.Content = particles[r.Next(0, particles.Count)];
                particles.Remove(particleButtonLEFT.Content.ToString());

                particleButtonMIDDLE.Content = particles[r.Next(0, particles.Count)];
                particles.Remove(particleButtonMIDDLE.Content.ToString());

                particleButtonRIGHT.Content = particles[r.Next(0, particles.Count)];
                particles.Remove(particleButtonRIGHT.Content.ToString());
            }
            // The particle list is cleared and all the particles are added to the same list
            particles.Clear();

            particles.Add("を");
            particles.Add("に");
            particles.Add("が");
            particles.Add("で");
            particles.Add("の");
            particles.Add("は");

            await System.Threading.Tasks.Task.Delay(1200);
            start.Opacity = 100;
            start.Text = "GO!";
            if (sdr.resetgame == 1)
            {
                //Disables the particle buttons
                resultScreen.Visibility = winMessageTextBox.Visibility = resultsButton.Visibility = resultAvatar.Visibility = starLEFT.Visibility
                    = starMID.Visibility = starRIGHT.Visibility = Visibility.Collapsed;
                reset(true);

                //DecreasePlayerHealth.Begin();
                //monsterCountDown.Text = "CD: " + 2;
                monsterCountDown.Text = "CD: " + cooldownnumber;


                monsterHealth.Value = resetmonsterhealth;
            }
            await System.Threading.Tasks.Task.Delay(800);

            coverscreen.Visibility = start.Visibility = Visibility.Collapsed;


        }
        //private void animatedImage_ImageOpened(object sender, RoutedEventArgs e)
        //{
        //    Image img = sender as Image;

        //    Storyboard s = new Storyboard();

        //    DoubleAnimation doubleAni = new DoubleAnimation();
        //    //animation stuff
        //    doubleAni.To = 1;
        //    doubleAni.Duration = new Duration(TimeSpan.FromMilliseconds(2000));

        //    Storyboard.SetTarget(doubleAni, img);
        //    Storyboard.SetTargetProperty(doubleAni, "Opacity");

        //    s.Children.Add(doubleAni);

        //    s.Begin();
        //}
        private void resultsButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            coinamountdisplay.Visibility = Visibility.Collapsed;
            totalcoinafteramountdisplay.Visibility = Visibility.Collapsed;

            DecreaseMonsterHealth.Stop();
            var rootFrame = Window.Current.Content as Frame;

            rootFrame.Navigate(typeof(resultsScreen), sdr);
            Window.Current.Content = rootFrame;
            Window.Current.Activate();
        }
        private async void DecraseParticlesTimer_Completed(object sender, object e)
        {
            int alternate = 0;
            particleButtonLEFT.IsEnabled = particleButtonMIDDLE.IsEnabled = particleButtonRIGHT.IsEnabled = particleButtonRIGHTIGHT.IsEnabled = false;

            for (int i = 1; i < 9; i++)
            {
                if (alternate == 0)
                {
                    sentencesTextBox.Margin = new Thickness(38, 58, 0, 0);
                    await System.Threading.Tasks.Task.Delay(100);
                    alternate++;
                }
                else
                {
                    sentencesTextBox.Margin = new Thickness(48, 58, 0, 0);
                    await System.Threading.Tasks.Task.Delay(100);
                    alternate = 0;
                }
            }
            await System.Threading.Tasks.Task.Delay(600);
            NumOfWrong++;

            int temp2 = (int)monsterCountDown.Text.ElementAt(4) - 48;
            monsterCountDownDecrementer();

            if (attacknumber == 1)
            {
                await System.Threading.Tasks.Task.Delay(800);

            }
            else if (attacknumber == 2)
            {
                await System.Threading.Tasks.Task.Delay(1200);

            }
            else if (attacknumber == 3)
            {

                await System.Threading.Tasks.Task.Delay(2000);

            }
            else if (attacknumber == 4)
            {
                await System.Threading.Tasks.Task.Delay(2400);

            }
            else if (attacknumber == 5)
            {
                await System.Threading.Tasks.Task.Delay(2600);

            }
            else
            {
                await System.Threading.Tasks.Task.Delay(4200);
            }


            reset(true);
        }
       // private async Task CreateSaveStar(int level, int starcounter)
        //{
            //string line = "";
            //int c = 0;

            //var savesFolder = await local.CreateFolderAsync("Saves", CreationCollisionOption.OpenIfExists);

            //Stream s = await savesFolder.OpenStreamForReadAsync(usr.fileName);
            //using (StreamReader r = new StreamReader(s))
            //{
            //    while (line != null)
            //    {
            //        line = r.ReadLine();

            //        if (!line.Contains("LEVEL " + level + ": "))
            //        {
            //            c += line.Count();
            //        }
            //        else
            //        {
            //            c += (level + 2);
            //            break;
            //        }
            //    }

            //    r.Dispose();
            //    s.Dispose();
            //}

            //var file = await savesFolder.CreateFileAsync(usr.fileName, CreationCollisionOption.OpenIfExists);
            //var stream = await file.OpenAsync(FileAccessMode.ReadWrite);

            //using (var outputStream = stream.GetOutputStreamAt((ulong)c))
            //{
            //    DataWriter dataWriter = new DataWriter(outputStream);
            //    dataWriter.WriteString("LEVEL " + level + ": " + starcounter);

            //    await dataWriter.StoreAsync();
            //    await outputStream.FlushAsync();
            //    stream.Dispose();
            //}
        //}
        //the star system is used to set the star amount to the level        
        async Task starsystem(int level, int staramount)
        {
            lookUpData = await levelsTable.LookupAsync(sdr.Id);
            JObject jo = new JObject();
            jo.Add("id", sdr.Id);

            if (level == 1)
            {
                if (lookUpData.L1 < staramount) { jo.Add("L" + level, staramount); }
                else { return; }
            }
            else if (level == 2)
            {
                if (lookUpData.L2 < staramount) { jo.Add("L" + level, staramount); }
                else { return; }
            }
            else if (level == 3)
            {
                if (lookUpData.L3 < staramount) { jo.Add("L" + level, staramount); }
                else { return; }
            }
            else if (level == 4)
            {
                if (lookUpData.L4 < staramount) { jo.Add("L" + level, staramount); }
                else { return; }
            }
            else if (level == 5)
            {
                if (lookUpData.L5 < staramount) { jo.Add("L" + level, staramount); }
                else { return; }
            }
            else if (level == 6)
            {
                if (lookUpData.L6 < staramount) { jo.Add("L" + level, staramount); }
                else { return; }
            }
            else if (level == 7)
            {
                if (lookUpData.L7 < staramount) { jo.Add("L" + level, staramount); }
                else { return; }
            }
            else if (level == 8)
            {
                if (lookUpData.L8 < staramount) { jo.Add("L" + level, staramount); }
                else { return; }
            }
            else if (level == 9)
            {
                if (lookUpData.L9 < staramount) { jo.Add("L" + level, staramount); }
                else { return; }
            }
            else if (level == 10)
            {
                if (lookUpData.L10 < staramount) { jo.Add("L" + level, staramount); }
                else { return; }
            }
            else if (level == 11)
            {
                if (lookUpData.L11 < staramount) { jo.Add("L" + level, staramount); }
                else { return; }
            }
            else if (level == 12)
            {
                if (lookUpData.L12 < staramount) { jo.Add("L" + level, staramount); }
                else { return; }
            }

            await levelsTable.UpdateAsync(jo);
            

            //if (level == 1)
            //{
            //    if (usr.starcountlevel1 == 3)
            //    {
            //        usr.starcountlevel1 = 3;
            //    }
            //    else if (usr.starcountlevel1 == 2)
            //    {
            //        if (staramount == 3)
            //        {
            //            usr.starcountlevel1 = 3;
            //        }
            //        else
            //        {
            //            usr.starcountlevel1 = 2;
            //        }
            //    }
            //    else
            //    {
            //        if (staramount == 3)
            //        {
            //            usr.starcountlevel1 = 3;
            //            await CreateSaveStar(level, staramount);
            //        }
            //        else if (staramount == 2)
            //        {
            //            usr.starcountlevel1 = 2;
            //            await CreateSaveStar(level, staramount);
            //        }
            //        else
            //        {
            //            usr.starcountlevel1 = 1;
            //            await CreateSaveStar(level, staramount);
            //        }
            //    }
            //}
            //else if (level == 2)
            //{
            //    if (usr.starcountlevel2 == 3)
            //    {
            //        usr.starcountlevel2 = 3;
            //    }
            //    else if (usr.starcountlevel2 == 2)
            //    {
            //        if (staramount == 3)
            //        {
            //            usr.starcountlevel2 = 3;
            //        }
            //        else
            //        {
            //            usr.starcountlevel2 = 2;
            //        }
            //    }
            //    else
            //    {
            //        if (staramount == 3)
            //        {
            //            usr.starcountlevel2 = 3;
            //            await CreateSaveStar(level, staramount);
            //        }
            //        else if (staramount == 2)
            //        {
            //            usr.starcountlevel2 = 2;
            //            await CreateSaveStar(level, staramount);
            //        }
            //        else
            //        {
            //            usr.starcountlevel2 = 1;
            //            await CreateSaveStar(level, staramount);
            //        }
            //    }
            //}
            //else if (level == 3)
            //{
            //    if (usr.starcountlevel3 == 3)
            //    {
            //        usr.starcountlevel3 = 3;
            //    }
            //    else if (usr.starcountlevel3 == 2)
            //    {
            //        if (staramount == 3)
            //        {
            //            usr.starcountlevel3 = 3;
            //        }
            //        else
            //        {
            //            usr.starcountlevel3 = 2;
            //        }
            //    }
            //    else
            //    {
            //        if (staramount == 3)
            //        {
            //            usr.starcountlevel3 = 3;
            //            await CreateSaveStar(level, staramount);
            //        }
            //        else if (staramount == 2)
            //        {
            //            usr.starcountlevel3 = 2;
            //            await CreateSaveStar(level, staramount);
            //        }
            //        else
            //        {
            //            usr.starcountlevel3 = 1;
            //            await CreateSaveStar(level, staramount);
            //        }
            //    }
            //}
            //else if (level == 4)
            //{
            //    if (usr.starcountlevel4 == 3)
            //    {
            //        usr.starcountlevel4 = 3;
            //    }
            //    else if (usr.starcountlevel4 == 2)
            //    {
            //        if (staramount == 3)
            //        {
            //            usr.starcountlevel4 = 3;
            //        }
            //        else
            //        {
            //            usr.starcountlevel4 = 2;
            //        }
            //    }
            //    else
            //    {
            //        if (staramount == 3)
            //        {
            //            usr.starcountlevel4 = 3;
            //            await CreateSaveStar(level, staramount);
            //        }
            //        else if (staramount == 2)
            //        {
            //            usr.starcountlevel4 = 2;
            //            await CreateSaveStar(level, staramount);
            //        }
            //        else
            //        {
            //            usr.starcountlevel4 = 1;
            //            await CreateSaveStar(level, staramount);
            //        }
            //    }
            //}
            //else if (level == 5)
            //{
            //    if (usr.starcountlevel5 == 3)
            //    {
            //        usr.starcountlevel5 = 3;
            //    }
            //    else if (usr.starcountlevel5 == 2)
            //    {
            //        if (staramount == 3)
            //        {
            //            usr.starcountlevel5 = 3;
            //        }
            //        else
            //        {
            //            usr.starcountlevel5 = 2;
            //        }
            //    }
            //    else
            //    {
            //        if (staramount == 3)
            //        {
            //            usr.starcountlevel5 = 3;
            //            await CreateSaveStar(level, staramount);
            //        }
            //        else if (staramount == 2)
            //        {
            //            usr.starcountlevel5 = 2;
            //            await CreateSaveStar(level, staramount);
            //        }
            //        else
            //        {
            //            usr.starcountlevel5 = 1;
            //            await CreateSaveStar(level, staramount);
            //        }
            //    }
            //}
            //else if (level == 6)
            //{
            //    if (usr.starcountlevel6 == 3)
            //    {
            //        usr.starcountlevel6 = 3;
            //    }
            //    else if (usr.starcountlevel6 == 2)
            //    {
            //        if (staramount == 3)
            //        {
            //            usr.starcountlevel6 = 3;
            //        }
            //        else
            //        {
            //            usr.starcountlevel6 = 2;
            //        }
            //    }
            //    else
            //    {
            //        if (staramount == 3)
            //        {
            //            usr.starcountlevel6 = 3;
            //            await CreateSaveStar(level, staramount);
            //        }
            //        else if (staramount == 2)
            //        {
            //            usr.starcountlevel6 = 2;
            //            await CreateSaveStar(level, staramount);
            //        }
            //        else
            //        {
            //            usr.starcountlevel6 = 1;
            //            await CreateSaveStar(level, staramount);
            //        }
            //    }
            //}
            //else if (level == 7)
            //{
            //    if (usr.starcountlevel7 == 3)
            //    {
            //        usr.starcountlevel7 = 3;
            //    }
            //    else if (usr.starcountlevel7 == 2)
            //    {
            //        if (staramount == 3)
            //        {
            //            usr.starcountlevel7 = 3;
            //        }
            //        else
            //        {
            //            usr.starcountlevel7 = 2;
            //        }
            //    }
            //    else
            //    {
            //        if (staramount == 3)
            //        {
            //            usr.starcountlevel7 = 3;
            //            await CreateSaveStar(level, staramount);
            //        }
            //        else if (staramount == 2)
            //        {
            //            usr.starcountlevel7 = 2;
            //            await CreateSaveStar(level, staramount);
            //        }
            //        else
            //        {
            //            usr.starcountlevel7 = 1;
            //            await CreateSaveStar(level, staramount);
            //        }
            //    }
            //}
            //else if (level == 8)
            //{
            //    if (usr.starcountlevel8 == 3)
            //    {
            //        usr.starcountlevel8 = 3;
            //    }
            //    else if (usr.starcountlevel8 == 2)
            //    {
            //        if (staramount == 3)
            //        {
            //            usr.starcountlevel8 = 3;
            //        }
            //        else
            //        {
            //            usr.starcountlevel8 = 2;
            //        }
            //    }
            //    else
            //    {
            //        if (staramount == 3)
            //        {
            //            usr.starcountlevel8 = 3;
            //            await CreateSaveStar(level, staramount);
            //        }
            //        else if (staramount == 2)
            //        {
            //            usr.starcountlevel8 = 2;
            //            await CreateSaveStar(level, staramount);
            //        }
            //        else
            //        {
            //            usr.starcountlevel8 = 1;
            //            await CreateSaveStar(level, staramount);
            //        }
            //    }
            //}
            //else if (level == 9)
            //{
            //    if (usr.starcountlevel9 == 3)
            //    {
            //        usr.starcountlevel9 = 3;
            //    }
            //    else if (usr.starcountlevel9 == 2)
            //    {
            //        if (staramount == 3)
            //        {
            //            usr.starcountlevel9 = 3;
            //        }
            //        else
            //        {
            //            usr.starcountlevel9 = 2;
            //        }
            //    }
            //    else
            //    {
            //        if (staramount == 3)
            //        {
            //            usr.starcountlevel9 = 3;
            //            await CreateSaveStar(level, staramount);
            //        }
            //        else if (staramount == 2)
            //        {
            //            usr.starcountlevel9 = 2;
            //            await CreateSaveStar(level, staramount);
            //        }
            //        else
            //        {
            //            usr.starcountlevel9 = 1;
            //            await CreateSaveStar(level, staramount);
            //        }
            //    }
            //}
            //else if (level == 10)
            //{
            //    if (usr.starcountlevel10 == 3)
            //    {
            //        usr.starcountlevel10 = 3;
            //    }
            //    else if (usr.starcountlevel10 == 2)
            //    {
            //        if (staramount == 3)
            //        {
            //            usr.starcountlevel10 = 3;
            //        }
            //        else
            //        {
            //            usr.starcountlevel10 = 2;
            //        }
            //    }
            //    else
            //    {
            //        if (staramount == 3)
            //        {
            //            usr.starcountlevel10 = 3;
            //            await CreateSaveStar(level, staramount);
            //        }
            //        else if (staramount == 2)
            //        {
            //            usr.starcountlevel10 = 2;
            //            await CreateSaveStar(level, staramount);
            //        }
            //        else
            //        {
            //            usr.starcountlevel10 = 1;
            //            await CreateSaveStar(level, staramount);
            //        }
            //    }
            //}
            //else if (level == 11)
            //{
            //    if (usr.starcountlevel11 == 3)
            //    {
            //        usr.starcountlevel11 = 3;
            //    }
            //    else if (usr.starcountlevel11 == 2)
            //    {
            //        if (staramount == 3)
            //        {
            //            usr.starcountlevel11 = 3;
            //        }
            //        else
            //        {
            //            usr.starcountlevel11 = 2;
            //        }
            //    }
            //    else
            //    {
            //        if (staramount == 3)
            //        {
            //            usr.starcountlevel11 = 3;
            //            await CreateSaveStar(level, staramount);
            //        }
            //        else if (staramount == 2)
            //        {
            //            usr.starcountlevel11 = 2;
            //            await CreateSaveStar(level, staramount);
            //        }
            //        else
            //        {
            //            usr.starcountlevel11 = 1;
            //            await CreateSaveStar(level, staramount);
            //        }
            //    }
            //}
            //else if (level == 12)
            //{
            //    if (usr.starcountlevel12 == 3)
            //    {
            //        usr.starcountlevel3 = 3;
            //    }
            //    else if (usr.starcountlevel12 == 2)
            //    {
            //        if (staramount == 3)
            //        {
            //            usr.starcountlevel12 = 3;
            //        }
            //        else
            //        {
            //            usr.starcountlevel12 = 2;
            //        }
            //    }
            //    else
            //    {
            //        if (staramount == 3)
            //        {
            //            usr.starcountlevel12 = 3;
            //            await CreateSaveStar(level, staramount);
            //        }
            //        else if (staramount == 2)
            //        {
            //            usr.starcountlevel12 = 2;
            //            await CreateSaveStar(level, staramount);
            //        }
            //        else
            //        {
            //            usr.starcountlevel12 = 1;
            //            await CreateSaveStar(level, staramount);
            //        }
            //    }
            //}

        }
        private async void DecreaseMonsterHealth_Completed(object sender, object e)
        {
            if (monsterHealth.Value == 0)
            {
                wronganswersignleft.Visibility = wronganswersignmiddle.Visibility = wronganswersignright.Visibility = wronganswersignrightright.Visibility = correctanswersignleft.Visibility = correctanswersignmiddle.Visibility = correctanswersignright.Visibility = correctanswersignrightright.Visibility = Visibility.Collapsed;
                numberofenemies = numberofenemies - 1;
                t.Stop();
                if (sdr.selectedlevel >= 9)
                {
                    // coin amount for 3 monsters killed
                    coinamount += r.Next(75, 100);

                }
                else if (sdr.selectedlevel >= 5)
                {
                    //coin amount for 2 monsters killed
                    coinamount += r.Next(50, 75);

                }
                else
                {
                    coinamount += r.Next(25, 50);
                }


                //CHANGE THIS ONCE TEAM FIGURES OUT STAR SYSTEM***********************************************************************************************************
                if (numberofenemies == 0)
                {
                    heal.Visibility = stoptime.Visibility = Visibility.Collapsed;
                    DecreaseParticlesTimer.Stop();
                    DecreaseMonsterHealth.Stop();
                    DecreasePlayerHealth.Stop();
                    Coincount.Visibility = Visibility.Collapsed;

                    if (sdr.selectedlevel >= 1)
                    {
                        lookUpData = await levelsTable.LookupAsync(sdr.Id);
                        if (sdr.selectedlevel == 12 && lookUpData.L12 == 0)
                        {
                            coverscreen.Visibility = finalmessage.Visibility = closefinalmessage.Visibility = Visibility.Visible;

                        }
                        if (NumOfWrong == 0)
                        {
                            resultScreen.Visibility = winMessageTextBox.Visibility = resultsButton.Visibility = resultAvatar.Visibility = starLEFT.Visibility
                            = starMID.Visibility = starRIGHT.Visibility = Visibility.Visible;

                            if (sdr.selectedlevel != 0)
                            {
                                await starsystem(sdr.selectedlevel, 3);
                            }
                        }
                        else if (NumOfWrong <= 2)
                        {
                            resultScreen.Visibility = winMessageTextBox.Visibility = resultsButton.Visibility = resultAvatar.Visibility = starLEFT.Visibility
                            = starMID.Visibility = Visibility.Visible;

                            if (sdr.selectedlevel != 0)
                            {
                                await starsystem(sdr.selectedlevel, 2);
                            }

                        }
                        else
                        {
                            resultScreen.Visibility = winMessageTextBox.Visibility = resultsButton.Visibility = resultAvatar.Visibility = starLEFT.Visibility
                            = Visibility.Visible;

                            if (sdr.selectedlevel != 0)
                            {
                                await starsystem(sdr.selectedlevel, 1);
                            }
                        }
                        coinamountdisplay.Visibility = Visibility.Visible;
                        coinamountdisplay.Text = "Coins Earned: " + coinamount;
                        usr.coins += coinamount;
                        await playerTable.UpdateAsync(usr);

                        totalcoinafteramountdisplay.Visibility = Visibility.Visible;
                        totalcoinafteramountdisplay.Text = "Total Coins: " + usr.coins;


                    }
                    else
                    {
                        resultScreen.Visibility = winMessageTextBox.Visibility = resultsButton.Visibility = resultAvatar.Visibility = Visibility.Visible;

                    }
                    NumOfWrong = 0;
                }
                //This is if there are still enemies to defeat.
                else
                {
                    Coincount.Text = "Coins: " + coinamount;
                    monster.Opacity = 0;

                    int monsternumber = r.Next(1, 5);
                    if (sdr.selectedlevel >= 9)
                    {
                        image.Opacity = 0;
                        image2.Opacity = 100;

                        if (monsternumber == 1)
                        {
                            monster.Source = new BitmapImage(new Uri("ms-appx:/Assets/Battle Sequence/blueslime.png"));
                            cooldownnumber = 2;
                            attacknumber = 4;
                        }
                        else if (monsternumber == 2)
                        {
                            monster.Source = new BitmapImage(new Uri("ms-appx:/Assets/Battle Sequence/greenslime.png"));
                            cooldownnumber = 3;
                            attacknumber = 4;
                        }
                        else if (monsternumber == 3)
                        {
                            monster.Source = new BitmapImage(new Uri("ms-appx:/Assets/Battle Sequence/yellowslime.png"));
                            cooldownnumber = 3;
                            attacknumber = 3;
                        }
                        else if (monsternumber == 4)
                        {
                            monster.Source = new BitmapImage(new Uri("ms-appx:/Assets/Battle Sequence/redslime.png"));
                            cooldownnumber = 4;
                            attacknumber = 6;
                        }
                        else
                        {
                            monster.Source = new BitmapImage(new Uri("ms-appx:/Assets/Battle Sequence/purpleslime.png"));
                            cooldownnumber = 1;
                            attacknumber = 3;
                        }
                    }
                    else if (sdr.selectedlevel >= 5)
                    {
                        image.Opacity = 0;
                        image2.Opacity = 100;
                        //particles = new List<String>() { "を", "に", "が", "で", "の", "は" };
                        //saveparticles = new List<String>();
                        //sentences = new List<String>() { "シャワー _____ あびます.", "                  い    \r\nのど _____ 痛いです.",
                        // "かんこう\r\n観光ルートをガイドブック _____ \r\nし\r\n調べます." };
                        //answers = new List<String>() { "を", "が", "で" };
                        if (monsternumber == 1)
                        {
                            monster.Source = new BitmapImage(new Uri("ms-appx:/Assets/slimes/teenslimeblue.png"));
                            cooldownnumber = 2;
                            attacknumber = 3;
                        }
                        else if (monsternumber == 2)
                        {
                            monster.Source = new BitmapImage(new Uri("ms-appx:/Assets/slimes/teenslimegreen.png"));
                            cooldownnumber = 3;
                            attacknumber = 3;
                        }
                        else if (monsternumber == 3)
                        {
                            monster.Source = new BitmapImage(new Uri("ms-appx:/Assets/slimes/teenslimeyellow.png"));
                            cooldownnumber = 3;
                            attacknumber = 2;
                        }
                        else if (monsternumber == 4)
                        {
                            monster.Source = new BitmapImage(new Uri("ms-appx:/Assets/slimes/teenslimered.png"));
                            cooldownnumber = 4;
                            attacknumber = 5;
                        }
                        else
                        {
                            monster.Source = new BitmapImage(new Uri("ms-appx:/Assets/slimes/teenslimepurple.png"));
                            cooldownnumber = 1;
                            attacknumber = 2;
                        }
                    }
                    else if (sdr.selectedlevel >= 1)
                    {
                        image.Opacity = 0;
                        image2.Opacity = 100;
                        //particles = new List<String>() { "を", "に", "が", "で", "の", "は" };
                        //saveparticles = new List<String>();
                        //sentences = new List<String>() { "シャワー _____ あびます.", "                  い    \r\nのど _____ 痛いです.",
                        // "かんこう\r\n観光ルートをガイドブック _____ \r\nし\r\n調べます." };
                        //answers = new List<String>() { "を", "が", "で" };
                        if (monsternumber == 1)
                        {
                            monster.Source = new BitmapImage(new Uri("ms-appx:/Assets/slimes/bebeslimeblue.png"));
                            cooldownnumber = 2;
                            attacknumber = 2;
                        }
                        else if (monsternumber == 2)
                        {
                            monster.Source = new BitmapImage(new Uri("ms-appx:/Assets/slimes/bebeslimegreen.png"));
                            cooldownnumber = 3;
                            attacknumber = 2;
                        }
                        else if (monsternumber == 3)
                        {
                            monster.Source = new BitmapImage(new Uri("ms-appx:/Assets/slimes/bebeslimepurple.png"));
                            cooldownnumber = 1;
                            attacknumber = 1;
                        }
                        else if (monsternumber == 4)
                        {
                            monster.Source = new BitmapImage(new Uri("ms-appx:/Assets/slimes/bebeslimered.png"));
                            cooldownnumber = 4;
                            attacknumber = 3;
                        }
                        else
                        {
                            monster.Source = new BitmapImage(new Uri("ms-appx:/Assets/slimes/bebeslimeyellow.png"));
                            cooldownnumber = 3;
                            attacknumber = 1;
                        }
                    }
                    monsterCountDown.Text = "CD: " + cooldownnumber;

                    monster.Opacity = 100;
                    DecreaseMonsterHealth.Stop();

                    reset();
                }
            }

        }
        private async void particleButtonLEFT_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await checkParticleButton(particleButtonLEFT);
        }
        private async void particleButtonMIDDLE_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await checkParticleButton(particleButtonMIDDLE);
        }
        private async void particleButtonRIGHT_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await checkParticleButton(particleButtonRIGHT);
        }
        private async void particleButtonRIGHTRIGHT_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await checkParticleButton(particleButtonRIGHTIGHT);
        }

        private void DecreasePlayerHealth_Completed(object sender, object e)
        {
            if (playerHealth.Value == 0)
            {
                DecreaseMonsterHealth.Stop();
                int temp = (int)monsterCountDown.Text.ElementAt(4) - 48;
                monsterCountDown.Text = "CD: " + (temp - 1);
                if (temp - 1 == 0)
                {
                    //DecreasePlayerHealth.Begin();
                    monsterCountDown.Text = "CD: " + cooldownnumber;

                }
                t.Stop();
                //DecreaseMonsterHealth.Stop();
                
                DecreasePlayerHealth.Stop();
                DecreaseParticlesTimer.Stop();
                wronganswersignleft.Visibility = wronganswersignmiddle.Visibility = wronganswersignright.Visibility = correctanswersignleft.Visibility = correctanswersignmiddle.Visibility = correctanswersignright.Visibility = Visibility.Collapsed;

                if (sdr.selectedlevel >= 1)
                {
                    tryagainMessageTextBox.Text = "Game Over!";
                    tryagainButton.Content = "Back to Level Select";
                    NumOfWrong = 0;
                }
                else
                {
                    tryagainMessageTextBox.Text = "Try Again!";
                    tryagainButton.Content = "Retry";
                    NumOfWrong = 0;

                }

                resultScreen.Visibility = tryagainButton.Visibility = resultAvatar.Visibility = tryagainMessageTextBox.Visibility = Visibility.Visible;

            }
            DecreaseParticlesTimer.Stop();

        }

        private async void heal_Tapped(object sender, TappedRoutedEventArgs e)
        {
            playerHealth.Value = playerfullhealth;
            particleButtonLEFT.Visibility = particleButtonMIDDLE.Visibility = particleButtonRIGHT.Visibility = particleButtonRIGHTIGHT.Visibility = sentencesTextBox.Visibility = Visibility.Visible;
            usr.healpowerup -= 1;
            if (usr.healpowerup == 0)
            {
                heal.Visibility = Visibility.Collapsed;
            }
            await playerTable.UpdateAsync(usr);

            //usr.coins = usr.coins - 1;
            //add a decrease powerup count here
        }

        private async void stoptime_Tapped(object sender, TappedRoutedEventArgs e)
        {
            particleButtonLEFT.Visibility = particleButtonMIDDLE.Visibility = particleButtonRIGHT.Visibility = particleButtonRIGHTIGHT.Visibility = sentencesTextBox.Visibility = Visibility.Visible;
            covertime.Visibility = Visibility.Visible;
            usr.timerpowerup -= 1;
            if (usr.timerpowerup == 0)
            {
                stoptime.Visibility = Visibility.Collapsed;
            }

            //usr.coins = usr.coins - 1;
            //add a decrease poweup count here
            int timedelay = 30;
            DecreaseParticlesTimer.Pause();
            while (timedelay != 0)
            {
                DecreaseParticlesTimer.Pause();
                await System.Threading.Tasks.Task.Delay(100);
                timedelay--;
            }
            await playerTable.UpdateAsync(usr);
            if(ischeckingparticle == false)
            {
                DecreaseParticlesTimer.Resume();
            }
            covertime.Visibility = Visibility.Collapsed;
        }

        private void closefinalmessage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            coverscreen.Visibility = finalmessage.Visibility = closefinalmessage.Visibility = Visibility.Collapsed;

        }

        private void tryagainButton_Click(object sender, RoutedEventArgs e)
        {
            sdr.answersRESULTS.Clear();
            sdr.sentencesRESULTS.Clear();

            wronganswersignrightright.Visibility = correctanswersignrightright.Visibility = wronganswersignleft.Visibility = wronganswersignmiddle.Visibility = wronganswersignright.Visibility = correctanswersignleft.Visibility = correctanswersignmiddle.Visibility = correctanswersignright.Visibility = Visibility.Collapsed;
            if (sdr.selectedlevel >= 1)
            {

                var rootFrame = Window.Current.Content as Frame;

                //rootFrame.Navigate(typeof(resultsScreen), usr);
                rootFrame.Navigate(typeof(levelselect), sdr);
                Window.Current.Content = rootFrame;
                Window.Current.Activate();
            }
            else
            {
                resultScreen.Visibility = tryagainButton.Visibility = resultsButton.Visibility = resultAvatar.Visibility = tryagainMessageTextBox.Visibility = Visibility.Collapsed;
                DecreaseMonsterHealth.Stop();
                DecreasePlayerHealth.Stop();
                reset(true);
                int temp = (int)monsterCountDown.Text.ElementAt(4) - 48;
                monsterCountDown.Text = "CD: " + (temp - 1);
                if (temp - 1 == 0)
                {
                    //DecreasePlayerHealth.Begin();
                    //monsterCountDown.Text = "CD: " + 2;
                    monsterCountDown.Text = "CD: " + cooldownnumber;

                }
            }

        }

        // Checks if the button pressed is the right answer for the sentence displayed above the monster. If the answer is correct, the 
        // following events occur:
        //   - Timer t and pt will be reset to their default values, 5 and 8, respectively
        //   - The 'CD' or 'Count Down' value to the right of the monster will decrement
        //   - The monster's health will decrease
        //   - The choices of particles and sentence above the monster will change
        // If the answer is wrong:
        //   - Timer t and pt will reset
        //   - The 'CD' value will decrease
        //   - The sentence above the monster will not change, but the order of the particles will be shuffled
        //   - Note that if the value of pt reaches zero, it will be counted as a 'wrong answer' and all events corresponding to wrong answers
        // will be executed
        // If the monster's health reaches zero, a result screen is displayed, showing the amount of correct and wrong answers given
        async Task checkParticleButton(Button button)
        {
            ischeckingparticle = true;
            DecreaseParticlesTimer.Pause();

            if (stoptime.Visibility != Visibility.Collapsed)
            {
                covertime.Visibility = Visibility.Visible;
            }
            else
            {
                covertime.Visibility = Visibility.Collapsed;
            }
            if (heal.Visibility != Visibility.Collapsed)
            {
                coverheal.Visibility = Visibility.Visible;
            }
            else
            {
                coverheal.Visibility = Visibility.Collapsed;
            }
            int alternate = 0;
            int delay = 0;
            int indexOfSentence = sentences.IndexOf(sentencesTextBox.Text);

            particleButtonLEFT.IsEnabled = particleButtonMIDDLE.IsEnabled = particleButtonRIGHT.IsEnabled = particleButtonRIGHTIGHT.IsEnabled = false;
            double temp = monsterHealth.Value;
            if (copy)
            {
                sdr.answersRESULTS.Add(button.Content.ToString());
            }

            if (button.Content.ToString() == answers[indexOfSentence])
            {
                if (copy)
                {
                    sdr.answersRESULTS.Add("\n");
                }

                NumOfCorrect++;
                temp--; //NOTE: temp and monsterHealth.Value represent the monster's health, but the animation happens AFTER every line of
                        // code is executed. This is why temp was created so that the monster's health can be updated as to not unnecessarily
                        // call the reset function. Thus, when monsterHealth.Value is updated, so must temp.

                //***************************************first animation********************************************************************************


                for (int i = 1; i < 9; i++)
                {
                    if (alternate == 0)
                    {
                        sentencesTextBox.Margin = new Thickness(38, 58, 0, 0);
                        await System.Threading.Tasks.Task.Delay(100);
                        alternate++;
                    }
                    else
                    {
                        sentencesTextBox.Margin = new Thickness(48, 58, 0, 0);
                        await System.Threading.Tasks.Task.Delay(100);
                        alternate = 0;
                    }
                }
                await System.Threading.Tasks.Task.Delay(500);

                if (button == particleButtonLEFT)
                {
                    correctanswersignleft.Visibility = Visibility.Visible;
                    await System.Threading.Tasks.Task.Delay(600);
                }
                else if (button == particleButtonMIDDLE)
                {
                    correctanswersignmiddle.Visibility = Visibility.Visible;
                    await System.Threading.Tasks.Task.Delay(600);
                }
                else if (button == particleButtonRIGHT)
                {
                    correctanswersignright.Visibility = Visibility.Visible;
                    await System.Threading.Tasks.Task.Delay(600);
                }
                else
                {
                    correctanswersignrightright.Visibility = Visibility.Visible;
                    await System.Threading.Tasks.Task.Delay(600);
                }
                for (int i = 1; i < 100; i++)
                {
                    if (delay == 2)
                    {
                        await Task.Delay(10);

                        delay = 0;
                    }
                    else
                    {
                        sentencesTextBox.Margin = new Thickness(43, 58 + i, 0, 0);
                        delay++;
                    }
                }

                ShowAnimation.Begin();

                await System.Threading.Tasks.Task.Delay(100);
                ShowAnimation.Begin();
                await System.Threading.Tasks.Task.Delay(1200);
                if (monsterHealth.Value - 1 > 0)
                {
                    int temp2 = (int)monsterCountDown.Text.ElementAt(4) - 48;
                    monsterCountDownDecrementer();


                    if (temp2 - 1 == 0)
                    {
                        if(attacknumber == 1)
                        {
                            await System.Threading.Tasks.Task.Delay(800);

                        }
                        else if(attacknumber == 2)
                        {
                            await System.Threading.Tasks.Task.Delay(1200);

                        }
                        else if(attacknumber == 3)
                        {
                            
                            await System.Threading.Tasks.Task.Delay(2000);

                        }
                        else if(attacknumber == 4)
                        {
                            await System.Threading.Tasks.Task.Delay(2400);

                        }
                        else if (attacknumber == 5)
                        {
                            await System.Threading.Tasks.Task.Delay(2600);

                        }
                        else
                        {
                            await System.Threading.Tasks.Task.Delay(3000);
                        }
                    }
                    //monsterCountDownDecrementer();
                }
                
                DecreaseMonsterHealth.Begin();

                if (temp != 0)
                {
                    reset(false);
                }
            }
            else
            {

                for (int i = 1; i < 9; i++)
                {
                    if (alternate == 0)
                    {
                        sentencesTextBox.Margin = new Thickness(38, 58, 0, 0);
                        await System.Threading.Tasks.Task.Delay(100);
                        alternate++;
                    }
                    else
                    {
                        sentencesTextBox.Margin = new Thickness(48, 58, 0, 0);
                        await System.Threading.Tasks.Task.Delay(100);
                        alternate = 0;
                    }
                }
                await System.Threading.Tasks.Task.Delay(500);
                NumOfWrong++;
                if (button == particleButtonLEFT)
                {
                    wronganswersignleft.Visibility = Visibility.Visible;
                    await System.Threading.Tasks.Task.Delay(600);
                }
                else if (button == particleButtonMIDDLE)
                {
                    wronganswersignmiddle.Visibility = Visibility.Visible;
                    await System.Threading.Tasks.Task.Delay(600);
                }
                else if (button == particleButtonRIGHT)
                {
                    wronganswersignright.Visibility = Visibility.Visible;
                    await System.Threading.Tasks.Task.Delay(600);
                }
                else
                {
                    wronganswersignrightright.Visibility = Visibility.Visible;
                    await System.Threading.Tasks.Task.Delay(600);
                }
                int temp2 = (int)monsterCountDown.Text.ElementAt(4) - 48;
                monsterCountDownDecrementer();


                if (temp2 - 1 == 0)
                {
                    if (attacknumber == 1)
                    {
                        await System.Threading.Tasks.Task.Delay(800);

                    }
                    else if (attacknumber == 2)
                    {
                        await System.Threading.Tasks.Task.Delay(1200);

                    }
                    else if (attacknumber == 3)
                    {

                        await System.Threading.Tasks.Task.Delay(2000);

                    }
                    else if (attacknumber == 4)
                    {
                        await System.Threading.Tasks.Task.Delay(2400);

                    }
                    else if (attacknumber == 5)
                    {
                        await System.Threading.Tasks.Task.Delay(2600);

                    }
                    else
                    {
                        await System.Threading.Tasks.Task.Delay(4200);
                    }
                }

                reset(true);
            }
            covertime.Visibility = coverheal.Visibility = Visibility.Collapsed;
            ischeckingparticle = false;
        }

        async

                // Decrements the value of 'CD' by converting the last element to an int and adding the edited value back to the 'CD' string
                void monsterCountDownDecrementer()
        {
            int temp = (int)monsterCountDown.Text.ElementAt(4) - 48;
            monsterCountDown.Text = "CD: " + (temp - 1);

            if (temp - 1 == 0)
            {
                for (int i = attacknumber; i != 0; i--)
                {
                    if(playerHealth.Value <= 0)
                    {
                        break;
                    }
                    else
                    {
                        DecreasePlayerHealth.Begin();
                        await System.Threading.Tasks.Task.Delay(525);
                    }
                    
                }
                
                //monsterCountDown.Text = "CD: " + 2;
                monsterCountDown.Text = "CD: " + cooldownnumber;

            }

        }

        public void reset() // For levels with multiple monsters
        {

            //if (attacknumber <= 2)
            //{
            //    await System.Threading.Tasks.Task.Delay(800);
            //}
            //else if (attacknumber <= 4)
            //{
            //    await System.Threading.Tasks.Task.Delay(1600);
            //}
            //else
            //{
            //    await System.Threading.Tasks.Task.Delay(2000);
            //}
            prevSentence = sentencesTextBox.Text;
            monsterCountDown.Text = "CD: " + cooldownnumber;

            //monsterCountDown.Text = "CD: " + 2;
            sentencesTextBox.Margin = new Thickness(43, 58, 0, 0);

            wronganswersignrightright.Visibility = correctanswersignrightright.Visibility = wronganswersignleft.Visibility = wronganswersignmiddle.Visibility = wronganswersignright.Visibility = correctanswersignleft.Visibility = correctanswersignmiddle.Visibility = correctanswersignright.Visibility = Visibility.Collapsed;
            particleButtonLEFT.IsEnabled = particleButtonMIDDLE.IsEnabled = particleButtonRIGHT.IsEnabled = particleButtonRIGHTIGHT.IsEnabled = false;
            particleButtonLEFT.Content = particleButtonMIDDLE.Content = particleButtonRIGHT.Content = particleButtonRIGHTIGHT.Content = null;

            if(resultScreen.Visibility != Visibility.Visible)
            {

                DecreaseParticlesTimer.Stop();
                particlesTimer.Value = particlesTimer.Maximum;
                t_time = 0;
                t.Start();
            }
            else
            {
                DecreaseParticlesTimer.Stop();
                particlesTimer.Value = particlesTimer.Maximum;
            }
            

            sentencesTextBox.Text = sentences[r.Next(0, sentences.Count)];

            while (prevSentence == sentencesTextBox.Text)
            {
                sentencesTextBox.Text = sentences[r.Next(0, sentences.Count)];
            }

            index = sentences.IndexOf(sentencesTextBox.Text);
            if (!sdr.sentencesRESULTS.Contains(sentencesTextBox.Text))
            {
                sdr.sentencesRESULTS.Add(sentencesTextBox.Text);
                sdr.sentencesRESULTS.Add("\n");
                copy = true;
            }
            else
            {
                copy = false;
            }

            int temp = r.Next(1, 12);

            if (temp <= 3)
            {
                particleButtonLEFT.Content = answers[index];
                particles.Remove(particleButtonLEFT.Content.ToString());

                particleButtonMIDDLE.Content = particles[r.Next(0, particles.Count)];
                particles.Remove(particleButtonMIDDLE.Content.ToString());

                particleButtonRIGHT.Content = particles[r.Next(0, particles.Count)];
                particles.Remove(particleButtonRIGHT.Content.ToString());

                particleButtonRIGHTIGHT.Content = particles[r.Next(0, particles.Count)];
                particles.Remove(particleButtonRIGHTIGHT.Content.ToString());

            }
            else if (temp >= 4 && temp <= 6)
            {
                particleButtonMIDDLE.Content = answers[index];
                particles.Remove(particleButtonMIDDLE.Content.ToString());

                particleButtonLEFT.Content = particles[r.Next(0, particles.Count)];
                particles.Remove(particleButtonLEFT.Content.ToString());

                particleButtonRIGHT.Content = particles[r.Next(0, particles.Count)];
                particles.Remove(particleButtonRIGHT.Content.ToString());

                particleButtonRIGHTIGHT.Content = particles[r.Next(0, particles.Count)];
                particles.Remove(particleButtonRIGHTIGHT.Content.ToString());
            }
            else if (temp >= 7 && temp <= 9)
            {
                particleButtonRIGHT.Content = answers[index];
                particles.Remove(particleButtonRIGHT.Content.ToString());

                particleButtonRIGHTIGHT.Content = particles[r.Next(0, particles.Count)];
                particles.Remove(particleButtonRIGHTIGHT.Content.ToString());

                particleButtonLEFT.Content = particles[r.Next(0, particles.Count)];
                particles.Remove(particleButtonLEFT.Content.ToString());

                particleButtonMIDDLE.Content = particles[r.Next(0, particles.Count)];
                particles.Remove(particleButtonMIDDLE.Content.ToString());
            }
            else
            {
                particleButtonRIGHTIGHT.Content = answers[index];
                particles.Remove(particleButtonRIGHTIGHT.Content.ToString());

                particleButtonLEFT.Content = particles[r.Next(0, particles.Count)];
                particles.Remove(particleButtonLEFT.Content.ToString());

                particleButtonMIDDLE.Content = particles[r.Next(0, particles.Count)];
                particles.Remove(particleButtonMIDDLE.Content.ToString());

                particleButtonRIGHT.Content = particles[r.Next(0, particles.Count)];
                particles.Remove(particleButtonRIGHT.Content.ToString());
            }

            particles.Clear();

            particles.Add("を");
            particles.Add("に");
            particles.Add("が");
            particles.Add("で");
            particles.Add("の");
            particles.Add("は");
        }

        // This function is called after every tap on the particle buttons or when the value of pt reaches zero. The algorithms are mostly the
        // same as those in the 'OnNavigateTo' function except for the 'WrongAns' variable. This variable is true when a correct answer is
        // chosen and stores false otherwise. The statements in the if-loop will only execute if a correct answer is given. Else, the currently
        // displayed sentence will not change
        public void reset(bool WrongAns)
        {
            prevSentence = sentencesTextBox.Text;

            sentencesTextBox.Margin = new Thickness(43, 58, 0, 0);
            //particleButtonLEFT.IsEnabled = particleButtonMIDDLE.IsEnabled = particleButtonRIGHT.IsEnabled = true;
            wronganswersignrightright.Visibility = correctanswersignrightright.Visibility = wronganswersignleft.Visibility = wronganswersignmiddle.Visibility = wronganswersignright.Visibility = correctanswersignleft.Visibility = correctanswersignmiddle.Visibility = correctanswersignright.Visibility = Visibility.Collapsed;
            particleButtonLEFT.IsEnabled = particleButtonMIDDLE.IsEnabled = particleButtonRIGHT.IsEnabled = particleButtonRIGHTIGHT.IsEnabled = false;
            particleButtonLEFT.Content = particleButtonMIDDLE.Content = particleButtonRIGHT.Content = particleButtonRIGHTIGHT.Content = null;

            //timer.Text = "5";
            if (resultScreen.Visibility != Visibility.Visible)
            {

                DecreaseParticlesTimer.Stop();
                particlesTimer.Value = particlesTimer.Maximum;
                t_time = 0;
                t.Start();
            }
            else
            {
                DecreaseParticlesTimer.Stop();
                particlesTimer.Value = particlesTimer.Maximum;
            }

            if (!WrongAns)
            {
                sentencesTextBox.Text = sentences[r.Next(0, sentences.Count)];

                while (prevSentence == sentencesTextBox.Text)
                {
                    sentencesTextBox.Text = sentences[r.Next(0, sentences.Count)];
                }

                index = sentences.IndexOf(sentencesTextBox.Text);
                if (!sdr.sentencesRESULTS.Contains(sentencesTextBox.Text))
                {
                    sdr.sentencesRESULTS.Add(sentencesTextBox.Text);
                    sdr.sentencesRESULTS.Add("\n");
                    copy = true;
                }
                else
                {
                    copy = false;
                }
            }

            int temp = r.Next(1, 12);

            if (temp <= 3)
            {
                particleButtonLEFT.Content = answers[index];
                particles.Remove(particleButtonLEFT.Content.ToString());

                particleButtonMIDDLE.Content = particles[r.Next(0, particles.Count)];
                particles.Remove(particleButtonMIDDLE.Content.ToString());

                particleButtonRIGHT.Content = particles[r.Next(0, particles.Count)];
                particles.Remove(particleButtonRIGHT.Content.ToString());

                particleButtonRIGHTIGHT.Content = particles[r.Next(0, particles.Count)];
                particles.Remove(particleButtonRIGHTIGHT.Content.ToString());

            }
            else if (temp >= 4 && temp <= 6)
            {
                particleButtonMIDDLE.Content = answers[index];
                particles.Remove(particleButtonMIDDLE.Content.ToString());

                particleButtonLEFT.Content = particles[r.Next(0, particles.Count)];
                particles.Remove(particleButtonLEFT.Content.ToString());

                particleButtonRIGHT.Content = particles[r.Next(0, particles.Count)];
                particles.Remove(particleButtonRIGHT.Content.ToString());

                particleButtonRIGHTIGHT.Content = particles[r.Next(0, particles.Count)];
                particles.Remove(particleButtonRIGHTIGHT.Content.ToString());
            }
            else if (temp >= 7 && temp <= 9)
            {
                particleButtonRIGHT.Content = answers[index];
                particles.Remove(particleButtonRIGHT.Content.ToString());

                particleButtonRIGHTIGHT.Content = particles[r.Next(0, particles.Count)];
                particles.Remove(particleButtonRIGHTIGHT.Content.ToString());

                particleButtonLEFT.Content = particles[r.Next(0, particles.Count)];
                particles.Remove(particleButtonLEFT.Content.ToString());

                particleButtonMIDDLE.Content = particles[r.Next(0, particles.Count)];
                particles.Remove(particleButtonMIDDLE.Content.ToString());
            }
            else
            {
                particleButtonRIGHTIGHT.Content = answers[index];
                particles.Remove(particleButtonRIGHTIGHT.Content.ToString());

                particleButtonLEFT.Content = particles[r.Next(0, particles.Count)];
                particles.Remove(particleButtonLEFT.Content.ToString());

                particleButtonMIDDLE.Content = particles[r.Next(0, particles.Count)];
                particles.Remove(particleButtonMIDDLE.Content.ToString());

                particleButtonRIGHT.Content = particles[r.Next(0, particles.Count)];
                particles.Remove(particleButtonRIGHT.Content.ToString());
            }

            particles.Clear();

            particles.Add("を");
            particles.Add("に");
            particles.Add("が");
            particles.Add("で");
            particles.Add("の");
            particles.Add("は");
        }
    }
}
