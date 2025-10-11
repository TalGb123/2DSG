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
        public Player Owner { get; set; }
        public int Direction { get; set; } = 1; 

        public Bullet(int left, int top, int dir, Player owner)
        {
            Sprite = new PictureBox();
            Sprite.Size = new Size(10, 5);
            Owner = owner;
            Sprite.BackColor = Owner.Sprite.BackColor;
            Sprite.Left = left;
            Sprite.Top = top;
            Direction = dir;
        }

        public void Move()
        {
            Sprite.Left += Speed * Direction;
        }
    }
}
