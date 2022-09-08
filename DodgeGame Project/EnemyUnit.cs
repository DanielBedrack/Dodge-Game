using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;


namespace DodgeGame_Project
{
    public class EnemyUnit : MovableUnit
    {
        public static BitmapImage badGuy = new BitmapImage(new Uri(@"ms-appx:///Assets/Salat_bowl.png"));
        public const int ENEMY_SPEED = 2;
        const int WIDTH = 50;
        const int HEIGHT = 50;
        public bool IsAlive = true;

        public EnemyUnit(int x, int y) : base(WIDTH, HEIGHT, ENEMY_SPEED, badGuy)
        {

        }

    }

}
