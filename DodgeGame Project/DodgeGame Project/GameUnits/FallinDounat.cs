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
        public static BitmapImage dounatPic = new BitmapImage(new Uri(@"ms-appx:///Assets/Humburger.png"));
        public const int DOUNAT_SPEED = 10;
        const int WIDTH = 70;
        const int HEIGHT = 70;


        public FallinDounat() : base(WIDTH, HEIGHT, DOUNAT_SPEED, dounatPic)
        {


        }

    }
}
