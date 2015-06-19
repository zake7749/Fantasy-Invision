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

public class CircleShootEnemy : Enemy
{
    bool disposed = false;
    String[] ImageFile = { "eveNS1.png", "eveNS2.png"};
    public CircleShootEnemy(int x, int y)
        : base(x, y)
    {
        lx = x;
        ly = y;
        vx = 0;
        vy = 1;
        newsetClock();
        loadImage();
        imgAutoSize();
        Shootmode = "Circle";
        health = 10;
    }

    private void newsetClock()
    {
        clock = 0;
        clockLimit = 10;//每隔 20f 發射一顆子彈
        bulletNum = 5;
        bulletEachTime = 6;//每次射擊都會有 5 發子彈
        bulletRestoreClock = 0;
        bulletRestoreLimit = 100000000;//每隔 bulletRestoreLimit f 進行一次射擊
        move = 0;
        moveLimit = 0;//每隔 1f 可以移動 p+vx,p+vy.      
    }

    protected override void loadImage()
    {
        Random robj = new Random();
        img = new System.Windows.Forms.PictureBox();
        img.Location = img.Location = new Point(Convert.ToInt32(lx), Convert.ToInt32(ly));
        img.Image = Image.FromFile(Application.StartupPath + "\\assest\\Enemy\\" + ImageFile[robj.Next(0, 1)]);
        img.BackColor = Color.Transparent;
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

       ~CircleShootEnemy()
       {
          Dispose(false);
       }
}