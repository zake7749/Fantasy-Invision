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
    class Fighter : Enemy
    {
        Boolean disposed = false;//for destructor.

        private Boolean flip;//used in function FixY();
        private int ImageClock, ImageClockLimit,So;
        private Boolean StallLock;//used in function Stall();
        private Boolean RestoreLock;//used in function RestoreVelocity();
        private double RestoreVx;//used in function RestoreVelocity();
        private double RestoreVy;//used in function RestoreVelocity();
        private Image[] S;
        public Fighter(int x,int y):base(x,y)
        {
            RestoreLock = false;
            StallLock = true;
            flip = true;
            ImageClockLimit = 7;
            So = 1;
            lx = x;
            ly = y;
            vx = 0;
            vy = 1.8;
            setClock();
            //loadImage();
            setEnemyImage();
            imgAutoSize();
            Shootmode = "Split-5";
            health = 10;
        }

        protected override void setClock()
        {
            //f = frame = timer interval of FixUpdate
            clock = 0;
            clockLimit = 7;//每隔 14f 發射一顆子彈
            bulletNum = 3;
            bulletEachTime = 3;//每次射擊都會有 3 發子彈
            bulletRestoreClock = 0;
            bulletRestoreLimit = 175;//每隔 bulletRestoreLimit f 進行一次射擊
            move = 0;
            moveLimit = 0;//每隔 1f 可以移動 p+vx,p+vy.
        }

        public override void Move()
        {
            lx += vx;
            ly += vy;
            FixY();
            FixX();
            img.Location = new Point(Convert.ToInt32(lx), Convert.ToInt32(ly));
            img.BackColor = Color.Transparent;
        }

        private void FixY()
        {
            if(flip)
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
                vx = -1;
            else if (lx < 20)
                vx = 1;
        }

        private void setEnemyImage()
        {
            img = new System.Windows.Forms.PictureBox();
            img.Location = img.Location = new Point(Convert.ToInt32(lx), Convert.ToInt32(ly));
            S = new Image[6];
            String s;
            int i;
            for (i = 1; i < 6; i++)
            {
                s = "\\assest\\Enemy\\ButterflyS" + i + ".png";
                S[i] = Image.FromFile(Application.StartupPath + s);
            }
            img.Image = S[1];
        }

        public override void ChangeImage()
        {
            ImageClock++;
            if (ImageClock > ImageClockLimit)
            {
                ImageClock = 0;
                if (So > 5)
                    So = 1;
                    img.Image = S[So];
                    So++;
            }
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
                    RestoreVelocity();
                }
                return true;
            }
            return false;
        }

        private void Stall()
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
            if(RestoreLock)
            {
                StallLock = true;
                RestoreLock = false;
                vx = RestoreVx;
                vy = RestoreVy;
            }
        }

       //Dispose method
       protected override void Dispose(bool disposing)
       {
          if (disposed)
             return; 

          if (disposing) {
             // Free any other managed objects here.
             //
          }

          // Free any unmanaged objects here.
          //
          disposed = true;
       }


       ~Fighter()
       {
          Dispose(false);
       }
    }
}
