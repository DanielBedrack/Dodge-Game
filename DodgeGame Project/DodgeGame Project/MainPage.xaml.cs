using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Popups;
using Windows.Media.Playback;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DodgeGame_Project
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        GameBoard game;//nameing gameboard
        DispatcherTimer timer = new DispatcherTimer();//creating timer
        DispatcherTimer DounatTimer = new DispatcherTimer(); //crating DounatTimer

        bool pause = true;
        public MediaElement themeSong;



        public MainPage()
        {

            this.InitializeComponent();
            //======================================================================================================================

            Rect windowRectangle = ApplicationView.GetForCurrentView().VisibleBounds;//size of board

            DounatTimer = new DispatcherTimer();
            DounatTimer.Interval = TimeSpan.FromSeconds(3);
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(20);//time to tick 

            game = new GameBoard(windowRectangle.Width, windowRectangle.Height, timer, DounatTimer, Score);//adding size to gameboard,

            DounatTimer.Tick += game.DounatTick;
            timer.Tick += game.EnemyTick;

            Window.Current.CoreWindow.KeyDown += game.CoreWindow_KeyDown;// callind the pleyer movement method

            createMediaElement();

        }


        private void Pause_Continue_Click(object sender, RoutedEventArgs e)
        {
            if (pause)
            {
                timer.Stop();
                DounatTimer.Stop();
                themeSong.Stop();
                Pause_Continue.Content = "Resume";
                pause = false;
            }
            else
            {
                DounatTimer.Start();
                timer.Start();
                themeSong.Play();
                Pause_Continue.Content = "Pause";
                pause = true;

            }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {

            game.CreateBoard(GameON);
            timer.Start();
            DounatTimer.Start();
            themeSong.Play();
            Reset.Content = "Reset";
            game.score = 0;
            Score.Text = $"Score: 0";

        }


        public void createMediaElement()
        {
            themeSong = new MediaElement();
            themeSong.Source = new Uri(@"ms-appx:///audio/familyGuy.mp3");
            themeSong.Margin = new Thickness(0);
            themeSong.Volume = 0.9;
            themeSong.Visibility = Visibility.Collapsed;
            FatherGrid.Children.Add(themeSong);
            themeSong.IsLooping = true;
            themeSong.Play();

        }


    }
}
