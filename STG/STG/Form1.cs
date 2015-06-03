using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace STG
{
    public partial class Form1 : Form
    {
        List<PictureBox> enemy;
        List<PictureBox> enemyBullet;
        int est;
        List<Bullet> playerBullet;
        Player player;
        Button b;
        public Form1()
        {
            playerBullet = new List<Bullet>();
            InitializeComponent();
            player = new Player(100, 100);
            this.panel1.Controls.Add(player.img);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL = @"TOS8.m4a";
            axWindowsMediaPlayer1.settings.setMode("loop", true);
            axWindowsMediaPlayer1.settings.autoStart = true;
        }

        private void movePlayer()
        {
            if(player.canMove())
            {
                player.Move();
            }
        }

        private void movePlayerBullet()
        {
            foreach(Bullet b in playerBullet)
            {
                b.Move();
            }
        }

        private void bulletBounderCheck()
        {
            foreach(Bullet b in playerBullet)
            {
                if (b.ly < 0)
                {
                    b.img.Dispose();
                }
            }
        }

        private void FixedUpdate(object sender, EventArgs e)
        {
            movePlayer();
            movePlayerBullet();
            bulletBounderCheck();
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void player_CreateBullet()
        {
            if(player.canShoot())
            {
                Point xy = player.getShootPlace();
                Bullet b = new Bullet(xy.X,xy.Y);
                this.panel1.Controls.Add(b.img);
                playerBullet.Add(b);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    player.addV(0, -3);
                    break;
                case Keys.Down:
                    player.addV(0, 3);
                    break;
                case Keys.Right:
                    player.addV(3, 0);
                    break;
                case Keys.Left:
                    player.addV(-3, 0);
                    break;
                case Keys.Space:
                    player_CreateBullet();
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    player.vy =0;
                    break;
                case Keys.Down:
                    player.vy =0;
                    break;
                case Keys.Right:
                    player.vx =0;
                    break;
                case Keys.Left:
                    player.vx = 0;
                    break;
            }
        }
    }

    public class GameObject
    {
        public int lx,ly;//location
        public int vx,vy;//velocity
        public int vxupLimit,vyupLimit;//
        public int vxdownLimit,vydownLimit;//
        public int clock, clockLimit;//for update
        public int move, moveLimit;//for update
        public PictureBox img;

        public GameObject(int x,int y)
        {

        }

        public Boolean canShoot()
        {
            clock++;
            if (clock > clockLimit)
            {
                clock = 0;
                return true;
            }
            return false;
        }

        public Point getShootPlace()
        {
            Point p = new Point(lx+20, ly);
            return p;
        }

        public Boolean canMove()
        {
            move++;
            if (move > moveLimit)
            {
                move = 0;
                return true;
            }
            return false;
        }

        public void Move()
        {
            lx += vx;
            ly += vy;
            img.Location = new Point(lx, ly);
        }
    }

    public class Player : GameObject
    {
        public Player(int x, int y) : base(x,y)
        {
            lx = x;
            ly = y;
            //f = frame = timer interval of FixUpdate 
            clock = 0;
            clockLimit = 1;//每隔 1f 有一發子彈
            move = 0;
            moveLimit = 0;//每隔 1f 可以移動 p+vx,p+vy.
            vx = 0;
            vxupLimit = 3;//x軸速度在 3~-3
            vxdownLimit = -3;
            vy = 0;
            vyupLimit = 3;//y軸速度在 3~-3
            vydownLimit = -3;
            img = new System.Windows.Forms.PictureBox();
            img.Location = new Point(lx,ly);
            img.Image = Image.FromFile(Application.StartupPath + "\\assest\\player.png");
        }

        public void addV(int ax,int ay)
        {
            if(ax!=0)
            {
                if (ax > 0 && vx < vxupLimit)
                    vx += ax;
                if (ax < 0 && vx > vxdownLimit)
                    vx += ax;
            }
            else
            {
                if (ay > 0 && vy < vyupLimit)
                    vy += ay;
                if (ay < 0 && vy > vydownLimit)
                    vy += ay;
            }
        }
     
    }

    public class Bullet : GameObject
    {
        public Bullet(int x, int y) : base(x, y)
        {
            lx = x;
            ly = y;
            //f = frame = timer interval of FixUpdate 
            move = 0;
            moveLimit = 0;//每隔 1f 可以移動 p+vx,p+vy.
            vx = 0;
            vy = -5;
            img = new System.Windows.Forms.PictureBox();
            img.Location = new Point(lx, ly);
            img.Image = Image.FromFile(Application.StartupPath + "\\assest\\Bullet_black.png");
        }
    }
}
