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

        public FunctionObject(int x, int y)
             : base(x, y)
        {
            lx = x;
            ly = y;
            vx = 0;
            vy = 1;
            move = 0;
            moveLimit = 0;
            DecideType();
            LoadImage(ObjType);

        }

        private void DecideType()
        {
            Random decide = new Random(Guid.NewGuid().GetHashCode());
            ObjType = decide.Next(0, 7);

        }

        private void LoadImage(int type)
        {
            img = new System.Windows.Forms.PictureBox();
            img.Location = img.Location = new Point(Convert.ToInt32(lx), Convert.ToInt32(ly));
            img.Image = Image.FromFile(Application.StartupPath + "\\assest\\FunctionObject\\" + imageFile[type]);
            img.BackColor = Color.Transparent;
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
