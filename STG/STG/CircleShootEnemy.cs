using System;

public class CircleShootEnemy : Enemy
{
    public CircleShootEnemy(int x, int y)
        : base(x, y)
    {
        lx = x;
        ly = 0;
        vx = 0;
        vy = 1;
        newsetClock();
        loadImage();
        Shootmode = "Circle";
        health = 10;
    }

    private void newsetClock()
    {
        clock = 0;
        clockLimit = 10;//每隔 20f 發射一顆子彈
        bulletNum = 5;
        bulletEachTime = 5;//每次射擊都會有 5 發子彈
        bulletRestoreClock = 0;
        bulletRestoreLimit = 100000000;//每隔 bulletRestoreLimit f 進行一次射擊
        move = 0;
        moveLimit = 0;//每隔 1f 可以移動 p+vx,p+vy.      
    }
}