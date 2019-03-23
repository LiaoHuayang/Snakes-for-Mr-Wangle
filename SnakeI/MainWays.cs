using SnakeI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SnakeI
{
    public  class MainWays
    {
       static public List<Snake>  InitiSnakes(Point location,int speed)
        {
            List<Snake> ssss = new List<Snake>();
            Snake[] ss = new Snake[4];
            ssss.Clear();
           
            for(int i=0;i<4;i++)
            {
                ss[i]= new Snake(new Point(location.X-i*60,location.Y),speed,Snake.Direction.Right);
                ssss.Add(ss[i] );
            }
            return ssss;
        }
      static  public bool IsEaten(List<Snake> snakes,Foods f)
        {
         
            int sX = snakes[0].Location.X, sY = snakes[0].Location.Y;
            int fX = f.Location.X, fY = f.Location.Y;
            if (sX<fX+30&&sX>fX-30&& sY < fY + 20 && sY> fY - 20)
            {
                SoundPlayer sp = new SoundPlayer(Properties.Resources.eat);
                sp.Play();
                Snake s = snakes.Last();
                switch(s.Dir)
                {
                    case Snake.Direction.Up :
                        snakes.Add(new Snake(new Point(s.Location.X , s.Location.Y+60), s.Speed, s.Dir));
                        break;
                    case Snake.Direction.Down:
                        snakes.Add(new Snake(new Point(s.Location.X, s.Location.Y - 60), s.Speed, s.Dir));
                        break;
                    case Snake.Direction.Left:
                        snakes.Add(new Snake(new Point(s.Location.X+60, s.Location.Y), s.Speed, s.Dir));
                        break;
                    default:
                        snakes.Add(new Snake(new Point(s.Location.X - 60, s.Location.Y), s.Speed, s.Dir));
                        break;
                }
                
                return true;
            }
            return false;
        }
       static public bool IsOver(List<Snake> snakes,Size formSize)
        {
            Point p = snakes[0].Location;
            //for (int i = snakes.Count-1; i >0; i--)
            //    {
            //    if (p == snakes[i].Location)
            //    {
            //        return true;
            //    }
            //}
            int sX = snakes[0].Location.X, sY = snakes[0].Location.Y;
            foreach (Snake  s in snakes)
            {
                int fX = s.Location.X, fY = s.Location.Y;
                if (s!=snakes[0]&&sX < fX + 20 && sX > fX - 20 && sY < fY + 20 && sY > fY - 20)
                {
                    return true;
                }
            }
         
            if (p.X < 0 || p.X > formSize.Width-60 || p.Y < 0 || p.Y > formSize.Height-60)
                return true;
        
            return false;
        }
    }
}
