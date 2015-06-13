using System;
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
    class Fighter : Enemy
    {
        Boolean flip;

        public Fighter(int x,int y):base(x,y)
        {
            flip = true;
            lx = x;
            ly = y;
            vx = 0;
            vy = 1.8;
            setClock();
            loadImage();
            imgAutoSize();
            Shootmode = "Split-5";
            health = 10;
        }

        protected void setClock()
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
    }
}
