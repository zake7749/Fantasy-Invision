using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    class Bomber : Enemy
    {
        public Bomber(int x,int y) : base(x,y)
        {
            lx = x;
            ly = y;
            vx = 0;
            vy = 4;
            setClock();
            loadImage();
            imgAutoSize();
            Shootmode = "None";
            health = 5;
        }
    }
}
