﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Diagnostics;
using Microsoft.DirectX;
using Microsoft.DirectX.DirectSound;

namespace STG
{
    public partial class Form1 : Form
    {
        List<Enemy> enemies;
        List<EnemyBullet> enemyBullet;
        int totalEnemy = 0;
        List<Bullet> playerBullet;
        Player player;
        Stopwatch gameTime = new Stopwatch();
        //Device dv = new Device();
        //SecondaryBuffer buf;
        string[] context = new string[20];
        int countContext = 0;

        SoundPlayer SFXBGM;
        System.Media.SoundPlayer SFXplayerShot = new System.Media.SoundPlayer(Application.StartupPath + "\\SFX\\DAMAGE.WAV");


        public Form1()
        {
            playerBullet = new List<Bullet>();
            enemyBullet = new List<EnemyBullet>();
            enemies = new List<Enemy>();

            InitializeComponent();
            player = new Player(100, 100);
            this.panel1.Controls.Add(player.img);

            labelTime.Text = "";
            labelContext.Text = "";
            //dv.SetCooperativeLevel(STG.Form1.ActiveForm, CooperativeLevel.Priority);
            //buf = new SecondaryBuffer(@"\SFX\DAMAGE.WAV", dv);
            SetStory();        
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Update.Stop();
            gameTime.Reset();
            //gameTime.Start();
            //playing music in loop
            SFXBGM = new SoundPlayer("TOS8.wav");
            SFXBGM.PlayLooping();

            //story mode
            labelContext.Text = context[countContext];
            countContext++;
            
        }

        //Move Object
        private void movePlayer()
        {
            if (player.canMove())
            {
                player.Move();
            }
        }
        private void moveEnemy()
        {
            foreach (Enemy e in enemies)
            {
                if (e.canMove())
                {
                    e.Move();
                    enemy_CreateBullet(e);
                }
            }
        }
        private void movePlayerBullet()
        {
            foreach (Bullet b in playerBullet)
            {
                b.Move();
            }
        }
        private void moveEnemyBullet()
        {
            foreach (EnemyBullet eb in enemyBullet)
            {
                if (eb.getMoveMode() == "Circle")
                    eb.circleMove();
                
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
            foreach (Bullet b in playerBullet)
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
            
            //degree++;
            //angle = Math.PI * degree / 180.0;

            //X += Update.Interval;
            movePlayer();
            moveEnemy();
            movePlayerBullet();
            moveEnemyBullet();

            bulletBounderCheck();
            Collision();
            //circle.circleMove(200.0, 200.0, angle);

            labelTime.Text = ((int)gameTime.Elapsed.TotalSeconds).ToString();
            if ((int)(gameTime.Elapsed.TotalSeconds) == 40)
            {
                if ((gameTime.Elapsed.TotalSeconds) == 41.0000000)
 
                    gameTime.Stop();

                Update.Stop();
                countContext = 6;
                labelContext.Visible = true;
                labelContext.Text = context[countContext];
            }
        }

        //Set story
        private void SetStory()
        {
            context[0] = "武則天：居然有人敢擄走朕的女兒，而且還是先前一向對我們友好的巴比倫銀河帝國";
            context[1] = "武則天：現在是仗著母星離地球很遠就可以藐視朕的權威";
            context[2] = "武則天：王將軍聽令，朕任你為大周星際帝國神都艦艦長";
            context[3] = "武則天：立刻前往銀河救回太平公主！";
            context[4] = "王孝傑：末將遵旨";
            context[5] = "";
            context[6] = "(敵方戰艦的殘骸中有一台播放器)";
            context[7] = "王孝傑：啟稟陛下，有一台敵軍遺留下來的撥放器";
            context[8] = "武則天：速速撥放";
            context[9] = "王孝傑：諾";
            context[10] = "...：哈哈哈哈哈！武媚娘，還記得我是誰嗎？";
            context[11] = "武則天：(究竟是誰，這聲音好耳熟...，該不會是徐慧？)";
            context[12] = "徐慧：沒錯，武媚娘！就是我，你的女兒就在我手上，哈哈哈哈哈...";
            context[13] = "武則天：你不是已經死了？現在怎麼還活著？你竟然擄走我的女兒，你這個賤人！王將軍，快救回公主！";
            context[14] = "王孝傑：遵旨";
            context[15] = "";
        }

        //Collision
        private void Collision()
        {
            foreach (EnemyBullet eb in enemyBullet)
            {
                if (Math.Abs((int)(eb.lx) - 18 - (int)(player.lx)) < 18 && Math.Abs((int)(eb.ly) - 25 - (int)(player.ly)) < 25)
                {
                    System.Threading.Thread.Sleep(100);
                    return;
                }

            }
            foreach (Enemy en in enemies)
            {
                if (Math.Abs((int)(en.lx) - (int)(player.lx)) < 20 && Math.Abs((int)(en.ly) - (int)(player.ly)) < 36)
                    System.Threading.Thread.Sleep(100);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void player_CreateBullet()
        {
            if (player.canShoot())
            {
                Point xy = player.getShootPlace();
                Bullet b = new Bullet(xy.X, xy.Y);
                SFXplayerShot.Play();
                //buf.Play(0, BufferPlayFlags.Default);
                
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
                String mode = e.getShootMode();
                switch (mode)
                {
                    case "Line":
                        break;
                    case "Ray":
                        /*Vector2D bulletV = e.getVelocity(Convert.ToInt32(player.lx), Convert.ToInt32(player.ly));

                        label1.Text =player.lx.ToString();
                        label2.Text =player.ly.ToString();*/
                        double bulletX = (player.lx - e.lx) / 100;
                        double bulletY = (player.ly - e.ly) / 100;
                        eb.SetV(bulletX,bulletY);
                        break;
                    case "Circle":
                        eb.setMoveMode("Circle");
                        eb.setXY(e.lx, e.ly);
                        break;
                }
            }
        }
        private void create_Enemy()
        {
            Random robj = new Random();
            int x = robj.Next(20, 450);
            Enemy e = new Enemy(x, 0);
            this.panel1.Controls.Add(e.img);
            enemies.Add(e);
        }
        private void create_GunTurret()
        {
            Random robj = new Random();
            int x = robj.Next(20, 450);
            GunTurret e = new GunTurret(x, 0);
            this.panel1.Controls.Add(e.img);
            enemies.Add(e);
        }

        private void create_CircleShootEnemy()
        {
            Random robj = new Random();
            int x = robj.Next(20, 450);
            CircleShootEnemy e = new CircleShootEnemy(x, 0);
            this.panel1.Controls.Add(e.img);
            enemies.Add(e);
        }

        //Player key detect
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
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
                case Keys.X:
                    create_GunTurret();
                    break;
                case Keys.C:
                    create_CircleShootEnemy();
                    break;
            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    player.vy = 0;
                    break;
                case Keys.Down:
                    player.vy = 0;
                    break;
                case Keys.Right:
                    player.vx = 0;
                    break;
                case Keys.Left:
                    player.vx = 0;
                    break;
            }
        }

        private void panel1_MouseClick_1(object sender, MouseEventArgs e)
        {
            if (labelContext.Visible == true)
            {
                if (countContext == 5 || countContext == 15)
                {
                    labelContext.Visible = false;
                    Update.Start();
                    gameTime.Start();
                }
                labelContext.Text = context[countContext];
                countContext++;
            }         
        }
    }

