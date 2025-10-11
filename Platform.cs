using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2DSG
{
    public class Platform
    {
        public PictureBox Sprite { get; set; }

        public Platform(int x, int y, int width, int height)
        {
            Sprite = new PictureBox();
            Sprite.Size = new Size(width, height);
            Sprite.BackColor = Color.Gray;
            Sprite.Left = x;
            Sprite.Top = y;
        }
    }
}
