using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace DodgeGame_Project
{
    public class PlayerUnit : MovableUnit// the class of the user, including a pic and speed of movement
    {
        static BitmapImage goodGuy = new BitmapImage(new Uri(@"ms-appx:///Assets/PeterGriffin.png"));

        public const int PLAYER_SPEED = 30;
        const int WIDTH = 150;
        const int HEIGHT = 150;

        public PlayerUnit(int x, int y) : base(WIDTH, HEIGHT, PLAYER_SPEED, goodGuy)
        {

        }
    }
}