    public class GameObject
    {
        public double lx, ly;//location
        public double vx, vy;//velocity
        public int health;
        public int vxupLimit, vyupLimit;//
        public int vxdownLimit, vydownLimit;//
        public int clock, clockLimit;//for update
        public int move, moveLimit;//for update
        public PictureBox img;

        public GameObject(int x, int y)
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
            Point p = new Point(Convert.ToInt32(lx + 20), Convert.ToInt32(ly));
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
            img.Location = new Point(Convert.ToInt32(lx), Convert.ToInt32(ly));
            img.BackColor = Color.Transparent;
        }

        public void SetV(double x, double y)
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
    public class Vector2D
    {
        public double x;
        public double y;

        public Vector2D()
        {
            x = 0;
            y = 0;
        }

        public Vector2D(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }
    public class Player : GameObject
    {
        public Player(int x, int y)
            : base(x, y)
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
            img.Location = new Point(Convert.ToInt32(lx), Convert.ToInt32(ly));
            img.Image = Image.FromFile(Application.StartupPath + "\\assest\\player.png");
            img.BackColor = Color.Transparent;
            imgAutoSize();
        }

        public void addV(int ax, int ay)
        {
            if (ax != 0)
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
            if (lx + vx > 0 && lx + vx < 500)
                lx += vx;
            if (ly + vy > 0 && ly + vy < 600)
                ly += vy;
            img.Location = img.Location = new Point(Convert.ToInt32(lx), Convert.ToInt32(ly));
            img.BackColor = Color.Transparent;
        }
    }
    public class Bullet : GameObject
    {
        public Bullet(int x, int y)
            : base(x, y)
        {
            lx = x;
            ly = y;
            //f = frame = timer interval of FixUpdate 
            move = 0;
            moveLimit = 0;//每隔 1f 可以移動 p+vx,p+vy.
            vx = 0;
            vy = -5;
            img = new System.Windows.Forms.PictureBox();
            img.Location = img.Location = new Point(Convert.ToInt32(lx), Convert.ToInt32(ly));
            img.Image = Image.FromFile(Application.StartupPath + "\\assest\\Bllet_black.png");
            img.BackColor = Color.Transparent;
            imgAutoSize();
            //img.BackColor = Color.Black;
        }
    }
    public class EnemyBullet : GameObject
    {
        //public double lx, ly;
        public int middleX, middleY;
        public int radius;
        private string MoveMode;
        public double angle;

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
            img.Location = img.Location = new Point(Convert.ToInt32(lx), Convert.ToInt32(ly));
            img.Image = Image.FromFile(Application.StartupPath + "\\assest\\Bullet_black.BMP");
            img.BackColor = Color.Transparent;

