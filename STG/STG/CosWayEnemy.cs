using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    class CosWayEnemy : Enemy
    {
        public CosWayEnemy(int x, int y)
            : base(x, y)
        {
            lx = x;
            ly = 0;
            vx = 0;
            vy = 1;
            newsetClock();
            loadImage();
            Shootmode = "Cos";
            health = 10;
        }

        private void newsetClock()
        {
            clock = 0;
            clockLimit = 1;//每隔 20f 發射一顆子彈
            bulletNum = 20;
            bulletEachTime = 20;//每次射擊都會有 20 發子彈
            bulletRestoreClock = 0;
            bulletRestoreLimit = 100;//每隔 bulletRestoreLimit f 進行一次射擊
            move = 0;
            moveLimit = 0;//每隔 1f 可以移動 p+vx,p+vy.      
        }
    }
}