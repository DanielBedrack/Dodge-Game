using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace DodgeGame_Project
{
    internal class FallinDounat : MovableUnit
    {
        public static BitmapImage dounatPic = new BitmapImage(new Uri(@"ms-appx:///Assets/donut.png"));
        public const int DOUNAT_SPEED = 10;
        const int WIDTH = 35;
        const int HEIGHT = 35;


        public FallinDounat() : base(WIDTH, HEIGHT, DOUNAT_SPEED, dounatPic)
        {


        }

    }
}
