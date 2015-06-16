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

public class Bullet : GameObject
{
    public Bullet(int x, int y)
        : base(x, y)
    {
        lx = x;
        ly = y;
        //f = frame = timer interval of FixUpdate 
        move = 0;
        moveLimit = 0;//每隔 1f 可以移動 p+vx,p+vy.
        vx = 0;
        vy = -5;
        img = new System.Windows.Forms.PictureBox();
        img.Location = img.Location = new Point(Convert.ToInt32(lx), Convert.ToInt32(ly));
        img.Image = Image.FromFile(Application.StartupPath + "\\assest\\PlayerBulletMid.png");
        img.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
        img.BackColor = Color.Transparent;
        imgAutoSize();
        //img.BackColor = Color.Black;
    }
}