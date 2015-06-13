public class GunTurret : Enemy
{
    public GunTurret(int x, int y)
        : base(x, y)
    {
        lx = x;
        ly = 0;
        vx = 0;
        vy = 1;
        setClock();
        loadImage();
        Shootmode = "Ray";
        health = 10;
    }

    /*public Vector2D getVelocity(int px, int py)
    {
        double bux = (px - Convert.ToInt32(lx))/100;
        double buy = (py - Convert.ToInt32(ly))/100;
        Vector2D bulletVelocity = new Vector2D(bux, buy);
        return bulletVelocity;
    }*/

    private int getGCD(int a, int b)
    {
        int c;
        while (a != 0)
        {
            c = a;
            a = b % a;
            b = c;
        }
        return b;
    }

}