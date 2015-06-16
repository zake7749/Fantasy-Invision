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
        private Double LockLx;//Used for Split-shootmode.
        private Double LockLy;//Used for Split-shootmode.
   
        //Painter
        Graphics gp;
        Bitmap bp;
        
        //background code
        public PictureBox background1;
        public PictureBox background2;
        public PictureBox background3;
        int b1y;
        int b2y;
        int b3y;

        SoundPlayer SFXBGM;


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

            PlayBulletPlayer.URL = @"SFX\DAMAGE.WAV";
            //dv.SetCooperativeLevel(STG.Form1.ActiveForm, CooperativeLevel.Priority);
            //buf = new SecondaryBuffer(@"\SFX\DAMAGE.WAV", dv);
            //SetStory();    
            
            //background code
            /*
            background1 = new System.Windows.Forms.PictureBox();
            background1.Location = new Point(0, 290);
            background1.Image = Image.FromFile(Application.StartupPath + "\\assest\\background.jpg");
            this.panel1.Controls.Add(background1);
            background1.Width = background1.Image.Width;
            background1.Height = background1.Image.Height;

            background2 = new System.Windows.Forms.PictureBox();
            background2.Location = new Point(0, -58);
            background2.Image = Image.FromFile(Application.StartupPath + "\\assest\\background.jpg");
            this.panel1.Controls.Add(background2);
            background2.Width = background1.Image.Width;
            background2.Height = background1.Image.Height;

            background3 = new System.Windows.Forms.PictureBox();
            background2.Location = new Point(0, -406);
            background3.Image = Image.FromFile(Application.StartupPath + "\\assest\\background.jpg");
            this.panel1.Controls.Add(background3);
            background3.Width = background1.Image.Width;
            background3.Height = background1.Image.Height;

            b1y = 290;
            b2y = -58;
            b3y = -406;
            */

            //background code
        }

        private void RePaint()
        {
            Bitmap bmpPic1 = new Bitmap(this.Width,this.Height);
            Graphics g = Graphics.FromImage(bmpPic1); 
            foreach(EnemyBullet eb in enemyBullet)
            {
                g.DrawImage(eb.img.Image, new Point((int)eb.lx, (int)eb.ly)); 
            }
            foreach(Bullet b in playerBullet)
            {
                g.DrawImage(b.img.Image, new Point((int)b.lx, (int)b.ly)); 
            }
            foreach (Enemy e in enemies)
            {
                g.DrawImage(e.img.Image, new Point((int)e.lx, (int)e.ly));
            }
            g.DrawImage(player.img.Image, new Point((int)player.lx, (int)player.ly));
            pictureBox1.Image = (Image)bmpPic1; 
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            //Update.Stop();
            //gameTime.Reset();
            //gameTime.Start();
            //playing music in loop

            //BGMPlayer.URL = @"TOS8.wav";
            //BGMPlayer.settings.setMode("loop", true);
            //BGMPlayer.settings.autoStart = true;
            //story mode
            labelContext.Text = context[countContext];
            countContext++;
        }
        
        
        //background code
        /*
        private void updateBackground()
        {
            b1y++;
            b2y++;
            b3y++;
            if (b1y >= 638) b1y = -406;
            if (b2y >= 638) b2y = -406;
            if (b3y >= 638) b3y = -406;
            background1.Location = new Point(0, b1y);
            background2.Location = new Point(0, b2y);
            background3.Location = new Point(0, b3y);
        }*/
        //background code

        //Update Object
        private void updatePlayer()
        {
            //insert any change by time on player
            if (player.canMove())
            {
                player.ChangeImage();
                player_CreateBullet();
                player.Move();
            }
        }
        private void updateEnemy()
        {
            //insert any change by time on enemy
            foreach (Enemy e in enemies)
            {
                if (e.canMove())
                {
                    e.Move();
                }
                enemy_CreateBullet(e);
            }
        }
        private void updatePlayerBullet()
        {
            //insert any change by time on bullet
            foreach (Bullet b in playerBullet)
            {
                if (b.Explode())
                {
                    
                }
                else
                {
                    b.Move();
                }
            }
        }
        private void updateEnemyBullet()
        {
            //insert any change by time on enemyBullet
            foreach (EnemyBullet eb in enemyBullet)
            {
                eb.Move();
            }
        }

        //Bounder check
        private void enemyBounderCheck()
        {
            //foreach迴圈無法使用List.Remove()，所以改用for迴圈
            for (var i = 0; i < enemies.Count;i++ )
            {
               
                if (enemies[i].ly > this.Height)
                {
                    enemies[i].img.Dispose();
                    enemies[i].Dispose();
                    enemies.Remove(enemies[i]);
                }
            }
/*                foreach (Enemy e in enemies)
                {
                    if (e.ly < 0)
                    {
                        e.img.Dispose();
                        e.clockLimit = 5000000;//Temp method, if you hava any idea,just modify it.
                        e.Dispose();
                        enemies.Remove(e);
                    }
                    if (e.ly > 635)
                    {
                        e.img.Dispose();
                        e.Dispose();
                        enemies.Remove(e);
                    }
                }*/
        }
        private void bulletBounderCheck()
        {
            //foreach迴圈無法使用List.Remove()，所以改用for迴圈
            for (var i = 0; i < playerBullet.Count;i++ )
            {
                if (playerBullet[i].ly < 0)
                {
                    playerBullet[i].img.Dispose();
                    playerBullet[i].Dispose();
                    playerBullet.Remove(playerBullet[i]);
                }
            }
/*               foreach (Bullet b in playerBullet)
                {
                    if (b.ly < 0)
                    {
                        b.img.Dispose();
                        b.Dispose();
                        playerBullet.Remove(b);
                    }
                }*/
            for (var i = 0; i < enemyBullet.Count; i++)
            {
                if (enemyBullet[i].ly < -150 || enemyBullet[i].ly > this.Height+150 || enemyBullet[i].lx < -100 || enemyBullet[i].lx > 600)
                {
                    enemyBullet[i].img.Dispose();
                    enemyBullet[i].Dispose();
                    enemyBullet.Remove(enemyBullet[i]);
                }
            }
/*            foreach (EnemyBullet b in enemyBullet)
            {
                if (b.ly > 635)
                {
                    b.img.Dispose();
                    enemyBullet.Remove(b);
                    b.Dispose();
                    
                }
            }*/
        }
        private void BounderCheck()
        {
            enemyBounderCheck();
            bulletBounderCheck();
        }

        //Update
        private void FixedUpdate(object sender, EventArgs e)
        {
            
            //degree++;
            //angle = Math.PI * degree / 180.0;
            RePaint();
            //X += Update.Interval;
            updatePlayer();
            updateEnemy();
            updatePlayerBullet();
            updateEnemyBullet();
            
            //background code
            //updateBackground();

            bulletBounderCheck();
            Collision();
            //circle.circleMove(200.0, 200.0, angle);


            /*
             * Please write comment for each code block added.
             * In addition, DO NOT add anything except FUNCTIONs in FixedUpdate.
             * 
             * labelTime.Text = ((int)gameTime.Elapsed.TotalSeconds).ToString();
            if ((int)(gameTime.Elapsed.TotalSeconds) == 40)
            {
                if ((gameTime.Elapsed.TotalSeconds) == 41.0000000)
 
                    gameTime.Stop();

                Update.Stop();
                countContext = 6;
                labelContext.Visible = true;
                labelContext.Text = context[countContext];
            }*/
            DebugMessage();

        }

        private void DebugMessage()
        {
            label1.Text = enemies.Count.ToString();
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

            foreach(Bullet b in playerBullet)
            {
                foreach(Enemy en in enemies)
                {
                    if(Math.Abs(b.ly - en.ly)<en.img.Height&&Math.Abs(b.lx-en.lx)<en.img.Width)
                    {
                        en.health--;
                        b.setTimetoExplode(true);
                    }
                }
            }

            foreach (EnemyBullet eb in enemyBullet)
            {
                if (Math.Abs((int)(eb.lx) - 18 - (int)(player.lx)) < 18 && Math.Abs((int)(eb.ly) - 25 - (int)(player.ly)) < 25)
                {
                    //System.Threading.Thread.Sleep(100);
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
                Bullet b1 = new Bullet((int)player.lx-5, (int)player.ly-20);
                Bullet b2 = new Bullet((int)player.lx+22, (int)player.ly-20);
                //PlayBulletPlayer.Ctlcontrols.play();
                //SFXplayerShot.Play();
                //buf.Play(1, BufferPlayFlags.Default);
                
                //this.panel1.Controls.Add(b.img);
                playerBullet.Add(b1);
                playerBullet.Add(b2);
            }
        }

        //Enemy
        private void enemy_CreateBullet(Enemy e)
        {
            if (e.canShoot()&&e.Shootmode!="None")
            {
                String mode = e.getShootMode();
                Point xy = e.getShootPlace();
                switch (mode)
                {
                    case "Line":
                        EnemyBullet ebLine = new EnemyBullet(xy.X, xy.Y);
                        if(enemyBullet.LastIndexOf(null) != -1)
                            enemyBullet.Insert(enemyBullet.LastIndexOf(null), ebLine);
                        else
                            enemyBullet.Add(ebLine);
                        //this.panel1.Controls.Add(ebLine.img);
                        break;
                    case "Ray":
                        EnemyBullet ebRay = new EnemyBullet(xy.X, xy.Y);
                        Vector2D bulletV = e.getVelocity(player.lx, player.ly);
                        ebRay.SetV(bulletV.x,bulletV.y);
                        if (enemyBullet.LastIndexOf(null) != -1)
                            enemyBullet.Insert(enemyBullet.LastIndexOf(null), ebRay);
                        else
                            enemyBullet.Add(ebRay);
                        //this.panel1.Controls.Add(ebRay.img);
                        break;
                    case "Circle":
                        EnemyBullet ebCircle = new EnemyBullet(xy.X, xy.Y);
                        ebCircle.setMoveMode("Circle");
                        ebCircle.setXY(e.lx, e.ly);
                        if (enemyBullet.LastIndexOf(null) != -1)
                        
                            enemyBullet.Insert(enemyBullet.LastIndexOf(null), ebCircle);
                        else
                            enemyBullet.Add(ebCircle);
                        //this.panel1.Controls.Add(ebCircle.img);
                        break;
                    case "Cos":
                        EnemyBullet ebCosine = new EnemyBullet(xy.X,xy.Y);
                        ebCosine.setMoveMode("Cos");
                        ebCosine.useGreenRay();
                        if (enemyBullet.LastIndexOf(null) != -1)
                            enemyBullet.Insert(enemyBullet.LastIndexOf(null), ebCosine);
                        else 
                            enemyBullet.Add(ebCosine);
                        //this.panel1.Controls.Add(ebCosine.img);
                        break;
                    case "Split-3":
                        createBulletCurtain(e, 5);
                        break;
                    case "Split-5":
                        createBulletCurtain(e, 5);
                        break;
                    case "Berserk":
                        EnemyBullet ebBerserk = new EnemyBullet(xy.X, xy.Y);
                        Vector2D bulletBV = e.getVelocity(player.lx, player.ly);
                        ebBerserk.SetV(bulletBV.x, bulletBV.y);
                        if (enemyBullet.LastIndexOf(null) != -1)
                            enemyBullet.Insert(enemyBullet.LastIndexOf(null), ebBerserk);
                        else
                            enemyBullet.Add(ebBerserk);
                        //this.panel1.Controls.Add(ebBerserk.img);
                        break;
                }
            }
        }
        private void createBulletCurtain(Enemy e,int scale)
        {
            Point xy = e.getShootPlace();
            int velocity = 5,i = 0;
            double interval = 100/(scale-1);
            double start = -1*(scale-1)/2*interval;
            double vX = 0, vY = 0;
            double normal = 0;
            label7.Text = interval.ToString();
            for(i=0;i<scale;i++)
            {
                EnemyBullet eb = new EnemyBullet(xy.X,xy.Y);

                normal = Normalizer(start,100);

                vX = start / normal;
                vY = 100 / normal;
                vX *= velocity;
                vY *= velocity;
                eb.SetV(vX, vY);
                start += interval;
                if (enemyBullet.LastIndexOf(null) != -1)
                    enemyBullet.Insert(enemyBullet.LastIndexOf(null), eb);
                else 
                    enemyBullet.Add(eb);

                //this.panel1.Controls.Add(eb.img);
            }

        }
        private double Normalizer(double x,double y)
        {
            return Math.Sqrt(Math.Pow(x,2) + Math.Pow(y,2));
        }

        //Create enemy object
        private void create_Enemy()
        {
            Random robj = new Random();
            int x = robj.Next(20, 450);
            Enemy e = new Enemy(x, 0);
            //this.panel1.Controls.Add(e.img);
            if (enemies.LastIndexOf(null) != -1)
                enemies.Insert(enemies.LastIndexOf(null), e);
            else
                enemies.Add(e);
        }
        private Enemy create_Enemy(int x, int y)
        {
            Enemy e = new Enemy(x, y);
            //this.panel1.Controls.Add(e.img);
            if (enemies.LastIndexOf(null) != -1)
                enemies.Insert(enemies.LastIndexOf(null), e);
            else
                enemies.Add(e);
            return e;
        }
        private void create_GunTurret()
        {
            Random robj = new Random();
            int x = robj.Next(20, 450);
            GunTurret e = new GunTurret(x, 0);
            //this.panel1.Controls.Add(e.img);
            if (enemies.LastIndexOf(null) != -1)
                enemies.Insert(enemies.LastIndexOf(null), e);
            else
                enemies.Add(e);
        }
        private GunTurret create_GunTurret(int x, int y)
        {
            GunTurret e = new GunTurret(x, y);
            Random robj = new Random();
            e.SetV(0, robj.NextDouble() * 2);
            //this.panel1.Controls.Add(e.img);
            if (enemies.LastIndexOf(null) != -1)
                enemies.Insert(enemies.LastIndexOf(null), e);
            else
                enemies.Add(e);
            return e;
        }
        private void create_CircleShootEnemy()
        {
            Random robj = new Random();
            int x = robj.Next(20, 450);
            CircleShootEnemy e = new CircleShootEnemy(x, 0);
            //this.panel1.Controls.Add(e.img);
            if (enemies.LastIndexOf(null) != -1)
                enemies.Insert(enemies.LastIndexOf(null), e);
            else
                enemies.Add(e);
        }
        private CircleShootEnemy create_CircleShootEnemy(int x, int y)
        {
            CircleShootEnemy e = new CircleShootEnemy(x, y);
            //this.panel1.Controls.Add(e.img);
            if (enemies.LastIndexOf(null) != -1)
                enemies.Insert(enemies.LastIndexOf(null), e);
            else
                enemies.Add(e);
            return e;
        }
        private Bomber create_Bomber(int x, int y,Boolean ShiftMode)
        {
            Bomber e = new Bomber(x, y);
            if (ShiftMode)
                e.OpenShiftMode();
            //this.panel1.Controls.Add(e.img);
            if (enemies.LastIndexOf(null) != -1)
                enemies.Insert(enemies.LastIndexOf(null), e);
            else
                enemies.Add(e);
            return e;
        }
        private Fighter create_Fighter(int x, int y)
        {
            Fighter e = new Fighter(x, y);
            //this.panel1.Controls.Add(e.img);
            if (enemies.LastIndexOf(null) != -1)
                enemies.Insert(enemies.LastIndexOf(null), e);
            else
                enemies.Add(e);
            return e;
        }
        private CosWayEnemy create_CosWayEnemy()
        {
            Random robj = new Random();
            int x = robj.Next(20, 450);
            CosWayEnemy e = new CosWayEnemy(x, 0);
            //this.panel1.Controls.Add(e.img);
            if (enemies.LastIndexOf(null) != -1)
                enemies.Insert(enemies.LastIndexOf(null), e);
            else
                enemies.Add(e);
            return e;
        }
        private Berserker create_Berserker(int x, int y)
        {
            Berserker e = new Berserker(x, y);
            //this.panel1.Controls.Add(e.img);
            if (enemies.LastIndexOf(null) != -1)
                enemies.Insert(enemies.LastIndexOf(null), e);
            else
                enemies.Add(e);
            return e;
        }

        //Player key detect
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            switch (e.KeyCode)
            {
                case Keys.Up:
                    player.setSLR(0);
                    player.addV(0, -4);
                    break;
                case Keys.Down:
                    player.setSLR(0);
                    player.addV(0, 4);
                    break;
                case Keys.Right:
                    player.setSLR(2);
                    player.addV(4, 0);
                    break;
                case Keys.Left:
                    player.setSLR(1);
                    player.addV(-4, 0);
                    break;
                case Keys.Space:
                    player_CreateBullet();
                    break;
                case Keys.Z:
                    EnemyCreateFactory("Line-Left-Side");
                    break;
                case Keys.X:
                    EnemyCreateFactory("Line-Right-Side");
                    break;
                case Keys.C:
                    EnemyCreateFactory("V-formation-Small");
                    break;
                case Keys.V:
                    EnemyCreateFactory("V-formation-Small");
                    break;
                case Keys.B:
                    EnemyCreateFactory("V-formation-Normal");
                    break;
                case Keys.N:
                    EnemyCreateFactory("V-formation-Large");
                    break;
                case Keys.M:
                    EnemyCreateFactory("Simple");
                    break;
                case Keys.A:
                    create_CosWayEnemy();
                    break;
                case Keys.S:
                    create_Fighter(10,10);
                    break;
                case Keys.D:
                    create_Berserker(10, 10);
                    break;
                case Keys.F:
                    EnemyCreateFactory("Bomber-Collision");
                    break;
            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            player.setSLR(0);
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


        private void EnemyCreateFactory(String way)
        {
            int i,j,k;

            Random robj = new Random();
            int[] yDeviation = { 0, -60, -120, -180, -240,-300 };
            int[] VFormation = {-120,-90,-60,-30,0,-30,-60,-90,-120};
            int FormationRange = 40;
            int x;
            switch(way)
            {
                case "Line-Left-Side" :
                    x = robj.Next(25, 100);
                    for (i = 0; i < 3; i++)
                    {
                        create_GunTurret(x, yDeviation[i]);
                    }
                    break;

                case "Line-Right-Side":
                    x = robj.Next(400, 475);
                    for (i = 0; i < 3; i++)
                    {
                        create_GunTurret(x,yDeviation[i]);
                    }
                    break;

                case "V-formation-Small":
                    x = robj.Next(75, 425);
                    for (i = 3; i < 6; i++)
                    {
                        Enemy e;
                        e = create_Enemy(x,VFormation[i]);
                        e.Shootmode = "Split-3";
                        x += FormationRange;
                    }
                    break;

                case "V-formation-Normal":
                    x = robj.Next(75, 125);
                    for (i = 1; i < 8; i++)
                    {
                        create_Bomber(x,VFormation[i],false);
                        x += FormationRange;
                    }
                    break;

                case "V-formation-Large":
                    x = robj.Next(75, 100);
                    for (i = 0; i < 9; i++)
                    {
                        create_Bomber(x,VFormation[i],false);
                        x += FormationRange;
                    }
                    break;

                case "Bomber-Collision":
                    int divX = 50;
                    int divY = -30;
                    for (i = 1; i < 12; i++)
                    {
                        create_Bomber(i * divX,i * divY,true);
                    }
                    break;

                case "Simple":
                    x = robj.Next(25, 475);
                    create_CircleShootEnemy(x,0);
                    break;
            }
        }

    }
}
