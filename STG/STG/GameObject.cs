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

public class GameObject : IDisposable
{
    public double lx, ly;//location
    public double vx, vy;//velocity
    public int health;
    public int vxupLimit, vyupLimit;//
    public int vxdownLimit, vydownLimit;//
    public int clock, clockLimit;//for update
    public int move, moveLimit;//for update
    public PictureBox img;

    //Variable for dispose method
    bool disposed = false;

    public GameObject(int x, int y)
    {

    }

    public Boolean canShoot()
    {
        clock++;
        if (clock > clockLimit)
        {
            clock = 0;
            return true;
        }
        return false;
    }

    public Point getShootPlace()
    {
        Point p = new Point(Convert.ToInt32(lx + 20), Convert.ToInt32(ly));
        return p;
    }

    public virtual Boolean canMove()
    {
        move++;
        if (move > moveLimit)
        {
            move = 0;
            return true;
        }
        return false;
    }

    public virtual void Move()
    {
        lx += vx;
        ly += vy;
        img.Location = new Point(Convert.ToInt32(lx), Convert.ToInt32(ly));
        img.BackColor = Color.Transparent;
    }

    public void SetV(double x, double y)
    {
        vx = x;
        vy = y;
    }

    public Boolean isAlive()
    {
        if (health <= 0)
            return false;
        else
            return true;
    }

    protected void imgAutoSize()
    {
        img.Width = img.Image.Width;
        img.Height = img.Image.Height;
    }

    //Dispose Method
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    protected virtual void Dispose(bool disposing)
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

   ~GameObject()
   {
      Dispose(false);
   }
}

public class Vector2D
{
    public double x;
    public double y;

    public Vector2D()
    {
        x = 0;
        y = 0;
    }

    public Vector2D(double x, double y)
    {
        this.x = x;
        this.y = y;
    }
}