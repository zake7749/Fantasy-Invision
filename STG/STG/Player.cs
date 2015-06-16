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
    private int SLR;//S for stand,L for Left,R for Right.
    private int ImageClock, ImageClockLimit;
    private Image[] S, L, R;
    private int So, Lo, Ro;//Image order for S,L,R;Used in Function ChangeImage();
    public Player(int x, int y)
        : base(x, y)
    {
        health = 10;
        lx = x;
        ly = y;
        //f = frame = timer interval of FixUpdate 
        clock = 0;
        clockLimit = 7;//每隔 10f 有一發子彈
        ImageClock = 0;
        ImageClockLimit = 10;
        move = 0;
        moveLimit = 0;//每隔 1f 可以移動 p+vx,p+vy.
        vx = 0;
        vxupLimit = 3;//x軸速度在 3~-3
        vxdownLimit = -3;
        vy = 0;
        vyupLimit = 3;//y軸速度在 3~-3
        vydownLimit = -3;
        setPlayerImage();
        img = new System.Windows.Forms.PictureBox();
        img.Location = new Point(Convert.ToInt32(lx), Convert.ToInt32(ly));
        img.Image = S[1];
        img.BackColor = Color.Transparent;
        imgAutoSize();
        ResetImageOrder();
    }

    private void setPlayerImage()
    {
        S = new Image[9];
        L = new Image[3];
        R = new Image[3];
        String s;
        int i;
        for(i=1;i<9;i++)
        {
            s = "\\assest\\playerS"+i+".png";
            S[i] = Image.FromFile(Application.StartupPath + s);
        }
        for(i=1;i<3;i++)
        {
            s = "\\assest\\playerL" + i + ".png";
            L[i] = Image.FromFile(Application.StartupPath + s);
        }
        for (i=1;i<3;i++)
        {
            s = "\\assest\\playerR" + i + ".png";
            R[i] = Image.FromFile(Application.StartupPath + s);
        }
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

    public void setSLR(int mode)
    {
        SLR = mode;
        ResetImageOrder();
    }

    private void ResetImageOrder()
    {
        ImageClock = ImageClockLimit;
        So = 1;
        Lo = 1;
        Ro = 1;
    }

    public void ChangeImage()
    {
        ImageClock++;
        if (ImageClock > ImageClockLimit)
        {
            ImageClock = 0;
            switch (SLR)
            {
                case 0:

                    if (So > 8)
                        So = 1;
                    img.Image = S[So];
                    So++;
                    break;
                case 1:
                    if (Lo > 2)
                        Lo = 1;
                    img.Image = L[Lo];
                    Lo++;

                    break;
                case 2:
                    if (Ro > 2)
                        Ro = 1;
                    img.Image = R[Ro];
                    Ro++;
                    break;
            }
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