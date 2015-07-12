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

namespace STG
{
    public partial class Form1 : Form
    {
        List<Enemy> enemies;
        List<EnemyBullet> enemyBullet;
        List<Bullet> playerBullet;
        List<FunctionObject> functionObj;
        Player player;
        EnemyFactory enemyFactory;
        Stopwatch gameTime = new Stopwatch();

        string[] context = new string[35];
        int countContext = 0;
        private Boolean RecFlipX, RecFlipY;
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
        int initalLife;
        int Life;
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
        Boolean IsBossDead = false;

        SoundPlayer EnterBtn;
        SoundPlayer ClickBtn;
        SoundPlayer Enemydie;
        SoundPlayer GMG;
        SoundPlayer Playerdie;
        SoundPlayer AttackPlayer;
        SoundPlayer GetObj;

        public Form1(int inputLife)
        {
            InitializeComponent();

            //SFX
            EnterBtn = new SoundPlayer(Application.StartupPath + @"\SFX\click_touch.wav");
            btnLeave.MouseEnter += btnStoreGrade_MouseEnter;
            ClickBtn = new SoundPlayer(Application.StartupPath + @"\SFX\click_click.wav");
            btnLeave.MouseClick += btnStoreGrade_MouseClick;
            Enemydie = new SoundPlayer(Application.StartupPath + @"\SFX\enemydie.wav");
            GMG = new SoundPlayer(Application.StartupPath + @"\SFX\playerdie.wav");
            Playerdie = new SoundPlayer(Application.StartupPath + @"\SFX\se_pldead00.wav");
            AttackPlayer = new SoundPlayer(Application.StartupPath +  @"\SFX\se_plst00.wav");
            GetObj = new SoundPlayer(Application.StartupPath + @"\SFX\GetThing.wav");

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
            enemyFactory = new EnemyFactory(enemies);
            Score = 0;
            player = new Player(300, 500);

            initalLife = inputLife;
            Life = initalLife;
            Time = 0;
            labelTime.Text = Time.ToString(); ;
            labelScore.Text = "0";
            labelHP.Text = initalLife.ToString();
            labelContext.Text = "";

            //Story mode
            SetStory();
            gameTime.Reset();
            Update.Stop();
            FunctionObjTimer.Stop();

            //background code            
            background1 = new System.Windows.Forms.PictureBox();
            background1.Location = new Point(0, 0);
            background1.Image = Image.FromFile(Application.StartupPath + "\\assest\\Background\\stage04.png");
            background1.Width = background1.Image.Width;
            background1.Height = background1.Image.Height;

            background2 = new System.Windows.Forms.PictureBox();
            background2.Location = new Point(0, -580);
            background2.Image = Image.FromFile(Application.StartupPath + "\\assest\\Background\\stage04.png"); ;
            background2.Width = background2.Image.Width;
            background2.Height = background2.Image.Height;
            b1y = 0;
            b2y = -580;

        }

