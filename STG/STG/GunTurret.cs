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
        ly = 0;
        vx = 0;
        vy = 1;
        setClock();
        loadImage();
        Shootmode = "Ray";
        health = 10;
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