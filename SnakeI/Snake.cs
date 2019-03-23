using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeI
{
   public  class Snake
    {
        private Point location;
        private Image snke;
        private int speed;
        private Direction dir;
        private bool head;
   
        static private Snake s = null;
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
        public int Speed
        {
            get
            {
                return speed;
            }

            set
            {
                speed = value;
            }
        }
        public Direction Dir
        {
            get
            {
                return dir;
            }

            set
            {
                dir = value;
            }
        }
        public static Snake S
        {
            get
            {
                return s;
            }

            set
            {
                s = value;
            }
        }
        public Snake()
        { }
        public Snake(Point location, int speed, Direction dir)
        {
            this.location = location;
            this.speed = speed;
            this.dir = dir;
            if(S==null)
            {
                head = true;
                HeadImage();
                S = new Snake();
            }
            else
            {
                head = false;
                BodyImage();
            }

        }
        private void BodyImage()
        {
            switch (this.dir)
            {
                case Direction.Up:
                    snke = Properties.Resources.up;
                    break;
                case Direction.Down:
                    snke = Properties.Resources.down;
                    break;
                case Direction.Left:
                    snke = Properties.Resources.left;
                    break;
                default:
                    snke = Properties.Resources.right;
                    break;
            }

        }
        private void HeadImage()
        {
            switch (this.dir)
            {
                case Direction.Up:
                    snke = Properties.Resources.chiup;
                    break;
                case Direction.Down:
                    snke = Properties.Resources.chidown;
                    break;
                case Direction.Left:
                    snke = Properties.Resources.chileft;
                    break;
                default:
                    snke = Properties.Resources.chiright;
                    break;
            }
        }
        public  enum Direction
        {
            Left,
            Right,
            Up,
            Down
        }
        public void Move()
        {
             switch(Dir)
            {
                case Direction.Up:
                    location.Y -=Speed;
                    break;
                case Direction.Down:
                    location.Y += Speed;
                    break;
                case Direction.Left:
                    location.X -= Speed;
                    break;
                default :
                    location.X += Speed;
                    break;
            }
            if (head)
                HeadImage();
            else
                BodyImage();
        }
        public void Draw(Graphics g)
        {
            Move();
            g.DrawImage(snke, Location.X, Location.Y, 60, 60);
        }
    }
}
