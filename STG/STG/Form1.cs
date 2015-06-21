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
using System.Diagnostics;
using Microsoft.DirectX;
using Microsoft.DirectX.DirectSound;

namespace STG
{
    public partial class Form1 : Form
    {
        //AxWMPLib.AxWindowsMediaPlayer PlayBulletPlayer;
        List<Enemy> enemies;
        List<EnemyBullet> enemyBullet;
        List<Bullet> playerBullet;
        List<FunctionObject> functionObj;
        Player player;
        Stopwatch gameTime = new Stopwatch();
        //Device dv = new Device();
        //SecondaryBuffer buf;
        string[] context = new string[31];
        int countContext = 0;
        private Double LockLx;//Used for Split-shootmode.
        private Double LockLy;//Used for Split-shootmode.
        private String[] BIGSIZE = { "BlueBlackCircle", "GreenBlackCircle", "RedBlackCircle", "YellowBlackCircle" };
        private String[] STARBOX = { "BlackBigStar", "BlueBigStar", "GreenBigStar", "SkyBigStar", "RedBigStar", "YellowBigStar" };
        private String[] HEARTBOX = { "PinkHeart", "SkyHeart", "GreenHeart",};
        private String[] STATE = { "Line-Left-Side", "Line-Right-Side", "V-formation-Small", "V-formation-Normal", "V-formation-Large", "Bomber-Collision", "None", "Fighter-Two", "Berserker", "Lighter", "None", "Circle","Boss" };
        private Random Randomizer;
        private Double flag;
        int[] EnenyGenerate = {10,10,10,10,10,2,10,10,2,2,2,10,10,4,4,10,10,2,10,7,10,10,10,7,10,10,8,7,10,8,10,10,8,10,10,10,7,7,10,5,10,10,8,10,10,10,0,10,1,10,0,0,10,1,10,1,10,1,1,10,0,10,10,3,10,2,3,10,3,2,10,10,9,10,10,10,9,10,10,9,10,10,8,10,10,9,8,10,10,7,10,10,7,10,10,9,10,10,9,10,10,12};
        int generateOrder = 0;
        //Painter
        Graphics gp;
        Bitmap bp;
        
        //State
        int Life = 10;
        int stateClock = 0;
        int stateClockLimit = 68;
        Boolean StageClear = false;
        //background code
        public PictureBox background1;
        public PictureBox background2;
        public PictureBox background3;
        int b1y;
        int b2y;
        //int b3y;
        int Score;
        int Time;

        SoundPlayer EnterBtn;
        SoundPlayer ClickBtn;
        SoundPlayer Enemydie;
        SoundPlayer GMG;
        SoundPlayer Playerdie;
        SoundPlayer AttackPlayer;
        //SoundPlayer PlayerShoot;
        private bool signal = false;

        public Form1()
        {
            InitializeComponent();

            //SFX
            EnterBtn = new SoundPlayer(Application.StartupPath + @"\SFX\click_touch.wav");
            btnLeave.MouseEnter += btnStoreGrade_MouseEnter;
            ClickBtn = new SoundPlayer(Application.StartupPath + @"\SFX\click_click.wav");
            //PlayerShoot = new SoundPlayer(Application.StartupPath + @"\SFX\se_plst00.wav");
            btnLeave.MouseClick += btnStoreGrade_MouseClick;
            Enemydie = new SoundPlayer(Application.StartupPath + @"\SFX\enemydie.wav");
            GMG = new SoundPlayer(Application.StartupPath + @"\SFX\playerdie.wav");
            Playerdie = new SoundPlayer(Application.StartupPath + @"\SFX\se_pldead00.wav");
            AttackPlayer = new SoundPlayer(Application.StartupPath +  @"\SFX\se_plst00.wav");

            BGMPlayer.URL = @"SFX\GameBackgoundMusic.wav";
            BGMPlayer.settings.setMode("Loop", true);
            BGMPlayer.settings.volume = 70;
            trackBarVolume.Value = BGMPlayer.settings.volume;
            BGMPlayer.Ctlcontrols.play();
            
            Randomizer = new Random();
            playerBullet = new List<Bullet>();
            enemyBullet = new List<EnemyBullet>();
            enemies = new List<Enemy>();
            functionObj = new List<FunctionObject>();
            Score = 0;
            player = new Player(300, 500);

            Time = 0;
            labelTime.Text = Time.ToString(); ;
            labelScore.Text = "0";
            labelContext.Text = "";

            //label2.Text = "";
            //PlayBulletPlayer.Ctlcontrols.play();
            //Story mode
            SetStory();
            gameTime.Reset();
            Update.Stop();
            FunctionObjTimer.Stop();

            //background code            
            background1 = new System.Windows.Forms.PictureBox();
            background1.Location = new Point(0, 290);
            background1.Image = Image.FromFile(Application.StartupPath + "\\assest\\Background\\stage04.png");
            background1.Width = background1.Image.Width;
            background1.Height = background1.Image.Height;

            background2 = new System.Windows.Forms.PictureBox();
            background2.Location = new Point(0, -58);
            background2.Image = Image.FromFile(Application.StartupPath + "\\assest\\Background\\stage04.png"); ;
            background2.Width = background1.Image.Width;
            background2.Height = background1.Image.Height;

            b1y = 0;
            b2y = b1y-640;

            //this.panel1.Controls.Add(background3);
            //background3.Width = background1.Image.Width;
            //background3.Height = background1.Image.Height;
        }

