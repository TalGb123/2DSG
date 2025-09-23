using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2DSG
{
    public partial class Form1 : Form
    {
        Player player;
        List<Bullet> bullets = new List<Bullet>();
        List<Platform> platforms = new List<Platform>();
        Timer gameTimer = new Timer();

        bool goLeft, goRight;

        public Form1()
        {
            InitializeComponent();

            // create player
            player = new Player(50, 350);
            this.Controls.Add(player.Sprite);

            // create platforms
            var ground = new Platform(0, 400, 800, 50);
            var floating = new Platform(200, 300, 120, 20);
            platforms.Add(ground);
            platforms.Add(floating);

            foreach (var p in platforms)
                this.Controls.Add(p.Sprite);

            // setup game loop
            gameTimer.Interval = 20;
            gameTimer.Tick += GameLoop;
            gameTimer.Start();

            this.KeyDown += KeyIsDown;
            this.KeyUp += KeyIsUp;
        }

        private void GameLoop(object sender, EventArgs e)
        {
            // 1. Process input
            if (goLeft) player.MoveLeft();
            if (goRight) player.MoveRight();

            // 2. Update physics
            player.ApplyGravity();

            // 3. Update bullets
            foreach (var b in bullets)
                b.Move();

            // 4. Detect collisions (landing)
            CheckCollisions();
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
                goLeft = true;
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
                goRight = true;
            if (e.KeyCode == Keys.W && !player.Jumping)
            {
                player.Jumping = true;
                player.JumpSpeed = -player.Force * 2;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
                goLeft = false;
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
                goRight = false;
        }

        private void CheckCollisions()
        {
            bool onPlatform = true;

            foreach (var platform in platforms)
            {
                Rectangle playerRect = player.Sprite.Bounds;
                Rectangle platRect = platform.Sprite.Bounds;
                onPlatform = true;

                if (playerRect.IntersectsWith(platRect) && player.JumpSpeed >= 0)
                {
                    if (playerRect.Bottom <= platRect.Top + player.JumpSpeed)
                    {
                        player.Jumping = false;
                        player.JumpSpeed = 0;
                        player.Sprite.Top = platRect.Top - player.Sprite.Height;
                        onPlatform = false;
                    }
                }
            }

            if (!onPlatform)
            {
                player.Jumping = true;
            }
        }
    }
}
