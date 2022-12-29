using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml;

namespace DodgeGame_Project
{
    internal class GameBoard// includes the player class and enemy list
    {
        int boardWidth, boardHeight;
        const int ENEMY_COUNT = 5;
        DispatcherTimer timer;
        DispatcherTimer DounatTimer;
        PlayerUnit player;
        List<EnemyUnit> enemies = new List<EnemyUnit>();
        List<FallinDounat> dounats = new List<FallinDounat>();
        Random r = new Random();
        Canvas map;
        TextBlock txtScore;
        public int score = 0;

        public GameBoard(double width, double height, DispatcherTimer timer, DispatcherTimer DounatTimer, TextBlock Score)
        {
            boardWidth = (int)width;
            boardHeight = (int)height;
            this.timer = timer;
            this.DounatTimer = DounatTimer;
            txtScore = Score;
        }
        public void CreateBoard(Canvas newMap)
        {

            map = newMap;

            //creating player
            if (player != null)
            {
                map.Children.Remove(player.pic);
            }

            player = new PlayerUnit(boardWidth, boardHeight);
            Canvas.SetLeft(player.pic, boardWidth / 2);
            Canvas.SetTop(player.pic, boardHeight - 250);
            map.Children.Add(player.pic);

            //creating enemy
            if (enemies != null)
            {
                foreach (EnemyUnit enemy in enemies)
                {
                    map.Children.Remove(enemy.pic);
                }
            }

            enemies = new List<EnemyUnit>();

            for (int i = 0; i < ENEMY_COUNT; i++)
            {
                int locX= 350 * (i + 1);
                int locY= r.Next(boardHeight - 600);
                EnemyUnit newEnemy = new EnemyUnit(locX,locY);
                enemies.Add(newEnemy);

                Canvas.SetTop(enemies[i].pic, locY);
                Canvas.SetLeft(enemies[i].pic, locX);

                map.Children.Add(enemies[i].pic);
            }
            //creating dounat
            if (dounats != null)
            {
                foreach (FallinDounat dounat in dounats)
                {
                    map.Children.Remove(dounat.pic);
                }
            }
            dounats = new List<FallinDounat>();

        }
        public async void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args)// moves the user and checks if user clash
        {

            if (ClashAWall() && timer.IsEnabled)
            {
                switch (args.VirtualKey)
                {
                    case VirtualKey.Up:
                        {
                            player.MoveUp();
                            break;
                        }
                    case VirtualKey.Down:
                        {
                            player.MoveDown();
                            break;
                        }
                    case VirtualKey.Left:
                        {
                            player.MoveLeft();
                            break;
                        }
                    case VirtualKey.Right:
                        {
                            player.MoveRight();
                            break;
                        }

                }
                if (!ClashAWall())
                {
                    map.Children.Remove(player.pic);//end the game
                    player = new PlayerUnit(r.Next(boardWidth), r.Next(boardHeight));
                    DounatTimer.Stop();
                    timer.Stop();
                    await new MessageDialog("Out of bounds, you lose! \n please try again!").ShowAsync();

                }
            }

        }

        public void EnemyTick(object sender, object e)// moves the enemy and the dounats
        {
            for (int i = 0; i < dounats.Count; i++)
            {
                Canvas.SetTop(dounats[i].pic, Canvas.GetTop(dounats[i].pic) + 2); //Dounat's speed
                if (DetectDounatTouch(dounats[i]))
                {
                    score++;
                    txtScore.Text = $"Score: {score}";
                }
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                if (Canvas.GetLeft(enemies[i].pic) > Canvas.GetLeft(player.pic))
                    enemies[i].MoveLeft();

                else
                {
                    enemies[i].MoveRight();

                }
                if (Canvas.GetTop(enemies[i].pic) > Canvas.GetTop(player.pic))
                {
                    enemies[i].MoveUp();
                }
                else
                {
                    enemies[i].MoveDown();
                }
                DetectEnemyTouch(enemies[i]);
            }

            WinGame();
        }


        public async void DetectEnemyTouch(EnemyUnit enemy)//checks if enemy touch
        {
            if (enemy.IsTouching(player))
            {
                map.Children.Remove(player.pic);//end the game              
                DounatTimer.Stop();
                timer.Stop();
                await new MessageDialog("You lose! \n Try again!").ShowAsync();
            }

            foreach (EnemyUnit other in enemies)
            {
                if (enemies.IndexOf(other) == enemies.IndexOf(enemy))
                    return;

                if (enemy.IsTouching(other))
                {
                    map.Children.Remove(other.pic);
                    other.IsAlive = false;
                }
            }

        }
        public void DounatTick(object sender, object e)// move dounat
        {

            int x = r.Next(boardWidth);
            int y = 0;
            FallinDounat newDounat = new FallinDounat();
            dounats.Add(newDounat);

            Canvas.SetTop(dounats[dounats.Count - 1].pic, y);
            Canvas.SetLeft(dounats[dounats.Count - 1].pic, x);

            map.Children.Add(dounats[dounats.Count - 1].pic);

        }
        public bool DetectDounatTouch(FallinDounat dounat)//checks if enemy touches dounat
        {
            if (player.IsTouching(dounat))
            {
                map.Children.Remove(dounat.pic);
                dounats.Remove(dounat);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ClashAWall()// checks if player is out of bounds
        {
            if (Canvas.GetTop(player.pic) > 600 || Canvas.GetTop(player.pic) < 0) return false;
            if (Canvas.GetLeft(player.pic) > 1500 || Canvas.GetLeft(player.pic) < 0) return false;
            return true;

        }

        public async void WinGame()//check's win
        {
            int count = 0;
            foreach (EnemyUnit enemy in enemies)
            {
                if (enemy.IsAlive == false)
                {
                    count++;
                }
            }
            if (count == ENEMY_COUNT - 1)
            {
                timer.Stop();
                DounatTimer.Stop();
                await new MessageDialog($"YOU WON!!! \n You succeeded to escape the salat bowl's and to catch {score} dounats!").ShowAsync();
            }

        }
    }
}
