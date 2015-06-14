using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    class Bomber : Enemy
    {
        bool disposed = false;

        public Bomber(int x,int y) : base(x,y)
        {
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
