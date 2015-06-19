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

public class EnemyBullet : GameObject
{
    bool disposed = false;
    //public double lx, ly;
    public int middleX, middleY;
    public int radius;
    private string MoveMode;
    public double angle;
    public int range;

    public EnemyBullet(int x, int y)
        : base(x, y)
    {
        lx = x;
        ly = y;
        //f = frame = timer interval of FixUpdate 
        move = 0;
        moveLimit = 0;//每隔 1f 可以移動 p+vx,p+vy.
        vx = 0;
        vy = 5;
        img = new System.Windows.Forms.PictureBox();
        img.Location = img.Location = new Point(Convert.ToInt32(lx), Convert.ToInt32(ly));
        img.Image = Image.FromFile(Application.StartupPath + "\\assest\\EnemyBullet\\EnemyGreenBullet.png");
        img.BackColor = Color.Transparent;
        imgAutoSize();
        MoveMode = "";
        radius = 100;
        angle = 0;
    }

    public override void Move()
    {
        if (MoveMode == "Cos")
        {
            ly += vy;
            lx += 10 * Math.Cos(angle += 0.5);
            img.Location = new Point(Convert.ToInt32(lx), Convert.ToInt32(ly));
            img.BackColor = Color.Transparent;
        }
        else if (MoveMode == "Sin")
        {
            ly += vy;
            lx -= 10 * Math.Sin(angle += 0.5);
            img.Location = new Point(Convert.ToInt32(lx), Convert.ToInt32(ly));
            img.BackColor = Color.Transparent;
        }
        else if (MoveMode == "Circle")
        {
            middleY++;
            lx = Convert.ToInt32(middleX + radius * Math.Cos(angle += 0.05));
            ly = Convert.ToInt32(middleY + radius * Math.Sin(angle += 0.05));
            img.Location = new Point(Convert.ToInt32(lx), Convert.ToInt32(ly));
            img.BackColor = Color.Transparent;
        }
        else
        {
            lx += vx;
            ly += vy;
            img.Location = new Point(Convert.ToInt32(lx), Convert.ToInt32(ly));
            img.BackColor = Color.Transparent;
        }
    }

    public void useGreenRay()
    {
        img.Image = Image.FromFile(Application.StartupPath + "\\assest\\Green_Ray.png");
        img.BackColor = Color.Transparent;
    }

    public void setXY(double ox, double oy)
    {
        middleX = (int)ox + 20;
        middleY = (int)oy + 20;
    }

    public void setMoveMode(string outstr)
    {
        MoveMode = outstr;
    }

    public string getMoveMode()
    {
        return MoveMode;
    }

    //擦彈判定 回傳為距離邊框之pixle長度 例如:16*16的圖 回傳2代表距離邊框2pixle為擦彈範圍
    public void setImage(string outstr)
    {
        //下面這一行有問題
        img.Image = Image.FromFile(Application.StartupPath + "\\assest\\EnemyBullet\\" + outstr + ".png");
        if (outstr.ToCharArray(outstr.Length - 6, 6).ToString() == "Circle")
        {
            if (outstr.ToCharArray(outstr.Length - 9, 3).ToString() == "Big")
            {
                range = 3;
            }
            else if (outstr.ToCharArray(outstr.Length - 11, 5).ToString() == "Round")
            {
                range = 4;
            }
            else if (outstr.ToCharArray(outstr.Length - 11, 5).ToString() == "Black")
            {
                range = 9;
            }
            else range = 2;
        }
        else if (outstr.ToCharArray(outstr.Length - 4, 4).ToString() == "Star")
        {
            if (outstr.ToCharArray(outstr.Length - 7, 3).ToString() == "Big")
            {
                range = 3;
            }
            else range = 2;
        }
        else if (outstr.ToCharArray(outstr.Length - 4, 4).ToString() == "Star")
        {
            range = 4;
        }
        else range = 1;
    }
    //Dispose method
    protected override void Dispose(bool disposing)
    {
        if (disposed)
            return;

        if (disposing)
        {
            // Free any other managed objects here.
            //
        }

        // Free any unmanaged objects here.
        //
        disposed = true;
    }

    ~EnemyBullet()
    {
        Dispose(false);
    }
}