        private void Form1_Load(object sender, EventArgs e)
        {         
            //gameTime.Start(); //這是畫面右下角的計時器
            //story mode
            labelContext.Text = context[countContext];
            countContext++;         
        }

        private void RePaint()
        {

            Bitmap bmpPic1 = new Bitmap(this.Width,this.Height);

            Graphics g = Graphics.FromImage(bmpPic1);
            
            //Draw background
            g.DrawImage(background1.Image, -10, b1y);
            g.DrawImage(background2.Image, -10, b2y);
            //g.DrawImage(background3.Image, -10, b3y);

            //Bullet
            foreach(EnemyBullet eb in enemyBullet)
            {
                g.DrawImage(eb.img.Image, new Point((int)eb.lx, (int)eb.ly)); 
            }
            foreach(Bullet b in playerBullet)
            {
                g.DrawImage(b.img.Image, new Point((int)b.lx, (int)b.ly)); 
            }
            //Enemy
            foreach (Enemy e in enemies)
            {
                g.DrawImage(e.img.Image, new Point((int)e.lx, (int)e.ly));
            }
            //FunctionObject
            foreach(FunctionObject f in functionObj)
            {
                g.DrawImage(f.img.Image, new Point((int)f.lx, (int)f.ly));
            }
            //Player
            g.DrawImage(player.img.Image, new Point((int)player.lx, (int)player.ly));
            pictureBox1.Image = (Image)bmpPic1; 
        }

        private void Generalizer()
        {
            stateClock++;
            if(stateClock > stateClockLimit)
            {
                if (generateOrder < EnenyGenerate.Length)
                    EnemyCreateFactory(STATE[EnenyGenerate[generateOrder++]]);
                else if (StageClear)
                {
                    EnemyCreateFactory(STATE[Randomizer.Next(0,12)]);
                }
                stateClock = 0;
            }
        }

        //background code      
        private void updateBackground()
        {
            b1y+=5;
            b2y+=5;
            //b3y+=5;
            if (b1y >= 640) b1y = -640;
            if (b2y >= 640) b2y = -640;
            //if (b3y >= 638) b3y = -406;
            background1.Location = new Point(-10, b1y);
            background2.Location = new Point(-10, b2y);
            //background3.Location = new Point(-10, b3y);
        }

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
                    e.ChangeImage();
                }
                
