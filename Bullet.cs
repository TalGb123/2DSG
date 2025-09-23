using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2DSG
{
    internal class Bullet
    {
        public PictureBox Sprite { get; set; }
        public int Speed { get; set; } = 10;

        public Bullet(int x, int y)
        {
            Sprite = new PictureBox();
            Sprite.Size = new Size(10, 5);
            Sprite.BackColor = Color.Red;
            Sprite.Left = x;
            Sprite.Top = y;
        }

        public void Move()
        {
            Sprite.Left += Speed;
        }
    }
}
