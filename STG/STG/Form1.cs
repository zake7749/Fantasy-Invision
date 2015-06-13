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
            //SetStory();        
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Update.Stop();
            //gameTime.Reset();
            //gameTime.Start();
            //playing music in loop
            SFXBGM = new SoundPlayer("TOS8.wav");
            SFXBGM.PlayLooping();

            //story mode
            labelContext.Text = context[countContext];
            countContext++;
        }

        //Move Object
        private void updatePlayer()
        {
            //insert any change by time on player
            if (player.canMove())
            {
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
                b.Move();
            }
        }
        private void updateEnemyBullet()
        {
            //insert any change by time on enemyBullet
            foreach (EnemyBullet eb in enemyBullet)
            {
                if (eb.getMoveMode() == "Circle")
                    eb.circleMove();
                if (eb.getMoveMode() == "Cos")
                    eb.CosMove();
                if (eb.getMoveMode() == "Sin")
                    eb.SinMove();
                else
                {
                    eb.Move();
                }
            }
        }

        //Bounder check
        private void enemyBounderCheck()
        {
            foreach (Enemy e in enemies)
            {
                if (e.ly < 0)
                {
                    e.img.Dispose();
                    e.clockLimit = 5000000;//Temp method, if you hava any idea,just modify it.
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

            //X += Update.Interval;
            updatePlayer();
            updateEnemy();
            updatePlayerBullet();
            updateEnemyBullet();

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
            if (e.canShoot()&&e.Shootmode!="None")
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
                        Vector2D bulletV = e.getVelocity(player.lx, player.ly);
                        eb.SetV(bulletV.x,bulletV.y);
                        break;
                    case "Circle":
                        eb.setMoveMode("Circle");
                        eb.setXY(e.lx, e.ly);
                        break;
                    case "Cos":
                        eb.setMoveMode("Cos");
                        eb.useGreenRay();
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
        private void create_Enemy(int x,int y)
        {
            Enemy e = new Enemy(x, y);
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
        private void create_GunTurret(int x,int y)
        {
            GunTurret e = new GunTurret(x, y);
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

        private void create_CircleShootEnemy(int x,int y)
        {
            CircleShootEnemy e = new CircleShootEnemy(x, y);
            this.panel1.Controls.Add(e.img);
            enemies.Add(e);
        }
        private void create_Bomber(int x, int y)
        {
            Bomber e = new Bomber(x, y);
            this.panel1.Controls.Add(e.img);
            enemies.Add(e);
        }

        private void create_CosWayEnemy()
        {
            Random robj = new Random();
            int x = robj.Next(20, 450);
            CosWayEnemy e = new CosWayEnemy(x, 0);
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

        private void EnemyCreateFactory(String way)
        {
            int i,j,k;

            Random robj = new Random();
            int[] yDeviation = { 0, -60, -120, -180, -240,-300 };
            int[] VFormation = {-90,-60,-30,0,-30,-60,-90};
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
                    for (i = 2; i < 5; i++)
                    {
                        create_Enemy(x,VFormation[i]);
                        x += FormationRange;
                    }
                    break;

                case "V-formation-Normal":
                    x = robj.Next(75, 425);
                    for (i = 1; i < 6; i++)
                    {
                        create_Bomber(x,VFormation[i]);
                        x += FormationRange;
                    }
                    break;

                case "V-formation-Large":
                    x = robj.Next(75, 425);
                    for (i = 0; i < 7; i++)
                    {
                        create_Bomber(x,VFormation[i]);
                        x += FormationRange;
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