            MoveMode = "";
            radius = 100;
            angle = 0;
        }

        public void circleMove()
        {
            middleY++;
            lx = Convert.ToInt32(middleX + radius * Math.Cos(angle += 0.05));
            ly = Convert.ToInt32(middleY + radius * Math.Sin(angle += 0.05));
            img.Location = new Point(Convert.ToInt32(lx), Convert.ToInt32(ly));
            img.BackColor = Color.Transparent;
        }
        
        public void setXY(double ox, double oy)
        {
            middleX = (int)ox + 20;
            middleY = (int)oy + 20;
        }

        public void setMoveMode(string outstr)
        {
            MoveMode = outstr;
        }

        public string getMoveMode()
        {
            return MoveMode;
        }
    }
    public class Enemy : GameObject
    {
        public String Shootmode;

        public int bulletNum;

        public int bulletEachTime;
        public int bulletRestoreLimit;
        public int bulletRestoreClock;

        public Enemy(int x, int y)
            : base(x, y)
        {
            lx = x;
            ly = 0;
            vx = 0;
            vy = 1;
            setClock();
            loadImage();
            Shootmode = "Line";
            health = 10;
        }

        protected void setClock()
        {
            //f = frame = timer interval of FixUpdate
            clock = 0;
            clockLimit = 10;//每隔 20f 發射一顆子彈
            bulletNum = 5;
            bulletEachTime = 5;//每次射擊都會有 5 發子彈
            bulletRestoreClock = 0;
            bulletRestoreLimit = 175;//每隔 bulletRestoreLimit f 進行一次射擊
            move = 0;
            moveLimit = 0;//每隔 1f 可以移動 p+vx,p+vy.
        }

        protected void loadImage()
        {
            img = new System.Windows.Forms.PictureBox();
            img.Location = img.Location = new Point(Convert.ToInt32(lx), Convert.ToInt32(ly));
            img.Image = Image.FromFile(Application.StartupPath + "\\assest\\player.png");
            img.BackColor = Color.Transparent;
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
        protected void adjustTimeInterval()
        {
            bulletRestoreClock++;
            if (bulletRestoreClock > bulletRestoreLimit)
            {
                bulletRestoreClock = 0;
                bulletNum = bulletEachTime;
            }
        }
        public String getShootMode()
        {
            return Shootmode;
        }

        public Vector2D getVelocity(int px, int py)
        {
            Vector2D bulletVelocity = new Vector2D(px, py);
            return bulletVelocity;
        }
    }
    public class GunTurret : Enemy
    {
        public GunTurret(int x, int y)
            : base(x, y)
        {
            lx = x;
            ly = 0;
            vx = 0;
            vy = 1;
            setClock();
            loadImage();
            Shootmode = "Ray";
            health = 10;
        }

        /*public Vector2D getVelocity(int px, int py)
        {
            double bux = (px - Convert.ToInt32(lx))/100;
            double buy = (py - Convert.ToInt32(ly))/100;
            Vector2D bulletVelocity = new Vector2D(bux, buy);
            return bulletVelocity;
        }*/

        private int getGCD(int a, int b)
        {
            int c;
            while (a != 0)
            {
                c = a;
                a = b % a;
                b = c;
            }
            return b;
        }

    }

    public class CircleShootEnemy : Enemy
    {
        public CircleShootEnemy(int x, int y)
            : base(x, y)
        {
            lx = x;
            ly = 0;
            vx = 0;
            vy = 1;
            newsetClock();
            loadImage();
            Shootmode = "Circle";
            health = 10;
        }

        private void newsetClock()
        {
            clock = 0;
            clockLimit = 10;//每隔 20f 發射一顆子彈
            bulletNum = 5;
            bulletEachTime = 5;//每次射擊都會有 5 發子彈
            bulletRestoreClock = 0;
            bulletRestoreLimit = 100000000;//每隔 bulletRestoreLimit f 進行一次射擊
            move = 0;
            moveLimit = 0;//每隔 1f 可以移動 p+vx,p+vy.      
        }
    }
}
