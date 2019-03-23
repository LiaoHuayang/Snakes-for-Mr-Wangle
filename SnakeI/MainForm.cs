using SnakeI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        static  Graphics g;
        Foods f;
        Random rand;
        bool replayfoods= true;

        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow(); //获得本窗体的句柄
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);//设置此窗体为活动窗体
        static List<Snake> snakes = new List<Snake>();
      static  List<Snake> ocus = new List<Snake>();

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            List<Snake> temp = new List<Snake>();
            if (replayfoods)
            {
                replayfoods = !replayfoods;
                f.LayFood(g);
            }
            else
            {
                f.Draw(g);
            }
                foreach (Snake s in snakes)
            {
                foreach (Snake sPositin in ocus)
                {
                    if (s.Location == sPositin.Location)
                    {
                        s.Dir = sPositin.Dir;
                        if(s==snakes.Last())
                        {
                            temp.Add(sPositin);
                        }
                    }
                    }
                s.Draw(g);
            }
            foreach (Snake sp in temp)
                ocus.Remove(sp);
        }

        private void tmr_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
            if (this.Handle != GetForegroundWindow()) //持续使该窗体置为最前,屏蔽该行则单次置顶
            {
                SetForegroundWindow(this.Handle);
            }

                if (MainWays.IsEaten(snakes, f))
            {
                replayfoods = true;
            }
  
            if (MainWays.IsOver(snakes,this.Size))
            {
                tmr.Enabled = !tmr.Enabled;
                SoundPlayer sp = new SoundPlayer(Properties.Resources.game_over);
                sp.Play();
                if (MessageBox.Show("你是否再来一局！", "游戏结束", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    InitiSnake();
                else
                    Application.Exit();
            }

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            InitiSnake();
            //SoundPlayer sp = new SoundPlayer(Properties.Resources.gamebg);
            //sp.Play();
        }

        private void InitiSnake()
        {
            snakes.Clear();
            ocus.Clear();
            Snake.S = null;
            replayfoods = true;
             foreach(Snake s in MainWays.InitiSnakes(new Point(600,240), 10))
            {
                snakes.Add(s);
            }
            rand = new Random();
            f = new Foods(this.Size, rand);
            tmr.Enabled = true;
            //SoundPlayer sp = new SoundPlayer(Properties.Resources.gamebg);
            //sp.Play();
        }



        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
           // MessageBox.Show(e.KeyCode.ToString());
            switch (e.KeyCode)
            {
                case Keys.Down:
                    if (snakes[0].Dir != Snake.Direction.Up)
                    {
                        snakes[0].Dir = Snake.Direction.Down;
                        ocus.Add(new Snake(snakes[0].Location, snakes[0].Speed, snakes[0].Dir));
                    }
                    break;
                case Keys.Up:
                    if (snakes[0].Dir != Snake.Direction.Down)
                    {
                        snakes[0].Dir = Snake.Direction.Up;
                        ocus.Add(new Snake(snakes[0].Location, snakes[0].Speed, snakes[0].Dir));
                    }
                    break;
                case Keys.Left:
                    if (snakes[0].Dir != Snake.Direction.Right)
                    {
                        snakes[0].Dir = Snake.Direction.Left;
                        ocus.Add(new Snake(snakes[0].Location, snakes[0].Speed, snakes[0].Dir));
                    }
                    break;
                case Keys.Right:
                    if (snakes[0].Dir != Snake.Direction.Left)
                    {
                        snakes[0].Dir = Snake.Direction.Right;
                        ocus.Add(new Snake(snakes[0].Location, snakes[0].Speed, snakes[0].Dir));
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
