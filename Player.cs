using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2DSG
{
    internal class Player
    {
        public PictureBox Sprite { get; private set; }
        public int Speed { get; set; } = 5;
        public int JumpSpeed { get; set; } = 0;
        public bool Jumping { get; set; } = false;
        public int Force { get; set; } = 8;

        public Player(int x, int y)
        {
            Sprite = new PictureBox();
            Sprite.Size = new Size(40, 60);
            Sprite.BackColor = Color.Blue;
            Sprite.Left = x;
            Sprite.Top = y;
        }

        public void MoveLeft() => Sprite.Left -= Speed;
        public void MoveRight() => Sprite.Left += Speed;

        public void ApplyGravity()
        {
            Sprite.Top += JumpSpeed;
            if (Jumping) JumpSpeed += 1; 
        }
    }
}
