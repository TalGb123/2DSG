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
        public Health Health { get; private set; }
        public bool FacingRight { get; set; } = true;
        public DateTime LastFireTime { get; set; } = DateTime.MinValue;
        public int FireDelayMs { get; set; } = 200;

        public Player(int x, int y, int maxHealth, string spriteColor)
        {
            Sprite = new PictureBox();
            Sprite.Size = new Size(40, 60);
            Sprite.BackColor = Color.FromName(spriteColor);
            Sprite.Left = x;
            Sprite.Top = y;
            Health = new Health(maxHealth);
        }

        public void MoveLeft()
        {
            Sprite.Left -= Speed;
            FacingRight = false;
        }

        public void MoveRight()
        {
            Sprite.Left += Speed;
            FacingRight = true;
        }

        public void ApplyGravity()
        {
            Sprite.Top += JumpSpeed;
            if (Jumping) JumpSpeed += 1; 
        }

        public bool CanFire()
        {
            return (DateTime.Now - LastFireTime).TotalMilliseconds >= FireDelayMs;
        }

        public void RegisterFire()
        {
            LastFireTime = DateTime.Now;
        }
    }
}
