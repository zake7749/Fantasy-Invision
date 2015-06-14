using System;
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
    class Bomber : Enemy
    {
        bool disposed = false;
        private Boolean ShiftMode;
        public Bomber(int x,int y) : base(x,y)
        {
            ShiftMode = false;
            lx = x;
            ly = y;
            vx = 0;
            vy = 5;
            setClock();
            loadImage();
            imgAutoSize();
            Shootmode = "None";
            health = 5;
        }

        public void OpenShiftMode()
        {
            ShiftMode = true;
            vy = 1;
            vx = -3.5;
        }

        public override void Move()
        {
            if (ShiftMode)
            {
                FixX();
            }
            lx += vx;
            ly += vy;
            img.Location = new Point(Convert.ToInt32(lx), Convert.ToInt32(ly));
            img.BackColor = Color.Transparent;
        }

        private void FixX()
        {
            if (lx > 550)
                vx = -4;
            else if (lx < 20)
                vx = 4;
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

       ~Bomber()
       {
          Dispose(false);
       }
    }
}
