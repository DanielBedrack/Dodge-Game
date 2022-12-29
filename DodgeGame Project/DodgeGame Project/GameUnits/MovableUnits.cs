using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace DodgeGame_Project
{
    public abstract class MovableUnit// the father class which declare touching and user's movement
    {
        public Image pic = new Image();
        int speed;

        public MovableUnit(int width, int height, int speed, BitmapImage pic)
        {
            this.pic.Source = pic;
            this.pic.Width = width;
            this.pic.Height = height;
            this.speed = speed;

        }
        public void MoveUp()
        {
            Canvas.SetTop(this.pic, Canvas.GetTop(this.pic) - this.speed);
        }
        public void MoveDown()
        {
            Canvas.SetTop(this.pic, Canvas.GetTop(this.pic) + this.speed);
        }
        public void MoveLeft()
        {
            Canvas.SetLeft(this.pic, Canvas.GetLeft(this.pic) - this.speed);
        }
        public void MoveRight()
        {
            Canvas.SetLeft(this.pic, Canvas.GetLeft(this.pic) + this.speed);
        }


        public bool IsTouching(MovableUnit item)//detcet clashing
        {
            double myTop = Canvas.GetTop(this.pic), otherTop = Canvas.GetTop(item.pic), myLeft = Canvas.GetLeft(this.pic), otherLeft = Canvas.GetLeft(item.pic);

            return (Math.Abs(myTop - otherTop) < this.pic.Height && Math.Abs(myLeft - otherLeft) < this.pic.Width);


        }




    }

}
