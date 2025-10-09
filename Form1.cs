using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace _2DSG
{
    public partial class Form1 : Form
    {
        List<Player> players = new List<Player>();
        List<Bullet> bullets = new List<Bullet>();
        List<Platform> platforms = new List<Platform>();
        Timer gameTimer = new Timer();
        private const string MapFilePath = "Assets/Data/Maps.txt";

        // Movement flags for each player (expandable)
        List<bool> pLeft = new List<bool>();
        List<bool> pRight = new List<bool>();
        List<bool> pJump = new List<bool>();
        List<bool> pFire = new List<bool>();

        public Form1()
        {
            InitializeComponent();

            // Set fixed large window size
            this.ClientSize = new Size(1280, 720);

            // Load all maps from the text file
            List<Map> allMaps = Map.LoadAll(MapFilePath);

            // Select a map (e.g., the first one)
            Map selectedMap = allMaps[0]; // You can add a selector UI later

            // Create players and movement flags
            int w = this.ClientSize.Width;
            int h = this.ClientSize.Height;
            string[] playerColors = { "Red", "Blue", "Green", "Yellow" }; // Colors for players
            int i = 0;
            foreach (var start in selectedMap.PlayerStarts)
            {
                int px = (int)(start.X * w);
                int py = (int)(start.Y * h);
                var player = new Player(px, py, 100, playerColors[i]); // Default health/color for now
                players.Add(player);
                this.Controls.Add(player.Sprite);
                this.Controls.Add(player.Health.Sprite);
                i++;

                // Initialize movement flags for each player
                pLeft.Add(false);
                pRight.Add(false);
                pJump.Add(false);
                pFire.Add(false);
            }

            // Create platforms from the selected map
            platforms = selectedMap.CreatePlatforms(this.ClientSize.Width, this.ClientSize.Height);
            foreach (var platform in platforms)
            {
                this.Controls.Add(platform.Sprite);
            }

            // Put Image for background (very laggy in winforms within the limits i got)
            //if (!string.IsNullOrEmpty(selectedMap.BackgroundImage))
            //{
            //    this.BackgroundImage = Image.FromFile(selectedMap.BackgroundImage);
            //    this.BackgroundImageLayout = ImageLayout.Stretch;
            //}

            // setup game loop
            gameTimer.Interval = 20;
            gameTimer.Tick += GameLoop;
            gameTimer.Start();

            this.KeyDown += KeyIsDown;
            this.KeyUp += KeyIsUp;
        }

        private void GameLoop(object sender, EventArgs e)
        {
            // Player movement
            for (int i = 0; i < players.Count; i++)
            {
                if (pLeft[i]) players[i].MoveLeft();
                if (pRight[i]) players[i].MoveRight();
                if (pJump[i] && !players[i].Jumping)
                {
                    players[i].Jumping = true;
                    players[i].JumpSpeed = -players[i].Force * 2;
                }
            }

            // Physics
            foreach (var player in players)
                player.ApplyGravity();

            // Bullets
            foreach (var b in bullets)
                b.Move();

            // Shooting (same keys for all players, can be expanded)
            for (int i = 0; i < players.Count; i++)
            {
                if (pFire[i] && Bullet.CanFire())
                {
                    int direction = players[i].FacingRight ? 1 : -1;
                    var bullet = new Bullet(
                        players[i].FacingRight ? players[i].Sprite.Left + players[i].Sprite.Width : players[i].Sprite.Left - 10,
                        players[i].Sprite.Top + players[i].Sprite.Height / 2,
                        direction,
                        players[i]
                    );
                    bullets.Add(bullet);
                    this.Controls.Add(bullet.Sprite);
                    Bullet.RegisterFire();
                }
            }

            // Collisions
            foreach (var player in players)
                CheckCollisions(player);

            CheckBulletPlayerCollisions();

            // Health bar settings
            int healthBarWidth = 60;
            int healthBarHeight = 8;

            // Update health bar positions and colors
            foreach (var player in players)
            {
                player.Health.Sprite.Width = (int)(healthBarWidth * (double)player.Health.Current / player.Health.Max);
                player.Health.Sprite.Height = healthBarHeight;
                player.Health.Sprite.Left = player.Sprite.Left + (player.Sprite.Width - healthBarWidth) / 2;
                player.Health.Sprite.Top = player.Sprite.Top - healthBarHeight - 5;
                player.Health.Sprite.BackColor = player.Health.Current > 0 ? Color.Green : Color.Red;
            }

            // Game Over
            GameOver();
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            // For now, same keys for all players
            if (e.KeyCode == Keys.A) pLeft[0] = true;
            if (e.KeyCode == Keys.D) pRight[0] = true;
            if (e.KeyCode == Keys.W) pJump[0] = true;
            if (e.KeyCode == Keys.X) pFire[0] = true;

            if (players.Count > 1)
            {
                if (e.KeyCode == Keys.Left) pLeft[1] = true;
                if (e.KeyCode == Keys.Right) pRight[1] = true;
                if (e.KeyCode == Keys.Up) pJump[1] = true;
                if (e.KeyCode == Keys.LButton) pFire[1] = true;
            }
            // Add more controls for more players as needed
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) pLeft[0] = false;
            if (e.KeyCode == Keys.D) pRight[0] = false;
            if (e.KeyCode == Keys.W) pJump[0] = false;
            if (e.KeyCode == Keys.X) pFire[0] = false;

            if (players.Count > 1)
            {
                if (e.KeyCode == Keys.Left) pLeft[1] = false;
                if (e.KeyCode == Keys.Right) pRight[1] = false;
                if (e.KeyCode == Keys.Up) pJump[1] = false;
                if (e.KeyCode == Keys.LButton) pFire[1] = false;
            }
            // Add more controls for more players as needed
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            // Example: left mouse button fires for player 2 (index 1)
            if (e.Button == MouseButtons.Left && players.Count > 1)
            {
                pFire[1] = true;
            }
            // Add more logic for additional players if needed
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Left && players.Count > 1)
            {
                pFire[1] = false;
            }
            // Add more logic for additional players if needed
        }

        private void CheckCollisions(Player player)
        {
            bool landed = false;

            foreach (var platform in platforms)
            {
                Rectangle playerRect = player.Sprite.Bounds;
                Rectangle platRect = platform.Sprite.Bounds;

                // Only check for landing if the player is falling
                bool isFalling = player.JumpSpeed >= 0;

                // Calculate where the player would be after this frame
                int nextBottom = playerRect.Bottom + player.JumpSpeed;

                // Check if player is above the platform and will cross its top boundary
                bool willLandOnTop =
                    isFalling &&
                    playerRect.Bottom <= platRect.Top && // Was above the platform
                    nextBottom >= platRect.Top &&        // Will cross the top
                    playerRect.Right > platRect.Left + 5 && // Not too far left
                    playerRect.Left < platRect.Right - 5;   // Not too far right

                if (willLandOnTop)
                {
                    // Land on platform
                    player.Jumping = false;
                    player.JumpSpeed = 0;
                    player.Sprite.Top = platRect.Top - player.Sprite.Height;
                    landed = true;
                    break;
                }
            }

            if (!landed)
            {
                player.Jumping = true;
            }
        }

        private void CheckBulletPlayerCollisions()
        {
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                var b = bullets[i];

                // Check collision with each player
                for (int p = 0; p < players.Count; p++)
                {
                    if (b.Owner != players[p] && b.Sprite.Bounds.IntersectsWith(players[p].Sprite.Bounds))
                    {
                        players[p].Health.TakeDamage();
                        this.Controls.Remove(b.Sprite);
                        bullets.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        private void GameOver()
        {
            int aliveCount = 0;
            Player lastAlive = null;
            foreach (var player in players)
            {
                if (!player.Health.IsDead)
                {
                    aliveCount++;
                    lastAlive = player;
                }
                if (player.Sprite.Top > this.ClientSize.Height)
                {
                    gameTimer.Stop();
                    MessageBox.Show("Game Over! A player fell off the map.");
                    return;
                }
            }
            if (aliveCount <= 1)
            {
                gameTimer.Stop();
                string message = aliveCount == 1 ? "We have a winner!" : "It's a draw!";
                if (lastAlive != null)
                {
                    message += $"\nWinner is the {lastAlive.Sprite.BackColor.Name} player!";
                }
                MessageBox.Show(message, "Game Over");
                this.Close(); // Close the game window
            }
        }
    }
}