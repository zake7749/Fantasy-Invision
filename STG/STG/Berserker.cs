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
    class Berserker : Enemy
    {
        bool disposed = false;
        private int ImageClock, ImageClockLimit, So;
        private Image[] S;
        private Boolean flip;//used in function FixY();
        private Double LockLx;
        private Double LockLy;
        private Boolean LockPlace;

        public Berserker(int x,int y) : base(x,y)
        {
            ImageClockLimit = 7;
            So = 1;
            flip = true;
            lx = x;
            ly = y;
            vx = 0;
            vy = 1;
            setClock();
            //loadImage();
            setEnemyImage();
            imgAutoSize();
            Shootmode = "Berserk";
            health = 100;
        }

        protected override void setClock()
        {
            //f = frame = timer interval of FixUpdate
            LockPlace = true;
            clock = 0;
            clockLimit = 8;//每隔 20f 發射一顆子彈
            bulletNum = 20;
            bulletEachTime = 20;//每次射擊都會有 20 發子彈
            bulletRestoreClock = 0;
            bulletRestoreLimit = 500;//每隔 bulletRestoreLimit f 進行一次射擊
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

        private void FixY()
        {
            if (flip)
            {
                vy += 0.01;
                if (vy > 1.5)
                {
                    flip = false;
                }
            }
            else
            {
                vy -= 0.01;
                if (vy < -1.5)
                {
                    flip = true;
                }
            }

        }

        private void FixX()
        {
            if (lx > 500)
                vx = -3;
            else if (lx < 20)
                vx = 3;
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

       public override Vector2D getVelocity(double px, double py)
       {
           Random robj = new Random();
           if(bulletNum>=0)
           {
               LockLocation(px, py);
           }
           else
           {
               LockPlace = true;
           }
           double bux = (LockLx - lx);
           double buy = (LockLy - ly);

           //speed normaliziation.
           double normal = Math.Sqrt(Math.Pow(bux, 2) + Math.Pow(buy, 2));
           bux /= normal;
           buy /= normal;

           //set Velocity as 4 pixel/fs;
           bux *= 4;
           buy *= 4;

           //Randomize.
           bux += 1.5*(robj.NextDouble());
           buy += 1.5*(robj.NextDouble());

           Vector2D bulletVelocity = new Vector2D(bux, buy);
           return bulletVelocity;
       }

       private void LockLocation(Double px,Double py) 
       {
           if(LockPlace)
           {
               LockPlace = false;
               LockLx = px;
               LockLy = py;
           }
       }

       ~Berserker()
       {
          Dispose(false);
       }
    }
}
