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


public class Enemy : GameObject
{
    bool disposed = false;

    public String Shootmode;
    public String Movemode;
    public Boolean isCritical;
    protected int bulletNum;
    protected int bulletEachTime;
    protected int bulletRestoreLimit;
    protected int bulletRestoreClock;
    protected String[] imageFile = { "eveS1.png", "eveS2.png", "eveS3.png"};
    public Enemy(int x, int y)
        : base(x, y)
    {
        lx = x;
        ly = y;
        vx = 0;
        vy = 1;
        setClock();
        loadImage();
        imgAutoSize();
        Shootmode = "Line";
        Movemode = "Line";
        health = 10;
    }

    protected virtual void setClock()
    {
        //f = frame = timer interval of FixUpdate
        clock = 0;
        clockLimit = 10;//每隔 20f 發射一顆子彈
        bulletNum = 1;
        bulletEachTime = 1;//每次射擊都會有 1 發子彈
        bulletRestoreClock = 0;
        bulletRestoreLimit = 175;//每隔 bulletRestoreLimit f 進行一次射擊
        move = 0;
        moveLimit = 0;//每隔 1f 可以移動 p+vx,p+vy.
    }

    protected virtual void loadImage()
    {
        Random robj = new Random();
        img = new System.Windows.Forms.PictureBox();
        img.Location = img.Location = new Point(Convert.ToInt32(lx), Convert.ToInt32(ly));
        img.Image = Image.FromFile(Application.StartupPath + "\\assest\\Enemy\\" + imageFile[robj.Next(0,2)]);
        img.BackColor = Color.Transparent;
    }

    public virtual Boolean canShoot()
    {
        adjustTimeInterval();
        clock++;
        if (clock > clockLimit && bulletNum > 0)
        {
            clock = 0;
            bulletNum--;
            return true;
        }
        return false;
    }

    protected virtual void adjustTimeInterval()
    {
        bulletRestoreClock++;
        if (bulletRestoreClock > bulletRestoreLimit)
        {
            bulletRestoreClock = 0;
            bulletNum = bulletEachTime;
        }
    }

    public String getShootMode()
    {
        return Shootmode;
    }

    public virtual Vector2D getVelocity(double px, double py)
    {
        Vector2D bulletVelocity = new Vector2D(px, py);
        return bulletVelocity;
    }

    public virtual void Stall()
    {

    }

    public virtual void ChangeImage()
    {

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

       ~Enemy()
       {
          Dispose(false);
       }
}