        private void Form1_Load(object sender, EventArgs e)
        {         
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
                if (generateOrder < EnenyGenerate.Length){
                    enemyFactory.createEnemy(STATE[EnenyGenerate[generateOrder++]]);                        
                }                   
                else if (StageClear)
                {
                    generateOrder = 0;
                    StageClear = false;
                    enemyFactory.createEnemy(STATE[Randomizer.Next(0, 12)]);                  
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
            if (b1y > 580) b1y = -580;
            if (b2y > 580) b2y = -580;
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
        private void updatePlayerAttack()
        {
            if (player.IsAttackEnhancing(int.Parse(labelTime.Text)) == false)
            {
                player.setAttack();
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
                if (enemyBullet[i].ly < -150 || enemyBullet[i].ly > this.Height+150 || enemyBullet[i].lx < -100 || enemyBullet[i].lx > 700)
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
            updatePlayerAttack();
            updatePlayerCondtionInLabel();
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

        }

        private void updatePlayerCondtionInLabel()
        {
            if(player.IsOP2(int.Parse(labelTime.Text)) == true){
                labelOP.ForeColor = Color.Gold;
                labelOP.Text = "Yes"; 
            }
            else if (player.IsOP2(int.Parse(labelTime.Text)) == false)
            {
                labelOP.ForeColor = Color.White;
                labelOP.Text = "No";
            }

            if (player.getAttack() == 50)
            {
                labelAttack.ForeColor = Color.Gold;
            }
            else if (player.getAttack() == 20)
            {
                labelAttack.ForeColor = Color.Red;
            }
            else if (player.getAttack() == 5)
            {
                labelAttack.ForeColor = Color.Aqua;
            }
            else
            {
                labelAttack.ForeColor = Color.White;
            }
            labelAttack.Text = player.getAttack().ToString();
        }
        private void DebugMessage()
        {
            //label1.Text = enemies.Count.ToString();
        }

        //Set story
        private void SetStory()
        {
            context[0] = "闇黑皇后：小美，我就知道你會來救你爺爺，不過我是不會輕易放人，想救出爺爺，就必須要先打敗我，哈哈哈";
            context[1] = "小美：可惡的皇后，為什麼要綁架我爺爺？我一定要救出爺爺！衝啊";
            context[2] = "闇黑皇后：眾妖精聽令，務必擊敗小美";
            context[3] = "妖精們：遵命";
            context[4] = "";
            context[5] = "(在城堡中庭)";
            context[6] = "小美：好累，敵人好多，咦？這是什麼？(小美撿到一個晶瑩剔透水的晶球)";
            context[7] = "(水晶球中) 爺爺：小美，不要來救爺爺，快逃啊！";
            context[8] = "小美：為什麼？爺爺，我一定會救你出去";
            context[9] = "爺爺：小美...不...(影像消失)";
            context[10] = "闇黑皇后：不要叫了，沒用的";
            context[11] = "小美：哼，你到底爺爺做了什麼？";
            context[12] = "闇黑皇后：沒什麼，我還對你爺爺很好呢，哈哈哈";
            context[13] = "小美：最好是，快放了我爺爺";
            context[14] = "闇黑皇后：不可能，除非你能打敗我，不過你還得先找到我";
            context[15] = "闇黑皇后：妖精們，上！";
            context[16] = "";
            context[17] = "(城堡某處走廊)";
            context[18] = "小美：天啊，怎麼這麼多妖精...根本打不完";
            context[19] = "...：小美，小美...";
            context[21] = "小美：是誰？";
            context[22] = "...：小美，我不是敵人，我在你右前方的畫室裡";
            context[23] = "小美：我怎麼知道你不是敵人";
            context[24] = "...：我是被皇后困在城堡裡的幽魂，我叫可可，我想脫離皇后的魔掌";
            context[25] = "小美：好的，等我";
            context[26] = "可可：小美，這個城堡裡有很多被闇黑皇后抓來的人，即使死了他的靈魂依舊會被困在城堡裡";
            context[27] = "可可：我已經死了100年了，我好希望我能夠脫離苦海";
            context[28] = "小美：那你知道我爺爺被關在哪裡嗎？";
            context[29] = "可可：這我就不清楚了，每一層樓的房間都有可能，或者地下的牢房也有可能";
            context[30] = "小美：那我該怎麼辦？";
            context[31] = "可可：我有一份地圖，這是這座城堡的地圖，或許對你有用，希望你能打敗皇后";
            context[32] = "小美：謝謝你！";
            context[33] = "";

        }
        private void UpdateStory()
        {
            if (IsBossDead && countContext < context.Length - 1)//出現於BOSS之前
            {
                IsBossDead = false;
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
                        enemies[i].health -= player.getAttack();
                        if (!enemies[i].isAlive())
                        {
                            
                            if(enemies[i].isCritical)
                            {
                                StageClear = true;
                            }                           
                            Score += 500;
                            enemies[i].img.Dispose();
                            Enemydie.Play();
                            if (enemies[i].IsBossEnemy())
                            {
                                IsBossDead = true;
                            }
                            enemies[i].Dispose();
                            enemies.Remove(enemies[i]);
                        }
                        b.setTimetoExplode(true);
                    }
                }
            }

            for (var i = 0; i < enemyBullet.Count;i++ )
            {
                if (!player.IsOP2(int.Parse(labelTime.Text)))
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
                if (!player.IsOP2(int.Parse(labelTime.Text)))
                {
                    if (Math.Abs((int)(en.lx) - (int)(player.lx)) < 20 && Math.Abs((int)(en.ly) - (int)(player.ly)) < 36)
                    {
                        GameOver();
                    }
                }
            }
            for (var i = 0; i < enemies.Count; i++)
            {
                if (player.IsOP2(int.Parse(labelTime.Text)))
                {
                    if (Math.Abs((int)(enemies[i].lx) - (int)(player.lx)) < 20 && Math.Abs((int)(enemies[i].ly) - (int)(player.ly)) < 36)
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
                    GetObj.Play();
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
                            player.setOPEndTime(int.Parse(labelTime.Text), 15);
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
                        case 7:
                            player.setAttackEnhance(int.Parse(labelTime.Text), 10, functionObj[i].Attack);
                            break;
                        case 8:
                            player.setAttackEnhance(int.Parse(labelTime.Text), 10, functionObj[i].Attack);
                            break;
                        case 9:
                            player.setAttackEnhance(int.Parse(labelTime.Text), 10, functionObj[i].Attack);
                            break;
                    }
                    if (Life >= initalLife)
                        Life = initalLife;
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
                player.setOPEndTime(int.Parse(labelTime.Text), 2);
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
                            flag = (flag == 0.314) ? 0 : 0.314;
                            double rlx = flag, rly = flag;
                            while (rlx < Math.PI * 2)
                            {
                                rlx += Math.PI / 15;
                                rly += Math.PI / 15;
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
                    case "Boss-Rec":
                            EnemyBullet ebRec;
                            int RecYRange = 120, RecXRange = 120;
                            int Reci;
                            for (Reci = 30; Reci < this.Width; Reci+=RecXRange)
                            {
                                    ebRec = new EnemyBullet(Reci, -10);
                                    ebRec.SetV(0, 1);
                                    ebRec.setImage("SkySignleCircle");
                                if (enemyBullet.LastIndexOf(null) != -1)
                                    enemyBullet.Insert(enemyBullet.LastIndexOf(null), ebRec);
                                else
                                    enemyBullet.Add(ebRec);
                            }

                            for (Reci = 75; Reci < this.Width; Reci += RecXRange)
                            {
                                ebRec = new EnemyBullet(Reci, 625);
                                ebRec.SetV(0, -1);
                                ebRec.setImage("BlueSignleCircle");
                                if (enemyBullet.LastIndexOf(null) != -1)
                                    enemyBullet.Insert(enemyBullet.LastIndexOf(null), ebRec);
                                else
                                    enemyBullet.Add(ebRec);
                            }

                            for (Reci = 75; Reci < this.Height; Reci += RecYRange)
                            {

                                ebRec = new EnemyBullet(0, Reci);
                                ebRec.SetV(1, 0);
                                ebRec.setImage("GreenSignleCircle");
                                RecFlipY = false;
                                if (enemyBullet.LastIndexOf(null) != -1)
                                    enemyBullet.Insert(enemyBullet.LastIndexOf(null), ebRec);
                                else
                                    enemyBullet.Add(ebRec);
                            }

                            for (Reci = 30; Reci < this.Height; Reci += RecYRange)
                            {
                                ebRec = new EnemyBullet(600, Reci);
                                ebRec.SetV(-1, 0);
                                ebRec.setImage("PurpleSignleCircle");
                                RecFlipY = false;
                                if (enemyBullet.LastIndexOf(null) != -1)
                                    enemyBullet.Insert(enemyBullet.LastIndexOf(null), ebRec);
                                else
                                    enemyBullet.Add(ebRec);
                            }
                            
                            if(Randomizer.Next(0,38)>20)
                            {
                                ebRec = new EnemyBullet(xy.X, xy.Y);
                                Vector2D bulletRECV = e.getVelocity(player.lx, player.ly);
                                ebRec.SetV(bulletRECV.x, bulletRECV.y);
                                ebRec.setImage("SkyRoundCircle");
                                if (enemyBullet.LastIndexOf(null) != -1)
                                    enemyBullet.Insert(enemyBullet.LastIndexOf(null), ebRec);
                                else
                                    enemyBullet.Add(ebRec);
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
                /*
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
                 */
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
                if (context[countContext] != "")
                {
                    labelContext.Text = context[countContext];
                    countContext++;
                }
                if (context[countContext] == "")
                {
                    labelContext.Visible = false;
                    Update.Start();
                    FunctionObjTimer.Start();
                    gameTime.Start();
                    countContext++;
                }
                
                if (countContext == context.Length)
                {
                    countContext = context.Length;
                }
                    
            } 
        }

    }
}
