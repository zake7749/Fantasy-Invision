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
            Shootmode = "Split-3";
            health = 10;
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
