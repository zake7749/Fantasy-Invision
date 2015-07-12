using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    class EnemyFactory
    {
        private List<Enemy> enemies;
        private Random Randomizer;
        public EnemyFactory(List<Enemy> e)
        {
            enemies = e;
            Randomizer = new Random();
        }

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
        private Bomber create_Bomber(int x, int y, Boolean ShiftMode)
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

        public void createEnemy(String way)
        {
            int i;

            Random robj = new Random();
            int[] yDeviation = { 0, -60, -120, -180, -240, -300 };
            int[] VFormation = { -120, -90, -60, -30, 0, -30, -60, -90, -120 };
            int FormationRange = 40;
            int x;
            switch (way)
            {
                case "Line-Left-Side":
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
                        create_GunTurret(x, yDeviation[i]);
                    }
                    break;

                case "V-formation-Small":
                    x = robj.Next(75, 425);
                    for (i = 3; i < 6; i++)
                    {
                        Enemy e;
                        e = create_Enemy(x, VFormation[i]);
                        e.Shootmode = "Split-3";
                        x += FormationRange;
                    }
                    break;

                case "V-formation-Normal":
                    x = robj.Next(75, 125);
                    for (i = 1; i < 8; i++)
                    {
                        create_Bomber(x, VFormation[i], false);
                        x += FormationRange;
                    }
                    break;

                case "V-formation-Large":
                    x = robj.Next(75, 100);
                    for (i = 0; i < 9; i++)
                    {
                        create_Bomber(x, VFormation[i], false);
                        x += FormationRange;
                    }
                    break;

                case "Bomber-Collision":
                    int divX = 50;
                    int divY = -30;
                    for (i = 1; i < 12; i++)
                    {
                        create_Bomber(i * divX, i * divY, true);
                    }
                    break;

                case "Simple":
                    x = robj.Next(25, 475);
                    create_CircleShootEnemy(x, 0);
                    break;
                case "Boss":
                    create_Boss(300, 20);
                    break;
                case "Fighter-Two":
                    create_Fighter(Randomizer.Next(1, 250), -10);
                    create_Fighter(Randomizer.Next(350, 600), -10);
                    break;
                case "Berserker":
                    create_Berserker(Randomizer.Next(1, 650), -10);
                    break;
                case "Lighter":
                    create_Lighter(Randomizer.Next(150, 550), -15);
                    break;
                case "Circle":
                    create_CircleShootEnemy(Randomizer.Next(0, 200), -5);
                    create_CircleShootEnemy(Randomizer.Next(200, 400), -5);
                    create_CircleShootEnemy(Randomizer.Next(400, 650), -5);
                    break;
                case "None":
                    break;
            }
        }
    }
}
