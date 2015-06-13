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


public class GunTurret : Enemy
{
    public GunTurret(int x, int y)
        : base(x, y)
    {
        lx = x;
        ly = y;
        vx = 0;
        vy = 2;
        setClock();
        loadImage();
        imgAutoSize();
        Shootmode = "Ray";
        health = 10;
    }

    protected void setClock()
    {
        //f = frame = timer interval of FixUpdate
        clock = 0;
        clockLimit = 15;//每隔 30f 發射一顆子彈
        bulletNum = 5;
        bulletEachTime = 5;//每次射擊都會有 2 發子彈
        bulletRestoreClock = 0;
        bulletRestoreLimit = 175;//每隔 bulletRestoreLimit f 進行一次射擊
        move = 0;
        moveLimit = 0;//每隔 1f 可以移動 p+vx,p+vy.
    }

    public override void Move()
    {
        lx += vx;
        ly += vy;
        if(vy>0.3)
            vy -= 0.005;
        img.Location = new Point(Convert.ToInt32(lx), Convert.ToInt32(ly));
        img.BackColor = Color.Transparent;
    }

    public override Vector2D getVelocity(double px, double py)
    {
        double bux = (px - lx);
        double buy = (py - ly);
        
        //speed normaliziation.
        double normal = Math.Sqrt(Math.Pow(bux,2) + Math.Pow(buy,2));
        bux /= normal;
        buy /= normal;

        //set Velocity as 10 pixel/fs;
        bux *= 10;
        buy *= 10;

        Vector2D bulletVelocity = new Vector2D(bux, buy);
        return bulletVelocity;
    }

}