                enemy_CreateBullet(e);
            }
        }
        private void updatePlayerBullet()
        {
            //insert any change by time on bullet
            for (var i = 0; i < playerBullet.Count ; i++ )
            {
                playerBullet[i].Move();
                if (playerBullet[i].Explode())
                {
                    playerBullet[i].img.Dispose();
                    playerBullet[i].Dispose();
                    playerBullet.Remove(playerBullet[i]);
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

        //Update FunctionObject
        private void updateFuntionObject()
        {
            foreach(FunctionObject f in functionObj)
            {
                f.Move();
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
        }
        private void bulletBounderCheck()
        {
            //foreach迴圈無法使用List.Remove()，所以改用for迴圈
            for (var i = 0; i < playerBullet.Count;i++ )
            {
                if (playerBullet[i].ly < -200)
                {
                    playerBullet[i].img.Dispose();
                    playerBullet[i].Dispose();
                    playerBullet.Remove(playerBullet[i]);
                }
            }

            for (var i = 0; i < enemyBullet.Count; i++)
            {
                if (enemyBullet[i].ly < -150 || enemyBullet[i].ly > this.Height+150 || enemyBullet[i].lx < -100 || enemyBullet[i].lx > 600)
                {
                    enemyBullet[i].img.Dispose();
                    enemyBullet[i].Dispose();
                    enemyBullet.Remove(enemyBullet[i]);
                }
            }

        }
        private void functionObjCheck()
        {
            for (var i = 0; i < functionObj.Count; i++)
            {

                if (functionObj[i].ly > this.Height + 100)
                {
                    functionObj[i].img.Dispose();
                    functionObj[i].Dispose();
                    functionObj.Remove(functionObj[i]);
                }
            }
        }
        private void BounderCheck()
        {
            enemyBounderCheck();
            bulletBounderCheck();
            functionObjCheck();
        }

        //Update
        private void FixedUpdate(object sender, EventArgs e)
        {

            RePaint();
            Generalizer();
            updatePlayer();
            updateEnemy();
            updatePlayerBullet();
            updateEnemyBullet();
            updateFuntionObject();            
            
            //background code
            updateBackground();

            bulletBounderCheck();
            Collision();

            labelHP.Text = Life.ToString();
            labelScore.Text = Score.ToString();
            labelTime.Text = ((int)gameTime.Elapsed.TotalSeconds + Time).ToString();
            UpdateStory();
            /*
             * Please write comment for each code block added.
             * In addition, DO NOT add anything except FUNCTIONs in FixedUpdate.            
             */
            DebugMessage();
        }

        private void DebugMessage()
        {
            //label1.Text = enemies.Count.ToString();
        }

        //Set story
        private void SetStory()
        {
            context[0] = "聯邦皇帝馬蝗任期將至，聯邦帝國四處民不聊生，各地高舉反馬旗幟...";
            context[1] = "各個聯邦國的王侯都有意出任聯邦皇帝，此時綠菜王國的國王空心菜得到許多聯邦國的支持...";
            context[2] = "(綠菜王國  宮廷)";
            context[3] = "空心菜：諸位國人，現在聯邦境內紛擾不斷，與皇上身邊的亂臣賊子脫不了干係，我將前往天龍國首都清君側！";
            context[4] = "宮廷眾臣：陛下萬歲！臣等願跟隨陛下";
            context[5] = "";
            context[6] = "(綠菜王國、大肚王國與芒果公國的邊境)";
            context[7] = "大肚王：停下，爾等如此行為等同叛亂";
            context[8] = "空心菜：不，我這是在保護皇上";
            context[9] = "老胡：一派胡言，等我將你拿下";
            context[10] = "(雙方進入一陣激戰)";
            context[11] = "芒果公爵：我救駕來遲了！";
            context[12] = "(大肚王就這樣被打敗了)";
            context[13] = "大肚王：我居然就這麼敗了，不甘心...(拔劍自盡)";
            context[14] = "空心菜：公爵，感謝您及時救援";
            context[15] = "芒果公爵：不會，我很支持您的理想，請讓我一同跟隨您";
            context[16] = "";
            context[17] = "(大肚王國與石油王國的邊境)";
            context[18] = "空心菜和芒果公爵在軍隊前方領隊，兩人相談甚歡，此時突然有刺客冒出";
            context[19] = "神秘刺客：大膽叛徒，居然公然反馬，納命來！";
            context[20] = "小兵：保護陛下！(迅速將刺客拿下)";
            context[21] = "空心菜：說，是誰派你來？";
            context[22] = "此時刺客將口中毒藥包咬破，服毒自盡了...";
            context[23] = "芒果公爵：可惜了，不知道是誰";
            context[24] = "空心菜：(拉開刺客的頭套)唉呀！這好眼熟...是薇閣將軍啊，我想是太師小金飛刀派來的吧";
            context[25] = "(天龍國首都)";
            context[26] = "手下：報告太師，你派出的刺客失手，已經服毒自盡了";
            context[27] = "小金飛刀：可惡，痛失一員大將，快找丞相，要他聯合周圍盟國抵抗叛軍";
            context[28] = "手下：是！";
            context[29] = "";
            context[30] = "";
        }
        private void UpdateStory()
        {
            if (int.Parse(labelTime.Text) == 30 || int.Parse(labelTime.Text) == 60)
            {
                Time += ((int)gameTime.Elapsed.TotalSeconds) + 1;
                gameTime.Reset();
                Update.Stop();
                FunctionObjTimer.Stop();
                labelContext.Visible = true;
                labelContext.Text = context[countContext];
            }
        }

        //Collision
        private void Collision()
        {
            foreach(Bullet b in playerBullet)
            {
                for (var i = 0; i < enemies.Count;i++ )
                {
                    if (Math.Abs(b.ly - enemies[i].ly) < enemies[i].img.Height && Math.Abs(b.lx - enemies[i].lx) < enemies[i].img.Width)
                    {
                        enemies[i].health--;
                        if (!enemies[i].isAlive())
                        {
                            
                            if(enemies[i].isCritical)
                            {
                                StageClear = true;
                            }
                            Score += 500;
                            enemies[i].img.Dispose();
                            enemies[i].Dispose();
                            enemies.Remove(enemies[i]);
                            Enemydie.Play();
                        }
                        b.setTimetoExplode(true);
                    }
                }
            }

            for (var i = 0; i < enemyBullet.Count;i++ )
            {
                if (!player.IsOP2((int) gameTime.Elapsed.TotalSeconds))
                {
                    if (player.img.Bounds.IntersectsWith(enemyBullet[i].img.Bounds))
                    {
                        if (player.lx - enemyBullet[i].lx < enemyBullet[i].img.Width - 2*enemyBullet[i].range)
                        {
                            GameOver();
                            //Playerdie.Play();
                            enemyBullet[i].img.Dispose();
                            enemyBullet[i].Dispose();
                            enemyBullet.Remove(enemyBullet[i]);
                        }
                        else
                        {
                            Score += 10000;
                            labelScore.Text = Score.ToString();
                        }
                    }

                    /* is to be modified.
                    else if (Math.Abs((int)(enemyBullet[i].lx) - 18 - (int)(player.lx)) < 18 && Math.Abs((int)(enemyBullet[i].lx) - enemyBullet[i].range - 18 - (int)(player.lx)) > 18
                        && Math.Abs((int)(enemyBullet[i].ly) - 25 - (int)(player.ly)) < 25 && Math.Abs((int)(enemyBullet[i].ly) - enemyBullet[i].range - 25 - (int)(player.ly)) > 25)
                    {
                        //擦彈動作
                        Score += 30;
                        labelScore.Text = Score.ToString();

                        if (Math.Abs((int)(enemyBullet[i].lx) - 18 - (int)(player.lx)) > 18 - enemyBullet[i].range && Math.Abs((int)(enemyBullet[i].ly) - 25 - (int)(player.ly)) > 25 - enemyBullet[i].range)
                        {
                            //擦彈動作
                            Score += 100;
                        }
                        else
                        {
                            GameOver();
                            //Playerdie.Play();
                            enemyBullet[i].img.Dispose();
                            enemyBullet[i].Dispose();
                            enemyBullet.Remove(enemyBullet[i]);
                        }
                    }*/
                }
            }

            foreach (Enemy en in enemies)
            {
                if (!player.IsOP2((int) gameTime.Elapsed.TotalSeconds))
                {
                    if (Math.Abs((int)(en.lx) - (int)(player.lx)) < 20 && Math.Abs((int)(en.ly) - (int)(player.ly)) < 36)
                    {
                        GameOver();
                    }
                }
            }
            for (var i = 0; i < enemies.Count; i++)
            {
                if (player.IsOP2((int)gameTime.Elapsed.TotalSeconds))
                {
                    if (Math.Abs((int)(enemies[i].lx) - (int)(enemies[i].lx)) < 20 && Math.Abs((int)(enemies[i].ly) - (int)(player.ly)) < 36)
                    {
                        enemies[i].health--;
                    }
                }
            }

            //吃到FunctionObj
            for (var i = 0; i < functionObj.Count; i++)
            {
                if (Math.Abs((int)functionObj[i].lx - (int)(player.lx)) < 20 && Math.Abs((int)(functionObj[i].ly) - (int)(player.ly)) < 36)
                {
                    Score += 10;
                    switch (functionObj[i].getObjType())
                    {
                        case 0:
                            Life += functionObj[i].Life;
                            break;
                        case 1:
                            Life += functionObj[i].Life;
                            break;
                        case 2:
                            Life += functionObj[i].Life;
                            break;
                        case 3:
                            player.setOPEndTime((int)gameTime.Elapsed.TotalSeconds, 10);
                            break;
                        case 4:
                            Score += functionObj[i].Score;
                            break;
                        case 5:
                            Score += functionObj[i].Score;
                            break;
                        case 6:
                            Score += functionObj[i].Score;
                            break;
                    }
                    if (Life > 10)
                        Life = 10;
                    functionObj[i].img.Dispose();
                    functionObj[i].Dispose();
                    functionObj.Remove(functionObj[i]);
                }
            }
        }

        private void GameOver()
        {
            if(Life > 0)
            {                
                Life--;
                //player = new Player(300, 550);
                //player.setOPClock(100);
                player.setOPEndTime((int)gameTime.Elapsed.TotalSeconds, 2);
                Playerdie.Play();
                labelHP.Text = Life.ToString();
            }
            if(Life <= 0)
            {
                BGMPlayer.Ctlcontrols.stop();
                AttackPlayer.Stop();
                
                GMG.Play();
                panel3.Visible = true;
                Update.Stop();
                FunctionObjTimer.Stop();
                gameTime.Stop();
                //Playerdie.Play();
                
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
                        ebRay.setImage(STARBOX[Randomizer.Next(0,5)]);
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
                        ebCircle.setImage("BlueBigCircle");
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
                        ebBerserk.setImage(BIGSIZE[Randomizer.Next(0,3)]);
                        ebBerserk.SetV(bulletBV.x, bulletBV.y);
                        if (enemyBullet.LastIndexOf(null) != -1)
                            enemyBullet.Insert(enemyBullet.LastIndexOf(null), ebBerserk);
                        else
                            enemyBullet.Add(ebBerserk);
                        //this.panel1.Controls.Add(ebBerserk.img);
                        break;
                    case "Round":
                        double rx = 0, ry = 0;
                        while(rx < Math.PI*2)
                        {
                            rx += Math.PI/10;
                            ry += Math.PI/10;
                            EnemyBullet ebRound = new EnemyBullet(xy.X+6,xy.Y+20);
                            ebRound.setImage("SkySignleCircle");
                            ebRound.SetV(Math.Sin(rx)*3,Math.Cos(ry)*3);
                            if (enemyBullet.LastIndexOf(null) != -1)
                                enemyBullet.Insert(enemyBullet.LastIndexOf(null), ebRound);
                            else
                                enemyBullet.Add(ebRound);
                        }
                            break;

                    case "Boss-Round":
                            flag = (flag == 5) ? 7 : 5;
                            double rlx = 0, rly = 0;
                            while (rlx < Math.PI * 2)
                            {
                                rlx += Math.PI / flag;
                                rly += Math.PI / flag;
                                EnemyBullet ebRound = new EnemyBullet(xy.X + 6, xy.Y + 20);
                                ebRound.setImage("SkyBigCircle");
                                ebRound.SetV(Math.Sin(rlx) * 3, Math.Cos(rly) * 3);
                                if (enemyBullet.LastIndexOf(null) != -1)
                                    enemyBullet.Insert(enemyBullet.LastIndexOf(null), ebRound);
                                else
                                    enemyBullet.Add(ebRound);
                            }

                            break;
                    case "Boss-Cross":
                            int iCross = 0;
                            for (iCross = 0; iCross < 5; iCross++)
                            {
                                EnemyBullet ebCross;
                                int choice = Randomizer.Next(0, 2);
                                if (choice == 0)
                                {
                                    ebCross = new EnemyBullet(Randomizer.Next(0, 620), Randomizer.Next(600, 700));
                                    ebCross.SetV(0, -2.5);
                                }
                                else
                                {
                                    ebCross = new EnemyBullet(Randomizer.Next(0, 620), Randomizer.Next(-100, 0));
                                    ebCross.SetV(0, 2.5);
                                }
                                ebCross.setImage(HEARTBOX[Randomizer.Next(0, 3)]);

                                if (ebCross.vy > 0)
                                    ebCross.img.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);

                                if (enemyBullet.LastIndexOf(null) != -1)
                                    enemyBullet.Insert(enemyBullet.LastIndexOf(null), ebCross);
                                else
                                    enemyBullet.Add(ebCross);
                            }
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
            //label7.Text = interval.ToString();
            for(i=0;i<scale;i++)
            {
                EnemyBullet eb = new EnemyBullet(xy.X+20,xy.Y+10);

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

        private Boss create_Boss(int x, int y)
        {
            Boss e = new Boss(x, y);
            //this.panel1.Controls.Add(e.img);
            if (enemies.LastIndexOf(null) != -1)
                enemies.Insert(enemies.LastIndexOf(null), e);
            else
                enemies.Add(e);
            return e;
        }

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

        private Lighter create_Lighter(int x, int y)
        {
            Lighter e = new Lighter(x, y);
            //this.panel1.Controls.Add(e.img);
            if (enemies.LastIndexOf(null) != -1)
                enemies.Insert(enemies.LastIndexOf(null), e);
            else
                enemies.Add(e);
            return e;
        }

        //Create FunctionObject
        private void create_FunctionObject()
        {
            Random robj = new Random();
            int x = robj.Next(10, 585);
            FunctionObject fo = new FunctionObject(x, -25);
            if (functionObj.LastIndexOf(null) != -1)
                functionObj.Insert(functionObj.LastIndexOf(null), fo);
            else
                functionObj.Add(fo);
        }

        //Player key detect
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            switch (e.KeyCode)
            {
                case Keys.Up:
                    player.setSLR(0);
                    player.addV(0, -5);
                    break;
                case Keys.Down:
                    player.setSLR(0);
                    player.addV(0, 5);
                    break;
                case Keys.Right:
                    player.setSLR(2);
                    player.addV(5, 0);
                    break;
                case Keys.Left:
                    player.setSLR(1);
                    player.addV(-5, 0);
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
                case Keys.G:
                    create_Lighter(10, 10);
                    break;
                case Keys.H:
                    create_Boss(100, 10);
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
                    x = robj.Next(25, 300);
                    for (i = 0; i < 3; i++)
                    {
                        create_GunTurret(x, yDeviation[i]);
                    }
                    break;

                case "Line-Right-Side":
                    x = robj.Next(300, 650);
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
                case "Boss":
                    create_Boss(300, 20);
                    break;
                case "Fighter-Two":
                    create_Fighter(Randomizer.Next(1,250),-10);
                    create_Fighter(Randomizer.Next(350,600), -10);
                    break;
                case "Berserker":
                    create_Berserker(Randomizer.Next(1,650),-10);
                    break;
                case "Lighter":
                    create_Lighter(Randomizer.Next(150, 550), -15);
                    break;
                case "Circle":
                    create_CircleShootEnemy(Randomizer.Next(0, 200),-5);
                    create_CircleShootEnemy(Randomizer.Next(200, 400),-5);
                    create_CircleShootEnemy(Randomizer.Next(400, 650),-5);
                    break;
                case "None":
                    break;
            }
        }

        private void btnStoreGrade_Click(object sender, EventArgs e)
        {
            Form4 AddGrade = new Form4(int.Parse(labelScore.Text), int.Parse(labelTime.Text));
            AddGrade.Show();
            btnStoreGrade.Enabled = false;
        }

        private void btnLeave_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnStoreGrade_MouseEnter(object sender, EventArgs e)
        {
            EnterBtn.Play();
        }

        private void btnStoreGrade_MouseClick(object sender, MouseEventArgs e)
        {
            ClickBtn.Play();
        }

        private void trackBarVolume_Scroll(object sender, EventArgs e)
        {
            BGMPlayer.settings.volume = trackBarVolume.Value;
        }

        private void FunctionObjTimer_Tick(object sender, EventArgs e) //For create FunctionObject (每3秒產生1個) 
        {
            create_FunctionObject();
        }

        private void labelContext_MouseClick(object sender, MouseEventArgs e) //Story mode
        {
            if (labelContext.Visible == true)
            {
                if (countContext == 5 || countContext == 16 || countContext ==29)
                {
                    labelContext.Visible = false;
                    Update.Start();
                    FunctionObjTimer.Start();
                    gameTime.Start();
                }
                labelContext.Text = context[countContext];
                countContext++;
            } 
        }

    }
}
