using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Drawing;

namespace STG
{
    class FunctionObject : GameObject
    {
        Boolean disposed = false;
        private int ObjType;//0:+10~20HP, 1:+30~50HP, 2:+HP full, 3:player become invincible, 4:+100~200 Score, 5:+300~500 Score, 6:+1000 Score
        protected string[] imageFile = { "HP20.png", "HP50.png", "HPfull.png", "shield.png", "score200.png", "score500.png", "score1000.png" };
        public int Score;
        public int Life;

        public FunctionObject(int x, int y)
             : base(x, y)
        {
            lx = x;
            ly = y;
            vx = 0;
            vy = 3;
            move = 0;
            moveLimit = 0;
            Score = 0;
            Life = 0;
            DecideType();
            LoadImage(ObjType);

        }

        private void DecideType()
        {

            Random decide = new Random(Guid.NewGuid().GetHashCode());
            int probability = decide.Next(1, 21);
            switch (probability)
            {
                case 1:
                case 2:
                case 3:
                    ObjType = 0;
                    Life = 1;
                    break;
                case 4:
                case 5:
                    ObjType = 1;
                    Life = 3;
                    break;
                case 6:
                    ObjType = 2;
                    Life = 5;
                    break;
                case 7:
                case 8:
                    ObjType = 3;
                    break;
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                    ObjType = 4;
                    Score = decide.Next(50, 200);
                    break;
                case 15:
                case 16:
                case 17:
                case 18:
                    ObjType = 5;
                    Score = decide.Next(300, 600);
                    break;
                case 19:
                case 20:
                    ObjType = 6;
                    Score = Score = decide.Next(1000, 1100); 
                    break;
            }
        }

        private void LoadImage(int type)
        {
            img = new System.Windows.Forms.PictureBox();
            img.Location = img.Location = new Point(Convert.ToInt32(lx), Convert.ToInt32(ly));
            img.Image = Image.FromFile(Application.StartupPath + "\\assest\\FunctionObject\\" + imageFile[type]);
            img.BackColor = Color.Transparent;
        }

        public int getObjType()
        {
            return ObjType;
        }

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

        ~FunctionObject()
        {
            Dispose(false);
        }
    }
}
