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


public class Player : GameObject
{
    public Player(int x, int y)
        : base(x, y)
    {
        health = 10;
        lx = x;
        ly = y;
        //f = frame = timer interval of FixUpdate 
        clock = 0;
        clockLimit = 10;//每隔 10f 有一發子彈
        move = 0;
        moveLimit = 0;//每隔 1f 可以移動 p+vx,p+vy.
        vx = 0;
        vxupLimit = 3;//x軸速度在 3~-3
        vxdownLimit = -3;
        vy = 0;
        vyupLimit = 3;//y軸速度在 3~-3
        vydownLimit = -3;
        img = new System.Windows.Forms.PictureBox();
        img.Location = new Point(Convert.ToInt32(lx), Convert.ToInt32(ly));
        img.Image = Image.FromFile(Application.StartupPath + "\\assest\\player.png");
        img.BackColor = Color.Transparent;
        imgAutoSize();
    }

    public void addV(int ax, int ay)
    {
        if (ax != 0)
        {
            if (ax > 0 && vx < vxupLimit)
                vx += ax;
            if (ax < 0 && vx > vxdownLimit)
                vx += ax;
        }
        else
        {
            if (ay > 0 && vy < vyupLimit)
                vy += ay;
            if (ay < 0 && vy > vydownLimit)
                vy += ay;
        }
    }

    public void Move()
    {
        if (lx + vx > 0 && lx + vx < 500)
            lx += vx;
        if (ly + vy > 0 && ly + vy < 600)
            ly += vy;
        img.Location = img.Location = new Point(Convert.ToInt32(lx), Convert.ToInt32(ly));
        img.BackColor = Color.Transparent;
    }
}