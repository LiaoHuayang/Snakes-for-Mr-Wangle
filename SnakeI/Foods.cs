using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeI
{
   public  class Foods
    {
        private Point location;
        static   private Image food=Properties.Resources.food;
        private Size formsize;
        private Random rand;
        public Point Location
        {
            get
            {
                return location;
            }

            set
            {
                location = value;
            }
        }

        public Foods(Size formsize, Random rand)
        {
            this.rand = rand;
            this.formsize = formsize;
        }
        public void Draw(Graphics g)
        {
            g.DrawImage(food, location.X,location.Y, 60, 60);
        }
        public  void LayFood(Graphics g)
        {
            int x = rand.Next(0, formsize.Width-120);
            int y = rand.Next(0, formsize.Height-120);
            this.location = new Point(x, y);
            g.DrawImage(food, x,y, 60, 60);
        }
    }
}
