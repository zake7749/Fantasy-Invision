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
        List<Enemy> enemies;
        List<EnemyBullet> enemyBullet;
        int totalEnemy = 0;
        List<Bullet> playerBullet;
        Player player;
        EnemyBullet circle;

        SoundPlayer SFXBGM;
        System.Media.SoundPlayer SFXplayerShot = new System.Media.SoundPlayer(Application.StartupPath + "\\SFX\\DAMAGE.WAV");
        double angle; 
        double degree = 0.0;


        public Form1()
        {
            playerBullet = new List<Bullet>();
            enemyBullet = new List<EnemyBullet>();
            enemies = new List<Enemy>();

            InitializeComponent();
            player = new Player(100, 100);
            circle = new EnemyBullet(300, 300);
            this.panel1.Controls.Add(player.img);
            this.panel1.Controls.Add(circle.img);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Update.Start();
            //playing music in loop
            SFXBGM = new SoundPlayer("TOS8.wav");
            SFXBGM.PlayLooping();
            
        }

        //Move Object
        private void movePlayer()
        {
            if(player.canMove())
            {
                player.Move();
            }
        }
        private void moveEnemy()
        {
            foreach(Enemy e in enemies)
            {
                if(e.canMove())
                {
                    e.Move();
                    enemy_CreateBullet(e);
                }
            }
        }
        private void movePlayerBullet()
        {
            foreach(Bullet b in playerBullet)
            {
                b.Move();
            }
        }
        private void moveEnemyBullet()
        {
            foreach (EnemyBullet eb in enemyBullet)
            {
                eb.Move();
            }
        }

        //Bounder check
        private void BounderCheck()
        {
            enemyBounderCheck();
            bulletBounderCheck();
        }

        private void enemyBounderCheck()
        {
            foreach (Enemy e in enemies)
            {
                if (e.ly < 0)
                {
                    e.img.Dispose();
                }
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
            foreach (EnemyBullet b in enemyBullet)
            {
                if (b.ly > 1000)
                {
                    b.img.Dispose();
                }
            }
        }
        
        //Update
        private void FixedUpdate(object sender, EventArgs e)
        {
            
            degree++;
            
            angle = Math.PI * degree/180.0;
            
            //X += Update.Interval;
            movePlayer();
            moveEnemy();
            movePlayerBullet();
            moveEnemyBullet();

            bulletBounderCheck();

            circle.circleMove(angle);
            
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
                SFXplayerShot.Play();
                this.panel1.Controls.Add(b.img);
                playerBullet.Add(b);
            }
        }

        //Enemy
        private void enemy_CreateBullet(Enemy e)
        {
            if (e.canShoot())
            {
                Point xy = e.getShootPlace();
                EnemyBullet eb = new EnemyBullet(xy.X, xy.Y);
                this.panel1.Controls.Add(eb.img);
                enemyBullet.Add(eb);
            }
        }
        private void create_Enemy()
        {
            Random robj = new Random();
            int x = robj.Next(20, 600);
            Enemy e = new Enemy(x, 0);
            this.panel1.Controls.Add(e.img);
            enemies.Add(e);
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
                case Keys.Z:
                    create_Enemy();
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
        public int health;
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

        public void SetV(int x,int y)
        {
            vx = x;
            vy = y;
        }

        public Boolean isAlive()
        {
            if (health <= 0)
                return false;
            else
                return true;
        }

        protected void imgAutoSize()
        {
            img.Width = img.Image.Width;
            img.Height = img.Image.Height;
        }
    }

    public class Player : GameObject
    {
        public Player(int x, int y) : base(x,y)
        {
            health = 10;
            lx = x;
            ly = y;
            //f = frame = timer interval of FixUpdate 
            clock = 0;
            clockLimit = 10;//每隔 10f 有一發子彈
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

        public void Move()
        {
            if(lx+vx>0&&lx+vx<500)
                lx += vx;
            if(ly+vy>0&&ly+vy<600)
                ly += vy;
            img.Location = new Point(lx, ly);
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
            img.Image = Image.FromFile(Application.StartupPath + "\\assest\\Bllet_black.png");
            imgAutoSize();
            //img.BackColor = Color.Black;
        }
    }
    public class EnemyBullet : GameObject
    {
        //public double lx, ly;
        public int middleX, middleY;
        public int radius;

        public EnemyBullet(int x, int y)
            : base(x, y)
        {
            lx = x;
            ly = y;
            //f = frame = timer interval of FixUpdate 
            move = 0;
            moveLimit = 0;//每隔 1f 可以移動 p+vx,p+vy.
            vx = 0;
            vy = 5;
            img = new System.Windows.Forms.PictureBox();
            img.Location = new Point(lx, ly);
            img.Image = Image.FromFile(Application.StartupPath + "\\assest\\Bullet_black.BMP");

            radius = 100;
            middleX = x - 100;
            middleY = y;
        }

        public void circleMove(double sp)
        {
            lx = Convert.ToInt32(middleX + radius * Math.Cos(sp));
            ly = Convert.ToInt32(middleY + radius * Math.Sin(sp));
            img.Location = new Point(lx, ly);
        }
    }
    public class Enemy : GameObject
    {
        String Shootmode;

        int bulletNum;

        int bulletEachTime;
        int bulletRestoreLimit;
        int bulletRestoreClock;

        public Enemy(int x, int y) : base(x, y)
        {
            lx = x;
            ly = 0;
            //f = frame = timer interval of FixUpdate 
            clock = 0;
            clockLimit = 10;//每隔 20f 發射一顆子彈
            bulletNum = 5;
            bulletEachTime = 5;//每次射擊都會有 5 發子彈
            bulletRestoreClock = 0;
            bulletRestoreLimit = 175;//每隔 bulletRestoreLimit f 進行一次射擊
            move = 0;
            moveLimit = 0;//每隔 1f 可以移動 p+vx,p+vy.
            vx = 0;
            vy = 1;
            img = new System.Windows.Forms.PictureBox();
            img.Location = new Point(lx, ly);
            img.Image = Image.FromFile(Application.StartupPath + "\\assest\\player.png");
            Shootmode = "normal";
            health = 10;
        }
        public Boolean canShoot()
        {
            adjustTimeInterval();
            clock++;
            if (clock > clockLimit && bulletNum > 0)
            {
                clock = 0;
                bulletNum--;
                return true;
            }
            return false;
        }
        private void adjustTimeInterval()
        {
            bulletRestoreClock++;
            if(bulletRestoreClock>bulletRestoreLimit)
            {
                bulletRestoreClock = 0;
                bulletNum = bulletEachTime;
            }
        }
        public String getShootMode()
        {
            return Shootmode;
        }
    }
}
