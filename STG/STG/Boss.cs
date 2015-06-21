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
    class Boss : Enemy
    {
        Boolean disposed = false;//for destructor.
        Boolean LockMove = false;
        private Boolean flip;//used in function FixY();
        private int ImageClock, ImageClockLimit;
        private Boolean StallLock;//used in function Stall();
        private Boolean RestoreLock;//used in function RestoreVelocity();
        private double RestoreVx;//used in function RestoreVelocity();
        private double RestoreVy;//used in function RestoreVelocity();
        private String[] BulletString = {"","","","","","","",""};
        private String[] ShootMode = { "", "", "Boss-Round", "Boss-Cross", "Round", "", "", "" };
        private int nowMode;
        private int SLR;//S for stand,L for Left,R for Right.
        private Image[] S, L, R;
        private int So, Lo, Ro;//Image order for S,L,R;Used in Function ChangeImage();


        public Boss(int x, int y)
            : base(x, y)
        {
            RestoreLock = false;
            StallLock = true;
            flip = true;
            ImageClockLimit = 7;
            nowMode = 4;
            So = 1;
            lx = x;
            ly = y;
            vx = 4.5;
            vy = 1.8;
            setClock();
            //loadImage();
            setBossImage();
            imgAutoSize();
            Shootmode = "Round";
            health = 4500;
        }

        public void changeMode()
        {
            int newMode = health/1000;
            if(newMode!=nowMode)
            {
                nowMode = newMode;
                modeFix(nowMode);
            }
        }

        private void modeFix(int m)
        {
            Shootmode = ShootMode[m];
            switch(m)
            {
                case 4:
                    break;
                case 3:
                    clock = 0;
                    clockLimit = 20;
                    bulletNum = 3;
                    bulletEachTime = 3;
                    bulletRestoreClock = 0;
                    bulletRestoreLimit = 100;
                    break;
                case 2:
                    break;
                case 1:
                    break;
                case 0:
                    break;
            }
        }

        protected override void setClock()
        {
            //f = frame = timer interval of FixUpdate
            clock = 0;
            clockLimit = 20;//每隔 40f 發射一顆子彈
            bulletNum = 3;
            bulletEachTime = 3;//每次射擊都會有 3 發子彈
            bulletRestoreClock = 0;
            bulletRestoreLimit = 200;//每隔 bulletRestoreLimit f 進行一次射擊
            move = 0;
            moveLimit = 0;//每隔 1f 可以移動 p+vx,p+vy.
        }

        public override void Move()
        {
            lx += vx;
            ly += vy;
            changeMode();
            FixY();
            FixX();
            img.Location = new Point(Convert.ToInt32(lx), Convert.ToInt32(ly));
            img.BackColor = Color.Transparent;
        }

        private void FixY()
        {
            if (flip)
            {
                vy += 0.01;
                if (vy > 1)
                {
                    flip = false;
                }
            }
            else
            {
                vy -= 0.01;
                if (vy < -1)
                {
                    flip = true;
                }
            }

        }

        private void FixX()
        {
            if (lx > 500)
                vx = -4.5;
            else if (lx < 20)
                vx = 4.5;
        }

        private void setEnemyImage()
        {
            img = new System.Windows.Forms.PictureBox();
            img.Location = img.Location = new Point(Convert.ToInt32(lx), Convert.ToInt32(ly));
            S = new Image[6];
            String s;
            int i;
            for (i = 1; i < 5; i++)
            {
                s = "\\assest\\Enemy\\LighterS" + i + ".png";
                S[i] = Image.FromFile(Application.StartupPath + s);
            }
            img.Image = S[1];
        }


        public override Boolean canMove()
        {
            move++;
            if (move > moveLimit)
            {
                move = 0;
                return true;
            }
            return false;
        }

        public override Boolean canShoot()
        {
            adjustTimeInterval();
            clock++;
            if (clock > clockLimit && bulletNum > 0)
            {
                Stall();
                clock = 0;
                bulletNum--;
                if (bulletNum == 0)
                {
                    //RestoreVelocity();
                }
                return true;
            }
            return false;
        }

        /*private void Stall()
        {
            if (StallLock)
            {
                RestoreLock = true;
                StallLock = false;
                RestoreVx = vx;
                RestoreVy = vy;
                vx = 0;
                vy = 0;
            }
        }

        private void RestoreVelocity()
        {
            if (RestoreLock)
            {
                StallLock = true;
                RestoreLock = false;
                vx = RestoreVx;
                vy = RestoreVy;
            }
        }*/

        //Dispose method
        protected override void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }

        private void setBossImage()
        {

            img = new System.Windows.Forms.PictureBox();
            img.Location = img.Location = new Point(Convert.ToInt32(lx), Convert.ToInt32(ly));

            S = new Image[7];
            L = new Image[6];
            R = new Image[5];
            String s;
            int i;
            for (i = 1; i < 6; i++)
            {
                s = "\\assest\\Enemy\\ymS" + i + ".png";
                S[i] = Image.FromFile(Application.StartupPath + s);
            }
            for (i = 1; i < 5; i++)
            {
                s = "\\assest\\Enemy\\ymL" + i + ".png";
                L[i] = Image.FromFile(Application.StartupPath + s);
            }
            for (i = 1; i < 4; i++)
            {
                s = "\\assest\\Enemy\\ymR" + i + ".png";
                R[i] = Image.FromFile(Application.StartupPath + s);
            }
            img.Image = S[1];
        }

        private void setSLR()
        {
            int oldSLR = SLR;
            if(vx==0)
            {
                SLR = 0;
            }
            else if(vx<0)
            {
                SLR = 1;
            }
            else
            {
                SLR = 2;
            }
            if(oldSLR!=SLR)
            {
                ResetImageOrder();
            }
        }

        private void ResetImageOrder()
        {
            ImageClock = ImageClockLimit;
            So = 1;
            Lo = 1;
            Ro = 1;
        }

        public override void ChangeImage()
        {
            ImageClock++;
            if (ImageClock > ImageClockLimit)
            {
                setSLR();
                ImageClock = 0;
                switch (SLR)
                {
                    case 0:
                        if (So > 5)
                            So = 1;
                        img.Image = S[So];
                        So++;
                        break;
                    case 1:
                        img.Image = L[Lo];
                        if (Lo < 2)
                            Lo++;
                        break;
                    case 2:
                        img.Image = R[Ro];
                        if (Ro < 2)
                            Ro++;
                        break;
                }
            }
        }

        ~Boss()
        {
            Dispose(false);
        }
    }
}
